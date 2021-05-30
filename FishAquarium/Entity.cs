using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace FishAquarium
{
    public abstract class Entity
    {
        protected Rectangle viewRadius;
        public Rectangle Body;

        public Bitmap Sprite { get; protected set; }
        protected Bitmap deadSprite;
        protected Bitmap aliveLeftSprite;
        protected Bitmap aliveRightSprite;
        protected int hunger;

        public bool State { get; private protected set; } //жива или мертва
        public double Energy { get; private protected set; }
        public int Speed { get; private protected set; }
        public double GiveEnergy { get; private protected set; }
        public string Type { get; private protected set; }
        public int[] Head { get; private protected set; }
        public double Сaution { get; private protected set; }
        public int ViewRadiusPar { get; private protected set; }
        public int PosX { get; private protected set; }
        public int PosY { get; private protected set; }
        public int[] Target { get; private set; }
        public int[,] TargetLimiterCont { get; private set; }

        public int Dx { get; private protected set; } = -1;
        public int Dy { get; private protected set; }

        protected World world;
        Random random;

        private protected Dictionary<string, double> Weights { get; set; }

        public Entity(int posX, int posY, Bitmap[] sprite, World world)
        {
            this.world = world;
            PosX = posX;
            PosY = posY;
            Head = new int[] { PosX, PosY + Body.Height };
            State = true;
            Target = new int[] { PosX, PosY };
            TargetLimiterCont = new int[8, 2];
            aliveLeftSprite = sprite[0];
            aliveRightSprite = sprite[2];
            deadSprite = sprite[1];
            Sprite = aliveLeftSprite;
            random = new Random(PosX + PosY);
        }

        public void FishDie()
        {
            Sprite = deadSprite;
            GiveEnergy = 10;
            Type = "Die";
            Dx = 0;
            Dy = -1;
        }

        // Определение коллизии со всех сторон объекта
        public bool CheckCollisionFromAllSides(List<Entity> worldArr)
        {
            Rectangle actRect = new Rectangle(PosX, PosY, Body.Width, Body.Height);
            foreach (Entity obj in worldArr)
            {
                if (obj != this && obj.Type != "Worm" && obj.Body.IntersectsWith(actRect))
                {
                    return true;
                }
            }
            return false;
        }

        // Определение коллизии со стороны движения объекта
        private protected List<char> CheckCollision(List<Entity> worldArr)
        {
            List<char> collSide = new List<char>();
            Rectangle actRect = new Rectangle(PosX + Dx * Speed, PosY + Dy * Speed, Body.Width, Body.Height);
            if (!world.CheckGroundColl(actRect))
            {
                foreach (Entity obj in worldArr)
                {
                    if (obj != this && obj.Type != "Worm" && obj.Type != "Die" && obj.Body.IntersectsWith(actRect))
                    {
                        if (Dx > 0)
                            collSide.Add('r');
                        else if (Dx < 0)
                            collSide.Add('l');

                        if (Dy > 0)
                            collSide.Add('b');
                        else if (Dy < 0)
                            collSide.Add('t');
                    }
                }
            }
            else
            {
                collSide.Add('f');
            }
            return collSide;
        }

        private protected bool CheckBorders()
        {
            Rectangle obj = new Rectangle(PosX + Dx * Speed, PosY + Dy * Speed, Body.Width, Body.Height);
            if (obj.Top > 0 && obj.Bottom < world.Height && obj.Left > 0 && obj.Right < world.Width)
                return true;
            return false;
        }

        private protected void ChooseDir()
        {
            //fish.Dx = 0;
            Dy = 0;

            if (Target[0] > Head[0])
            {
                if (Math.Abs(Target[0] - Head[0]) >= Body.Width || Target[1] == Head[1])
                    Dx = 1;
            }
            else if (Target[0] < Head[0])
            {
                if (Math.Abs(Target[0] - Head[0]) >= Body.Width || Target[1] == Head[1])
                    Dx = -1;
            }

            if (Target[1] > Head[1])
                Dy = 1;
            else if (Target[1] < Head[1])
                Dy = -1;
        }

        private protected void FishMove(int OptSpeedX, int OptSpeedY)
        {
            PosX += OptSpeedX * Dx;
            PosY += OptSpeedY * Dy;
            Head[1] = PosY + Body.Height / 2;
            Body.X = PosX;
            Body.Y = PosY;

            if (Dx == -1)
            {
                Sprite = aliveLeftSprite;
                Head[0] = PosX;
            }
            else if (Dx == 1)
            {
                Sprite = aliveRightSprite;
                Head[0] = PosX + Body.Width;
            }
            else
                Head[0] = PosX;

            viewRadius.X = Head[0] - 50;
            viewRadius.Y = Head[1] - 50;
        }

        private protected virtual void FishEat(List<Entity> worldArr)
        {
            foreach (Entity obj in worldArr)
            {
                if (obj.Type == "Worm" && obj.Body.IntersectsWith(Body))
                {
                    Energy += obj.GiveEnergy;
                    obj.Destroy();
                }
            }
        }

        protected internal void Destroy()
        {
            State = false;
            Body = new Rectangle(-1, -1, 0, 0);
            Sprite = null;
            Head[0] = -1;
            Head[1] = -1;
        }

        private protected void PrepareFishMove(List<Entity> worldArr)
        {
            int OptSpeedX = Math.Min(Math.Abs(Target[0] - Head[0]), Speed);
            int OptSpeedY = Math.Min(Math.Abs(Target[1] - Head[1]), Speed);

            if (OptSpeedX == 0)
                OptSpeedX = Speed;

            if (OptSpeedY == 0)
                OptSpeedY = Speed;

            ChooseDir();

            int[] points = new int[] { PosX + OptSpeedX * Dx, PosY + OptSpeedY * Dy };

            List<char> collSide = CheckCollision(worldArr);
            if (collSide.Count == 0)
            {
                FishMove(OptSpeedX, OptSpeedY);
                TargetLimiter(points);
            }
            else
            {
                if (collSide.Contains('f'))
                {
                    Dx *= -1;
                    Dy *= -1;
                    //GetTarget(fish);
                }
                if (collSide.Contains('l') || collSide.Contains('r'))
                    Dx *= -1;
                if (collSide.Contains('b') || collSide.Contains('t'))
                    Dy *= -1;

                collSide = CheckCollision(worldArr);
                if (collSide.Count == 0 && CheckBorders())
                {
                    FishMove(OptSpeedX, OptSpeedY);
                    TargetLimiter(points);
                }
            }
        }

        private protected void GetTarget()
        {
            int Dx = random.Next(-1, 2);
            int Dy = random.Next(-1, 2);
            int stepX = random.Next(200, 400);
            int stepY = random.Next(200, 400);//50/100
            int[] target = new int[2];

            target[0] = (Head[0] + Dx * stepX + world.Width - Body.Width) % (world.Width - Body.Width);
            target[1] = (Head[1] + Dy * stepY + world.Height - Body.Height) % (world.Height - Body.Height);
            Target = target;
        }

        private protected void TargetLimiter(int[] lastMove)
        {
            int[,] newTL = new int[8, 2];
            for (int i = 0; i < 7; i++)
            {
                newTL[i, 0] = TargetLimiterCont[i + 1, 0];
                newTL[i, 1] = TargetLimiterCont[i + 1, 1];
            }
            newTL[7, 0] = lastMove[0];
            newTL[7, 1] = lastMove[1];
            TargetLimiterCont = newTL;
        }

        private protected void TargetLimiter()
        {
            int overtaps = 0; // Совпадений
            for (int i = 0; i < TargetLimiterCont.Length / 4; i++)
            {
                if (TargetLimiterCont[i, 0] == TargetLimiterCont[i + 2, 0] && TargetLimiterCont[i, 1] == TargetLimiterCont[i + 2, 1])
                {
                    overtaps++;
                }
            }
            if (overtaps >= 4)
                GetTarget();
        }

        private protected List<Entity> FishOverview(List<Entity> worldArr)
        {
            List<Entity> sight = new List<Entity>();

            foreach (Entity fish in worldArr)
            {
                if (fish != this && viewRadius.IntersectsWith(fish.Body))
                {
                    sight.Add(fish);
                }
            }

            return sight;
        }

        private protected static int MaxListInd(List<double> list)
        {
            int maxI = 0;
            double maxEl = list[0];
            for (int i = 1; i < list.Count; i++)
            {
                if (list[i] > maxEl)
                {
                    maxEl = list[i];
                    maxI = i;
                }
            }

            return maxI;
        }

        public abstract void UpdateFish(ref List<Entity> worldArr);

        public void StealFish(int Y)
        {
            PosY = Y - Body.Height;
            Body.Y = PosY;
        }
    }
}
