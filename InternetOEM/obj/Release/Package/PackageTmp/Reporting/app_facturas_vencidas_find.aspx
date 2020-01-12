<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="app_facturas_vencidas_find.aspx.cs" Inherits="ICommunity.Reporting.app_facturas_vencidas_find" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta charset="utf-8">
  <meta http-equiv="X-UA-Compatible" content="IE=edge">
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <meta http-equiv="Pragma" content="no-cache" />
  <meta http-equiv="Expires" content="-1" />
  <meta http-equiv="CACHE-CONTROL" content="NO-CACHE" />
  <title></title>
  <!-- Font Awesome -->
  <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.8.2/css/all.css" />
  <link rel="stylesheet" href="../dashboard/css/bootstrap.min.css" />
  <link rel="stylesheet" type="text/css" href="../css/stylesdebtcontrol.css" media="screen" />
  <link rel="stylesheet" type="text/css" href="../dashboard/css//mdb.min.css" />
  <style>
    /*--------------------------------------------------------------------------------------*/
    /*--------------------------------------------------------------------------------------*/
    /*--------------------------------------------------------------------------------------*/

    header {
      background-image: url(../images/barrasuperior_menu.png);
      /*background-position: center;*/
      background-repeat: no-repeat;
      background-size: cover;
    }

    .debtcontrol {
      /*background: #fff !important;*/
      /*background-image: linear-gradient(135deg, #fff 70%, #3D5375);*/
    }

    .nav-background {
      background: #3D5375 !important;
    }

    .tam-logo {
      width: 80px !important;
    }
  </style>
</head>
<body>
  <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:HiddenField ID="hdd_arrNkeyCliente" runat="server" />
    <asp:HiddenField ID="hdd_cli_show" runat="server" />

    <header>
      <nav class="navbar navbar-expand-lg debtcontrol navbar-light">

        <!-- Navbar brand -->
        <a class="navbar-brand tam-logo text-center" href="#">
          <img src="../images/logodebtcontrol.png" border="0" width="90%" /></a>

        <!-- Collapse button -->
        <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent-6"
          aria-controls="navbarSupportedContent-6" aria-expanded="false" aria-label="Toggle navigation">
          <span class="navbar-toggler-icon"></span>
        </button>

        <!-- Collapsible content -->
        <div class="collapse navbar-collapse" id="navbarSupportedContent-6">

          <!-- Links -->
          <%--<ul class="navbar-nav mr-auto">--%>
          <ul class="navbar-nav ml-auto nav-flex-icons right-icons">
            <li class="nav-item">
              <a class="nav-link waves-effect waves-light" href="../dashboard/mainaccess.aspx"><i class="fas fa-home  fa-2x white-text"></i></a>
            </li>
            <li class="nav-item">
              <asp:LinkButton ID="bnt_logout" runat="server" CssClass="nav-link waves-effect waves-light" OnClick="bnt_logout_Click"><i class="fas fa-power-off fa-2x red-text"></i></asp:LinkButton>
            </li>
          </ul>
        </div>
        <!-- Collapsible content -->
      </nav>
    </header>

    <%--<nav class="navbar-inverse">
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
    </nav>--%>

    <div class="container-fluid">
      <div class="blq_tile">
        <asp:Label ID="lblTitle" runat="server" CssClass="lblTitle" Text="FACTURAS VENCIDAS Y POR VENCER "></asp:Label><asp:Label ID="lblfechahoy" runat="server" CssClass="lblTitle"></asp:Label>
      </div>
      <div class="row">
        <div id="colClientes" class="col-md-3" runat="server" visible="false">
          <div><span>Clientes</span></div>
          <div></div>
          <asp:DropDownList ID="cmbCliente" CssClass="inputCmbBox" runat="server">
          </asp:DropDownList>
        </div>
        <div id="colHolding" class="col-md-3" runat="server" visible="false">
          <div><span>Holding</span></div>
          <div></div>
          <asp:DropDownList ID="cmbHolding" CssClass="inputCmbBox" runat="server">
          </asp:DropDownList>
        </div>
      </div>
      <div class="row">
        <div id="colDeudor" runat="server" class="blq_search col-3" visible="false">
          <div><span>Deudor</span></div>
          <div></div>
          <telerik:RadTextBox ID="rdTxtDeudor" runat="server" CssClass="control-text-search" Enabled="false" Text=""></telerik:RadTextBox>
          <a id="btnDeudores" href="" class="btnsearch"></a>
          <asp:HiddenField ID="hddCodDeudor" runat="server" />
        </div>
      </div>
      <div class="row">
        <div class="col-3 text-left">
          <asp:Button ID="idBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary btn-sm" OnClick="idBuscar_Click" />
        </div>
      </div>
      <div id="idGrilla" runat="server" visible="false" class="row">
        <div class="col-12">
          <asp:Label ID="lblmoneda" runat="server" CssClass="lblmoneda"></asp:Label>
          <telerik:RadGrid ID="rdGridFacturasVencidas" runat="server" OnNeedDataSource="rdGridFacturasVencidas_NeedDataSource" OnItemCommand="rdGridFacturasVencidas_ItemCommand" OnItemDataBound="rdGridFacturasVencidas_ItemDataBound"
            AllowPaging="true" AllowSorting="true" ShowStatusBar="true" PageSize="10" GridLines="None"
            AllowAutomaticUpdates="true" AllowAutomaticInserts="true" AllowAutomaticDeletes="true" Skin="Sitefinity">
            <ExportSettings HideStructureColumns="true"></ExportSettings>
            <PagerStyle Mode="NextPrevAndNumeric" />
            <MasterTableView AutoGenerateColumns="false" ShowHeader="true" ShowFooter="true"
              TableLayout="Fixed" ShowHeadersWhenNoRecords="true" CommandItemDisplay="Top">
              <CommandItemSettings ShowExportToExcelButton="true" ShowRefreshButton="false" ShowAddNewRecordButton="false" />
              <Columns>
                <telerik:GridBoundColumn DataField="ncodholding" HeaderText="Cod Sociedad"
                  UniqueName="ncodholding">
                  <HeaderStyle Font-Size="Smaller" Width="100px" HorizontalAlign="Center" />
                  <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>

                <telerik:GridBoundColumn DataField="CodigoDeudor" HeaderText="Cod Deudor"
                  UniqueName="CodigoDeudor">
                  <HeaderStyle Font-Size="Smaller" Width="100px" HorizontalAlign="Center" />
                  <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>

                <telerik:GridBoundColumn DataField="RUT Deudor" HeaderText="RUT Deudor"
                  UniqueName="RUT Deudor">
                  <HeaderStyle Font-Size="Smaller" Width="100px" HorizontalAlign="Center" />
                  <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>

                <telerik:GridBoundColumn DataField="Deudor" HeaderText="Deudor"
                  UniqueName="Deudor">
                  <HeaderStyle Font-Size="Smaller" Width="200px" HorizontalAlign="Center" />
                  <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>

                <telerik:GridBoundColumn DataField="Tipo Documento" HeaderText="Tipo Documento"
                  UniqueName="Tipo Documento">
                  <HeaderStyle Font-Size="Smaller" Width="100px" HorizontalAlign="Center" />
                  <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>

                <telerik:GridBoundColumn DataField="Número Documento" HeaderText="Número Documento"
                  UniqueName="Número Documento">
                  <HeaderStyle Font-Size="Smaller" Width="100px" HorizontalAlign="Center" />
                  <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>

                <telerik:GridBoundColumn DataField="Fecha Emisión" HeaderText="Fecha Emisión"
                  UniqueName="Fecha Emisión" DataType="System.DateTime" DataFormatString="{0:d/M/yyyy}">
                  <HeaderStyle Font-Size="Smaller" Width="100px" HorizontalAlign="Center" />
                  <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>

                <telerik:GridBoundColumn DataField="Fecha Vencimiento" HeaderText="Fecha Vencimiento"
                  UniqueName="Fecha Vencimiento" DataType="System.DateTime" DataFormatString="{0:d/M/yyyy}">
                  <HeaderStyle Font-Size="Smaller" Width="100px" HorizontalAlign="Center" />
                  <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>

                <telerik:GridBoundColumn DataField="Monto" HeaderText="Monto"
                  UniqueName="Monto">
                  <HeaderStyle Font-Size="Smaller" Width="100px" HorizontalAlign="Center" />
                  <ItemStyle HorizontalAlign="Right" />
                </telerik:GridBoundColumn>

                <telerik:GridBoundColumn DataField="Saldo" HeaderText="Saldo"
                  UniqueName="Saldo">
                  <HeaderStyle Font-Size="Smaller" Width="100px" HorizontalAlign="Center" />
                  <ItemStyle HorizontalAlign="Right" />
                </telerik:GridBoundColumn>

              </Columns>
            </MasterTableView>
          </telerik:RadGrid>
        </div>

      </div>
      <div style="height: 30px;">
        <br />
        <br />
      </div>
    </div>
    <script type="text/javascript" src="../dashboard/js/jquery-3.4.1.min.js"></script>
    <!-- Bootstrap tooltips -->
    <script type="text/javascript" src="../dashboard/js/popper.min.js"></script>
    <!-- Bootstrap core JavaScript -->
    <script type="text/javascript" src="../dashboard/js/bootstrap.min.js"></script>
    <!-- MDB core JavaScript -->
    <script type="text/javascript" src="../dashboard/js/mdb.min.js"></script>

    <script type="text/javascript" src="../Resources/fancybox/jquery.min.js"></script>
    <script type="text/javascript" src="../Resources/fancybox/jquery.mousewheel-3.0.4.pack.js"></script>
    <script type="text/javascript" src="../Resources/fancybox/jquery.fancybox-1.3.4.js"></script>
    <link rel="stylesheet" type="text/css" href="../Resources/fancybox/jquery.fancybox-1.3.4.css" media="screen" />
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
