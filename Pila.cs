using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROYECTO_AUTOLAVADO_2
{
    public class Pila
    {
        public int Tope;
        private int Cantidad;
        private int Capacidad;
        public object[] DatosPila;

        public Pila(int numero)
        {
            this.Capacidad = numero;
            Tope = -1;
            Cantidad = 0;
            DatosPila = new object[numero];
        }

        public bool Pilavacia()
        {
            return Tope == -1;
        }
        public bool Pilallena()
        {
            return Tope == Capacidad - 1;
        }
        public void Push(object Elemento)
        {
            if (!Pilallena())
            {
                DatosPila[++Tope] = Elemento;
                Cantidad++;
            }
            else
            {
                Console.WriteLine("pila llena...");
                return;
            }
        }
        public object? Pop()
        {
            if (!Pilavacia())
            {
                object x = DatosPila[Tope];
                Tope--;
                Cantidad--;
                return x;
            }
            else
            {
                Console.WriteLine("pila vacia...");
                return null;
            }
        }
        public object Peek()
        {
            return DatosPila[Tope];
        }
        public void MostrarPila()
        {
            if (Pilavacia())
            {
                Console.WriteLine("La pila esta vacia");
                return;
            }
            Console.WriteLine("Elementos de la pila");
            for (int i = 0; i <= Tope; i++)
            {
                Console.WriteLine(DatosPila[i]);
            }
        }
        public void LimpiarPila()
        {
            Tope = -1;
            Cantidad = 0;
            // Base = -1;
        }
        public object RecogerTope()
        {
            return DatosPila[Tope];
        }
    }
}
