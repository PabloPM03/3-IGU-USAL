using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaAmigos
{
    internal class Amigo
    {
        public string nombre { get; set; }
        public string apellido { get; set; }
        public int edad { get; set; }

        public Amigo(string nombre, string apellido)
        {
            Random random = new Random();
            this.nombre = nombre;
            this.apellido = apellido;
            this.edad = random.Next(18, 25+1);
        }

        public override string ToString()
        {
            return $"{nombre} {apellido} ({edad})";
        }

    }
}
