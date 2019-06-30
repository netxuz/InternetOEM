<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GalleryImgContent.ascx.cs"
  Inherits="ICommunity.Controls.GalleryImgContent" %>
<link rel="stylesheet" type="text/css" href="Resources/jquery.ad-gallery.css">
<script type="text/javascript" src="Resources/jquery.min.js"></script>
<script type="text/javascript" src="Resources/jquery.ad-gallery.js"></script>

<script type="text/javascript">
  $(function() {
    var galleries = $('.ad-gallery').adGallery();
    $('#switch-effect').change(
      function() {
        galleries[0].settings.effect = $(this).val();
        return false;
      }
    );
    $('#toggle-slideshow').click(
      function() {
        galleries[0].slideshow.toggle();
        return false;
      }
    );
    $('#toggle-description').click(
      function() {
        if(!galleries[0].settings.description_wrapper) {
          galleries[0].settings.description_wrapper = $('#descriptions');
        } else {
          galleries[0].settings.description_wrapper = false;
        }
        return false;
      }
    );
  });
  </script>

<style type="text/css">
  *
  {
    font-family: "Lucida Grande" , "Lucida Sans Unicode" , "Lucida Sans" , Verdana, Arial, sans-serif;
    color: #333;
    line-height: 140%;
  }
  select, input, textarea
  {
    font-size: 1em;
  }
  body
  {
    padding: 30px;
    font-size: 70%;
    width: 800px;
  }
  h2
  {
    margin-top: 1.2em;
    margin-bottom: 0;
    padding: 0;
    border-bottom: 1px dotted #dedede;
  }
  h3
  {
    margin-top: 1.2em;
    margin-bottom: 0;
    padding: 0;
  }
  .example
  {
    border: 1px solid #CCC;
    background: #f2f2f2;
    padding: 10px;
  }
  ul
  {
    list-style-image: url(Resources/list-style.gif);
  }
  pre
  {
    font-family: "Lucida Console" , "Courier New" , Verdana;
    border: 1px solid #CCC;
    background: #f2f2f2;
    padding: 10px;
  }
  code
  {
    font-family: "Lucida Console" , "Courier New" , Verdana;
    margin: 0;
    padding: 0;
  }
  #gallery
  {
    padding: 30px;
    background: #e1eef5;
  }
  #descriptions
  {
    position: relative;
    height: 50px;
    background: #EEE;
    margin-top: 10px;
    width: 640px;
    padding: 10px;
    overflow: hidden;
  }
  #descriptions .ad-image-description
  {
    position: absolute;
  }
  #descriptions .ad-image-description .ad-description-title
  {
    display: block;
  }
</style>
<div id="gallery" class="ad-gallery">
  <div class="ad-image-wrapper">
  </div>
  <div class="ad-controls">
  </div>
  <div class="ad-nav">
    <div class="ad-thumbs">
      <ul class="ad-thumb-list">
        <div id="GalleryContent" runat="server">
        </div>
      </ul>
    </div>
  </div>
</div>

