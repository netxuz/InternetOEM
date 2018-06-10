using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using Telerik.Web.UI;
using OnlineServices.Conn;
using OnlineServices.SystemData;
using OnlineServices.Method;

namespace ICommunity
{
  public partial class menu_app_reportesruta : System.Web.UI.Page
  {
    Web oWeb = new Web();
    Culture oCulture = new Culture();
    protected void Page_Load(object sender, EventArgs e)
    {
      oWeb.ValidaSessionAdm();
      btnBuscar.Text = oCulture.GetResource("Global", "btnBuscar");
    }

    protected void rdUsuarios_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        GridColumn oGridColumn;

        oGridColumn = rdUsuarios.MasterTableView.Columns.FindByUniqueName("NomUsuario");
        oGridColumn.HeaderText = oCulture.GetResource("Usuario", "NomUsuario");

        oGridColumn = rdUsuarios.MasterTableView.Columns.FindByUniqueName("ApeUsuario");
        oGridColumn.HeaderText = oCulture.GetResource("Usuario", "ApeUsuario");

        SysUsuario oUsuario = new SysUsuario(ref oConn);
        if (!string.IsNullOrEmpty(txt_buscar.Text.ToString()))
        {
          oUsuario.NomUsuario = txt_buscar.Text;
        }
        rdUsuarios.DataSource = oUsuario.GetMotorista();

        oConn.Close();
      }
    }

    protected void rdUsuarios_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
      switch (e.CommandName)
      {
        case "cmdEdit":
          string[] cParam = new string[2];
          cParam[0] = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["cod_user"].ToString();
          Response.Redirect(String.Format("AppRuta/applistactividad.aspx?CodUsuario={0}", cParam));
          break;
      }
    }

    protected void rdUsuarios_ItemDataBound(object source, Telerik.Web.UI.GridItemEventArgs e)
    {
      if (e.Item is GridDataItem)
      {
        GridDataItem oItem = e.Item as GridDataItem;
        (oItem["Editar"].Controls[0] as Button).CssClass = "BtnColEditar";
      }
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
      rdUsuarios.Rebind();
    }
  }
}