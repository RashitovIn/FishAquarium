using System;
using System.Collections.Generic;
using System.Drawing;

namespace FishAquarium
{
    abstract class Entity
    {
        public Rectangle Body;
        public Rectangle viewRadius;
        public Bitmap Sprite;
        public Bitmap deadSprite;
        public Bitmap aliveLeftSprite;
        public Bitmap aliveRightSprite;
        public string dInfo;
        public Brush Color { get; set; }
        public bool State { get; set; } //жива или мертва
        public double Energy { get; set; }
        public int Speed { get; set; }
        public double GiveEnergy { get; set; }
        public string Type { get; set; }
        public int[] Head { get; set; }
        public double Сaution { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public int[] Target { get; set; }
        public int[,] TargetLimiter { get; set; }
        public int SignOfGoal { get; set; }
        public int ldx = -1;
        public int Dx { get; set; } = -1;
        public int Dy { get; set; }

        Random random;

        public Dictionary<string, double> Weights { get; set; }
        public double[] LayerOdds { get; set; } // Коэффициенты слоёв

        public Entity(int posX, int posY, Bitmap[] sprite)
        {
            dInfo = "left";
            //random = new Random(posX + posY);
            PosX = posX;
            PosY = posY;
            Head = new int[] { PosX, PosY + Body.Height };
            State = true;
            Target = new int[] { PosX, PosY };
            TargetLimiter = new int[4, 2];
            aliveLeftSprite = sprite[0];
            aliveRightSprite = sprite[2];
            deadSprite = sprite[1];
            Sprite = aliveLeftSprite;
            Color = Brushes.Transparent;
        }

        public void FishDie()
        {
            //Color = Brushes.Gray;
            Sprite = deadSprite;
            GiveEnergy = 3;
            Type = "Die";
            Dx = 0;
            Dy = -1;
        }

        public void FishMove(int OptSpeedX, int OptSpeedY)
        {
            PosX += OptSpeedX * Dx;
            PosY += OptSpeedY * Dy;
            //Head[0] = PosX;
            Head[1] = PosY + Body.Height / 2;
            Body.X = PosX;
            Body.Y = PosY;
            //if (Dx != ldx && Dx != 0)
            //{
            if (Dx == -1)
            {
                dInfo = "left";
                Sprite = aliveLeftSprite;
                Head[0] = PosX;
            }
            else if (Dx == 1)
            {
                dInfo = "right";
                Sprite = aliveRightSprite;
                Head[0] = PosX + Body.Width;
            }
            //fish.Sprite.RotateFlip(RotateFlipType.RotateNoneFlipX);
            //ldx = Dx;
            //}
            else
            {
                Head[0] = PosX;
            }
            viewRadius.X = Head[0] - 50;
            viewRadius.Y = Head[1] - 50;
        }
    }

    class PredFish : Entity
    {
        public PredFish(int posX, int posY, Bitmap[] sprite, Random random) : base(posX, posY, sprite)
        {
            Type = "Pred";
            Energy = 1000;
            //Color = Brushes.Red;
            Speed = random.Next(5, 20);
            Body = new Rectangle(posX, posY, 100, 30);
            viewRadius = new Rectangle(posX - 50, posY - 50, 100, 100);
            //LayerOdds = new double[] { 1, 0.6, 0.2, 0.1, 0.08, 0.2, 0.1, 0.05, 0.03, 0.005, 0.0002 };
            Weights = new Dictionary<string, double>
            {
                ["Pred"] = 0,
                ["Worm"] = 0.9,
                ["Herb"] = 0.7,
                ["Die"] = 0.5,
                ["None"] = 0,
            };
        }
    }

    class HerbFish : Entity
    {
        public HerbFish(int posX, int posY, Bitmap[] sprite, Random random) : base(posX, posY, sprite)
        {
            Сaution = 100;
            Type = "Herb";
            Energy = 1000;
            //Color = Brushes.Green;
            Speed = random.Next(5, 20);
            Body = new Rectangle(posX, posY, 60, 30);
            viewRadius = new Rectangle(posX - 50, posY - 50, 100, 100);
            //LayerOdds = new double[] { 1, 0.5, 0.2, 0.1, 0.08, 0.2, 0.1, 0.05, 0.03, 0.005, 0.0002 };
            Weights = new Dictionary<string, double>
            {
                ["Pred"] = 0.9,
                ["Worm"] = 0.6,
                ["Herb"] = 0,
                ["Die"] = 0,
                ["None"] = 0,
            };
        }
    }

    class Worm : Entity
    {
        public Worm(int posX, int posY, Bitmap[] sprite, Random random) : base(posX, posY, sprite)
        {
            //Color = Brushes.Yellow;
            GiveEnergy = 10;
            Type = "Worm";
            Energy = 0;
            Speed = random.Next(5, 15);
            Dx = 0;
            Dy = 1;
            Body = new Rectangle(posX, posY, 25, 25);
        }
    }
    class Weed : Entity
    {
        public Weed(int posX, int posY, Bitmap[] sprite) : base(posX, posY, sprite)
        {
            PosX = posX;
            PosY = posY;
            //State = true;
            Color = Brushes.LimeGreen;
            GiveEnergy = 10;
            Type = "Weed";
            Energy = 0;
        }

        /*public void CreateWeedRow(ref Fish fishArr, )
        {

        }*/
    }

}
