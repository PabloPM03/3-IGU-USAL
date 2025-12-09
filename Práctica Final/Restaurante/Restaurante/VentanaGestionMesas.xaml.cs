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
    public partial class VentanaGestionMesas : Window
    {
        VentanaAnadirPlato ventanaAnadirPlato;
        internal VentanaGestionMesas()
        {
            InitializeComponent();

            this.DataContext = DatosRestaurante.datos;
            btnAnadirPlato.IsEnabled = false;
            btnEliminarPlato.IsEnabled = false;

            DatosRestaurante.datos.cambioSeleccionMesa += OnCambioSeleccionMesa;
        }

        private void OnCambioSeleccionMesa(Mesa mesaSeleccionada)
        {
            if (mesaSeleccionada != null)
            {
                cuadroMesas.SelectedItem = mesaSeleccionada;
                cuadroMesas.ScrollIntoView(mesaSeleccionada);
            }
        }

        public void cuadroMesas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cuadroMesas.SelectedItem != null)
            {
                Mesa mesaSeleccionada = (Mesa)cuadroMesas.SelectedItem;
                DatosRestaurante.datos.mesaSeleccionada = mesaSeleccionada;

                cuadroComandas.ItemsSource = mesaSeleccionada.comanda;

                btnAnadirPlato.IsEnabled = true;
                btnEliminarPlato.IsEnabled = true;
            }
        }

        private void btnEliminarPlato_Click(object sender, RoutedEventArgs e)
        {
            DatosRestaurante.datos.mesaSeleccionada.comanda.Remove((Plato)cuadroComandas.SelectedItem);
        }

        private void btnAnadirPlato_Click(object sender, RoutedEventArgs e)
        {
            ventanaAnadirPlato = new VentanaAnadirPlato();
            ventanaAnadirPlato.ShowDialog();
        }
    }
}
