<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Parametros.aspx.cs" Inherits="ICommunity.Parametros" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="container">
      <div class="row">&nbsp;</div>
      <div class="row">
        <asp:Button ID="btnGrabar" runat="server" OnClick="btnGrabar_Click" CssClass="btn btn-primary" Text="Button" />
        <asp:Button ID="btnRSS" runat="server" OnClick="btnRSS_Click" CssClass="btn btn-primary" Text="Button" />
      </div>
      <div class="row">&nbsp;</div>
      <div id="opciones" class="row" runat="server">
      </div>
    </div>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
      <Windows>
        <telerik:RadWindow ID="rdWindow" runat="server"></telerik:RadWindow>
      </Windows>
    </telerik:RadWindowManager>
  </form>
</body>
</html>
