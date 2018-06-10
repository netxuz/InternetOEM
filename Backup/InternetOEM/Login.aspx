<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ICommunity.Login" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>ICommunity</title>
</head>
<body>
  <form id="form1" runat="server">
  <asp:scriptmanager ID="Scriptmanager1" runat="server"></asp:scriptmanager>
  <div id="idCntrUsrLogin" runat="server">
    <div>
      <asp:Label ID="lLogin" runat="server" Text="" Visible="false"></asp:Label></div>
    <div>
      <asp:TextBox ID="txtLogin" runat="server" CssClass="inptexto"></asp:TextBox></div>
    <div>
      <asp:Label ID="lPassword" runat="server" Text="" Visible="false"></asp:Label></div>
    <div>
      <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="inptexto"></asp:TextBox></div>
    <div>
      <asp:Button ID="btnAceptar" runat="server" Text="" CssClass="btnAceptar" OnClick="btnAceptar_Click" /></div>
  </div>
  <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
  <Windows>
    <telerik:RadWindow>
    </telerik:RadWindow>
  </Windows>
  </telerik:RadWindowManager>
  </form>
</body>
</html>
