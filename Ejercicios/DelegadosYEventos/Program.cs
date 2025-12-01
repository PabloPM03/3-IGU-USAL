using System;

namespace ClasesAcopladas
{
    //delegate void aviso(object sender, ProcesandoEventArgs e);

    class ProcesandoEventArgs : EventArgs
    {
        public int x {  get; set; }

        public ProcesandoEventArgs(int x)
        {
            this.x = x;
        }
    }
    class Acopladas
    {
        static void Main(string[] args)
        {
            Observador obj = new Observador();
            obj.funciona();
        }
    }
    class Observador
    {
        TrabajoDuro tb;
        public Observador() { 
            tb = new TrabajoDuro();
            tb.informe += InformeAvance;
            tb.informe += InformeAvance2;
            tb.informe -= InformeAvance2;
        }
        public void funciona()
        {
            Console.WriteLine("Vamos a probar el informe");
            tb.ATrabajar();
            Console.WriteLine("Terminado");
        }
        public void InformeAvance(Object sender, ProcesandoEventArgs e)
        {
            string str = String.Format("Ya llevamos el {0}", e.x);
            Console.WriteLine(str);
        }
        public void InformeAvance2(Object sender, ProcesandoEventArgs e)
        {
            Console.WriteLine("*****");
        }
    }
    class TrabajoDuro
    {
        int PocentajeHecho;
        //public event aviso informe;
        public event EventHandler<ProcesandoEventArgs> informe;

        public TrabajoDuro()
        {
            PocentajeHecho = 0;
        }

        void OnProcesando(int dato)
        {
            informe?.Invoke(this, new ProcesandoEventArgs(dato));
        }
        public void ATrabajar()
        {
            int i;
            for (i = 0; i < 500; i++)
            {
                System.Threading.Thread.Sleep(1); //Hacemos el trabajo
                switch (i)
                {
                    case 125:
                        PocentajeHecho = 25;
                        if (informe != null)
                        {
                            OnProcesando(PocentajeHecho);
                        }
                        break;
                    case 250:
                        PocentajeHecho = 50;
                        if (informe != null)
                        {
                            OnProcesando(PocentajeHecho);
                        }
                        break;
                    case 375:
                        PocentajeHecho = 75;
                        if (informe != null)
                        {
                            OnProcesando(PocentajeHecho);
                        }
                        break;
                }
            }
        }
    }
}