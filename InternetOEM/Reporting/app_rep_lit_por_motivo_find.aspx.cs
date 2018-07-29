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
  public partial class app_rep_lit_por_motivo_find : System.Web.UI.Page
  {
    private OnlineServices.Method.Web oWeb = new OnlineServices.Method.Web();
    private OnlineServices.Method.Usuario oIsUsuario;
    protected void Page_Load(object sender, EventArgs e)
    {
      oIsUsuario = oWeb.ValidaUserAppReport();
      oIsUsuario = oWeb.GetObjUsuario();

      DateTime dTimeNow = DateTime.Now;
      getMenu(idReportePago, oIsUsuario.CodUsuario, "1");
      getMenu(idProcesoSeguimiento, oIsUsuario.CodUsuario, "2");
      getMenu(idCartolas, oIsUsuario.CodUsuario, "3");
      getMenu(idProcesoNormalizacion, oIsUsuario.CodUsuario, "4");
      getMenu(idIndicadoresClaves, oIsUsuario.CodUsuario, "5");
      getMenu(IndClasificacionRiesgo, oIsUsuario.CodUsuario, "6");
      getMenuAntalis(indAntalis, oIsUsuario.CodUsuario);

      if (!IsPostBack)
      {
        RadDatePicker1.DateInput.DateFormat = "dd-MM-yyyy";
        RadDatePicker2.DateInput.DateFormat = "dd-MM-yyyy";
        RadDatePicker1.SelectedDate = dTimeNow.AddMonths(-1);
        RadDatePicker2.SelectedDate = dTimeNow;

        Log oLog = new Log();
        oLog.IdUsuario = oIsUsuario.CodUsuario;
        oLog.ObsLog = "REPORTE DE LITIGIOS POR MOTIVO";
        oLog.CodEvtLog = "1";
        oLog.AppLog = "REPORTES DEBTCONTROL";
        oLog.putLog();
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
      getDatatble();
    }

    protected void rdGridReporteLitigiosMotivo_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
      rdGridReporteLitigiosMotivo.DataSource = getDatatble();
    }

    protected void rdGridReporteLitigiosMotivo_ItemCommand(object source, GridCommandEventArgs e)
    {
      if (e.CommandName == RadGrid.ExportToExcelCommandName)
      {
        DataTable dt = getDatatble();

        XLWorkbook wb = new XLWorkbook();
        wb.Worksheets.Add(dt, "litigiomotivo");

        Response.Clear();
        Response.Buffer = true;
        Response.Charset = "";
        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        Response.AddHeader("content-disposition", "attachment;filename=litigiomotivo_" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx");
        using (MemoryStream MyMemoryStream = new MemoryStream())
        {
          wb.SaveAs(MyMemoryStream);
          MyMemoryStream.WriteTo(Response.OutputStream);
          Response.Flush();
          Response.End();
        }
      }
    }

    protected void rdGridReporteLitigiosMotivo_ItemDataBound(object sender, GridItemEventArgs e)
    {

    }

    public DataTable getDatatble()
    {
      DataTable dt = null;

      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        cReporteLitigiosMotivos oReporteLitigiosMotivos = new cReporteLitigiosMotivos(ref oConn);
        oReporteLitigiosMotivos.CodDeudor = hddCodDeudor.Value;
        oReporteLitigiosMotivos.CodNkey = oIsUsuario.CodNkey;
        oReporteLitigiosMotivos.NkeyUsuario = oIsUsuario.NKeyUsuario;
        oReporteLitigiosMotivos.TipoUsuario = oIsUsuario.TipoUsuario;
        oReporteLitigiosMotivos.DtFchIni = DateTime.Parse(RadDatePicker1.SelectedDate.ToString()).ToString("yyyyMMdd");
        oReporteLitigiosMotivos.DtFchFin = DateTime.Parse(RadDatePicker2.SelectedDate.ToString()).ToString("yyyyMMdd");
        dt = oReporteLitigiosMotivos.Get();

      }
      oConn.Close();

      return dt;
    }
  }
}