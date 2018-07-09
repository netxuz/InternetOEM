<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="app_rep_chq_fecha_find.aspx.cs" Inherits="ICommunity.Reporting.app_rep_chq_fecha_find" %>

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
        <asp:Label ID="lblTitle" runat="server" CssClass="lblTitle" Text="REPORTE CHEQUE A FECHA"></asp:Label>
      </div>
      <div class="blq_search">
        <div><span>Deudor</span></div>
        <div></div>
        <telerik:RadTextBox ID="rdTxtDeudor" runat="server" CssClass="control-text-search" Enabled="false" Text=""></telerik:RadTextBox>
        <a id="btnDeudores" href="app_show_deudores.aspx" class="btnsearch"></a>
        <asp:HiddenField ID="hddCodDeudor" runat="server" />
      </div>
      <div class="blq_date">
        <div><span>Fecha Inicio</span></div>
        <div>
          <telerik:RadDatePicker ID="RadDatePicker1" runat="server" AutoPostBack="true"
            DateInput-EmptyMessage="" MinDate="01/01/1000" MaxDate="01/01/3000">
            <Calendar>
              <SpecialDays>
                <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
              </SpecialDays>
            </Calendar>
          </telerik:RadDatePicker>
        </div>
      </div>
      <div class="blq_date">
        <div><span>Fecha Hasta</span></div>
        <div>
          <telerik:RadDatePicker ID="RadDatePicker2" runat="server" AutoPostBack="true"
            DateInput-EmptyMessage="" MinDate="01/01/1000" MaxDate="01/01/3000">
            <Calendar>
              <SpecialDays>
                <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
              </SpecialDays>
            </Calendar>
          </telerik:RadDatePicker>
        </div>
      </div>
      <div class="blq_btn_search">
        <div>
          <asp:Button ID="idBuscar" runat="server" Text="Buscar" CssClass="btn btn-lg btn-primary btn-block" Width="100px" OnClick="idBuscar_Click" />
        </div>
      </div>
      <div id="idGrilla" runat="server" visible="false">

        <telerik:RadGrid ID="rdGridRepChqFecha" runat="server" OnNeedDataSource="rdGridRepChqFecha_NeedDataSource" OnItemCommand="rdGridRepChqFecha_ItemCommand"
          AllowPaging="true" AllowSorting="true" ShowStatusBar="true" PageSize="10" GridLines="None"
          AllowAutomaticUpdates="true" AllowAutomaticInserts="true" AllowAutomaticDeletes="true" Skin="Sitefinity">
          <ExportSettings HideStructureColumns="true"></ExportSettings>
          <PagerStyle Mode="NextPrevAndNumeric" />
          <MasterTableView AutoGenerateColumns="false" ShowHeader="true" ShowFooter="true"
            TableLayout="Fixed" ShowHeadersWhenNoRecords="true" CommandItemDisplay="Top">
            <CommandItemSettings ShowExportToExcelButton="true" ShowRefreshButton="false" ShowAddNewRecordButton="false" />
            <Columns>
              <telerik:GridBoundColumn DataField="coddeudor" HeaderText="Código"
                UniqueName="coddeudor">
                <HeaderStyle Font-Size="Smaller" Width="100px" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="nomdeudor" HeaderText="Razón Social"
                UniqueName="nomdeudor">
                <HeaderStyle Font-Size="Smaller" Width="200px" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Left" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="Numpago" HeaderText="Número Cheque"
                UniqueName="Numpago">
                <HeaderStyle Font-Size="Smaller" Width="100px" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="FechaPago" HeaderText="Fecha Recepción"
                UniqueName="FechaPago" DataType="System.DateTime" DataFormatString="{0:d/M/yyyy}">
                <HeaderStyle Font-Size="Smaller" Width="100px" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="MontoPago" HeaderText="Monto"
                UniqueName="MontoPago" Aggregate="Sum" DataFormatString="{0:N0}" FooterAggregateFormatString="{0:N0}">
                <HeaderStyle Font-Size="Smaller" Width="100px" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Right" />
                <FooterStyle HorizontalAlign="Right" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="VencCheque" HeaderText="Vencimiento"
                UniqueName="VencCheque" DataType="System.DateTime" DataFormatString="{0:d/M/yyyy}">
                <HeaderStyle Font-Size="Smaller" Width="100px" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="MontoFactura" HeaderText="Monto Aplicado"
                UniqueName="MontoFactura" Aggregate="Sum" DataFormatString="{0:N0}" FooterAggregateFormatString="{0:N0}">
                <HeaderStyle Font-Size="Smaller" Width="100px" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Right" />
                <FooterStyle HorizontalAlign="Right" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="NumFactura" HeaderText="Número Factura"
                UniqueName="NumFactura">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" Width="100px" />
                <ItemStyle HorizontalAlign="Center" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="MontoFactura" HeaderText="Monto"
                UniqueName="MontoFactura" Aggregate="Sum" DataFormatString="{0:N0}" FooterAggregateFormatString="{0:N0}">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" Width="100px" />
                <ItemStyle HorizontalAlign="Right" />
                <FooterStyle HorizontalAlign="Right" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="FechaVencFac" HeaderText="Vencimiento"
                UniqueName="FechaVencFac" DataType="System.DateTime" DataFormatString="{0:d/M/yyyy}">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" Width="100px" />
                <ItemStyle HorizontalAlign="Center" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="DiasExceso" HeaderText="Sobreplazo"
                UniqueName="DiasExceso">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" Width="100px" />
                <ItemStyle HorizontalAlign="Center" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="NomVendedor" HeaderText="Vendedor"
                UniqueName="NomVendedor">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" Width="100px" />
                <ItemStyle HorizontalAlign="Left" />
              </telerik:GridBoundColumn>
            </Columns>
          </MasterTableView>
        </telerik:RadGrid>

      </div>
      <div style="height: 30px;">
        <br />
        <br />
      </div>
    </div>
    <footer class="footer">
      <div class="container">
        <p class="text-muted">Debtcontrol © 2016 | Todos derechos reservados. Antonia López de Bello 172 Of 304 - Santiago - Chile | Teléfono: +562 5996100</p>
      </div>
    </footer>
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
