using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROYECTO_AUTOLAVADO_2
{
    public static class DataGlobal
    {
        const int DataSize = 100;
        public static Pila? PilaClientes { get; set; } = new Pila(DataSize);
        public static int ContadorClientes { get; set; }
        public static Cola? ColaLavado { get; set; } = new Cola(10);
        public static Cola? ColaAceite { get; set; } = new Cola(5);
        public static Cola? ColaBalanceo { get; set; } = new Cola(5);
        public static float TasaBCV { get; set; }
    }
}
