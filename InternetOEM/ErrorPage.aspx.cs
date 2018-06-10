using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;  
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;
using OnlineServices.Method;

namespace ICommunity
{
  public partial class ErrorPage : System.Web.UI.Page
  {
    Web oWeb = new Web();
    Culture oCulture = new Culture();
    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
        goSendError(oWeb.GetData("err"));
    }

    protected void goSendError(string sCodErr) {
      Log oLog = new Log();
      oLog.CodEvtLog = sCodErr;
      oLog.IdUsuario = "-1";
      oLog.ObsLog = "Error codigo " + sCodErr;
      //oLog.putLog();
    }

  }
}
