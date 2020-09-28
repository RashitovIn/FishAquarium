using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace FishAquarium
{
    class World
    {
        private int cols;
        private int rows;
        private Fish[,] localFishArr;
        private Fish[,] localNewFishArr;

        public World()
        {

        }

        public World(int cols, int rows)
        {
            this.cols = cols;
            this.rows = rows;
        }

        public void FishUpdate(Fish[,] fishArr, ref Fish[,] newFishArr, int x, int y)
        {
            localFishArr = fishArr;
            localNewFishArr = newFishArr;

            if (fishArr[x, y].Target[0] == fishArr[x, y].PosX && fishArr[x, y].Target[1] == fishArr[x, y].PosY)
                GetTarget(fishArr[x, y]);

            FishMove(fishArr[x, y]);
            newFishArr[fishArr[x, y].PosX, fishArr[x, y].PosY] = fishArr[x, y];
        }

        private void FishMove(Fish fish)
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
            if (NextPosY < rows && NextPosY >= 0)
            {
                NextPosX = (NextPosX + cols) % cols;
                if (localFishArr[NextPosX, NextPosY] == null && localNewFishArr[NextPosX, NextPosY] == null)
                {
                    return true;
                }
            }
            return false;
        }

        private void GetTarget(Fish fish)
        {
            Random random = new Random();
            int stepX = random.Next(10);
            int stepY = random.Next(20);
            int sideX = random.Next(-1, 2);
            int sideY = random.Next(-1, 2);
            int[] target = new int[2];

            target[0] = (fish.PosX + sideX * stepX + cols) % cols;
            target[1] = (fish.PosY + sideY * stepY + rows) % rows;
            fish.Target = target;
        }
    }
}
