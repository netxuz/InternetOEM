<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Zonas.aspx.cs" Inherits="ICommunity.Zonas" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title></title>
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
        <telerik:radgrid id="rdZona" runat="server" showstatusbar="True" autogeneratecolumns="false"
          allowsorting="True" pagesize="15" allowpaging="True" gridlines="None" onneeddatasource="rdZona_NeedDataSource"
          onitemcommand="rdZona_ItemCommand" onitemdatabound="rdZona_ItemDataBound">
          <MasterTableView DataKeyNames="cod_zona">
            <Columns>
              <telerik:GridButtonColumn HeaderStyle-CssClass="HEditar" ButtonType="PushButton"
                ButtonCssClass="Editar" Text="Editar" UniqueName="Editar" CommandName="cmdEdit">
              </telerik:GridButtonColumn>
              <telerik:GridButtonColumn HeaderStyle-CssClass="HEliminar" ButtonType="PushButton"
                ButtonCssClass="Eliminar" Text="Eliminar" UniqueName="Eliminar" CommandName="cmdDelete">
              </telerik:GridButtonColumn>
              <telerik:GridBoundColumn UniqueName="NomZona" DataField="nom_zona" AllowSorting="true">
              </telerik:GridBoundColumn>
              <telerik:GridBoundColumn UniqueName="EstZona" DataField="est_zona" AllowSorting="true">
              </telerik:GridBoundColumn>
            </Columns>
          </MasterTableView>
        </telerik:radgrid>
      </div>
    </div>
  </div>
  </form>
</body>
</html>
