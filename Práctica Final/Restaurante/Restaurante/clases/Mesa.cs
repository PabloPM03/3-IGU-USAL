using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Restaurante
{
    public enum EstadoMesa { libre, reservada, ocupada, ocupadaComanda };
    public enum FormaMesa { cuadrada2, rectangular6, rectangular8, circulo2, elipse6, elipse8 };
    internal class Mesa
    {
        public int id { get; set; }
        public int comensales { get; set; }
        public int capacidad { get; set; }
        public int posicionX { get; set; }
        public int posicionY {  get; set; }
        public EstadoMesa estado { get; set; }
        public FormaMesa forma { get; set; }
        public Comanda comanda { get; set; }
        public Rectangle figuraMesa { get; set; }

        public void setEstado(Enum estado) { }

        public Rectangle nuevaMesa(int id, int comensales, int capacidad, EstadoMesa estado, FormaMesa forma)
        {
            this.id = id;
            this.comensales = comensales;
            this.capacidad = capacidad;
            this.estado = estado;
            this.forma = forma;

            Rectangle figuraMesa = new Rectangle();
            figuraMesa.Fill = Brushes.White;
            figuraMesa.Stroke = Brushes.Black;
            figuraMesa.StrokeThickness = 2;

            this.figuraMesa = figuraMesa;

            return figuraMesa;
        }
    }
}
