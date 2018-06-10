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
using System.IO;
using System.Text;

using OnlineServices.Conn;
using OnlineServices.SystemData;
using OnlineServices.Method;

namespace ICommunity
{
  public partial class Upload : System.Web.UI.Page
  {
    Web oWeb = new Web();
    protected void Page_Load(object sender, EventArgs e)
    {
      OnlineServices.Method.Usuario oIsUsuario;
      oIsUsuario = oWeb.GetObjUsuario();
      HttpContext.Current.Response.ContentType = "text/plain";
      HttpContext.Current.Response.Expires = -1;
      try {
        HttpPostedFile postedFile = HttpContext.Current.Request.Files["Filedata"];
        StringBuilder sPath = new StringBuilder();
        sPath.Append(Server.MapPath("."));
        sPath.Append(@"\rps_onlineservice\");
        sPath.Append(@"\escorts\");
        sPath.Append(@"\escort_");
        sPath.Append(oIsUsuario.CodUsuario);
        if (!Directory.Exists(sPath.ToString()))
          Directory.CreateDirectory(sPath.ToString());

        sPath.Append(@"\" + postedFile.FileName);
        postedFile.SaveAs(sPath.ToString());
        HttpContext.Current.Response.Write("rps_onlineservice/" + postedFile.FileName);
        HttpContext.Current.Response.StatusCode = 200;

        DBConn oConn = new DBConn();
        if (oConn.Open())
        {
          SysArchivosUsuarios oArchivosUsuarios = new SysArchivosUsuarios(ref oConn);
          oArchivosUsuarios.Accion = "CREAR";
          oArchivosUsuarios.CodUsuario = oIsUsuario.CodUsuario;
          oArchivosUsuarios.DateArchivo = DateTime.Now.ToString();
          oArchivosUsuarios.NomArchivo = postedFile.FileName;
          oArchivosUsuarios.Put();

          if (string.IsNullOrEmpty(oArchivosUsuarios.Error))
          {
            string cPath = Server.MapPath(".") + @"\binary\";
            string sFileUsrArchivo = "UserArchivo_" + oIsUsuario.CodUsuario + ".bin";
            oArchivosUsuarios.SerializaTblUserArchivo(ref oConn, cPath, sFileUsrArchivo);
          }

          oConn.Close();
        }
      }
      catch (Exception ex)
      {
        HttpContext.Current.Response.Write("Error: " + ex.Message);
      }

    }

    public new bool IsReusable
    {
      get
      {
        return false;
      }
    }

  }
}
