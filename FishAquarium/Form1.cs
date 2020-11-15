﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FishAquarium
{
    public partial class Form1 : Form
    {
        private int currentGen = 0;
        private Graphics graphics;
        private int resolution;
        private int[] ratio = new int[2];
        private int rows;
        private int cols;

        private Entity[,] worldArr;
        public int fishCount;
        public int fishPredCount;
        public int fishHerbCount;

        Font drawFont = new Font("Arial", 7);
        SolidBrush drawBrush = new SolidBrush(Color.White);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Text = $"Аквариум";
        }

        private void StartWorld()
        {
            if (timer.Enabled)
                return;

            currentGen = 0;
            genLb.Text = Convert.ToString(currentGen);

            timer.Interval = (int)nudTimer.Value;
            nudTimer.Enabled = false;
            nudResolution.Enabled = false;
            nudDensity.Enabled = false;
            ratioTB.Enabled = false;
            contBtn.Enabled = false;

            resolution = (int)nudResolution.Value;
            rows = fieldPB.Height / resolution;
            cols = fieldPB.Width / resolution;

            worldArr = new Entity[cols, rows];
            bool state;
            fishCount = 0;
            fishPredCount = 0;
            fishHerbCount = 0;
            string text = ratioTB.Text;
            string[] ratioStr = text.Split(new char[] { '/' });

            int i = 0;
            foreach (string s in ratioStr)
            {
                ratio[i] = Convert.ToInt32(s);
                i++;
            }

            /*HerbFish fish = new HerbFish(5, 5);
            fishCount++;
            fishPredCount++;
            fishArr[5, 5] = fish;*/
            /*PredFish fish1 = new PredFish(50, 50, Brushes.Red);
            fishCount++;
            fishPredCount++;
            fishArr[0, 4] = fish1;*/


            /*Worm worm = new Worm(0, 2, Brushes.Yellow);
            fishArr[0, 2] = worm;
            Worm worm1 = new Worm(0, 2, Brushes.Yellow);
            fishArr[1, 3] = worm1;
            Worm worm2 = new Worm(0, 2, Brushes.Yellow);
            fishArr[1, 2] = worm2;
            Worm worm3 = new Worm(0, 2, Brushes.Yellow);
            fishArr[1, 4] = worm3;*/

            Random random = new Random();
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < cols; x++)
                {
                    state = random.Next(100 - (int)nudDensity.Value) == 0;

                    if (y <= rows / 2 && state && fishPredCount < ratio[0])
                    {
                        PredFish fish = new PredFish(x, y);
                        fishCount++;
                        fishPredCount++;
                        worldArr[x, y] = fish;
                    }
                    else if (y > rows / 2 && state && fishHerbCount < ratio[1])
                    {
                        HerbFish fish = new HerbFish(x, y);
                        fishCount++;
                        fishHerbCount++;
                        worldArr[x, y] = fish;
                    }
                }
            }

            fieldPB.Image = new Bitmap(fieldPB.Width, fieldPB.Height);
            graphics = Graphics.FromImage(fieldPB.Image);
            countLb.Text = Convert.ToString(fishCount);
            predCountLb.Text = Convert.ToString(fishPredCount);
            herbCountLb.Text = Convert.ToString(fishHerbCount);
            timer.Start();
        }

        private void Upgrade()
        {
            graphics.Clear(Color.FromArgb(0, Color.White));
            World world = new World(cols, rows);

            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    if (worldArr[x, y] != null && worldArr[x, y].State)
                    {
                        world.UpdateWorld(ref worldArr, x, y, ref listBox1);
                    }
                }
            }

            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    if (worldArr[x, y] != null)
                    {
                        worldArr[x, y].Checked = false;

                        //graphics.DrawImage(newImage, fishArr[x, y].PosX * resolution + (float)0.5, fishArr[x, y].PosY * resolution + (float)0.5, resolution - 1, resolution - 1);
                        graphics.FillRectangle(worldArr[x, y].Color, worldArr[x, y].PosX * resolution + (float)0.5, worldArr[x, y].PosY * resolution + (float)0.5, resolution - 1, resolution - 1);
                        graphics.DrawString(Convert.ToString(worldArr[x, y].Energy), drawFont, drawBrush, worldArr[x, y].PosX * resolution + (float)0.5, worldArr[x, y].PosY * resolution + (float)0.5);
                    }
                }
            }

            Pen pen = new Pen(Color.FromArgb(50, 45, 45, 48), 1);
            for (int y = 0; y < rows; ++y)
            {
                graphics.DrawLine(pen, 0, y * resolution, cols * resolution, y * resolution);
            }

            for (int x = 0; x < cols; ++x)
            {
                graphics.DrawLine(pen, x * resolution, 0, x * resolution, rows * resolution);
            }

            /*Pen greenPen = new Pen(Color.Gray, 5);

            PointF[] curvePoints = new PointF[7];
            float[] Y = { 15, 15, 0.5F, 0.5F, 0.5F, 15, 15 };
            for (int i = 0; i < 7; i++)
            {
                PointF point = new PointF(i * cols * resolution / 6, (rows - Y[i]) * resolution );
                listBox1.Items.Add(Convert.ToString(i * cols * resolution / 6 + " " + (rows - Y[i]) * resolution));
                curvePoints[i] = point;
            }

            graphics.DrawCurve(greenPen, curvePoints, 0.2F);*/

            fieldPB.Refresh();
            genLb.Text = Convert.ToString(++currentGen);
            countLb.Text = Convert.ToString(fishCount);
        }

        private void StopWorld()
        {
            if (!timer.Enabled)
                return;

            timer.Stop();
            nudResolution.Enabled = true;
            nudDensity.Enabled = true;
            nudTimer.Enabled = true;
            ratioTB.Enabled = true;
            contBtn.Enabled = true;
        }

        private void contBtn_Click(object sender, EventArgs e)
        {
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Upgrade();
        }

        private void TimerTrackBar_Scroll(object sender, EventArgs e)
        {
            nudTimer.Value = TimerTrackBar.Value;
            timer.Interval = TimerTrackBar.Value;
        }

        private void StartBtn_Click(object sender, EventArgs e)
        {
            StartWorld();
            listBox1.Items.Clear();
        }

        private void StopBtn_Click(object sender, EventArgs e)
        {
            StopWorld();
        }

        private void FieldPB_MouseMove(object sender, MouseEventArgs e)
        {
            if (!timer.Enabled)
                return;

            if (e.Button == MouseButtons.Left)
            {
                var x = e.Location.X / resolution;
                var y = e.Location.Y / resolution;
                var validationPassed = ValidateMousePosition(x, y);
                if (validationPassed && worldArr[x, y] == null)
                {
                    Worm worm = new Worm(x, y);
                    worldArr[x, y] = worm;
                }
                    
            }

            if (e.Button == MouseButtons.Right)
            {
                var x = e.Location.X / resolution;
                var y = e.Location.Y / resolution;
                var validationPassed = ValidateMousePosition(x, y);
                if (validationPassed && worldArr[x, y] != null)
                {
                    worldArr[x, y] = null;
                }
            }
        }

        private bool ValidateMousePosition(int x, int y)
        {
            return x >= 0 && y >= 0 && x < cols && y < rows;
        }
    }
}
