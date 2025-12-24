// Archivo: Models/ProductoWeb.cs
using System;

namespace BioFarmaWeb.Models
{
    [Serializable]
    public class ProductoWeb
    {
        public int ProductoID { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int CantidadEnStock { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public int CategoriaID { get; set; }
        public string ImagenUrl { get; set; }
    }
}
