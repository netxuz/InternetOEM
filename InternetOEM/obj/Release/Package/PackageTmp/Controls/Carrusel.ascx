<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Carrusel.ascx.cs" Inherits="ICommunity.Controls.Carrusel" %>

<script type="text/javascript" language="javascript" src="Resources/Carrusel/jquery-1.8.2.min.js"></script>

<script type="text/javascript" language="javascript" src="Resources/Carrusel/jquery.carouFredSel-6.2.1-packed.js"></script>

<style type="text/css" media="all">
  .image_carousel
  {
    padding: 5px 0px 5px 0px;
  }
  .ctl08_foo1
  {
  	/*width: 735px;*/
  }
  .image_carousel img
  {
    border: 1px solid #ccc;
    background-color: white;
    padding: 0px;
    margin: 0px;
    display: block;
    float: left;
    width: 180px;
    height: 180px;
  }
  .image_carousel a
  {
    display: block;
    float: left;
  }
  .clearfix
  {
    float: none;
    clear: both;
  }
</style>
<div class="image_carousel">
  <div id="foo1" runat="server">
  </div>
  <div class="clearfix">
  </div>
</div>
