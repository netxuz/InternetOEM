<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="ICommunity.Antalis._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta charset="utf-8">
  <meta http-equiv="X-UA-Compatible" content="IE=edge">
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <title></title>
  <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
  <link rel="stylesheet" href="../css/styleadmin.css">
  <link rel="stylesheet" type="text/css" href="../css/stylesdebtcontrol.css" media="screen" />
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
</head>
<body class="bodyadm">
  <form id="form1" runat="server">
    <div class="container">
      <div class="row">&nbsp;</div>
      <div class="row">
        <h4>APLICATIVO ANTALIS</h4>
      </div>
      <div class="row">
        &nbsp;
      </div>
      <div class="row">
        <asp:LinkButton ID="lnkBtnCentroDistribucion" runat="server" OnClick="lnkBtnCentroDistribucion_Click"><span class="glyphicon glyphicon-list-alt">&nbsp;ASIGNACION DE CENTROS DE DISTRIBUCIÓN</span></asp:LinkButton>
      </div>
      <div class="row">
        <asp:LinkButton ID="lnkBtnRoles" runat="server" OnClick="lnkBtnRoles_Click"><span class="glyphicon glyphicon-list-alt">&nbsp;ASIGNACION DE ROLES A USUARIOS</span></asp:LinkButton>
      </div>
    </div>
  </form>
</body>
</html>
