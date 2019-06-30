<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Contactenos.ascx.cs"
  Inherits="ICommunity.Controls.Contactenos" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<telerik:RadWindowManager ID="RadWindowManager" Skin="Black" runat="server">
  <Windows>
    <telerik:RadWindow ID="RadWindow" runat="server">
    </telerik:RadWindow>
  </Windows>
</telerik:RadWindowManager>
<asp:Panel ID="CntPanel" runat="server" CssClass="mailform" DefaultButton="btnAceptar">
  <fieldset>
    <div class="col-md-4 col-sm-4 col-xs-12 col_mod">
      <label data-add-placeholder="">
        <asp:TextBox ID="txtNombre" runat="server" placeholder="Nombre:"></asp:TextBox>
        <asp:RequiredFieldValidator ID="valNombre" runat="server" ControlToValidate="txtNombre"
          Display="Static" ErrorMessage="*"></asp:RequiredFieldValidator>
      </label>
    </div>
    <div class="col-md-4 col-sm-4 col-xs-12 col_mod">
      <label data-add-placeholder="">
        <asp:TextBox ID="txtEmail" runat="server" placeholder="E-mail:"></asp:TextBox>
        <asp:RequiredFieldValidator ID="valEmail" runat="server" ControlToValidate="txtEmail"
          Display="Static" ErrorMessage="*"></asp:RequiredFieldValidator>
        <asp:CustomValidator ID="valtxtEmailVal" runat="server" ErrorMessage="*" ControlToValidate="txtEmail"
          Display="Static" OnServerValidate="ServerValidationEmail"></asp:CustomValidator>
      </label>
    </div>
    <div class="col-md-4 col-sm-4 col-xs-12 col_mod">
      <label data-add-placeholder="">
        <asp:TextBox ID="txtFono" runat="server" placeholder="Celular:"></asp:TextBox>
      </label>
    </div>
    <div class="col-md-12 col-sm-12 col-xs-12 col_mod">
      <label data-add-placeholder="" class="textarea">
        <asp:TextBox ID="txtComentario" runat="server" TextMode="MultiLine" placeholder="Mensaje:"
          Rows="7" Columns="40"></asp:TextBox>
        <asp:RequiredFieldValidator ID="valComentario" runat="server" ControlToValidate="txtComentario"
          Display="Static" ErrorMessage="*"></asp:RequiredFieldValidator>
      </label>
      <asp:Button ID="BtnAceptar" runat="server" CssClass="btn-contactenos" OnClick="BtnAceptar_Click" />
    </div>
  </fieldset>
</asp:Panel>
