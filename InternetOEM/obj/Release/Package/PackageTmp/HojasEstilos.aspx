<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HojasEstilos.aspx.cs" Inherits="ICommunity.HojasEstilos" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta charset="utf-8">
  <meta http-equiv="X-UA-Compatible" content="IE=edge">
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <title></title>
  <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
  <link rel="stylesheet" href="css/styleadmin.css">
  <link rel="stylesheet" type="text/css" href="css/stylesdebtcontrol.css" media="screen" />
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
</head>
<body class="bodyadm">
  <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager" runat="server">
    </asp:ScriptManager>
    <div class="container">
      <div class="row">&nbsp;</div>
      <div class="row">
        <asp:Button ID="btnGrabar" runat="server" CssClass="btn btn-primary" Text="" OnClick="btnGrabar_Click" />
        <hr />
      </div>
      <div class="row">
        <div class="form-group">
          <asp:Label ID="lblCss" runat="server" Text=""></asp:Label>
          <telerik:RadUpload ID="rdUploadFileCss" runat="server" ControlObjectsVisibility="None"
            MaxFileInputsCount="1" Language="es-CL">
          </telerik:RadUpload>
        </div>
        <div class="form-group">
          <asp:Label ID="lblZip" runat="server" Text=""></asp:Label>
          <telerik:RadUpload ID="rdUploadZip" runat="server" ControlObjectsVisibility="None"
            MaxFileInputsCount="1" Language="es-CL">
          </telerik:RadUpload>
        </div>
        <div class="form-group">
          <telerik:RadListBox ID="rdListAsignados" runat="server" Width="200px" Height="200px"
            SelectionMode="Multiple" AllowTransfer="true" TransferToID="rdListDisponibles">
          </telerik:RadListBox>
        </div>
        <div class="form-group">
          <telerik:RadListBox ID="rdListDisponibles" runat="server" Width="200px" Height="200px"
            SelectionMode="Multiple" AllowTransfer="true" TransferToID="rdListAsignados" AllowDelete="true" AutoPostBackOnDelete="true" OnDeleted="rdListDisponibles_Deleted">
          </telerik:RadListBox>
        </div>
      </div>
    </div>
  </form>
</body>
</html>
