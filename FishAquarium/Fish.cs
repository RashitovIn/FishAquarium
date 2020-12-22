using System;
using System.Collections.Generic;
using System.Drawing;

namespace FishAquarium
{
    abstract class Entity
    {
        protected Rectangle viewRadius;
        protected Rectangle body;
        public Rectangle Body 
        {
            get
            {
                return body;
            }
            protected set
            {
                body = value;
            }
        }
        public Bitmap Sprite { get; protected set; }
        protected Bitmap deadSprite;
        protected Bitmap aliveLeftSprite;
        protected Bitmap aliveRightSprite;
        public string dInfo;
        protected int hunger;

        public Brush Color { get; private protected set; }
        public bool State { get; private set; } //жива или мертва
        public double Energy { get; private protected set; }
        public int Speed { get; private protected set; }
        public double GiveEnergy { get; protected set; }
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
            dInfo = "left";
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
            Color = Brushes.Transparent;
            random = new Random(PosX + PosY);
        }

        public void FishDie()
        {
            if (Type == "Herb")
                world.fishHerbCount--;
            else if (Type == "Pred")
                world.fishPredCount--;
            world.fishCount--;
            //Color = Brushes.Gray;
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
            body.X = PosX;
            body.Y = PosY;

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
            if (Type == "Herb")
                world.fishHerbCount--;
            world.fishCount--;
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
            //if (fish.TargetLimiter[0, 0] == fish.TargetLimiter[2, 0] && fish.TargetLimiter[0, 1] == fish.TargetLimiter[2, 1] && fish.TargetLimiter[1, 0] == fish.TargetLimiter[3, 0] && fish.TargetLimiter[1, 1] == fish.TargetLimiter[3, 1])
            //GetTarget(fish);
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

        private protected virtual void ViewAnalys(List<Entity> sight)
        {

        }

        public virtual void UpdateFish(ref List<Entity> worldArr)
        {

        }
    }

    class PredFish : Entity
    {
        public PredFish(int posX, int posY, Bitmap[] sprite, World world, int width, int height, Random random) : base(posX, posY, sprite, world)
        {
            Type = "Pred";
            Energy = 7;
            ViewRadiusPar = 100;
            hunger = 30;//100
            //Color = Brushes.Red;
            Speed = random.Next(5, 15);
            Body = new Rectangle(posX, posY, width, height);
            viewRadius = new Rectangle(Head[0] - ViewRadiusPar / 2, Head[1] - ViewRadiusPar / 2, ViewRadiusPar, ViewRadiusPar);
            Weights = new Dictionary<string, double>
            {
                ["Pred"] = 0,
                ["Worm"] = 0.9,
                ["Herb"] = 0.7,
                ["Die"] = 0.5
            };
        }

        private protected override void FishEat(List<Entity> worldArr)
        {
            foreach (Entity obj in worldArr)
            {
                if (obj.Type == "Worm" && obj.Body.IntersectsWith(Body))
                {
                    Energy += obj.GiveEnergy;
                    obj.Destroy();
                }
                else if (Type == "Pred" && obj.Type == "Die" && obj.Body.IntersectsWith(Body))
                {
                    Energy += obj.GiveEnergy;
                    obj.Destroy();
                }
                else if (Energy <= hunger && Type == "Pred" && obj.Type == "Herb" && obj != this)
                {
                    Rectangle actRect = new Rectangle(Head[0] - Body.Height / 2, Head[1] - Body.Height / 2, Body.Height, Body.Height);
                    if (obj.Body.IntersectsWith(actRect))
                    {
                        Energy += obj.GiveEnergy;
                        obj.Destroy();
                    }
                }
            }
        }

        private protected override void ViewAnalys(List<Entity> sight)
        {
            List<double> pref = new List<double>();

            foreach (Entity obj in sight)
                pref.Add(Weights[obj.Type]);

            int maxI = MaxListInd(pref);

            if (sight[maxI].Type != "Pred")
            {
                if (sight[maxI].Type == "Worm" || sight[maxI].Type == "Die" || (sight[maxI].Type == "Herb" && Energy <= hunger))
                {
                    Target[0] = sight[maxI].PosX + sight[maxI].Body.Width / 2;
                    Target[1] = sight[maxI].PosY + sight[maxI].Body.Height / 2;
                }
                else
                {
                    GetTarget();
                }
            }
        }

        public override void UpdateFish(ref List<Entity> worldArr)
        {
            if (Type == "Die")
            {
                Rectangle actRect = new Rectangle(PosX + Dx * Speed, PosY + Dy * Speed, Body.Width, Body.Height);
                if (CheckBorders() && !world.CheckGroundColl(actRect))//CheckCollision(worldArr, obj).Count == 0 &&
                {
                    FishMove(Speed, Speed);
                }
                return;
            }

            if (FishOverview(worldArr).Count != 0)
            {
                ViewAnalys(FishOverview(worldArr));
            }
            else
            {
                if (Body.Contains(Target[0], Target[1]))
                    GetTarget();
                //if (obj.Target[0] == obj.Head[0] && obj.Target[1] == obj.Head[1])
                //  GetTarget(obj);
            }

            FishEat(worldArr);

            PrepareFishMove(worldArr);
            TargetLimiter();

            Energy -= 0.5;
        }
    }

    class HerbFish : Entity
    {
        public HerbFish(int posX, int posY, Bitmap[] sprite, World world, int width, int height, Random random) : base(posX, posY, sprite, world)
        {
            Type = "Herb";
            Energy = 100;
            ViewRadiusPar = 100;
            GiveEnergy = 30;
            //Color = Brushes.Green;
            Speed = random.Next(10, 25);
            Body = new Rectangle(posX, posY, width, height);
            viewRadius = new Rectangle(Head[0] - ViewRadiusPar / 2, Head[1] - ViewRadiusPar / 2, ViewRadiusPar, ViewRadiusPar);
            Сaution = 500;
            Weights = new Dictionary<string, double>
            {
                ["Pred"] = 0.9,
                ["Worm"] = 0.6,
                ["Herb"] = 0,
                ["Die"] = 0
            };
        }

        private protected override void FishEat(List<Entity> worldArr)
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

        private protected override void ViewAnalys(List<Entity> sight)
        {
            List<double> pref = new List<double>();
            bool isPred = false;
            List<Entity> predList = new List<Entity>();

            foreach (Entity obj in sight)
            {
                pref.Add(Weights[obj.Type]);
                isPred = obj.Type == "Pred";
                if (obj.Type == "Pred")
                {
                    predList.Add(obj);
                }
            }

            int maxI = MaxListInd(pref);

            if (sight[maxI].Type != "Herb" && sight[maxI].Type != "Die")
            {
                if (isPred)
                {
                    double minDistToEnemy = 9999999;
                    double cautionVecX = 0;
                    double cautionVecY = 0;

                    foreach (Entity enemy in predList)
                    {
                        if (enemy.Dx != Dx)
                        {
                            int enX = enemy.PosX + enemy.Body.Width / 2;
                            int enY = enemy.PosY + enemy.Body.Height / 2;
                            int thisX = PosX + Body.Width / 2;
                            int thisY = PosY + Body.Height / 2;
                            double vecX = Math.Pow(enX - thisX, 2);
                            double vecY = Math.Pow(enY - thisY, 2);
                            double distance = Math.Sqrt(vecX + vecY);

                            if (distance < Сaution && distance < minDistToEnemy)
                            {
                                minDistToEnemy = distance;
                                cautionVecX = thisX - enX + thisX;
                                cautionVecY = thisY - enY + thisY;
                            }
                        }
                    }

                    if (minDistToEnemy != 9999999)
                    {
                        int targetX;
                        int targetY;
                        if ((int)cautionVecX * Dx >= world.Width)
                            targetX = world.Width;
                        else if ((int)cautionVecX * Dx <= 0)
                            targetX = 0;
                        else
                            targetX = (int)cautionVecX;//* fish.Dx;

                        if ((int)cautionVecY * Dy >= world.Height)
                            targetY = world.Height - Body.Height;
                        else if ((int)cautionVecY * Dy <= 0)
                            targetY = 0 + Body.Height;
                        else
                            targetY = (int)cautionVecY * Dy;

                        Target[0] = (targetX);
                        Target[1] = (targetY);
                    }
                }
                else
                {
                    Target[0] = sight[maxI].PosX + sight[maxI].Body.Width / 2;
                    Target[1] = sight[maxI].PosY + sight[maxI].Body.Height / 2;
                }
            }
        }

        public override void UpdateFish(ref List<Entity> worldArr)
        {
            if (Type == "Die")
            {
                Rectangle actRect = new Rectangle(PosX + Dx * Speed, PosY + Dy * Speed, Body.Width, Body.Height);
                if (CheckBorders() && !world.CheckGroundColl(actRect))//CheckCollision(worldArr, obj).Count == 0 &&
                {
                    FishMove(Speed, Speed);
                }
                return;
            }

            if (FishOverview(worldArr).Count != 0)
            {
                ViewAnalys(FishOverview(worldArr));
            }
            else
            {
                if (Body.Contains(Target[0], Target[1]))
                    GetTarget();
                //if (obj.Target[0] == obj.Head[0] && obj.Target[1] == obj.Head[1])
                //  GetTarget(obj);
            }

            FishEat(worldArr);

            PrepareFishMove(worldArr);
            TargetLimiter();

            Energy -= 0.5;
        }
    }

    class Worm : Entity
    {
        public Worm(int posX, int posY, Bitmap[] sprite, World world, int width, int height, Random random) : base(posX, posY, sprite, world)
        {
            //Color = Brushes.Yellow;
            GiveEnergy = 25;
            Type = "Worm";
            Energy = 0;
            Speed = random.Next(5, 15);
            Dx = 0;
            Dy = 1;
            Body = new Rectangle(posX, posY, width, height);
        }

        public override void UpdateFish(ref List<Entity> worldArr)
        {
            if (Type == "Worm")
            {
                Rectangle actRect = new Rectangle(PosX + Dx * Speed, PosY + Dy * Speed, Body.Width, Body.Height);
                if (CheckBorders() && !world.CheckGroundColl(actRect))//CheckCollision(worldArr, obj).Count == 0 &&
                {
                    FishMove(Speed, Speed);
                }
                return;
            }
        }
    }
}
