<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OlvidoPassword.ascx.cs"
  Inherits="ICommunity.Controls.OlvidoPassword" %>
<div id="context_olvpwd" runat="server">
  <div id="context_olvpwd_title">
    <asp:Label ID="ttlmsnolvpwd" runat="server" Text=""></asp:Label>
  </div>
  <div id="context_olvpwd_master">
    <div id="context_lblmsnoextpwd" runat="server" visible="false">
      <asp:Label ID="lblmsnoextpwd" runat="server" Text=""></asp:Label>
    </div>
    <div id="context_lblmsnolvpwd">
      <asp:Label ID="lblmsnolvpwd" runat="server" Text=""></asp:Label>
    </div>
    <div id="context_lblemlolvpwd">
      <asp:Label ID="lblemlolvpwd" runat="server" Text=""></asp:Label>
    </div>
    <div id="context_txtemlolvpwd">
      <asp:TextBox ID="txtEmlOlvPwd" CssClass="csstxtemlolvpwd" runat="server"></asp:TextBox>
      <asp:RequiredFieldValidator ID="valEmlUsuario" runat="server" ControlToValidate="txtEmlOlvPwd"
        Display="Static" ErrorMessage="" Text="*"></asp:RequiredFieldValidator>
      <asp:CustomValidator ID="valEmlUsuarioVal" runat="server" ErrorMessage="" Text="*"
        ControlToValidate="txtEmlOlvPwd" Display="Static" OnServerValidate="ServerValidationEml"></asp:CustomValidator>
    </div>
    <div id="context_btnolvpwd">
      <asp:Button ID="btnOlvPpwd" runat="server" Text="" CssClass="cssBtnOlvPwd" OnClick="btnOlvPpwd_Click" />
    </div>
  </div>
</div>
<div id="context_olvpwd_resp" runat="server" visible="false">
  <div id="context_olvpwd_title_resp">
    <asp:Label ID="ttlmsnolvpwd_resp" runat="server" Text=""></asp:Label>
  </div>
  <div id="context_olvpwd_master_resp">
    <div id="context_lblmsnolvpwd_resp">
      <asp:Label ID="lblmsnolvpwd_resp" runat="server" Text=""></asp:Label>
    </div>
  </div>
</div>
</div> 