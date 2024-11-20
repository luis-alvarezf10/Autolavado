namespace PROYECTO_AUTOLAVADO_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Tasa Inspect = new Tasa();
            Inspect?.CargarValorDolar();
            DataGlobal.TasaBCV = Convert.ToSingle(Inspect.DolarBCV.ToString("F2"));
            Open open = new Open();
            open.Home();
        }
    }
}
