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

namespace CuadroModal
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Ventana_CuadroModal cdm;
        CuadroNoModal cdnm = null;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CDModal_Click(object sender, RoutedEventArgs e)
        {
            if (sender == CDModal)
            {
                cdm = new Ventana_CuadroModal();
                cdm.Owner = this;
                Brush br = ElCanvas.Background.Clone();
                double opacidad = br.Opacity;

                cdm.Opacidad = opacidad;
                cdm.ShowDialog();

                if (cdm.DialogResult == true)
                {
                    br.Opacity = cdm.Opacidad;
                    ElCanvas.Background = br;

                    if (cdnm != null)
                    {
                        cdnm.Opacidad = cdm.Opacidad;
                    }
                }
            }
            else if (sender == CDNoModal)
            {
                if (cdnm != null)
                {
                    return;
                }
                cdnm = new CuadroNoModal();
                cdnm.Owner = this;
                CDNoModal.IsEnabled = false;
                Brush br = ElCanvas.Background;
                cdnm.Opacidad = br.Opacity;
                cdnm.CambioOpacidad += Cdnm_CambioOpacidad;

                cdnm.Show();
            }
        }

        private void Cdnm_Closed(object sender, EventArgs e)
        {
            cdnm = null;
            CDNoModal.IsEnabled = true;
        }

        private void Cdnm_CambioOpacidad(object sender, CambioOpacidadEventArgs e)
        {
            Brush br = ElCanvas.Background.Clone();
            br.Opacity = e.ValorOpacidad;
            ElCanvas.Background = br;
        }


        private void CDNoModal_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
