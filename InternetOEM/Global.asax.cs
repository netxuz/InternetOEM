using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.IO;
using System.IO.Compression;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using System.Web.SessionState;
using System.Xml.Linq;
using OnlineServices.Method;

namespace ICommunity
{
  public class Global : System.Web.HttpApplication
  {
    Web oWeb = new Web();
    protected void Application_Start(object sender, EventArgs e)
    {
      Application["SmtpServer"] = string.Empty;
      Application["EmailSender"] = string.Empty;
      Application["NameSender"] = string.Empty;
      Application["SiteName"] = string.Empty;
      Application["WinRadSkin"] = string.Empty;
      Application["ClientFacebook"] = string.Empty;
      Application["ClientTwitter"] = string.Empty;
      Application["NoApellido"] = string.Empty;
      Application["NoFono"] = string.Empty;
      Application["URLSite"] = string.Empty;
      Application["GoogleSiteVerification"] = string.Empty;
      Application["PortSmtpServer"] = string.Empty;
      Application["UserSmtp"] = string.Empty;
      Application["PwdSmtp"] = string.Empty;
      Application["setAccount"] = string.Empty;
      Application["setDomainName"] = string.Empty;
      Application["trackPageview"] = string.Empty;

      string cPath = Server.MapPath(".");
      DataTable dParame = oWeb.DeserializarTbl(cPath, "parametros.bin");
      if (dParame != null)
        if (dParame.Rows.Count > 0) {
          foreach (DataRow oRow in dParame.Rows)
          {
            if (oRow["cod_codigo"].ToString() == "1")
              Application["SmtpServer"] = oRow["valor_parametro"].ToString();
            if (oRow["cod_codigo"].ToString() == "2")
              Application["EmailSender"] = oRow["valor_parametro"].ToString();
            if (oRow["cod_codigo"].ToString() == "3")
              Application["NameSender"] = oRow["valor_parametro"].ToString();
            if (oRow["cod_codigo"].ToString() == "4")
              Application["SiteName"] = oRow["valor_parametro"].ToString();
            if (oRow["cod_codigo"].ToString() == "5")
              Application["WinRadSkin"] = oRow["valor_parametro"].ToString();
            if (oRow["cod_codigo"].ToString() == "6")
              Application["ClientFacebook"] = oRow["valor_parametro"].ToString();
            if (oRow["cod_codigo"].ToString() == "7")
              Application["ClientTwitter"] = oRow["valor_parametro"].ToString();
            if (oRow["cod_codigo"].ToString() == "9")
              Application["NoApellido"] = oRow["valor_parametro"].ToString();
            if (oRow["cod_codigo"].ToString() == "10")
              Application["NoFono"] = oRow["valor_parametro"].ToString();
            if (oRow["cod_codigo"].ToString() == "13")
              Application["URLSite"] = oRow["valor_parametro"].ToString();
            if (oRow["cod_codigo"].ToString() == "14")
              Application["GoogleSiteVerification"] = oRow["valor_parametro"].ToString();
            if (oRow["cod_codigo"].ToString() == "15")
              Application["PortSmtpServer"] = oRow["valor_parametro"].ToString();
            if (oRow["cod_codigo"].ToString() == "16")
              Application["UserSmtp"] = oRow["valor_parametro"].ToString();
            if (oRow["cod_codigo"].ToString() == "17")
              Application["PwdSmtp"] = oRow["valor_parametro"].ToString();
            if (oRow["cod_codigo"].ToString() == "18")
              Application["setAccount"] = oRow["valor_parametro"].ToString();
            if (oRow["cod_codigo"].ToString() == "19")
              Application["setDomainName"] = oRow["valor_parametro"].ToString();
            if (oRow["cod_codigo"].ToString() == "20")
              Application["trackPageview"] = oRow["valor_parametro"].ToString();
          }
        }
      dParame = null;
      
    }

    protected void Session_Start(object sender, EventArgs e)
    {
      Session["USUARIO"] = string.Empty;
      Session["CodUsuarioPerfil"] = string.Empty;
      Session["AdministradorPanel"] = string.Empty;
    }

    protected void Application_BeginRequest(object sender, EventArgs e)
    {

    }

    protected void Application_AuthenticateRequest(object sender, EventArgs e)
    {

    }

    protected void Application_Error(object sender, EventArgs e)
    {

    }

    protected void Session_End(object sender, EventArgs e)
    {

    }

    protected void Application_End(object sender, EventArgs e)
    {

    }

    protected void Application_PreRequestHandlerExecute(object sender, EventArgs e)
    {
      
    }
  }
}