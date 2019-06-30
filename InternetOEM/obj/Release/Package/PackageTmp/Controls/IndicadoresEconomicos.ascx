<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IndicadoresEconomicos.ascx.cs" Inherits="ICommunity.Controls.IndicadoresEconomicos" %>
<h2>Indicadores Financieros</h2>
<table>
  <tbody>
    <tr class="wow fadeInRight" data-wow-duration="2s" data-wow-delay="0.2s">
      <td>
        <h4 class="text-secondary">D&Oacute;LAR OBSERVADO</h4>
      </td>
      <td><span id="idDolar" runat="server"></span><%--<br />--%>
        <%--<p class="h5 text-primary">por comercio</p>--%>
      </td>
    </tr>
    <tr class="wow fadeInRight" data-wow-duration="2s" data-wow-delay="0.4s">
      <td>
        <h4 class="text-secondary">EURO</h4>
      </td>
      <td><span id="idEuro" runat="server"></span><%--<br />--%>
        <%--<p class="h5 text-primary">por comercio</p>--%>
      </td>
    </tr>
    <tr class="wow fadeInRight" data-wow-duration="2s" data-wow-delay="0.6s">
      <td>
        <h4 class="text-secondary">UF</h4>
      </td>
      <td><span id="idUF" runat="server"></span><%--<br />
        <p class="h5 text-primary">por comercio</p>--%>
      </td>
    </tr>
  </tbody>
</table>
