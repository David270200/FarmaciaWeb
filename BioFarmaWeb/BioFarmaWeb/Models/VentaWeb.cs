using System;

namespace BioFarmaWeb.Models
{
    public class VentaWeb
    {
        public int VentaID { get; set; }
        public int ProductoID { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }
        public DateTime Fecha { get; set; }
    }
}
