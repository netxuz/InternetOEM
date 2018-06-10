<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Escort.aspx.cs" Inherits="ICommunity.viewProfile" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="Controls/RelUsuarioControl.ascx" TagName="RelUsuarioControl" TagPrefix="oRelUsuarioControl" %>
<%@ Register Src="Controls/TwittControl.ascx" TagName="TwittControl" TagPrefix="uc1" %>
<%@ Register Src="Controls/ImgCertificado.ascx" TagName="ImgCertificado" TagPrefix="uc2" %>
<%@ Register src="Controls/MensajesUsuarios.ascx" tagname="MensajesUsuarios" tagprefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title></title>
  <meta name="keywords" content="Escort, Escorts, Escort en Chile, Escort en Santiago, Acompañante, Prostitutas, Prostituta, Putas, Puta" />
  <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
  <meta http-equiv="Content-Language" content="es" />
  <meta name="viewport" content="initial-scale=1.0, user-scalable=no" />
  <meta property="og:type" content="website" />
  <meta property="og:title" content="Escorts Club" />
  <meta property="og:site_name" content="www.escortsclub.cl" />
  <meta property="og:url" content="http://www.escortsclub.cl/" />
  <meta property="og:image" content="http://www.escortsclub.cl/style/images/favicon.png" />
  <link href="style/images/favicon.ico" type="image/vnd.microsoft.icon" rel="shortcut icon" />
  <link href="style/images/favicon.png" type="image/png" rel="icon" />
  <link href="http://www.escortsclub.cl/style/images/favicon.png" rel="image_src" />
  <link rel="stylesheet" type="text/css" href="style/masterstyle.css" />
  <script type="text/javascript" src="Resources/jquery-1.5.1.min.js"></script>
  <script type="text/javascript" src="Resources/jQueryGallery/jQueryGallery.js"></script>

</head>
<body id="bodyProfile">
  <form id="frmProfile" runat="server">
  <asp:ScriptManager ID="ScriptManager1" runat="server">
  </asp:ScriptManager>
  <table border="0" cellpadding="0" cellspacing="0" width="100%">
    <tr>
      <td valign="top" colspan="3">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
          <tr>
            <td id="MnSupProfile" valign="top">
              <div style="float: left; display: block;">
                <telerik:RadRating ID="RdRankingUSer" ItemCount="7" Enabled="false" runat="server">
                </telerik:RadRating>
              </div>
            </td>
            <td id="BtnProfile">
              <asp:HyperLink ID="HyperLink1" runat="server" Visible="false">Evalua el servicio</asp:HyperLink>
              <asp:HyperLink ID="HyperLink2" runat="server">Historial de Ranking</asp:HyperLink>
              <asp:HyperLink ID="HyperLink3" runat="server">Recomiéndame</asp:HyperLink>
              <uc1:TwittControl ID="TwittControl" runat="server" />
            </td>
          </tr>
        </table>
      </td>
    </tr>
    <tr>
      <td valign="top" colspan="3">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
          <tr>
            <td id="MenuProfile" colspan="2" valign="top">
              <div class="lblNomVieProfile">
                <asp:Label ID="lblNombre" CssClass="titNombre" runat="server"></asp:Label>
                <div class="msgclient">
                  No olvides mencionar que me viste en Escorts Club.</div>
              </div>
            </td>
          </tr>
        </table>
      </td>
    </tr>
    <tr>
      <td valign="top" align="center">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
          <tr>
            <td valign="top">
              <img class="btnImg" id="startAjaxPrev" src="Resources/jQueryGallery/ad_prev.png" />
            </td>
            <td valign="top">
              <div id="loading">
              </div>
              <div id="jGallery" runat="server"></div>
            </td>
            <td valign="top">
              <img class="btnImg" id="startAjaxNext" src="Resources/jQueryGallery/ad_next.png" />
            </td>
          </tr>
        </table>
      </td>
      <td valign="top" class="colcenter">
        <div id="datosbasicos" runat="server">
        </div>
      </td>
    </tr>
  </table>
  </form>
</body>
</html>
