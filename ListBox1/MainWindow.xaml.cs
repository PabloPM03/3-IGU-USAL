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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ListBox1
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        public void MainWindow_Loaded(object sender, EventArgs e)
        {

        }

        private void tbnAnadir_Click(object sender, RoutedEventArgs e)
        {
            string nuevoElemento;
            if (txtNombre.Text.Length > 0)
            {
                nuevoElemento = txtNombre.Text;
                if (txtApellidos.Text.Length > 0)
                {
                    nuevoElemento = nuevoElemento + " " + txtApellidos.Text;
                }
                listNombres.Items.Add(nuevoElemento);
                txtNombre.Clear();
                txtApellidos.Clear();
            }
            else
            {
                MessageBox.Show("Por favor, ingrese un nombre");
            }
        }

        private void listNombres_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listNombres.SelectedItem != null)
            {
                string nombreSeleccionado = listNombres.SelectedItem.ToString();
                labelSalida.Content = nombreSeleccionado;
            }
            else
            {
                labelSalida.Content = " ";
            }
        }
    }
}
