using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

using OnlineServices.Conn;
using OnlineServices.Method;
using OnlineServices.Dashboard;

namespace ICommunity.Dashboard.Controls
{
  public partial class estadovsreal : System.Web.UI.UserControl
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
          if (oConn.Open())
          {
            string sMes = ("00" + DateTime.Now.AddMonths(-1).Month.ToString()).Substring(("00" + DateTime.Now.AddMonths(-1).Month.ToString()).Length - 2);

            double iEstimadoMesAnterior = 0;
            double iReaMesAnterior = 0;
            cDashboard oEstadimadoVsReal = new cDashboard(ref oConn);
            oEstadimadoVsReal.nKeyCliente = oIsUsuario.NKeyUsuario;
            oEstadimadoVsReal.nKeyDeudor = oIsUsuario.NKeyDeudor;
            oEstadimadoVsReal.CodHolding = oIsUsuario.NCodHolding;
            oEstadimadoVsReal.Periodo = sMes + "-01-" + (sMes == "12" ? DateTime.Now.AddYears(-1).Year.ToString() : DateTime.Now.Year.ToString());
            //Response.Write("GetEstimadoVsReal;" + DateTime.Now.Millisecond.ToString() + "<br>");
            DataTable dt = oEstadimadoVsReal.GetEstimadoVsReal();
            //Response.Write("GetEstimadoVsReal;" + DateTime.Now.Millisecond.ToString() + "<br>");
            if (dt != null)
            {
              if (dt.Rows.Count > 0)
              {
                bEnable = true;
                iEstimadoMesAnterior = double.Parse(dt.Rows[0]["estimado"].ToString());
                iReaMesAnterior = double.Parse(dt.Rows[0]["real"].ToString());
              }
            }
            dt = null;

            oEstadimadoVsReal.Periodo = (sMes == "12" ? DateTime.Now.AddYears(-1).Year.ToString() : DateTime.Now.Year.ToString()) + sMes;
            //Response.Write("GetFacturado;" + DateTime.Now.Millisecond.ToString() + "<br>");
            double iFacturadoMesAnterior = oEstadimadoVsReal.GetFacturado();
            //Response.Write("GetFacturado;" + DateTime.Now.Millisecond.ToString() + "<br>");

            sMes = ("00" + DateTime.Now.Month.ToString()).Substring(("00" + DateTime.Now.Month.ToString()).Length - 2);

            double iEstimadoMes = 0;
            double iReaMes = 0;
            oEstadimadoVsReal.Periodo = sMes + "-01-" + DateTime.Now.Year.ToString();
            //Response.Write("GetEstimadoVsReal;" + DateTime.Now.Millisecond.ToString() + "<br>");
            dt = oEstadimadoVsReal.GetEstimadoVsReal();
            //Response.Write("GetEstimadoVsReal;" + DateTime.Now.Millisecond.ToString() + "<br>");
            if (dt != null)
            {
              if (dt.Rows.Count > 0)
              {
                bEnable = true;
                iEstimadoMes = double.Parse(dt.Rows[0]["estimado"].ToString());
                iReaMes = double.Parse(dt.Rows[0]["real"].ToString());
              }
            }
            dt = null;

            oEstadimadoVsReal.Periodo = DateTime.Now.Year.ToString() + sMes;
            //Response.Write("GetFacturado;" + DateTime.Now.Millisecond.ToString() + "<br>");
            float iFacturado = oEstadimadoVsReal.GetFacturado();
            //Response.Write("GetFacturado;" + DateTime.Now.Millisecond.ToString() + "<br>");

            string sdata = "['PERIODO', 'Facturado', { type: 'string', role: 'annotation' }, 'Estimado', { type: 'string', role: 'annotation' }, 'Real', { type: 'string', role: 'annotation' }],";
            sdata = sdata + "['Anterior'," + Math.Round(iFacturadoMesAnterior,0).ToString() + ",'" + ((iFacturadoMesAnterior > 0)? Math.Round((iFacturadoMesAnterior/1000000),0).ToString() :"0") + "M'," + Math.Round(iEstimadoMesAnterior,0).ToString() + ",'" + ((iEstimadoMesAnterior > 0) ? Math.Round((iEstimadoMesAnterior / 1000000),0).ToString() : "0") + "M'," + Math.Round(iReaMesAnterior,0).ToString() + ",'" + ((iReaMesAnterior > 0) ? Math.Round((iReaMesAnterior / 1000000),0).ToString() : "0") + "M'],";
            sdata = sdata + "['Actual'," + Math.Round(iFacturado,0).ToString() + ",'" + ((iFacturado > 0) ? Math.Round((iFacturado / 1000000),0).ToString() : "0") + "M'," + Math.Round(iEstimadoMes,0).ToString() + ",'" + ((iEstimadoMes > 0) ? Math.Round((iEstimadoMes / 1000000),0).ToString() : "0") + "M'," + Math.Round(iReaMes,0).ToString() + ",'" + ((iReaMes > 0) ? Math.Round((iReaMes / 1000000),0).ToString() : "0") + "M']";

            StringBuilder sHtml = new StringBuilder();

            sHtml.Append(" google.charts.setOnLoadCallback(drawlineChart2); ");
            sHtml.Append(" function drawlineChart2() { ");
            sHtml.Append(" var data = google.visualization.arrayToDataTable([ ");
            sHtml.Append(sdata);
            sHtml.Append(" ]); ");

            sHtml.Append(" var options = { annotations: { alwaysOutside: true, textStyle: { fontSize: 12, auraColor: 'none', color: '#fff' }, boxStyle: { stroke: '#79B6CD', strokeWidth: 1, rx: 10, ry: 10, gradient: { color1: '#79B6CD', color2: '#79B6CD', x1: '0%', y1: '0%', x2: '100%', y2: '100%' } } }, legend: { position: 'bottom', textStyle: { color: '#c5c5c5', fontName: 'Lato', fontSize: 13, bold: true, } }, vAxis: { format: 'short', textStyle: { color: '#c5c5c5', fontName: 'Lato', fontSize: 12, bold: true } }, hAxis: { textStyle: { color: '#c5c5c5', fontName: 'Lato', fontSize: 13, bold: true } }, colors: ['#BAE1EF', '#A5E6FE', '#6498FF'] }; ");

            sHtml.Append(" var chart = new google.visualization.ColumnChart(document.getElementById('lineChart2')); ");
            sHtml.Append(" chart.draw(data, options); ");
            sHtml.Append(" } ");

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "lineChart2", sHtml.ToString(), true);

            string sDia = ("00" + DateTime.Now.Day.ToString()).Substring(("00" + DateTime.Now.Day.ToString()).Length - 2);


            oEstadimadoVsReal.Periodo = DateTime.Now.Year.ToString() + sMes + sDia;
            //Response.Write("GetTotal;" + DateTime.Now.Millisecond.ToString() + "<br>");
            double itotal = oEstadimadoVsReal.GetTotal();
            //Response.Write("GetTotal;" + DateTime.Now.Millisecond.ToString() + "<br>");
            double iDeudaVencida = oEstadimadoVsReal.GetDeudaVencida();
            //Response.Write("GetDeudaVencida;" + DateTime.Now.Millisecond.ToString() + "<br>");
            double iDeudaDocumentada = oEstadimadoVsReal.GetDeudaDocumentada();
            //Response.Write("GetDeudaDocumentada;" + DateTime.Now.Millisecond.ToString() + "<br>");
            double iFacturasMayor30 = oEstadimadoVsReal.GetFacturasMayor30();
            //Response.Write("GetFacturasMayor30;" + DateTime.Now.Millisecond.ToString() + "<br>");
            double iFacturasMayor30SinLitigio = oEstadimadoVsReal.GetFacturasMayor30SinLitigio();
            //Response.Write("GetFacturasMayor30SinLitigio;" + DateTime.Now.Millisecond.ToString() + "<br>");

            lblTotal.Text = oIsUsuario.Moneda + " " + Math.Truncate(itotal).ToString("N0");
            lblVencidas.Text = oIsUsuario.Moneda + " " + Math.Truncate(iDeudaVencida).ToString("N0");
            lblDeudaDocumentada.Text = oIsUsuario.Moneda + " " + Math.Truncate(iDeudaDocumentada).ToString("N0");
            lblVencidas30.Text = oIsUsuario.Moneda + " " + Math.Truncate(iFacturasMayor30).ToString("N0");
            lblVencidas30SinLitigios.Text = oIsUsuario.Moneda + " " + Math.Truncate(iFacturasMayor30SinLitigio).ToString("N0");

            //if ((iEstimadoMesAnterior != 0) || (iReaMesAnterior != 0) || (iEstimadoMes != 0) || (iReaMes != 0) || (itotal != 0) || (iDeudaVencida != 0) || (iFacturasMayor30 != 0) || (iFacturasMayor30SinLitigio != 0))
            //  bEnable = true;
          }
          oConn.Close();
        }
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