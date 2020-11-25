using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace FishAquarium
{
    abstract class Entity
    {
        public Rectangle Body;
        public Brush Color { get; set; }
        public bool State { get; set; } //жива или мертва
        public int PosX { get; set; }
        public int PosY { get; set; }
        public double[] Target { get; set; }
        public double TargetLimiter { get; set; }
        public int SignOfGoal { get; set; }
        public double Energy { get; set; }
        public int Speed { get; set; }
        public double GiveEnergy { get; set; }
        public string Type { get; set; }
        public int Dx { get; set; }
        public int Dy { get; set; }

        Random rand;

        public Dictionary<string, double> Weights { get; set; }
        public double[] LayerOdds { get; set; } // Коэффициенты слоёв

        public Entity(int posX, int posY)
        {
            rand = new Random(posX + posY);
            PosX = posX;
            PosY = posY;
            State = true;
            //Color = color;
            Target = new double[2];
            Target[0] = PosX;
            Target[1] = PosY;
            Body = new Rectangle(posX, posY, 20, 10);
        }

        public void FishDie()
        {
            Color = Brushes.Gray;
            GiveEnergy = 3;
            Type = "Die";
        }

        public void FishMove()
        {
            Speed = rand.Next(5, 10);
            Dx = rand.Next(-1, 2);
            Dy = rand.Next(-1, 2);
            int xMove = PosX + Speed * Dx;
            int yMove = PosY + Speed * Dy;

            if (xMove >)
            PosX += Speed * Dx;
            PosY += Speed * Dy;
            Body.X = PosX;
            Body.Y = PosY;
        }
    }

    class PredFish : Entity
    {        
        public PredFish(int posX, int posY) :base(posX, posY)
        {
            Type = "Pred";
            Energy = 50;
            Color = Brushes.Red;
            /*LayerOdds = new double[] { 1, 0.6, 0.2, 0.1, 0.08, 0.2, 0.1, 0.05, 0.03, 0.005, 0.0002 };
            Weights = new Dictionary<string, double>
            {
                ["Pred"] = 0,
                ["Worm"] = 0.9,
                ["Herb"] = 0.7,
                ["Die"] = 0.5,
                ["None"] = 0,
            };*/
        }
    }

    class HerbFish : Entity
    {
        public HerbFish(int posX, int posY) : base(posX, posY)
        {
            Type = "Herb";
            Energy = 10;
            Color = Brushes.Green;
            GiveEnergy = 5;
            LayerOdds = new double[] { 1, 0.5, 0.2, 0.1, 0.08, 0.2, 0.1, 0.05, 0.03, 0.005, 0.0002 };
            Weights = new Dictionary<string, double>
            {
                ["Pred"] = 0.9,
                ["Worm"] = 0.6,
                ["Herb"] = 0,
                ["Die"] = 0,
                ["None"] = 0,
            };
        }
    }

    class Worm : Entity
    {
        public Worm(int posX, int posY) : base(posX, posY)
        {
            PosX = posX;
            PosY = posY;
            State = true;
            Color = Brushes.Yellow;
            GiveEnergy = 10;
            Type = "Worm";
            Energy = 0;
        }
    }
    class Weed : Entity
    {
        public Weed(int posX, int posY) : base(posX, posY)
        {
            PosX = posX;
            PosY = posY;
            State = true;
            Color = Brushes.LimeGreen;
            GiveEnergy = 10;
            Type = "Weed";
            Energy = 0;
        }

        /*public void CreateWeedRow(ref Fish fishArr, )
        {

        }*/
    }

}
