<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="dso.ascx.cs" Inherits="ICommunity.Dashboard.Controls.dso" %>
<div class="card">
  <div class="cb-ficha">
    <a class="trash-ficha waves-effect waves-light mr-4" data-value="20190921122305"><i class="fas fa-trash-alt light-trash"></i></a>
    <span class="card-title tt-ficha text-left">DSO</span>
  </div>
  <hr />
  <div class="card-body">
    <div id="idVistaEnable" runat="server" visible="false">
      <div id="lineChart1" style="height: 300px;"></div>
      <div class="row">
        <div class="col-1"></div>
        <div class="col-10 sback-dso">
          <div class="row">
            <div class="col"></div>
            <div class="col-3 text-center Valiniamiento">
              <div class="scircle-calendar-dso">
                <i class="fas fa-calendar-alt fa-2x white-text"></i>
              </div>
            </div>
            <div class="col-4 text-left Valiniamiento">
              <span class="lbl-dia-proceso-normal-dso">SLA:</span>
              <asp:Label runat="server" ID="lb_sla_dso" CssClass="lbl-dias-dso"></asp:Label>
            </div>
            <div class="col">
            </div>
          </div>
        </div>
        <div class="col-1"></div>
      </div>
    </div>
    <div id="idVistaNoEnable" runat="server" visible="false">
      <p class="justify-content-center text-alert"><span class="noinfo">No existe información para los filtros seleccionados</span></p>
    </div>
  </div>
</div>
