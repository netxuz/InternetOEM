<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ETarget.aspx.cs" Inherits="ICommunity.ETarget" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title></title>
</head>
<body>
  <form id="form1" runat="server">
  <asp:ScriptManager ID="ScriptManager" runat="server">
  </asp:ScriptManager>
  <telerik:RadWindowManager ID="RadWindowManager" runat="server">
  </telerik:RadWindowManager>
  <div id="MasterPage">
    <div class="BloqueMenu">
      <div class="Botonera">
        <asp:Button ID="btnSave" runat="server" Text="Enviar" OnClick="btnSave_Click" ValidationGroup="ConfigEmail" />
      </div>
      <div class="lblTitulo">
        <asp:Label ID="lblTitulo" runat="server" Text="Envio de Mensajes a Usuarios"></asp:Label>
      </div>
    </div>
    <div class="modulo">
      <div class="modulo">
        <div class="lblAsunto">
          <asp:Label ID="lblAsunto" runat="server" Text="Asunto"></asp:Label>
        </div>
        <asp:TextBox ID="txtAsunto" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="valAsunto" runat="server" ControlToValidate="txtAsunto"
          Display="Static" ErrorMessage="*" ValidationGroup="ConfigEmail"></asp:RequiredFieldValidator>
      </div>
      <div class="modulo">
        <div class="lblCuerpo">
          <asp:Label ID="lblCuerpo" runat="server" Text="Cuerpo"></asp:Label>
        </div>
        <telerik:RadEditor ID="rdCuerpoEmail" runat="server" OnClientPasteHtml="OnClientPasteHtml">
          <Tools>
            <telerik:EditorToolGroup>
              <telerik:EditorTool Name="Bold" />
              <telerik:EditorTool Name="Italic" />
              <telerik:EditorTool Name="Underline" />
              <telerik:EditorSeparator />
              <telerik:EditorTool Name="ForeColor" />
              <telerik:EditorTool Name="BackColor" />
              <telerik:EditorSeparator />
              <telerik:EditorTool Name="FontName" />
              <telerik:EditorTool Name="RealFontSize" />
            </telerik:EditorToolGroup>
          </Tools>
          <Languages>
            <telerik:SpellCheckerLanguage Code="es-ES" Title="Español" />
          </Languages>
          <Content>
          </Content>
        </telerik:RadEditor>
        <asp:RequiredFieldValidator ID="valCuerpoEmail" runat="server" ControlToValidate="rdCuerpoEmail"
          Display="Static" ErrorMessage="*" ValidationGroup="ConfigEmail"></asp:RequiredFieldValidator>
      </div>
    </div>
  </div>

  <script language="JavaScript" type="text/javascript">
    function OnClientPasteHtml(sender, args){
    var commandName = args.get_commandName();
    var value = args.get_value();
      if (commandName == 'Paste'){
        args.set_value(cleanUpText(value));
      }
    };
  </script>

  </form>
</body>
</html>
