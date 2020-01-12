<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="overview.ascx.cs" Inherits="ICommunity.Dashboard.Controls.Overview" %>
<div class="card">
  <div class="cb-ficha">
    <a class="trash-ficha waves-effect waves-light mr-4" data-value="20190921122301"><i class="fas fa-trash-alt light-trash"></i></a>
    <span class="card-title tt-ficha text-left">Overview</span>
  </div>
  <hr/>
  <div class="card-body">
    <div id="idOverviewEnable" runat="server" visible="false">
      <div class="row">
        <div class="col-6 Valiniamiento">
          <div class="row">
            <div class="col-2 text-right"><br /></div>
            <div class="col-2 text-right">
              <i class="fas fa-dollar-sign blue-dollar fa-3x"></i>
            </div>
            <div class="col-8 Valiniamiento">
              <asp:Label ID="lblTitle" runat="server" CssClass="tt-overview"></asp:Label>
            </div>
          </div>
        </div>
        <div class="col-6 text-center">
          <div id="donut_single"></div>
          <div id="labelOverlay">
            <asp:Label ID="lblPorcentaje" runat="server" CssClass="percentaje-overview"></asp:Label>
          </div>
        </div>
      </div>
      <div class="row">
        <div class="col-1">
          <br />
        </div>
        <div class="col-5 bkg_overview_blue">
          <div class="row">
            <div class="col-12 text-center">
              <asp:Label ID="lblConsumido" runat="server" CssClass="tt-col-blue-overview"></asp:Label>
            </div>
          </div>
          <div class="row">
            <div class="col-12 text-center">
              <asp:Label ID="tt_consumido" runat="server" CssClass="lb-col-blue-overview"></asp:Label>
            </div>
          </div>
        </div>
        <div class="col-5 bkg_overview_pink">
          <div class="row">
            <div class="col-12 text-center">
              <asp:Label ID="lblDisponible" runat="server" CssClass="tt-col-pink-overview"></asp:Label>
            </div>
          </div>
          <div class="row">
            <div class="col-12 text-center">
              <asp:Label ID="tt_disponible" runat="server" CssClass="lb-col-pink-overview"></asp:Label>
            </div>
          </div>
        </div>
        <div class="col-1">
          <br />
        </div>
      </div>
      <div class="row">
        <div class="col-12 text-center">
          <div id="idStatus" runat="server" class="row" visible="false">
          </div>
        </div>
      </div>
      <div class="row">
        <div class="col-12">
          <hr class="hr-dash" />
        </div>
      </div>
      <div class="row">
        <div class="col-12 text-center space-tt-detalle-pago">
          <span class="tt-detalle-pago">DETALLE DE PAGO:</span>
        </div>
      </div>
      <div class="row row-line-height">
        <div class="col-6 text-right">
          <span class="tt-dat-overview">Último Abono:</span>
        </div>
        <div class="col-6 text-left">
          <asp:Label ID="lbl_UltAbono" runat="server" CssClass="lb-dat-overview"></asp:Label>
        </div>
      </div>
      <div class="row row-line-height">
        <div class="col-6 text-right">
          <span class="tt-dat-overview">Monto Abonado:</span>
        </div>
        <div class="col-6 text-left">
          <asp:Label ID="lb_Monto_abono" runat="server" CssClass="lb-dat-overview"></asp:Label>
        </div>
      </div>
      <div class="row row-line-height">
        <div class="col-6 text-right">
          <span class="tt-dat-overview">Tipo de Pago:</span>
        </div>
        <div class="col-6 text-left">
          <asp:Label ID="lb_tipo_pago" runat="server" CssClass="lb-dat-overview"></asp:Label>
        </div>
      </div>
      <div class="row row-line-height">
        <div class="col-6 text-right">
          <span class="tt-dat-overview">Seguro de Crédito:</span>
        </div>
        <div class="col-6 text-left">
          <asp:Label ID="lb_Seguro_Credito" runat="server" CssClass="lb-dat-overview"></asp:Label>
        </div>
      </div>
      <div class="row row-line-height">
        <div class="col-6 text-right">
          <span class="tt-dat-overview">Canal:</span>
        </div>
        <div class="col-6 text-left">
          <asp:Label ID="lb_Canal" runat="server" CssClass="lb-dat-overview"></asp:Label>
        </div>
      </div>
      <div class="row row-line-height">
        <div class="col-6 text-right">
          <span class="tt-dat-overview">Vía de Pago:</span>
        </div>
        <div class="col-6 text-left">
          <asp:Label ID="lb_Via_Pago" runat="server" CssClass="lb-dat-overview"></asp:Label>
        </div>
      </div>
    </div>

    <%--<div class="row">
      <div class="row">
        <div class="col-12">
          <div id="oProgressBar" runat="server" class="progress"></div>
        </div>
      </div>
    </div>--%>
    <div id="idOverviewNoEnable" runat="server" visible="false">
      <p class="justify-content-center text-alert"><span class="noinfo">No existe información para los filtros seleccionados</span></p>
    </div>
  </div>
</div>
