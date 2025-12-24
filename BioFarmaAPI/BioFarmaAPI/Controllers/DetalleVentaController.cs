using BioFarmaWeb.Models;
using BioFarmaWebApi.DAL;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace BioFarmaWebApi.Controllers
{
    public class DetalleVentaController : ApiController
    {
        BaseDeDatos db = new BaseDeDatos();

        // GET: api/DetalleVenta
        [HttpGet]
        public IEnumerable<DetalleVenta> Get()
        {
            return db.ObtenerDetallesVenta();
        }

        // POST: api/DetalleVenta
        public IHttpActionResult Post([FromBody] DetalleVenta detalle)
        {
            if (detalle == null)
                return BadRequest("Datos inválidos.");

            try
            {
                // llamamos el metodo correctamente desde DAL
                db.InsertarDetalleVenta(
                    detalle.VentaID,
                    detalle.ProductoID,
                    detalle.Cantidad,
                    detalle.PrecioUnitario
                );

                return Ok("Detalle registrado correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest("Error al registrar el detalle: " + ex.Message);
            }
        }
    }
}