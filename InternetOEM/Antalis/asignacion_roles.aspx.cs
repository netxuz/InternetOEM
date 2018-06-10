using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Telerik.Web.UI;
using OnlineServices.Conn;
using OnlineServices.Antalis;
using OnlineServices.Method;

namespace ICommunity.Antalis
{
  public partial class asignacion_roles : System.Web.UI.Page
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

        oGridColumn = rdUsuarios.MasterTableView.Columns.FindByUniqueName("EstUsuario");
        oGridColumn.HeaderText = oCulture.GetResource("Global", "Estado");

        cAntsUsuarios oUsuario = new cAntsUsuarios(ref oConn);
        oUsuario.EstUsuario = "V";
        if (!string.IsNullOrEmpty(txt_buscar.Text.ToString()))
        {
          oUsuario.NomUsuario = txt_buscar.Text;
        }
        rdUsuarios.DataSource = oUsuario.Get();

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
          Response.Redirect(String.Format("usuario_rol_antalis.aspx?CodUsuario={0}", cParam));
          break;
      }
    }

    protected void rdUsuarios_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
      if (e.Item is GridDataItem)
      {
        GridDataItem oItem = e.Item as GridDataItem;
        (oItem["Editar"].Controls[0] as Button).CssClass = "BtnColEditar";

        if ((!string.IsNullOrEmpty(oItem["EstUsuario"].Text)) && (oItem["EstUsuario"].Text != "&nbsp;"))
        {
          if (oItem["EstUsuario"].Text == "V")
            oItem["EstUsuario"].Text = oCulture.GetResource("Global", "Vigente");
          else if (oItem["EstUsuario"].Text == "B")
            oItem["EstUsuario"].Text = oCulture.GetResource("Global", "Bloqueado");
          else if (oItem["EstUsuario"].Text == "E")
            oItem["EstUsuario"].Text = oCulture.GetResource("Global", "Eliminado");
          else
            oItem["EstUsuario"].Text = oCulture.GetResource("Global", "NoVigente");
        }
      }
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
      rdUsuarios.Rebind();
    }
  }
}