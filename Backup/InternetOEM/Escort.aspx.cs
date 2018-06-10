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
using OnlineServices.AppData;
using OnlineServices.SystemData;
using ICommunity.Controls;

namespace ICommunity
{
  public partial class viewProfile : System.Web.UI.Page
  {
    private OnlineServices.Method.Usuario oIsUsuario;
    private string pCodUsuario = string.Empty;
    private Web oWeb = new Web();
    private Log oLog = new Log();

    protected void Page_Load(object sender, EventArgs e)
    {
      DataTable oRanking;
      DataRow[] oRows;
      oIsUsuario = oWeb.GetObjUsuario();
      if (!IsPostBack)
      {
        pCodUsuario = oWeb.GetData("CodUsuario");
        if (getUser(pCodUsuario))
        {
          //ImgCertificado.CodUsuario = pCodUsuario;
          if (!string.IsNullOrEmpty(oIsUsuario.CodUsuario))
          {
            oRanking = oWeb.DeserializarTbl(Server.MapPath("."), "Ranking.bin");
            if (oRanking != null)
              if (oRanking.Rows.Count > 0)
              {
                oRows = oRanking.Select("cod_usuario =" + pCodUsuario + " and cod_cliente =" + oIsUsuario.CodUsuario + " and fch_ranking >= '" + DateTime.Now.AddDays(-10).ToString("yyyy-MM-dd HH:mm:ss") + "'");
                if (oRows != null)
                  if (oRows.Count() == 0)
                  {
                    HyperLink1.Visible = true;
                    HyperLink1.NavigateUrl = "ranking.aspx?CodUsuario=" + pCodUsuario;
                  }
                oRows = null;
              }
              else
              {
                HyperLink1.Visible = true;
                HyperLink1.NavigateUrl = "ranking.aspx?CodUsuario=" + pCodUsuario;
              }
            oRanking = null;
          }
          else
            HyperLink1.Visible = false;

          oRanking = oWeb.DeserializarTbl(Server.MapPath("."), "Ranking.bin");
          if (oRanking != null)
            if (oRanking.Rows.Count > 0)
            {
              oRows = oRanking.Select("cod_usuario = " + pCodUsuario);
              if (oRows != null)
                if (oRows.Count() > 0)
                  HyperLink2.NavigateUrl = "ranking.aspx?CodUsuario=" + pCodUsuario + "&H=t";
                else
                  HyperLink2.Visible = false;
              oRows = null;
            }
          oRanking = null;
          HyperLink3.NavigateUrl = "recomiendame.aspx?CodUsuario=" + pCodUsuario;
          TwittControl.Attributes["CodSegUsuario"] = pCodUsuario;
          TwittControl.Attributes["Method"] = "putFollow";
          if (((Session["USUARIO"] != null) && (!string.IsNullOrEmpty(Session["USUARIO"].ToString()))) && (int.Parse(oIsUsuario.Tipo) > 1))
            TwittControl.Visible = false;

          oLog.CodEvtLog = "004";
          oLog.IdUsuario = oLog.IdUsuario = (!string.IsNullOrEmpty(oIsUsuario.CodUsuario) ? oIsUsuario.CodUsuario : "-1");
          oLog.ObsLog = "<VISTAPERFIL>" + pCodUsuario;
          //oLog.putLog();

          HtmlGenericControl oDivSlider = (HtmlGenericControl)this.FindControl("datosbasicos");
          FichaUsuario oFichaUsuario = new FichaUsuario();
          oFichaUsuario.UsrPortal = pCodUsuario;
          oDivSlider.Controls.Add(oFichaUsuario);
        }
        else
        {
          TwittControl.Visible = false;
          Response.Redirect(".");
        }
      }
    }

    public bool getUser(string pCodUsuario)
    {
      string sNombre = string.Empty;
      bool bValUsr = false;
      HtmlMeta oMeta;
      int iCountHeader = 0;
      BinaryUsuario dUsuario = new BinaryUsuario();
      SysUsuario oUsuario = new SysUsuario();
      oUsuario.Path = Server.MapPath(".");
      oUsuario.CodUsuario = pCodUsuario;
      dUsuario = oUsuario.ClassGet();
      if (dUsuario != null)
      {
        bValUsr = (dUsuario.EstUsuario == "V" ? true : false);
        if (bValUsr)
        {
          sNombre = dUsuario.NomUsuario.ToString() + " " + dUsuario.ApeUsuario.ToString();
          lblNombre.Text = sNombre.ToString().Trim();
          RdRankingUSer.Value = (!string.IsNullOrEmpty(dUsuario.NotaRanking) ? decimal.Parse(dUsuario.NotaRanking) : 0);

          Page.Title = "Escort " + sNombre.ToString().Trim() + ", Escorts en Chile - Santiago";
          oMeta = new HtmlMeta();
          oMeta.Attributes.Add("name", "title");
          oMeta.Attributes.Add("content", "Escort " + sNombre.ToString().Trim() + ", Escorts en Chile - Santiago");
          Page.Header.Controls.AddAt(iCountHeader, oMeta);
          iCountHeader++;

          oMeta = new HtmlMeta();
          oMeta.Attributes.Add("name", "description");
          oMeta.Attributes.Add("content", "Escort " + sNombre.ToString().Trim() + ", Escorts en Chile - Santiago");
          Page.Header.Controls.AddAt(iCountHeader, oMeta);
          iCountHeader++;

          oMeta = new HtmlMeta();
          oMeta.Attributes.Add("property", "og:description");
          oMeta.Attributes.Add("content", "Escort " + sNombre.ToString().Trim() + ", Escorts en Chile - Santiago");
          Page.Header.Controls.AddAt(iCountHeader, oMeta);
          iCountHeader++;

          StringBuilder sLi;
          string cPath = Server.MapPath(".");
          DataTable dUserArchivo = oWeb.DeserializarTbl(cPath, "UserArchivo_" + pCodUsuario + ".bin");
          if (dUserArchivo != null)
            if (dUserArchivo.Rows.Count > 0)
            {
              jGallery.Controls.Add(new LiteralControl("<ul id=\"tumbs\" class=\"vwImgUsrTumbs\">"));
              foreach (DataRow oRow in dUserArchivo.Rows)
              {
                sLi = new StringBuilder();
                sLi.Append("<li class=\"UrlLi\" title=\"http://").Append(Application["URLSite"].ToString()).Append("/rps_onlineservice/escorts/escort_").Append(oRow["cod_usuario"].ToString()).Append("/").Append(oRow["nom_archivo"].ToString()).Append("\"></li>");
                jGallery.Controls.Add(new LiteralControl(sLi.ToString()));
              }
              jGallery.Controls.Add(new LiteralControl("</ul>"));
            }
          dUserArchivo = null;
        }
      }
      dUsuario = null;
      return bValUsr;
    }
  }
}
