<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="litigios.ascx.cs" Inherits="ICommunity.Dashboard.Controls.litigios" %>
<div class="card">
  <div class="cb-ficha">
    <a class="trash-ficha waves-effect waves-light mr-4" data-value="20190921122307"><i class="fas fa-trash-alt light-trash"></i></a>
    <span class="card-title tt-ficha text-left">Litigios</span>
  </div>
  <hr />
  <div class="card-body">
    <div id="idVistaEnable" runat="server" visible="false" class="padding-litigios">
      <div class="row">
        <div class="col-1"></div>
        <div class="col-10 sback-litigios">
          <div class="row">
            <div class="col-3 text-center Valiniamiento">
              <div class="scircle-calendar">
                <i class="fas fa-calendar-alt fa-2x white-text"></i>
              </div>
            </div>
            <div class="col-5 text-left Valiniamiento">
              <span class="lbl-dia-proceso-normal-litigio">Días proceso normalización:</span>
            </div>
            <div class="col-4 text-left Valiniamiento">
              <asp:Label runat="server" ID="lblDiasProvision" CssClass="lbl-dias-litigios"></asp:Label>
            </div>
          </div>
        </div>
        <div class="col-1"></div>
      </div>
      <div class="row"><div class="col-12"><br /></div></div>
      <div class="row">
        <div class="col-12">
          
          <div class="row">
            <div class="col-3 litigios30 text-center Valiniamiento"><span>0-30 días</span></div>
            <div class="col-3 litigios60 text-center Valiniamiento"><span>31-60 días</span></div>
            <div class="col-3 litigios90 text-center Valiniamiento"><span>61-90 días</span></div>
            <div class="col-3 litigiosMay text-center Valiniamiento"><span>> 90 días</span></div>
          </div>

          <div class="row space-top-litigios">
            <div class="col-3 montos-litigios30 text-center"><asp:Label ID="lbl_30" runat="server"></asp:Label></div>
            <div class="col-3 montos-litigios60 text-center"><asp:Label ID="lbl_60" runat="server"></asp:Label></div>
            <div class="col-3 montos-litigios90 text-center"><asp:Label ID="lbl_90" runat="server"></asp:Label></div>
            <div class="col-3 montos-litigiosMay text-center"><asp:Label ID="lbl_mayor" runat="server"></asp:Label></div>
          </div>

          <%--<table class="table table-sm">
            <tbody>
              <tr>
                <th scope="row" class="lbllitigiosdata">
                  </th>
                <th scope="row" class="lbllitigiosdata">
                  </th>
                <th scope="row" class="lbllitigiosdata">
                  </th>
                <th scope="row" class="lbllitigiosdata">
                  </th>
              </tr>
              <tr>
                <th scope="row" class="litigios30 tex-center">
                  <span>0-30 días</span></th>
                <th class="litigios60 tex-center">
                  <span>31-60 días</span></th>
                <th class="litigios90 tex-center">
                  <span>61-90 días</span></th>
                <th class="litigiosMay tex-center">
                  <span>> 90 días</span></th>
              </tr>
            </tbody>
          </table>--%>
        </div>

      </div>
      <%--<div class="row">
        <div class="col-12">
          <a href="#!" class="d-flex justify-content-end"><span>Ver más <i class="fas fa-angle-double-right"></i></span></a>
        </div>
      </div>--%>
    </div>
    <div id="idVistaNoEnable" runat="server" visible="false">
      <p class="justify-content-center text-alert"><span class="noinfo">No existe información para los filtros seleccionados</span></p>
    </div>
  </div>
</div>

