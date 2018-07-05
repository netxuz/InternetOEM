<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="applistactividad.aspx.cs" Inherits="ICommunity.AppRuta.applistactividad" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta charset="utf-8">
  <meta http-equiv="X-UA-Compatible" content="IE=edge">
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
    <div class="container">
      <div class="row">&nbsp;</div>
      <div class="row">
        <asp:Button ID="btnVolver" runat="server" class="btn btn-default" Text="Volver" OnClick="btnVolver_Click" />
      </div>
      <div class="row">
        <h4>DIAS DE RUTA MOTORISTA <asp:Label ID="lblMotorista" runat="server"></asp:Label></h4>
      </div>
      <div class="row">&nbsp;</div>
      <div class="row">
        <telerik:RadGrid ID="rdActividad" runat="server" ShowStatusBar="True" AutoGenerateColumns="false"
          AllowSorting="True" PageSize="10" AllowPaging="True" GridLines="None" OnNeedDataSource="rdActividad_NeedDataSource" 
          OnItemCommand="rdActividad_ItemCommand" OnItemDataBound="rdActividad_ItemDataBound" Skin="Sitefinity">
          <MasterTableView DataKeyNames="key_fecha_actividad" AutoGenerateColumns="false" ShowHeader="true"
            TableLayout="Fixed" ShowHeadersWhenNoRecords="true" CommandItemDisplay="Top">
            <CommandItemSettings ShowExportToExcelButton="false" ShowRefreshButton="false" ShowAddNewRecordButton="false" />
            <Columns>
              <telerik:GridButtonColumn ButtonType="PushButton" UniqueName="Editar" CommandName="cmdEdit">
                <HeaderStyle Font-Size="Smaller" Width="50px" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
              </telerik:GridButtonColumn>

              <telerik:GridBoundColumn UniqueName="fecha_actividad" DataField="fecha_actividad" HeaderText="FECHA DE ACTIVIDAD" AllowSorting="true">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Left" />
              </telerik:GridBoundColumn>

            </Columns>
          </MasterTableView>
        </telerik:RadGrid>
      </div>
    </div>
    <asp:HiddenField ID="hddcodusuario" runat="server" />
  </form>
</body>
</html>
