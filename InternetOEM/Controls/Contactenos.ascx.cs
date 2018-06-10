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

namespace ICommunity.Controls
{
  public partial class Contactenos : System.Web.UI.UserControl
  {
    bool bEmailOk;
    Web oWeb = new Web();
    Culture oCulture = new Culture();
    Emailing oEmailing = new Emailing();
    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        //lblCntTitle.Text = oCulture.GetResource("Contactenos", "Contact");
        //lblCntDes.Text = oCulture.GetResource("Contactenos", "Descripcion");
        //lblNombre.Text = oCulture.GetResource("Contactenos", "Nombre");
        //lblEmail.Text = oCulture.GetResource("Contactenos", "Email");
        //lblFono.Text = oCulture.GetResource("Contactenos", "Fono");
        //lblComentario.Text = oCulture.GetResource("Contactenos", "Comentario");
        BtnAceptar.Text = oCulture.GetResource("Global", "btnAceptar");
      }

    }

    protected void ServerValidationEmail(object source, ServerValidateEventArgs args)
    {
      try
      {
        bEmailOk = args.IsValid = oWeb.ValidaMail(args.Value);
      }
      catch
      {
        bEmailOk = args.IsValid = false;
      }

    }

    protected void BtnAceptar_Click(object sender, EventArgs e)
    {
      if (bEmailOk)
      {
        StringBuilder js = new StringBuilder();
        js.Append("function LgRespuesta() {");
        StringBuilder oHtml = new StringBuilder();

        StringBuilder sAsunto = new StringBuilder();
        StringBuilder sPath = new StringBuilder();
        sPath.Append(Server.MapPath("."));
        DataTable dParamEmail = oWeb.DeserializarTbl(sPath.ToString(), "ParamEmail.bin");
        if (dParamEmail != null)
          if (dParamEmail.Rows.Count > 0)
          {
            DataRow[] oRows = dParamEmail.Select(" tipo_email = 'T' ");
            if (oRows != null)
            {
              if (oRows.Count() > 0)
              {
                sAsunto.Append(oRows[0]["asunto_email"].ToString());
                sAsunto.Replace("[NOMBRESITIO]", Application["SiteName"].ToString());
                oHtml.Append(oRows[0]["cuerpo_email"].ToString());
                oHtml.Replace("[CLIENTE]", txtNombre.Text);
                oHtml.Replace("[EMAIL_CLIENTE]", txtEmail.Text);
                oHtml.Replace("[FONO_CLIENTE]", txtFono.Text);
                oHtml.Replace("[MENSAJE_CLIENTE]", txtComentario.Text);
                oHtml.Replace("[NOMBRESITIO]", Application["SiteName"].ToString());

                oEmailing.FromName = Application["NameSender"].ToString();
                oEmailing.From = Application["EmailSender"].ToString();
                oEmailing.Address = Application["EmailSender"].ToString();
                oEmailing.Subject = sAsunto.ToString();
                oEmailing.Body = oHtml;
                if (oEmailing.EmailSend())
                {
                  js.Append(" window.radalert('").Append(oCulture.GetResource("Mensajes", "sMessage07")).Append("', 400, 100,'" + oCulture.GetResource("Global", "MnsAtencion") + "'); ");
                }
                else
                {
                  js.Append(" window.radalert('").Append(oCulture.GetResource("Error", "sError03")).Append("', 400, 100,'" + oCulture.GetResource("Global", "MnsAtencion") + "'); ");
                }
                js.Append(" Sys.Application.remove_load(LgRespuesta); ");
                js.Append("};");
                js.Append("Sys.Application.add_load(LgRespuesta);");
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "LgRespuesta", js.ToString(), true);
              }
            }
            oRows = null;
          }
        dParamEmail = null;

      }
      else
      {
        StringBuilder js = new StringBuilder();
        js.Append("function LgRespuesta() {");
        js.Append(" window.radalert('").Append(oCulture.GetResource("Error", "sError01")).Append("', 400, 100,'" + oCulture.GetResource("Global", "MnsAtencion") + "'); ");
        js.Append(" Sys.Application.remove_load(LgRespuesta); ");
        js.Append("};");
        js.Append("Sys.Application.add_load(LgRespuesta);");
        Page.ClientScript.RegisterStartupScript(Page.GetType(), "LgRespuesta", js.ToString(), true);
      }

    }
  }
}