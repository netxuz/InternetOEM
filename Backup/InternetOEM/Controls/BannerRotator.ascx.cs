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
  public partial class BannerRotator : System.Web.UI.UserControl
  {
    Web oWeb = new Web();
    StringBuilder sIdControls = new StringBuilder();
    string sCodBanner = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        getBannerRotator(this.Attributes["CodBanner"].ToString());
      }
    }

    protected void getBannerRotator(string pCodBanner)
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
            sCodBanner = pCodBanner;
            rotator.Controls.Add(new LiteralControl("<div id=\"slider" + sCodBanner + "\" class=\"slider\">"));
            rotator.Controls.Add(new LiteralControl("<ul id=\"slider" + sCodBanner + "Content\" class=\"sliderContent\">"));
            foreach (DataRow dRow in oRow)
            {
              rotator.Controls.Add(new LiteralControl("<li id=\"slider" + sCodBanner + "Image\" class=\"sliderImage\">"));
              sPath = new StringBuilder();
              sPath.Append("~/rps_onlineservice/banners/banner_").Append(pCodBanner).Append("/").Append(dRow["nom_img_banner"].ToString());

              if (!string.IsNullOrEmpty(dRow["url_img_banner"].ToString()))
              {
                HyperLink oHyperLink = new HyperLink();
                oHyperLink.NavigateUrl = dRow["url_img_banner"].ToString();
                oImage = new Image();
                //oImage.AlternateText = "Paramours Escorts";
                oImage.ImageUrl = sPath.ToString();
                oHyperLink.Controls.Add(oImage);
                rotator.Controls.Add(oHyperLink);

                if (sIdControls.Length == 0)
                  sIdControls.Append(oHyperLink.ClientID);
                else
                  sIdControls.Append(",").Append(oHyperLink.ClientID);
              }
              else
              {
                oImage = new Image();
                //oImage.AlternateText = "Paramours Escorts";
                oImage.ImageUrl = sPath.ToString();
                rotator.Controls.Add(oImage);
              }

              if (!string.IsNullOrEmpty(dRow["text_img_banner"].ToString())){
                rotator.Controls.Add(new LiteralControl("<span class=\"bottom\">" + dRow["text_img_banner"].ToString() + "</span>"));
              }

              rotator.Controls.Add(new LiteralControl("</li>"));
            }
            rotator.Controls.Add(new LiteralControl("<div class=\"clear sliderImage\"></div>"));
            rotator.Controls.Add(new LiteralControl("</ul>"));
            rotator.Controls.Add(new LiteralControl("</div>"));
          }
        }
        oRow = null;
      }
      dImgBanner = null;
    }

    protected override void OnPreRender(EventArgs e)
    {

      if (!string.IsNullOrEmpty(sCodBanner)) {
        StringBuilder jsBanner = new StringBuilder();
        jsBanner.Append("$(document).ready(function() {\n");
        jsBanner.Append(" $('#slider").Append(sCodBanner).Append("').s3Slider({\n");
        jsBanner.Append(" timeOut: 3000 \n");
        jsBanner.Append(" });\n");
        jsBanner.Append("});\n");
        Page.ClientScript.RegisterStartupScript(this.GetType(), "JQBanner_" + sCodBanner, jsBanner.ToString(), true);
      }

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

        Page.ClientScript.RegisterStartupScript(this.GetType(), "JQueryFancyboxRotator_" + sCodBanner, js.ToString(), true);
      }
    }

  }
}