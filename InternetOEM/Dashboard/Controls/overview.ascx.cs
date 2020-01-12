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
  public partial class Overview : System.Web.UI.UserControl
  {
    private OnlineServices.Method.Web oWeb = new OnlineServices.Method.Web();
    private OnlineServices.Method.Usuario oIsUsuario;

    protected void Page_Load(object sender, EventArgs e)
    {
      bool bEnable = false;
      bool bLCA = false;
      oIsUsuario = oWeb.ValidaUserAppReport();
      if ((!string.IsNullOrEmpty(oIsUsuario.NKeyDeudor)) && (!string.IsNullOrEmpty(oIsUsuario.NKeyUsuario)) || (!string.IsNullOrEmpty(oIsUsuario.NCodHolding)))
      {
        double iConsumido = 0;
        double iDisponible = 0;
        double iLCA = 0;
        DBConn oConn = new DBConn();
        if (oConn.Open())
        {
          cDashboard oOverView = new cDashboard(ref oConn);
          oOverView.CodHolding = oIsUsuario.NCodHolding;
          oOverView.nKeyCliente = oIsUsuario.NKeyUsuario;
          oOverView.nKeyDeudor = oIsUsuario.NKeyDeudor;
          //Response.Write("GetLCAConsumido;" + DateTime.Now.Millisecond.ToString() + "<br>");
          DataTable dtLCA_Consumido = oOverView.GetLCAConsumido();
          //Response.Write("GetLCAConsumido;" + DateTime.Now.Millisecond.ToString() + "<br>");
          if (dtLCA_Consumido != null)
          {
            if (dtLCA_Consumido.Rows.Count > 0)
            {
              iConsumido = double.Parse(dtLCA_Consumido.Rows[0]["consumido"].ToString());
              tt_consumido.Text = oIsUsuario.Moneda + " " + Math.Truncate(double.Parse(dtLCA_Consumido.Rows[0]["consumido"].ToString())).ToString("N0");
            }
          }
          dtLCA_Consumido = null;

          //Response.Write("GetLCADisponible;" + DateTime.Now.Millisecond.ToString() + "<br>");
          DataTable dtLCA_Disponible = oOverView.GetLCADisponible();
          //Response.Write("GetLCADisponible;" + DateTime.Now.Millisecond.ToString() + "<br>");
          if (dtLCA_Disponible != null)
          {
            if (dtLCA_Disponible.Rows.Count > 0)
            {
              iLCA = double.Parse(dtLCA_Disponible.Rows[0]["disponible"].ToString());
              iDisponible = iLCA - iConsumido;
              tt_disponible.Text = oIsUsuario.Moneda + " " + Math.Truncate(iDisponible).ToString("N0");
            }
          }
          dtLCA_Consumido = null;

          double iPorcentaje = 0;
          string sProgress = string.Empty;
          string sPorcentaje = string.Empty;
          lblPorcentaje.CssClass = "percentaje-overview";
          if ((iConsumido != 0) && (iLCA != 0))
          {
            bLCA = true;
            bEnable = true;
            lblTitle.Text = "Línea de Crédito";
            lblConsumido.Text = "Consumido";
            lblDisponible.Text = "Disponible";
            iPorcentaje = Math.Round((iConsumido / iLCA) * 100, 0);
            sPorcentaje = iPorcentaje.ToString() + "%";
            lblPorcentaje.Text = sPorcentaje;
            if (iDisponible <= 0)
            {
              if (iPorcentaje <= -100000)
                lblPorcentaje.CssClass = "percentaje-overview-red-low";
              else if(iPorcentaje >= 100000)
                lblPorcentaje.CssClass = "percentaje-overview-red-low";
              else
                lblPorcentaje.CssClass = "percentaje-overview-red";
            }
            else {
              if (iPorcentaje >= 100000)
                lblPorcentaje.CssClass = "percentaje-overview-low";
              else
                lblPorcentaje.CssClass = "percentaje-overview";
            }
          }
          else {
            string sMes = "00" + DateTime.Now.Month.ToString();
            sMes = sMes.Substring(sMes.Length - 2);

            oOverView.Periodo = sMes + "-01-" + DateTime.Now.Year.ToString();
            //Response.Write("GetEstimadoCaja;" + DateTime.Now.Millisecond.ToString() + "<br>");
            DataTable dtEstimadoCaja = oOverView.GetEstimadoCaja();
            //Response.Write("GetEstimadoCaja;" + DateTime.Now.Millisecond.ToString() + "<br>");
            if (dtEstimadoCaja != null) {
              if (dtEstimadoCaja.Rows.Count > 0) {
                bEnable = true;

                iConsumido = double.Parse(dtEstimadoCaja.Rows[0]["estimado"].ToString());
                tt_consumido.Text = oIsUsuario.Moneda + " " + Math.Truncate(double.Parse(dtEstimadoCaja.Rows[0]["estimado"].ToString())).ToString("N0");

                iDisponible = double.Parse(dtEstimadoCaja.Rows[0]["real"].ToString());
                tt_disponible.Text = oIsUsuario.Moneda + " " + Math.Truncate(double.Parse(dtEstimadoCaja.Rows[0]["real"].ToString())).ToString("N0");

                lblTitle.Text = "Estimado Caja";
                lblConsumido.Text = "Estimado";
                lblDisponible.Text = "Real";
                if ((iConsumido != 0) && (iDisponible != 0))
                {
                  iPorcentaje = Math.Round((iDisponible / iConsumido) * 100, 0);
                  if (iPorcentaje >= 100){
                    if (iPorcentaje >= 100000)
                      lblPorcentaje.CssClass = "percentaje-overview-crema-low";
                    else
                      lblPorcentaje.CssClass = "percentaje-overview-crema";
                  }
                  else {
                    if (iPorcentaje <= -100000)
                      lblPorcentaje.CssClass = "percentaje-overview-low";
                    else
                      lblPorcentaje.CssClass = "percentaje-overview";
                  }

                  sPorcentaje = iPorcentaje.ToString() + "%";
                }
                else {
                  sPorcentaje = "0%";
                }
                lblPorcentaje.Text = sPorcentaje;
              }
            }
            dtEstimadoCaja = null;
          }

          StringBuilder sHtml_Graph = new StringBuilder();
          sHtml_Graph.Append(" google.charts.setOnLoadCallback(drawChart); ");
          sHtml_Graph.Append(" function drawChart() { ");
          sHtml_Graph.Append(" var data = google.visualization.arrayToDataTable([ ");
          sHtml_Graph.Append(" ['Porcentaje','Dato'], ");
          sHtml_Graph.Append(" ['" + lblConsumido.Text +  "'," + ((iConsumido >= 0) ? iConsumido.ToString("0.00", CultureInfo.InvariantCulture) : "0") + "], ");
          sHtml_Graph.Append(" ['" + lblDisponible.Text + "'," + ((iDisponible > 0) ? iDisponible.ToString("0.00", CultureInfo.InvariantCulture) : "0") + "], ");
          sHtml_Graph.Append(" ]); ");
          sHtml_Graph.Append(" var options = { ");
          sHtml_Graph.Append(" pieHole: 0.9, ");
          sHtml_Graph.Append(" pieSliceText:'none', pieSliceTextStyle: 'none', pieSliceBorderColor: 'none', ");
          if (bLCA)
            sHtml_Graph.Append(" colors: ['" + ((iDisponible <= 0) ? "#D0021B" : "#1578FF") + "', '#FFDFAB'], ");
          else {
            if (iPorcentaje >= 100) {
              sHtml_Graph.Append(" colors: ['#FFDFAB', '#FFDFAB'], ");
            } else if (iPorcentaje <= 0) {
              sHtml_Graph.Append(" colors: ['#1578FF', '#1578FF'], ");
            }
            else
            {
              sHtml_Graph.Append(" colors: ['#1578FF', '#FFDFAB'], ");
            }
          }
            
          sHtml_Graph.Append(" legend: 'none', chartArea: { left: 10, right: 10, bottom: 10, top: 10 } ");
          sHtml_Graph.Append(" }; ");
          sHtml_Graph.Append(" var chart = new google.visualization.PieChart(document.getElementById('donut_single')); ");
          sHtml_Graph.Append(" chart.draw(data, options); ");
          sHtml_Graph.Append(" }; ");

          ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "donut_single", sHtml_Graph.ToString(), true);

          //sProgress = "<div class=\"progress-bar\" role=\"progressbar\" style=\"width:" + sPorcentaje + "\" aria-valuenow=\"25\" aria-valuemin=\"0\" aria-valuemax=\"100\">" + sPorcentaje + "</div>";
          //oProgressBar.Controls.Add(new LiteralControl(sProgress));

          string sTipoPago = string.Empty;
          //Response.Write("GetMonto_TipoPago;" + DateTime.Now.Millisecond.ToString() + "<br>");
          DataTable dt_Monto_TipoPago = oOverView.GetMonto_TipoPago();
          //Response.Write("GetMonto_TipoPago;" + DateTime.Now.Millisecond.ToString() + "<br>");
          if (dt_Monto_TipoPago != null) {
            if (dt_Monto_TipoPago.Rows.Count > 0) {
              lb_Monto_abono.Text = oIsUsuario.Moneda + " " + Math.Truncate(double.Parse(dt_Monto_TipoPago.Rows[0]["monto_ultimo_abono"].ToString())).ToString("N0");
              lb_tipo_pago.Text = dt_Monto_TipoPago.Rows[0]["sTipoPago"].ToString(); 
            }
          }
          dt_Monto_TipoPago = null;

          //Response.Write("GetUltimoDocPago;" + DateTime.Now.Millisecond.ToString() + "<br>");
          DataTable dt_UltDocPago = oOverView.GetUltimoDocPago();
          //Response.Write("GetUltimoDocPago;" + DateTime.Now.Millisecond.ToString() + "<br>");
          if (dt_UltDocPago != null)
          {
            if (dt_UltDocPago.Rows.Count > 0)
            {
              bEnable = true;
              string dUltimoAbono = string.Empty;

              switch (dt_UltDocPago.Rows[0]["descripcion_via_pago"].ToString())
              {
                case "Depósito":
                  dUltimoAbono = DateTime.Parse(dt_UltDocPago.Rows[0]["dfechaVencimiento"].ToString()).ToString("dd-MM-yyyy");
                  break;
                default:
                  dUltimoAbono = DateTime.Parse(dt_UltDocPago.Rows[0]["dFechaIngreso"].ToString()).ToString("dd-MM-yyyy");
                  break;
              }

              lbl_UltAbono.Text = dUltimoAbono;
              

              if (!string.IsNullOrEmpty(dt_UltDocPago.Rows[0]["seguro"].ToString()))
              {
                switch (dt_UltDocPago.Rows[0]["seguro"].ToString())
                {
                  case "0":
                    lb_Seguro_Credito.Text = "No";
                    break;
                  case "1":
                    lb_Seguro_Credito.Text = "Si";
                    break;
                }
              }

              if (bLCA) {
                idStatus.Visible = true;
                string sEstatus = dt_UltDocPago.Rows[0]["estatus"].ToString();
                string sColorIcono = string.Empty;
                StringBuilder sHtml = new StringBuilder();
                sHtml.Append(" <div class=\"col-md-12 col_status_overview\"> ");

                sHtml.Append("<span class=\"tt-status\">Status Línea de crédito: </span><span class=\"lbl-dt-status\">").Append(sEstatus.Trim()).Append("</span>");

                sHtml.Append("</div>");
                idStatus.Controls.Add(new LiteralControl(sHtml.ToString()));
              }              

              lb_Via_Pago.Text = dt_UltDocPago.Rows[0]["descripcion_via_pago"].ToString();
              lb_Canal.Text = dt_UltDocPago.Rows[0]["canal"].ToString();
            }
          }
          dt_UltDocPago = null;
        }
        oConn.Close();

        if (bEnable)
        {
          idOverviewEnable.Visible = true;
          idOverviewNoEnable.Visible = false;
        }
        else
        {
          idOverviewEnable.Visible = false;
          idOverviewNoEnable.Visible = true;
        }
      }
      else {
        idOverviewEnable.Visible = false;
        idOverviewNoEnable.Visible = false;
      }
    }
  }
}
