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
using OnlineServices.Conn;
using OnlineServices.SystemData;
using OnlineServices.Method;

namespace ICommunity
{
  public partial class Usuario : System.Web.UI.Page
  {
    Culture oCulture = new Culture();
    Web oWeb = new Web();
    protected void Page_Load(object sender, EventArgs e)
    {
      if (!string.IsNullOrEmpty(oWeb.GetData("CodUsuario")))
        getImagenes(oWeb.GetData("CodUsuario"));
    }
    protected void getImagenes(string sCodUsuario) {
      DBConn oConn = new DBConn();
      if (oConn.Open()) {
        SysArchivosUsuarios ArchivosUsuarios = new SysArchivosUsuarios(ref oConn);
        ArchivosUsuarios.CodUsuario = sCodUsuario;
        DataTable dArchivos = ArchivosUsuarios.Get();
        if (dArchivos != null)
          if (dArchivos.Rows.Count > 0) {
            RadBinaryImage oBinaryImage;
            FileStream fileStream;
            StringBuilder sPath;
            loadImage.Controls.Add(new LiteralControl("<div id=\"bloqimgs\">"));
            foreach (DataRow oRow in dArchivos.Rows) {
              sPath = new StringBuilder();
              sPath.Append(HttpContext.Current.Server.MapPath("."));
              sPath.Append(@"\rps_onlineservice\");
              sPath.Append(@"\escorts\");
              sPath.Append(@"\escort_");
              sPath.Append(sCodUsuario);
              sPath.Append(@"\");
              sPath.Append(oRow["nom_archivo"].ToString());
              fileStream = new FileStream(sPath.ToString(), FileMode.Open);

              oBinaryImage = new RadBinaryImage();
              oBinaryImage.DataValue = oWeb.GetImageBytes(fileStream, 100, 100);
              oBinaryImage.Width = Unit.Pixel(50);
              oBinaryImage.AutoAdjustImageControlSize = false;
              fileStream.Close();

              loadImage.Controls.Add(new LiteralControl("<div class=\"imgUsrList\">"));
              loadImage.Controls.Add(oBinaryImage);
              loadImage.Controls.Add(new LiteralControl("</div>"));
            }
            loadImage.Controls.Add(new LiteralControl("</div>"));
          }
        dArchivos = null;
        oConn.Close();
      }

    }

  }
}