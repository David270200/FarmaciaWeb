using BioFarmaWeb.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.UI.WebControls;
using System.Net.Http;
using System.Text;

namespace BioFarmaWeb
{
    public partial class Productos : System.Web.UI.Page
    {
        // Funcion que guarda los productos cargados desde la API
        private List<ProductoWeb> ProductosCargados
        {
            get { return ViewState["ProductosCargados"] as List<ProductoWeb> ?? new List<ProductoWeb>(); }
            set { ViewState["ProductosCargados"] = value; }
        }

        // Carrito de compras
        protected List<ItemCarrito> Carrito
        {
            get { return Session["Carrito"] as List<ItemCarrito> ?? new List<ItemCarrito>(); }
            set { Session["Carrito"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarProductos();
            }
        }
        
        //funcionalidad al boton de compra
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string busqueda = txtBuscar.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(busqueda))
            {
                GridView1.DataSource = ProductosCargados;
                GridView1.DataBind();
                return;
            }

            var productosFiltrados = ProductosCargados
                .Where(p => p.Nombre.ToLower().Contains(busqueda))
                .ToList();

            GridView1.DataSource = productosFiltrados;
            GridView1.DataBind();
        }

        //funcionalidad al boton que carga los productos EL PREGUNTA A LA API SOBRE LOS PRODUCTOS Y LOS TRAE
        private void CargarProductos(string filtro = "")
        {
            string apiUrl = "https://localhost:44330/API/Productos";

            if (!string.IsNullOrEmpty(filtro))
            {
                string filtroEnc = Uri.EscapeDataString(filtro);
                apiUrl += "?nombre=" + filtroEnc;
            }

            try
            {
                using (WebClient client = new WebClient())
                {
                    client.Encoding = System.Text.Encoding.UTF8;
                    client.Headers["Accept"] = "application/json";

                    string json = client.DownloadString(apiUrl);

                    List<ProductoWeb> productos = JsonConvert.DeserializeObject<List<ProductoWeb>>(json);

                    ProductosCargados = productos;

                    GridView1.DataSource = productos;
                    GridView1.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Write("<div style='color:red; text-align:center;'>Error al cargar productos: "
                               + Server.HtmlEncode(ex.Message) + "</div>");
            }
        }

        //BOTON de compra, aunque creo que mejor lo cambio en el html como enviar a carrito
        protected void btnComprar_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow fila = (GridViewRow)btn.NamingContainer; // obtengo la fila del botón
            int productoID = int.Parse(btn.CommandArgument);

            // Tomo la cantidad del TextBox en la misma fila
            TextBox txtCantidad = (TextBox)fila.FindControl("txtCantidad");
            int cantidad = 1;
            int.TryParse(txtCantidad.Text.Trim(), out cantidad);
            if (cantidad < 1) cantidad = 1;

            ProductoWeb producto = ProductosCargados.FirstOrDefault(p => p.ProductoID == productoID);

            if (producto != null)
            {
                List<ItemCarrito> carrito = Carrito;

                var itemExistente = carrito.FirstOrDefault(i => i.Producto.ProductoID == productoID);
                if (itemExistente != null)
                {
                    itemExistente.Cantidad += cantidad;
                }
                else
                {
                    carrito.Add(new ItemCarrito { Producto = producto, Cantidad = cantidad });
                }

                Carrito = carrito;

                Response.Write($"<div style='text-align:center; color:green;'>Producto {producto.Nombre} agregado al carrito</div>");
            }
        }

        //este es la funcionalidad que hace que se abra el carrito y de el total de los productos
        protected void btnVerCarrito_Click(object sender, EventArgs e)
        {
            rptCarrito.DataSource = Carrito;
            rptCarrito.DataBind();

            // Calcula total
            decimal total = Carrito.Sum(i => i.Producto.Precio * i.Cantidad);
            lblTotalCarrito.Text = total.ToString("0.00");

            modalCarrito.Style["display"] = "block";
        }

        protected void btnCerrarCarrito_Click(object sender, EventArgs e)
        {
            modalCarrito.Style["display"] = "none";
        }

        //  funcionalidad al boton Confirmar compra, NO TOCAR ESTE BOTON HOY VIVI LA DE 'MI CODIGO FUNCIONA NO SE PORQUE PERO FUNCIONA'
    protected void btnConfirmarCompra_Click(object sender, EventArgs e)
{
    if (Carrito.Count == 0)
    {
                Response.Write("<div style='color:red; text-align:center;'>El carrito esta vacio</div>");
                return;
            }

    if (Session["ClienteID"] == null)
    {
                Response.Write("<div style='color:red; text-align:center;'>No se ha encontrado Sesión del cliente</div>");
                return;
            }

    int clienteID = (int)Session["ClienteID"];
    string direccionEntrega = "Mi dirección de prueba";

    // Construir objeto que coincida con VentaModel
    var venta = new
    {
        ClienteID = clienteID,
        DireccionEntrega = direccionEntrega,
        Productos = Carrito.Select(p => new {
            ProductoID = p.Producto.ProductoID,
            Cantidad = p.Cantidad
        }).ToList()
    };

    string json = JsonConvert.SerializeObject(venta);

    try
    {
        using (var client = new System.Net.Http.HttpClient())
        {
            client.BaseAddress = new Uri("https://localhost:44330/");
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = client.PostAsync("api/ventas", content).Result;

            if (response.IsSuccessStatusCode)
            {
                // Vaciar carrito
                Carrito = new List<ItemCarrito>();
                modalCarrito.Style["display"] = "none";
                        Response.Write("<div style='color:red; text-align:center;'>¡Compra realizada con exito!</div>");
            }
            else
            {
                var respContent = response.Content.ReadAsStringAsync().Result;
                        Response.Write("<div style='color:red; text-align:center;'>Error al procesar la compra: </div>");
            }
        }
    }
    catch (Exception ex)
    {
                Response.Write("<div style='color:red; text-align:center;'>Error al procesar la Compra: </div>");
    }
}

    }
}
