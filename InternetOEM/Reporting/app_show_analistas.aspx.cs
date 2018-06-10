using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using OnlineServices.Conn;
using OnlineServices.Method;
using OnlineServices.Reporting;
using Telerik.Web.UI;

namespace ICommunity.Reporting
{
  public partial class app_show_analistas : System.Web.UI.Page
  {
    private OnlineServices.Method.Web oWeb = new OnlineServices.Method.Web();
    private OnlineServices.Method.Usuario oIsUsuario;
    protected void Page_Load(object sender, EventArgs e)
    {
      oIsUsuario = oWeb.GetObjUsuario();
    }

    protected void rdGridAnalista_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        cAnalistas oAnalistas = new cAnalistas(ref oConn);
        oAnalistas.Nombre = rdTxtAnalista.Text;
        DataTable dtanalista = oAnalistas.Get();
        rdGridAnalista.DataSource = dtanalista;

      }
      oConn.Close();
    }

    protected void rdGridAnalista_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
      if (e.CommandName == "Select")
      {
        hdd_coddeudor.Value = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["nkey_analista"].ToString();
        hdd_razonsocial.Value = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["snombre"].ToString();

        Page.ClientScript.RegisterStartupScript(this.GetType(), "jsExec", "onClose();", true);
      }
    }

    protected void idBuscar_Click(object sender, EventArgs e)
    {
      rdGridAnalista.Rebind();
    }
  }
}