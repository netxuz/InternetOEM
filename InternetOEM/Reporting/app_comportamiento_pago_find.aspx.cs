using System;
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
  public partial class app_comportamiento_pago_find : System.Web.UI.Page
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

      if (!IsPostBack) {
        Log oLog = new Log();
        oLog.IdUsuario = oIsUsuario.CodUsuario;
        oLog.ObsLog = "REPORTE COMPORTAMIENTO DE PAGO";
        oLog.CodEvtLog = "1";
        oLog.AppLog = "REPORTES DEBTCONTROL";
        oLog.putLog();
      }
      
      //if (idGrilla.Visible)
      //  getGrid();
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
          }
        }
        dtPerfil = null;
      }
      oConn.Close();

      oHtmControl.Controls.Add(new LiteralControl("<li><a href='../antalis/reportevalijas.aspx'>Valijas Validadas</a></li>"));
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
      rdGridComportamientoPago.Rebind();
    }

    protected void getGrid()
    {
      DataTable dtcompPago = null;
      DataTable dt;
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        cComportamientoPago oComportamientoPago = new cComportamientoPago(ref oConn);
        oComportamientoPago.CodDeudor = hddCodDeudor.Value;
        oComportamientoPago.CodNkey = oIsUsuario.CodNkey;
        oComportamientoPago.NkeyUsuario = oIsUsuario.NKeyUsuario;
        oComportamientoPago.TipoUsuario = oIsUsuario.TipoUsuario;
        oComportamientoPago.CodAnalista = hddCodAnalista.Value;
        oComportamientoPago.Estado = rdBtnTypeQuery.SelectedValue;
        dtcompPago = oComportamientoPago.Get();
      }
      oConn.Close();

      GridBoundColumn oGridBoundColumn;

      dt = getDatatble(dtcompPago);
      if (dtcompPago != null)
      {
        if (dtcompPago.Rows.Count > 0)
        {
          rdGridComportamientoPago.MasterTableView.Columns.Clear();

          oGridBoundColumn = new GridBoundColumn();
          oGridBoundColumn.DataField = "mesfila";
          oGridBoundColumn.HeaderText = "mesfila";
          oGridBoundColumn.UniqueName = "mesfila";
          rdGridComportamientoPago.MasterTableView.Columns.Add(oGridBoundColumn);

          oGridBoundColumn = new GridBoundColumn();
          oGridBoundColumn.DataField = dtcompPago.Rows[0]["mesano"].ToString();
          oGridBoundColumn.UniqueName = "Actual";
          oGridBoundColumn.DataFormatString = "{0:n}";
          oGridBoundColumn.HeaderText = dtcompPago.Rows[0]["mesano"].ToString();
          rdGridComportamientoPago.MasterTableView.Columns.Add(oGridBoundColumn);

          oGridBoundColumn = new GridBoundColumn();
          oGridBoundColumn.DataField = dtcompPago.Rows[0]["mesano1"].ToString();
          oGridBoundColumn.UniqueName = "Mes-1";
          oGridBoundColumn.DataFormatString = "{0:n}";
          oGridBoundColumn.HeaderText = dtcompPago.Rows[0]["mesano1"].ToString();
          rdGridComportamientoPago.MasterTableView.Columns.Add(oGridBoundColumn);

          oGridBoundColumn = new GridBoundColumn();
          oGridBoundColumn.DataField = dtcompPago.Rows[0]["mesano2"].ToString();
          oGridBoundColumn.UniqueName = "Mes-2";
          oGridBoundColumn.DataFormatString = "{0:n}";
          oGridBoundColumn.HeaderText = dtcompPago.Rows[0]["mesano2"].ToString();
          rdGridComportamientoPago.MasterTableView.Columns.Add(oGridBoundColumn);

          oGridBoundColumn = new GridBoundColumn();
          oGridBoundColumn.DataField = dtcompPago.Rows[0]["mesano3"].ToString();
          oGridBoundColumn.UniqueName = "Mes-3";
          oGridBoundColumn.DataFormatString = "{0:n}";
          oGridBoundColumn.HeaderText = dtcompPago.Rows[0]["mesano3"].ToString();
          rdGridComportamientoPago.MasterTableView.Columns.Add(oGridBoundColumn);

          oGridBoundColumn = new GridBoundColumn();
          oGridBoundColumn.DataField = dtcompPago.Rows[0]["mesano4"].ToString();
          oGridBoundColumn.UniqueName = "Mes-4";
          oGridBoundColumn.DataFormatString = "{0:n}";
          oGridBoundColumn.HeaderText = dtcompPago.Rows[0]["mesano4"].ToString();
          rdGridComportamientoPago.MasterTableView.Columns.Add(oGridBoundColumn);

          oGridBoundColumn = new GridBoundColumn();
          oGridBoundColumn.DataField = dtcompPago.Rows[0]["mesano5"].ToString();
          oGridBoundColumn.UniqueName = "Mes-5";
          oGridBoundColumn.DataFormatString = "{0:n}";
          oGridBoundColumn.HeaderText = dtcompPago.Rows[0]["mesano5"].ToString();
          rdGridComportamientoPago.MasterTableView.Columns.Add(oGridBoundColumn);

          oGridBoundColumn = new GridBoundColumn();
          oGridBoundColumn.DataField = dtcompPago.Rows[0]["mesano6"].ToString();
          oGridBoundColumn.UniqueName = "Mes-6";
          oGridBoundColumn.DataFormatString = "{0:n}";
          oGridBoundColumn.HeaderText = dtcompPago.Rows[0]["mesano6"].ToString();
          rdGridComportamientoPago.MasterTableView.Columns.Add(oGridBoundColumn);

          oGridBoundColumn = new GridBoundColumn();
          oGridBoundColumn.DataField = dtcompPago.Rows[0]["mesano7"].ToString();
          oGridBoundColumn.UniqueName = "Mes-7";
          oGridBoundColumn.DataFormatString = "{0:n}";
          oGridBoundColumn.HeaderText = dtcompPago.Rows[0]["mesano7"].ToString();
          rdGridComportamientoPago.MasterTableView.Columns.Add(oGridBoundColumn);

          oGridBoundColumn = new GridBoundColumn();
          oGridBoundColumn.DataField = dtcompPago.Rows[0]["mesano8"].ToString();
          oGridBoundColumn.UniqueName = "Mes-8";
          oGridBoundColumn.DataFormatString = "{0:n}";
          oGridBoundColumn.HeaderText = dtcompPago.Rows[0]["mesano8"].ToString();
          rdGridComportamientoPago.MasterTableView.Columns.Add(oGridBoundColumn);

          oGridBoundColumn = new GridBoundColumn();
          oGridBoundColumn.DataField = dtcompPago.Rows[0]["mesano9"].ToString();
          oGridBoundColumn.UniqueName = "Mes-9";
          oGridBoundColumn.DataFormatString = "{0:n}";
          oGridBoundColumn.HeaderText = dtcompPago.Rows[0]["mesano9"].ToString();
          rdGridComportamientoPago.MasterTableView.Columns.Add(oGridBoundColumn);
        }
      }
      dtcompPago = null;

      rdGridComportamientoPago.DataSource = dt;


    }

    public DataTable getDatatble(DataTable dt)
    {
      DataTable dtabla = null;

      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        if (dt != null)
        {
          if (dt.Rows.Count > 0)
          {
            cComportamientoDePago oComportamientoDePago = new cComportamientoDePago();
            oComportamientoDePago.Tabla = dt;
            oComportamientoDePago.setTable();

            foreach (DataRow oRow in dt.Rows)
            {
              oComportamientoDePago.MesFila = oRow["mesfila"].ToString();

              oComportamientoDePago.MesAno = ((!string.IsNullOrEmpty(oRow["actual"].ToString())) && (double.Parse(oRow["actual"].ToString()) > 0) ? ((double.Parse(oRow["actual"].ToString()) / double.Parse(dt.Compute("SUM(actual)", "").ToString())) * 100) : 1);
              oComportamientoDePago.MesAno1 = ((!string.IsNullOrEmpty(oRow["mes-1"].ToString())) && (double.Parse(oRow["mes-1"].ToString()) > 0) ? ((double.Parse(oRow["mes-1"].ToString()) / double.Parse(dt.Compute("SUM([mes-1])", "").ToString())) * 100) : 1);
              oComportamientoDePago.MesAno2 = ((!string.IsNullOrEmpty(oRow["mes-2"].ToString())) && (double.Parse(oRow["mes-2"].ToString()) > 0) ? ((double.Parse(oRow["mes-2"].ToString()) / double.Parse(dt.Compute("SUM([mes-2])", "").ToString())) * 100) : 1);
              oComportamientoDePago.MesAno3 = ((!string.IsNullOrEmpty(oRow["mes-3"].ToString())) && (double.Parse(oRow["mes-3"].ToString()) > 0) ? ((double.Parse(oRow["mes-3"].ToString()) / double.Parse(dt.Compute("SUM([mes-3])", "").ToString())) * 100) : 1);
              oComportamientoDePago.MesAno4 = ((!string.IsNullOrEmpty(oRow["mes-4"].ToString())) && (double.Parse(oRow["mes-4"].ToString()) > 0) ? ((double.Parse(oRow["mes-4"].ToString()) / double.Parse(dt.Compute("SUM([mes-4])", "").ToString())) * 100) : 1);
              oComportamientoDePago.MesAno5 = ((!string.IsNullOrEmpty(oRow["mes-5"].ToString())) && (double.Parse(oRow["mes-5"].ToString()) > 0) ? ((double.Parse(oRow["mes-5"].ToString()) / double.Parse(dt.Compute("SUM([mes-5])", "").ToString())) * 100) : 1);
              oComportamientoDePago.MesAno6 = ((!string.IsNullOrEmpty(oRow["mes-6"].ToString())) && (double.Parse(oRow["mes-6"].ToString()) > 0) ? ((double.Parse(oRow["mes-6"].ToString()) / double.Parse(dt.Compute("SUM([mes-6])", "").ToString())) * 100) : 1);
              oComportamientoDePago.MesAno7 = ((!string.IsNullOrEmpty(oRow["mes-7"].ToString())) && (double.Parse(oRow["mes-7"].ToString()) > 0) ? ((double.Parse(oRow["mes-7"].ToString()) / double.Parse(dt.Compute("SUM([mes-7])", "").ToString())) * 100) : 1);
              oComportamientoDePago.MesAno8 = ((!string.IsNullOrEmpty(oRow["mes-8"].ToString())) && (double.Parse(oRow["mes-8"].ToString()) > 0) ? ((double.Parse(oRow["mes-8"].ToString()) / double.Parse(dt.Compute("SUM([mes-8])", "").ToString())) * 100) : 1);
              oComportamientoDePago.MesAno9 = ((!string.IsNullOrEmpty(oRow["mes-9"].ToString())) && (double.Parse(oRow["mes-9"].ToString()) > 0) ? ((double.Parse(oRow["mes-9"].ToString()) / double.Parse(dt.Compute("SUM([mes-9])", "").ToString())) * 100) : 1);
              oComportamientoDePago.AddRow();

            }
            dtabla = oComportamientoDePago.Get();
          }
        }
        dt = null;

      }
      oConn.Close();

      return dtabla;
    }

    protected void rdGridComportamientoPago_ItemCommand1(object source, GridCommandEventArgs e)
    {
      if (e.CommandName == RadGrid.ExportToExcelCommandName)
      {
        DataTable dtcompPago = null;
        DataTable dt = null;

        DBConn oConn = new DBConn();
        if (oConn.Open())
        {
          cComportamientoPago oComportamientoPago = new cComportamientoPago(ref oConn);
          oComportamientoPago.CodDeudor = hddCodDeudor.Value;
          oComportamientoPago.CodNkey = oIsUsuario.CodNkey;
          oComportamientoPago.NkeyUsuario = oIsUsuario.NKeyUsuario;
          oComportamientoPago.TipoUsuario = oIsUsuario.TipoUsuario;
          oComportamientoPago.CodAnalista = hddCodAnalista.Value;
          oComportamientoPago.Estado = rdBtnTypeQuery.SelectedValue;
          dtcompPago = oComportamientoPago.Get();
        }
        oConn.Close();

        dt = getDatatble(dtcompPago);

        XLWorkbook wb = new XLWorkbook();
        wb.Worksheets.Add(dt, "comportamientopago");

        Response.Clear();
        Response.Buffer = true;
        Response.Charset = "";
        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        Response.AddHeader("content-disposition", "attachment;filename=comportamientopago_" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx");
        using (MemoryStream MyMemoryStream = new MemoryStream())
        {
          wb.SaveAs(MyMemoryStream);
          MyMemoryStream.WriteTo(Response.OutputStream);
          Response.Flush();
          Response.End();
        }
      }
    }

    protected void rdGridComportamientoPago_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
      getGrid();
    }
  }

  public class cComportamientoDePago
  {
    private DataTable table = new DataTable("comportamientopago");

    private string pMesFila;
    public string MesFila { get { return pMesFila; } set { pMesFila = value; } }

    private double pMesAno;
    public double MesAno { get { return pMesAno; } set { pMesAno = value; } }

    private double pMesAno1;
    public double MesAno1 { get { return pMesAno1; } set { pMesAno1 = value; } }

    private double pMesAno2;
    public double MesAno2 { get { return pMesAno2; } set { pMesAno2 = value; } }

    private double pMesAno3;
    public double MesAno3 { get { return pMesAno3; } set { pMesAno3 = value; } }

    private double pMesAno4;
    public double MesAno4 { get { return pMesAno4; } set { pMesAno4 = value; } }

    private double pMesAno5;
    public double MesAno5 { get { return pMesAno5; } set { pMesAno5 = value; } }

    private double pMesAno6;
    public double MesAno6 { get { return pMesAno6; } set { pMesAno6 = value; } }

    private double pMesAno7;
    public double MesAno7 { get { return pMesAno7; } set { pMesAno7 = value; } }

    private double pMesAno8;
    public double MesAno8 { get { return pMesAno8; } set { pMesAno8 = value; } }

    private double pMesAno9;
    public double MesAno9 { get { return pMesAno9; } set { pMesAno9 = value; } }

    private DataTable dTabla;
    public DataTable Tabla { get { return dTabla; } set { dTabla = value; } }

    public cComportamientoDePago()
    {

    }

    public void setTable()
    {
      table.Columns.Add(new DataColumn("mesfila", typeof(string)));

      if (dTabla != null)
      {
        if (dTabla.Rows.Count > 0)
        {
          table.Columns.Add(new DataColumn(dTabla.Rows[0]["mesano"].ToString(), typeof(string)));
          table.Columns.Add(new DataColumn(dTabla.Rows[0]["mesano1"].ToString(), typeof(double)));
          table.Columns.Add(new DataColumn(dTabla.Rows[0]["mesano2"].ToString(), typeof(double)));
          table.Columns.Add(new DataColumn(dTabla.Rows[0]["mesano3"].ToString(), typeof(double)));
          table.Columns.Add(new DataColumn(dTabla.Rows[0]["mesano4"].ToString(), typeof(double)));
          table.Columns.Add(new DataColumn(dTabla.Rows[0]["mesano5"].ToString(), typeof(double)));
          table.Columns.Add(new DataColumn(dTabla.Rows[0]["mesano6"].ToString(), typeof(double)));
          table.Columns.Add(new DataColumn(dTabla.Rows[0]["mesano7"].ToString(), typeof(double)));
          table.Columns.Add(new DataColumn(dTabla.Rows[0]["mesano8"].ToString(), typeof(double)));
          table.Columns.Add(new DataColumn(dTabla.Rows[0]["mesano9"].ToString(), typeof(double)));
        }
      }
      dTabla = null;

    }

    public void AddRow()
    {
      table.Rows.Add(pMesFila, pMesAno, pMesAno1, pMesAno2, pMesAno3, pMesAno4, pMesAno5, pMesAno6, pMesAno7, pMesAno8, pMesAno9);
    }

    public DataTable Get()
    {
      return table;
    }

  }
}