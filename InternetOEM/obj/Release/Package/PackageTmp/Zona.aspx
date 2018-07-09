<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Zona.aspx.cs" Inherits="ICommunity.Zona" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="Controls/PageObject.ascx" TagName="PageObject" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta charset="utf-8">
  <meta http-equiv="X-UA-Compatible" content="IE=edge">
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <title></title>
  <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
  <link rel="stylesheet" href="css/styleadmin.css">
  <link rel="stylesheet" type="text/css" href="css/stylesdebtcontrol.css" media="screen" />
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
</head>
<body class="bodyadm">
  <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager" runat="server">
    </asp:ScriptManager>
    <asp:HiddenField ID="CodZona" runat="server" />
    <div class="container">
      <div class="row">&nbsp;</div>
      <div class="row">
        <asp:Button ID="btnGrabar" runat="server" Text="Grabar" CssClass="btn btn-primary" OnClick="btnGrabar_Click" />
        <hr />
      </div>
      <div class="row">
        <asp:Label ID="lblTitulo" runat="server" Text="Titulo"></asp:Label>
      </div>
      <div class="row">
        <div class="form-group">
          <asp:TextBox ID="txtTitulo" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
        <div class="form-group">
          <asp:DropDownList ID="rdCmbEstado" CssClass="form-control" runat="server">
            <asp:ListItem Text="Vigente" Value="V"></asp:ListItem>
            <asp:ListItem Text="No Vigente" Value="N"></asp:ListItem>
          </asp:DropDownList>
        </div>
        <div class="form-group">
          <div class="checkbox">
            <asp:CheckBox ID="chk_despliegue" runat="server" Text="Despliegue contenidos" />
          </div>
        </div>
        <div class="form-group">
          <asp:Label ID="lblDescripcion" runat="server" Text="Descripción"></asp:Label>
        </div>
        <div class="form-group">
          <telerik:RadEditor ID="rdDescripcion" runat="server">
          </telerik:RadEditor>
        </div>
        <div class="form-group">
          <uc1:PageObject ID="PageObject" pbIndZonas="true" runat="server" />
        </div>
      </div>
    </div>
    <script language="JavaScript" type="text/javascript">
      function GetIdEditor() {
        return $find("<%= rdDescripcion.ClientID %>");
      }
    </script>
  </form>
</body>
</html>
