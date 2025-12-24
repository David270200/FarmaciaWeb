using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;
using BioFarmaWeb.Models;
using BioFarmaWebApi.DAL;

namespace BioFarmaWeb.Controllers
{
    public class LoginController : ApiController
    {
        BaseDeDatos db = new BaseDeDatos();

        [HttpPost]
        [Route("api/login")]
        public IHttpActionResult Login(LoginModel login)
        {
            if (login == null)
            {
                return BadRequest("Datos inválidos");
            }

            //  Intentar login como Cliente
            string queryCliente = "SELECT ClienteID, CorreoElectronico FROM Clientes " +
                                  "WHERE CorreoElectronico = @correo AND Contrasena = @pass";

            DataTable tablaCliente = db.EjecutarConsulta(queryCliente,
                new SqlParameter("@correo", login.CorreoElectronico),
                new SqlParameter("@pass", login.Contrasena));

            if (tablaCliente.Rows.Count > 0)
            {
                return Ok(new
                {
                    Exito = true,
                    Tipo = "Cliente",
                    ID = tablaCliente.Rows[0]["ClienteID"]
                });
            }

            //  Intentar login como Empleado
            string queryEmp = "SELECT EmpleadoID, CorreoElectronico FROM Empleado " +
                              "WHERE CorreoElectronico = @correo AND Contrasena = @pass";

            DataTable tablaEmp = db.EjecutarConsulta(queryEmp,
                new SqlParameter("@correo", login.CorreoElectronico),
                new SqlParameter("@pass", login.Contrasena));

            if (tablaEmp.Rows.Count > 0)
            {
                return Ok(new
                {
                    Exito = true,
                    Tipo = "Empleado",
                    ID = tablaEmp.Rows[0]["EmpleadoID"]
                });
            }

            return Ok(new { Exito = false, Mensaje = "Credenciales incorrectas" });
        }
    }
}
