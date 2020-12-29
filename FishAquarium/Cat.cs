using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace FishAquarium
{
    class Cat
    {
        public Bitmap catBitmap;
        public Rectangle catRect;
        Entity StealedFish;
        bool stealing;
        Random random;

        public Cat(Bitmap catBitmap, Random random)
        {
            this.catBitmap = catBitmap;
            stealing = false;
            catRect = new Rectangle(0, 0, 0, 0);
            this.random = random;
        }
        public void Stealing(Entity fish)
        {
            if (random.Next(30) == 0)
            {
                if (!stealing)
                {
                    stealing = true;
                    StealedFish = fish;
                    Rectangle rect = new Rectangle(fish.PosX, fish.Body.Bottom - 120, 60, 120);
                    catRect = rect;
                }
                else if (stealing)
                {
                    StealingProc();
                }
            }
        }

        public void StealingProc()
        {
            if (stealing)
            {
                catRect.Y += -10;
                StealedFish.StealFish(catRect.Y);

                if (catRect.Bottom <= 0)
                {
                    stealing = false;
                    StealedFish.Destroy();
                }
            }
        }
    }
}
