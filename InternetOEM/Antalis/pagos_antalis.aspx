﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pagos_antalis.aspx.cs" Inherits="ICommunity.Antalis.pagos_antalis" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta charset="utf-8" />
  <meta http-equiv="X-UA-Compatible" content="IE=edge" />
  <meta name="viewport" content="width=device-width, initial-scale=1" />
  <title>Home</title>
  <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
  <!-- Bootstrap core CSS -->
  <link href="../css/bootstrap.min.css" rel="stylesheet">
  <!-- Material Design Bootstrap -->
  <link href="../css/mdb.min.css" rel="stylesheet">
  <!-- Antalis -->
  <link rel="stylesheet" href="../css/antalis.css" />
  <link rel="stylesheet" href="../css/datepicker.css" />
</head>
<body>
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
      <div class="row">&nbsp;</div>
      <div class="row">
        <div class="col-md-6">
          <asp:Label ID="lblTitle" runat="server" CssClass="lblTitle" Text="PAGOS ANTALIS"></asp:Label>
        </div>
        <div class="col-md-6">
          <asp:Button ID="btnIngresarPago" runat="server" class="btn btn-primary" Text="Ingresar Pago" OnClick="btnIngresarPago_Click"  />
        </div>
        <div class="col-md-12">
          <hr style="#b8b8b8" />
        </div>
      </div>
      <div class="row">
        <div class="col-md-2">
          <span>Fecha Inicio</span>
          <div class="input-append date" id="dp3" data-date="12-02-2012" data-date-format="dd-mm-yyyy">
            <input class="form-control" size="16" type="text" value="12-02-2012" readonly>
            <span class="add-on"><i class="icon-th"></i></span>
          </div>
        </div>
        <div class="col-md-2">
          <span>Fecha Hasta</span>
          <div class="input-append date" id="dp4" data-date="12-02-2012" data-date-format="dd-mm-yyyy">
            <input class="form-control" size="16" type="text" value="12-02-2012" readonly>
            <span class="add-on"><i class="icon-th"></i></span>
          </div>
        </div>
        <div class="col-md-2">
          <asp:Button ID="idBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary" Width="100px" />
        </div>
      </div>
      <div id="idGrilla" runat="server" visible="false">
      </div>
      <div style="height: 30px;">
        <br />
        <br />
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
    $(function () {
      $('#dp3').datepicker();
      $('#dp4').datepicker();
    });
  </script>
</body>
</html>
