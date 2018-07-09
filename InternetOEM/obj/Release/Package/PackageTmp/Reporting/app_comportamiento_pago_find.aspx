<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="app_comportamiento_pago_find.aspx.cs" Inherits="ICommunity.Reporting.app_comportamiento_pago_find" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta charset="utf-8">
  <meta http-equiv="X-UA-Compatible" content="IE=edge">
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <title></title>
  <link rel="stylesheet" href="../css/bootstrap.min.css">
  <link rel="stylesheet" type="text/css" href="../css/stylesdebtcontrol.css" media="screen" />
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>

  <script type="text/javascript" src="../Resources/fancybox/jquery.min.js"></script>
  <script type="text/javascript" src="../Resources/fancybox/jquery.mousewheel-3.0.4.pack.js"></script>
  <script type="text/javascript" src="../Resources/fancybox/jquery.fancybox-1.3.4.js"></script>
  <link rel="stylesheet" type="text/css" href="../Resources/fancybox/jquery.fancybox-1.3.4.css" media="screen" />
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
            <li><a href="#">Panel de Control</a></li>
          </ul>
        </div>
      </div>
    </nav>

    <div class="container">
      <div class="blq_tile">
        <asp:Label ID="lblTitle" runat="server" CssClass="lblTitle" Text="COMPORTAMIENTO DE PAGO"></asp:Label>
      </div>
      <div class="blq_search">
        <div><span>Analista</span></div>
        <div></div>
        <telerik:RadTextBox ID="rdTxtAnalista" runat="server" CssClass="control-text-search" Enabled="false" Text=""></telerik:RadTextBox>
        <a id="btnAnalista" href="app_show_analistas.aspx" class="btnsearch"></a>
        <asp:HiddenField ID="hddCodAnalista" runat="server" />
        <div class="lblerror">
          <asp:Label ID="lblErrorAnalista" runat="server" Text=""></asp:Label>
        </div>
      </div>
      <div class="blq_search">
        <div><span>Deudor</span></div>
        <div></div>
        <telerik:RadTextBox ID="rdTxtDeudor" runat="server" CssClass="control-text-search" Enabled="false" Text=""></telerik:RadTextBox>
        <a id="btnDeudores" href="app_show_deudores.aspx" class="btnsearch"></a>
        <asp:HiddenField ID="hddCodDeudor" runat="server" />
        <div class="lblerror">
          <asp:Label ID="lblErrorDeudor" runat="server" Text=""></asp:Label>
        </div>
      </div>
      <div class="blq_btn_search">
        <div></div>
        <div></div>
        <asp:RadioButtonList ID="rdBtnTypeQuery" CssClass="inputcheckbox" runat="server">
          <asp:ListItem Text="Cliente" Value="0" Selected="True"></asp:ListItem>
          <asp:ListItem Text="Analista" Value="2"></asp:ListItem>
          <asp:ListItem Text="Deudor" Value="3"></asp:ListItem>
        </asp:RadioButtonList>
      </div>
      <div class="blq_btn_search">
        <div>
          <asp:Button ID="idBuscar" runat="server" Text="Buscar" CssClass="btn btn-lg btn-primary btn-block" Width="100px" OnClick="idBuscar_Click" OnClientClick="return onValidateAnalista();" />
        </div>
      </div>
      <div id="idGrilla" runat="server" visible="false">

        <telerik:RadGrid ID="rdGridComportamientoPago" runat="server" OnItemCommand="rdGridComportamientoPago_ItemCommand1" OnNeedDataSource="rdGridComportamientoPago_NeedDataSource"
          AllowPaging="true" AllowSorting="true" ShowStatusBar="true" PageSize="10" GridLines="None"
          AllowAutomaticUpdates="true" AllowAutomaticInserts="true" AllowAutomaticDeletes="true" Skin="Sitefinity">
          <ExportSettings HideStructureColumns="true"></ExportSettings>
          <PagerStyle Mode="NextPrevAndNumeric" />
          <MasterTableView AutoGenerateColumns="false" ShowHeader="true"
            TableLayout="Fixed" ShowHeadersWhenNoRecords="true" CommandItemDisplay="Top">
            <CommandItemSettings ShowExportToExcelButton="true" ShowRefreshButton="false" ShowAddNewRecordButton="false" />
          </MasterTableView>
        </telerik:RadGrid>

      </div>
    </div>
    <script>
      var objLblAnalista = document.getElementById('<%= lblErrorAnalista.ClientID %>');
      var objLblDeudor = document.getElementById('<%= lblErrorDeudor.ClientID %>');
      var x, y, z, w;
      $(document).ready(function () {

        /* Apply fancybox to multiple items */

        $("#btnDeudores").fancybox({
          'width': 600,
          'height': 700,
          'transitionIn': 'elastic',
          'transitionOut': 'elastic',
          'speedIn': 600,
          'speedOut': 200,
          'overlayShow': false,
          'type': 'iframe',
          'onCleanup': function () {
            x = $("#fancybox-frame").contents().find("#hdd_razonsocial").val();
            y = $("#fancybox-frame").contents().find("#hdd_coddeudor").val();
          },
          'onClosed': function () {
            var text = $find("<%= rdTxtDeudor.ClientID %>");
            text.set_value(x);
            hddCodDeudor.value = y;
          }
        });

        $("#btnAnalista").fancybox({
          'width': 600,
          'height': 700,
          'transitionIn': 'elastic',
          'transitionOut': 'elastic',
          'speedIn': 600,
          'speedOut': 200,
          'overlayShow': false,
          'type': 'iframe',
          'onCleanup': function () {
            z = $("#fancybox-frame").contents().find("#hdd_analista").val();
            w = $("#fancybox-frame").contents().find("#hdd_codanalista").val();
          },
          'onClosed': function () {
            var text = $find("<%= rdTxtAnalista.ClientID %>");
            text.set_value(z);
            hddCodAnalista.value = w;
          }
        });

        $('#<%=rdBtnTypeQuery.ClientID %>').change(function () {
          var objTxtDeudor = $find("<%= rdTxtDeudor.ClientID %>");
          var objTxtAnalista = $find("<%= rdTxtAnalista.ClientID %>");

          objLblDeudor.innerHTML = "";
          objLblAnalista.innerHTML = "";

          objTxtDeudor.set_value('');
          hddCodDeudor.value = "";

          objTxtAnalista.set_value('');
          hddCodAnalista.value = "";


        });

      });

      function onValidateAnalista() {
        breturn = true;
        var objTxtDeudor = $find("<%= rdTxtDeudor.ClientID %>");
        var objTxtAnalista = $find("<%= rdTxtAnalista.ClientID %>");
        var rbvalue = $("input[@name=<%=rdBtnTypeQuery.ClientID%>]:radio:checked").val();

        if ((rbvalue == "3") && (objTxtDeudor.get_value() == "")) {
          objLblDeudor.innerHTML = "* Debe seleccionar deudor.";
          breturn = false;
        }

        if ((rbvalue == "2") && objTxtAnalista.get_value() == "") {
          objLblAnalista.innerHTML = "* Debe seleccionar analista.";
          breturn = false;
        }
        return breturn;
      }
    </script>

  </form>
</body>
</html>
