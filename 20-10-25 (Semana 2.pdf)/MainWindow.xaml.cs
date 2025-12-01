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

namespace _20_10_25__Semana_2.pdf_
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string strFormat = "{0,-30} {1,-15} {2,-15} {3,-15}";
        public MainWindow()
        {
            InitializeComponent();

            cabecera2.Text = string.Format(strFormat, "Evento", "Sender", "Fuente", "Fuente Original");

            UIElement[] elementos = new UIElement[] { this, grid, cabecera, cabecera2, boton, panel, textoBoton };
            foreach (UIElement i in elementos)
            {
                i.PreviewKeyDown += gestorglobal;
                i.PreviewKeyUp += gestorglobal;
                i.PreviewTextInput += gestorglobal;
                i.KeyDown += gestorglobal;
                i.KeyUp += gestorglobal;
                i.TextInput += gestorglobal;
                i.PreviewMouseDown += gestorglobal;
                i.PreviewMouseUp += gestorglobal;
                i.MouseDown += gestorglobal;
                i.MouseUp += gestorglobal;
                i.PreviewMouseMove += gestorglobal;
            }
            boton.Click += gestorglobal;
        }

        private void I_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        void gestorglobal(Object sender, RoutedEventArgs e)
        {
            TextBlock linea = new TextBlock();
            linea.Text = String.Format(strFormat,
            e.RoutedEvent.Name,
            nombreobjeto(sender),
            nombreobjeto(e.Source),
            nombreobjeto(e.OriginalSource));
            panel.Children.Add(linea);
            scrollViewer.ScrollToBottom();
        }
        string nombreobjeto(Object obj)
        {
            string[] parseada = obj.GetType().ToString().Split('.');
            return parseada[parseada.Length - 1];
        }
        protected override void OnPreviewKeyUp(KeyEventArgs e)
        {
            base.OnPreviewKeyUp(e);
            Console.WriteLine("OnPreviewKeyUp");
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            base.OnPreviewKeyDown(e);
            Console.WriteLine("OnPreviewKeyDown");
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            Console.WriteLine("OnKeyUp");
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            Console.WriteLine("OnKeyUp");
        }

    }
}