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
using OnlineServices.Reporting;
using OnlineServices.Method;
using System.Web.Services;

namespace ICommunity
{
  public partial class usuario_rep_debtcontrol : System.Web.UI.Page
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
        if (oConn.Open())
        {
          SysUsuario oUsuario = new SysUsuario(ref oConn);
          oUsuario.CodUsuario = CodUsuario.Value;
          DataTable dt = oUsuario.Get();
          if (dt != null)
          {
            if (dt.Rows.Count > 0)
            {
              lblUsuario.Text = dt.Rows[0]["nom_user"].ToString() + ' ' + dt.Rows[0]["ape_user"].ToString();
            }
          }
          dt = null;
        }
        oConn.Close();
      }
    }

    protected void rdReportesUsuario_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        GridColumn oGridColumn;

        oGridColumn = rdReportesUsuario.MasterTableView.Columns.FindByUniqueName("NomConsulta");
        oGridColumn.HeaderText = oCulture.GetResource("Reportes", "NomConsulta");

        cDebtUsrAsignados oDebtUsrAsignados = new cDebtUsrAsignados(ref oConn);
        oDebtUsrAsignados.CodUsuario = CodUsuario.Value;
        oDebtUsrAsignados.NOTIn = true;
        if (!string.IsNullOrEmpty(txtBuscar.Text))
          oDebtUsrAsignados.NomConsulta = txtBuscar.Text;
        rdReportesUsuario.DataSource = oDebtUsrAsignados.GetConsultaByUsuario();

        oConn.Close();
      }
    }

    protected void rdReportesUsuario_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
      switch (e.CommandName)
      {
        case "cmdDelete":
          string pCodConsulta = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["cod_consulta"].ToString();
          DBConn oConn = new DBConn();
          if (oConn.Open())
          {
            cDebtUsrAsignados oDebtUsrAsignados = new cDebtUsrAsignados(ref oConn);
            oDebtUsrAsignados.CodUsuario = CodUsuario.Value;
            oDebtUsrAsignados.CodConsulta = pCodConsulta;
            oDebtUsrAsignados.Accion = "ELIMINAR";
            oDebtUsrAsignados.Put();

            oConn.Close();
          }
          rdReportesUsuario.Rebind();
          rdReportesNotIn.Rebind();
          UpdatePanel2.Update();
          break;
      }
    }

    protected void rdReportesUsuario_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
      if (e.Item is GridDataItem)
      {
        GridDataItem oItem = e.Item as GridDataItem;
        (oItem["Eliminar"].Controls[0] as Button).CssClass = "BtnColEliminar";
      }
    }

    protected void UpdatePanel1_Load(object sender, EventArgs e)
    {
      if (Request.Form["__EVENTARGUMENT"] != null)
      {
        if (Request.Form["__EVENTARGUMENT"].ToString() == "priorityUpdate")
        {
          lblStatus.Visible = false;
          lblStatus.Text = "Status actualizado satisfactoriamente";
          rdReportesUsuario.Rebind();
          rdReportesNotIn.Rebind();
          UpdatePanel1.Update();
        }
      }
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
      rdReportesUsuario.Rebind();
    }

    protected void btnVolver_Click(object sender, EventArgs e)
    {
      Response.Redirect("usuarios_rep_debtcontrol.aspx");
    }

    protected void rdReportesNotIn_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        GridColumn oGridColumn;

        oGridColumn = rdReportesNotIn.MasterTableView.Columns.FindByUniqueName("NomConsulta");
        oGridColumn.HeaderText = oCulture.GetResource("Reportes", "NomConsulta");

        cDebtUsrAsignados oDebtUsrAsignados = new cDebtUsrAsignados(ref oConn);
        oDebtUsrAsignados.CodUsuario = CodUsuario.Value;
        oDebtUsrAsignados.NOTIn = false;
        oDebtUsrAsignados.EstConsulta = "V";
        if (!string.IsNullOrEmpty(txtBuscarReportes.Text))
          oDebtUsrAsignados.NomConsulta = txtBuscarReportes.Text;
        rdReportesNotIn.DataSource = oDebtUsrAsignados.GetConsultaByUsuario();

        oConn.Close();
      }
    }

    protected void rdReportesNotIn_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
      switch (e.CommandName)
      {
        case "cmdAgregar":
          string CodConsulta = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["cod_consulta"].ToString();
          DBConn oConn = new DBConn();
          if (oConn.Open())
          {
            cDebtUsrAsignados oDebtUsrAsignados = new cDebtUsrAsignados(ref oConn);
            oDebtUsrAsignados.CodConsulta = CodConsulta;
            oDebtUsrAsignados.CodUsuario = CodUsuario.Value;
            oDebtUsrAsignados.Accion = "CREAR";
            oDebtUsrAsignados.Put();
            oConn.Close();
          }
          rdReportesUsuario.Rebind();
          rdReportesNotIn.Rebind();
          break;
      }
    }

    protected void rdReportesNotIn_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
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
      rdReportesUsuario.Rebind();
      rdReportesNotIn.Rebind();
      UpdatePanel1.Update();
    }

    protected void BtnBuscarReportesNotIn_Click(object sender, EventArgs e)
    {
      rdReportesNotIn.Rebind();
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
      Page.ClientScript.RegisterStartupScript(this.GetType(), "hide", "$(function () { $('#myModal').modal('hide'); });", true);
      rdReportesUsuario.Rebind();
      rdReportesNotIn.Rebind();
      UpdatePanel1.Update();
    }

    [WebMethod]
    public static cFiltro[] getFiltros(string CodUsuario, string CodConsulta)
    {
      List<cFiltro> details = new List<cFiltro>();

      cFiltro oFiltro = new cFiltro();
      oFiltro.Deudor = string.Empty;
      oFiltro.Holding = string.Empty;

      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        cDebtUsrAsignados oDebtUsrAsignados = new cDebtUsrAsignados(ref oConn);
        oDebtUsrAsignados.CodUsuario = CodUsuario;
        oDebtUsrAsignados.CodConsulta = CodConsulta;
        DataTable dt = oDebtUsrAsignados.Get();
        if (dt != null)
          if (dt.Rows.Count > 0)
          {
            oFiltro.Deudor = dt.Rows[0]["filtro_deudor"].ToString();
            oFiltro.Holding = dt.Rows[0]["filtro_holding"].ToString();
          }
        dt = null;
        oConn.Close();
      }
      details.Add(oFiltro);
      return details.ToArray();
    }

    [WebMethod]
    public static cExito[] setFiltros(string CodUsuario, string CodConsulta, string sDeudor, string sHolding)
    {
      List<cExito> details = new List<cExito>();

      cExito oExito = new cExito();
      oExito.Exito = string.Empty;

      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        cDebtUsrAsignados oDebtUsrAsignados = new cDebtUsrAsignados(ref oConn);
        oDebtUsrAsignados.CodUsuario = CodUsuario;
        oDebtUsrAsignados.CodConsulta = CodConsulta;
        oDebtUsrAsignados.FiltroDeudor = sDeudor;
        oDebtUsrAsignados.FiltroHolding = sHolding;
        oDebtUsrAsignados.Accion = "EDITAR";
        oDebtUsrAsignados.Put();
        oConn.Close();

        oExito.Exito = "V";
      }
      details.Add(oExito);
      return details.ToArray();
    }  

  }

  public class cFiltro
  {
    public string Deudor { get; set; }
    public string Holding { get; set; }
  }

  public class cExito
  {
    public string Exito { get; set; }
  }
}