<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="codepassword.aspx.cs" Inherits="ICommunity.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title></title>
</head>
<body>
  <form id="form1" runat="server">
    <div>
      <table>
        <tr>
          <td>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
          </td>
        </tr>
        <tr><td><asp:Label ID="lblDone" runat="server"></asp:Label></td></tr>
        <tr>
          <td>
            <asp:TextBox ID="txtCodUser" runat="server"></asp:TextBox>
          </td>
        </tr>
        <tr>
          <td>
            <asp:Button ID="btnshowclave" runat="server" Text="Muestra Clave" OnClick="btnshowclave_Click" />
          </td>
        </tr>
        <tr>
          <td><asp:Label ID="lblPassword" runat="server"></asp:Label></td>
        </tr>
      </table>
    </div>
  </form>
</body>
</html>
