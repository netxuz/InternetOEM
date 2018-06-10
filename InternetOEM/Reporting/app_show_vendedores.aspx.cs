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
  public partial class app_show_vendedores : System.Web.UI.Page
  {
    private OnlineServices.Method.Web oWeb = new OnlineServices.Method.Web();
    private OnlineServices.Method.Usuario oIsUsuario;
    protected void Page_Load(object sender, EventArgs e)
    {
      oIsUsuario = oWeb.GetObjUsuario();
    }

    protected void rdGridVendedores_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        cVendedor oVendedor = new cVendedor(ref oConn);
        oVendedor.CodNkey = oIsUsuario.CodNkey;
        oVendedor.Nombre = rdTxtNombreDeudor.Text;
        DataTable dtVendedor = oVendedor.Get();
        rdGridVendedores.DataSource = dtVendedor;

      }
      oConn.Close();
    }

    protected void idBuscar_Click(object sender, EventArgs e)
    {
      rdGridVendedores.Rebind();
    }

    protected void rdGridVendedores_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
      if (e.CommandName == "Select")
      {
        hdd_coddeudor.Value = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["nkey_vendedor"].ToString();
        hdd_razonsocial.Value = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["snombre"].ToString();

        Page.ClientScript.RegisterStartupScript(this.GetType(), "jsExec", "onClose();", true);
      }
    }
  }
}