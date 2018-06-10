using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using OnlineServices.Conn;
using OnlineServices.CmsData;
using OnlineServices.Method;

namespace ICommunity
{
  public partial class Zona : System.Web.UI.Page
  {
    Web oWeb = new Web();
    protected void Page_Load(object sender, EventArgs e)
    {
      oWeb.ValidaSessionAdm();
      if (!IsPostBack)
      {
        CodZona.Value = oWeb.GetData("CodZona");
        if (!string.IsNullOrEmpty(CodZona.Value))
        {
          DBConn oConn = new DBConn();
          if (oConn.Open())
          {
            CmsZona oZona = new CmsZona(ref oConn);
            oZona.CodZona = CodZona.Value;
            DataTable dZona = oZona.Get();
            if (dZona != null)
              if (dZona.Rows.Count > 0)
              {
                txtTitulo.Text = dZona.Rows[0]["nom_zona"].ToString();
                rdDescripcion.Content = dZona.Rows[0]["texto_zona"].ToString();
                rdCmbEstado.Items.FindItemByValue(dZona.Rows[0]["est_zona"].ToString()).Selected = true;
                chk_despliegue.Checked = (dZona.Rows[0]["ind_desp_cont"].ToString() == "V" ? true : false);
              }
            dZona = null;

            oConn.Close();
          }
        }
      }
    }

    protected void btnGrabar_Click(object sender, EventArgs e)
    {
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        oConn.BeginTransaction();
        string cPath = Server.MapPath(".") + @"\binary\";
        CmsZona oZona = new CmsZona(ref oConn);
        oZona.CodZona = CodZona.Value;
        oZona.NomZona = txtTitulo.Text;
        oZona.TextoZona = rdDescripcion.Content;
        oZona.EstZona = rdCmbEstado.SelectedValue;
        oZona.IndDespCont = (chk_despliegue.Checked == true ? "V" : "N");
        oZona.Accion = (string.IsNullOrEmpty(CodZona.Value) ? "CREAR" : "EDITAR");
        oZona.Put();
        CodZona.Value = oZona.CodZona;
        if (string.IsNullOrEmpty(oZona.Error))
        {
          oConn.Commit();
          string sFile = "Zona_" + oZona.CodZona + ".bin";
          oZona.SerializaZona(ref oConn, cPath, sFile);
        }
        else
          oConn.Rollback();

        oConn.Close();

      }
    }
  }
}
