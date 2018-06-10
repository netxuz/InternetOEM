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

using OnlineServices.Conn;
using OnlineServices.AppData;
using OnlineServices.Method;
using Telerik.Web.UI;

namespace ICommunity.Controls
{
  public partial class LoadImgBanner : System.Web.UI.UserControl
  {
    private OnlineServices.Method.Web oWeb = new OnlineServices.Method.Web();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    
  }
}