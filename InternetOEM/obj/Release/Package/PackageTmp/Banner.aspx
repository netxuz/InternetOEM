<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Banner.aspx.cs" Inherits="ICommunity.Banner" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Banner</title>
</head>
<body>
  <form id="form1" runat="server">
  <asp:ScriptManager ID="ScriptManager1" runat="server">
  </asp:ScriptManager>
  <asp:HiddenField ID="CodBanner" runat="server" />
  <div class="entorno">
    <div class="panel">
      <div>
        <asp:Label ID="lblTituloBanner" runat="server" Text="Titulo"></asp:Label>
      </div>
      <div>
        <asp:TextBox ID="txtTituloBanner" runat="server"></asp:TextBox>
      </div>
      <div>
        <asp:Button ID="btnGrabar" runat="server" Text="Grabar" OnClick="btnGrabar_Click" />
      </div>
      <div>
        <asp:Label ID="lblTipoBanner" runat="server" Text="Tipo Banner"></asp:Label>
      </div>
      <div>
        <telerik:RadComboBox ID="rdCmbTipoBanner" runat="server">
          <Items>
            <telerik:RadComboBoxItem Text="Slide" Value="S" />
            <telerik:RadComboBoxItem Text="Rotator" Value="R" />
            <telerik:RadComboBoxItem Text="Carrusel" Value="C" />
          </Items>
        </telerik:RadComboBox>
      </div>
      <div>
        <asp:Label ID="lblEstado" runat="server" Text="Estado"></asp:Label>
      </div>
      <div>
        <telerik:RadComboBox ID="rdCmbEstado" runat="server">
          <Items>
            <telerik:RadComboBoxItem Text="Vigente" Value="V" />
            <telerik:RadComboBoxItem Text="No Vigente" Value="N" />
          </Items>
        </telerik:RadComboBox>
      </div>
    </div>
    <div id="Image" class="panel" runat="server" visible="false">
      <telerik:RadGrid ID="rdImage" Skin="Windows7" runat="server" PageSize="100" GridLines="None"
        AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" ShowStatusBar="true"
        OnNeedDataSource="rdImage_NeedDataSource" OnUpdateCommand="rdImage_UpdateCommand"
        OnDeleteCommand="rdImage_DeleteCommand" OnRowDrop="rdImage_RowDrop">
        <MasterTableView Width="100%" CommandItemDisplay="Top" DataKeyNames="cod_img_banner">
          <Columns>
            <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn">
            </telerik:GridEditCommandColumn>
            <telerik:GridBoundColumn DataField="nom_img_banner" HeaderText="Imagen" UniqueName="NomCampo">
            </telerik:GridBoundColumn>
            <telerik:GridButtonColumn ConfirmText="Desea eliminar la imagen?" ConfirmDialogType="RadWindow"
              ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete" Text="Delete"
              UniqueName="DeleteColumn">
            </telerik:GridButtonColumn>
          </Columns>
          <EditFormSettings EditFormType="WebUserControl" UserControlName="~/Controls/LoadImgBanner.ascx">
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
  <script type="text/javascript">
  function OnClientPasteHtml(sender, args){
    var commandName = args.get_commandName();
    var value = args.get_value();
      if (commandName == 'Paste'){
        args.set_value(cleanUpText(value));
      }
    };
  </script>
</body>
</html>
