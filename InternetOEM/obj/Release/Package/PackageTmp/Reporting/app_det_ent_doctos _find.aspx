<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="app_det_ent_doctos _find.aspx.cs" Inherits="ICommunity.Reporting.app_det_ent_doctos__find" %>

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
        <asp:Label ID="lblTitle" runat="server" CssClass="lblTitle" Text="DETALLE ENTREGA DOCUMENTOS"></asp:Label>
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
          <asp:Button ID="idBuscar" runat="server" Text="Buscar" CssClass="btn btn-lg btn-primary btn-block" Width="100px" OnClick="idBuscar_Click" />
        </div>
      </div>
      <div id="idGrilla" runat="server" visible="false">
        <telerik:RadGrid ID="rdGridDetEntreDoc" runat="server" OnNeedDataSource="rdGridDetEntreDoc_NeedDataSource" OnItemDataBound="rdGridDetEntreDoc_ItemDataBound" OnItemCommand="rdGridDetEntreDoc_ItemCommand"
          AllowPaging="true" AllowSorting="true" ShowStatusBar="true" PageSize="10" GridLines="None" AllowAutomaticUpdates="true" AllowAutomaticInserts="true" AllowAutomaticDeletes="true" Skin="Sitefinity">
          <ExportSettings HideStructureColumns="true"></ExportSettings>
          <PagerStyle Mode="NextPrevAndNumeric" />
          <MasterTableView AutoGenerateColumns="false" ShowHeader="true"
            TableLayout="Fixed" ShowHeadersWhenNoRecords="true" CommandItemDisplay="Top">
            <CommandItemSettings ShowExportToExcelButton="true" ShowRefreshButton="false" ShowAddNewRecordButton="false" />
            <Columns>
              <telerik:GridBoundColumn DataField="RUT" HeaderText="RUT"
                UniqueName="RUT">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="Deudor" HeaderText="Deudor"
                UniqueName="NomDeu">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Left" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="tipo_docto" HeaderText="Documento"
                UniqueName="Documento">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="nnumerofactura" HeaderText="N° Factura"
                UniqueName="nnumerofactura">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="Monto" HeaderText="Monto"
                UniqueName="Monto">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="femision" HeaderText="Emisión" DataType="System.DateTime" DataFormatString="{0:d/M/yyyy}"
                UniqueName="femision">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="frecepcion" HeaderText="Recepción" DataType="System.DateTime" DataFormatString="{0:d/M/yyyy}"
                UniqueName="frecepcion">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="fentrega" HeaderText="Entrega" DataType="System.DateTime" DataFormatString="{0:d/M/yyyy}"
                UniqueName="fentrega">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="diasrecep" HeaderText="Días Rec." DataType="System.DateTime" DataFormatString="{0:d/M/yyyy}"
                UniqueName="diasrecep">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="feriadosinicial" HeaderText="Fer. En"
                UniqueName="feriadosinicial">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="diasdespacho" HeaderText="Días Des."
                UniqueName="diasdespacho">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="feriados" HeaderText="Fer."
                UniqueName="feriados">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn DataField="status" HeaderText="Status"
                UniqueName="status">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
              </telerik:GridBoundColumn>
              
            </Columns>
          </MasterTableView>
        </telerik:RadGrid>

      </div>
      <div style="height:30px;">
        <br /><br />
      </div>
    </div>
  </form>
</body>
</html>
