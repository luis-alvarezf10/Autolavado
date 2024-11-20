using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PROYECTO_AUTOLAVADO_2
{
    internal class Atencion
    {
        private string[] ServiciosAlmacenado = {"Lavado Completo", "Cambio de Aceite", "Balanceo"};
        public void DateCliente()
        {
            DataClientes Data = new DataClientes();
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("Ingrese ID unico de membresia: ");
            int id = int.Parse(Console.ReadLine() ?? throw new ArgumentNullException());
            int indice = Data.SearchId(id);
            if (indice == -1)
            {
                Console.WriteLine("No se encontró este cliente en la base de datos...");
                Console.WriteLine("Presione cualquier tecla.");
                Console.ReadKey();
            }

            Console.Clear();
            Cliente? cliente = (Cliente?)DataGlobal.PilaClientes?.DatosPila[indice];
            Console.WriteLine("MOSTRANDO...");
            cliente?.ShowInfoCliente();
            if(cliente.cita == null)
            {
                string? V = cliente.vehiculo?.tipo;
                int indiceServicios = MenuServicios();
                int cantidad = 0;
                string? NombreServicio = ServiciosAlmacenado[indiceServicios];
                float precio = 0;
                switch (NombreServicio)
                {
                    // creo que lo pude hacer con una funcion no importa
                    case "Lavado Completo":
                        if (!DataGlobal.ColaLavado.ColaLlena())
                        {
                            precio = V == "Auto" ? 14.0f : 21.0f;
                            cliente.cita = NombreServicio;
                            DataGlobal.ColaLavado.Encolar(cliente);
                            cantidad = DataGlobal.ColaLavado.Cantidad;
                        }
                        else {
                            message();
                        }
                        break;
                    case "Cambio de Aceite":
                        if (!DataGlobal.ColaAceite.ColaLlena())
                        {
                            precio = V == "Auto" ? 15.0f : 20.0f;
                            cliente.cita = NombreServicio;
                            DataGlobal.ColaAceite.Encolar(cliente);
                            cantidad = DataGlobal.ColaAceite.Cantidad;
                        }
                        else
                        {
                            message();
                        }
                        break;
                    case "Balanceo":
                        if (!DataGlobal.ColaBalanceo.ColaLlena())
                        {
                            precio = V == "Auto" ? 25.0f : 35.0f;
                            cliente.cita = NombreServicio;
                            DataGlobal.ColaBalanceo.Encolar(cliente);
                            cantidad = DataGlobal.ColaBalanceo.Cantidad;
                        }
                        else
                        {
                            message();
                        }
                        break;
                }
                cliente.posicion = cantidad;
                Console.WriteLine("Proceso de cita completado...");
            }
            else
            {
                Console.WriteLine("Ya este cliente tiene una cita agendada...");
            }
            Console.ReadKey();
        }
        public void AttendCliente()
        {
            Console.Clear();
            Console.WriteLine("Ingrese Que servicio desea trabajar.");
            int op = MenuServicios();
            string NombreServicio = ServiciosAlmacenado[op];
            switch (NombreServicio)
            {
                // creo que lo pude hacer con una funcion no importa
                case "Lavado Completo":
                    MoveCliente(DataGlobal.ColaLavado, NombreServicio);
                    break;
                case "Cambio de Aceite":
                    MoveCliente(DataGlobal.ColaAceite, NombreServicio);
                    break;
                case "Balanceo":
                    MoveCliente(DataGlobal.ColaBalanceo, NombreServicio);
                    break;
            }
        }
        public void CancelDateCliente()
        {
            Console.Clear();
            Console.WriteLine("Proceso de cancelar cita. ");
            int op = MenuServicios();
            string? NombreServicio = ServiciosAlmacenado[op];
            Console.WriteLine("Ingrese el id unico de cliente: ");
            int id = int.Parse(Console.ReadLine() ?? throw new ArgumentNullException());
            switch (NombreServicio)
            {
                case "Lavado Completo":
                    DataGlobal.ColaLavado = EraseDateCliente(DataGlobal.ColaLavado,id);
                    break;
                case "Cambio de Aceite":
                    DataGlobal.ColaAceite = EraseDateCliente(DataGlobal.ColaAceite, id);
                    break;
                case "Balanceo":
                    DataGlobal.ColaBalanceo = EraseDateCliente(DataGlobal.ColaBalanceo, id);
                    break;
                default:
                    Console.WriteLine("Posiblemente ha ingresado un servicio adicional");
                    return;
            }
            

        }
        public void ShowListClientes()
        {
            Console.Clear();
            Console.WriteLine("Proceso de mostra colas de espera por servicio. ");
            int op = MenuServicios();
            string? NombreServicio = ServiciosAlmacenado[op];
            switch (NombreServicio)
            {
                case "Lavado Completo":
                    ShowCola(DataGlobal.ColaLavado, NombreServicio);
                    break;
                case "Cambio de Aceite":
                    ShowCola(DataGlobal.ColaAceite, NombreServicio);
                    break;
                case "Balanceo":
                    ShowCola(DataGlobal.ColaBalanceo, NombreServicio);
                    break;
                default:
                    Console.WriteLine("Posiblemente ha ingresado un servicio adicional," +
                        " sin antes aplicar la funcionalidad del programa...");
                    Console.ReadKey();
                    return;
            }
            Console.ReadKey();
        }
        public void ShowFacturaCliente()
        {
            DataClientes Data = new DataClientes();
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("Ingrese ID unico de membresia: ");
            int id = int.Parse(Console.ReadLine() ?? throw new ArgumentNullException());
            int indice = Data.SearchId(id);
            if (indice == -1)
            {
                Console.WriteLine("No se encontró este cliente en la base de datos...");
                Console.WriteLine("Presione cualquier tecla.");
                Console.ReadKey();
            }

            Console.Clear();
            Cliente? cliente = (Cliente?)DataGlobal.PilaClientes?.DatosPila[indice];
            Console.WriteLine("MOSTRANDO...");
            cliente?.ShowInfoCliente();
            if(cliente.cita != null)
            {
                Console.WriteLine("Antes de solicitar Factura, primero debe haber salido del servicio en que se encuentra.");
                Console.ReadKey();
                return;
            }
            cliente.ShowFactura();
        }
        private int MenuServicios()
        {
            int eleccion;
            Console.WriteLine("Ingrese que servicio desea solicitar.");
            do
            {
                Console.WriteLine("OPCIONES.");
                for(int i = 0; i < ServiciosAlmacenado.Length; i++)
                {
                    Console.WriteLine($"({i + 1}). {ServiciosAlmacenado[i]}.");
                }
                eleccion = int.Parse(Console.ReadLine() ?? throw new ArgumentNullException());
                if (eleccion < 1 || eleccion > ServiciosAlmacenado.Length) Console.WriteLine("Error. Fuera de rango");
            } while (eleccion < 1 || eleccion > ServiciosAlmacenado.Length);
            return eleccion - 1;
        }
        private void message()
        {
            Console.WriteLine("Error de cola");
            Console.WriteLine("Presione cualquier tecla...");
            Console.ReadKey();
            return;
        }
        private void ShowCola(Cola cola, string servicio)
        {
            if (cola.ColaVacia())
            {
                Console.WriteLine("La cola está vacía.");
                return;
            }

            Console.WriteLine($"Contenido de la cola {servicio}:");
            for (int i = cola.Inicio; ; i = (i + 1) % cola.Capacidad)
            {
                // Obtener el cliente actual
                Cliente? cliente = (Cliente?)cola.DatosCola[i];
                if (cliente != null)
                {
                    cliente.ShowInfoCliente(); // Mostrar la información del cliente
                }

                // Detener el ciclo cuando se haya procesado el último elemento
                if (i == cola.Fin)
                    break;
            }
        }
        private void MoveCliente(Cola cola, string NombreServicio)
        {
            float precio = 0;
            Cliente cliente = (Cliente)cola.DatosCola[cola.Inicio];
            Console.WriteLine("Mostrando informacion de primer cliente de la cola: " + NombreServicio);
            cliente.ShowInfoCliente();
            Console.WriteLine("Presione cualquier tecla");
            Console.ReadKey();
            if (!cola.ColaVacia())
            {
                cola.Desencolar();
                if(NombreServicio == "Balanceo")
                {
                    ProcessBalanceo();
                }
            }
            else
            {
                message();
            }
            string? V = cliente.vehiculo.tipo;
            switch (cliente.cita)
            {
                case "Lavado Completo":
                    precio = V == "Auto" ? 14.0f : 21.0f;
                    break;
                case "Cambio de Aceite":
                    precio = V == "Auto" ? 15.0f : 20.0f;
                    break;
                case "Balanceo":
                    precio = V == "Auto" ? 25.0f : 35.0f;
                    break;

            }
            cliente.cita = null;
            Console.WriteLine("Cliente atentido.");
            // agrega servicio a la factura jejeje
            Servicio servicio = new Servicio();
            servicio.nombre = NombreServicio;
            servicio.precio = precio;
            cliente.InsertServicio(servicio);
            Console.ReadKey();
        }
        private void ProcessBalanceo()
        {
            Pila PilaCaucho = new Pila(4);
            Console.WriteLine("Procesando Balanceo de cauchos.");
            for(int i = 0; i < 4; i++)
            {
                PilaCaucho.Push(i + 1);
            }
            int c = 0;
            while (!PilaCaucho.Pilavacia())
            {
                Console.WriteLine($"Balanceando caucho {c + 1}, (Presione cualquier tecla cuando este listo)");
                PilaCaucho.Pop();
                Console.ReadKey();
                c++;
            }
            Console.WriteLine("Balanceo completado exitosamente");
            Console.ReadKey();

        }
        private Cola EraseDateCliente(Cola cola, int id)
        {
            Console.ReadKey();
            Cola colaAux = new(cola.Capacidad);
            Cliente cliente;
            bool eliminado = false;
            while (!cola.ColaVacia())
            {
                cliente = (Cliente)cola.Desencolar();
                if(cliente.id != id)
                {
                    if (cliente.posicion != 1) cliente.posicion--;
                    colaAux.Encolar(cliente);
                }
                else
                {
                    eliminado = true;
                    cliente.cita = null;
                    cliente.posicion = 0;
                }
            }
            if(eliminado)
            {
                Console.WriteLine("Cliente ya eliminado");
            }
            else
            {
                Console.WriteLine("Cliente no encontrado.");
            }
            Console.ReadKey();
            return colaAux;
        }
    }
}
