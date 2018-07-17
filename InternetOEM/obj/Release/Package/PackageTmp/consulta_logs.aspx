<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="consulta_logs.aspx.cs" Inherits="ICommunity.consulta_logs" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta charset="utf-8">
  <meta http-equiv="X-UA-Compatible" content="IE=edge">
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <title></title>
  <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
  <link rel="stylesheet" href="css/styleadmin.css">
  <link rel="stylesheet" type="text/css" href="css/stylesdebtcontrol.css" media="screen" />
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
</head>
<body class="bodyadm">
  <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager" runat="server">
    </asp:ScriptManager>
    <div class="container">
      <div class="row">&nbsp;</div>
      <div class="row">
        <h4>REPORTE LOG'S APLICATIVOS DEBTCONTROL</h4>
      </div>
      <div class="row">
        <br />
      </div>
      <div class="row">
        <div class="col-md-3">
          <div><span>Nombre Usuario</span></div>
          <asp:TextBox ID="txt_buscar_usuario" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
        <div class="col-md-3">
          <div><span>Fecha Inicio</span></div>
          <telerik:RadDatePicker ID="RadDatePicker1" runat="server" AutoPostBack="true"
            DateInput-EmptyMessage="" MinDate="01/01/1000" MaxDate="01/01/3000">
            <Calendar>
              <SpecialDays>
                <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
              </SpecialDays>
            </Calendar>
          </telerik:RadDatePicker>
        </div>
        <div class="col-md-3">
          <div><span>Fecha Hasta</span></div>
          <telerik:RadDatePicker ID="RadDatePicker2" runat="server" AutoPostBack="true"
            DateInput-EmptyMessage="" MinDate="01/01/1000" MaxDate="01/01/3000">
            <Calendar>
              <SpecialDays>
                <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday" />
              </SpecialDays>
            </Calendar>
          </telerik:RadDatePicker>
        </div>
        <div class="col-md-3">
          <div><span>APLICATIVO</span></div>
          <asp:DropDownList ID="rdComboAPP" CssClass="form-control" runat="server">
            <asp:ListItem Text="<< SELECCIONA APLICACIÓN >>" Value=""></asp:ListItem>
            <asp:ListItem Text="LOGIN" Value="0"></asp:ListItem>
            <asp:ListItem Text="REPORTES DEBTCONTROL" Value="1"></asp:ListItem>
            <asp:ListItem Text="ANTALIS" Value="2"></asp:ListItem>
          </asp:DropDownList>
        </div>
      </div>
      <div class="row">
        <br />
      </div>
      <div class="row">
        <div class="col-md-12 text-center">
          <asp:Button ID="btnBuscar" runat="server" CssClass="btn btn-primary" Text="BUSCAR" OnClick="btnBuscar_Click" />
        </div>
      </div>
      <div class="row">&nbsp;</div>
      <div id="Rowgrilla" runat="server" visible="false" class="row">
        <div class="col-md-12">
          <telerik:RadGrid ID="rdLogs" runat="server" ShowStatusBar="True" AutoGenerateColumns="false" OnNeedDataSource="rdLogs_NeedDataSource"
            AllowSorting="True" PageSize="15" AllowPaging="True" GridLines="None" Skin="Sitefinity">
            <MasterTableView DataKeyNames="cod_log" AutoGenerateColumns="false" ShowHeader="true"
              TableLayout="Fixed" ShowHeadersWhenNoRecords="true" CommandItemDisplay="Top">
              <CommandItemSettings ShowExportToExcelButton="false" ShowRefreshButton="false" ShowAddNewRecordButton="false" />
              <Columns>
                <telerik:GridBoundColumn UniqueName="nombreapellido" HeaderText="USUARIO" DataField="nombreapellido" AllowSorting="true">
                  <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                  <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>

                <telerik:GridBoundColumn UniqueName="fechahora" HeaderText="FECHA" DataField="fechahora" AllowSorting="true">
                  <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                  <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>

                <telerik:GridBoundColumn UniqueName="app_log" HeaderText="APLICACIÓN" DataField="app_log" AllowSorting="true">
                  <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                  <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>

                <telerik:GridBoundColumn UniqueName="obslog" HeaderText="OBSERVACIÓN" DataField="obs_log" AllowSorting="true">
                  <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                  <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>

              </Columns>
            </MasterTableView>
          </telerik:RadGrid>
        </div>

      </div>
      <div class="row">
        <br /><br />
      </div>
  </form>
</body>
</html>
