using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Parábola
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            dibujar();
        }

        public void dibujar()
        {
            //Dominio Matemático
            double xMin = -10, xMax = 10;
            double yMin = 0, yMax = 100;
            double x, y;

            //Dominio Interfaz
            double xPant, yPant;
            double xPantMin = 0, xPantMax = canvas.ActualWidth;
            double yPantMin = canvas.ActualHeight, yPantMax = 0;

            //line.Points.Clear();

            double numPuntos = 100;

            for (int i = 0; i < numPuntos; i++)
            {
                x = xMin + (xMax - xMin) * i / numPuntos;
                y = x * x;

                xPant = xPantMin + (x - xMin) * (xPantMax - xPantMin) / (xMax - xMin);
                yPant = yPantMin + (y - yMin) * (yPantMax - yPantMin) / (yMax - yMin);

                line.Points.Add(new Point(xPant, yPant));

                //Console.WriteLine($"x_{i}={x}");

            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            dibujar();
        }
    }
}
