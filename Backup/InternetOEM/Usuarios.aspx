<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="ICommunity.Usuarios" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta http-equiv="X-UA-Compatible" content="IE=8" />
  <title>Usuarios</title>
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
        <asp:TextBox ID="txt_buscar" runat="server"></asp:TextBox>
        <asp:Button ID="btnBuscar" runat="server" OnClick="btnBuscar_Click" />
      </div>
      <div>
        <telerik:RadGrid ID="rdUsuarios" runat="server" ShowStatusBar="True" AutoGenerateColumns="false"
          AllowSorting="True" PageSize="15" AllowPaging="True" GridLines="None" OnNeedDataSource="rdUsuarios_NeedDataSource"
          OnItemCommand="rdUsuarios_ItemCommand" OnItemDataBound="rdUsuarios_ItemDataBound">
          <MasterTableView DataKeyNames="cod_usuario">
            <Columns>
              <telerik:GridButtonColumn HeaderStyle-CssClass="HEditar" ButtonType="PushButton"
                ButtonCssClass="Editar" Text="Editar" UniqueName="Editar" CommandName="cmdEdit">
              </telerik:GridButtonColumn>
              
              <telerik:GridButtonColumn HeaderStyle-CssClass="HEliminar" ButtonType="PushButton"
                ButtonCssClass="Eliminar" Text="Eliminar" UniqueName="Eliminar" CommandName="cmdDelete">
              </telerik:GridButtonColumn>
              
              <telerik:GridBoundColumn UniqueName="NomUsuario" DataField="nom_usuario" AllowSorting="true">
              </telerik:GridBoundColumn>
              
              <telerik:GridBoundColumn UniqueName="ApeUsuario" DataField="ape_usuario" AllowSorting="true">
              </telerik:GridBoundColumn>
              
              <telerik:GridTemplateColumn UniqueName="EstUsuario" DataField="est_usuario" HeaderText="Estado">
                <ItemTemplate>
                  <telerik:RadTextBox ID="txtestusuario" runat="server" Text='<%# Bind("est_usuario") %>'>
                  </telerik:RadTextBox>
                </ItemTemplate>
              </telerik:GridTemplateColumn>
            </Columns>
          </MasterTableView>
        </telerik:RadGrid>
      </div>
    </div>
  </div>
  </form>
</body>
</html>
