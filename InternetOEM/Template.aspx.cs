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
  public partial class Template1 : System.Web.UI.Page
  {
    Web oWeb = new Web();
    protected void Page_Load(object sender, EventArgs e)
    {
      oWeb.ValidaSessionAdm();
      if (!IsPostBack)
      {
        CodTemplate.Value = oWeb.GetData("CodTemplate");
        if (!string.IsNullOrEmpty(CodTemplate.Value))
        {
          DBConn oConn = new DBConn();
          if (oConn.Open())
          {
            CmsTemplate oTemplate = new CmsTemplate(ref oConn);
            oTemplate.CodTemplate = CodTemplate.Value;
            DataTable dTemplate = oTemplate.Get();
            if (dTemplate != null)
              if (dTemplate.Rows.Count > 0)
              {
                txtTitulo.Text = dTemplate.Rows[0]["nom_template"].ToString();
                rdDescripcion.Content = dTemplate.Rows[0]["texto_template"].ToString();
                rdCmbEstado.Items.FindByValue(dTemplate.Rows[0]["est_template"].ToString()).Selected = true;
              }
            dTemplate = null;

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
        CmsTemplate oTemplate = new CmsTemplate(ref oConn);
        oTemplate.CodTemplate = CodTemplate.Value;
        oTemplate.NomTemplate = txtTitulo.Text;
        oTemplate.TextoTemplate = rdDescripcion.Content;
        oTemplate.EstTemplate = rdCmbEstado.SelectedValue;
        oTemplate.Accion = (string.IsNullOrEmpty(CodTemplate.Value) ? "CREAR" : "EDITAR");
        oTemplate.Put();
        CodTemplate.Value = oTemplate.CodTemplate;
        if (string.IsNullOrEmpty(oTemplate.Error))
        {
          oConn.Commit();
          string sFile = "Template_" + oTemplate.CodTemplate + ".bin";
          oTemplate.SerializaTemplate(ref oConn, cPath, sFile);
        }
        else {
          oConn.Rollback();
        }

        oConn.Close();

      }
    }

  }
}
