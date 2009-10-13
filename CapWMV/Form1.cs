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
        private Button output;
        private TextBox timeTextBox;
        private TextBox synctime;
        private Panel panelPreview;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;


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

        System.Diagnostics.Stopwatch MyStopWatch = new System.Diagnostics.Stopwatch();

        private void Form1_Load(object sender, EventArgs e)
        {
            output.Enabled = false;
            sync.Enabled = false;
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
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.sync = new System.Windows.Forms.Button();
            this.output = new System.Windows.Forms.Button();
            this.timeTextBox = new System.Windows.Forms.TextBox();
            this.synctime = new System.Windows.Forms.TextBox();
            this.panelPreview = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 184);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 44);
            this.button1.TabIndex = 0;
            this.button1.Text = "Start";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(64, 249);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(160, 19);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "foo.wmv";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(62, 231);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Output file";
            // 
            // sync
            // 
            this.sync.Location = new System.Drawing.Point(114, 184);
            this.sync.Name = "sync";
            this.sync.Size = new System.Drawing.Size(75, 44);
            this.sync.TabIndex = 7;
            this.sync.Text = "Sync";
            this.sync.UseVisualStyleBackColor = true;
            this.sync.Click += new System.EventHandler(this.sync_Click);
            // 
            // output
            // 
            this.output.Location = new System.Drawing.Point(218, 184);
            this.output.Name = "output";
            this.output.Size = new System.Drawing.Size(75, 44);
            this.output.TabIndex = 9;
            this.output.Text = "Out";
            this.output.UseVisualStyleBackColor = true;
            this.output.Click += new System.EventHandler(this.output_Click);
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
            this.timeTextBox.Text = "00:00:00:000";
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
            this.synctime.Text = "00:00:00:000";
            // 
            // panelPreview
            // 
            this.panelPreview.Location = new System.Drawing.Point(322, 12);
            this.panelPreview.Name = "panelPreview";
            this.panelPreview.Size = new System.Drawing.Size(268, 256);
            this.panelPreview.TabIndex = 12;
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
            this.ClientSize = new System.Drawing.Size(602, 280);
            this.Controls.Add(this.panelPreview);
            this.Controls.Add(this.synctime);
            this.Controls.Add(this.timeTextBox);
            this.Controls.Add(this.output);
            this.Controls.Add(this.sync);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "AsfFilter";
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

        Capture cam = null;

        private void button1_Click(object sender, System.EventArgs e)
        {
            const int VIDEODEVICE = 0; // zero based index of video capture device to use

            Cursor.Current = Cursors.WaitCursor;

            if (cam == null)
            {
                cam = new Capture(VIDEODEVICE, textBox1.Text);
                cam.Start();
                button1.Text = "Stop";
                textBox1.ReadOnly = true;
                sync.Enabled = true;
                synctime.Text = ("00:00:00:000");
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
                output.Enabled = true;
                timeTextBox.Text = string.Format("{00:00:00:00:000}", MyStopWatch.ElapsedMilliseconds);

                // Close it down
                cam.Dispose();
                cam = null;
            }

            Cursor.Current = Cursors.Default;
        }

        private void sync_Click(object sender, EventArgs e)
        {
            timeTextBox.Text = string.Format("{00:00:00:00:000}", MyStopWatch.ElapsedMilliseconds);
            syn.Add(string.Format("{00:00:00:00:000}", MyStopWatch.ElapsedMilliseconds));
        }

        private void output_Click(object sender, EventArgs e)
        {
            output.Enabled = false;
            MyStopWatch.Reset();
            timeTextBox.Text = ("00:00:00:000");
            foreach (string s in syn)
            {
                synctime.Text += System.Environment.NewLine + s;
            }
        }
	}
}