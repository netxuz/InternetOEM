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
using OnlineServices.Antalis;
using OnlineServices.Method;

namespace ICommunity.Antalis
{
  public partial class usuarios_centrosdistribucion : System.Web.UI.Page
  {
    Web oWeb = new Web();
    Culture oCulture = new Culture();
    protected void Page_Load(object sender, EventArgs e)
    {
      oWeb.ValidaSessionAdm();
      if (!IsPostBack)
      {
        CodUsuario.Value = oWeb.GetData("CodUsuario");
        DBConn oConn = new DBConn();
        if (oConn.Open()) {
          SysUsuario oUsuario = new SysUsuario(ref oConn);
          oUsuario.CodUsuario = CodUsuario.Value;
          DataTable dt = oUsuario.Get();
          if (dt != null) {
            if (dt.Rows.Count > 0) {
              lblUsuario.Text = dt.Rows[0]["nom_user"].ToString() + " " + dt.Rows[0]["ape_user"].ToString();
            }
          }
          dt = null;
        }
        oConn.Close();
      }
    }

    protected void rdUsuarios_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        cAntsUsuarios oAntsUsuarios = new cAntsUsuarios(ref oConn);
        oAntsUsuarios.CodUsuario = CodUsuario.Value;
        rdUsuarios.DataSource = oAntsUsuarios.GetCentrosDistribución();
      }
    }

    protected void rdUsuarios_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
      switch (e.CommandName)
      {
        case "cmdEliminar":
          string CodCentroDist = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["cod_centrodist"].ToString();
          DBConn oConn = new DBConn();
          if (oConn.Open())
          {
            cAntsUsuarios oAntsUsuarios = new cAntsUsuarios(ref oConn);
            oAntsUsuarios.CodUsuario = CodUsuario.Value;
            oAntsUsuarios.CodCentroDist = CodCentroDist;
            oAntsUsuarios.Accion = "ELIMINAR";
            oAntsUsuarios.PutCentroDist();
            rdUsuarios.Rebind();
          }
          oConn.Close();
          break;
      }
    }

    protected void rdUsuarios_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
      if (e.Item is GridDataItem)
      {
        GridDataItem oItem = e.Item as GridDataItem;
        (oItem["Editar"].Controls[0] as Button).CssClass = "BtnColEliminar";
      }
    }

    protected void btnVolver_Click(object sender, EventArgs e)
    {
      Response.Redirect("asignacion_centrodistribucion.aspx");
    }
  }
}