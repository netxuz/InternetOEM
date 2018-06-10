<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ranking.aspx.cs" Inherits="ICommunity.Ranking" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
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
  <telerik:RadScriptManager ID="RadScriptManager1" runat="server" />
  <asp:HiddenField ID="CodUsuario" runat="server" />
  <asp:HiddenField ID="H" runat="server" />
  <div id="objRanking" runat="server"></div>
  <div id="objMessageInfo" visible="false" runat="server">
    <div class="mdlRankig">
      <asp:Label ID="lblTtRnk" runat="server"></asp:Label>
    </div>
    <div class="mdlMsgNotaRankig">
      <asp:Label ID="lblMsgNotaOk" runat="server"></asp:Label>
    </div>
    <div class="mdlDesRankig">
      <asp:Label ID="lblMsgOk" runat="server"></asp:Label>
    </div>
    <div class="mdlbotonera">
      <asp:Button ID="btnVolver" runat="server" CssClass="btnEvalAceptar" OnClick="oBtnVolver_Click" />
    </div>
  </div>
  </form>

  <script type="text/javascript">

/******************************************
* Scrollable content script II- © Dynamic Drive (www.dynamicdrive.com)
* Visit http://www.dynamicdrive.com/ for full source code
* This notice must stay intact for use
******************************************/

// modified 17-October-2011


function move(id,spd){
 var obj=document.getElementById(id),max=-obj.offsetHeight+obj.parentNode.offsetHeight,top=parseInt(obj.style.top);
 if ((spd>0&&top<=0)||(spd<0&&top>=max)){
  obj.style.top=top+spd+"px";
  move.to=setTimeout(function(){ move(id,spd); },20);
 }
 else {
  obj.style.top=(spd>0?0:max)+"px";
 }
}

  </script>

</body>
</html>
