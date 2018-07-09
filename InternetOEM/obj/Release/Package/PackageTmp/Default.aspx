<%@ Page Language="C#" AutoEventWireup="true" Inherits="OnlineServices.Web.UI.Render" %>

<!DOCTYPE html>

<script runat="server">
  OnlineServices.Method.Web oWeb = new OnlineServices.Method.Web();

  void Page_Init(object sender, EventArgs e)
  {
    CodNodo = oWeb.GetData("hddCodNodo");
  }
</script>

<html>
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
