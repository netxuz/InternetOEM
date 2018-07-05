<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="app_resumen_cobranza_find.aspx.cs" Inherits="ICommunity.Reporting.app_resumen_cobranza_find" %>

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
        <asp:Label ID="lblTitle" runat="server" CssClass="lblTitle" Text="RESUMEN DE COBRANZA"></asp:Label>
      </div>
      <div class="blq_search">
        <div><span>Deudor</span></div>
        <div></div>
        <telerik:RadTextBox ID="rdTxtDeudor" runat="server" CssClass="control-text-search" Enabled="false" Text=""></telerik:RadTextBox>
        <a id="btnDeudores" href="app_show_deudores.aspx" class="btnsearch"></a>
        <asp:HiddenField ID="hddCodDeudor" runat="server" />
      </div>
      <div class="blq_search">
        <div><span>Nº Pago</span></div>
        <div></div>
        <telerik:RadTextBox ID="rdTxtNumPago" runat="server" CssClass="control-text-search" Width="50px" Text=""></telerik:RadTextBox>
        <asp:HiddenField ID="HiddenField1" runat="server" />
      </div>
      <div class="blq_date">
        <div><span>Fecha Inicio</span></div>
        <div>
          <telerik:RadDatePicker ID="RadDatePicker3" runat="server" AutoPostBack="true"
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
          <telerik:RadDatePicker ID="RadDatePicker4" runat="server" AutoPostBack="true"
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

        <telerik:RadGrid ID="rdGridResumenCobranza" runat="server" 
          OnNeedDataSource="rdGridResumenCobranza_NeedDataSource" 
          OnItemCommand="rdGridResumenCobranza_ItemCommand" 
          OnItemDataBound="rdGridResumenCobranza_ItemDataBound"
          AllowPaging="true" AllowSorting="true" ShowStatusBar="true" PageSize="10" GridLines="None" AllowAutomaticUpdates="true" AllowAutomaticInserts="true" AllowAutomaticDeletes="true" Skin="Sitefinity">
          <ExportSettings HideStructureColumns="true"></ExportSettings>
          <PagerStyle Mode="NextPrevAndNumeric" />
          <MasterTableView AutoGenerateColumns="false" ShowHeader="true" ShowFooter="true"
            TableLayout="Fixed" ShowHeadersWhenNoRecords="true" CommandItemDisplay="Top">
            <CommandItemSettings ShowExportToExcelButton="true" ShowRefreshButton="false" ShowAddNewRecordButton="false" />
            <Columns>
              <telerik:GridBoundColumn DataField="código" HeaderText="Código"
                UniqueName="Código">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="RazónSocial" HeaderText="Deudor"
                UniqueName="RazónSocial">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Left" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="Tipo_Pago" HeaderText="Tipo Pago"
                UniqueName="Tipo_Pago">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="Número_Pago" HeaderText="Número Pago"
                UniqueName="Número_Pago">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="Monto_Pago" HeaderText="Monto Pago"
                UniqueName="Monto_Pago" Aggregate="Sum" DataFormatString="{0:N0}" FooterAggregateFormatString="{0:N0}">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Right" />
                <FooterStyle HorizontalAlign="Right" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="Emisión_Pago" HeaderText="Emisión Pago" DataType="System.DateTime" DataFormatString="{0:d/M/yyyy}"
                UniqueName="Emisión_Pago">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="Banco" HeaderText="Banco"
                UniqueName="Banco">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Right" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="Plaza" HeaderText="Plaza"
                UniqueName="Plaza">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Right" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="Sala" HeaderText="Sala"
                UniqueName="Sala">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Right" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="Tipo_Documento_Aplicado" HeaderText="Tipo Documento Aplicado"
                UniqueName="Tipo_Documento_Aplicado">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="Nº_Documento_Aplicado" HeaderText="Nº Documento Aplicado"
                UniqueName="Nº_Documento_Aplicado">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Right" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="Abono" HeaderText="Abono"
                UniqueName="Abono" Aggregate="Sum" DataFormatString="{0:N0}" FooterAggregateFormatString="{0:N0}">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Right" />
                <FooterStyle HorizontalAlign="Right" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="Saldo" HeaderText="Saldo"
                UniqueName="Saldo" Aggregate="Sum" DataFormatString="{0:N0}" FooterAggregateFormatString="{0:N0}">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Right" />
                <FooterStyle HorizontalAlign="Right" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="Observación" HeaderText="Observación"
                UniqueName="Observación">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Left" />
              </telerik:GridBoundColumn>

            </Columns>
          </MasterTableView>
        </telerik:RadGrid>

      </div>
      <div style="height:30px;">
        <br /><br />
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
