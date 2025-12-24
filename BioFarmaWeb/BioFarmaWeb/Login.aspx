<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="BioFarmaWeb.Login" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Login BioFarma</title>

    <style>
        body {
            margin: 0;
            padding: 0;
            font-family: Arial, sans-serif;
            background: #6E6969; /* Fondo más gris para resaltar la tarjeta */
        }

        .container {
            width: 380px;
            margin: 110px auto;
            padding: 30px;

            /* diseño de tarjeta */
            background: #f7f9fc; /* fondo de tarjeta diferente al blanco para que se note la diferencia */
            border: 2px solid #c3ccd5;  /* borde visible */
            border-radius: 16px;        /* bordes más suaves */
            box-shadow: 0px 6px 20px rgba(0,0,0,0.18);

            text-align: center;
        }


        .input-label {
            text-align: left;
            display: block;
            font-weight: bold;
            margin-top: 16px;
            color: #2e2e2e;
        }

        .input-box {
            width: 100%;
            padding: 12px;
            margin-top: 6px;
            border-radius: 8px;
            border: 1.6px solid #b8b8b8;
            font-size: 15px;
            background-color: #ffffff;
        }

        .input-box:focus {
            border-color: #1d6fa5;
            outline: none;
            box-shadow: 0 0 6px rgba(29,111,165,0.4);
        }

        .btn-primary {
            width: 100%;
            padding: 12px;
            background: #1d6fa5;
            color: white;
            border: none;
            border-radius: 8px;
            margin-top: 22px;
            cursor: pointer;
            font-size: 17px;
            font-weight: bold;
            transition: 0.2s;
        }

        .btn-primary:hover {
            background: #155378;
        }

        .btn-secondary {
            width: 100%;
            padding: 11px;
            background: #807370;
            color: #333;
            border: none;
            border-radius: 8px;
            margin-top: 14px;
            cursor: pointer;
            font-size: 15px;
            transition: 0.2s;
        }

        .btn-secondary:hover {
            background: #d2d2d2;
        }

        #lblMensaje {
            margin-top: 20px;
            display: block;
            color: #d00;
            font-weight: bold;
        }

        .logo {
            width: 110px;
            margin-bottom: 10px;
            filter: drop-shadow(0px 2px 3px rgba(0,0,0,0.25));
        }

        .title {
            font-size: 26px;
            font-weight: bold;
            color: #1d6fa5;
            margin-bottom: 25px;
            letter-spacing: 1px;
        }
    </style>

</head>
<body>

    <form id="form1" runat="server">
        <div class="container">

            <img src="Imagenes/LogoBioFarma.png" class="logo" alt="Logo BioFarma" />
               <div class="title">BioFarma</div>

            <asp:Label runat="server" Text="Correo electrónico:" CssClass="input-label" />
            <asp:TextBox ID="txtCorreo" runat="server" CssClass="input-box" />

            <asp:Label runat="server" Text="Contraseña:" CssClass="input-label" />
            <asp:TextBox ID="txtPass" runat="server" TextMode="Password" CssClass="input-box" />

            <asp:Button ID="btnLogin" runat="server" Text="Entrar" CssClass="btn-primary" OnClick="btnLogin_Click" />

            <asp:Button ID="btnRegistrar" runat="server" Text="Registrarse" CssClass="btn-secondary" OnClick="btnRegistrar_Click" />

            <asp:Label ID="lblMensaje" runat="server" />

        </div>
    </form>

</body>
</html>