using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GridEventosSobreGridRaton;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        Loaded += MainWindow_Loaded;
    }


    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        ElGrid.Focusable = true;
        ElGrid.Focus();
    }

    private void Window_KeyDown(object sender, KeyEventArgs e)
    {
        int row = Grid.GetRow(ElRect);
        int col = Grid.GetColumn(ElRect);

        if (e.Key == Key.Left)
        {
            col--;
            if (col < 0)
            {
                col = ElGrid.ColumnDefinitions.Count - 1;
            }
            Grid.SetColumn(ElRect, col);
        }
        else if (e.Key == Key.Right)
        {
            col++;
            if (col < 0)
            {
                col = 0;
            }
            Grid.SetColumn(ElRect, col);
        }
        else if (e.Key == Key.Up)
        {
            row--;
            if (row < 0)
            {
                row = ElGrid.RowDefinitions.Count - 1;
            }
            Grid.SetRow(ElRect, row);
        }
        else if (e.Key == Key.Down)
        {
            row++;
            if (row < 0)
            {
                row = ElGrid.RowDefinitions.Count - 1;
            }
            Grid.SetRow(ElRect, col);
        }
    }

    private void Window_MouseMove(object sender, MouseEventArgs e)
    {
        RectRot.Angle = (RectRot.Angle >= 359) ? 0 : RectRot.Angle + 2;
    }
}