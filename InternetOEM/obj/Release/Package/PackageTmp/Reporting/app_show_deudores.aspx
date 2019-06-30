<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="app_show_deudores.aspx.cs" Inherits="ICommunity.Reporting.app_show_deudores" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta charset="utf-8">
  <meta http-equiv="X-UA-Compatible" content="IE=edge">
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <title></title>
  <link rel="stylesheet" href="../css/bootstrap.min.css">
  <link rel="stylesheet" type="text/css" href="../css/stylesdebtcontrol.css" media="screen" />
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
  <script>
    function onClose() {
      parent.$.fancybox.close();
    }
  </script>
</head>
<body>
  <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:HiddenField ID="hdd_arrNkeyCliente" runat="server" />
    <div class="container">
      <div class="blq_tile">
        <asp:label ID="lblTitle" runat="server" CssClass="lblTitle" Text="DEUDORES"></asp:label>
      </div>
      <div class="blq_search">
        <telerik:RadTextBox ID="rdTxtNombreDeudor" CssClass="control-text-search" runat="server" placeholder="Ingresa Deudor"></telerik:RadTextBox>
      </div>
      <div class="blq_search">
        <telerik:RadTextBox ID="rdTxtCodigo" CssClass="control-text-search" runat="server" placeholder="Ingresa Código Deudor"></telerik:RadTextBox>
      </div>
      <div class="blq_btn_search">
        <asp:Button ID="idBuscar" runat="server" Text="Buscar" CssClass="btn btn-lg btn-primary btn-block" Width="100px" OnClick="idBuscar_Click" />
      </div>
      <div style="width: 100%">
        <telerik:RadGrid ID="rdGridDeudores" runat="server" OnNeedDataSource="rdGridDeudores_NeedDataSource" OnItemCommand="rdGridDeudores_ItemCommand" AllowPaging="true" AllowSorting="true" ShowStatusBar="true" PageSize="10" GridLines="None" AllowAutomaticUpdates="true" AllowAutomaticInserts="true" AllowAutomaticDeletes="true"
          Skin="Sitefinity">
          <PagerStyle Mode="NextPrevAndNumeric" />
          <MasterTableView PageSize="10" DataKeyNames="lngCodDeudor, RazonSocial" AutoGenerateColumns="false" ShowHeader="true" TableLayout="Fixed" ShowHeadersWhenNoRecords="true">
            <Columns>
              <telerik:GridButtonColumn CommandName="Select" Text="Seleccione" UniqueName="Select">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" Width="50px" />
              </telerik:GridButtonColumn>
              <telerik:GridBoundColumn DataField="RazonSocial" HeaderText="Razón Social"
                UniqueName="RazónSocial">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
              </telerik:GridBoundColumn>
              <telerik:GridBoundColumn DataField="RUT" HeaderText="RUT"
                UniqueName="RUT">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
              </telerik:GridBoundColumn>
            </Columns>
          </MasterTableView>
        </telerik:RadGrid>
        <asp:HiddenField ID="hdd_coddeudor" runat="server" Value="" />
        <asp:HiddenField ID="hdd_razonsocial" runat="server" Value="" />
      </div>
    </div>
  </form>
</body>
</html>
