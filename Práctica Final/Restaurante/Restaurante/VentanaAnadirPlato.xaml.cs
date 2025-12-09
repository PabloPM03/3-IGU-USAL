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

namespace Restaurante
{
    public partial class VentanaAnadirPlato : Window
    {

        public VentanaAnadirPlato()
        {
            InitializeComponent();

            this.DataContext = DatosRestaurante.datos;

            btnAnadirPlato.IsEnabled = false;
            categoriaComboBox.SelectedIndex = 0;
        }

        private void cuadroCartaCategoría_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cuadroCartaCategoría.SelectedItems != null)
            {
                btnAnadirPlato.IsEnabled = true;
            }
            else
            {
                btnAnadirPlato.IsEnabled = false;
            }

        }

        private void categoriaComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var datos = DatosRestaurante.datos;

            switch (categoriaComboBox.SelectedIndex)
            {
                case 0: cuadroCartaCategoría.ItemsSource = datos.listaPlatosPrimeros; break;
                case 1: cuadroCartaCategoría.ItemsSource = datos.listaPlatosSegundos; break;
                case 2: cuadroCartaCategoría.ItemsSource = datos.listaPlatosPostres; break;
                case 3: cuadroCartaCategoría.ItemsSource = datos.listaPlatosSobremesas; break;
            }
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAnadirPlato_Click(object sender, RoutedEventArgs e)
        {
            Plato platoSeleccionado = (Plato)cuadroCartaCategoría.SelectedItem;
            Mesa mesaSeleccionada = DatosRestaurante.datos.mesaSeleccionada;

            mesaSeleccionada.anadirPlato(platoSeleccionado);
        }
    }
}
