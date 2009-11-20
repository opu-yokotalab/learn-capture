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

//追加分
using System.Windows.Ink;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using System.IO;
using System.Json;
using System.Diagnostics;


namespace testrect
{
    /// <summary>
    /// Window1.xaml の相互作用ロジック
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(this._init);
        }

        string path;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            path = Properties.Settings.Default.defaultname;
            if (path == "default")
            {
                info.Text = "動画群のあるフォルダを指定してください";
            }
            else
            {
                info.Text = Properties.Settings.Default.defaultname;
                var list = new List<string>(Properties.Settings.Default.defaultfile.Trim().Split('\n'));

                foreach (var value in list)
                {
                    if (value != "")
                    {
                        videolist.Items.Add(value);
                    }
                }
            }

            media.Play();
            media.Pause();
        }


        ///
        //演出の作成
        ///

        public double wi;
        public double he;
        public double recxst;
        public double recyst;
        public double recxen;
        public double recyen;
        public double senxst;
        public double senyst;
        public string ss;
        

        //四角の演出
        private bool dragStart = false;
        private Point dragStartPnt;
        //書き込む四角
        public Rectangle drawingRectangle;
        //演出の選択
        public Mode mode;


        public enum Mode
        {
            Sentence,
            Rectangle
        }

        private void rectdraw_Checked(object sender, RoutedEventArgs e)
        {
            mode = Mode.Rectangle;
        }

        private void textdraw_Checked(object sender, RoutedEventArgs e)
        {
            mode = Mode.Sentence;
        }

        private void rect_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            StartDragging(e.GetPosition(drawAria));
            Point str = e.GetPosition(rect);
            double x = str.X;
            double y = str.Y;
            recxst = x;
            recyst = y;
            //strleft.Text = x.ToString();
            //strtop.Text = y.ToString();
            switch (mode)
            {
                case (Mode.Rectangle):
                    break;
                case (Mode.Sentence):
                    DrawSentence(dragStartPnt);
                    senxst = x;
                    senyst = y;
                    ss = letter.Text;
                    break;
            }
        }

        private void rect_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            StopDragging();
            Point end = e.GetPosition(rect);
            double x = end.X;
            double y = end.Y;
            recxen = x;
            recyen = y;
            //endleft.Text = x.ToString();
            //endtop.Text = y.ToString();
        }

        private void rect_MouseLeave(object sender, MouseEventArgs e)
        {
            StopDragging();
        }

        private void rect_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragStart)
            {
                //drawAriaのポジションを取得
                var curPos = e.GetPosition(drawAria);
                curPos = new Point(curPos.X, curPos.Y);
                switch (mode)
                {
                    case (Mode.Rectangle):
                        DrawRectangle(dragStartPnt, curPos);
                        break;
                    case (Mode.Sentence):
                        break;
                }
            }
        }

        public TextBlock DrawSentence(Point startPnt)
        {
            var sentence = new TextBlock
            {
                FontSize=double.Parse(fontsize.Text),
                Text=letter.Text,
                Background = new SolidColorBrush(Colors.Transparent)
            };
            Canvas.SetLeft(sentence, startPnt.X);
            Canvas.SetTop(sentence, startPnt.Y);
            senxst = startPnt.X;
            senyst = startPnt.Y;

            drawAria.Children.Add(sentence);
            return sentence;
        }

        private void DrawRectangle(Point startPnt, Point endPnt)
        {
            //描く□の幅と高さ：終点の座標から始点を引いた値
            var width = endPnt.X - startPnt.X;
            var height = endPnt.Y - startPnt.Y;

            //書き込む四角に何も入っていない状態
            if (drawingRectangle == null)
            {
                //下のdrawingRectangleを代入
                drawingRectangle = DrawRectangle(startPnt.X, startPnt.Y, width, height);
            }

            //幅と高さが０未満か以上かで場合分け
            //０以上ならそのまま、未満ならwidth、heightを+(endPntからstartPntに向って描画)
            if (width < 0)
            {
                Canvas.SetLeft(drawingRectangle, startPnt.X + width);
            }
            else
            {
                Canvas.SetLeft(drawingRectangle, startPnt.X);
            }
            if (height < 0)
            {
                Canvas.SetTop(drawingRectangle, startPnt.Y + height);
            }
            else
            {
                Canvas.SetTop(drawingRectangle, startPnt.Y);
            }

            //width、heightの絶対値を返す(負の値が来る場合も考えられるので)
            drawingRectangle.Width = Math.Abs(width);
            drawingRectangle.Height = Math.Abs(height);
        }

        public Rectangle DrawRectangle(double left, double top, double width, double height)
        {
            //新しく書き込む四角の形式

            var rectangle = new Rectangle
            {
                //width、heightの絶対値を返す(負の値が来る場合も考えられるので)
                Width = Math.Abs(width),
                Height = Math.Abs(height),
                Stroke = new SolidColorBrush(Colors.Red),
                StrokeThickness = 2,
                Name="re"
            };

            Canvas.SetLeft(rectangle, left);
            Canvas.SetTop(rectangle, top);

            whset(Math.Abs(width), Math.Abs(height));
            drawAria.Children.Add(rectangle);

            return rectangle;
        }

        private void whset(double x,double y)
        {
            wi = x;
            he = y;
        }

        private void StartDragging(Point startPnt)
        {
            if (!dragStart)
            {
                //ドラッグの開始
                dragStart = true;
                dragStartPnt = startPnt;
            }
        }

        private void StopDragging()
        {
            if (dragStart)
            {
                //ドラッグの終了
                dragStart = false;
            }
        }







        ///
        //シークバーの演出
        ///

        private DispatcherTimer Timer;
        double seconds;
        private bool ststop = false;
        public static Storyboard sb = new Storyboard();
        
        
        //private bool seekclick=false;
        

        private void _init(object sender, RoutedEventArgs e)
        {
            this.Timer = new DispatcherTimer();
            //タイムスパン引数(日、時、分、秒、ミリ秒)
            Timer.Interval = new TimeSpan(200);
            Timer.Tick += new EventHandler(seekTimer_Tick);
            Timer.Start();
        }

        private void seekTimer_Tick(object sender, EventArgs e)
        {
            
            if (media.NaturalDuration.HasTimeSpan)
            {
                seconds = media.NaturalDuration.TimeSpan.TotalSeconds;
            }
            progressBar.Width = media.Position.TotalSeconds / seconds * 320;
            position.Text = media.Position.ToString();
        }

        //現在のシークバーの場所へsyncを追加
        private void addsync_Click(object sender, RoutedEventArgs e)
        {
            var i = 0;
            var addsync = TimeSpan.Parse(position.Text); 

            var image = new Image
            {
                Source = new BitmapImage(new Uri("../marker.png", UriKind.Relative)),
                Margin = new Thickness(-10, 0, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Left,
                Height = 20,
                Width = 20,
                Tag = i
            };

            if (media.NaturalDuration.HasTimeSpan)
            {
                Canvas.SetLeft(image, addsync.TotalSeconds / media.NaturalDuration.TimeSpan.TotalSeconds * 320.0);
                markers.Children.Add(image);
            }
        }


        //メディアが停止、一時停止状態だとバーの位置を動かしても画像が変わらない

        private void progressBarBg_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //seekclick = true;
            //バーの位置を取得
            var position = e.GetPosition(progressBarBg);
            var second = media.NaturalDuration.TimeSpan.TotalSeconds * (position.X / progressBarBg.Width);

            
            //メディアの再生時間をバーの位置から取得
            media.Position = TimeSpan.FromSeconds(second);
            //sb.Seek(TimeSpan.FromSeconds(second));

            sb.SeekAlignedToLastTick(media.Position);
            
            //if (sb.GetCurrentState() == ClockState.Stopped)
            if(ststop)
            {
                sb.Begin();
                sb.Pause();
            }
        }

        private void media_MediaOpened(object sender, RoutedEventArgs e)
        {
            //var i = 0;
            //foreach (var value in MediaData.media[movie_index].sync)
            //{

            //}
        }




        //再生関連
        public string filename;
        public Uri video;
        public bool syncset = false;

        private void playmedia()
        {
            media.Play();
        }

        //メディアの再生
        private void play_Click(object sender, RoutedEventArgs e)
        {
            media.Play();
            Uri video = media.Source;

            //System.IO.StreamReader sr = new System.IO.StreamReader(@"C:\Users\matuyosi\Desktop\ヴァージョン\trunk\Authoring\Authoring\Butterfly.json");
            //var j = JsonValue.Load(sr) as JsonValue;
            //List<TimeSpan> sync = new List<TimeSpan>();

            
            var sync2 = TimeSpan.Parse("00:00:05.001");
            var sync1 = TimeSpan.Parse("00:00:01.001");
            var i = 0;

            //foreach (var value in (JsonArray)j["sync"])
            //{
            //    sync.Add(TimeSpan.Parse(value));
            //}

            var image1 = new Image
            {
                Source = new BitmapImage(new Uri("../marker.png", UriKind.Relative)),
                Margin = new Thickness(-10, 0, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Left,
                Height = 20,
                Width = 20,
                Tag = i
            };

            var image2 = new Image
            {
                Source = new BitmapImage(new Uri("../marker.png", UriKind.Relative)),
                Margin = new Thickness(-10, 0, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Left,
                Height = 20,
                Width = 20,
                Tag = i
            };

            if (media.NaturalDuration.HasTimeSpan)
            {
                if (!syncset)
                {
                    Canvas.SetLeft(image1, sync1.TotalSeconds / media.NaturalDuration.TimeSpan.TotalSeconds * 320.0);
                    markers.Children.Add(image1);

                    Canvas.SetLeft(image2, sync2.TotalSeconds / media.NaturalDuration.TimeSpan.TotalSeconds * 320.0);
                    markers.Children.Add(image2);
                    //foreach (var value in sync)
                    //{
                    //    Canvas.SetLeft(image, value.TotalSeconds / media.NaturalDuration.TimeSpan.TotalSeconds * 320.0);
                    //    markers.Children.Add(image);
                    //}
                    syncset = true;
                }
            }

            MediaTimeline mediatimeline =new MediaTimeline(media.Source);
            Storyboard.SetTargetName(mediatimeline, media.Name);

            sb.Children.Add(mediatimeline);
            sb.Begin(this,true);
        }

        //メディアの停止
        private void stop_Click(object sender, RoutedEventArgs e)
        {
            sb.Stop(this);
            media.Stop();            
            ststop = true;
        }

        //メディアの一時停止
        private void pause_Click(object sender, RoutedEventArgs e)
        {
            media.Pause();
            ststop = true;
            sb.Pause(this);
        }

        //動画群の入っているフォルダを指定
        private void fileopen_Click(object sender, RoutedEventArgs e)
        {
            string stPrompt = string.Empty;
            

            var opener = new System.Windows.Forms.FolderBrowserDialog();
            if (opener.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }
            path = opener.SelectedPath;
            var dirinfo = new DirectoryInfo(path);

            foreach (var value in dirinfo.GetFiles("*.wmv"))
            {
                stPrompt += value.Name + System.Environment.NewLine;
                
            }

            //フォルダ内のwmvファイルをすべて列挙
            
            var list = new List<string>(stPrompt.Trim().Split('\n'));

            

            foreach(var value in list){
                if (value != "")
                {
                    videolist.Items.Add(value);
                }
            }

            Properties.Settings.Default.defaultname = path;
            Properties.Settings.Default.defaultfile = stPrompt;

            Properties.Settings.Default.Save();

            //ファイルを直接開く方
            //System.Windows.Forms.OpenFileDialog open = new System.Windows.Forms.OpenFileDialog();
            //open.Filter = "動画ファイル(*.wmv)|*.wmv";
            //filename = open.FileName;

            //media.Source=new(Uri)filename;
        }

        //撮影動画が保存されているフォルダから動画ファイルを選択し、Sourceに関連付ける
        //関連付けが上手くできていないのかまだ動かない
        private void videolist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string s = Properties.Settings.Default.defaultname+(string)videolist.SelectedItem;
            video = new Uri(s);
            media.Source = video;
            playmedia();
        }


        //演出を全て削除
        private void drawclear_Click(object sender, RoutedEventArgs e)
        {
            drawAria.Children.Clear();
        }

        //jsonファイルの書き出し
        //オーサリング内容を反映させる
        //現段階では文字と□の位置情報のみ
        private void jout_Click(object sender, RoutedEventArgs e)
        {
            double wi = Math.Abs(recxen - recxst);
            double he = Math.Abs(recyen - recyst);
            var jsonFileName = "Butterfly.json";

           
            var outputjson = "{\n";

            //動画群の名前
            outputjson += "\"tag\" : \"" + "Butterfly" + ".wmv" + "\",\n";

            //動画群の個々の名前、視点、演出
            //まだ個々には出来ていない
            outputjson += "\"video\" : ";


            outputjson += "\"url\" : [\"" + "Butterfly" + ".wmv" + "\"],\n";
            outputjson += "\"viewpoint\" : [\"" + "正面" + "\"],\n";


            //同期ポイント書き込み
            outputjson += "\"sync\" : [";
            outputjson += "\"00:00:01\",";
            outputjson += "\"00:00:03\"],";
 
            //演出：四角書き込み
            outputjson += "\"rectangle\" : [ "+"\n";
            outputjson +="{"+"\n";
            outputjson += "\"height\" : \""+ wi +"\",";
            outputjson += "\"width\" : \"" + he + "\",";
            outputjson += "\"color\" : \"red\",";
            outputjson += "\"margin_x\" : \""+recxst+"\",";
            outputjson += "\"margin_y\" : \"" + recyst + "\",";
            outputjson += "}" + "\n";
            outputjson += "],";

            //演出：文字書き込み
            outputjson += "\"text\" : [ " + "\n";
            outputjson += "{" + "\n";
            outputjson += "\"text\" : \"" + ss + "\",";
            outputjson += "\"color\" : \"red\",";
            outputjson += "\"margin_x\" : \"" + senxst + "\",";
            outputjson += "\"margin_y\" : \"" + senyst + "\",";
            outputjson += "}" + "\n";
            outputjson += "]";

            outputjson += "}";
            
            using (var fileJson = new StreamWriter(path + "\\" + jsonFileName))
            {
                fileJson.Write(outputjson);
            }
        }



    }
}
