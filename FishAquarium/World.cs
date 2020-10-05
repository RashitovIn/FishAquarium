using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
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

            if (fishArr[x, y].Type == "Worm")
            {
                newFishArr[fishArr[x, y].PosX, fishArr[x, y].PosY] = fishArr[x, y];
                return;
            }

            //if (fishArr[x, y].Target[0] == fishArr[x, y].PosX && fishArr[x, y].Target[1] == fishArr[x, y].PosY)
            //    GetTarget(fishArr[x, y], ref lb);

            FishRadar(fishArr[x, y]);

            FishMove(fishArr[x, y]);
            newFishArr[fishArr[x, y].PosX, fishArr[x, y].PosY] = fishArr[x, y];
        }

        private string CheckType(int x, int y)
        {
            string type = null;

            type = "predFish";

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
                            if (i != x && j != y)
                                field[i, j] = CheckType(i, j);
                        }
                    }
                }
            }

            return field;
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

        private void GetTarget(World fish, ref ListBox lb)
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
