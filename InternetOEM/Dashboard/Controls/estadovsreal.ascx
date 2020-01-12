<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="estadovsreal.ascx.cs" Inherits="ICommunity.Dashboard.Controls.estadovsreal" %>
<div class="card">
  <div class="cb-ficha">
    <a class="trash-ficha waves-effect waves-light mr-4" data-value="20190921122302"><i class="fas fa-trash-alt light-trash"></i></a>
    <span class="card-title tt-ficha text-left">Estimado vs Real</span>
  </div>
  <hr/>
  <div class="card-body">
    <div id="idVistaEnable" runat="server" visible="false">
      <div class="row">
        <div class="col-12">
          <div id="lineChart2"></div>
        </div>
      </div>
      <div class="row">
        <div class="col-12">
          <hr class="hr-dash" />
        </div>
      </div>
      <div class="row">
        <div class="col-12 text-center space-tt-detalle-pago">
          <span class="tt-detalle-pago">DETALLE CUENTAS POR COBRAR:</span>
        </div>
      </div>
      <div class="row">
        <div class="col-6 separador-right">
          <div class="row">
            <div class="col-12 text-right">
              <span class="tt-dat-overview">Total</span>
            </div>
          </div>
          <div class="row">
            <div class="col-12 text-right">
              <asp:Label ID="lblTotal" runat="server" CssClass="lb-detalle-cuentas-x-cobrar"></asp:Label>
            </div>
          </div>
          <div class="row">
            <div class="col-12 text-right">
              <span class="tt-dat-overview">Vencidas</span>
            </div>
          </div>
          <div class="row">
            <div class="col-12 text-right">
              <asp:Label ID="lblVencidas" runat="server" CssClass="lb-detalle-cuentas-x-cobrar"></asp:Label>
            </div>
          </div>
          <div class="row">
            <div class="col-12 text-right">
              <span class="tt-dat-overview">Deuda Documentada</span>
            </div>
          </div>
          <div class="row">
            <div class="col-12 text-right">
              <asp:Label ID="lblDeudaDocumentada" runat="server" CssClass="lb-detalle-cuentas-x-cobrar"></asp:Label>
            </div>
          </div>
        </div>
        <div class="col-6">
          <div class="row">
            <div class="col-12 text-left">
              <span class="tt-dat-overview">Vencidas > 30 días</span>
            </div>
          </div>
          <div class="row">
            <div class="col-12 text-left">
              <asp:Label ID="lblVencidas30" runat="server" CssClass="lb-detalle-cuentas-x-cobrar"></asp:Label>
            </div>
          </div>
          <div class="row">
            <div class="col-12 text-left">
              <span class="tt-dat-overview">Facturas > 30 días sin litigios</span>
            </div>
          </div>
          <div class="row">
            <div class="col-12 text-left">
              <asp:Label ID="lblVencidas30SinLitigios" runat="server" CssClass="lb-detalle-cuentas-x-cobrar"></asp:Label>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div id="idVistaNoEnable" runat="server" visible="false">
      <p class="justify-content-center text-alert"><span class="noinfo">No existe información para los filtros seleccionados</span></p>
    </div>
  </div>
</div>
