using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Data;
using System.Linq;
using Telerik.Web.UI;

using OnlineServices.CmsData;
using OnlineServices.Method;
using OnlineServices.SystemData;

namespace OnlineServices.Web.UI
{
  public partial class Render : System.Web.UI.Page
  {
    private BinaryNodo oBinaryNodo;
    private BinaryContenido oBinaryContenido;
    private BinaryTemplate oBinaryTemplate;
    private BinaryZona oBinaryZona;

    private OnlineServices.Method.Usuario oIsUsuario;
    private OnlineServices.Method.Web oWeb = new OnlineServices.Method.Web();
    private OnlineServices.Method.Controls oControls = new OnlineServices.Method.Controls();
    private OnlineServices.Method.Culture oCulture = new OnlineServices.Method.Culture();

    private bool bIsMobil = false;
    private string CodUsuario = string.Empty;
    private string TipoUsuario = string.Empty;
    private int iCountHeader = 0;

    private string pCodNodo = string.Empty;
    public string CodNodo { get { return pCodNodo; } set { pCodNodo = value; } }

    public Render()
    {

    }

    protected override void OnPreRender(EventArgs e)
    {
      HtmlMeta oMeta;
      StringBuilder cUrl;
      HtmlLink oHLink;

      base.OnPreRender(e);

      if (!bIsMobil)
      {
        if (Request.Browser.IsBrowser("IE"))
        {
          float cVerion = -1;
          cVerion = (float)(Request.Browser.MajorVersion + Request.Browser.MinorVersion);
          if (cVerion >= 7.0)
          {
            oMeta = new HtmlMeta();
            oMeta.Attributes.Add("http-equiv", "X-UA-Compatible");
            oMeta.Attributes.Add("content", "IE=100");
            Page.Header.Controls.AddAt(iCountHeader, oMeta);
            iCountHeader++;
          }
        }

        oMeta = new HtmlMeta();
        oMeta.Attributes.Add("name", "google-site-verification");
        oMeta.Attributes.Add("content", Application["GoogleSiteVerification"].ToString());
        Page.Header.Controls.AddAt(iCountHeader, oMeta);
        iCountHeader++;

        oMeta = new HtmlMeta();
        oMeta.Attributes.Add("http-equiv", "Content-Type");
        oMeta.Attributes.Add("content", "text/html; charset=UTF-8");
        Page.Header.Controls.AddAt(iCountHeader, oMeta);
        iCountHeader++;

        oMeta = new HtmlMeta();
        oMeta.Attributes.Add("http-equiv", "Content-Language");
        oMeta.Attributes.Add("content", "ES");
        Page.Header.Controls.AddAt(iCountHeader, oMeta);
        iCountHeader++;

        oMeta = new HtmlMeta();
        oMeta.Attributes.Add("name", "viewport");
        oMeta.Attributes.Add("content", "initial-scale=1.0, user-scalable=no");
        Page.Header.Controls.AddAt(iCountHeader, oMeta);
        iCountHeader++;

        oMeta = new HtmlMeta();
        oMeta.Attributes.Add("property", "og:type");
        oMeta.Attributes.Add("content", "website");
        Page.Header.Controls.AddAt(iCountHeader, oMeta);
        iCountHeader++;

        oMeta = new HtmlMeta();
        oMeta.Attributes.Add("property", "og:title");
        oMeta.Attributes.Add("content", Application["SiteName"].ToString());
        Page.Header.Controls.AddAt(iCountHeader, oMeta);
        iCountHeader++;

        oMeta = new HtmlMeta();
        oMeta.Attributes.Add("property", "og:site_name");
        oMeta.Attributes.Add("content", Application["URLSite"].ToString());
        Page.Header.Controls.AddAt(iCountHeader, oMeta);
        iCountHeader++;

        oMeta = new HtmlMeta();
        oMeta.Attributes.Add("property", "og:url");
        oMeta.Attributes.Add("content", "http://" + Application["URLSite"].ToString() + "/");
        Page.Header.Controls.AddAt(iCountHeader, oMeta);
        iCountHeader++;

        cUrl = new StringBuilder();
        cUrl.Append("http://").Append(Application["URLSite"].ToString());
        cUrl.Append("/style/images/favicon.jpg?t=");
        cUrl.Append(DateTime.Now.ToString("yyyMMdd"));
        oMeta = new HtmlMeta();
        oMeta.Attributes.Add("property", "og:image");
        oMeta.Attributes.Add("content", this.Page.ResolveClientUrl(cUrl.ToString()));
        Page.Header.Controls.AddAt(iCountHeader, oMeta);
        iCountHeader++;

        //cUrl = new StringBuilder();
        //cUrl.Append("<script src=\"Resources/fgoogle.js\" language=\"JavaScript\"></script>");
        //Page.Header.Controls.Add(new LiteralControl(cUrl.ToString()));

        cUrl = new StringBuilder();
        //cUrl.Append("<script src=\"Resources/jquery.min.js\"></script>");
        cUrl.Append("<script src=\"Resources/jquery-1.5.1.min.js\"></script>");
        Page.Header.Controls.Add(new LiteralControl(cUrl.ToString()));

        cUrl = new StringBuilder();
        cUrl.Append("<script src=\"Resources/jquery.autosize.js\" language=\"JavaScript\"></script>");
        Page.Header.Controls.Add(new LiteralControl(cUrl.ToString()));

        //cUrl = new StringBuilder();
        //cUrl.Append("<script type=\"text/javascript\" src=\"http://maps.google.cl/maps/api/js?sensor=true\"></script>");
        //Page.Header.Controls.Add(new LiteralControl(cUrl.ToString()));
      }
      else
      {
        oMeta = new HtmlMeta();
        oMeta.Attributes.Add("name", "viewport");
        oMeta.Attributes.Add("content", "width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0;");
        Page.Header.Controls.AddAt(iCountHeader, oMeta);
        iCountHeader++;

        oMeta = new HtmlMeta();
        oMeta.Attributes.Add("name", "apple-mobile-web-app-capable");
        oMeta.Attributes.Add("content", "yes");
        Page.Header.Controls.AddAt(iCountHeader, oMeta);
        iCountHeader++;

        //cUrl = new StringBuilder();
        //cUrl.Append("<script src=\"Resources/fphonegoogle.js\" language=\"JavaScript\"></script>");
        //Page.Header.Controls.Add(new LiteralControl(cUrl.ToString()));
      }

      oMeta = new HtmlMeta();
      oMeta.Attributes.Add("name", "category");
      oMeta.Attributes.Add("content", "Adult");
      Page.Header.Controls.AddAt(iCountHeader, oMeta);
      iCountHeader++;

      oMeta = new HtmlMeta();
      oMeta.Attributes.Add("name", "classification");
      oMeta.Attributes.Add("content", "18");
      Page.Header.Controls.AddAt(iCountHeader, oMeta);
      iCountHeader++;

      oMeta = new HtmlMeta();
      oMeta.Attributes.Add("name", "robots");
      oMeta.Attributes.Add("content", "all | index | follow");
      Page.Header.Controls.AddAt(iCountHeader, oMeta);
      iCountHeader++;

      oMeta = new HtmlMeta();
      oMeta.Attributes.Add("name", "Pragma");
      oMeta.Attributes.Add("content", "no-cache");
      Page.Header.Controls.AddAt(iCountHeader, oMeta);
      iCountHeader++;

      oMeta = new HtmlMeta();
      oMeta.Attributes.Add("name", "revisit-after");
      oMeta.Attributes.Add("content", "1 days");
      Page.Header.Controls.AddAt(iCountHeader, oMeta);
      iCountHeader++;

      cUrl = new StringBuilder();
      cUrl.Append("~/style/masterstyle.css");
      oHLink = new HtmlLink();
      oHLink.Attributes.Add("href", this.Page.ResolveClientUrl(cUrl.ToString()));
      oHLink.Attributes.Add("type", "text/css");
      oHLink.Attributes.Add("rel", "stylesheet");
      Page.Header.Controls.Add(oHLink);

      cUrl = new StringBuilder();
      cUrl.Append("~/style/images/favicon.ico");
      oHLink = new HtmlLink();
      oHLink.Attributes.Add("href", this.Page.ResolveClientUrl(cUrl.ToString()));
      oHLink.Attributes.Add("type", "image/vnd.microsoft.icon");
      //oHLink.Attributes.Add("type", "image/x-icon");
      oHLink.Attributes.Add("rel", "shortcut icon");
      Page.Header.Controls.Add(oHLink);

      cUrl = new StringBuilder();
      cUrl.Append("/style/images/favicon.jpg?t=");
      cUrl.Append(DateTime.Now.ToString("yyyMMdd"));
      oHLink = new HtmlLink();
      oHLink.Attributes.Add("href", this.Page.ResolveClientUrl(cUrl.ToString()));
      oHLink.Attributes.Add("type", "image/png");
      //oHLink.Attributes.Add("type", "image/x-icon");
      oHLink.Attributes.Add("rel", "icon");
      Page.Header.Controls.Add(oHLink);

      cUrl = new StringBuilder();
      cUrl.Append("http://").Append(Application["URLSite"].ToString());
      cUrl.Append("/style/images/favicon.jpg?t=");
      cUrl.Append(DateTime.Now.ToString("yyyMMdd"));
      oHLink = new HtmlLink();
      oHLink.Attributes.Add("href", this.Page.ResolveClientUrl(cUrl.ToString()));
      oHLink.Attributes.Add("rel", "image_src");
      Page.Header.Controls.Add(oHLink);

      cUrl = new StringBuilder();
      cUrl.Append("http://").Append(Application["URLSite"].ToString());
      oHLink = new HtmlLink();
      oHLink.Attributes.Add("href", this.Page.ResolveClientUrl(cUrl.ToString()));
      oHLink.Attributes.Add("rel", "canonical");
      Page.Header.Controls.Add(oHLink);

      oHLink = new HtmlLink();
      oHLink.Attributes.Add("rel", "alternate");
      oHLink.Attributes.Add("hreflang", "es-CL");
      oHLink.Attributes.Add("href", this.Page.ResolveClientUrl(cUrl.ToString()));
      Page.Header.Controls.Add(oHLink);

      cUrl = new StringBuilder();
      cUrl.Append("<script src=\"Resources/GlobalFunction.js\" type=\"text/javascript\"></script>");
      Page.Header.Controls.Add(new LiteralControl(cUrl.ToString()));

      cUrl = new StringBuilder();
      cUrl.Append("<script src=\"http://www.google-analytics.com/ga.js\" async=\"\" type=\"text/javascript\"></script>");
      Page.Header.Controls.Add(new LiteralControl(cUrl.ToString()));

      StringBuilder jsGoogle = new StringBuilder();
      jsGoogle.Append("var _gaq = _gaq || [];\n");
      jsGoogle.Append("_gaq.push(['_setAccount', '").Append(Application["setAccount"].ToString()).Append("']);\n");
      jsGoogle.Append("_gaq.push(['_setDomainName', '").Append(Application["setDomainName"].ToString()).Append("']);\n");
      jsGoogle.Append("_gaq.push(['_trackPageview']);\n");

      jsGoogle.Append("(function() {\n");
      jsGoogle.Append(" var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;\n");
      jsGoogle.Append(" ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';\n");
      jsGoogle.Append(" var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);\n");
      jsGoogle.Append("})();\n");

      Page.ClientScript.RegisterStartupScript(this.GetType(), "JQGoogle", jsGoogle.ToString(), true);

    }

    protected string getNodoIni(string sCodContenido)
    {
      string iCodNodo = string.Empty;
      string cPath = Server.MapPath(".");
      DataRow[] oRow;
      DataTable oNodos = oWeb.DeserializarTbl(cPath, "Nodos.bin");
      if (oNodos != null)
        if (oNodos.Rows.Count > 0)
        {
          if (string.IsNullOrEmpty(sCodContenido))
          {
            if (!bIsMobil)
              oRow = oNodos.Select(" ini_nodo = 'V' ");
            else
              oRow = oNodos.Select(" ini_nodo_phone = 'V' ");
            if (oRow != null)
              if (oRow.Count() > 0)
                iCodNodo = oRow[0]["cod_nodo"].ToString();
          }
          else
          {
            oRow = oNodos.Select(" cont_nodo = 'V' ");
            if (oRow != null)
              if (oRow.Count() > 0)
                iCodNodo = oRow[0]["cod_nodo"].ToString();
          }
        }
      oNodos = null;
      return iCodNodo;
    }

    protected string getNodoByType(string sType)
    {
      DataRow[] oRow;
      string sCodNodo = string.Empty;

      StringBuilder sQuery = new StringBuilder();
      sQuery.Append(" cod_nodo_rel = 0 ");

      switch (sType)
      {
        case "olvpwd":  //Olvido Clave
          sQuery.Append(" and est_nodo = 'O' and ind_olvclave_nodo = 'V' ");
          break;
        case "rstpwd":  //Reseteo de Clave
          sQuery.Append(" and est_nodo = 'O' and ind_rstclave_nodo = 'V' ");
          break;
        case "lgn": //Login
          sQuery.Append(" and est_nodo = 'O' and ind_login_nodo = 'V' ");
          break;
        case "pts": //Politicas de Seguridad
          sQuery.Append(" and est_nodo = 'O' and ind_poltsecure_nodo = 'V' ");
          break;
        case "tru": //Terminos de uso
          sQuery.Append(" and est_nodo = 'O' and ind_termuse_nodo = 'V' ");
          break;
        case "rgs": //Registrate
          sQuery.Append(" and ind_registrate_nodo = 'V' ");
          break;
        case "prf": //Ver Perfil de Usuario.
          sQuery.Append(" and ind_photo_nodo = 'V' ");
          break;
        case "iph": //Inicio Phone.
          sQuery.Append(" and ini_nodo_phone = 'V' ");
          break;
        case "cph": //Contactanos Phone
          sQuery.Append(" and cont_nodo_phone = 'V' ");
          break;
        default:
          sQuery.Append(" and est_nodo = 'V' ");
          break;
      }

      DataTable oNodos = oWeb.DeserializarTbl(Server.MapPath("."), "Nodos.bin");
      if (oNodos != null)
        if (oNodos.Rows.Count > 0)
        {
          oRow = oNodos.Select(sQuery.ToString());
          if (oRow != null)
            if (oRow.Count() > 0)
              sCodNodo = oRow[0]["cod_nodo"].ToString();
          oRow = null;
        }
      oNodos = null;

      return sCodNodo;
    }

    protected void getIsPhone()
    {
      string strUserAgent = Request.UserAgent.ToString().ToLower();
      if ((Request.Browser.IsMobileDevice == true) || (strUserAgent.Contains("iphone")) || (strUserAgent.Contains("blackberry")) || (strUserAgent.Contains("mobile")) || (strUserAgent.Contains("windows ce")) || (strUserAgent.Contains("opera mini")) || (strUserAgent.Contains("android")))
        bIsMobil = true;

      Session["isMobil"] = bIsMobil.ToString();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      HtmlMeta oMeta;
      if ((Session["isMobil"] == null) || (string.IsNullOrEmpty(Session["isMobil"].ToString())))
        getIsPhone();
      else
        bIsMobil = bool.Parse(Session["isMobil"].ToString());

      if (((!string.IsNullOrEmpty(oWeb.GetData("fts"))) && (oWeb.GetData("fts") == "t")) && (!string.IsNullOrEmpty(oWeb.GetData("tp"))))
      {
        switch (oWeb.GetData("tp"))
        {
          case "lvc":
            string sRepClave = oWeb.GetData("tk");
            DataTable dUsuario = oWeb.DeserializarTbl(Server.MapPath("."), "Usuarios.bin");
            if (dUsuario != null)
            {
              DataRow[] oDataRow = dUsuario.Select(" cod_usuario = '" + oWeb.UnCrypt(sRepClave) + "' and est_usuario = 'V' ");
              if (oDataRow != null)
                if (oDataRow.Length > 0)
                {
                  Session["USRCHANGEPWD"] = oDataRow[0]["cod_usuario"].ToString();
                  pCodNodo = getNodoByType("rstpwd");
                }
              oDataRow = null;
            }
            dUsuario = null;
            break;
          case "rmd":
            Session["CodUsuarioPerfil"] = oWeb.UnCrypt(oWeb.GetData("tu"));
            pCodNodo = getNodoByType("prf");
            break;
        }
        if (string.IsNullOrEmpty(pCodNodo))
          pCodNodo = getNodoIni(string.Empty);

        ViewState["CodNodo"] = pCodNodo;
        Session["CodNodo"] = pCodNodo;
      }
      else
      {
        #region INICIO CON VARIABLES DE ENTRADA
        if (((Session["CodContenido"] == null) || (string.IsNullOrEmpty(Session["CodContenido"].ToString()))) && (string.IsNullOrEmpty(oWeb.GetData("CodContenido"))))
        {
          if (!IsPostBack)
          {
            if ((Session["CodNodo"] == null) || (string.IsNullOrEmpty(Session["CodNodo"].ToString())))
            {
              if ((ViewState["CodNodo"] == null) || (string.IsNullOrEmpty(ViewState["CodNodo"].ToString())))
              {
                pCodNodo = getNodoIni(string.Empty);
                ViewState["CodNodo"] = pCodNodo;
                Session["CodNodo"] = pCodNodo;
              }
              else
              {
                pCodNodo = ViewState["CodNodo"].ToString();
                Session["CodNodo"] = pCodNodo;
              }
            }
            else
            {
              pCodNodo = Session["CodNodo"].ToString();
              ViewState["CodNodo"] = Session["CodNodo"].ToString();
            }
          }
          else
          {
            if ((Session["CodNodo"] == null) || (string.IsNullOrEmpty(Session["CodNodo"].ToString())))
            {
              pCodNodo = getNodoIni(string.Empty);
              ViewState["CodNodo"] = pCodNodo;
              Session["CodNodo"] = pCodNodo;
            }
            else if (!string.IsNullOrEmpty(Session["CodNodo"].ToString()))
            {
              pCodNodo = Session["CodNodo"].ToString();
              ViewState["CodNodo"] = Session["CodNodo"].ToString();
            }
            else
            {
              pCodNodo = ViewState["CodNodo"].ToString();
              Session["CodNodo"] = pCodNodo;
            }
          }
        }
        else
        {
          if ((Session["CodContenido"] != null) && (!string.IsNullOrEmpty(Session["CodContenido"].ToString())))
          {
            pCodNodo = getNodoIni(Session["CodContenido"].ToString());
            ViewState["CodNodo"] = pCodNodo;
            Session["CodNodo"] = pCodNodo;
          }
          else
          {
            Session["CodContenido"] = oWeb.GetData("CodContenido");
            pCodNodo = getNodoIni(oWeb.GetData("CodContenido"));
            ViewState["CodNodo"] = pCodNodo;
            Session["CodNodo"] = pCodNodo;
          }
        }
        #endregion
      }
      if ((Session["USUARIO"] != null) && (!string.IsNullOrEmpty(Session["USUARIO"].ToString())))
      {
        Usuario oUsuario = (Usuario)Session["USUARIO"];
        CodUsuario = oUsuario.CodUsuario;
        TipoUsuario = oUsuario.Tipo;
      }
      else
      {
        if ((Session["USRCHANGEPWD"] == null) || (string.IsNullOrEmpty(Session["USRCHANGEPWD"].ToString())))
        {
          string sKey = string.Empty;
          HttpCookie myCookie = new HttpCookie("AccesCookie");
          myCookie = Request.Cookies["AccesCookie"];
          if (myCookie != null)
          {
            sKey = myCookie.Value;
            if (!string.IsNullOrEmpty(sKey))
            {
              DataTable dUsuario = oWeb.DeserializarTbl(Server.MapPath("."), "Usuarios.bin");
              if (dUsuario != null)
              {
                DataRow[] oDataRow = dUsuario.Select(" cod_usuario = '" + oWeb.UnCrypt(sKey) + "' and est_usuario = 'V' ");
                if (oDataRow != null)
                  if (oDataRow.Length > 0)
                  {
                    oIsUsuario = oWeb.GetObjUsuario();
                    oIsUsuario.CodUsuario = oDataRow[0]["cod_usuario"].ToString();
                    oIsUsuario.Tipo = oDataRow[0]["cod_tipo"].ToString();
                    oIsUsuario.Nombres = (oDataRow[0]["nom_usuario"].ToString() + " " + oDataRow[0]["ape_usuario"].ToString()).Trim();
                    oIsUsuario.Email = oDataRow[0]["eml_usuario"].ToString();
                    oIsUsuario.Fono = oDataRow[0]["fono_usuario"].ToString();

                    Session["USUARIO"] = oIsUsuario;
                    CodUsuario = oIsUsuario.CodUsuario;
                    TipoUsuario = oIsUsuario.Tipo;
                  }
                oDataRow = null;
              }
              dUsuario = null;
            }
          }
        }
      }

      StringBuilder oFolder = new StringBuilder();
      oFolder.Append(Server.MapPath("."));

      CmsNodos oCmsNodos = new CmsNodos();
      oCmsNodos.Path = oFolder.ToString();
      oCmsNodos.CodNodo = pCodNodo;
      oBinaryNodo = oCmsNodos.ClassGet();

      if (oBinaryNodo != null)
      {
        if (!string.IsNullOrEmpty(oBinaryNodo.TextoNodo.ToString()))
        {
          oMeta = new HtmlMeta();
          oMeta.Attributes.Add("name", "description");
          oMeta.Attributes.Add("content", oBinaryNodo.TextoNodo.ToString());
          Page.Header.Controls.AddAt(iCountHeader, oMeta);
          iCountHeader++;

          oMeta = new HtmlMeta();
          oMeta.Attributes.Add("property", "og:description");
          oMeta.Attributes.Add("content", oBinaryNodo.TextoNodo.ToString());
          Page.Header.Controls.AddAt(iCountHeader, oMeta);
          iCountHeader++;
        }
        if (!string.IsNullOrEmpty(oBinaryNodo.TitleHeaderNodo.ToString()))
        {
          oMeta = new HtmlMeta();
          oMeta.Attributes.Add("name", "title");
          oMeta.Attributes.Add("content", oBinaryNodo.TitleHeaderNodo.ToString().Trim());
          Page.Header.Controls.AddAt(iCountHeader, oMeta);
          iCountHeader++;

          Page.Title = oBinaryNodo.TitleHeaderNodo.ToString().Trim();
        }
        if (!string.IsNullOrEmpty(oBinaryNodo.KeywordsNodo.ToString()))
        {
          oMeta = new HtmlMeta();
          oMeta.Attributes.Add("name", "keywords");
          oMeta.Attributes.Add("content", oBinaryNodo.KeywordsNodo.ToString());
          Page.Header.Controls.AddAt(iCountHeader, oMeta);
          iCountHeader++;

          oMeta = new HtmlMeta();
          oMeta.Attributes.Add("http-equiv", "keywords");
          oMeta.Attributes.Add("content", oBinaryNodo.KeywordsNodo.ToString());
          Page.Header.Controls.AddAt(iCountHeader, oMeta);
          iCountHeader++;
        }
        if (!string.IsNullOrEmpty(oBinaryNodo.CodTemplate.ToString()))
        {
          HtmlGenericControl oHtmlPage = (HtmlGenericControl)this.FindControl("RenderPreview");

          //oHtmlPage.Controls.Add(oControls.LoadRadWindow());

          ScriptManager oScriptManager = new ScriptManager();
          oScriptManager.ID = DateTime.Now.ToString("yyyyMMdd").ToString() + "_oSmg_" + pCodNodo.ToString();
          oHtmlPage.Controls.Add(oScriptManager);

          CmsTemplate oCmsTemplate = new CmsTemplate();
          oCmsTemplate.CodTemplate = oBinaryNodo.CodTemplate.ToString();
          oCmsTemplate.Path = oFolder.ToString();
          oBinaryTemplate = oCmsTemplate.ClassGet();
          if (oBinaryTemplate != null)
          {
            //oHtmlPage.Controls.Add(new LiteralControl(TemplateAnalysis(sBody)));

            string sBody = oBinaryTemplate.TextoTemplate;
            while (!string.IsNullOrEmpty(sBody))
            {
              if (sBody.Contains("accept=\"Type:"))
              {
                int Inicio = sBody.IndexOf("accept=\"Type:");
                Inicio = sBody.LastIndexOf("<fieldset", Inicio);
                int Fin = sBody.IndexOf("</fieldset>", Inicio) + 11;
                string Tag = sBody.Substring(Inicio, Fin - Inicio);

                //Escribe el html antes del objeto
                oHtmlPage.Controls.Add(new LiteralControl(sBody.Substring(0, Inicio)));
                //Procesa el objeto
                if (sBody.Contains("accept=\"Type:"))
                {
                  if ((Session["CodContenido"] != null) && (!string.IsNullOrEmpty(Session["CodContenido"].ToString())))
                  {
                    CmsContenidos oContenidos = new CmsContenidos();
                    oContenidos.Path = oFolder.ToString();
                    oContenidos.CodContenido = Session["CodContenido"].ToString();
                    oBinaryContenido = oContenidos.ClassGet();

                    if (oBinaryContenido != null)
                    {
                      TemplateAnalysis(oHtmlPage, Tag, null, oBinaryContenido, ((oBinaryNodo.IniNodo == "V" || oBinaryNodo.IndIniNodoPhone == "V") ? true : false));
                      oBinaryContenido = null;
                    }
                    else
                      TemplateAnalysis(oHtmlPage, Tag, null, null, ((oBinaryNodo.IniNodo == "V" || oBinaryNodo.IndIniNodoPhone == "V") ? true : false));
                  }
                  else
                  {
                    TemplateAnalysis(oHtmlPage, Tag, null, null, ((oBinaryNodo.IniNodo == "V" || oBinaryNodo.IndIniNodoPhone == "V") ? true : false));
                  }
                  //ParseObjeto(oDiv, Tag);
                }
                //Quita la informacion procesada
                sBody = sBody.Substring(Fin);
              }
              else
              {
                oHtmlPage.Controls.Add(new LiteralControl(sBody));
                sBody = string.Empty;
              }
            }
            oBinaryTemplate = null;
          }
        }
      }
      oBinaryNodo = null;
    }

    protected void TemplateAnalysis(HtmlGenericControl oHtmlPage, string sTag, DataRow oDataRow, BinaryContenido oBinaryContenido, bool bIndPortal)
    {
      BinaryUsuario oBinaryUsuario;
      SysUsuario oUsuario;
      //Button oButton;
      LinkButton oButton;
      StringBuilder oHlink;
      //string sBodyRender = string.Empty;
      //string sFieldSet = string.Empty;
      //Match m;
      UserControl oControl;
      Match mOpc;
      Regex rgPatterOpcion;
      //Regex rgPatter = new Regex("<FIELDSET(.*?)<\\/FIELDSET>", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Multiline);
      //for (m = rgPatter.Match(sBody); m.Success; m = m.NextMatch())
      //{
      //  sFieldSet = m.Groups[1].ToString();
      rgPatterOpcion = new Regex("Type:(.*?)\"", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Multiline);
      mOpc = rgPatterOpcion.Match(sTag);
      if (mOpc.Success)
      {
        switch (mOpc.Groups[1].ToString())
        {
          case "oNavMenuPrincipal":
            oControl = (UserControl)this.Page.LoadControl("~/Controls/NavegacionPrincipal.ascx");
            oHtmlPage.Controls.Add(oControl);
            break;
          case "oNavMenuSegundario":
            oControl = (UserControl)this.Page.LoadControl("~/Controls/NavegacionPrincipal.ascx");
            oControl.Attributes.Add("CodNodo", pCodNodo);
            oHtmlPage.Controls.Add(oControl);
            break;
          case "oContTitulo":
            if (oDataRow != null)
            {
              //oButton = new Button();
              oButton = new LinkButton();
              oButton.ID = "btn_tit_" + oDataRow["cod_contenido"].ToString();
              oButton.Click += new EventHandler(oButton_Click);
              oButton.Text = oDataRow["titulo_contenido"].ToString();
              oButton.Attributes["CodContenido"] = oDataRow["cod_contenido"].ToString();
              oButton.CssClass = "btnTitulo";
              oHtmlPage.Controls.Add(oButton);
            }
            else
            {
              oHtmlPage.Controls.Add(new LiteralControl(oBinaryContenido.TituloContenido));
            }
            break;
          case "oContDescripcion":
            oHtmlPage.Controls.Add(new LiteralControl((oDataRow != null ? oDataRow["texto_contenido"].ToString() : oBinaryContenido.TextoContenido)));
            break;
          case "oContResumen":
            oHtmlPage.Controls.Add(new LiteralControl((oDataRow) != null ? oDataRow["resumen_contenido"].ToString() : oBinaryContenido.ResumenContenido));
            break;
          case "oContFchPub":
            oHtmlPage.Controls.Add(new LiteralControl((oDataRow != null ? oDataRow["date_contenido"].ToString() : oBinaryContenido.DateContenido)));
            break;
          case "oContFile":
            break;
          case "oContImg":
            break;
          case "oContDestacado":
            oControl = (UserControl)this.Page.LoadControl("~/Controls/ContenidoDestacado.ascx");
            oControl.Attributes.Add("CodNodo", pCodNodo);
            oHtmlPage.Controls.Add(oControl);
            break;
          case "oFichaUsuario":
            oControl = (UserControl)this.Page.LoadControl("~/Controls/FichaUsuario.ascx");
            oHtmlPage.Controls.Add(oControl);

            //oUsuario = new SysUsuario();
            //oUsuario.Path = Server.MapPath(".");
            //oUsuario.CodUsuario = Session["CodUsuarioPerfil"].ToString();
            //oBinaryUsuario = oUsuario.ClassGet();
            //if (oBinaryUsuario != null)
            //  if (int.Parse(oBinaryUsuario.CodTipo) > 1)
            //  {
            //    oControl = (UserControl)this.Page.LoadControl("~/Controls/FichaUsuario.ascx");
            //    oHtmlPage.Controls.Add(oControl);
            //  }
            break;
          case "oImagenPerfil":
            oControl = (UserControl)this.Page.LoadControl("~/Controls/ImagenPerfil.ascx");
            oHtmlPage.Controls.Add(oControl);
            break;
          case "oNombreUsuario":
            oControl = (UserControl)this.Page.LoadControl("~/Controls/NombreUsuario.ascx");
            oHtmlPage.Controls.Add(oControl);
            break;
          case "oNombreUsuarioNoButton":
            oControl = (UserControl)this.Page.LoadControl("~/Controls/NombreUsuario.ascx");
            oControl.Attributes.Add("blnLabel", "1");
            oHtmlPage.Controls.Add(oControl);
            break;
          case "oNombreUsuarioVistaPhone":
            oControl = (UserControl)this.Page.LoadControl("~/Controls/NombreUsuario.ascx");
            oControl.Attributes.Add("blnLabel", "1");
            oControl.Attributes.Add("csStyle", "titNombre");
            oHtmlPage.Controls.Add(oControl);
            break;
          case "CtrlImgCertificado":
            //if (CodUsuario != Session["CodUsuarioPerfil"].ToString())
            //{
            oControl = (UserControl)this.Page.LoadControl("~/Controls/ImgCertificado.ascx");
            oHtmlPage.Controls.Add(oControl);
            //}
            break;
          case "oLoadUsrImgProfile":
            if ((!string.IsNullOrEmpty(TipoUsuario)) && ((TipoUsuario) == "1") && (CodUsuario == Session["CodUsuarioPerfil"].ToString()))
            {
              oControl = (UserControl)this.Page.LoadControl("~/Controls/LoadImgUser.ascx");
              oHtmlPage.Controls.Add(oControl);
            }
            break;
          case "oRegistro":
            oControl = (UserControl)this.Page.LoadControl("~/Controls/CreateUsers.ascx");
            oControl.Attributes.Add("CodUsuario", CodUsuario);
            oControl.Attributes.Add("TypeUsr", (((TipoUsuario) == "1") ? "0" : "1"));
            oHtmlPage.Controls.Add(oControl);
            break;
          case "oRegistroUsrNormal":
            oControl = (UserControl)this.Page.LoadControl("~/Controls/CreateUsers.ascx");
            oControl.Attributes.Add("CodUsuario", CodUsuario);
            oControl.Attributes.Add("TypeUsr", "0");
            oHtmlPage.Controls.Add(oControl);
            break;
          case "oRegistroUsrCliente":
            oControl = (UserControl)this.Page.LoadControl("~/Controls/CreateUsers.ascx");
            oControl.Attributes.Add("CodUsuario", CodUsuario);
            oControl.Attributes.Add("TypeUsr", "1");
            oHtmlPage.Controls.Add(oControl);
            break;
          //case "oRegUsrSystem":
          //  oControl = (UserControl)this.Page.LoadControl("~/Controls/CreateUsers.ascx");
          //  oControl.Attributes.Add("CodUsuario", CodUsuario);
          //  oHtmlPage.Controls.Add(oControl);
          //  break;
          //case "oRegUsrNormal":
          //  if ((string.IsNullOrEmpty(TipoUsuario)) || ((TipoUsuario) == "1"))
          //  {
          //    oControl = (UserControl)this.Page.LoadControl("~/Controls/CreateUsers.ascx");
          //    oControl.Attributes.Add("CodUsuario", CodUsuario);
          //    oControl.Attributes.Add("TypeUsr", "0");
          //    oHtmlPage.Controls.Add(oControl);
          //  }
          //  break;
          //case "oRegUsrLevel":
          //  if ((string.IsNullOrEmpty(TipoUsuario)) || (int.Parse(TipoUsuario) > 1))
          //  {
          //    oControl = (UserControl)this.Page.LoadControl("~/Controls/CreateUsers.ascx");
          //    oControl.Attributes.Add("CodUsuario", CodUsuario);
          //    oControl.Attributes.Add("TypeUsr", "1");
          //    oHtmlPage.Controls.Add(oControl);
          //  }
          //  break;
          //**************************************
          //*************** PORTAL ***************
          //**************************************
          case "CntrGaleryPortal":
            oControl = (UserControl)this.Page.LoadControl("~/Controls/GaleryControl.ascx");
            oControl.Attributes.Add("DataType", "USR");
            if (bIndPortal)
              oControl.Attributes.Add("IndUsrDest", "V");
            oHtmlPage.Controls.Add(oControl);
            break;
          case "CntrGaleryTipo1":
            oControl = (UserControl)this.Page.LoadControl("~/Controls/GaleryControl.ascx");
            oControl.Attributes.Add("DataType", "USR");
            oControl.Attributes.Add("TypeUsr", "2");
            if (bIndPortal)
              oControl.Attributes.Add("IndUsrDest", "V");
            oHtmlPage.Controls.Add(oControl);
            break;
          case "CntrGaleryTipo2":
            oControl = (UserControl)this.Page.LoadControl("~/Controls/GaleryControl.ascx");
            oControl.Attributes.Add("DataType", "USR");
            oControl.Attributes.Add("TypeUsr", "3");
            if (bIndPortal)
              oControl.Attributes.Add("IndUsrDest", "V");
            oHtmlPage.Controls.Add(oControl);
            break;
          case "CntrGaleryTipo3":
            oControl = (UserControl)this.Page.LoadControl("~/Controls/GaleryControl.ascx");
            oControl.Attributes.Add("DataType", "USR");
            oControl.Attributes.Add("TypeUsr", "4");
            if (bIndPortal)
              oControl.Attributes.Add("IndUsrDest", "V");
            oHtmlPage.Controls.Add(oControl);
            break;
          //**************************************
          //*************** PHONE ****************
          //**************************************
          case "CntrGaleryPortalPhone":
            oControl = (UserControl)this.Page.LoadControl("~/Controls/GaleryControlPhone.ascx");
            oControl.Attributes.Add("DataType", "USR");
            if (bIndPortal)
              oControl.Attributes.Add("IndUsrDest", "V");
            oHtmlPage.Controls.Add(oControl);
            break;
          case "CntrGaleryPhoneTipo1":
            oControl = (UserControl)this.Page.LoadControl("~/Controls/GaleryControlPhone.ascx");
            oControl.Attributes.Add("DataType", "USR");
            oControl.Attributes.Add("TypeUsr", "2");
            if (bIndPortal)
              oControl.Attributes.Add("IndUsrDest", "V");
            oHtmlPage.Controls.Add(oControl);
            break;
          case "CntrGaleryPhoneTipo2":
            oControl = (UserControl)this.Page.LoadControl("~/Controls/GaleryControlPhone.ascx");
            oControl.Attributes.Add("DataType", "USR");
            oControl.Attributes.Add("TypeUsr", "3");
            if (bIndPortal)
              oControl.Attributes.Add("IndUsrDest", "V");
            oHtmlPage.Controls.Add(oControl);
            break;
          case "CntrGaleryPhoneTipo3":
            oControl = (UserControl)this.Page.LoadControl("~/Controls/GaleryControlPhone.ascx");
            oControl.Attributes.Add("DataType", "USR");
            oControl.Attributes.Add("TypeUsr", "4");
            if (bIndPortal)
              oControl.Attributes.Add("IndUsrDest", "V");
            oHtmlPage.Controls.Add(oControl);
            break;
          //**************************************
          //**************************************
          case "CntrGaleryImgContenido":
            oControl = (UserControl)this.Page.LoadControl("~/Controls/GalleryImgContent.ascx");
            oControl.Attributes.Add("DataType", "CONT");
            oControl.Attributes.Add("pContenido", (oDataRow != null ? oDataRow["date_contenido"].ToString() : oBinaryContenido.CodContenido));
            oHtmlPage.Controls.Add(oControl);
            break;
          case "CtrContactenos":
            oControl = (UserControl)this.Page.LoadControl("~/Controls/Contactenos.ascx");
            oHtmlPage.Controls.Add(oControl);
            break;
          case "EmailToUser":
            oControl = (UserControl)this.Page.LoadControl("~/Controls/EmailToUser.ascx");
            oHtmlPage.Controls.Add(oControl);
            break;
          case "CtrAdmVentaArriendo":
            oControl = (UserControl)this.Page.LoadControl("~/Controls/AdmVentaArriendo.ascx");
            oHtmlPage.Controls.Add(oControl);
            break;
          case "CtrLogin":
            oControl = (UserControl)this.Page.LoadControl("~/Controls/LoginUsers.ascx");
            oHtmlPage.Controls.Add(oControl);
            break;
          case "CtrViewImages":
            oControl = (UserControl)this.Page.LoadControl("~/Controls/viewImagesUsers.ascx");
            oHtmlPage.Controls.Add(oControl);
            break;
          case "CtrViewImagesUsrPhone":
            oControl = (UserControl)this.Page.LoadControl("~/Controls/viewImageUsrPhone.ascx");
            oHtmlPage.Controls.Add(oControl);
            break;
          case "CtrRanking":
            oControl = (UserControl)this.Page.LoadControl("~/Controls/ZoneRankingProfile.ascx");
            oHtmlPage.Controls.Add(oControl);
            break;
          case "CtrBusqueda":
            break;
          case "CtrGoogle":
            StringBuilder js = new StringBuilder();
            js.Append("<script type=\"text/javascript\"> ");
            js.Append("var geocoder; ");
            js.Append("var map; ");
            js.Append("geocoder = new google.maps.Geocoder(); ");
            js.Append("var latlng = new google.maps.LatLng(document.getElementById(\"myLatitude\").value,document.getElementById(\"myLongitude\").value); ");
            js.Append("var myOptions = { ");
            js.Append("zoom: 16, ");
            js.Append("center: latlng, ");
            js.Append("mapTypeId: google.maps.MapTypeId.ROADMAP ");
            js.Append("}; ");
            js.Append("map = new google.maps.Map(document.getElementById(\"map\"), myOptions); ");
            js.Append("var marker = new google.maps.Marker({ ");
            js.Append("position: latlng, ");
            js.Append("title: document.getElementById(\"myAddress\").value ");
            js.Append("}); ");
            js.Append("marker.setMap(map); ");
            js.Append("</script> ");
            oHtmlPage.Controls.Add(new LiteralControl(js.ToString()));
            break;
          case "CtrGoogleSearch":
            break;
          case "CtrRequestFriend":
            oControl = (UserControl)this.Page.LoadControl("~/Controls/RelUsuarioControl.ascx");
            oControl.Attributes.Add("Method", "FriendRequest");
            oHtmlPage.Controls.Add(oControl);
            break;
          case "CtrConfirmRequestFriend":
            oControl = (UserControl)this.Page.LoadControl("~/Controls/RelUsuarioControl.ascx");
            oControl.Attributes.Add("Method", "ConfirmFriendRequest");
            oHtmlPage.Controls.Add(oControl);
            break;
          case "CtrFriend":
            oControl = (UserControl)this.Page.LoadControl("~/Controls/viewFriendUser.ascx");
            oHtmlPage.Controls.Add(oControl);
            break;
          case "CtrComment":
            oUsuario = new SysUsuario();
            oUsuario.Path = Server.MapPath(".");
            oUsuario.CodUsuario = Session["CodUsuarioPerfil"].ToString();
            oBinaryUsuario = oUsuario.ClassGet();
            if (oBinaryUsuario != null)
              if (oBinaryUsuario.CodTipo != "1")
              {
                oControl = (UserControl)this.Page.LoadControl("~/Controls/MsnUsersControl.ascx");
                oControl.Attributes.Add("Method", "Comment");
                oHtmlPage.Controls.Add(oControl);
              }
            oBinaryUsuario = null;
            break;
          case "CtrUserComment":
            oControl = (UserControl)this.Page.LoadControl("~/Controls/MsnUsersControl.ascx");
            oControl.Attributes.Add("Method", "GetComments");
            oHtmlPage.Controls.Add(oControl);
            break;
          case "CommentToUser":
            oControl = (UserControl)this.Page.LoadControl("~/Controls/MsnUsersControl.ascx");
            oControl.Attributes.Add("Method", "CommentToUser");
            oHtmlPage.Controls.Add(oControl);
            break;
          case "CtrRss":
            oHlink = new StringBuilder();
            oHlink.Append("<a href=\"http://").Append(HttpContext.Current.Request.ServerVariables["HTTP_HOST"].ToString() + "/rss.xml\"");
            oHlink.Append(" title=\"").Append(oCulture.GetResource("Mensajes", "sMessage12")).Append("\"");
            oHlink.Append(" class=\"IMGRss\" target=\"_blank\"></a>");
            oHtmlPage.Controls.Add(new LiteralControl(oHlink.ToString()));
            break;
          case "CtrFbck":
            oHlink = new StringBuilder();
            oHlink.Append("<a href=\"http://www.facebook.com/").Append(HttpContext.Current.Application["ClientFacebook"].ToString());
            oHlink.Append("\" title=\"").Append(oCulture.GetResource("Mensajes", "sMessage13")).Append("\"");
            oHlink.Append(" class=\"IMGFBCK\" target=\"_blank\"></a>");
            oHtmlPage.Controls.Add(new LiteralControl(oHlink.ToString()));
            break;
          case "CtrTwitter":
            oHlink = new StringBuilder();
            oHlink.Append("<a href=\"http://twitter.com/").Append(HttpContext.Current.Application["ClientTwitter"].ToString());
            oHlink.Append("\" title=\"").Append(oCulture.GetResource("Mensajes", "sMessage14")).Append("\"");
            oHlink.Append(" class=\"IMGTWITTER\" target=\"_blank\"></a>");
            oHtmlPage.Controls.Add(new LiteralControl(oHlink.ToString()));
            break;
          case "CtrFollow":
            oControl = (UserControl)this.Page.LoadControl("~/Controls/TwittControl.ascx");
            oControl.Attributes.Add("Method", "getFollow");
            oHtmlPage.Controls.Add(oControl);
            break;
          case "CrtCloseSession":
            if ((Session["USUARIO"] != null) && (!string.IsNullOrEmpty(Session["USUARIO"].ToString())))
            {
              //oButton = new Button();
              oButton = new LinkButton();
              oButton.ID = "btn_close";
              oButton.Click += new EventHandler(btnCloseSession_Click);
              oButton.Text = oCulture.GetResource("Global", "btnCerrarSession");
              oButton.CssClass = "btnCloseSession";
              oButton.CausesValidation = false;
              oHtmlPage.Controls.Add(oButton);
            }
            break;
          case "CrtStartSession":
            if ((Session["USUARIO"] == null) || (string.IsNullOrEmpty(Session["USUARIO"].ToString())))
            {
              //oButton = new Button();
              oButton = new LinkButton();
              oButton.ID = "btn_start";
              oButton.Click += new EventHandler(btnStartSession_Click);
              oButton.Text = oCulture.GetResource("Global", "btnStarSession");
              oButton.CssClass = "btnStarSession";
              oButton.CausesValidation = false;
              oHtmlPage.Controls.Add(oButton);
            }
            break;
          case "CrtPolSecure":
            //oButton = new Button();
            oButton = new LinkButton();
            oButton.ID = "btn_polsecure";
            oButton.Click += new EventHandler(btnPolSecure_Click);
            oButton.Text = oCulture.GetResource("Global", "btnPolSecure");
            oButton.CssClass = "btnPolSecure";
            oButton.CausesValidation = false;
            oHtmlPage.Controls.Add(oButton);
            break;
          case "CrtTermUse":
            //oButton = new Button();
            oButton = new LinkButton();
            oButton.ID = "btn_termuse";
            oButton.Click += new EventHandler(btnTermUse_Click);
            oButton.Text = oCulture.GetResource("Global", "btnTermUse");
            oButton.CssClass = "btnTermUse";
            oButton.CausesValidation = false;
            oHtmlPage.Controls.Add(oButton);
            break;
          case "CrtOlvidoPassword":
            oControl = (UserControl)this.Page.LoadControl("~/Controls/OlvidoPassword.ascx");
            oHtmlPage.Controls.Add(oControl);
            break;
          case "CrtRestaurarPassword":
            oControl = (UserControl)this.Page.LoadControl("~/Controls/RestarPassword.ascx");
            oHtmlPage.Controls.Add(oControl);
            break;
          case "CrtRegistrate":
            if ((Session["USUARIO"] == null) || (string.IsNullOrEmpty(Session["USUARIO"].ToString())))
            {
              //oButton = new Button();
              oButton = new LinkButton();
              oButton.ID = "btn_Registrate";
              oButton.Click += new EventHandler(btnRegistrate_Click);
              oButton.Text = oCulture.GetResource("Global", "btnRegistrate");
              oButton.CssClass = "btnRegistrate";
              oButton.CausesValidation = false;
              oHtmlPage.Controls.Add(oButton);
            }
            break;
          case "CrtIniPhone":
            //oButton = new Button();
            oButton = new LinkButton();
            oButton.ID = "btn_ini_phone";
            oButton.Click += new EventHandler(btnIniPhone_Click);
            oButton.Text = oCulture.GetResource("Global", "btnIniPhone");
            oButton.CssClass = "btnIniPhone";
            oButton.CausesValidation = false;
            oHtmlPage.Controls.Add(oButton);
            break;
          case "CrtContPhone":
            //oButton = new Button();
            oButton = new LinkButton();
            oButton.ID = "btn_cont_phone";
            oButton.Click += new EventHandler(btnContPhone_Click);
            oButton.Text = oCulture.GetResource("Global", "btnContPhone");
            oButton.CssClass = "btnContPhone";
            oButton.CausesValidation = false;
            oHtmlPage.Controls.Add(oButton);
            break;
          case "Apps":
            break;
          default:
            string sZona = mOpc.Groups[1].ToString();
            if (sZona.Contains("zona_"))
            {
              StringBuilder oFolder = new StringBuilder();
              oFolder.Append(Server.MapPath("."));

              string pCodZona = sZona.Substring(5);
              CmsZona oCmsZona = new CmsZona();
              oCmsZona.Path = oFolder.ToString();
              oCmsZona.CodZona = pCodZona;
              oBinaryZona = oCmsZona.ClassGet();
              if (oBinaryZona != null)
                if (oBinaryZona.IndDespCont.ToString() == "V")
                {
                  //oFolder.Append(@"\binary\");
                  DataTable dContenidosNodo = oWeb.DeserializarTbl(oFolder.ToString(), "Contenido_n" + pCodNodo + ".bin");
                  if (dContenidosNodo != null)
                    if (dContenidosNodo.Rows.Count > 0)
                    {
                      foreach (DataRow oRow in dContenidosNodo.Rows)
                      {
                        string sBody = oBinaryZona.TextoZona;
                        while (!string.IsNullOrEmpty(sBody))
                        {
                          if (sBody.Contains("accept=\"Type:"))
                          {
                            int Inicio = sBody.IndexOf("accept=\"Type:");
                            Inicio = sBody.LastIndexOf("<fieldset", Inicio);
                            int Fin = sBody.IndexOf("</fieldset>", Inicio) + 11;
                            string Tag = sBody.Substring(Inicio, Fin - Inicio);

                            //Escribe el html antes del objeto
                            oHtmlPage.Controls.Add(new LiteralControl(sBody.Substring(0, Inicio)));
                            //Procesa el objeto
                            if (sBody.Contains("accept=\"Type:"))
                            {
                              TemplateAnalysis(oHtmlPage, Tag, oRow, null, bIndPortal);
                              //ParseObjeto(oDiv, Tag);
                            }
                            //Quita la informacion procesada
                            sBody = sBody.Substring(Fin);
                          }
                          else
                          {
                            oHtmlPage.Controls.Add(new LiteralControl(sBody));
                            sBody = string.Empty;
                          }
                        }
                      }
                    }
                  dContenidosNodo = null;
                }
                else
                {
                  string sBody = oBinaryZona.TextoZona;
                  while (!string.IsNullOrEmpty(sBody))
                  {
                    if (sBody.Contains("accept=\"Type:"))
                    {
                      int Inicio = sBody.IndexOf("accept=\"Type:");
                      Inicio = sBody.LastIndexOf("<fieldset", Inicio);
                      int Fin = sBody.IndexOf("</fieldset>", Inicio) + 11;
                      string Tag = sBody.Substring(Inicio, Fin - Inicio);

                      //Escribe el html antes del objeto
                      oHtmlPage.Controls.Add(new LiteralControl(sBody.Substring(0, Inicio)));
                      //Procesa el objeto
                      if (sBody.Contains("accept=\"Type:"))
                      {
                        TemplateAnalysis(oHtmlPage, Tag, null, null, bIndPortal);
                        //ParseObjeto(oDiv, Tag);
                      }
                      //Quita la informacion procesada
                      sBody = sBody.Substring(Fin);
                    }
                    else
                    {
                      oHtmlPage.Controls.Add(new LiteralControl(sBody));
                      sBody = string.Empty;
                    }
                  }
                }
              oBinaryZona = null;
            }
            else if (sZona.Contains("banners_"))
            {
              string pCodBanner = sZona.Substring(8);
              DataTable dBanner = oWeb.DeserializarTbl(Server.MapPath(".").ToString(), "Banner.bin");
              if (dBanner != null) { 
                if (dBanner.Rows.Count > 0){
                  DataRow[] dRow = dBanner.Select(" cod_banner = " + pCodBanner);
                  if (dRow != null) {
                    if (dRow.Count() > 0) {
                      if (dRow[0]["tipo_banner"].ToString() == "S")
                        oControl = (UserControl)this.Page.LoadControl("~/Controls/BannerSlider.ascx");
                      else if (dRow[0]["tipo_banner"].ToString() == "R")
                        oControl = (UserControl)this.Page.LoadControl("~/Controls/BannerRotator.ascx");
                      else
                        oControl = (UserControl)this.Page.LoadControl("~/Controls/Carrusel.ascx");
                      oControl.Attributes.Add("CodBanner", pCodBanner);
                      oHtmlPage.Controls.Add(oControl);
                    }
                  }
                  dRow = null;
                }
              }
              dBanner = null;

            }
            break;
        }
      }
      //}
      //sBodyRender = sBody;
      //return sBodyRender;
    }

    void oButton_Click(object sender, EventArgs e)
    {
      Session["CodContenido"] = (sender as LinkButton).Attributes["CodContenido"].ToString();
      Page.Response.Redirect(".");
    }
    void btnCloseSession_Click(object sender, EventArgs e)
    {
      DateTime dNow = DateTime.Now;
      HttpCookie oHttpCookie = new HttpCookie("AccesCookie");
      oHttpCookie.Value = string.Empty;
      oHttpCookie.Expires = dNow.AddYears(100);
      Response.Cookies.Add(oHttpCookie);

      Session["CodContenido"] = string.Empty;
      Session["CodNodo"] = string.Empty;
      Session["USUARIO"] = string.Empty;
      Session["CodUsuarioPerfil"] = string.Empty;
      ViewState["CodNodo"] = string.Empty;

      Page.Response.Redirect(".");
    }
    void btnStartSession_Click(object sender, EventArgs e)
    {
      Session["CodNodo"] = getNodoByType("lgn");
      Page.Response.Redirect(".");
    }
    void btnRegistrate_Click(object sender, EventArgs e)
    {
      Session["CodNodo"] = getNodoByType("rgs");
      Page.Response.Redirect(".");
    }
    void btnIniPhone_Click(object sender, EventArgs e)
    {
      Session["CodNodo"] = getNodoByType("iph");
      Page.Response.Redirect(".");
    }
    void btnPolSecure_Click(object sender, EventArgs e)
    {
      Session["CodNodo"] = getNodoByType("pts");
      Page.Response.Redirect(".");
    }
    void btnTermUse_Click(object sender, EventArgs e)
    {
      Session["CodNodo"] = getNodoByType("tru");
      Page.Response.Redirect(".");
    }
    void btnContPhone_Click(object sender, EventArgs e)
    {
      Session["CodNodo"] = getNodoByType("cph");
      Page.Response.Redirect(".");
    }
  }
}
