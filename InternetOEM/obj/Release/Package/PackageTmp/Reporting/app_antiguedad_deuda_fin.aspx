<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="app_antiguedad_deuda_fin.aspx.cs" Inherits="ICommunity.Reporting.app_antiguedad_deuda_fin" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta charset="utf-8">
  <meta http-equiv="X-UA-Compatible" content="IE=edge">
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <meta http-equiv="Pragma" content="no-cache" />
  <meta http-equiv="Expires" content="-1" />
  <meta http-equiv="CACHE-CONTROL" content="NO-CACHE" />
  <title></title>
  <!-- Font Awesome -->
  <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.8.2/css/all.css" />
  <link rel="stylesheet" href="../dashboard/css/bootstrap.min.css" />
  <link rel="stylesheet" type="text/css" href="../css/stylesdebtcontrol.css" media="screen" />
  <link rel="stylesheet" type="text/css" href="../dashboard/css//mdb.min.css" />
  <style>
    /*--------------------------------------------------------------------------------------*/
    /*--------------------------------------------------------------------------------------*/
    /*--------------------------------------------------------------------------------------*/
    header {
      background-image: url(../images/barrasuperior_menu.png);
      /*background-position: center;*/
      background-repeat: no-repeat;
      background-size: cover;
    }

    .debtcontrol {
      /*background: #fff !important;*/
      /*background-image: linear-gradient(135deg, #fff 70%, #3D5375);*/
    }

    .nav-background {
      background: #3D5375 !important;
    }

    .tam-logo {
      width: 80px !important;
    }
  </style>
</head>
<body>
  <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>
    <asp:HiddenField ID="hdd_arrNkeyCliente" runat="server" />
    <asp:HiddenField ID="hdd_cli_show" runat="server" />
    <header>
      <nav class="navbar navbar-expand-lg debtcontrol navbar-light">

        <!-- Navbar brand -->
        <a class="navbar-brand tam-logo text-center" href="#">
          <img src="../images/logodebtcontrol.png" border="0" width="90%" /></a>

        <!-- Collapse button -->
        <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent-6"
          aria-controls="navbarSupportedContent-6" aria-expanded="false" aria-label="Toggle navigation">
          <span class="navbar-toggler-icon"></span>
        </button>

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

    <div class="container-fluid">
      <div class="blq_tile">
        <asp:Label ID="lblTitle" runat="server" CssClass="lblTitle" Text="ANTIGUEDAD DE DEUDA"></asp:Label>
      </div>
      <div class="row">
        <div id="colClientes" class="col-md-3" runat="server" visible="false">
          <div><span>Clientes</span></div>
          <div></div>
          <asp:DropDownList ID="cmbCliente" CssClass="inputCmbBox" runat="server">
          </asp:DropDownList>
        </div>
        <div id="colHolding" class="col-md-3" runat="server" visible="false">
          <div><span>Holding</span></div>
          <div></div>
          <asp:DropDownList ID="cmbHolding" CssClass="inputCmbBox" runat="server">
          </asp:DropDownList>
          <div class="lblerror">
            <asp:Label ID="lblError2" runat="server" Text=""></asp:Label>
          </div>
        </div>
      </div>
      <div class="row">
        <div class="col-3 blq_search">
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
        <div class="col-3 blq_btn_search">
          <div></div>
          <div>
            <asp:RadioButtonList ID="RdBtnTypeQuery" CssClass="inputcheckbox" runat="server">
              <asp:ListItem Text="Deudor" Selected="True" Value="0"></asp:ListItem>
              <asp:ListItem Text="Cliente" Value="1"></asp:ListItem>
              <asp:ListItem Text="Analista" Value="2"></asp:ListItem>
              <asp:ListItem Text="Vendedor" Value="3"></asp:ListItem>
              <asp:ListItem Text="Holding" Value="4"></asp:ListItem>
            </asp:RadioButtonList>
          </div>
        </div>
      </div>
      <div class="row">
        <div class="col-3 text-left">
          <asp:Button ID="idBuscar" runat="server" Text="Buscar" OnClick="idBuscar_Click" CssClass="btn btn-primary btn-sm" OnClientClick="return onValidateAnalista();" />
        </div>
      </div>
      <div id="idGrilla" runat="server" visible="false" class="row">
        <div class="col-12">
          <asp:Label ID="lblmoneda" runat="server" CssClass="lblmoneda"></asp:Label>
          <telerik:RadGrid ID="rdGridAntiguedadDeuda" runat="server" OnNeedDataSource="rdGridAntiguedadDeuda_NeedDataSource" OnItemCommand="rdGridAntiguedadDeuda_ItemCommand" OnPreRender="rdGridAntiguedadDeuda_PreRender" OnItemDataBound="rdGridAntiguedadDeuda_ItemDataBound"
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
      </div>
      <div style="height: 30px;">
        <br />
        <br />
      </div>
    </div>
    <script type="text/javascript" src="../dashboard/js/jquery-3.4.1.min.js"></script>
    <!-- Bootstrap tooltips -->
    <script type="text/javascript" src="../dashboard/js/popper.min.js"></script>
    <!-- Bootstrap core JavaScript -->
    <script type="text/javascript" src="../dashboard/js/bootstrap.min.js"></script>

    <script type="text/javascript" src="../Resources/fancybox/jquery.min.js"></script>
    <script type="text/javascript" src="../Resources/fancybox/jquery.mousewheel-3.0.4.pack.js"></script>
    <script type="text/javascript" src="../Resources/fancybox/jquery.fancybox-1.3.4.js"></script>
    <link rel="stylesheet" type="text/css" href="../Resources/fancybox/jquery.fancybox-1.3.4.css" media="screen" />
    <script>
      var x, y;
      var lblError = document.getElementById('<%= lblError.ClientID %>');
      $(document).ready(function () {

        $("#idBuscar").click(function () {
          if ($("#cmbCliente").val() == "") {
            alert("Debe seleccionar cliente");
            return false;
          }
        });

        $("#btnSearch").click(function () {
          lblError.innerHTML = "";
          var rdBtnValor = $('#<%=RdBtnTypeQuery.ClientID %> input[type=radio]:checked').val();
          var objHref = document.getElementById("objShowSearch");

          if (rdBtnValor == "0") {
            CodCliente = "";
            if ($("#hdd_cli_show").val() == "V") {
              if ($("#<%= cmbCliente.ClientID %>").val() != "")
                CodCliente = $("#<%= cmbCliente.ClientID %>").val();
              else {
                alert("Debe seleccionar cliente");
                return false;
              }

            }

            objHref.href = "app_show_deudores.aspx?ArrCodCliente=" + CodCliente;
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

        if ((rbvalue != "1") && (rbvalue != "4") && (objTxtDeudor.get_value() == "")) {
          lblError.innerHTML = "* Debe seleccionar el dato para la consulta.";
          breturn = false;
        }

        if ((rbvalue == "4") && ($("#cmbHolding").val() == "")) {
          lblError2.innerHTML = "* Debe seleccionar un holding para la consulta.";
          breturn = false;
        }

        return breturn;
      }
    </script>
  </form>
</body>
</html>
