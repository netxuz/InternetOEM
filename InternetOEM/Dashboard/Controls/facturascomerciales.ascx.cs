using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Globalization;

using OnlineServices.Conn;
using OnlineServices.Method;
using OnlineServices.Dashboard;

namespace ICommunity.Dashboard.Controls
{
  public partial class facturascomerciales : System.Web.UI.UserControl
  {
    private OnlineServices.Method.Web oWeb = new OnlineServices.Method.Web();
    private OnlineServices.Method.Usuario oIsUsuario;

    protected void Page_Load(object sender, EventArgs e)
    {
      oIsUsuario = oWeb.ValidaUserAppReport();
      if ((!string.IsNullOrEmpty(oIsUsuario.NKeyDeudor)) && (!string.IsNullOrEmpty(oIsUsuario.NKeyUsuario)) || (!string.IsNullOrEmpty(oIsUsuario.NCodHolding)))
      {
        DBConn oConn = new DBConn();
        if (oConn.Open())
        {
          double iDiscrecionalesMes = 0;
          double iAcuerdosComerciales = 0;
          double iCantDoc = 0;
          double iAcuerdosComercialesAno = 0;
          double iTotal = 0;
          cDashboard oFacturasComerciales = new cDashboard(ref oConn);
          oFacturasComerciales.nKeyCliente = oIsUsuario.NKeyUsuario;
          oFacturasComerciales.nKeyDeudor = oIsUsuario.NKeyDeudor;
          oFacturasComerciales.CodHolding = oIsUsuario.NCodHolding;
          iDiscrecionalesMes = oFacturasComerciales.getDiscrecionalesMes();
          iAcuerdosComerciales = oFacturasComerciales.getAcuerdosComercialesMes();

          string sData = "['Tipo', 'Monto', { role: 'annotation' }, { role: 'style' }],";
          sData = sData + "['Discrecionales'," + Math.Round(iDiscrecionalesMes, 0).ToString() + ",'" + Math.Round(iDiscrecionalesMes, 0).ToString("N0") + "','#F57F23'],";
          sData = sData + "['Acuerdos Comerciales'," + Math.Round(iAcuerdosComerciales,0).ToString() + ",'" + Math.Round(iAcuerdosComerciales, 0).ToString("N0") + "','#9AD558']";

          StringBuilder sHtml = new StringBuilder();
          sHtml.Append(" google.charts.setOnLoadCallback(drawBarChart); ");
          sHtml.Append(" function drawBarChart() { ");
          sHtml.Append(" var data = google.visualization.arrayToDataTable([ ");
          sHtml.Append(sData);
          sHtml.Append(" ]); ");
          sHtml.Append(" var Options = { annotations: { textStyle: { fontSize: 12, auraColor: 'none', color: '#ffffff', bold: true } }, legend: \"none\", vAxis: { textPosition: 'none' }, hAxis: { format: 'short', textStyle: { color: '#c5c5c5', fontName: 'Lato', fontSize: 10, bold: true } } }; ");
          sHtml.Append(" var oChart = new google.visualization.BarChart(document.getElementById('horizontalBar2')); ");
          sHtml.Append(" oChart.draw(data, Options); } ");

          ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "horizontalBar2", sHtml.ToString(), true);

          DataTable dt = oFacturasComerciales.getNoDiscrecionalAno();
          if (dt != null)
          {
            if (dt.Rows.Count > 0)
            {
              iCantDoc = double.Parse(dt.Rows[0]["documentos"].ToString());
              idDocumentos.Text = iCantDoc.ToString("N0");
              iAcuerdosComercialesAno = double.Parse(dt.Rows[0]["acuerdos_comerciales"].ToString());
              idAcuerdosComerciales.Text = oIsUsuario.Moneda + " " + iAcuerdosComercialesAno.ToString("N0");
            }
          }
          dt = null;

          iTotal = oFacturasComerciales.getTotalAcuerdosComercialesAno();

          if ((iAcuerdosComercialesAno > 0) && (iTotal > 0))
          {
            idBudget.Text = ((iAcuerdosComercialesAno / iTotal) * 100).ToString("N0") + "%";
          }
          else
          {
            idBudget.Text = "N/A";
          }
        }
        oConn.Close();
        idVistaEnable.Visible = true;
        idVistaNoEnable.Visible = false;
      }
      else
      {
        idVistaEnable.Visible = false;
        idVistaNoEnable.Visible = true;
      }
    }
  }
}