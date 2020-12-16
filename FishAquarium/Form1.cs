using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace FishAquarium
{
    public partial class Form1 : Form
    {
        private Graphics graphics;
        private List<Entity> worldArr;
        private World world;
        private Random random;
        public GraphicsPath ground;
        private int currentGen = 0;
        private int[] ratio = new int[2];
        private int width;
        private int height;
        private readonly int MAX_FISH_WIDTH = 200;
        private readonly int MAX_FISH_HEIGHT = 100;

        private int bitmapsCount;
        private Bitmap[,] Bitmaps;

        public int fishCount;
        public int fishPredCount;
        public int fishHerbCount;

        public Font drawFont = new Font("Arial", 10);
        public SolidBrush drawBrush = new SolidBrush(Color.Black);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Text = $"Аквариум";
        }

        private void LoadImages(string[,] usedBitmaps)
        {
            Bitmaps = new Bitmap[bitmapsCount, 3];

            var appDir = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));
            var fullPath = Path.Combine(appDir, @"src");
            Bitmap[] image = new Bitmap[3];

            for (int i = 0; i < bitmapsCount; i++)
            {
                image[0] = new Bitmap(Image.FromFile(Path.Combine(fullPath, usedBitmaps[i, 0])));
                image[1] = new Bitmap(Image.FromFile(Path.Combine(fullPath, usedBitmaps[i, 1])));
                image[2] = new Bitmap(Image.FromFile(Path.Combine(fullPath, usedBitmaps[i, 0])));
                image[0].MakeTransparent(Color.FromArgb(255, 255, 255));
                image[1].MakeTransparent(Color.FromArgb(255, 255, 255));
                image[2].MakeTransparent(Color.FromArgb(255, 255, 255));
                image[2].RotateFlip(RotateFlipType.RotateNoneFlipX);
                Bitmaps[i, 0] = image[0];
                Bitmaps[i, 1] = image[1];
                Bitmaps[i, 2] = image[2];
            }
        }

        private void StartWorld()
        {
            string[,] usedBitmaps = new string[,]
            {
                {"okun.bmp", "okun_dead.bmp"},
                {"chuka.bmp", "chuka_dead.bmp"},
                {"fish_xz.bmp", "fish_xz_dead.bmp"},
                {"worm.bmp", "worm.bmp"}
            };

            bitmapsCount = usedBitmaps.Length / 2;
            LoadImages(usedBitmaps);
           
            if (timer.Enabled)
                return;

            currentGen = 0;
            genLb.Text = Convert.ToString(currentGen);

            timer.Interval = (int)nudTimer.Value;
            nudTimer.Enabled = false;
            nudDensity.Enabled = false;
            ratioTB.Enabled = false;
            contBtn.Enabled = false;

            //bool state;
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

            fishCount = ratio[0] + ratio[1];
            worldArr = new List<Entity>();
            width = fieldPB.Width;
            height = fieldPB.Height;
            world = new World(width, height);

            random = new Random();

            i = 0;
            int x, y;
            int lastX = 0;
            int lastY = 50;
            while (i < ratio[0])
            {
                x = (lastX + width + MAX_FISH_WIDTH) % width;

                y = lastY;
                if (x + MAX_FISH_WIDTH >= width)
                    y = (y + MAX_FISH_HEIGHT + height) % height;
                lastX = x;
                lastY = y;
                PredFish fish = new PredFish(x, y, Bitmaps.GetRow(1), random);
                worldArr.Add(fish);
                fishPredCount++;
                i++;
            }

            while (i < fishCount)
            {
                x = (lastX + width + MAX_FISH_WIDTH) % width;

                y = lastY;
                if (x + MAX_FISH_WIDTH >= width)
                    y = (y + MAX_FISH_HEIGHT + height) % height;
                lastX = x;
                lastY = y;
                HerbFish fish = new HerbFish(x, y, Bitmaps.GetRow(0), random);
                worldArr.Add(fish);
                fishHerbCount++;
                i++;
            }
            /*PredFish fish = new PredFish(100, 100, Bitmaps.GetRow(1), random);
            worldArr.Add(fish);
            HerbFish fishs = new HerbFish(200, 200, Bitmaps.GetRow(0), random);
            worldArr.Add(fishs);
            PredFish fishhh = new PredFish(300, 300, Bitmaps.GetRow(1), random);
            worldArr.Add(fishhh);
            HerbFish fishshh = new HerbFish(400, 400, Bitmaps.GetRow(0), random);
            worldArr.Add(fishshh);*/

            fieldPB.Image = new Bitmap(fieldPB.Width, fieldPB.Height);
            graphics = Graphics.FromImage(fieldPB.Image);
            countLb.Text = Convert.ToString(fishCount);
            predCountLb.Text = Convert.ToString(fishPredCount);
            herbCountLb.Text = Convert.ToString(fishHerbCount);

            ground = new GraphicsPath();
            float[] X = { 0, 0, (float)(width * 0.2), (float)(width * 0.45), (float)(width * 0.58), (float)(width * 0.79), (float)(width * 0.95), width, width };
            float[] Y = { height, height * (float)0.75, height * (float)0.95, height * (float)0.85, height * (float)0.9, height - 10, height * (float)0.9, height * (float)0.75, height };

            PointF[] curvePoints = new PointF[Y.Length];
            for (i = 0; i < curvePoints.Length; i++)
            {
                PointF point = new PointF(X[i], Y[i]);
                curvePoints[i] = point;
            }

            ground.AddClosedCurve(curvePoints);
            ground.CloseAllFigures();

            timer.Start();
        }

        private void Debug(Entity fish)
        {
            //graphics.FillRectangle(Brushes.Red, fish.Target[0], fish.Target[1], 5, 5);
            //graphics.FillRectangle(Brushes.Yellow, fish.Head[0], fish.Head[1], 5, 5);
            //graphics.DrawString(Convert.ToString(fish.Dx) + "  |  " + Convert.ToString(fish.ldx), new Font("Arial", 14), Brushes.White, fish.PosX + (float)0.5, fish.PosY + (float)0.5);
            //graphics.DrawString(fish.dInfo, new Font("Arial", 14), Brushes.White, fish.PosX + (float)0.5, fish.PosY + (float)0.5);
            Pen p = new Pen(Color.Green, 2);
            //graphics.DrawLine(p, (float)fish.Head[0], (float)fish.Head[1], (float)fish.Target[0], (float)fish.Target[1]);
        }

        private void Upgrade()
        {
            graphics.Clear(Color.FromArgb(0, Color.White));
            foreach (Entity fish in worldArr)
            {
                if (fish.State)
                {
                    //graphics.FillRectangle(Brushes.Blue, fish.viewRadius);
                    //graphics.FillPath(Brushes.Brown, ground);
                    graphics.FillRectangle(fish.Color, fish.Body);
                    //if (ground.IsVisible(fish.PosX, fish.PosY))
                    //listBox1.Items.Add("Yes");
                    graphics.DrawImage(fish.Sprite, fish.Body);
                    if (fish.Type != "Worm" && fish.Type != "Die")
                    {
                        Debug(fish);
                        //graphics.DrawString(Convert.ToString(fish.Energy), drawFont, drawBrush, fish.PosX, fish.PosY);
                    }
                }
                world.UpdateWorld(ref worldArr, fish, ref listBox1);
            }

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
                var x = e.Location.X;
                var y = e.Location.Y;
                var validationPassed = ValidateMousePosition(x, y, 'l');
                Worm worm = new Worm(x, y, Bitmaps.GetRow(3), random);
                if (validationPassed && !World.CheckCollisionFromAllSides(worldArr, worm))
                    worldArr.Add(worm);
            }

            if (e.Button == MouseButtons.Right)
            {
                var x = e.Location.X;
                var y = e.Location.Y;
                var validationPassed = ValidateMousePosition(x, y);

                if (validationPassed)
                {
                    foreach (Entity fish in worldArr)
                    {
                        if (fish.Body.Contains(x, y))
                        {
                            worldArr.Remove(fish);
                            return;
                        }
                    }
                }
            }
        }

        private bool ValidateMousePosition(int x, int y)
        {
            return x >= 0 && y >= 0 && x < width && y < height;
        }

        private bool ValidateMousePosition(int x, int y, char l)
        {
            return x >= 0 && y >= 0 && x < width - 50 && y < height - 50;
        }

    }
}
