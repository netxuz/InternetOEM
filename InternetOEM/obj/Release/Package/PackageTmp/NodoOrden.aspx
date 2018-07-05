<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NodoOrden.aspx.cs" Inherits="ICommunity.NodoOrden" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Ordena Nodos</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:HiddenField ID="CodNodo" runat="server" />
    <div>
      <asp:Button ID="btnGrabar" runat="server" Text="" OnClick="btnGrabar_Click" />
    </div>
    <div>
      <telerik:RadListBox ID="rdListNodos" AllowReorder="true" Width="200px" runat="server">
      </telerik:RadListBox>
    </div>
    </form>
</body>
</html>
