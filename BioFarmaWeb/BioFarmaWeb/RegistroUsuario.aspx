<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistroUsuario.aspx.cs" Inherits="BioFarmaWeb.RegistroUsuario" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Registro BioFarma</title>
    <style>
        body {
            margin: 0;
            padding: 0;
            font-family: Arial, sans-serif;
            background: #eef2f5;
        }

        .container {
            width: 360px;
            margin: 80px auto;
            padding: 30px;
            background: white;
            border: 1px solid #d7d7d7;
            border-radius: 12px;
            box-shadow: 0px 4px 15px rgba(0,0,0,0.12);
            text-align: center;
        }

        h2 {
            margin-bottom: 20px;
            color: #1d6fa5;
            font-weight: bold;
        }

        .input-label {
            text-align: left;
            display: block;
            font-weight: bold;
            margin-top: 12px;
            color: #333;
        }

        .input-box {
            width: 100%;
            padding: 10px;
            margin-top: 6px;
            border-radius: 6px;
            border: 1px solid #c7c7c7;
            font-size: 15px;
        }

        .btn-primary {
            width: 100%;
            padding: 10px;
            background: #1d6fa5;
            color: white;
            border: none;
            border-radius: 6px;
            margin-top: 20px;
            cursor: pointer;
            font-size: 16px;
        }

        .btn-primary:hover {
            background: #155378;
        }

        #lblMensaje {
            margin-top: 15px;
            display: block;
            color: red;
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Registro de Usuario</h2>

            <asp:Label runat="server" Text="Nombre completo:" CssClass="input-label" />
            <asp:TextBox ID="txtNombre" runat="server" CssClass="input-box" />

            <asp:Label runat="server" Text="Correo electrónico:" CssClass="input-label" />
            <asp:TextBox ID="txtCorreo" runat="server" CssClass="input-box" />

            <asp:Label runat="server" Text="Contraseña:" CssClass="input-label" />
            <asp:TextBox ID="txtPass" runat="server" TextMode="Password" CssClass="input-box" />

            <asp:Button ID="btnRegistrar" runat="server" Text="Registrarse" CssClass="btn-primary" OnClick="btnRegistrar_Click" />

            <asp:Label ID="lblMensaje" runat="server" />
        </div>
    </form>
</body>
</html>
 b
