using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using BioFarmaWeb.Models;

namespace BioFarmaWeb
{
    public partial class Login : System.Web.UI.Page
    {
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string correo = txtCorreo.Text.Trim();
            string pass = txtPass.Text.Trim();

            var datosLogin = new
            {
                CorreoElectronico = correo,
                Contrasena = pass
            };

            string json = JsonConvert.SerializeObject(datosLogin);

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44330/");

                StringContent contenido = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage respuesta = client.PostAsync("api/login", contenido).Result;

                if (respuesta.IsSuccessStatusCode)
                {
                    var contenidoResp = respuesta.Content.ReadAsStringAsync().Result;

                    dynamic resultado = JsonConvert.DeserializeObject(contenidoResp);

                    if (resultado.Exito == true)
                    {
                        string tipo = resultado.Tipo.ToString();

                        if (tipo == "Cliente")
                        {
                            Response.Redirect("Productos.aspx");
                        }
                        else if (tipo == "Empleado")
                        {
                            Response.Redirect("Administrador.aspx");
                        }
                    }
                    else
                    {
                        lblMensaje.Text = "Credenciales incorrectas.";
                    }
                }
                else
                {
                    lblMensaje.Text = "Error de servidor.";
                }
            }
        }
        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegistroUsuario.aspx"); 
        }
    }
}