using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROYECTO_AUTOLAVADO_2
{
    internal class Open
    {
        public void Home()
        {
            DataClientes Data = new DataClientes();
            Atencion Job = new Atencion();
            int op;
            do
            {
                Console.Clear();
                op = Menu();
                switch (op)
                {
                    case 1:
                        Data.AddCliente();
                        break;
                    case 2:
                        Data.UpdateCliente();
                        break;
                    case 3:
                        Data.DeleteCliente();
                        break;
                    case 4:
                        Job.DateCliente();
                        break;
                    case 5:
                        Job.AttendCliente();
                        break;
                    case 6:
                        Job.CancelDateCliente();
                        break;
                    case 7:
                        Job.ShowListClientes();
                        break;
                    case 8:
                        Data.ReadCliente();
                        break;
                    case 9:
                        Job.ShowFacturaCliente();
                        break;
                    case 11:
                        Data.ShowClientes();
                        break;


                }
            } while (op != 0);
        }
        private int Menu()
        {
            int op;
            do
            {
                Console.Clear();
                Console.WriteLine("-----------------MENU DE OPCIONES-----------------");
                Console.WriteLine("Ingrese una ocpion para:");
                Console.WriteLine("(1). Registrar cliente.");
                Console.WriteLine("(2). Modificar datos cliente.");
                Console.WriteLine("(3). Eliminar cliente.");
                Console.WriteLine("(4). Citar Cliente a un servicio.");
                Console.WriteLine("(5). Atender Cliente. ");
                Console.WriteLine("(6). Cancelar Cita Cliente.");
                Console.WriteLine("(7). Listar clientes por servicio.");
                Console.WriteLine("(8). Mostrar informacion de Cliente, Servicio agendado, y posicion.");
                Console.WriteLine("(9). Pagar y generar Factura de cliente.");
                Console.WriteLine("(0). SALIR...");

                op = Convert.ToInt32(Console.ReadLine());
                // efe si entra una letra xd
                if (op < 0 || op > 11)
                {
                    Console.WriteLine("ERROR: opción inválida. Presione cualquier tecla para continuar...");
                    Console.ReadKey();
                }
            } while (op < 0 || op > 11);
            return op;
        }
    }
}
