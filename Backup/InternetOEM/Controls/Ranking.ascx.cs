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

using Telerik.Web.UI;
using OnlineServices.AppData;
using OnlineServices.Method;

namespace ICommunity.Controls
{
  public partial class Ranking : System.Web.UI.UserControl
  {
    private OnlineServices.Method.Web oWeb = new OnlineServices.Method.Web();
    private OnlineServices.Method.Culture oCulture = new OnlineServices.Method.Culture();
    private OnlineServices.Method.Usuario oIsUsuario;

    protected void Page_Load(object sender, EventArgs e)
    {
      oIsUsuario = oWeb.GetObjUsuario();
      putUserRanking();
    }

    protected void putUserRanking() {
      Label sPregRanking;
      RadRating oRadRating;
      StringBuilder sSQL = new StringBuilder();
      sSQL.Append(" est_preg_ranking = 'V' ");
      DataTable dPregRanking = oWeb.DeserializarTbl(Server.MapPath("."), "PregRanking.bin");
      if (dPregRanking != null) {
        if (dPregRanking.Rows.Count > 0) {
          DataRow[] oRows = dPregRanking.Select(sSQL.ToString());
          if (oRows != null) {
            if (oRows.Count() > 0) {
              foreach (DataRow oRow in oRows) {
                Controls.Add(new LiteralControl("<div class=\"mdlpregrank\">"));
                Controls.Add(new LiteralControl("<div class=\"labelpregrank\">"));
                sPregRanking = new Label();
                sPregRanking.Text = oRow["preg_ranking"].ToString();
                Controls.Add(sPregRanking);
                Controls.Add(new LiteralControl("</div>"));

                Controls.Add(new LiteralControl("<div class=\"objrankpreg\">"));
                oRadRating = new RadRating();
                oRadRating.ID = "RdRating_" + oRow["cod_preg_ranking"].ToString();
                oRadRating.ItemCount = 7;
                oRadRating.SelectionMode = RatingSelectionMode.Continuous;
                oRadRating.Precision = RatingPrecision.Item;
                oRadRating.Orientation = Orientation.Horizontal;
                Controls.Add(oRadRating);
                Controls.Add(new LiteralControl("</div>"));
                Controls.Add(new LiteralControl("</div>"));
              }
            }
          }
          oRows = null;
        }
      }
      dPregRanking = null;
    }

    protected void getUserRanking(string pCodCliente, string pCodUsuario) { 

    }

  }
}