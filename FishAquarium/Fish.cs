using System;
using System.Collections.Generic;
using System.Drawing;

namespace FishAquarium
{
    class PredFish : Entity
    {
        public event FishDie DieEvent;
        public event FishSteal OnTheSurface;
        public PredFish(int posX, int posY, Bitmap[] sprite, World world, int width, int height, Random random) : base(posX, posY, sprite, world)
        {
            Type = "Pred";
            Energy = 70;
            ViewRadiusPar = 100;
            hunger = 30;//100
            Speed = random.Next(5, 15);
            Body = new Rectangle(posX, posY, width, height);
            viewRadius = new Rectangle(Head[0] - ViewRadiusPar / 2, Head[1] - ViewRadiusPar / 2, ViewRadiusPar, ViewRadiusPar);
            Weights = new Dictionary<string, double>
            {
                ["Pred"] = 0,
                ["Worm"] = 0.9,
                ["Herb"] = 0.7,
                ["Die"] = 0.5,
                ["Stealed"] = 0
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
                else if (obj.Type == "Die" && obj.Body.IntersectsWith(Body))
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

        private protected void ViewAnalys(List<Entity> sight)
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
                if (PosY < 20)
                {
                    DieEvent(this);
                }
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
            }

            FishEat(worldArr);

            PrepareFishMove(worldArr);
            TargetLimiter();

            if (PosY <= 30)
            {
                OnTheSurface(this);
            }

            Energy -= 1;

            if (Energy <= 0)
            {
                FishDie();
            }
        }
    }

    class HerbFish : Entity
    {
        public event FishDie DieEvent;
        public event FishSteal OnTheSurface;
        public HerbFish(int posX, int posY, Bitmap[] sprite, World world, int width, int height, Random random) : base(posX, posY, sprite, world)
        {
            Type = "Herb";
            Energy = 100;
            ViewRadiusPar = 100;
            GiveEnergy = 30;
            Speed = random.Next(10, 25);
            Body = new Rectangle(posX, posY, width, height);
            viewRadius = new Rectangle(Head[0] - ViewRadiusPar / 2, Head[1] - ViewRadiusPar / 2, ViewRadiusPar, ViewRadiusPar);
            Сaution = 500;
            Weights = new Dictionary<string, double>
            {
                ["Pred"] = 0.9,
                ["Worm"] = 0.6,
                ["Herb"] = 0,
                ["Die"] = 0,
                ["Stealed"] = 0
            };
        }

        private protected void ViewAnalys(List<Entity> sight)
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
                if (PosY < 20)
                {
                    DieEvent(this);
                }
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
            }

            FishEat(worldArr);

            PrepareFishMove(worldArr);
            TargetLimiter();

            if (PosY <= 30)
            {
                OnTheSurface(this);
            }

            Energy -= 1;

            if (Energy <= 0)
            {
                FishDie();
            }
        }
    }

    class Worm : Entity
    {
        public Worm(int posX, int posY, Bitmap[] sprite, World world, int width, int height, Random random) : base(posX, posY, sprite, world)
        {
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
