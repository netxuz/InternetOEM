<%@ Page Language="C#" AutoEventWireup="true" Inherits="OnlineServices.Web.UI.Render" %>

<script runat="server">
  OnlineServices.Method.Web oWeb = new OnlineServices.Method.Web();
  
  void Page_Init(object sender, EventArgs e)
  {
    CodNodo = oWeb.GetData("CodNodo");
  }
</script>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="RenderPreview" runat="server">
    
    </div>
    </form>
</body>
</html>
