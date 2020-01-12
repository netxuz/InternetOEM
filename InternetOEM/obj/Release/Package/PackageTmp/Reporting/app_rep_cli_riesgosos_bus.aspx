<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="app_rep_cli_riesgosos_bus.aspx.cs" Inherits="ICommunity.Reporting.app_rep_cli_riesgosos_bus" %>

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
    <div class="container-fluid">
      <div class="blq_tile">
        <asp:Label ID="lblTitle" runat="server" CssClass="lblTitle" Text="REPORTE CLIENTES RIESGOSOS"></asp:Label>
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
        <div class="col-3 text-left">
          <asp:Button ID="idBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary btn-sm" OnClick="idBuscar_Click" />
        </div>
      </div>

      <div style="height: 30px;">
        <br />
        <br />
      </div>
      <div id="idGrilla" runat="server" visible="false" class="row">
        <div class="col-12">
          <asp:Label ID="lblmoneda" runat="server" CssClass="lblmoneda"></asp:Label>
          <telerik:RadGrid ID="rdGridRepMotNoPago" runat="server" OnNeedDataSource="rdGridRepMotNoPago_NeedDataSource" OnItemCommand="rdGridRepMotNoPago_ItemCommand" OnItemDataBound="rdGridRepMotNoPago_ItemDataBound"
            AllowPaging="true" AllowSorting="true" ShowStatusBar="true" PageSize="10" GridLines="None" AllowAutomaticUpdates="true" AllowAutomaticInserts="true" AllowAutomaticDeletes="true" Skin="Sitefinity">
            <ExportSettings HideStructureColumns="true"></ExportSettings>
            <PagerStyle Mode="NextPrevAndNumeric" />
            <MasterTableView AutoGenerateColumns="false" ShowHeader="true"
              TableLayout="Fixed" ShowHeadersWhenNoRecords="true" CommandItemDisplay="Top">
              <CommandItemSettings ShowExportToExcelButton="true" ShowRefreshButton="false" ShowAddNewRecordButton="false" />
              <Columns>
                <telerik:GridBoundColumn DataField="ncodigodeudor" HeaderText="Código"
                  UniqueName="ncodigodeudor">
                  <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                  <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>

                <telerik:GridBoundColumn DataField="snombre" HeaderText="Deudor"
                  UniqueName="snombre">
                  <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                  <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <%-- DataFormatString="{0:N0}" FooterAggregateFormatString="{0:N0}" --%>
                <telerik:GridBoundColumn DataField="DeudaTotal" HeaderText="Deuda Total"
                  UniqueName="DeudaTotal" Aggregate="Sum">
                  <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                  <ItemStyle HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <%-- DataFormatString="{0:N0}" FooterAggregateFormatString="{0:N0}" --%>
                <telerik:GridBoundColumn DataField="DeudaVencida" HeaderText="Deuda Vencida"
                  UniqueName="DeudaVencida" Aggregate="Sum">
                  <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
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
  </form>
  <script type="text/javascript" src="../dashboard/js/jquery-3.4.1.min.js"></script>
  <!-- Bootstrap tooltips -->
  <script type="text/javascript" src="../dashboard/js/popper.min.js"></script>
  <!-- Bootstrap core JavaScript -->
  <script type="text/javascript" src="../dashboard/js/bootstrap.min.js"></script>
  <script>
    $(document).ready(function () {
      $("#idBuscar").click(function () {
        if ($("#cmbCliente").val() == "") {
          alert("Debe seleccionar cliente");
          return false;
        }
      });
    });
  </script>
</body>
</html>
