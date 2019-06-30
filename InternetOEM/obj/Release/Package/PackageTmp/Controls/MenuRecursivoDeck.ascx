<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuRecursivoDeck.ascx.cs" Inherits="ICommunity.Controls.MenuRecursivoDeck" %>



<header class="cd-main-header">
  <div class="container">
    <div id="gp-search" class="gp-search">
    </div>
    <ul class="cd-header-buttons">
      <li><a class="cd-nav-trigger" href="#cd-primary-nav"><span></span></a></li>
    </ul>
  </div>
</header>
<main class="cd-main-content main">
</main>

<nav class="cd-nav">
  <ul runat="server" id="cdprimarynav" class="cd-primary-nav is-fixed closed">
    <li class="mobile-view closeMobileMenu"><span id="closeMobileMenu__trigger">Close Menu</span></li>



  </ul>
</nav>



<script type="text/javascript" src="http://www.genpact.com/craft/assets/javascript/vendor/jquery.min.js"></script>
<script type="text/javascript" src="http://www.genpact.com/craft/assets/javascript/vendor/jquery.mobile.custom.min.js"></script>
<script type="text/javascript" src="http://www.genpact.com/craft/assets/javascript/vendor/bootstrap.min.js"></script>
<script type="text/javascript" src="http://www.genpact.com/craft/assets/javascript/vendor/bootstrap-select.min.js"></script>
<script type="text/javascript" src="http://www.genpact.com/craft/assets/javascript/main.js"></script>
<asp:HiddenField ID="hddCodNodo" runat="server" />