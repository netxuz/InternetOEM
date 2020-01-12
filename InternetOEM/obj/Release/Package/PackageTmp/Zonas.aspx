<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Zonas.aspx.cs" Inherits="ICommunity.Zonas" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
        <asp:Button ID="btnCrear" runat="server" CssClass="btn btn-primary" OnClick="btnCrear_Click" />
      </div>
      <div class="row">&nbsp;</div>
      <div class="row">
        <telerik:RadGrid ID="rdZona" runat="server" ShowStatusBar="True" AutoGenerateColumns="false"
          AllowSorting="True" PageSize="15" AllowPaging="True" GridLines="None" OnNeedDataSource="rdZona_NeedDataSource"
          OnItemCommand="rdZona_ItemCommand" OnItemDataBound="rdZona_ItemDataBound" Skin="Sitefinity">
          <MasterTableView DataKeyNames="cod_zona" AutoGenerateColumns="false" ShowHeader="true"
            TableLayout="Fixed" ShowHeadersWhenNoRecords="true" CommandItemDisplay="Top">
            <CommandItemSettings ShowExportToExcelButton="false" ShowRefreshButton="false" ShowAddNewRecordButton="false" />
            <Columns>
              <telerik:GridButtonColumn ButtonType="PushButton" UniqueName="Editar" CommandName="cmdEdit">
                <HeaderStyle Font-Size="Smaller" Width="50px" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
              </telerik:GridButtonColumn>
              <telerik:GridButtonColumn ButtonType="PushButton" UniqueName="Eliminar" CommandName="cmdDelete">
                <HeaderStyle Font-Size="Smaller" Width="50px" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
              </telerik:GridButtonColumn>
              <telerik:GridBoundColumn UniqueName="NomZona" DataField="nom_zona" AllowSorting="true">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Left" />
              </telerik:GridBoundColumn>
              <telerik:GridBoundColumn UniqueName="EstZona" DataField="est_zona" AllowSorting="true">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
              </telerik:GridBoundColumn>
            </Columns>
          </MasterTableView>
        </telerik:RadGrid>
      </div>
    </div>
  </form>
</body>
</html>
