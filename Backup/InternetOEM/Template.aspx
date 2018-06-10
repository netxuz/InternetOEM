<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Template.aspx.cs" Inherits="ICommunity.Template1" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="Controls/PageObject.ascx" TagName="PageObject" TagPrefix="Online" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Untitled Page</title>
</head>
<body>
  <form id="form1" runat="server">
  <asp:ScriptManager ID="ScriptManager1" runat="server">
  </asp:ScriptManager>
  <asp:HiddenField ID="CodTemplate" runat="server" />
  <div class="entorno">
    <div class="panel">
      <div>
        <asp:Label ID="lblTitulo" runat="server" Text="Título"></asp:Label>
      </div>
      <div>
        <asp:TextBox ID="txtTitulo" runat="server"></asp:TextBox>
      </div>
      <div>
        <asp:Button ID="btnGrabar" runat="server" Text="Grabar" OnClick="btnGrabar_Click" />
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
    <div class="panel">
      <asp:Label ID="lblDescripcion" runat="server" Text="Descripcion"></asp:Label>
    </div>
    <div>
      <telerik:RadEditor ID="rdDescripcion" runat="server" OnClientLoad="OnClientLoad" OnClientPasteHtml="OnClientPasteHtml">
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
      <Online:PageObject ID="PageObject1" pbIndZonas="true" runat="server" />
    </div>
  </div>

  <script language="JavaScript" type="text/javascript">
    function GetIdEditor() {
      return $find("<%= rdDescripcion.ClientID %>");
    };
    function OnClientPasteHtml(sender, args){
    var commandName = args.get_commandName();
    var value = args.get_value();
      if (commandName == 'Paste'){
        args.set_value(cleanUpText(value));
      }
    };
  </script>

  </form>
</body>
</html>
