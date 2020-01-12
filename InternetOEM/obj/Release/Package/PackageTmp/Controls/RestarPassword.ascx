<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RestarPassword.ascx.cs"
  Inherits="ICommunity.Controls.RestarPassword" %>
<div id="context_rstpwd" runat="server">
  <asp:Panel ID="panel01" runat="server" DefaultButton="btnRstPpwd">
    <div id="context_lblmsnrstpwd">
      <asp:Label ID="lblmsnrstpwd" runat="server" Text=""></asp:Label>
    </div>
    <div id="context_txtmsgreingnewpwd">
      <asp:Label ID="lblmsgreingnewpwd" runat="server" Text=""></asp:Label>
    </div>
    <div id="idCntrUsrLogin" runat="server">
      <div id="context-RstPwd">
        <div id="context_txtRstPwd01">
          <div id="context_lblRstPwd01">
            <asp:Label ID="lblRstPwd01" runat="server" Text=""></asp:Label>
          </div>
          <asp:TextBox ID="txtRstPwd01" TextMode="Password" CssClass="inptexto" runat="server"></asp:TextBox>
          <asp:RequiredFieldValidator ID="valTxtClave" runat="server" ControlToValidate="txtRstPwd01"
            Display="Static" ErrorMessage="" Text="*"></asp:RequiredFieldValidator>
        </div>
        <div id="context_txtRstPwd02">
          <div id="context_lblRstPwd02">
            <asp:Label ID="lblRstPwd02" runat="server" Text=""></asp:Label>
          </div>
          <asp:TextBox ID="txtRstPwd02" TextMode="Password" CssClass="inptexto" runat="server"></asp:TextBox>
          <asp:RequiredFieldValidator ID="valTxtRepClave" runat="server" ControlToValidate="txtRstPwd02"
            Display="Static" ErrorMessage="" Text="*"></asp:RequiredFieldValidator>
          <asp:CompareValidator ID="valComparar" runat="server" ControlToValidate="txtRstPwd02"
            ControlToCompare="txtRstPwd01" Display="Static" ErrorMessage="" Text="*"></asp:CompareValidator>
        </div>
        <div id="context_btnrstpwd">
          <asp:Button ID="btnRstPpwd" runat="server" Text="" CssClass="btnAceptar" OnClick="btnRstPpwd_Click" />
        </div>
      </div>
    </div>
  </asp:Panel>
</div>
<div id="context_olvpwd_resp" runat="server" visible="false">
  <asp:Panel ID="panel2" runat="server" DefaultButton="btnAceptar">
    <div id="context_lblmsnrstpwd_resp">
      <asp:Label ID="lblmsnrstpwd_resp" runat="server" Text=""></asp:Label>
    </div>
    <div id="context_btnAceptar">
      <asp:Button ID="btnAceptar" runat="server" Text="" CssClass="cssBtnOlvPwd" OnClick="btnAceptar_Click" />
    </div>
  </asp:Panel>
</div>
