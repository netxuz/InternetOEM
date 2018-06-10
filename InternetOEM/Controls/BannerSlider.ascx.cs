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
  public partial class BannerSlider : System.Web.UI.UserControl
  {
    Web oWeb = new Web();
    StringBuilder sIdControls = new StringBuilder();
    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        getBannerSlider(this.Attributes["CodBanner"].ToString());
      }
    }

    protected void getBannerSlider(string pCodBanner)
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
                oHyperLink.NavigateUrl = dRow["url_img_banner"].ToString();
                oImage = new Image();
                oImage.ImageUrl = sPath.ToString();
                //oImage.AlternateText = "Paramours Escorts";
                oHyperLink.Controls.Add(oImage);
                featured.Controls.Add(oHyperLink);

                if (sIdControls.Length == 0)
                  sIdControls.Append(oHyperLink.ClientID);
                else
                  sIdControls.Append(",").Append(oHyperLink.ClientID);
              }
              else {  
                oImage = new Image();
                //oImage.AlternateText = "Paramours Escorts";
                oImage.ImageUrl = sPath.ToString();
                featured.Controls.Add(oImage);
              }
            }
            StringBuilder sJs = new StringBuilder();
            sJs.Append("$(window).load(function() {").Append("$('#").Append(this.ClientID).Append("_featured').orbit();});");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "sJScript", sJs.ToString(), true);
          }
        }
        oRow = null;
      }
      dImgBanner = null;
    }

    protected override void OnPreRender(EventArgs e)
    {
      string arrId = sIdControls.ToString();
      if (!string.IsNullOrEmpty(arrId))
      {
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

        Page.ClientScript.RegisterStartupScript(this.GetType(), "JQueryFancyboxBanner", js.ToString(), true);
      }
    }
  }
}