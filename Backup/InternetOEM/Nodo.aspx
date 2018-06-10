<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Nodo.aspx.cs" Inherits="ICommunity.Nodo" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Nodo</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:HiddenField ID="CodNodo" runat="server" />
    <asp:HiddenField ID="CodNodoRel" runat="server" />
    <asp:HiddenField ID="OrdNodo" runat="server" />
    <asp:HiddenField ID="sAccion" runat="server" />
    <div class="entorno">
      <div class="panel">
        <div>
          <asp:Label ID="lblTitulo" runat="server" Text="Titulo"></asp:Label>
        </div>
        <div>
          <asp:TextBox ID="txtTitulo" runat="server"></asp:TextBox>
        </div>
        <div>
          <asp:Button ID="btnGrabar" runat="server" Text="Grabar" OnClick="btnGrabar_Click" />
        </div>
        <div>
          <asp:Label ID="lblTemplate" runat="server" Text="Template"></asp:Label>
        </div>
        <div>
          <telerik:RadComboBox ID="rdCmbTemplate" runat="server">
          </telerik:RadComboBox>
        </div>
        <div>
          <asp:Label ID="lblEstado" runat="server" Text="Estado"></asp:Label>
        </div>
        <div>
          <telerik:RadComboBox ID="rdCmbEstado" runat="server">
            <Items>
              <telerik:RadComboBoxItem Text="Vigente" Value="V" />
              <telerik:RadComboBoxItem Text="No Vigente" Value="N" />
              <telerik:RadComboBoxItem Text="Phone" Value="P" />
              <telerik:RadComboBoxItem Text="Oculto" Value="O" />
            </Items>
          </telerik:RadComboBox>
        </div>
        <div>
          <asp:Label ID="lblInicio" runat="server" Text="Inicio"></asp:Label>
        </div>
        <div>
          <asp:CheckBox ID="chk_inicio" runat="server" />
        </div>
        <div>
          <asp:Label ID="lblPerfil" runat="server" Text="Perfil"></asp:Label>
        </div>
        <div>
          <asp:CheckBox ID="chk_perfil" runat="server" />
        </div>
        <div>
          <asp:Label ID="lblAsocUsrPerfil" runat="server" Text="Asociar usuario a perfil"></asp:Label>
        </div>
        <div>
          <asp:CheckBox ID="chk_asocusrperfil" runat="server" />
        </div>
        <div>
          <asp:Label ID="lblPrivUsrClient" runat="server" Text="Solo a usuarios clientes"></asp:Label>
        </div>
        <div>
          <asp:CheckBox ID="chk_PrivUsrClient" runat="server" />
        </div>
        <div>
          <asp:Label ID="lblPrivUsrSite" runat="server" Text="Solo a usuarios del sitio"></asp:Label>
        </div>
        <div>
          <asp:CheckBox ID="chk_PrivUsrSite" runat="server" />
        </div>
        <div>
          <asp:Label ID="lblContenido" runat="server" Text="Contenidos"></asp:Label>
        </div>
        <div>
          <asp:CheckBox ID="chk_contenido" runat="server" />
        </div>
        <div>
          <asp:Label ID="lblPrivado" runat="server" Text="Privado"></asp:Label>
        </div>
        <div>
          <asp:CheckBox ID="chk_privado" runat="server" />
        </div>
        <div>
          <asp:Label ID="lblOlvClave" runat="server" Text="Olvido Clave"></asp:Label>
        </div>
        <div>
          <asp:CheckBox ID="chk_olvclave" runat="server" />
        </div>
        <div>
          <asp:Label ID="lblRstClave" runat="server" Text="Restablece Clave"></asp:Label>
        </div>
        <div>
          <asp:CheckBox ID="chk_rstclave" runat="server" />
        </div>
        <div>
          <asp:Label ID="Label1" runat="server" Text="Login"></asp:Label>
        </div>
        <div>
          <asp:CheckBox ID="chk_login" runat="server" />
        </div>
        <div>
          <asp:Label ID="lblPoltSecure" runat="server" Text="Politicas de Seguridad"></asp:Label>
        </div>
        <div>
          <asp:CheckBox ID="chk_poltsecure" runat="server" />
        </div>
        <div>
          <asp:Label ID="lblTermUse" runat="server" Text="Terminos de Uso"></asp:Label>
        </div>
        <div>
          <asp:CheckBox ID="chk_termuse" runat="server" />
        </div>
        <div>
          <asp:Label ID="lblRegistrate" runat="server" Text="Registrate"></asp:Label>
        </div>
        <div>
          <asp:CheckBox ID="chk_registrate" runat="server" />
        </div>
        <div>
          <asp:Label ID="lblPagExito" runat="server" Text="Pagina Exito"></asp:Label>
        </div>
        <div>
          <asp:CheckBox ID="chk_pagexito" runat="server" />
        </div>
        <div>
          <asp:Label ID="lblPhotos" runat="server" Text="Pagina Fotos"></asp:Label>
        </div>
        <div>
          <asp:CheckBox ID="chk_pagefotos" runat="server" />
        </div>
        <div>
          <asp:Label ID="lblNodIniPhone" runat="server" Text="Nodo Inicio Phone"></asp:Label>
        </div>
        <div>
          <asp:CheckBox ID="chk_ini_nod_phone" runat="server" />
        </div>
        <div>
          <asp:Label ID="lblNodProfilePhone" runat="server" Text="Nodo Perfil Phone"></asp:Label>
        </div>
        <div>
          <asp:CheckBox ID="chk_prf_nod_phone" runat="server" />
        </div>
        <div>
          <asp:Label ID="lblNodContPhone" runat="server" Text="Nodo Contenido Phone"></asp:Label>
        </div>
        <div>
          <asp:CheckBox ID="chk_cont_nod_phone" runat="server" />
        </div>
      </div>
      <div class="panel">
        <div>
          <asp:Label ID="lblDescripcion" runat="server" Text="Descripción"></asp:Label>
        </div>
        <div>
          <asp:TextBox ID="textDescripcion" runat="server" Columns="80" Rows="14" TextMode="MultiLine"></asp:TextBox>
        </div>
      </div>
      <div class="panel">
        <div>
          <asp:Label ID="lblTitHeader" runat="server" Text="Titulo Header"></asp:Label>
        </div>
        <div>
          <asp:TextBox ID="txtTitHeader" Columns="80" Rows="14" TextMode="MultiLine" runat="server"></asp:TextBox>
        </div>
      </div>
      <div class="panel">
        <div>
          <asp:Label ID="lblKeyWords" runat="server" Text="KeyWords"></asp:Label>
        </div>
        <div>
          <asp:TextBox ID="txtKeyWords" Columns="80" Rows="14" TextMode="MultiLine" runat="server"></asp:TextBox>
        </div>
      </div>
    </div>
    </form>
</body>
</html>
