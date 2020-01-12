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
  public partial class comportamientofinanciero : System.Web.UI.UserControl
  {
    private OnlineServices.Method.Web oWeb = new OnlineServices.Method.Web();
    private OnlineServices.Method.Usuario oIsUsuario;

    protected void Page_Load(object sender, EventArgs e)
    {
      bool bEnable = false;
      calendario_pago_nodias.Visible = false;
      oIsUsuario = oWeb.ValidaUserAppReport();
      if ((!string.IsNullOrEmpty(oIsUsuario.NKeyDeudor)) && (!string.IsNullOrEmpty(oIsUsuario.NKeyUsuario)) || (!string.IsNullOrEmpty(oIsUsuario.NCodHolding)))
      {
        DBConn oConn = new DBConn();
        if (oConn.Open())
        {
          string nKeyCodigoDeudor = string.Empty;
          string sDias = string.Empty;
          string sSemanas = string.Empty;
          string sValor = string.Empty;
          string sTipo = string.Empty;

          cDashboard oCalendarioPago = new cDashboard(ref oConn);
          oCalendarioPago.nKeyCliente = oIsUsuario.NKeyUsuario;
          oCalendarioPago.nKeyDeudor = oIsUsuario.NKeyDeudor;
          oCalendarioPago.CodHolding = oIsUsuario.NCodHolding;
          oCalendarioPago.Periodo = DateTime.Now.ToString("yyyyMMdd");
          //Response.Write("getCalendarioPago;" + DateTime.Now.Millisecond.ToString() + "<br>");
          DataTable dt = oCalendarioPago.getCalendarioPago();
          //Response.Write("getCalendarioPago;" + DateTime.Now.Millisecond.ToString() + "<br>");
          if (dt != null)
          {
            if (dt.Rows.Count > 0)
            {
              lbl_compromiso_pago.Text = "Si";
              lbl_fecha.Text = DateTime.Parse(dt.Rows[0]["fecha"].ToString()).ToString("dd-MM-yyyy");
              lblMonto.Text = oIsUsuario.Moneda + " " + (!string.IsNullOrEmpty(dt.Rows[0]["Monto"].ToString()) ? double.Parse(dt.Rows[0]["Monto"].ToString()).ToString("N0") : "0");
            }
            else
            {
              lbl_compromiso_pago.Text = "No";
              lbl_fecha.Text = "N/A";
              lblMonto.Text = "N/A";
            }
          }
          dt = null;
          //Response.Write("GetCumplidas;" + DateTime.Now.Millisecond.ToString() + "<br>");
          double iCumplido = oCalendarioPago.GetCumplidas();
          //Response.Write("GetCumplidas;" + DateTime.Now.Millisecond.ToString() + "<br>");
          lbl_cumplido.Text = iCumplido.ToString();

          //Response.Write("GetNoCumplidas;" + DateTime.Now.Millisecond.ToString() + "<br>");
          double iNoComplido = oCalendarioPago.GetNoCumplidas();
          //Response.Write("GetNoCumplidas;" + DateTime.Now.Millisecond.ToString() + "<br>");
          lbl_no_cumplido.Text = iNoComplido.ToString();

          //Response.Write("Get_nKeyCodigoDeudor;" + DateTime.Now.Millisecond.ToString() + "<br>");
          nKeyCodigoDeudor = oCalendarioPago.Get_nKeyCodigoDeudor();
          //Response.Write("Get_nKeyCodigoDeudor;" + DateTime.Now.Millisecond.ToString() + "<br>");
          if (!string.IsNullOrEmpty(nKeyCodigoDeudor))
          {
            oCalendarioPago.nKeyCodigoDeudor = nKeyCodigoDeudor;
            //Response.Write("getDiaPago;" + DateTime.Now.Millisecond.ToString() + "<br>");
            DataTable dtt = oCalendarioPago.getDiaPago();
            //Response.Write("getDiaPago;" + DateTime.Now.Millisecond.ToString() + "<br>");
            if (dtt != null)
            {
              if (dtt.Rows.Count > 0)
              {
                foreach (DataRow oRow in dtt.Rows)
                {
                  sTipo = oRow["tipo"].ToString();
                  if (sTipo == "1")
                  {
                    sDias = "Día";
                    sSemanas = (!string.IsNullOrEmpty(sSemanas) ? sSemanas + ", " + oRow["valor"].ToString() : oRow["valor"].ToString());
                  }
                  else
                  {
                    sDias = oRow["dia_letra"].ToString();
                    sValor = (!string.IsNullOrEmpty(sValor) ? sValor + "," + oRow["valor"].ToString() : oRow["valor"].ToString());
                    sSemanas = (!string.IsNullOrEmpty(sSemanas) ? sSemanas + ", " + oRow["valor"].ToString() + "°" : oRow["valor"].ToString() + "°");
                  }
                }
              }
            }
            dtt = null;
          }

          lbl_dias.Text = string.Empty;
          lbl_semana.Text = string.Empty;
          if ((!string.IsNullOrEmpty(sDias)) || (!string.IsNullOrEmpty(sSemanas)))
          {
            lbl_dias.Text = sDias;
            if (sTipo == "1")
              lbl_semana.Text = sSemanas;
            else
            {
              if (sValor != "1,2,3,4")
                lbl_semana.Text = sSemanas;
            }
          }
          else
          {
            calendario_pago_sidias.Visible = false;
            calendario_pago_nodias.Visible = true;
          }
          double iPorcentaje = 0;
          string sPorcentaje = string.Empty;
          if ((iNoComplido != 0) || (iCumplido != 0))
          {
            iPorcentaje = ((iCumplido / (iNoComplido + iCumplido)) * 100);
            sPorcentaje = iPorcentaje.ToString("N0") + "%";
            //lblIndiceCredibilidad.Text = sPorcentaje;
          }
          else
          {
            sPorcentaje = "0%";
            //lblIndiceCredibilidad.Text = sPorcentaje;
          }
          string sCanvas = "<canvas id=\"myCanvas\" width=\"50\" height=\"50\"></canvas>";
          string sClassBackGroudColor = string.Empty;
          string sStyleBoxRiesgo = string.Empty;
          string sStyleTxtRiesgo = string.Empty;
          string sIndexRiesgo = string.Empty;
          string sColor = string.Empty;
          if (iPorcentaje <= 20)
          {
            sIndexRiesgo = "2";
            sColor = "#D0021B";
            sClassBackGroudColor = "sbackgroudcolorbar_rojo";
            sStyleBoxRiesgo = "box-riesgo-rojo";
            sStyleTxtRiesgo = "lbl-riesgo-rojo";
            id_rojo.Controls.Add(new LiteralControl(sCanvas));
          }
          else if ((iPorcentaje > 20) && (iPorcentaje <= 40))
          {
            sIndexRiesgo = "4";
            sColor = "#F57F23";
            sClassBackGroudColor = "sbackgroudcolorbar_naranjo";
            sStyleBoxRiesgo = "box-riesgo-naranjo";
            sStyleTxtRiesgo = "lbl-riesgo-naranjo";
            id_naranjo.Controls.Add(new LiteralControl(sCanvas));
          }
          else if ((iPorcentaje > 40) && (iPorcentaje <= 60)) {
            sIndexRiesgo = "6";
            sColor = "#F5D423";
            sClassBackGroudColor = "sbackgroudcolorbar_amarillo";
            sStyleBoxRiesgo = "box-riesgo-amarillo";
            sStyleTxtRiesgo = "lbl-riesgo-amarillo";
            id_amarillo.Controls.Add(new LiteralControl(sCanvas));
          }
          else if ((iPorcentaje > 60) && (iPorcentaje <= 80))
          {
            sIndexRiesgo = "8";
            sColor = "#7ED321";
            sClassBackGroudColor = "sbackgroudcolorbar_verdeclaro";
            sStyleBoxRiesgo = "box-riesgo-verdeclaro";
            sStyleTxtRiesgo = "lbl-riesgo-verdeclaro";
            id_verdeclaro.Controls.Add(new LiteralControl(sCanvas));
          }
          else if ((iPorcentaje > 80) && (iPorcentaje <= 100))
          {
            sIndexRiesgo = "10";
            sColor = "#4EA528";
            sClassBackGroudColor = "sbackgroudcolorbar_verdeoscuro";
            sStyleBoxRiesgo = "box-riesgo-verdeoscuro";
            sStyleTxtRiesgo = "lbl-riesgo-verdeoscuro";
            id_verdeocscuro.Controls.Add(new LiteralControl(sCanvas));
          }

          StringBuilder sHtml = new StringBuilder();
          sHtml.Append("var canvas = document.getElementById('myCanvas');");
          sHtml.Append("var context = canvas.getContext('2d');");
          sHtml.Append("var centerX = canvas.width / 2;");
          sHtml.Append("var centerY = canvas.height / 2;");
          sHtml.Append("var radius = 20;");
          sHtml.Append("context.beginPath();");
          sHtml.Append("context.arc(centerX, centerY, radius, 0, 2 * Math.PI, false);");
          sHtml.Append("context.fillStyle = '" + sColor + "';");
          sHtml.Append("context.fill();");
          sHtml.Append("context.lineWidth = 5;");
          sHtml.Append("context.strokeStyle = '#fff';");
          sHtml.Append("context.stroke();");
          sHtml.Append("context.lineWidth = 1;");
          sHtml.Append("context.fillStyle = \"#fff\";");
          sHtml.Append("context.lineStyle = \"#fff\";");
          sHtml.Append("context.font = \"12px Lato\";");
          sHtml.Append("context.fillText('" + sPorcentaje + "', 15, 30);");

          string sBoxRiesgo = "<div class=\""+ sClassBackGroudColor + "\"><span class=\"index-riesgo\">" + sIndexRiesgo + "</span></div>";
          //idBox_riesgo.Controls.Add(new LiteralControl(sBoxRiesgo));

          string sTt_riesgo = "<span class=\"" + sStyleBoxRiesgo + "\">Nivel de riesgo del cliente</span>";
          //idtit_riesgo.Controls.Add(new LiteralControl(sTt_riesgo));

          ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "comportamiento-financiero", sHtml.ToString(), true);

          //string sProgress = "<div class=\"progress-bar " + sClassBackGroudColor + "\" role=\"progressbar\" style=\"width:" + sPorcentaje + "\" aria-valuenow=\"25\" aria-valuemin=\"0\" aria-valuemax=\"100\">" + sPorcentaje + "</div>";
          //oBarIndiceCredibilidad.Controls.Add(new LiteralControl(sProgress));
        }
        oConn.Close();
      }
    }
  }
}