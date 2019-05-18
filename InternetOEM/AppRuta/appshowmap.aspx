<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="appshowmap.aspx.cs" Inherits="ICommunity.AppRuta.appshowmap" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta charset="utf-8">
  <meta http-equiv="X-UA-Compatible" content="IE=edge">
  <title></title>
  <style>
    #map {
      width: 100%;
      height: 500px;
    }
  </style>
  <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
  <link rel="stylesheet" href="../css/styleadmin.css">
  <link rel="stylesheet" type="text/css" href="../css/stylesdebtcontrol.css" media="screen" />
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
</head>
<body class="bodyadm">
  <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager" runat="server">
    </asp:ScriptManager>
    <div class="container">
      <div class="row">&nbsp;</div>
      <div class="row">
        <asp:Button ID="btnVolver" runat="server" class="btn btn-default" Text="Volver" OnClick="btnVolver_Click" />
      </div>
      <div class="row">
        <h4>RUTA DIA <asp:Label ID="lblDia" runat="server"></asp:Label> MOTORISTA <asp:Label ID="lblMotorista" runat="server"></asp:Label></h4>
      </div>
      <div class="row">&nbsp;</div>
      <div class="row">
        <telerik:RadGrid ID="rdActividad" runat="server" ShowStatusBar="True" AutoGenerateColumns="false"
          AllowSorting="True" PageSize="5" AllowPaging="True" GridLines="None" OnNeedDataSource="rdActividad_NeedDataSource" Skin="Sitefinity">
          <MasterTableView AutoGenerateColumns="false" ShowHeader="true"
            TableLayout="Fixed" ShowHeadersWhenNoRecords="true" CommandItemDisplay="Top">
            <CommandItemSettings ShowExportToExcelButton="false" ShowRefreshButton="false" ShowAddNewRecordButton="false" />
            <Columns>

              <telerik:GridBoundColumn UniqueName="cliente" DataField="cliente" HeaderText="CLIENTE" AllowSorting="true">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Left" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn UniqueName="deudor" DataField="deudor" HeaderText="DEUDOR" AllowSorting="true">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Left" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn UniqueName="montodeuda" DataField="montodeuda" HeaderText="MONTO DEUDA" AllowSorting="true">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Left" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn UniqueName="montopagado" DataField="montopagado" HeaderText="MONTO PAGADO" AllowSorting="true">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Left" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn UniqueName="direccion" DataField="direccion" HeaderText="DIRECCION" AllowSorting="true">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Left" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn UniqueName="comuna" DataField="comuna" HeaderText="COMUNA" AllowSorting="true">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Left" />
              </telerik:GridBoundColumn>

              <telerik:GridBoundColumn UniqueName="fecha_actividad" HeaderText="FECHA" DataField="fecha_actividad" AllowSorting="true">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Left" />
              </telerik:GridBoundColumn>

            </Columns>
          </MasterTableView>
        </telerik:RadGrid>
      </div>
      <div class="row">&nbsp;</div>
      <div class="row">
        <div id="map"></div>
      </div>
      <div class="row">&nbsp;</div>
    </div>
    <asp:HiddenField ID="hddcodusuario" runat="server" />
    <asp:HiddenField ID="hddfecha" runat="server" />
  </form>
  <script>
    function initMap() {

      var map = new google.maps.Map(document.getElementById('map'), {
        center: origen,
        zoom: 1
      });

      var directionsDisplay = new google.maps.DirectionsRenderer({
        map: map
      });

      // Pass the directions request to the directions service.
      var directionsService = new google.maps.DirectionsService();
      directionsService.route(request, function (response, status) {
        if (status == 'OK') {
          // Display the route on the map.
          directionsDisplay.setDirections(response);
        } else {
          alert(status)
        }
      });

      // Create a marker and set its position.
      //var marker = new google.maps.Marker({
      //  map: map,
      //  position: rancagua,
      //  title: 'Hello World!'
      //});
    }

  </script>
  <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyD09fCWb7NoSRcK7-nLnrTYQvLl5_JS7X4&callback=initMap" async defer></script>
</body>
</html>
