﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="app_indicadores_efectividad_find.aspx.cs" Inherits="ICommunity.Reporting.app_indicadores_efectividad_find" %>

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
    <asp:HiddenField ID="hddCodRubro" runat="server" />
    <asp:HiddenField ID="hddCodDeudor" runat="server" />
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
        <asp:Label ID="lblTitle" runat="server" CssClass="lblTitle" Text="INDICADORES DE EFECTIVIDAD"></asp:Label>
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
      <div id="blqDeudor" runat="server" class="row">
        <div class="col-3">
          <div><span>Deudor</span></div>
          <div></div>
          <telerik:RadTextBox ID="rdTxtDeudor" runat="server" CssClass="control-text-search" Enabled="false" Text=""></telerik:RadTextBox>
          <a id="btnSearch" href="#" class="btnsearch"></a>
          <a id="objShowSearch" href="#"></a>
          <div class="lblerror">
            <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
          </div>
        </div>
      </div>
      <div id="blqOpcReporte1" runat="server" class="row">
        <div class="col-3">
          <div><span>Opciones del Reporte</span></div>
          <div></div>
          <asp:RadioButtonList ID="rdBtnTypeQuery" CssClass="inputcheckbox" RepeatDirection="Horizontal" runat="server">
            <asp:ListItem Text="Cliente" Value="0" Selected="True"></asp:ListItem>
            <asp:ListItem Text="Vendedor" Value="1"></asp:ListItem>
            <asp:ListItem Text="Analista" Value="2"></asp:ListItem>
            <asp:ListItem Text="Deudor" Value="3"></asp:ListItem>
            <asp:ListItem Text="Rubro" Value="4"></asp:ListItem>
            <asp:ListItem Text="Canal" Value="5"></asp:ListItem>
            <asp:ListItem Text="Holding" Value="11"></asp:ListItem>
          </asp:RadioButtonList>
        </div>
      </div>
      <div class="row">
        <div class="col-3">
          <asp:CheckBox ID="chkDetalleMes" runat="server" CssClass="inputcheckbox" Text="Detalle Mes" />
        </div>
      </div>
      <div class="row" id="blqFecha" runat="server">
        <div class="col-3">
          <div><span>Ingrese fecha</span></div>
          <div></div>
          <asp:DropDownList ID="rdCmbMes" CssClass="inputCmbBox" runat="server">
            <asp:ListItem Text="Enero" Value="1"></asp:ListItem>
            <asp:ListItem Text="Febrero" Value="2"></asp:ListItem>
            <asp:ListItem Text="Marzo" Value="3"></asp:ListItem>
            <asp:ListItem Text="Abril" Value="4"></asp:ListItem>
            <asp:ListItem Text="Mayo" Value="5"></asp:ListItem>
            <asp:ListItem Text="Junio" Value="6"></asp:ListItem>
            <asp:ListItem Text="Julio" Value="7"></asp:ListItem>
            <asp:ListItem Text="Agosto" Value="8"></asp:ListItem>
            <asp:ListItem Text="Septiembre" Value="9"></asp:ListItem>
            <asp:ListItem Text="Octubre" Value="10"></asp:ListItem>
            <asp:ListItem Text="Noviembre" Value="11"></asp:ListItem>
            <asp:ListItem Text="Diciembre" Value="12"></asp:ListItem>
          </asp:DropDownList>
          <telerik:RadTextBox ID="txt_ano" CssClass="control-text-ano" runat="server"></telerik:RadTextBox>
        </div>
      </div>
      <div class="row" id="blqOpcReporte2" runat="server">
        <div class="col-3">
          <div><span>Opciones del Reporte</span></div>
          <div></div>
          <asp:RadioButtonList ID="rdBtnTypeQuery2" CssClass="inputcheckbox" RepeatDirection="Horizontal" runat="server">
            <asp:ListItem Text="Vendedor" Value="8" Selected="True"></asp:ListItem>
            <asp:ListItem Text="Analista" Value="9"></asp:ListItem>
            <asp:ListItem Text="Deudor" Value="6"></asp:ListItem>
            <asp:ListItem Text="Rubro" Value="7"></asp:ListItem>
            <asp:ListItem Text="Canal" Value="10"></asp:ListItem>
          </asp:RadioButtonList>
        </div>
      </div>
      <div class="row" id="blqOpcIndTop" runat="server">
        <div class="col-3">
          <div></div>
          <div></div>
          <div>
            <asp:CheckBox ID="chkIndicadoresTop" CssClass="inputcheckbox" runat="server" Text="Indicadores Top" />
          </div>
        </div>
      </div>
      <div class="row" id="blqCriterio" runat="server">
        <div class="col-3">
          <div><span>Criterio</span></div>
          <div></div>
          <asp:RadioButtonList ID="rdBtnCriterio" CssClass="inputcheckbox" RepeatDirection="Horizontal" runat="server">
            <asp:ListItem Text="DSO" Value="1" Selected="True"></asp:ListItem>
            <asp:ListItem Text="DBT" Value="2"></asp:ListItem>
            <asp:ListItem Text="Overdue" Value="3"></asp:ListItem>
          </asp:RadioButtonList>
        </div>
      </div>
      <div class="row">
        <div class="col-3 text-left">
          <asp:Button ID="idBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary btn-sm" OnClick="idBuscar_Click" OnClientClick="return onValidateAnalista();" />
        </div>
      </div>
      <div class="row">
        <div class="col-12">
          <div id="idGrilla" runat="server" visible="false">
            <asp:Label ID="lblmoneda" runat="server" CssClass="lblmoneda"></asp:Label>
            <telerik:RadGrid ID="rdGridIndicadoresEfectividad" runat="server" OnNeedDataSource="rdGridIndicadoresEfectividad_NeedDataSource" OnItemCommand="rdGridIndicadoresEfectividad_ItemCommand" OnPreRender="rdGridIndicadoresEfectividad_PreRender" OnItemDataBound="rdGridIndicadoresEfectividad_ItemDataBound"
              AllowPaging="true" AllowSorting="true" ShowStatusBar="true" PageSize="10" GridLines="None" AllowAutomaticUpdates="true" AllowAutomaticInserts="true" AllowAutomaticDeletes="true" Skin="Sitefinity">
              <ExportSettings HideStructureColumns="true"></ExportSettings>
              <PagerStyle Mode="NextPrevAndNumeric" />
              <MasterTableView AutoGenerateColumns="false" ShowHeader="true"
                TableLayout="Fixed" ShowHeadersWhenNoRecords="true" CommandItemDisplay="Top">
                <CommandItemSettings ShowExportToExcelButton="true" ShowRefreshButton="false" ShowAddNewRecordButton="false" />
                <Columns>
                  <telerik:GridBoundColumn DataField="periodo" HeaderText="Periodo"
                    UniqueName="periodo">
                    <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                  </telerik:GridBoundColumn>

                  <telerik:GridBoundColumn DataField="NomDeudor" HeaderText="Deudor"
                    UniqueName="NomDeudor">
                    <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" />
                  </telerik:GridBoundColumn>

                  <telerik:GridBoundColumn DataField="Rubro" HeaderText="Rubro"
                    UniqueName="Rubro">
                    <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" />
                  </telerik:GridBoundColumn>

                  <telerik:GridBoundColumn DataField="NomVendedor" HeaderText="Vendedor"
                    UniqueName="NomVendedor">
                    <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" />
                  </telerik:GridBoundColumn>

                  <telerik:GridBoundColumn DataField="NomAnalista" HeaderText="Analista"
                    UniqueName="NomAnalista">
                    <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" />
                  </telerik:GridBoundColumn>

                  <telerik:GridBoundColumn DataField="Canal" HeaderText="Canal"
                    UniqueName="Canal">
                    <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" />
                  </telerik:GridBoundColumn>

                  <telerik:GridBoundColumn DataField="DBTDias" HeaderText="DBT días"
                    UniqueName="DBTDias">
                    <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                  </telerik:GridBoundColumn>
                  <%-- DataFormatString="{0:N0}" --%>
                  <telerik:GridBoundColumn DataField="DSO" HeaderText="DSO días"
                    UniqueName="DSO">
                    <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" />
                  </telerik:GridBoundColumn>
                  <%--  --%>
                  <telerik:GridBoundColumn DataField="Overdue" HeaderText="Overdue %" DataFormatString="{0:N0}"
                    UniqueName="Overdue">
                    <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" />
                  </telerik:GridBoundColumn>
                  <%--  --%>
                  <telerik:GridBoundColumn DataField="OverdueCritico" HeaderText="Overdue Critico %" DataFormatString="{0:N0}"
                    UniqueName="OverdueCritico">
                    <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" />
                  </telerik:GridBoundColumn>
                  <%--  --%>
                  <telerik:GridBoundColumn DataField="VencidoFacturado" HeaderText="Vencido / Facturado %" DataFormatString="{0:N0}"
                    UniqueName="VencidoFacturado">
                    <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" />
                  </telerik:GridBoundColumn>
                  <%-- DataFormatString="{0:N0}" --%>
                  <telerik:GridBoundColumn DataField="Cobrado" HeaderText="Recaudado $"
                    UniqueName="Cobrado">
                    <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" />
                  </telerik:GridBoundColumn>
                  <%-- DataFormatString="{0:N0}" --%>
                  <telerik:GridBoundColumn DataField="Deuda" HeaderText="Cuentas por Cobrar $"
                    UniqueName="Deuda">
                    <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" />
                  </telerik:GridBoundColumn>
                  <%-- DataFormatString="{0:N0}" --%>
                  <telerik:GridBoundColumn DataField="Vencido" HeaderText="Vencido $"
                    UniqueName="Vencido">
                    <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" />
                  </telerik:GridBoundColumn>
                  <%-- DataFormatString="{0:N0}" --%>
                  <telerik:GridBoundColumn DataField="Facturado" HeaderText="Facturado $"
                    UniqueName="Facturado">
                    <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" />
                  </telerik:GridBoundColumn>
                  <%-- DataFormatString="{0:N0}" --%>
                  <telerik:GridBoundColumn DataField="NC" HeaderText="NC $"
                    UniqueName="NC">
                    <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" />
                  </telerik:GridBoundColumn>
                  <%-- DataFormatString="{0:N0}" --%>
                  <telerik:GridBoundColumn DataField="FP" HeaderText="Factura Publicidad $"
                    UniqueName="FP">
                    <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" />
                  </telerik:GridBoundColumn>
                  <%-- DataFormatString="{0:N0}" --%>
                  <telerik:GridBoundColumn DataField="Ficticio" HeaderText="Ajustes $"
                    UniqueName="Ficticio">
                    <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" />
                  </telerik:GridBoundColumn>
                </Columns>
              </MasterTableView>
            </telerik:RadGrid>
          </div>
        </div>
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
    <link rel="stylesheet" type="text/css" href="/css/stylesdebtcontrol.css" media="screen" />
    <script>
      var x, y;
      var objLbl = document.getElementById('<%= lblError.ClientID %>');

      $(document).ready(function () {

        $("#idBuscar").click(function () {
          if ($("#cmbCliente").val() == "") {
            alert("Debe seleccionar cliente");
            return false;
          }
        });

        /* Apply fancybox to multiple items */
        $("#btnSearch").click(function () {
          var rdBtnValor = $('#<%=rdBtnTypeQuery.ClientID %> input[type=radio]:checked').val();
          var objHref = document.getElementById("objShowSearch");

          if (rdBtnValor == "0") {
            alert("Seleccione otra opción");

          } else if (rdBtnValor == "1") {
            objHref.href = "app_show_vendedores.aspx";
            $("#objShowSearch").trigger("click");

          } else if (rdBtnValor == "2") {
            objHref.href = "app_show_analistas.aspx";
            $("#objShowSearch").trigger("click");

          } else if (rdBtnValor == "3") {
            CodCliente = "";
            if ($("#hdd_cli_show").val() == "V") {
              if ($("#<%= cmbCliente.ClientID %>").val() != "")
                CodCliente = $("#<%= cmbCliente.ClientID %>").val();
              else {
                alert("Debe seleccionar cliente");
                return false;
              }
            }

            objHref.href = "app_show_rubrosintermobi.aspx?ArrCodCliente=" + CodCliente;
            $("#objShowSearch").trigger("click");

          } else if (rdBtnValor == "4") {
            objHref.href = "app_show_rubros.aspx?hdd_canal=R";
            $("#objShowSearch").trigger("click");

          } else if (rdBtnValor == "5") {
            objHref.href = "app_show_rubros.aspx?hdd_canal=C";
            $("#objShowSearch").trigger("click");
          }
        });

        /* Apply fancybox to multiple items */
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

        $("#chkDetalleMes").click(function () {
          loadDetalleMes();
        });

        $("#chkIndicadoresTop").click(function () {
          loadIndicadores();
        });

        $('#<%=rdBtnTypeQuery.ClientID %>').change(function () {
          var objTxtDeudor = $find("<%= rdTxtDeudor.ClientID %>");

          objLbl.innerHTML = "";
          objTxtDeudor.set_value('');

        });

      });

      function onValidateAnalista() {
        breturn = true;
        var rdBtnValor = $('#<%=rdBtnTypeQuery.ClientID %> input[type=radio]:checked').val();
        var objTxtDeudor = $find("<%= rdTxtDeudor.ClientID %>");

        if ((rdBtnValor != "0") && (rdBtnValor != "6") && (rdBtnValor != "7") && (rdBtnValor != "8") && (rdBtnValor != "9") && (rdBtnValor != "10") && (rdBtnValor != "11")) {
          if (objTxtDeudor.get_value() == "") {
            objLbl.innerHTML = "* Debe seleccionar datos para la consulta.";
            breturn = false;
          }
        } else {

          if ((rdBtnValor == "11") && ($("#cmbHolding").val() == "")) {
            var objLb2 = document.getElementById('<%= lblError2.ClientID %>');
              objLb2.innerHTML = "* Debe seleccionar holding para la consulta.";
              breturn = false;
            }

            objTxtDeudor.set_value('');
            hddCodDeudor.value = "";

          }

          return breturn;
        }

        function loadDetalleMes() {
          if ($("#chkDetalleMes").is(':checked')) {
            $("#blqDeudor").css("display", "none");
            $("#blqOpcReporte1").css("display", "none");

            $("#blqFecha").css("display", "block");
            $("#blqOpcReporte2").css("display", "block");
            $("#blqOpcIndTop").css("display", "block");
          } else {
            $("#blqFecha").css("display", "none");
            $("#blqOpcReporte2").css("display", "none");
            $("#blqOpcIndTop").css("display", "none");
            $("#blqCriterio").css("display", "none");

            $("#blqDeudor").css("display", "block");
            $("#blqOpcReporte1").css("display", "block");
          }
        }

        function loadIndicadores() {
          if ($("#chkIndicadoresTop").is(':checked')) {
            $("#blqCriterio").css("display", "block");
          } else {
            $("#blqCriterio").css("display", "none");
          }
        }
    </script>
  </form>
</body>
</html>
