<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Administrador.aspx.cs" Inherits="BioFarmaWeb.Administrador" ResponseEncoding="utf-8"%>

<!DOCTYPE html>
<html>
<head runat="server">
    <meta charset="utf-8" />
    <title>Panel de Administrador</title>

    <style>
        body {
            font-family: 'Segoe UI', Arial, sans-serif;
            margin: 0;
            background: #eef3f8;
        }

        /* Menú lateral */
        .menu {
            width: 230px;
            height: 100vh;
            background: #1565c0;
            color: white;
            position: fixed;
            top: 0;
            left: 0;
            padding-top: 30px;
            box-shadow: 3px 0 10px rgba(0,0,0,0.2);
        }

        .menu button {
            width: 100%;
            padding: 15px;
            border: none;
            background: transparent;
            color: white;
            text-align: left;
            cursor: pointer;
            font-size: 16px;
            transition: all 0.2s ease;
            border-radius: 8px;
        }

        .menu button:hover {
            background: rgba(255,255,255,0.25);
            transform: translateX(5px);
        }

        /* Contenido general */
        .contenido {
            margin-left: 260px;
            padding: 25px;
        }

        /* Titulos */
        .panelTitulo {
            font-size: 26px;
            margin-bottom: 20px;
            font-weight: 700;
            color: #333;
        }

        /* Tarjetas */
        #panelRegistrar, 
        #panelInventario, 
        #panelVentas, 
        #PanelClientes {
            background: white;
            padding: 25px;
            border-radius: 18px;
            box-shadow: 0 3px 15px rgba(0,0,0,0.1);
            animation: fadeIn .2s ease;
        }

        /* Animación */
        @keyframes fadeIn {
            from { opacity: 0; transform: translateY(10px); }
            to { opacity: 1; transform: translateY(0); }
        }

        /* Campos */
        .campo {
            margin-bottom: 15px;
            font-weight: 600;
            color: #444;
        }

        /* Inputs */
        input[type="text"],
        input[type="number"],
        input[type="date"],
        select,
        textarea {
            width: 100%;
            padding: 12px 15px;
            border: 1px solid #ccc;
            border-radius: 12px;
            font-size: 16px;
            outline: none;
            transition: 0.2s;
            background: #fafafa;
        }

        input:focus, select:focus, textarea:focus {
            border-color: #1e88e5;
            background: white;
            box-shadow: 0 0 6px rgba(30,136,229,0.4);
        }

        /* Botón guardar */
        #btnGuardarProducto {
            background: #43a047 !important;
            color: white !important;
            padding: 12px 20px;
            font-size: 16px;
            border-radius: 12px;
            border: none;
            cursor: pointer;
            transition: 0.2s ease;
        }

        #btnGuardarProducto:hover {
            background: #2e7d32 !important;
            box-shadow: 0 4px 10px rgba(46,125,50,0.4);
        }

        /* GridView */
        table {
            width: 100%;
            border-collapse: collapse;
            margin-top: 15px;
            border-radius: 12px;
            overflow: hidden;
            background: white;
            box-shadow: 0 3px 12px rgba(0,0,0,0.1);
        }

        table th {
            background: #1e88e5;
            color: white;
            padding: 12px;
            font-size: 16px;
            font-weight: 600;
        }

        table td {
            padding: 12px;
            border-bottom: 1px solid #eee;
        }

        table tr:hover td {
            background: #f1f7ff;
        }

        .tabla {
            width: 100%;
        }
    </style>

</head>
<body>

    <form id="form1" runat="server">

        <!-- menu lateral -->
        <div class="menu">
            <button runat="server" onserverclick="BtnRegistrarProducto_Click">Registrar Producto</button>
            <button runat="server" onserverclick="BtnInventario_Click">Inventario</button>
            <button runat="server" onserverclick="BtnVentas_Click">Ventas</button>
            <button runat="server" onserverclick="BtnClientes_Click">Clientes</button>
            <button runat="server" onserverclick="BtnCerrarSesion_Click">Cerrar sesión</button>
        </div>

        <!--area de contenido -->
        <div class="contenido">

            <!-- panel de registrar productos -->
            <div id="panelRegistrar" runat="server" visible="false">
                <div class="panelTitulo">Registrar Producto</div>

                <div class="campo">Nombre: <asp:TextBox ID="txtNombre" runat="server" /></div>
                <div class="campo">Descripción: <asp:TextBox ID="txtDescripcion" runat="server" /></div>
                <div class="campo">Precio: <asp:TextBox ID="txtPrecio" runat="server" /></div>
                <div class="campo">Cantidad en Stock: <asp:TextBox ID="txtStock" runat="server" /></div>
                <div class="campo">Fecha de Vencimiento: <asp:TextBox ID="txtVencimiento" runat="server" TextMode="Date" /></div>
                <div class="campo">Categoría ID: <asp:TextBox ID="txtCategoriaID" runat="server" /></div>

                <asp:Button ID="btnGuardarProducto" runat="server" Text="Guardar Producto" OnClick="btnGuardarProducto_Click" />
                <br /><br />
                <asp:Label ID="lblMensajeProducto" runat="server" ForeColor="Red"></asp:Label>
            </div>

            <!-- panel de inventario -->
            <div id="panelInventario" runat="server" visible="false">
                <div class="panelTitulo">Inventario</div>

                <asp:GridView ID="gridInventario" runat="server" AutoGenerateColumns="False" DataKeyNames="ProductoID" OnRowCommand="gridInventario_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="ProductoID" HeaderText="ID" />
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                        <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                        <asp:BoundField DataField="Precio" HeaderText="Precio" />
                        <asp:BoundField DataField="CantidadEnStock" HeaderText="Stock" />
                        <asp:BoundField DataField="FechaVencimiento" HeaderText="Vence" DataFormatString="{0:yyyy-MM-dd}" />
                        <asp:BoundField DataField="CategoriaID" HeaderText="Categoría" />

                        <asp:ButtonField Text="Editar" CommandName="Editar" />
                        <asp:ButtonField Text="Eliminar" CommandName="Eliminar" />
                    </Columns>
                </asp:GridView>
            </div>

            <!-- panel de ventas -->
            <div id="panelVentas" runat="server" visible="false">
                <div class="panelTitulo">Registro de Ventas</div>

                <asp:GridView ID="gridVentas" runat="server" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="DetalleID" HeaderText="ID Detalle" />
                        <asp:BoundField DataField="VentaID" HeaderText="Venta" />
                        <asp:BoundField DataField="ProductoID" HeaderText="Producto" />
                        <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                        <asp:BoundField DataField="PrecioUnitario" HeaderText="Precio Unitario" DataFormatString="{0:C}" />
                    </Columns>
                </asp:GridView>
            </div>

            <!-- panel de clientes -->
            <div id="PanelClientes" runat="server" visible="false" class="contenido">
                <h2 class="panelTitulo">Clientes Registrados</h2>

                <asp:GridView 
                    ID="GridClientes" 
                    runat="server" 
                    AutoGenerateColumns="false"
                    CssClass="tabla"
                    GridLines="None"
                    CellPadding="8">

                    <Columns>
                        <asp:BoundField DataField="ClienteID" HeaderText="ID" />
                        <asp:BoundField DataField="NombreCliente" HeaderText="Cliente" />
                        <asp:BoundField DataField="CorreoElectronico" HeaderText="Correo" />
                        <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
                    </Columns>

                </asp:GridView>
            </div>

        </div>

    </form>
</body>
</html>
