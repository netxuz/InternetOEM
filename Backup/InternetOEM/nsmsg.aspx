<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="nsmsg.aspx.cs" Inherits="ICommunity.nsmsg" %>

<%@ Register Src="Controls/NavegacionPrincipal.ascx" TagName="NavegacionPrincipal"
  TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>Paramours Portal Escorts y Acompañantes de Chile, Escorts en Santiago, Viña
    del Mar y Regiones.</title>
  <meta name="description" content="Paramours portal de Escorts y Acompañantes en Chile. Directorio de servicios Escorts en Santiago y Regiones de Chile." />
  <meta property="og:description" content="Paramours portal de Escorts y Acompañantes en Chile. Directorio de servicios Escorts en Santiago y Regiones de Chile." />
  <meta name="title" content="Paramours Portal Escorts y Acompañantes de Chile, Escorts en Santiago, Viña del Mar y Regiones." />
  <meta name="keywords" content="Paramours Escorts Escort Chile santiago providencia prostitutas acompañantes anfitrionas universitarias agencias despedidas" />
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
            <div id="bnLogotype">
              <img alt="" src="style/images/bnlogo.png" style="border-width: 0px; border-style: solid;" /></div>
            <uc1:NavegacionPrincipal ID="NavegacionPrincipal" runat="server" />
          </td>
        </tr>
        <tr>
          <td id="masterboard" runat="server" valign="top" align="center">
            <asp:Panel ID="panel" runat="server" DefaultButton="btnAceptar">
              <table id="TBLLogin" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                  <td valign="top">
                    <div class="lblTitle">
                      <asp:Label ID="lblTitle" runat="server" Text="Para eliminar la suscripción, ingrese su login y clave para verificar su indentidad"
                        Visible="true"></asp:Label>
                    </div>
                  </td>
                </tr>
                <tr>
                  <td valign="top" align="center">
                    <div id="idCntrUsrLogin" runat="server">
                      <div id="context-login">
                        <div class="objLgn">
                          <div class="lblLogin">
                            <asp:Label ID="lblLogin" runat="server" Text="" Visible="true"></asp:Label></div>
                          <asp:TextBox ID="txtLogin" runat="server" CssClass="inptexto"></asp:TextBox>
                        </div>
                        <div class="objLgn">
                          <div class="lblLogin">
                            <asp:Label ID="lblPassword" runat="server" Text="" Visible="true"></asp:Label></div>
                          <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="inptexto"></asp:TextBox>
                        </div>
                        <div class="objbtnLogin">
                          <asp:Button ID="btnAceptar" runat="server" Text="" CssClass="btnAceptar" OnClick="btnAceptar_Click" />
                        </div>
                      </div>
                    </div>
                  </td>
                </tr>
              </table>
            </asp:Panel>
          </td>
        </tr>
        <tr>
          <td>
            <div id="idPie">
              <table id="tblFoot" border="0" cellpadding="0" cellspacing="0" width="100%">
                <tbody>
                  <tr>
                    <td>
                      <table border="0" cellpadding="0" cellspacing="0" width="95%">
                        <tbody>
                          <tr>
                            <td align="left">
                              <div class="textomenupie">
                                &copy; 2013 Paramours</div>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </td>
                  </tr>
                  <tr>
                    <td align="center">
                      <div class="textopie">
                        Sitio s&oacute;lo para mayores de 18 a&ntilde;os, en publicaci&oacute;n y usuarios
                        visitantes. Derechos exclusivos reservados por Paramours.
                      </div>
                    </td>
                  </tr>
                </tbody>
              </table>
            </div>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
  </form>
</body>
</html>
