using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
  public partial class detallevalija : System.Web.UI.Page
  {
    private OnlineServices.Method.Web oWeb = new OnlineServices.Method.Web();
    private OnlineServices.Method.Usuario oIsUsuario;
    protected void Page_Load(object sender, EventArgs e)
    {
      oIsUsuario = oWeb.ValidaUserAppReport();
      //getMenu(idReportePago, oIsUsuario.CodUsuario, "1");
      //getMenu(idProcesoSeguimiento, oIsUsuario.CodUsuario, "2");
      //getMenu(idCartolas, oIsUsuario.CodUsuario, "3");
      //getMenu(idProcesoNormalizacion, oIsUsuario.CodUsuario, "4");
      //getMenu(idIndicadoresClaves, oIsUsuario.CodUsuario, "5");
      //getMenu(IndClasificacionRiesgo, oIsUsuario.CodUsuario, "6");

      //getMenuAntalis(indAntalis, oIsUsuario.CodUsuario);

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
                  lblTitulo = "DETALLE DE PAGO / CHEQUES AL DÍA";
                  break;
                case "2":
                  lblTitulo = "DETALLE DE PAGO / CHEQUES A FECHA";
                  break;
                case "4":
                  lblTitulo = "DETALLE DE PAGO / LETRA";
                  break;
                case "5":
                  lblTitulo = "DETALLE DE PAGO / TARJETA";
                  break;
                case "6":
                  lblTitulo = "DETALLE DE PAGO / TRANSFERENCIA";
                  break;
              }
              lblTitle.Text = lblTitulo;
            }
          }
          dt = null;

        }
        oConn.Close();
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

        //e.Row.Cells[0].Text = ((e.Row.Cells[11].Text.ToString() != "0") && (e.Row.Cells[12].Text.ToString() != "0") ? "FNC" : ((e.Row.Cells[11].Text.ToString() != "0") ? "F" : "NC"));

        e.Row.Cells[0].Text = getTipoDoc(hdd_tipo_documento.Value);

        switch (hdd_tipo_documento.Value)
        {
          case "1":
          case "2":
          case "4":

            if (e.Row.Cells[5].Text.ToString() != "&nbsp;")
            {
              DBConn oConn = new DBConn();
              if (oConn.Open())
              {
                cAntBancos oBancos = new cAntBancos(ref oConn);
                oBancos.NKeyBanco = e.Row.Cells[5].Text.ToString();
                DataTable dt = oBancos.Get();
                if (dt != null)
                {
                  if (dt.Rows.Count > 0)
                  {
                    e.Row.Cells[5].Text = dt.Rows[0]["ncod"].ToString() + " - " + dt.Rows[0]["snombre"].ToString();
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

    protected string getTipoDoc(string iCodTipo)
    {
      string sTipo = string.Empty;
      switch (iCodTipo)
      {
        case "1":
          sTipo = "CHD";
          break;
        case "2":
          sTipo = "CHF";
          break;
        case "3":
          sTipo = "EFE";
          break;
        case "4":
          sTipo = "LTR";
          break;
        case "5":
          sTipo = "TDC";
          break;
        case "6":
          sTipo = "TNF";
          break;

      }
      return sTipo;
    }

    protected void bnt_logout_Click(object sender, EventArgs e)
    {
      Session["USUARIO"] = string.Empty;
      Session["CodUsuarioPerfil"] = string.Empty;
      Response.Redirect("/");
    }
  }
}