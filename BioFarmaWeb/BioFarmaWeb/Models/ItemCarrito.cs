using System;
using BioFarmaWeb.Models; // Para que reconozca ProductoWeb

namespace BioFarmaWeb.Models
{
    [Serializable]
    public class ItemCarrito
    {
        public ProductoWeb Producto { get; set; }
        public int Cantidad { get; set; }
    }
}