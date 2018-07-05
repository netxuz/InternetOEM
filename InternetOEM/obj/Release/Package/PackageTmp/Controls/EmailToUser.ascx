<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EmailToUser.ascx.cs"
  Inherits="ICommunity.Controls.EmailToUser" %>
<div id="bloquehistoria" runat="server" visible="false">
</div>
<div id="bloqueusuarios" runat="server" visible="false">
</div>
<div id="bloquemensaje" runat="server" visible="false">
  <div clss="txtMessaje">
    <asp:TextBox ID="txtCuerpo" runat="server" TextMode="MultiLine" Rows="10" Columns="40"></asp:TextBox>
  </div>
  <div class="boton">
    <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" 
      onclick="btnAceptar_Click" />
  </div>
</div>