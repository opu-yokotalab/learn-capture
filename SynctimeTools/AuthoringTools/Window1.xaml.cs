using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
// 追加分
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Json;
using IWshRuntimeLibrary;

namespace AuthoringTools
{
    /// <summary>
    /// Window1.xaml の相互作用ロジック
    /// </summary>
    public partial class Window1 : Window
    {
        //ストップウォッチクラスsw
        public Stopwatch sw = new Stopwatch();
        //タイマの作成(このタイマ刻みの間隔の時間を取得)
        public System.Windows.Threading.DispatcherTimer timer
            = new System.Windows.Threading.DispatcherTimer();

        //フォルダパス(撮影時にファイルが作成されるフォルダ)
        public string path;
        public string sn;
        
        //撮影時、ファイル名後の連番
        int num=1;
        public string lastName = "";
        //リストボックスから選ばれた名前
        public string s;

        public int selectedindex;
        

        public Window1()
        {
            InitializeComponent();
            //常に最前面に移動
            //出来ればDebutの前面に表示位が好ましいかも
            this.Topmost = true;
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //namelist.Items.Add("生徒Ａ");
            //namelist.Items.Add("生徒Ｂ");
            //namelist.Items.Add("生徒Ｃ");

            //実行時にDebutを同時起動：フルパスで指定（ここも設定ファイルにいれていいかも）
            System.Diagnostics.Process Debue = System.Diagnostics.Process.Start(@"C:\Program Files\NCH Software\Debut\debut.exe");
            //Process.Start(@"C:\Program Files\NCH Software\Debut\debut.exe");
            //timerを200のスパン(200*100ns=20μs：2.0*10^-5)で取得
            timer.Interval = new TimeSpan(200);
            //タイマが経過するとtiner_Tickイベント
            timer.Tick += new EventHandler(timer_Tick);
            //タイマをスタートさせる
            timer.Start();
            //動画ファイルを保存するフォルダを設定ファイルから読み込む
            path = Properties.Settings.Default.defaultname;

            //被撮影者の名前一覧を取得
            //動画ファイルを保存するフォルダからname.txtを読み出す
            sn = path+"\\name.txt";
            if (System.IO.File.Exists(sn))
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(sn, System.Text.Encoding.GetEncoding(932));
                //撮影者名のリストに追加
                while (sr.Peek() > -1)
                {
                    namelist.Items.Add(sr.ReadLine());                    
                }
                sr.Close();
                //リストの書き出し
                sr = new System.IO.StreamReader(sn, System.Text.Encoding.GetEncoding(932));
                studentlist.Text = sr.ReadToEnd();
                sr.Close();
            }
            else
            {
                //初回起動は何もしない
                if (Properties.Settings.Default.defaultname == "default")
                {
                    return;
                }
                else
                {
                    System.IO.File.Create(sn);
                }
            }


            //動画を保存するフォルダ名を表示(表示が大きくなりすぎる)
            //fileName.Text=path;
        }

        //timespanで取得された時間の合計を文字列で表示し、positionに表記
        void timer_Tick(object sender, EventArgs e)
        {
            //Elapse(timespanで経過した時間の合計を取得)
            //ToString(取得時間を文字列形式で返す)ここではhh:mm:ss.nnnnnnn
            position.Text = sw.Elapsed.ToString();
        }

        

        //SyncPoint：押されるたびにposition.textの内容を表記
        private void syncPointButton_Click(object sender, RoutedEventArgs e)
        {
            syncPointList.Text += position.Text + "\n";
            syncPointList.ScrollToEnd();
        }

        //startButton：Ctrl+Shift+F11を送り、撮影ファイル名と更新日時を取得、時間の計測を開始
        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            //初回起動時は設定ボタンでフォルダを指定してもらう
            if (path == "default")
            {
                syncPointList.Text="初回起動です、設定ボタンで動画を保存するフォルダを指定してください";
                return;
            }

            //ファイル名入力がなくてもリターン
            else if (rename.Text == "")
            {
                syncPointList.Text = "撮影ファイル名を入力してください";
                return;
            }

            else
            {
                var shell = new WshShellClass();
                //objectは型を選ばず格納できる
                //object wait = 0;
                ////指定のキーをアクティブなウィンドウに送る：^はCtrlキー,+はShiftキーつまりCtrl+Shift+F11
                ////第2引数はキーストロークで行われる処理が終了するまで一時停止時間　今回は停止なし
                //shell.SendKeys("^+{F11}", ref wait);

                if (st != null)
                {
                    if (st.CanWrite)
                    {

                        Byte[] sendBytes = Encoding.UTF8.GetBytes("撮影開始");
                        //Byte[] sendBytes = Encoding.UTF8.GetBytes(s + num);
                        st.Write(sendBytes, 0, sendBytes.Length);
                        shoot();
                    }
                }
                if (st2 != null)
                {
                    if (st2.CanWrite)
                    {

                        Byte[] sendBytes = Encoding.UTF8.GetBytes("撮影開始");
                        //Byte[] sendBytes = Encoding.UTF8.GetBytes(s+num);
                        st2.Write(sendBytes, 0, sendBytes.Length);
                        shoot();
                    }
                }
                else
                {
                    shoot();
                }


                System.Threading.Thread.Sleep(100);

                //refボタンで取得したディレクトリの情報を取得
                var dirinfo = new DirectoryInfo(path);
                
                //DateTime の最小有効値(定数の値は、0001 年 1 月 1 日の 00:00:00.0000000)
                var lastTime = DateTime.MinValue;
                //更新日時の最も新しいファイル名をlastName、更新日時をlastTimeへ代入
                foreach (var value in dirinfo.GetFiles())
                {
                    if (value.LastWriteTime > lastTime)
                    {
                        lastName = value.Name;
                        lastTime = value.LastWriteTime;
                    }
                }
                //fileNameテキスト部に撮影ファイル名を代入
                //fileName.Text = lastName;

                rename.Text=s+num+"を撮影中";

                //指定した時間だけ現在のスレッドを中断(撮影側の撮影開始を押してからのタイムラグ)
                System.Threading.Thread.Sleep(1300); // 実測


                //時間の計測を開始
                sw.Reset();
                syncPointList.Text = "";
                sw.Start();
            }
        }

        //stopButton：Ctrl+Shift+F12を送り、jsonファイルを作成
        private void stopButton_Click(object sender, RoutedEventArgs e)
        {
            //swが動いていなければreturn
            if (sw.IsRunning == false)
            {
                return;
            }



            var shell = new WshShellClass();
            object wait = 0;

            //Ctrl+Shift+F12を送る
            //他と接続されていたらそれにも送る
            shell.SendKeys("^+{F12}", ref wait);
            if (st != null)
            {
                if (st.CanWrite)
                {                    
                    Byte[] sendBytes = Encoding.UTF8.GetBytes("撮影終了");
                    st.Write(sendBytes, 0, sendBytes.Length);
                }
            }
            if (st2 != null)
            {
                if (st2.CanWrite)
                {                   
                    Byte[] sendBytes = Encoding.UTF8.GetBytes("撮影終了");
                    st2.Write(sendBytes, 0, sendBytes.Length);
                }
            }


            sw.Stop();

            System.Threading.Thread.Sleep(1000);

            //renameに入力した名前にリネーム
            string ren = path + "\\" + lastName;
            string re = path + "\\" + s + num + ".wmv";
            if(System.IO.File.Exists(re)){
                System.IO.File.Delete(re);
            }
            System.IO.File.Move(ren, re);

            //jsonファイルの書き出し
            //syncListにsyncPointListを取得
            var syncList = new List<string>(syncPointList.Text.Trim().Split('\n'));
            var jsonFileName = s + num + ".json";
            
            //書式
            var outputJson = "{\n";

            //現段階ではWMVで撮影することにしか対応していない
            //ほかの形式でも対応できるようにしたい
            outputJson += "\"tag\" : \"" + s+num +".wmv" + "\",\n";
            outputJson += "\"url\" : [\"" + s+num +".wmv" + "\"],\n";
            outputJson += "\"viewpoint\" : [\"" + "正面" + "\"],\n";
            outputJson += "\"sync\" : [\n";
            foreach (var value in syncList)
            {
                //syncListの中身がなくなるまでoutputjsonに格納
                if (value != "")
                {
                    outputJson += "\"" + value + "\"";
                    //最後の同期ポイント以外は後ろに,をつける
                    if (syncList.IndexOf(value) != syncList.Count - 1)
                    {
                        outputJson += ",";
                    }
                }
                outputJson += "\n";
            }
            outputJson += "],\n";
            outputJson += "\"rectangle\" : [],\n";
            outputJson += "\"text\" : []\n";
            outputJson += "}";

            Debug.WriteLine(path);

            //jsonファイルの作成
            using (var fileJson = new StreamWriter(path + "\\" + jsonFileName))
            {
                fileJson.Write(outputJson);
            }

            num++;

            rename.Text = s + num + "の撮影待機中";
        }

        //refボタン：動画ファイルを保存するフォルダを指定する
        private void refButton_Click(object sender, RoutedEventArgs e)
        {
            //FolderBrowserDialogクラス：ユーザにフォルダを選択するように要求
            var opener = new System.Windows.Forms.FolderBrowserDialog();

            //showDialog()：既定のオーナーを使用してコモン ダイアログ ボックスを実行します。
            //"ファイルを開く"、"ファイルの保存"、"フォルダーを開く"
            //"検索と置換"、"印刷"、"ページ設定"、"フォント"、"色" の各ダイアログ ボックスで構成
            //http://msdn.microsoft.com/ja-jp/library/aa511274.aspx
            //DialogResult：ダイアログ ボックスの戻り値を示す識別子を指定します。今回の戻り値はOK
            if (opener.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }

            //SelectedPath：ユーザーが選択したパスを取得または設定します
            //ダイアログ ボックスで最初に選択するフォルダへのパス。
            //または、ユーザーが最後に選択したフォルダへのパス。
            //注：この名前空間、クラス、およびメンバは、.NET Framework Version 1.1 だけでサポートされています
            path = opener.SelectedPath;

            //設定ファイルを書き換え：撮影動画を入れるフォルダ
            Properties.Settings.Default.defaultname = path;
            Properties.Settings.Default.Save();

            string sn = path + "\\name.txt";
            if (System.IO.File.Exists(sn))
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(sn, System.Text.Encoding.GetEncoding(932));
                while (sr.Peek() > -1)
                {
                    namelist.Items.Add(sr.ReadLine());
                }
            }
            else
            {
                System.IO.File.Create(sn);
            }
        }

        //リストから選ばれた名前で撮影待機
        private void rename_TextChanged(object sender, TextChangedEventArgs e)
        {
                rename.Text = s + num + "の撮影待機中";
        }
        
        //撮影するファイル名を選択
        private void namelist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int i;
            num = 1;

            if (namelist.SelectedIndex != -1)
            {
                s = namelist.SelectedItem.ToString();
                i = s.IndexOf(':');
                s = s.Substring(i + 1);

                rename.Text = s + num + "の撮影待機中";
            }

            selectedindex = namelist.SelectedIndex;
        }


        
        //以下、接続部分

        //接続待ちスレッド
        private Thread thread = null;
        //接続待ちスレッド終了制御
        private bool bAlive;
        //スレッド終了待ちイベント
        private AutoResetEvent evt = new AutoResetEvent(false);

        public TcpClient tcp;
        public TcpClient tcp2;

        //接続時のネットワークストリーム
        public NetworkStream st = null;
        public NetworkStream st2 = null;

        public object wait = 0;

        public bool caflg=true;

        private void accept_Click(object sender, RoutedEventArgs e)
        {
            // 接続待ちのスレッドを開始
            Thread thread =
                    new Thread(new ThreadStart(ListenStart));
            // スレッドをスタート
            thread.Start();

            textBox2.Text = "接続待ち";

            caflg = false;

        }
        
        private void cance_Click(object sender, RoutedEventArgs e)
        {
            if (caflg)
            {
                return;
            }
            // スレッド制御フラグを設定
            bAlive = false;

            // スレッド終了待ち
            evt.WaitOne();

            // スレッドを破棄
            thread = null;

            caflg = true;

            textBox2.Text = "接続待ち中止";

        }

        private void ListenStart()
        {
            // Listenerの生成
            TcpListener listener = new TcpListener(IPAddress.Any, 9999);
            TcpListener listener2 = new TcpListener(IPAddress.Any, 9998);

            // 接続要求受け入れ開始
            //textBox1.Text = "接続待ち";           
            listener.Start();
            listener2.Start();

            // 終了制御フラグ
            bAlive = true;

            while (bAlive)
            {
                // 接続待ちがあるか？
                if (listener.Pending() == true)
                {
                    // 接続要求を受け入れる
                    tcp = listener.AcceptTcpClient();

                    // 接続したソケットに対する処理
                    //textBox1.Text = "接続！";

                    //// ソケットに対する処理
                    st = tcp.GetStream();
                }
                if (listener2.Pending() == true)
                {
                    // 接続要求を受け入れる
                    tcp2 = listener2.AcceptTcpClient();

                    // 接続したソケットに対する処理                    
                    //textBox1.Text = "接続！";

                    //// ソケットに対する処理
                    st2 = tcp2.GetStream();
                }
                // 少々待機
                Thread.Sleep(100);
            }

            // 接続待ち終了
            listener.Stop();
            listener2.Stop();

            // イベントをシグナル状態にする
            evt.Set();
        }


        private void shoot()
        {
            var shell = new WshShellClass();
            shell.SendKeys("^+{F11}", ref wait);
        }

        //生徒名を追加
        private void addst_Click(object sender, RoutedEventArgs e)
        {
            if (studentname.Text == "")
            {
                return;
            }

            System.IO.StreamWriter sw = new System.IO.StreamWriter(sn,true, System.Text.Encoding.GetEncoding(932));

            namelist.Items.Add(studentname.Text);
            studentlist.Text += "\n"+studentname.Text;

            sw.WriteLine(studentname.Text);
            studentname.Text = "";

            sw.Close();
        }

        //生徒名を削除:削除を外部ファイルに反映させるのがまだ
        private void delst_Click(object sender, RoutedEventArgs e)
        {
            System.IO.StreamWriter sw = new System.IO.StreamWriter(sn, true, System.Text.Encoding.GetEncoding(932));

            namelist.Items.RemoveAt(namelist.SelectedIndex);
            rename.Text = "項目を選択してください";
            

        }

        //接続待ち状態で閉じたときの対処
        private void Window_Closed(object sender, EventArgs e)
        {
            if (!caflg)
            {
                // スレッド制御フラグを設定
                bAlive = false;

                // スレッド終了待ち
                evt.WaitOne();

                // スレッドを破棄
                thread = null;
            }
            else
            {
                string s="ss";
                return;
            }
        }
    }
}
