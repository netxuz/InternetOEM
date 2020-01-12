<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="provision.ascx.cs" Inherits="ICommunity.Dashboard.Controls.provision" %>
<div class="card">
  <div class="cb-ficha">
    <a class="trash-ficha waves-effect waves-light mr-4" data-value="20190921122306"><i class="fas fa-trash-alt light-trash"></i></a>
    <span class="card-title tt-ficha text-left">Provisión</span>
  </div>
  <hr />
  <div class="card-body">
    <div id="idVistaEnable" runat="server" visible="false">
      <div class="row">
        <div class="col-6">
          <div id="pieChart"></div>
        </div>
        <div class="col-6 Valiniamiento">
          <div class="row">
            <div class="col-12 text-left">
              <span class="tt_provison_data">Provisión Acumulada</span>
            </div>
          </div>
          <div class="row">
            <div class="col-12 text-left">
              <asp:Label ID="lblProvisionAcumulada" runat="server" CssClass="lb_provison_data"></asp:Label>
            </div>
          </div>
          <div class="row"><div class="col-12 text-left"><br /></div></div>
          <div class="row">
            <div class="col-12 text-left">
              <span class="tit_impacto_provison">Impacto Provisión</span>
            </div>
          </div>
          <div class="row">
            <div class="col-12 text-left">
              <asp:Label ID="lblImpactoProvision" runat="server" CssClass="lb_impacto_provison"></asp:Label>
            </div>
          </div>
        </div>
      </div>
      <div class="row">
        <div class="col-1"><br /></div>
        <div class="col-10 text-center row-provison-sla">
          <span class="lbl-txt-provision-sla">Provisión SLA</span> <asp:Label ID="lblProvision" runat="server" CssClass="lbl-monto-provision-sla"></asp:Label>
        </div>
        <div class="col-1"><br /></div>
      </div>
    </div>
    <div id="idVistaNoEnable" runat="server" visible="false">
      <p class="justify-content-center text-alert"><span class="noinfo">No existe información para los filtros seleccionados</span></p>
    </div>
  </div>
</div>
