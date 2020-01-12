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
  public partial class provision : System.Web.UI.UserControl
  {
    private OnlineServices.Method.Web oWeb = new OnlineServices.Method.Web();
    private OnlineServices.Method.Usuario oIsUsuario;

    protected void Page_Load(object sender, EventArgs e)
    {
      bool bEnable = false;
      oIsUsuario = oWeb.ValidaUserAppReport();
      if ((!string.IsNullOrEmpty(oIsUsuario.NKeyDeudor)) && (!string.IsNullOrEmpty(oIsUsuario.NKeyUsuario)) || (!string.IsNullOrEmpty(oIsUsuario.NCodHolding)))
      {
        DBConn oConn = new DBConn();
        if (oConn.Open())
        {
          string sProvision = string.Empty;
          string sImpactoProvision = string.Empty;
          string sProvisionAcumulada = string.Empty;
          cDashboard oProvision = new cDashboard(ref oConn);
          oProvision.nKeyCliente = oIsUsuario.NKeyUsuario;
          oProvision.nKeyDeudor = oIsUsuario.NKeyDeudor;
          oProvision.CodHolding = oIsUsuario.NCodHolding;
          //Response.Write("GetProvision;" + DateTime.Now.Millisecond.ToString() + "<br>");
          sProvision = oProvision.GetProvision();
          //Response.Write("GetImpactoProvision;" + DateTime.Now.Millisecond.ToString() + "<br>");
          sImpactoProvision = oProvision.GetImpactoProvision();
          //Response.Write("GetProvisionAcumulada;" + DateTime.Now.Millisecond.ToString() + "<br>");
          sProvisionAcumulada = oProvision.GetProvisionAcumulada();

          if ((!string.IsNullOrEmpty(sImpactoProvision)) && (!string.IsNullOrEmpty(sProvisionAcumulada)))
          {
            bEnable = true;
            lblProvision.Text = (!string.IsNullOrEmpty(sProvision) ? oIsUsuario.Moneda + " " + double.Parse(sProvision).ToString("N0") + " M": "0");
            lblImpactoProvision.Text = (!string.IsNullOrEmpty(sImpactoProvision) ? oIsUsuario.Moneda + " " + double.Parse(sImpactoProvision).ToString("N0") : "0");
            lblProvisionAcumulada.Text = (!string.IsNullOrEmpty(sProvisionAcumulada)? oIsUsuario.Moneda + " " + (double.Parse(sProvisionAcumulada) * 1000000).ToString("N0") : "0");
            double iProvisionDisponible = (double.Parse(sProvisionAcumulada) * 1000000) - double.Parse(sImpactoProvision);

            string sData = "['TIPO', 'MONTO'],";
            sData = sData + "['Impacto Provisión'," + Math.Round(double.Parse(sImpactoProvision), 2).ToString("0.00", CultureInfo.InvariantCulture) + "],";
            sData = sData + "['Provisión Acumulada'," + Math.Round(iProvisionDisponible, 2).ToString("0.00", CultureInfo.InvariantCulture) + "]";

            StringBuilder sHtml = new StringBuilder();
            sHtml.Append(" google.charts.setOnLoadCallback(drawChartProvison); ");
            sHtml.Append(" function drawChartProvison() { ");
            sHtml.Append(" var data = google.visualization.arrayToDataTable([ ");
            sHtml.Append(sData);
            sHtml.Append(" ]); ");
            sHtml.Append(" var options = { pieHole: 0.5, pieSliceText:'none', pieSliceTextStyle: 'none', pieSliceBorderColor: 'none', legend: 'none', colors: ['#D0021B', '#7ED321'], chartArea: { left: 10, right: 10, bottom: 10, top: 10 } }; ");
            sHtml.Append(" var chart = new google.visualization.PieChart(document.getElementById('pieChart')); ");
            sHtml.Append(" chart.draw(data, options); } ");

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "pieChart", sHtml.ToString(), true);
          }
        }
        oConn.Close();
        if (bEnable)
        {
          idVistaEnable.Visible = true;
          idVistaNoEnable.Visible = false;
        }
        else
        {
          idVistaEnable.Visible = false;
          idVistaNoEnable.Visible = true;
        }
      }
      else
      {
        idVistaEnable.Visible = false;
        idVistaNoEnable.Visible = true;
      }
    }
  }
}