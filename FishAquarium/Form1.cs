using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace FishAquarium
{
    public partial class Form1 : Form
    {
        Graphics graphics;
        World world;
        int currentGen = 0;

        public Font drawFont = new Font("Arial", 10);
        public SolidBrush drawBrush = new SolidBrush(Color.White);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Text = $"Аквариум";
        }

        private void contBtn_Click_1(object sender, EventArgs e)
        {
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            world.Upgrade();
            genLb.Text = Convert.ToString(++currentGen);
            countLb.Text = Convert.ToString(world.fishCount);
            predCountLb.Text = Convert.ToString(world.fishPredCount);
            herbCountLb.Text = Convert.ToString(world.fishHerbCount);
        }

        private void TimerTrackBar_Scroll_1(object sender, EventArgs e)
        {
            nudTimer.Value = TimerTrackBar.Value;
            timer.Interval = TimerTrackBar.Value;
        }

        private void startBtn_Click_1(object sender, EventArgs e)
        {
            if (timer.Enabled)
                return;

            world = new World(fieldPB.Width, fieldPB.Height, fieldPB, ratioTB.Text);

            genLb.Text = Convert.ToString(currentGen);

            timer.Interval = (int)nudTimer.Value;
            nudTimer.Enabled = false;
            ratioTB.Enabled = false;
            contBtn.Enabled = false;

            world.StartWorld();

            countLb.Text = Convert.ToString(world.fishCount);
            predCountLb.Text = Convert.ToString(world.fishPredCount);
            herbCountLb.Text = Convert.ToString(world.fishHerbCount);

            timer.Start();
        }

        private void stopBtn_Click_1(object sender, EventArgs e)
        {
            if (!timer.Enabled)
                return;

            timer.Stop();
            nudTimer.Enabled = true;
            ratioTB.Enabled = true;
            contBtn.Enabled = true;
        }

        private void FieldPB_MouseMove(object sender, MouseEventArgs e)
        {
            if (!timer.Enabled)
                return;

            if (e.Button == MouseButtons.Left)
            {
                var x = e.Location.X;
                var y = e.Location.Y;
                world.FeedingFish(x, y);
            }

            if (e.Button == MouseButtons.Right)
            {
                var x = e.Location.X;
                var y = e.Location.Y;
                world.DeleteEntity(x, y);
            }
        }
    }
}
