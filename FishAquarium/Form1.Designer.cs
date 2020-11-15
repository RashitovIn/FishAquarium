namespace FishAquarium
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.nudDensity = new System.Windows.Forms.NumericUpDown();
            this.ratioTB = new System.Windows.Forms.TextBox();
            this.stopBtn = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.nudResolution = new System.Windows.Forms.NumericUpDown();
            this.startBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.contBtn = new System.Windows.Forms.Button();
            this.countLb = new System.Windows.Forms.Label();
            this.TimerTrackBar = new System.Windows.Forms.TrackBar();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.nudTimer = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.genLb = new System.Windows.Forms.Label();
            this.herbCountLb = new System.Windows.Forms.Label();
            this.predCountLb = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.fieldPB = new System.Windows.Forms.PictureBox();
            this.timer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDensity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudResolution)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TimerTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTimer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fieldPB)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.fieldPB);
            this.splitContainer1.Size = new System.Drawing.Size(1254, 629);
            this.splitContainer1.SplitterDistance = 346;
            this.splitContainer1.TabIndex = 1;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.nudDensity);
            this.splitContainer2.Panel1.Controls.Add(this.ratioTB);
            this.splitContainer2.Panel1.Controls.Add(this.stopBtn);
            this.splitContainer2.Panel1.Controls.Add(this.label7);
            this.splitContainer2.Panel1.Controls.Add(this.label5);
            this.splitContainer2.Panel1.Controls.Add(this.label1);
            this.splitContainer2.Panel1.Controls.Add(this.nudResolution);
            this.splitContainer2.Panel1.Controls.Add(this.startBtn);
            this.splitContainer2.Panel1.Controls.Add(this.label2);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.contBtn);
            this.splitContainer2.Panel2.Controls.Add(this.countLb);
            this.splitContainer2.Panel2.Controls.Add(this.TimerTrackBar);
            this.splitContainer2.Panel2.Controls.Add(this.label6);
            this.splitContainer2.Panel2.Controls.Add(this.label4);
            this.splitContainer2.Panel2.Controls.Add(this.nudTimer);
            this.splitContainer2.Panel2.Controls.Add(this.label8);
            this.splitContainer2.Panel2.Controls.Add(this.listBox1);
            this.splitContainer2.Panel2.Controls.Add(this.label3);
            this.splitContainer2.Panel2.Controls.Add(this.genLb);
            this.splitContainer2.Panel2.Controls.Add(this.herbCountLb);
            this.splitContainer2.Panel2.Controls.Add(this.predCountLb);
            this.splitContainer2.Panel2.Controls.Add(this.label10);
            this.splitContainer2.Size = new System.Drawing.Size(346, 629);
            this.splitContainer2.SplitterDistance = 153;
            this.splitContainer2.TabIndex = 0;
            // 
            // nudDensity
            // 
            this.nudDensity.Location = new System.Drawing.Point(7, 66);
            this.nudDensity.Name = "nudDensity";
            this.nudDensity.Size = new System.Drawing.Size(136, 20);
            this.nudDensity.TabIndex = 3;
            this.nudDensity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudDensity.Value = new decimal(new int[] {
            80,
            0,
            0,
            0});
            // 
            // ratioTB
            // 
            this.ratioTB.Location = new System.Drawing.Point(7, 121);
            this.ratioTB.Name = "ratioTB";
            this.ratioTB.Size = new System.Drawing.Size(136, 20);
            this.ratioTB.TabIndex = 1;
            this.ratioTB.Text = "50/50";
            this.ratioTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // stopBtn
            // 
            this.stopBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.stopBtn.Location = new System.Drawing.Point(7, 184);
            this.stopBtn.Name = "stopBtn";
            this.stopBtn.Size = new System.Drawing.Size(136, 31);
            this.stopBtn.TabIndex = 5;
            this.stopBtn.Text = "Стоп";
            this.stopBtn.UseVisualStyleBackColor = true;
            this.stopBtn.Click += new System.EventHandler(this.StopBtn_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(7, 105);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(132, 13);
            this.label7.TabIndex = 19;
            this.label7.Text = "Хищных/Травоядных";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(7, 89);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Количесво";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(7, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Разрешение";
            // 
            // nudResolution
            // 
            this.nudResolution.Location = new System.Drawing.Point(7, 25);
            this.nudResolution.Maximum = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.nudResolution.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudResolution.Name = "nudResolution";
            this.nudResolution.Size = new System.Drawing.Size(136, 20);
            this.nudResolution.TabIndex = 1;
            this.nudResolution.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudResolution.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // startBtn
            // 
            this.startBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.startBtn.Location = new System.Drawing.Point(7, 147);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(136, 31);
            this.startBtn.TabIndex = 4;
            this.startBtn.Text = "Старт";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.StartBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(7, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Плотность населения";
            // 
            // contBtn
            // 
            this.contBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.contBtn.Location = new System.Drawing.Point(10, 87);
            this.contBtn.Name = "contBtn";
            this.contBtn.Size = new System.Drawing.Size(172, 31);
            this.contBtn.TabIndex = 22;
            this.contBtn.Text = "Возобновить";
            this.contBtn.UseVisualStyleBackColor = true;
            this.contBtn.Click += new System.EventHandler(this.contBtn_Click);
            // 
            // countLb
            // 
            this.countLb.AutoSize = true;
            this.countLb.Location = new System.Drawing.Point(156, 154);
            this.countLb.Name = "countLb";
            this.countLb.Size = new System.Drawing.Size(13, 13);
            this.countLb.TabIndex = 9;
            this.countLb.Text = "0";
            this.countLb.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // TimerTrackBar
            // 
            this.TimerTrackBar.LargeChange = 50;
            this.TimerTrackBar.Location = new System.Drawing.Point(9, 51);
            this.TimerTrackBar.Maximum = 1000;
            this.TimerTrackBar.Minimum = 100;
            this.TimerTrackBar.Name = "TimerTrackBar";
            this.TimerTrackBar.Size = new System.Drawing.Size(172, 45);
            this.TimerTrackBar.TabIndex = 10;
            this.TimerTrackBar.TickFrequency = 100;
            this.TimerTrackBar.Value = 200;
            this.TimerTrackBar.Scroll += new System.EventHandler(this.TimerTrackBar_Scroll);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 154);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Всего рыб:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(9, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(137, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Обновление мира(мс)";
            // 
            // nudTimer
            // 
            this.nudTimer.Location = new System.Drawing.Point(9, 25);
            this.nudTimer.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.nudTimer.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudTimer.Name = "nudTimer";
            this.nudTimer.Size = new System.Drawing.Size(172, 20);
            this.nudTimer.TabIndex = 16;
            this.nudTimer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudTimer.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 176);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 13);
            this.label8.TabIndex = 10;
            this.label8.Text = "Хищных рыб:";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(10, 225);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(172, 277);
            this.listBox1.TabIndex = 20;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 132);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Поколение:";
            // 
            // genLb
            // 
            this.genLb.AutoSize = true;
            this.genLb.Location = new System.Drawing.Point(156, 132);
            this.genLb.Name = "genLb";
            this.genLb.Size = new System.Drawing.Size(13, 13);
            this.genLb.TabIndex = 7;
            this.genLb.Text = "0";
            this.genLb.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // herbCountLb
            // 
            this.herbCountLb.AutoSize = true;
            this.herbCountLb.Location = new System.Drawing.Point(156, 200);
            this.herbCountLb.Name = "herbCountLb";
            this.herbCountLb.Size = new System.Drawing.Size(13, 13);
            this.herbCountLb.TabIndex = 13;
            this.herbCountLb.Text = "0";
            this.herbCountLb.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // predCountLb
            // 
            this.predCountLb.AutoSize = true;
            this.predCountLb.Location = new System.Drawing.Point(156, 176);
            this.predCountLb.Name = "predCountLb";
            this.predCountLb.Size = new System.Drawing.Size(13, 13);
            this.predCountLb.TabIndex = 11;
            this.predCountLb.Text = "0";
            this.predCountLb.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(10, 200);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(95, 13);
            this.label10.TabIndex = 12;
            this.label10.Text = "Травоядных рыб:";
            // 
            // fieldPB
            // 
            this.fieldPB.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("fieldPB.BackgroundImage")));
            this.fieldPB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.fieldPB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldPB.Location = new System.Drawing.Point(0, 0);
            this.fieldPB.Name = "fieldPB";
            this.fieldPB.Size = new System.Drawing.Size(900, 625);
            this.fieldPB.TabIndex = 0;
            this.fieldPB.TabStop = false;
            this.fieldPB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FieldPB_MouseMove);
            this.fieldPB.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FieldPB_MouseMove);
            // 
            // timer
            // 
            this.timer.Tick += new System.EventHandler(this.Timer_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1254, 629);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudDensity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudResolution)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TimerTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTimer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fieldPB)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button stopBtn;
        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.NumericUpDown nudDensity;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudResolution;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox fieldPB;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Label genLb;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label herbCountLb;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label predCountLb;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label countLb;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.NumericUpDown nudTimer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox ratioTB;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TrackBar TimerTrackBar;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button contBtn;
    }
}

