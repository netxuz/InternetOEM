<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CampoUsuario.ascx.cs"
  Inherits="ICommunity.Controls.CampoUsuario" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:HiddenField ID="hddCodCampo" runat="server" />
<div>
  <asp:Label ID="lblNomCampo" runat="server" Text="Nombre Campo"></asp:Label>
  <telerik:RadTextBox ID="TxtColumna" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.nom_campo") %>'>
  </telerik:RadTextBox>
  <asp:Label ID="Tipo" Text="Tipo" runat="server"></asp:Label>
  <telerik:RadComboBox ID="rdCmbTipo" runat="server" AutoPostBack="True" EmptyMessage="Seleccione"
    OnSelectedIndexChanged="rdCmbTipo_SelectedIndexChanged">
    <Items>
      <telerik:RadComboBoxItem runat="server" Text="Seleccione" />
      <telerik:RadComboBoxItem runat="server" Text="Lista" Value="0" />
      <telerik:RadComboBoxItem runat="server" Text="Lista Multiple Selección" Value="1" />
      <telerik:RadComboBoxItem runat="server" Text="Texto" Value="2" />
      <telerik:RadComboBoxItem runat="server" Text="Fecha" Value="4" />
      <telerik:RadComboBoxItem runat="server" Text="Numerico" Value="5" />
      <telerik:RadComboBoxItem runat="server" Text="Url - Link" Value="6" />
    </Items>
  </telerik:RadComboBox>
</div>
<div id="idOpciones" runat="server" visible="false">
  <div>
    <asp:Label ID="lblDescribe" runat="server" Text="Escriba cada opción en una linea distinta"
      CssClass="ayuda_tipo"></asp:Label>
    <telerik:RadTextBox ID="txtAtributos" TextMode="MultiLine" Height="100" Width="290"
      runat="server">
    </telerik:RadTextBox>
    <asp:CheckBox ID="chk_multSelect" Text="Dejar campo Multiple Selección" runat="server" />
    <asp:Button ID="btnAtributos" runat="server" CssClass="Lupa" Text="Atributo" OnClientClick="openWindowLista();return false;" />
  </div>
</div>
<div id="idOpcionesTxt" runat="server" visible="false">
  <div>
    <asp:CheckBox ID="chk_observacion" Text="Dejar campo tipo observación" runat="server" />
  </div>
  <div>
    <asp:CheckBox ID="chk_despl_usr" Text="Dejar disponible a todos los usuarios" runat="server" />
  </div>
  <div>
    <asp:CheckBox ID="chk_despl_portal" Text="Dejar disponible en el portal" runat="server" />
  </div>
  <div>
    <asp:CheckBox ID="chk_ind_validacion" Text="Activar validar llenado" runat="server" />
  </div>
</div>
<div class="opBtn">
  <asp:Button ID="btnGrabar" runat="server" CommandName="Update" Text="Grabar" CssClass="btnGrabar" />
  <asp:Button ID="btnCancelar" runat="server" CommandName="Cancel" Text="Cancelar"
    CssClass="btnCancelar" />
</div>