using System;
using System.Collections;
using System.Collections.Generic;
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
using System.IO;
using System.Text;
using Telerik.Web.UI;

using OnlineServices.Method;

namespace ICommunity.Controls
{
  public partial class GaleryControl : System.Web.UI.UserControl
  {
    private string sType = string.Empty;
    private string sTypeUsr = string.Empty;
    private bool bIndUsrDest;
    private bool blnOfertaExit = false;

    private string sRuta = string.Empty;
    public string Ruta { get { return sRuta; } set { sRuta = value; } }

    private string sCodOpciones = string.Empty;
    public string CodOpciones { get { return sCodOpciones; } set { sCodOpciones = value; } }

    Web oWeb = new Web();
    NumberFormatInfo oNumInfo = new CultureInfo("es-CL", false).NumberFormat;
    StringBuilder sIdControls = new StringBuilder();
    //Label oLabel;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        if (!string.IsNullOrEmpty(this.Attributes["IndUsrDest"]))
        {
          ViewState["IndUsrDest"] = this.Attributes["IndUsrDest"];
          bIndUsrDest = (ViewState["IndUsrDest"].ToString() == "V" ? true : false);
        }
        if (!string.IsNullOrEmpty(this.Attributes["DataType"]))
        {
          ViewState["DataType"] = this.Attributes["DataType"];
          sType = ViewState["DataType"].ToString();
        }

        if (!string.IsNullOrEmpty(this.Attributes["TypeUsr"]))
        {
          string[] arrDataKeyNames = { "cod_usuario" };
          rdGaleryCntrl.ClientDataKeyNames = arrDataKeyNames;
          ViewState["TypeUsr"] = this.Attributes["TypeUsr"];
          sTypeUsr = ViewState["TypeUsr"].ToString();
        }
        if ((string.IsNullOrEmpty(this.Attributes["IndUsrDest"])) && (string.IsNullOrEmpty(this.Attributes["DataType"])) && (string.IsNullOrEmpty(this.Attributes["TypeUsr"]))) {
          bIndUsrDest = true;
          ViewState["IndUsrDest"] = bIndUsrDest;

          sType = "USR";
          ViewState["DataType"] = sType;

          sTypeUsr = "2";
          ViewState["TypeUsr"] = sTypeUsr;
          string[] arrDataKeyNames = { "cod_usuario" };
          rdGaleryCntrl.ClientDataKeyNames = arrDataKeyNames;
        }
      }
      else
      {
        if (ViewState["DataType"] != null)
          sType = ViewState["DataType"].ToString();

        if (ViewState["TypeUsr"] != null)
          sTypeUsr = ViewState["TypeUsr"].ToString();

        if (ViewState["IndUsrDest"] != null)
          bIndUsrDest = (ViewState["IndUsrDest"].ToString() == "V" ? true : false);
      }
    }

    protected void rdGaleryCntrl_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
      DataTable nData = null;
      if (sType == "USR")
      {
        StringBuilder sSQL = new StringBuilder();
        sSQL.Append(" est_usuario = 'V' ");
        if (!string.IsNullOrEmpty(sTypeUsr))
          sSQL.Append(" and cod_tipo = ").Append(sTypeUsr);
        if (bIndUsrDest)
          sSQL.Append(" and destacado_usuario = 'V' ");
        DataTable dUsuario = oWeb.DeserializarTbl((!string.IsNullOrEmpty(sRuta) ? Server.MapPath(".").ToUpper().Replace(sRuta.ToUpper(), "") : Server.MapPath(".")), "Usuarios.bin");

        if (dUsuario.Select(sSQL.ToString()).Count() > 0)
        {
          DataTable oTbl = dUsuario.Select(sSQL.ToString()).CopyToDataTable();
          nData = CollectionExtensions.OrderRandomly(oTbl.AsEnumerable()).CopyToDataTable();
        }
        rdGaleryCntrl.DataSource = nData;

        //if (oRows != null)
        //  if (oRows.Length > 0)
        //  {
        //    DataTable nData = oRows.CopyToDataTable();
        //    rdGaleryCntrl.DataSource = nData;
        //    nData = null;
        //  }
        //oRows = null;
      }
    }

    protected void rdGaleryCntrl_ItemDataBound(object sender, RadListViewItemEventArgs e)
    {
      blnOfertaExit = false;
      string sNombre = string.Empty;
      if (e.Item is RadListViewDataItem)
      {
        if (sType == "USR")
        {
          //LinkButton oLinkButton = e.Item.FindControl("idLnkButton") as LinkButton;
          //oLinkButton.OnClientClick = string.Format("ViewProfile({0}); return false;", ((e.Item as RadListViewDataItem).DataItem as DataRowView)["cod_usuario"].ToString());

          HyperLink oHyperLink = e.Item.FindControl("idLnkButton") as HyperLink;
          oHyperLink.NavigateUrl = string.Format("../Escort.aspx?CodUsuario={0}", ((e.Item as RadListViewDataItem).DataItem as DataRowView)["cod_usuario"].ToString());
          if (sIdControls.Length == 0)
            sIdControls.Append(oHyperLink.ClientID);
          else
            sIdControls.Append(",").Append(oHyperLink.ClientID);
          //oHyperLink.NavigateUrl = "http://localhost/ISBAN/fancybox/thickbox/boletines.html";

          sNombre = ((e.Item as RadListViewDataItem).DataItem as DataRowView)["nom_usuario"].ToString() + " " + ((e.Item as RadListViewDataItem).DataItem as DataRowView)["ape_usuario"].ToString();

          RadBinaryImage oImge = e.Item.FindControl("idImgUser") as RadBinaryImage;
          oImge.AlternateText = "Escort " + sNombre.Trim() + ", Escorts en Chile - Santiago";
          //Esta debe quedar
          oImge.ImageUrl = oWeb.getImageProfileUser(((e.Item as RadListViewDataItem).DataItem as DataRowView)["cod_usuario"].ToString(), sRuta);
          //oImge.Height = 150;
          //oImge.DataValue = oWeb.getImageProfileUser(((e.Item as RadListViewDataItem).DataItem as DataRowView)["cod_usuario"].ToString(), 150, 150);

          Label oLabel = e.Item.FindControl("idLblNomUser") as Label;
          oLabel.Text = sNombre;

          HtmlGenericControl oMdlDataUser = e.Item.FindControl("mdlDataUser") as HtmlGenericControl;
          DataTable dCampoUsuario = oWeb.DeserializarTbl((!string.IsNullOrEmpty(sRuta) ? Server.MapPath(".").ToUpper().Replace(sRuta.ToUpper(), "") : Server.MapPath(".")), "CampoUsuarios.bin");
          if (dCampoUsuario != null)
          {
            if (dCampoUsuario.Rows.Count > 0)
            {
              DataRow[] oRows = dCampoUsuario.Select(" ind_despliegue_portal = 'V' ", " ord_campo ");
              if (oRows != null)
              {
                if (oRows.Count() > 0)
                {
                  foreach (DataRow oRow in oRows)
                  {
                    DataTable dInfUsuario = oWeb.DeserializarTbl((!string.IsNullOrEmpty(sRuta) ? Server.MapPath(".").ToUpper().Replace(sRuta.ToUpper(), "") : Server.MapPath(".")), "InfoUsuario_" + ((e.Item as RadListViewDataItem).DataItem as DataRowView)["cod_usuario"].ToString() + ".bin");
                    if (dInfUsuario != null)
                    {
                      if (dInfUsuario.Rows.Count > 0)
                      {
                        DataRow[] oRowsInf = dInfUsuario.Select("cod_campo = " + oRow["cod_campo"].ToString());
                        if (oRowsInf != null)
                        {
                          if (oRowsInf.Count() > 0)
                          {
                            //oLabel = new Label();
                            //oLabel.Text = oRow["nom_campo"].ToString();
                            //oMdlDataUser.Controls.Add(oLabel);
                            if (!string.IsNullOrEmpty(oRowsInf[0]["val_campo"].ToString()))
                            {
                              if ((oRowsInf[0]["cod_campo"].ToString() == "20140815025804") && (!blnOfertaExit))
                                oMdlDataUser.Controls.Add(new LiteralControl("<p class=\"dat20140815025745\" class=\"tTipMsgMoney\">"));
                              else
                              {
                                if (oRowsInf[0]["cod_campo"].ToString() == "20140815025745")
                                  blnOfertaExit = true;
                                oMdlDataUser.Controls.Add(new LiteralControl("<p class=\"dat" + oRowsInf[0]["cod_campo"].ToString() + "\">"));
                              }
                              oLabel = new Label();
                              switch (oRow["tipo_campo"].ToString().Trim())
                              {
                                case "5":
                                  oNumInfo.CurrencyDecimalDigits = 0;
                                  int iValCampo = int.Parse(oRowsInf[0]["val_campo"].ToString());
                                  if (oRowsInf[0]["cod_campo"].ToString() == "20140815025804")
                                    oLabel.Text = (blnOfertaExit ? "Normal " : "") + iValCampo.ToString("C", oNumInfo);
                                  else
                                    oLabel.Text = iValCampo.ToString("C", oNumInfo);
                                  break;
                                default:
                                  oLabel.Text = oRowsInf[0]["val_campo"].ToString();
                                  break;
                              }
                              oMdlDataUser.Controls.Add(oLabel);
                              oMdlDataUser.Controls.Add(new LiteralControl("</p>"));
                            }
                            else if ((oRowsInf[0]["cod_campo"].ToString() == "20140815025804") && (!blnOfertaExit))
                            {
                              oMdlDataUser.Controls.Add(new LiteralControl("<p class=\"dat20140815025745\">"));
                              oLabel = new Label();
                              oLabel.Text = "$ Consultar";
                              oMdlDataUser.Controls.Add(oLabel);
                              oMdlDataUser.Controls.Add(new LiteralControl("</p>"));
                            }
                          }
                        }
                        oRowsInf = null;
                      }
                      dInfUsuario = null;
                    }
                  }
                }
              }
              oRows = null;
            }
          }
          dCampoUsuario = null;
        }
      }
    }

    protected void rdGaleryCntrl_PreRender(object sender, EventArgs e)
    {
      string arrId = sIdControls.ToString();
      StringBuilder js = new StringBuilder();
      js.Append("$(document).ready(function() {");
      foreach (string sIdCntr in arrId.Split(','))
      {
        js.Append("$(\"#").Append(sIdCntr).Append("\").fancybox({");
        //js.Append("'width' : '60%',");
        //js.Append("'height' : '100%',");
        js.Append("'autoDimensions' : false,");
        js.Append("'autoScale' : false,");
        js.Append("'transitionIn' : 'elastic',");
        js.Append("'transitionOut' : 'elastic',");
        js.Append("'type' : 'iframe'");
        js.Append("});");
      }
      js.Append("});");

      Page.ClientScript.RegisterStartupScript(this.GetType(), "JQueryFancybox", js.ToString(), true);
    }
  }

  public static class CollectionExtensions
  {
    private static Random random = new Random();
    public static IEnumerable<T> OrderRandomly<T>(this IEnumerable<T> collection)
    {

      // Order items randomly
      List<T> randomly = new List<T>(collection);
      while (randomly.Count > 0)
      {
        Int32 index = random.Next(randomly.Count);
        yield return randomly[index];
        randomly.RemoveAt(index);
      }
    } // OrderRandomly
  }
}