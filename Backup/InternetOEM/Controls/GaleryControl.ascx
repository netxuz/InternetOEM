<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GaleryControl.ascx.cs"
  Inherits="ICommunity.Controls.GaleryControl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!---
<script type="text/javascript" src="Resources/fancybox/jquery.min.js"></script>
<script>
		window.jQuery && document.write('<script src="Resources/fancybox/jquery-1.4.3.min.js"><\/script>');
</script>
-->
<script type="text/javascript" src="../Resources/fancybox/jquery.mousewheel-3.0.4.pack.js"></script>
<script type="text/javascript" src="../Resources/fancybox/jquery.fancybox-1.3.4.js"></script>

<link rel="stylesheet" type="text/css" href="../Resources/fancybox/jquery.fancybox-1.3.4.css" media="screen" />
<link rel="stylesheet" href="../Resources/fancybox/style.css" />
<div class="mdlgaleria">
  <telerik:RadListView ID="rdGaleryCntrl" runat="server" ItemPlaceholderID="idPHldGalery"
    OnNeedDataSource="rdGaleryCntrl_NeedDataSource" OnItemDataBound="rdGaleryCntrl_ItemDataBound"
    OnPreRender="rdGaleryCntrl_PreRender">
    <LayoutTemplate>
      <div class="RadListView RadListViewFloated RLVCarList">
        <div class="rlvFloated rlvNoScroll">
          <asp:PlaceHolder ID="idPHldGalery" runat="server"></asp:PlaceHolder>
        </div>
      </div>
    </LayoutTemplate>
    <ItemTemplate>
      <div class="rlvIBlock">
        <div class="rdlvI">
          <asp:HyperLink ID="idLnkButton" runat="server">
            <telerik:RadBinaryImage ID="idImgUser" runat="server" />
            <h2 class="rdlblNombre">
              <asp:Label ID="idLblNomUser" runat="server" Text=''></asp:Label>
            </h2>
          </asp:HyperLink>
        </div>
        <telerik:RadToolTip ID="rdToolTip" runat="server" TargetControlID="idLnkButton" RenderInPageRoot="true"
          Width="150" Height="120" IgnoreAltAttribute="true" Position="BottomRight" RelativeTo="Mouse"
          MouseTrailing="true" OffsetX="10" ShowDelay="1" HideDelay="1" EnableEmbeddedSkins="false"
          Skin="MyCustomSkin">
          <div id="mdlDataUser" class="tTipMsg" runat="server">
          </div>
        </telerik:RadToolTip>
      </div>
    </ItemTemplate>
  </telerik:RadListView>
</div>

<script type="text/javascript">
function ViewProfile(CodUsuario){
  cUrl = "Escort.aspx?CodUsuario=" + CodUsuario;
  var manager = GetRadWindowManager();
  var oWnd = radopen(cUrl, "oRdWindow"); //manager.getWindowByName("oRdWindow");
  oWnd.set_modal(true);
  //oWnd.setUrl(cUrl);
  //oWnd.set_modal(true);
  //oWnd.center();
  //oWnd.show();

} 
</script>

