<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Zona.aspx.cs" Inherits="ICommunity.Zona" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register src="Controls/PageObject.ascx" tagname="PageObject" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Zona</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager" runat="server">
    </asp:ScriptManager>
    <asp:HiddenField ID="CodZona" runat="server" />
    <div class="entorno">
      <div class="panel">
        <div>
          <asp:Label ID="lblTitulo" runat="server" Text="Titulo"></asp:Label>
        </div>
        <div>
          <asp:TextBox ID="txtTitulo" runat="server"></asp:TextBox>
        </div>
        <div>
          <asp:Button ID="btnGrabar" runat="server" Text="Grabar" 
            onclick="btnGrabar_Click" />
        </div>
        <div>
          <telerik:RadComboBox ID="rdCmbEstado" runat="server">
            <Items>
              <telerik:RadComboBoxItem Text="Vigente" Value="V" />
              <telerik:RadComboBoxItem Text="No Vigente" Value="N" />
            </Items>
          </telerik:RadComboBox>
        </div>
        <div>
          <asp:CheckBox ID="chk_despliegue" runat="server" Text="Despliegue contenidos" />
        </div>
      </div>
      <div class="panel">
        <div>
          <asp:Label ID="lblDescripcion" runat="server" Text="Descripción"></asp:Label>
        </div>
        <div>
          <telerik:RadEditor ID="rdDescripcion" runat="server">
          </telerik:RadEditor>
        </div>
        <div>
          <uc1:PageObject ID="PageObject" runat="server" />          
        </div>
      </div>
    </div>
    <script language="JavaScript" type="text/javascript">
      function GetIdEditor() {
        return $find("<%= rdDescripcion.ClientID %>");
      }
    </script>
    </form>
</body>
</html>
