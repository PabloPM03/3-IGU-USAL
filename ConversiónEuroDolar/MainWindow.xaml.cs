using System.Globalization;
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

namespace ConversiónEuroDolar
{



    public partial class MainWindow : Window
    {
        double tasa = 1.3;
        public MainWindow()
        {
            InitializeComponent();
            Dolar.KeyDown += NumericTextBox_KeyDown;
            Euro.KeyDown += NumericTextBox_KeyDown;
        }

        private void NumericTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (sender == Euro)
                {
                    Dolar.Text = (Euro.DoubleValue * 1.16).ToString();
                }
                else if (sender == Dolar)
                {
                    Euro.Text = (Dolar.DoubleValue / 1.3).ToString();
                }
            }
        }
    }
}

    