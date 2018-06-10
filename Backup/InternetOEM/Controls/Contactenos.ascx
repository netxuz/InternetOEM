<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Contactenos.ascx.cs"
  Inherits="ICommunity.Controls.Contactenos" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<telerik:RadWindowManager ID="RadWindowManager" Skin="Black" runat="server">
    <Windows>
      <telerik:RadWindow ID="RadWindow" runat="server">
      </telerik:RadWindow>
    </Windows>
  </telerik:RadWindowManager>
<asp:Panel ID="CntPanel" runat="server" DefaultButton="btnAceptar">
  <div id="dvCntTitle">
    <asp:Label ID="lblCntTitle" runat="server" Text=""></asp:Label>
  </div>
  <div id="dvCntDescripcion">
    <asp:Label ID="lblCntDes" runat="server"></asp:Label>
  </div>
  <div id="dvContactenos">
    <div class="divCntLine">
      <div class="lblData">
        <asp:Label ID="lblNombre" runat="server"></asp:Label>
      </div>
      <div class="txtBox">
        <asp:TextBox ID="txtNombre" runat="server" CssClass="cTxtBox"></asp:TextBox>
        <asp:RequiredFieldValidator ID="valNombre" runat="server" ControlToValidate="txtNombre"
          Display="Static" ErrorMessage="*"></asp:RequiredFieldValidator>
      </div>
    </div>
    <div class="divCntLine">
      <div class="lblData">
        <asp:Label ID="lblEmail" runat="server"></asp:Label></div>
      <div class="txtBox">
        <asp:TextBox ID="txtEmail" runat="server" CssClass="cTxtBox"></asp:TextBox>
        <asp:RequiredFieldValidator ID="valEmail" runat="server" ControlToValidate="txtEmail"
          Display="Static" ErrorMessage="*"></asp:RequiredFieldValidator>
        <asp:CustomValidator ID="valtxtEmailVal" runat="server" ErrorMessage="*" ControlToValidate="txtEmail"
          Display="Static" OnServerValidate="ServerValidationEmail"></asp:CustomValidator>
      </div>
    </div>
    <div class="divCntLine">
      <div class="lblData">
        <asp:Label ID="lblFono" runat="server"></asp:Label></div>
      <div class="txtBox">
        <asp:TextBox ID="txtFono" runat="server" CssClass="cTxtBox"></asp:TextBox></div>
    </div>
    <div class="divCntLine">
      <div class="lblData">
        <asp:Label ID="lblComentario" runat="server"></asp:Label></div>
      <div class="txtBox">
        <asp:TextBox ID="txtComentario" runat="server" CssClass="cTxtBox" TextMode="MultiLine"
          Rows="7" Columns="40"></asp:TextBox>
        <asp:RequiredFieldValidator ID="valComentario" runat="server" ControlToValidate="txtComentario"
          Display="Static" ErrorMessage="*"></asp:RequiredFieldValidator>
      </div>
    </div>
    <div id="divCntLine">
      <asp:Button ID="BtnAceptar" runat="server" CssClass="btnAceptar" OnClick="BtnAceptar_Click" />
    </div>
  </div>
</asp:Panel>
