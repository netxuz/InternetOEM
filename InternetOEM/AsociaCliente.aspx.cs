using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text;
using System.Data;
using Telerik.Web.UI;
using OnlineServices.Conn;
using OnlineServices.SystemData;
using OnlineServices.Method;

namespace ICommunity
{
  public partial class AsociaCliente : System.Web.UI.Page
  {
    Web oWeb = new Web();
    protected void Page_Load(object sender, EventArgs e)
    {
      oWeb.ValidaSessionAdm();
      CodUsuario.Value = oWeb.GetData("CodUsuario");

      if (!IsPostBack) {
        DBConn oConn = new DBConn();
        if (oConn.Open()) {
          SysUsuario oUsuario = new SysUsuario(ref oConn);
          oUsuario.CodUsuario = CodUsuario.Value;
          DataTable dt = oUsuario.Get();
          if (dt != null) {
            if (dt.Rows.Count > 0) {
              lblClientesAsignados.Text = "Clientes Asignados a " + dt.Rows[0]["nom_user"].ToString() + " " + dt.Rows[0]["ape_user"].ToString();
            }
          }
          dt = null;
          oConn.Close();
        }

      }
    }

    protected void rdClienteUsuario_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        SysClienteUsuario oClienteUsuario = new SysClienteUsuario(ref oConn);
        oClienteUsuario.CodUsuario = CodUsuario.Value;
        rdClienteUsuario.DataSource = oClienteUsuario.Get();

        oConn.Close();
      }

    }

    protected void rdClienteUsuario_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
      switch (e.CommandName)
      {
        case "cmdDelete":
          string pNkeyCliente = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["nkey_user"].ToString();
          DBConn oConn = new DBConn();
          if (oConn.Open())
          {
            SysClienteUsuario oClienteUsuario = new SysClienteUsuario(ref oConn);
            oClienteUsuario.CodUsuario = CodUsuario.Value;
            oClienteUsuario.NkeyUser = pNkeyCliente;
            oClienteUsuario.Accion = "ELIMINAR";
            oClienteUsuario.Put();

            oConn.Close();
          }
          rdClienteUsuario.Rebind();
          rdCliente.Rebind();
          UpdatePanel2.Update();
          break;
      }
    }

    protected void rdClienteUsuario_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
      if (e.Item is GridDataItem)
      {
        GridDataItem oItem = e.Item as GridDataItem;
        (oItem["Eliminar"].Controls[0] as Button).CssClass = "BtnColEliminar";
      }
    }

    protected void btnClose2_Click(object sender, EventArgs e)
    {
      Page.ClientScript.RegisterStartupScript(this.GetType(), "hide", "$(function () { $('#myModal').modal('hide'); });", true);
      rdClienteUsuario.Rebind();
      rdCliente.Rebind();
      UpdatePanel1.Update();
    }

    protected void BtnBuscarClienteNotIn_Click(object sender, EventArgs e)
    {
      rdCliente.Rebind();
    }

    protected void rdCliente_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
      DBConn oConn = new DBConn();
      if (oConn.Open()) {
        SysClienteUsuario oClienteUsuario = new SysClienteUsuario(ref oConn);
        oClienteUsuario.CodUsuario = CodUsuario.Value;
        oClienteUsuario.sNombre = txtBuscarCliente.Text;
        rdCliente.DataSource = oClienteUsuario.GetClienteNotInUsuario();

        oConn.Close();
      }
    }

    protected void rdCliente_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
      switch (e.CommandName)
      {
        case "cmdAgregar":
          string nKeyUsuario = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["nkey_cliente"].ToString();
          DBConn oConn = new DBConn();
          if (oConn.Open())
          {
            SysClienteUsuario oClienteUsuario = new SysClienteUsuario(ref oConn);
            oClienteUsuario.NkeyUser = nKeyUsuario;
            oClienteUsuario.CodUsuario = CodUsuario.Value;
            oClienteUsuario.Accion = "CREAR";
            oClienteUsuario.Put();
            oConn.Close();
          }
          rdClienteUsuario.Rebind();
          rdCliente.Rebind();
          UpdatePanel2.Update();
          break;
      }
    }

    protected void rdCliente_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
      if (e.Item is GridDataItem)
      {
        GridDataItem oItem = e.Item as GridDataItem;

        (oItem["Agregar"].Controls[0] as Button).CssClass = "BtnColAgregar";
      }
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
      Page.ClientScript.RegisterStartupScript(this.GetType(), "hide", "$(function () { $('#myModal').modal('hide'); });", true);
      rdClienteUsuario.Rebind();
      rdCliente.Rebind();
      UpdatePanel1.Update();
    }

    protected void btnVolver_Click(object sender, EventArgs e)
    {
      Response.Redirect("Usuario.aspx?CodUsuario=" + CodUsuario.Value);
    }

    protected void UpdatePanel1_Load(object sender, EventArgs e)
    {
      if (Request.Form["__EVENTARGUMENT"] != null)
      {
        if (Request.Form["__EVENTARGUMENT"].ToString() == "priorityUpdate")
        {
          lblStatus.Visible = false;
          lblStatus.Text = "Status actualizado satisfactoriamente";
          rdClienteUsuario.Rebind();
          rdCliente.Rebind();
          UpdatePanel1.Update();
        }
      }
    }
  }
}