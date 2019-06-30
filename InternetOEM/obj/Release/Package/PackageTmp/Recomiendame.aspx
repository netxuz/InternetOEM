<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Recomiendame.aspx.cs" Inherits="ICommunity.Recomiendame" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title></title>
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
