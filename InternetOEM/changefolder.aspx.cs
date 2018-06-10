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
using OnlineServices.Method;

namespace ICommunity
{
  public partial class changefolder : System.Web.UI.Page
  {
    Web oWeb = new Web();
    protected void Page_Load(object sender, EventArgs e)
    {
      DataTable dUsuario = oWeb.DeserializarTbl(Server.MapPath("."), "Usuarios.bin");
      StringBuilder sFolder = new StringBuilder();
      sFolder.Append(HttpContext.Current.Server.MapPath("."));
      sFolder.Append(@"\rps_onlineservice\escorts\");
      if (Directory.Exists(sFolder.ToString()))
      {
        if (dUsuario != null)
        {
          StringBuilder sSubFolder1;
          StringBuilder sSubFolder2;
          foreach (DataRow oRows in dUsuario.Rows)
          {
            sSubFolder1 = new StringBuilder();
            sSubFolder1.Append(sFolder.ToString());
            sSubFolder1.Append(@"\usuario_").Append(oRows["cod_usuario"].ToString());

            sSubFolder2 = new StringBuilder();
            sSubFolder2.Append(sFolder.ToString());
            sSubFolder2.Append(@"\escort_").Append(oRows["cod_usuario"].ToString());
            if (Directory.Exists(sSubFolder1.ToString()))
            {
              Directory.Move(sSubFolder1.ToString(), sSubFolder2.ToString());
            }
          }
        }
        dUsuario = null;
      }
    }
  }
}
