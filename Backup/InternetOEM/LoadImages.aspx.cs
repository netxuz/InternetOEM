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
using OnlineServices.Method;

namespace ICommunity
{
  public partial class LoadImages : System.Web.UI.Page
  {
    Web oWeb = new Web();
    protected void Page_Load(object sender, EventArgs e)
    {
      if (string.IsNullOrEmpty(oWeb.GetData("CodContenido")))
      {
        OnlineServices.Method.Usuario oIsUsuario;
        oIsUsuario = oWeb.GetObjUsuario();
        Session["ReloadImageUser"] = "1";
        oUserCntrFileUpload.pUID = oIsUsuario.CodUsuario;
        oUserCntrFileUpload.pUrlPage = "~/Handler.ashx";
      }
      else {
        Session["ReloadImageCont"] = "1";
        oUserCntrFileUpload.pUID = oWeb.GetData("CodContenido");
        oUserCntrFileUpload.pUrlPage = "~/FileContent.ashx";
      }
    }
  }
}
