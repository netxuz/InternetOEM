<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="ICommunity.WebForm1" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title></title>
  <style type="text/css">
      .rtsImg {
      margin: 2px 4px 0 0 !important;
    }

    .restrictionZone {
      width: 800px;
      height: 600px;
      border: solid 1px #000;
    }
  </style>
</head>
<body>
  <form id="form1" runat="server">
    <div>
      <telerik:RadScriptManager ID="RadScriptManager1" runat="server"></telerik:RadScriptManager>
      <table cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
        <tr>
          <td>
            <div id="RestrictionZone" class="restrictionZone">
              <!-- / -->
            </div>
          </td>
        </tr>
      </table>
      <telerik:RadWindowManager EnableShadow="true" ID="RadWindowManager" DestroyOnClose="true"
        RestrictionZoneID="RestrictionZone" Opacity="99" runat="server">
        <Windows>
          <telerik:RadWindow ID="RadWindow1" VisibleOnPageLoad="true" Title="Google" NavigateUrl="http://sys.debtcontrol.cl/paneldecontrol/default.aspx" runat="server">
          </telerik:RadWindow>
        </Windows>
      </telerik:RadWindowManager>


    </div>
  </form>
</body>
</html>
