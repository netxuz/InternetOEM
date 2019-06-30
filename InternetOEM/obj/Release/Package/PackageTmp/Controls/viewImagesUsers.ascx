<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="viewImagesUsers.ascx.cs"
  Inherits="ICommunity.Controls.viewImagesUsers" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<script type="text/javascript" src="Resources/fancybox/jquery.min.js"></script>
<script>
		!window.jQuery && document.write('<script src="Resources/fancybox/jquery-1.4.3.min.js"><\/script>');
</script>
<script type="text/javascript" src="Resources/fancybox/jquery.mousewheel-3.0.4.pack.js"></script>
<script type="text/javascript" src="Resources/fancybox/jquery.fancybox-1.3.4.js"></script>
<link rel="stylesheet" type="text/css" href="Resources/fancybox/jquery.fancybox-1.3.4.css" media="screen" />
<link rel="stylesheet" href="Resources/fancybox/style.css" />

<script type="text/javascript">
		$(document).ready(function() {
		  $("a[rel=ImgFotoUser]").fancybox({
				'transitionIn'		: 'elastic',
				'transitionOut'		: 'elastic',
				'titlePosition' 	: 'outside',
				'titleFormat'		: function(title, currentArray, currentIndex, currentOpts) {
					return '<span id="fancybox-title-over">Image ' + (currentIndex + 1) + ' / ' + currentArray.length + (title.length ? ' &nbsp; ' + title : '') + '</span>';
				}
			});
		});
</script>

<div class="moduloimagenusuario">
  <div id="objwin" runat="server"></div>
  <asp:Label ID="lblMisImagenes" runat="server" Text="" CssClass="lblMisFotos"></asp:Label>
  <asp:Button ID="btnUpload" runat="server" OnClientClick="LoadImage(); return false;"
    CssClass="btnUploadImages" />
  <div id="GaleriaMisFotos">
    <telerik:RadListView ID="rdUserImage" runat="server" ItemPlaceholderID="idPHolder"
      OnNeedDataSource="rdUserImage_NeedDataSource" OnItemCommand="rdUserImage_ItemCommand"
      OnItemDataBound="rdUserImage_ItemDataBound" DataKeyNames="cod_archivo">
      <LayoutTemplate>
        <div class="RadListView RadListViewFloated RLVCarList">
          <div class="rlvFloated rlvNoScroll">
            <asp:PlaceHolder ID="idPHolder" runat="server"></asp:PlaceHolder>
          </div>
        </div>
      </LayoutTemplate>
      <ItemTemplate>
        <div class="rdlImgUsr">
          <div class="rlvI">
            <div class="rlvImg">
              <asp:HyperLink ID="idLnkButton" runat="server">
                <telerik:RadBinaryImage ID="oBinaryImage" runat="server" />
              </asp:HyperLink>
            </div>
          </div>
          <div id="botonlistview">
            <asp:Button ID="btnImgPrincipal" runat="server" ToolTip="Seleccionar esta foto como foto de perfil"
              Text="Seleccionar esta foto como foto de perfil" CssClass="btnImgPerfil" CommandName="IMGPERFIL" />
            <asp:Button ID="btnEliminar" runat="server" ToolTip="Eliminar esta foto" Text="Eliminar esta foto"
              CssClass="btnDelImage" CommandName="IMGDELETE" />
          </div>
        </div>
      </ItemTemplate>
    </telerik:RadListView>
  </div>
</div>

<script type="text/javascript">
function LoadImage(){
  var cUrl = "LoadImages.aspx";
  var manager = GetRadWindowManager();
  var oWnd = manager.getWindowByName("oRdWindow");
  oWnd.setUrl(cUrl);
  oWnd.setSize(600,400);
  oWnd.set_modal(false);
  oWnd.center();
  oWnd.show();
  oWnd.add_close(OnClientClose);

}
function OnClientClose(sender, eventArgs)
{
 document.forms[0].submit();
}

</script>

