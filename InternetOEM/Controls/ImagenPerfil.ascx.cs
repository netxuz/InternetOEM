using System;
using System.Text;
using System.IO;
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

using Telerik.Web.UI;
using OnlineServices.Conn;
using OnlineServices.Method;
using OnlineServices.SystemData;

namespace ICommunity.Controls
{
  public partial class ImagenPerfil : System.Web.UI.UserControl
  {
    Web oWeb = new Web();
    Culture oCulture = new Culture();
    protected void Page_Load(object sender, EventArgs e)
    {
      if ((Session["CodUsuarioPerfil"] != null) && (!string.IsNullOrEmpty(Session["CodUsuarioPerfil"].ToString()))) {

        RadBinaryImage oBinaryImage = new RadBinaryImage();
        oBinaryImage.CssClass = "imagenperfil";
        oBinaryImage.DataValue = oWeb.getImageProfileUser(Session["CodUsuarioPerfil"].ToString(), 300, 300);
        oBinaryImage.Width = Unit.Pixel(225);
        oBinaryImage.Height = Unit.Pixel(225);
        oBinaryImage.AutoAdjustImageControlSize = false;
        Controls.Add(oBinaryImage);

        //string cPath = Server.MapPath(".");
        //DataTable dUserArchivo = oWeb.DeserializarTbl(cPath, "UserArchivo_" + Session["CodUsuarioPerfil"].ToString() + ".bin");
        //if (dUserArchivo != null)
        //  if (dUserArchivo.Rows.Count > 0) {
        //    DataRow[] oRows = dUserArchivo.Select(" tip_archivo = 'P' ");
        //    if (oRows != null)
        //      if (oRows.Count() > 0) {
        //        StringBuilder sPath = new StringBuilder();
        //        sPath.Append(Server.MapPath("."));
        //        sPath.Append(@"\rps_onlineservice\");
        //        sPath.Append(@"\usuarios\");
        //        sPath.Append(@"\usuario_");
        //        sPath.Append(Session["CodUsuarioPerfil"].ToString());
        //        sPath.Append(@"\");
        //        sPath.Append(oRows[0]["nom_archivo"].ToString());
        //        FileStream fileStream = new FileStream(sPath.ToString(), FileMode.Open);

        //        RadBinaryImage oBinaryImage = new RadBinaryImage();
        //        oBinaryImage.CssClass = "imagenperfil";
        //        oBinaryImage.DataValue = oWeb.GetImageBytes(fileStream, 300, 300);
        //        fileStream.Close();
        //        Controls.Add(oBinaryImage);
        //      }
        //    oRows = null;
        //  }
        //dUserArchivo = null;
      }
    }
  }
}