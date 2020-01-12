<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mainaccess.aspx.cs" Inherits="ICommunity.Dashboard.mainaccess" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta charset="utf-8" />
  <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
  <meta http-equiv="x-ua-compatible" content="ie=edge" />
  <meta http-equiv="Pragma" content="no-cache" />
  <meta http-equiv="Expires" content="-1" />
  <meta http-equiv="CACHE-CONTROL" content="NO-CACHE" />
  <!-- Font Awesome -->
  <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.8.2/css/all.css" />
  <!-- Bootstrap core CSS -->
  <link href="css/bootstrap.min.css" rel="stylesheet" />
  <!-- Material Design Bootstrap -->
  <link href="css/mdb.min.css" rel="stylesheet" />
  <!-- Your custom styles (optional) -->
  <link href="css/style.css" rel="stylesheet" />

  <title>Dashboard</title>
  <!-- SCRIPTS -->
  <link href='https://fonts.googleapis.com/css?family=Lato:100,400,700,900' rel='stylesheet' type='text/css' />

  <style>
    /*
    DEMO STYLE
*/
    body {
      font-family: 'Lato', sans-serif;
      /*background-color: #F6F7FF;*/
      /*background: #fafafa;*/
    }

    header {
      background-image: url(../images/barrasuperior_menu.png);
      /*background-position: center;*/
      background-repeat: no-repeat;
      background-size: cover;
    }

    p {
      font-family: 'Lato', sans-serif;
      font-size: 1.1em;
      font-weight: 300;
      line-height: 1.7em;
      color: #999;
    }

    a,
    a:hover,
    a:focus {
      color: inherit;
      text-decoration: none;
      transition: all 0.3s;
    }

    /*.navbar {
      padding: 15px 10px;
      background: #fff;
      border: none;
      border-radius: 0;
      margin-bottom: 40px;
      box-shadow: 1px 1px 3px rgba(0, 0, 0, 0.1);
    }*/

    .navbar-btn {
      box-shadow: none;
      outline: none !important;
      border: none;
    }

    .line {
      width: 100%;
      height: 1px;
      border-bottom: 1px dashed #ddd;
      margin: 40px 0;
    }

    i,
    span {
      display: inline-block;
    }

    /* ---------------------------------------------------
    SIDEBAR STYLE
----------------------------------------------------- */

    /* ---------------------------------------------------
    SIDEBAR STYLE
----------------------------------------------------- */
    .btn-barra-menu {
      background: transparent;
      border: none;
    }

    #sidebar {
      width: 250px;
      position: fixed;
      top: 0;
      right: -250px;
      height: 100vh;
      z-index: 999;
      background: #ffffff;
      color: #C8C8C8;
      transition: all 0.3s;
      overflow-y: scroll;
      box-shadow: 3px 3px 3px rgba(0, 0, 0, 0.2);
    }

      #sidebar.active {
        right: 0;
      }

    #dismiss {
      width: 35px;
      height: 35px;
      line-height: 35px;
      text-align: center;
      background: #fff;
      position: absolute;
      top: 10px;
      right: 10px;
      cursor: pointer;
      -webkit-transition: all 0.3s;
      -o-transition: all 0.3s;
      transition: all 0.3s;
    }

      #dismiss:hover {
        background: #fff;
        color: #1B4C80;
      }

    .overlay {
      display: none;
      position: fixed;
      width: 100vw;
      height: 100vh;
      background: rgba(0, 0, 0, 0.7);
      z-index: 998;
      opacity: 0;
      transition: all 0.5s ease-in-out;
      top: 0;
    }

      .overlay.active {
        display: block;
        opacity: 1;
      }

    #sidebar .sidebar-header {
      padding: 20px;
      background: #6d7fcc;
    }

    #sidebar ul.components {
      padding: 20px 0;
      /*border-bottom: 1px solid #47748b;*/
    }

    #sidebar ul p {
      color: #fff;
      padding: 10px;
    }

    #sidebar ul li a {
      padding: 10px;
      font-size: 1em;
      font-weight: 400;
      display: block;
    }

      #sidebar ul li a:hover {
        color: #1B4C80;
        background: #fff;
      }

    #sidebar ul li.active > a,
    a[aria-expanded="true"] {
      color: #1B4C80;
      background: #ffffff;
    }

    a[data-toggle="collapse"] {
      position: relative;
    }

    .dropdown-toggle::after {
      display: block;
      position: absolute;
      top: 50%;
      right: 20px;
      transform: translateY(-50%);
    }

    ul ul a {
      font-size: 0.9em !important;
      padding-left: 30px !important;
      background: #ffffff;
    }

    ul.CTAs {
      padding: 20px;
    }

      ul.CTAs a {
        text-align: center;
        font-size: 0.9em !important;
        display: block;
        border-radius: 5px;
        margin-bottom: 5px;
      }

    a.download {
      background: #fff;
      color: #7386D5;
    }

    a.article,
    a.article:hover {
      background: #6d7fcc !important;
      color: #fff !important;
    }

    .list-unstyled.collapse.show {
      color: #96BEDC;
    }

    /* ---------------------------------------------------
    CONTENT STYLE
----------------------------------------------------- */

    #content {
      width: 95%;
      /*padding: 20px;*/
      min-height: 100vh;
      transition: all 0.3s;
      /*position: absolute;
      top: 0;*/
    }

    /*--------------------------------------------------------------------------------------*/
    /*--------------------------------------------------------------------------------------*/
    /*--------------------------------------------------------------------------------------*/

    /*.margin-conteiner {
      margin-top: 30px;
    }*/

    .lastrequest {
      font-size: .5rem;
      font-weight: 400;
      border-radius: 8px;
      box-shadow: 0 2px 5px 0 rgba(0,0,0,.16), 0 2px 10px 0 rgba(0,0,0,.12);
      margin: 10px;
      height: 100px;
      width: 100px;
      float: left;
      position: relative;
      min-height: 100px;
      color: #fff;
    }

    .mostrequest {
      font-size: .5rem;
      font-weight: 400;
      border-radius: 8px;
      box-shadow: 0 2px 5px 0 rgba(0,0,0,.16), 0 2px 10px 0 rgba(0,0,0,.12);
      margin: 10px;
      height: 100px;
      width: 100px;
      float: left;
      position: relative;
      min-height: 100px;
      color: #fff;
    }

    .blq-cyan {
      background-image: linear-gradient(180deg, #00838f 100%, #00838f 100%);
      /*background-color: #66A5EB;*/
    }

    .blq-teal {
      background-image: linear-gradient(180deg, #00bfa5 100%, #00bfa5 100%);
    }

    .blq-lime {
      background-image: linear-gradient(180deg, #cddc39 100%, #cddc39 100%);
    }

    .blq-unique-color {
      background-image: linear-gradient(180deg, #3F729B 100%, #3F729B 100%);
    }

    .blq-deep-orange {
      background-image: linear-gradient(180deg, #ff7043 100%, #ff7043 100%);
    }

    .blq-orange {
      background-image: linear-gradient(180deg, #ffd180 100%, #ffd180 100%);
    }

    .blq-teal-accent-2 {
      background-image: linear-gradient(180deg, #64ffda 100%, #64ffda 100%);
    }

    .blq-teal-accent-3 {
      background-image: linear-gradient(180deg, #1de9b6 100%,#1de9b6 100%);
    }

    .blq-pink {
      background-image: linear-gradient(180deg, #ec407a 100%,#ec407a 100%);
    }

    .blq-pink-accent-1 {
      background-image: linear-gradient(180deg, #ff80ab 100%,#ff80ab 100%);
    }

    .blq-plo {
      background-image: linear-gradient(180deg, #757575 100%,#757575 100%);
    }



    .tt-blq-fx {
      padding: 20px 5px 0px 5px;
      font-weight: 900;
    }

    .btm-blq-fch {
      padding: 10px;
      position: absolute;
      bottom: 0;
      left: 0px;
    }

    .btm-blq {
      padding: 10px;
      position: absolute;
      bottom: 0;
      left: 0px;
    }

    .btm-blq-getin {
      font-size: 1.2rem;
      padding: 10px;
      position: absolute;
      bottom: 0;
      right: 5px;
    }

    .tt-fecha {
      font-weight: 400;
    }

    .dt-fecha {
      font-weight: 700;
    }

    .go-in {
      font-size: 0.8rem;
    }

    .tt-menu {
      font-size: 1.2rem;
      font-weight: 900;
      color: #0d47a1;
    }

    .blq-tt {
      padding: 20px 20px 0px 20px;
    }

    hr {
      border-color: #ffca28 !important;
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
  <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/malihu-custom-scrollbar-plugin/3.1.5/jquery.mCustomScrollbar.min.css">
  <!-- Font Awesome JS -->
  <script defer src="https://use.fontawesome.com/releases/v5.0.13/js/solid.js" integrity="sha384-tzzSw1/Vo+0N5UhStP3bvwWPq+uvzCMfrN1fEFe+xBmv1C/AtVX5K0uZtmcHitFZ" crossorigin="anonymous"></script>
  <script defer src="https://use.fontawesome.com/releases/v5.0.13/js/fontawesome.js" integrity="sha384-6OIrr52G08NpOFSZdxxz1xdNSndlD4vdcf/q2myIUVO0VsqaGHJsB0RaBE01VTOY" crossorigin="anonymous"></script>

</head>
<body>
  <form id="form1" runat="server">
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
              <asp:LinkButton ID="bnt_logout" runat="server" CssClass="nav-link waves-effect waves-light" OnClick="bnt_logout_Click"><i class="fas fa-power-off fa-2x red-text"></i></asp:LinkButton>
            </li>
            <li class="nav-item">
              <button type="button" id="sidebarCollapse" class="btn-barra-menu nav-link waves-effect waves-light"><i class="fas fa-bars fa-2x text-white"></i></button>
            </li>
          </ul>
        </div>
        <!-- Collapsible content -->
      </nav>
    </header>
    <div class="wrapper align-items-center d-flex flex-column">
      <!-- Sidebar  -->
      <nav id="sidebar">
        <div id="dismiss">
          <i class="fas fa-arrow-right"></i>
        </div>

        <ul class="list-unstyled components">
          <li id="zone_dashboard" runat="server"></li>

          <li>
            <a href="#debt-reportes" data-toggle="collapse" aria-expanded="false" class="dropdown-toggle">
              <i class="fas fa-file-alt"></i>
              Reportes
            </a>
            <ul class="collapse list-unstyled" id="debt-reportes">
              <li>
                <a href="#idReportePago" data-toggle="collapse" aria-expanded="false" class="dropdown-toggle">
                  <i class="far fa-file"></i>
                  Reportes de Pago
                </a>
                <ul class="collapse list-unstyled" id="idReportePago" runat="server">
                </ul>
              </li>
              <li>
                <a href="#idProcesoSeguimiento" data-toggle="collapse" aria-expanded="false" class="dropdown-toggle">
                  <i class="far fa-file"></i>
                  Proceso de Seguimiento
                </a>
                <ul class="collapse list-unstyled" id="idProcesoSeguimiento" runat="server">
                </ul>
              </li>
              <li>
                <a href="#idCartolas" data-toggle="collapse" aria-expanded="false" class="dropdown-toggle">
                  <i class="far fa-file"></i>
                  Cartolas
                </a>
                <ul class="collapse list-unstyled" id="idCartolas" runat="server">
                </ul>
              </li>
              <li>
                <a href="#idProcesoNormalizacion" data-toggle="collapse" aria-expanded="false" class="dropdown-toggle">
                  <i class="far fa-file"></i>
                  Proceso de Normalización
                </a>
                <ul class="collapse list-unstyled" id="idProcesoNormalizacion" runat="server">
                </ul>
              </li>
              <li>
                <a href="#idIndicadoresClaves" data-toggle="collapse" aria-expanded="false" class="dropdown-toggle">
                  <i class="far fa-file"></i>
                  Indicadores Claves
                </a>
                <ul class="collapse list-unstyled" id="idIndicadoresClaves" runat="server">
                </ul>
              </li>
              <li>
                <a href="#IndClasificacionRiesgo" data-toggle="collapse" aria-expanded="false" class="dropdown-toggle">
                  <i class="far fa-file"></i>
                  Clasificación de Riesgo
                </a>
                <ul class="collapse list-unstyled" id="IndClasificacionRiesgo" runat="server">
                </ul>
              </li>

            </ul>
          </li>
          <li>
            <a href="#indAntalis" data-toggle="collapse" aria-expanded="false" class="dropdown-toggle">
              <i class="fas fa-money-check-alt"></i>
              Gestión de Pago
            </a>
            <ul class="collapse list-unstyled" id="indAntalis" runat="server">
            </ul>
          </li>
        </ul>

        <%--<ul class="list-unstyled CTAs">
          <li>
            <a href="https://bootstrapious.com/tutorial/files/sidebar.zip" class="download">Download source</a>
          </li>
          <li>
            <a href="https://bootstrapious.com/p/bootstrap-sidebar" class="article">Back to article</a>
          </li>
        </ul>--%>
      </nav>

      <!-- Page Content  -->
      <div id="content">


        <div class="container-fluid margin-conteiner">
          <!--Visto Recientemente-->
          <div class="row">
            <div id="Last20Request" runat="server" class="col-md-12">
              <div class="blq-tt">
                <span class="tt-menu">Visto recientemente:</span><hr />
              </div>
            </div>
          </div>

          <!--Lo más Visto-->
          <div class="row">
            <div id="id20MostRequest" runat="server" class="col-md-12">
              <div class="blq-tt">
                <asp:Label ID="lbl_lomasvistopor" runat="server" CssClass="tt-menu"></asp:Label><hr />
              </div>
            </div>
          </div>

          <!--Más valorado por la gerencia-->
          <div class="row">
            <div class="col-md-12">
              <div class="blq-tt">
                <span class="tt-menu">Más valorado por la gerencia:</span><hr />
              </div>
              <div class="lastrequest blq-deep-orange">
                <a href="litigios.aspx">
                  <div class="tt-blq-fx">LITIGIOS COMPAÑIA</div>
                  <div class="btm-blq-getin">
                    <i class='fas fa-sign-in-alt'></i>
                  </div>
                </a>
              </div>
              <div id="MasValoradoPorlaGerencia" runat="server"></div>
              <div class="lastrequest blq-plo">
                <div class="tt-blq-fx">COMPANY DASHBOARD</div>
                <div class="btm-blq-fch">
                  <div class="dt-fecha">Próximamente</div>
                </div>
              </div>
              <div class="lastrequest blq-plo">
                <div class="tt-blq-fx">PROVISIÓN COMPAÑIA</div>
                <div class="btm-blq-fch">
                  <div class="dt-fecha">Próximamente</div>
                </div>
              </div>
            </div>
          </div>

          <!--Visión de compañia-->
          <div class="row">
            <div class="col-md-12">
              <div class="blq-tt">
                <span class="tt-menu">Visión de compañía:</span><hr />
              </div>
              <div class="lastrequest blq-orange">
                <a href="litigios.aspx">
                  <div class="tt-blq-fx">LITIGIOS</div>
                  <div class="btm-blq-getin">
                    <i class='fas fa-sign-in-alt'></i>
                  </div>
                </a>
              </div>
              <div id="VisionDeCompañía" runat="server"></div>
              <div class="lastrequest blq-plo">
                <div class="tt-blq-fx">PROVISIÓN</div>
                <div class="btm-blq-fch">
                  <div class="dt-fecha">Próximamente</div>
                </div>
              </div>
              <div class="lastrequest blq-plo">
                <div class="tt-blq-fx">PAST DUE CRÍTICO (Valor). No incluye notas de crédito</div>
                <div class="btm-blq-fch">
                  <div class="dt-fecha">Próximamente</div>
                </div>
              </div>
              <div class="lastrequest blq-plo">
                <div class="tt-blq-fx">PAST DUE CRÍTICO (Días). No incluye Notas de crédito</div>
                <div class="btm-blq-fch">
                  <div class="dt-fecha">Próximamente</div>
                </div>
              </div>
              <div class="lastrequest blq-plo">
                <div class="tt-blq-fx">DEUDA VENCIDA > 30 DÍAS SIN LITIGIOS</div>
                <div class="btm-blq-fch">
                  <div class="dt-fecha">Próximamente</div>
                </div>
              </div>
              <div class="lastrequest blq-plo">
                <div class="tt-blq-fx">SOBREGIRO LÍNEA DE CRÉDITO</div>
                <div class="btm-blq-fch">
                  <div class="dt-fecha">Próximamente</div>
                </div>
              </div>
              <div class="lastrequest blq-plo">
                <div class="tt-blq-fx">ACUERDOS COMERCIALES Y DISCRECIONALES</div>
                <div class="btm-blq-fch">
                  <div class="dt-fecha">Próximamente</div>
                </div>
              </div>
              <div class="lastrequest blq-plo">
                <div class="tt-blq-fx">MOVIMIENTOS NO IDENTIFICADOS</div>
                <div class="btm-blq-fch">
                  <div class="dt-fecha">Próximamente</div>
                </div>
              </div>
            </div>
          </div>

          <!--Analizando clientes en el día a dia-->
          <div class="row">
            <div class="col-md-12">
              <div class="blq-tt">
                <span class="tt-menu">Analizando clientes en el día a día:</span><hr />
              </div>
              <div class="lastrequest blq-teal-accent-2">
                <a href="default.aspx">
                  <div class="tt-blq-fx">CUSTOMER PROFILE</div>
                  <div class="btm-blq-getin">
                    <i class='fas fa-sign-in-alt'></i>
                  </div>
                </a>
              </div>
              <div id="AnalizandoClientesDiaADia" runat="server"></div>
              <div class="lastrequest blq-plo">
                <div class="tt-blq-fx">ESTIMADO DE CAJA</div>
                <div class="btm-blq-fch">
                  <div class="dt-fecha">Próximamente</div>
                </div>
              </div>
              <div class="lastrequest blq-plo">
                <div class="tt-blq-fx">DEUDA VENCIDA > 30 DÍAS SIN LITIGIOS</div>
                <div class="btm-blq-fch">
                  <div class="dt-fecha">Próximamente</div>
                </div>
              </div>
              <div class="lastrequest blq-plo">
                <div class="tt-blq-fx">PROVISIÓN</div>
                <div class="btm-blq-fch">
                  <div class="dt-fecha">Próximamente</div>
                </div>
              </div>
              <div class="lastrequest blq-plo">
                <div class="tt-blq-fx">PROCESO DE NORMALIZACIÓN</div>
                <div class="btm-blq-fch">
                  <div class="dt-fecha">Próximamente</div>
                </div>
              </div>
              <div class="lastrequest blq-plo">
                <div class="tt-blq-fx">ÍNDICE DE CREDIBILIDAD</div>
                <div class="btm-blq-fch">
                  <div class="dt-fecha">Próximamente</div>
                </div>
              </div>
              <div class="lastrequest blq-plo">
                <div class="tt-blq-fx">ACUERDOS COMERCIALES Y DISCRECIONALES</div>
                <div class="btm-blq-fch">
                  <div class="dt-fecha">Próximamente</div>
                </div>
              </div>
            </div>
          </div>

          <!-- Clientes de cuidado-->
          <div class="row">
            <div id="ClientesDeCuidado" runat="server" class="col-md-12">
              <div class="blq-tt">
                <span class="tt-menu">Clientes de cuidado:</span><hr />
              </div>
              <div class="lastrequest blq-teal-accent-3">
                <a href="clientes-mayor-monto-pastdue.aspx">
                  <div class="tt-blq-fx">TOP 20 CLIENTES CON MAYOR % EN MONTO PAST DUE</div>
                  <div class="btm-blq-getin">
                    <i class='fas fa-sign-in-alt'></i>
                  </div>
                </a>
              </div>
              <div class="lastrequest blq-teal-accent-3">
                <a href="clientes-mayor-monto-pastdue-critico.aspx">
                  <div class="tt-blq-fx">TOP 20 CLIENTES CON MAYOR % EN MONTO PAST DUE CRITICO</div>
                  <div class="btm-blq-getin">
                    <i class='fas fa-sign-in-alt'></i>
                  </div>
                </a>
              </div>
              <div class="lastrequest blq-teal-accent-3">
                <a href="clientes-mayor-aporte-provision.aspx">
                  <div class="tt-blq-fx">TOP 20 CLIENTES CON MAYOR APORTE EN PROVISIÓN</div>
                  <div class="btm-blq-getin">
                    <i class='fas fa-sign-in-alt'></i>
                  </div>
                </a>
              </div>
              <div class="lastrequest blq-teal-accent-3">
                <a href="clientes-mas-litigios.aspx">
                  <div class="tt-blq-fx">TOP 20 CLIENTES CON MÁS LITIGIOS</div>
                  <div class="btm-blq-getin">
                    <i class='fas fa-sign-in-alt'></i>
                  </div>
                </a>
              </div>
              <div class="lastrequest blq-teal-accent-3">
                <a href="clientes-menor-credibilidad.aspx">
                  <div class="tt-blq-fx">TOP 20 CLIENTES CON MENOR CREDIBILIDAD</div>
                  <div class="btm-blq-getin">
                    <i class='fas fa-sign-in-alt'></i>
                  </div>
                </a>
              </div>
            </div>

          </div>

          <!--- Para seguir haciendo negocios--->
          <div class="row">
            <div id="ParaSeguirHaciendoNegocios" runat="server" class="col-md-12">
              <div class="blq-tt">
                <span class="tt-menu">Para seguir haciendo negocios:</span><hr />
              </div>
              <div class="lastrequest blq-pink">
                <a href="clientes-mejor-indice-credibilidad.aspx">
                  <div class="tt-blq-fx">TOP 50 CLIENTES CON MEJOR ÍNDICE DE CREDIBILIDAD</div>
                  <div class="btm-blq-getin">
                    <i class='fas fa-sign-in-alt'></i>
                  </div>
                </a>
              </div>
              <div class="lastrequest blq-pink">
                <a href="clientes-venta-potencial.aspx">
                  <div class="tt-blq-fx">TOP 50 VENTA POTENCIAL</div>
                  <div class="btm-blq-getin">
                    <i class='fas fa-sign-in-alt'></i>
                  </div>
                </a>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="overlay"></div>
    </div>
  </form>
  <!-- JQuery -->
  <script type="text/javascript" src="js/jquery-3.4.1.min.js"></script>
  <!-- Bootstrap tooltips -->
  <script type="text/javascript" src="js/popper.min.js"></script>
  <!-- Bootstrap core JavaScript -->
  <script type="text/javascript" src="js/bootstrap.min.js"></script>
  <!-- MDB core JavaScript -->
  <script type="text/javascript" src="js/mdb.min.js"></script>
  <script src="https://cdnjs.cloudflare.com/ajax/libs/malihu-custom-scrollbar-plugin/3.1.5/jquery.mCustomScrollbar.concat.min.js"></script>
  <script type="text/javascript">
    $(document).ready(function () {
      $("#sidebar").mCustomScrollbar({
        theme: "minimal"
      });

      $('#dismiss, .overlay').on('click', function () {
        $('#sidebar').removeClass('active');
        $('.overlay').removeClass('active');
      });

      $('#sidebarCollapse').on('click', function () {
        $('#sidebar').addClass('active');
        $('.overlay').addClass('active');
        $('.collapse.in').toggleClass('in');
        $('a[aria-expanded=true]').attr('aria-expanded', 'false');
      });
    });
  </script>
</body>
</html>
