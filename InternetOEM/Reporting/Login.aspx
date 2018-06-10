<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ICommunity.Reporting.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta charset="utf-8">
  <meta http-equiv="X-UA-Compatible" content="IE=edge">
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <title>Home</title>
  <link rel="stylesheet" href="../css/bootstrap.min.css">
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
  <link rel="stylesheet" type="text/css" href="../css/stylesdebtcontrol.css" media="screen" />
</head>
<body class="bodycss">

  <asp:Panel ID="panel" runat="server" DefaultButton="btnAceptar">
    <div class="container_login">
      <form id="form1" runat="server" class="form-signin">
        <asp:Label ID="lblTitle" runat="server" Text="Inicia sesión" Visible="true" CssClass="form-signin-heading"></asp:Label>
        <asp:Label ID="lblLogin" runat="server" Text="Usuario" Visible="true" CssClass="sr-only"></asp:Label>
        <asp:TextBox ID="txtLogin" runat="server" CssClass="form-control" placeholder="Usuario"></asp:TextBox>
        <asp:Label ID="lblPassword" runat="server" Text="" Visible="true" CssClass="sr-only"></asp:Label>
        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Password"></asp:TextBox>
        <asp:Button ID="btnAceptar" runat="server" Text="" CssClass="btn btn-lg btn-primary btn-block" OnClick="btnAceptar_Click" />
      </form>
    </div>
  </asp:Panel>

</body>
</html>
