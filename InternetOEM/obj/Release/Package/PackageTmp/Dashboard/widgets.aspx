<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="widgets.aspx.cs" Inherits="ICommunity.Dashborad.widgets" %>

<!DOCTYPE html>

<html lang="en">
<head>
  <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
  <meta http-equiv="x-ua-compatible" content="ie=edge">
  <!-- Font Awesome -->
  <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.8.2/css/all.css">
  <!-- Bootstrap core CSS -->
  <link href="css/bootstrap.min.css" rel="stylesheet">
  <!-- Material Design Bootstrap -->
  <link href="css/mdb.min.css" rel="stylesheet">
  <!-- Your custom styles (optional) -->
  <link href="css/style.css" rel="stylesheet">

  <title>DASHBOARD</title>
</head>
<body class="blue-grey lighten-5">
  <!--Navbar-->
  <nav class="navbar navbar-expand-lg white navbar-light">

    <!-- Navbar brand -->
    <a class="navbar-brand text-center" href="#"><img src="../images/logodebtcontrol.png" border="0" width="40%" /></a>

    <!-- Collapse button -->
    <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#basicExampleNav"
      aria-controls="basicExampleNav" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>

    <!-- Collapsible content -->
    <div class="collapse navbar-collapse" id="basicExampleNav">

      <!-- Links -->
      <ul class="navbar-nav mr-auto">
        <li class="nav-item ">
          <a class="nav-link blue-grey-text d-flex justify-content-end" href="#!">
            <h5><i class="fas fa-user-circle blue-text"></i><b>Andry Flores</b></h5>
          </a>
        </li>
        <li class="nav-item">
          <a class="nav-link blue-grey-text d-flex justify-content-end" href="#">
            <h5><i class="fas fa-users blue-text"></i><b>Customer Profile</b></h5>
          </a>
        </li>
        <li class="nav-item">
          <a class="nav-link blue-grey-text d-flex justify-content-end" href="#!">
            <h5><i class="fas fa-chart-pie blue-text"></i><b>Reportes</b></h5>
          </a>
        </li>
        <li class="nav-item">
          <a class="nav-link blue-grey-text d-flex justify-content-end" href="#!">
            <h5><i class="fas fa-cog blue-text"></i><b>Configuración</b></h5>
          </a>
        </li>
      </ul>
      <!-- Links -->
      <form class="form-inline">
        <div class="md-form dbtform my-0">
          <input id="txtSearch" class="form-control dbtinput mr-sm-2" type="text" placeholder="Buscar" aria-label="Search">
        </div>
      </form>
    </div>
    <!-- Collapsible content -->

  </nav>
  <!--/.Navbar-->
  <div class="container-fluid">
    <!-- Card -->
    <div class="row mdb-color">
      <div class="col-md-12">
        <span style="font-size:20px" class="align-middle text-white">Customer Profile Falabella Retail S.A Cod. 9074900</span>
      </div>
    </div>
    <div class="row">
      <div class="col-md-12">
        <br />
      </div>
    </div>
    <div class="row">
      <div class="col-md-3">
        <div class="card">
          <!-- Card content -->
          <div class="card-body">

            <!-- Title -->
            <h5 class="card-title blue-grey-text text-center">GENERACION DE CAJA</h5>
            <!-- Text -->
            <p class="card-text white-text text-center"><i class="far fa-money-bill-alt fa-3x  yellow-text"></i></p>
            <!-- Button -->
            <a href="#!" class="yellow-text d-flex justify-content-end">
              <h5><i class="far fa-check-square"></i></h5>
            </a>

          </div>

        </div>
        <!-- Card -->
      </div>
      <div class="col-md-3">
        <div class="card">
          <!-- Card content -->
          <div class="card-body">

            <!-- Title -->
            <h5 class="card-title blue-grey-text text-center">INDICADORES</h5>
            <!-- Text -->
            <p class="card-text white-text text-center"><i class="fas fa-chart-line fa-3x  yellow-text"></i></p>
            <!-- Button -->
            <a href="#!" class="yellow-text d-flex justify-content-end">
              <h5><i class="far fa-check-square"></i></h5>
            </a>

          </div>

        </div>
        <!-- Card -->
      </div>
      <div class="col-md-3">
        <div class="card">
          <!-- Card content -->
          <div class="card-body">

            <!-- Title -->
            <h5 class="card-title blue-grey-text text-center">ANTIGUEDAD DEUDA</h5>
            <!-- Text -->
            <p class="card-text grey-text text-center"><i class="fas fa-file-invoice-dollar fa-3x"></i></p>
            <!-- Button -->
            <a href="#!" class="grey-text d-flex justify-content-end">
              <h5><i class="far fa-check-square"></i></h5>
            </a>

          </div>

        </div>
        <!-- Card -->
      </div>
      <div class="col-md-3">
        <div class="card">
          <!-- Card content -->
          <div class="card-body">

            <!-- Title -->
            <h5 class="card-title blue-grey-text text-center">ALERTAS</h5>
            <!-- Text -->
            <p class="card-text grey-text text-center"><i class="far fa-bell fa-3x yellow-text"></i></p>
            <!-- Button -->
            <a href="#!" class="grey-text d-flex justify-content-end">
              <h5><i class="far fa-check-square yellow-text"></i></h5>
            </a>

          </div>

        </div>
        <!-- Card -->
      </div>
    </div>

  </div>

  <!-- SCRIPTS -->
  <!-- JQuery -->
  <script type="text/javascript" src="js/jquery-3.4.1.min.js"></script>
  <!-- Bootstrap tooltips -->
  <script type="text/javascript" src="js/popper.min.js"></script>
  <!-- Bootstrap core JavaScript -->
  <script type="text/javascript" src="js/bootstrap.min.js"></script>
  <!-- MDB core JavaScript -->
  <script type="text/javascript" src="js/mdb.min.js"></script>
</body>
</html>
