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
using System.IO;
using System.Text;
using Telerik.Web.UI;

using OnlineServices.Method;

namespace ICommunity.Controls
{
  public partial class GaleryControlPhone : System.Web.UI.UserControl
  {
    private string sType = string.Empty;
    private string sTypeUsr = string.Empty;
    private bool bIndUsrDest;
    private bool blnOfertaExit = false;

    Web oWeb = new Web();

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

      StringBuilder sSQL = new StringBuilder();
      sSQL.Append(" est_usuario = 'V' ");
      if (!string.IsNullOrEmpty(sTypeUsr))
        sSQL.Append(" and cod_tipo = ").Append(sTypeUsr);
      if (bIndUsrDest)
        sSQL.Append(" and destacado_usuario = 'V' ");
      DataTable dUsuario = oWeb.DeserializarTbl(Server.MapPath("."), "Usuarios.bin");
      DataTable oTbl = dUsuario.Select(sSQL.ToString()).CopyToDataTable();

      DataTable nData = CollectionExtensions.OrderRandomly(oTbl.AsEnumerable()).CopyToDataTable();
      rdGaleryCntrl.DataSource = nData;

      //DataRow[] oRows = dUsuario.Select(sSQL.ToString());
      //if (oRows != null)
      //  if (oRows.Length > 0)
      //  {
      //    DataTable nData = oRows.CopyToDataTable();
      //    rdGaleryCntrl.DataSource = nData;
      //    nData = null;
      //  }
      //oRows = null;

    }

    protected void rdGaleryCntrl_ItemDataBound(object sender, RadListViewItemEventArgs e)
    {
      blnOfertaExit = false;
      string sNombre = string.Empty;
      if (e.Item is RadListViewDataItem)
      {
        if (sType == "USR")
        {
          LinkButton oLinkButton = e.Item.FindControl("idLnkButton") as LinkButton;
          oLinkButton.Attributes["CodUsuario"] = ((e.Item as RadListViewDataItem).DataItem as DataRowView)["cod_usuario"].ToString();
          oLinkButton.Click +=new EventHandler(oLinkButton_Click);

          sNombre = ((e.Item as RadListViewDataItem).DataItem as DataRowView)["nom_usuario"].ToString() + " " + ((e.Item as RadListViewDataItem).DataItem as DataRowView)["ape_usuario"].ToString();

          RadBinaryImage oImge = e.Item.FindControl("idImgUser") as RadBinaryImage;
          oImge.AlternateText = sNombre.Trim() + " Escort de Chile - Santiago.";
          oImge.ImageUrl = oWeb.getImageProfileUser(((e.Item as RadListViewDataItem).DataItem as DataRowView)["cod_usuario"].ToString(),string.Empty);
          //oImge.Height = 150;
          //oImge.DataValue = oWeb.getImageProfileUser(((e.Item as RadListViewDataItem).DataItem as DataRowView)["cod_usuario"].ToString(), 150, 150);

          Label oLabel = e.Item.FindControl("idLblNomUser") as Label;
          oLabel.Text = ((e.Item as RadListViewDataItem).DataItem as DataRowView)["nom_usuario"].ToString() + " " + ((e.Item as RadListViewDataItem).DataItem as DataRowView)["ape_usuario"].ToString();

        }
      }
    }

    protected void oLinkButton_Click(object sender, EventArgs e)
    {
      DataRow[] oRow;
      DataTable oNodos = oWeb.DeserializarTbl(Server.MapPath("."), "Nodos.bin");
      if (oNodos != null)
        if (oNodos.Rows.Count > 0)
        {
          oRow = oNodos.Select(" pf_nodo_phone = 'V' ");
          if (oRow != null)
          {
            if (oRow.Count() > 0)
            {
              Session["CodNodo"] = oRow[0]["cod_nodo"].ToString();
              Session["CodUsuarioPerfil"] = (sender as LinkButton).Attributes["CodUsuario"].ToString();
            }
          }
          oRow = null;
        }
      oNodos = null;
      Page.Response.Redirect(".");
    }
  }
}