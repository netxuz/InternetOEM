<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewTest.aspx.cs" Inherits="ICommunity.ViewTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title></title>
  <link rel="stylesheet" type="text/css" href="style/masterstyle.css" />
  <link rel="stylesheet" href="Resources/Galleriffic/css/basic.css" type="text/css" />
  <link rel="stylesheet" href="Resources/Galleriffic/css/galleriffic-1.css" type="text/css" />

  <script type="text/javascript" src="Resources/Galleriffic/js/jquery-1.3.2.js"></script>

  <script type="text/javascript" src="Resources/Galleriffic/js/jquery.galleriffic.js"></script>

</head>
<body id="bodyProfile">
  <form id="form1" runat="server">
  <div>
    <div id="gallery" class="content">
      <div id="controls" class="controls">
      </div>
      <div class="slideshow-container">
        <div id="loading" class="loader">
        </div>
        <div id="slideshow" class="slideshow">
        </div>
      </div>
    </div>
    <div id="thumbs" class="navigation">
      <ul id="imgserv" class="thumbs noscript" runat="server">
      </ul>
    </div>
    <div style="clear: both;">
    </div>
  </div>

  <script type="text/javascript">
			// We only want these styles applied when javascript is enabled
			$('div.navigation').css({'width' : '300px', 'float' : 'left'});
			$('div.content').css('display', 'block');

			$(document).ready(function() {				
				// Initialize Minimal Galleriffic Gallery
				$('#thumbs').galleriffic({
					imageContainerSel:      '#slideshow',
					controlsContainerSel:   '#controls'
				});
			});
  </script>

  </form>
</body>
</html>
