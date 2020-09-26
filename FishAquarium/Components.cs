using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace FishAquarium
{
    class Components
    {
        private Graphics graphics;

        private int currentGen = 0;
        private int resolution;
        private bool[,] field;
        private int rows;
        private int cols;
        private PictureBox fieldPB;
        private NumericUpDown nudResolution;
        private NumericUpDown nudDensity;

        public Components()
        {

        }

        public Components(Graphics graphics, PictureBox fieldPB, NumericUpDown nudResolution, NumericUpDown nudDensity)
        {
            this.graphics = graphics;
            this.fieldPB = fieldPB;
            this.nudResolution = nudResolution;
            this.nudDensity = nudDensity;
        }
    }
}
