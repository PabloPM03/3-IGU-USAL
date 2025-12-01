using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Animaciones
{
    internal class Figura
    {
        private double X, Y;
        private double despX, despY;
        private Shape fig;

        public Figura(double LimX, double LimY)
        {
            Random rand = new Random();

            int tipo = rand.Next(2);
            Debug.WriteLine("tipo: " + tipo.ToString());
            if (tipo > 0)
            {
                fig = new Ellipse();
            }
            else
            {
                fig = new Rectangle();
            }


            double min = rand.Next((int)LimX / 5);
            fig.Width = (min < 20 ? 20 : min);
            min = rand.Next((int)LimX / 5);
            fig.Height = (min < 20 ? 20 : min);

            X = rand.Next((int)(LimX - fig.Width));
            Y = rand.Next((int)(LimY - fig.Height));


            fig.Stroke = Brushes.Black;
            fig.StrokeThickness = 1F;
            Color clr = Color.FromRgb((byte)rand.Next(256), (byte)rand.Next(256), (byte)rand.Next(256));
            fig.Fill = new SolidColorBrush(clr);

            min = rand.Next(5);
            despX = (min < 1 ? 1 : min);
            despY = (min < 1 ? 1 : min);
        }

        public void mueve(double limX, double limY)
        {
            X += despX;
            Y += despY;

            if ((X + fig.Width + despX) > limX || X < 0)
            {
                despX = -despX;
            }
            if ((Y + fig.Height + despY) > limY || Y < 0)
            {
                despY = -despY;
            }
        }

        public double posX
        {
            get
            {
                return X;
            }
        }
        public double posY
        {
            get
            {
                return Y;
            }
        }
        public Shape laFig
        {
            get
            {
                return fig;
            }

        }

        public override string ToString()
        {
            string res = "Figura: " + this.laFig.GetType() + this.posX + ", " + this.posY;
            return res;
        }

    }
}
