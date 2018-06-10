using System;
using System.IO;
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

using Telerik.Web.UI;
using OnlineServices.Conn;
using OnlineServices.CmsData;
using OnlineServices.Method;

namespace ICommunity
{
  public partial class Zonas : System.Web.UI.Page
  {
    Web oWeb = new Web();
    Culture oCulture = new Culture();
    protected void Page_Load(object sender, EventArgs e)
    {
      oWeb.ValidaSessionAdm();
      btnCrear.Text = oCulture.GetResource("Global", "btnCrear");
      btnCrear.ToolTip = oCulture.GetResource("Global", "btnCrear");
    }

    protected void rdZona_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        CmsZona oZona = new CmsZona(ref oConn);

        GridColumn oGridColumn;

        oGridColumn = rdZona.MasterTableView.Columns.FindByUniqueName("NomZona");
        oGridColumn.HeaderText = oCulture.GetResource("Zona", "NomZona");

        oGridColumn = rdZona.MasterTableView.Columns.FindByUniqueName("EstZona");
        oGridColumn.HeaderText = oCulture.GetResource("Zona", "EstZona");

        rdZona.DataSource = oZona.Get();

        oConn.Close();
      }

    }

    protected void rdZona_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
      if (e.Item is GridDataItem)
      {
        GridDataItem oItem = e.Item as GridDataItem;
        (oItem["Editar"].Controls[0] as Button).CssClass = "BtnColEditar";
        (oItem["Eliminar"].Controls[0] as Button).CssClass = "BtnColEliminar";

        if ((!string.IsNullOrEmpty(oItem["EstZona"].Text)) && (oItem["EstZona"].Text != "&nbsp;"))
        {
          if (oItem["EstZona"].Text == "V")
            oItem["EstZona"].Text = oCulture.GetResource("Zona", "Vigente");
          else
            oItem["EstZona"].Text = oCulture.GetResource("Zona", "NoVigente");
        }
      }
    }

    protected void rdZona_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
      switch (e.CommandName)
      {
        case "cmdEdit":
          string[] cParam = new string[2];
          cParam[0] = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["cod_zona"].ToString();
          Response.Redirect(String.Format("Zona.aspx?CodZona={0}", cParam));
          break;
        case "cmdDelete":
          string pCodZona = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["cod_zona"].ToString();
          DBConn oConn = new DBConn();
          if (oConn.Open())
          {
            string sPath = Server.MapPath(".") + @"\binary\Zona_" + pCodZona + ".bin";
            File.Delete(sPath);

            CmsZona oZona = new CmsZona(ref oConn);
            oZona.CodZona = pCodZona;
            oZona.Accion = "ELIMINAR";
            oZona.Put();

            oConn.Close();
          }
          rdZona.Rebind();
          break;
      }
    }

    protected void btnCrear_Click(object sender, EventArgs e)
    {
      Response.Redirect("Zona.aspx");
    }
  }
}
