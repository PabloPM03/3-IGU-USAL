using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

using System.Windows.Shapes;        //Para poder crear figuras
using System.Windows.Controls;      //Para poder crear el grid
using System.Windows;               //Para reducir texto de propiedades

namespace Restaurante
{
    public enum EstadoMesa { Libre, Reservada, Ocupada, OcupadaComanda };
    public enum FormaMesa { Rectangular, Elipse };
    public class Mesa
    {
        public int id { get; set; }
        public int comensales { get; set; }
        public int capacidad { get; set; }
        public int posicionX { get; set; }
        public int posicionY {  get; set; }
        public EstadoMesa estado { get; set; }
        public FormaMesa forma { get; set; }
        public List<Plato> comanda { get; set; }
        public Shape figuraMesa { get; set; }
        public Grid gridMesaRepresentada { get; set; }




        public Grid nuevaMesa(int id, int comensales, int capacidad, EstadoMesa estado, FormaMesa forma)
        {
            Grid gridMesa = new Grid(); //Grid interno para colocar la figura de la mesa y los diferentes datos

            this.id = id;
            this.comensales = comensales;
            this.capacidad = capacidad;
            this.estado = estado;
            this.forma = forma;

                //CREACIÓN FORMA MESA
            if (forma == FormaMesa.Rectangular)
            {
                this.figuraMesa = new Rectangle();
            }
            else
            {
                this.figuraMesa = new Ellipse();
            }

                //CREACIÓN TEXTOS CON DATOS
            StackPanel panelDatos = new StackPanel();
            panelDatos.VerticalAlignment = VerticalAlignment.Center;
            panelDatos.HorizontalAlignment = HorizontalAlignment.Center;
            panelDatos.Orientation = Orientation.Vertical;

            TextBlock txtId = new TextBlock();
            txtId.Text = "MESA " + this.id;
            txtId.FontSize = 6;
            txtId.HorizontalAlignment = HorizontalAlignment.Center;

            TextBlock txtCapacidad = new TextBlock();
            txtCapacidad.Text = "Capacidad: " + this.capacidad;
            txtCapacidad.FontSize = 6;
            txtCapacidad.HorizontalAlignment = HorizontalAlignment.Center;

            TextBlock txtComensales = new TextBlock();
            txtComensales.Text = "Comensales " + this.comensales;
            txtComensales.FontSize = 6;
            txtComensales.HorizontalAlignment = HorizontalAlignment.Center;

            panelDatos.Children.Add(txtId);
            panelDatos.Children.Add(txtCapacidad);
            panelDatos.Children.Add(txtComensales);

            gridMesa.Children.Add(this.figuraMesa);

            gridMesa.Children.Add(panelDatos);

            return gridMesa;
        }


        public void anadirPlato(Plato plato)
        {
            comanda.Add(plato);
        }

        public void setEstado(EstadoMesa estado)
        {
            this.estado = estado;
            switch (estado)
            {
                case EstadoMesa.Libre: figuraMesa.Fill = Brushes.White; break;

                case EstadoMesa.Reservada: figuraMesa.Fill = Brushes.Yellow; break;

                case EstadoMesa.Ocupada: figuraMesa.Fill = Brushes.Orange; break;

                case EstadoMesa.OcupadaComanda: figuraMesa.Fill = Brushes.Red; break;
            }
        }
    }
}
