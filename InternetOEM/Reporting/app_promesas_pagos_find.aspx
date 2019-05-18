<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="app_promesas_pagos_find.aspx.cs" Inherits="ICommunity.Reporting.app_promesas_pagos_find" %>

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
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:HiddenField ID="hdd_arrNkeyCliente" runat="server" />
    <asp:HiddenField ID="hdd_cli_show" runat="server" />
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
      <div class="blq_tile">
        <asp:Label ID="lblTitle" runat="server" CssClass="lblTitle" Text="PROMESAS DE PAGO"></asp:Label>
      </div>
      <div class="row">
        <div id="colClientes" class="col-md-4" runat="server" visible="false">
          <div><span>Clientes</span></div>
          <div></div>
          <asp:DropDownList ID="cmbCliente" CssClass="inputCmbBox" runat="server">
          </asp:DropDownList>
        </div>
        <div id="colHolding" class="col-md-4" runat="server" visible="false">
          <div><span>Holding</span></div>
          <div></div>
          <asp:DropDownList ID="cmbHolding" CssClass="inputCmbBox" runat="server">
          </asp:DropDownList>
        </div>
      </div>
      <div class="blq_search">
        <div><span>Analista</span></div>
        <div></div>
        <telerik:RadTextBox ID="rdTxtDeudor" runat="server" CssClass="control-text-search" Enabled="false" Text=""></telerik:RadTextBox>
        <a id="btnDeudores" href="app_show_analistas.aspx" class="btnsearch"></a>
        <div class="lblerror"><asp:Label ID="lblError" runat="server" Text=""></asp:Label></div>
        <asp:HiddenField ID="hddCodDeudor" runat="server" />
      </div>
      <div class="blq_search">
        <div><span>&nbsp;</span></div>
        <div></div>
        <asp:DropDownList ID="rdCmbTypeQuery" CssClass="inputCmbBox" runat="server">
          <asp:ListItem Text="Cliente" Value="true"></asp:ListItem>
          <asp:ListItem Text="Analista" Value="false"></asp:ListItem>
        </asp:DropDownList>
      </div>
      <div class="blq_date">
        <div><span>Fecha Inicio</span></div>
        <div>
          <telerik:RadDatePicker ID="RadDatePicker1" runat="server" AutoPostBack="true"
            DateInput-EmptyMessage="" MinDate="01/01/1000" MaxDate="01/01/3000">
            <Calendar>
              <SpecialDays>
                <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
              </SpecialDays>
            </Calendar>
          </telerik:RadDatePicker>
        </div>
      </div>
      <div class="blq_date">
        <div><span>Fecha Hasta</span></div>
        <div>
          <telerik:RadDatePicker ID="RadDatePicker2" runat="server" AutoPostBack="true"
            DateInput-EmptyMessage="" MinDate="01/01/1000" MaxDate="01/01/3000">
            <Calendar>
              <SpecialDays>
                <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
              </SpecialDays>
            </Calendar>
          </telerik:RadDatePicker>
        </div>
      </div>
      <div class="blq_btn_search">
        <div>
          <asp:Button ID="idBuscar" runat="server" Text="Buscar" CssClass="btn btn-lg btn-primary btn-block" Width="100px" OnClick="idBuscar_Click" OnClientClick="return onValidateAnalista();" />
        </div>
      </div>
      <div id="idGrilla" runat="server" visible="false">
        <asp:Label ID="lblmoneda" runat="server" CssClass="lblmoneda"></asp:Label>
        <telerik:RadGrid ID="rdGridPromesaPagos" runat="server" OnNeedDataSource="rdGridPromesaPagos_NeedDataSource" OnItemCommand="rdGridPromesaPagos_ItemCommand" OnItemDataBound="rdGridPromesaPagos_ItemDataBound" AllowPaging="true" AllowSorting="true" ShowStatusBar="true" PageSize="10" GridLines="None" AllowAutomaticUpdates="true" AllowAutomaticInserts="true" AllowAutomaticDeletes="true" Skin="Sitefinity">
          <ExportSettings HideStructureColumns="true"></ExportSettings>
          <PagerStyle Mode="NextPrevAndNumeric" />
          <MasterTableView AutoGenerateColumns="false" ShowHeader="true"
            TableLayout="Fixed" ShowHeadersWhenNoRecords="true" CommandItemDisplay="Top">
            <CommandItemSettings ShowExportToExcelButton="true" ShowRefreshButton="false" ShowAddNewRecordButton="false" />
            <Columns>
              <telerik:GridBoundColumn DataField="CódigoDeudor" HeaderText="Código Deudor"
                UniqueName="CódigoDeudor">
                <HeaderStyle Font-Size="Smaller" Width="100px" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="Razón_Social" HeaderText="Razón Social"
                UniqueName="Razón_Social">
                <HeaderStyle Font-Size="Smaller" Width="300px" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Left" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="Última_Gestión" HeaderText="Última Gestión"
                UniqueName="Última_Gestión" DataType="System.DateTime" DataFormatString="{0:d/M/yyyy}">
                <HeaderStyle Font-Size="Smaller" Width="100px" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
              </telerik:GridBoundColumn>
              <%-- Monto_Prometido --%>
              <telerik:GridBoundColumn DataField="Monto_Prometido" HeaderText="Monto Prometido"
                UniqueName="Monto_Prometido">
                <HeaderStyle Font-Size="Smaller" Width="100px" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Right" />
              </telerik:GridBoundColumn>
              <%-- Total_Pagado --%>
              <telerik:GridBoundColumn DataField="Total_Pagado" HeaderText="Total Pagado"
                UniqueName="Total_Pagado">
                <HeaderStyle Font-Size="Smaller" Width="100px" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Right" />
              </telerik:GridBoundColumn>
              <%-- Saldo --%>
              <telerik:GridBoundColumn DataField="Saldo" HeaderText="Saldo"
                UniqueName="Saldo">
                <HeaderStyle Font-Size="Smaller" Width="100px" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Right" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="Atraso" HeaderText="Atraso"
                UniqueName="Atraso">
                <HeaderStyle Font-Size="Smaller" Width="100px" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Right" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="Observaciones" HeaderText="Observaciones"
                UniqueName="Observaciones">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Left" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="ComerntarioDeudor" HeaderText="Comentario Deudor"
                UniqueName="ComerntarioDeudor">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Left" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="ComerntarioAnalista" HeaderText="Comerntario Analista"
                UniqueName="ComerntarioAnalista">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Left" />
              </telerik:GridBoundColumn>

            </Columns>
          </MasterTableView>
        </telerik:RadGrid>
      </div>
    </div>
    <script>
      var x, y;
      $(document).ready(function () {

        $("#idBuscar").click(function () {
          if ($("#cmbCliente").val() == "") {
            alert("Debe seleccionar cliente");
            return false;
          }
        });

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
            x = $("#fancybox-frame").contents().find("#hdd_analista").val();
            y = $("#fancybox-frame").contents().find("#hdd_codanalista").val();
          },
          'onClosed': function () {
            var text = $find("<%= rdTxtDeudor.ClientID %>");
            text.set_value(x);
            hddCodDeudor.value = y;
          }
        });
      });

      $("#rdCmbTypeQuery").change(function () {
        var text = $find("<%= rdTxtDeudor.ClientID %>");
        if (this.value == "true") {
          text.set_value("");
          hddCodDeudor.value = "";
        }
      });
      
      function onValidateAnalista() {
        var objLbl = document.getElementById('<%= lblError.ClientID %>');
        var objTxt = document.getElementById('<%= rdTxtDeudor.ClientID %>')
        var combox = document.getElementById('<%= rdCmbTypeQuery.ClientID %>');
          
          if (combox.options[combox.selectedIndex].value == 'false') {
            if (objTxt.innerText == '') {
              objLbl.innerHTML = '* Debe seleccionar al Analista.';
              return false;
            }
          } else {
            objLbl.innerHTML = '';
          }
      }
    </script>
  </form>
</body>
</html>
