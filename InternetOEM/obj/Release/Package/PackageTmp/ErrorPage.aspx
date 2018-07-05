<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="ICommunity.ErrorPage" %>

<%@ Register Src="Controls/NavegacionPrincipal.ascx" TagName="NavegacionPrincipal"
  TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title></title>
</head>
<body>
  <form id="form1" runat="server">
  <asp:ScriptManager ID="ScriptManager1" runat="server">
  </asp:ScriptManager>
  <div>
    <table border="0" cellspacing="0" cellpadding="0" width="995px" align="center">
      <tbody>
        <tr>
          <td style="width: 100%;" id="Menu">
            <uc1:NavegacionPrincipal ID="NavegacionPrincipal" runat="server" />
          </td>
        </tr>
        <tr>
          <td id="masterboard" valign="top" align="center">
            <div class="ImgError">
              <img src="style/images/MsgAlerta.png" border="0" />
            </div>
            <div class="msgError">
              <span>Estimado Cliente(a),<br />
                en estos momentos no es posible cursar el servicio solicitado, por favor intentelo
                m&aacute;s tarde.<br />
                <br />
                Si el problema a&uacute;n persiste, comun&iacute;quese con nosotros a trav&eacute;s
                de las opciones Cont&aacute;ctenos del sitio.</span>
            </div>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
  </form>
</body>
</html>
