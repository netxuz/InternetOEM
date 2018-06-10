<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CreateUsers.ascx.cs"
  Inherits="ICommunity.Controls.CreateUsers" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:HiddenField ID="CodUsuario" runat="server" />
<div id="ErrMsg">
  <asp:ValidationSummary ID="valSum" CssClass="CsErrMsg" DisplayMode="SingleParagraph" HeaderText="Existen campos que deben ser completados " runat="server" />
</div>
<div id="panel">
  <telerik:RadPanelBar ID="RadPanelBar" runat="server" EnableEmbeddedSkins="false" Width="600px">
    <Items>
      <telerik:RadPanelItem Text="Datos Básicos" Expanded="true">
        <ContentTemplate>
          <div class="ContenidoTemplate">
            <div class="dCampo">
              <div class="dLabel">
                <asp:Label ID="lblNomUsuario" runat="server" CssClass="cLabel"></asp:Label>
              </div>
              <div class="dControl">
                <asp:TextBox ID="txtNomUsuario" runat="server" CssClass="cControl"></asp:TextBox>
                <asp:RequiredFieldValidator ID="valNomUsuario" runat="server" ControlToValidate="txtNomUsuario"
                  Display="Static" ErrorMessage="" Text="*"></asp:RequiredFieldValidator>
              </div>
            </div>
            <div id="idApellido" class="dCampo" runat="server">
              <div class="dLabel">
                <asp:Label ID="lblApeUsuario" runat="server" CssClass="cLabel"></asp:Label>
              </div>
              <div class="dControl">
                <asp:TextBox ID="txtApeUsuario" runat="server" CssClass="cControl"></asp:TextBox>
                <asp:RequiredFieldValidator ID="valApeUsuario" runat="server" ControlToValidate="txtApeUsuario"
                  Display="Static" ErrorMessage="" Text="*"></asp:RequiredFieldValidator>
              </div>
            </div>
            <div class="dCampo">
              <div class="dLabel">
                <asp:Label ID="lblLogin" runat="server" CssClass="cLabel"></asp:Label>
              </div>
              <div class="dControl">
                <asp:TextBox ID="txtLoginAlmacenado" runat="server" CssClass="cControl" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtLogin" runat="server" CssClass="cControl"></asp:TextBox>
                <asp:RequiredFieldValidator ID="valLogin" runat="server" ControlToValidate="txtLogin"
                  Display="Static" ErrorMessage="" Text="*"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="custValLogin" runat="server" ErrorMessage="" Text="*" ControlToValidate="txtLogin"
                  Display="Static" OnServerValidate="ServerValidationLogin"></asp:CustomValidator>
              </div>
            </div>
            <div class="dCampo">
              <div class="dLabel">
                <asp:Label ID="lblClave" runat="server" CssClass="cLabel"></asp:Label>
              </div>
              <div class="dControl">
                <asp:TextBox ID="txtClave" runat="server" TextMode="Password" CssClass="cControl"></asp:TextBox>
                <asp:RequiredFieldValidator ID="valTxtClave" runat="server" ControlToValidate="txtClave"
                  Display="Static" ErrorMessage="" Text="*"></asp:RequiredFieldValidator>
              </div>
            </div>
            <div class="dCampo">
              <div class="dLabel">
                <asp:Label ID="lblRepClave" runat="server" CssClass="cLabel"></asp:Label>
              </div>
              <div class="dControl">
                <asp:TextBox ID="txtRepClave" runat="server" TextMode="Password" CssClass="cControl"></asp:TextBox>
                <asp:RequiredFieldValidator ID="valTxtRepClave" runat="server" ControlToValidate="txtRepClave"
                  Display="Static" ErrorMessage="" Text="*"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="valComparar" runat="server" ControlToValidate="txtRepClave"
                  ControlToCompare="txtClave" Display="Static" ErrorMessage="" Text="*"></asp:CompareValidator>
              </div>
            </div>
            <div class="dCampo">
              <div class="dLabel">
                <asp:Label ID="lblEmlUsuario" runat="server" CssClass="cLabel"></asp:Label>
              </div>
              <div class="dControl">
                <asp:TextBox ID="txtEmlUsuario" runat="server" CssClass="cControl"></asp:TextBox>
                <asp:RequiredFieldValidator ID="valEmlUsuario" runat="server" ControlToValidate="txtEmlUsuario"
                  Display="Static" ErrorMessage="" Text="*"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="valEmlUsuarioVal" runat="server" ErrorMessage="" Text="*" ControlToValidate="txtEmlUsuario"
                  Display="Static" OnServerValidate="ServerValidationEml"></asp:CustomValidator>
              </div>
            </div>
            <div id="idFono" class="dCampo" runat="server">
              <div class="dLabel">
                <asp:Label ID="lblFonoUsuario" runat="server" CssClass="cLabel"></asp:Label>
              </div>
              <div class="dControl">
                <asp:TextBox ID="txtFonoUsuario" runat="server" CssClass="cControl"></asp:TextBox>
                <asp:RequiredFieldValidator ID="valFonoUsuario" runat="server" ControlToValidate="txtFonoUsuario"
                  Display="Static" ErrorMessage="" Text="*"></asp:RequiredFieldValidator>
              </div>
            </div>
            <div id="idDestacado" class="dCampo" runat="server">
              <div class="dLabel">
                <asp:Label ID="lblDestUsuario" runat="server" CssClass="cLabel"></asp:Label>
              </div>
              <div class="dControl">
                <asp:CheckBox ID="check_destacado" runat="server" />
              </div>
            </div>
            <div id="idTipoUsuario" class="dCampo" runat="server">
              <div class="dLabel">
                <asp:Label ID="lblTipoUsuario" runat="server" CssClass="cLabel"></asp:Label>
              </div>
              <div class="dControl">
                <telerik:RadComboBox ID="rdCmbTipoUsuario" runat="server" CssClass="cControl">
                </telerik:RadComboBox>
              </div>
            </div>
            <div id="idEstUsuario" class="dCampo" runat="server" visible="true">
              <div class="dLabel">
                <asp:Label ID="lblEstUsuario" runat="server" CssClass="cLabel"></asp:Label>
              </div>
              <div class="dControl">
                <telerik:RadComboBox ID="rdCmbEstUsuario" runat="server" CssClass="cControl">
                </telerik:RadComboBox>
              </div>
            </div>
            <div id="idCertificado" class="dCampo" runat="server">
              <div class="dLabel">
                <asp:Label ID="lblCertificado" runat="server" CssClass="cLabel"></asp:Label>
              </div>
              <div class="dControl">
                <asp:CheckBox ID="check_certificado" runat="server" />
              </div>
            </div>
          </div>
        </ContentTemplate>
      </telerik:RadPanelItem>
      <telerik:RadPanelItem Text="Información Adicional" Visible="true" Expanded="true">
        <ContentTemplate>
          <div id="idUsrInf" runat="server" class="ContenidoTemplate">
          </div>
        </ContentTemplate>
      </telerik:RadPanelItem>
    </Items>
  </telerik:RadPanelBar>
  <div id="boton">
    <asp:Button ID="btnGrabar" runat="server" CssClass="btnGrabar" OnClick="btnGrabar_Click" />
    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
  </div>
</div>
