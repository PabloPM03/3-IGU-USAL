using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public VentanaReservar ventanaModificarDatos;
        public VentanaAnadirPlato ventanaAnadirPlato;

        private double zoomActual = 1;

        public MainWindow()
        {
            InitializeComponent();

            bool sesionLimpia = true;
            MessageBoxResult resultado = MessageBox.Show("Bienvenido, quieres iniciar una sesión cargada con los datos de prueba? (En caso contrario se iniciará una sesión vacía)", "Bienvenido",
            MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (resultado == MessageBoxResult.Yes) { sesionLimpia = false; }
            DatosRestaurante.datos.inicializarDatos(sesionLimpia);

            this.DataContext = DatosRestaurante.datos;
            
            dibujarMesasMapa();
            ajustarMapa();

            DatosRestaurante.datos.cambioSeleccionMesa += OnCambioSeleccionMesa;
        }


            //-------------------------------//
            //-- GESTIÓN MESA SELECCIONADA --//
            //-------------------------------//
        private void OnCambioSeleccionMesa(Mesa mesaSeleccionada)
        {
            if ( mesaSeleccionada != null) 
            { 
                seleccionarMesa(mesaSeleccionada); 
                if (comboBox_menuControlMesa.SelectedItem != mesaSeleccionada)
                {
                    comboBox_menuControlMesa.SelectedItem = mesaSeleccionada;
                }
            }
            actualizarGraficas();
        }
        public void mesaSeleccionada_Click(Mesa mesaSeleccionada)
        {
            this.DataContext = DatosRestaurante.datos;
            DatosRestaurante.datos.mesaSeleccionada = mesaSeleccionada;
            seleccionarMesa(mesaSeleccionada);
        }
        public void seleccionarMesa(Mesa mesaSeleccionada)
        {
            foreach (Mesa m in DatosRestaurante.datos.listaMesas)
            {
                m.figuraMesa.Stroke = Brushes.Black; m.figuraMesa.StrokeThickness = 2;
            }
            mesaSeleccionada.figuraMesa.Stroke = Brushes.Blue;
            mesaSeleccionada.figuraMesa.StrokeThickness = 5;

            
            menuControlMesa.Visibility = Visibility.Visible;
            actualizarInterfazSegunEstado();
            btnGestionMesas.Background = Brushes.Gray;

            btnVerCarta.Background = Brushes.LightGray;
            btnEstadistica.Background = Brushes.LightGray;
        }


        public void actualizarInterfazSegunEstado()
        {
            switch (DatosRestaurante.datos.mesaSeleccionada.estado)
            {
                case EstadoMesa.Libre:
                    panelInfoMesa.Visibility = Visibility.Hidden;
                    panelInfoMesaLibre.Visibility = Visibility.Visible;

                    btn_Funcion2.Content = "Reservar Mesa";
                    btn_Funcion1.Visibility = Visibility.Hidden;
                    btn_Funcion2.Visibility = Visibility.Visible;
                    btn_Funcion3.Visibility = Visibility.Hidden;

                    gridComandas.Visibility = Visibility.Hidden;
                    break;

                case EstadoMesa.Reservada:
                    panelInfoMesaLibre.Visibility = Visibility.Hidden;
                    panelInfoMesa.Visibility = Visibility.Visible;

                    btn_Funcion1.Content = "Anular Reserva";
                    btn_Funcion2.Content = "Modificar Reserva";
                    btn_Funcion3.Content = "Clientes Sentados";
                    btn_Funcion1.Visibility = Visibility.Visible;
                    btn_Funcion2.Visibility = Visibility.Visible;
                    btn_Funcion3.Visibility = Visibility.Visible;

                    gridComandas.Visibility = Visibility.Hidden;
                    break;

                case EstadoMesa.Ocupada:
                    panelInfoMesaLibre.Visibility = Visibility.Hidden;
                    panelInfoMesa.Visibility = Visibility.Visible;

                    btn_Funcion2.Content = "Tomar Nota";
                    btn_Funcion1.Visibility = Visibility.Hidden;
                    btn_Funcion2.Visibility = Visibility.Visible;
                    btn_Funcion3.Visibility = Visibility.Hidden;

                    gridComandas.Visibility = Visibility.Hidden;
                    break;

                case EstadoMesa.OcupadaComanda:
                    panelInfoMesaLibre.Visibility = Visibility.Hidden;
                    panelInfoMesa.Visibility = Visibility.Visible;

                    btn_Funcion1.Content = "Añadir Platos";
                    btn_Funcion3.Content = "Clientes Marcharon";
                    btn_Funcion1.Visibility = Visibility.Visible;
                    btn_Funcion2.Visibility = Visibility.Hidden;
                    btn_Funcion3.Visibility = Visibility.Visible;

                    cuadroComandas.ItemsSource = DatosRestaurante.datos.mesaSeleccionada.comanda;
                    gridComandas.Visibility = Visibility.Visible;

                    break;
            }
        }
            //AQUI SE GESTIONAN LOS CLICKS EN LOS BOTONES DEL MENU DESPLEGADO
        private void botonesMenuGestionMesas_Click(object sender, RoutedEventArgs e)
        {
                //ESTADO MESA LIBRE
            if (DatosRestaurante.datos.mesaSeleccionada.estado == EstadoMesa.Libre && sender == btn_Funcion2)
            {
                ventanaModificarDatos = new VentanaReservar();
                ventanaModificarDatos.ShowDialog();
                DatosRestaurante.datos.mesaSeleccionada.estado = EstadoMesa.Reservada;
            }

            //ESTADO MESA RESERVADA
            else if (DatosRestaurante.datos.mesaSeleccionada.estado == EstadoMesa.Reservada)
            {
                if (sender == btn_Funcion1) //Anular Reserva
                {
                    DatosRestaurante.datos.mesaSeleccionada.comensales = 0;
                    DatosRestaurante.datos.mesaSeleccionada.estado = EstadoMesa.Libre;
                } 
                else if (sender == btn_Funcion2)    //Modificar Reserva
                {
                    ventanaModificarDatos = new VentanaReservar();
                    ventanaModificarDatos.ShowDialog();
                }
                else    //CLIENTES SENTADOS
                {
                    DatosRestaurante.datos.mesaSeleccionada.estado = EstadoMesa.Ocupada;
                }
            }

            //ESTADO MESA OCUPADA
            else if (DatosRestaurante.datos.mesaSeleccionada.estado == EstadoMesa.Ocupada && sender == btn_Funcion2)
            {
                ventanaAnadirPlato = new VentanaAnadirPlato();
                ventanaAnadirPlato.ShowDialog();
            }

            //ESTADO MESA OCUPADA CON COMANDA
            else if (DatosRestaurante.datos.mesaSeleccionada.estado == EstadoMesa.OcupadaComanda)
            {
                if (sender == btn_Funcion1)
                {
                    ventanaAnadirPlato = new VentanaAnadirPlato();
                    ventanaAnadirPlato.ShowDialog();
                }
                else if (sender == btn_Funcion3)
                {
                    DatosRestaurante.datos.mesaSeleccionada.comensales = 0;
                    DatosRestaurante.datos.mesaSeleccionada.estado = EstadoMesa.Libre;
                }
            }

            if (sender == btn_GestorAvanzado)
            {
                if (ventanaGestionMesas == null || !ventanaGestionMesas.IsLoaded)
                {
                    ventanaGestionMesas = new VentanaGestionMesas();
                    ventanaGestionMesas.Show();
                }
                else { ventanaGestionMesas.Activate(); }
            }
            actualizarInterfazSegunEstado();
        }


        //---------- FUNCIONES DE COLUMNA DE BOTONES ----------//

        private void btnGestionMesas_Click(object sender, RoutedEventArgs e)
        {
            if (menuControlMesa.Visibility == Visibility.Visible)
            {
                btnGestionMesas.Background = Brushes.LightGray;
                menuControlMesa.Visibility = Visibility.Hidden;
            }
            else
            {
                if (gridZoom.Visibility == Visibility.Visible)
                {
                    if (DatosRestaurante.datos.mesaSeleccionada == null) DatosRestaurante.datos.mesaSeleccionada = DatosRestaurante.datos.listaMesas[0];
                    btnGestionMesas.Background = Brushes.Gray;
                    menuControlMesa.Visibility = Visibility.Visible;
                }

                    gridZoom.Visibility = Visibility.Visible;
                    btnEstadistica.Background = Brushes.LightGray;
                    tabEstadistica.Visibility = Visibility.Hidden;
            }
        }


        private void btnEstadistica_Click(object sender, RoutedEventArgs e)
        {
            if (tabEstadistica.Visibility == Visibility.Visible)
            {
                gridZoom.Visibility = Visibility.Visible;
                btnEstadistica.Background = Brushes.LightGray;
                tabEstadistica.Visibility = Visibility.Hidden;
            }
            else
            {
                if (DatosRestaurante.datos.mesaSeleccionada != null)
                {
                    tabEstadistica.SelectedIndex = 1;
                }
                else
                {
                    tabEstadistica.SelectedIndex = 0;
                }
                gridZoom.Visibility = Visibility.Hidden;
                btnGestionMesas.Background = Brushes.LightGray;
                menuControlMesa.Visibility = Visibility.Hidden;
                btnEstadistica.Background = Brushes.Gray;
                tabEstadistica.Visibility = Visibility.Visible;
            }

            this.UpdateLayout();
            actualizarGraficas();

        }


        private void btnVerCarta_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Opción en desarrollo, disculpa las molestias", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);

        }


        private void btnReiniciar_Click(object sender, RoutedEventArgs e)
        {
            bool sesionLimpia = false;

            MessageBoxResult resultado = MessageBox.Show("Quieres iniciar una sesión limpia? (En caso contrario se cargaran los datos de prueba)", "Bienvenido",
            MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (resultado != MessageBoxResult.Cancel)
            {
                if (resultado == MessageBoxResult.Yes) { sesionLimpia = true; }


                DatosRestaurante.datos.inicializarDatos(sesionLimpia);
                this.DataContext = DatosRestaurante.datos;

                dibujarMesasMapa();
                ajustarMapa();

            }
        }





        //-----------------------------------------//
        //---------- GESTIÓN VISUAL SALA ----------//
        //-----------------------------------------//

        public void dibujarMesasMapa()
        {
            foreach (Mesa m in DatosRestaurante.datos.listaMesas)
            {
                Grid gridMesa = m.gridMesaRepresentada;
                gridMesa.MouseLeftButtonDown += (sender, e) => mesaSeleccionada_Click(m);
                gridMapaMesas.Children.Add(gridMesa);

                Grid.SetColumn(gridMesa, m.posicionX);
                Grid.SetColumnSpan(gridMesa, m.spanX);
                Grid.SetRow(gridMesa, m.posicionY);
                Grid.SetRowSpan(gridMesa, m.spanY);
            }
        }

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
            actualizarGraficas();
        }



        //-----------------------------------------//
        //---------- GESTIÓN ESTADISTICA ----------//
        //-----------------------------------------//

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl) actualizarGraficas();
        }
        private void actualizarGraficas()
        {
            if (tabEstadistica.Visibility != Visibility.Visible) return;

            if (tabEstadistica.SelectedIndex == 0) dibujarGraficaGlobal();
            else dibujarGraficaMesa(DatosRestaurante.datos.mesaSeleccionada);
        }

            //GRAFICA GLOBAL
        private void dibujarGraficaGlobal()
        {
            if (canvasGlobal == null) return;

            canvasGlobal.Children.Clear();
            ObservableCollection<Mesa> listaMesas = DatosRestaurante.datos.listaMesas;
            if (listaMesas.Count == 0) return;


            double maxPlatos = 0;
            foreach (var m in listaMesas)
            {
                if (m.historialPlatos.Count > maxPlatos) maxPlatos = m.historialPlatos.Count;
            }
            if (maxPlatos == 0) maxPlatos = 1;

            double anchoCanvas = canvasGlobal.ActualWidth;
            double altoCanvas = canvasGlobal.ActualHeight - 30; // Margen inferior

            if (anchoCanvas <= 0 || altoCanvas <= 0) return;

            double anchoBarra = (anchoCanvas / listaMesas.Count) * 0.7;
            double espacio = (anchoCanvas / listaMesas.Count) * 0.3;
            for (int i = 0; i < listaMesas.Count; i++)
            {
                Mesa m = listaMesas[i];
                double alturaBarra = (m.historialPlatos.Count / maxPlatos) * altoCanvas;

                Rectangle rect = new Rectangle();
                rect.Width = anchoBarra;
                rect.Height = alturaBarra;
                rect.Fill = Brushes.Crimson;
                rect.ToolTip = $"Mesa {m.id}: {m.historialPlatos.Count} platos totales";

                double x = (i * (anchoBarra + espacio)) + (espacio / 2);
                double y = altoCanvas - alturaBarra;

                Canvas.SetLeft(rect, x);
                Canvas.SetTop(rect, y);
                canvasGlobal.Children.Add(rect);

                TextBlock label = new TextBlock();
                label.Text = "M" + m.id;
                label.FontSize = 10;
                Canvas.SetLeft(label, x + (anchoBarra / 2) - 8);
                Canvas.SetTop(label, altoCanvas + 5);
                canvasGlobal.Children.Add(label);
            }
        }

        //GRAFICA MESA SELECCIONADA
        private void dibujarGraficaMesa(Mesa m)
        {
            if (canvasMesa == null) return;
            canvasMesa.Children.Clear();

            if (m == null) { txtTituloMesaEstadistica.Text = "Seleccione una mesa primero."; return; }

            txtTituloMesaEstadistica.Text = $"Estadística Mesa {m.id} (Total Histórico: {m.historialPlatos.Count})";

            var primeros = m.historialPlatos.Where(p => p.tipo == TipoPlato.Primero).ToList();
            var segundos = m.historialPlatos.Where(p => p.tipo == TipoPlato.Segundo).ToList();
            var postres = m.historialPlatos.Where(p => p.tipo == TipoPlato.Postre).ToList();
            var sobremesas = m.historialPlatos.Where(p => p.tipo == TipoPlato.Sobremesa).ToList();

            double maxPlatos = Math.Max(Math.Max(primeros.Count, segundos.Count), Math.Max(postres.Count, sobremesas.Count));

            if (maxPlatos == 0) maxPlatos = 1;

            double anchoCanvas = canvasMesa.ActualWidth;
            double altoCanvas = canvasMesa.ActualHeight - 200;

            if (anchoCanvas <= 0 || altoCanvas <= 0) return;

            double anchoColumna = anchoCanvas / 7;

            dibujarColumnaApilada(primeros, anchoCanvas * 0.15, anchoColumna, altoCanvas, maxPlatos, "Primeros");
            dibujarColumnaApilada(segundos, anchoCanvas * 0.38, anchoColumna, altoCanvas, maxPlatos, "Segundos");
            dibujarColumnaApilada(postres, anchoCanvas * 0.61, anchoColumna, altoCanvas, maxPlatos, "Postres");
            dibujarColumnaApilada(sobremesas, anchoCanvas * 0.84, anchoColumna, altoCanvas, maxPlatos, "Sobremesa");
        }

        private void dibujarColumnaApilada(List<Plato> platos, double xCentro, double ancho, double altoCanvas, double maxScale, string categoriaPlato)
        {
            var grupos = platos.GroupBy(p => p.nombre).Select(g => new { Nombre = g.Key, Cantidad = g.Count() }).ToList(); //Agrupación platos iguales

            double yAcumulada = altoCanvas;
            Random r = new Random();

            StackPanel panelLeyendaPorCategoria = new StackPanel();
            panelLeyendaPorCategoria.Orientation = Orientation.Vertical;
            panelLeyendaPorCategoria.Width = ancho + 40;

            foreach (var grupo in grupos)
            {
                double alturaSegmento = (grupo.Cantidad / maxScale) * altoCanvas;

                SolidColorBrush colorPlato = new SolidColorBrush(Color.FromRgb((byte)r.Next(50, 200), (byte)r.Next(50, 200), (byte)r.Next(50, 200)));

                Rectangle rect = new Rectangle();
                rect.Width = ancho;
                rect.Height = alturaSegmento;
                rect.Fill = colorPlato;
                rect.ToolTip = $"{grupo.Nombre}: {grupo.Cantidad}";

                yAcumulada -= alturaSegmento;
                Canvas.SetLeft(rect, xCentro - (ancho / 2));
                Canvas.SetTop(rect, yAcumulada + 20);
                canvasMesa.Children.Add(rect);

                StackPanel itemLeyenda = new StackPanel();
                itemLeyenda.Orientation = Orientation.Horizontal;
                itemLeyenda.Margin = new Thickness(0, 2, 0, 2);

                Rectangle colorBox = new Rectangle();
                colorBox.Width = 10;
                colorBox.Height = 10;
                colorBox.Fill = colorPlato;
                colorBox.Margin = new Thickness(0, 0, 5, 0);

                TextBlock txtPlato = new TextBlock();
                txtPlato.Text = $"{grupo.Nombre} ({grupo.Cantidad})";
                txtPlato.FontSize = 9;
                txtPlato.TextWrapping = TextWrapping.Wrap;

                itemLeyenda.Children.Add(colorBox);
                itemLeyenda.Children.Add(txtPlato);

                panelLeyendaPorCategoria.Children.Add(itemLeyenda);
            }

            TextBlock labelCategoriaPlato = new TextBlock();
            labelCategoriaPlato.Text = categoriaPlato;
            labelCategoriaPlato.FontWeight = FontWeights.Bold;
            labelCategoriaPlato.HorizontalAlignment = HorizontalAlignment.Center;

            Canvas.SetLeft(labelCategoriaPlato, xCentro - 20);
            Canvas.SetTop(labelCategoriaPlato, altoCanvas + 25);
            canvasMesa.Children.Add(labelCategoriaPlato);

            Canvas.SetLeft(panelLeyendaPorCategoria, xCentro - (ancho / 2) - 20);
            Canvas.SetTop(panelLeyendaPorCategoria, altoCanvas + 45);
            canvasMesa.Children.Add(panelLeyendaPorCategoria);
        }
    }
}
