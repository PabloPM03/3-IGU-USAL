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
        public event Action<Mesa> MesaSeleccionadaEvento;

        List<Mesa> listaMesas;
        List<Plato> listaPlatosPrimeros;
        List<Plato> listaPlatosSegundos;
        List<Plato> listaPlatosPostres;
        List<Plato> listaPlatosSobremesas;

        Mesa mesaSeleccionada;

        VentanaAnadirPlato ventanaAnadirPlato;
        internal VentanaGestionMesas(List<Mesa> l, List<Plato> lp, List<Plato> ls, List<Plato> lpp, List<Plato> lss, Mesa mesaSeleccionada)
        {
            InitializeComponent();

            cuadroMesas.ItemsSource = l;
            this.listaMesas = l;
            this.listaPlatosPrimeros = lp;
            this.listaPlatosSegundos = ls;
            this.listaPlatosPostres = lpp;
            this.listaPlatosSobremesas = lss;

            if (mesaSeleccionada != null) { SeleccionMesaDesdeMapa(mesaSeleccionada); }
        }

        public void cuadroMesas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cuadroMesas.SelectedItem != null)
            {
                Mesa mesaSeleccionada = (Mesa)cuadroMesas.SelectedItem;
                this.mesaSeleccionada = mesaSeleccionada;
                cuadroComandas.ItemsSource = mesaSeleccionada.comanda;

                MesaSeleccionadaEvento?.Invoke(mesaSeleccionada);

            }
        }

        public void SeleccionMesaDesdeMapa(Mesa mesaSeleccionada)
        {
            cuadroMesas.SelectedItem = mesaSeleccionada;
            cuadroMesas.ScrollIntoView(mesaSeleccionada);
            this.mesaSeleccionada = mesaSeleccionada;
        }

        public void cuadroComandas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnEliminarPlato_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAnadirPlato_Click(object sender, RoutedEventArgs e)
        {
            if (ventanaAnadirPlato == null || !ventanaAnadirPlato.IsLoaded)
            {

                ventanaAnadirPlato = new VentanaAnadirPlato(this.mesaSeleccionada, this.listaPlatosPrimeros, listaPlatosSegundos, listaPlatosPostres, listaPlatosSobremesas);

                ventanaAnadirPlato.Show();

            }
            else
            {
                ventanaAnadirPlato.Activate();
            }

        }
    }
}
