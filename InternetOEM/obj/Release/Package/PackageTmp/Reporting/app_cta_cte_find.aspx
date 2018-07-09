<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="app_cta_cte_find.aspx.cs" Inherits="ICommunity.Reporting.app_cta_cte_find" %>

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

  <script type="text/javascript" src="../Resources/fancybox/jquery.min.js"></script>
  <script type="text/javascript" src="../Resources/fancybox/jquery.mousewheel-3.0.4.pack.js"></script>
  <script type="text/javascript" src="../Resources/fancybox/jquery.fancybox-1.3.4.js"></script>
  <link rel="stylesheet" type="text/css" href="../Resources/fancybox/jquery.fancybox-1.3.4.css" media="screen" />
</head>
<body>
  <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>
    <nav class="navbar-inverse">
      <div class="container-fluid">
        <div class="navbar-header">
          <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#myNavbar">
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
          </button>
          <a class="navbar-brand" href="#">Debtcontrol</a>
        </div>
        <div class="collapse navbar-collapse" id="myNavbar">
          <ul class="nav navbar-nav">
            <li class="dropdown">
              <a class="dropdown-toggle" data-toggle="dropdown" href="#">Reportes de Pago <span class="caret"></span></a>
              <ul id="idReportePago" runat="server" class="dropdown-menu"></ul>
            </li>
            <li class="dropdown">
              <a class="dropdown-toggle" data-toggle="dropdown" href="#">Proceso de Seguimiento<span class="caret"></span></a>
              <ul id="idProcesoSeguimiento" runat="server" class="dropdown-menu"></ul>
            </li>
            <li class="dropdown">
              <a class="dropdown-toggle" data-toggle="dropdown" href="#">Cartolas<span class="caret"></span></a>
              <ul id="idCartolas" runat="server" class="dropdown-menu"></ul>
            </li>
            <li class="dropdown">
              <a class="dropdown-toggle" data-toggle="dropdown" href="#">Proceso de Normalización<span class="caret"></span></a>
              <ul id="idProcesoNormalizacion" runat="server" class="dropdown-menu"></ul>
            </li>
            <li class="dropdown">
              <a class="dropdown-toggle" data-toggle="dropdown" href="#">Indicadores Claves<span class="caret"></span></a>
              <ul id="idIndicadoresClaves" runat="server" class="dropdown-menu"></ul>
            </li>
            <li class="dropdown">
              <a class="dropdown-toggle" data-toggle="dropdown" href="#">Clasificación de Riesgo<span class="caret"></span></a>
              <ul id="IndClasificacionRiesgo" runat="server" class="dropdown-menu"></ul>
            </li>
            <li><a href="#">Panel de Control</a></li>
          </ul>
        </div>
      </div>
    </nav>

    <div class="container">
      <div class="blq_tile">
        <asp:Label ID="lblTitle" runat="server" CssClass="lblTitle" Text="CUENTA CORRIENTE"></asp:Label>
      </div>
      <div class="blq_search">
        <div><span>Deudor</span></div>
        <div></div>
        <telerik:RadTextBox ID="rdTxtDeudor" runat="server" CssClass="control-text-search" Enabled="false" Text=""></telerik:RadTextBox>
        <a id="btnDeudores" href="app_show_deudores.aspx" class="btnsearch"></a>
        <div><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="rdTxtDeudor" ForeColor="Red" ErrorMessage="* Debe seleccionar el deudor." ValidationGroup="ValidPage"></asp:RequiredFieldValidator></div>
        <asp:HiddenField ID="hddCodDeudor" runat="server" />
      </div>
      <div>
        <div></div>
        <div></div>
        <asp:RadioButtonList ID="Estado" CssClass="inputcheckbox" runat="server">
          <asp:ListItem Text="Todas" Value="" Selected="True"></asp:ListItem>
          <asp:ListItem Text="Abiertas" Value="Abierta"></asp:ListItem>
          <asp:ListItem Text="Cerrada" Value="Cerrada"></asp:ListItem>
        </asp:RadioButtonList>
      </div>
      <div class="blq_btn_search">
        <div>
          <asp:Button ID="idBuscar" runat="server" Text="Buscar" CssClass="btn btn-lg btn-primary btn-block" Width="100px" OnClick="idBuscar_Click" ValidationGroup="ValidPage" />
        </div>
      </div>
      <div id="idGrilla" runat="server" visible="false">

        <telerik:RadGrid ID="rdGridCtaCte" runat="server" OnNeedDataSource="rdGridCtaCte_NeedDataSource" OnItemCommand="rdGridCtaCte_ItemCommand" OnItemDataBound="rdGridCtaCte_ItemDataBound"
          AllowPaging="true" AllowSorting="true" ShowStatusBar="true" PageSize="10" GridLines="None" AllowAutomaticUpdates="true" AllowAutomaticInserts="true" AllowAutomaticDeletes="true" Skin="Sitefinity">
          <ExportSettings HideStructureColumns="true"></ExportSettings>
          <PagerStyle Mode="NextPrevAndNumeric" />
          <MasterTableView AutoGenerateColumns="false" ShowHeader="true" ShowFooter="true"
            TableLayout="Fixed" ShowHeadersWhenNoRecords="true" CommandItemDisplay="Top">
            <CommandItemSettings ShowExportToExcelButton="true" ShowRefreshButton="false" ShowAddNewRecordButton="false" />
            <Columns>
              <telerik:GridBoundColumn DataField="tipodocumento" HeaderText="Tipo documento"
                UniqueName="tipodocumento">
                <HeaderStyle Font-Size="Smaller" Width="200px" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Left" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="numerodocumento" HeaderText="Numero Documento"
                UniqueName="numerodocumento">
                <HeaderStyle Font-Size="Smaller" Width="100px" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="emision" HeaderText="Emisión"
                UniqueName="emision" DataType="System.DateTime" DataFormatString="{0:d/M/yyyy}">
                <HeaderStyle Font-Size="Smaller" Width="100px" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="sorigen" HeaderText="Origen"
                UniqueName="sorigen">
                <HeaderStyle Font-Size="Smaller" Width="100px" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="numeroorigen" HeaderText="Num. Origen"
                UniqueName="numeroorigen">
                <HeaderStyle Font-Size="Smaller" Width="100px" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="nDebe" HeaderText="Debe"
                UniqueName="nDebe" Aggregate="Sum" DataFormatString="{0:N0}" FooterAggregateFormatString="{0:N0}">
                <HeaderStyle Font-Size="Smaller" Width="100px" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Right" />
                <FooterStyle HorizontalAlign="Right" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="nMontoOriginal" HeaderText="Monto Original"
                UniqueName="nMontoOriginal" Aggregate="Sum" DataFormatString="{0:N0}" FooterAggregateFormatString="{0:N0}">
                <HeaderStyle Font-Size="Smaller" Width="100px" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Right" />
                <FooterStyle HorizontalAlign="Right" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="nHaber" HeaderText="Haber"
                UniqueName="nHaber" Aggregate="Sum" DataFormatString="{0:N0}" FooterAggregateFormatString="{0:N0}">
                <HeaderStyle Font-Size="Smaller" Width="100px" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Right" />
                <FooterStyle HorizontalAlign="Right" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="dVencimiento" HeaderText="Vencimiento"
                UniqueName="dVencimiento" DataType="System.DateTime" DataFormatString="{0:d/M/yyyy}">
                <HeaderStyle Font-Size="Smaller" Width="100px" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="nsaldo" HeaderText="Saldo"
                UniqueName="nsaldo" Aggregate="Sum" DataFormatString="{0:N0}" FooterAggregateFormatString="{0:N0}">
                <HeaderStyle Font-Size="Smaller" Width="100px" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Right" />
                <FooterStyle HorizontalAlign="Right" />
              </telerik:GridBoundColumn>
            </Columns>
          </MasterTableView>
        </telerik:RadGrid>

      </div>
    </div>
    <script>
      var x, y;
      $(document).ready(function () {

        /* Apply fancybox to multiple items */

        $("#btnDeudores").fancybox({
          'width': 600,
          'height': 700,
          'transitionIn': 'elastic',
          'transitionOut': 'elastic',
          'speedIn': 600,
          'speedOut': 200,
          'overlayShow': false,
          'type': 'iframe',
          'onCleanup': function () {
            x = $("#fancybox-frame").contents().find("#hdd_razonsocial").val();
            y = $("#fancybox-frame").contents().find("#hdd_coddeudor").val();
          },
          'onClosed': function () {
            var text = $find("<%= rdTxtDeudor.ClientID %>");
            text.set_value(x);
            hddCodDeudor.value = y;
          }
        });

      });
    </script>
  </form>
</body>
</html>
