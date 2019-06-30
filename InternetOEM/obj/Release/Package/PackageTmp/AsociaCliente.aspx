<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AsociaCliente.aspx.cs" Inherits="ICommunity.AsociaCliente" %>

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
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="container">
      <div class="row">&nbsp;</div>
      <div class="row">
        <asp:Button ID="btnVolver" runat="server" class="btn btn-default" Text="Volver" OnClick="btnVolver_Click" />
        <button type="button" id="btnSeleccionar" class="btn btn-primary" data-toggle="modal" data-target="#myModal">Asociar Cliente a Usuario</button>
      </div>
      <div class="row">&nbsp;</div>
      <div class="row">
        <h4>
          <asp:Label ID="lblClientesAsignados" Text="" runat="server"></asp:Label></h4>
      </div>
      <div class="row">&nbsp;</div>
      <div class="row">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" OnLoad="UpdatePanel1_Load">
          <ContentTemplate>
            <asp:Label ID="lblStatus" runat="server" Visible="false"></asp:Label>
            <telerik:RadGrid ID="rdClienteUsuario" runat="server" ShowStatusBar="True" AutoGenerateColumns="false" AllowSorting="True" PageSize="15" AllowPaging="True" GridLines="None" Skin="Sitefinity"
              OnNeedDataSource="rdClienteUsuario_NeedDataSource"
              OnItemCommand="rdClienteUsuario_ItemCommand"
              OnItemDataBound="rdClienteUsuario_ItemDataBound">
              <MasterTableView DataKeyNames="nkey_user" AutoGenerateColumns="false" ShowHeader="true"
                TableLayout="Fixed" ShowHeadersWhenNoRecords="true" CommandItemDisplay="Top">
                <CommandItemSettings ShowExportToExcelButton="false" ShowRefreshButton="false" ShowAddNewRecordButton="false" />
                <Columns>

                  <telerik:GridButtonColumn ButtonType="PushButton" UniqueName="Eliminar" CommandName="cmdDelete">
                    <HeaderStyle Font-Size="Smaller" Width="50px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                  </telerik:GridButtonColumn>

                  <telerik:GridBoundColumn UniqueName="nkey_user" HeaderText="nKeyCliente" DataField="nkey_user" AllowSorting="true">
                    <HeaderStyle Font-Size="Smaller" Width="100px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                  </telerik:GridBoundColumn>

                  <telerik:GridBoundColumn UniqueName="sNombre" HeaderText="Razón Social" DataField="sNombre" AllowSorting="true">
                    <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" />
                  </telerik:GridBoundColumn>

                </Columns>
              </MasterTableView>

            </telerik:RadGrid>
          </ContentTemplate>
        </asp:UpdatePanel>
      </div>
      <div class="row">
        <div class="modal fade" id="myModal" role="dialog">
          <div class="modal-dialog modal-lg">

            <!-- Modal content-->
            <div class="modal-content">
              <div class="modal-header">
                <asp:Button ID="btnClose2" Text="&times;" runat="server" CssClass="close" OnClick="btnClose2_Click" />
                <h4 class="modal-title">Seleccione Cliente</h4>
              </div>
              <div class="modal-body">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                  <ContentTemplate>
                    <div class="row">
                      <div class="col-xs-4">
                        <asp:TextBox ID="txtBuscarCliente" CssClass="form-control" runat="server"></asp:TextBox>
                      </div>
                      <div class="col-xs-4">
                        <asp:Button ID="BtnBuscarClienteNotIn" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="BtnBuscarClienteNotIn_Click" />
                      </div>
                    </div>
                    <div class="row">&nbsp;</div>
                    <div class="row">
                      <telerik:RadGrid ID="rdCliente" runat="server" ShowStatusBar="True" AutoGenerateColumns="false" OnNeedDataSource="rdCliente_NeedDataSource" OnItemCommand="rdCliente_ItemCommand" OnItemDataBound="rdCliente_ItemDataBound"
                        AllowSorting="True" PageSize="15" AllowPaging="True" GridLines="None" Skin="Sitefinity">
                        <MasterTableView DataKeyNames="nkey_cliente" AutoGenerateColumns="false" ShowHeader="true"
                          TableLayout="Fixed" ShowHeadersWhenNoRecords="true" CommandItemDisplay="Top">
                          <CommandItemSettings ShowExportToExcelButton="false" ShowRefreshButton="false" ShowAddNewRecordButton="false" />
                          <Columns>

                            <telerik:GridButtonColumn ButtonType="PushButton" UniqueName="Agregar" CommandName="cmdAgregar">
                              <HeaderStyle Font-Size="Smaller" Width="50px" HorizontalAlign="Center" />
                              <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridButtonColumn>

                            <telerik:GridBoundColumn UniqueName="sNombre" HeaderText="nKeyCliente" DataField="nkey_cliente" AllowSorting="true">
                              <HeaderStyle Font-Size="Smaller" Width="100px" HorizontalAlign="Center" />
                              <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn UniqueName="sNombre" HeaderText="Razón Social" DataField="sNombre" AllowSorting="true">
                              <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                              <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>

                          </Columns>
                        </MasterTableView>
                      </telerik:RadGrid>
                    </div>
                  </ContentTemplate>
                </asp:UpdatePanel>
              </div>
              <div class="modal-footer">
                <asp:Button ID="btnClose" Text="Close" runat="server" CssClass="btn btn-default" OnClick="btnClose_Click" />
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <asp:HiddenField ID="CodUsuario" runat="server" />
  </form>
</body>
</html>
