<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Recomiendame.aspx.cs" Inherits="ICommunity.Recomiendame" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Paramours - Escorts en Chile, en Santiago, en Vi&ntilde;a del Mar, y en Regiones.</title>
  <meta name="description" content="Paramours, Portal de Chicas Escorts y Acompañantes en Chile, Santiago, Viña del Mar y Regiones." />
  <meta property="og:description" content="Paramours, Portal de Chicas Escorts y Acompañantes en Chile, Santiago, Viña del Mar y Regiones." />
  <meta name="title" content="Paramours - Escorts en Chile, en Santiago, en Vi&ntilde;a del Mar, y en Regiones." />
  <meta name="keywords" content="Paramours - Escorts en Chile, Escorts en Santiago, Escorts en Vi&ntilde;a del Mar, Escorts en Rancagua, Acompa&ntilde;antes en Chile, Acompa&ntilde;antes en Santiago, Acompa&ntilde;antes en Vi&ntilde;a del Mar, Escort en Chile, Escort en Santiago, Escort en Vi&ntilde;a del Mar, Escorts, Escort, Acompa&ntilde;ante, Acompa&ntilde;antes, Scorts, Scort, , Sexo, Chicas, Swinger, Gay, Minas, Peteras, Prepago, Prostitutas, Prostituta, Putas, Puta, Anfitrionas, Anfitriona, Milf, Americana, Anal, Masajes, Masaje" />
  <meta name="google-site-verification" content="GKZCtWRBCo6WDMJDPU-TlmDYmNSmG1xHIj_mAVVpzEA" />
  <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
  <meta http-equiv="Content-Language" content="ES" />
  <meta name="viewport" content="initial-scale=1.0, user-scalable=no" />
  <meta property="og:type" content="website" />
  <meta property="og:title" content="PARAMOURS" />
  <meta property="og:site_name" content="www.paramours.cl" />
  <meta property="og:url" content="http://www.paramours.cl/" />
  <meta property="og:image" content="http://www.paramours.cl/style/images/favicon.png" />
  <link href="style/masterstyle.css" rel="stylesheet" type="text/css" />
  <link href="style/images/favicon.ico" type="image/vnd.microsoft.icon" rel="shortcut icon" />
  <link href="style/images/favicon.png" type="image/png" rel="icon" />
  <link href="http://www.paramours.cl/style/images/favicon.png" rel="image_src" />
  <link rel="stylesheet" type="text/css" href="style/masterstyle.css" />
</head>
<body id="bodyProfile">
  <form id="form1" runat="server">
  <asp:HiddenField ID="CodUsuario" runat="server" />
  <table border="0" cellspacing="0" cellpadding="0" width="100%" align="center">
    <tbody>
      <tr>
        <td id="masterboard" valign="top" align="center">
          <asp:Panel ID="CntPanel" runat="server" DefaultButton="btnAceptar">
            <div id="dvCntTitle">
              <asp:Label ID="lblCntTitle" runat="server" Text=""></asp:Label>
            </div>
            <div id="dvRecomiendame" runat="server">
              <div class="divCntLine">
                <div class="lblData">
                  <asp:Label ID="lblNombre" runat="server"></asp:Label>
                </div>
                <div id="txtNombreCliente" class="txtBox" runat="server">
                  <asp:TextBox ID="txtNombre" runat="server" CssClass="cTxtBox"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="valNombre" runat="server" ControlToValidate="txtNombre"
                    Display="Static" ErrorMessage="*" ValidationGroup="GroupBtnAceptar"></asp:RequiredFieldValidator>
                </div>
              </div>
              <div class="divCntLine">
                <div class="lblData">
                  <asp:Label ID="lblNombreAmigo" runat="server"></asp:Label>
                </div>
                <div class="txtBox">
                  <asp:TextBox ID="txtNombreAmigo" runat="server" CssClass="cTxtBox"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="valNombreAmigo" runat="server" ControlToValidate="txtNombreAmigo"
                    Display="Static" ErrorMessage="*" ValidationGroup="GroupBtnAceptar"></asp:RequiredFieldValidator>
                </div>
              </div>
              <div class="divCntLine">
                <div class="lblData">
                  <asp:Label ID="lblEmail" runat="server"></asp:Label></div>
                <div class="txtBox">
                  <asp:TextBox ID="txtEmail" runat="server" CssClass="cTxtBox"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="valEmail" runat="server" ControlToValidate="txtEmail"
                    Display="Static" ErrorMessage="*" ValidationGroup="GroupBtnAceptar"></asp:RequiredFieldValidator>
                  <asp:CustomValidator ID="valtxtEmailVal" runat="server" ErrorMessage="*" ControlToValidate="txtEmail"
                    Display="Static" OnServerValidate="ServerValidationEmail" ValidationGroup="GroupBtnAceptar"></asp:CustomValidator>
                </div>
              </div>
              <div class="divCntLine">
                <div class="lblData">
                  <asp:Label ID="lblComentario" runat="server"></asp:Label></div>
                <div class="txtBox">
                  <asp:TextBox ID="txtComentario" runat="server" CssClass="cTxtBox" TextMode="MultiLine"
                    Rows="7" Columns="40"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="valComentario" runat="server" ControlToValidate="txtComentario"
                    Display="Static" ErrorMessage="*" ValidationGroup="GroupBtnAceptar"></asp:RequiredFieldValidator>
                </div>
              </div>
              <div id="divCntLine">
                <asp:Button ID="BtnVolver" runat="server" CssClass="btnVolver" OnClick="BtnVolver_Click" />
                <asp:Button ID="BtnAceptar" runat="server" ValidationGroup="GroupBtnAceptar" CssClass="btnAceptar" OnClick="BtnAceptar_Click" />
              </div>
            </div>
            <div id="dvMsgResult" runat="server" visible="false">
              <div class="blMsgResult">
                <asp:Label ID="lblMsgResult" runat="server"></asp:Label>
              </div>
              <div class="blVlvrResult">
                <asp:Button ID="BtnVolverOK" runat="server" CssClass="btnVolver" OnClick="BtnVolver_Click" />
              </div>
            </div>
          </asp:Panel>
        </td>
      </tr>
    </tbody>
  </table>
  </form>
</body>
</html>
