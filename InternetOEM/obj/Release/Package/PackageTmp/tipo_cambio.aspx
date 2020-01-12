<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tipo_cambio.aspx.cs" Inherits="ICommunity.tipo_cambio" %>

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
        <div class="col-md-3">
          <asp:DropDownList ID="rdCmbMeses" runat="server" CssClass="form-control">
            <asp:ListItem Value="0" Text="<< Seleccione Mes >>"></asp:ListItem>
            <asp:ListItem Value="01" Text="Enero"></asp:ListItem>
            <asp:ListItem Value="02" Text="Febrero"></asp:ListItem>
            <asp:ListItem Value="03" Text="Marzo"></asp:ListItem>
            <asp:ListItem Value="04" Text="Abril"></asp:ListItem>
            <asp:ListItem Value="05" Text="Mayo"></asp:ListItem>
            <asp:ListItem Value="06" Text="Junio"></asp:ListItem>
            <asp:ListItem Value="07" Text="Julio"></asp:ListItem>
            <asp:ListItem Value="08" Text="Agosto"></asp:ListItem>
            <asp:ListItem Value="09" Text="Septiembre"></asp:ListItem>
            <asp:ListItem Value="10" Text="Octubre"></asp:ListItem>
            <asp:ListItem Value="11" Text="Noviembre"></asp:ListItem>
            <asp:ListItem Value="12" Text="Diciembre"></asp:ListItem>
          </asp:DropDownList>
        </div>
        <div class="col-md-2">
          <asp:DropDownList ID="rsTpCmb" runat="server" CssClass="form-control">
            <asp:ListItem Value="3" Text="CLP a USD"></asp:ListItem>
            <asp:ListItem Value="2" Text="SOL a CLP"></asp:ListItem>
            <asp:ListItem Value="4" Text="COL a CLP"></asp:ListItem>
            <asp:ListItem Value="5" Text="ARS a CLP"></asp:ListItem>
            <asp:ListItem Value="1" Text="CLP a CLP"></asp:ListItem>
            
          </asp:DropDownList>
        </div>
        <div class="col-md-2">
          <asp:TextBox ID="txt_valor" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-md-1">
          <asp:Button ID="btn_grabar" runat="server" Text="Agregar" CssClass="btn btn-primary" OnClick="btn_grabar_Click" />
        </div>
      </div>
      <div class="row">
        <div class="col-md-12">&nbsp;</div>
      </div>
      <div class="row">
        <div class="col-md-12">
          <div class="row">
            <div class="col-md-3">
              <asp:Label ID="lbl_tipo_cambio" runat="server" Text="Tipo Cambio"></asp:Label>
              <asp:DropDownList ID="rdTipoCambio" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="rdTipoCambio_SelectedIndexChanged">
                <asp:ListItem Value="3" Text="CLP a USD"  Selected="True"></asp:ListItem>
                <asp:ListItem Value="2" Text="SOL a CLP"></asp:ListItem>
                <asp:ListItem Value="4" Text="COL a CLP"></asp:ListItem>
                <asp:ListItem Value="5" Text="ARS a CLP"></asp:ListItem>
                <asp:ListItem Value="1" Text="CLP a CLP"></asp:ListItem>
              </asp:DropDownList>
            </div>
          </div>
          <div class="row">
            <div class="col-md-12">&nbsp;</div>
          </div>
          <div class="row">
            <div class="col-md-12">
              <telerik:RadGrid ID="rdGridTipoCambio" runat="server" ShowStatusBar="True" AutoGenerateColumns="false"
                AllowSorting="True" PageSize="15" AllowPaging="True" GridLines="None"
                OnNeedDataSource="rdGridTipoCambio_NeedDataSource" OnItemCommand="rdGridTipoCambio_ItemCommand"
                OnItemDataBound="rdGridTipoCambio_ItemDataBound" Skin="Sitefinity">
                <MasterTableView DataKeyNames="cod_lic_moneda" AutoGenerateColumns="false" ShowHeader="true" TableLayout="Fixed" ShowHeadersWhenNoRecords="true" CommandItemDisplay="Top">
                  <CommandItemSettings ShowExportToExcelButton="false" ShowRefreshButton="false" ShowAddNewRecordButton="false" />
                  <Columns>
                    <telerik:GridButtonColumn ButtonType="PushButton" UniqueName="Eliminar" CommandName="cmdDelete">
                      <HeaderStyle Font-Size="Smaller" Width="50px" HorizontalAlign="Center" />
                      <ItemStyle HorizontalAlign="Center" />
                    </telerik:GridButtonColumn>

                    <telerik:GridBoundColumn DataField="mes" HeaderStyle-Width="100px" HeaderText="Mes"
                      UniqueName="mes">
                      <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ano" HeaderStyle-Width="100px" HeaderText="Año"
                      UniqueName="ano">
                      <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="valor_moneda" HeaderStyle-Width="100px" HeaderText="Valor"
                      UniqueName="valor_moneda">
                      <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                    </telerik:GridBoundColumn>
                  </Columns>
                </MasterTableView>
              </telerik:RadGrid>
            </div>
          </div>

        </div>
      </div>
    </div>
    <!-- Central Modal Medium Warning -->
    <div class="modal fade" id="centralModalWarning" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
      aria-hidden="true">
      <div class="modal-dialog modal-notify modal-warning" role="document">
        <!--Content-->
        <div class="modal-content">
          <!--Header-->
          <div class="modal-header">
            <p class="heading lead">Cambio realizado.</p>

            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true" class="white-text">&times;</span>
            </button>
          </div>

          <!--Body-->
          <div class="modal-body">
            <div class="text-center">
              <i class="fas fa-check fa-4x mb-3 animated rotateIn"></i>
              <p>
                Se ha realizado correctamente el cambio.
              </p>
            </div>
          </div>

          <!--Footer-->
          <div class="modal-footer justify-content-center">
            <a type="button" class="btn btn-primary" data-dismiss="modal">Cerrar</a>
          </div>
        </div>
        <!--/.Content-->
      </div>
    </div>
    <!-- Central Modal Medium Warning-->
  </form>
</body>
</html>
