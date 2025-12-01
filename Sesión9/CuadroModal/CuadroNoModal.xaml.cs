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
using System.Windows.Shapes;

namespace CuadroModal
{
    /// <summary>
    /// Lógica de interacción para CuadroNoModal.xaml
    /// </summary>
    /// 
    public class CambioOpacidadEventArgs : EventArgs
    {
        public double ValorOpacidad {  get; set; }
    }
    public partial class CuadroNoModal : Window
    {
        public event EventHandler<CambioOpacidadEventArgs> CambioOpacidad;
        double opacidad = 0;
        public CuadroNoModal()
        {
            InitializeComponent();
        }

        protected virtual void OnCambioOpacidad(CambioOpacidadEventArgs e)
        {
            CambioOpacidad?.Invoke(this, e);
        }

        public double Opacidad
        {
            get { return opacidad; }
            set
            {
                opacidad = value;
                SliderOpacidad.Value = opacidad;
            }
        }

        private void SliderOpacidad_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            opacidad = SliderOpacidad.Value;
            if (Leyenda != null)
            {
                Leyenda.Text = opacidad.ToString("F");
            }
            CambioOpacidadEventArgs args = new CambioOpacidadEventArgs();
            args.ValorOpacidad = opacidad;
            OnCambioOpacidad(args);
        }

        private void Cerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
