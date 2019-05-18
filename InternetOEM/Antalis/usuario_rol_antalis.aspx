<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="usuario_rol_antalis.aspx.cs" Inherits="ICommunity.Antalis.usuario_rol_antalis" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta charset="utf-8">
  <meta http-equiv="X-UA-Compatible" content="IE=edge">
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <title></title>
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
    <asp:HiddenField ID="CodUsuario" runat="server" />
    <div class="container">
      <div class="row">
        <br />
        <asp:Button ID="btnVolver" runat="server" class="btn btn-default" Text="Volver" OnClick="btnVolver_Click" />
        <asp:Button ID="btnGrabar" runat="server" Text="Grabar" CssClass="btn btn-primary" OnClick="btnGrabar_Click" />
        <hr />
      </div>
      <div class="row">
        <h3>
          <asp:Label ID="lblUsuario" runat="server"></asp:Label></h3>
      </div>
      <div class="row">
        <h3>Roles</h3>
        <div class="form-group">
          <div class="checkbox">
            <asp:CheckBox ID="chk_ingreso_pagos" runat="server" Text="DIGITADOR DE PAGOS" />
          </div>
          <div class="checkbox">
            <asp:CheckBox ID="chk_controller" runat="server" Text="CONTROLLER DE PAGOS" />
          </div>
          <div id="idRowTipoPago" class="row" style="display: none;">
            <div class="col-md-4">
              <span for="cmb_tipo_pago">METODO DE PAGO: </span>
              <div class="checkbox">
                <asp:CheckBox ID="chk_cheque_dia" runat="server" Text="CHEQUE AL DIA" />
              </div>
              <div class="checkbox">
                <asp:CheckBox ID="chk_cheque_fecha" runat="server" Text="CHEQUE A FECHA" />
              </div>
              <div class="checkbox">
                <asp:CheckBox ID="chk_efectivo" runat="server" Text="EFECTIVO" />
              </div>
              <div class="checkbox">
                <asp:CheckBox ID="chk_letra" runat="server" Text="LETRA" />
              </div>
              <div class="checkbox">
                <asp:CheckBox ID="chk_tarjeta" runat="server" Text="TARJETA" />
              </div>
              <div class="checkbox">
                <asp:CheckBox ID="chk_transferencia" runat="server" Text="TRANSFERENCIA" />
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </form>
  <script>
    $("#chk_controller").click(function () {
      var obj = document.getElementById("idRowTipoPago");
      if ($("#chk_controller").is(':checked')) {
        obj.style.display = "block";
        $("#chk_cheque_dia").attr('checked', false);
        $("#chk_cheque_fecha").attr('checked', false);
        $("#chk_efectivo").attr('checked', false);
        $("#chk_letra").attr('checked', false);
        $("#chk_tarjeta").attr('checked', false);
        $("#chk_transferencia").attr('checked', false);
      } else {
        obj.style.display = "none";
      }
    });

    $("#btnGrabar").click(function () {
      if ($("#chk_controller").is(':checked')) {
        if ((!$("#chk_cheque_dia").is(':checked')) && (!$("#chk_cheque_fecha").is(':checked')) && (!$("#chk_efectivo").is(':checked')) && (!$("#chk_letra").is(':checked')) && (!$("#chk_tarjeta").is(':checked')) && (!$("#chk_transferencia").is(':checked'))) {
          alert("Debe seleccionar el metodo de pago a controlar");
          return false;
        }
      }
    });
  </script>
</body>
</html>
