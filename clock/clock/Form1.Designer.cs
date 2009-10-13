namespace clock
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timeTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.start = new System.Windows.Forms.Button();
            this.sync = new System.Windows.Forms.Button();
            this.end = new System.Windows.Forms.Button();
            this.output = new System.Windows.Forms.Button();
            this.synctime = new System.Windows.Forms.TextBox();
            this.nowsync = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // timeTextBox
            // 
            this.timeTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.timeTextBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.timeTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.timeTextBox.Font = new System.Drawing.Font("MS UI Gothic", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.timeTextBox.ForeColor = System.Drawing.Color.Black;
            this.timeTextBox.Location = new System.Drawing.Point(0, 0);
            this.timeTextBox.Name = "timeTextBox";
            this.timeTextBox.ReadOnly = true;
            this.timeTextBox.Size = new System.Drawing.Size(292, 55);
            this.timeTextBox.TabIndex = 0;
            this.timeTextBox.TabStop = false;
            this.timeTextBox.Text = "00:00:00:000";
            this.timeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(55, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "H";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(111, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "M";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(173, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(12, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "S";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(248, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "ms";
            // 
            // start
            // 
            this.start.Location = new System.Drawing.Point(12, 72);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(56, 23);
            this.start.TabIndex = 5;
            this.start.Text = "Start";
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // sync
            // 
            this.sync.Location = new System.Drawing.Point(150, 72);
            this.sync.Name = "sync";
            this.sync.Size = new System.Drawing.Size(56, 23);
            this.sync.TabIndex = 6;
            this.sync.Text = "Sync";
            this.sync.UseVisualStyleBackColor = true;
            this.sync.Click += new System.EventHandler(this.sync_Click);
            // 
            // end
            // 
            this.end.Location = new System.Drawing.Point(74, 72);
            this.end.Name = "end";
            this.end.Size = new System.Drawing.Size(56, 23);
            this.end.TabIndex = 7;
            this.end.Text = "End";
            this.end.UseVisualStyleBackColor = true;
            this.end.Click += new System.EventHandler(this.end_Click);
            // 
            // output
            // 
            this.output.Location = new System.Drawing.Point(212, 72);
            this.output.Name = "output";
            this.output.Size = new System.Drawing.Size(56, 23);
            this.output.TabIndex = 8;
            this.output.Text = "Output";
            this.output.UseVisualStyleBackColor = true;
            this.output.Click += new System.EventHandler(this.output_Click);
            // 
            // synctime
            // 
            this.synctime.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.synctime.Location = new System.Drawing.Point(12, 152);
            this.synctime.Multiline = true;
            this.synctime.Name = "synctime";
            this.synctime.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.synctime.Size = new System.Drawing.Size(256, 149);
            this.synctime.TabIndex = 9;
            this.synctime.Text = "00:00:00:000";
            // 
            // nowsync
            // 
            this.nowsync.Font = new System.Drawing.Font("MS UI Gothic", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.nowsync.Location = new System.Drawing.Point(12, 121);
            this.nowsync.Name = "nowsync";
            this.nowsync.Size = new System.Drawing.Size(255, 25);
            this.nowsync.TabIndex = 10;
            this.nowsync.Text = "00:00:00:000";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("MS UI Gothic", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.Location = new System.Drawing.Point(12, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 20);
            this.label5.TabIndex = 11;
            this.label5.Text = "New Sync";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(292, 310);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.nowsync);
            this.Controls.Add(this.synctime);
            this.Controls.Add(this.output);
            this.Controls.Add(this.end);
            this.Controls.Add(this.sync);
            this.Controls.Add(this.start);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.timeTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "C# Sync Clock";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox timeTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button start;
        private System.Windows.Forms.Button sync;
        private System.Windows.Forms.Button end;
        private System.Windows.Forms.Button output;
        private System.Windows.Forms.TextBox synctime;
        private System.Windows.Forms.TextBox nowsync;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label5;
    }
}

