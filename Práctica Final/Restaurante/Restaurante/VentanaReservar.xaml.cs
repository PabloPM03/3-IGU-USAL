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
    public partial class VentanaReservar : Window
    {
        public VentanaReservar()
        {
            InitializeComponent();
            this.DataContext = DatosRestaurante.datos;
        }

        private void btn_Aceptar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
