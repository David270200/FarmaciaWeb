using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace BioFarmaWeb
{
    public partial class RegistroUsuario : System.Web.UI.Page
    {
        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text.Trim();
            string correo = txtCorreo.Text.Trim();
            string contrasena = txtPass.Text.Trim();

            if (string.IsNullOrEmpty(nombre) ||
                string.IsNullOrEmpty(correo) ||
                string.IsNullOrEmpty(contrasena))
            {
                lblMensaje.Text = "Complete todos los campos.";
                return;
            }

            //  Crea el objeto EXACTO que mi API recibe
            var nuevoCliente = new
            {
                NombreCliente = nombre,
                CorreoElectronico = correo,
                Contrasena = contrasena
            };

            string json = JsonConvert.SerializeObject(nuevoCliente);

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    //  URL base de mi API
                    client.BaseAddress = new Uri("https://localhost:44330/");

                    // El JSON que enviamos
                    StringContent contenido = new StringContent(json, Encoding.UTF8, "application/json");

                    //  Llamada al endpoint de mi API
                    HttpResponseMessage respuesta = client.PostAsync("api/clientes", contenido).Result;

                    if (respuesta.IsSuccessStatusCode)
                    {
                        var resultado = respuesta.Content.ReadAsStringAsync().Result;
                        dynamic data = JsonConvert.DeserializeObject(resultado);

                        if (data.Exito == true)
                        {
                            lblMensaje.ForeColor = System.Drawing.Color.Green;
                            lblMensaje.Text = "¡Registro exitoso! Ahora puede iniciar sesión.";
                        }
                        else
                        {
                            lblMensaje.ForeColor = System.Drawing.Color.Red;
                            lblMensaje.Text = data.Mensaje;
                        }
                    }
                    else
                    {
                        lblMensaje.ForeColor = System.Drawing.Color.Red;
                        lblMensaje.Text = "Error al registrar usuario.";
                    }
                }
            }
            catch (Exception ex)
            {
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Text = "Error al comunicarse con el servidor: " + ex.Message;
            }
        }
    }
}
