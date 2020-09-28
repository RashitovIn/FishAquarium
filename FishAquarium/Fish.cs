using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishAquarium
{
    class Fish : World
    {
        public Brush Color { get; set; }
        public bool State { get; set; } //жива или мертва
        public int Id { get; private set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public int[] Target { get; set; } 
        public int SignOfGoal { get; set; }
        public int Energy { get; set; }
        public int Speed { get; set; }
       
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
            Energy = 1;
        }
    }

    class PredFish : Fish
    {
        private string type = "Pred";
        public string Type { get; }

        public PredFish(int id, int posX, int posY, Brush color) :base(id, posX, posY, color)
        {

        }
    }

    class HerbFish : Fish
    {
        private string type = "Herb";
        public string Type { get; }

        public HerbFish(int id, int posX, int posY, Brush color) : base(id, posX, posY, color)
        {

        }
    }

    class Worm : World
    {
        
    }
}
