<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Usuario.aspx.cs" Inherits="ICommunity.Usuario" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="Controls/CreateUsers.ascx" TagName="CreateUsers" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta http-equiv="X-UA-Compatible" content="IE=8" />
  <title></title>
</head>
<body>
  <form id="form1" runat="server">
  <asp:ScriptManager ID="ScriptManager" runat="server">
  </asp:ScriptManager>
  <uc1:CreateUsers ID="CreateUsers" runat="server" />
  <div id="loadImage" runat="server">
  </div>
  <telerik:RadWindowManager ID="RadWindowManager" runat="server">
    <Windows>
      <telerik:RadWindow ID="RadWindow" runat="server">
      </telerik:RadWindow>
    </Windows>
  </telerik:RadWindowManager>
  </form>
  
</body>
</html>
