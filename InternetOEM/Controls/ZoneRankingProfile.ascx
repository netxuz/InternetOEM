<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ZoneRankingProfile.ascx.cs"
  Inherits="ICommunity.Controls.ZoneRankingProfile" %>
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
		  $("a#ctl03_HyperLink1").fancybox({
				'titlePosition'		: 'outside',
				'overlayColor'		: '#000',
				'overlayOpacity'	: 0.9
			});
			$("a#ctl03_HyperLink2").fancybox({
				'titlePosition'		: 'outside',
				'overlayColor'		: '#000',
				'overlayOpacity'	: 0.9
			});
			$("a#ctl03_HyperLink3").fancybox({
				'titlePosition'		: 'outside',
				'overlayColor'		: '#000',
				'overlayOpacity'	: 0.9
			});
    });
  </script>
  
<div id="ZoneRankingProfile">
<table border="0" cellpadding="0" cellspacing="0" width="100%">
  <tr>
    <td id="MnSupProfile" valign="top">
      <div style="float: left; display: block;">
        <telerik:RadRating ID="RdRankingUSer" ItemCount="7" Enabled="false" runat="server">
        </telerik:RadRating>
      </div>
    </td>
    <td id="BtnProfile">
      <asp:HyperLink ID="HyperLink1" runat="server" Visible="false">Evalua el servicio</asp:HyperLink>
      <asp:HyperLink ID="HyperLink2" runat="server">Historial de Ranking</asp:HyperLink>
      <asp:HyperLink ID="HyperLink3" runat="server">Recomiéndame</asp:HyperLink>
    </td>
  </tr>
</table>
</div>