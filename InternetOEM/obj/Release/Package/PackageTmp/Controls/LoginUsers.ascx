<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoginUsers.ascx.cs"
  Inherits="ICommunity.Controls.LoginUsers" %>
<asp:Panel ID="panel" runat="server" DefaultButton="btnAceptar">
  <table id="TBLLogin" cellpadding="0" cellspacing="0" width="100%">
    <tr>
      <td valign="top">
        <div class="lblTitle">
          <asp:Label ID="lblTitle" runat="server" Text="Inicia sesión" Visible="true"></asp:Label></div>
        </div>
      </td>
    </tr>
    <tr>
      <td valign="top" align="center">
        <div id="idCntrUsrLogin" runat="server">
          <div id="context-login">
            <div class="objLgn">
              <div class="lblLogin">
                <asp:Label ID="lblLogin" runat="server" Text="" Visible="true"></asp:Label></div>
              <asp:TextBox ID="txtLogin" runat="server" CssClass="inptexto"></asp:TextBox>
            </div>
            <div class="objLgn">
              <div class="lblLogin">
                <asp:Label ID="lblPassword" runat="server" Text="" Visible="true"></asp:Label></div>
              <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="inptexto"></asp:TextBox>
            </div>
            <div class="objbtnLogin">
              <asp:Button ID="btnAceptar" runat="server" Text="" CssClass="btnAceptar" OnClick="btnAceptar_Click" />
            </div>
          </div>
          <div id="context-login-boton">
            <asp:CheckBox ID="chkRememberLogin" CssClass="lblchkRemeber" runat="server" Text="" />
            <asp:LinkButton ID="lnkOlvidoPass" CssClass="lnkLvP" runat="server" Text="" OnClick="lnkOlvidoPass_Click"></asp:LinkButton>
          </div>
        </div>
      </td>
    </tr>
  </table>
</asp:Panel>
