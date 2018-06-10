<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="viewImageUsrPhone.ascx.cs"
  Inherits="ICommunity.Controls.viewImageUsrPhone" %>
<link href="Resources/Phone/styles.css" type="text/css" rel="stylesheet" />
<link href="Resources/Phone/photoswipe.css" type="text/css" rel="stylesheet" />
<script type="text/javascript" src="Resources/Phone/js/lib/klass.min.js"></script>
<script type="text/javascript" src="Resources/Phone/js/code.photoswipe-3.0.5.min.js"></script>
<script type="text/javascript">
		(function(window, PhotoSwipe){
			document.addEventListener('DOMContentLoaded', function(){
				var
					options = {},
					instance = PhotoSwipe.attach( window.document.querySelectorAll('#Gallery a'), options );
			
			}, false);
		}(window, window.Code.PhotoSwipe));
</script>
<div id="GaleryPhone" runat="server">
</div>