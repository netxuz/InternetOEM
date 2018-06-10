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

using OnlineServices.Conn;
using OnlineServices.SystemData;
using OnlineServices.Method;

namespace ICommunity
{
  public partial class ETarget : System.Web.UI.Page
  {
    Web oWeb = new Web();
    Culture oCulture = new Culture();
    protected void Page_Load(object sender, EventArgs e)
    {
      oWeb.ValidaSessionAdm();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
      string CodeEncrypt = string.Empty;
      StringBuilder txtContent = new StringBuilder();
      txtContent.Append(rdCuerpoEmail.Content);

      StringBuilder js = new StringBuilder();
      js.Append("function LgRespuesta() {");
      Emailing oEmailing = new Emailing();
      oEmailing.FromName = Application["NameSender"].ToString();
      oEmailing.From = Application["EmailSender"].ToString();

      StringBuilder sPath = new StringBuilder();
      sPath.Append(Server.MapPath("."));
      DataTable dUsuarios = oWeb.DeserializarTbl(sPath.ToString(), "Usuarios.bin");
      if (dUsuarios != null)
      {
        if (dUsuarios.Rows.Count > 0) {
          DataRow[] dRows = dUsuarios.Select(" cod_tipo = 1 and notetarget <> '1' ");
          if (dRows != null) {
            if (dRows.Count() > 0) {
              foreach (DataRow oRow in dRows) {
                oEmailing.Address = oRow["eml_usuario"].ToString();
              }
            }
          }
          dRows = null;
        }
        
        oEmailing.Subject = txtAsunto.Text;
        oEmailing.Body = txtContent;
        if (oEmailing.EmailSend())
        {
          js.Append(" window.radalert('").Append(oCulture.GetResource("Mensajes", "sMessage07")).Append("', 400, 100,'" + oCulture.GetResource("Global", "MnsAtencion") + "'); ");
        }
        else
        {
          js.Append(" window.radalert('").Append(oCulture.GetResource("Error", "sError03")).Append("', 400, 100,'" + oCulture.GetResource("Global", "MnsAtencion") + "'); ");
        }
      }
      else {
        js.Append(" window.radalert('").Append(oCulture.GetResource("Error", "sError03")).Append("', 400, 100,'" + oCulture.GetResource("Global", "MnsAtencion") + "'); ");
      }
      dUsuarios = null;

      js.Append(" Sys.Application.remove_load(LgRespuesta); ");
      js.Append("};");
      js.Append("Sys.Application.add_load(LgRespuesta);");
      Page.ClientScript.RegisterStartupScript(this.GetType(), "LgRespuesta", js.ToString(), true);

    }
  }
}
