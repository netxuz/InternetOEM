﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ingreso_pagos.aspx.cs" Inherits="ICommunity.Antalis.ingreso_pagos" EnableEventValidation="false" %>

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
  <form id="form1" runat="server" autocomplete="off">
    <asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>
    <asp:HiddenField ID="hddnkey_cliente" runat="server" />
    <asp:HiddenField ID="hdd_cod_pago" runat="server" />
    <asp:HiddenField ID="hdd_cod_documento" runat="server" />
    <asp:HiddenField ID="hdd_cod_factura" runat="server" />
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
              <a class="dropdown-toggle" data-toggle="dropdown" href="#">Gestión de Pago<span class="caret"></span></a>
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
        <div class="col-md-6 text-right">
          <asp:Label ID="lblValija" runat="server" CssClass="lblTitle"></asp:Label>
        </div>
      </div>
      <div class="row vAlign">
        <div class="col-md-3">
          <span>RAZÓN SOCIAL:
            <asp:Label ID="lblRazonSocial" runat="server"></asp:Label></span>
        </div>
        <div class="col-md-3">
          <span for="cmb_centrodistribucion">CENTRO DE DISTRIBUCION:</span>
          <asp:DropDownList ID="cmb_centrodistribucion" CssClass="form-control" runat="server">
          </asp:DropDownList>
        </div>
        <div class="col-md-3">
          <span for="cmb_documento">METODO DE PAGO: </span>
          <asp:DropDownList ID="cmb_documento" CssClass="form-control" runat="server">
            <asp:ListItem Text="<< Seleccione Método de Pago >>" Value=""></asp:ListItem>
            <asp:ListItem Text="Cheque al día" Value="1"></asp:ListItem>
            <asp:ListItem Text="Cheque a fecha" Value="2"></asp:ListItem>
            <asp:ListItem Text="Efectivo" Value="3"></asp:ListItem>
            <asp:ListItem Text="Letra" Value="4"></asp:ListItem>
            <asp:ListItem Text="Tarjeta" Value="5"></asp:ListItem>
            <asp:ListItem Text="Transferencia" Value="6"></asp:ListItem>
          </asp:DropDownList>
        </div>
        <div class="col-md-3">
          <span>FECHA RECEPCION:
            <asp:Label ID="lbl_fecha_recepcion" runat="server"></asp:Label></span>
          <asp:HiddenField ID="txt_fecha_recepcion" runat="server"></asp:HiddenField>
        </div>
      </div>
      <div id="idRow1" runat="server" class="row vAlign">
        <!-- Material input -->
        <div class="col-md-3">
          <div class="md-form">
            <asp:TextBox ID="txt_codigosap" runat="server" CssClass="form-control"></asp:TextBox>
            <label for="txt_codigosap">CODIGO SAP</label>
          </div>
        </div>
        <!--<div class="col-md-3">
          <div class="md-form" style="width: 20rem;">
            <asp:TextBox ID="txt_razon_social" runat="server" CssClass="form-control"></asp:TextBox>
            <label for="txt_razon_social">RAZON SOCIAL</label>
          </div>
        </div>-->
        <div class="col-md-3">
          <div class="md-form" style="width: 20rem;">
            <asp:TextBox ID="txt_num_documento" runat="server" CssClass="form-control"></asp:TextBox>
            <label for="txt_num_documento">NUMERO CHEQUE / OPERACION</label>
          </div>
        </div>
        <div class="col-md-3">
          <div class="md-form" style="width: 20rem;">
            <asp:TextBox ID="txt_importe" runat="server" CssClass="form-control"></asp:TextBox>
            <label for="txt_importe">IMPORTE</label>
          </div>
        </div>
        <div class="col-md-3">
          <span for="cmb_bancos">BANCO:</span>
          <asp:DropDownList ID="cmb_bancos" CssClass="form-control" runat="server"></asp:DropDownList>
        </div>
        <div class="col-md-3">
          <span for="fch_documento">FECHA DOCUMENTO</span>
          <div class="input-append date" id="dp4" data-date-format="dd-mm-yyyy">
            <asp:TextBox ID="fch_documento" runat="server" CssClass="form-control" ReadOnly></asp:TextBox>
            <span class="add-on"><i class="icon-th"></i></span>
            <asp:HiddenField ID="hdd_fchdocument" runat="server" />
          </div>
        </div>
      </div>
      <div id="idRow2" runat="server" class="row vAlign">
        <div class="col-md-2">
          <span for="cmb_guiadespacho">GUIA DESPACHO:</span>
          <asp:DropDownList ID="cmb_guiadespacho" CssClass="form-control" runat="server">
          </asp:DropDownList>
          <asp:HiddenField ID="hddGuiasDespacho" runat="server" />
        </div>
        <div class="col-md-4">
          <div class="row">
            <div class="col-md-12">
              <span for="cmb_facturas"># FACTURA:</span>
              <asp:Label ID="cmb_facturas" runat="server"></asp:Label>
            </div>
          </div>
          <div class="row">
            <div class="col-md-12">
              <span for="lbl_valor_factura">VALOR FACTURA:</span>
              <asp:Label ID="lbl_valor_factura" runat="server"></asp:Label>
            </div>
          </div>
          <asp:HiddenField ID="hdd_facturas" runat="server" />
        </div>
      </div>
      <div id="idRow3" runat="server" class="row vAlign">
        <div class="col-md-12 text-center">
          <asp:Button ID="btnCancelarUpdate" runat="server" class="btn btn-default" Text="CANCELAR" OnClick="btnCancelarUpdate_Click" Visible="false" />
          <asp:Button ID="btnIngresarImportes" runat="server" class="btn btn-primary" Text="INGRESAR PAGO" OnClick="btnIngresarImportes_Click" />
        </div>
      </div>
      <div class="row">
        <br />
      </div>
      <div class="row">
        <asp:GridView ID="gdPagos" runat="server" CssClass="table table-hover"
          DataKeyNames="cod_documento" BorderStyle="Solid"
          BorderWidth="0" GridLines="Horizontal"
          AutoGenerateColumns="false"
          OnRowDeleting="gdPagos_RowDeleting"
          OnSelectedIndexChanged="gdPagos_SelectedIndexChanged"
          OnPageIndexChanging="gdPagos_PageIndexChanging" OnRowDataBound="gdPagos_RowDataBound">
          <Columns>
            <asp:CommandField ButtonType="Link" ShowDeleteButton="true" DeleteText="Dele" ItemStyle-CssClass="BtnColEliminar" ItemStyle-Width="1px" />
            <asp:CommandField ButtonType="Link" ShowSelectButton="true" SelectText="Sele" ItemStyle-CssClass="BtnColEditar" ItemStyle-Width="1px" />
            <asp:BoundField HeaderText="# DOCUMENTO" DataField="num_documento" />
            <asp:BoundField HeaderText="CODIGO BANCO" DataField="cod_banco" />
            <asp:BoundField HeaderText="FECHA DOCUMENTO" DataField="fch_documento" />
            <asp:BoundField HeaderText="# GUIA DESPACHO" DataField="num_guia_despacho" />
            <asp:BoundField HeaderText="# FACTURA" DataField="num_factura" />
            <asp:BoundField HeaderText="IMPORTE" DataField="importe" />
          </Columns>
        </asp:GridView>
      </div>
      <div class="col-md-6"></div>
      <div class="col-md-6 text-right">
        <div class="row">
          <span>Total de Documentos :
            <asp:Label ID="lblCantidad" runat="server"></asp:Label></span>
          <asp:HiddenField ID="hdd_cantidad_doc" runat="server" />
        </div>
        <div class="row">
          <span>Monto Total :
            <asp:Label ID="lblMonto" runat="server"></asp:Label></span>
          <asp:HiddenField ID="hdd_importe_total" runat="server" />
        </div>
      </div>
      <div class="row">
        <div class="col-md-12 text-center">
          <asp:Button ID="btnImprimirValija" runat="server" Text="Imprimir Valija" CssClass="btn btn-default" OnClick="btnImprimirValija_Click" Visible="false" />
          <asp:Button ID="btnAbrirValija" runat="server" Text="Abrir Valija" CssClass="btn btn-primary" OnClick="btnAbrirValija_Click" Visible="false" />
          <asp:Button ID="btnCerrarValija" runat="server" Text="Cerrar Valija" CssClass="btn btn-primary" OnClick="btnCerrarValija_Click" Visible="false" />
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

    //----------------------------------------------------------------------------------------------------------------------

    var allowSubmit = true;
    $("#btnIngresarImportes").click(function (e) {
      if ($("#cmb_centrodistribucion").val() == "") {
        alert('Debe seleccionar centro de distribución');
        return false;
      }

      if ($("#cmb_documento").val() == "") {
        alert('Debe seleccionar tipo de documento');
        return false;
      }

      if ($("#txt_codigosap").val() == "") {
        alert('Debe ingresar código SAP del cliente');
        return false;
      }

      //if ($("#txt_razon_social").val() == "") {
      //  alert('Debe ingresar razón social del cliente');
      //  return false;
      //}

      if (($("#cmb_documento").val() != "3") && ($("#txt_num_documento").val() == "")) {
        alert('Debe ingresar código / número del documento');
        return false;
      }

      if ((($("#cmb_documento").val() != "3") && ($("#cmb_documento").val() != "5") && ($("#cmb_documento").val() != "6")) && ($("#cmb_bancos").val() == "")) {
        alert('Debe ingresar el Banco del documento');
        return false;
      }

      if ($("#cmb_documento").val() != "3") {
        if (($("#fch_documento").val() == "")) {
          alert('Debe ingresar la fecha del documento');
          return false;
        } else {
          var dSys = new Date();
          var sano = dSys.getUTCFullYear();
          var smes = '0' + (parseInt(dSys.getUTCMonth()) + 1);
          smes = smes.substring(smes.length - 2, smes.length);
          var sdia = '0' + dSys.getUTCDate();
          sdia = sdia.substring(sdia.length - 2, sdia.length);
          var sFchDia = sano + smes + sdia;

          var sFchDoc = $("#fch_documento").val();
          sFchDoc = sFchDoc.substring(sFchDoc.length - 4, sFchDoc.length) + sFchDoc.substring(sFchDoc.length - 7, sFchDoc.length - 5) + sFchDoc.substring(0, 2);

          if (($("#cmb_documento").val() == "1") && (parseInt(sFchDia) < parseInt(sFchDoc))) {
            alert('La fecha del documento no puede ser superior a la fecha actual');
            return false;
          }

          if (($("#cmb_documento").val() == "2") && (parseInt(sFchDia) > parseInt(sFchDoc))) {
            alert('La fecha del documento no puede ser con fecha menor o igual a la fecha actual');
            return false;
          }
          document.getElementById("<%=hdd_fchdocument.ClientID%>").value = $("#fch_documento").val();
        }
      }

      if (($("#cmb_guiadespacho").val() == null) || ($("#cmb_guiadespacho").val() == 0)) {
        alert('Debe seleccionar guia de despacho');
        return false;
      } else {
        document.getElementById("<%=hddGuiasDespacho.ClientID%>").value = $("#cmb_guiadespacho").val();
      }

      if ($("#hdd_facturas").val() == "") {
        alert('Debe seleccionar numero de factura');
        return false;
      }

      if (($("#txt_importe").val() == "")) {
        alert('Debe ingresar el importe a pagar');
        return false;
      }

      var btn = $(this);
      if ((allowSubmit) && (($("#cmb_documento").val() == "1") || ($("#cmb_documento").val() == "2"))) {
        e.preventDefault();
        var cUrl = "ingreso_pagos.aspx/getValida";
        var datos = "{sCodNumDocumento:" + $("#txt_num_documento").val() + ",sCodBanco:" + $("#cmb_bancos").val() + "}";
        $.ajax({
          type: "POST",
          url: cUrl,
          data: datos,
          contentType: "application/json; charset=utf-8",
          dataType: "json",

          success: function (data) {
            $.each(data.d, function (key, value) {
              if (value.bExiste == "EXISTE") {
                alert('Número de cheque ya ocupado de la misma entidad bancaria ');
              } else {
                allowSubmit = false;
                btn.trigger('click');
              }
            });
          },

          error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(textStatus + ": " + XMLHttpRequest.responseText);
          }
        });
      }

    });

    //----------------------------------------------------------------------------------------------------------------------

    $("#cmb_documento").focusout(function () {
      $("#txt_num_documento").removeAttr('disabled');
      $("#cmb_bancos").removeAttr('disabled');

      if ($("#cmb_documento").val() == "3") {
        $("#txt_num_documento").attr('disabled', 'disabled');
        $("#cmb_bancos").attr('disabled', 'disabled');
      }

      if ($("#cmb_documento").val() == "5") {
        $("#cmb_bancos").attr('disabled', 'disabled');
      }

      if ($("#cmb_documento").val() == "6") {
        $("#cmb_bancos").attr('disabled', 'disabled');
      }

    });

    //----------------------------------------------------------------------------------------------------------------------

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
      if (($("#cmb_guiadespacho").val() != null) && ($("#cmb_guiadespacho").val() != 0)) {
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
            $.each(data.d, function (key, value) {
              $("#cmb_facturas").empty();
              $("#cmb_facturas").append(value.nNumeroFactura);

              $("#lbl_valor_factura").empty();
              if (value.nSaldo == "0") {
                $("#lbl_valor_factura").append(value.nMontoFactura);
                document.getElementById("<%=hdd_facturas.ClientID%>").value = value.nNumeroFactura + '|' + value.nMontoFactura;
              } else {
                $("#lbl_valor_factura").append(value.nSaldo);
                document.getElementById("<%=hdd_facturas.ClientID%>").value = value.nNumeroFactura + '|' + value.nSaldo;
              }

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