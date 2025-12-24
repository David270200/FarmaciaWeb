<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="BioFarmaWeb.Productos" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>BioFarma/Cliente</title>
</head>
<body>
    <form id="form1" runat="server">

        <!-- Bdiseño de la barra superior, meterle el logo -->
        <div style="display:flex; justify-content:space-between; align-items:center; padding:10px 20px;">
            <div>
                <img src="Imagenes/LogoBioFarma.png" alt="BioFarma" style="height:60px;" />
            </div>
            <div style="flex:1; text-align:center;">
                <asp:TextBox ID="txtBuscar" runat="server" 
                             placeholder="Buscar producto..." Width="300px"
                             Style="padding:10px 15px; border-radius:12px; border:2px solid #007BFF; font-size:16px;" />
                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click"
                            Style="background-color:#007BFF; color:white; border:none; padding:8px 20px; border-radius:8px; cursor:pointer;" />
            </div>
        </div>

        <!-- titulo -->
        <h2 style="text-align:center; margin-top:20px;">Listado de Productos</h2>

        <!-- grid de productos -->
        <div style="display:flex; justify-content:center; margin-top:20px;">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="ProductoID" HeaderText="ID" />
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                    <asp:BoundField DataField="Precio" HeaderText="Precio" DataFormatString="{0:C}" />

                    <asp:TemplateField HeaderText="Imagen">
                        <ItemTemplate>
                            <div style="display:flex; justify-content:center; align-items:center;">
                                <img src='<%# Eval("ImagenUrl") %>' style="width:150px; height:150px; object-fit:cover; border:2px solid #ccc; box-shadow:0 0 5px rgba(0,0,0,0.2);" />
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Cantidad">
                     <ItemTemplate>
                       <asp:TextBox ID="txtCantidad" runat="server" Text="1" Width="40px" />
                     </ItemTemplate>
                   </asp:TemplateField>

                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:Button ID="btnComprar" runat="server" Text="Comprar" 
                                        CommandArgument='<%# Eval("ProductoID") %>' 
                                        OnClick="btnComprar_Click"
                                        Style="background-color:#28a745; color:white; border:none; padding:5px 15px; border-radius:6px; cursor:pointer;" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>

        <!-- boton para el carrito -->
        <div style="text-align:right; margin:25px;">
            <asp:Button ID="btnVerCarrito" runat="server" Text="🛒" OnClick="btnVerCarrito_Click" Style="border-radius:5px; background:#807370; color:white; font-size:20px; padding: 6px 14px; border:none; cursor:pinter;" />

        </div>

        <!-- modal del carrito -->
        <div id="modalCarrito" runat="server" style="display:none; position:fixed; top:10%; right:10%; width:300px; background:white; border:2px solid #007BFF; padding:15px; box-shadow:0 0 10px rgba(0,0,0,0.3); z-index:1000;">
            <h3>Carrito</h3>

            <!-- Repeater del carrito -->
            <asp:Repeater ID="rptCarrito" runat="server">
                <ItemTemplate>
                    <div style="display:flex; justify-content:space-between; margin-bottom:10px; border-bottom:1px solid #ccc; padding-bottom:5px;">
                        <span><%# Eval("Producto.Nombre") %> x <%# Eval("Cantidad") %></span>
                        <span>$<%# Convert.ToDecimal(Eval("Producto.Precio")) * Convert.ToInt32(Eval("Cantidad")) %></span>
                    </div>
                </ItemTemplate>
            </asp:Repeater>

            <!-- Total -->
            <div style="display:flex; justify-content:space-between; font-weight:bold; border-top:2px solid #007BFF; padding-top:5px; margin-top:10px;">
                <span>Total:</span>
                <span>$<asp:Label ID="lblTotalCarrito" runat="server" Text="0.00"></asp:Label></span>
            </div>

            <!-- Boton confirmar compra -->
<div style="text-align:center; margin-top:10px;">
    <asp:Button ID="btnConfirmarCompra" runat="server" Text="Confirmar Compra"
                OnClick="btnConfirmarCompra_Click"
                Style="background-color:#28a745; color:white; border:none; padding:8px 20px; border-radius:6px; cursor:pointer;" />
</div>


            <!-- Boton cerrar -->
            <div style="text-align:center; margin-top:15px;">
                <asp:Button ID="btnCerrarCarrito" runat="server" Text="Cerrar" OnClick="btnCerrarCarrito_Click" Style="background-color:#dc3545; color:white; border:none; padding:8px 20px; border-radius:6px; cursor:pointer;" />
            </div>
        </div>

    </form>
</body>
</html>