<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="clientes-venta-potencial.aspx.cs" Inherits="ICommunity.Dashboard.clientes_venta_potencial" %>

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
      width: 60%;
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

    .blq-cel {
      background-image: linear-gradient(180deg, #8CB9DC 100%, #569DF2 100%);
      /*background-color: #66A5EB;*/
    }

    .blq-mar {
      background-image: linear-gradient(180deg, #B02731 100%, #8D1731 100%);
    }

    .blq-ver {
      background-image: linear-gradient(180deg, #5A994E 100%, #017059 100%);
    }

    .blq-ama {
      background-image: linear-gradient(180deg, #F6B857 100%, #F8A32E 100%);
    }

    .blq-azu {
      background-image: linear-gradient(180deg, #437CDF 100%, #2E49BC 100%);
    }

    .blq-nar {
      background-image: linear-gradient(180deg, #FB9761 100%, #F76B1C 100%);
    }

    .blq-mor {
      background-image: linear-gradient(180deg, #894FC6 100%, #C96ED8 100%);
    }

    .blq-plo {
      background-image: linear-gradient(180deg, #C1C1C1 100%,#C1C1C1 100%);
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

    .sub-tt-menu {
      font-size: 1.2rem;
      font-weight: 900;
      color: #1a237e;
    }

    .tt-menu {
      font-size: .9rem;
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

    .blq-sub-tt {
      padding: 0px 20px 0px 20px;
    }

    .blq-cliente-menu {
      /*width: 900px;*/
      margin-top: 20px;
      margin-bottom: 10px;
      margin-right: 10px;
      height: 45px;
      background-color:#1de9b6;
      border-radius:10px 10px 0px 0px;
      /*float:left;*/
    }

    .blq-categoria-menu {
      justify-content: center;
      width: 60px;
      padding: 10px 10px 0px 10px;
      text-align: center;
      float: left;
      display: flex;
      flex-direction: column;
      background-color: #1de9b6;
      border-radius: 8px;
      margin-left: 10px;
      margin-right: 10px;
    }

    .txt-categoria-menu {
      /*font-size:3rem;*/
      font-weight: 900;
      color: #fff;
    }

    .blq-data-client-menu {
      position: relative;
    }

    .blq-tt-cliente-menu {
      padding-top: 10px;
    }

    .tt-cliente-menu {
      /*font-size:1.2rem;*/
      font-weight: 900;
      color: #fff;
    }

    .blq-dt-pastdue-menu {
      padding-top: 10px;
      right: 0;
      position: absolute;
      top: 0;
      margin-right: 40px;
    }

    .dt-pastdue-menu {
      /*font-size:1rem;*/
      font-weight: 900;
      color: #fff;
    }

    .blq-cliente {
      /*width: 900px;*/
      margin-top: 10px;
      margin-bottom: 10px;
      margin-right: 10px;
      height: 80px;
      /*float:left;*/
      box-shadow: 0 5px 10px 0 rgba(0,0,0,.16), 0 5px 10px 0px rgba(0,0,0,.12);
    }

    .blq-categoria {
      justify-content: center;
      width: 60px;
      padding: 10px 10px 10px 10px;
      text-align: center;
      float: left;
      height: 60px;
      display: flex;
      flex-direction: column;
      box-shadow: 0 0px 0px 0 rgba(0,0,0,.16), 0 0px 8px 0px rgba(0,0,0,.12);
      background-color: #1de9b6;
      border-radius: 8px;
      margin-top: 10px;
      margin-left: 10px;
      margin-right: 10px;
    }

    .txt-categoria {
      /*font-size:3rem;*/
      font-weight: 900;
      color: #fff;
    }

    .blq-data-client {
      position: relative;
    }

    .blq-tt-cliente {
      padding-top: 20px;
    }

    .tt-cliente {
      /*font-size:1.2rem;*/
      font-weight: 900;
      color: #757575;
    }

    .blq-dt-pastdue {
      padding-top: 20px;
      right: 0;
      position: absolute;
      top: 0;
      margin-right: 10px;
    }

    .dt-pastdue {
      /*font-size:1rem;*/
      font-weight: 900;
      color: #1de9b6;
    }

    /*.blq-rojo {
      background-color:#D0021B;
    }

    .blq-naranjo {
      background-color:#F57F23;
    }
    .blq-amarillo {
      background-color:#F5D423;
    }
    .blq-verde-claro {
      background-color:#7ED321;
    }
    .blq-verde {
      background-color:#4DA227;
    }*/

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
              <a class="nav-link waves-effect waves-light" href="mainaccess.aspx"><i class="fas fa-home  fa-2x white-text"></i></a>
            </li>
            <li class="nav-item">
              <asp:LinkButton ID="bnt_logout" runat="server" CssClass="nav-link waves-effect waves-light" OnClick="bnt_logout_Click"><i class="fas fa-power-off fa-2x red-text"></i></asp:LinkButton>
            </li>
            <li class="nav-item">
              <button type="button" id="sidebarCollapse"  class="btn-barra-menu nav-link waves-effect waves-light"> <i class="fas fa-bars fa-2x text-white"></i> </button>
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
          <div class="row">
            <div id="ClientesDeCuidado" runat="server" class="col-md-12">
              <div class="blq-tt">
                <span class="sub-tt-menu">VENTA POTENCIAL</span><hr />
              </div>
              <div class="blq-cliente-menu">
                <div class="blq-categoria-menu"><span class="txt-categoria">#</span></div>
                <div class="blq-data-client-menu">
                  <div class="blq-tt-cliente-menu"><span class="tt-cliente-menu">Cliente</span></div>
                  <div class="blq-dt-pastdue-menu"><span class="dt-pastdue-menu">Monto</span></div>
                </div>
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
