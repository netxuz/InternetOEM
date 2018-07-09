﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="app_antiguedad_deuda_fin.aspx.cs" Inherits="ICommunity.Reporting.app_antiguedad_deuda_fin" %>

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
        <asp:Label ID="lblTitle" runat="server" CssClass="lblTitle" Text="ANTIGUEDAD DE DEUDA"></asp:Label>
      </div>
      <div class="blq_search">
        <div><span>Seleccione tipo de consulta</span></div>
        <div></div>
        <telerik:RadTextBox ID="rdTxtDeudor" runat="server" CssClass="control-text-search" Enabled="false" Text=""></telerik:RadTextBox>
        <a id="btnSearch" href="#" class="btnsearch"></a>
        <a id="objShowSearch" href="#"></a>
        <asp:HiddenField ID="hddCodDeudor" runat="server" />
        <div class="lblerror">
          <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
        </div>
      </div>
      <div class="blq_btn_search">
        <div></div>
        <div>
          <asp:RadioButtonList ID="RdBtnTypeQuery" CssClass="inputcheckbox" runat="server">
            <asp:ListItem Text="Deudor" Selected="True" Value="0"></asp:ListItem>
            <asp:ListItem Text="Cliente" Value="1"></asp:ListItem>
            <asp:ListItem Text="Analista" Value="2"></asp:ListItem>
            <asp:ListItem Text="Vendedor" Value="3"></asp:ListItem>
          </asp:RadioButtonList>
        </div>
      </div>
      <div class="blq_btn_search">
        <div>
          <asp:Button ID="idBuscar" runat="server" Text="Buscar" OnClick="idBuscar_Click" CssClass="btn btn-lg btn-primary btn-block" Width="100px" OnClientClick="return onValidateAnalista();" />
        </div>
      </div>
      <div id="idGrilla" runat="server" visible="false">
        <telerik:RadGrid ID="rdGridAntiguedadDeuda" runat="server" OnNeedDataSource="rdGridAntiguedadDeuda_NeedDataSource" OnItemCommand="rdGridAntiguedadDeuda_ItemCommand" OnPreRender="rdGridAntiguedadDeuda_PreRender"
          AllowPaging="true" AllowSorting="true" ShowStatusBar="true" PageSize="10" GridLines="None" AllowAutomaticUpdates="true" AllowAutomaticInserts="true" AllowAutomaticDeletes="true" Skin="Sitefinity">
          <ExportSettings HideStructureColumns="true"></ExportSettings>
          <PagerStyle Mode="NextPrevAndNumeric" />
          <MasterTableView AutoGenerateColumns="false" ShowHeader="true" ShowFooter="true"
            TableLayout="Fixed" ShowHeadersWhenNoRecords="true" CommandItemDisplay="Top">
            <CommandItemSettings ShowExportToExcelButton="true" ShowRefreshButton="false" ShowAddNewRecordButton="false" />
            <Columns>
              <telerik:GridBoundColumn DataField="sTipoDocumento" HeaderText="Tipo Documento"
                UniqueName="sTipoDocumento">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="nNumeroFactura" HeaderText="Numero Factura"
                UniqueName="nNumeroFactura">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="ncodigodeudor" HeaderText="Código"
                UniqueName="ncodigodeudor">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="snombre" HeaderText="Deudor"
                UniqueName="snombre">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Left" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="total" HeaderText="Deuda Total"
                UniqueName="total">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Right" />
                <FooterStyle HorizontalAlign="Right" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="total_0" HeaderText="Deuda por Vencer"
                UniqueName="total_0">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Right" />
                <FooterStyle HorizontalAlign="Right" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="total_15" HeaderText="1 a 15 Vencida"
                UniqueName="total_15">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Right" />
                <FooterStyle HorizontalAlign="Right" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="total_30" HeaderText="16 a 30 Vencida"
                UniqueName="total_30">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Right" />
                <FooterStyle HorizontalAlign="Right" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="total_60" HeaderText="31 a 60 Vencida"
                UniqueName="total_60">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Right" />
                <FooterStyle HorizontalAlign="Right" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="total_90" HeaderText="61 a 90 Vencida"
                UniqueName="total_90">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Right" />
                <FooterStyle HorizontalAlign="Right" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="total_180" HeaderText="91 a 180 Vencida"
                UniqueName="total_180">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Right" />
                <FooterStyle HorizontalAlign="Right" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="total_360" HeaderText="180 a 360 Vencida"
                UniqueName="total_360">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Right" />
                <FooterStyle HorizontalAlign="Right" />
              </telerik:GridBoundColumn>
            </Columns>
          </MasterTableView>
        </telerik:RadGrid>

      </div>
      <div style="height: 30px;">
        <br />
        <br />
      </div>
    </div>
    <script>
      var x, y;
      var lblError = document.getElementById('<%= lblError.ClientID %>');
      $(document).ready(function () {

        /* Apply fancybox to multiple items */
        $("#btnSearch").click(function () {
          lblError.innerHTML = "";
          var rdBtnValor = $('#<%=RdBtnTypeQuery.ClientID %> input[type=radio]:checked').val();
          var objHref = document.getElementById("objShowSearch");

          if (rdBtnValor == "0") {
            objHref.href = "app_show_deudores.aspx";
            $("#objShowSearch").trigger("click");

          } else if (rdBtnValor == "1") {
            alert('Por favor, seleccione otra opción.');
            return;

          } else if (rdBtnValor == "2") {
            objHref.href = "app_show_analistas.aspx";
            $("#objShowSearch").trigger("click");

          } else if (rdBtnValor == "3") {
            objHref.href = "app_show_vendedores.aspx";
            $("#objShowSearch").trigger("click");
          }
        });

        $("#objShowSearch").fancybox({
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

        $('#<%=RdBtnTypeQuery.ClientID %>').change(function () {
          var objTxtDeudor = $find("<%= rdTxtDeudor.ClientID %>");

           lblError.innerHTML = "";
           objTxtDeudor.set_value('');
           hddCodDeudor.value = "";

         });

      });

       function onValidateAnalista() {
         breturn = true;
         var objTxtDeudor = $find("<%= rdTxtDeudor.ClientID %>");
        var rbvalue = $("input[@name=<%=RdBtnTypeQuery.ClientID%>]:radio:checked").val();

        if ((rbvalue != "1") && (objTxtDeudor.get_value() == "")) {
          lblError.innerHTML = "* Debe seleccionar el dato para la consulta.";
          breturn = false;
        }

        return breturn;
      }
    </script>
  </form>
</body>
</html>