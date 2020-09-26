using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FishAquarium
{
    public partial class Form1 : Form
    {
        private int currentGen = 0;
        private Graphics graphics;
        private World world;
        private int resolution;
        private bool[,] field;
        private int rows;
        private int cols;

        private Fish[,] fishArr;
        private Fish[,] newFishArr;
        public int fishCount;

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

            resolution = (int)nudResolution.Value;
            rows = fieldPB.Height / resolution;
            cols = fieldPB.Width / resolution;

            field = new bool[cols, rows];
            fishArr = new Fish[cols, rows];

            bool state;
            fishCount = 0;

            Random random = new Random();
            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    /*fishCount++;
                    Fish fish = new Fish(fishCount, 0, 0, Brushes.Red);
                    fish.Target[0] = 0;
                    fish.Target[1] = 2;
                    fishArr[0, 0] = fish;
                    fishCount++;
                    Fish fishh = new Fish(fishCount, 0, 4, Brushes.Green);
                    fishh.Target[0] = 0;
                    fishh.Target[1] = 2;
                    fishArr[0, 4] = fishh;*/
                    state = random.Next(100 - (int)nudDensity.Value) == 0;
                    field[x, y] = state;

                    if (state)
                    {
                        Fish fish = new Fish(fishCount, x, y, Brushes.Green);
                        fishCount++;
                        fishArr[x, y] = fish;
                    }
                }
            }

            fieldPB.Image = new Bitmap(fieldPB.Width, fieldPB.Height);
            graphics = Graphics.FromImage(fieldPB.Image);
            countLb.Text = Convert.ToString(fishCount);
            timer.Start();
        }

        private void Upgrade()
        {
            graphics.Clear(Color.FromArgb(0, Color.White));

            newFishArr = new Fish[cols, rows];


            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    //var neighboursCount = CountNeighbours(x, y);

                    if (fishArr[x, y] != null && fishArr[x, y].State)
                    {

                        /*if (!hasLife && neighboursCount == 3)
                            newField[x, y] = true;
                        else if (hasLife && (neighboursCount < 2 || neighboursCount > 3))
                            newField[x, y] = false;
                        else
                            newField[x, y] = field[x, y];*/

                        if (fishArr[x, y].Target[0] == fishArr[x, y].PosX && fishArr[x, y].Target[1] == fishArr[x, y].PosY)
                            GetTarget(fishArr[x, y]);

                        FishMove(fishArr[x, y]);
                        newFishArr[fishArr[x, y].PosX, fishArr[x, y].PosY] = fishArr[x, y];

                        //listBox1.Items.Add(Convert.ToString(fishArr[x, y].Id) + " " + x + " " + y + " " + fishArr[x, y].Target[0] + " " + fishArr[x, y].Target[1]);

                        graphics.FillRectangle(fishArr[x, y].Color, fishArr[x, y].PosX * resolution, fishArr[x, y].PosY * resolution, resolution, resolution);
                    }
                }
            }
            fishArr = newFishArr;
            fieldPB.Refresh();
            genLb.Text = Convert.ToString(++currentGen);
            countLb.Text = Convert.ToString(fishCount);
        }

        private void FishMove(Fish fish)
        {
            if (fish.Target[0] > fish.PosX)
            {
                if (CheckMove(fish.PosX + 1, fish.PosY))
                    fish.PosX = fish.PosX + 1;
            }
            else if (fish.Target[0] < fish.PosX)
            {
                if (CheckMove(fish.PosX - 1, fish.PosY))
                    fish.PosX = fish.PosX - 1;
            }                

            if (fish.Target[1] > fish.PosY)
            {
                if (CheckMove(fish.PosX, fish.PosY + 1))
                    fish.PosY = fish.PosY + 1;
            }
            else if (fish.Target[1] < fish.PosY)
            {
                if (CheckMove(fish.PosX, fish.PosY - 1))
                    fish.PosY = fish.PosY - 1;
            }
        }

        private bool CheckMove(int NextPosX, int NextPosY)
        {
            if (NextPosY < rows && NextPosY >= 0)
            {
                NextPosX = (NextPosX + cols) % cols;
                if (fishArr[NextPosX, NextPosY] == null && newFishArr[NextPosX, NextPosY] == null)
                {
                    //listBox1.Items.Add("lol");
                    return true;
                }
            }
            //listBox1.Items.Add("Stolk");
            return false;
        }

        private void GetTarget(Fish fish)
        {
            Random random = new Random();
            int stepX = random.Next(10);
            int stepY = random.Next(20);
            int sideX = random.Next(-1, 2);
            int sideY = random.Next(-1, 2);
            int[] target = new int[2];

            target[0] = (fish.PosX + sideX * stepX + cols) % cols;
            target[1] = (fish.PosY + sideY * stepY + rows) % rows;
            fish.Target = target;
        }

        private int CountNeighbours(int x, int y)
        {
            int count = 0;

            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    var col = (x + i + cols) % cols;
                    var row = (y + j + rows) % rows;

                    var isSelfChecking = col == x && row == y;
                    var hasLife = field[col, row];

                    if (hasLife && !isSelfChecking)
                        count++;
                }
            }

            return count;
        }

        private void StopWorld()
        {
            if (!timer.Enabled)
                return;

            timer.Stop();
            nudResolution.Enabled = true;
            nudDensity.Enabled = true;
            nudTimer.Enabled = true;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Upgrade();
        }

        private void StartBtn_Click(object sender, EventArgs e)
        {
            StartWorld();
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
                if (validationPassed && fishArr[x, y] == null)
                {
                    Fish fish = new Fish(fishCount, x, y, Brushes.Green);
                    fishCount++;
                    fishArr[x, y] = fish;
                }
                    
            }

            if (e.Button == MouseButtons.Right)
            {
                var x = e.Location.X / resolution;
                var y = e.Location.Y / resolution;
                var validationPassed = ValidateMousePosition(x, y);
                if (validationPassed && fishArr[x, y] != null)
                {
                    fishArr[x, y].State = false;
                    //fishCount--;
                    //fishArr[x, y] = fish;
                }
            }
        }

        private bool ValidateMousePosition(int x, int y)
        {
            return x >= 0 && y >= 0 && x < cols && y < rows;
        }

        private void nudDensity_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
