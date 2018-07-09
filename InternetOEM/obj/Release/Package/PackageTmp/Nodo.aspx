<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Nodo.aspx.cs" Inherits="ICommunity.Nodo" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta charset="utf-8">
  <meta http-equiv="X-UA-Compatible" content="IE=edge">
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <title></title>
  <link rel="stylesheet" href="css/bootstrap.min.css">
  <link rel="stylesheet" href="css/styleadmin.css">
  <link rel="stylesheet" type="text/css" href="../css/stylesdebtcontrol.css" media="screen" />
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
</head>
<body class="bodyadm">
  <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:HiddenField ID="CodNodo" runat="server" />
    <asp:HiddenField ID="CodNodoRel" runat="server" />
    <asp:HiddenField ID="OrdNodo" runat="server" />
    <asp:HiddenField ID="sAccion" runat="server" />

    <div class="container">
      <div class="row">
        <br />
        <asp:Button ID="btnGrabar" runat="server" Text="Grabar" CssClass="btn btn-primary" OnClick="btnGrabar_Click" />
        <hr />
      </div>
      <div class="row">
        <h3>Datos Básicos</h3>
        <div class="form-group">
          <asp:Label ID="lblTitulo" runat="server" Text="Titulo"></asp:Label>
          <asp:TextBox ID="txtTitulo" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
        <div class="form-group">
          <asp:Label ID="lblTemplate" runat="server" Text="Template"></asp:Label>
          <asp:DropDownList ID="rdCmbTemplate" CssClass="form-control" runat="server">
          </asp:DropDownList>
        </div>
        <div class="form-group">
          <asp:Label ID="lblEstado" runat="server" Text="Estado"></asp:Label>
          <asp:DropDownList ID="rdCmbEstado" CssClass="form-control" runat="server">
            <asp:ListItem Text="Vigente" Value="V"></asp:ListItem>
            <asp:ListItem Text="No Vigente" Value="N"></asp:ListItem>
            <asp:ListItem Text="Phone" Value="P"></asp:ListItem>
            <asp:ListItem Text="Oculto" Value="O"></asp:ListItem>
          </asp:DropDownList>
        </div>
      </div>

      <div class="row">
        <h3>Configuración Nodo</h3>
        <div class="form-group">
          <div class="checkbox">
            <asp:CheckBox ID="chk_inicio" runat="server" Text="Página inicio" />
          </div>
          <div class="checkbox">
            <asp:CheckBox ID="chk_contenido" runat="server" Text="Página de Contenidos" />
          </div>
          <div class="checkbox">
            <asp:CheckBox ID="chk_termuse" runat="server" Text="Página de terminos de uso" />
          </div>
          <div class="checkbox">
            <asp:CheckBox ID="chk_poltsecure" runat="server" Text="Página de politicas de seguridad" />
          </div>
          <div class="checkbox">
            <asp:CheckBox ID="chk_privado" runat="server" Text="Página Privada" />
          </div>
          <div class="checkbox">
            <asp:CheckBox ID="chk_asocusrperfil" runat="server" Text="Asociar usuario a perfil" />
          </div>
          <div class="checkbox">
            <asp:CheckBox ID="chk_PrivUsrClient" runat="server" Text="Solo a usuarios clientes" />
          </div>
          <div class="checkbox">
            <asp:CheckBox ID="chk_PrivUsrSite" runat="server" Text="Solo a usuarios del sitio" />
          </div>
          <div class="checkbox">
            <asp:CheckBox ID="chk_perfil" runat="server" Text="Página perfil usuario" />
          </div>
          <div class="checkbox">
            <asp:CheckBox ID="chk_registrate" runat="server" Text="Página de registrate" />
          </div>
          <div class="checkbox">
            <asp:CheckBox ID="chk_olvclave" runat="server" Text="Página olvido clave" />
          </div>
          <div class="checkbox">
            <asp:CheckBox ID="chk_rstclave" runat="server" Text="Página restablece clave" />
          </div>
          <div class="checkbox">
            <asp:CheckBox ID="chk_login" runat="server" Text="Página de login" />
          </div>
          <div class="checkbox">
            <asp:CheckBox ID="chk_pagexito" runat="server" Text="Página de Exito" />
          </div>
          <div class="checkbox">
            <asp:CheckBox ID="chk_pagefotos" runat="server" Text="Página de Fotos" />
          </div>
          <div class="checkbox">
            <asp:CheckBox ID="chk_ini_nod_phone" runat="server" Text="Página inicio phone" />
          </div>
          <div class="checkbox">
            <asp:CheckBox ID="chk_prf_nod_phone" runat="server" Text="Página perfil phone" />
          </div>
          <div class="checkbox">
            <asp:CheckBox ID="chk_cont_nod_phone" runat="server" Text="Página Contenido Phone" />
          </div>
        </div>
      </div>
      <div class="row">
        <h3>Configuración Analytics</h3>
        <div class="form-group">
          <asp:Label ID="lblDescripcion" runat="server" Text="Descripción"></asp:Label>
          <asp:TextBox ID="textDescripcion" runat="server" CssClass="form-control" Columns="80" Rows="14" TextMode="MultiLine"></asp:TextBox>
        </div>
        <div class="form-group">
          <asp:Label ID="lblTitHeader" runat="server" Text="Titulo Header"></asp:Label>
          <asp:TextBox ID="txtTitHeader" Columns="80" Rows="14" CssClass="form-control" TextMode="MultiLine" runat="server"></asp:TextBox>
        </div>
        <div class="form-group">
          <asp:Label ID="lblKeyWords" runat="server" Text="KeyWords"></asp:Label>
          <asp:TextBox ID="txtKeyWords" Columns="80" Rows="14" CssClass="form-control" TextMode="MultiLine" runat="server"></asp:TextBox>
        </div>
      </div>
    </div>
  </form>
</body>
</html>
