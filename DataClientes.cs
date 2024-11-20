using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace PROYECTO_AUTOLAVADO_2
{
    internal class DataClientes
    {
        // FUNCIONES PRINCIPALES DE DATOS CLIENTES.
        // registrar, modificar, eliminar...
        #region
        public void AddCliente()
        {

            Cliente agregar = new Cliente();
            Console.WriteLine("Cedula: ");
            int cedula;
            if (!int.TryParse(Console.ReadLine(), out cedula))
            {
                Console.WriteLine("cedula no valida");
                return;
            }
            if (!SearchCedula(cedula))
            {
                // INFORMACION DE CLIENTE...
                int id = ++DataGlobal.ContadorClientes;
                Console.WriteLine("Id de membresia unico es: " + id);
                Console.WriteLine("Nombre: ");
                string nombre = Console.ReadLine() ?? throw new ArgumentNullException();
                Console.WriteLine("Apellido: ");
                string apellido = Console.ReadLine() ?? throw new ArgumentNullException();

                // INFORMACION DE VEHICULO...

                Console.ReadKey();
                Console.Clear();
                Console.WriteLine("Informacion de vehiculo:");
                Vehiculo Seleccion = new Vehiculo();    
                int eleccion;
                do
                {
                    Console.WriteLine("Tipo de vehiculo. \n(1). Auto.   (2). Camioneta. ");
                    eleccion = int.Parse(Console.ReadLine() ?? throw new ArgumentNullException());
                } while (eleccion < 1 || eleccion > 2);
                Seleccion.tipo = eleccion == 1 ? "Auto" : "Camioneta";
                Console.WriteLine("Tipo de vehiculo seleccionado: " + Seleccion.tipo + "\n");
                Console.WriteLine("Modelo: ");
                Seleccion.modelo = Console.ReadLine() ?? throw new ArgumentNullException();
                Console.WriteLine("Placa: ");
                Seleccion.placa = Console.ReadLine() ?? throw new ArgumentNullException();


                Cliente cliente = new Cliente { 
                    id = id, 
                    nombre = nombre, 
                    apellido = apellido, 
                    cedula = cedula.ToString(),
                    vehiculo = Seleccion
                };
                DataGlobal.PilaClientes?.Push(cliente);
                Console.WriteLine("Cliente agregado exitosamente.");;
            }
            else
            {
                Console.WriteLine("ya existe esta cedula...");
                Console.ReadKey();
                return;
            }
            Console.WriteLine("Presione cualquier tecla...");
            Console.ReadKey();
        }
        public void UpdateCliente()
        {
            Console.WriteLine("Ingrese el id asignado al cliente: ");
            int entrada = int.Parse(Console.ReadLine() ?? throw new ArgumentNullException());
            int indice = SearchId(entrada);
            if (indice == -1)
            {
                Console.WriteLine("Cliente no encontrado...");
                return;
            }
            Cliente? cliente = (Cliente?)DataGlobal.PilaClientes?.DatosPila[indice];

            if (cliente != null)
            {

            }
            cliente?.ShowInfoCliente();
            // Mostrar la información actual
            Console.WriteLine("Ingrese el nuevo nombre (dejar vacío para no cambiar): ");
            string? nuevoNombre = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nuevoNombre))
            {
                if (cliente != null)
                {
                    // Asignar el nuevo nombre
                    cliente.nombre = nuevoNombre;
                }
                else
                {
                    Console.WriteLine("El cliente no existe o no ha sido inicializado.");
                }
            }

            Console.WriteLine("Ingrese el nuevo apellido (dejar vacío para no cambiar): ");
            string? nuevoApellido = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nuevoApellido))
            {
                if (cliente != null)
                {
                    // Asignar el nuevo nombre
                    cliente.apellido = nuevoApellido;
                }
                else
                {
                    Console.WriteLine("El cliente no existe o no ha sido inicializado.");
                }
            }

            Console.WriteLine("Ingrese la nueva cédula (dejar vacío para no cambiar): ");
            string? nuevaCedula = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nuevaCedula))
            {
                if (cliente != null)
                {
                    // Asignar el nuevo nombre
                    cliente.cedula = nuevaCedula;
                }
                else
                {
                    Console.WriteLine("El cliente no existe o no ha sido inicializado.");
                }
            }

            Console.Clear();
            Vehiculo Seleccion = new Vehiculo();        
            Console.WriteLine("INFORMACION DE VEHICULO...");

            Console.WriteLine("Ingrese el nuevo tipo de carro (dejar vacío para no cambiar) \n " +
                "(1). Auto.     (2). Camioneta. ");
            string n = Console.ReadLine() ?? throw new ArgumentNullException();
            if (!string.IsNullOrWhiteSpace(n))
            {
                if(Convert.ToInt32(n) == 1)
                {
                    Seleccion.tipo = "Auto";

                }else if(Convert.ToInt32(n) == 2)
                {
                    Seleccion.tipo = "Camioneta";
                }
                else
                {
                    Console.WriteLine("Error, fuera de rango...");
                    return;
                }
            }

            Console.WriteLine("Ingrese el nuevo Modelo de carro (dejar vacío para no cambiar): ");
            string? nuevoModelo = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nuevoModelo))
            {
                Seleccion.modelo = nuevoModelo;
            }

            Console.WriteLine("Ingrese la nuevo placa de carro (dejar vacío para no cambiar): ");
            string? nuevaPlaca = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nuevaPlaca))
            {
                Seleccion.placa = nuevaPlaca;
            }

            if (cliente != null)
            {
                cliente.nombre = nuevoNombre;
            }
            else
            {
                Console.WriteLine("El cliente no existe o no ha sido inicializado.");
            }

            if (DataGlobal.PilaClientes?.DatosPila != null && indice >= 0 && indice < DataGlobal.PilaClientes.DatosPila.Length)
            {
                // Asignar el cliente al índice
                DataGlobal.PilaClientes.DatosPila[indice] = cliente;
            }
            else
            {
                Console.WriteLine("Error: DatosPila es nulo o el índice está fuera de rango.");
            }

            Console.WriteLine("Cliente actualizado exitosamente.");
            Console.ReadKey();
        }
        public void DeleteCliente()
        {
            Console.WriteLine("Ingrese el id asignado al cliente: ");
            int entrada = int.Parse(Console.ReadLine() ?? throw new ArgumentNullException());
            int indice = SearchId(entrada);
            if (indice == -1)
            {
                Console.WriteLine("Cliente no encontrado...");
                return;
            }
            Cliente cliente = (Cliente)DataGlobal.PilaClientes.DatosPila[indice];

            if (cliente.cita != null)
            {
                Console.WriteLine($"Para poder eliminar este cliente primero debe ser eliminado de la lista de espera de servicio '{cliente.id}'.");
                Console.WriteLine("Presione cualquier tecla.");
                return;

            }
            for (int i = indice; i < DataGlobal.PilaClientes?.Tope; i++)
            {
                DataGlobal.PilaClientes.DatosPila[i] = DataGlobal.PilaClientes.DatosPila[i + 1];
            }

            // Reducir el tope de la pila
            DataGlobal.PilaClientes.DatosPila[DataGlobal.PilaClientes.Tope] = 0; // Opcional: limpiar el espacio
            DataGlobal.PilaClientes.Tope--;

            Console.WriteLine("Cliente eliminado exitosamente.");
            Console.ReadKey();
        }
        public void ReadCliente()
        {
            Atencion Watch = new Atencion();
            Console.Clear();
            Console.WriteLine("Ingrese el id unico de cliente para buscar informacion: ");
            int id = int.Parse(Console.ReadLine() ?? throw new ArgumentNullException());
            int indice = SearchId(id);
            if(indice == -1)
            {
                Console.WriteLine("No existe ningun cliente con este id");
            }
            Cliente cliente = (Cliente)DataGlobal.PilaClientes.DatosPila[indice];
            cliente.ShowInfoCliente();
            Console.ReadKey();
        }
        public void ShowClientes()
        {
            Console.Clear();
            Console.WriteLine("INFORMACION GENERAL DE CLIENTES");
            for (int i = 0; i <= DataGlobal.PilaClientes?.Tope; i++)
            {
                Cliente cliente = (Cliente)DataGlobal.PilaClientes.DatosPila[i];
                cliente.ShowInfoCliente();
            }
            Console.ReadKey();
        }
        #endregion

        // FUNCIONES DE CONSULTA DE PILA
        // buscarxID, buscarxCedula, etc...
        #region 
        private bool SearchCedula(int cedula)
        {
            for (int i = 0; i <= DataGlobal.PilaClientes?.Tope; i++)
            {
                if (DataGlobal.PilaClientes.DatosPila[i] is Cliente cliente && cliente.cedula == cedula.ToString())
                {
                    return true;
                }
            }
            return false;
        }
        public int SearchId(int entrada)
        {
            for (int i = 0; i <= DataGlobal.PilaClientes?.Tope; i++)
            {
                if (DataGlobal.PilaClientes.DatosPila[i] is Cliente cliente && cliente.id == entrada)
                {
                    return i;
                }
            }
            return -1;
        }
        #endregion
    }
}
