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
    /// Lógica de interacción para CuadroModal.xaml
    /// </summary>
    public partial class Ventana_CuadroModal : Window
    {
        double opacidad = 0.0;
        public Ventana_CuadroModal()
        {
            InitializeComponent();
        }

        public double Opacidad
        {
            get { return opacidad; }
            set 
            { 
                //CDModal.SliderOpacidad PROHIBIDO
                //SliderOpacidad es una variable pública
                opacidad = value;
                SliderOpacidad.Value = value;
            }
        }

        private void SliderOpacidad_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            opacidad = SliderOpacidad.Value;
            if (Leyenda != null)
            {
                Leyenda.Text = opacidad.ToString("F");
            }
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
        private void Aceptar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
