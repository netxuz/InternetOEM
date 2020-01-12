<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="comportamientofinanciero.ascx.cs" Inherits="ICommunity.Dashboard.Controls.comportamientofinanciero" %>
<div class="card">
  <div class="cb-ficha">
    <a class="trash-ficha waves-effect waves-light mr-4" data-value="20190921122303"><i class="fas fa-trash-alt light-trash"></i></a>
    <span class="card-title tt-ficha text-left">Comportamiento Financiero</span>
  </div>
  <hr />
  <div class="card-body">
    <%--<div class="row">
      <div class="col-1">
        <br />
      </div>
      <div id="idBox_riesgo" class="col-3 text-center" runat="server">
      </div>
      <div class="col-8">
        <div id="idtit_riesgo" runat="server"></div>
        <p class="txt-riesgo">En escala de 1 a 10, a mayor puntaje menor es el riesgo del evaluado.</p>
      </div>
    </div>
    <div class="row">
      <div class="col-12">
        <hr class="hr-dash" />
      </div>
    </div>--%>
    <div class="row">
      <div class="col-12 text-center space-tt-detalle-pago">
        <span class="tt-detalle-pago">COMPROMISO DE PAGO:</span>
      </div>
    </div>
    <div class="row">
      <div class="col-6 separador-right">
        <div class="row">
          <div class="col-12 text-right">
            <span class="tt-dat-overview">Posee compromiso:</span>
            <asp:Label ID="lbl_compromiso_pago" runat="server" CssClass="lb-dat-overview"></asp:Label>
          </div>
        </div>
        <div class="row">
          <div class="col-12 text-right">
            <span class="tt-dat-overview">Cumplidos:</span>
            <asp:Label ID="lbl_cumplido" runat="server" CssClass="lb-dat-overview"></asp:Label>
          </div>
        </div>
        <div class="row">
          <div class="col-12 text-right">
            <span class="tt-dat-overview">No Cumplidos:</span>
            <asp:Label ID="lbl_no_cumplido" runat="server" CssClass="lb-dat-overview"></asp:Label>
          </div>
        </div>
      </div>
      <div class="col-6">
        <div class="row">
          <div class="col-12 text-left">
            <span class="tt-dat-overview">Fecha:</span>
            <asp:Label ID="lbl_fecha" runat="server" CssClass="lb-dat-overview"></asp:Label>
          </div>
        </div>
        <div class="row">
          <div class="col-12 text-left">
            <span class="tt-dat-overview">Monto:</span>
            <asp:Label ID="lblMonto" runat="server" CssClass="lb-dat-overview"></asp:Label>
          </div>
        </div>
      </div>
    </div>
    <div class="row">
      <div class="col-12">
        <br />
      </div>
    </div>
    <div class="row">
      <div class="col-12">
        <div class="row">
          <div class="col-1">
            <br />
          </div>
          <div class="col-5 calendario-pago-back-left Valiniamiento">
            <div class="row">
              <div class="col-5 text-right Valiniamiento"><i class="far fa-calendar-alt fa-2x pink-calendario"></i></div>
              <div class="col-7"><span class="tt-col-pink-overview">Calendario de Pago</span></div>
            </div>
          </div>

          <div class="col-5 calendario-pago-back-right" id="calendario_pago_sidias" runat="server">
            <%--<span class="tt-lb-compromiso-pago">Dias:</span>--%><asp:Label ID="lbl_dias" runat="server" CssClass="lb-dat-overview"></asp:Label><br />
            <%--<span class="tt-lb-compromiso-pago">Semanas:</span>--%><asp:Label ID="lbl_semana" runat="server" CssClass="lb-dat-overview"></asp:Label>
          </div>
          <div class="col-5 calendario-pago-back-right" id="calendario_pago_nodias" runat="server" visible="false">
              <span class="lb-dat-overview">Al vencimiento</span>
          </div>
          <div class="col-1">
            <br />
          </div>
        </div>
      </div>
    </div>
    <div class="row"><div class="col-12">&nbsp;</div></div>
    <div class="row">
      <div class="col-12 text-center">
        <span class="tt-detalle-pago">INDICE DE CREDIBILIDAD</span>
      </div>
    </div>
    <div class="row"><div class="col-12">&nbsp;</div></div>
    <div class="row">
      <div class="col-1 index-creb-blanco"></div>
      <div id="id_rojo" runat="server" class="col-2 index-creb-rojo">&nbsp;</div>
      <div id="id_naranjo" runat="server" class="col-2 index-creb-naranjo">&nbsp;</div>
      <div id="id_amarillo" runat="server" class="col-2 index-creb-amarillo">&nbsp;</div>
      <div id="id_verdeclaro" runat="server" class="col-2 index-creb-verdeclaro">&nbsp;</div>
      <div id="id_verdeocscuro" runat="server" class="col-2 index-creb-verdeoscuro">&nbsp;</div>
      <div class="col-1 index-creb-blanco"></div>
    </div>
    <div class="row">
      <div class="col-2 text-center"><span class="lb-dat-overview">0</span></div>
      <div class="col-2 text-center"><span class="lb-dat-overview">20</span></div>
      <div class="col-2 text-center"><span class="lb-dat-overview">40</span></div>
      <div class="col-2 text-center"><span class="lb-dat-overview">60</span></div>
      <div class="col-2 text-center"><span class="lb-dat-overview">80</span></div>
      <div class="col-2 text-center"><span class="lb-dat-overview">100</span></div>
    </div>
  </div>
</div>
