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
  public partial class pastduecritico : System.Web.UI.UserControl
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
        {
          string sPastDue = string.Empty;
          string sDocOverDue = string.Empty;
          string sPastDueCritico = string.Empty;
          string sDocOverDueCritico = string.Empty;

          if (oConn.Open())
          {
            cDashboard oPastDue = new cDashboard(ref oConn);
            oPastDue.nKeyCliente = oIsUsuario.NKeyUsuario;
            oPastDue.nKeyDeudor = oIsUsuario.NKeyDeudor;
            oPastDue.CodHolding = oIsUsuario.NCodHolding;

            double iTotalPastDue = oPastDue.getTotalPastDue();
            double iPastDueCritico = oPastDue.getPastDueCritico();

            string sMes = DateTime.Now.Month.ToString();
            sMes = ((sMes.Length > 1) ? sMes : " " + sMes);

            oPastDue.Periodo = DateTime.Now.AddYears(-1).Year.ToString() + "-" + sMes;
            double iTotalPastDueHist = oPastDue.getTotalPastDueHist();
            double iPastDueCriticoHist = oPastDue.getPastDueCriticoHist();

            lb_monto_total.Text = oIsUsuario.Moneda + Math.Truncate(iTotalPastDue).ToString("N0");
            lb_monto_critico.Text = oIsUsuario.Moneda + Math.Truncate(iPastDueCritico).ToString("N0");
            lb_monto_total_hist.Text = oIsUsuario.Moneda + Math.Truncate(iTotalPastDueHist).ToString("N0");
            lb_monto_critico_hist.Text = oIsUsuario.Moneda + Math.Truncate(iPastDueCriticoHist).ToString("N0");

            lb_variacion_total.Text =  ((iTotalPastDue > 0 && iTotalPastDueHist > 0)? (((iTotalPastDue - iTotalPastDueHist) / iTotalPastDueHist) * 100).ToString("0.00", CultureInfo.InvariantCulture) + "%" : "");
            lb_variacion_critico_hist.Text = ((iPastDueCritico > 0 && iPastDueCriticoHist > 0)? (((iPastDueCritico - iPastDueCriticoHist) / iPastDueCriticoHist) * 100).ToString("0.00", CultureInfo.InvariantCulture) + "%" : "");

            string sPeriodo1 = oWeb.getMes(DateTime.Now.Month).Substring(0, 3) + ' ' + DateTime.Now.Year.ToString();
            lb_mesano_actual.Text = sPeriodo1;

            string sPeriodo2 = oWeb.getMes(DateTime.Now.Month).Substring(0, 3) + ' ' + DateTime.Now.AddYears(-1).Year.ToString();
            lb_mesano_ant.Text = sPeriodo2;

            StringBuilder sHtml = new StringBuilder();
            sHtml.Append(" google.charts.setOnLoadCallback(drawChartPasDue); ");
            sHtml.Append(" function drawChartPasDue() { ");
            sHtml.Append(" var data = google.visualization.arrayToDataTable([ ");
            sHtml.Append(" ['PERIODO','Total', 'Crítico'], ");
            sHtml.Append(" ['" + sPeriodo1 + "'," + iTotalPastDue.ToString("0.00", CultureInfo.InvariantCulture) + "," + iPastDueCritico.ToString("0.00", CultureInfo.InvariantCulture) + "], ");
            sHtml.Append(" ['" + sPeriodo2 + "'," + iTotalPastDueHist.ToString("0.00", CultureInfo.InvariantCulture) + "," + iPastDueCriticoHist.ToString("0.00", CultureInfo.InvariantCulture) + "] ");
            sHtml.Append(" ]); ");
            sHtml.Append(" var options = { ");
            sHtml.Append(" legend: { position: 'bottom', textStyle: { color: '#c5c5c5', fontName: 'Lato', fontSize: 13, bold: true, } }, ");
            sHtml.Append(" vAxis: { format: 'short', textStyle: { color: '#639ECC', fontName: 'Lato', fontSize: 14, bold: true } }, ");
            sHtml.Append(" hAxis: { textStyle: { color: '#ACACAC', fontName: 'Lato', fontSize: 10, bold: true } }, ");
            sHtml.Append(" colors: ['#7ED321', '#6498FF'], ");
            sHtml.Append(" bars: 'horizontal' ");
            sHtml.Append(" }; ");
            sHtml.Append(" var chart = new google.charts.Bar(document.getElementById('chartPasDue'));  ");
            sHtml.Append(" chart.draw(data, google.charts.Bar.convertOptions(options)); ");
            sHtml.Append(" } ");

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "chartPasDue", sHtml.ToString(), true);
          }
          oConn.Close();

          bEnable = true;

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
      }
      else {
        idVistaEnable.Visible = false;
        idVistaNoEnable.Visible = true;
      }
    }
  }
}