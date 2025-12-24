using BioFarmaWeb.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Web.UI.WebControls;

namespace BioFarmaWeb
{
    public partial class Administrador : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //   menu lateral

        protected void BtnRegistrarProducto_Click(object sender, EventArgs e)
        {
            panelRegistrar.Visible = true;
            panelInventario.Visible = false;
            panelVentas.Visible = false;
            PanelClientes.Visible = false;
        }

        protected void BtnInventario_Click(object sender, EventArgs e)
        {
            panelRegistrar.Visible = false;
            panelInventario.Visible = true;
            panelVentas.Visible = false;
            PanelClientes.Visible = false;

            CargarInventario();
        }

        protected void BtnVentas_Click(object sender, EventArgs e)
        {
            panelRegistrar.Visible = false;
            panelInventario.Visible = false;
            panelVentas.Visible = true;
            PanelClientes.Visible = false;

            CargarDetalleVenta();
        }

        protected void BtnClientes_Click(object sender, EventArgs e)
        {
            panelRegistrar.Visible = false;
            panelInventario.Visible = false;
            panelVentas.Visible = false;
            PanelClientes.Visible = true;

            CargarClientes();
        }


        protected void BtnCerrarSesion_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }


        //   metodo para registrar y actualizar productos

        protected void btnGuardarProducto_Click(object sender, EventArgs e)
        {
            try
            {
                ProductoWeb nuevo = new ProductoWeb
                {
                    Nombre = txtNombre.Text,
                    Descripcion = txtDescripcion.Text,
                    Precio = Convert.ToDecimal(txtPrecio.Text),
                    CantidadEnStock = Convert.ToInt32(txtStock.Text),
                    FechaVencimiento = Convert.ToDateTime(txtVencimiento.Text),
                    CategoriaID = Convert.ToInt32(txtCategoriaID.Text),
                    ImagenUrl = ""
                };

                if (ViewState["ProductoID"] != null)
                {
                    int id = Convert.ToInt32(ViewState["ProductoID"]);

                    ActualizarProducto(id, nuevo);

                    lblMensajeProducto.ForeColor = System.Drawing.Color.Green;
                    lblMensajeProducto.Text = "Producto actualizado correctamente.";

                    ViewState["ProductoID"] = null;
                    btnGuardarProducto.Text = "Guardar Producto";

                    txtNombre.Text = "";
                    txtDescripcion.Text = "";
                    txtPrecio.Text = "";
                    txtStock.Text = "";
                    txtVencimiento.Text = "";
                    txtCategoriaID.Text = "";

                    CargarInventario();
                }
                else
                {
                    string url = "https://localhost:44330/API/Productos";
                    string json = JsonConvert.SerializeObject(nuevo);

                    using (WebClient client = new WebClient())
                    {
                        client.Encoding = Encoding.UTF8;
                        client.Headers["Content-Type"] = "application/json";
                        client.Headers["Accept"] = "application/json";

                        client.UploadString(url, "POST", json);
                    }

                    lblMensajeProducto.ForeColor = System.Drawing.Color.Green;
                    lblMensajeProducto.Text = "Producto registrado correctamente.";

                    txtNombre.Text = "";
                    txtDescripcion.Text = "";
                    txtPrecio.Text = "";
                    txtStock.Text = "";
                    txtVencimiento.Text = "";
                    txtCategoriaID.Text = "";

                    if (panelInventario.Visible)
                        CargarInventario();
                }
            }
            catch (Exception ex)
            {
                lblMensajeProducto.ForeColor = System.Drawing.Color.Red;
                lblMensajeProducto.Text = "Error registrando/actualizando producto: " + ex.Message;
            }
        }

        private void ActualizarProducto(int id, ProductoWeb producto)
        {
            string url = $"https://localhost:44330/API/Productos/{id}";
            string json = JsonConvert.SerializeObject(producto);

            using (WebClient client = new WebClient())
            {
                client.Headers["Content-Type"] = "application/json";
                client.UploadString(url, "PUT", json);
            }
        }

        //   inventario - editar y eliminar productos

        protected void gridInventario_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int productoID = Convert.ToInt32(gridInventario.DataKeys[index].Value);

            if (e.CommandName == "Eliminar")
            {
                EliminarProducto(productoID);
            }
            else if (e.CommandName == "Editar")
            {
                CargarProductoEnFormulario(productoID);
            }
        }

        private void EliminarProducto(int id)
        {
            try
            {
                string url = $"https://localhost:44330/API/Productos/{id}";

                using (WebClient client = new WebClient())
                {
                    client.Headers["Content-Type"] = "application/json";
                    client.UploadString(url, "DELETE", "");
                }

                CargarInventario();
            }
            catch
            {
            }
        }

        private void CargarProductoEnFormulario(int id)
        {
            string url = $"https://localhost:44330/API/Productos/{id}";

            using (WebClient client = new WebClient())
            {
                client.Headers["Content-Type"] = "application/json";
                string json = client.DownloadString(url);

                ProductoWeb p = JsonConvert.DeserializeObject<ProductoWeb>(json);

                txtNombre.Text = p.Nombre;
                txtDescripcion.Text = p.Descripcion;
                txtPrecio.Text = p.Precio.ToString();
                txtStock.Text = p.CantidadEnStock.ToString();
                txtVencimiento.Text = p.FechaVencimiento.ToString("yyyy-MM-dd");
                txtCategoriaID.Text = p.CategoriaID.ToString();

                ViewState["ProductoID"] = id;
                btnGuardarProducto.Text = "Actualizar Producto";
                panelRegistrar.Visible = true;
            }
        }

        //   CARGAR INVENTARIO
      
        private void CargarInventario()
        {
            string url = "https://localhost:44330/API/Productos";

            using (WebClient client = new WebClient())
            {
                client.Headers["Content-Type"] = "application/json";
                string json = client.DownloadString(url);

                var lista = JsonConvert.DeserializeObject<List<ProductoWeb>>(json);

                gridInventario.DataSource = lista;
                gridInventario.DataBind();
            }
        }
    
        //   funcion para cargar Detalle Ventas
        
        private void CargarDetalleVenta()
        {
            string url = "https://localhost:44330/API/DetalleVenta";

            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(url);

                var lista = JsonConvert.DeserializeObject<List<DetalleVentaWeb>>(json);

                gridVentas.DataSource = lista;
                gridVentas.DataBind();
            }
        }

        //   funcion para cargar clientes
     
        private void CargarClientes()
        {
            string url = "https://localhost:44330/API/Clientes";

            using (WebClient client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;

                string json = client.DownloadString(url);

                var lista = JsonConvert.DeserializeObject<List<dynamic>>(json);

                GridClientes.DataSource = lista;
                GridClientes.DataBind();
            }
        }
    }
}