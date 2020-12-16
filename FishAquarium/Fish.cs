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
        public int ViewRadiusPar { get; set; }
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

        public Entity(int posX, int posY, Bitmap[] sprite)
        {
            dInfo = "left";
            //random = new Random(posX + posY);
            PosX = posX;
            PosY = posY;
            Head = new int[] { PosX, PosY + Body.Height };
            State = true;
            Target = new int[] { PosX, PosY };
            TargetLimiter = new int[8, 2];
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
            Head[1] = PosY + Body.Height / 2;
            Body.X = PosX;
            Body.Y = PosY;

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
            else
                Head[0] = PosX;

            viewRadius.X = Head[0] - 50;
            viewRadius.Y = Head[1] - 50;
        }

        public void Destroy()
        {
            State = false;
            Body = new Rectangle(-1, -1, 0, 0);
            Sprite = null;
            Head[0] = -1;
            Head[1] = -1;
        }
    }

    class PredFish : Entity
    {
        public PredFish(int posX, int posY, Bitmap[] sprite, int width, int height, Random random) : base(posX, posY, sprite)
        {
            Type = "Pred";
            Energy = 120;
            ViewRadiusPar = 70;
            //Color = Brushes.Red;
            Speed = random.Next(5, 15);
            Body = new Rectangle(posX, posY, width, height);
            viewRadius = new Rectangle(posX - ViewRadiusPar / 2, posY - ViewRadiusPar / 2, ViewRadiusPar, ViewRadiusPar);
            Weights = new Dictionary<string, double>
            {
                ["Pred"] = 0,
                ["Worm"] = 0.9,
                ["Herb"] = 0.7,
                ["Die"] = 0.5
            };
        }
    }

    class HerbFish : Entity
    {
        public HerbFish(int posX, int posY, Bitmap[] sprite, int width, int height, Random random) : base(posX, posY, sprite)
        {
            Type = "Herb";
            Energy = 150;
            ViewRadiusPar = 200;
            //Color = Brushes.Green;
            Speed = random.Next(10, 25);
            Body = new Rectangle(posX, posY, width, height);
            viewRadius = new Rectangle(posX - ViewRadiusPar / 2, posY - ViewRadiusPar / 2, ViewRadiusPar, ViewRadiusPar);
            Сaution = 500;
            Weights = new Dictionary<string, double>
            {
                ["Pred"] = 0.9,
                ["Worm"] = 0.6,
                ["Herb"] = 0,
                ["Die"] = 0
            };
        }
    }

    class Worm : Entity
    {
        public Worm(int posX, int posY, Bitmap[] sprite, int width, int height, Random random) : base(posX, posY, sprite)
        {
            //Color = Brushes.Yellow;
            GiveEnergy = 10;
            Type = "Worm";
            Energy = 0;
            Speed = random.Next(5, 15);
            Dx = 0;
            Dy = 1;
            Body = new Rectangle(posX, posY, width, height);
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
