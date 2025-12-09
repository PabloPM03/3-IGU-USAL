using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Restaurante
{
    public class DatosRestaurante
    {
        public static DatosRestaurante datos { get; } = new DatosRestaurante();

        public ObservableCollection<Mesa> listaMesas { get; set; }
        public List<Plato> listaPlatosPrimeros { get; set; }
        public List<Plato> listaPlatosSegundos {  get; set; }
        public List<Plato> listaPlatosPostres {  get; set; }
        public List<Plato> listaPlatosSobremesas { get; set; }
        public Array tiposFormas
        {
            get { return Enum.GetValues(typeof(FormaMesa)); }
        }

        public event Action<Mesa> cambioSeleccionMesa;
        private Mesa _mesaSeleccionada;
        public Mesa mesaSeleccionada
        {
            get {  return _mesaSeleccionada; }
            set 
            {
                if (_mesaSeleccionada == value) return;
                _mesaSeleccionada = value; 
                cambioSeleccionMesa?.Invoke(_mesaSeleccionada); 
                OnPropertyChanged(); 
            }
        }

        private DatosRestaurante()
        {
            listaMesas = new ObservableCollection<Mesa>();
            listaPlatosPrimeros = new List<Plato>();
            listaPlatosSegundos = new List<Plato>();
            listaPlatosPostres = new List<Plato>();
            listaPlatosSobremesas = new List<Plato>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }




        public void inicializarDatos(Boolean sesionLimpia)
        {
            listaMesas.Clear();
            cargarMesasPrueba(sesionLimpia);
            cargarCartaPrueba();
            cargarComandasPrueba();
        }

        //----------------------------------------------//
        //---------- BANCO DE DATOS DE PRUEBA ----------//
        //----------------------------------------------//
        public void cargarMesasPrueba(bool sesionLimpia)
        {
            //---------- DATOS DE LAS MESAS ----------//
            int[] posicionesX = { 13, 13, 13, 13, 13, 9, 11, 10, 9, 9,
                9, 7, 5, 3, 3, 5, 7, 9, 7, 5 };
            int[] posicionesY = { 15, 13, 9, 7, 5, 3, 5, 9, 12, 14,
                16, 2, 2, 3, 7, 9, 9, 6, 7, 5 };
            int[] spanX = { 1, 2, 1, 1, 1, 3, 1, 1, 2, 2,
                2, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            int[] spanY = { 3, 1, 3, 1, 1, 1, 1, 1, 1, 1,
                1, 2, 2, 3, 3, 2, 2, 1, 1, 1 };
            int[] comensales = { 7, 0, 6, 2, 0, 7, 0, 1, 0, 4,
                3, 0, 5, 0, 7, 4, 0, 0, 2, 1 };
            int[] capacidades = { 8, 5, 8, 2, 2, 8, 2, 2, 5, 5,
                5, 5, 5, 8, 8, 5, 5, 2, 2, 2 };
            EstadoMesa[] estados = {EstadoMesa.Ocupada, EstadoMesa.Libre, EstadoMesa.Reservada, EstadoMesa.OcupadaComanda, EstadoMesa.Libre,
                EstadoMesa.OcupadaComanda, EstadoMesa.Libre, EstadoMesa.OcupadaComanda, EstadoMesa.Libre, EstadoMesa.OcupadaComanda,
                EstadoMesa.Reservada, EstadoMesa.OcupadaComanda, EstadoMesa.Ocupada, EstadoMesa.Libre, EstadoMesa.OcupadaComanda,
                EstadoMesa.OcupadaComanda, EstadoMesa.Libre, EstadoMesa.Libre, EstadoMesa.Ocupada, EstadoMesa.OcupadaComanda };
            FormaMesa[] formas = { FormaMesa.Elipse, FormaMesa.Rectangular, FormaMesa.Elipse, FormaMesa.Rectangular, FormaMesa.Elipse,
                FormaMesa.Rectangular, FormaMesa.Elipse, FormaMesa.Rectangular, FormaMesa.Rectangular, FormaMesa.Rectangular,
                FormaMesa.Elipse, FormaMesa.Elipse, FormaMesa.Rectangular, FormaMesa.Rectangular, FormaMesa.Rectangular,
                FormaMesa.Rectangular, FormaMesa.Elipse, FormaMesa.Rectangular, FormaMesa.Elipse, FormaMesa.Elipse, };

            int i = 0;
            //---------- CREACIÓN DE MESAS ----------//
            for (i = 0; i < 20; i++)
            {
                Mesa m = new Mesa();
                m.posicionX = posicionesX[i];
                m.posicionY = posicionesY[i];
                m.spanX = spanX[i];
                m.spanY = spanY[i];
                Grid gridMesa;

                if (sesionLimpia)
                {
                    m.comensales = 0;
                    gridMesa = m.nuevaMesa(i+1, 0, capacidades[i], EstadoMesa.Libre, formas[i]);
                }
                else
                {
                    m.comensales = comensales[i];
                    gridMesa = m.nuevaMesa(i+1, comensales[i], capacidades[i], estados[i], formas[i]);
                }

                m.gridMesaRepresentada = gridMesa;

                listaMesas.Add(m);
            }
        }
        public void cargarCartaPrueba()
        {
            // --- PRIMEROS (5 Platos) ---
            listaPlatosPrimeros.Add(new Plato(11, "Ensalada César Imperial", "Lechuga romana crujiente, crutones de ajo, parmesano reggiano y nuestra salsa secreta.", 9.50, TipoPlato.Primero));
            listaPlatosPrimeros.Add(new Plato(12, "Crema de Calabaza y Jengibre", "Suave crema de calabaza asada con un toque picante de jengibre y semillas de sésamo.", 8.00, TipoPlato.Primero));
            listaPlatosPrimeros.Add(new Plato(13, "Risotto de Setas Silvestres", "Arroz arborio cremoso cocinado con boletus, trufa negra y terminación de mantequilla.", 12.50, TipoPlato.Primero));
            listaPlatosPrimeros.Add(new Plato(14, "Gazpacho Andaluz Tradicional", "Sopa fría de tomate, pimiento y pepino con guarnición de jamón ibérico y huevo.", 7.50, TipoPlato.Primero));
            listaPlatosPrimeros.Add(new Plato(15, "Parrillada de Verduras", "Selección de verduras de temporada a la brasa con aceite de albahaca y sal maldon.", 10.00, TipoPlato.Primero));

            // --- SEGUNDOS (5 Platos) ---
            listaPlatosSegundos.Add(new Plato(21, "Entrecot de Ternera (300g)", "Corte de lomo bajo a la parrilla con pimientos de padrón y patatas gajo.", 22.00, TipoPlato.Segundo));
            listaPlatosSegundos.Add(new Plato(22, "Salmón Noruego al Horno", "Lomo de salmón glaseado con miel y mostaza sobre cama de espárragos trigueros.", 18.50, TipoPlato.Segundo));
            listaPlatosSegundos.Add(new Plato(23, "Carrillada Ibérica al Vino Tinto", "Carne tierna estofada a baja temperatura durante 12 horas con puré de patata trufado.", 16.00, TipoPlato.Segundo));
            listaPlatosSegundos.Add(new Plato(24, "Lubina a la Espalda", "Pescado fresco abierto a la plancha con refrito de ajos, guindilla y vinagre de Jerez.", 19.00, TipoPlato.Segundo));
            listaPlatosSegundos.Add(new Plato(25, "Lasaña de Carne a la Boloñesa", "Capas de pasta fresca, ragú de ternera y cerdo, bechamel suave y gratinado de mozzarella.", 14.50, TipoPlato.Segundo));

            // --- POSTRES (5 Platos) ---
            listaPlatosPostres.Add(new Plato(31, "Coulant de Chocolate", "Bizcocho caliente con corazón de chocolate fundido, acompañado de helado de vainilla.", 6.50, TipoPlato.Postre));
            listaPlatosPostres.Add(new Plato(32, "Tarta de Queso Casera", "Estilo New York Cheesecake con base de galleta y mermelada de frutos rojos silvestres.", 6.00, TipoPlato.Postre));
            listaPlatosPostres.Add(new Plato(33, "Tiramisú Clásico", "Capas de bizcocho soletilla empapados en café espresso y crema de mascarpone con cacao.", 5.50, TipoPlato.Postre));
            listaPlatosPostres.Add(new Plato(34, "Sorbete de Limón al Cava", "Refrescante batido de helado de limón con un toque de cava brut nature.", 5.00, TipoPlato.Postre));
            listaPlatosPostres.Add(new Plato(35, "Brocheta de Frutas de Temporada", "Selección de frutas frescas cortadas con baño de chocolate negro caliente.", 4.50, TipoPlato.Postre));

            // --- SOBREMESAS / BEBIDAS ESPECIALES (5 Platos) ---
            listaPlatosSobremesas.Add(new Plato(41, "Café Irlandés Especial", "Café espresso, whisky irlandés, azúcar moreno y capa de nata montada.", 7.00, TipoPlato.Sobremesa));
            listaPlatosSobremesas.Add(new Plato(42, "Gin Tonic Premium", "Ginebra de autor con tónica fever-tree, cardamomo y twist de lima.", 9.00, TipoPlato.Sobremesa));
            listaPlatosSobremesas.Add(new Plato(43, "Mojito Cubano", "Ron blanco, hierbabuena fresca, lima, azúcar y soda con mucho hielo picado.", 8.50, TipoPlato.Sobremesa));
            listaPlatosSobremesas.Add(new Plato(44, "Té Matcha Latte", "Té verde japonés en polvo batido con leche espumada y un toque de miel.", 4.00, TipoPlato.Sobremesa));
            listaPlatosSobremesas.Add(new Plato(45, "Chupito de Hierbas", "Licor digestivo tradicional de hierbas maceradas, servido muy frío.", 3.00, TipoPlato.Sobremesa));
        }
        public void cargarComandasPrueba()
        {
            Random r = new Random();
            foreach (Mesa m in listaMesas)
            {
                if (m.historialPlatos == null) m.historialPlatos = new ObservableCollection<Plato>();
                if (m.estado == EstadoMesa.OcupadaComanda)
                {
                    if (m.comanda == null)
                    {
                        m.comanda = new ObservableCollection<Plato>();
                    }
                    else
                    {
                        m.comanda.Clear();
                    }
                    int numPlatos = r.Next(0, 3);
                    for (int i = 0; i < m.comensales * numPlatos; i++)
                    {
                        int tipo = r.Next(0, 4);
                        List<Plato> listaOrigen = null;
                        switch (tipo)
                        {
                            case 0: listaOrigen = listaPlatosPrimeros; break;
                            case 1: listaOrigen = listaPlatosSegundos; break;
                            case 2: listaOrigen = listaPlatosPostres; break;
                            case 3: listaOrigen = listaPlatosSobremesas; break;

                        }

                        if (listaOrigen != null && listaOrigen.Count > 0)
                        {
                            int id = r.Next(0, listaOrigen.Count);

                            Plato platoElegido = listaOrigen[id];
                            m.comanda.Add(platoElegido);
                            m.historialPlatos.Add(platoElegido);
                        }
                    }
                }
            }
        }
    }
}
