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
                if (obj != fish && obj.Body.IntersectsWith(actRect))
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
                if (obj != fish && obj.Body.IntersectsWith(actRect))
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

            if (fish.Type == "Pred")
            {
                fish.Target[0] = sight[maxI].PosX + sight[maxI].Body.Width / 2;
                fish.Target[1] = sight[maxI].PosY + sight[maxI].Body.Height / 2;
            }
            else if (fish.Type == "Herb")
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
                        int targetX = 0;
                        int targetY = 0;
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
                if (CheckCollision(worldArr, obj).Count == 0 && CheckBorders(obj))
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
                if (obj.Target[0] == obj.Head[0] && obj.Target[1] == obj.Head[1])
                    GetTarget(obj);
            }

            PrepareFishMove(worldArr, obj);
            TargetLimiter(obj);

            obj.Energy -= 0.5;

            if (obj.Energy == 0)
                onCount += obj.FishDie;

            //onCount += worldArr[i].FishMove;

            //onCount?.Invoke();
        }

        /*private void GetDefTarget(Entity fish, string[,] field, ListBox lb)
        {
            int n = field.GetLength(0);
            double[,] oddsField = new double[n, n];
            bool isZero = true;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (field[i, j] != null)
                        oddsField[i, j] = fish.Weights[field[i, j]];
                    else
                        oddsField[i, j] = 0;

                    if (oddsField[i, j] != 0)
                        isZero = false;
                }
            }

            n--;
            int layersCount = n / 2 - 1;
            if (!isZero)
            {
                int count = 0;
                int col;
                while (layersCount > -1)
                {
                    col = 0 + count;
                    for (int j = count; j <= n - count; j++)
                    {
                        oddsField[col, j] = oddsField[col, j] * fish.LayerOdds[layersCount];
                    }

                    col = n - count;
                    for (int j = count; j <= n - count; j++)
                    {
                        oddsField[col, j] = oddsField[col, j] * fish.LayerOdds[layersCount];
                    }

                    for (int j = count + 1; j <= n - count - 1; j++)
                    {
                        oddsField[count, j] = oddsField[count, j] * fish.LayerOdds[layersCount];
                    }

                    for (int j = count + 1; j <= n - count - 1; j++)
                    {
                        oddsField[n - count, j] = oddsField[n - count, j] * fish.LayerOdds[layersCount];
                    }

                    layersCount--;
                    count++;
                }

                int stepX;
                int stepY;
                int[] target = new int[2];
                int[] maxInd = MaxIndex(oddsField);
                //
                for (int i = 0; i <= n; i++)
                {
                    lb.Items.Add(Convert.ToString(oddsField[i, 0] + " " + oddsField[i, 1] + " " + oddsField[i, 2] + " " + oddsField[i, 3] + " " + oddsField[i, 4]));
                }
                lb.Items.Add("--");
                lb.Items.Add(Convert.ToString(maxInd[0] + " " + maxInd[1]));
                lb.Items.Add("---------------");
                //

                if (oddsField[maxInd[0], maxInd[1]] == 0)
                {
                    return;
                }
                stepX = maxInd[0] - n / 2;
                stepY = maxInd[1] - n / 2;

                target[0] = fish.PosX + stepX;
                target[1] = fish.PosY + stepY;
                fish.Target = target;
            }
            else
                GetTarget(fish, random);
        }

        private int[] MaxIndex(double[,] mat)
        {
            int[] maxI = new int[2];
            int mX = 0;
            int mY = 0;
            int n = mat.GetLength(0);

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (mat[i, j] > mat[mX, mY])
                    {
                        mX = i;
                        mY = j;
                    }
                }
            }

            maxI[0] = mX;
            maxI[1] = mY;
            return maxI;
        }

        private string CheckType(int x, int y)
        {
            string type = null;

            if (localWorldArr[x, y] != null)
            {
                if (localWorldArr[x, y].PosX == x && localWorldArr[x, y].PosY == y)
                {
                    type = localWorldArr[x, y].Type;
                }
            }/*
            else
            {
                type = "None";
            }*/
        /*
            return type;
        }

        private string[,] FishRadar(Entity fish)
        {
            int sens = 5;
            int x = fish.PosX;
            int y = fish.PosY;
            int xi;
            int yj;
            string[,] field = new string[sens * 2 + 1, sens * 2 + 1];

            for (int i = -sens; i <= sens; i++)
            {
                xi = x + i;

                if (xi >= 0 && xi < cols)
                {
                    for (int j = -sens; j <= sens; j++)
                    {
                        yj = y + j;

                        if (yj >= 0 && yj < rows)
                        {
                            if (xi != x || yj != y)
                                //field[i + sens, j + sens] = fish[xi, yj];
                                field[i + sens, j + sens] = CheckType(xi, yj);
                        }
                    }
                }
            }

            return field;
        }

        private void FishMove(Entity fish)
        {
            if (fish.Target[0] > fish.PosX)
            {
                if (CheckMove(fish.PosX + 1, fish.PosY, fish))
                {
                    fish.PosX = fish.PosX + 1;
                    xMove = true;
                }
            }
            else if (fish.Target[0] < fish.PosX)
            {
                if (CheckMove(fish.PosX - 1, fish.PosY, fish))
                {
                    fish.PosX = fish.PosX - 1;
                    xMove = true;
                }
            }

            if (fish.Target[1] > fish.PosY)
            {
                if (CheckMove(fish.PosX, fish.PosY + 1, fish))
                {
                    fish.PosY = fish.PosY + 1;
                    yMove = true;
                }
            }
            else if (fish.Target[1] < fish.PosY)
            {
                if (CheckMove(fish.PosX, fish.PosY - 1, fish))
                {
                    fish.PosY = fish.PosY - 1;
                    yMove = true;
                }
            }
        }

        private bool CheckMove(int NextPosX, int NextPosY, Entity fish)
        {
            if (NextPosY < rows && NextPosY >= 0 && NextPosX < cols && NextPosX >= 0)
            {
                if (localWorldArr[NextPosX, NextPosY] == null)
                    return true;

                if (fish.Type == "Pred")
                {
                    if (localWorldArr[NextPosX, NextPosY].Type == "Worm" || localWorldArr[NextPosX, NextPosY].Type == "Herb" || localWorldArr[NextPosX, NextPosY].Type == "Die")
                    {
                        fish.Energy += localWorldArr[NextPosX, NextPosY].GiveEnergy;
                        return true;
                    }
                }
                else if (fish.Type == "Herb")
                {
                    if (localWorldArr[NextPosX, NextPosY].Type == "Worm")
                    {
                        fish.Energy += localWorldArr[NextPosX, NextPosY].GiveEnergy;
                        return true;
                    }
                }
            }

            return false;
        }*/
    }

    public static class ArrayExtensions
    {
        public static T[] GetRow<T>(this T[,] data, int i)
        {
            return Enumerable.Range(0, data.GetLength(1)).Select(j => data[i, j]).ToArray();
        }
    }
}