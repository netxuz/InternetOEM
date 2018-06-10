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

namespace ICommunity.Controls
{
  public partial class Carrusel : System.Web.UI.UserControl
  {
    Web oWeb = new Web();
    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        getBannerCarrusel(this.Attributes["CodBanner"].ToString());
      }
    }

    protected void getBannerCarrusel(string pCodBanner)
    {
      Image oImage;
      StringBuilder sPath;
      DataTable dImgBanner = oWeb.DeserializarTbl(Server.MapPath(".").ToString(), "ImgBanner.bin");
      if (dImgBanner != null)
      {
        DataRow[] oRow = dImgBanner.Select(" cod_banner = " + pCodBanner);
        if (oRow != null)
        {
          if (oRow.Count() > 0)
          {
            foreach (DataRow dRow in oRow)
            {
              sPath = new StringBuilder();
              sPath.Append("~/rps_onlineservice/banners/banner_").Append(pCodBanner).Append("/").Append(dRow["nom_img_banner"].ToString());
              if (!string.IsNullOrEmpty(dRow["url_img_banner"].ToString()))
              {
                HyperLink oHyperLink = new HyperLink();
                oHyperLink.Attributes["rel"] = "fancybox";
                oHyperLink.NavigateUrl = dRow["url_img_banner"].ToString();
                oImage = new Image();
                oImage.ImageUrl = sPath.ToString();
                //oImage.AlternateText = "Paramours Escorts";
                oHyperLink.Controls.Add(oImage);
                foo1.Controls.Add(oHyperLink);
              }
              else
              {
                oImage = new Image();
                //oImage.AlternateText = "Paramours Escorts";
                oImage.ImageUrl = sPath.ToString();
                foo1.Controls.Add(oImage);
              }
            }
          }
        }
        oRow = null;
      }
      dImgBanner = null;
    }

    protected override void OnPreRender(EventArgs e)
    {
      StringBuilder js = new StringBuilder();
      js.Append("$(function() {\n");
      js.Append("$(\"#").Append(foo1.ClientID).Append("\").carouFredSel();\n");
      js.Append("$(\"#").Append(foo1.ClientID).Append(" a\").fancybox({\n");
      js.Append("autoDimensions : false,\n");
      js.Append("autoScale : false,\n");
      js.Append("transitionIn : 'elastic',\n");
      js.Append("transitionOut : 'elastic',\n");
      js.Append("type : 'iframe',\n");
      js.Append("cyclic	: true,\n");
      js.Append("onStart	: function() {\n");
      js.Append("$(\"#").Append(foo1.ClientID).Append("\").trigger(\"pause\");\n");
      js.Append("},\n");
      js.Append("onClosed: function() {\n");
      js.Append("$(\"#").Append(foo1.ClientID).Append("\").trigger(\"play\");\n");
      js.Append("}\n");
      js.Append("});\n");
      js.Append("});\n");

      Page.ClientScript.RegisterStartupScript(this.GetType(), "JQueryCarruselBanner", js.ToString(), true);
    }
  }
}