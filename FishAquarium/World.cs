using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace FishAquarium
{
    class World
    {
        private int cols;
        private int rows;
        private World[,] localFishArr;
        private World[,] localNewFishArr;
        public Brush Color { get; set; }
        public bool State { get; set; } //жива или мертва
        public int PosX { get; set; }
        public int PosY { get; set; }
        public int[] Target { get; set; }
        public int GiveEnergy { get; set; }
        public string Type { get; set; }

        public World()
        {

        }

        public World(int cols, int rows)
        {
            this.cols = cols;
            this.rows = rows;
        }

        public void FishUpdate(World[,] fishArr, ref World[,] newFishArr, int x, int y, ref ListBox lb)
        {
            localFishArr = fishArr;
            localNewFishArr = newFishArr;

            string[,] field;

            if (fishArr[x, y].Type == "Worm")
            {
                newFishArr[fishArr[x, y].PosX, fishArr[x, y].PosY] = fishArr[x, y];
                return;
            }

            field = FishRadar(fishArr[x, y]);
            if (fishArr[x, y].Target[0] == fishArr[x, y].PosX && fishArr[x, y].Target[1] == fishArr[x, y].PosY)
                GetDefTarget(fishArr[x, y], field, lb);
            //GetTarget(fishArr[x, y]);

            //GetDefTarget(fishArr[x, y], field);

            FishMove(fishArr[x, y]);
            newFishArr[fishArr[x, y].PosX, fishArr[x, y].PosY] = fishArr[x, y];
        }

        private void GetDefTarget(World fish, string[,] field, ListBox lb)
        {
            Dictionary<string, double> weights = new Dictionary<string, double>
            {
                ["Pred"] = -0.5,
                ["Worm"] = 0.7,
                ["Herb"] = -0.1
            };
            double[] layerOdds = { 1, 0.7, 0.5, 0.4, 0.3, 0.2, 0.1 }; // Коэффициенты слоёв

            int n = field.GetLength(0);
            double[,] oddsField = new double[n, n];
            bool isZero = true;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (field[i, j] != null)
                    {
                        oddsField[i, j] = weights[field[i, j]];
                        isZero = false;
                    }
                    else
                        oddsField[i, j] = 0;
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
                        oddsField[col, j] = oddsField[col, j] * layerOdds[layersCount];
                    }

                    col = n - count;
                    for (int j = count; j <= n - count; j++)
                    {
                        oddsField[col, j] = oddsField[col, j] * layerOdds[layersCount];
                    }

                    for (int j = count + 1; j <= n - count - 1; j++)
                    {
                        oddsField[count, j] = oddsField[count, j] * layerOdds[layersCount];
                    }

                    for (int j = count + 1; j <= n - count - 1; j++)
                    {
                        oddsField[n - count, j] = oddsField[n - count, j] * layerOdds[layersCount];
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
                stepX = maxInd[0] - n / 2;
                stepY = maxInd[1] - n / 2;

                target[0] = fish.PosX + stepX;
                target[1] = fish.PosY + stepY;
                fish.Target = target;
            }
            else
                GetTarget(fish);
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

            /*if (((x < 0 && y < 1) || (x > 0 && y < 0)) && localNewFishArr[x, y] != null)
            {
                type = localNewFishArr[x, y].Type;
                    //MessageBox.Show("new " + Convert.ToString(x + " " + y));
            }
            else if (((x < 1 && y > 0) || (x > 0 && y > -1)) && localFishArr[x, y] != null)
            {
                type = localFishArr[x, y].Type;
                    //MessageBox.Show("old " + Convert.ToString(x + " " + y));
            }*/

            if (localNewFishArr[x, y] != null)
            {
                type = localNewFishArr[x, y].Type;
            }
            else if (localFishArr[x, y] != null && localFishArr[x, y].PosX == x && localFishArr[x, y].PosY == y)
            {
                type = localFishArr[x, y].Type;
            }

            return type;
        }

        private string[,] FishRadar(World fish)
        {
            int sens = 2;
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
                                field[i + sens, j + sens] = CheckType(xi, yj);
                        }
                    }
                }
            }

            return field;
        }

        private void GoToTarget(World fish)
        {
            int xWay = fish.Target[0] - fish.PosX;
            int yWay = fish.Target[1] - fish.PosY;


        }

        private void FishMove(World fish)
        {
            if (fish.Target[0] > fish.PosX)
            {
                if (CheckMove(fish.PosX + 1, fish.PosY))
                    fish.PosX = fish.PosX + 1;
            }
            else if (fish.Target[0] < fish.PosX)
            {
                if (CheckMove(fish.PosX - 1, fish.PosY))
                    fish.PosX = fish.PosX - 1;
            }

            if (fish.Target[1] > fish.PosY)
            {
                if (CheckMove(fish.PosX, fish.PosY + 1))
                    fish.PosY = fish.PosY + 1;
            }
            else if (fish.Target[1] < fish.PosY)
            {
                if (CheckMove(fish.PosX, fish.PosY - 1))
                    fish.PosY = fish.PosY - 1;
            }
        }

        private bool CheckMove(int NextPosX, int NextPosY)
        {
            if (NextPosY < rows && NextPosY >= 0 && NextPosX < cols && NextPosX >= 0)
            {
                if (localFishArr[NextPosX, NextPosY] == null && localNewFishArr[NextPosX, NextPosY] == null)
                {
                    return true;
                }
            }
            return false;
        }

        private void GetTarget(World fish)
        {
            Random random = new Random();
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
