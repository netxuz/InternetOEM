<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CreateUsers.ascx.cs"
  Inherits="ICommunity.Controls.CreateUsers" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:HiddenField ID="CodUsuario" runat="server" />
<div class="row">&nbsp;</div>
<div id="row">
  <asp:Button ID="btnGrabar" runat="server" CssClass="btn btn-primary" OnClick="btnGrabar_Click" />
  <asp:Button ID="btnNKeyCliente" runat="server" CssClass="btn btn-primary" Text="Asocia Clientes" OnClick="btnNKeyCliente_Click" Visible="false" />
  <asp:Button ID="btnHolding" runat="server" CssClass="btn btn-primary" Text="Asocia Holding" OnClick="btnHolding_Click" Visible="false" />
  <hr />
</div>
<%--<div class="">
  <asp:Label ID="lblErrorMsg" CssClass="alert alert-warning" runat="server"></asp:Label>
</div>--%>
<div class="row">
  <asp:ValidationSummary ID="valSum" CssClass="alert alert-warning" DisplayMode="SingleParagraph" HeaderText="Existen campos que deben ser completados " runat="server" />
</div>
<div class="row">
  <h3>Datos Básicos</h3>
  <div class="form-group">
    <asp:Label ID="lblNomUsuario" runat="server"></asp:Label>
    <asp:TextBox ID="txtNomUsuario" runat="server" CssClass="form-control"></asp:TextBox>
    <asp:RequiredFieldValidator ID="valNomUsuario" runat="server" ControlToValidate="txtNomUsuario" Display="Static" CssClass="MsgError" ErrorMessage="" Text="*"></asp:RequiredFieldValidator>
  </div>
  <div class="form-group" id="idApellido" runat="server">
    <asp:Label ID="lblApeUsuario" runat="server"></asp:Label>
    <asp:TextBox ID="txtApeUsuario" runat="server" CssClass="form-control"></asp:TextBox>
    <asp:RequiredFieldValidator ID="valApeUsuario" runat="server" ControlToValidate="txtApeUsuario" Display="Static" CssClass="MsgError" ErrorMessage="" Text="*"></asp:RequiredFieldValidator>
  </div>
  <div class="form-group">
    <asp:Label ID="lblLogin" runat="server"></asp:Label>
    <asp:TextBox ID="txtLoginAlmacenado" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtLogin" runat="server" CssClass="form-control"></asp:TextBox>
    <asp:RequiredFieldValidator ID="valLogin" runat="server" ControlToValidate="txtLogin" Display="Static" CssClass="MsgError" ErrorMessage="" Text="*"></asp:RequiredFieldValidator>
    <asp:CustomValidator ID="custValLogin" runat="server" ErrorMessage="" Text="*" ControlToValidate="txtLogin" Display="Static" OnServerValidate="ServerValidationLogin"></asp:CustomValidator>
  </div>
  <div class="form-group">
    <asp:Label ID="lblClave" runat="server"></asp:Label>
    <asp:TextBox ID="txtClave" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
    <asp:RequiredFieldValidator ID="valTxtClave" runat="server" ControlToValidate="txtClave" Display="Static" CssClass="MsgError" ErrorMessage="" Text="*"></asp:RequiredFieldValidator>
  </div>
  <div class="form-group">
    <asp:Label ID="lblRepClave" runat="server"></asp:Label>
    <asp:TextBox ID="txtRepClave" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
    <asp:RequiredFieldValidator ID="valTxtRepClave" runat="server" ControlToValidate="txtRepClave" Display="Static" CssClass="MsgError" ErrorMessage="" Text="*"></asp:RequiredFieldValidator>
    <asp:CompareValidator ID="valComparar" runat="server" ControlToValidate="txtRepClave" ControlToCompare="txtClave" Display="Static" ErrorMessage="" Text="*"></asp:CompareValidator>
  </div>
  <div class="form-group">
    <asp:Label ID="lblEmlUsuario" runat="server"></asp:Label>
    <asp:TextBox ID="txtEmlUsuario" runat="server" CssClass="form-control"></asp:TextBox>
    <asp:RequiredFieldValidator ID="valEmlUsuario" runat="server" ControlToValidate="txtEmlUsuario" Display="Static" CssClass="MsgError" ErrorMessage="" Text="*"></asp:RequiredFieldValidator>
    <%--<asp:CustomValidator ID="valEmlUsuarioVal" runat="server" ErrorMessage="" Text="*" ControlToValidate="txtEmlUsuario" Display="Static" OnServerValidate="ServerValidationEml"></asp:CustomValidator>--%>
  </div>
  <div class="form-group">
    <asp:Label ID="lblNumeroCliente" runat="server"></asp:Label>
    <asp:TextBox ID="txtNumeroCliente" runat="server" CssClass="form-control"></asp:TextBox>
  </div>
  <div class="form-group" id="idFono" runat="server">
    <asp:Label ID="lblFonoUsuario" runat="server"></asp:Label>
    <asp:TextBox ID="txtFonoUsuario" runat="server" CssClass="form-control"></asp:TextBox>
    <asp:RequiredFieldValidator ID="valFonoUsuario" runat="server" ControlToValidate="txtFonoUsuario" Display="Static" CssClass="MsgError" ErrorMessage="" Text="*"></asp:RequiredFieldValidator>
  </div>
  <div class="form-group" id="idDestacado" runat="server" visible="false">
    <asp:Label ID="lblDestUsuario" runat="server"></asp:Label>
    <div class="checkbox">
      <asp:CheckBox ID="check_destacado" runat="server" />
    </div>
  </div>
  <div class="form-group" id="idTipoUsuario" runat="server">
    <asp:Label ID="lblTipoUsuario" runat="server"></asp:Label>
    <asp:DropDownList ID="rdCmbTipoUsuario" runat="server" CssClass="form-control">
    </asp:DropDownList>
  </div>
  <div class="form-group" id="idEstUsuario" runat="server">
    <asp:Label ID="lblEstUsuario" runat="server"></asp:Label>
    <asp:DropDownList ID="rdCmbEstUsuario" runat="server" CssClass="form-control"></asp:DropDownList>
  </div>
  <div class="form-group" id="idCertificado" runat="server" visible="false">
    <asp:Label ID="lblCertificado" runat="server"></asp:Label>
    <div class="checkbox">
      <asp:CheckBox ID="check_certificado" runat="server" />
    </div>
  </div>
</div>
<div class="row">
  <div id="idUsrInf" runat="server" class="ContenidoTemplate">
  </div>
</div>
