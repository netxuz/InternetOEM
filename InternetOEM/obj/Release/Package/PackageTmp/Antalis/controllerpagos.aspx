<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="controllerpagos.aspx.cs" Inherits="ICommunity.Antalis.controllerpagos" %>

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
    <asp:HiddenField ID="hdd_tipo_controller" runat="server" />
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
        <div class="col-md-12">
          <asp:Label ID="lblTitle" runat="server" CssClass="lblTitle" Text="PAGOS ANTALIS A VALIDAR"></asp:Label>
        </div>
      </div>
      <div class="row vAlign">
        <div class="col-md-3">
          <div class="md-form">
            <asp:TextBox ID="txt_num_valija" runat="server" CssClass="form-control"></asp:TextBox>
            <label for="txt_num_valija"># VALIJA</label>
          </div>
        </div>
        <div class="col-md-3">
          <div class="md-form">
            <asp:TextBox ID="txt_cliente" runat="server" CssClass="form-control"></asp:TextBox>
            <label for="txt_cliente">CLIENTE</label>
          </div>
        </div>
        <div class="col-md-3">
          <div class="md-form">
            <span for="cmb_centrodistribucion">CENTRO DE DISTRIBUCION:</span>
            <asp:DropDownList ID="cmb_centrodistribucion" CssClass="form-control" runat="server">
            </asp:DropDownList>
          </div>
        </div>
        <div class="col-md-3">
          <span for="cmb_documento">METODO DE PAGO: </span>
          <asp:DropDownList ID="cmb_documento" CssClass="form-control" runat="server">
            <asp:ListItem Text="<< Seleccione tipo de documento >>" Value=""></asp:ListItem>
          </asp:DropDownList>
        </div>
      </div>
      <div class="row vAlign">
        <div class="col-md-3">
          <div class="md-form">
            <span for="cmd_tipo_documento">TIPO DE DOCUMENTO:</span>
            <asp:DropDownList ID="cmd_tipo_documento" CssClass="form-control" runat="server">
              <asp:ListItem Text="<< Seleccione un opción >>" Value="0"></asp:ListItem>
              <asp:ListItem Text="Guia de despacho" Value="1"></asp:ListItem>
              <asp:ListItem Text="Factura" Value="2"></asp:ListItem>
              <asp:ListItem Text="Nota de crédito" Value="3"></asp:ListItem>
            </asp:DropDownList>
          </div>
        </div>
        <div class="col-md-3">
          <div class="md-form">
            <asp:TextBox ID="txt_num_documento" runat="server" CssClass="form-control"></asp:TextBox>
            <label for="txt_num_documento"># Documento</label>
          </div>
        </div>
        <div class="col-md-3">
          <span for="fch_inicio">FECHA INICIO</span>
          <div class="input-append date" id="dp3" data-date="<%= DateTime.Now.ToString("dd-MM-yyyy")  %>" data-date-format="dd-mm-yyyy">
            <asp:TextBox ID="fch_inicio" runat="server" CssClass="form-control"></asp:TextBox>
            <span class="add-on"><i class="icon-th"></i></span>
            <asp:HiddenField ID="hdd_fch_inicio" runat="server" />
          </div>
        </div>
        <div class="col-md-3">
          <span for="fch_hasta">FECHA FINAL</span>
          <div class="input-append date" id="dp4" data-date="<%= DateTime.Now.ToString("dd-MM-yyyy")  %>" data-date-format="dd-mm-yyyy">
            <asp:TextBox ID="fch_hasta" runat="server" CssClass="form-control"></asp:TextBox>
            <span class="add-on"><i class="icon-th"></i></span>
            <asp:HiddenField ID="hdd_fch_hasta" runat="server" />
          </div>
        </div>
      </div>
      <div class="row vAlign">
        <div class="col-md-12 text-center">
          <asp:Button ID="idBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary" Width="100px" OnClick="idBuscar_Click" />
        </div>
      </div>
      <div class="row"><br /></div>
      <div class="row">
        <div class="container">
          <asp:GridView ID="gdPagos" runat="server" CssClass="table table-hover"
            DataKeyNames="cod_pago, cod_tipo_pago" BorderStyle="Solid"
            BorderWidth="0" GridLines="Horizontal"
            AutoGenerateColumns="false" 
            OnSelectedIndexChanging="gdPagos_SelectedIndexChanging"
            OnPageIndexChanging="gdPagos_PageIndexChanging" 
            OnRowDataBound="gdPagos_RowDataBound">
            <Columns>
              <asp:CommandField ButtonType="Link" ShowSelectButton="true" SelectText="Sele" ItemStyle-CssClass="BtnColEditar" ItemStyle-Width="1px" />
              <asp:BoundField HeaderText="# Valija" DataField="cod_pago" />
              <asp:BoundField HeaderText="Centro Distribución" DataField="cod_centrodist" />
              <asp:BoundField HeaderText="Metodo de pago" DataField="cod_tipo_pago" />
              <asp:BoundField HeaderText="Fecha Recepción" DataField="fech_recepcion" />
            </Columns>
          </asp:GridView>
        </div>
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

    $("#idBuscar").click(function () {

      if (($("#txt_num_documento").val() != "") && ($("#cmd_tipo_documento").val() == "0")) {
        alert("Debe debe seleccionar el tipo de documento para realizar la busqueda");
        return false;
      }

      if (($("#fch_inicio").val() != "") && ($("#fch_hasta").val() != "")) {
        document.getElementById("<%=hdd_fch_inicio.ClientID%>").value = $("#fch_inicio").val();
        document.getElementById("<%=hdd_fch_hasta.ClientID%>").value = $("#fch_hasta").val();
        
      } else {
        document.getElementById("<%=hdd_fch_inicio.ClientID%>").value = "";
        document.getElementById("<%=hdd_fch_hasta.ClientID%>").value = "";

        if (($("#fch_inicio").val() != "") && ($("#fch_hasta").val() == "")) {
          alert("Debe ingresar fecha final para realizar la busqueda");
          return false;
        }

        if (($("#fch_inicio").val() == "") && ($("#fch_hasta").val() != "")) {
          alert("Debe ingresar fecha inicial para realizar la busqueda");
          return false;
        }
      }

    });

  </script>
</body>
</html>
