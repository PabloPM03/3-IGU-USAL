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
        List<Plato> listaPlatosPrimeros;
        List<Plato> listaPlatosSegundos;
        List<Plato> listaPlatosPostres;
        List<Plato> listaPlatosSobremesas;

        Mesa mesaSeleccionada;
        Plato platoSeleccionado;

        internal VentanaAnadirPlato(Mesa mesaSeleccionada, List<Plato> listaPlatosPrimeros, List<Plato> listaPlatosSegundos, List<Plato> listaPlatosPostres, List<Plato> listaPlatosSobremesas)
        {
            InitializeComponent();
            this.listaPlatosPrimeros = listaPlatosPrimeros;
            this.listaPlatosSegundos = listaPlatosSegundos;
            this.listaPlatosPostres = listaPlatosPostres;
            this.listaPlatosSobremesas = listaPlatosSobremesas;

            this.mesaSeleccionada = mesaSeleccionada;

            btnAnadirPlato.IsEnabled = false;
            categoriaComboBox.SelectedIndex = 0;
        }

        private void cuadroCartaCategoría_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cuadroCartaCategoría.SelectedItem != null)
            {
                this.platoSeleccionado = (Plato) cuadroCartaCategoría.SelectedItem;
                btnAnadirPlato.IsEnabled = true;
            }
            else
            {
                btnAnadirPlato.IsEnabled = false;
            }
        }

        private void categoriaComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (categoriaComboBox.SelectedIndex)
            {
                case 0: cuadroCartaCategoría.ItemsSource = listaPlatosPrimeros; break;
                case 1: cuadroCartaCategoría.ItemsSource = listaPlatosSegundos; break;
                case 2: cuadroCartaCategoría.ItemsSource = listaPlatosPostres; break;
                case 3: cuadroCartaCategoría.ItemsSource = listaPlatosSobremesas; break;
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {

            this.Close();
        }

        private void btnAnadirPlato_Click(object sender, RoutedEventArgs e)
        {
            mesaSeleccionada.anadirPlato(platoSeleccionado);
            this.Close();
        }
    }
}
