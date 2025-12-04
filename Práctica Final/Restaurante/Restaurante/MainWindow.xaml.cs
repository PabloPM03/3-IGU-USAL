using System;
using System.Collections;
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
    public partial class MainWindow : Window
    {
        public VentanaGestionMesas ventanaGestionMesas;

        List<Mesa> listaMesas = new List<Mesa>();
        List<Plato> listaPlatosPrimeros = new List<Plato>();
        List<Plato> listaPlatosSegundos = new List<Plato>();
        List<Plato> listaPlatosPostres = new List<Plato>();
        List<Plato> listaPlatosSobremesas = new List<Plato>();
        private double zoomActual = 1;
        Mesa mesaSeleccionada;

        public MainWindow()
        {
            InitializeComponent();
            ajustarMapa();
            mesasPrueba();
            cartaPrueba();
            comandasPrueba();
        }
        

        public void mesaSeleccionada_Click(Mesa mesaSeleccionada)
        {
            //RESALTAR MESA
            foreach (Mesa m in listaMesas) 
            {
                m.figuraMesa.Stroke = Brushes.Black; m.figuraMesa.StrokeThickness = 2;
            }


            if (mesaSeleccionada.gridMesaRepresentada.Children.Count > 0 && mesaSeleccionada.gridMesaRepresentada.Children[0] is Shape figuraMesa)
            {
                figuraMesa.Stroke = Brushes.Blue;
                figuraMesa.StrokeThickness = 5;
            }


            //Click en el Mapa

            if (ventanaGestionMesas != null && ventanaGestionMesas.IsLoaded)
            {
                ventanaGestionMesas.SeleccionMesaDesdeMapa(mesaSeleccionada);
            }
            this.mesaSeleccionada = mesaSeleccionada;
        }
        public void seleccionarMesa(Mesa mesaSeleccionada)
        {


        }




            
            //---------- FUNCIONES DE COLUMNA DE BOTONES ----------//

        private void btnVerDatosMesa_Click(object sender, RoutedEventArgs e)
        {
            if (extendedMenu.Visibility == Visibility.Visible)
            {
                btnAnadirMesas.Background = Brushes.LightGray;
                extendedMenu.Visibility = Visibility.Hidden;
            }
            else
            {
                btnAnadirMesas.Background = Brushes.Gray;
                extendedMenu.Visibility = Visibility.Visible;
            }
        }

        private void btnGestionMesas_Click(object sender, RoutedEventArgs e)
        {
            if (ventanaGestionMesas == null || !ventanaGestionMesas.IsLoaded)
            {

                ventanaGestionMesas = new VentanaGestionMesas(this.listaMesas, this.listaPlatosPrimeros, this.listaPlatosSegundos, this.listaPlatosPostres, this.listaPlatosSobremesas, this.mesaSeleccionada);

                ventanaGestionMesas.MesaSeleccionadaEvento += mesaSeleccionada_Click;

                ventanaGestionMesas.Show();

            }
            else
            {
                ventanaGestionMesas.Activate();
            }

        }
        private void btnAnadirMesas_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnVerCarta_Click(object sender, RoutedEventArgs e)
        {
            if (extendedMenu.Visibility == Visibility.Visible)
            {
                btnVerCarta.Background = Brushes.LightGray;
                extendedMenu.Visibility = Visibility.Hidden;
            }
            else
            {
                btnVerCarta.Background = Brushes.Gray;
                extendedMenu.Visibility = Visibility.Visible;
            }

        }

        private void btnCuenta_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAjustes_Click(object sender, RoutedEventArgs e)
        {

        }





        //---------------------------------------//
        //---------- GESTIÓN ZOOM SALA ----------//
        //---------------------------------------//

        //----- MANEJO EVENTOS REDIMENSIONADO -----//
        public void ajustarMapa()
        {
            double ancho = scrollMapa.ActualWidth;
            double alto = scrollMapa.ActualHeight;
            double anchoOriginal = gridMapaMesas.Width;
            double altoOriginal = gridMapaMesas.Height;

            double escalaX = ancho / anchoOriginal;
            double escalaY = alto / altoOriginal;

            double escalaAplicada = escalaX;
            if (escalaX > escalaY) { escalaAplicada = escalaY; }

            escalaAplicada *= zoomActual;

            escalaMapa.ScaleX = escalaAplicada;
            escalaMapa.ScaleY = escalaAplicada;
        }
            //----- MANEJO EVENTOS BOTONES + y - -----//
        private void btnZoom_Click(object sender, RoutedEventArgs e)
        {
            if (sender == btnZoomMas)
            {
                if (zoomActual < 3)
                {
                    zoomActual += 0.5;
                }
            }
            else
            {
                if (zoomActual > 1) {
                    zoomActual -= 0.5;
                }

            }
            ajustarMapa();
        }
        private void ventanaPrincipal_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ajustarMapa();
        }



        //----------------------------------------------//
        //---------- BANCO DE DATOS DE PRUEBA ----------//
        //----------------------------------------------//
        public void mesasPrueba()
        {
            //---------- DATOS DE LAS MESAS ----------//
            int[] posicionesX = { 13, 13, 13, 13, 13, 9, 11, 10, 9, 9,
                9, 7, 5, 3, 3, 5, 7, 9, 7, 5 };
            int[] posicionesY = { 15, 13, 9, 7, 5, 3, 5, 9, 12, 14,
                16, 2, 2, 3, 7, 9, 9, 6, 7, 5 };
            int[] spanX = { 1, 2, 1, 1, 1, 3, 1, 1, 2, 2,
                2, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            int[] spanY = { 3, 1, 3, 1, 1, 1, 1, 1, 1, 1,
                1, 2, 2, 3, 3, 2, 2, 1, 1, 1 };
            int[] comensales = { 7, 4, 6, 2, 2, 7, 2, 1, 3, 4,
                3, 5, 5, 8, 7, 4, 4, 2, 2, 1 };
            int[] capacidades = { 8, 5, 8, 2, 2, 8, 2, 2, 5, 5,
                5, 5, 5, 8, 8, 5, 5, 2, 2, 2 };
            EstadoMesa[] estados = {EstadoMesa.Ocupada, EstadoMesa.Libre, EstadoMesa.Reservada, EstadoMesa.OcupadaComanda, EstadoMesa.Libre,
                EstadoMesa.Ocupada, EstadoMesa.Libre, EstadoMesa.Reservada, EstadoMesa.Libre, EstadoMesa.OcupadaComanda,
                EstadoMesa.Reservada, EstadoMesa.Libre, EstadoMesa.Ocupada, EstadoMesa.Libre, EstadoMesa.OcupadaComanda,
                EstadoMesa.Reservada, EstadoMesa.Libre, EstadoMesa.Libre, EstadoMesa.Ocupada, EstadoMesa.OcupadaComanda };
            FormaMesa[] formas = { FormaMesa.Elipse, FormaMesa.Rectangular, FormaMesa.Rectangular, FormaMesa.Rectangular, FormaMesa.Rectangular,
                FormaMesa.Rectangular, FormaMesa.Rectangular, FormaMesa.Rectangular, FormaMesa.Rectangular, FormaMesa.Rectangular,
                FormaMesa.Rectangular, FormaMesa.Rectangular, FormaMesa.Rectangular, FormaMesa.Rectangular, FormaMesa.Rectangular,
                FormaMesa.Rectangular, FormaMesa.Rectangular, FormaMesa.Rectangular, FormaMesa.Rectangular, FormaMesa.Rectangular, };

            int i = 0;
            //---------- CREACIÓN DE MESAS ----------//
            for (i = 0; i < 20; i++)
            {
                Mesa m = new Mesa();
                Grid gridMesa = m.nuevaMesa(i, comensales[i], capacidades[i], estados[i], formas[i]);

                m.gridMesaRepresentada = gridMesa;

                gridMesa.MouseLeftButtonDown += (sender, e) => mesaSeleccionada_Click(m);

                gridMapaMesas.Children.Add(gridMesa);
                listaMesas.Add(m);

                Grid.SetColumn(gridMesa, posicionesX[i]);
                Grid.SetColumnSpan(gridMesa, spanX[i]);
                Grid.SetRow(gridMesa, posicionesY[i]);
                Grid.SetRowSpan(gridMesa, spanY[i]);

                m.figuraMesa.Stroke = Brushes.Black; m.figuraMesa.StrokeThickness = 2;
                m.setEstado(estados[i]);

            }
        }
        public void cartaPrueba()
        {
            // --- PRIMEROS (5 Platos) ---
            listaPlatosPrimeros.Add(new Plato(1, "Ensalada César Imperial", "Lechuga romana crujiente, crutones de ajo, parmesano reggiano y nuestra salsa secreta.", 9.50, TipoPlato.Primero));
            listaPlatosPrimeros.Add(new Plato(2, "Crema de Calabaza y Jengibre", "Suave crema de calabaza asada con un toque picante de jengibre y semillas de sésamo.", 8.00, TipoPlato.Primero));
            listaPlatosPrimeros.Add(new Plato(3, "Risotto de Setas Silvestres", "Arroz arborio cremoso cocinado con boletus, trufa negra y terminación de mantequilla.", 12.50, TipoPlato.Primero));
            listaPlatosPrimeros.Add(new Plato(4, "Gazpacho Andaluz Tradicional", "Sopa fría de tomate, pimiento y pepino con guarnición de jamón ibérico y huevo.", 7.50, TipoPlato.Primero));
            listaPlatosPrimeros.Add(new Plato(5, "Parrillada de Verduras", "Selección de verduras de temporada a la brasa con aceite de albahaca y sal maldon.", 10.00, TipoPlato.Primero));

            // --- SEGUNDOS (5 Platos) ---
            listaPlatosSegundos.Add(new Plato(1, "Entrecot de Ternera (300g)", "Corte de lomo bajo a la parrilla con pimientos de padrón y patatas gajo.", 22.00, TipoPlato.Segundo));
            listaPlatosSegundos.Add(new Plato(2, "Salmón Noruego al Horno", "Lomo de salmón glaseado con miel y mostaza sobre cama de espárragos trigueros.", 18.50, TipoPlato.Segundo));
            listaPlatosSegundos.Add(new Plato(3, "Carrillada Ibérica al Vino Tinto", "Carne tierna estofada a baja temperatura durante 12 horas con puré de patata trufado.", 16.00, TipoPlato.Segundo));
            listaPlatosSegundos.Add(new Plato(4, "Lubina a la Espalda", "Pescado fresco abierto a la plancha con refrito de ajos, guindilla y vinagre de Jerez.", 19.00, TipoPlato.Segundo));
            listaPlatosSegundos.Add(new Plato(5, "Lasaña de Carne a la Boloñesa", "Capas de pasta fresca, ragú de ternera y cerdo, bechamel suave y gratinado de mozzarella.", 14.50, TipoPlato.Segundo));

            // --- POSTRES (5 Platos) ---
            listaPlatosPostres.Add(new Plato(1, "Coulant de Chocolate", "Bizcocho caliente con corazón de chocolate fundido, acompañado de helado de vainilla.", 6.50, TipoPlato.Postre));
            listaPlatosPostres.Add(new Plato(2, "Tarta de Queso Casera", "Estilo New York Cheesecake con base de galleta y mermelada de frutos rojos silvestres.", 6.00, TipoPlato.Postre));
            listaPlatosPostres.Add(new Plato(3, "Tiramisú Clásico", "Capas de bizcocho soletilla empapados en café espresso y crema de mascarpone con cacao.", 5.50, TipoPlato.Postre));
            listaPlatosPostres.Add(new Plato(4, "Sorbete de Limón al Cava", "Refrescante batido de helado de limón con un toque de cava brut nature.", 5.00, TipoPlato.Postre));
            listaPlatosPostres.Add(new Plato(5, "Brocheta de Frutas de Temporada", "Selección de frutas frescas cortadas con baño de chocolate negro caliente.", 4.50, TipoPlato.Postre));

            // --- SOBREMESAS / BEBIDAS ESPECIALES (5 Platos) ---
            listaPlatosSobremesas.Add(new Plato(1, "Café Irlandés Especial", "Café espresso, whisky irlandés, azúcar moreno y capa de nata montada.", 7.00, TipoPlato.Sobremesa));
            listaPlatosSobremesas.Add(new Plato(2, "Gin Tonic Premium", "Ginebra de autor con tónica fever-tree, cardamomo y twist de lima.", 9.00, TipoPlato.Sobremesa));
            listaPlatosSobremesas.Add(new Plato(3, "Mojito Cubano", "Ron blanco, hierbabuena fresca, lima, azúcar y soda con mucho hielo picado.", 8.50, TipoPlato.Sobremesa));
            listaPlatosSobremesas.Add(new Plato(4, "Té Matcha Latte", "Té verde japonés en polvo batido con leche espumada y un toque de miel.", 4.00, TipoPlato.Sobremesa));
            listaPlatosSobremesas.Add(new Plato(5, "Chupito de Hierbas", "Licor digestivo tradicional de hierbas maceradas, servido muy frío.", 3.00, TipoPlato.Sobremesa));
        }
        public void comandasPrueba()
        {
            Random r = new Random();
            foreach (Mesa m in listaMesas)
            {
                if (m.comanda == null)
                {
                    m.comanda = new List<Plato>();
                }
                else
                {
                    m.comanda.Clear();
                }
                int numPlatos = r.Next(0, 5);
                for (int i = 0; i < numPlatos; i++)
                {
                    int tipo = r.Next(0, 4);
                    List<Plato> listaOrigen = null;
                    switch (tipo)
                    {
                        case 0: listaOrigen = listaPlatosPrimeros; break;
                        case 1: listaOrigen = listaPlatosSegundos; break;
                        case 2: listaOrigen = listaPlatosPostres; break;
                        case 3: listaOrigen = listaPlatosSobremesas; break;

                    }

                    if (listaOrigen != null && listaOrigen.Count > 0)
                    {
                        int id = r.Next(0, listaOrigen.Count);
                        m.comanda.Add(listaOrigen[id]);
                    }

                }

                           
                
            }
        }

    }
}
