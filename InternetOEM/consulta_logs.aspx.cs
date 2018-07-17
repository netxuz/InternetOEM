using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using OnlineServices.Conn;
using OnlineServices.SystemData;
using OnlineServices.Method;

namespace ICommunity
{
  public partial class consulta_logs : System.Web.UI.Page
  {
    Web oWeb = new Web();
    protected void Page_Load(object sender, EventArgs e)
    {
      oWeb.ValidaSessionAdm();
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
      Rowgrilla.Visible = true;
      rdLogs.Rebind();
    }

    protected void rdLogs_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        SysLog oSysLog = new SysLog(ref oConn);
        oSysLog.NombreApellido = txt_buscar_usuario.Text;
        oSysLog.CodEvtLog = rdComboAPP.SelectedValue;
        if ((!string.IsNullOrEmpty(RadDatePicker1.SelectedDate.ToString())) && (!string.IsNullOrEmpty(RadDatePicker2.SelectedDate.ToString())))
        {
          oSysLog.FechaInicial = DateTime.Parse(RadDatePicker1.SelectedDate.ToString()).ToString("yyyyMMdd");
          oSysLog.FechaFinal = DateTime.Parse(RadDatePicker2.SelectedDate.ToString()).ToString("yyyyMMdd");
        }
        rdLogs.DataSource = oSysLog.Get();
        oConn.Close();
      }
    }
  }
}