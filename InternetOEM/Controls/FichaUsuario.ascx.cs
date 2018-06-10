using System;
using System.Collections;
using System.Configuration;
using System.Globalization;
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
namespace ICommunity.Controls
{
  public partial class FichaUsuario : System.Web.UI.UserControl
  {
    private string pUsrPortal;
    public string UsrPortal { get { return pUsrPortal; } set { pUsrPortal = value; } }
    private bool blnOfertaExit = false;

    Web oWeb = new Web();
    NumberFormatInfo oNumInfo = new CultureInfo("es-CL", false).NumberFormat;
    OnlineServices.Method.Culture oCulture = new OnlineServices.Method.Culture();
    string pCodUsuario = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
      if ((Session["CodUsuarioPerfil"] != null) && (!string.IsNullOrEmpty(Session["CodUsuarioPerfil"].ToString())))
        getInfoUser(Session["CodUsuarioPerfil"].ToString());
      else
        if (!string.IsNullOrEmpty(pUsrPortal))
          getInfoUser(pUsrPortal);
    }

    protected void getInfoUser(string pCodUsuario)
    {
      Controls.Add(new LiteralControl("<div id=\"tblFichaUsuario\">"));
      Label oLabel;

      SysUsuario oUsuario = new SysUsuario();
      oUsuario.Path = Server.MapPath(".");
      oUsuario.CodUsuario = pCodUsuario;
      BinaryUsuario dUsuario = oUsuario.ClassGet();
      
      string cPath = Server.MapPath(".");
      DataTable dOpcionesCampo = oWeb.DeserializarTbl(cPath, "OpcionesCampo.bin");

      string pCodCampo = string.Empty;
      DataTable dInfoUsuario = oWeb.DeserializarTbl(cPath, "InfoUsuario_" + pCodUsuario + ".bin");
      if (dInfoUsuario != null)
        if (dInfoUsuario.Rows.Count > 0)
        {
          foreach (DataRow oRow in dInfoUsuario.Rows)
          {
            switch (oRow["tipo_campo"].ToString().Trim())
            {
              case "0":
                if (dOpcionesCampo != null)
                  if (dOpcionesCampo.Rows.Count > 0)
                  {
                    if (!string.IsNullOrEmpty(oRow["val_campo"].ToString()))
                    {
                      DataRow[] oRows = dOpcionesCampo.Select(" cod_opcion =" + oRow["val_campo"].ToString());
                      if (oRows != null)
                        if (oRows.Count() > 0)
                        {
                          if (!string.IsNullOrEmpty(oRows[0]["nom_opcion"].ToString()))
                          {
                            if (pCodCampo != oRow["cod_campo"].ToString())
                            {
                              if (!string.IsNullOrEmpty(pCodCampo))
                              {
                                //Controls.Add(new LiteralControl("</div>"));
                                Controls.Add(new LiteralControl("</div>"));
                              }
                              Controls.Add(new LiteralControl("<div id=\"idCmpFch_" + oRow["cod_campo"].ToString() + "\" class=\"dCampoFicha\">"));
                              Controls.Add(new LiteralControl("<div id=\"idTitStyle_" + oRow["cod_campo"].ToString() + "\" class=\"titstyle\">"));
                              pCodCampo = oRow["cod_campo"].ToString();
                              oLabel = new Label();
                              oLabel.ID = "lbl_" + oRow["cod_campo"].ToString();
                              oLabel.Text = oRow["nom_campo"].ToString();
                              Controls.Add(oLabel);
                              Controls.Add(new LiteralControl("</div>"));
                              Controls.Add(new LiteralControl("<div id=\"idCampo_" + oRow["cod_campo"].ToString() + "\" class=\"datstyle\">"));
                            }

                            oLabel = new Label();
                            oLabel.ID = "dtt_" + oRow["cod_campo"].ToString() + "_" + oRows[0]["cod_opcion"].ToString();
                            oLabel.Text = oRows[0]["nom_opcion"].ToString();
                            Controls.Add(oLabel);
                            Controls.Add(new LiteralControl("</div>"));
                          }
                        }
                      oRows = null;
                    }
                  }
                break;
              case "1":
                if (dOpcionesCampo != null)
                  if (dOpcionesCampo.Rows.Count > 0)
                  {
                    if (!string.IsNullOrEmpty(oRow["val_campo"].ToString()))
                    {
                      DataRow[] oRows = dOpcionesCampo.Select(" cod_opcion =" + oRow["val_campo"].ToString());
                      if (oRows != null)
                        if (oRows.Count() > 0)
                        {
                          if (!string.IsNullOrEmpty(oRows[0]["nom_opcion"].ToString()))
                          {
                            if (pCodCampo != oRow["cod_campo"].ToString())
                            {
                              if (!string.IsNullOrEmpty(pCodCampo))
                              {
                                //Controls.Add(new LiteralControl("</div>"));
                                Controls.Add(new LiteralControl("</div>"));
                              }
                              Controls.Add(new LiteralControl("<div id=\"idCmpFch_" + oRow["cod_campo"].ToString() + "\" class=\"dCampoFicha\">"));
                              Controls.Add(new LiteralControl("<div id=\"idTitStyle_" + oRow["cod_campo"].ToString() + "\" class=\"titstyle\">"));
                              pCodCampo = oRow["cod_campo"].ToString();
                              oLabel = new Label();
                              oLabel.ID = "lbl_" + oRow["cod_campo"].ToString();
                              oLabel.Text = oRow["nom_campo"].ToString();
                              Controls.Add(oLabel);
                              Controls.Add(new LiteralControl("</div>"));
                              Controls.Add(new LiteralControl("<div id=\"idCampo_" + oRow["cod_campo"].ToString() + "\" class=\"datstyle\">"));
                            }

                            Controls.Add(new LiteralControl("<div class=\"datiterstyle\">"));
                            oLabel = new Label();
                            oLabel.ID = "dtt_" + oRow["cod_campo"].ToString() + "_" + oRows[0]["cod_opcion"].ToString();
                            oLabel.Text = oRows[0]["nom_opcion"].ToString();
                            Controls.Add(oLabel);
                            Controls.Add(new LiteralControl("</div>"));
                            //Controls.Add(new LiteralControl("</div>"));
                          }
                        }
                      oRows = null;
                    }
                  }
                break;
              case "2":
              case "4":
                if ((!string.IsNullOrEmpty(oRow["val_campo"].ToString()))||(!string.IsNullOrEmpty(oRow["text_campo"].ToString())))
                {
                  if (pCodCampo != oRow["cod_campo"].ToString())
                  {
                    if (!string.IsNullOrEmpty(pCodCampo))
                    {
                      //Controls.Add(new LiteralControl("</div>"));
                      Controls.Add(new LiteralControl("</div>"));
                    }
                    Controls.Add(new LiteralControl("<div id=\"idCmpFch_" + oRow["cod_campo"].ToString() + "\" class=\"dCampoFicha\">"));
                    Controls.Add(new LiteralControl("<div id=\"idTitStyle_" + oRow["cod_campo"].ToString() + "\" class=\"titstyle\">"));
                    pCodCampo = oRow["cod_campo"].ToString();
                    oLabel = new Label();
                    oLabel.ID = "lbl_" + oRow["cod_campo"].ToString();
                    oLabel.Text = oRow["nom_campo"].ToString();
                    Controls.Add(oLabel);
                    Controls.Add(new LiteralControl("</div>"));
                    Controls.Add(new LiteralControl("<div id=\"idCampo_" + oRow["cod_campo"].ToString() + "\" class=\"datstyle\">"));
                  }

                  oLabel = new Label();
                  oLabel.ID = "dtt_" + oRow["cod_campo"].ToString();
                  if (oRow["desp_campo"].ToString() == "O")
                    oLabel.Text = oRow["text_campo"].ToString();
                  else
                    oLabel.Text = oRow["val_campo"].ToString();
                  Controls.Add(oLabel);
                  Controls.Add(new LiteralControl("</div>"));
                }
                break;
              case "5":
                oNumInfo.CurrencyDecimalDigits = 0;
                if (!string.IsNullOrEmpty(oRow["val_campo"].ToString()))
                {
                  if (pCodCampo != oRow["cod_campo"].ToString())
                  {
                    if (!string.IsNullOrEmpty(pCodCampo))
                    {
                      //Controls.Add(new LiteralControl("</div>"));
                      Controls.Add(new LiteralControl("</div>"));
                    }
                    Controls.Add(new LiteralControl("<div id=\"idCmpFch_" + oRow["cod_campo"].ToString() + "\" class=\"dCampoFicha\">"));
                    Controls.Add(new LiteralControl("<div id=\"idTitStyle_" + oRow["cod_campo"].ToString() + "\" class=\"titstyle\">"));
                    pCodCampo = oRow["cod_campo"].ToString();
                    oLabel = new Label();
                    oLabel.ID = "lbl_" + oRow["cod_campo"].ToString();
                    oLabel.Text = oRow["nom_campo"].ToString();
                    Controls.Add(oLabel);
                    Controls.Add(new LiteralControl("</div>"));
                    Controls.Add(new LiteralControl("<div id=\"idCampo_" + oRow["cod_campo"].ToString() + "\" class=\"datstyle\">"));
                  }

                  int iValCampo = int.Parse(oRow["val_campo"].ToString());
                  oLabel = new Label();
                  if ((oRow["cod_campo"].ToString() == "20140815025804") && (!blnOfertaExit))
                    oLabel.ID = "dtt_20140815025745";
                  else
                  {
                    if (oRow["cod_campo"].ToString() == "20140815025745")
                      blnOfertaExit = true;
                    oLabel.ID = "dtt_" + oRow["cod_campo"].ToString();
                  }
                  oLabel.Text = iValCampo.ToString("C", oNumInfo);
                  Controls.Add(oLabel);
                  Controls.Add(new LiteralControl("</div>"));
                }
                else if ((oRow["cod_campo"].ToString() == "20140815025804") && (!blnOfertaExit) && (dUsuario.CodTipo != "1"))
                {
                  if (pCodCampo != oRow["cod_campo"].ToString())
                  {
                    if (!string.IsNullOrEmpty(pCodCampo))
                    {
                      Controls.Add(new LiteralControl("</div>"));
                    }
                    Controls.Add(new LiteralControl("<div id=\"idCmpFch_" + oRow["cod_campo"].ToString() + "\" class=\"dCampoFicha\">"));
                    Controls.Add(new LiteralControl("<div id=\"idTitStyle_" + oRow["cod_campo"].ToString() + "\" class=\"titstyle\">"));
                    pCodCampo = oRow["cod_campo"].ToString();
                    oLabel = new Label();
                    oLabel.ID = "lbl_" + oRow["cod_campo"].ToString();
                    oLabel.Text = oRow["nom_campo"].ToString();
                    Controls.Add(oLabel);
                    Controls.Add(new LiteralControl("</div>"));
                    Controls.Add(new LiteralControl("<div id=\"idCampo_" + oRow["cod_campo"].ToString() + "\" class=\"datstyle\">"));
                  }
                  oLabel = new Label();
                  //oLabel.ID = "dtt_20110804164958";
                  oLabel.ID = "dtt_20140815025745";
                  oLabel.Text = "Consultar";
                  Controls.Add(oLabel);
                  Controls.Add(new LiteralControl("</div>"));
                }
                break;
              case "6":
                if (!string.IsNullOrEmpty(oRow["val_campo"].ToString()))
                {
                  if (pCodCampo != oRow["cod_campo"].ToString())
                  {
                    if (!string.IsNullOrEmpty(pCodCampo))
                    {
                      //Controls.Add(new LiteralControl("</div>"));
                      Controls.Add(new LiteralControl("</div>"));
                    }
                    Controls.Add(new LiteralControl("<div id=\"idCmpFch_" + oRow["cod_campo"].ToString() + "\" class=\"dCampoFicha\">"));
                    Controls.Add(new LiteralControl("<div id=\"idTitStyle_" + oRow["cod_campo"].ToString() + "\" class=\"titstyle\">"));
                    pCodCampo = oRow["cod_campo"].ToString();
                    oLabel = new Label();
                    oLabel.ID = "lbl_" + oRow["cod_campo"].ToString();
                    oLabel.Text = oRow["nom_campo"].ToString();
                    Controls.Add(oLabel);
                    Controls.Add(new LiteralControl("</div>"));
                    Controls.Add(new LiteralControl("<div id=\"idCampo_" + oRow["cod_campo"].ToString() + "\" class=\"datstyle\">"));
                  }
                  HyperLink oHyperLink = new HyperLink();
                  oHyperLink.ID = "dtt_" + oRow["cod_campo"].ToString();
                  oHyperLink.NavigateUrl = oRow["val_campo"].ToString();
                  oHyperLink.Text = oRow["val_campo"].ToString();
                  oHyperLink.Target = "_blank";
                  Controls.Add(oHyperLink);
                  Controls.Add(new LiteralControl("</div>"));
                  /*oLabel = new Label();
                  oLabel.ID = "dtt_" + oRow["cod_campo"].ToString();
                  oLabel.Text = oRow["val_campo"].ToString();
                  Controls.Add(oLabel);]/
                  */
                }
                break;
            }
          }

        }
      dOpcionesCampo = null;
      dInfoUsuario = null;
      Controls.Add(new LiteralControl("</div>"));
      Controls.Add(new LiteralControl("</div>"));
    }
  }
}