using BioFarmaWebApi.DAL;
using BioFarmaWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;

namespace BioFarmaWebApi.Controllers
{
    public class ProductosController : ApiController
    {
        BaseDeDatos db = new BaseDeDatos();

       
        // GET: api/Productos
       
        [HttpGet]
        public IHttpActionResult Get()
        {
            string query = "SELECT * FROM Productos";
            DataTable tabla = db.EjecutarConsulta(query);

            List<Producto> lista = new List<Producto>();

            foreach (DataRow row in tabla.Rows)
            {
                lista.Add(MapearFila(row));
            }

            return Json(lista); //toma la lista y la convierte a formato JSON
        }

        
        // GET: api/Productos/5
       
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            string query = "SELECT * FROM Productos WHERE ProductoID = @id";

            DataTable tabla = db.EjecutarConsulta(query,
                new SqlParameter("@id", id));

            if (tabla.Rows.Count == 0)
                return NotFound();

            return Json(MapearFila(tabla.Rows[0]));
        }

        
        // POST: api/Productos
      
        [HttpPost]
        public IHttpActionResult Post([FromBody] Producto producto)
        {
            string query = @"INSERT INTO Productos 
                            (Nombre, Descripcion, Precio, CantidadEnStock, FechaVencimiento, CategoriaID, ImagenUrl)
                             VALUES (@Nombre, @Descripcion, @Precio, @CantidadEnStock, @FechaVencimiento, @CategoriaID, @ImagenUrl)";

            int filas = db.EjecutarComando(query,
                new SqlParameter("@Nombre", producto.Nombre),
                new SqlParameter("@Descripcion", producto.Descripcion),
                new SqlParameter("@Precio", producto.Precio),
                new SqlParameter("@CantidadEnStock", producto.CantidadEnStock),
                new SqlParameter("@FechaVencimiento", producto.FechaVencimiento),
                new SqlParameter("@CategoriaID", producto.CategoriaID),
                new SqlParameter("@ImagenUrl", producto.ImagenUrl)
            );

            if (filas > 0)
                return Ok("Producto agregado correctamente");

            return BadRequest("No se pudo agregar el producto");
        }

        
        // PUT: api/Productos/5
        
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody] Producto producto)
        {
            string query = @"UPDATE Productos SET 
                                Nombre = @Nombre,
                                Descripcion = @Descripcion,
                                Precio = @Precio,
                                CantidadEnStock = @CantidadEnStock,
                                FechaVencimiento = @FechaVencimiento,
                                CategoriaID = @CategoriaID,
                                ImagenUrl = @ImagenUrl
                             WHERE ProductoID = @ProductoID";

            int filas = db.EjecutarComando(query,
                new SqlParameter("@ProductoID", id),
                new SqlParameter("@Nombre", producto.Nombre),
                new SqlParameter("@Descripcion", producto.Descripcion),
                new SqlParameter("@Precio", producto.Precio),
                new SqlParameter("@CantidadEnStock", producto.CantidadEnStock),
                new SqlParameter("@FechaVencimiento", producto.FechaVencimiento),
                new SqlParameter("@CategoriaID", producto.CategoriaID),
                new SqlParameter("@ImagenUrl", producto.ImagenUrl)
            );

            if (filas > 0)
                return Ok("Producto actualizado correctamente");

            return BadRequest("No se pudo actualizar el producto");
        }

        
        // DELETE: api/Productos/5
        
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            string query = "DELETE FROM Productos WHERE ProductoID = @id";

            int filas = db.EjecutarComando(query,
                new SqlParameter("@id", id));

            if (filas > 0)
                return Ok("Producto eliminado correctamente");

            return BadRequest("No se pudo eliminar el producto");
        }


        // metodo privado para mapear DataRow a Producto sirve para que haya un orden

        private Producto MapearFila(DataRow row)
        {
            return new Producto
            {
                ProductoID = Convert.ToInt32(row["ProductoID"]),
                Nombre = row["Nombre"].ToString(),
                Descripcion = row["Descripcion"].ToString(),
                Precio = Convert.ToDecimal(row["Precio"]),
                CantidadEnStock = Convert.ToInt32(row["CantidadEnStock"]),
                FechaVencimiento = Convert.ToDateTime(row["FechaVencimiento"]),
                CategoriaID = Convert.ToInt32(row["CategoriaID"]),
                ImagenUrl = row.Table.Columns.Contains("ImagenUrl")
                            ? row["ImagenUrl"].ToString()
                            : "" // si existe null o columna vacía
            };
        }
    }
}
