using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OnlineServices.Method;

namespace ICommunity.Controls
{
  public partial class IndicadoresEconomicos : System.Web.UI.UserControl
  {
    IndicadorEconomico oIndEco;
    Web oWeb = new Web();
    protected void Page_Load(object sender, EventArgs e)
    {
      oWeb.GetIndicadoresEconomicos();
      oIndEco = (IndicadorEconomico)Session["INDICADORESESCONOCMICOS"];
      idDolar.Controls.Add(new LiteralControl(oIndEco.DolarObs));
      idEuro.Controls.Add(new LiteralControl(oIndEco.Euro));
      idUF.Controls.Add(new LiteralControl(oIndEco.UF));
    }
  }
}