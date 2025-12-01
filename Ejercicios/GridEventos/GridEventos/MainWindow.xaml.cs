using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GridEventos;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Window_KeyDown(object sender, KeyEventArgs e)
    {
        int row = Grid.GetRow(ElRect);
        int col = Grid.GetColumn(ElRect);

        if (e.Key == Key.Left)
        {
            col--;
            Grid.SetColumn(ElRect, col);
        }
        else if (e.Key == Key.Right)
        {
            col++;
            Grid.SetColumn(ElRect, col);
        }
        else if (e.Key == Key.Up)
        {
            row--;
            Grid.SetRow(ElRect, row);
        }
        else if (e.Key == Key.Down)
        {
            row++;
            Grid.SetRow(ElRect, col);
        }
    }
}