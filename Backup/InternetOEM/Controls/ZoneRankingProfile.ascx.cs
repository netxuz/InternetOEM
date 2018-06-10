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

using OnlineServices.Method;
using OnlineServices.AppData;
using OnlineServices.SystemData;

namespace ICommunity.Controls
{
  public partial class ZoneRankingProfile : System.Web.UI.UserControl
  {
    private OnlineServices.Method.Usuario oIsUsuario;
    private string pCodUsuario = string.Empty;
    private Web oWeb = new Web();
    protected void Page_Load(object sender, EventArgs e)
    {
      bool blnUsuario = false;
      oIsUsuario = oWeb.GetObjUsuario();
      if ((Session["CodUsuarioPerfil"] != null) && (!string.IsNullOrEmpty(Session["CodUsuarioPerfil"].ToString())) && (!string.IsNullOrEmpty(oIsUsuario.CodUsuario)))
      {
        BinaryUsuario dUsuario = new BinaryUsuario();
        SysUsuario oUsuario = new SysUsuario();
        oUsuario.Path = Server.MapPath(".");
        oUsuario.CodUsuario = Session["CodUsuarioPerfil"].ToString();
        dUsuario = oUsuario.ClassGet();
        if (dUsuario != null)
          if (dUsuario.CodTipo != "1")
          {
            blnUsuario = true;
            RdRankingUSer.Value = (!string.IsNullOrEmpty(dUsuario.NotaRanking) ? decimal.Parse(dUsuario.NotaRanking) : 0);
          }
        dUsuario = null;

        if (blnUsuario)
        {
          DataTable oRanking = oWeb.DeserializarTbl(Server.MapPath("."), "Ranking.bin");
          if (oRanking != null)
            if (oRanking.Rows.Count > 0)
            {
              DataRow[] oRows = oRanking.Select("cod_usuario =" + Session["CodUsuarioPerfil"].ToString() + " and cod_cliente =" + oIsUsuario.CodUsuario + " and fch_ranking >= '" + DateTime.Now.AddDays(-10).ToString("yyyy-MM-dd HH:mm:ss") + "'");
              if (oRows != null)
                if (oRows.Count() == 0)
                {
                  HyperLink1.Visible = true;
                  HyperLink1.NavigateUrl = "ranking.aspx?CodUsuario=" + Session["CodUsuarioPerfil"].ToString();
                }
              oRows = null;
            }
            else
            {
              HyperLink1.Visible = true;
              HyperLink1.NavigateUrl = "ranking.aspx?CodUsuario=" + Session["CodUsuarioPerfil"].ToString();
            }
          oRanking = null;

          HyperLink2.NavigateUrl = "ranking.aspx?CodUsuario=" + Session["CodUsuarioPerfil"].ToString() + "&H=t";
          HyperLink3.NavigateUrl = "recomiendame.aspx?CodUsuario=" + Session["CodUsuarioPerfil"].ToString();
        }
        else {
          RdRankingUSer.Visible = false;
          HyperLink1.Visible = false;
          HyperLink2.Visible = false;
          HyperLink3.Visible = false;
        }
      }
    }

  }
}