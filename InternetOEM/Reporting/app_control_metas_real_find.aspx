<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="app_control_metas_real_find.aspx.cs" Inherits="ICommunity.Reporting.app_control_metas_real_find" %>

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
        <asp:Label ID="lblTitle" runat="server" CssClass="lblTitle" Text="CONTROL METAS vs REAL"></asp:Label>
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
          <div class="lblerror">
            <asp:Label ID="lblError2" runat="server" Text=""></asp:Label>
          </div>
        </div>
      </div>
      <div class="blq_search">
        <div><span>Seleccione Analista</span></div>
        <div></div>
        <telerik:RadTextBox ID="rdTxtDeudor" runat="server" CssClass="control-text-search" Enabled="false" Text=""></telerik:RadTextBox>
        <a id="btnDeudores" href="app_show_analistas.aspx" class="btnsearch"></a>
        <asp:HiddenField ID="hddCodDeudor" runat="server" />
        <div class="lblerror">
          <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
        </div>
      </div>
      <div class="blq_date">
        <div><span>Seleccione Fecha</span></div>
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
      <div class="blq_btn_search">
        <div></div>
        <div></div>
        <asp:RadioButtonList ID="rdBtnTypeQuery" runat="server" CssClass="inputcheckbox" RepeatDirection="Horizontal">
          <asp:ListItem Text="Cliente" Value="0" Selected="True"></asp:ListItem>
          <asp:ListItem Text="Analista" Value="1"></asp:ListItem>
          <asp:ListItem Text="Resumen cliente" Value="2"></asp:ListItem>
          <asp:ListItem Text="Holding" Value="3"></asp:ListItem>
        </asp:RadioButtonList>
      </div>
      <div class="blq_btn_search">
        <div>
          <asp:Button ID="idBuscar" runat="server" Text="Buscar" CssClass="btn btn-lg btn-primary btn-block" Width="100px" OnClick="idBuscar_Click" OnClientClick="return onValidateAnalista();" />
        </div>
      </div>
      <div id="idGrilla" runat="server" visible="false">
        <telerik:RadGrid ID="rdGridControlMetasvsReal" runat="server" OnNeedDataSource="rdGridControlMetasvsReal_NeedDataSource" OnItemCommand="rdGridControlMetasvsReal_ItemCommand" OnPreRender="rdGridControlMetasvsReal_PreRender"
          AllowPaging="true" AllowSorting="true" ShowStatusBar="true" PageSize="10" GridLines="None" AllowAutomaticUpdates="true" AllowAutomaticInserts="true" AllowAutomaticDeletes="true" Skin="Sitefinity">
          <ExportSettings HideStructureColumns="true"></ExportSettings>
          <PagerStyle Mode="NextPrevAndNumeric" />
          <MasterTableView AutoGenerateColumns="false" ShowHeader="true"
            TableLayout="Fixed" ShowHeadersWhenNoRecords="true" CommandItemDisplay="Top">
            <CommandItemSettings ShowExportToExcelButton="false" ShowRefreshButton="false" ShowAddNewRecordButton="false" />
            <Columns>
              <telerik:GridBoundColumn DataField="ncodigodeudor"
                UniqueName="ncodigodeudor" HeaderText="Código Deudor">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" Width="60px" />
                <ItemStyle HorizontalAlign="Center" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="NomDeudor"
                UniqueName="NomDeudor" HeaderText="Deudor">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" Width="100px" />
                <ItemStyle HorizontalAlign="Left" />
              </telerik:GridBoundColumn>

              <telerik:GridTemplateColumn UniqueName="TemplateColumn1">
                <HeaderTemplate>
                  <table border="0" cellpadding="0" cellspacing="0" align="center" width="150px">
                    <tr class="Label">
                      <td colspan="2" valign="top" align="center"><font class="tit_menu">Semana 1</font></td>
                    </tr>
                    <tr class="Label">
                      <td width="50%" valign="top" align="center"><font class="tit_menu"><asp:label ID="fecha_ini_sem1" runat="server"></asp:label></font></td>
                      <td width="50%" valign="top" align="center"><font class="tit_menu"><asp:label ID="fecha_fin_sem1" runat="server"></asp:label></font></td>
                    </tr>
                    <tr class="Label">
                      <td width="50%" valign="bottom" align="center"><font class="tit_menu">Real</font></td>
                      <td width="50%" valign="bottom" align="center"><font class="tit_menu">Estimado</font></td>
                    </tr>
                  </table>
                </HeaderTemplate>
                <ItemTemplate>
                  <table border="0" cellpadding="0" cellspacing="0" align="center" width="150px" class="tbl_group">
                    <tr class="rg_group">
                      <td width="50%" valign="top" align="right"><font class="data"><%# DataBinder.Eval(Container.DataItem, "real1", "{0:#,##}") %></font></td>
                      <td width="50%" valign="top" align="right"><font class="data"><%# DataBinder.Eval(Container.DataItem, "estimado1", "{0:#,##}") %></font></td>
                    </tr>
                  </table>
                </ItemTemplate>
              </telerik:GridTemplateColumn>

              <telerik:GridTemplateColumn UniqueName="TemplateColumn2">
                <HeaderTemplate>
                  <table border="0" cellpadding="0" cellspacing="0" align="center" width="150px">
                    <tr class="Label">
                      <td colspan="2" valign="top" align="center"><font class="tit_menu">Semana 2</font></td>
                    </tr>
                    <tr class="Label">
                      <td width="50%" valign="top" align="center"><font class="tit_menu"><asp:label ID="fecha_ini_sem2" runat="server"></asp:label></font></td>
                      <td width="50%" valign="top" align="center"><font class="tit_menu"><asp:label ID="fecha_fin_sem2" runat="server"></asp:label></font></td>
                    </tr>
                    <tr class="Label">
                      <td width="50%" valign="bottom" align="center"><font class="tit_menu">Real</font></td>
                      <td width="50%" valign="bottom" align="center"><font class="tit_menu">Estimado</font></td>
                    </tr>
                  </table>
                </HeaderTemplate>
                <ItemTemplate>
                  <table border="0" cellpadding="0" cellspacing="0" align="center" width="150px" class="tbl_group">
                    <tr class="rg_group">
                      <td width="50%" valign="top" align="right"><font class="data"><%# DataBinder.Eval(Container.DataItem, "real2", "{0:#,##}") %></font></td>
                      <td width="50%" valign="top" align="right"><font class="data"><%# DataBinder.Eval(Container.DataItem, "estimado2", "{0:#,##}") %></font></td>
                    </tr>
                  </table>
                </ItemTemplate>
              </telerik:GridTemplateColumn>

              <telerik:GridTemplateColumn UniqueName="TemplateColumn3">
                <HeaderTemplate>
                  <table border="0" cellpadding="0" cellspacing="0" align="center" width="150px">
                    <tr class="Label">
                      <td colspan="2" valign="top" align="center"><font class="tit_menu">Semana 3</font></td>
                    </tr>
                    <tr class="Label">
                      <td width="50%" valign="top" align="center"><font class="tit_menu"><asp:label ID="fecha_ini_sem3" runat="server"></asp:label></font></td>
                      <td width="50%" valign="top" align="center"><font class="tit_menu"><asp:label ID="fecha_fin_sem3" runat="server"></asp:label></font></td>
                    </tr>
                    <tr class="Label">
                      <td width="50%" valign="bottom" align="center"><font class="tit_menu">Real</font></td>
                      <td width="50%" valign="bottom" align="center"><font class="tit_menu">Estimado</font></td>
                    </tr>
                  </table>
                </HeaderTemplate>
                <ItemTemplate>
                  <table border="0" cellpadding="0" cellspacing="0" align="center" width="150px" class="tbl_group">
                    <tr class="rg_group">
                      <td width="50%" valign="top" align="right"><font class="data"><%# DataBinder.Eval(Container.DataItem, "real3", "{0:#,##}") %></font></td>
                      <td width="50%" valign="top" align="right"><font class="data"><%# DataBinder.Eval(Container.DataItem, "estimado3", "{0:#,##}") %></font></td>
                    </tr>
                  </table>
                </ItemTemplate>
              </telerik:GridTemplateColumn>

              <telerik:GridTemplateColumn UniqueName="TemplateColumn4">
                <HeaderTemplate>
                  <table border="0" cellpadding="0" cellspacing="0" align="center" width="150px">
                    <tr class="Label">
                      <td colspan="2" valign="top" align="center"><font class="tit_menu">Semana 4</font></td>
                    </tr>
                    <tr class="Label">
                      <td width="50%" valign="top" align="center"><font class="tit_menu"><asp:label ID="fecha_ini_sem4" runat="server"></asp:label></font></td>
                      <td width="50%" valign="top" align="center"><font class="tit_menu"><asp:label ID="fecha_fin_sem4" runat="server"></asp:label></font></td>
                    </tr>
                    <tr class="Label">
                      <td width="50%" valign="bottom" align="center"><font class="tit_menu">Real</font></td>
                      <td width="50%" valign="bottom" align="center"><font class="tit_menu">Estimado</font></td>
                    </tr>
                  </table>
                </HeaderTemplate>
                <ItemTemplate>
                  <table border="0" cellpadding="0" cellspacing="0" align="center" width="150px" class="tbl_group">
                    <tr class="rg_group">
                      <td width="50%" valign="top" align="right"><font class="data"><%# DataBinder.Eval(Container.DataItem, "real4", "{0:#,##}") %></font></td>
                      <td width="50%" valign="top" align="right"><font class="data"><%# DataBinder.Eval(Container.DataItem, "estimado4", "{0:#,##}") %></font></td>
                    </tr>
                  </table>
                </ItemTemplate>
              </telerik:GridTemplateColumn>

              <telerik:GridTemplateColumn UniqueName="TemplateColumn5">
                <HeaderTemplate>
                  <table border="0" cellpadding="0" cellspacing="0" align="center" width="150px">
                    <tr class="Label">
                      <td colspan="2" valign="top" align="center"><font class="tit_menu">Semana 5</font></td>
                    </tr>
                    <tr class="Label">
                      <td width="50%" valign="top" align="center"><font class="tit_menu"><asp:label ID="fecha_ini_sem5" runat="server"></asp:label></font></td>
                      <td width="50%" valign="top" align="center"><font class="tit_menu"><asp:label ID="fecha_fin_sem5" runat="server"></asp:label></font></td>
                    </tr>
                    <tr class="Label">
                      <td width="50%" valign="bottom" align="center"><font class="tit_menu">Real</font></td>
                      <td width="50%" valign="bottom" align="center"><font class="tit_menu">Estimado</font></td>
                    </tr>
                  </table>
                </HeaderTemplate>
                <ItemTemplate>
                  <table border="0" cellpadding="0" cellspacing="0" align="center" width="150px" class="tbl_group">
                    <tr class="rg_group">
                      <td width="50%" valign="top" align="right"><font class="data"><%# DataBinder.Eval(Container.DataItem, "real5", "{0:#,##}") %></font></td>
                      <td width="50%" valign="top" align="right"><font class="data"><%# DataBinder.Eval(Container.DataItem, "estimado5", "{0:#,##}") %></font></td>
                    </tr>
                  </table>
                </ItemTemplate>
              </telerik:GridTemplateColumn>

              <telerik:GridTemplateColumn UniqueName="TemplateColumn6">
                <HeaderTemplate>
                  <table border="0" cellpadding="0" cellspacing="0" align="center" width="150px">
                    <tr class="Label">
                      <td colspan="2" valign="top" align="center"><font class="tit_menu">&nbsp;</font></td>
                    </tr>
                    <tr class="Label">
                      <td width="50%" valign="top" align="center"><font class="tit_menu"><asp:label ID="fecha_ini_sem6" runat="server" Text="&nbsp;"></asp:label></font></td>
                      <td width="50%" valign="top" align="center"><font class="tit_menu"><asp:label ID="fecha_fin_sem6" runat="server" Text="&nbsp;"></asp:label></font></td>
                    </tr>
                    <tr class="Label">
                      <td width="50%" valign="bottom" align="center"><font class="tit_menu">T. Real</font></td>
                      <td width="50%" valign="bottom" align="center"><font class="tit_menu">T. Estim</font></td>
                    </tr>
                  </table>
                </HeaderTemplate>
                <ItemTemplate>
                  <table border="0" cellpadding="0" cellspacing="0" align="center" width="150px" class="tbl_group">
                    <tr class="rg_group">
                      <td width="50%" valign="top" align="right"><font class="data"><%# DataBinder.Eval(Container.DataItem, "treal", "{0:#,##}") %></font></td>
                      <td width="50%" valign="top" align="right"><font class="data"><%# DataBinder.Eval(Container.DataItem, "testimado", "{0:#,##}") %></font></td>
                    </tr>
                  </table>
                </ItemTemplate>
              </telerik:GridTemplateColumn>

              <telerik:GridBoundColumn DataField="real"
                UniqueName="Real" HeaderText="Real" DataFormatString="{0:N0}">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" Width="100px" />
                <ItemStyle HorizontalAlign="Right" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="Estimado"
                UniqueName="Estimado" HeaderText="Estimado" DataFormatString="{0:N0}">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" Width="100px" />
                <ItemStyle HorizontalAlign="Right" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="diferencia"
                UniqueName="Diferencia" HeaderText="Diferencia" DataFormatString="{0:N0}">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" Width="100px" />
                <ItemStyle HorizontalAlign="Right" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="SDeudor"
                UniqueName="SDeudor" HeaderText="Comentario Deudor">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" Width="100px" />
                <ItemStyle HorizontalAlign="Left" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="SAnalista"
                UniqueName="SAnalista" HeaderText="Comentario Analista">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" Width="100px" />
                <ItemStyle HorizontalAlign="Left" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="EtapaCob"
                UniqueName="EtapaCob" HeaderText="Etapa Cobranza">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" Width="100px" />
                <ItemStyle HorizontalAlign="Left" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="comentario"
                UniqueName="comentario" HeaderText="Comentario">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" Width="100px" />
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
    <script>
      var x, y;
      var objLbl = document.getElementById('<%= lblError.ClientID %>');
      var objLb2 = document.getElementById('<%= lblError2.ClientID %>');
      $(document).ready(function () {
        objLbl.innerHTML = "";

        $("#idBuscar").click(function () {
          if ($("#cmbCliente").val() == "") {
            alert("Debe seleccionar cliente");
            return false;
          }
        });

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
            objLbl.innerHTML = "";
          },
          'onClosed': function () {
            var text = $find("<%= rdTxtDeudor.ClientID %>");
            text.set_value(x);
            hddCodDeudor.value = y;
            objLbl.innerHTML = "";
          }
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

        if ((rdBtnValor != "0") && (rdBtnValor != "2") && (rdBtnValor != "3")) {
          if (objTxtDeudor.get_value() == "") {
            objLbl.innerHTML = "* Debe seleccionar un analista para la consulta.";
            breturn = false;
          }
        } else {
          if ((rdBtnValor == "3") && ($("#cmbHolding").val() == "")) {
            objLb2.innerHTML = "* Debe seleccionar holding para la consulta.";
            breturn = false;
          } else {
            objTxtDeudor.set_value('');
            hddCodDeudor.value = "";
          }
        }

        return breturn;
      }

    </script>

  </form>
</body>
</html>
