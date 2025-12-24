using System;
using System.Data.SqlClient;
using System.Web.Http;
using BioFarmaWeb.Models;
using BioFarmaWebApi.DAL;

namespace BioFarmaWeb.Controllers
{
    public class ClientesController : ApiController
    {
        BaseDeDatos db = new BaseDeDatos();

        // POST: api/clientes
        [HttpPost]
        [Route("api/clientes")]
        public IHttpActionResult RegistrarCliente(ClienteModel cliente)
        {
            if (cliente == null)
            {
                return BadRequest("Datos inválidos");
            }

            try
            {
                string query = @"INSERT INTO Clientes (NombreCliente, Telefono, CorreoElectronico, Contrasena)
                                 VALUES (@nombre, @telefono, @correo, @pass)";

                int filas = db.EjecutarComando(query,
                    new SqlParameter("@nombre", cliente.NombreCliente),
                    new SqlParameter("@telefono", (object)cliente.Telefono ?? DBNull.Value),
                    new SqlParameter("@correo", cliente.CorreoElectronico),
                    new SqlParameter("@pass", cliente.Contrasena)
                );

                if (filas > 0)
                {
                    return Ok(new { Exito = true, Mensaje = "Cliente registrado correctamente" });
                }
                else
                {
                    return Ok(new { Exito = false, Mensaje = "No se pudo registrar el cliente" });
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("api/clientes")]
        public IHttpActionResult ObtenerClientes()
        {
            string query = "SELECT ClienteID, NombreCliente, Telefono, CorreoElectronico FROM Clientes";

            var tabla = db.EjecutarConsulta(query);

            return Ok(tabla);
        }

    }
}