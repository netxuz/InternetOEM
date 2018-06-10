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
using System.IO;
using Telerik.Web.UI;
using OnlineServices.Method;

namespace ICommunity.Controls
{
  public partial class GalleryImgContent : System.Web.UI.UserControl
  {
    Web oWeb = new Web();
    private string pContenido = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
      StringBuilder sPath;
      StringBuilder sUrlImg;
      FileStream filestream;
      RadBinaryImage oImge;
      pContenido = this.Attributes["pContenido"];

      string cPath = Server.MapPath(".");
      DataTable dContenido = oWeb.DeserializarTbl(cPath, "ContenidoArchivo_" + pContenido + ".bin");
      if (dContenido != null)
      {
        if (dContenido.Rows.Count > 0)
        {
          DataRow[] oRows = dContenido.Select(" ext_archivo in ('.jpg','.bmp','.gif', '.tiff', '.png') ");
          if (oRows.Count() > 0)
          {
            foreach (DataRow oRow in oRows) {
              GalleryContent.Controls.Add(new LiteralControl("<li>"));

              sUrlImg = new StringBuilder();
              sUrlImg.Append("<a href=\"rps_onlineservice/contenido/contenido_").Append(pContenido).Append("/").Append(oRow["nom_archivo"].ToString()).Append("\">");
              GalleryContent.Controls.Add(new LiteralControl(sUrlImg.ToString()));
              
              sPath = new StringBuilder();
              sPath.Append(HttpContext.Current.Server.MapPath("."));
              sPath.Append(@"\rps_onlineservice\");
              sPath.Append(@"\contenido\");
              sPath.Append(@"\contenido_");
              sPath.Append(pContenido);
              sPath.Append(@"\");
              sPath.Append(oRow["nom_archivo"].ToString());

              filestream = new FileStream(sPath.ToString(), FileMode.Open);
              oImge = new RadBinaryImage();
              oImge.DataValue = oWeb.GetImageBytes(filestream, 70, 70);
              filestream.Close();
              GalleryContent.Controls.Add(oImge);

              //sUrlImg = new StringBuilder();
              //sUrlImg.Append("<img src=\"rps_onlineservice/contenido/contenido_").Append(pContenido).Append("/").Append(oRow["nom_archivo"].ToString()).Append("\">");
              //GalleryContent.Controls.Add(new LiteralControl(sUrlImg.ToString()));

              GalleryContent.Controls.Add(new LiteralControl("</a>"));

              GalleryContent.Controls.Add(new LiteralControl("</li>"));
            }
          }
          oRows = null;
        }
      }
      dContenido = null;
    }
  }
}