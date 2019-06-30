<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wTestFrame.aspx.cs" Inherits="ICommunity.wTestFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
  <style>
    #testjquery {
      width:800px;
      height:600px;
      border:1px solid #000;
    }
  </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="testjquery">

    </div>
    </form>
  <script>
    $('#testjquery').load('http://sys.debtcontrol.cl/paneldecontrol/default.aspx');
  </script>
</body>
</html>
