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
using System.Windows.Threading;

namespace Animaciones
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Figura> listaFiguras;
        DispatcherTimer temporizador;
        public MainWindow()
        {
            InitializeComponent();
            listaFiguras = new List<Figura>();
            temporizador = new DispatcherTimer();
            temporizador.Tick += Temporarizador_Tick;
            temporizador.Interval = new TimeSpan(0, 0, 0, 0, 40);
        }

        private void Temporarizador_Tick(object sender, EventArgs e)
        {
            foreach (Figura f in listaFiguras)
            {
                f.mueve(elCanvas.ActualWidth, elCanvas.ActualHeight);
                Canvas.SetLeft(f.laFig, f.posX);
                Canvas.SetTop(f.laFig, f.posY);
            }
        }
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            Figura f;

            f = new Figura(elCanvas.ActualWidth, elCanvas.ActualHeight);
            listaFiguras.Add(f);

            elCanvas.Children.Add(f.laFig);
            Canvas.SetLeft(f.laFig, f.posX);
            Canvas.SetTop(f.laFig, f.posY);

            f.laFig.MouseDown += LaFig_MouseDown;

            f.laFig.Tag = f;

            if (btnAnimar.IsEnabled == false)
            {
                btnAnimar.IsEnabled = true;
            }
        }

        private void btnAnimar_Click(object sender, RoutedEventArgs e)
        {
            if ((string)btnAnimar.Content == "Animar")
            {
                btnAnimar.Content = "Parar";
                temporizador.Start();
            }
            else if ((string)btnAnimar.Content == "Parar")
            {
                btnAnimar.Content = "Animar";
                temporizador.Stop();
            }
        }

        private void LaFig_MouseDown(object sender, EventArgs e)
        {
            Shape elShape = sender as Shape;
            Figura laFigura = elShape.Tag as Figura;

            listaFiguras.Remove(laFigura);
            elCanvas.Children.Remove(elShape);

            if (listaFiguras.Count <= 0)
            {
                if((string)btnAnimar.Content == "Parar")
                {
                    btnAnimar.Content = "Animar";
                    temporizador.Stop();
                }
                btnAnimar.IsEnabled = false;
            }
        }

    }
}
