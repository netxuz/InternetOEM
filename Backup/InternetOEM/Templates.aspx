<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Templates.aspx.cs" Inherits="ICommunity.Templates" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Untitled Page</title>
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
        <telerik:RadGrid ID="rdTemplate" runat="server" ShowStatusBar="True" AutoGenerateColumns="false"
          AllowSorting="True" PageSize="15" AllowPaging="True" GridLines="None" OnNeedDataSource="rdTemplate_NeedDataSource"
          OnItemCommand="rdTemplate_ItemCommand" OnItemDataBound="rdTemplate_ItemDataBound">
          <MasterTableView DataKeyNames="cod_template">
            <Columns>
              <telerik:GridButtonColumn HeaderStyle-CssClass="HEditar" ButtonType="PushButton"
                ButtonCssClass="Editar" Text="Editar" UniqueName="Editar" CommandName="cmdEdit">
              </telerik:GridButtonColumn>
              <telerik:GridButtonColumn HeaderStyle-CssClass="HEliminar" ButtonType="PushButton"
                ButtonCssClass="Eliminar" Text="Eliminar" UniqueName="Eliminar" CommandName="cmdDelete">
              </telerik:GridButtonColumn>
              <telerik:GridBoundColumn UniqueName="NomTemplate" DataField="nom_template" AllowSorting="true">
              </telerik:GridBoundColumn>
              <telerik:GridBoundColumn UniqueName="EstTemplate" DataField="est_template" AllowSorting="true">
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
