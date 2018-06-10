using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using Telerik.Web.UI;
using OnlineServices.Conn;
using OnlineServices.SystemData;
using OnlineServices.Method;

namespace ICommunity
{
  public partial class Perfiles : System.Web.UI.Page
  {
    Web oWeb = new Web();
    Culture oCulture = new Culture();

    protected void Page_Load(object sender, EventArgs e)
    {
      oWeb.ValidaSessionAdm();
    }

    protected void rdPerfil_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        GridColumn oGridColumn;

        oGridColumn = rdPerfil.MasterTableView.Columns.FindByUniqueName("NomPerfil");
        oGridColumn.HeaderText = oCulture.GetResource("Perfil", "NomPerfil");

        oGridColumn = rdPerfil.MasterTableView.Columns.FindByUniqueName("EstPerfil");
        oGridColumn.HeaderText = oCulture.GetResource("Perfil", "EstPerfil");

        SysPerfiles oPerfiles = new SysPerfiles(ref oConn);
        rdPerfil.DataSource = oPerfiles.Get();

        oConn.Close();
      }
    }

    protected void rdPerfil_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
      switch (e.CommandName)
      {
        case "cmdEdit":
          string[] cParam = new string[2];
          cParam[0] = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["cod_perfil"].ToString();
          Response.Redirect(String.Format("perfil.aspx?CodPerfil={0}", cParam));
          break;
        case "cmdDelete":
          string pCodPerfil = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["cod_perfil"].ToString();
          DBConn oConn = new DBConn();
          if (oConn.Open())
          {
            string sPath = Server.MapPath(".") + @"\binary\Perfil_" + pCodPerfil + ".bin";
            File.Delete(sPath);

            SysPerfiles oPerfiles = new SysPerfiles(ref oConn);
            oPerfiles.CodPerfil = pCodPerfil;
            oPerfiles.Accion = "ELIMINAR";
            oPerfiles.Put();

            oConn.Close();
          }
          rdPerfil.Rebind();
          break;
      }
    }

    protected void rdPerfil_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
      if (e.Item is GridDataItem)
      {
        GridDataItem oItem = e.Item as GridDataItem;

        (oItem["Editar"].Controls[0] as Button).CssClass = "BtnColEditar";
        (oItem["Eliminar"].Controls[0] as Button).CssClass = "BtnColEliminar";

        if ((!string.IsNullOrEmpty(oItem["EstPerfil"].Text)) && (oItem["EstPerfil"].Text != "&nbsp;"))
        {
          if (oItem["EstPerfil"].Text == "V")
            oItem["EstPerfil"].Text = oCulture.GetResource("Perfil", "Vigente");
          else
            oItem["EstPerfil"].Text = oCulture.GetResource("Perfil", "NoVigente");
        }
      }
    }
  }
}