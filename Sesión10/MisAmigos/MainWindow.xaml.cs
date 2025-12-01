using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

public class ClaseAmigo : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string nombrePropiedad)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nombrePropiedad));
    }

    string _nombre;
    string _apellido;
    string _alias;

    public string Nombre
    {
        get { return _nombre; }
        set
        { 
            _nombre = value;
            OnPropertyChanged("Nombre");
        }
    }
    public string Apellido
    {
        get { return _apellido; }
        set
        {
            _apellido = value;
            OnPropertyChanged("Apellido");
        }
    }
    public string Alias
    {
        get { return _alias; }
        set
        {
            _alias = value;
            OnPropertyChanged("Alias");
        }
    }

    public ClaseAmigo(string nombre, string apellido, string alias)
    {
        Nombre = nombre;
        Apellido = apellido;
        Alias = alias;
    }
}

namespace MisAmigos
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<ClaseAmigo> misAmigos;
        public MainWindow()
        {
            InitializeComponent();

            misAmigos = new ObservableCollection<ClaseAmigo>();

            AliasListBox.ItemsSource = misAmigos;
            NombreCompletoListBox.ItemsSource = misAmigos;

            //ContentRendered evento

            misAmigos.Add(new ClaseAmigo("Juan", "Pérez", "juanito"));
            misAmigos.Add(new ClaseAmigo("María", "González", "mary"));
            misAmigos.Add(new ClaseAmigo("Luis", "Rodríguez", "lucho"));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ModificarBoton.Content.ToString() == "MODIFICAR AMIGO" && AliasListBox.SelectedItem != null)
            {
                ClaseAmigo amigoSeleccionado = (ClaseAmigo) AliasListBox.SelectedItem;

                amigoSeleccionado.Nombre = NombreTextBox.Text;
                amigoSeleccionado.Apellido = ApellidoTextBox.Text;
                amigoSeleccionado.Alias = AliasTextBox.Text;

                AliasListBox.Items.Refresh();
                NombreCompletoListBox.Items.Refresh();
            }
            else
            {
                ClaseAmigo nuevo = new ClaseAmigo(NombreTextBox.Text, ApellidoTextBox.Text, AliasTextBox.Text);
                misAmigos.Add(nuevo);
                AliasListBox.Items.Refresh();
                NombreCompletoListBox.Items.Refresh();
            }

            //OPCIONAL
            /*
            foreach (var amigo in misAmigos)
            {
                Console.WriteLine(
            }
            */
        }

        private void AliasListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClaseAmigo amigoSeleccionado = (ClaseAmigo)AliasListBox.SelectedItem;

            if (amigoSeleccionado != null)
            {
                NombreTextBox.Text = amigoSeleccionado.Nombre;
                ApellidoTextBox.Text = amigoSeleccionado.Apellido;
                AliasTextBox.Text = amigoSeleccionado.Alias;
                ModificarBoton.Content = "MODIFICAR AMIGO";
            }
            else
            {
                ModificarBoton.Content = "AGREGAR AMIGO";
            }
            NombreCompletoListBox.SelectedItem = amigoSeleccionado;
        }
    }
}
