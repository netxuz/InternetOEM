<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HojasEstilos.aspx.cs" Inherits="ICommunity.HojasEstilos" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Hojas de Estilos</title>
</head>
<body>
  <form id="form1" runat="server">
  <asp:ScriptManager ID="ScriptManager" runat="server">
  </asp:ScriptManager>
  <div class="entorno">
    <div class="panel">
      <div class="boton">
        <asp:Button ID="btnGrabar" runat="server" Text="" OnClick="btnGrabar_Click" />
      </div>
      <div>
        <asp:Label ID="lblCss" runat="server" Text=""></asp:Label>
        <telerik:RadUpload ID="rdUploadFileCss" runat="server" ControlObjectsVisibility="None"
          MaxFileInputsCount="1" Language="es-CL">
        </telerik:RadUpload>
      </div>
      <div>
        <asp:Label ID="lblZip" runat="server" Text=""></asp:Label>
        <telerik:RadUpload ID="rdUploadZip" runat="server" ControlObjectsVisibility="None"
          MaxFileInputsCount="1" Language="es-CL">
        </telerik:RadUpload>
      </div>
      <div>
        <telerik:RadListBox ID="rdListAsignados" runat="server" Width="200px" Height="200px"
          SelectionMode="Multiple" AllowTransfer="true" TransferToID="rdListDisponibles">
        </telerik:RadListBox>
      </div>
      <div>
        <telerik:RadListBox ID="rdListDisponibles" runat="server" Width="200px" Height="200px"
          SelectionMode="Multiple" AllowTransfer="true" TransferToID="rdListAsignados" AllowDelete="true" AutoPostBackOnDelete="true" OnDeleted="rdListDisponibles_Deleted">
        </telerik:RadListBox>
      </div>
    </div>
  </div>
  </form>
</body>
</html>
