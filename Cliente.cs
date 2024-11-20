using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROYECTO_AUTOLAVADO_2
{
    public class Cliente
    {
        public int id { get; set;}
        public string? nombre { get; set;}
        public string? apellido {  get; set;}
        public string? cedula { get; set;}
        public Vehiculo? vehiculo { get; set;}
        public string? cita {  get; set;}
        public int posicion { get; set;}
        public static Pila? factura { get; set; } = new Pila(10);
        public void ShowInfoCliente()
        {
            Console.WriteLine("Informacion general \n ");
            Console.WriteLine($"\tId unico de membresia: {id}. \n" +
                $"\tNombre Completo: {nombre} {apellido}. \n" +
                $"\tCedula: {cedula}.");
            vehiculo?.ShowInfoVehiculo();
            if(cita != null)
            {
                Console.WriteLine("\tCita actual solicitada '" + cita + "' " +
                    "en la posicion ("+ posicion +").\n");
            }
            else
            {
                Console.WriteLine("\tNo posee cita actualmente.");
            }
        }
        public void InsertServicio(Servicio servicio)
        {
            if (factura != null)
            {
                if (!factura.Pilallena())
                {
                    factura.Push(servicio);
                }
                else
                {
                    Console.WriteLine("HOLA");
                }
            }
        }
        public void ShowFactura()
        {
            Console.WriteLine($"Cliente: {nombre} {apellido}    Cedula: {cedula}" +
                $"    Placa: {vehiculo?.placa}      Vehiculo: {vehiculo?.modelo}");
            Console.WriteLine($"\n");
            int cont = 0;
            float TotalDivisas = 0;
            float TotalBolivares = 0;
            while(!factura.Pilavacia())
            {
                Servicio servicio = (Servicio)factura.DatosPila[factura.Tope];
                Console.WriteLine($"Item: {++cont}    {servicio.nombre}    {servicio.precio} {DataGlobal.TasaBCV * servicio.precio}");
                TotalDivisas += servicio.precio;
                TotalBolivares += (DataGlobal.TasaBCV * servicio.precio);
                factura.Pop();
            }
            Console.WriteLine($"Total Divisas: {TotalDivisas}   Total Bolivares (BCV): {TotalBolivares}");
            factura.LimpiarPila();
            Console.ReadKey();
        }
    }
}
