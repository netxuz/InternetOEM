using System;
using System.Collections.Generic;
using System.Globalization;
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
  public partial class app_cartola_find : System.Web.UI.Page
  {
    private OnlineServices.Method.Web oWeb = new OnlineServices.Method.Web();
    private OnlineServices.Method.Usuario oIsUsuario;
    CultureInfo oCulture = new CultureInfo("es-CL");
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
        rdCmbBoxMeses.Items.FindByValue(dTimeNow.AddMonths(-1).Month.ToString()).Selected = true;
        tdTxtAno.Text = DateTime.Now.Year.ToString();

        Log oLog = new Log();
        oLog.IdUsuario = oIsUsuario.CodUsuario;
        oLog.ObsLog = "REPORTE DE CARTOLA";
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
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        cCliente oCliente = new cCliente(ref oConn);
        oCliente.CodNkey = oIsUsuario.CodNkey;
        DataTable dtCliente = oCliente.GeCliente();
        if (dtCliente != null)
        {
          if (dtCliente.Rows.Count > 0)
          {
            sNombre.Text = dtCliente.Rows[0]["cliente"].ToString();
            sDirec.Text = dtCliente.Rows[0]["direccion"].ToString();
            sComuna.Text = dtCliente.Rows[0]["comuna"].ToString();
            sRut.Text = dtCliente.Rows[0]["Rut"].ToString() + " - " + dtCliente.Rows[0]["dv"].ToString();
          }
        }
        dtCliente = null;

        cEjecutivo oEjecutivo = new cEjecutivo(ref oConn);
        oEjecutivo.CodNkey = oIsUsuario.CodNkey;
        DataTable dtEjecutivo = oEjecutivo.Get();
        if (dtEjecutivo != null)
        {
          if (dtEjecutivo.Rows.Count > 0)
          {
            sEjeNombre.Text = dtEjecutivo.Rows[0]["Ejecutivo"].ToString();
            sEjeFax.Text = dtEjecutivo.Rows[0]["Fax"].ToString();
            sTele.Text = dtEjecutivo.Rows[0]["Telefono"].ToString();
            sEMail.Text = dtEjecutivo.Rows[0]["EMail"].ToString();
          }
        }
        dtEjecutivo = null;
      }
      oConn.Close();

      rdGridCartola.Rebind();
    }

    protected void rdGridCartola_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
      string lngMntTtlSaldo = "0";
      string lngKeyAnt = "0";
      string dtFechIni = "19900101";
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        lblFechCartola.Text = rdCmbBoxMeses.SelectedValue + "/" + tdTxtAno.Text;
        int dtMes1 = int.Parse(rdCmbBoxMeses.SelectedValue) - 1;
        int dtAno1 = 0;
        if (dtMes1 == 0)
        {
          dtMes1 = 12;
          dtAno1 = int.Parse(tdTxtAno.Text) - 1;
        }
        else
          dtAno1 = int.Parse(tdTxtAno.Text);

        cCartola oCartola = new cCartola(ref oConn);
        oCartola.CodDeudor = hddCodDeudor.Value;
        oCartola.CodNkey = oIsUsuario.CodNkey;
        oCartola.NkeyUsuario = oIsUsuario.NKeyUsuario;
        oCartola.TipoUsuario = oIsUsuario.TipoUsuario;

        oCartola.DtMes = dtMes1.ToString();
        oCartola.DtAno = dtAno1.ToString();
        DataTable dtDataMes = oCartola.GetDataMesAnterior();
        if (dtDataMes != null)
        {
          if (dtDataMes.Rows.Count > 0)
          {
            lngKeyAnt = dtDataMes.Rows[0]["keyAnt"].ToString();
            dtFechIni = dtDataMes.Rows[0]["fechainicial"].ToString();
          }
        }
        dtDataMes = null;

        oCartola.DtMes = rdCmbBoxMeses.SelectedValue;
        oCartola.DtAno = tdTxtAno.Text;
        dtDataMes = oCartola.GetDataMesSolicitado();
        if (dtDataMes != null)
        {
          if (dtDataMes.Rows.Count > 0)
          {
            oCartola.KeyAnt = lngKeyAnt;
            DataTable dtSaldoAnte = oCartola.GetReturnSaldoAnterior();
            if (dtSaldoAnte != null)
            {
              if (dtSaldoAnte.Rows.Count > 0)
              {
                lngMntTtlSaldo = dtSaldoAnte.Rows[0]["saldoanterior"].ToString();
              }
            }
            dtSaldoAnte = null;

            lblFechIni.Text = DateTime.Parse(dtFechIni).ToString("dd-MM-yyyy");
            lblMntTtlSaldo.Text = int.Parse(lngMntTtlSaldo).ToString("N0", oCulture);
            lblFechFnl.Text = DateTime.Parse(dtDataMes.Rows[0]["fechafinal"].ToString()).ToString("dd-MM-yyyy");

            oCartola.KeyAct = dtDataMes.Rows[0]["keyAct"].ToString();
            DataTable dtCartola = oCartola.Get();
            if (dtCartola != null)
            {
              if (dtCartola.Rows.Count > 0)
              {
                int iCantDoc = int.Parse(dtCartola.Compute("Sum(Cantidad_Documentos)", "").ToString());
                int iCantPagos = int.Parse(dtCartola.Compute("Sum(Cantidad_Pagos)", "").ToString());
                int iMntTtDoc = int.Parse(dtCartola.Compute("Sum(Monto_Documento)", "").ToString());
                int iMntTtFct = int.Parse(dtCartola.Compute("Sum(Monto_Factura)", "").ToString());

                lblCantDoc.Text = iCantDoc.ToString();
                lblCantPagos.Text = iCantPagos.ToString();
                lblMntTtDoc.Text = iMntTtDoc.ToString("N0", oCulture);
                lblMntTtFct.Text = iMntTtFct.ToString("N0", oCulture);
                lblSaldoFinalAuxiliar.Text = ((int.Parse(lngMntTtlSaldo) + iMntTtDoc) - iMntTtFct).ToString("N0", oCulture);
                rdGridCartola.DataSource = dtCartola;
              }
            }
            dtCartola = null;
          }
          dtDataMes = null;
        }
        oConn.Close();
      }
    }
  }
}