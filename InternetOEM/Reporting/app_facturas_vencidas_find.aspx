<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="app_facturas_vencidas_find.aspx.cs" Inherits="ICommunity.Reporting.app_facturas_vencidas_find" %>

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
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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
        <asp:Label ID="lblTitle" runat="server" CssClass="lblTitle" Text="FACTURAS VENCIDAS"></asp:Label>
      </div>
      <div class="blq_search">
        <div><span>Deudor</span></div>
        <div></div>
        <telerik:RadTextBox ID="rdTxtDeudor" runat="server" CssClass="control-text-search" Enabled="false" Text=""></telerik:RadTextBox>
        <a id="btnDeudores" href="app_show_deudores.aspx" class="btnsearch"></a>
        <asp:HiddenField ID="hddCodDeudor" runat="server" />
      </div>
      <div class="blq_search">
        <div><span>Monto Mayor Que</span></div>
        <div></div>
        <asp:DropDownList ID="CmbBoxMontoMayor" CssClass="inputCmbBox" runat="server">
          <asp:ListItem Text="1" Value="1"></asp:ListItem>
          <asp:ListItem Text="100.000" Value="100000"></asp:ListItem>
          <asp:ListItem Text="500.000" Value="500000"></asp:ListItem>
          <asp:ListItem Text="1.000.000" Value="1000000"></asp:ListItem>
          <asp:ListItem Text="2.000.000" Value="2000000"></asp:ListItem>
          <asp:ListItem Text="4.000.000" Value="4000000"></asp:ListItem>
          <asp:ListItem Text="8.000.000" Value="8000000"></asp:ListItem>
          <asp:ListItem Text="16.000.000" Value="16000000"></asp:ListItem>
        </asp:DropDownList>
      </div>
      <div class="blq_search">
        <div><span>Atraso Mayor Que</span></div>
        <div></div>
        <asp:DropDownList ID="CmbBoxAtrasoMayor" CssClass="inputCmbBox" runat="server">
          <asp:ListItem Text="1" Value="1"></asp:ListItem>
          <asp:ListItem Text="7" Value="7"></asp:ListItem>
          <asp:ListItem Text="15" Value="15"></asp:ListItem>
          <asp:ListItem Text="21" Value="21"></asp:ListItem>
          <asp:ListItem Text="30" Value="30"></asp:ListItem>
          <asp:ListItem Text="40" Value="40"></asp:ListItem>
          <asp:ListItem Text="50" Value="50"></asp:ListItem>
          <asp:ListItem Text="60" Value="60"></asp:ListItem>
          <asp:ListItem Text="90" Value="90"></asp:ListItem>
          <asp:ListItem Text="120" Value="120"></asp:ListItem>
          <asp:ListItem Text="150" Value="150"></asp:ListItem>
          <asp:ListItem Text="180" Value="180"></asp:ListItem>
          <asp:ListItem Text="200" Value="200"></asp:ListItem>
          <asp:ListItem Text="250" Value="250"></asp:ListItem>
          <asp:ListItem Text="300" Value="300"></asp:ListItem>
        </asp:DropDownList>
      </div>
      <div class="blq_date">
        <div><span>Fecha</span></div>
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
      <div class="blq_btn_search">
        <div>
          <asp:Button ID="idBuscar" runat="server" Text="Buscar" CssClass="btn btn-lg btn-primary btn-block" Width="100px" OnClick="idBuscar_Click" />
        </div>
      </div>
      <div id="idGrilla" runat="server" visible="false">

        <telerik:RadGrid ID="rdGridFacturasVencidas" runat="server" OnNeedDataSource="rdGridFacturasVencidas_NeedDataSource" OnItemCommand="rdGridFacturasVencidas_ItemCommand"
          AllowPaging="true" AllowSorting="true" ShowStatusBar="true" PageSize="10" GridLines="None"
          AllowAutomaticUpdates="true" AllowAutomaticInserts="true" AllowAutomaticDeletes="true" Skin="Sitefinity">
          <ExportSettings HideStructureColumns="true"></ExportSettings>
          <PagerStyle Mode="NextPrevAndNumeric" />
          <MasterTableView AutoGenerateColumns="false" ShowHeader="true" ShowFooter="true"
            TableLayout="Fixed" ShowHeadersWhenNoRecords="true" CommandItemDisplay="Top">
            <CommandItemSettings ShowExportToExcelButton="true" ShowRefreshButton="false" ShowAddNewRecordButton="false" />
            <Columns>
              <telerik:GridBoundColumn DataField="nCodigoDeudor" HeaderText="Código Deudor"
                UniqueName="nCodigoDeudor">
                <HeaderStyle Font-Size="Smaller" Width="100px" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
                <FooterStyle HorizontalAlign="Center" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="sNombreDeudor" HeaderText="Deudor"
                UniqueName="sNombreDeudor">
                <HeaderStyle Font-Size="Smaller" Width="200px" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Left" />
                <FooterStyle HorizontalAlign="Left" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="sTipoDocumento" HeaderText="Tp doc."
                UniqueName="sTipoDocumento">
                <HeaderStyle Font-Size="Smaller" Width="100px" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
                <FooterStyle HorizontalAlign="Center" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="nNumeroDocumento" HeaderText="Nº Doc."
                UniqueName="nNumeroDocumento">
                <HeaderStyle Font-Size="Smaller" Width="100px" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
                <FooterStyle HorizontalAlign="Center" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="nOrigen" HeaderText="Origen"
                UniqueName="nOrigen">
                <HeaderStyle Font-Size="Smaller" Width="100px" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
                <FooterStyle HorizontalAlign="Center" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="nMonto" HeaderText="Monto"
                UniqueName="nMonto" Aggregate="Sum" DataFormatString="{0:N0}" FooterAggregateFormatString="{0:N0}">
                <HeaderStyle Font-Size="Smaller" Width="100px" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Right" />
                <FooterStyle HorizontalAlign="Right" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="dFechaEmision" HeaderText="Fecha Emisión"
                UniqueName="dFechaEmision" DataType="System.DateTime" DataFormatString="{0:d/M/yyyy}">
                <HeaderStyle Font-Size="Smaller" Width="100px" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
                <FooterStyle HorizontalAlign="Center" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="dFechaVencimiento" HeaderText="Fecha Vencimiento"
                UniqueName="dFechaVencimiento" DataType="System.DateTime" DataFormatString="{0:d/M/yyyy}">
                <HeaderStyle Font-Size="Smaller" Width="100px" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
                <FooterStyle HorizontalAlign="Center" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="nSaldo" HeaderText="Saldo"
                UniqueName="nSaldo" Aggregate="Sum" DataFormatString="{0:N0}" FooterAggregateFormatString="{0:N0}">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" Width="100px" />
                <ItemStyle HorizontalAlign="Right" />
                <FooterStyle HorizontalAlign="Right" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="nAtraso" HeaderText="Atraso"
                UniqueName="nAtraso">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" Width="100px" />
                <ItemStyle HorizontalAlign="Center" />
                <FooterStyle HorizontalAlign="Center" />
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
