<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="app_historico_pago_find.aspx.cs" Inherits="ICommunity.Reporting.app_historico_pago_find" %>

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
    <asp:HiddenField ID="hdd_arrNkeyCliente" runat="server" />
    <asp:HiddenField ID="hdd_cli_show" runat="server" />
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
            <li class="dropdown">
              <a class="dropdown-toggle" data-toggle="dropdown" href="#">Gestión de Pago<span class="caret"></span></a>
              <ul id="indAntalis" runat="server" class="dropdown-menu"></ul>
            </li>
          </ul>
        </div>
      </div>
    </nav>

    <div class="container">
      <div class="blq_tile">
        <asp:Label ID="lblTitle" runat="server" CssClass="lblTitle" Text="HISTORICO DE PAGO"></asp:Label>
      </div>
      <div class="row">
        <div id="colClientes" class="col-md-4" runat="server" visible="false">
          <div><span>Clientes</span></div>
          <div></div>
          <asp:DropDownList ID="cmbCliente" CssClass="inputCmbBox" runat="server">
          </asp:DropDownList>
        </div>
        <div id="colHolding" class="col-md-4" runat="server" visible="false">
          <div><span>Holding</span></div>
          <div></div>
          <asp:DropDownList ID="cmbHolding" CssClass="inputCmbBox" runat="server">
          </asp:DropDownList>
        </div>
      </div>
      <div id="colDeudor" runat="server" class="blq_search" visible="false">
        <div><span>Deudor</span></div>
        <div></div>
        <telerik:RadTextBox ID="rdTxtDeudor" runat="server" CssClass="control-text-search" Enabled="false" Text=""></telerik:RadTextBox>
        <a id="btnDeudores" class="btnsearch"></a>
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
        <asp:Label ID="lblmoneda" runat="server" CssClass="lblmoneda"></asp:Label>
        <telerik:RadGrid ID="rdGridHistoricoPago" runat="server" 
          OnNeedDataSource="rdGridHistoricoPago_NeedDataSource"
          OnItemCommand="rdGridHistoricoPago_ItemCommand" OnItemDataBound="rdGridHistoricoPago_ItemDataBound"
          AllowPaging="true" AllowSorting="true" ShowStatusBar="true" PageSize="10" GridLines="None" 
          AllowAutomaticUpdates="true" AllowAutomaticInserts="true" AllowAutomaticDeletes="true" Skin="Sitefinity">
          <ExportSettings HideStructureColumns="true"></ExportSettings>
          <PagerStyle Mode="NextPrevAndNumeric" />
          <MasterTableView AutoGenerateColumns="false" ShowHeader="true" ShowFooter="true"
            TableLayout="Fixed" ShowHeadersWhenNoRecords="true" CommandItemDisplay="Top">
            <CommandItemSettings ShowExportToExcelButton="true" ShowRefreshButton="false" ShowAddNewRecordButton="false" />
            <Columns>

              <telerik:GridBoundColumn DataField="ncodholding" HeaderText="Código Sociedad"
                UniqueName="ncodholding">
                <HeaderStyle Font-Size="Smaller" Width="100px" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
              </telerik:GridBoundColumn>
              
              <telerik:GridBoundColumn DataField="Código" HeaderText="Código"
                UniqueName="Código">
                <HeaderStyle Font-Size="Smaller" Width="100px" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
              </telerik:GridBoundColumn>
              
              <telerik:GridBoundColumn DataField="Razón Social" HeaderText="Razón Social"
                UniqueName="Razón Social">
                <HeaderStyle Font-Size="Smaller" Width="200px" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Left" />
              </telerik:GridBoundColumn>
              
              <telerik:GridBoundColumn DataField="Tipo Pago" HeaderText="Tipo Pago"
                UniqueName="Tipo Pago">
                <HeaderStyle Font-Size="Smaller" Width="100px" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
              </telerik:GridBoundColumn>
              
              <telerik:GridBoundColumn DataField="Número Pago" HeaderText="Número Pago"
                UniqueName="Número Pago">
                <HeaderStyle Font-Size="Smaller" Width="100px" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
              </telerik:GridBoundColumn>
              
              <telerik:GridBoundColumn DataField="Fecha Documento" HeaderText="Fecha Documento"
                UniqueName="Fecha Documento" DataType="System.DateTime" DataFormatString="{0:d/M/yyyy}">
                <HeaderStyle Font-Size="Smaller" Width="100px" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
              </telerik:GridBoundColumn>
              
              <telerik:GridBoundColumn DataField="Total Pago" HeaderText="Total Pago"
                UniqueName="Total Pago" Aggregate="Sum">
                <HeaderStyle Font-Size="Smaller"  Width="100px" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Right" />
                <FooterStyle HorizontalAlign="Right" />
              </telerik:GridBoundColumn>
              
              <telerik:GridBoundColumn DataField="Número Factura" HeaderText="Número Factura"
                UniqueName="Número Factura">
                <HeaderStyle Font-Size="Smaller"  Width="100px" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
              </telerik:GridBoundColumn>
              
              <telerik:GridBoundColumn DataField="Emisión" HeaderText="Emisión" DataType="System.DateTime" DataFormatString="{0:d/M/yyyy}"
                UniqueName="Emisión">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
              </telerik:GridBoundColumn>
              
              <telerik:GridBoundColumn DataField="Vencimiento" HeaderText="Vencimiento" DataType="System.DateTime" DataFormatString="{0:d/M/yyyy}"
                UniqueName="Vencimiento">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
              </telerik:GridBoundColumn>
              
              <telerik:GridBoundColumn DataField="Abono" HeaderText="Aplic_Factura"
                UniqueName="Aplic_Factura" Aggregate="Sum" >
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Right" Width="100px" />
                <ItemStyle HorizontalAlign="Right" />
                <FooterStyle HorizontalAlign="Right" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="DBT" HeaderText="DBT"
                UniqueName="DBT">
                <HeaderStyle Font-Size="Smaller"  Width="100px" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="Vendedor" HeaderText="Vendedor"
                UniqueName="Vendedor">
                <HeaderStyle Font-Size="Smaller"  Width="100px" HorizontalAlign="Center" />
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

        $("#idBuscar").click(function () {
          if (($("#cmbCliente").val() == "") && ($("#hddCodDeudor").val() == "") && ($("#cmbHolding").val() == "")) {
            alert("Debe seleccionar cliente, o cliente - deudor, o holding");
            return false;
          }
        });

        $("#btnDeudores").click(function () {
          CodCliente = "";
          if ($("#hdd_cli_show").val() == "V") {
            if ($("#<%= cmbCliente.ClientID %>").val() != "")
              CodCliente = $("#<%= cmbCliente.ClientID %>").val();
            else {
              alert("Debe seleccionar cliente");
              return false;
            }
          }

          $.fancybox({
            'width': 600,
            'height': 700,
            'transitionIn': 'elastic',
            'transitionOut': 'elastic',
            'speedIn': 600,
            'speedOut': 200,
            'overlayShow': false,
            'href': 'app_show_deudores.aspx?ArrCodCliente=' + CodCliente,
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
          return false;

        });

      });
    </script>
  </form>
</body>
</html>
