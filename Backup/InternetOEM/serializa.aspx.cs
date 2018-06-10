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
using OnlineServices.Method;
using OnlineServices.SystemData;

namespace ICommunity
{
  public partial class serializa : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        SysArchivosUsuarios oArchivosUsuarios;
        SysUsuario oUsuario = new SysUsuario(ref oConn);
        DataTable dUsuario = oUsuario.Get();
        if (dUsuario != null) {
          foreach (DataRow oRow in dUsuario.Rows) {
            oArchivosUsuarios = new SysArchivosUsuarios(ref oConn);
            oArchivosUsuarios.CodUsuario = oRow["cod_usuario"].ToString();
            string cPath = HttpContext.Current.Server.MapPath(".") + @"\binary\";
            string sFileUsrArchivo = "UserArchivo_" + oRow["cod_usuario"].ToString() + ".bin";
            oArchivosUsuarios.SerializaTblUserArchivo(ref oConn, cPath, sFileUsrArchivo);
          }
        }
        dUsuario = null;

      }
      oConn.Close();
    }
  }
}
