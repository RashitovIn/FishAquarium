using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace FishAquarium
{
    public delegate void FishDie(Entity obj);
    public delegate void FishSteal(Entity obj);
    public class World
    {
        Random random;
        GraphicsPath ground;
        Graphics graphics;
        List<Entity> worldArr;
        PictureBox fieldPB;
        Cat cat;
        Cleaner cleaner;

        string ratioText;

        int[] ratio = new int[2];

        readonly int MAX_FISH_WIDTH = 200;
        readonly int MAX_FISH_HEIGHT = 100;

        int bitmapsCount;
        Bitmap[,] Bitmaps;

        public int fishCount;
        public int fishPredCount;
        public int fishHerbCount;

        public Font drawFont = new Font("Arial", 10);
        public SolidBrush drawBrush = new SolidBrush(Color.White);

        public int Width { get; private set; }
        public int Height { get; private set; }

        public World(int width, int height, PictureBox fieldPB, string ratioStr)
        {
            Width = width;
            Height = height;
            this.fieldPB = fieldPB;
            ratioText = ratioStr;
            random = new Random();
        }

        void LoadImages(string[,] usedBitmaps)
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

        void CreateGround()
        {
            ground = new GraphicsPath();
            float[] X = { 0, 0, (float)(Width * 0.2), (float)(Width * 0.45), (float)(Width * 0.58), (float)(Width * 0.79), (float)(Width * 0.95), Width, Width };
            float[] Y = { Height, Height * (float)0.75, Height * (float)0.95, Height * (float)0.85, Height * (float)0.9, Height - 10, Height * (float)0.9, Height * (float)0.75, Height };

            PointF[] curvePoints = new PointF[Y.Length];
            for (int i = 0; i < curvePoints.Length; i++)
            {
                PointF point = new PointF(X[i], Y[i]);
                curvePoints[i] = point;
            }

            ground.AddClosedCurve(curvePoints);
            ground.CloseAllFigures();
        }

        public void StartWorld()
        {
            string[,] usedBitmaps = new string[,]
            {
                {"okun.bmp", "okun_dead.bmp"},
                {"chuka.bmp", "chuka_dead.bmp"},
                {"fish_xz.bmp", "fish_xz_dead.bmp"},
                {"worm.bmp", "worm.bmp"},
                {"cat.bmp", "cat.bmp"},
                {"cleaner.bmp", "cleaner.bmp"}
            };

            bitmapsCount = usedBitmaps.Length / 2;
            LoadImages(usedBitmaps);

            fishPredCount = 0;
            fishHerbCount = 0;

            string[] ratioStr = ratioText.Split(new char[] { '/' });

            int i = 0;
            foreach (string s in ratioStr)
            {
                ratio[i] = Convert.ToInt32(s);
                i++;
            }

            fishCount = ratio[0] + ratio[1];
            worldArr = new List<Entity>();

            fieldPB.Image = new Bitmap(fieldPB.Width, fieldPB.Height);
            graphics = Graphics.FromImage(fieldPB.Image);
            CreateGround();

            random = new Random();

            cat = new Cat(Bitmaps.GetRow(4)[0], random);
            cleaner = new Cleaner(Bitmaps.GetRow(5)[0], random);

            i = 0;
            int x, y;
            int lastX = 0;
            int lastY = 50;
            while (i < ratio[0])
            {
                x = (lastX + Width + MAX_FISH_WIDTH) % Width;
                y = lastY;
                if (x + MAX_FISH_WIDTH >= Width)
                    y = (y + MAX_FISH_HEIGHT + Height) % Height;
                lastX = x;
                lastY = y;
                PredFish fish = new PredFish(x, y, Bitmaps.GetRow(1), this, 100, 30, random);
                worldArr.Add(fish);
                fish.DieEvent += cleaner.FishDied;
                fish.OnTheSurface += cat.Stealing;
                fishPredCount++;
                i++;
            }

            while (i < fishCount)
            {
                x = (lastX + Width + MAX_FISH_WIDTH) % Width;
                y = lastY;
                if (x + MAX_FISH_WIDTH >= Width)
                    y = (y + MAX_FISH_HEIGHT + Height) % Height;
                lastX = x;
                lastY = y;
                HerbFish fish;
                if (random.Next(2) == 0)
                    fish = new HerbFish(x, y, Bitmaps.GetRow(0), this, 60, 30, random);
                else
                    fish = new HerbFish(x, y, Bitmaps.GetRow(2), this, 70, 35, random);
                worldArr.Add(fish);
                fish.DieEvent += cleaner.FishDied;
                fish.OnTheSurface += cat.Stealing;
                fishHerbCount++;
                i++;
            }
        }

        public void Upgrade()
        {
            graphics.Clear(Color.FromArgb(0, Color.White));
            foreach (Entity obj in worldArr)
            {
                if (obj.State)
                {
                    Color cr = Color.FromArgb(43, 21, 8);
                    SolidBrush sb = new SolidBrush(cr);
                    graphics.FillPath(sb, ground);
                    graphics.FillRectangle(Brushes.Transparent, obj.Body);
                    graphics.DrawImage(obj.Sprite, obj.Body);
                    graphics.DrawImage(cleaner.cleanerBitmap, cleaner.cleanerRect);
                    graphics.DrawImage(cat.catBitmap, cat.catRect);

                    graphics.DrawString(Convert.ToString(obj.Energy), drawFont, drawBrush, obj.PosX, obj.PosY);

                    obj.UpdateFish(ref worldArr);
                }
            }
            cleaner.CleaningProc();
            cat.StealingProc();
            fieldPB.Refresh();
        }

        public bool CheckGroundColl(Rectangle obj)
        {
            int y = obj.Y + obj.Height;
            for (int i = obj.X; i < obj.X + obj.Width; i++)
            {
                if (ground.IsVisible(i, y))
                {
                    return true;
                }
            }
            return false;
        }

        public void FeedingFish(int x, int y)
        {
            var validationPassed = ValidateMousePosition(x, y);
            Worm worm = new Worm(x, y, Bitmaps.GetRow(3), this, 25, 25, random);
            if (validationPassed && !worm.CheckCollisionFromAllSides(worldArr))
                worldArr.Add(worm);
        }

        bool ValidateMousePosition(int x, int y)
        {
            return x >= 0 && y >= 0 && x < Width - 50 && y < Height - 50;
        }
    }

    public static class ArrayExtensions
    {
        public static T[] GetRow<T>(this T[,] data, int i)
        {
            return Enumerable.Range(0, data.GetLength(1)).Select(j => data[i, j]).ToArray();
        }
    }
}