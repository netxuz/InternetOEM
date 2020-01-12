<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="facturascomerciales.ascx.cs" Inherits="ICommunity.Dashboard.Controls.facturascomerciales" %>
<div class="card">
  <div class="cb-ficha">
    <a class="trash-ficha waves-effect waves-light mr-4" data-value="20190921122308"><i class="fas fa-trash-alt light-trash"></i></a>
    <span class="card-title tt-ficha text-left">Facturas Comerciales</span>
  </div>
  <hr/>
  <div class="card-body">
    <div id="idVistaEnable" runat="server" visible="false" class="mc-fact-cm">
      <div class="row">
        <div class="col-12 text-center">
          <span class="tt-acumalado-cy">Aplicaciones del mes al CY:</span>
        </div>
      </div>
      <div class="row">
        <div class="col-12">
          <div id="horizontalBar2"></div>
        </div>
      </div>
      <div class="row space-col-btm-fac-com">
        <div class="col-6 text-center"><i class="fas fa-square square-discresionales"></i> <span class="tt-legend-discresionales">Discrecionales</span></div>
        <div class="col-6 text-center"><i class="fas fa-square square-acuer-comer"></i> <span class="tt-legend-acuer-comer">Acuerdos Comerciales</span></div>
      </div>
      <div class="row">
        <div class="col-12 bk-acumulado-cy space-col-top-fac-com">
          <table border="0">
            <tr>
              <td colspan="3" class="text-center"><span class="tt-acumalado-cy">Acumulado CY:</span></td>
            </tr>
            <tr>
              <td class="td-acum_cy text-center"><span class="lb-acumalado-cy">Budget <%= DateTime.Now.Year.ToString() %></span></td>
              <td class="td-acum_cy text-center"><span class="lb-acumalado-cy">Documentos:</span></td>
              <td class="text-center"><span class="lb-acumalado-cy">Acuerdos Comerciales:</span></td>
            </tr>
            <tr>
              <td class="td-dt-acum-cy space-col-btm-fac-com">
                <asp:Label ID="idBudget" runat="server" CssClass="dt-acumalado-cy"></asp:Label></td>
              <td class="td-dt-acum-cy space-col-btm-fac-com">
                <asp:Label ID="idDocumentos" runat="server" CssClass="dt-acumalado-cy"></asp:Label></td>
              <td class="td-dt-acum-cy space-col-btm-fac-com">
                <asp:Label ID="idAcuerdosComerciales" runat="server" CssClass="dt-acumalado-cy"></asp:Label></td>
            </tr>
          </table>
        </div>
      </div>
    </div>
    <div id="idVistaNoEnable" runat="server" visible="false">
      <p class="justify-content-center text-alert"><span class="noinfo">No existe información para los filtros seleccionados</span></p>
    </div>
  </div>
</div>
