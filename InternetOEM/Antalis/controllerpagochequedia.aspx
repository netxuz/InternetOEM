﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="controllerpagochequedia.aspx.cs" Inherits="ICommunity.Antalis.controllerpagochequedia" %>

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
  <link rel="stylesheet" type="text/css" href="https://cdnjs.cloudflare.com/ajax/libs/fancybox/3.5.7/jquery.fancybox.css" />
  <link rel="stylesheet" type="text/css" href="https://cdnjs.cloudflare.com/ajax/libs/fancybox/3.5.7/jquery.fancybox.min.css" />
  <style>
    .cmp-validado {
      font-size:1rem;
    }
    .green_text {
      color:#7ED321;
      font-weight:400;
    }
    .red_text {
      color:#D0021B;
      font-weight:400;
    }
    .blue_text {
      color:#1578FF;
      font-weight:400;
    }
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
  <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>
    <asp:HiddenField ID="hdd_cod_pago" runat="server" />
    <asp:HiddenField ID="hdd_cod_documento" runat="server" />
    <asp:HiddenField ID="hdd_tipo_documento" runat="server" />

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
          <asp:Label ID="lblTitle" runat="server" CssClass="lblTitle" Text=""></asp:Label>
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
          <span>CENTRO DE DISTRIBUCION:
            <asp:Label ID="lblCentroDistribucion" runat="server"></asp:Label></span>
        </div>
        <div class="col-md-4">
          <span>FECHA RENDICIÓN:
            <asp:Label ID="lblFecharecepcion" runat="server"></asp:Label></span>
        </div>
      </div>
      <!--ROW 1 -->
      <div id="idRow1" runat="server" visible="false" class="row vAlign">
        <div class="col-md-3">
          <span>RAZÓN SOCIAL:
            <asp:Label ID="lblRazonSocialPago" runat="server"></asp:Label></span>
        </div>
        <div class="col-md-3">
          <span>CUENTA CORRIENTE:
            <asp:Label ID="lblcuentacorriente" runat="server"></asp:Label></span>
        </div>
        <div class="col-md-3">
          <span>NUMERO CHEQUE / OPERACION:
            <asp:Label ID="lblNumOperacion" runat="server"></asp:Label></span>
        </div>
        <div class="col-md-3" id="idColBanco" runat="server" visible="false">
          <span>BANCO:
            <asp:Label ID="lblBanco" runat="server"></asp:Label></span>
        </div>
        <div class="col-md-3">
          <span>FECHA TRANSACCIÓN:
            <asp:Label ID="lblFechtransaccion" runat="server"></asp:Label></span>
        </div>
      </div>
      <!--ROW 2 -->
      <div id="idRow2" runat="server" visible="false" class="row vAlign">
        <div class="col-md-4">
          <span>IMPORTE:
            <asp:Label ID="lblimporte" runat="server"></asp:Label></span>
          <asp:HiddenField ID="hdd_importe" runat="server" />
        </div>
        <div class="col-md-4">
          <div class="md-form" style="width: 20rem;">
            <asp:TextBox ID="txt_importe_recibido" runat="server" CssClass="form-control"></asp:TextBox>
            <label for="txt_importe_recibido">IMPORTE RECIBIDO</label>
          </div>
        </div>
        <div class="col-md-4">
          <div class="md-form" style="width: 20rem;">
            <asp:TextBox ID="txt_discrepancia" runat="server" CssClass="form-control"></asp:TextBox>
            <label for="txt_discrepancia">DISCREPANCIA</label>
          </div>
        </div>
      </div>
      <!--ROW 3 -->
      <div id="idRow3" runat="server" visible="false" class="row vAlign">
        <div class="col-md-12">
          <asp:Button ID="btnCancelModify" runat="server" Text="CANCELAR" CssClass="btn btn-default" OnClick="btnCancelModify_Click" />
          <asp:Button ID="btnModificar" runat="server" Text="ACEPTAR" CssClass="btn btn-primary" OnClick="btnModificar_Click" />
        </div>
      </div>
      <div class="row">
        <br />
      </div>
      <div class="row">
        <div class="green_text cmp-validado">* dato validado</div>
        <div class="red_text cmp-validado">* dato no valido</div>
        <div class="blue_text cmp-validado">* dato no fue posible validarlo</div>
      </div>
      <div class="row">
        <asp:GridView ID="gdPagos" runat="server" CssClass="table table-hover"
          DataKeyNames="cod_documento, nod_cod_documento" BorderStyle="Solid"
          BorderWidth="0" GridLines="Horizontal"
          AutoGenerateColumns="false" OnRowCommand="gdPagos_RowCommand" OnPageIndexChanging="gdPagos_PageIndexChanging" OnRowDataBound="gdPagos_RowDataBound">
          <Columns>
            <asp:TemplateField>
              <ItemTemplate>
                <asp:LinkButton runat="server" ID="BtnlnkSI" CssClass="BtnColEditar" CommandName="SI">SI</asp:LinkButton>
              </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
              <ItemTemplate>
                <asp:LinkButton runat="server" ID="BtnlnkNO" CssClass="BtnColEditar" CommandName="NO">NO</asp:LinkButton>
              </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="TIPO DOCUMENTO" />
            <asp:BoundField HeaderText="# DOCUMENTO" DataField="num_documento" />
            <asp:BoundField HeaderText="RAZÓN SOCIAL" DataField="nom_deudor" />
            <asp:BoundField HeaderText="CUENTA CORRIENTE" DataField="cuenta_corriente" />
            <asp:BoundField HeaderText="FECHA DOCUMENTO" DataField="fch_documento" />
            <asp:BoundField HeaderText="BANCO" DataField="cod_banco" />
            <asp:BoundField HeaderText="IMPORTE" DataField="importe" DataFormatString="{0:N0}" />
            <asp:BoundField HeaderText="# GUIA DESPACHO" DataField="num_guia_despacho" />
            <asp:BoundField HeaderText="# FACTURA" DataField="num_factura" />
            
            <asp:BoundField HeaderText="VALOR FACTURA ORIGINAL" DataField="valor_factura_original" DataFormatString="{0:N0}" />
            <asp:BoundField HeaderText="SALDO FACTURA" DataField="importe_factura" DataFormatString="{0:N0}" />
            <asp:BoundField HeaderText="APLICACION PAGO FACTURA" DataField="aplicacion_pago_factura" DataFormatString="{0:N0}" />
            <asp:BoundField HeaderText="APLICACION PAGO NOTA CREDITO" DataField="aplicacion_nota_credito" DataFormatString="{0:N0}" />

            <asp:BoundField HeaderText="IMPORTE RECIBIDO" DataField="importe_recibido" DataFormatString="{0:N0}" />
            <asp:BoundField HeaderText="DISCREPANCIA" DataField="discrepancia" DataFormatString="{0:N0}" />
            <asp:TemplateField>
              <ItemTemplate>
                <asp:HyperLink ID="btn_img_frente" runat="server" CssClass="img_cheque"><span class="glyphicon glyphicon-picture"></span></asp:HyperLink>
              </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
              <ItemTemplate>
                <asp:HyperLink ID="btn_img_atras" runat="server" CssClass="img_cheque"><span class="glyphicon glyphicon-picture"></span></asp:HyperLink>
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
        </div>
        <div class="row">
          <span>Total Importe:
            <asp:Label ID="lblMonto" runat="server"></asp:Label></span>
        </div>
        <div class="row">
          <span>Total Recibido:
            <asp:Label ID="lblImporteTotalRecibido" runat="server"></asp:Label></span>
          <asp:HiddenField ID="hdd_importetotal_recibido" runat="server" />
        </div>
        <div class="row">
          <span>Total Discrepancia:
            <asp:Label ID="lblDiscrepanciaTotal" runat="server"></asp:Label></span>
          <asp:HiddenField ID="hdd_total_discrepancia" runat="server" />
        </div>
      </div>
      <div class="row">
        <div class="col-md-12 text-center">
          <asp:Button ID="btnRechazar" runat="server" Text="RECHAZAR" class="btn btn-default" Visible="false" OnClick="btnRechazar_Click" />
          <asp:Button ID="btnAceptar" runat="server" Text="ACEPTAR" class="btn btn-primary" Visible="false" OnClick="btnAceptar_Click" />
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
  <script src="https://code.jquery.com/jquery-3.4.1.min.js"></script>
  <script src="https://cdnjs.cloudflare.com/ajax/libs/fancybox/3.5.7/jquery.fancybox.js"></script>
  <script src="https://cdnjs.cloudflare.com/ajax/libs/fancybox/3.5.7/jquery.fancybox.min.js"></script>
  <script>
    $("#txt_importe_recibido").focusout(function () {
      if ($("#txt_importe_recibido").val() != '') {
        var iDiscrepancia = parseInt($("#hdd_importe").val()) - parseInt($("#txt_importe_recibido").val());
        $("#txt_discrepancia").val(iDiscrepancia);
        $("#txt_discrepancia").focus();
      }
    });
  </script>
</body>
</html>
