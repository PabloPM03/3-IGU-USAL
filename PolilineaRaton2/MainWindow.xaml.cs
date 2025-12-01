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

namespace PolilineaRaton2
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool previsualizado;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (previsualizado)
            {
                if (linea.Points.Count > 0)
                {
                    preview.X1 = linea.Points[linea.Points.Count - 1].X;
                    preview.Y1 = linea.Points[linea.Points.Count - 1].Y;

                    preview.X2 = e.GetPosition(canvas).X;
                    preview.Y2 = e.GetPosition(canvas).Y;
                }
            }
            else
            {
                preview.X1 = preview.Y1 = preview.X2 = preview.Y2 = 0;
            }
        }

        private void canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            linea.Points.Add(e.GetPosition(canvas));
            previsualizado = true;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                previsualizado = false;
            }
        }
    }
}
