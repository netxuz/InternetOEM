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
using OnlineServices.SystemData;
using OnlineServices.Method;
using OnlineServices.Conn;
namespace ICommunity
{
  public partial class Ranking : System.Web.UI.Page
  {
    private OnlineServices.Method.Web oWeb = new OnlineServices.Method.Web();
    private OnlineServices.Method.Culture oCulture = new OnlineServices.Method.Culture();
    private OnlineServices.Method.Usuario oIsUsuario;

    protected override void OnPreRender(EventArgs e)
    {
      //HtmlMeta oMeta;
      StringBuilder cUrl;
      //HtmlLink oHLink;

      base.OnPreRender(e);

      cUrl = new StringBuilder();
      cUrl.Append("<script src=\"Resources/jquery.min.js\"></script>");
      Page.Header.Controls.Add(new LiteralControl(cUrl.ToString()));

      cUrl = new StringBuilder();
      cUrl.Append("<script src=\"Resources/jquery.autosize.js\" language=\"JavaScript\"></script>");
      Page.Header.Controls.Add(new LiteralControl(cUrl.ToString()));

      cUrl = new StringBuilder();
      cUrl.Append("<script src=\"Resources/GlobalFunction.js\" type=\"text/javascript\"></script>");
      Page.Header.Controls.Add(new LiteralControl(cUrl.ToString()));

    }
    protected void Page_Load(object sender, EventArgs e)
    {
      oIsUsuario = oWeb.GetObjUsuario();
      CodUsuario.Value = oWeb.GetData("CodUsuario");
      if ((string.IsNullOrEmpty(oWeb.GetData("H"))) && (!string.IsNullOrEmpty(oIsUsuario.CodUsuario)))
        putUserRanking();
      else if (!string.IsNullOrEmpty(oWeb.GetData("H")))
      {
        H.Value = oWeb.GetData("H");
        getHistUserRanking();
      }
    }
    protected void putUserRanking()
    {
      Button oButton;
      Label sPregRanking;
      RadRating oRadRating;
      StringBuilder sSQL = new StringBuilder();
      sSQL.Append(" est_preg_ranking = 'V' ");
      DataTable dPregRanking = oWeb.DeserializarTbl(Server.MapPath("."), "PregRanking.bin");
      if (dPregRanking != null)
      {
        if (dPregRanking.Rows.Count > 0)
        {
          DataRow[] oRows = dPregRanking.Select(sSQL.ToString());
          if (oRows != null)
          {
            if (oRows.Count() > 0)
            {
              objRanking.Controls.Add(new LiteralControl("<div class=\"mdlRankig\">" + oCulture.GetResource("Ranking", "TituloRanking") + "</div>"));
              objRanking.Controls.Add(new LiteralControl("<div class=\"mdlDesRankig\">" + oCulture.GetResource("Ranking", "DesRanking") + "</div>"));
              foreach (DataRow oRow in oRows)
              {
                objRanking.Controls.Add(new LiteralControl("<div class=\"mdlpregrank\">"));
                objRanking.Controls.Add(new LiteralControl("<div class=\"labelpregrank\">"));
                sPregRanking = new Label();
                sPregRanking.Text = oRow["preg_ranking"].ToString();
                objRanking.Controls.Add(sPregRanking);
                objRanking.Controls.Add(new LiteralControl("</div>"));

                objRanking.Controls.Add(new LiteralControl("<div class=\"objrankpreg\">"));
                oRadRating = new RadRating();
                oRadRating.ID = "RdRating_" + oRow["cod_preg_ranking"].ToString();
                oRadRating.ItemCount = 7;
                oRadRating.SelectionMode = RatingSelectionMode.Continuous;
                oRadRating.Precision = RatingPrecision.Item;
                oRadRating.Orientation = Orientation.Horizontal;
                objRanking.Controls.Add(oRadRating);
                objRanking.Controls.Add(new LiteralControl("</div>"));
                objRanking.Controls.Add(new LiteralControl("</div>"));
              }
              objRanking.Controls.Add(new LiteralControl("<div class=\"mdlobsrank\">"));
              objRanking.Controls.Add(new LiteralControl("<div class=\"labelObsrank\">Comente :</div>"));
              objRanking.Controls.Add(new LiteralControl("<div class=\"txtComente\">"));
              TextBox oTexBox = new TextBox();
              oTexBox.ID = "oRxtComent";
              oTexBox.TextMode = TextBoxMode.MultiLine;
              oTexBox.CssClass = "CssMstComente";
              objRanking.Controls.Add(oTexBox);
              objRanking.Controls.Add(new LiteralControl("</div>"));
              objRanking.Controls.Add(new LiteralControl("</div>"));

              objRanking.Controls.Add(new LiteralControl("<div class=\"mdlbotonera\">"));
              
              oButton = new Button();
              oButton.ID = "putEval";
              oButton.Click += new EventHandler(oButton_Click);
              oButton.CssClass = "btnEvalAceptar";
              oButton.Text = oCulture.GetResource("Global", "btnAceptar");
              objRanking.Controls.Add(oButton);

              oButton = new Button();
              oButton.ID = "putVolverEval";
              oButton.Click += new EventHandler(oBtnVolver_Click);
              oButton.CssClass = "btnEvalAceptar";
              oButton.Text = oCulture.GetResource("Global", "btnVolver");
              objRanking.Controls.Add(oButton);

              objRanking.Controls.Add(new LiteralControl("</div>"));
            }
          }
          oRows = null;
        }
      }
      dPregRanking = null;
    }
    protected void getHistUserRanking()
    {
      BinaryUsuario dUsuario;
      SysUsuario oUsuario;
      RadRating oRadRating;
      string sNombreCliente = string.Empty;
      DataTable dPregRanking = oWeb.DeserializarTbl(Server.MapPath("."), "PregRanking.bin");

      objRanking.Controls.Add(new LiteralControl("<div class=\"MenuHistRanking\">"));
      Button oButton = new Button();
      oButton.ID = "BtnVolverGHUR";
      oButton.Click += new EventHandler(oBtnVolver_Click);
      oButton.CssClass = "btnEvalVolver";
      oButton.Text = oCulture.GetResource("Global", "btnVolver");
      objRanking.Controls.Add(oButton);
      objRanking.Controls.Add(new LiteralControl("<div class=\"imgUp\"><a href=\"#\" onmouseover=\"move('container',5)\" onmouseout=\"clearTimeout(move.to)\"><img src=\"style/images/ad_down.png\" border=\"0\"></a></div>"));
      objRanking.Controls.Add(new LiteralControl("</div>"));

      objRanking.Controls.Add(new LiteralControl("<div style=\"position: relative; width: 100%; height: 420px; overflow: hidden\">"));
      objRanking.Controls.Add(new LiteralControl("<div id=\"container\" style=\"position: absolute; left: 0; top: 0\">"));

      objRanking.Controls.Add(new LiteralControl("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">"));
      StringBuilder sSQL = new StringBuilder();
      sSQL.Append(" cod_usuario = ").Append(CodUsuario.Value);
      DataTable dRanking = oWeb.DeserializarTbl(Server.MapPath("."), "Ranking.bin");
      if (dRanking != null)
      {
        if (dRanking.Rows.Count > 0)
        {
          DataRow[] oRowUsr = dRanking.Select(sSQL.ToString(), "fch_ranking desc");

          foreach (DataRow oRow in oRowUsr)
          {
            objRanking.Controls.Add(new LiteralControl("<tr>"));
            objRanking.Controls.Add(new LiteralControl("<td class=\"clmnNomb\" width=\"20%\" vAlign=\"top\">"));

            objRanking.Controls.Add(new LiteralControl("<div class=\"lblNombCliente\">"));
            dUsuario = new BinaryUsuario();
            oUsuario = new SysUsuario();
            oUsuario.Path = Server.MapPath(".");
            oUsuario.CodUsuario = oRow["cod_cliente"].ToString();
            dUsuario = oUsuario.ClassGet();
            if (dUsuario != null)
              sNombreCliente = dUsuario.NomUsuario + " " + dUsuario.ApeUsuario;
            dUsuario = null;
            objRanking.Controls.Add(new LiteralControl("<span>" + sNombreCliente + "</span>"));
            objRanking.Controls.Add(new LiteralControl("</div>"));

            objRanking.Controls.Add(new LiteralControl("<div class=\"objRanking\">"));
            oRadRating = new RadRating();
            oRadRating.ID = "RdRating_" + oRow["cod_ranking"].ToString();
            oRadRating.ItemCount = 7;
            oRadRating.Enabled = false;
            oRadRating.Value = decimal.Parse(oRow["nota_ranking"].ToString());
            oRadRating.SelectionMode = RatingSelectionMode.Continuous;
            oRadRating.Precision = RatingPrecision.Item;
            oRadRating.Orientation = Orientation.Horizontal;
            objRanking.Controls.Add(oRadRating);
            objRanking.Controls.Add(new LiteralControl("</div>"));

            objRanking.Controls.Add(new LiteralControl("<div class=\"lblFchRanking\">"));
            objRanking.Controls.Add(new LiteralControl("<span>" + DateTime.Parse(oRow["fch_ranking"].ToString()).ToString("dd-MM-yyyy HH:mm") + "</span>"));
            objRanking.Controls.Add(new LiteralControl("</div>"));

            objRanking.Controls.Add(new LiteralControl("</td>"));
            objRanking.Controls.Add(new LiteralControl("<td class=\"clmnPreg\" width=\"40%\" vAlign=\"top\">"));

            DataTable dAptPregRanking = oWeb.DeserializarTbl(Server.MapPath("."), "AptPregRanking_" + oRow["cod_ranking"].ToString() + ".bin");
            if (dAptPregRanking != null)
              if (dAptPregRanking.Rows.Count > 0) {
                foreach (DataRow oRows in dAptPregRanking.Rows) {
                  DataRow[] dPreg = dPregRanking.Select(" cod_preg_ranking = " + oRows["cod_preg_ranking"].ToString());
                  if (dPreg != null)
                    if (dPreg.Count() > 0) {
                      objRanking.Controls.Add(new LiteralControl("<div class=\"mdlPregRanking\">"));
                      objRanking.Controls.Add(new LiteralControl("<div class=\"mdlTitlePregRanking\">"));
                      objRanking.Controls.Add(new LiteralControl("<span>" + dPreg[0]["preg_ranking"].ToString() + "</span>"));
                      objRanking.Controls.Add(new LiteralControl("</div>"));
                      objRanking.Controls.Add(new LiteralControl("<div class=\"mdlValPregRanking\">"));
                      objRanking.Controls.Add(new LiteralControl("<span>" + oRows["nota_preg_ranking"].ToString() + "</span>"));
                      objRanking.Controls.Add(new LiteralControl("</div>"));
                      objRanking.Controls.Add(new LiteralControl("</div>"));
                    }
                  dPreg = null;
                }
              }
            dAptPregRanking = null;

            objRanking.Controls.Add(new LiteralControl("</td>"));
            objRanking.Controls.Add(new LiteralControl("<td class=\"clmnObs\" vAlign=\"top\">"));
            objRanking.Controls.Add(new LiteralControl("<div class=\"oBsRanking\"><span>" + oRow["obs_ranking"].ToString() + "</span></div>"));
            objRanking.Controls.Add(new LiteralControl("</td>"));
            objRanking.Controls.Add(new LiteralControl("</tr>"));
          }
        }
      }
      objRanking.Controls.Add(new LiteralControl("</table>"));
      objRanking.Controls.Add(new LiteralControl("</div></div>"));
      objRanking.Controls.Add(new LiteralControl("<div class=\"imgDown\">"));
      objRanking.Controls.Add(new LiteralControl("<a href=\"#\" onmouseover=\"move('container',-5)\" onmouseout=\"clearTimeout(move.to)\"><img src=\"style/images/ad_up.png\" border=\"0\"></a>"));
      objRanking.Controls.Add(new LiteralControl("</div>"));
    }
    protected void oBtnVolver_Click(object sender, EventArgs e)
    {
      Response.Redirect("Escort.aspx?CodUsuario=" + CodUsuario.Value);
    }
    void oButton_Click(object sender, EventArgs e)
    {
      try
      {
        AppRanking oAppRanking;
        decimal iNotaVal = 0;
        int iCantPreg = 0;
        StringBuilder sError = new StringBuilder();
        DBConn oConn = new DBConn();
        if (oConn.Open())
        {
          oAppRanking = new AppRanking(ref oConn);
          oAppRanking.CodCliente = oIsUsuario.CodUsuario;
          oAppRanking.CodUsuario = CodUsuario.Value;
          oAppRanking.FchRanking = DateTime.Now.ToString();
          oAppRanking.ObsRanking = (this.FindControl("oRxtComent") as TextBox).Text;
          oAppRanking.Accion = "CREAR";
          oAppRanking.Put();
          if (string.IsNullOrEmpty(oAppRanking.Error))
          {
            string pCodRanking = oAppRanking.CodRanking;
            AptPregRanking oAptPregRanking = new AptPregRanking(ref oConn);
            oAptPregRanking.Accion = "CREAR";
            oAptPregRanking.CodRanking = pCodRanking;

            StringBuilder sSQL = new StringBuilder();
            sSQL.Append(" est_preg_ranking = 'V' ");
            DataTable dPregRanking = oWeb.DeserializarTbl(Server.MapPath("."), "PregRanking.bin");
            if (dPregRanking != null)
            {
              if (dPregRanking.Rows.Count > 0)
              {
                DataRow[] oRows = dPregRanking.Select(sSQL.ToString());
                if (oRows != null)
                {
                  if (oRows.Count() > 0)
                  {
                    iCantPreg = oRows.Count();
                    foreach (DataRow oRow in oRows)
                    {
                      iNotaVal = iNotaVal + decimal.Parse((this.FindControl("RdRating_" + oRow["cod_preg_ranking"].ToString()) as RadRating).Value.ToString());
                      oAptPregRanking.NotaPregRanking = (this.FindControl("RdRating_" + oRow["cod_preg_ranking"].ToString()) as RadRating).Value.ToString();
                      oAptPregRanking.CodPregRanking = oRow["cod_preg_ranking"].ToString();
                      oAptPregRanking.Accion = "CREAR";
                      oAptPregRanking.Put();
                      if (!string.IsNullOrEmpty(oAptPregRanking.Error))
                        sError.Append(oAptPregRanking.Error);
                    }
                  }
                }
              }
            }

            if (sError.Length == 0)
            {
              iNotaVal = iNotaVal / iCantPreg;
              oAptPregRanking.CodPregRanking = string.Empty;
              oAptPregRanking.SerializaTblPregRanking(ref oConn, Server.MapPath(".") + @"\binary\");
              oAppRanking.CodRanking = pCodRanking;
              oAppRanking.NotaRanking = iNotaVal.ToString();
              oAppRanking.Accion = "EDITAR";
              oAppRanking.Put();
              if (string.IsNullOrEmpty(oAppRanking.Error))
              {
                oAppRanking.SerializaTblRanking(ref oConn, Server.MapPath(".") + @"\binary\");
                lblMsgNotaOk.Text = oCulture.GetResource("Ranking", "lblMsgNotaOk") + " " + decimal.Round(iNotaVal,1).ToString();

                iNotaVal = 0;
                oAppRanking.CodCliente = string.Empty;
                oAppRanking.CodRanking = string.Empty;
                DataTable dUsrRanking = oAppRanking.Get();
                if (dUsrRanking != null)
                  if (dUsrRanking.Rows.Count > 3)
                  {
                    foreach (DataRow oRow in dUsrRanking.Rows)
                    {
                      iNotaVal = iNotaVal + decimal.Parse(oRow["nota_ranking"].ToString());
                    }
                    iNotaVal = iNotaVal / dUsrRanking.Rows.Count;
                    SysUsuario oUsuario = new SysUsuario(ref oConn);
                    oUsuario.CodUsuario = CodUsuario.Value;
                    oUsuario.NotaRanking = iNotaVal.ToString();
                    oUsuario.Accion = "EDITAR";
                    oUsuario.Put();
                    if (string.IsNullOrEmpty(oUsuario.Error))
                    {
                      oUsuario.SerializaTblUsuario(ref oConn, Server.MapPath(".") + @"\binary\", "Usuarios.bin");
                      oUsuario.SerializaUsuario(ref oConn, Server.MapPath(".") + @"\binary\", "Usuario_" + CodUsuario.Value + ".bin");
                    }
                  }
                dUsrRanking = null;
              }
            }
            objRanking.Visible = false;
            objMessageInfo.Visible = true;
            lblTtRnk.Text = oCulture.GetResource("Ranking", "TituloRanking");
            lblMsgOk.Text = oCulture.GetResource("Ranking", "lblMsgOk");
            btnVolver.Text = oCulture.GetResource("Global", "btnVolver");
            oConn.Close();
          }
        }
      }
      catch (Exception ex)
      {

      }
    }
  }
}