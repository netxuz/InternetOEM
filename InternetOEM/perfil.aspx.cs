using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;

using Telerik.Web.UI;
using OnlineServices.Conn;
using OnlineServices.SystemData;
using OnlineServices.Method;

namespace ICommunity
{
  public partial class perfil : System.Web.UI.Page
  {

    Web oWeb = new Web();
    Culture oCulture = new Culture();

    protected void Page_Load(object sender, EventArgs e)
    {
      oWeb.ValidaSessionAdm();
      if (!IsPostBack)
      {
        CodPerfil.Value = oWeb.GetData("CodPerfil");
        DBConn oConn = new DBConn();
        if (oConn.Open())
        {
          SysPerfiles oPerfiles = new SysPerfiles(ref oConn);
          oPerfiles.CodPerfil = CodPerfil.Value;
          DataTable dt = oPerfiles.Get();
          if (dt != null)
          {
            if (dt.Rows.Count > 0)
            {
              lblPerfil.Text = dt.Rows[0]["nom_perfil"].ToString();
            }
          }
          dt = null;
        }
        oConn.Close();
      }
    }

    protected void rdPerfilUsuario_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        GridColumn oGridColumn;

        oGridColumn = rdPerfilUsuario.MasterTableView.Columns.FindByUniqueName("NomUsuario");
        oGridColumn.HeaderText = oCulture.GetResource("Perfil", "NomUsuario");

        SyrPerfilesUsuarios oPerfilesUsuarios = new SyrPerfilesUsuarios(ref oConn);
        oPerfilesUsuarios.CodPerfil = CodPerfil.Value;
        oPerfilesUsuarios.NOTIn = true;
        if (!string.IsNullOrEmpty(txtTitulo.Text))
          oPerfilesUsuarios.NomApeUsuario = txtTitulo.Text;
        rdPerfilUsuario.DataSource = oPerfilesUsuarios.GetUsuariosByPerfil();

        oConn.Close();
      }
    }

    protected void rdPerfilUsuario_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
      switch (e.CommandName)
      {
        case "cmdDelete":
          string pCodUsuario = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["cod_user"].ToString();
          DBConn oConn = new DBConn();
          if (oConn.Open())
          {
            SyrPerfilesUsuarios oPerfilesUsuarios = new SyrPerfilesUsuarios(ref oConn);
            oPerfilesUsuarios.CodUsuario = pCodUsuario;
            oPerfilesUsuarios.CodPerfil = CodPerfil.Value;
            oPerfilesUsuarios.Accion = "ELIMINAR";
            oPerfilesUsuarios.Put();

            oConn.Close();
          }
          rdPerfilUsuario.Rebind();
          rdUsuarios.Rebind();
          UpdatePanel2.Update();
          break;
      }
    }

    protected void rdPerfilUsuario_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
      if (e.Item is GridDataItem)
      {
        GridDataItem oItem = e.Item as GridDataItem;
        (oItem["Eliminar"].Controls[0] as Button).CssClass = "BtnColEliminar";
      }
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
      rdPerfilUsuario.Rebind();
    }

    protected void UpdatePanel1_Load(object sender, EventArgs e)
    {
      if (Request.Form["__EVENTARGUMENT"] != null)
      {
        if (Request.Form["__EVENTARGUMENT"].ToString() == "priorityUpdate")
        {
          lblStatus.Visible = false;
          lblStatus.Text = "Status actualizado satisfactoriamente";
          rdPerfilUsuario.Rebind();
          rdUsuarios.Rebind();
          UpdatePanel1.Update();
        }
      }
    }

    protected void btnClose2_Click(object sender, EventArgs e)
    {
      Page.ClientScript.RegisterStartupScript(this.GetType(), "hide", "$(function () { $('#myModal').modal('hide'); });", true);
      rdPerfilUsuario.Rebind();
      rdUsuarios.Rebind();
      UpdatePanel1.Update();
    }

    protected void rdUsuarios_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        GridColumn oGridColumn;

        oGridColumn = rdUsuarios.MasterTableView.Columns.FindByUniqueName("NomUsuario");
        oGridColumn.HeaderText = oCulture.GetResource("Perfil", "NomUsuario");

        SyrPerfilesUsuarios oPerfilesUsuarios = new SyrPerfilesUsuarios(ref oConn);
        oPerfilesUsuarios.CodPerfil = CodPerfil.Value;
        oPerfilesUsuarios.NOTIn = false;
        if (!string.IsNullOrEmpty(txtBuscarUsuario.Text))
          oPerfilesUsuarios.NomApeUsuario = txtBuscarUsuario.Text;
        rdUsuarios.DataSource = oPerfilesUsuarios.GetUsuariosByPerfil();

        oConn.Close();
      }
    }

    protected void rdUsuarios_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
      switch (e.CommandName)
      {
        case "cmdAgregar":
          string CodUsuario = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["cod_user"].ToString();
          DBConn oConn = new DBConn();
          if (oConn.Open())
          {
            SyrPerfilesUsuarios oPerfilesUsuarios = new SyrPerfilesUsuarios(ref oConn);
            oPerfilesUsuarios.CodPerfil = CodPerfil.Value;
            oPerfilesUsuarios.CodUsuario = CodUsuario;
            oPerfilesUsuarios.Accion = "CREAR";
            oPerfilesUsuarios.Put();
            oConn.Close();
          }
          rdPerfilUsuario.Rebind();
          rdUsuarios.Rebind();
          break;
      }
    }

    protected void rdUsuarios_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
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
      rdPerfilUsuario.Rebind();
      rdUsuarios.Rebind();
      UpdatePanel1.Update();
    }

    protected void BtnBuscarUsuariosNotIn_Click(object sender, EventArgs e)
    {
      rdUsuarios.Rebind();
    }

    protected void btnVolver_Click(object sender, EventArgs e)
    {
      Response.Redirect("perfiles.aspx");
    }
  }
}