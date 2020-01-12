<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="historicodbt.ascx.cs" Inherits="ICommunity.Dashboard.Controls.historicodbt__" %>
<div class="card">
  <div class="cb-ficha">
    <a class="trash-ficha waves-effect waves-light mr-4" data-value="20191012141100"><i class="fas fa-trash-alt light-trash"></i></a>
    <span class="card-title tt-ficha text-left">Histórico DBT</span>
  </div>
  <hr/>
  <div class="card-body">
    <div id="horizontalBar" style="height: 300px;"></div>
    <div class="row">
        <div class="col-1"></div>
        <div class="col-10 sback-dbt">
          <div class="row">
            <div class="col-2"></div>
            <div class="col-3 text-center Valiniamiento">
              <div class="scircle-calendar-dbt">
                <i class="fas fa-calendar-alt fa-2x white-text"></i>
              </div>
            </div>
            <div class="col-5 text-left Valiniamiento">
              <span class="lbl-dia-proceso-normal-dbt">Condición Pago:</span>
              <asp:Label runat="server" ID="lb_sla_dbt" CssClass="lbl-dias-dbt"></asp:Label>
            </div>
            <div class="col">
            </div>
          </div>
        </div>
        <div class="col-1"></div>
      </div>
  </div>
</div>
