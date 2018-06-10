using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using OnlineServices.Conn;
using OnlineServices.Method;
using OnlineServices.AppRuta;
using OnlineServices.SystemData;

namespace ICommunity.AppRuta
{
  public partial class applistactividad : System.Web.UI.Page
  {
    Web oWeb = new Web();
    string pCodUsuario = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
      oWeb.ValidaSessionAdm();
      if (!IsPostBack)
      {
        hddcodusuario.Value = oWeb.GetData("CodUsuario");

        DBConn oConn = new DBConn();
        if (oConn.Open())
        {
          SysUsuario oUsuario = new SysUsuario(ref oConn);
          oUsuario.CodUsuario = hddcodusuario.Value;
          DataTable dt = oUsuario.Get();
          if (dt != null) {
            if (dt.Rows.Count > 0) {
              lblMotorista.Text = dt.Rows[0]["nom_user"].ToString().ToUpper() + " " + dt.Rows[0]["ape_user"].ToString().ToUpper();
            }
          }
          dt = null;

        }
        oConn.Close();
      }
    }

    protected void rdActividad_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        cAppRegActividad oAppRegActividad = new cAppRegActividad(ref oConn);
        oAppRegActividad.CodUsuario = hddcodusuario.Value;
        rdActividad.DataSource = oAppRegActividad.Get();
      }
      oConn.Close();

    }

    protected void rdActividad_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
      switch (e.CommandName)
      {
        case "cmdEdit":
          string[] cParam = new string[2];
          cParam[0] = hddcodusuario.Value;
          cParam[1] = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["key_fecha_actividad"].ToString();
          Response.Redirect(String.Format("appshowmap.aspx?CodUsuario={0}&pFecha={1}", cParam[0], cParam[1]));
          break;
      }
    }

    protected void rdActividad_ItemDataBound(object source, Telerik.Web.UI.GridItemEventArgs e)
    {
      if (e.Item is GridDataItem)
      {
        GridDataItem oItem = e.Item as GridDataItem;
        (oItem["Editar"].Controls[0] as Button).CssClass = "BtnColEditar";
      }
    }

    protected void btnVolver_Click(object sender, EventArgs e)
    {
      Response.Redirect("../menu_app_reportesruta.aspx");
    }
  }
}