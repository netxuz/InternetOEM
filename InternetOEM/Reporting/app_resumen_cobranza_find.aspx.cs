﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using OnlineServices.Conn;
using OnlineServices.Method;
using OnlineServices.Reporting;
using Telerik.Web.UI;
using ClosedXML.Excel;

using OnlineServices.Antalis;
using OnlineServices.SystemData;

namespace ICommunity.Reporting
{
  public partial class app_resumen_cobranza_find : System.Web.UI.Page
  {
    private OnlineServices.Method.Web oWeb = new OnlineServices.Method.Web();
    private OnlineServices.Method.Usuario oIsUsuario;
    string Signomoneda = string.Empty;
    string Decimales = string.Empty;
    bool bCliente = false;
    bool bDeudor;
    bool bHolding;
    protected void Page_Load(object sender, EventArgs e)
    {
      oIsUsuario = oWeb.ValidaUserAppReport();
      oIsUsuario = oWeb.GetObjUsuario();

      DateTime dTimeNow = DateTime.Now;
      //getMenu(idReportePago, oIsUsuario.CodUsuario, "1");
      //getMenu(idProcesoSeguimiento, oIsUsuario.CodUsuario, "2");
      //getMenu(idCartolas, oIsUsuario.CodUsuario, "3");
      //getMenu(idProcesoNormalizacion, oIsUsuario.CodUsuario, "4");
      //getMenu(idIndicadoresClaves, oIsUsuario.CodUsuario, "5");
      //getMenu(IndClasificacionRiesgo, oIsUsuario.CodUsuario, "6");
      //getMenuAntalis(indAntalis, oIsUsuario.CodUsuario);

      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        if (!IsPostBack)
        {
          string arrNkeyCliente = string.Empty;
          SysClienteUsuario oClienteUsuario = new SysClienteUsuario(ref oConn);
          oClienteUsuario.CodUsuario = oIsUsuario.CodUsuario;
          DataTable dt = oClienteUsuario.Get();
          if (dt != null)
          {
            foreach (DataRow dRow in dt.Rows)
            {
              arrNkeyCliente = (string.IsNullOrEmpty(arrNkeyCliente) ? dRow["nkey_user"].ToString() : arrNkeyCliente + "," + dRow["nkey_user"].ToString());
            }

            hdd_arrNkeyCliente.Value = arrNkeyCliente;
          }
          dt = null;

          if (arrNkeyCliente.Split(',').Count() > 0)
          {
            hdd_cli_show.Value = "V";
            bCliente = true;
            cCliente oCliente = new cCliente(ref oConn);
            oCliente.ArrNkeyCliente = arrNkeyCliente;
            dt = oCliente.GetClientes();

            if (dt != null)
            {
              cmbCliente.Items.Add(new ListItem("<< Seleccione Cliente >>", string.Empty));
              foreach (DataRow oRow in dt.Rows)
              {
                cmbCliente.Items.Add(new ListItem(oRow["snombre"].ToString(), oRow["nkey_cliente"].ToString()));
              }
            }
            dt = null;

            colClientes.Visible = true;
          }

          cDebtUsrAsignados oDebtUsrAsignados = new cDebtUsrAsignados(ref oConn);
          oDebtUsrAsignados.CodUsuario = oIsUsuario.CodUsuario;
          oDebtUsrAsignados.CodConsulta = "1";
          dt = oDebtUsrAsignados.Get();
          if (dt != null)
          {
            if (dt.Rows.Count > 0)
            {
              bDeudor = ((dt.Rows[0]["filtro_deudor"].ToString() == "V") ? true : false);
              bHolding = ((dt.Rows[0]["filtro_holding"].ToString() == "V") ? true : false);
            }
          }
          dt = null;

          if (bDeudor)
            colDeudor.Visible = true;

          if (bHolding)
          {
            colHolding.Visible = true;
            cCliente oCliente = new cCliente(ref oConn);
            oCliente.ArrNkeyCliente = arrNkeyCliente;
            dt = oCliente.GetHolding();
            if (dt != null)
            {
              if (dt.Rows.Count > 0)
              {
                cmbHolding.Visible = true;
                cmbHolding.Items.Add(new ListItem("<< Seleccione Holding >>", string.Empty));
                foreach (DataRow oRow in dt.Rows)
                {
                  cmbHolding.Items.Add(new ListItem(oRow["holding"].ToString(), oRow["ncodholding"].ToString()));
                }
              }
            }

          }

          RadDatePicker3.DateInput.DateFormat = "dd-MM-yyyy";
          RadDatePicker4.DateInput.DateFormat = "dd-MM-yyyy";
          RadDatePicker3.SelectedDate = dTimeNow.AddMonths(-1);
          RadDatePicker4.SelectedDate = dTimeNow;

          Log oLog = new Log();
          oLog.IdUsuario = oIsUsuario.CodUsuario;
          oLog.ObsLog = "REPORTE DE RESUMEN DE COBRANZA";
          oLog.CodEvtLog = "1";
          oLog.AppLog = "REPORTES DEBTCONTROL";
          oLog.putLog();
        }

        if (!string.IsNullOrEmpty(cmbCliente.SelectedValue))
        {
          cCliente oCliente = new cCliente(ref oConn);
          oCliente.CodNkey = cmbCliente.SelectedValue;
          DataTable dt = oCliente.GeCliente();
          if (dt != null)
          {
            if (dt.Rows.Count > 0)
            {
              Signomoneda = dt.Rows[0]["signomoneda"].ToString().Trim();
              Decimales = dt.Rows[0]["decimales"].ToString();
            }
          }
          dt = null;

          if (!string.IsNullOrEmpty(Signomoneda))
            lblmoneda.Text = "Montos expresados en " + Signomoneda;

        }
        oConn.Close();
      }

    }

    protected void getMenuAntalis(System.Web.UI.HtmlControls.HtmlGenericControl oHtmControl, string pCoduser)
    {

      DBConn oConn = new DBConn();
      if (oConn.Open())
      {

        SyrPerfilesUsuarios oSysPerfilesUsuarios = new SyrPerfilesUsuarios(ref oConn);
        oSysPerfilesUsuarios.CodUsuario = pCoduser;
        oSysPerfilesUsuarios.CodPerfil = "7";
        DataTable dtPerfil = oSysPerfilesUsuarios.Get();
        if (dtPerfil != null)
        {
          if (dtPerfil.Rows.Count > 0)
          {
            cAntsUsuarios oAntsUsuarios = new cAntsUsuarios(ref oConn);
            oAntsUsuarios.CodUsuario = pCoduser;
            DataTable dtAntRoles = oAntsUsuarios.GetRoles();
            if (dtAntRoles != null)
            {
              foreach (DataRow oRow in dtAntRoles.Rows)
              {

                if (oRow["cod_rol"].ToString() == "1")
                  oHtmControl.Controls.Add(new LiteralControl("<li><a href='../antalis/pagos_antalis.aspx'>Ingreso de Pago</a></li>"));
                if (oRow["cod_rol"].ToString() == "2")
                  oHtmControl.Controls.Add(new LiteralControl("<li><a href='../antalis/controllerpagos.aspx'>Validación de Pago</a></li>"));
              }
            }
            dtAntRoles = null;
            oHtmControl.Controls.Add(new LiteralControl("<li><a href='../antalis/reportevalijas.aspx'>Valijas Validadas</a></li>"));
          }
        }
        dtPerfil = null;
      }
      oConn.Close();

    }

    protected void getMenu(System.Web.UI.HtmlControls.HtmlGenericControl oHtmControl, string pCodUser, string oOrdConsulta)
    {
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        cReportes oReportes = new cReportes(ref oConn);
        oReportes.CodUser = pCodUser;
        oReportes.OrdConsulta = oOrdConsulta;
        DataTable dtQuery = oReportes.GetMenu();
        if (dtQuery != null)
        {
          if (dtQuery.Rows.Count > 0)
          {
            foreach (DataRow oRow in dtQuery.Rows)
            {
              oHtmControl.Controls.Add(new LiteralControl("<li><a href=\"../reporting/" + oRow["url_consulta_new"].ToString() + "\">" + oRow["nom_consulta"].ToString() + "</a></li>"));
            }
          }
        }
        dtQuery = null;
      }
      oConn.Close();
    }

    protected void idBuscar_Click(object sender, EventArgs e)
    {
      idGrilla.Visible = true;
      rdGridResumenCobranza.Rebind();
    }

    protected void rdGridResumenCobranza_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
      rdGridResumenCobranza.DataSource = getDatatble();
    }

    protected void rdGridResumenCobranza_ItemCommand(object source, GridCommandEventArgs e)
    {
      if (e.CommandName == RadGrid.ExportToExcelCommandName)
      {
        DataTable dt = getDatatble();

        XLWorkbook wb = new XLWorkbook();
        wb.Worksheets.Add(dt, "resumencobranza");

        Response.Clear();
        Response.Buffer = true;
        Response.Charset = "";
        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        Response.AddHeader("content-disposition", "attachment;filename=resumencobranza_" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx");
        using (MemoryStream MyMemoryStream = new MemoryStream())
        {
          wb.SaveAs(MyMemoryStream);
          MyMemoryStream.WriteTo(Response.OutputStream);
          Response.Flush();
          Response.End();
        }
      }
    }

    protected void rdGridResumenCobranza_ItemDataBound(object sender, GridItemEventArgs e)
    {
      if (e.Item is GridDataItem)
      {
        GridDataItem item = (GridDataItem)e.Item;
        DataRowView row = (DataRowView)e.Item.DataItem;

        if (!string.IsNullOrEmpty(Decimales))
        {
          if (int.Parse(Decimales) > 0)
          {
            item["Monto_Pago"].Text = double.Parse(row["Monto_Pago"].ToString()).ToString("N" + Decimales);
            item["Abono"].Text = double.Parse(row["Abono"].ToString()).ToString("N" + Decimales);
            item["Saldo"].Text = double.Parse(row["Saldo"].ToString()).ToString("N" + Decimales);
          }
          else
          {
            item["Monto_Pago"].Text = double.Parse(row["Monto_Pago"].ToString()).ToString("N0");
            item["Abono"].Text = double.Parse(row["Abono"].ToString()).ToString("N0");
            item["Saldo"].Text = double.Parse(row["Saldo"].ToString()).ToString("N0");
          }
        }
        else
        {
          item["Monto_Pago"].Text = double.Parse(row["Monto_Pago"].ToString()).ToString("N0");
          item["Abono"].Text = double.Parse(row["Abono"].ToString()).ToString("N0");
          item["Saldo"].Text = double.Parse(row["Saldo"].ToString()).ToString("N0");
        }
      }
    }

    public DataTable getDatatble()
    {
      DataTable dt = null;

      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        cResumenCobranza oResumenCobranza = new cResumenCobranza(ref oConn);
        oResumenCobranza.CodDeudor = hddCodDeudor.Value;
        oResumenCobranza.CodNkey = ((!string.IsNullOrEmpty(cmbCliente.SelectedValue) ? cmbCliente.SelectedValue : hdd_arrNkeyCliente.Value));
        oResumenCobranza.NcodHolding = cmbHolding.SelectedValue;
        oResumenCobranza.NkeyUsuario = oIsUsuario.NKeyUsuario;
        oResumenCobranza.TipoUsuario = oIsUsuario.TipoUsuario;
        oResumenCobranza.NumPago = rdTxtNumPago.Text;
        oResumenCobranza.DtFchIni = DateTime.Parse(RadDatePicker3.SelectedDate.ToString()).ToString("yyyyMMdd");
        oResumenCobranza.DtFchFin = DateTime.Parse(RadDatePicker4.SelectedDate.ToString()).ToString("yyyyMMdd");
        dt = oResumenCobranza.Get();

      }
      oConn.Close();

      return dt;
    }

    protected void Page_PreRender(object o, EventArgs e)
    {

      rdGridResumenCobranza.MasterTableView.GetColumn("ncodholding").Display = true;
      rdGridResumenCobranza.MasterTableView.GetColumn("nkey_deudor").Display = true;
      rdGridResumenCobranza.MasterTableView.GetColumn("RazónSocial").Display = true;

      if (!string.IsNullOrEmpty(hddCodDeudor.Value))
      {
        rdGridResumenCobranza.MasterTableView.GetColumn("ncodholding").Display = false;
        rdGridResumenCobranza.MasterTableView.GetColumn("nkey_deudor").Display = false;
        rdGridResumenCobranza.MasterTableView.GetColumn("RazónSocial").Display = false;
      }

      if ((string.IsNullOrEmpty(hddCodDeudor.Value)) && (string.IsNullOrEmpty(cmbCliente.SelectedValue)) && (string.IsNullOrEmpty(cmbHolding.SelectedValue)))
      {
        rdGridResumenCobranza.MasterTableView.GetColumn("ncodholding").Display = false;
      }

    }

    protected void bnt_logout_Click(object sender, EventArgs e)
    {
      Session["USUARIO"] = string.Empty;
      Session["CodUsuarioPerfil"] = string.Empty;
      Response.Redirect("/");
    }
  }
}