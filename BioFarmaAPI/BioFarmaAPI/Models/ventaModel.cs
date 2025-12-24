using System.Collections.Generic;

namespace BioFarmaWeb.Models
{
    public class ProductoCompra
    {
        public int ProductoID { get; set; }
        public int Cantidad { get; set; }
    }

    public class VentaModel
    {
        public int ClienteID { get; set; }
        public string DireccionEntrega { get; set; }
        public List<ProductoCompra> Productos { get; set; }
    }
}
