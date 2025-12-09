using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Data;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows;

namespace Restaurante
{
    public enum EstadoMesa { Libre, Reservada, Ocupada, OcupadaComanda };
    public enum FormaMesa { Rectangular, Elipse };


    public class Mesa : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        public int id { get; set; }
        public int capacidad { get; set; }
        public int posicionX { get; set; }
        public int spanX { get; set; }
        public int posicionY { get; set; }
        public int spanY { get; set; }
        public Shape figuraMesa { get; set; }
        public Grid gridMesaRepresentada { get; set; }

        private FormaMesa _forma;
        public FormaMesa forma
        {
            get { return _forma; }
            set { if (_forma == value) return; _forma = value; OnPropertyChanged("forma"); actualizarFormaVisual(); }
        }
        private int _comensales;
        public int comensales
        {
            get { return _comensales; }
            set 
            {
                int valorFinal = value;
                if (_comensales == value) return;
                if (value > this.capacidad) valorFinal = this.capacidad;
                else if (valorFinal <= 0) valorFinal = 1;
                else _comensales = value;

                _comensales = valorFinal; 
                OnPropertyChanged("comensales"); 
            }
        }
        private EstadoMesa _estado;
        public EstadoMesa estado
        {
            get { return _estado; }
            set { if (_estado == value) return; _estado = value; OnPropertyChanged("estado"); actualizarColorMesa();  }
        }
        private ObservableCollection<Plato> _comanda;
        public ObservableCollection<Plato> comanda
        {
            get { return _comanda; }
            set { if (_comanda == value) return; OnPropertyChanged("comanda"); }
        }
        private ObservableCollection<Plato> _historialPlatos;
        public ObservableCollection<Plato> historialPlatos
        {
            get { return _historialPlatos; }
            set { if (_historialPlatos == value) return; OnPropertyChanged("historialPlatos"); }
        }





        public Mesa()
        {
            _comanda = new ObservableCollection<Plato>();
            _historialPlatos = new ObservableCollection<Plato>();
        }



        public void anadirPlato(Plato plato)
        {
            comanda.Add(plato);
            historialPlatos.Add(plato);

            if (this.estado != EstadoMesa.OcupadaComanda)
            {
                this.estado = EstadoMesa.OcupadaComanda;
            }
        }


        public Grid nuevaMesa(int id, int comensales, int capacidad, EstadoMesa estado, FormaMesa forma)
        {
                //GRID CONTENEDOR
            Grid gridMesa = new Grid();

            this.id = id;
            this._comensales = comensales;
            this.capacidad = capacidad;
            this._estado = estado;
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

            this.figuraMesa.Stroke = Brushes.Black;
            this.figuraMesa.StrokeThickness = 2;

                //CREACIÓN TEXTOS CON DATOS
            StackPanel panelDatos = new StackPanel();
            panelDatos.VerticalAlignment = VerticalAlignment.Center;
            panelDatos.HorizontalAlignment = HorizontalAlignment.Center;
            panelDatos.Orientation = Orientation.Vertical;

            TextBlock txtId = new TextBlock();
            txtId.Text = "MESA " + this.id;
            txtId.FontSize = 6;
            txtId.FontWeight = FontWeights.Bold;
            txtId.HorizontalAlignment = HorizontalAlignment.Center;

            TextBlock txtCapacidad = new TextBlock();
            txtCapacidad.Text = "Capacidad: " + this.capacidad;
            txtCapacidad.FontSize = 6;
            txtCapacidad.HorizontalAlignment = HorizontalAlignment.Center;

            TextBlock txtComensales = new TextBlock();
            txtComensales.FontSize = 6;
            txtComensales.HorizontalAlignment = HorizontalAlignment.Center;

            Binding bindingComensales = new Binding("comensales");
            bindingComensales.Source = this;
            bindingComensales.StringFormat = "Comensales: {0}";
            txtComensales.SetBinding(TextBlock.TextProperty, bindingComensales);

            panelDatos.Children.Add(txtId);
            panelDatos.Children.Add(txtCapacidad);
            panelDatos.Children.Add(txtComensales);

            gridMesa.Children.Add(this.figuraMesa);

            gridMesa.Children.Add(panelDatos);

            this.estado = estado;

            actualizarColorMesa();

            return gridMesa;
        }
        public void actualizarColorMesa()
        {
            if (figuraMesa == null)
            {
                return;
            }
            switch (this.estado)
            {
                case EstadoMesa.Libre: figuraMesa.Fill = Brushes.White; break;
                case EstadoMesa.Reservada: figuraMesa.Fill = Brushes.Yellow; break;
                case EstadoMesa.Ocupada: figuraMesa.Fill = Brushes.Orange; break;
                case EstadoMesa.OcupadaComanda: figuraMesa.Fill = Brushes.Red; break;
            }
        }
        public void actualizarFormaVisual()
        {
            if (this.gridMesaRepresentada == null) return;
            if (this.figuraMesa != null && this.gridMesaRepresentada.Children.Contains(this.figuraMesa))
            {
                this.gridMesaRepresentada.Children.Remove(this.figuraMesa);
            }

            if (this.forma == FormaMesa.Rectangular)
            {
                this.figuraMesa = new Rectangle();
            }
            else
            {
                this.figuraMesa = new Ellipse();
            }

            this.figuraMesa.Stroke = Brushes.Black;
            this.figuraMesa.StrokeThickness = 2;
            this.gridMesaRepresentada.Children.Insert(0, this.figuraMesa);

            actualizarColorMesa();
        }
    }
}
