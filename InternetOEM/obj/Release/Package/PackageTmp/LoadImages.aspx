<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoadImages.aspx.cs" Inherits="ICommunity.LoadImages" %>

<%@ Register src="Controls/FileUpload.ascx" tagname="FileUpload" tagprefix="UserCntrFileUpload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <script type="text/javascript">
      function goOnload(){
        var oTop = document.body.scrollTop;
        document.body.scroll = "no";
        document.body.style.overflow = "hidden";
        document.body.scrollTop = oTop;
      }
    </script>
    <link href="style/masterstyle.css" type="text/css" rel="stylesheet" />
</head>
<body onload="goOnload();" id="bodyProfile">
    <form id="form1" runat="server">
    <div>
      <UserCntrFileUpload:FileUpload ID="oUserCntrFileUpload" runat="server" />
    </div>
    </form>
</body>
</html>
