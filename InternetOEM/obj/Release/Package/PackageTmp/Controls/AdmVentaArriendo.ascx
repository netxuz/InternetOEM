<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdmVentaArriendo.ascx.cs"
  Inherits="ICommunity.Controls.Contactenos" %>
<div id="dvContactenos">
  <div class="divNombre">
    <div class="label_nombre">
      <asp:Label ID="lblNombre" runat="server"></asp:Label>
    </div>
    <div class="textbox_nombre">
      <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
      <asp:RequiredFieldValidator ID="valNombre" runat="server" ControlToValidate="txtNombre"
        Display="Static" ErrorMessage="*"></asp:RequiredFieldValidator>
    </div>
  </div>
  <div class="divRut">
    <div class="label_rut">
      <asp:Label ID="lblRut" runat="server" Text="RUT"></asp:Label>
    </div>
    <div class="textbox_rut">
      <asp:TextBox ID="txtRut" runat="server"></asp:TextBox>
      <asp:RequiredFieldValidator ID="valRut" runat="server" ControlToValidate="txtRut"
        Display="Static" ErrorMessage="*"></asp:RequiredFieldValidator>
    </div>
  </div>
  <div class="divEmail">
    <div class="label_email">
      <asp:Label ID="lblEmail" runat="server"></asp:Label></div>
    <div class="textbox_email">
      <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
      <asp:RequiredFieldValidator ID="valEmail" runat="server" ControlToValidate="txtEmail"
        Display="Static" ErrorMessage="*"></asp:RequiredFieldValidator>
      <asp:CustomValidator ID="valtxtEmailVal" runat="server" ErrorMessage="*" ControlToValidate="txtEmail"
        Display="Static" OnServerValidate="ServerValidationEmail"></asp:CustomValidator>
    </div>
  </div>
  <div class="divFono">
    <div class="label_fono">
      <asp:Label ID="lblFono" runat="server"></asp:Label></div>
    <div class="textbox_fono">
      <asp:TextBox ID="txtFono" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="valFono" runat="server" ControlToValidate="txtFono"
      Display="Static" ErrorMessage="*"></asp:RequiredFieldValidator></div>
  </div>
  <div class="divDireccion">
    <div class="label_direccion">
      <asp:Label ID="Label1" runat="server" Text="Dirección"></asp:Label></div>
    <div class="textbox_direccion">
      <asp:TextBox ID="txtDireccion" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="valDireccion" runat="server" ControlToValidate="txtDireccion"
      Display="Static" ErrorMessage="*"></asp:RequiredFieldValidator></div>
  </div>
  <div class="divComuna">
    <div class="label_comuna">
      <asp:Label ID="lblComuna" runat="server" Text="Comuna"></asp:Label></div>
    <div class="textbox_comuna">
      <asp:TextBox ID="txtComuna" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="valComuna" runat="server" ControlToValidate="txtComuna"
      Display="Static" ErrorMessage="*"></asp:RequiredFieldValidator></div>
  </div>
  <div class="divTerreno">
    <div class="label_terreno">
      <asp:Label ID="lblTerreno" runat="server" Text="Terreno/M2"></asp:Label></div>
    <div class="textbox_terreno">
      <asp:TextBox ID="txtTerreno" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="valTerreno" runat="server" ControlToValidate="txtTerreno"
      Display="Static" ErrorMessage="*"></asp:RequiredFieldValidator></div>
  </div>
  <div class="divNumHabitaciones">
    <div class="label_numhabitaciones">
      <asp:Label ID="lblNumHabitaciones" runat="server" Text="N° Habitaciones"></asp:Label></div>
    <div class="textbox_numhabitaciones">
      <asp:TextBox ID="txtNumAbitaciones" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="valNumAbitaciones" runat="server" ControlToValidate="txtNumAbitaciones"
      Display="Static" ErrorMessage="*"></asp:RequiredFieldValidator></div>
  </div>
  <div class="divNumBanos">
    <div class="label_numbanos">
      <asp:Label ID="lblNumBanos" runat="server" Text="N° Baños"></asp:Label></div>
    <div class="textbox_numBanos">
      <asp:TextBox ID="txtNumBanos" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="valNumBanos" runat="server" ControlToValidate="txtNumBanos"
      Display="Static" ErrorMessage="*"></asp:RequiredFieldValidator></div>
  </div>
  <div class="divComentario">
    <div class="label_comentario">
      <asp:Label ID="lblComentario" runat="server"></asp:Label></div>
    <div class="textbox_comentario">
      <asp:TextBox ID="txtComentario" runat="server" TextMode="MultiLine" Rows="7" Columns="40"></asp:TextBox>
      <asp:RequiredFieldValidator ID="valComentario" runat="server" ControlToValidate="txtComentario"
        Display="Static" ErrorMessage="*"></asp:RequiredFieldValidator>
    </div>
  </div>
  <div id="boton">
    <asp:Button ID="BtnAceptar" runat="server" OnClick="BtnAceptar_Click" />
  </div>
</div>
