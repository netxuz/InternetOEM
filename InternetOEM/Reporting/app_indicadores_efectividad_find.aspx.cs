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
  public partial class app_indicadores_efectividad_find : System.Web.UI.Page
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
        txt_ano.Text = DateTime.Now.Year.ToString();

        Log oLog = new Log();
        oLog.IdUsuario = oIsUsuario.CodUsuario;
        oLog.ObsLog = "REPORTE DE INDICADORES DE EFECTIVIDAD";
        oLog.CodEvtLog = "1";
        oLog.AppLog = "REPORTES DEBTCONTROL";
        oLog.putLog();
      }

      if (chkDetalleMes.Checked) {
        Page.ClientScript.RegisterStartupScript(this.GetType(), "onLoadDetalle", "loadDetalleMes();", true);
      }

      if (chkIndicadoresTop.Checked) {
        Page.ClientScript.RegisterStartupScript(this.GetType(), "onLoadIndicadores", "loadIndicadores();", true);
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
      rdGridIndicadoresEfectividad.Rebind();
    }

    protected void rdGridIndicadoresEfectividad_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
      rdGridIndicadoresEfectividad.DataSource = getDatatble();
    }

    protected void rdGridIndicadoresEfectividad_ItemCommand(object source, GridCommandEventArgs e)
    {
      if (e.CommandName == RadGrid.ExportToExcelCommandName)
      {
        DataTable dt = getDatatble();

        XLWorkbook wb = new XLWorkbook();
        wb.Worksheets.Add(dt, "indicadoresefectividad");

        Response.Clear();
        Response.Buffer = true;
        Response.Charset = "";
        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        Response.AddHeader("content-disposition", "attachment;filename=indicadoresefectividad_" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx");
        using (MemoryStream MyMemoryStream = new MemoryStream())
        {
          wb.SaveAs(MyMemoryStream);
          MyMemoryStream.WriteTo(Response.OutputStream);
          Response.Flush();
          Response.End();
        }
      }
    }

    public DataTable getDatatble()
    {
      DataTable dt = null;

      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        cIndicadorDeEfectividad ocIndicadorDeEfectividad = new cIndicadorDeEfectividad();
        ocIndicadorDeEfectividad.TypeQuery = rdBtnTypeQuery.SelectedValue;
        ocIndicadorDeEfectividad.setTable();

        cIndicadoresEfectividad oIndicadoresEfectividad = new cIndicadoresEfectividad(ref oConn);
        oIndicadoresEfectividad.CodDeudor = hddCodDeudor.Value;
        oIndicadoresEfectividad.CodNkey = oIsUsuario.CodNkey;
        oIndicadoresEfectividad.Estado = (!chkDetalleMes.Checked ? rdBtnTypeQuery.SelectedValue : rdBtnTypeQuery2.SelectedValue);
        oIndicadoresEfectividad.MesIni = rdCmbMes.SelectedValue;
        oIndicadoresEfectividad.AnoIni = txt_ano.Text;
        oIndicadoresEfectividad.Criterio = rdBtnCriterio.SelectedValue;
        oIndicadoresEfectividad.CodRubro = hddCodRubro.Value;
        DataTable dtIdentEfect = oIndicadoresEfectividad.Get();

        if (dtIdentEfect != null)
        {
          foreach (DataRow oRow in dtIdentEfect.Rows)
          {
            double VencidoFacturado = 0;
            if ((rdBtnTypeQuery.SelectedValue != "6") || (rdBtnTypeQuery.SelectedValue != "7") || (rdBtnTypeQuery.SelectedValue != "8") || (rdBtnTypeQuery.SelectedValue != "9") || (rdBtnTypeQuery.SelectedValue != "10"))
              ocIndicadorDeEfectividad.Periodo = oRow["periodo"].ToString();
            if (rdBtnTypeQuery.SelectedValue == "6")
              ocIndicadorDeEfectividad.NomDeudor = oRow["NomDeudor"].ToString();
            if (rdBtnTypeQuery.SelectedValue == "7")
              ocIndicadorDeEfectividad.Rubro = oRow["Rubro"].ToString();
            if (rdBtnTypeQuery.SelectedValue == "8")
              ocIndicadorDeEfectividad.NomVendedor = oRow["NomVendedor"].ToString();
            if (rdBtnTypeQuery.SelectedValue == "9")
              ocIndicadorDeEfectividad.NomAnalista = oRow["NomAnalista"].ToString();
            if (rdBtnTypeQuery.SelectedValue == "10")
              ocIndicadorDeEfectividad.Canal = oRow["Rubro"].ToString();
            ocIndicadorDeEfectividad.DBTDias = (((string.IsNullOrEmpty(oRow["Atraso"].ToString()) && string.IsNullOrEmpty(oRow["Cobrado"].ToString())) && ((double.Parse(oRow["Atraso"].ToString()) > 0) && (double.Parse(oRow["Cobrado"].ToString())) > 0)) ? (double.Parse(oRow["Atraso"].ToString()) / double.Parse(oRow["Cobrado"].ToString())).ToString() : "0");
            ocIndicadorDeEfectividad.DSO = double.Parse(oRow["DSO"].ToString()).ToString("#,##0.00");
            ocIndicadorDeEfectividad.Overdue = (((string.IsNullOrEmpty(oRow["Vencido"].ToString()) && string.IsNullOrEmpty(oRow["Deuda"].ToString())) && ((double.Parse(oRow["Vencido"].ToString()) > 0) && (double.Parse(oRow["Deuda"].ToString())) > 0)) ? (double.Parse(oRow["Vencido"].ToString()) / double.Parse(oRow["Deuda"].ToString())).ToString("#,##0.00") : "0");
            ocIndicadorDeEfectividad.OverdueCritico = (((string.IsNullOrEmpty(oRow["deuda_mas_30"].ToString()) && string.IsNullOrEmpty(oRow["Deuda"].ToString())) && ((double.Parse(oRow["deuda_mas_30"].ToString()) > 0) && (double.Parse(oRow["Deuda"].ToString())) > 0)) ? (double.Parse(oRow["deuda_mas_30"].ToString()) / double.Parse(oRow["Deuda"].ToString())).ToString("#,##0.00") : "0");

            if ((!string.IsNullOrEmpty(oRow["Vencido"].ToString())) || (double.Parse(oRow["Vencido"].ToString()) == 0) || (!string.IsNullOrEmpty(oRow["Facturado"].ToString())) || (double.Parse(oRow["Facturado"].ToString()) == 0) || (!string.IsNullOrEmpty(oRow["NC"].ToString())) || (double.Parse(oRow["NC"].ToString()) == 0))
              VencidoFacturado = 0;
            else
            {
              if ((double.Parse(oRow["Facturado"].ToString()) - double.Parse(oRow["NC"].ToString())) == 0)
              {
                VencidoFacturado = 0;
              }
              else
              {
                VencidoFacturado = (double.Parse(oRow["Vencido"].ToString()) / (double.Parse(oRow["Facturado"].ToString()) - double.Parse(oRow["NC"].ToString())) * 100);
              }
            }
            ocIndicadorDeEfectividad.VencidoFacturado = VencidoFacturado.ToString("#,##0.00");
            ocIndicadorDeEfectividad.Cobrado = (!string.IsNullOrEmpty(oRow["Vencido"].ToString()) ? double.Parse(oRow["Vencido"].ToString()).ToString("#,##0.00") : "0");
            ocIndicadorDeEfectividad.Deuda = double.Parse(oRow["Deuda"].ToString()).ToString("#,##0.00");
            ocIndicadorDeEfectividad.Facturado = double.Parse(oRow["Facturado"].ToString()).ToString("#,##0.00");
            ocIndicadorDeEfectividad.NC = double.Parse(oRow["NC"].ToString()).ToString("#,##0.00");
            ocIndicadorDeEfectividad.FP = double.Parse(oRow["FP"].ToString()).ToString("#,##0.00");
            ocIndicadorDeEfectividad.Ficticio = ((!string.IsNullOrEmpty(oRow["Ficticio"].ToString()) && double.Parse(oRow["Ficticio"].ToString()) != 0) ? double.Parse(oRow["Ficticio"].ToString()).ToString("#,##0.00") : "0");

            ocIndicadorDeEfectividad.AddRow();
          }
          dt = ocIndicadorDeEfectividad.Get();
        }
        dtIdentEfect = null;

      }
      oConn.Close();

      return dt;
    }

    protected void rdGridIndicadoresEfectividad_PreRender(object source, EventArgs e)
    {
      if ((rdBtnTypeQuery.SelectedValue != "6") && (rdBtnTypeQuery.SelectedValue != "7") && (rdBtnTypeQuery.SelectedValue != "8") && (rdBtnTypeQuery.SelectedValue != "9") && (rdBtnTypeQuery.SelectedValue != "10"))
        rdGridIndicadoresEfectividad.MasterTableView.GetColumn("periodo").Visible = false;
      else
        rdGridIndicadoresEfectividad.MasterTableView.GetColumn("periodo").Visible = true;

      if (rdBtnTypeQuery.SelectedValue == "6")
        rdGridIndicadoresEfectividad.MasterTableView.GetColumn("NomDeudor").Visible = true;
      else
        rdGridIndicadoresEfectividad.MasterTableView.GetColumn("NomDeudor").Visible = false;

      if (rdBtnTypeQuery.SelectedValue == "7")
        rdGridIndicadoresEfectividad.MasterTableView.GetColumn("Rubro").Visible = true;
      else
        rdGridIndicadoresEfectividad.MasterTableView.GetColumn("Rubro").Visible = false;

      if (rdBtnTypeQuery.SelectedValue == "8")
        rdGridIndicadoresEfectividad.MasterTableView.GetColumn("NomVendedor").Visible = true;
      else
        rdGridIndicadoresEfectividad.MasterTableView.GetColumn("NomVendedor").Visible = false;

      if (rdBtnTypeQuery.SelectedValue == "9")
        rdGridIndicadoresEfectividad.MasterTableView.GetColumn("NomAnalista").Visible = true;
      else
        rdGridIndicadoresEfectividad.MasterTableView.GetColumn("NomAnalista").Visible = false;

      if (rdBtnTypeQuery.SelectedValue == "10")
        rdGridIndicadoresEfectividad.MasterTableView.GetColumn("Canal").Visible = true;
      else
        rdGridIndicadoresEfectividad.MasterTableView.GetColumn("Canal").Visible = false;
    }
  }

  public class cIndicadorDeEfectividad
  {
    private DataTable table = new DataTable("IndicadorDeEfectividad");

    private string pPeriodo;
    public string Periodo { get { return pPeriodo; } set { pPeriodo = value; } }

    private string pNomDeudor;
    public string NomDeudor { get { return pNomDeudor; } set { pNomDeudor = value; } }

    private string pRubro;
    public string Rubro { get { return pRubro; } set { pRubro = value; } }

    private string pNomVendedor;
    public string NomVendedor { get { return pNomVendedor; } set { pNomVendedor = value; } }

    private string pNomAnalista;
    public string NomAnalista { get { return pNomAnalista; } set { pNomAnalista = value; } }

    private string pCanal;
    public string Canal { get { return pCanal; } set { pCanal = value; } }

    private string pDBTDias;
    public string DBTDias { get { return pDBTDias; } set { pDBTDias = value; } }

    private string pDSO;
    public string DSO { get { return pDSO; } set { pDSO = value; } }

    private string pOverdue;
    public string Overdue { get { return pOverdue; } set { pOverdue = value; } }

    private string pOverdueCritico;
    public string OverdueCritico { get { return pOverdueCritico; } set { pOverdueCritico = value; } }

    private string pVencidoFacturado;
    public string VencidoFacturado { get { return pVencidoFacturado; } set { pVencidoFacturado = value; } }

    private string pCobrado;
    public string Cobrado { get { return pCobrado; } set { pCobrado = value; } }

    private string pDeuda;
    public string Deuda { get { return pDeuda; } set { pDeuda = value; } }

    private string pVencido;
    public string Vencido { get { return pVencido; } set { pVencido = value; } }

    private string pFacturado;
    public string Facturado { get { return pFacturado; } set { pFacturado = value; } }

    private string pNC;
    public string NC { get { return pNC; } set { pNC = value; } }

    private string pFP;
    public string FP { get { return pFP; } set { pFP = value; } }

    private string pFicticio;
    public string Ficticio { get { return pFicticio; } set { pFicticio = value; } }

    private string pTypeQuery;
    public string TypeQuery { get { return pTypeQuery; } set { pTypeQuery = value; } }

    public cIndicadorDeEfectividad()
    {

    }

    public void setTable()
    {
      if ((pTypeQuery != "6") && (pTypeQuery != "7") && (pTypeQuery != "8") && (pTypeQuery != "9") && (pTypeQuery != "10"))
        table.Columns.Add(new DataColumn("periodo", typeof(string)));

      if (pTypeQuery == "6")
        table.Columns.Add(new DataColumn("NomDeudor", typeof(string)));

      if (pTypeQuery == "7")
        table.Columns.Add(new DataColumn("Rubro", typeof(string)));

      if (pTypeQuery == "8")
        table.Columns.Add(new DataColumn("NomVendedor", typeof(string)));

      if (pTypeQuery == "9")
        table.Columns.Add(new DataColumn("NomAnalista", typeof(string)));

      if (pTypeQuery == "10")
        table.Columns.Add(new DataColumn("Canal", typeof(string)));

      table.Columns.Add(new DataColumn("DBTDias", typeof(string)));
      table.Columns.Add(new DataColumn("DSO", typeof(string)));
      table.Columns.Add(new DataColumn("Overdue", typeof(string)));
      table.Columns.Add(new DataColumn("OverdueCritico", typeof(string)));
      table.Columns.Add(new DataColumn("VencidoFacturado", typeof(string)));
      table.Columns.Add(new DataColumn("Cobrado", typeof(string)));
      table.Columns.Add(new DataColumn("Deuda", typeof(string)));
      table.Columns.Add(new DataColumn("Vencido", typeof(string)));
      table.Columns.Add(new DataColumn("Facturado", typeof(string)));
      table.Columns.Add(new DataColumn("NC", typeof(string)));
      table.Columns.Add(new DataColumn("FP", typeof(string)));
      table.Columns.Add(new DataColumn("Ficticio", typeof(string)));

    }

    public void AddRow()
    {
      if ((pTypeQuery != "6") && (pTypeQuery != "7") && (pTypeQuery != "8") && (pTypeQuery != "9") && (pTypeQuery != "10"))
        table.Rows.Add(pPeriodo, pDBTDias, pDSO, pOverdue, pOverdueCritico, pVencidoFacturado, pCobrado, pDeuda, pVencido, pFacturado, pNC, pFP, pFicticio);

      if (pTypeQuery != "7")
        table.Rows.Add(pNomDeudor, pDBTDias, pDSO, pOverdue, pOverdueCritico, pVencidoFacturado, pCobrado, pDeuda, pVencido, pFacturado, pNC, pFP, pFicticio);

      if (pTypeQuery != "8")
        table.Rows.Add(pRubro, pDBTDias, pDSO, pOverdue, pOverdueCritico, pVencidoFacturado, pCobrado, pDeuda, pVencido, pFacturado, pNC, pFP, pFicticio);

      if (pTypeQuery != "9")
        table.Rows.Add(pNomVendedor, pDBTDias, pDSO, pOverdue, pOverdueCritico, pVencidoFacturado, pCobrado, pDeuda, pVencido, pFacturado, pNC, pFP, pFicticio);

      if (pTypeQuery != "10")
        table.Rows.Add(pCanal, pDBTDias, pDSO, pOverdue, pOverdueCritico, pVencidoFacturado, pCobrado, pDeuda, pVencido, pFacturado, pNC, pFP, pFicticio);

    }

    public DataTable Get()
    {
      return table;
    }

  }

}