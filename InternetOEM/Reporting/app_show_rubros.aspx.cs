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
  public partial class app_show_rubros : System.Web.UI.Page
  {
    private OnlineServices.Method.Web oWeb = new OnlineServices.Method.Web();
    private OnlineServices.Method.Usuario oIsUsuario;
    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        hdd_canal.Value = oWeb.GetData("hdd_canal");
        oIsUsuario = oWeb.GetObjUsuario();

        if (hdd_canal.Value == "R")
        {
          lblTitle.Text = "RUBROS";
          rdTxtNombreDeudor.Attributes.Add("placeholder", "RUBROS");
        }
        else {
          lblTitle.Text = "CANALES";
          rdTxtNombreDeudor.Attributes.Add("placeholder", "CANALES");
        }

      }
    }

    protected void idBuscar_Click(object sender, EventArgs e)
    {
      rdGridRubros.Rebind();
    }

    protected void rdGridRubros_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        cRubros oRubros = new cRubros(ref oConn);
        oRubros.IndCanal = hdd_canal.Value;
        oRubros.Nombre = rdTxtNombreDeudor.Text;
        DataTable dtRubros = oRubros.Get();
        rdGridRubros.DataSource = dtRubros;

      }
      oConn.Close();
    }

    protected void rdGridRubros_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
      if (e.CommandName == "Select")
      {
        hdd_coddeudor.Value = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["nKey_Rubros"].ToString();
        hdd_razonsocial.Value = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["sRubro"].ToString();

        Page.ClientScript.RegisterStartupScript(this.GetType(), "jsExec", "onClose();", true);
      }
    }

    protected void rdGridRubros_PreRender(object source, EventArgs e)
    {
      if (hdd_canal.Value == "R")
      {
        rdGridRubros.MasterTableView.GetColumn("Canal").HeaderText = "RUBROS";
      }
      else {
        rdGridRubros.MasterTableView.GetColumn("Canal").HeaderText = "CANALES";
      }
      rdGridRubros.Rebind();
    }
  }
}