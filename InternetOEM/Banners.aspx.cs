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
using OnlineServices.Conn;
using OnlineServices.AppData;
using OnlineServices.Method;

namespace ICommunity
{
  public partial class Banners : System.Web.UI.Page
  {
    Web oWeb = new Web();
    Culture oCulture = new Culture();
    protected void Page_Load(object sender, EventArgs e)
    {
      oWeb.ValidaSessionAdm();
      btnCrear.Text = oCulture.GetResource("Global", "btnCrear");
      btnCrear.ToolTip = oCulture.GetResource("Global", "btnCrear");

    }
    protected void rdBanners_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        GridColumn oGridColumn;

        oGridColumn = rdBanners.MasterTableView.Columns.FindByUniqueName("NomBanner");
        oGridColumn.HeaderText = oCulture.GetResource("Banner", "NomBanner");

        oGridColumn = rdBanners.MasterTableView.Columns.FindByUniqueName("EstBanner");
        oGridColumn.HeaderText = oCulture.GetResource("Banner", "Estado");

        AppBanner oBanner = new AppBanner(ref oConn);
        rdBanners.DataSource = oBanner.Get();

        oConn.Close();
      }

    }

    protected void rdBanners_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
      if (e.Item is GridDataItem)
      {
        GridDataItem oItem = e.Item as GridDataItem;

        if ((!string.IsNullOrEmpty(oItem["EstBanner"].Text)) && (oItem["EstBanner"].Text != "&nbsp;"))
        {
          if (oItem["EstBanner"].Text == "V")
            oItem["EstBanner"].Text = oCulture.GetResource("Global", "Vigente");
          else
            oItem["EstBanner"].Text = oCulture.GetResource("Global", "NoVigente");
        }
      }
    }

    protected void rdBanners_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
      switch (e.CommandName)
      {
        case "cmdEdit":
          string[] cParam = new string[2];
          cParam[0] = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["cod_banner"].ToString();
          Response.Redirect(String.Format("Banner.aspx?CodBanner={0}", cParam));
          break;
        case "cmdDelete":
          string pCodBanner = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["cod_banner"].ToString();
          DBConn oConn = new DBConn();
          if (oConn.Open())
          {
            oConn.BeginTransaction();
            AppImgBanner oImgBanner = new AppImgBanner(ref oConn);
            oImgBanner.CodBanner = pCodBanner;
            oImgBanner.Accion = "ELIMINAR";
            oImgBanner.Put();

            if (string.IsNullOrEmpty(oImgBanner.Error))
            {
              AppBanner oBanner = new AppBanner(ref oConn);
              oBanner.CodBanner = pCodBanner;
              oBanner.Accion = "ELIMINAR";
              oBanner.Put();

              if (string.IsNullOrEmpty(oBanner.Error))
              {
                oConn.Commit();
                StringBuilder cPath = new StringBuilder();
                cPath.Append(Server.MapPath(".")).Append(@"\binary\");
                oImgBanner.CodImgBanner = string.Empty;
                oImgBanner.CodBanner = string.Empty;
                oImgBanner.SerializaImgBanner(ref oConn, cPath.ToString());
                oBanner.SerializaBanner(ref oConn, cPath.ToString());

                cPath = new StringBuilder();
                cPath.Append(Server.MapPath("."));
                cPath.Append(@"\rps_onlineservice\");
                cPath.Append(@"\banners\");
                cPath.Append(@"\banner_");
                cPath.Append(pCodBanner);
                if (Directory.Exists(cPath.ToString()))
                  Directory.Delete(cPath.ToString(),true);
              }
              else
                oConn.Rollback();
            }
            else
              oConn.Rollback();

          }
          oConn.Close();
          rdBanners.Rebind();
          break;
      }
    }

    protected void btnCrear_Click(object sender, EventArgs e)
    {
      Response.Redirect("Banner.aspx");
    }
  }
}
