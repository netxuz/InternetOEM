<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ingreso_pagos.aspx.cs" Inherits="ICommunity.Antalis.ingreso_pagos" %>

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
    <asp:HiddenField ID="hddnkey_cliente" runat="server" />
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
          <asp:Label ID="lblTitle" runat="server" CssClass="lblTitle" Text="INGRESAR PAGOS"></asp:Label>
        </div>
        <div class="col-md-12">
          <hr style="#b8b8b8" />
        </div>
      </div>
      <div class="row">
        <div class="col-md-3">
          <span>CLIENTE: CRISTIAN ESCOBAR</span>
        </div>
        <div class="col-md-3">
          <span for="cmb_centrodistribucion">CENTRO DE DISTRIBUCION:</span>
          <asp:DropDownList ID="cmb_centrodistribucion" CssClass="form-control" runat="server">
          </asp:DropDownList>
        </div>
        <div class="col-md-3">
          <span for="cmb_documento">TIPO DE PAGO: </span>
          <asp:DropDownList ID="cmb_documento" CssClass="form-control" runat="server">
            <asp:ListItem Text="<< Seleccione tipo de documento >>" Value=""></asp:ListItem>
            <asp:ListItem Text="Cheque al día" Value="1"></asp:ListItem>
            <asp:ListItem Text="Efectivo" Value="2"></asp:ListItem>
            <asp:ListItem Text="Letra" Value="3"></asp:ListItem>
          </asp:DropDownList>
        </div>
        <div class="col-md-3">
          <div class="md-form" style="width: 20rem;">
            <asp:TextBox ID="txt_fecha_recepcion" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
            <label for="txt_fecha_recepcion">FECHA RECEPCION</label>
          </div>
        </div>
      </div>
      <div class="row">
        <!-- Material input -->
        <div class="col-md-2">
          <div class="md-form" style="width: 20rem;">
            <asp:TextBox ID="txt_codigosap" runat="server" CssClass="form-control"></asp:TextBox>
            <label for="txt_codigosap">CODIGO SAP</label>
          </div>
        </div>
        <div class="col-md-2">
          <div class="md-form" style="width: 20rem;">
            <asp:TextBox ID="txt_razon_social" runat="server" CssClass="form-control"></asp:TextBox>
            <label for="txt_razon_social">RAZON SOCIAL</label>
          </div>
        </div>
        <div class="col-md-2">
          <div class="md-form" style="width: 20rem;">
            <asp:TextBox ID="txt_num_documento" runat="server" CssClass="form-control"></asp:TextBox>
            <label for="txt_num_documento">NUMERO CHEQUE / OPERACION</label>
          </div>
        </div>
        <div class="col-md-2">
          <span for="cmb_bancos">BANCO:</span>
          <asp:DropDownList ID="cmb_bancos" CssClass="form-control" runat="server"></asp:DropDownList>
        </div>
        <div class="col-md-2">
          <span>FECHA DOCUMENTO</span>
          <div class="input-append date" id="dp4" data-date-format="dd-mm-yyyy">
            <asp:TextBox ID="fch_documento" runat="server" CssClass="form-control" ReadOnly></asp:TextBox>
            <span class="add-on"><i class="icon-th"></i></span>
          </div>
        </div>
      </div>
      <div class="row">
        <div class="col-md-2">
          <span for="cmb_guiadespacho">GUIA DESPACHO:</span>
          <asp:DropDownList ID="cmb_guiadespacho" CssClass="form-control" runat="server">
          </asp:DropDownList>
        </div>
        <div class="col-md-2">
          <span for="cmb_facturas">FACTURAS:</span>
          <asp:DropDownList ID="cmb_facturas" CssClass="form-control" runat="server">
          </asp:DropDownList>
        </div>
        <div class="col-md-2">
          <div class="md-form" style="width: 20rem;">
            <asp:TextBox ID="txt_importe" runat="server" CssClass="form-control"></asp:TextBox>
            <label for="txt_importe">IMPORTE</label>
          </div>
        </div>
      </div>
      <div class="row">
        <div class="col-md-12">
          <asp:Button ID="btnIngresarImportes" runat="server" class="btn btn-primary" Text="INGRESAR IMPORTES" OnClick="btnIngresarImportes_Click" />
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
    $(function () {
      $("#dp4").datepicker();
    });

    $("#txt_codigosap").focusout(function () {
      //96829710
      if ($("#txt_codigosap").val() != "") {
        var target = $("#cmb_guiadespacho");
        var cUrl = "ingreso_pagos.aspx/getGuiasDespacho";
        var datos = "{nkeycliente:" + $("#hddnkey_cliente").val() + ",ncodigodeudor:" + $("#txt_codigosap").val() + "}";
        $.ajax({
          type: "POST",
          url: cUrl,
          data: datos,
          contentType: "application/json; charset=utf-8",
          dataType: "json",

          success: function (data) {

            $("#cmb_guiadespacho").empty().append($("<option></option>").val("0").html("<< Seleccione Guia Despacho >>"));
            $.each(data.d, function (key, value) {
              var option = $(document.createElement("option"));
              option.html(value.guiasdespacho);
              option.val(value.guiasdespacho);
              $("#cmb_guiadespacho").append(option);
            });
          },

          error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(textStatus + ": " + XMLHttpRequest.responseText);
          }
        });
      } else {
        $("#cmb_guiadespacho").empty();
      }
    });

    //----------------------------------------------------------------------------------------------------------------------

    $("#cmb_guiadespacho").focusout(function () {
      //96829710
      if (($("#cmb_guiadespacho").val() != null)&&($("#cmb_guiadespacho").val() != 0)) {
        var target = $("#cmb_facturas");
        var cUrl = "ingreso_pagos.aspx/getFacturas";
        var datos = "{sGuiaDespacho:" + $("#cmb_guiadespacho").val() + "}";
        $.ajax({
          type: "POST",
          url: cUrl,
          data: datos,
          contentType: "application/json; charset=utf-8",
          dataType: "json",

          success: function (data) {

            $("#cmb_facturas").empty().append($("<option></option>").val("0").html("<< Seleccione Guia Despacho >>"));
            $.each(data.d, function (key, value) {
              var option = $(document.createElement("option"));
              option.html(value.nNumeroFactura);
              option.val(value.nNumeroFactura);
              $("#cmb_facturas").append(option);
            });
          },

          error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(textStatus + ": " + XMLHttpRequest.responseText);
          }
        });
      } else {
        $("#cmb_facturas").empty();
      }
    });

  </script>
</body>
</html>
