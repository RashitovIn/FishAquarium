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
            this.contBtn = new System.Windows.Forms.Button();
            this.countLb = new System.Windows.Forms.Label();
            this.TimerTrackBar = new System.Windows.Forms.TrackBar();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.nudTimer = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.genLb = new System.Windows.Forms.Label();
            this.herbCountLb = new System.Windows.Forms.Label();
            this.predCountLb = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.ratioTB = new System.Windows.Forms.TextBox();
            this.stopBtn = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.startBtn = new System.Windows.Forms.Button();
            this.fieldPB = new System.Windows.Forms.PictureBox();
            this.timer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.contBtn);
            this.splitContainer1.Panel1.Controls.Add(this.countLb);
            this.splitContainer1.Panel1.Controls.Add(this.TimerTrackBar);
            this.splitContainer1.Panel1.Controls.Add(this.label6);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.nudTimer);
            this.splitContainer1.Panel1.Controls.Add(this.label8);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.genLb);
            this.splitContainer1.Panel1.Controls.Add(this.herbCountLb);
            this.splitContainer1.Panel1.Controls.Add(this.predCountLb);
            this.splitContainer1.Panel1.Controls.Add(this.label10);
            this.splitContainer1.Panel1.Controls.Add(this.ratioTB);
            this.splitContainer1.Panel1.Controls.Add(this.stopBtn);
            this.splitContainer1.Panel1.Controls.Add(this.label7);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.startBtn);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.fieldPB);
            this.splitContainer1.Size = new System.Drawing.Size(1254, 629);
            this.splitContainer1.SplitterDistance = 197;
            this.splitContainer1.TabIndex = 1;
            // 
            // contBtn
            // 
            this.contBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.contBtn.Location = new System.Drawing.Point(10, 136);
            this.contBtn.Name = "contBtn";
            this.contBtn.Size = new System.Drawing.Size(176, 31);
            this.contBtn.TabIndex = 56;
            this.contBtn.Text = "Возобновить";
            this.contBtn.UseVisualStyleBackColor = true;
            this.contBtn.Click += new System.EventHandler(this.contBtn_Click_1);
            // 
            // countLb
            // 
            this.countLb.AutoSize = true;
            this.countLb.Location = new System.Drawing.Point(157, 206);
            this.countLb.Name = "countLb";
            this.countLb.Size = new System.Drawing.Size(13, 13);
            this.countLb.TabIndex = 48;
            this.countLb.Text = "0";
            this.countLb.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.countLb.Visible = false;
            // 
            // TimerTrackBar
            // 
            this.TimerTrackBar.LargeChange = 50;
            this.TimerTrackBar.Location = new System.Drawing.Point(10, 332);
            this.TimerTrackBar.Maximum = 1000;
            this.TimerTrackBar.Minimum = 50;
            this.TimerTrackBar.Name = "TimerTrackBar";
            this.TimerTrackBar.Size = new System.Drawing.Size(176, 45);
            this.TimerTrackBar.TabIndex = 49;
            this.TimerTrackBar.TickFrequency = 100;
            this.TimerTrackBar.Value = 200;
            this.TimerTrackBar.Scroll += new System.EventHandler(this.TimerTrackBar_Scroll_1);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 206);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 13);
            this.label6.TabIndex = 47;
            this.label6.Text = "Всего рыб:";
            this.label6.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(10, 279);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(137, 13);
            this.label4.TabIndex = 54;
            this.label4.Text = "Обновление мира(мс)";
            // 
            // nudTimer
            // 
            this.nudTimer.Location = new System.Drawing.Point(10, 306);
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
            this.nudTimer.Size = new System.Drawing.Size(176, 20);
            this.nudTimer.TabIndex = 55;
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
            this.label8.Location = new System.Drawing.Point(11, 228);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 13);
            this.label8.TabIndex = 50;
            this.label8.Text = "Хищных рыб:";
            this.label8.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 182);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 13);
            this.label3.TabIndex = 45;
            this.label3.Text = "Итерация таймера:";
            // 
            // genLb
            // 
            this.genLb.AutoSize = true;
            this.genLb.Location = new System.Drawing.Point(157, 182);
            this.genLb.Name = "genLb";
            this.genLb.Size = new System.Drawing.Size(13, 13);
            this.genLb.TabIndex = 46;
            this.genLb.Text = "0";
            this.genLb.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // herbCountLb
            // 
            this.herbCountLb.AutoSize = true;
            this.herbCountLb.Location = new System.Drawing.Point(157, 252);
            this.herbCountLb.Name = "herbCountLb";
            this.herbCountLb.Size = new System.Drawing.Size(13, 13);
            this.herbCountLb.TabIndex = 53;
            this.herbCountLb.Text = "0";
            this.herbCountLb.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.herbCountLb.Visible = false;
            // 
            // predCountLb
            // 
            this.predCountLb.AutoSize = true;
            this.predCountLb.Location = new System.Drawing.Point(157, 228);
            this.predCountLb.Name = "predCountLb";
            this.predCountLb.Size = new System.Drawing.Size(13, 13);
            this.predCountLb.TabIndex = 51;
            this.predCountLb.Text = "0";
            this.predCountLb.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.predCountLb.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(11, 252);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(95, 13);
            this.label10.TabIndex = 52;
            this.label10.Text = "Травоядных рыб:";
            this.label10.Visible = false;
            // 
            // ratioTB
            // 
            this.ratioTB.Location = new System.Drawing.Point(10, 36);
            this.ratioTB.Name = "ratioTB";
            this.ratioTB.Size = new System.Drawing.Size(176, 20);
            this.ratioTB.TabIndex = 38;
            this.ratioTB.Text = "7/7";
            this.ratioTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // stopBtn
            // 
            this.stopBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.stopBtn.Location = new System.Drawing.Point(10, 99);
            this.stopBtn.Name = "stopBtn";
            this.stopBtn.Size = new System.Drawing.Size(176, 31);
            this.stopBtn.TabIndex = 42;
            this.stopBtn.Text = "Стоп";
            this.stopBtn.UseVisualStyleBackColor = true;
            this.stopBtn.Click += new System.EventHandler(this.stopBtn_Click_1);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(10, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(132, 13);
            this.label7.TabIndex = 44;
            this.label7.Text = "Хищных/Травоядных";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(10, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 13);
            this.label5.TabIndex = 43;
            this.label5.Text = "Количесво";
            // 
            // startBtn
            // 
            this.startBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.startBtn.Location = new System.Drawing.Point(10, 62);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(176, 31);
            this.startBtn.TabIndex = 41;
            this.startBtn.Text = "Старт";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.startBtn_Click_1);
            // 
            // fieldPB
            // 
            this.fieldPB.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("fieldPB.BackgroundImage")));
            this.fieldPB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.fieldPB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldPB.Location = new System.Drawing.Point(0, 0);
            this.fieldPB.Name = "fieldPB";
            this.fieldPB.Size = new System.Drawing.Size(1049, 625);
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
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TimerTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTimer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fieldPB)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.PictureBox fieldPB;
        private System.Windows.Forms.Button contBtn;
        private System.Windows.Forms.Label countLb;
        private System.Windows.Forms.TrackBar TimerTrackBar;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nudTimer;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label genLb;
        private System.Windows.Forms.Label herbCountLb;
        private System.Windows.Forms.Label predCountLb;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox ratioTB;
        private System.Windows.Forms.Button stopBtn;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button startBtn;
    }
}

