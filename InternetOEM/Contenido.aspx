<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Contenido.aspx.cs" Inherits="ICommunity.Contenido" %>

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
  <asp:HiddenField ID="CodNodo" runat="server" />
  <asp:HiddenField ID="CodContenido" runat="server" />
  <div class="entorno">
    <div class="panel">
      <div>
        <asp:Label ID="lblTitulo" runat="server" Text="Titulo"></asp:Label>
      </div>
      <div>
        <asp:TextBox ID="txtTitulo" runat="server"></asp:TextBox>
      </div>
      <div>
        <asp:Button ID="btnGrabar" runat="server" Text="Grabar" OnClick="btnGrabar_Click" />
        <asp:Button ID="btnArchivos" runat="server" Text="Archivos" Visible="false" OnClientClick="LoadImage(); return false;" />
      </div>
      <div>
        <asp:Label ID="lblEstado" runat="server" Text="Estado"></asp:Label>
      </div>
      <div>
        <telerik:RadComboBox ID="rdCmbEstado" runat="server">
          <Items>
            <telerik:RadComboBoxItem Text="Vigente" Value="V" />
            <telerik:RadComboBoxItem Text="No Vigente" Value="N" />
            <telerik:RadComboBoxItem Text="Usuario" Value="P" />
          </Items>
        </telerik:RadComboBox>
      </div>
      <div>
        <asp:CheckBox ID="chk_destacado" runat="server" Text="Contenido Destacado" />
      </div>
      <div>
        <asp:CheckBox ID="chk_rss" runat="server" Text="Contenido para RSS" />
      </div>
      <div>
        <asp:Label ID="lblResumen" runat="server" Text="Resúmen"></asp:Label>
      </div>
      <div>
        <telerik:RadEditor ID="rdResumen" runat="server" OnClientPasteHtml="OnClientPasteHtml">
          <Tools>
            <telerik:EditorToolGroup Tag="FileManagers">
              <telerik:EditorTool Name="ImageManager" />
              <telerik:EditorTool Name="FlashManager" />
              <telerik:EditorTool Name="MediaManager" />
            </telerik:EditorToolGroup>
            <telerik:EditorToolGroup>
              <telerik:EditorTool Name="Bold" />
              <telerik:EditorTool Name="Italic" />
              <telerik:EditorTool Name="Underline" />
              <telerik:EditorSeparator />
              <telerik:EditorTool Name="ForeColor" />
              <telerik:EditorTool Name="BackColor" />
              <telerik:EditorSeparator />
              <telerik:EditorTool Name="FontName" />
              <telerik:EditorTool Name="RealFontSize" />
            </telerik:EditorToolGroup>
          </Tools>
          <ImageManager ViewPaths="~/ContentImages" DeletePaths="~/ContentImages" UploadPaths="~/ContentImages" />
          <FlashManager ViewPaths="~/ContentFlash" DeletePaths="~/ContentFlash" UploadPaths="~/ContentFlash" />
          <MediaManager ViewPaths="~/ContentMedia" DeletePaths="~/ContentMedia" UploadPaths="~/ContentMedia" />
          <Languages>
            <telerik:SpellCheckerLanguage Code="es-ES" Title="Español" />
          </Languages>
          <Content>
          </Content>
        </telerik:RadEditor>
      </div>
      <div>
        <asp:Label ID="lblContenido" runat="server" Text="Contenido"></asp:Label>
      </div>
      <div>
        <telerik:RadEditor ID="rdDescripcion" runat="server" OnClientPasteHtml="OnClientPasteHtml">
        <Tools>
            <telerik:EditorToolGroup Tag="FileManagers">
              <telerik:EditorTool Name="ImageManager" />
              <telerik:EditorTool Name="FlashManager" />
              <telerik:EditorTool Name="MediaManager" />
            </telerik:EditorToolGroup>
            <telerik:EditorToolGroup>
              <telerik:EditorTool Name="Bold" />
              <telerik:EditorTool Name="Italic" />
              <telerik:EditorTool Name="Underline" />
              <telerik:EditorSeparator />
              <telerik:EditorTool Name="ForeColor" />
              <telerik:EditorTool Name="BackColor" />
              <telerik:EditorSeparator />
              <telerik:EditorTool Name="FontName" />
              <telerik:EditorTool Name="RealFontSize" />
            </telerik:EditorToolGroup>
          </Tools>
          <ImageManager ViewPaths="~/ContentImages" DeletePaths="~/ContentImages" UploadPaths="~/ContentImages" />
          <FlashManager ViewPaths="~/ContentFlash" DeletePaths="~/ContentFlash" UploadPaths="~/ContentFlash" />
          <MediaManager ViewPaths="~/ContentMedia" DeletePaths="~/ContentMedia" UploadPaths="~/ContentMedia" />
          <Languages>
            <telerik:SpellCheckerLanguage Code="es-ES" Title="Español" />
          </Languages>
          <Content>
          </Content>
        </telerik:RadEditor>
      </div>
      <div id="idGridFile" runat="server" visible="false">
        <div>
          <asp:Label ID="lblArchivos" runat="server" Text="Archivos"></asp:Label>
        </div>
        <div>
          <telerik:RadGrid ID="rdgArchivos" runat="server" ShowStatusBar="True" AutoGenerateColumns="false"
            AllowSorting="True" PageSize="15" AllowPaging="True" GridLines="None" OnNeedDataSource="rdgArchivos_NeedDataSource"
            OnItemCommand="rdgArchivos_ItemCommand">
            <MasterTableView DataKeyNames="cod_archivo">
              <Columns>
                <telerik:GridButtonColumn ButtonType="PushButton" ButtonCssClass="Eliminar" Text="Eliminar"
                  UniqueName="Eliminar" CommandName="cmdDelete">
                </telerik:GridButtonColumn>
                <telerik:GridBoundColumn UniqueName="NomArchivo" HeaderText="Archivo" DataField="nom_archivo"
                  AllowSorting="true">
                </telerik:GridBoundColumn>
              </Columns>
            </MasterTableView>
          </telerik:RadGrid>
        </div>
      </div>
    </div>
  </div>
  <telerik:RadWindowManager ID="RadWindowManager" runat="server">
    <Windows>
      <telerik:RadWindow ID="oRdWindow" runat="server">
      </telerik:RadWindow>
    </Windows>
  </telerik:RadWindowManager>
  </form>

  <script type="text/javascript">
  function LoadImage(){
    var pCodContenido = document.getElementById("<%= CodContenido.ClientID %>");
    var cUrl = "LoadImages.aspx?CodContenido=" + pCodContenido.value;
    var oWnd = $find("oRdWindow");
    oWnd.setUrl(cUrl);
    oWnd.setSize(600,400);
    oWnd.set_modal(false);
    oWnd.center();
    oWnd.show();
    oWnd.add_close(OnClientClose);
  };
  function OnClientClose(sender, eventArgs){
    document.forms[0].submit();
  };
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
