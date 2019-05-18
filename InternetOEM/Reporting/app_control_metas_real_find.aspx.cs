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
  public partial class app_control_metas_real_find : System.Web.UI.Page
  {
    private OnlineServices.Method.Web oWeb = new OnlineServices.Method.Web();
    private OnlineServices.Method.Usuario oIsUsuario;
    bool bCliente = false;
    bool bDeudor;
    bool bHolding;
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
        DBConn oConn = new DBConn();
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
          oDebtUsrAsignados.CodConsulta = "13";
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

          //if (bDeudor)
          //  colDeudor.Visible = true;

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


        RadDatePicker1.DateInput.DateFormat = "dd-MM-yyyy";
        RadDatePicker1.SelectedDate = dTimeNow.AddMonths(-1);

        Log oLog = new Log();
        oLog.IdUsuario = oIsUsuario.CodUsuario;
        oLog.ObsLog = "REPORTE DE CONTROL METAS REAL";
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
      rdGridControlMetasvsReal.Rebind();
    }

    protected void rdGridControlMetasvsReal_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
      rdGridControlMetasvsReal.DataSource = getDatatble();
    }

    protected void rdGridControlMetasvsReal_ItemCommand(object source, GridCommandEventArgs e)
    {
      if (e.CommandName == RadGrid.ExportToExcelCommandName)
      {
        DataTable dt = getDatatble();

        XLWorkbook wb = new XLWorkbook();
        wb.Worksheets.Add(dt, "controlmetasvsreal");

        Response.Clear();
        Response.Buffer = true;
        Response.Charset = "";
        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        Response.AddHeader("content-disposition", "attachment;filename=controlmetasvsreal_" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx");
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
        cControlMetasvsReal oControlMetasvsReal = new cControlMetasvsReal(ref oConn);
        oControlMetasvsReal.CodDeudor = hddCodDeudor.Value;
        oControlMetasvsReal.CodNkey = ((!string.IsNullOrEmpty(cmbCliente.SelectedValue) ? cmbCliente.SelectedValue : hdd_arrNkeyCliente.Value));
        oControlMetasvsReal.NcodHolding = cmbHolding.SelectedValue;
        oControlMetasvsReal.NkeyUsuario = oIsUsuario.NKeyUsuario;
        oControlMetasvsReal.TipoUsuario = oIsUsuario.TipoUsuario;
        oControlMetasvsReal.DtFchIni = DateTime.Parse(RadDatePicker1.SelectedDate.ToString()).ToString("yyyyMMdd");
        oControlMetasvsReal.Estado = rdBtnTypeQuery.SelectedValue;
        dt = oControlMetasvsReal.Get();

      }
      oConn.Close();

      return dt;
    }

    protected void rdGridControlMetasvsReal_PreRender(object source, EventArgs e)
    {
      if (rdBtnTypeQuery.SelectedValue != "2")
      {
        rdGridControlMetasvsReal.MasterTableView.GetColumn("TemplateColumn1").Visible = true;
        rdGridControlMetasvsReal.MasterTableView.GetColumn("TemplateColumn2").Visible = true;
        rdGridControlMetasvsReal.MasterTableView.GetColumn("TemplateColumn3").Visible = true;
        rdGridControlMetasvsReal.MasterTableView.GetColumn("TemplateColumn4").Visible = true;
        rdGridControlMetasvsReal.MasterTableView.GetColumn("TemplateColumn5").Visible = true;
        rdGridControlMetasvsReal.MasterTableView.GetColumn("TemplateColumn6").Visible = true;
        rdGridControlMetasvsReal.MasterTableView.GetColumn("Real").Visible = false;
        rdGridControlMetasvsReal.MasterTableView.GetColumn("Estimado").Visible = false;
        rdGridControlMetasvsReal.MasterTableView.GetColumn("Diferencia").Visible = false;
        rdGridControlMetasvsReal.MasterTableView.GetColumn("SDeudor").Visible = false;
        rdGridControlMetasvsReal.MasterTableView.GetColumn("SAnalista").Visible = false;
        rdGridControlMetasvsReal.MasterTableView.GetColumn("EtapaCob").Visible = false;
        rdGridControlMetasvsReal.MasterTableView.GetColumn("comentario").Visible = false;

        DataTable dt = null;
        DBConn oConn = new DBConn();
        if (oConn.Open())
        {
          cControlMetasvsReal oControlMetasvsReal = new cControlMetasvsReal(ref oConn);
          oControlMetasvsReal.CodDeudor = hddCodDeudor.Value;
          oControlMetasvsReal.CodNkey = oIsUsuario.CodNkey;
          oControlMetasvsReal.NkeyUsuario = oIsUsuario.NKeyUsuario;
          oControlMetasvsReal.TipoUsuario = oIsUsuario.TipoUsuario;
          oControlMetasvsReal.DtFchIni = DateTime.Parse(RadDatePicker1.SelectedDate.ToString()).ToString("yyyyMMdd");
          oControlMetasvsReal.Estado = rdBtnTypeQuery.SelectedValue;
          dt = oControlMetasvsReal.Get();

          if (dt != null)
          {
            if (dt.Rows.Count > 0)
            {
              Label lb;
              GridHeaderItem headerItem;

              headerItem = (GridHeaderItem)rdGridControlMetasvsReal.MasterTableView.GetItems(GridItemType.Header)[0];
              if (headerItem.FindControl("fecha_ini_sem1") != null)
              {
                lb = (Label)headerItem.FindControl("fecha_ini_sem1");
                lb.Text = DateTime.Parse(dt.Rows[0]["fecha_ini_sem1"].ToString()).ToString("dd-MM-yyyy");
              }

              if (headerItem.FindControl("fecha_fin_sem1") != null)
              {
                lb = (Label)headerItem.FindControl("fecha_fin_sem1");
                lb.Text = DateTime.Parse(dt.Rows[0]["fecha_fin_sem1"].ToString()).ToString("dd-MM-yyyy");
              }

              if (headerItem.FindControl("fecha_ini_sem2") != null)
              {
                lb = (Label)headerItem.FindControl("fecha_ini_sem2");
                lb.Text = DateTime.Parse(dt.Rows[0]["fecha_ini_sem2"].ToString()).ToString("dd-MM-yyyy");
              }

              if (headerItem.FindControl("fecha_fin_sem2") != null)
              {
                lb = (Label)headerItem.FindControl("fecha_fin_sem2");
                lb.Text = DateTime.Parse(dt.Rows[0]["fecha_fin_sem2"].ToString()).ToString("dd-MM-yyyy");
              }

              if (headerItem.FindControl("fecha_ini_sem3") != null)
              {
                lb = (Label)headerItem.FindControl("fecha_ini_sem3");
                lb.Text = DateTime.Parse(dt.Rows[0]["fecha_ini_sem3"].ToString()).ToString("dd-MM-yyyy");
              }

              if (headerItem.FindControl("fecha_fin_sem3") != null)
              {
                lb = (Label)headerItem.FindControl("fecha_fin_sem3");
                lb.Text = DateTime.Parse(dt.Rows[0]["fecha_fin_sem3"].ToString()).ToString("dd-MM-yyyy");
              }

              if (headerItem.FindControl("fecha_ini_sem4") != null)
              {
                lb = (Label)headerItem.FindControl("fecha_ini_sem4");
                lb.Text = DateTime.Parse(dt.Rows[0]["fecha_ini_sem4"].ToString()).ToString("dd-MM-yyyy");
              }

              if (headerItem.FindControl("fecha_fin_sem4") != null)
              {
                lb = (Label)headerItem.FindControl("fecha_fin_sem4");
                lb.Text = DateTime.Parse(dt.Rows[0]["fecha_fin_sem4"].ToString()).ToString("dd-MM-yyyy");
              }

              if (headerItem.FindControl("fecha_ini_sem5") != null)
              {
                lb = (Label)headerItem.FindControl("fecha_ini_sem5");
                lb.Text = DateTime.Parse(dt.Rows[0]["fecha_ini_sem5"].ToString()).ToString("dd-MM-yyyy");
              }

              if (headerItem.FindControl("fecha_fin_sem5") != null)
              {
                lb = (Label)headerItem.FindControl("fecha_fin_sem5");
                lb.Text = DateTime.Parse(dt.Rows[0]["fecha_fin_sem5"].ToString()).ToString("dd-MM-yyyy");
              }
            }
          }
          dt = null;

        }
        oConn.Close();
      }
      else {
        rdGridControlMetasvsReal.MasterTableView.GetColumn("TemplateColumn1").Visible = false;
        rdGridControlMetasvsReal.MasterTableView.GetColumn("TemplateColumn2").Visible = false;
        rdGridControlMetasvsReal.MasterTableView.GetColumn("TemplateColumn3").Visible = false;
        rdGridControlMetasvsReal.MasterTableView.GetColumn("TemplateColumn4").Visible = false;
        rdGridControlMetasvsReal.MasterTableView.GetColumn("TemplateColumn5").Visible = false;
        rdGridControlMetasvsReal.MasterTableView.GetColumn("TemplateColumn6").Visible = false;
        rdGridControlMetasvsReal.MasterTableView.GetColumn("Real").Visible = true;
        rdGridControlMetasvsReal.MasterTableView.GetColumn("Estimado").Visible = true;
        rdGridControlMetasvsReal.MasterTableView.GetColumn("Diferencia").Visible = true;
        rdGridControlMetasvsReal.MasterTableView.GetColumn("SDeudor").Visible = true;
        rdGridControlMetasvsReal.MasterTableView.GetColumn("SAnalista").Visible = true;
        rdGridControlMetasvsReal.MasterTableView.GetColumn("EtapaCob").Visible = true;
        rdGridControlMetasvsReal.MasterTableView.GetColumn("comentario").Visible = true;
      }

    }
  }
}