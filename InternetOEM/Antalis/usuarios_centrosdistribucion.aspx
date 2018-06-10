<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="usuarios_centrosdistribucion.aspx.cs" Inherits="ICommunity.Antalis.usuarios_centrosdistribucion" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta charset="utf-8">
  <meta http-equiv="X-UA-Compatible" content="IE=edge">
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <title></title>
  <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
  <link rel="stylesheet" href="../css/styleadmin.css">
  <link rel="stylesheet" type="text/css" href="../css/stylesdebtcontrol.css" media="screen" />
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
</head>
<body class="bodyadm">
  <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager" runat="server">
    </asp:ScriptManager>
    <asp:HiddenField ID="CodUsuario" runat="server" />
    <div class="container">
      <div class="row">
        <br />
        <asp:Button ID="btnVolver" runat="server" class="btn btn-default" Text="Volver" OnClick="btnVolver_Click" />
        <asp:Button ID="btnGrabar" runat="server" Text="Asignar" CssClass="btn btn-primary" />
        <hr />
      </div>

      <div class="row">
        <h3>
          <asp:Label ID="lblUsuario" runat="server"></asp:Label></h3>
      </div>
      <div class="row">
        <h4>CENTROS DE DISTRUCION ASIGNADOS</h4>
        <telerik:RadGrid ID="rdUsuarios" runat="server" ShowStatusBar="True" AutoGenerateColumns="false" OnNeedDataSource="rdUsuarios_NeedDataSource" OnInsertCommand="rdUsuarios_InsertCommand" OnItemDataBound="rdUsuarios_ItemDataBound"
          AllowSorting="True" PageSize="15" AllowPaging="True" GridLines="None" Skin="Sitefinity">
          <MasterTableView DataKeyNames="cod_centrodist" AutoGenerateColumns="false" ShowHeader="true"
            TableLayout="Fixed" ShowHeadersWhenNoRecords="true" CommandItemDisplay="Top">
            <CommandItemSettings ShowExportToExcelButton="false" ShowRefreshButton="false" ShowAddNewRecordButton="false" />
            <Columns>
              <telerik:GridButtonColumn ButtonType="PushButton" UniqueName="Eliminar" CommandName="cmdEliminar">
                <HeaderStyle Font-Size="Smaller" Width="50px" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
              </telerik:GridButtonColumn>

              <telerik:GridBoundColumn UniqueName="CodCentroDist" DataField="cod_centrodist" AllowSorting="true">
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
