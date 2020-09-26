using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishAquarium
{
    class Fish
    {
        public Brush Color { get; set; }
        public bool State { get; set; } //жива или мертва
        public int Id { get; private set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public int[] Target { get; set; } 
        public int SignOfGoal { get; set; }
       
        public Fish(int id, int posX, int posY, Brush color)
        {
            Id = id;
            PosX = posX;
            PosY = posY;
            State = true;
            Color = color;
            Target = new int[2];
            Target[0] = PosX;
            Target[1] = PosY;
        }

        public Fish()
        {

        }

        public void getRandomPos(Fish fish)
        {
            Random rand = new Random();
            int step = rand.Next(1, 5);

            if (step == 1)
            {//Вверх
                fish.PosY = -1;
            }
            else if (step == 2)
            {//направо
                fish.PosX = +1;
            }
            else if (step == 3)
            {//вниз
                fish.PosY = +1;
            }
            else if (step == 4)
            {//налево
                fish.PosX = -1;
            }
        }
    }
}
