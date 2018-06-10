<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BannerSlider.ascx.cs"
  Inherits="ICommunity.Controls.BannerSlider" %>
<link rel="stylesheet" href="Resources/slider/orbit-1.2.3.css" />
<link rel="stylesheet" href="Resources/slider/demo-style.css" />

<script type="text/javascript" src="Resources/jquery-1.5.1.min.js"></script>

<script type="text/javascript" src="Resources/slider/jquery.orbit-1.2.3.min.js"></script>

<div id="scfnct" runat="server">
<script type="text/javascript">
			$(window).load(function() {
				$('#ctl07_featured').orbit();
			});
</script>
</div>
<div id="featured" runat="server">
</div>
