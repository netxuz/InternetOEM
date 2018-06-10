<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="ICommunity.Reporting._default" %>

<%@ Register Src="~/Controls/IndicadoresEconomicos.ascx" TagPrefix="uc1" TagName="IndicadoresEconomicos" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta charset="utf-8" />
  <meta http-equiv="X-UA-Compatible" content="IE=edge" />
  <meta name="viewport" content="width=device-width, initial-scale=1" />
  <title>Home</title>
  <link rel="stylesheet" href="../css/bootstrap.min.css" />
  <link rel="stylesheet" href="../css/style.css" />
  <link rel="stylesheet" href="../css/stylesdebtcontrol.css" />
  <link rel="stylesheet" href="../css/mdb.min.css">
</head>
<body class="bodydesktop">
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
            <li class="dropdown">
              <a class="dropdown-toggle" data-toggle="dropdown" href="#">Antalis<span class="caret"></span></a>
              <ul id="indAntalis" runat="server" class="dropdown-menu"></ul>
            </li>
          </ul>
        </div>
      </div>
    </nav>

    <div class="container">
      <div class="row">
        <div class="col-md-12">
          <h2>Paneles de Control</h2>
        </div>
      </div>
      <%--<div class="row">
        <div class="col-md-12 wow fadeInUp" data-wow-duration="2s" data-wow-delay="0.8s">
          <h2>Platform Features</h2>
          <h4 class="text-primary">Mes cuml dia sed inenias cet inger lot aliiqtes dolore ipsum amemod
                            ictor utligulat.</h4>

          <p>
            Mes cuml dia sed in lacus ut eniascet ing erto aliiqt es site amet eismod ictorligulate ameti
                            dapibus ticdu nt mtsen lusto dolor ltissim comes cuml dia sed inertio lacusu eni ascet dol
                            ingerto aliiqt sit. Amet eism com odictor ut ligulate cot ameti dapibu emo.
          </p>
        </div>
      </div>--%>
      <div id="idpanel" runat="server" class="row">
      </div>
      <%--<div class="row"><div class="col-md-12"><br /></div></div>--%>
      <div class="row">
        <div class="col-md-6"></div>
        <div class="col-md-6">
          <uc1:IndicadoresEconomicos runat="server" ID="IndicadoresEconomicos" />
        </div>
      </div>
      <div class="row">
        <div class="col-md-12">
          <br />
        </div>
      </div>
    </div>
    <footer class="footer">
      <div class="container">
        <p class="text-muted">Debtcontrol © 2016 | Todos derechos reservados. Antonia López de Bello 172 Of 304 - Santiago - Chile | Teléfono: +562 5996100</p>
      </div>
    </footer>
  </form>
  <!-- SCRIPTS -->
  <!-- JQuery -->
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
  <!-- googleapis -->
  <script type="text/javascript" src="../js/jquery-3.1.1.min.js"></script>
  <!-- Bootstrap core JavaScript -->
  <script type="text/javascript" src="../js/bootstrap.min.js"></script>
  <!-- MDB core JavaScript -->
  <script type="text/javascript" src="../js/mdb.min.js"></script>
  <script>
    wow = new WOW({
      boxClass: 'wow', // default
      animateClass: 'animated', // default
      offset: 0, // default
      mobile: true, // default
      live: true // default
    })
    wow.init();
  </script>
</body>
</html>
