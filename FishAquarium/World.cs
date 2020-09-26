using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace FishAquarium
{
    class World
    {
        private int cols;
        private int rows;
        private Fish[,] localFishArr;

        public World(int cols, int rows)
        {
            this.cols = cols;
            this.rows = rows;
        }

        public void FishUpdate(ref Fish[,] fishArr, int x, int y)
        {
            Fish [,] newFishArr = fishArr;
            localFishArr = fishArr;

                    if (newFishArr[x, y] != null && newFishArr[x, y].State)
                    {
                        if (newFishArr[x, y].Target[0] == newFishArr[x, y].PosX && newFishArr[x, y].Target[1] == newFishArr[x, y].PosY)
                            GetTarget(newFishArr[x, y]);

                        FishMove(newFishArr[x, y]);

                        fishArr[newFishArr[x, y].PosX, newFishArr[x, y].PosY] = newFishArr[x, y];
                fishArr[x, y] = null;
                    }

        }

        private void FishMove(Fish fish)
        {
            if (fish.Target[0] > fish.PosX && CheckMove(fish.PosX + 1, fish.PosY))
                fish.PosX = fish.PosX + 1;
            else if (fish.Target[0] < fish.PosX && CheckMove(fish.PosX - 1, fish.PosY))
                fish.PosX = fish.PosX - 1;

            if (fish.Target[1] > fish.PosY && CheckMove(fish.PosX, fish.PosY + 1))
            {
                fish.PosY = fish.PosY + 1;
            }
            else if (fish.Target[1] < fish.PosY && CheckMove(fish.PosX, fish.PosY - 1))
            {
                fish.PosY = fish.PosY - 1;
            }
        }

        private bool CheckMove(int NextPosX, int NextPosY)
        {
            if (NextPosY < rows && NextPosY >= 0)
            {
                NextPosX = (NextPosX + cols) % cols;
                if (localFishArr[NextPosX, NextPosY] == null)
                {
                    //listBox1.Items.Add("lol");
                    return true;
                }
            }
            //listBox1.Items.Add("Stolk");
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
