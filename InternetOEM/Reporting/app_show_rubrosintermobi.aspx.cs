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
  public partial class app_show_rubrosintermobi : System.Web.UI.Page
  {
    private OnlineServices.Method.Web oWeb = new OnlineServices.Method.Web();
    private OnlineServices.Method.Usuario oIsUsuario;
    protected void Page_Load(object sender, EventArgs e)
    {
      oIsUsuario = oWeb.GetObjUsuario();
      if (!IsPostBack)
      {
        hdd_arrNkeyCliente.Value = oWeb.GetData("ArrCodCliente");
      }
    }

    protected void rdGridDeudores_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        cDeudores oDeudores = new cDeudores(ref oConn);
        oDeudores.CodNkey = ((!string.IsNullOrEmpty(hdd_arrNkeyCliente.Value) ? hdd_arrNkeyCliente.Value : oIsUsuario.CodNkey));
        oDeudores.TipoUsuario = oIsUsuario.TipoUsuario;
        oDeudores.NkeyUsuario = oIsUsuario.NKeyUsuario;
        oDeudores.Nombre = rdTxtNombreDeudor.Text;
        oDeudores.RUT = rdTxtCodigo.Text;
        DataTable dtDeudores = oDeudores.GetDeudorRubrosIntermobi();
        rdGridDeudores.DataSource = dtDeudores;

      }
      oConn.Close();
    }

    protected void idBuscar_Click(object sender, EventArgs e)
    {
      rdGridDeudores.Rebind();
    }

    protected void rdGridDeudores_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
      if (e.CommandName == "Select")
      {
        hdd_coddeudor.Value = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["nkey_deudor"].ToString();
        hdd_razonsocial.Value = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["snombre"].ToString();

        Page.ClientScript.RegisterStartupScript(this.GetType(), "jsExec", "onClose();", true);
      }

    }

  }
}