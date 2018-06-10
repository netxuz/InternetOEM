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
using System.Xml;
using System.Xml.Linq;
using System.Text;

using OnlineServices.Conn;
using OnlineServices.Method;
using OnlineServices.CmsData;
using OnlineServices.SystemData;

namespace ICommunity
{
  public partial class Parametros : System.Web.UI.Page
  {
    Web oWeb = new Web();
    Culture oCulture = new Culture();
    protected void Page_Load(object sender, EventArgs e)
    {
      oWeb.ValidaSessionAdm();
      if (!IsPostBack)
      {
        btnGrabar.Text = oCulture.GetResource("Global", "btnGrabar");
        btnRSS.Text = oCulture.GetResource("Global", "btnRSS");
      }
      getParametros();
    }

    protected void getParametros()
    {
      Label oLabel;
      TextBox oTextBox;
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        SysParametros oParametros = new SysParametros(ref oConn);
        DataTable dParam = oParametros.Get();
        if (dParam != null)
          if (dParam.Rows.Count > 0)
          {
            foreach (DataRow oRow in dParam.Rows)
            {
              opciones.Controls.Add(new LiteralControl("<div>"));
              opciones.Controls.Add(new LiteralControl("<div>"));

              oLabel = new Label();
              oLabel.Text = oRow["nom_parametro"].ToString();
              opciones.Controls.Add(oLabel);

              opciones.Controls.Add(new LiteralControl("<div>"));
              opciones.Controls.Add(new LiteralControl("<div>"));

              oTextBox = new TextBox();
              oTextBox.ID = "txt_" + oRow["cod_codigo"].ToString();
              oTextBox.Text = oRow["valor_parametro"].ToString();
              opciones.Controls.Add(oTextBox);

              opciones.Controls.Add(new LiteralControl("<div>"));
              opciones.Controls.Add(new LiteralControl("<div>"));
            }
          }
        dParam = null;

        oConn.Close();
      }
    }

    protected void btnGrabar_Click(object sender, EventArgs e)
    {
      SysParametros oParametros;
      TextBox oTextBox;
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        oParametros = new SysParametros(ref oConn);
        DataTable dParam = oParametros.Get();
        if (dParam != null)
          if (dParam.Rows.Count > 0)
          {
            foreach (DataRow oRow in dParam.Rows)
            {
              oTextBox = (TextBox)Page.FindControl("txt_" + oRow["cod_codigo"].ToString());
              if (oTextBox != null)
              {
                oParametros = new SysParametros(ref oConn);
                oParametros.CodCodigo = oRow["cod_codigo"].ToString();
                oParametros.ValorParametro = oTextBox.Text;
                oParametros.Accion = "EDITAR";
                oParametros.Put();

                if (string.IsNullOrEmpty(oParametros.Error))
                {
                  switch (oRow["cod_codigo"].ToString())
                  {
                    case "1":
                      Application["SmtpServer"] = oTextBox.Text;
                      break;
                    case "2":
                      Application["EmailSender"] = oTextBox.Text;
                      break;
                    case "3":
                      Application["NameSender"] = oTextBox.Text;
                      break;
                    case "4":
                      Application["SiteName"] = oTextBox.Text;
                      break;
                    case "5":
                      Application["WinRadSkin"] = oTextBox.Text;
                      break;
                    case "6":
                      Application["ClientFacebook"] = oTextBox.Text;
                      break;
                    case "7":
                      Application["ClientTwitter"] = oTextBox.Text;
                      break;
                    case "13":
                      Application["URLSite"] = oTextBox.Text;
                      break;
                    case "14":
                      Application["GoogleSiteVerification"] = oTextBox.Text;
                      break;
                    case "15":
                      Application["PortSmtpServer"] = oTextBox.Text;
                      break;
                    case "16":
                      Application["UserSmtp"] = oTextBox.Text;
                      break;
                    case "17":
                      Application["PwdSmtp"] = oTextBox.Text;
                      break;
                  }
                }
              }
            }
          }
        dParam = null;

        StringBuilder sPath = new StringBuilder();
        sPath.Append(Server.MapPath("."));
        sPath.Append(@"\binary\");

        oParametros.SerializaTblParametros(ref oConn, sPath.ToString());
        oConn.Close();
      }
    }

    protected void btnRSS_Click(object sender, EventArgs e)
    {
      string sUrlSite = "http://" + Application["URLSite"].ToString();
      string sNameSite = string.Empty;
      string sTitleHeaderNodo = string.Empty;
      string sMessage = oCulture.GetResource("Mensajes", "sMessage11");
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        CmsNodos oNodos = new CmsNodos(ref oConn);
        oNodos.IniNodo = "V";
        DataTable dNodo = oNodos.Get();
        if (dNodo != null)
          if (dNodo.Rows.Count > 0)
            sTitleHeaderNodo = dNodo.Rows[0]["titleheader_nodo"].ToString();
        dNodo = null;


        CmsContenidos oContenidos = new CmsContenidos(ref oConn);
        oContenidos.IndRss = "1";
        DataTable dContenidos = oContenidos.Get();
        if (dContenidos != null)
          if (dContenidos.Rows.Count > 0)
          {
            var articlesEntries = from p in dContenidos.AsEnumerable()
                                  orderby p["date_contenido"]
                                  select new XElement("item",
                                    new XElement("title", p["titulo_contenido"]),
                                    //new XElement("link", string.Format(sUrlSite + "/Default.aspx?CodContenido={0}", p["cod_contenido"])),
                                    new XElement("link", string.Format(sUrlSite)), // + "/Default.aspx?CodContenido={0}", p["cod_contenido"])),
                                    new XElement("pubDate", p["date_contenido"]),
                                    new XElement("description", p["resumen_contenido"]));

            XDocument doc = new XDocument(
            new XDeclaration("1.0", "utf-8", "yes"),
            new XElement("rss",
              new XAttribute("version", "2.0"),
              new XElement("channel",
                new XElement("title", Application["SiteName"].ToString()),
                new XElement("description", sTitleHeaderNodo),
                new XElement ("lastBuildDate", DateTime.UtcNow.ToUniversalTime()),
                new XElement("link", sUrlSite),
                articlesEntries
                )
               )
            );
            StringBuilder cPath = new StringBuilder();
            cPath.Append(Server.MapPath(".")).Append(@"\rss.xml");
            doc.Save(cPath.ToString());
            sMessage = oCulture.GetResource("Mensajes", "sMessage10");
          }
        dContenidos = null;
        oConn.Close();

        StringBuilder js = new StringBuilder();
        js.Append("function LgRespuesta() {");
        js.Append(" window.radalert('").Append(sMessage).Append("', 400, 100,'" + oCulture.GetResource("Global", "MnsAtencion") + "'); ");
        js.Append(" Sys.Application.remove_load(LgRespuesta); ");
        js.Append("};");
        js.Append("Sys.Application.add_load(LgRespuesta);");
        Page.ClientScript.RegisterStartupScript(this.GetType(), "LgRespuesta", js.ToString(), true);
      }
    }


  }
}
