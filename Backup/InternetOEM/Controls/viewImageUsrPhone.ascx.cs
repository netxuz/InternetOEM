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

namespace ICommunity.Controls
{
  public partial class viewImageUsrPhone : System.Web.UI.UserControl
  {
    private string pCodUsuario = string.Empty;
    private Web oWeb = new Web();

    protected void Page_Load(object sender, EventArgs e)
    {
      pCodUsuario = oWeb.GetData("CodUsuario");
      getImagesUser(Session["CodUsuarioPerfil"].ToString());
    }

    protected void getImagesUser(string pCodUsuario)
    {
      StringBuilder sHref;
      Image oImage;
      HtmlGenericControl oDivSlider = (HtmlGenericControl)this.FindControl("GaleryPhone");
      string cPath = Server.MapPath(".");
      DataTable dUserArchivo = oWeb.DeserializarTbl(cPath, "UserArchivo_" + pCodUsuario + ".bin");
      if (dUserArchivo != null)
        if (dUserArchivo.Rows.Count > 0)
        {
          oDivSlider.Controls.Add(new LiteralControl("<ul id=\"Gallery\" class=\"gallery\">"));
          foreach (DataRow oRow in dUserArchivo.Rows)
          {
            oDivSlider.Controls.Add(new LiteralControl("<li>"));
            sHref = new StringBuilder();
            sHref.Append("<a href=\"rps_onlineservice/escorts/escort_").Append(oRow["cod_usuario"].ToString()).Append("/").Append(oRow["nom_archivo"].ToString()).Append("\">");
            oDivSlider.Controls.Add(new LiteralControl(sHref.ToString()));
            oImage = new Image();
            oImage.ID = oRow["cod_archivo"].ToString();
            oImage.ImageUrl = "~/rps_onlineservice/escorts/escort_" + oRow["cod_usuario"].ToString() + "/" + oRow["nom_archivo"].ToString();
            oImage.Width = Unit.Pixel(82);
            oDivSlider.Controls.Add(oImage);
            oDivSlider.Controls.Add(new LiteralControl("</a>"));
            oDivSlider.Controls.Add(new LiteralControl("</li>"));
          }
          oDivSlider.Controls.Add(new LiteralControl("</ul>"));
        }
      dUserArchivo = null;
    }
  }
}