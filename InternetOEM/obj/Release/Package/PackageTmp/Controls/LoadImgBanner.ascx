<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoadImgBanner.ascx.cs"
  Inherits="ICommunity.Controls.LoadImgBanner" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<div id="idLoadImgBanner">
  <div class="RdUpload">
    <span class="button">Escoge una Foto</span>
    <asp:FileUpload ID="FileUploadImg" runat="server" />
  </div>
  <div class="RdUpload">
    <span class="button">Link Perfil</span>
    <asp:TextBox ID="txtUrlLink" runat="server"></asp:TextBox>
  </div>
  <div class="RdUpload">
    <span class="button">Comentario de la Foto</span>
    <telerik:RadEditor ID="TxtComentarioImage" runat="server" OnClientPasteHtml="OnClientPasteHtml">
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
      <Languages>
        <telerik:SpellCheckerLanguage Code="es-ES" Title="Español" />
      </Languages>
      <Content>
      </Content>
    </telerik:RadEditor>
  </div>
  <div class="UploadBoton">
    <asp:Button ID="btnGrabar" runat="server" CommandName="Update" Text="Grabar" CssClass="btnGrabar" />
    <asp:Button ID="btnCancelar" runat="server" CommandName="Cancel" Text="Cancelar"
      CssClass="btnCancelar" />
  </div>
</div>
