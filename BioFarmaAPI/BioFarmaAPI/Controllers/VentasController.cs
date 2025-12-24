using System;
using System.Data.SqlClient;
using System.Web.Http;
using BioFarmaWeb.Models;
using BioFarmaWebApi.DAL;

namespace BioFarmaWeb.Controllers
{
    public class VentasController : ApiController
    {
        BaseDeDatos db = new BaseDeDatos();

        [HttpPost]
        [Route("api/ventas")]
        public IHttpActionResult CrearVenta(VentaModel venta)
        {
            if (venta == null || venta.Productos == null || venta.Productos.Count == 0)
                return BadRequest("Datos de venta inválidos");

            try
            {
                //  Insertar la venta
                string queryVenta = "INSERT INTO Ventas (FechaVenta, ClienteID, TotalVenta, DireccionEntrega) " +
                                    "VALUES (GETDATE(), @ClienteID, 0, @DireccionEntrega); SELECT SCOPE_IDENTITY();";

                int ventaID = Convert.ToInt32(db.EjecutarConsulta(queryVenta,
                    new SqlParameter("@ClienteID", venta.ClienteID),
                    new SqlParameter("@DireccionEntrega", venta.DireccionEntrega)).Rows[0][0]);

                decimal totalVenta = 0;

                //  Insertar productos en DetalleVenta y actualizar stock
                foreach (var p in venta.Productos)
                {
                    // Obtener precio del producto
                    string queryPrecio = "SELECT Precio, CantidadEnStock FROM Productos WHERE ProductoID=@ProductoID";
                    var dtProd = db.EjecutarConsulta(queryPrecio, new SqlParameter("@ProductoID", p.ProductoID));

                    if (dtProd.Rows.Count == 0)
                        return BadRequest("Producto no encontrado: " + p.ProductoID);

                    int stock = Convert.ToInt32(dtProd.Rows[0]["CantidadEnStock"]);
                    decimal precio = Convert.ToDecimal(dtProd.Rows[0]["Precio"]);

                    if (p.Cantidad > stock)
                        return BadRequest($"No hay suficiente stock para el producto {p.ProductoID}");

                    // Insertar detalleVenta
                    string queryDetalle = "INSERT INTO DetalleVenta (VentaID, ProductoID, Cantidad, PrecioUnitario) " +
                                          "VALUES (@VentaID, @ProductoID, @Cantidad, @PrecioUnitario)";
                    db.EjecutarComando(queryDetalle,
                        new SqlParameter("@VentaID", ventaID),
                        new SqlParameter("@ProductoID", p.ProductoID),
                        new SqlParameter("@Cantidad", p.Cantidad),
                        new SqlParameter("@PrecioUnitario", precio));

                    // Actualiza el stock
                    string queryStock = "UPDATE Productos SET CantidadEnStock = CantidadEnStock - @Cantidad WHERE ProductoID=@ProductoID";
                    db.EjecutarComando(queryStock,
                        new SqlParameter("@Cantidad", p.Cantidad),
                        new SqlParameter("@ProductoID", p.ProductoID));

                    totalVenta += precio * p.Cantidad;
                }

                // Actualiza el total de la venta
                string queryTotal = "UPDATE Ventas SET TotalVenta=@Total WHERE VentaID=@VentaID";
                db.EjecutarComando(queryTotal,
                    new SqlParameter("@Total", totalVenta),
                    new SqlParameter("@VentaID", ventaID));

                return Ok(new { Exito = true, VentaID = ventaID });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpGet]
        [Route("api/ventas")]
        public IHttpActionResult ObtenerVentas()
        {
            try
            {
                string query = @"SELECT VentaID, FechaVenta, ClienteID, TotalVenta, DireccionEntrega 
                         FROM Ventas ORDER BY VentaID DESC";

                var dt = db.EjecutarConsulta(query);

                return Ok(dt);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
