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

namespace Restaurante
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double escalaMapaActual = 1;
        public MainWindow()
        {
            InitializeComponent();
            configInicial();
        }

        public void configInicial()
        {
            int tamanoBoton = 40;

            //columnaBotones.Width() = tamanoBoton;
        }

        private void btnVerRestaurante_Click(object sender, RoutedEventArgs e)
        {

        }

        private void canvasAnadirMesa_Click(object sender, RoutedEventArgs e)
        {
            if (extendedMenu.Visibility == Visibility.Visible)
            {
                btnAnadirMesa.Background = Brushes.LightGray;
                extendedMenu.Visibility = Visibility.Hidden;
            }
            else
            {
                btnAnadirMesa.Background = Brushes.Gray;
                extendedMenu.Visibility = Visibility.Visible;
            }

        }

        private void btnVerCarta_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCuenta_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAjustes_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnZoom_Click(object sender, RoutedEventArgs e)
        {
            if (sender == btnZoomMas)
            {
                if (escalaMapaActual < 3)
                {
                    escalaMapaActual += 0.25;
                }
            }
            else
            {
                if (escalaMapaActual > 1)
                {
                    escalaMapaActual -= 0.25;
                }

            }

            escalaMapa.ScaleX = escalaMapaActual;
            escalaMapa.ScaleY = escalaMapaActual;
        }
    }
}
