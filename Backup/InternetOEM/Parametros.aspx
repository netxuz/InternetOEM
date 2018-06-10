<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Parametros.aspx.cs" Inherits="ICommunity.Parametros" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Parametros</title>
</head>
<body>
  <form id="form1" runat="server">
  <asp:ScriptManager ID="ScriptManager1" runat="server">
  </asp:ScriptManager>
  <div>
    <div>
      <asp:Button ID="btnGrabar" runat="server" OnClick="btnGrabar_Click" Text="Button" />
      <asp:Button ID="btnRSS" runat="server" OnClick="btnRSS_Click" Text="Button" />
    </div>
    <div id="opciones" runat="server">
    </div>
  </div>
  <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
  <Windows>
    <telerik:RadWindow ID="rdWindow" runat="server"></telerik:RadWindow>
  </Windows>
  </telerik:RadWindowManager>
  </form>
</body>
</html>
