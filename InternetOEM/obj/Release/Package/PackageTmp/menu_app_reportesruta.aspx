<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="menu_app_reportesruta.aspx.cs" Inherits="ICommunity.menu_app_reportesruta" %>

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
        <h4>REPORTE RUTAS MOTORISTAS</h4>
      </div>
      <div class="row">
        <div class="input-group">
          <asp:TextBox ID="txt_buscar" class="form-control" placeholder="Buscar" runat="server"></asp:TextBox>
          <div class="input-group-btn">
            <asp:Button ID="btnBuscar" runat="server" CssClass="btn btn-primary" OnClick="btnBuscar_Click" />
          </div>
        </div>
      </div>
      <div class="row">&nbsp;</div>
      <div class="row">
        <telerik:RadGrid ID="rdUsuarios" runat="server" ShowStatusBar="True" AutoGenerateColumns="false"
          AllowSorting="True" PageSize="15" AllowPaging="True" GridLines="None" OnNeedDataSource="rdUsuarios_NeedDataSource"
          OnItemCommand="rdUsuarios_ItemCommand" OnItemDataBound="rdUsuarios_ItemDataBound" Skin="Sitefinity">
          <MasterTableView DataKeyNames="cod_user" AutoGenerateColumns="false" ShowHeader="true"
            TableLayout="Fixed" ShowHeadersWhenNoRecords="true" CommandItemDisplay="Top">
            <CommandItemSettings ShowExportToExcelButton="false" ShowRefreshButton="false" ShowAddNewRecordButton="false" />
            <Columns>
              <telerik:GridButtonColumn ButtonType="PushButton" UniqueName="Editar" CommandName="cmdEdit">
                <HeaderStyle Font-Size="Smaller" Width="50px" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
              </telerik:GridButtonColumn>

              <telerik:GridBoundColumn UniqueName="NomUsuario" DataField="nom_user" AllowSorting="true">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Left" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn UniqueName="ApeUsuario" DataField="ape_user" AllowSorting="true">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Left" />
              </telerik:GridBoundColumn>

            </Columns>
          </MasterTableView>
        </telerik:RadGrid>
      </div>
    </div>
  </form>
</body>
</html>
