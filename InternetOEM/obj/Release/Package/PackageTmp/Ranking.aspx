<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ranking.aspx.cs" Inherits="ICommunity.Ranking" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title></title>
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
