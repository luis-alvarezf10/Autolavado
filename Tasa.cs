using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using HtmlAgilityPack;
namespace PROYECTO_AUTOLAVADO_2
{
    internal class Tasa
    {
        public Decimal DolarBCV;
        public async Task<decimal> ObtenerValorDolarOficial()
        {
            string url = "https://www.bcv.org.ve/";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string pageContents = await response.Content.ReadAsStringAsync();

                var htmlDoc = new HtmlAgilityPack.HtmlDocument();
                htmlDoc.LoadHtml(pageContents);
                var dolarNode = htmlDoc.DocumentNode.SelectSingleNode("//div[@id='dolar']/div[@class='field-content']/div[@class='row recuadrotsmc']/div[@class='col-sm-6 col-xs-6 centrado']/strong");
                if (dolarNode != null)
                {
                    string dolarText = dolarNode.InnerText.Trim();
                    if (decimal.TryParse(dolarText, out decimal valorDolar))
                    {
                        return valorDolar;
                    }
                    else
                    {
                        throw new Exception("Error al convertir el valor del dolar a decimal.");
                    }
                }
                else
                {
                    throw new Exception("No se pudo encontrar el valor del dolar en la pagina");
                }
            }
        }
        public async Task CargarValorDolar()
        {
            try
            {
                // Cargar en segundo plano
                var valorDolarTask = ObtenerValorDolarOficial();
                decimal valorDolar = await valorDolarTask;
                DolarBCV = valorDolar;
            }
            catch (Exception ex)
            {
               Console.WriteLine(ex.Message);
            }
        }
    }
}
