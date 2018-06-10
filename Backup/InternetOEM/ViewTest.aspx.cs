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
using System.Text;

using OnlineServices.Method;
using OnlineServices.AppData;
using OnlineServices.SystemData;
using ICommunity.Controls;

namespace ICommunity
{
  public partial class ViewTest : System.Web.UI.Page
  {
    private OnlineServices.Method.Usuario oIsUsuario;
    private string pCodUsuario = string.Empty;
    private Web oWeb = new Web();
    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
        getImagesUser(oWeb.GetData("CodUsuario"));
      
    }

    protected void getImagesUser(string pCodUsuario)
    {
      StringBuilder sHref;
      //Image oImage;
      HtmlGenericControl oDivSlider = (HtmlGenericControl)this.FindControl("imgserv");
      string cPath = Server.MapPath(".");
      DataTable dUserArchivo = oWeb.DeserializarTbl(cPath, "UserArchivo_" + pCodUsuario + ".bin");
      if (dUserArchivo != null)
        if (dUserArchivo.Rows.Count > 0)
        {
          int iCount = 1;
          foreach (DataRow oRow in dUserArchivo.Rows)
          {
            oDivSlider.Controls.Add(new LiteralControl("<li>"));
            sHref = new StringBuilder();
            sHref.Append("<a class=\"thumb\" href=\"rps_onlineservice/usuarios/usuario_").Append(oRow["cod_usuario"].ToString()).Append("/").Append(oRow["nom_archivo"].ToString()).Append("\" title=\"Title #").Append(iCount.ToString()).Append("\">");
            oDivSlider.Controls.Add(new LiteralControl(sHref.ToString()));
            oDivSlider.Controls.Add(new LiteralControl("Title #" + iCount.ToString()));
            oDivSlider.Controls.Add(new LiteralControl("</a>"));
            oDivSlider.Controls.Add(new LiteralControl("</li>"));
            iCount++;
          }
        }
      dUserArchivo = null;
    }
  }
}
