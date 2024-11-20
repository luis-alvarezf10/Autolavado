using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROYECTO_AUTOLAVADO_2
{
    public class Vehiculo
    {
        public string? tipo {  get; set; }
        public string? modelo {  get; set; }
        public string? placa { get; set; }   
        public void ShowInfoVehiculo()
        {
            Console.WriteLine($"\tinformacion de carro. \n" +
                $"\t\tTipo: {tipo} \n" +
                $"\t\tModelo: {modelo} de placa: {placa}.\n");
        }
    }
}
