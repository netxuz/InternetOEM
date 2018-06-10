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
using OnlineServices.SystemData;
using OnlineServices.AppRuta;

namespace ICommunity.AppRuta
{
  public partial class appshowmap : System.Web.UI.Page
  {
    Web oWeb = new Web();
    string pCodUsuario = string.Empty;
    string pFecha = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
      oWeb.ValidaSessionAdm();
      if (!IsPostBack)
      {
        hddcodusuario.Value = oWeb.GetData("CodUsuario");
        hddfecha.Value = oWeb.GetData("pFecha");

        DBConn oConn = new DBConn();
        if (oConn.Open()) {
          SysUsuario oUsuario = new SysUsuario(ref oConn);
          oUsuario.CodUsuario = hddcodusuario.Value;
          DataTable dt = oUsuario.Get();
          if (dt != null) {
            if (dt.Rows.Count > 0) {
              lblMotorista.Text = dt.Rows[0]["nom_user"].ToString().ToUpper() + " " + dt.Rows[0]["ape_user"].ToString().ToUpper();
            }
          }
          dt = null;
        }
        oConn.Close();

        lblDia.Text = hddfecha.Value.Substring(6, 2) + "/" + hddfecha.Value.Substring(4, 2) + "/" + hddfecha.Value.Substring(0, 4);
      }
    }

    protected void rdActividad_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
      string ltorigen = string.Empty;
      string ltdestino = string.Empty;
      DataTable dt = null;

      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        cAppRegActividad oAppRegActividad = new cAppRegActividad(ref oConn);
        oAppRegActividad.IndFecha = hddfecha.Value;
        oAppRegActividad.CodUsuario = hddcodusuario.Value;
        dt = oAppRegActividad.GetRuta();
      }
      oConn.Close();

      rdActividad.DataSource = dt;

      if (dt != null)
      {
        if (dt.Rows.Count > 0)
        {
          StringBuilder js = new StringBuilder();
          if (dt.Rows.Count == 1)
          {
            //ltorigen = dt.Rows[0]["localizacion"].ToString();
            ltorigen = "{ lat: " + dt.Rows[0]["localizacion"].ToString().Substring(0, 10).Replace(",", ".") + ", lng: " + dt.Rows[0]["localizacion"].ToString().Substring(11, 10).Replace(",", ".") + " };";
            js.Append(" var origen = ").Append(ltorigen);
            js.Append(" var destino = ").Append(ltdestino);

            // Set destination, origin and travel mode.
            js.Append(" var request = { ");
            js.Append(" destination: destino, ");
            js.Append(" origin: origen, ");
            js.Append(" travelMode: 'DRIVING' ");
            js.Append(" }; ");
          }
          else if (dt.Rows.Count == 2)
          {
            //ltorigen = dt.Rows[0]["localizacion"].ToString();
            ltorigen = "{ lat: " + dt.Rows[0]["localizacion"].ToString().Substring(0, 10).Replace(",", ".") + ", lng: " + dt.Rows[0]["localizacion"].ToString().Substring(11, 10).Replace(",", ".") + " };";

            //ltdestino = dt.Rows[dt.Rows.Count - 1]["localizacion"].ToString();
            ltdestino = "{ lat: " + dt.Rows[dt.Rows.Count - 1]["localizacion"].ToString().Substring(0, 10).Replace(",", ".") + ", lng: " + dt.Rows[dt.Rows.Count - 1]["localizacion"].ToString().Substring(11, 10).Replace(",", ".") + " };";

            js.Append(" var origen = ").Append(ltorigen);
            js.Append(" var destino = ").Append(ltdestino);

            // Set destination, origin and travel mode.
            js.Append(" var request = { ");
            js.Append(" destination: destino, ");
            js.Append(" origin: origen, ");
            js.Append(" travelMode: 'DRIVING' ");
            js.Append(" }; ");
          }
          else if (dt.Rows.Count > 2)
          {
            //ltorigen = dt.Rows[0]["localizacion"].ToString();
            ltorigen = "{ lat: " + dt.Rows[0]["localizacion"].ToString().Substring(0, 10).Replace(",", ".") + ", lng: " + dt.Rows[0]["localizacion"].ToString().Substring(11, 10).Replace(",", ".") + " };";

            string[] waypts = new string[dt.Rows.Count - 2];
            for (int i = 0; i < dt.Rows.Count - 2; i++)
            {
              waypts[i] = "{ lat: " + dt.Rows[i + 1]["localizacion"].ToString().Substring(0, 10).Replace(",", ".") + ", lng: " + dt.Rows[i + 1]["localizacion"].ToString().Substring(11, 10).Replace(",", ".") + " }";
              //waypts[i] = dt.Rows[i+1]["localizacion"].ToString();
            }

            //ltdestino = dt.Rows[dt.Rows.Count - 1]["localizacion"].ToString();
            ltdestino = "{ lat: " + dt.Rows[dt.Rows.Count - 1]["localizacion"].ToString().Substring(0, 10).Replace(",", ".") + ", lng: " + dt.Rows[dt.Rows.Count - 1]["localizacion"].ToString().Substring(11, 10).Replace(",", ".") + " };";

            js.Append(" var origen = ").Append(ltorigen);
            js.Append(" var destino = ").Append(ltdestino);
            js.Append(" var waypts = [];");

            foreach (string sWay in waypts)
            {
              js.Append(" waypts.push({");
              js.Append(" location: ").Append(sWay).Append(",");
              js.Append(" stopover: true ");
              js.Append(" }); ");
            }

            // Set destination, origin and travel mode.
            js.Append(" var request = { ");
            js.Append(" destination: destino, ");
            js.Append(" origin: origen, ");
            js.Append(" waypoints: waypts, ");
            js.Append(" optimizeWaypoints: true, ");
            js.Append(" travelMode: 'DRIVING' ");
            js.Append(" }; ");

          }
          Page.ClientScript.RegisterStartupScript(this.GetType(), "fLoadMaps", js.ToString(), true);
        }
      }
      dt = null;
    }

    protected void btnVolver_Click(object sender, EventArgs e)
    {
      Response.Redirect(String.Format("applistactividad.aspx?CodUsuario={0}", hddcodusuario.Value));
    }
  }
}