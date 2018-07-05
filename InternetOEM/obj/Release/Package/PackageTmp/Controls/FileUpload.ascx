<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FileUpload.ascx.cs"
  Inherits="ICommunity.Controls.FileUpload" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<style type="text/css">
  /*div.RadUpload .ruFakeInput
  {
    visibility: hidden;
    width: 0px;
  }
  div.RadUpload .ruRemove
  {
    visibility: hidden;
    height:0px;
  }
  div.RadUpload .ruUploadProgress
  {
  	height:0px;
  	visibility: hidden;
  }
  div.RadUpload .ruUploadSuccess
  {
  	height:0px;
  	visibility: hidden;
  }
  div.RadUpload .ruRemove
  {
    visibility: hidden;
    height:0px;
  }
  div.RadUpload .ruFileWrap
  {
  	
  }
  div.RadUpload .ruButton
  {
  	background : #000;
  }*/
</style>
<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
  <Scripts>
    <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
    <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
    <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
  </Scripts>
</telerik:RadScriptManager>
<div class="MenuFotos"><asp:Label ID="lblFotos" runat="server" CssClass="lblFotos"></asp:Label></div>
<div class="DesFotos">
  <div class="TitFotos">Sugerencia de carga</div>
  <div class="txtBody">Puedes seleccionar varias fotos en el cuadro de diálogo si mantienes presionada la tecla "Ctrl" a la vez que haces clic en las fotos.</div>
</div>
<div class="CntrFotos">
<telerik:RadAsyncUpload runat="server" Width="100px" Height="100px" ID="RadAsyncUpload1" EnableFileInputSkinning="true"
  OnClientFileUploaded="fileUploaded" MultipleFileSelection="Automatic" OnClientValidationFailed="FileValidation" AllowedFileExtensions="jpeg,jpg,gif,png,bmp">
</telerik:RadAsyncUpload>
<div class="ErrorHolder"></div>
<span class="info"></span>
</div>
<script type="text/javascript">
    //<![CDATA[
        function FileValidation(sender, eventArgs){
           $(".ErrorHolder").append("<p>Validation failed for '" + eventArgs.get_fileName() + "'.</p>").fadeIn("slow");
        }
        
        function fileUploaded(sender, args) {
            //var id = args.get_fileInfo().ImageID;
           
            //$(".info").append(String.format("<strong>{0}</strong> succesfully insterted. Record ID - {1}.</p>", args.get_fileName(), id)).fadeIn("slow");
        }

     //]]>
</script>

