<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CampoUsuarios.aspx.cs"
  Inherits="ICommunity.CampoUsuarios" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Campos Usuario</title>
</head>
<body>
  <form id="form1" runat="server">
  <asp:ScriptManager ID="ScriptManager" runat="server">
  </asp:ScriptManager>
  <div class="entorno">
    <div class="panel">
      <telerik:RadGrid ID="rdCampoUsuarios" Skin="Windows7" runat="server" PageSize="100"
        GridLines="None" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
        ShowStatusBar="true" OnNeedDataSource="rdCampoUsuarios_NeedDataSource" OnUpdateCommand="rdCampoUsuarios_UpdateCommand"
        OnDeleteCommand="rdCampoUsuarios_DeleteCommand" OnRowDrop="rdCampoUsuarios_RowDrop">
        <MasterTableView Width="100%" CommandItemDisplay="Top" DataKeyNames="cod_campo">
          <Columns>
            <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn">
            </telerik:GridEditCommandColumn>
            <telerik:GridBoundColumn DataField="nom_campo" HeaderText="Nombre Campo" UniqueName="NomCampo">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="tipo_campo" HeaderText="Tipo Campo" UniqueName="TipoCampo">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="est_campo" HeaderText="Estado" UniqueName="EstCampo">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="desp_campo" HeaderText="DespCampo" UniqueName="DespCampo" Visible="false">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="ind_despliegue" HeaderText="DespUsuario" UniqueName="DespUsuario" Visible="false">
            </telerik:GridBoundColumn>
            <telerik:GridButtonColumn ConfirmText="Desea eliminar el campo?" ConfirmDialogType="RadWindow"
              ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete" Text="Delete"
              UniqueName="DeleteColumn">
            </telerik:GridButtonColumn>
          </Columns>
          <EditFormSettings EditFormType="WebUserControl" UserControlName="~/Controls/CampoUsuario.ascx">
            <EditColumn UniqueName="EditCommandColumn1">
            </EditColumn>
          </EditFormSettings>
        </MasterTableView>
        <ClientSettings AllowRowsDragDrop="True">
          <Selecting AllowRowSelect="True" EnableDragToSelectRows="false" />
          <ClientEvents OnRowDblClick="RowDblClick" />
        </ClientSettings>
      </telerik:RadGrid>
    </div>
  </div>
  <telerik:RadCodeBlock ID="RadCodeBlock" runat="server">
    <script type="text/javascript">
      function RowDblClick(sender, eventArgs){
        sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
      }
    </script>
  </telerik:RadCodeBlock>
  </form>
</body>
</html>
