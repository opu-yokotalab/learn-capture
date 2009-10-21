/****************************************************************************
While the underlying libraries are covered by LGPL, this sample is released 
as public domain.  It is distributed in the hope that it will be useful, but 
WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY 
or FITNESS FOR A PARTICULAR PURPOSE.  
*****************************************************************************/

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Text;



namespace AsfFilter
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private Button sync;
        private TextBox timeTextBox;
        private TextBox synctime;
        private Panel panelPreview;
        private Timer timer1;
        private Button option;
        private TextBox movienumber;
        private System.ComponentModel.IContainer components;
        //プレビュー用
        //private Capt cam1;
        AsfFilter.Form2.Capt cap;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
                       
            
			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

        
        
        ArrayList syn = new ArrayList();
        int num=1;

        System.Diagnostics.Stopwatch MyStopWatch = new System.Diagnostics.Stopwatch();

        private void Form1_Load_1(object sender, EventArgs e)
        {                        
            sync.Enabled = false;
            movienumber.Text=textBox1.Text+num+"の撮影を待機中";
            
            //const int VIDEODEVICE = 0; // zero based index of video capture device to use
            //cam1 = new Capt(VIDEODEVICE, 320, 240, 15, panelPreview);
            //プレビュー表示(表示するとCapture.cs18行目でシステムリソース不足になる)
            //キャプチャピンが重複してる？
        }

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.sync = new System.Windows.Forms.Button();
            this.timeTextBox = new System.Windows.Forms.TextBox();
            this.synctime = new System.Windows.Forms.TextBox();
            this.panelPreview = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.option = new System.Windows.Forms.Button();
            this.movienumber = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(23, 184);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 44);
            this.button1.TabIndex = 0;
            this.button1.Text = "Start";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("MS UI Gothic", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox1.Location = new System.Drawing.Point(23, 249);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(95, 26);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "foo";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(21, 231);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Output file";
            // 
            // sync
            // 
            this.sync.Location = new System.Drawing.Point(116, 184);
            this.sync.Name = "sync";
            this.sync.Size = new System.Drawing.Size(75, 44);
            this.sync.TabIndex = 7;
            this.sync.Text = "Sync";
            this.sync.UseVisualStyleBackColor = true;
            this.sync.Click += new System.EventHandler(this.sync_Click);
            // 
            // timeTextBox
            // 
            this.timeTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.timeTextBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.timeTextBox.Font = new System.Drawing.Font("MS UI Gothic", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.timeTextBox.ForeColor = System.Drawing.Color.Black;
            this.timeTextBox.Location = new System.Drawing.Point(0, 0);
            this.timeTextBox.Name = "timeTextBox";
            this.timeTextBox.ReadOnly = true;
            this.timeTextBox.Size = new System.Drawing.Size(305, 55);
            this.timeTextBox.TabIndex = 10;
            this.timeTextBox.TabStop = false;
            this.timeTextBox.Text = "00:00:00.000";
            this.timeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // synctime
            // 
            this.synctime.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.synctime.Location = new System.Drawing.Point(23, 61);
            this.synctime.Multiline = true;
            this.synctime.Name = "synctime";
            this.synctime.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.synctime.Size = new System.Drawing.Size(256, 117);
            this.synctime.TabIndex = 11;
            // 
            // panelPreview
            // 
            this.panelPreview.Location = new System.Drawing.Point(322, 12);
            this.panelPreview.Name = "panelPreview";
            this.panelPreview.Size = new System.Drawing.Size(268, 256);
            this.panelPreview.TabIndex = 12;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // option
            // 
            this.option.Location = new System.Drawing.Point(204, 184);
            this.option.Name = "option";
            this.option.Size = new System.Drawing.Size(75, 44);
            this.option.TabIndex = 13;
            this.option.Text = "Option";
            this.option.UseVisualStyleBackColor = true;
            this.option.Click += new System.EventHandler(this.option_Click);
            // 
            // movienumber
            // 
            this.movienumber.Font = new System.Drawing.Font("MS UI Gothic", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.movienumber.Location = new System.Drawing.Point(124, 249);
            this.movienumber.Name = "movienumber";
            this.movienumber.Size = new System.Drawing.Size(181, 26);
            this.movienumber.TabIndex = 14;
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
            this.ClientSize = new System.Drawing.Size(602, 280);
            this.Controls.Add(this.movienumber);
            this.Controls.Add(this.option);
            this.Controls.Add(this.panelPreview);
            this.Controls.Add(this.synctime);
            this.Controls.Add(this.timeTextBox);
            this.Controls.Add(this.sync);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "AsfFilter";
            this.Load += new System.EventHandler(this.Form1_Load_1);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

        AsfFilter.Form2.Capt cam = null;
        
        private void button1_Click(object sender, System.EventArgs e)
        {
            const int VIDEODEVICE = 0; // zero based index of video capture device to use

            string filename=textBox1.Text;
            int moji=filename.Length;
            Cursor.Current = Cursors.WaitCursor;

            if (cam == null)
            {
                cam = new AsfFilter.Form2.Capt(VIDEODEVICE, textBox1.Text + num + ".wmv");
                cam.Start();
                movienumber.Text=textBox1.Text+num+"を撮影中";
                button1.Text = "Stop";
                textBox1.ReadOnly = true;
                sync.Enabled = true;
                synctime.Text = ("00:00:00:00:000");
                syn.Clear();
                MyStopWatch.Start();
            }
            else
            {
                button1.Text = "Start";
                textBox1.ReadOnly = false;

                // Pause the recording
                cam.Pause();
                MyStopWatch.Stop();
                timeTextBox.Text = string.Format("{00:00:00:00:000}", MyStopWatch.ElapsedMilliseconds);

                // Close it down
                cam.Dispose();
                cam = null;

                //string name = textBox1.Text;
                //name = name.Replace(".wmv", "");
                string[] synctime = new string[syn.Count];
                MyStopWatch.Reset();
                timeTextBox.Text = ("00:00:00:000");
                StreamWriter fs = new StreamWriter(textBox1.Text +num+ ".json");

                for (int count = 0; count < syn.Count; count++)
                {
                    synctime[count] = (string)syn[count];
                }
                for (int count = 0; count < syn.Count; count++)
                {
                    synctime[count] = synctime[count].Remove(8, 1);
                }
                for (int count = 0; count < syn.Count; count++)
                {
                    synctime[count] = synctime[count].Insert(8, ".");
                }

                fs.WriteLine("{");
                fs.WriteLine("\"tag\" : " + "\"" + textBox1.Text + ".wmv" + "\"" + ",");
                fs.WriteLine("\"url\" : " + "[" + "\"" + textBox1.Text + ".wmv" + "\"" + "],");
                fs.WriteLine("\"viewpoint\" : [\"正面\"],");
                fs.Write("\"sync\" : [");

                for (int count = 0; count < syn.Count; count++)
                {
                    fs.Write("\"" + synctime[count] + "\"");
                    if (count != syn.Count - 1)
                    {
                        fs.WriteLine(",");
                    }
                }

                fs.WriteLine("],");
                fs.WriteLine("\"rectangle\" : [],");
                fs.WriteLine("\"text\" : []");
                fs.WriteLine("}");
                fs.Close();

                num++;
                movienumber.Text = textBox1.Text + num + "の撮影を待機中";

                ///jsonファイルの書き出し
                ///プレイヤーのJSONフォーマット
                ///
                /// {
                ///"tag" : "output.wmv",
                ///"url" : ["output.wmv"],
                ///"viewpoint" : ["正面"],
                ///"sync" : ["00:01:00","00:02:00"],
                ///"rectangle" : [],
                ///"text" : []
                ///}
                ///

                //foreach (string s in syn)
                //{
                //   synctime.Text += System.Environment.NewLine + s;
                //}

            }

            Cursor.Current = Cursors.Default;
        }

        private void sync_Click(object sender, EventArgs e)
        {
            synctime.Text += System.Environment.NewLine + string.Format("\"" + "{00:00:00:00:000}" + "\"", MyStopWatch.ElapsedMilliseconds);
            syn.Add(string.Format("{00:00:00:00:000}", MyStopWatch.ElapsedMilliseconds));
            synctime.SelectionStart = synctime.TextLength;
            synctime.ScrollToCaret();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timeTextBox.Text = string.Format("{00:00:00:00:000}", MyStopWatch.ElapsedMilliseconds);
        }

        private void option_Click(object sender, EventArgs e)
        {
            //cap.ShowPropertyPages();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            num = 1;
            movienumber.Text = textBox1.Text+num+"の撮影を待機中";
        }
	}
}
