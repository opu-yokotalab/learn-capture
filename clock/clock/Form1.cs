using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace clock
{


    //同期指定用プログラム
    //外部から使えるよう、ロジック部分のみにする
    //コントロールからなんらかのイベントを起こすと関数を呼び出す形に

    public partial class Form1 : Form
    {

        delegate void dlgSetString(object lbl, string text);

        //指定時間を格納しておくArrayList
        ArrayList syn = new ArrayList();

        public Form1()
        {
            InitializeComponent();
        }

        //Stopwatchクラス　MyStopWatch
        System.Diagnostics.Stopwatch MyStopWatch = new System.Diagnostics.Stopwatch();

        //起動時はoutputを押せないようにする
        private void Form1_Load(object sender, EventArgs e)
        {
            output.Enabled = false;
        }

        //Startボタンにより、同期指定時間を開始(MyStopWatchをスタート)
        //startを押せないように、endを押せるように
        private void start_Click(object sender, EventArgs e)
        {
            start.Enabled = false;
            end.Enabled = true;
            synctime.Text=("00:00:00:000");
            syn.Clear();
            MyStopWatch.Start();
        }

        //Endボタンにより、同期指定時間を終了
        //start,outputを押せるように、endを押せないように
        //timeTextBoxに終了時間を記載
        private void end_Click(object sender, EventArgs e)
        {
            MyStopWatch.Stop();
            start.Enabled = true;
            end.Enabled = false;
            output.Enabled = true;
            timeTextBox.Text = string.Format("{00:00:00:00:000}", MyStopWatch.ElapsedMilliseconds);
        }

        //Syncボタンにより、任意時間に同期指定
        //指定時間をtimeTextBoxに記載
        private void sync_Click(object sender, EventArgs e)
        {
            nowsync.Text = string.Format("{00:00:00:00:000}", MyStopWatch.ElapsedMilliseconds);
            syn.Add(string.Format("{00:00:00:00:000}", MyStopWatch.ElapsedMilliseconds));
            //synctime.Text += System.Environment.NewLine+string.Format("{00:00:00:00:000}", MyStopWatch.ElapsedMilliseconds);
        }

        //同期指定した時間をsynctimeに全て表示
        private void output_Click(object sender, EventArgs e)
        {
            output.Enabled = false;
            MyStopWatch.Reset();
            timeTextBox.Text =("00:00:00:000");
            foreach (string s in syn)
            {
                synctime.Text += System.Environment.NewLine+s;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timeTextBox.Text = string.Format("{00:00:00:00:000}", MyStopWatch.ElapsedMilliseconds);
        }
    }
}
