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
        }


        ///
        //演出の作成
        ///

        //四角の演出
        private bool dragStart = false;
        private Point dragStartPnt;
        //書き込む四角
        public Rectangle drawingRectangle;
        //演出の選択
        public Mode mode = Mode.Rectangle;

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
            strleft.Text = x.ToString();
            strtop.Text = y.ToString();
            DrawSentence(dragStartPnt);
        }

        private void rect_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            StopDragging();
            Point end = e.GetPosition(rect);
            double x = end.X;
            double y = end.Y;
            endleft.Text = x.ToString();
            endtop.Text = y.ToString();
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
                        DrawSentence(dragStartPnt);
                        break;
                }
            }
        }

        private void DrawSentence(Point startPnt)
        {
            var sentence = new TextBlock
            {
                FontSize=14,
                Text="test",
                Background = new SolidColorBrush(Colors.Transparent)
            };
            Canvas.SetLeft(sentence, startPnt.X);
            Canvas.SetTop(sentence, startPnt.Y);

            drawAria.Children.Add(sentence);
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
                StrokeThickness = 2
            };

            Canvas.SetLeft(rectangle, left);
            Canvas.SetTop(rectangle, top);

            drawAria.Children.Add(rectangle);

            return rectangle;
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

        private void texttest_Click(object sender, RoutedEventArgs e)
        {
            var text = new TextBlock();
            text.Text = "test";
            text.FontSize = 15;           

            Canvas.SetLeft(text,200);
            Canvas.SetTop(text,200);

            drawAria.Children.Add(text);
        }






        ///
        //シークバーの演出
        ///

        private DispatcherTimer Timer;
        double seconds;
        private bool ststop = false;
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

        //メディアが停止、一時停止状態だとバーの位置を動かしても画像が変わらない

        private void progressBarBg_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //seekclick = true;
            //バーの位置を取得
            var position = e.GetPosition(progressBarBg);
            var second = media.NaturalDuration.TimeSpan.TotalSeconds * (position.X / progressBarBg.Width);

            //メディアの再生時間をバーの位置から取得
            media.Position = TimeSpan.FromSeconds(second);

            if (ststop)
            {
                media.Play();
                System.Threading.Thread.Sleep(200);
                media.Pause();
            }
            //MediaData.sb.SeekAlignedToLastTick(media.Position);
            //if (MediaData.sb.GetCurrentState() == ClockState.Stopped)
            //{
            //    MediaData.sb.Begin();
            //    MediaData.sb.Pause();
            //}
        }

        //private void progressBarBg_MouseMove(object sender, MouseEventArgs e)
        //{

        //        //バーの位置を取得
        //        var position = e.GetPosition(progressBarBg);
        //        var second = media.NaturalDuration.TimeSpan.TotalSeconds * (position.X / progressBarBg.Width);

        //        //メディアの再生時間をバーの位置から取得
        //        media.Position = TimeSpan.FromSeconds(second);
            
        //}

        //private void progressBarBg_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        //{
        //    seekclick = false;
        //}

        //private void progressBarBg_MouseLeave(object sender, MouseEventArgs e)
        //{
        //    seekclick = false;
        //}



        //再生関連
        public string filename;

        private void play_Click(object sender, RoutedEventArgs e)
        {
            media.Play();
        }

        private void stop_Click(object sender, RoutedEventArgs e)
        {
            media.Stop();
            ststop = true;
        }

        private void pause_Click(object sender, RoutedEventArgs e)
        {
            media.Pause();
            ststop = true;
        }

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

            //ファイルを開くの方
            //System.Windows.Forms.OpenFileDialog open = new System.Windows.Forms.OpenFileDialog();
            //open.Filter = "動画ファイル(*.wmv)|*.wmv";
            //filename = open.FileName;

            //media.Source=new(Uri)filename;
        }


    }
}
