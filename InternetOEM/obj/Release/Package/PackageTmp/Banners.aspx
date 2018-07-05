<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Banners.aspx.cs" Inherits="ICommunity.Banners" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager" runat="server">
  </asp:ScriptManager>
  <div class="entorno">
    <div class="panel">
      <div>
        <asp:Button ID="btnCrear" runat="server" OnClick="btnCrear_Click" />
      </div>
      <div>
        <telerik:RadGrid ID="rdBanners" runat="server" ShowStatusBar="True" AutoGenerateColumns="false"
          AllowSorting="True" PageSize="15" AllowPaging="True" GridLines="None" OnNeedDataSource="rdBanners_NeedDataSource"
          OnItemCommand="rdBanners_ItemCommand" OnItemDataBound="rdBanners_ItemDataBound">
          <MasterTableView DataKeyNames="cod_banner">
            <Columns>
              <telerik:GridButtonColumn HeaderStyle-CssClass="HEditar" ButtonType="PushButton"
                ButtonCssClass="Editar" Text="Editar" UniqueName="Editar" CommandName="cmdEdit">
              </telerik:GridButtonColumn>
              <telerik:GridButtonColumn HeaderStyle-CssClass="HEliminar" ButtonType="PushButton"
                ButtonCssClass="Eliminar" Text="Eliminar" UniqueName="Eliminar" CommandName="cmdDelete">
              </telerik:GridButtonColumn>
              <telerik:GridBoundColumn UniqueName="NomBanner" DataField="nom_banner" AllowSorting="true">
              </telerik:GridBoundColumn>
              <telerik:GridBoundColumn UniqueName="EstBanner" DataField="est_banner" AllowSorting="true">
              </telerik:GridBoundColumn>
            </Columns>
          </MasterTableView>
        </telerik:RadGrid>
      </div>
    </div>
  </div>
    </form>
</body>
</html>
