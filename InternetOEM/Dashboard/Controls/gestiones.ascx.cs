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
  public partial class gestiones : System.Web.UI.UserControl
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
          cDashboard oGestiones = new cDashboard(ref oConn);
          oGestiones.nKeyCliente = oIsUsuario.NKeyUsuario;
          oGestiones.nKeyDeudor = oIsUsuario.NKeyDeudor;
          oGestiones.CodHolding = oIsUsuario.NCodHolding;
          //Response.Write("GetGestiones;" + DateTime.Now.Millisecond.ToString() + "<br>");
          DataTable dt = oGestiones.GetGestiones();
          //Response.Write("GetGestiones;" + DateTime.Now.Millisecond.ToString() + "<br>");
          if (dt != null)
          {
            if (dt.Rows.Count > 0)
            {
              bEnable = true;
              gdGestiones.DataSource = dt;
              gdGestiones.DataBind();
            }
            else
            {
              bEnable = false;
              getDummyTable();
            }
          }
          else
          {
            bEnable = false;
            getDummyTable();
          }
        }
        oConn.Close();

        if (bEnable)
        {
          idGestionesEnable.Visible = true;
          idGestionesNoEnable.Visible = false;
        }
        else
        {
          idGestionesEnable.Visible = false;
          idGestionesNoEnable.Visible = true;
        }
      }
      else {
        idGestionesEnable.Visible = false;
        idGestionesNoEnable.Visible = false;
      }      
    }

    protected void getDummyTable()
    {
      DataTable dt = new DataTable();
      dt.Clear();
      dt.Columns.Add("Fecha");
      dt.Columns.Add("sTipoGestion");
      dt.Columns.Add("sObservacion");
      dt.Rows.Add();
      gdGestiones.DataSource = dt;
      gdGestiones.DataBind();
    }

  }
}