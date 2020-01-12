<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="app_mov_carg_cart_find.aspx.cs" Inherits="ICommunity.Reporting.app_mov_carg_cart_find" %>

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
    <asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>

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
        <asp:Label ID="lblTitle" runat="server" CssClass="lblTitle" Text="MOVIMIENTOS CARGADOS CARTOLA"></asp:Label>
      </div>
      <div class="row">
        <div class="col-3 blq_date">
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
        <div class="col-3 blq_date">
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
      </div>
      <div class="row">
        <div class="col-3 text-left">
          <asp:Button ID="idBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary btn-sm" OnClick="idBuscar_Click" />
        </div>
      </div>
      <div id="idGrilla" class="row" runat="server" visible="false">
        <div class="col-12">
          <telerik:RadGrid ID="rdGridMovCargCartola" runat="server" OnNeedDataSource="rdGridMovCargCartola_NeedDataSource" OnItemCommand="rdGridMovCargCartola_ItemCommand" OnItemDataBound="rdGridMovCargCartola_ItemDataBound"
            AllowPaging="true" AllowSorting="true" ShowStatusBar="true" PageSize="10" GridLines="None" AllowAutomaticUpdates="true" AllowAutomaticInserts="true" AllowAutomaticDeletes="true" Skin="Sitefinity">
            <ExportSettings HideStructureColumns="true"></ExportSettings>
            <PagerStyle Mode="NextPrevAndNumeric" />
            <MasterTableView AutoGenerateColumns="false" ShowHeader="true" ShowFooter="true"
              TableLayout="Fixed" ShowHeadersWhenNoRecords="true" CommandItemDisplay="Top">
              <CommandItemSettings ShowExportToExcelButton="true" ShowRefreshButton="false" ShowAddNewRecordButton="false" />
              <Columns>
                <telerik:GridBoundColumn DataField="banco" HeaderText="Banco"
                  UniqueName="banco">
                  <HeaderStyle Font-Size="Smaller" Width="100px" HorizontalAlign="Center" />
                  <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>

                <telerik:GridBoundColumn DataField="fecha" HeaderText="Fecha"
                  UniqueName="fecha" DataType="System.DateTime" DataFormatString="{0:d/M/yyyy}">
                  <HeaderStyle Font-Size="Smaller" Width="100px" HorizontalAlign="Center" />
                  <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>

                <telerik:GridBoundColumn DataField="num_movto" HeaderText="N&ordm; Docto. Sucursal"
                  UniqueName="num_movto">
                  <HeaderStyle Font-Size="Smaller" Width="100px" HorizontalAlign="Center" />
                  <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>

                <telerik:GridBoundColumn DataField="descripcion" HeaderText="Descripci&oacute;n"
                  UniqueName="descripcion">
                  <HeaderStyle Font-Size="Smaller" Width="500px" HorizontalAlign="Center" />
                  <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>

                <telerik:GridBoundColumn HeaderText="Cod.Reporte Debtcontrol"
                  UniqueName="codreportedebtcontrol">
                  <HeaderStyle Font-Size="Smaller" Width="200px" HorizontalAlign="Center" />
                  <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>

                <telerik:GridBoundColumn DataField="importe" HeaderText="Dep&oacute;sitos Abonos"
                  UniqueName="abonos" Aggregate="Sum" DataFormatString="{0:N0}" FooterAggregateFormatString="{0:N0}">
                  <HeaderStyle Font-Size="Smaller" Width="100px" HorizontalAlign="Center" />
                  <ItemStyle HorizontalAlign="Right" />
                  <FooterStyle HorizontalAlign="Right" />
                </telerik:GridBoundColumn>

                <telerik:GridBoundColumn DataField="importe" HeaderText="Cheques Cargos"
                  UniqueName="cargos" Aggregate="Sum" DataFormatString="{0:N0}" FooterAggregateFormatString="{0:N0}">
                  <HeaderStyle Font-Size="Smaller" Width="100px" HorizontalAlign="Center" />
                  <ItemStyle HorizontalAlign="Right" />
                  <FooterStyle HorizontalAlign="Right" />
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
  </form>
</body>
</html>
