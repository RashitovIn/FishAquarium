using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FishAquarium
{
    class World
    {
        private Random random;

        private int Width { get; set; }
        private int Height { get; set; }

        public delegate void MethodContainer();
        public event MethodContainer onCount;

        public World()
        {
            random = new Random();
        }

        public World(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            random = new Random();
        }

        public void FishCheckEvents(Entity fish)
        {
            if (fish.Energy == 0)
            {

            }
        }

        // Определение коллизии со всех сторон объекта
        public static bool CheckCollisionFromAllSides(List<Entity> worldArr, Entity fish)
        {
            Rectangle actRect = new Rectangle(fish.PosX, fish.PosY, fish.Body.Width, fish.Body.Height);
            foreach (Entity obj in worldArr)
            {
                if (obj != fish && obj.Type != "Worm" && obj.Body.IntersectsWith(actRect))
                {
                    return true;
                }
            }
            return false;
        }

        // Определение коллизии со стороны движения объекта
        public static List<char> CheckCollision(List<Entity> worldArr, Entity fish)
        {
            List<char> collSide = new List<char>();
            Rectangle actRect = new Rectangle(fish.PosX + fish.Dx * fish.Speed, fish.PosY + fish.Dy * fish.Speed, fish.Body.Width, fish.Body.Height);
            foreach (Entity obj in worldArr)
            {
                if (obj != fish && obj.Type != "Worm" && obj.Body.IntersectsWith(actRect))
                {
                    if (fish.Dx > 0)
                        collSide.Add('r');
                    else if (fish.Dx < 0)
                        collSide.Add('l');

                    if (fish.Dy > 0)
                        collSide.Add('b');
                    else if (fish.Dy < 0)
                        collSide.Add('t');
                }
            }
            return collSide;
        }

        public bool CheckBorders(Entity fobj)
        {
            Rectangle obj = new Rectangle(fobj.PosX + fobj.Dx * fobj.Speed, fobj.PosY + fobj.Dy * fobj.Speed, fobj.Body.Width, fobj.Body.Height);
            if (obj.Top > 0 && obj.Bottom < Height && obj.Left > 0 && obj.Right < Width)
                return true;
            return false;
        }

        public void ChooseDir(Entity fish)
        {
            fish.Dx = 0;
            fish.Dy = 0;

            if (fish.Target[0] > fish.Head[0])
            {
                if (Math.Abs(fish.Target[0] - fish.Head[0]) >= fish.Body.Width || fish.Target[1] == fish.Head[1])
                    fish.Dx = 1;
            }
            else if (fish.Target[0] < fish.Head[0])
            {
                if (Math.Abs(fish.Target[0] - fish.Head[0]) >= fish.Body.Width || fish.Target[1] == fish.Head[1])
                    fish.Dx = -1;
            }

            if (fish.Target[1] > fish.Head[1])
                fish.Dy = 1;
            else if (fish.Target[1] < fish.Head[1])
                fish.Dy = -1;
        }

        public void PrepareFishMove(List<Entity> worldArr, Entity fish)
        {
            int OptSpeedX = Math.Min(Math.Abs(fish.Target[0] - fish.Head[0]), fish.Speed);
            int OptSpeedY = Math.Min(Math.Abs(fish.Target[1] - fish.Head[1]), fish.Speed);

            if (OptSpeedX == 0)
                OptSpeedX = fish.Speed;

            if (OptSpeedY == 0)
                OptSpeedY = fish.Speed;

            ChooseDir(fish);

            int[] points = new int[] { fish.PosX + OptSpeedX * fish.Dx, fish.PosY + OptSpeedY * fish.Dy };

            List<char> collSide = CheckCollision(worldArr, fish);
            if (collSide.Count == 0)
            {
                fish.FishMove(OptSpeedX, OptSpeedY);
                TargetLimiter(fish, points);
            }
            else
            {
                if (collSide.Contains('l') || collSide.Contains('r'))
                {
                    fish.Dx *= -1;
                    //onCount += fish.FishChangeDir;
                }
                if (collSide.Contains('b') || collSide.Contains('t'))
                    fish.Dy *= -1;

                collSide = CheckCollision(worldArr, fish);
                if (collSide.Count == 0 && CheckBorders(fish))
                {
                    fish.FishMove(OptSpeedX, OptSpeedY);
                    TargetLimiter(fish, points);
                }
            }
        }

        private void GetTarget(Entity fish)
        {
            //fish.ldx = fish.Dx;
            int Dx = random.Next(-1, 2);
            int Dy = random.Next(-1, 2);
            int stepX = random.Next(200, 400);
            int stepY = random.Next(50, 100);
            int[] target = new int[2];

            target[0] = (fish.Head[0] + Dx * stepX + Width - fish.Body.Width) % (Width - fish.Body.Width);
            target[1] = (fish.Head[1] + Dy * stepY + Height - fish.Body.Height) % (Height - fish.Body.Height);
            fish.Target = target;
        }

        private void TargetLimiter(Entity fish, int[] lastMove)
        {
            int[,] newTL = new int[4, 2];
            for (int i = 0; i < 3; i++)
            {
                newTL[i, 0] = fish.TargetLimiter[i + 1, 0];
                newTL[i, 1] = fish.TargetLimiter[i + 1, 1];
            }
            newTL[3, 0] = lastMove[0];
            newTL[3, 1] = lastMove[1];
            fish.TargetLimiter = newTL;
        }

        private void TargetLimiter(Entity fish)
        {
            if (fish.TargetLimiter[0, 0] == fish.TargetLimiter[2, 0] && fish.TargetLimiter[0, 1] == fish.TargetLimiter[2, 1] && fish.TargetLimiter[1, 0] == fish.TargetLimiter[3, 0] && fish.TargetLimiter[1, 1] == fish.TargetLimiter[3, 1])
                GetTarget(fish);
        }

        public List<Entity> FishOverview(Entity thisFish, List<Entity> worldArr)
        {
            List<Entity> sight = new List<Entity>();

            foreach (Entity fish in worldArr)
            {
                if (fish != thisFish && thisFish.viewRadius.IntersectsWith(fish.Body))
                {
                    sight.Add(fish);
                }
            }

            return sight;
        }

        public static int MaxListInd(List<double> list)
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

        public void ViewAnalys(Entity fish, List<Entity> sight)
        {
            List<double> pref = new List<double>();
            bool isPred = false;
            List<Entity> predList = new List<Entity>();

            foreach (Entity obj in sight)
            {
                pref.Add(fish.Weights[obj.Type]);
                isPred = obj.Type == "Pred";
                if (obj.Type == "Pred")
                {
                    predList.Add(obj);
                }
            }

            int maxI = MaxListInd(pref);

            if (fish.Type == "Pred" && sight[maxI].Type != "Pred")
            {
                fish.Target[0] = sight[maxI].PosX + sight[maxI].Body.Width / 2;
                fish.Target[1] = sight[maxI].PosY + sight[maxI].Body.Height / 2;
            }
            else if (fish.Type == "Herb" && sight[maxI].Type != "Herb")
            {
                if (isPred)
                {
                    double minDistToEnemy = 9999999;
                    double cautionVecX = 0;
                    double cautionVecY = 0;

                    foreach (Entity enemy in predList)
                    {
                        int enX = sight[maxI].PosX + sight[maxI].Body.Width / 2;
                        int enY = sight[maxI].PosY + sight[maxI].Body.Height / 2;
                        int thisX = fish.PosX + fish.Body.Width / 2;
                        int thisY = fish.PosY + fish.Body.Height / 2;
                        double vecX = Math.Pow(enX - thisX, 2);
                        double vecY = Math.Pow(enY - thisY, 2);
                        double distance = Math.Sqrt(vecX + vecY);

                        if (distance < fish.Сaution && distance < minDistToEnemy)
                        {
                            minDistToEnemy = distance;
                            cautionVecX = thisX - enX + thisX;
                            cautionVecY = thisY - enY + thisY;
                        }
                    }

                    if (minDistToEnemy != 9999999)
                    {
                        int targetX;
                        int targetY;
                        if ((int)cautionVecX * fish.Dx >= Width)
                            targetX = Width;
                        else if ((int)cautionVecX * fish.Dx <= 0)
                            targetX = 0;
                        else
                            targetX = (int)cautionVecX * fish.Dx;

                        if((int)cautionVecY * fish.Dy >= Height)
                            targetY = Height;
                        else if ((int)cautionVecY * fish.Dy <= 0)
                            targetY = 0;
                        else
                            targetY = (int)cautionVecY * fish.Dy;

                        fish.Target[0] = (targetX);
                        fish.Target[1] = (targetY);
                    }
                }
                else
                {
                    fish.Target[0] = sight[maxI].PosX + sight[maxI].Body.Width / 2;
                    fish.Target[1] = sight[maxI].PosY + sight[maxI].Body.Height / 2;
                }
            }
        }

        public bool CheckFishEat(Entity fish, List<Entity> worldArr)
        {
            Rectangle actRect = new Rectangle(fish.PosX + fish.Dx * fish.Speed, fish.PosY + fish.Dy * fish.Speed, fish.Body.Width, fish.Body.Height);
            foreach (Entity obj in worldArr)
            {
                if (fish.Type == "Herb" && obj.Type == "Worm" && obj.Body.IntersectsWith(actRect))
                {
                    fish.Energy += obj.GiveEnergy;
                    worldArr.Remove(obj);
                    return true;
                }
                else if (fish.Type == "Pred" && (obj.Type == "Herb" || obj.Type == "Worm") && obj != fish && obj.Body.IntersectsWith(actRect))
                {
                    fish.Energy += obj.GiveEnergy;
                    worldArr.Remove(obj);
                    return true;
                }
            }
            return false;
        }

        public void UpdateWorld(ref List<Entity> worldArr, Entity obj, ref ListBox lb)
        {
            if (obj.Type == "Worm" || obj.Type == "Die")
            {
                if (CheckBorders(obj))//CheckCollision(worldArr, obj).Count == 0 &&
                {
                    obj.FishMove(obj.Speed, obj.Speed);
                }
                return;
            }

            if (FishOverview(obj, worldArr).Count != 0)
            {
                ViewAnalys(obj, FishOverview(obj, worldArr));
            }
            else
            {
                if (obj.Body.Contains(obj.Target[0], obj.Target[1]))
                    GetTarget(obj);
                //if (obj.Target[0] == obj.Head[0] && obj.Target[1] == obj.Head[1])
                  //  GetTarget(obj);
            }

            PrepareFishMove(worldArr, obj);
            TargetLimiter(obj);

            obj.Energy -= 0.5;

            if (obj.Energy == 0)
                onCount += obj.FishDie;

            //onCount += worldArr[i].FishMove;

            onCount?.Invoke();
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