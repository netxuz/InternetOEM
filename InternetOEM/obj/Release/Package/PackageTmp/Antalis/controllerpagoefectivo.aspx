<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="controllerpagoefectivo.aspx.cs" Inherits="ICommunity.Antalis.controllerpagoefectivo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta charset="utf-8" />
  <meta http-equiv="X-UA-Compatible" content="IE=edge" />
  <meta name="viewport" content="width=device-width, initial-scale=1" />
  <title>Home</title>
  <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.8.2/css/all.css" />
  <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
  <!-- Bootstrap core CSS -->
  <link href="../css/bootstrap.min.css" rel="stylesheet">
  <!-- Material Design Bootstrap -->
  <link href="../css/mdb.min.css" rel="stylesheet">
  <!-- Antalis -->
  <link rel="stylesheet" href="../css/antalis.css" />
  <link rel="stylesheet" href="../css/datepicker.css" />
  <style>
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

    .navbar {
      padding-right: 0 !important;
      display: flex !important;
      align-items: center !important;
      padding: .5rem 1rem;
    }

    .navbar-nav {
      margin: 7.5px -15px !important;
    }

    .navbar-brand {
      display: inline-block !important;
      padding-top: .3125rem !important;
      padding-bottom: .3125rem !important;
      margin-right: 1rem !important;
      font-size: 1.25rem !important;
      line-height: inherit !important;
      white-space: nowrap !important;
      float: none !important;
      height: auto !important;
      padding: unset !important;
    }

    @media (min-width: 992px) {
      .navbar-expand-lg .navbar-collapse {
        display: flex !important;
        flex-basis: auto;
      }

      .navbar-collapse {
        flex-grow: 1;
        align-items: center;
      }
    }

    .divider-new, .navbar .nav-flex-icons {
      flex-direction: row;
    }

    .ml-auto, .mx-auto {
      margin-left: auto !important;
    }

    .nav-item {
      list-style: none !important;
    }

    @media (min-width: 768px) {
      .navbar-nav > li > a {
        padding-right: 25px !important;
      }
    }
  </style>
</head>
<body>
  <form id="form1" runat="server" autocomplete="off">
    <asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>
    <asp:HiddenField ID="hdd_cod_pago" runat="server" />

    <header>
      <nav class="navbar navbar-expand-lg debtcontrol navbar-light">

        <!-- Navbar brand -->
        <a class="navbar-brand tam-logo text-center" href="#">
          <img src="../images/logodebtcontrol.png" border="0" width="90%" /></a>

        <!-- Collapse button -->
        <%--<button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent-6"
          aria-controls="navbarSupportedContent-6" aria-expanded="false" aria-label="Toggle navigation">
          <span class="navbar-toggler-icon"></span>
        </button>--%>

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
    <div class="container">
      <div class="row">&nbsp;</div>
      <div class="row">
        <div class="col-md-6">
          <asp:Label ID="lblTitle" runat="server" CssClass="lblTitle" Text="VALIDACIÓN DE RECAUDACIÓN DE PAGOS / EFECTIVO"></asp:Label>
        </div>
        <div class="col-md-6 text-right">
          <asp:Label ID="lblValija" runat="server" CssClass="lblTitle"></asp:Label>
        </div>
      </div>
      <div class="row vAlign">
        <div class="col-md-4">
          <span>CLIENTE:
            <asp:Label ID="lblRazonSocial" runat="server"></asp:Label></span>
        </div>
        <div class="col-md-4">
          <span>RAZÓN SOCIAL:
            <asp:Label ID="lblNomDeudor" runat="server"></asp:Label></span>
        </div>
        <div class="col-md-4">
          <span>CENTRO DE DISTRIBUCION:
            <asp:Label ID="lblCentroDistribucion" runat="server"></asp:Label></span>
        </div>
        <div class="col-md-4">
          <span>FECHA RENDICIÓN:
            <asp:Label ID="lblFecharecepcion" runat="server"></asp:Label></span>
        </div>
      </div>
      <div id="idRow1" runat="server" class="row vAlign">
        <div class="col-md-4">
          <span>IMPORTE TOTAL:
            <asp:Label ID="lblimporte" runat="server"></asp:Label></span>
          <asp:HiddenField ID="hdd_importe" runat="server" />
        </div>
        <div class="col-md-3">
          <div class="md-form" style="width: 20rem;">
            <asp:TextBox ID="txt_importe_recibido" runat="server" CssClass="form-control"></asp:TextBox>
            <label for="txt_importe_recibido">IMPORTE RECIBIDO</label>
          </div>
        </div>
        <div class="col-md-3">
          <div class="md-form" style="width: 20rem;">
            <asp:TextBox ID="txt_discrepancia" runat="server" CssClass="form-control"></asp:TextBox>
            <label for="txt_discrepancia">DISCREPANCIA</label>
          </div>
        </div>
      </div>
      <div id="idRow3" runat="server" class="row vAlign">
        <div class="col-md-12 text-center">
          <asp:Button ID="btnRechazar" runat="server" class="btn btn-default" Text="RECHAZAR" OnClick="btnRechazar_Click" />
          <asp:Button ID="btnAprobar" runat="server" class="btn btn-primary" Text="APROBAR" OnClick="btnAprobar_Click" />
        </div>
      </div>
    </div>
  </form>
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
  <!-- Bootstrap tooltips -->
  <script type="text/javascript" src="../js/popper.min.js"></script>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
  <!-- datepicker core JavaScript -->
  <script type="text/javascript" src="../js/bootstrap-datepicker.js" charset="UTF-8"></script>
  <!-- MDB core JavaScript -->
  <script type="text/javascript" src="../js/mdb.min.js"></script>
  <script>
    $("#txt_importe_recibido").focusout(function () {
      if ($("#txt_importe_recibido").val() != '') {
        var iDiscrepancia = parseInt($("#hdd_importe").val()) - parseInt($("#txt_importe_recibido").val());
        $("#txt_discrepancia").val(iDiscrepancia);
        $("#txt_discrepancia").focus();
      }
    });

    $("#btnAprobar").click(function () {
      if (parseInt($("#txt_discrepancia").val()) > 0) {
        alert('No es posible aprobar la valija, debido a que existen discrepancias.');
        return false;
      }
    });

    $("#btnRechazar").click(function () {
      if (parseInt($("#txt_discrepancia").val()) == 0) {
        alert('No es posible rechazar la valija, debido a que no existen discrepancias.');
        return false;
      }
    });
  </script>
</body>
</html>
