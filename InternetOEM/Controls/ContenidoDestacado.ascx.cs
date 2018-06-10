using System;
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
using System.Text;

using OnlineServices.Conn;
using OnlineServices.SystemData;
using OnlineServices.Method;


namespace ICommunity.Controls
{
  public partial class ContenidoDestacado : System.Web.UI.UserControl
  {
    private string pCodNodo = string.Empty;
    Web oWeb = new Web();

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!string.IsNullOrEmpty(this.Attributes["CodNodo"]))
        pCodNodo = this.Attributes["CodNodo"];

      Controls.Add(new LiteralControl("<div id=\"vDestacados\">"));

      string cPath = Server.MapPath(".");
      DataTable dContenidos = oWeb.DeserializarTbl(cPath, "Contenidos.bin");
      if (dContenidos != null)
      {
        DataRow[] oRows = dContenidos.Select(" dest_contenido = 1 ");
        if (oRows != null)
          foreach (DataRow oRow in oRows) {
            Controls.Add(new LiteralControl("<div id=\"destcss_" + oRow["cod_contenido"].ToString() + "\">"));
            Controls.Add(new LiteralControl("<div class=\"titContenido\">"));
            Controls.Add(new LiteralControl(oRow["titulo_contenido"].ToString()));
            Controls.Add(new LiteralControl("</div>"));
            Controls.Add(new LiteralControl("<div class=\"datResumen\">"));
            Controls.Add(new LiteralControl(oRow["resumen_contenido"].ToString()));
            Controls.Add(new LiteralControl("</div>"));
            Controls.Add(new LiteralControl("</div>"));
          }
        oRows = null;
      }
      dContenidos = null;

      Controls.Add(new LiteralControl("</div>"));
    }
  }
}