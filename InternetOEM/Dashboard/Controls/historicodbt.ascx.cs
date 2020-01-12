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
  public partial class historicodbt__ : System.Web.UI.UserControl
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
          cDashboard oHistDBT = new cDashboard(ref oConn);
          oHistDBT.nKeyCliente = oIsUsuario.NKeyUsuario;
          oHistDBT.nKeyDeudor = oIsUsuario.NKeyDeudor;
          oHistDBT.CodHolding = oIsUsuario.NCodHolding;
          //Response.Write("GetDBT;" + DateTime.Now.Millisecond.ToString() + "<br>");
          DataTable dt = oHistDBT.GetDBT();
          //Response.Write("GetDBT;" + DateTime.Now.Millisecond.ToString() + "<br>");
          if (dt != null) {
            if (dt.Rows.Count > 0) {
              string sMes = string.Empty;
              string sMeses = string.Empty;
              string sDBT = string.Empty;
              string sData = "['Meses','Días', { role: 'annotation' } ]";
              foreach (DataRow oRow in dt.Rows)
              {
                sMes = oRow["periodo"].ToString().Trim();
                sData = sData + ",['" + sMes + "'," + Math.Round(double.Parse(oRow["dbt"].ToString()),0).ToString() + ",'" + Math.Round(double.Parse(oRow["dbt"].ToString()),0).ToString("N0") +  "']";
              }

              StringBuilder sHtml = new StringBuilder();
              sHtml.Append(" google.charts.setOnLoadCallback(drawRightY); ");
              sHtml.Append(" function drawRightY() { ");
              sHtml.Append(" var data = google.visualization.arrayToDataTable([ ");
              sHtml.Append(sData);
              sHtml.Append(" ]); ");
              sHtml.Append(" var materialOptions = { annotations: { alwaysOutside: true, textStyle: { fontSize: 12, auraColor: 'none', color: '#fff' }, boxStyle: { stroke: '#79B6CD', strokeWidth: 1, rx: 10, ry: 10, gradient: { color1: '#79B6CD', color2: '#79B6CD', x1: '0%', y1: '0%', x2: '100%', y2: '100%' } } }, legend: { position: 'top', textStyle: { color: '#c5c5c5', fontName: 'Lato', fontSize: 13, bold: true, } }, vAxis: { format: 'short', textStyle: { color: '#c5c5c5', fontName: 'Lato', fontSize: 12, bold: true } }, hAxis: { textStyle: { color: '#91BBDB', fontName: 'Lato', fontSize: 13, bold: true } }, colors: ['#A5E6FE', '#A5E6FE', '#A5E6FE', '#A5E6FE', '#A5E6FE'], bars: 'horizontal', axes: { y: { 0: { side: 'right' } } } }; ");
              sHtml.Append(" var materialChart = new google.visualization.BarChart(document.getElementById('horizontalBar')); ");
              sHtml.Append(" materialChart.draw(data, materialOptions); } ");

              ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "horizontalBar", sHtml.ToString(), true);
            }
          }
          dt = null;

          dt = oHistDBT.GetCondicionPago();
          if (dt != null)
          {
            if (dt.Rows.Count > 0)
            {
              lb_sla_dbt.Text = dt.Rows[0]["condicion_pago"].ToString() + " días";
            }
          }
          dt = null;

        }
        oConn.Close();
      }
    }
  }
}