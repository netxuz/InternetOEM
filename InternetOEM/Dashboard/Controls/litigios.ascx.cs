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
  public partial class litigios : System.Web.UI.UserControl
  {
    private OnlineServices.Method.Web oWeb = new OnlineServices.Method.Web();
    private OnlineServices.Method.Usuario oIsUsuario;

    protected void Page_Load(object sender, EventArgs e)
    {
      bool bEnable = false;
      oIsUsuario = oWeb.ValidaUserAppReport();
      if ((!string.IsNullOrEmpty(oIsUsuario.NKeyDeudor)) && (!string.IsNullOrEmpty(oIsUsuario.NKeyUsuario)) || (!string.IsNullOrEmpty(oIsUsuario.NCodHolding)))
      {
        string s30 = string.Empty;
        string s60 = string.Empty;
        string s90 = string.Empty;
        string sMayor = string.Empty;
        string sDiasProvision = string.Empty;

        DBConn oConn = new DBConn();
        if (oConn.Open())
        {
          DataTable dt = null;
          cDashboard oLitigios = new cDashboard(ref oConn);
          oLitigios.nKeyCliente = oIsUsuario.NKeyUsuario;
          oLitigios.nKeyDeudor = oIsUsuario.NKeyDeudor;
          oLitigios.CodHolding = oIsUsuario.NCodHolding;
          oLitigios.Periodo = "30";
          //Response.Write("GetLitigios 30;" + DateTime.Now.Millisecond.ToString() + "<br>");
          dt = oLitigios.GetLitigios();
          //Response.Write("GetLitigios 30;" + DateTime.Now.Millisecond.ToString() + "<br>");
          if (dt != null)
          {
            if (dt.Rows.Count > 0)
            {
              bEnable = true;
              s30 = dt.Rows[0]["saldo"].ToString();
              lbl_30.Text = oIsUsuario.Moneda + " " + double.Parse(s30).ToString("N0");
            }
          }
          dt = null;
          oLitigios.Periodo = "60";
          //Response.Write("GetLitigios 60;" + DateTime.Now.Millisecond.ToString() + "<br>");
          dt = oLitigios.GetLitigios();
          //Response.Write("GetLitigios 60;" + DateTime.Now.Millisecond.ToString() + "<br>");
          if (dt != null)
          {
            if (dt.Rows.Count > 0)
            {
              bEnable = true;
              s60 = dt.Rows[0]["saldo"].ToString();
              lbl_60.Text = oIsUsuario.Moneda + " " + double.Parse(s60).ToString("N0");
            }
          }
          dt = null;
          oLitigios.Periodo = "90";
          //Response.Write("GetLitigios 90;" + DateTime.Now.Millisecond.ToString() + "<br>");
          dt = oLitigios.GetLitigios();
          //Response.Write("GetLitigios 90;" + DateTime.Now.Millisecond.ToString() + "<br>");
          if (dt != null)
          {
            if (dt.Rows.Count > 0)
            {
              bEnable = true;
              s90 = dt.Rows[0]["saldo"].ToString();
              lbl_90.Text = oIsUsuario.Moneda + " " + double.Parse(s90).ToString("N0");
            }
          }
          dt = null;
          oLitigios.Periodo = "mayor";
          //Response.Write("GetLitigios mayor;" + DateTime.Now.Millisecond.ToString() + "<br>");
          dt = oLitigios.GetLitigios();
          //Response.Write("GetLitigios mayor;" + DateTime.Now.Millisecond.ToString() + "<br>");
          if (dt != null)
          {
            if (dt.Rows.Count > 0)
            {
              bEnable = true;
              sMayor = dt.Rows[0]["saldo"].ToString();
              lbl_mayor.Text = oIsUsuario.Moneda + " " + double.Parse(sMayor).ToString("N0");
            }
          }
          dt = null;

          double iResultado = 0;
          //Response.Write("GetDiasProcesoNormalizacion;" + DateTime.Now.Millisecond.ToString() + "<br>");
          dt = oLitigios.GetDiasProcesoNormalizacion();
          //Response.Write("GetDiasProcesoNormalizacion;" + DateTime.Now.Millisecond.ToString() + "<br>");
          if (dt != null)
          {
            if (dt.Rows.Count > 0)
            {
              bEnable = true;
              iResultado = double.Parse(dt.Compute(" sum(resultado) ", string.Empty).ToString());
              sDiasProvision = (iResultado / dt.Rows.Count).ToString("N0");
              lblDiasProvision.Text = sDiasProvision + " días";
            }
          }
          dt = null;

        }
        oConn.Close();

        //if (((!string.IsNullOrEmpty(s30)) && (s30 != "0")) || ((!string.IsNullOrEmpty(s60)) && (s60 != "0")) || ((!string.IsNullOrEmpty(s90)) && (s90 != "0")) || ((!string.IsNullOrEmpty(sMayor))&&(sMayor != "0")) || ((!string.IsNullOrEmpty(sDiasProvision)) && (sDiasProvision != "0")) )

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