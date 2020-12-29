using System;
using System.Drawing;
using System.Windows.Forms;

namespace FishAquarium
{
    class Cleaner
    {
        public Bitmap cleanerBitmap;
        public Rectangle cleanerRect;
        Entity CleanedFish;
        bool cleaning;
        Random random;

        public Cleaner(Bitmap cleanerBitmap, Random random)
        {
            this.cleanerBitmap = cleanerBitmap;
            cleaning = false;
            cleanerRect = new Rectangle(0, 0, 0, 0);
           
            this.random = random;
        }

        public void FishDied(Entity fish)
        {
            if(random.Next(100) == 0)
            {
                if (!cleaning)
                {
                    cleaning = true;
                    CleanedFish = fish;
                    Rectangle rect = new Rectangle(fish.PosX, fish.Body.Bottom - 120, 150, 200);
                    cleanerRect = rect;
                }
                else if (cleaning)
                {
                    CleaningProc();
                }
            }
        }

        public void CleaningProc()
        {
            if (cleaning)
            {
                cleanerRect.Y += -10;
                CleanedFish.StealFish(cleanerRect.Y);

                if (cleanerRect.Bottom <= 0)
                {
                    cleaning = false;
                    CleanedFish.Destroy();
                }
            }
        }

    }
}
