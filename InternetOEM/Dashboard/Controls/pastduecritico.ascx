<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="pastduecritico.ascx.cs" Inherits="ICommunity.Dashboard.Controls.pastduecritico" %>
<div class="card">
  <div class="cb-ficha">
    <a class="trash-ficha waves-effect waves-light mr-4" data-value="20190921122309"><i class="fas fa-trash-alt light-trash"></i></a>
    <span class="card-title tt-ficha text-left">Past Due</span>
  </div>
  <hr />
  <div class="card-body">
    <div id="idVistaEnable" runat="server" visible="false">
      <div class="row">
        <div class="col-12">
          <div id="chartPasDue"></div>
        </div>
      </div>
      <div class="row"><div class="col-12"><br /></div></div>
      <div class="row row-center-pastdue">
        <div class="col-12">
          <div class="row row-cb-pastdue">
            <div class="col-3 text-center col-tt-cab-barra"><span class="tt-tb-pastdue">Detalle</span></div>
            <div class="col-3 text-center col-tt-cab-barra">
              <asp:Label ID="lb_mesano_actual" runat="server" CssClass="tt-tb-pastdue"></asp:Label></div>
            <div class="col-3 text-center col-tt-cab-barra">
              <asp:Label ID="lb_mesano_ant" runat="server" CssClass="tt-tb-pastdue"></asp:Label></div>
            <div class="col-3 text-center"><span class="tt-tb-pastdue">Variación</span></div>
          </div>
          <div class="row row-md-pastdue">
            <div class="col-3 text-center col_dt-barra-rgt"><span class="tt-tb-pastdue">Total</span></div>
            <div class="col-3 text-center col_dt-barra-rgt">
              <asp:Label ID="lb_monto_total" runat="server" CssClass="dt-tb-pastdue"></asp:Label></div>
            <div class="col-3 text-center col_dt-barra-rgt">
              <asp:Label ID="lb_monto_total_hist" runat="server" CssClass="dt-tb-pastdue"></asp:Label></div>
            <div class="col-3 text-center">
              <asp:Label ID="lb_variacion_total" runat="server" CssClass="tt-tb-pastdue"></asp:Label></div>
          </div>
          <div class="row row-bottom-pastdue">
            <div class="col-3 text-center col_dt-barra-rgt"><span class="tt-tb-pastdue">Crítico</span></div>
            <div class="col-3 text-center col_dt-barra-rgt">
              <asp:Label ID="lb_monto_critico" runat="server" CssClass="dt-tb-pastdue"></asp:Label></div>
            <div class="col-3 text-center col_dt-barra-rgt">
              <asp:Label ID="lb_monto_critico_hist" runat="server" CssClass="dt-tb-pastdue"></asp:Label></div>
            <div class="col-3 text-center">
              <asp:Label ID="lb_variacion_critico_hist" runat="server" CssClass="tt-tb-pastdue"></asp:Label></div>
          </div>
        </div>
      </div>
    </div>
    <div id="idVistaNoEnable" runat="server" visible="false">
      <p class="justify-content-center text-alert"><span class="noinfo">No existe información para los filtros seleccionados</span></p>
    </div>
  </div>
</div>
