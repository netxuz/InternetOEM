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
      if (!IsPostBack)
      {
        txt_ano.Text = DateTime.Now.Year.ToString();

        if (oConn.Open())
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
              cmbCliente.Items.Add(new ListItem("<< Todos los Cliente >>", string.Empty));
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
          oDebtUsrAsignados.CodConsulta = "12";
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
          oConn.Close();
        }


        Log oLog = new Log();
        oLog.IdUsuario = oIsUsuario.CodUsuario;
        oLog.ObsLog = "REPORTE DE INDICADORES DE EFECTIVIDAD";
        oLog.CodEvtLog = "1";
        oLog.AppLog = "REPORTES DEBTCONTROL";
        oLog.putLog();
      }

      if (oConn.Open())
      {
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

      if (chkDetalleMes.Checked)
      {
        Page.ClientScript.RegisterStartupScript(this.GetType(), "onLoadDetalle", "loadDetalleMes();", true);
      }

      if (chkIndicadoresTop.Checked)
      {
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
        oIndicadoresEfectividad.CodNkey = ((!string.IsNullOrEmpty(cmbCliente.SelectedValue) ? cmbCliente.SelectedValue : hdd_arrNkeyCliente.Value));
        oIndicadoresEfectividad.NcodHolding = cmbHolding.SelectedValue;
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
              if (!string.IsNullOrEmpty(oRow["Vencido"].ToString()) && !string.IsNullOrEmpty(oRow["NC"].ToString())) { 
                if ((double.Parse(oRow["Facturado"].ToString()) - double.Parse(oRow["NC"].ToString())) == 0)
                {
                  VencidoFacturado = 0;
                }
                else
                {
                  VencidoFacturado = (double.Parse(oRow["Vencido"].ToString()) / (double.Parse(oRow["Facturado"].ToString()) - double.Parse(oRow["NC"].ToString())) * 100);
                }
              }else
                VencidoFacturado = 0;
            }
            ocIndicadorDeEfectividad.VencidoFacturado = VencidoFacturado.ToString("#,##0.00");
            ocIndicadorDeEfectividad.Cobrado = (!string.IsNullOrEmpty(oRow["Vencido"].ToString()) ? double.Parse(oRow["Vencido"].ToString()).ToString("#,##0.00") : "0");
            ocIndicadorDeEfectividad.Deuda = (!string.IsNullOrEmpty(oRow["Deuda"].ToString()) ? double.Parse(oRow["Deuda"].ToString()).ToString("#,##0.00") : "0") ;
            ocIndicadorDeEfectividad.Facturado = (!string.IsNullOrEmpty(oRow["Facturado"].ToString()) ? double.Parse(oRow["Facturado"].ToString()).ToString("#,##0.00") : "0");
            ocIndicadorDeEfectividad.NC = (!string.IsNullOrEmpty(oRow["NC"].ToString()) ? double.Parse(oRow["NC"].ToString()).ToString("#,##0.00") : "0") ;
            ocIndicadorDeEfectividad.FP = (!string.IsNullOrEmpty(oRow["FP"].ToString()) ? double.Parse(oRow["FP"].ToString()).ToString("#,##0.00") : "0") ;
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

    protected void rdGridIndicadoresEfectividad_ItemDataBound(object sender, GridItemEventArgs e)
    {
      if (e.Item is GridDataItem)
      {
        GridDataItem item = (GridDataItem)e.Item;
        DataRowView row = (DataRowView)e.Item.DataItem;

        if (!string.IsNullOrEmpty(Decimales))
        {
          if (int.Parse(Decimales) > 0)
          {
            if ((!string.IsNullOrEmpty(item["DSO"].Text)) && (item["DSO"].Text != "&nbsp;"))
              item["DSO"].Text = double.Parse(row["DSO"].ToString()).ToString("N" + Decimales);

            if ((!string.IsNullOrEmpty(item["Cobrado"].Text)) && (item["Cobrado"].Text != "&nbsp;"))
              item["Cobrado"].Text = double.Parse(row["Cobrado"].ToString()).ToString("N" + Decimales);

            if ((!string.IsNullOrEmpty(item["Deuda"].Text)) && (item["Deuda"].Text != "&nbsp;"))
              item["Deuda"].Text = double.Parse(row["Deuda"].ToString()).ToString("N" + Decimales);

            if ((!string.IsNullOrEmpty(item["Vencido"].Text)) && (item["Vencido"].Text != "&nbsp;"))
              item["Vencido"].Text = double.Parse(row["Vencido"].ToString()).ToString("N" + Decimales);

            if ((!string.IsNullOrEmpty(item["Facturado"].Text)) && (item["Facturado"].Text != "&nbsp;"))
              item["Facturado"].Text = double.Parse(row["Facturado"].ToString()).ToString("N" + Decimales);

            if ((!string.IsNullOrEmpty(item["NC"].Text)) && (item["NC"].Text != "&nbsp;"))
              item["NC"].Text = double.Parse(row["NC"].ToString()).ToString("N" + Decimales);

            if ((!string.IsNullOrEmpty(item["FP"].Text)) && (item["FP"].Text != "&nbsp;"))
              item["FP"].Text = double.Parse(row["FP"].ToString()).ToString("N" + Decimales);

            if ((!string.IsNullOrEmpty(item["Ficticio"].Text)) && (item["Ficticio"].Text != "&nbsp;"))
              item["Ficticio"].Text = double.Parse(row["Ficticio"].ToString()).ToString("N" + Decimales);
          }
          else
          {
            if ((!string.IsNullOrEmpty(item["DSO"].Text)) && (item["DSO"].Text != "&nbsp;"))
              item["DSO"].Text = double.Parse(row["DSO"].ToString()).ToString("N0");

            if ((!string.IsNullOrEmpty(item["Cobrado"].Text)) && (item["Cobrado"].Text != "&nbsp;"))
              item["Cobrado"].Text = double.Parse(row["Cobrado"].ToString()).ToString("N0");

            if ((!string.IsNullOrEmpty(item["Deuda"].Text)) && (item["Deuda"].Text != "&nbsp;"))
              item["Deuda"].Text = double.Parse(row["Deuda"].ToString()).ToString("N0");

            if ((!string.IsNullOrEmpty(item["Vencido"].Text)) && (item["Vencido"].Text != "&nbsp;"))
              item["Vencido"].Text = double.Parse(row["Vencido"].ToString()).ToString("N0");

            if ((!string.IsNullOrEmpty(item["Facturado"].Text)) && (item["Facturado"].Text != "&nbsp;"))
              item["Facturado"].Text = double.Parse(row["Facturado"].ToString()).ToString("N0");

            if ((!string.IsNullOrEmpty(item["NC"].Text)) && (item["NC"].Text != "&nbsp;"))
              item["NC"].Text = double.Parse(row["NC"].ToString()).ToString("N0");

            if ((!string.IsNullOrEmpty(item["FP"].Text)) && (item["FP"].Text != "&nbsp;"))
              item["FP"].Text = double.Parse(row["FP"].ToString()).ToString("N0");

            if ((!string.IsNullOrEmpty(item["Ficticio"].Text)) && (item["Ficticio"].Text != "&nbsp;"))
              item["Ficticio"].Text = double.Parse(row["Ficticio"].ToString()).ToString("N0");
          }
        }
        else
        {
          if ((!string.IsNullOrEmpty(item["DSO"].Text)) && (item["DSO"].Text != "&nbsp;"))
            item["DSO"].Text = double.Parse(row["DSO"].ToString()).ToString("N0");

          if ((!string.IsNullOrEmpty(item["Cobrado"].Text)) && (item["Cobrado"].Text != "&nbsp;"))
            item["Cobrado"].Text = double.Parse(row["Cobrado"].ToString()).ToString("N0");

          if ((!string.IsNullOrEmpty(item["Deuda"].Text)) && (item["Deuda"].Text != "&nbsp;"))
            item["Deuda"].Text = double.Parse(row["Deuda"].ToString()).ToString("N0");

          if ((!string.IsNullOrEmpty(item["Vencido"].Text)) && (item["Vencido"].Text != "&nbsp;"))
            item["Vencido"].Text = double.Parse(row["Vencido"].ToString()).ToString("N0");

          if ((!string.IsNullOrEmpty(item["Facturado"].Text)) && (item["Facturado"].Text != "&nbsp;"))
            item["Facturado"].Text = double.Parse(row["Facturado"].ToString()).ToString("N0");

          if ((!string.IsNullOrEmpty(item["NC"].Text)) && (item["NC"].Text != "&nbsp;"))
            item["NC"].Text = double.Parse(row["NC"].ToString()).ToString("N0");

          if ((!string.IsNullOrEmpty(item["FP"].Text)) && (item["FP"].Text != "&nbsp;"))
            item["FP"].Text = double.Parse(row["FP"].ToString()).ToString("N0");

          if ((!string.IsNullOrEmpty(item["Ficticio"].Text)) && (item["Ficticio"].Text != "&nbsp;"))
            item["Ficticio"].Text = double.Parse(row["Ficticio"].ToString()).ToString("N0");
        }
      }
    }

    protected void bnt_logout_Click(object sender, EventArgs e)
    {
      Session["USUARIO"] = string.Empty;
      Session["CodUsuarioPerfil"] = string.Empty;
      Response.Redirect("/");
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