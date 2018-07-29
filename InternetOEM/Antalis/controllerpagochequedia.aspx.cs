using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

using System.Data;
using System.Text;
using OnlineServices.Conn;
using OnlineServices.Method;
using OnlineServices.Reporting;

using OnlineServices.Antalis;
using OnlineServices.SystemData;
using System.Web.Services;

namespace ICommunity.Antalis
{
  public partial class controllerpagochequedia : System.Web.UI.Page
  {
    private OnlineServices.Method.Web oWeb = new OnlineServices.Method.Web();
    private OnlineServices.Method.Usuario oIsUsuario;
    Emailing oEmailing = new Emailing();

    protected void Page_Load(object sender, EventArgs e)
    {
      oIsUsuario = oWeb.ValidaUserAppReport();
      getMenu(idReportePago, oIsUsuario.CodUsuario, "1");
      getMenu(idProcesoSeguimiento, oIsUsuario.CodUsuario, "2");
      getMenu(idCartolas, oIsUsuario.CodUsuario, "3");
      getMenu(idProcesoNormalizacion, oIsUsuario.CodUsuario, "4");
      getMenu(idIndicadoresClaves, oIsUsuario.CodUsuario, "5");
      getMenu(IndClasificacionRiesgo, oIsUsuario.CodUsuario, "6");

      getMenuAntalis(indAntalis, oIsUsuario.CodUsuario);

      if (!IsPostBack)
      {
        hdd_cod_pago.Value = oWeb.GetData("CodPago");
        DBConn oConn = new DBConn();
        if (oConn.Open())
        {
          cAntPagos oPagos = new cAntPagos(ref oConn);
          oPagos.CodPagos = hdd_cod_pago.Value;
          DataTable dt = oPagos.Get();
          if (dt != null)
          {
            if (dt.Rows.Count > 0)
            {
              lblValija.Text = "# Valija " + dt.Rows[0]["cod_pago"].ToString();

              cCliente oCliente = new cCliente(ref oConn);
              oCliente.CodNkey = dt.Rows[0]["nkey_cliente"].ToString();
              DataTable dtCliente = oCliente.GeCliente();
              if (dtCliente != null)
              {
                if (dtCliente.Rows.Count > 0)
                {
                  lblRazonSocial.Text = dtCliente.Rows[0]["cliente"].ToString();
                }
              }
              dtCliente = null;

              cAntCentrosDistribucion oCentrosDistribucion = new cAntCentrosDistribucion(ref oConn);
              oCentrosDistribucion.CodCentroDist = dt.Rows[0]["cod_centrodist"].ToString();
              DataTable dtCentro = oCentrosDistribucion.GetByCod();
              if (dtCentro != null)
              {
                if (dtCentro.Rows.Count > 0)
                {
                  lblCentroDistribucion.Text = dtCentro.Rows[0]["descripcion"].ToString();
                }
              }
              dtCentro = null;

              lblFecharecepcion.Text = dt.Rows[0]["fech_recepcion"].ToString();
              hdd_tipo_documento.Value = dt.Rows[0]["cod_tipo_pago"].ToString();
              string lblTitulo = string.Empty;
              switch (dt.Rows[0]["cod_tipo_pago"].ToString())
              {
                case "1":
                  lblTitulo = "VALIDACIÓN DE RECAUDACIÓN DE PAGOS / CHEQUES AL DÍA";
                  break;
                case "2":
                  lblTitulo = "VALIDACIÓN DE RECAUDACIÓN DE PAGOS / CHEQUES AL FECHA";
                  break;
                case "4":
                  lblTitulo = "VALIDACIÓN DE RECAUDACIÓN DE PAGOS / LETRA";
                  break;
                case "5":
                  lblTitulo = "VALIDACIÓN DE RECAUDACIÓN DE PAGOS / TARJETA";
                  break;
                case "6":
                  lblTitulo = "VALIDACIÓN DE RECAUDACIÓN DE PAGOS / TRANSFERENCIA";
                  break;
              }
              lblTitle.Text = lblTitulo;
            }
          }
          dt = null;

        }
        oConn.Close();

        Log oLog = new Log();
        oLog.IdUsuario = oIsUsuario.CodUsuario;
        oLog.ObsLog = "VALIDACION VALIJA #" + hdd_cod_pago.Value;
        oLog.CodEvtLog = "2";
        oLog.AppLog = "ANTALIS";
        oLog.putLog();
      }

      onLoadGrid();
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

    protected void onLoadGrid()
    {
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        cAntDocumentosPago oDocumentosPago = new cAntDocumentosPago(ref oConn);
        oDocumentosPago.CodPagos = hdd_cod_pago.Value;
        DataTable dt = oDocumentosPago.GetDocFacturas();
        gdPagos.DataSource = dt;
        gdPagos.DataBind();

        if (dt != null)
        {
          if (dt.Rows.Count > 0)
          {
            lblCantidad.Text = string.Format("{0:N0}", dt.Compute("COUNT(cod_documento)", " nod_cod_documento is null "));

            string iImporteTotal = dt.Compute("SUM(importe)", string.Empty).ToString();
            lblMonto.Text = string.Format("{0:N0}",int.Parse(iImporteTotal));

            string iImporteTotalRecibido = (!string.IsNullOrEmpty(dt.Compute("SUM(importe_recibido)", string.Empty).ToString()) ? dt.Compute("SUM(importe_recibido)", string.Empty).ToString() : "0");
            lblImporteTotalRecibido.Text = string.Format("{0:N0}", int.Parse(iImporteTotalRecibido));
            hdd_importetotal_recibido.Value = iImporteTotalRecibido;

            string iDiscrepancia = (!string.IsNullOrEmpty(dt.Compute("SUM(discrepancia)", string.Empty).ToString()) ? dt.Compute("SUM(discrepancia)", string.Empty).ToString() : "0");
            lblDiscrepanciaTotal.Text = string.Format("{0:N0}", int.Parse(iDiscrepancia));
            hdd_total_discrepancia.Value = iDiscrepancia;

            if ((iImporteTotal == iImporteTotalRecibido) && (int.Parse(iDiscrepancia) == 0))
            {
              btnAceptar.Visible = true;
              btnRechazar.Visible = false;
            }
            else if ((iImporteTotal != iImporteTotalRecibido) && (int.Parse(iDiscrepancia) >= 0))
            {
              btnAceptar.Visible = false;
              btnRechazar.Visible = true;
            }
          }
        }
        dt = null;

        oConn.Close();
      }
    }

    protected void gdPagos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
      gdPagos.PageIndex = e.NewPageIndex;
      onLoadGrid();
    }

    protected void gdPagos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (e.Row.RowType == DataControlRowType.DataRow)
      {
        string sNodCodDocumento = gdPagos.DataKeys[e.Row.RowIndex].Values["nod_cod_documento"].ToString();
        foreach (DataControlFieldCell cell in e.Row.Cells)
        {
          foreach (Control control in cell.Controls)
          {
            LinkButton BtnlnkSI = control as LinkButton;
            if ((BtnlnkSI != null) && (BtnlnkSI.CommandName == "SI") && (!string.IsNullOrEmpty(sNodCodDocumento)))
            {
              BtnlnkSI.Visible = false;
            }else if ((BtnlnkSI != null) && (BtnlnkSI.CommandName == "NO") && (!string.IsNullOrEmpty(sNodCodDocumento)))
            {
              BtnlnkSI.Visible = false;
            }
          }
        }


        switch (hdd_tipo_documento.Value)
        {
          case "1":
          case "2":
          case "4":

            if (e.Row.Cells[6].Text.ToString() != "&nbsp;")
            {
              DBConn oConn = new DBConn();
              if (oConn.Open())
              {
                cAntBancos oBancos = new cAntBancos(ref oConn);
                oBancos.NKeyBanco = e.Row.Cells[6].Text.ToString();
                DataTable dt = oBancos.Get();
                if (dt != null)
                {
                  if (dt.Rows.Count > 0)
                  {
                    e.Row.Cells[6].Text = e.Row.Cells[6].Text.ToString() + " - " + dt.Rows[0]["snombre"].ToString();
                  }
                }
                dt = null;

              }
              oConn.Close();
            }
            break;
        }
      }
    }

    protected void btnModificar_Click(object sender, EventArgs e)
    {
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        cAntDocumentosPago oDocumentosPago = new cAntDocumentosPago(ref oConn);
        oDocumentosPago.CodDocumento = hdd_cod_documento.Value;
        oDocumentosPago.ImporteRecibido = txt_importe_recibido.Text;
        oDocumentosPago.Discrepancia = txt_discrepancia.Text;
        oDocumentosPago.Accion = "EDITAR";
        oDocumentosPago.Put();

        oConn.Close();
      }

      hdd_cod_documento.Value = string.Empty;
      txt_importe_recibido.Text = string.Empty;
      txt_discrepancia.Text = string.Empty;
      lblRazonSocialPago.Text = string.Empty;
      lblcuentacorriente.Text = string.Empty;
      lblFechtransaccion.Text = string.Empty;
      lblNumOperacion.Text = string.Empty;
      lblimporte.Text = string.Empty;
      hdd_importe.Value = string.Empty;

      idRow1.Visible = false;
      idRow2.Visible = false;
      idRow3.Visible = false;
      idColBanco.Visible = false;

      onLoadGrid();
    }

    protected void btnCancelModify_Click(object sender, EventArgs e)
    {
      hdd_cod_documento.Value = string.Empty;
      txt_importe_recibido.Text = string.Empty;
      txt_discrepancia.Text = string.Empty;
      lblRazonSocialPago.Text = string.Empty;
      lblcuentacorriente.Text = string.Empty;
      lblFechtransaccion.Text = string.Empty;
      lblNumOperacion.Text = string.Empty;
      lblimporte.Text = string.Empty;
      hdd_importe.Value = string.Empty;

      idRow1.Visible = false;
      idRow2.Visible = false;
      idRow3.Visible = false;
      idColBanco.Visible = false;

      onLoadGrid();
    }

    protected void btnRechazar_Click(object sender, EventArgs e)
    {
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        cAntPagos oPagos = new cAntPagos(ref oConn);
        oPagos.CodPagos = hdd_cod_pago.Value;
        oPagos.ImporteTotalRecibido = hdd_importetotal_recibido.Value;
        oPagos.Discrepancia = hdd_total_discrepancia.Value;
        oPagos.Estado = "A";
        oPagos.Accion = "EDITAR";
        oPagos.Put();
        oConn.Close();
      }

      StringBuilder sHmtl = new StringBuilder();
      sHmtl.Append("Se informa que la valija # ").Append(lblValija.Text).Append(" a sido rechazada.");

      AppSettingsReader appReader = new System.Configuration.AppSettingsReader();
      string sEmlAddr = appReader.GetValue("AntalisMail", typeof(string)).ToString();

      oEmailing.FromName = Application["NameSender"].ToString();
      oEmailing.From = Application["EmailSender"].ToString();
      oEmailing.Address = sEmlAddr;
      oEmailing.Subject = "Valija # " + lblValija.Text + ", RECHAZADA";
      oEmailing.Body = sHmtl;
      oEmailing.EmailSend();

      Log oLog = new Log();
      oLog.IdUsuario = oIsUsuario.CodUsuario;
      oLog.ObsLog = "RECHAZO VALIJA #" + hdd_cod_pago.Value;
      oLog.CodEvtLog = "2";
      oLog.AppLog = "ANTALIS";
      oLog.putLog();

      Response.Redirect("controllerpagos.aspx");
    }

    protected void btnAceptar_Click(object sender, EventArgs e)
    {
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        cAntPagos oPagos = new cAntPagos(ref oConn);
        oPagos.CodPagos = hdd_cod_pago.Value;
        oPagos.ImporteTotalRecibido = hdd_importetotal_recibido.Value;
        oPagos.Discrepancia = hdd_total_discrepancia.Value;
        oPagos.Estado = "V";
        oPagos.Accion = "EDITAR";
        oPagos.Put();
        oConn.Close();
      }

      string lblTitulo = string.Empty;
      switch (hdd_tipo_documento.Value)
      {
        case "1":
          lblTitulo = "CHEQUES AL DÍA";
          break;
        case "2":
          lblTitulo = "CHEQUES AL FECHA";
          break;
        case "4":
          lblTitulo = "LETRA";
          break;
        case "5":
          lblTitulo = "TARJETA";
          break;
        case "6":
          lblTitulo = "TRANSFERENCIA";
          break;
      }

      StringBuilder sHmtl = new StringBuilder();
      sHmtl.Append("Se informa que la valija # ").Append(lblValija.Text).Append(", de tipo de pago ").Append(lblTitulo).Append("  a sido validada.");

      AppSettingsReader appReader = new System.Configuration.AppSettingsReader();
      string sEmlAddr = appReader.GetValue("AntalisMail", typeof(string)).ToString();

      oEmailing.FromName = Application["NameSender"].ToString();
      oEmailing.From = Application["EmailSender"].ToString();
      oEmailing.Address = sEmlAddr;
      oEmailing.Subject = "Valija # " + lblValija.Text + ", VALIDADA";
      oEmailing.Body = sHmtl;
      oEmailing.EmailSend();

      Log oLog = new Log();
      oLog.IdUsuario = oIsUsuario.CodUsuario;
      oLog.ObsLog = "VALIDADO VALIJA #" + hdd_cod_pago.Value;
      oLog.CodEvtLog = "2";
      oLog.AppLog = "ANTALIS";
      oLog.putLog();

      Response.Redirect("controllerpagos.aspx");
    }

    protected void gdPagos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      LinkButton lnkBtn = (LinkButton)e.CommandSource;    // the button
      GridViewRow myRow = (GridViewRow)lnkBtn.Parent.Parent;  // the row
      GridView myGrid = (GridView)sender; // the gridview
      string pCodDocumento = gdPagos.DataKeys[myRow.RowIndex].Value.ToString(); // value of the datakey 

      if (e.CommandName == "NO")
      {

        DBConn oConn = new DBConn();
        if (oConn.Open())
        {
          cAntDocumentosPago oDocumentosPago = new cAntDocumentosPago(ref oConn);
          oDocumentosPago.CodDocumento = pCodDocumento;
          DataTable dt = oDocumentosPago.Get();
          if (dt != null)
          {
            if (dt.Rows.Count > 0)
            {
              idRow1.Visible = true;
              idRow2.Visible = true;
              idRow3.Visible = true;

              hdd_cod_documento.Value = dt.Rows[0]["cod_documento"].ToString();
              lblRazonSocialPago.Text = dt.Rows[0]["nom_deudor"].ToString();
              lblcuentacorriente.Text = dt.Rows[0]["cuenta_corriente"].ToString();
              lblFechtransaccion.Text = dt.Rows[0]["fch_documento"].ToString();
              lblNumOperacion.Text = dt.Rows[0]["num_documento"].ToString();

              cAntBancos oBancos = new cAntBancos(ref oConn);
              oBancos.NKeyBanco = dt.Rows[0]["cod_banco"].ToString();
              DataTable dtBanco = oBancos.Get();
              if (dtBanco != null)
              {
                if (dtBanco.Rows.Count > 0)
                {
                  lblBanco.Text = dt.Rows[0]["cod_banco"].ToString() + " - " + dtBanco.Rows[0]["snombre"].ToString();
                }
              }
              dtBanco = null;

              lblimporte.Text = string.Format("{0:N0}",int.Parse(dt.Rows[0]["importe"].ToString()));
              hdd_importe.Value = dt.Rows[0]["importe"].ToString();

            }
          }
          dt = null;
          oConn.Close();
        }
        btnAceptar.Visible = false;
        btnRechazar.Visible = false;
        if ((hdd_tipo_documento.Value == "1")||(hdd_tipo_documento.Value == "2")||(hdd_tipo_documento.Value == "4"))
          idColBanco.Visible = true;

      }
      else if (e.CommandName == "SI")
      {
        DBConn oConn = new DBConn();
        if (oConn.Open())
        {
          cAntDocumentosPago oDocumentosPago = new cAntDocumentosPago(ref oConn);
          oDocumentosPago.CodDocumento = pCodDocumento;
          DataTable dt = oDocumentosPago.Get();
          if (dt != null)
          {
            if (dt.Rows.Count > 0)
            {
              oDocumentosPago.ImporteRecibido = dt.Rows[0]["importe"].ToString();
            }
          }
          dt = null;

          oDocumentosPago.Discrepancia = "0";
          oDocumentosPago.Accion = "EDITAR";
          oDocumentosPago.Put();

          oConn.Close();
        }

        onLoadGrid();
      }
    }
  }
}