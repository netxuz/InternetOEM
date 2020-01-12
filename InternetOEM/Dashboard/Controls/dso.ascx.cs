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
  public partial class dso : System.Web.UI.UserControl
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
          cDashboard oDso = new cDashboard(ref oConn);
          oDso.nKeyCliente = oIsUsuario.NKeyUsuario;
          oDso.nKeyDeudor = oIsUsuario.NKeyDeudor;
          oDso.CodHolding = oIsUsuario.NCodHolding;
          oDso.Ano = DateTime.Now.Year.ToString();
          //Response.Write("GetDso;" + DateTime.Now.Millisecond.ToString()+"<br>");
          DataTable dt = oDso.GetDso();
          //Response.Write("GetDso;" + DateTime.Now.Millisecond.ToString() + "<br>");
          if (dt != null)
          {
            if (dt.Rows.Count > 0)
            {
              bEnable = true;
              string sMeses = string.Empty;
              string sMes = string.Empty;
              string sDso = string.Empty;

              string sData = "['Perido', 'Días']";
              foreach (DataRow oRow in dt.Rows)
              {
                sMes = oRow["periodo"].ToString().Trim();
                sData = sData + ",['" + sMes + "'," + double.Parse(oRow["dso"].ToString()).ToString("0.00", CultureInfo.InvariantCulture) + "]";
              }

              StringBuilder sHtml = new StringBuilder();
              sHtml.Append(" google.charts.setOnLoadCallback(drawChartDso); ");
              sHtml.Append(" function drawChartDso() { ");
              sHtml.Append(" var data = google.visualization.arrayToDataTable([ ");
              sHtml.Append(sData);
              sHtml.Append(" ]); ");
              sHtml.Append(" var options = { legend: { position: 'bottom', textStyle: { color: '#c5c5c5', fontName: 'Lato', fontSize: 13, bold: true, } }, vAxis: { format: 'short', textStyle: { color: '#c5c5c5', fontName: 'Lato', fontSize: 12, bold: true } }, hAxis: { textStyle: { color: '#629DCC', fontName: 'Lato', fontSize: 10, bold: true } }, colors: ['#F5A624'], pointSize: 10, backgroundColor: '#fff' }; ");
              sHtml.Append(" var chart = new google.visualization.AreaChart(document.getElementById('lineChart1')); ");
              sHtml.Append(" chart.draw(data, options); } ");

              ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "lineChart1", sHtml.ToString(), true);
            }
          }
          dt = null;

          dt = oDso.GetSlaDso();
          if (dt != null) {
            if (dt.Rows.Count > 0) {
              lb_sla_dso.Text = dt.Rows[0]["SLA_DSO"].ToString() + " " + dt.Rows[0]["unidad"].ToString();
            }
          }
          dt = null;          
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
      else {
        idVistaEnable.Visible = false;
        idVistaNoEnable.Visible = true;
      }
    }
  }
}