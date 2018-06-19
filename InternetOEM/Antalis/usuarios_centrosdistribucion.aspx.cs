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
        cAntCentrosDistribucion oCentrosDistribucion = new cAntCentrosDistribucion(ref oConn);
        oCentrosDistribucion.CodUsuario = CodUsuario.Value;
        rdUsuarios.DataSource = oCentrosDistribucion.GetCentrosDistByUsuario();
      }
    }

    protected void rdUsuarios_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
      switch (e.CommandName)
      {
        case "cmdEliminar":
          string CodCentroDist = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["cod_centrodist"].ToString();
          DBConn oConn = new DBConn();
          if (oConn.Open())
          {
            cAntCentrosDistribucion oCentrosDistribucion = new cAntCentrosDistribucion(ref oConn);
            oCentrosDistribucion.CodUsuario = CodUsuario.Value;
            oCentrosDistribucion.CodCentroDist = CodCentroDist;
            oCentrosDistribucion.Accion = "ELIMINAR";
            oCentrosDistribucion.Put();
            rdUsuarios.Rebind();
            rdCentrosDistribucion.Rebind();
            UpdatePanel2.Update();
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
        (oItem["Eliminar"].Controls[0] as Button).CssClass = "BtnColEliminar";
      }
    }

    protected void btnVolver_Click(object sender, EventArgs e)
    {
      Response.Redirect("asignacion_centrodistribucion.aspx");
    }

    protected void rdCentrosDistribucion_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
      DBConn oConn = new DBConn();
      if (oConn.Open()) {
        cAntCentrosDistribucion oCentrosDistribucion = new cAntCentrosDistribucion(ref oConn);
        oCentrosDistribucion.CodUsuario = CodUsuario.Value;
        oCentrosDistribucion.CodTblDatos = "CENDIS";
        if (!string.IsNullOrEmpty(txtBuscaCentros.Text)) {
          oCentrosDistribucion.Descripcion = txtBuscaCentros.Text;
        }
        rdCentrosDistribucion.DataSource = oCentrosDistribucion.Get();

        oConn.Close();
      }
    }

    protected void rdCentrosDistribucion_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
      switch (e.CommandName)
      {
        case "cmdAgregar":
          string CodCentroDist = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["valor"].ToString();
          DBConn oConn = new DBConn();
          if (oConn.Open())
          {
            cAntCentrosDistribucion oCentrosDistribucion = new cAntCentrosDistribucion(ref oConn);
            oCentrosDistribucion.CodUsuario = CodUsuario.Value;
            oCentrosDistribucion.CodCentroDist = CodCentroDist;
            oCentrosDistribucion.Accion = "CREAR";
            oCentrosDistribucion.Put();
            oConn.Close();
          }
          rdUsuarios.Rebind();
          rdCentrosDistribucion.Rebind();
          break;
      }
    }

    protected void rdCentrosDistribucion_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
      if (e.Item is GridDataItem)
      {
        GridDataItem oItem = e.Item as GridDataItem;

        (oItem["Agregar"].Controls[0] as Button).CssClass = "BtnColAgregar";
      }
    }

    protected void btnClose2_Click(object sender, EventArgs e)
    {
      Page.ClientScript.RegisterStartupScript(this.GetType(), "hide", "$(function () { $('#myModal').modal('hide'); });", true);
      rdUsuarios.Rebind();
      rdCentrosDistribucion.Rebind();
      UpdatePanel1.Update();
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
      Page.ClientScript.RegisterStartupScript(this.GetType(), "hide", "$(function () { $('#myModal').modal('hide'); });", true);
      rdUsuarios.Rebind();
      rdCentrosDistribucion.Rebind();
      UpdatePanel1.Update();
    }

    protected void btnBuscaCentros_Click(object sender, EventArgs e)
    {
      rdCentrosDistribucion.Rebind();
    }

    protected void UpdatePanel1_Load(object sender, EventArgs e)
    {
      if (Request.Form["__EVENTARGUMENT"] != null)
      {
        if (Request.Form["__EVENTARGUMENT"].ToString() == "priorityUpdate")
        {
          lblStatus.Visible = false;
          lblStatus.Text = "Status actualizado satisfactoriamente";
          rdUsuarios.Rebind();
          rdCentrosDistribucion.Rebind();
          UpdatePanel1.Update();
        }
      }
    }

    
  }
}