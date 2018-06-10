<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="usuario_rep_debtcontrol.aspx.cs" Inherits="ICommunity.usuario_rep_debtcontrol" %>

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
    <asp:HiddenField ID="CodUsuario" runat="server" />
    <div class="container">
      <div class="row">&nbsp;</div>
      <div class="row">
        <asp:Button ID="btnVolver" runat="server" class="btn btn-default" Text="Volver" OnClick="btnVolver_Click" />
        <button type="button" id="btnSeleccionar" class="btn btn-primary" data-toggle="modal" data-target="#myModal">Agregar Reporte a Usuario</button>
      </div>
      <div class="row">&nbsp;</div>
      <div class="row">
        <h4><asp:Label ID="lblUsuario" runat="server"></asp:Label></h4>
      </div>
      <div class="row">&nbsp;</div>
      <div class="row">
        <div class="form-group">
          <div class="col-xs-4">
            <asp:TextBox ID="txtBuscar" placeholder="Buscar" CssClass="form-control" runat="server"></asp:TextBox>
          </div>
          <div class="col-xs-4">
            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="btnBuscar_Click" />
          </div>
        </div>
      </div>
      <div class="row">&nbsp;</div>
      <div class="row">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" OnLoad="UpdatePanel1_Load">
          <ContentTemplate>
            <asp:Label ID="lblStatus" runat="server" Visible="false"></asp:Label>
            <telerik:RadGrid ID="rdReportesUsuario" runat="server" ShowStatusBar="True" AutoGenerateColumns="false"
              OnNeedDataSource="rdReportesUsuario_NeedDataSource" OnItemCommand="rdReportesUsuario_ItemCommand" OnItemDataBound="rdReportesUsuario_ItemDataBound"
              AllowSorting="True" PageSize="10" AllowPaging="True" GridLines="None" Skin="Sitefinity">
              <MasterTableView DataKeyNames="cod_consulta" AutoGenerateColumns="false" ShowHeader="true"
                TableLayout="Fixed" ShowHeadersWhenNoRecords="true" CommandItemDisplay="Top">
                <CommandItemSettings ShowExportToExcelButton="false" ShowRefreshButton="false" ShowAddNewRecordButton="false" />
                <Columns>

                  <telerik:GridButtonColumn ButtonType="PushButton" UniqueName="Eliminar" CommandName="cmdDelete">
                    <HeaderStyle Font-Size="Smaller" Width="50px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                  </telerik:GridButtonColumn>

                  <telerik:GridBoundColumn UniqueName="NomConsulta" DataField="nom_consulta" AllowSorting="true">
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
                <%--<button type="button" class="close" data-dismiss="modal">&times;</button>--%>
                <asp:Button ID="btnClose2" Text="&times;" runat="server" CssClass="close" OnClick="btnClose2_Click" />
                <h4 class="modal-title">Selecciona Conultas</h4>
              </div>
              <div class="modal-body">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                  <ContentTemplate>
                    <div class="row">
                      <div class="col-xs-4">
                        <asp:TextBox ID="txtBuscarReportes" CssClass="form-control" runat="server"></asp:TextBox>
                      </div>
                      <div class="col-xs-4">
                        <asp:Button ID="BtnBuscarReportesNotIn" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="BtnBuscarReportesNotIn_Click" />
                      </div>
                    </div>
                    <div class="row">&nbsp;</div>
                    <div class="row">
                      <telerik:RadGrid ID="rdReportesNotIn" runat="server" ShowStatusBar="True" AutoGenerateColumns="false"
                        OnNeedDataSource="rdReportesNotIn_NeedDataSource" OnItemCommand="rdReportesNotIn_ItemCommand" OnItemDataBound="rdReportesNotIn_ItemDataBound"
                        AllowSorting="True" PageSize="7" AllowPaging="True" GridLines="None" Skin="Sitefinity">
                        <MasterTableView DataKeyNames="cod_consulta" AutoGenerateColumns="false" ShowHeader="true"
                          TableLayout="Fixed" ShowHeadersWhenNoRecords="true" CommandItemDisplay="Top">
                          <CommandItemSettings ShowExportToExcelButton="false" ShowRefreshButton="false" ShowAddNewRecordButton="false" />
                          <Columns>

                            <telerik:GridButtonColumn ButtonType="PushButton" UniqueName="Agregar" CommandName="cmdAgregar">
                              <HeaderStyle Font-Size="Smaller" Width="50px" HorizontalAlign="Center" />
                              <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridButtonColumn>

                            <telerik:GridBoundColumn UniqueName="NomConsulta" DataField="nom_consulta" AllowSorting="true">
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
  </form>
</body>
</html>
