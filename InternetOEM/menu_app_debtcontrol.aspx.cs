using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ICommunity
{
  public partial class menu_app_debtcontrol : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void lnkBtnRepToUser_Click(object sender, EventArgs e)
    {
      Response.Redirect("usuarios_rep_debtcontrol.aspx");
    }
  }
}