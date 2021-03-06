﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ingreso_pagos.aspx.cs" Inherits="ICommunity.Antalis.ingreso_pagos" EnableEventValidation="false" %>

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
    .debtcontrol {
      /*background: #fff !important;*/
      /*background-image: linear-gradient(135deg, #fff 70%, #3D5375);*/
    }

    header {
      background-image: url(../images/barrasuperior_menu.png);
      /*background-position: center;*/
      background-repeat: no-repeat;
      background-size: cover;
    }

    .nav-background {
      background: #3D5375 !important;
    }

    .tam-logo {
      width: 80px !important;
    }
    .navbar {
      padding-right:0!important;
      display:flex!important;
      align-items:center!important;
      padding:.5rem 1rem;
    }

    .navbar-nav {
      margin:7.5px -15px!important;
    }

    .navbar-brand {
      display:inline-block!important;
      padding-top:.3125rem!important;
      padding-bottom:.3125rem!important;
      margin-right:1rem!important;
      font-size:1.25rem!important;
      line-height:inherit!important;
      white-space:nowrap!important;
      float:none!important;
      height:auto!important;
      padding:unset!important;
    }

    @media (min-width: 992px) {
      .navbar-expand-lg .navbar-collapse {
        display:flex!important;
        flex-basis:auto;
      }
      .navbar-collapse {
        flex-grow:1;
        align-items:center;
      }
    }

    .divider-new, .navbar .nav-flex-icons {
      flex-direction:row;
    }
    .ml-auto, .mx-auto {
      margin-left:auto!important;
    }

    .nav-item {
      list-style:none!important;
    }

    @media (min-width: 768px) {
      .navbar-nav > li > a {
        padding-right:25px!important;
      }
    }
  </style>
</head>
<body>
  <form id="form1" runat="server" autocomplete="off">
    <asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>
    <asp:HiddenField ID="hddnkey_cliente" runat="server" />
    <asp:HiddenField ID="hdd_cod_pago" runat="server" />
    <asp:HiddenField ID="hdd_cod_documento" runat="server" />
    <asp:HiddenField ID="hdd_nod_documento" runat="server" />
    <asp:HiddenField ID="hdd_cod_factura" runat="server" />

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
          <asp:Label ID="lblTitle" runat="server" CssClass="lblTitle" Text="INGRESAR PAGOS"></asp:Label>
        </div>
        <div class="col-md-6 text-right">
          <asp:Label ID="lblValija" runat="server" CssClass="lblTitle"></asp:Label>
        </div>
      </div>
      <div class="row vAlign">
        <div class="col-md-3">
          <div class="md-form" style="width: 20rem;">
            <span for="cmb_cliente">CLIENTE:</span>
            <asp:DropDownList ID="cmb_cliente" CssClass="form-control" runat="server">
            </asp:DropDownList>
          </div>
        </div>
        <div class="col-md-3">
          <div class="md-form" style="width: 20rem;">
            <span for="cmb_centrodistribucion">CENTRO DE DISTRIBUCION:</span>
            <asp:DropDownList ID="cmb_centrodistribucion" CssClass="form-control" runat="server">
            </asp:DropDownList>
          </div>
        </div>
        <div class="col-md-3">
          <div class="md-form" style="width: 20rem;">
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
        </div>
        <div class="col-md-3">
          <div class="md-form" style="width: 20rem;">
            <label>
              FECHA RECEPCION:
            <asp:Label ID="lbl_fecha_recepcion" runat="server"></asp:Label></label>
            <asp:HiddenField ID="txt_fecha_recepcion" runat="server"></asp:HiddenField>
          </div>
        </div>
      </div>
      <div id="idRow1" runat="server" class="row vAlign">
        <!-- Material input -->
        <div class="col-md-2">
          <div class="md-form" style="width: 20rem;">
            <asp:TextBox ID="txt_codigosap" runat="server" CssClass="form-control"></asp:TextBox>
            <label for="txt_codigosap">CODIGO SAP</label>
          </div>
        </div>
        <div class="col-md-2">
          <div class="md-form" style="width: 20rem;">
            <asp:TextBox ID="lblNomDeudor" runat="server" CssClass="form-control"></asp:TextBox>
            <label for="txt_num_documento">RAZÓN SOCIAL</label>
          </div>
        </div>
        <div class="col-md-2">
          <div class="md-form" style="width: 20rem;">
            <asp:TextBox ID="txt_cta_cte" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
            <label for="txt_cta_cte">CUENTA CORRIENTE</label>
          </div>
        </div>
        <div class="col-md-2">
          <div class="md-form" style="width: 20rem;">
            <asp:TextBox ID="txt_num_documento" runat="server" CssClass="form-control"></asp:TextBox>
            <label for="txt_num_documento">NUMERO CHEQUE / OPERACION</label>
          </div>
        </div>
        <div class="col-md-2">
          <div class="md-form" style="width: 20rem;">
            <asp:TextBox ID="txt_importe" runat="server" CssClass="form-control"></asp:TextBox>
            <label for="txt_importe">IMPORTE</label>
          </div>
        </div>
        <div class="col-md-2">
          <div class="md-form" style="width: 20rem;">
            <span for="cmb_bancos">BANCO:</span>
            <asp:DropDownList ID="cmb_bancos" CssClass="form-control" runat="server"></asp:DropDownList>
          </div>
        </div>
      </div>
      <div id="idRow2" runat="server" class="row vAlign">
        <div class="col-md-2">
          <div class="md-form" style="width: 20rem;">
            <span for="fch_documento">FECHA DOCUMENTO</span>
            <div class="input-append date" id="dp4" data-date-format="dd-mm-yyyy">
              <asp:TextBox ID="fch_documento" runat="server" CssClass="form-control" ReadOnly></asp:TextBox>
              <span class="add-on"><i class="icon-th"></i></span>
              <asp:HiddenField ID="hdd_fchdocument" runat="server" />
            </div>
          </div>
        </div>
        <div class="col-md-2">
          <div class="md-form" style="width: 20rem;">
            <span for="cmb_guiadespacho">GUIA DESPACHO:</span>
            <asp:DropDownList ID="cmb_guiadespacho" CssClass="form-control" runat="server">
            </asp:DropDownList>
            <asp:HiddenField ID="hddGuiasDespacho" runat="server" />
          </div>
        </div>
        <div id="numguidespacho" class="col-md-2" style="width: 20rem; display: none;">
          <div class="md-form" style="width: 20rem;">
            <asp:TextBox ID="txt_num_guia_despacho" runat="server" CssClass="form-control"></asp:TextBox>
            <label for="txt_num_guia_despacho"># GUIA DESPACHO:</label>
          </div>
        </div>
        <div class="col-md-2">
          <div class="md-form" style="width: 20rem;">
            <asp:TextBox ID="hdd_num_factura" runat="server" CssClass="form-control"></asp:TextBox>
            <label for="hdd_num_factura"># FACTURA</label>
          </div>
        </div>
        <div class="col-md-2">
          <div class="md-form" style="width: 20rem;">
            <asp:TextBox ID="txt_valor_factura" runat="server" CssClass="form-control"></asp:TextBox>
            <label for="txt_valor_factura">SALDO FACTURA</label>
          </div>
        </div>
        <div class="col-md-2">
          <div class="md-form" style="width: 20rem;">
            <asp:TextBox ID="txt_aplicacion_pago_factura" runat="server" CssClass="form-control"></asp:TextBox>
            <label for="txt_aplicacion_pago_factura">APLICACION PAGO FACTURA</label>
          </div>
        </div>
      </div>
      <div id="idRow4" runat="server" class="row vAlign">
        <div class="col-md-2">
          <div class="md-form" style="width: 20rem;">
            <span for="cmb_nota_credito"># NOTA DE CREDITO:</span>
            <asp:DropDownList ID="cmb_nota_credito" CssClass="form-control" runat="server">
            </asp:DropDownList>
            <asp:HiddenField ID="hddNotaCredito" runat="server" />
            <asp:HiddenField ID="hddCantNotaCredito" runat="server" />
          </div>
        </div>
        <div class="col-md-2">
          <div class="md-form" style="width: 20rem;">
            <asp:TextBox ID="txt_saldo_nota_credito" runat="server" CssClass="form-control"></asp:TextBox>
            <label for="txt_saldo_nota_credito">SALDO NOTA CREDITO</label>
          </div>
        </div>
        <div class="col-md-2">
          <div class="md-form" style="width: 20rem;">
            <asp:TextBox ID="txt_aplicacion_nota_credito" runat="server" CssClass="form-control"></asp:TextBox>
            <label for="txt_aplicacion_nota_credito">APLICACION NOTA CREDITO</label>
          </div>
        </div>
        <div class="col-md-1">
          <input type="button" value="+" id="someId" class="addButton" data-target-class="someClass" />
        </div>
        <div class="col-md-2">
          <div class="md-form" style="width: 20rem;">
            <asp:TextBox ID="txt_nuevo_saldo_factura" runat="server" CssClass="form-control"></asp:TextBox>
            <label for="txt_nuevo_saldo_factura">TOTAL SALDO FACTURA</label>
            <asp:HiddenField ID="hdd_nuevo_saldo_factura" runat="server" />
          </div>
        </div>
      </div>
      <div id="idRow5" class="row vAlignDinamic">
        <div class="col-md-2">
        </div>
        <div id="someClass" runat="server" class="col-md-6 someClass">
        </div>
        <div class="col-md-1">
        </div>
        <div class="col-md-2">
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
          DataKeyNames="cod_documento, nod_cod_documento" BorderStyle="Solid"
          BorderWidth="0" GridLines="Horizontal"
          AutoGenerateColumns="false"
          OnRowDeleting="gdPagos_RowDeleting"
          OnSelectedIndexChanged="gdPagos_SelectedIndexChanged" OnRowCommand="gdPagos_RowCommand"
          OnPageIndexChanging="gdPagos_PageIndexChanging" OnRowDataBound="gdPagos_RowDataBound">
          <Columns>
            <asp:TemplateField>
              <ItemTemplate>
                <asp:LinkButton runat="server" ID="BtnSameData" CssClass="" CommandName="SameData"><span class="glyphicon glyphicon-edit"></span>  UTILIZAR MISMO PAGO</asp:LinkButton>
              </ItemTemplate>
              <ItemStyle Width="150px" />
            </asp:TemplateField>
            <asp:CommandField ButtonType="Link" ShowDeleteButton="true" DeleteText="Dele" ItemStyle-CssClass="BtnColEliminar" ItemStyle-Width="1px" />
            <asp:CommandField ButtonType="Link" ShowSelectButton="true" SelectText="Sele" ItemStyle-CssClass="BtnColEditar" ItemStyle-Width="1px" />
            <asp:BoundField HeaderText="TIPO DOCUMENTO" />
            <asp:BoundField HeaderText="# DOCUMENTO" DataField="num_documento" />
            <asp:BoundField HeaderText="RAZÓN SOCIAL" DataField="nom_deudor" />
            <asp:BoundField HeaderText="CUENTA CORRIENTE" DataField="cuenta_corriente" />
            <asp:BoundField HeaderText="CODIGO BANCO" DataField="cod_banco" />
            <asp:BoundField HeaderText="FECHA DOCUMENTO" DataField="fch_documento" />
            <asp:BoundField HeaderText="IMPORTE" DataField="importe" DataFormatString="{0:N0}" />
            <asp:BoundField HeaderText="# GUIA DESPACHO" DataField="num_guia_despacho" />
            <asp:BoundField HeaderText="# FACTURA" DataField="num_factura" />
            <asp:BoundField HeaderText="VALOR FACTURA ORIGINAL" DataField="valor_factura_original" DataFormatString="{0:N0}" />
            <asp:BoundField HeaderText="SALDO FACTURA" DataField="importe_factura" DataFormatString="{0:N0}" />
            <asp:BoundField HeaderText="APLICACION PAGO FACTURA" DataField="aplicacion_pago_factura" DataFormatString="{0:N0}" />
            <asp:BoundField HeaderText="APLICACION PAGO NOTA CREDITO" DataField="aplicacion_nota_credito" DataFormatString="{0:N0}" />
            <asp:TemplateField HeaderText="">
              <ItemTemplate>
                <asp:LinkButton runat="server" ID="btnNotaCredito" CssClass="" CommandName="NotaCredito" Visible="false"><span class="	glyphicon glyphicon-search"></span>  VER NOTA CREDITO</asp:LinkButton>
              </ItemTemplate>
            </asp:TemplateField>
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
    <div class="container contentcenter">
      <div class="modal fade" id="myModal" role="dialog">
        <div class="modal-dialog modal-lg">
          <div class="modal-content">
            <div class="modal-header">
              <button type="button" class="close" data-dismiss="modal">&times;</button>
              <h4 class="modal-title">Notas de Crédito Aplicadas</h4>
            </div>
            <div class="modal-body">
              <div class="row rowcenter">
                <div class="col-md">
                  <asp:GridView ID="GridNC" runat="server" CssClass="table thead-light" BorderStyle="Solid" BorderWidth="0" GridLines="Horizontal" AutoGenerateColumns="false">
                    <Columns>
                      <asp:BoundField HeaderText="N° Nota Crédito" DataField="num_nc">
                        <HeaderStyle CssClass="tbTitRow" HorizontalAlign="Center" />
                        <ItemStyle CssClass="tbTitRow" HorizontalAlign="Center" />
                      </asp:BoundField>

                      <asp:BoundField HeaderText="Aplicacion Nota Credito" DataField="aplicacion_nota_credito">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Right" />
                      </asp:BoundField>
                    </Columns>
                  </asp:GridView>
                </div>
              </div>
            </div>
            <div class="modal-footer">
              <button type="button" class="btn btn-primary" data-dismiss="modal">Cerrar</button>
            </div>
          </div>
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
  <style>
    .contentcenter {
      text-align: center !important;
    }

    .rowcenter {
      display: inline-block;
      width: 60%;
    }
  </style>
  <script type="text/javascript" src="../js/mdb.min.js"></script>
  <script>
    function showCreateThanksYouForm(CodDocumento) {
      $.ajax({
        type: "POST",
        url: "ingreso_pagos.aspx/wsUpdateTable",
        data: '{"CodDocumento":"' + CodDocumento + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccess,
        failure: function (response) {
          alert('failure: ' + response.d);
        },
        error: function (response) {
          alert('error: ' + response.d);
        }
      });
    }

    function OnSuccess(response) {
      var xmlDoc = $.parseXML(response.d);
      var xml = $(xmlDoc);
      var customers = xml.find("Table");
      var row = $("[id*=GridNC] tr:last-child").clone(true);
      $("[id*=GridNC] tr").not($("[id*=GridNC] tr:first-child")).remove();

      $.each(customers, function () {
        var customer = $(this);
        $("td", row).eq(0).html($(this).find("num_nc").text());
        $("td", row).eq(1).html($(this).find("aplicacion_nota_credito").text());
        $("[id*=GridNC]").append(row);
        row = $("[id*=GridNC] tr:last-child").clone(true);
      });

      $('#myModal').modal('show');
    }

    function dropRow(idRow, sNumNC, iAppNC) {
      var iSalFactura = parseInt($("#txt_nuevo_saldo_factura").val().split('.').join(''));
      var iVal = iSalFactura + parseInt(iAppNC.split('.').join(''));
      var iValTtNc = parseInt($("#hdd_val_tt_nc").val().split('.').join('')) - parseInt(iAppNC.split('.').join(''));

      $("#hdd_val_tt_nc").val(iValTtNc);
      $("#txt_nuevo_saldo_factura").val(iVal);
      $("#hdd_nuevo_saldo_factura").val(iVal);

      $("#idrow_" + idRow).remove();

      $('#cmb_nota_credito option').each(function () {
        if ($(this).text() == sNumNC) {
          iAppNC = parseInt($(this).val()) + parseInt(iAppNC);
          $('#cmb_nota_credito option:contains(' + $(this).text() + ')').remove();
        }
      });

      $('#cmb_nota_credito').append($('<option>', {
        value: iAppNC,
        text: sNumNC
      }));
    }


    $(document).ready(function () {
      $("#txt_num_documento").focusout(function () {
        var valor = $('#txt_num_documento').val();
        //alert("valor : " + valor);
        //alert("isNumeric : " + $.isNumeric(valor));
        if (!$.isNumeric(valor)) {
          alert('El número de cheque / operación debe ser numerico');
          $('#txt_num_documento').val('');
        } else {
          $('#txt_num_documento').val(parseInt(valor));
        }
      });

      // Define a click handler for 
      $('input.addButton').click(function () {
        if ($("#hddNotaCredito").val() != "") {
          if ($('#hddCantNotaCredito').val() == "")
            iCant = 1;
          else
            iCant = parseInt($('#hddCantNotaCredito').val()) + 1;

          $('#hddCantNotaCredito').val(iCant);

          var target_class = $(this).attr('data-target-class');

          if (iCant == 1) {
            $("." + target_class).append("<div class='row'><div class='col-md-3 text-center'><span># NOTA DE CREDITO:</span></div><div class='col-md-3 text-center'><span>SALDO NOTA DE CREDITO:</span></div><div class='col-md-3 text-center'><span>APLICACION NOTA DE CREDITO:</span></div><div class='col-md-1'></div></div>");
          }

          $("." + target_class).append("<div id='idrow_" + iCant + "' class='row'><div class='col-md-3'><input type='text' id='txt_num_nc_" + iCant + "' name='txt_num_nc_" + iCant + "' value='" + $('#hddNotaCredito').val() + "' class='form-control'></div><div class='col-md-3'><input type='hidden' id='hdd_cod_nc_" + iCant + "' name='hdd_cod_nc_" + iCant + "' value='" + $('#hddNotaCredito').val() + "'><input id='txt_saldo_nc_" + iCant + "' name='txt_saldo_nc_" + iCant + "' type='text' value='" + $("#cmb_nota_credito").val() + "' class='form-control'></div><div class='col-md-3'><input id='txt_apl_nc_" + iCant + "' name='txt_apl_nc_" + iCant + "' type='text' value='" + $('#txt_aplicacion_nota_credito').val() + "'  class='form-control'></div><div class='col-md-1'><input type='button' value='-' id='idLessBtn" + iCant + "' onclick='dropRow(\"" + iCant + "\",\"" + $('#hddNotaCredito').val() + "\",\"" + $('#txt_aplicacion_nota_credito').val() + "\")' /></div></div>");

          var iValTtNc = $('#txt_aplicacion_nota_credito').val();
          if (iCant == 1) {
            $("." + target_class).append("<input type='hidden' id='hdd_val_tt_nc' name='hdd_val_tt_nc' value='" + iValTtNc + "'>")
          } else {
            var iValTtNcAnt = $("#hdd_val_tt_nc").val();
            var iValTtNc = parseInt(iValTtNc.split('.').join('')) + parseInt(iValTtNcAnt.split('.').join(''));
            $("#hdd_val_tt_nc").val(iValTtNc);
          }

          $('#hddNotaCredito').val('');
          $('#txt_saldo_nota_credito').val('');
          $('#txt_aplicacion_nota_credito').val('');
          $("#cmb_nota_credito option:selected").remove();
        } else {
          alert("debe seleccionar una nota de crédito");
          return false;
        }

      });
    });

    $(function () {
      $("#dp4").datepicker();
    });

    //----------------------------------------------------------------------------------------------------------------------

    var allowSubmit = true;
    $("#btnIngresarImportes").click(function (e) {

      if ($("#cmb_cliente").val() == "") {
        alert('Debe seleccionar cliente');
        return false;
      }

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

      if ((($("#cmb_documento").val() == "1") || ($("#cmb_documento").val() == "2")) && ($("#txt_cta_cte").val() == "")) {
        alert('Debe ingresar el número de cuenta corriente');
        $("#txt_cta_cte").focus();
        return false;
      }

      if (($("#cmb_documento").val() != "3") && ($("#txt_num_documento").val() == "")) {
        alert('Debe ingresar código / número del documento');
        $("#txt_num_documento").focus();
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

          var fechahoy = new Date();
          fechahoy.setDate(fechahoy.getDate() - 90);

          var sFechaDoc = $('#fch_documento').val()
          var sDia = sFechaDoc.substring(0, 2);
          var sMes = sFechaDoc.substring(3, 5);
          var sAno = sFechaDoc.substring(6, 10);
          var fechacheque = new Date(sAno, parseInt(sMes) - 1, sDia);

          if (($("#cmb_documento").val() == "1") && (fechacheque < fechahoy)) {
            alert('La fecha del documento no puede ser inferior a 90 días');
            return false;
          }

          //if (($("#cmb_documento").val() == "2") && (parseInt(sFchDia) >= parseInt(sFchDoc))) {
          //  alert('La fecha del documento no puede ser con fecha menor o igual a la fecha actual');
          //  return false;
          //}
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
        $("#txt_importe").focus();
        return false;
      }

      var ImporteTotal = $("#txt_importe").val();
      if (ImporteTotal.indexOf(',') != -1) {
        alert('El importe total no puede tener decimales');
        $("#txt_importe").focus();
        return false;
      }

      if (($("#txt_valor_factura").val() == "")) {
        alert('Debe ingresar el importe de la factura a pagar');
        $("#txt_valor_factura").focus();
        return false;
      }

      var ImporteFactura = $("#txt_valor_factura").val();
      if (ImporteFactura.indexOf(',') != -1) {
        alert('El importe de la factura no puede tener decimales');
        $("#txt_valor_factura").focus();
        return false;
      }

      if (($("#hdd_cod_documento").val() != "") || ($("#hdd_nod_documento").val() != "")) {
        allowSubmit = false;
      }

      var btn = $(this);
      if ((allowSubmit) && (($("#cmb_documento").val() == "1") || ($("#cmb_documento").val() == "2"))) {
        e.preventDefault();
        var cUrl = "ingreso_pagos.aspx/getValida";
        var datos = "{sCodNumDocumento:" + $("#txt_num_documento").val() + ",sCodBanco:" + $("#cmb_bancos").val() + ",sCtacte:" + $("#txt_cta_cte").val() + "}";
        $.ajax({
          type: "POST",
          url: cUrl,
          data: datos,
          contentType: "application/json; charset=utf-8",
          dataType: "json",

          success: function (data) {
            $.each(data.d, function (key, value) {
              if (value.bExiste == "EXISTE") {
                alert('Número de cheque ya ocupado. Misma cuenta corriente y entidad bancaria.');
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

      $("#hdd_num_factura").removeAttr('disabled');

    });

    //----------------------------------------------------------------------------------------------------------------------
    

    $("#cmb_documento").focusout(function () {
      $("#txt_num_documento").removeAttr('disabled');
      $("#cmb_bancos").removeAttr('disabled');
      $("#txt_cta_cte").removeAttr('disabled');



      if (($("#cmb_documento").val() != "1") && ($("#cmb_documento").val() != "2")) {
        $("#txt_cta_cte").attr('disabled', 'disabled');
      }

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

        if ($("#cmb_cliente").val() == "") {
          alert('Debe seleccionar cliente');
          return false;
        }

        var cUrl = "ingreso_pagos.aspx/getGuiasDespacho";
        var datos = "{nkeycliente:" + $("#cmb_cliente").val() + ",ncodigodeudor:" + $("#txt_codigosap").val() + "}";
        $.ajax({
          type: "POST",
          url: cUrl,
          data: datos,
          contentType: "application/json; charset=utf-8",
          dataType: "json",

          success: function (data) {

            $("#cmb_guiadespacho").empty().append($("<option></option>").val("0").html("<< Seleccione Guia Despacho >>"));
            if ($("#cmb_documento").val() == "6")
              $("#cmb_guiadespacho").append($("<option></option>").val("x").html("<< Agregar Guia Despacho >>"));

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

        var cUrl = "ingreso_pagos.aspx/getNotaCredito";
        var datos = "{nkeycliente:" + $("#cmb_cliente").val() + ",ncodigodeudor:" + $("#txt_codigosap").val() + "}";
        $.ajax({
          type: "POST",
          url: cUrl,
          data: datos,
          contentType: "application/json; charset=utf-8",
          dataType: "json",

          success: function (data) {

            $("#cmb_nota_credito").empty().append($("<option></option>").val("0").html("<< Seleccione Nota Crédito >>"));
            $.each(data.d, function (key, value) {
              var option = $(document.createElement("option"));
              if (value.sExiste == "V") {
                if (value.nSaldo != "0") {
                  option.html(value.nNumeroNotaCredito);
                  option.val(value.nSaldo);
                }
              } else {
                option.html(value.nNumeroNotaCredito);
                option.val(value.nMontoNotaCredito);
              }
              $("#cmb_nota_credito").append(option);
            });
          },

          error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(textStatus + ": " + XMLHttpRequest.responseText);
          }
        });


        var cUrl = "ingreso_pagos.aspx/getDeudor";
        var datos = "{nKeyCliente:" + $("#cmb_cliente").val() + ",nCodigoDeudor:" + $("#txt_codigosap").val() + "}";
        $.ajax({
          type: "POST",
          url: cUrl,
          data: datos,
          contentType: "application/json; charset=utf-8",
          dataType: "json",

          success: function (data) {
            $("#lblNomDeudor").empty();
            $("#lblNomDeudor").val(data.d);
            $("#lblNomDeudor").focus();
          },

          error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(textStatus + ": " + XMLHttpRequest.responseText);
          }
        });


      } else {
        $("#cmb_guiadespacho").empty();
        $("#lblNomDeudor").empty();
        $("#cmb_nota_credito").empty();
      }



    });

    //----------------------------------------------------------------------------------------------------------------------

    $("#cmb_guiadespacho").focusout(function () {
      //96829710
      if (($("#cmb_guiadespacho").val() != null) && ($("#cmb_guiadespacho").val() != 0)) {
        if ($("#cmb_guiadespacho").val() != "x") {
          var obj = document.getElementById("numguidespacho");
          obj.style.display = "none";

          var target = $("#cmb_facturas");
          var cUrl = "ingreso_pagos.aspx/getFacturas";

          if ($("#cmb_cliente").val() == "") {
            alert('Debe seleccionar cliente');
            return false;
          }

          //var datos = "{nKeyCliente:" + $("#hddnkey_cliente").val() + ",nCodigoDeudor:" + $("#txt_codigosap").val() + "}";
          var datos = "{sGuiaDespacho:" + $("#cmb_guiadespacho").val() + ",nkeycliente:" + $("#cmb_cliente").val() + "}";
          $.ajax({
            type: "POST",
            url: cUrl,
            data: datos,
            contentType: "application/json; charset=utf-8",
            dataType: "json",

            success: function (data) {
              $.each(data.d, function (key, value) {
                $("#hdd_num_factura").removeAttr('disabled');
                $("#hdd_num_factura").empty();
                $("#hdd_num_factura").val(value.nNumeroFactura);
                $("#hdd_num_factura").focus();
                $("#hdd_num_factura").attr('disabled', 'disabled');

                $("#txt_valor_factura").empty();
                if (value.nSaldo == "0") {
                  $("#txt_valor_factura").val(value.nMontoFactura);
                  $("#txt_valor_factura").focus();
                  $("#txt_valor_factura").attr('disabled', 'disabled');
                  $("#txt_aplicacion_pago_factura").val(value.nMontoFactura);
                } else {
                  $("#txt_valor_factura").val(value.nSaldo);
                  $("#txt_valor_factura").focus();
                  $("#txt_valor_factura").attr('disabled', 'disabled');
                  $("#txt_aplicacion_pago_factura").val(value.nSaldo);
                }
                $("#txt_aplicacion_pago_factura").focus();
                var nValor = parseInt($("#txt_valor_factura").val().split('.').join('')) - parseInt($("#txt_aplicacion_pago_factura").val().split('.').join(''))
                $("#txt_nuevo_saldo_factura").val(nValor);
                $("#hdd_nuevo_saldo_factura").val(nValor);
                $("#txt_nuevo_saldo_factura").focus();
                $("#txt_nuevo_saldo_factura").attr('disabled', 'disabled');

              });
            },

            error: function (XMLHttpRequest, textStatus, errorThrown) {
              alert(textStatus + ": " + XMLHttpRequest.responseText);
            }
          });
        } else {
          var obj = document.getElementById("numguidespacho");
          obj.style.display = "block";
          $("#numguidespacho").focus();
          alert('debe ingresar guia y factura');
          return;
        }
      } else {
        $("#cmb_facturas").empty();
      }
    });

    //----------------------------------------------------------------------------------------------------------------------

    $("#txt_aplicacion_pago_factura").focusout(function () {

      if ($("#cmb_nota_credito").val() != 0) {
        var nValor = parseInt($("#txt_valor_factura").val().split('.').join('')) - parseInt($("#txt_aplicacion_pago_factura").val().split('.').join('')) - parseInt($("#txt_aplicacion_nota_credito").val().split('.').join(''));

        $("#txt_nuevo_saldo_factura").val(nValor);
        $("#hdd_nuevo_saldo_factura").val(nValor);
        $("#txt_nuevo_saldo_factura").focus();
      } else {
        var nValor = parseInt($("#txt_valor_factura").val().split('.').join('')) - parseInt($("#txt_aplicacion_pago_factura").val().split('.').join(''));

        if (parseInt(nValor) < 0)
          nValor = 0;

        $("#txt_nuevo_saldo_factura").val(nValor);
        $("#hdd_nuevo_saldo_factura").val(nValor);
        $("#txt_nuevo_saldo_factura").focus();
        $("#txt_nuevo_saldo_factura").attr('disabled', 'disabled');
      }

    });

    //----------------------------------------------------------------------------------------------------------------------

    $('#cmb_nota_credito').trigger('change'); //This event will fire the change event. 

    $('#cmb_nota_credito').change(function () {
      if (($("#cmb_guiadespacho").val() == null) || ($("#cmb_guiadespacho").val() == 0)) {
        alert("Debe seleccionar una guia de despacho antes de seleccionar una nota de crédito");
        $("#cmb_nota_credito").val("0");
      } else {
        if (($("#txt_valor_factura").val() != "") && ($("#txt_aplicacion_pago_factura").val() != "")) {
          var data = $(this).val();
          if (data != "")
            $("#hddNotaCredito").val($('#cmb_nota_credito option:selected').text());
          else
            $("#hddNotaCredito").val('');

          $("#txt_saldo_nota_credito").val(data);
          $("#txt_saldo_nota_credito").focus();
          $("#txt_saldo_nota_credito").attr('disabled', 'disabled');
          $("#txt_aplicacion_nota_credito").val(data);
          //$("#txt_aplicacion_nota_credito").focus();

          var nValor;
          if (data != "") {
            nValor = parseInt($("#txt_valor_factura").val().split('.').join('')) - parseInt($("#txt_aplicacion_pago_factura").val().split('.').join('')) - parseInt($("#txt_aplicacion_nota_credito").val().split('.').join(''));
          } else {
            nValor = parseInt($("#txt_valor_factura").val().split('.').join('')) - parseInt($("#txt_aplicacion_pago_factura").val().split('.').join(''));
          }

          if ($('#hddCantNotaCredito').val() != "") {
            nValor = parseInt(nValor) - parseInt($("#hdd_val_tt_nc").val().split('.').join(''));
          }

          if (parseInt(nValor) < 0)
            nValor = "0";

          $("#txt_nuevo_saldo_factura").val(nValor);
          $("#hdd_nuevo_saldo_factura").val(nValor);
          $("#txt_aplicacion_nota_credito").focus();
          $("#txt_nuevo_saldo_factura").attr('disabled', 'disabled');
        } else {
          alert('Debe ingresar datos de numero de guia, numero factura y saldo factura antes de seleccionar la nota de crédito');
          $("#cmb_nota_credito").val("0");
          return;
        }

      }
    });

    //----------------------------------------------------------------------------------------------------------------------

    $("#txt_aplicacion_nota_credito").focusout(function () {
      var nValor;

      if ($("#txt_aplicacion_nota_credito").val() != "") {
        nValor = parseInt($("#txt_valor_factura").val().split('.').join('')) - parseInt($("#txt_aplicacion_pago_factura").val().split('.').join('')) - parseInt($("#txt_aplicacion_nota_credito").val().split('.').join(''));
      } else {
        nValor = parseInt($("#txt_valor_factura").val().split('.').join('')) - parseInt($("#txt_aplicacion_pago_factura").val().split('.').join(''));
      }

      if ($('#hddCantNotaCredito').val() != "") {
        nValor = parseInt(nValor) - parseInt($("#hdd_val_tt_nc").val().split('.').join(''));
      }

      if (parseInt(nValor) < 0)
        nValor = "0";

      $("#txt_nuevo_saldo_factura").val(nValor);
      $("#hdd_nuevo_saldo_factura").val(nValor);
      $("#txt_nuevo_saldo_factura").focus();
      $("#txt_nuevo_saldo_factura").attr('disabled', 'disabled');
    });


  </script>
</body>
</html>
