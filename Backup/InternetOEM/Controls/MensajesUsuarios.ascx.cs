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

using Telerik.Web.UI;
using OnlineServices.Conn;
using OnlineServices.CmsData;
using OnlineServices.SystemData;
using OnlineServices.Method;

namespace ICommunity.Controls
{
  public partial class MensajesUsuarios : System.Web.UI.UserControl
  {
    private string pCodNodo = string.Empty;
    private string pCodUsuarioRel = string.Empty;
    private OnlineServices.Method.Web oWeb = new OnlineServices.Method.Web();
    private OnlineServices.Method.Log oLog = new OnlineServices.Method.Log();
    private OnlineServices.Method.Culture oCulture = new OnlineServices.Method.Culture();
    private OnlineServices.Method.Usuario oIsUsuario;

    protected void Page_Load(object sender, EventArgs e)
    {
      string pCodUsuario = oWeb.GetData("CodUsuario");
      getComments(pCodUsuario);
    }

    protected void getComments(string CodUsuario)
    {
      Label oLabel;
      Controls.Add(new LiteralControl("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"mdlMisComentarios\" width=\"100%\">"));
      DataTable dContenidoUsuario = oWeb.DeserializarTbl(Server.MapPath("."), "Contenidos.bin");
      if (dContenidoUsuario != null)
        if (dContenidoUsuario.Rows.Count > 0)
        {
          StringBuilder sSQL = new StringBuilder();
          sSQL.Append(" cod_usuario = ").Append(CodUsuario);
          sSQL.Append(" and cod_contenido_rel is null and est_contenido = 'P' ");
          DataRow[] oRows = dContenidoUsuario.Select(sSQL.ToString(), " date_contenido desc ");
          if (oRows != null)
            if (oRows.Count() > 0)
            {
              SysUsuario objUsuario;
              BinaryUsuario dUsuario;
              foreach (DataRow oRow in oRows)
              {
                objUsuario = new SysUsuario();
                objUsuario.Path = Server.MapPath(".");
                objUsuario.CodUsuario = oRow["cod_usuario"].ToString();
                dUsuario = objUsuario.ClassGet();
                if (dUsuario != null)
                  if (dUsuario.EstUsuario == "V")
                  {
                    Controls.Add(new LiteralControl("<tr><td class=\"MsnUsrBlqPerfil\" colspan=\"2\" align=\"top\">"));
                    Controls.Add(new LiteralControl("<div class=\"MsnUsrComentUsrPerfil\">"));
                    oLabel = new Label();
                    oLabel.ID = "lblComment_" + oRow["cod_contenido"].ToString();
                    oLabel.Text = oRow["texto_contenido"].ToString();
                    oLabel.CssClass = "lblCommentPerfil";
                    Controls.Add(oLabel);
                    Controls.Add(new LiteralControl("</div><div class=\"MsnUsrFchUsrPerfil\">"));
                    oLabel = new Label();
                    oLabel.ID = "lblFecha_" + oRow["cod_contenido"].ToString();
                    oLabel.Text = oCulture.GetResource("Global", "Comentado") + " " + String.Format("{0:f}", DateTime.Parse(oRow["date_contenido"].ToString()));
                    oLabel.CssClass = "lblFechaComment";
                    Controls.Add(oLabel);
                    Controls.Add(new LiteralControl("</div>"));
                    Controls.Add(new LiteralControl("</td></tr>"));
                  }
                dUsuario = null;
              }
            }
          oRows = null;
        }
      dContenidoUsuario = null;
      Controls.Add(new LiteralControl("</table>"));
    }
  }
}