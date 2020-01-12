<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Contenidos.aspx.cs" Inherits="ICommunity.Contenidos" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Contenido</title>
</head>
<body>
  <form id="form1" runat="server">
  <asp:ScriptManager ID="ScriptManager" runat="server">
  </asp:ScriptManager>
  <asp:HiddenField ID="CodNodo" runat="server" />
    <div class="entorno">
    <div class="panel">
      <div class="boton">
        <asp:Button ID="btnCrear" runat="server" Text="" OnClick="btnCrear_Click" />
      </div>
      <div>
        <telerik:RadGrid ID="rdContenido" runat="server" ShowStatusBar="True" AutoGenerateColumns="false"
          AllowSorting="True" PageSize="15" AllowPaging="True" GridLines="None" OnNeedDataSource="rdContenido_NeedDataSource"
          OnItemCommand="rdContenido_ItemCommand" OnItemDataBound="rdContenido_ItemDataBound">
          <MasterTableView DataKeyNames="cod_contenido">
            <Columns>
              <telerik:GridButtonColumn HeaderStyle-CssClass="HEditar" ButtonType="PushButton"
                ButtonCssClass="Editar" Text="Editar" UniqueName="Editar" CommandName="cmdEdit">
              </telerik:GridButtonColumn>
              <telerik:GridButtonColumn HeaderStyle-CssClass="HEliminar" ButtonType="PushButton"
                ButtonCssClass="Eliminar" Text="Eliminar" UniqueName="Eliminar" CommandName="cmdDelete">
              </telerik:GridButtonColumn>
              <telerik:GridBoundColumn UniqueName="NomContenido" DataField="titulo_contenido" AllowSorting="true">
              </telerik:GridBoundColumn>
              <telerik:GridBoundColumn UniqueName="EstContenido" DataField="est_contenido" AllowSorting="true">
              </telerik:GridBoundColumn>
            </Columns>
          </MasterTableView>
        </telerik:RadGrid>
      </div>
    </div>
  </form>
</body>
</html>
