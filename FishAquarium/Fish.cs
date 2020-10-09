using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FishAquarium
{
    abstract class Fish : World
    {
        public int SignOfGoal { get; set; }
        public int Energy { get; set; }
        public int Speed { get; set; }
       
        public Fish(int posX, int posY, Brush color)
        {
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
        public PredFish(int posX, int posY, Brush color) :base(posX, posY, color)
        {
            Type = "Pred";
        }
    }

    class HerbFish : Fish
    {
        public HerbFish(int posX, int posY, Brush color) : base(posX, posY, color)
        {
            Type = "Herb";
        }
    }

    class Worm : World
    {
        public Worm(int posX, int posY, Brush color)
        {
            PosX = posX;
            PosY = posY;
            State = true;
            Color = color;
            GiveEnergy = 1;
            Type = "Worm";
        }
    }
}
