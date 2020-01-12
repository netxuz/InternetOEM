<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frame.aspx.cs" Inherits="ICommunity.frame" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/tr/xhtml11/dtd/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <meta http-equiv="X-UA-Compatible" content="IE=8" />
  <title>Opensite 6.0</title>
  <style type="text/css">
    html, body, form
    {
      height: 100%;
      margin: 0px;
      padding: 0px;
    }
  </style>
</head>
<body id="open" scroll="no">
  <form id="form1" runat="server">

  <asp:ScriptManager ID="ScriptManager1" runat="server">
  </asp:ScriptManager>

      <div id="decorationZone11111" style="height: 100%;">
        <telerik:RadSplitter ID="Radsplitter5" runat="server"
          Width="100%" Height="100%" Orientation="Horizontal">
          <telerik:RadPane ID="rpnBarraSuperior" runat="server" Height="71">
            
          </telerik:RadPane>
          <telerik:RadPane ID="rpnBarraInferior" runat="server" Scrolling="None">
            <telerik:RadSplitter ID="Radsplitter6" runat="server" CollapseMode="None">
              <telerik:RadPane ID="RadPane1" runat="server" Width="22" Scrolling="None">
                
              </telerik:RadPane>
              <telerik:RadSplitBar ID="Radsplitbar6" runat="server" CollapseMode="Forward" />
              <telerik:RadPane ID="rpnNavigate" CssClass="aa" runat="server">
              </telerik:RadPane>
            </telerik:RadSplitter>
          </telerik:RadPane>
        </telerik:RadSplitter>
      </div>
    
  
  <asp:HiddenField ID="CodSistema" runat="server" />
  </form>
</body>
</html>
