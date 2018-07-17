<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="app_cartola_find.aspx.cs" Inherits="ICommunity.Reporting.app_cartola_find" %>

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
        <asp:Label ID="lblTitle" runat="server" CssClass="lblTitle" Text="CARTOLA DE MOVIMIENTOS Y SALDOS"></asp:Label>
      </div>
      <div class="blq_search">
        <div><span>Deudor</span></div>
        <div></div>
        <telerik:RadTextBox ID="rdTxtDeudor" runat="server" CssClass="control-text-search" Enabled="false" Text=""></telerik:RadTextBox>
        <a id="btnDeudores" href="app_show_deudores.aspx" class="btnsearch"></a>
        <asp:HiddenField ID="hddCodDeudor" runat="server" />
      </div>
      <div class="blq_mesano">
        <div><span>Ingrese fecha</span></div>
        <div></div>
        <div>
          <asp:DropDownList ID="rdCmbBoxMeses" CssClass="inputCmbBoxMes" runat="server">
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
          <telerik:RadTextBox ID="tdTxtAno" runat="server" CssClass="control-text-ano" Text=""></telerik:RadTextBox>
        </div>


      </div>
      <div class="blq_btn_search">
        <div>
          <asp:Button ID="idBuscar" runat="server" Text="Buscar" CssClass="btn btn-lg btn-primary btn-block" Width="100px" OnClick="idBuscar_Click" />
        </div>
      </div>
      <div id="idGrilla" runat="server" visible="false">
        <div style="height: 20px; border-top: 1px solid #ccc"></div>
        <div>
          <table border="0" cellpadding="0" cellspacing="0" width="100%" align="center">
            <tr>
              <td align="center" width="100%">
                <table border="0" cellpadding="0" cellspacing="0" width="98%" align="center">
                  <tr>
                    <td width="50%" align="left">
                      <table border="0" cellpadding="0" cellspacing="0" width="98%" align="center">
                        <tr>
                          <td width="15%" align="left"><span class="data">Cliente</span></td>
                          <td width="5%" align="center"><span class="data">:</span></td>
                          <td align="left">
                            <asp:Label ID="sNombre" CssClass="data" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                          <td width="15%" align="left"><span class="data">Dirección</span></td>
                          <td width="5%" align="center"><span class="data">:</span></td>
                          <td align="left">
                            <asp:Label ID="sDirec" CssClass="data" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                          <td width="15%" align="left"><span class="data">Comuna</span></td>
                          <td width="5%" align="center"><span class="data">:</span></td>
                          <td align="left">
                            <asp:Label ID="sComuna" CssClass="data" runat="server"></asp:Label></td>
                        </tr>
                      </table>
                    </td>
                    <td width="50%" align="left">
                      <table border="0" cellpadding="0" cellspacing="0" width="98%" align="center">
                        <tr>
                          <td width="15%" align="left"><span class="data">Rut</span></td>
                          <td width="5%" align="center"><span class="data">:</span></td>
                          <td align="left">
                            <asp:Label ID="sRut" CssClass="data" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                          <td width="15%" align="left"><span class="data">Cartola</span></td>
                          <td width="5%" align="center"><span class="data">:</span></td>
                          <td align="left">
                            <asp:Label ID="lblFechCartola" CssClass="data" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                          <td width="15%" align="left"><span class="data">Desde</span></td>
                          <td width="5%" align="center"><span class="data">:</span></td>
                          <td align="left">
                            <asp:Label ID="lblFechIni" CssClass="data" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                          <td width="15%" align="left"><span class="data">Hasta</span></td>
                          <td width="5%" align="center"><span class="data">:</span></td>
                          <td align="left">
                            <asp:Label ID="lblFechFnl" CssClass="data" runat="server"></asp:Label></td>
                        </tr>
                      </table>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td height="5px"></td>
            </tr>
            <tr>
              <td align="center" width="100%">
                <table border="0" cellpadding="0" cellspacing="0" width="98%" align="center">
                  <tr>
                    <td colspan="3">
                      <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                          <td width="110px"><span class="data">Ejecutivo de cuenta</span></td>
                          <td width="10px" align="center"><span class="data">:</span></td>
                          <td>
                            <asp:Label ID="sEjeNombre" CssClass="data" runat="server"></asp:Label></td>
                        </tr>
                      </table>
                    </td>
                  </tr>
                  <tr>
                    <td width="33%">
                      <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                          <td width="15%"><span class="data">TELEFONO</span></td>
                          <td width="5%" align="center"><span class="data">:</span></td>
                          <td>
                            <asp:Label ID="sTele" CssClass="data" runat="server"></asp:Label></td>
                        </tr>
                      </table>
                    </td>
                    <td width="33%">
                      <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                          <td width="15%"><span class="data">FAX</span></td>
                          <td width="5%" align="center"><span class="data">:</span></td>
                          <td>
                            <asp:Label ID="sEjeFax" CssClass="data" runat="server"></asp:Label></td>
                        </tr>
                      </table>
                    </td>
                    <td width="33%">
                      <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                          <td width="15%"><span class="data">EMAIL</span></td>
                          <td width="5%" align="center"><span class="data">:</span></td>
                          <td>
                            <asp:Label ID="sEMail" CssClass="data" runat="server"></asp:Label></td>
                        </tr>
                      </table>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
        </div>
        <div style="height: 10px; border-bottom: 1px solid #ccc"></div>
        <div>
          <table border="0" cellpadding="0" cellspacing="0" width="100%" align="center">
            <tr>
              <td width="100%" align="center">
                <table border="0" cellpadding="1" cellspacing="0" width="98%" align="center">
                  <tr>
                    <td valign="top" align="center"><span class="data">Saldo Inicial Auxiliar</td>
                    <td valign="top" align="center"><span class="data">&nbsp;</span></td>
                    <td valign="top" align="center"><span class="data">Documentos Recibidos</span></td>
                    <td valign="top" align="center"><span class="data">&nbsp;</span></td>
                    <td valign="top" align="center"><span class="data">&nbsp;</span></td>
                    <td valign="top" align="center"><span class="data">Aplicación Transmitidos</span></td>
                    <td valign="top" align="center"><span class="data">&nbsp;</span></td>
                    <td valign="top" align="center"><span class="data">&nbsp;</span></td>
                    <td valign="top" align="center"><span class="data">Saldo Final Auxiliar</span></td>
                  </tr>
                  <tr>
                    <td valign="top" align="center">
                      <asp:Label ID="lblMntTtlSaldo" CssClass="data" runat="server"></asp:Label></td>
                    <td valign="top" align="center"><span class="data">&nbsp;</span></td>
                    <td valign="top" align="center">
                      <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                          <td align="center"><span class="data">NºDoc</span></td>
                          <td align="center"><span class="data">Monto</span></td>
                        </tr>
                        <tr>
                          <td align="center">
                            <asp:Label ID="lblCantDoc" CssClass="data" runat="server"></asp:Label></td>
                          <td align="center">
                            <asp:Label ID="lblMntTtDoc" CssClass="data" runat="server"></asp:Label></td>
                        </tr>
                      </table>
                    </td>
                    <td valign="top" align="center"><span class="data">&nbsp;</span></td>
                    <td valign="top" align="center"><span class="data">&nbsp;</span></td>
                    <td valign="top" align="center">
                      <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                          <td align="center"><span class="data">NºDoc</span></td>
                          <td align="center"><span class="data">Monto</span></td>
                        </tr>
                        <tr>
                          <td align="center">
                            <asp:Label ID="lblCantPagos" CssClass="data" runat="server"></asp:Label></td>
                          <td align="center">
                            <asp:Label ID="lblMntTtFct" CssClass="data" runat="server"></asp:Label></td>
                        </tr>
                      </table>
                    </td>
                    <td valign="top" align="center"><span class="data">&nbsp;</span></td>
                    <td valign="top" align="center"><span class="data">&nbsp;</span></td>
                    <td valign="top" align="center">
                      <asp:Label ID="lblSaldoFinalAuxiliar" CssClass="data" runat="server"></asp:Label></td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
        </div>
        <div>
          <telerik:RadGrid ID="rdGridCartola" runat="server" OnNeedDataSource="rdGridCartola_NeedDataSource"
            AllowPaging="true" AllowSorting="true" ShowStatusBar="true" PageSize="10" GridLines="None" AllowAutomaticUpdates="true" AllowAutomaticInserts="true" AllowAutomaticDeletes="true" Skin="Sitefinity">
            <ExportSettings HideStructureColumns="true"></ExportSettings>
            <PagerStyle Mode="NextPrevAndNumeric" />
            <MasterTableView AutoGenerateColumns="false" ShowHeader="true"
              TableLayout="Fixed" ShowHeadersWhenNoRecords="true" CommandItemDisplay="Top">
              <CommandItemSettings ShowExportToExcelButton="true" ShowRefreshButton="false" ShowAddNewRecordButton="false" />
              <Columns>
                <telerik:GridBoundColumn DataField="Fecha_Transacción" HeaderText="Fecha Transacción"
                  UniqueName="Fecha_Transacción" DataType="System.DateTime" DataFormatString="{0:d/M/yyyy}">
                  <HeaderStyle Font-Size="Smaller" Width="50px" HorizontalAlign="Center" />
                  <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>

                <telerik:GridBoundColumn DataField="Cantidad_Documentos" HeaderText="Numero Documentos"
                  UniqueName="Cantidad_Documentos">
                  <HeaderStyle Font-Size="Smaller" Width="50px" HorizontalAlign="Center" />
                  <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>

                <telerik:GridBoundColumn DataField="Monto_Documento" HeaderText="Monto"
                  UniqueName="Monto_Documento" Aggregate="Sum" DataFormatString="{0:N0}" FooterAggregateFormatString="{0:N0}">
                  <HeaderStyle Font-Size="Smaller" Width="50px" HorizontalAlign="Center" />
                  <ItemStyle HorizontalAlign="Right" />
                </telerik:GridBoundColumn>

                <telerik:GridBoundColumn DataField="Tipo_Documento" HeaderText="Descripción Documento"
                  UniqueName="Tipo_Documento">
                  <HeaderStyle Font-Size="Smaller" Width="50px" HorizontalAlign="Center" />
                  <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>

                <telerik:GridBoundColumn DataField="Cantidad_Pagos" HeaderText="Numero Aplicaciones"
                  UniqueName="Cantidad_Pagos">
                  <HeaderStyle Font-Size="Smaller" Width="50px" HorizontalAlign="Center" />
                  <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>

                <telerik:GridBoundColumn DataField="Monto_Factura" HeaderText="Monto"
                  UniqueName="Monto_Factura" Aggregate="Sum" DataFormatString="{0:N0}" FooterAggregateFormatString="{0:N0}">
                  <HeaderStyle Font-Size="Smaller" Width="50px" HorizontalAlign="Center" />
                  <ItemStyle HorizontalAlign="Right" />
                </telerik:GridBoundColumn>

                <telerik:GridBoundColumn DataField="Tipo_Pago" HeaderText="Descripción Documento"
                  UniqueName="Tipo_Pago">
                  <HeaderStyle Font-Size="Smaller" Width="200px" HorizontalAlign="Center" />
                  <ItemStyle HorizontalAlign="Left" />
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
    </div>
    <script>
      var x, y;
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

      });
    </script>
  </form>
</body>
</html>
