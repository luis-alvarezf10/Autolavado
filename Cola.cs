using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace PROYECTO_AUTOLAVADO_2
{
    public class Cola
    {
        public int Inicio;
        public int Fin;
        public int Cantidad;
        public int Capacidad;
        public object[] DatosCola;

        public Cola(int numero)
        {
            this.Capacidad = numero;
            Inicio = -1;
            Fin = -1;
            Cantidad = 0;
            DatosCola = new object[numero];
        }
        public bool ColaLlena()
        {
            return Cantidad == Capacidad;
        }
        public bool ColaVacia()
        {
            return Cantidad == 0;
        }
        public void Encolar(object Elemento)
        {
            if (ColaLlena())
            {
                Console.WriteLine("la cola esta llena...");
                return;
            }
            if (ColaVacia()) Inicio = 0;
            Fin = (Fin + 1) % Capacidad;
            DatosCola[Fin] = Elemento;
            Cantidad++;
        }
        public object? Desencolar()
        {
            if (ColaVacia())
            {
                Console.WriteLine("la cola esta vacia...");
                return null;
            }
            else
            {
                object Elemento = DatosCola[Inicio];
                if (Inicio == Fin)
                {
                    Inicio = -1;
                    Fin = -1;
                }
                else
                {
                    Inicio = (Inicio + 1) % Capacidad;
                }
                Cantidad--;
                return Elemento;
            }
        }
        public void Mostrar()
        {
            if (Inicio == -1)
            {
                Console.WriteLine("La cola está vacía.");
                return;
            }

            Console.WriteLine("Contenido de la cola:");
            int i = Inicio;
            while (true)
            {
                Console.Write(DatosCola[i] + " ");
                if (i == Fin) // Último elemento
                    break;

                i = (i + 1) % Capacidad; // Incrementar de forma circular
            }
            Console.WriteLine();
        }
    }
}
