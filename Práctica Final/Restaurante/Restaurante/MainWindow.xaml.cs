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
        List<Mesa> listaMesas = new List<Mesa>();
        private double zoomActual = 1;

        public MainWindow()
        {
            InitializeComponent();
            ajustarMapa();
            configInicial();
        }

        public void configInicial()
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
            EstadoMesa[] estados = {EstadoMesa.ocupada, EstadoMesa.libre, EstadoMesa.reservada, EstadoMesa.ocupadaComanda, EstadoMesa.libre,
                EstadoMesa.ocupada, EstadoMesa.libre, EstadoMesa.reservada, EstadoMesa.libre, EstadoMesa.ocupadaComanda,
                EstadoMesa.reservada, EstadoMesa.libre, EstadoMesa.ocupada, EstadoMesa.libre, EstadoMesa.ocupadaComanda,
                EstadoMesa.reservada, EstadoMesa.libre, EstadoMesa.libre, EstadoMesa.ocupada, EstadoMesa.ocupadaComanda };
            FormaMesa[] formas = { FormaMesa.rectangular8, FormaMesa.rectangular6, FormaMesa.rectangular8, FormaMesa.cuadrada2, FormaMesa.cuadrada2,
                FormaMesa.rectangular8, FormaMesa.cuadrada2, FormaMesa.cuadrada2, FormaMesa.rectangular6, FormaMesa.rectangular6,
                FormaMesa.rectangular6, FormaMesa.rectangular6, FormaMesa.rectangular6, FormaMesa.rectangular8, FormaMesa.rectangular8,
                FormaMesa.rectangular6, FormaMesa.rectangular6, FormaMesa.cuadrada2, FormaMesa.cuadrada2, FormaMesa.cuadrada2, };

            int i = 0;
            //---------- CREACIÓN DE MESAS ----------//
            for (i = 0; i < 20; i++)
            {
                Mesa m = new Mesa();
                Rectangle figuraMesa = m.nuevaMesa(i, comensales[i], capacidades[i], estados[i], formas[i]);

                gridMapaMesas.Children.Add(figuraMesa);
                listaMesas.Add(m);

                Grid.SetColumn(figuraMesa, posicionesX[i]);
                Grid.SetColumnSpan(figuraMesa, spanX[i]);
                Grid.SetRow(figuraMesa, posicionesY[i]);
                Grid.SetRowSpan(figuraMesa, spanY[i]);

                switch (m.estado)
                {
                    case EstadoMesa.libre: figuraMesa.Fill = Brushes.White; break;

                    case EstadoMesa.reservada: figuraMesa.Fill = Brushes.Yellow; break;

                    case EstadoMesa.ocupada: figuraMesa.Fill = Brushes.Orange; break;

                    case EstadoMesa.ocupadaComanda: figuraMesa.Fill = Brushes.Red; break;
                }
            }
        }
        
            
            //---------- FUNCIONES DE COLUMNA DE BOTONES ----------//

        private void btnVerDatosMesa_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAnadirMesa_Click(object sender, RoutedEventArgs e)
        {
            if (extendedMenu.Visibility == Visibility.Visible)
            {
                btnAnadirMesa.Background = Brushes.LightGray;
                extendedMenu.Visibility = Visibility.Hidden;
            }
            else
            {
                btnAnadirMesa.Background = Brushes.Gray;
                extendedMenu.Visibility = Visibility.Visible;
            }

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
            //----- MANEJO EVENTOS ZOOM -----//
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
    }
}
