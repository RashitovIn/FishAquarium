using System;
using System.Windows.Forms;

namespace FishAquarium
{
    class World
    {
        private int cols;
        private int rows;
        private Entity[,] localWorldArr;
        private bool xMove;
        private bool yMove;
        private Random random;

        public bool Checked { get; set; } = false; //Для цикла, если true значит уже сделал ход

        public delegate void MethodContainer();
        public event MethodContainer onCount;

        public World()
        {
            random = new Random();
        }

        public World(int cols, int rows)
        {
            this.cols = cols;
            this.rows = rows;
            random = new Random();
        }

        public void FishCheckEvents(Entity fish)
        {
            if (fish.Energy == 0)
            {

            }
        }

        public void UpdateWorld(ref Entity[,] worldArr, int x, int y, ref ListBox lb)
        {
            localWorldArr = worldArr;

            if (!worldArr[x, y].Checked)
            {
                xMove = false;
                yMove = false;

                string[,] field;

                if (worldArr[x, y].Type == "Worm")
                {
                    if (CheckMove(x, y + 1, worldArr[x, y]))
                    {
                        worldArr[x, y].Checked = true;
                        worldArr[x, y].PosY = worldArr[x, y].PosY + 1;
                        worldArr[x, y + 1] = worldArr[x, y];
                        worldArr[x, y] = null;
                    }
                    return;
                }
                else if (worldArr[x, y].Type == "Die")
                {
                    if (CheckMove(x, y - 1, worldArr[x, y]))
                    {
                        worldArr[x, y].Checked = true;
                        worldArr[x, y].PosY = worldArr[x, y].PosY - 1;
                        worldArr[x, y - 1] = worldArr[x, y];
                        worldArr[x, y] = null;
                    }
                    return;
                }

                worldArr[x, y].Checked = true;
                field = FishRadar(worldArr[x, y]);

                if (worldArr[x, y].TargetLimiter >= 2)
                {
                    worldArr[x, y].Target[0] = worldArr[x, y].PosX;
                    worldArr[x, y].Target[1] = worldArr[x, y].PosY;
                    worldArr[x, y].TargetLimiter = 0;
                }

                //if (fishArr[x, y].Target[0] == fishArr[x, y].PosX && fishArr[x, y].Target[1] == fishArr[x, y].PosY)
                GetDefTarget(worldArr[x, y], field, lb);
                //GetTarget(fishArr[x, y]);
                FishMove(worldArr[x, y]);

                worldArr[x, y].Energy -= 0.5;

                if (worldArr[x, y].Energy == 0)
                    onCount += worldArr[x, y].FishDie;

                if (xMove || yMove)
                {
                    worldArr[worldArr[x, y].PosX, worldArr[x, y].PosY] = worldArr[x, y];
                    worldArr[x, y] = null;
                }
                else
                    worldArr[x, y].TargetLimiter++;
            }
            onCount?.Invoke();
        }

        private void GetDefTarget(Entity fish, string[,] field, ListBox lb)
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

        private void MotionVector(string[,] field, int[] maxind, string type)
        {
            string targetType = field[maxind[0], maxind[1]];
            if (type == "Pred")
            {

            }
            else if (type == "Herb")
            {
                if (targetType == "Pred")
                {

                }
            }
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
        }

        private void GetTarget(Entity fish, Random random)
        {
            if (fish.Target[0] == fish.PosX && fish.Target[1] == fish.PosY)
            {
                int stepX = random.Next(10);
                int stepY = random.Next(10);
                int sideX = random.Next(-1, 2);
                int sideY = random.Next(-1, 2);
                int[] target = new int[2];

                target[0] = (fish.PosX + sideX * stepX + cols) % cols;
                target[1] = (fish.PosY + sideY * stepY + rows) % rows;
                fish.Target = target;
            }
        }
    }
}
