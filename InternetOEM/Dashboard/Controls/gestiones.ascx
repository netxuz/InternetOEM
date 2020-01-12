<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="gestiones.ascx.cs" Inherits="ICommunity.Dashboard.Controls.gestiones" %>
<div class="card">
  <div class="cb-ficha">
    <a class="trash-ficha waves-effect waves-light mr-4" data-value="20190921122304"><i class="fas fa-trash-alt light-trash"></i></a>
    <span class="card-title tt-ficha text-left">Gestiones</span>
  </div>
  <hr/>
  <div class="card-body">
    <div id="idGestionesEnable" runat="server" class="row" visible="false">
      <div class="col-12">
          <asp:GridView ID="gdGestiones" runat="server" ShowFooter="false" CssClass="table table-curved" BorderStyle="None" Width="100%" AllowPaging="true" BorderWidth="0" GridLines="None"
            AutoGenerateColumns="false">
            <AlternatingRowStyle CssClass="tr-border-round" />
            <Columns>
              <asp:BoundField HeaderText="Fecha" DataField="Fecha" DataFormatString="{0:dd/MM/yyyy}">
                <HeaderStyle Width="100px" CssClass="cabeceragestion" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" CssClass="" />
              </asp:BoundField>
              <asp:BoundField HeaderText="Tipo de Gestión" DataField="sTipoGestion">
                <HeaderStyle Width="150px" HorizontalAlign="Center" CssClass="cabeceragestion" />
                <ItemStyle HorizontalAlign="Left" CssClass="" />
              </asp:BoundField>
              <asp:BoundField HeaderText="Observaciones" DataField="sObservacion">
                <HeaderStyle HorizontalAlign="Center" CssClass="cabeceragestion" />
                <ItemStyle HorizontalAlign="Left" CssClass="" />
              </asp:BoundField>
            </Columns>
          </asp:GridView>
      </div>
    </div>
    <div id="idGestionesNoEnable" runat="server" class="row" visible="false">
      <div class="col-12">
        <p class="justify-content-center text-alert"><span class="noinfo">No existe información para los filtros seleccionados</span></p>
      </div>
    </div>

  </div>
</div>
