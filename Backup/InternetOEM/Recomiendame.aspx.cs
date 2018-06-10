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
using OnlineServices.SystemData;

namespace ICommunity
{
  public partial class Recomiendame : System.Web.UI.Page
  {
    bool bEmailOk;
    Emailing oEmailing = new Emailing();
    BinaryUsuario dUsuario;
    SysUsuario oUsuario;
    private OnlineServices.Method.Web oWeb = new OnlineServices.Method.Web();
    private OnlineServices.Method.Culture oCulture = new OnlineServices.Method.Culture();
    private OnlineServices.Method.Usuario oIsUsuario;
    protected void Page_Load(object sender, EventArgs e)
    {
      oIsUsuario = oWeb.GetObjUsuario();
      if (!IsPostBack)
      {
        CodUsuario.Value = oWeb.GetData("CodUsuario");
        string sTitle = oCulture.GetResource("Contactenos", "lblTitleRecomiendame");
        if (string.IsNullOrEmpty(oIsUsuario.CodUsuario))
          lblNombre.Text = oCulture.GetResource("Contactenos", "TuNombre");
        else
        {
          txtNombreCliente.Visible = false;
          txtNombre.Enabled = false;
        }
        lblNombreAmigo.Text = oCulture.GetResource("Contactenos", "NombreAmigo");
        lblEmail.Text = oCulture.GetResource("Contactenos", "EmailAmigo");
        lblComentario.Text = oCulture.GetResource("Contactenos", "Comentario");
        BtnVolver.Text = oCulture.GetResource("Global", "btnVolver");
        BtnVolverOK.Text = oCulture.GetResource("Global", "btnVolver");
        BtnAceptar.Text = oCulture.GetResource("Global", "btnAceptar");

        dUsuario = new BinaryUsuario();
        oUsuario = new SysUsuario();
        oUsuario.Path = Server.MapPath(".");
        oUsuario.CodUsuario = CodUsuario.Value;
        dUsuario = oUsuario.ClassGet();
        if (dUsuario != null)
          sTitle = sTitle.Replace("[NombreUsuario]", dUsuario.NomUsuario + " " + dUsuario.ApeUsuario);
        dUsuario = null;

        lblCntTitle.Text = sTitle;

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
    protected void BtnVolver_Click(object sender, EventArgs e)
    {
      Response.Redirect("Escort.aspx?CodUsuario=" + CodUsuario.Value);
    }
    protected void BtnAceptar_Click(object sender, EventArgs e)
    {
      dvRecomiendame.Visible = false;
      dvMsgResult.Visible = true;

      if (bEmailOk)
      {
        DataTable dParamEmail = oWeb.DeserializarTbl(Server.MapPath("."), "ParamEmail.bin");
        if (dParamEmail != null)
          if (dParamEmail.Rows.Count > 0)
          {
            DataRow[] oRows = dParamEmail.Select(" tipo_email = 'R' ");
            if (oRows != null)
            {
              if (oRows.Count() > 0)
              {
                string sNomUsuario = string.Empty;
                string sClientNom = string.Empty;
                dUsuario = new BinaryUsuario();
                oUsuario = new SysUsuario();
                oUsuario.Path = Server.MapPath(".");
                oUsuario.CodUsuario = CodUsuario.Value;
                dUsuario = oUsuario.ClassGet();
                if (dUsuario != null)
                  sNomUsuario = dUsuario.NomUsuario + " " + dUsuario.ApeUsuario;
                dUsuario = null;

                StringBuilder sPath = new StringBuilder();
                StringBuilder sFile = new StringBuilder();
                sFile.Append("UserArchivo_").Append(CodUsuario.Value).Append(".bin");

                DataTable dArchivoUsuario = oWeb.DeserializarTbl(Server.MapPath("."), sFile.ToString());
                if (dArchivoUsuario != null)
                {
                  if (dArchivoUsuario.Rows.Count > 0)
                  {
                    DataRow[] oRowsImg = dArchivoUsuario.Select(" tip_archivo = 'P' ");
                    if (oRowsImg != null)
                    {
                      if (oRowsImg.Count() > 0)
                      {
                          sPath.Append("/rps_onlineservice/escorts/escort_");
                        sPath.Append(CodUsuario.Value);
                        sPath.Append("/");
                        sPath.Append(oRowsImg[0]["nom_archivo"].ToString());
                      }
                    }
                    oRowsImg = null;
                  }
                  dArchivoUsuario = null;
                }

                if (!string.IsNullOrEmpty(oIsUsuario.CodUsuario))
                  sClientNom = oIsUsuario.Nombres;
                else
                  sClientNom = txtNombre.Text;

                StringBuilder sAsunto = new StringBuilder();
                sAsunto.Append(oRows[0]["asunto_email"].ToString());
                sAsunto.Replace("[USUARIO]", sNomUsuario);
                sAsunto.Replace("[CLIENTE]", sClientNom);

                StringBuilder oHtml = new StringBuilder();
                oHtml.Append(oRows[0]["cuerpo_email"].ToString());
                oHtml.Replace("[NOMBRE]", sClientNom);
                oHtml.Replace("[NOMBREAMIGO]", txtNombreAmigo.Text);
                oHtml.Replace("[USUARIO]", sNomUsuario);
                oHtml.Replace("[SITIO]", "http://" + Request.ServerVariables["HTTP_HOST"].ToString());
                oHtml.Replace("[DATALINK]", "?fts=t&tp=rmd&tu=" + HttpUtility.UrlEncode(oWeb.Crypt(CodUsuario.Value)));
                oHtml.Replace("[IMGPHOTOUSER]", sPath.ToString());
                oHtml.Replace("[NOMBRESITIO]", Application["SiteName"].ToString());
                oHtml.Replace("[COMENTARIO]", txtComentario.Text);

                Emailing oEmailing = new Emailing();
                oEmailing.FromName = Application["NameSender"].ToString();
                oEmailing.From = Application["EmailSender"].ToString();
                oEmailing.Address = txtEmail.Text;
                oEmailing.Subject = sAsunto.ToString();
                oEmailing.Body = oHtml;
                if (oEmailing.EmailSend())
                  lblMsgResult.Text = oCulture.GetResource("Contactenos", "lblMsgResultOk");
                else
                  lblMsgResult.Text = oCulture.GetResource("Contactenos", "lblMsgResultNok");
              }
            }
          }
      }
      else
      {
        lblMsgResult.Text = oCulture.GetResource("Contactenos", "lblMsgResultNok");
      }

    }

  }
}
