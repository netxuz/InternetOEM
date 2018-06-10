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
  public partial class AdmVentaArriendo : System.Web.UI.UserControl
  {
    bool bEmailOk;
    Web oWeb = new Web();
    Culture oCulture = new Culture();
    Emailing oEmailing = new Emailing();
    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack) {
        lblNombre.Text = oCulture.GetResource("Contactenos","Nombre");
        lblEmail.Text = oCulture.GetResource("Contactenos", "Email");
        lblFono.Text = oCulture.GetResource("Contactenos", "Fono");
        lblComentario.Text = oCulture.GetResource("Contactenos", "Comentario");
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
        oHtml.Append("<HTML><BODY><p><font face=verdana size=2>De: ").Append(txtNombre.Text);
        oHtml.Append("<br>RUT : ").Append(txtRut.Text);
        oHtml.Append("<br>Mail : ").Append(txtEmail.Text);
        oHtml.Append("<br>Fono : ").Append(txtFono.Text);
        oHtml.Append("<br>Dirección : ").Append(txtDireccion.Text);
        oHtml.Append("<br>Comuna : ").Append(txtComuna.Text);
        oHtml.Append("<br>Terreno/M2 : ").Append(txtTerreno.Text);
        oHtml.Append("<br>N° Habitaciones : ").Append(txtNumAbitaciones.Text);
        oHtml.Append("<br>N° Baños : ").Append(txtNumBanos.Text);
        oHtml.Append("<br>Mensaje: ").Append(txtComentario.Text).Append("</font></BODY></HTML>");

        oEmailing.FromName = Application["NameSender"].ToString();
        oEmailing.From = Application["EmailSender"].ToString();
        oEmailing.Address = Application["EmailSender"].ToString();
        oEmailing.Subject = oCulture.GetResource("Mensajes", "sMessage06") + Application["SiteName"].ToString();
        oEmailing.Body = oHtml;
        if (oEmailing.EmailSend())
        {
          js.Append(" window.radalert('").Append(oCulture.GetResource("Mensajes", "sMessage07")).Append("', 400, 100,'" + oCulture.GetResource("Global", "MnsAtencion") + "'); "); 
        }
        else {
          js.Append(" window.radalert('").Append(oCulture.GetResource("Error", "sError03")).Append("', 400, 100,'" + oCulture.GetResource("Global", "MnsAtencion") + "'); ");
        }
        js.Append(" Sys.Application.remove_load(LgRespuesta); ");
        js.Append("};");
        js.Append("Sys.Application.add_load(LgRespuesta);");
        Page.ClientScript.RegisterStartupScript(this.GetType(), "LgRespuesta", js.ToString(), true);
      }
      else {
        StringBuilder js = new StringBuilder();
        js.Append("function LgRespuesta() {");
        js.Append(" window.radalert('").Append(oCulture.GetResource("Error", "sError01")).Append("', 400, 100,'" + oCulture.GetResource("Global", "MnsAtencion") + "'); ");
        js.Append(" Sys.Application.remove_load(LgRespuesta); ");
        js.Append("};");
        js.Append("Sys.Application.add_load(LgRespuesta);");
        Page.ClientScript.RegisterStartupScript(this.GetType(), "LgRespuesta", js.ToString(), true);
      }

    }
  }
}