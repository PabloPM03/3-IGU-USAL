using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurante
{
    public enum TipoPlato { Primero, Segundo, Postre, Sobremesa }
    public class Plato
    {
        public int id {  get; set; }
        public String nombre {  get; set; }
        public TipoPlato tipo { get; set; }
        public String descripcion { get; set; }
        public double precio { get; set; }
        public Plato(int id, String nombre, String descripcion,double precio, TipoPlato tipo)
        {
            this.id = id;
            this.nombre = nombre;
            this.tipo = tipo;
            this.descripcion = descripcion;
            this.precio = precio;
        }

    }
}
