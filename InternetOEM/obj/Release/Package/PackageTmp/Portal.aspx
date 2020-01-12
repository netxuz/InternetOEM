<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Portal.aspx.cs" Inherits="ICommunity.Portal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
  <meta http-equiv="x-ua-compatible" content="ie=edge">
  <!-- Font Awesome -->
  <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.8.2/css/all.css">
  <!-- Bootstrap core CSS -->
  <link href="dashboard/css/bootstrap.min.css" rel="stylesheet">
  <!-- Material Design Bootstrap -->
  <link href="dashboard/css/mdb.min.css" rel="stylesheet">
  <!-- Your custom styles (optional) -->
  <link href="dashboard/css/style.css" rel="stylesheet">
  <title></title>
  <style>
    .abs-center {
      display: flex;
      align-items: center;
      justify-content: center;
      min-height: 100vh;
      width: 100%;
    }

    .rowportal {
      width: 100%;
    }

    .logohome {
      font-size: 50px!important;
    }

    .btnPortal {
      font-size: 14px!important;
      padding: 50px 40px 50px 40px!important;
    }

    .bodyadmcss {
      background-image: url(images/fondo6.jpg);
      background-repeat: no-repeat;
      background-position: center center;
      background-attachment: fixed;
      background-size: cover;
    }
  </style>
</head>
<body class="bodyadmcss">
  <form id="form1" runat="server">
    <div class="container">
      <div class="abs-center">
        <div class="row rowportal">
          <div class="col-md-4 text-center">
            <button id="btnPortal1" type="button" class="btn btn-warning btnPortal"><b><i class="fas fa-home logohome"></i><br />Accede a <br />Portal Antiguo</b></button>
          </div>
          <div class="col-md-4"></div>
          <div class="col-md-4 text-center">
            <button id="btnPortal2" type="button" class="btn btn-success btnPortal"><b><i class="fas fa-sign-in-alt logohome"></i><br />Accede a <br />Portal Nuevo</b></button>
          </div>
        </div>
      </div>
    </div>
  </form>
  <!-- SCRIPTS -->
  <!-- JQuery -->
  <script type="text/javascript" src="dashboard/js/jquery-3.4.1.min.js"></script>
  <!-- Bootstrap tooltips -->
  <script type="text/javascript" src="dashboard/js/popper.min.js"></script>
  <!-- Bootstrap core JavaScript -->
  <script type="text/javascript" src="dashboard/js/bootstrap.min.js"></script>
  <!-- MDB core JavaScript -->
  <script type="text/javascript" src="dashboard/js/mdb.min.js"></script>
  <script>
    $("#btnPortal1").click(function () {
      document.location = 'reporting/default.aspx';
    });
    $("#btnPortal2").click(function () {
      document.location = 'dashboard/default.aspx';
    });
  </script>
</body>
</html>
