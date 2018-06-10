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
  public partial class OlvidoPassword : System.Web.UI.UserControl
  {
    Culture oCulture = new Culture();
    Web oWeb = new Web();
    bool bEmailOk = true;
    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        ttlmsnolvpwd.Text = oCulture.GetResource("LoginUsers", "ttlMsnOlvPwd");
        lblmsnolvpwd.Text = oCulture.GetResource("LoginUsers", "lblMsnOlvPwd");
        lblemlolvpwd.Text = oCulture.GetResource("LoginUsers", "lblEmlOlvPwd");
        btnOlvPpwd.Text = oCulture.GetResource("Global", "btnAceptar");
        //lblmsnolvpwd_resp.Text = oCulture.GetResource("LoginUsers", "lblMsnOlvPwdResp");
      }
    }

    protected void ServerValidationEml(object source, ServerValidateEventArgs args)
    {
      try
      {
        if (!oWeb.ValidaMail(args.Value))
          bEmailOk = args.IsValid = false;
      }
      catch
      {
        bEmailOk = args.IsValid = false;
      }

    }

    protected void btnOlvPpwd_Click(object sender, EventArgs e)
    {
      string sCodUsrCrypt = string.Empty;
      string sNombApeUsuario = string.Empty;
      string sNombCrypt = string.Empty;

      if (bEmailOk)
      {


        DataTable dUsuario = oWeb.DeserializarTbl(Server.MapPath("."), "Usuarios.bin");
        if (dUsuario != null)
        {
          DataRow[] oRow = dUsuario.Select(" eml_usuario = '" + txtEmlOlvPwd.Text + "' and est_usuario = 'V' ");
          if (oRow != null)
          {
            if (oRow.Count() > 0)
            {
              sCodUsrCrypt = oWeb.Crypt(oRow[0]["cod_usuario"].ToString());
              sNombApeUsuario = (oRow[0]["nom_usuario"].ToString() + " " + oRow[0]["ape_usuario"].ToString()).Trim();
              //sNombCrypt = oWeb.Crypt(sNombApeUsuario);

              DataTable dParamEmail = oWeb.DeserializarTbl(Server.MapPath("."), "ParamEmail.bin");
              if (dParamEmail != null)
                if (dParamEmail.Rows.Count > 0)
                {
                  DataRow[] oRows = dParamEmail.Select(" tipo_email = 'W' ");
                  if (oRows != null)
                  {
                    if (oRows.Count() > 0)
                    {
                      StringBuilder sAsunto = new StringBuilder();
                      sAsunto.Append(oRows[0]["asunto_email"].ToString());
                      sAsunto.Replace("[NOMBRESITIO]", Application["SiteName"].ToString());

                      StringBuilder oHtml = new StringBuilder();
                      oHtml.Append(oRows[0]["cuerpo_email"].ToString());
                      oHtml.Replace("[NOMBRE]", sNombApeUsuario);
                      oHtml.Replace("[SITIO]", "http://" + Request.ServerVariables["HTTP_HOST"].ToString());
                      oHtml.Replace("[DATALINK]", "?fts=t&tp=lvc&tk=" + sCodUsrCrypt);
                      oHtml.Replace("[NOMBRESITIO]", Application["SiteName"].ToString());

                      Emailing oEmailing = new Emailing();
                      oEmailing.FromName = Application["NameSender"].ToString();
                      oEmailing.From = Application["EmailSender"].ToString();
                      oEmailing.Address = txtEmlOlvPwd.Text;
                      oEmailing.Subject = sAsunto.ToString();
                      oEmailing.Body = oHtml;
                      if (!oEmailing.EmailSend())
                      {
                        context_olvpwd.Visible = false;
                        context_olvpwd_resp.Visible = true;
                        ttlmsnolvpwd_resp.Text = oCulture.GetResource("LoginUsers", "lblErrOlvPwdTtResp");
                        lblmsnolvpwd_resp.Text = oCulture.GetResource("LoginUsers", "lblErrOlvPwdResp");
                      }
                      else
                      {
                        context_olvpwd.Visible = false;
                        context_olvpwd_resp.Visible = true;
                        ttlmsnolvpwd_resp.Text = oCulture.GetResource("LoginUsers", "lblMsnOlvPwdExitTt");
                        lblmsnolvpwd_resp.Text = oCulture.GetResource("LoginUsers", "lblMsnOlvPwdExitRsp");
                      }
                    }
                  }
                }
            }
            else
            {
              context_lblmsnoextpwd.Visible = true;
              lblmsnoextpwd.Text = oCulture.GetResource("LoginUsers", "lblNoExistePwd") + " " + txtEmlOlvPwd.Text;
              txtEmlOlvPwd.Text = "";
            }
            dUsuario = null;
          }
        }
      }
      else
      {
        context_lblmsnoextpwd.Visible = true;
        lblmsnoextpwd.Text = oCulture.GetResource("Error", "sError01");
        txtEmlOlvPwd.Text = "";
      }
    }
  }
}