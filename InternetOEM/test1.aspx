<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test1.aspx.cs" Inherits="ICommunity.test1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title></title>
  <script src="Resources/jquery-1.3.2.js" type="text/javascript"></script>
  <script src="Resources/jquery.MultiFile.js" type="text/javascript"></script>

</head>
<body>
  <form id="form1" runat="server">
  <div class="contentsite">
    <div class="top">
      Loggin
    </div>
    <div class="base">
      <div class="content">
       <asp:FileUpload ID="FileUpload1" runat="server" class="multi" />
        <br />
        <asp:Button ID="btnUpload" runat="server" Text="Upload All"
            onclick="btnUpload_Click" />
      </div>
      <div class="menuright">
      </div>
    </div>
    <div class="pie">
    </div>
  </div>
  </form>
</body>
</html>
