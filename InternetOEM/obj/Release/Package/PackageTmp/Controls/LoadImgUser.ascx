<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoadImgUser.ascx.cs"
  Inherits="ICommunity.LoadImgUser" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<div id="idLoadImgUser">
  <asp:LinkButton ID="LnkBtton" OnClientClick="goComenta('idBloqueloadimg'); return false;"
    runat="server">Carga tu imagen</asp:LinkButton>
  <div id="idBloqueloadimg" style="display: none;">
    <div class="RdUpload">
      <asp:FileUpload ID="FileUploadImg"  runat="server" />
      <span class="button">Escoge una Foto</span>
    </div>
    <asp:TextBox ID="dispName" runat="server"></asp:TextBox>
    <div class="UploadBoton">
      <asp:Button ID="btnAceptar" runat="server" CssClass="btnAceptarMiPhoto" OnClick="btnAceptar_Click" Text="ACEPTAR" /></div>
    <div>
      <asp:RegularExpressionValidator ID="FileUpLoadValidator" runat="server" ErrorMessage="Solo selecciona imagenes."
        ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.jpg|.JPG|.gif|.GIF|.png|.PNG)$"
        ControlToValidate="FileUploadImg">
      </asp:RegularExpressionValidator></div>
  </div>
</div>
<asp:HiddenField ID="hddCodUsrImgFileProfile" runat="server" />

<script type="text/javascript"> 
  function getStats(fObject,fName){
    var oControl = document.getElementById(fObject);
    fullName = fName;
    shortName = fullName.match(/[^\/\\]+$/);
    /*fName = shortName[0];
    oControl.style.width = fName.length + "0px";*/
    oControl.value = shortName;
  }
  function goComenta(idControl){
    var oControl = document.getElementById(idControl);
    if (oControl.style.display != 'block')
      oControl.style.display = 'block';
    else
      oControl.style.display = 'none';
  };
</script>

