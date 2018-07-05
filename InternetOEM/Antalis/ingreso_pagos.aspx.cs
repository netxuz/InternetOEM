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
  public partial class ingreso_pagos : System.Web.UI.Page
  {
    private OnlineServices.Method.Web oWeb = new OnlineServices.Method.Web();
    private OnlineServices.Method.Usuario oIsUsuario;
    bool bEstadoValija = false;
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

        if (string.IsNullOrEmpty(hdd_cod_pago.Value))
        {
          txt_fecha_recepcion.Value = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString();
          lbl_fecha_recepcion.Text = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString();

        }

        DBConn oConn = new DBConn();
        if (oConn.Open())
        {
          cAntCentrosDistribucion oCentrosDistribucion = new cAntCentrosDistribucion(ref oConn);
          oCentrosDistribucion.CodUsuario = oIsUsuario.CodUsuario;
          DataTable dtCntDst = oCentrosDistribucion.GetCentrosDistByUsuario();
          if (dtCntDst != null)
          {
            if (dtCntDst.Rows.Count > 0)
            {
              if (dtCntDst.Rows.Count > 1)
                cmb_centrodistribucion.Items.Add(new ListItem("<< Seleccione una opcion >>", ""));

              foreach (DataRow oRow in dtCntDst.Rows)
              {
                cmb_centrodistribucion.Items.Add(new ListItem(oRow["descripcion"].ToString(), oRow["cod_centrodist"].ToString()));
              }
            }
            else
            {
              cmb_centrodistribucion.Items.Add(new ListItem("No existen centros de distribución asociados", ""));
              cmb_centrodistribucion.Enabled = false;
            }
          }
          dtCntDst = null;

          cmb_bancos.Items.Add(new ListItem("<< Seleccione Banco >>", string.Empty));
          cAntBancos oBancos = new cAntBancos(ref oConn);
          DataTable dtBancos = oBancos.Get();
          if (dtBancos != null)
          {
            foreach (DataRow oRow in dtBancos.Rows)
            {
              cmb_bancos.Items.Add(new ListItem(oRow["nkey_banco"].ToString() + " - " + oRow["snombre"].ToString(), oRow["nkey_banco"].ToString()));
            }
          }
          dtBancos = null;

          if (!string.IsNullOrEmpty(hdd_cod_pago.Value))
          {
            lblValija.Text = "# Valija " + hdd_cod_pago.Value;
            cAntPagos oPagos = new cAntPagos(ref oConn);
            oPagos.CodPagos = hdd_cod_pago.Value;
            DataTable dtPagos = oPagos.Get();
            if (dtPagos != null)
            {
              if (dtPagos.Rows.Count > 0)
              {
                cmb_centrodistribucion.Items.FindByValue(dtPagos.Rows[0]["cod_centrodist"].ToString()).Selected = true; ;
                cmb_centrodistribucion.Enabled = false;
                cmb_documento.Items.FindByValue(dtPagos.Rows[0]["cod_tipo_pago"].ToString()).Selected = true;
                cmb_documento.Enabled = false;
                txt_fecha_recepcion.Value = dtPagos.Rows[0]["fech_recepcion"].ToString();
                lbl_fecha_recepcion.Text = dtPagos.Rows[0]["fech_recepcion"].ToString();
                bEstadoValija = ((dtPagos.Rows[0]["estado"].ToString() != "C") ? true : false);

                if (!bEstadoValija)
                {
                  idRow1.Visible = false;
                  idRow2.Visible = false;
                  idRow3.Visible = false;
                }

              }
            }
            dtPagos = null;

            onLoadGrid();
          }

          cCliente oCliente = new cCliente(ref oConn);
          oCliente.CodNkey = oIsUsuario.CodNkey;
          DataTable dt = oCliente.GeCliente();
          if (dt != null)
          {
            if (dt.Rows.Count > 0)
            {
              lblRazonSocial.Text = dt.Rows[0]["cliente"].ToString();
            }
          }
          dt = null;

          oConn.Close();
        }
        hddnkey_cliente.Value = oIsUsuario.CodNkey;
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

    [WebMethod()]
    public static cGuiasDespacho[] getGuiasDespacho(string nkeycliente, string ncodigodeudor)
    {
      List<cGuiasDespacho> details = new List<cGuiasDespacho>();

      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        cGuiasFacturas oGuiasFacturas = new cGuiasFacturas(ref oConn);
        oGuiasFacturas.NKeyCliente = nkeycliente;
        oGuiasFacturas.NCodigoDeudor = ncodigodeudor;
        DataTable dtGuias = oGuiasFacturas.GetGuiaDespacho();
        if (dtGuias != null)
        {
          foreach (DataRow oRow in dtGuias.Rows)
          {
            cGuiasDespacho oGuiasDespacho = new cGuiasDespacho();
            oGuiasDespacho.guiasdespacho = oRow["guidespacho"].ToString();
            details.Add(oGuiasDespacho);
          }
        }
        dtGuias = null;
        oConn.Close();
      }
      return details.ToArray();

    }

    [WebMethod()]
    public static cFacturas[] getFacturas(string sGuiaDespacho)
    {
      List<cFacturas> details = new List<cFacturas>();

      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        cGuiasFacturas oGuiasFacturas = new cGuiasFacturas(ref oConn);
        oGuiasFacturas.GuiaDespacho = sGuiaDespacho;
        DataTable dtFacturas = oGuiasFacturas.GetFacturas();
        if (dtFacturas != null)
        {
          foreach (DataRow oRow in dtFacturas.Rows)
          {
            cFacturas oFacturas = new cFacturas();
            oFacturas.nKeyFactura = oRow["nkey_factura"].ToString();
            oFacturas.nNumeroFactura = oRow["nnumerofactura"].ToString();
            oFacturas.nMontoFactura = oRow["nmontofactura"].ToString();
            details.Add(oFacturas);
          }
        }
        dtFacturas = null;
        oConn.Close();
      }
      return details.ToArray();

    }

    protected void btnIngresarImportes_Click(object sender, EventArgs e)
    {
      string pCodCentroDist = cmb_centrodistribucion.SelectedValue;
      string pCodTipoPago = cmb_documento.SelectedValue;
      string pFchRecepcion = txt_fecha_recepcion.Value;
      string pCodSAP = txt_codigosap.Text;
      //string sRazonSocial = txt_razon_social.Text;
      string pNumOperacion = txt_num_documento.Text;
      string pCodBanco = cmb_bancos.SelectedValue;
      string pFchDocumento = hdd_fchdocument.Value;
      string pGuiaDespacho = hddGuiasDespacho.Value;
      string[] sFactura = hdd_facturas.Value.Split('|');
      string pNumFactura = sFactura[0].ToString();
      string pValor = sFactura[1].ToString();
      string pImporte = txt_importe.Text;

      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        if (string.IsNullOrEmpty(hdd_cod_pago.Value))
        {
          cAntPagos oPagos = new cAntPagos(ref oConn);
          oPagos.CodUsuario = oIsUsuario.CodUsuario;
          oPagos.NKeyCliente = oIsUsuario.CodNkey;
          oPagos.CodCentroDist = pCodCentroDist;
          oPagos.CodTipoPago = pCodTipoPago;
          oPagos.FechRecepcion = pFchRecepcion;
          oPagos.Estado = "A";
          oPagos.Accion = "CREAR";
          oPagos.Put();

          string pCodPago = oPagos.CodPagos;
          hdd_cod_pago.Value = pCodPago;
          lblValija.Text = "# Valija " + hdd_cod_pago.Value;
        }

        DataTable dtFactura;
        cAntFactura oFactura;
        cAntDocumentosPago oAntDocumentosPago;

        if (!string.IsNullOrEmpty(hdd_cod_documento.Value))
        {
          oAntDocumentosPago = new cAntDocumentosPago(ref oConn);
          oAntDocumentosPago.CodDocumento = hdd_cod_documento.Value;
          DataTable dt = oAntDocumentosPago.GetDocFacturas();
          if (dt != null)
          {
            if (dt.Rows.Count > 0)
            {
              oFactura = new cAntFactura(ref oConn);
              oFactura.CodFactura = dt.Rows[0]["cod_factura"].ToString();
              dtFactura = oFactura.Get();
              if (dtFactura != null)
              {
                if (dtFactura.Rows.Count > 0)
                {
                  string nSaldo = dtFactura.Rows[0]["saldo_factura"].ToString();
                  oFactura.SaldoFactura = (int.Parse(nSaldo) + int.Parse(dt.Rows[0]["importe"].ToString())).ToString();
                  oFactura.Accion = "EDITAR";
                  oFactura.Put();
                }
              }
              dtFactura = null;
            }
          }
          dt = null;
        }

        string pCodFactura = string.Empty;

        oFactura = new cAntFactura(ref oConn);
        oFactura.NumFactura = pNumFactura;
        dtFactura = oFactura.Get();
        if (dtFactura != null)
        {
          if (dtFactura.Rows.Count > 0)
          {
            pCodFactura = dtFactura.Rows[0]["cod_factura"].ToString();
            pValor = dtFactura.Rows[0]["saldo_factura"].ToString();

            if (int.Parse(pValor) > int.Parse(pImporte))
              oFactura.SaldoFactura = (int.Parse(pValor) - int.Parse(pImporte)).ToString();
            else
              oFactura.SaldoFactura = "0";
            oFactura.CodFactura = pCodFactura;
            oFactura.Accion = "EDITAR";
            oFactura.Put();
          }
          else
          {
            oFactura.ValorFactura = pValor;
            oFactura.SaldoFactura = (int.Parse(pValor) - int.Parse(pImporte)).ToString();
            oFactura.Accion = "CREAR";
            oFactura.Put();

            pCodFactura = oFactura.CodFactura;
          }
        }
        dtFactura = null;

        oAntDocumentosPago = new cAntDocumentosPago(ref oConn);
        oAntDocumentosPago.CodPagos = hdd_cod_pago.Value;
        oAntDocumentosPago.CodDocumento = hdd_cod_documento.Value;
        oAntDocumentosPago.CodFactura = pCodFactura;
        oAntDocumentosPago.CodSAP = pCodSAP;
        //oAntDocumentosPago.NombreDeudor = sRazonSocial;
        if (!string.IsNullOrEmpty(pNumOperacion))
          oAntDocumentosPago.NumDocumento = pNumOperacion;
        if (!string.IsNullOrEmpty(pCodBanco))
          oAntDocumentosPago.CodBanco = pCodBanco;
        if (!string.IsNullOrEmpty(pFchDocumento))
          oAntDocumentosPago.FchDocumento = pFchDocumento;
        oAntDocumentosPago.NumGuiaDespacho = pGuiaDespacho;
        oAntDocumentosPago.importe = pImporte;
        oAntDocumentosPago.Accion = (string.IsNullOrEmpty(hdd_cod_documento.Value) ? "CREAR" : "EDITAR");
        oAntDocumentosPago.Put();

        string pCodDocumento = oAntDocumentosPago.CodDocumento;

        bEstadoValija = true;

        onLoadGrid();

        oConn.Close();

        hdd_cod_documento.Value = string.Empty;
        cmb_centrodistribucion.Enabled = false;
        cmb_documento.Enabled = false;

        txt_codigosap.Text = string.Empty;
        txt_codigosap.Enabled = true;

        //txt_razon_social.Text = string.Empty;
        txt_num_documento.Text = string.Empty;
        cmb_bancos.SelectedValue = string.Empty;

        fch_documento.Text = string.Empty;
        hdd_fchdocument.Value = string.Empty;

        cmb_guiadespacho.Enabled = true;
        cmb_guiadespacho.Items.Clear();
        hddGuiasDespacho.Value = string.Empty;

        cmb_facturas.Items.Clear();
        cmb_facturas.Enabled = true;
        hdd_facturas.Value = string.Empty;

        txt_importe.Text = string.Empty;

      }
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
            if (bEstadoValija)
              btnCerrarValija.Visible = true;

            lblCantidad.Text = dt.Rows.Count.ToString();
            hdd_cantidad_doc.Value = dt.Rows.Count.ToString();
            lblMonto.Text = dt.Compute("SUM(importe)", string.Empty).ToString();
            hdd_importe_total.Value = dt.Compute("SUM(importe)", string.Empty).ToString();

          }
        }
        dt = null;

        oConn.Close();
      }
    }

    protected void gdPagos_SelectedIndexChanged(object sender, EventArgs e)
    {
      string pCodDocumento = gdPagos.SelectedDataKey.Value.ToString();
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        cAntDocumentosPago oAntDocumentosPago = new cAntDocumentosPago(ref oConn);
        oAntDocumentosPago.CodDocumento = pCodDocumento;
        DataTable dtDocPago = oAntDocumentosPago.GetDocFacturas();

        if (dtDocPago != null)
        {
          if (dtDocPago.Rows.Count > 0)
          {
            hdd_cod_documento.Value = dtDocPago.Rows[0]["cod_documento"].ToString();
            txt_codigosap.Text = dtDocPago.Rows[0]["cod_sap"].ToString();
            txt_codigosap.Enabled = false;
            //txt_razon_social.Text = dtDocPago.Rows[0]["nom_deudor"].ToString();
            txt_num_documento.Text = dtDocPago.Rows[0]["num_documento"].ToString();
            cmb_bancos.SelectedValue = dtDocPago.Rows[0]["cod_banco"].ToString();

            cmb_guiadespacho.Items.Clear();
            cGuiasFacturas oGuiasFacturas = new cGuiasFacturas(ref oConn);
            oGuiasFacturas.NKeyCliente = oIsUsuario.CodNkey;
            oGuiasFacturas.NCodigoDeudor = dtDocPago.Rows[0]["cod_sap"].ToString();
            DataTable dtGuias = oGuiasFacturas.GetGuiaDespacho();
            if (dtGuias != null)
            {
              cmb_guiadespacho.Items.Add(new ListItem("<< Seleccione Guia Despacho >>", string.Empty));
              foreach (DataRow oRow in dtGuias.Rows)
              {
                cmb_guiadespacho.Items.Add(new ListItem(oRow["guidespacho"].ToString(), oRow["guidespacho"].ToString()));
              }
            }
            dtGuias = null;
            cmb_guiadespacho.Items.FindByValue(dtDocPago.Rows[0]["num_guia_despacho"].ToString()).Selected = true;
            cmb_guiadespacho.Enabled = false;

            cmb_facturas.Items.Clear();
            cGuiasFacturas Facturas = new cGuiasFacturas(ref oConn);
            Facturas.GuiaDespacho = dtDocPago.Rows[0]["num_guia_despacho"].ToString();
            DataTable dtFacturas = Facturas.GetFacturas();
            if (dtFacturas != null)
            {
              cmb_facturas.Items.Add(new ListItem("<< Seleccione Factura >>", string.Empty));
              foreach (DataRow oRow in dtFacturas.Rows)
              {
                cmb_facturas.Items.Add(new ListItem(oRow["nnumerofactura"].ToString(), oRow["nnumerofactura"].ToString() + "|" + oRow["nmontofactura"].ToString()));
              }
            }
            dtFacturas = null;
            cmb_facturas.SelectedValue = (dtDocPago.Rows[0]["num_factura"].ToString() + "|" + dtDocPago.Rows[0]["valor_factura"].ToString());
            //cmb_facturas.Enabled = false;

            fch_documento.Text = dtDocPago.Rows[0]["fch_documento"].ToString();
            txt_importe.Text = dtDocPago.Rows[0]["importe"].ToString();

            btnCerrarValija.Visible = false;
            btnCancelarUpdate.Visible = true;
          }
        }
        dtDocPago = null;


        oConn.Close();
      }

    }

    protected void gdPagos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
      string pCodDocumento = gdPagos.DataKeys[e.RowIndex].Value.ToString();

      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        string nCodFactura = string.Empty;
        string nImporte = string.Empty;
        cAntDocumentosPago oDocumentosPago = new cAntDocumentosPago(ref oConn);
        oDocumentosPago.CodDocumento = pCodDocumento;
        DataTable dt = oDocumentosPago.Get();
        if (dt != null)
        {
          if (dt.Rows.Count > 0)
          {
            nCodFactura = dt.Rows[0]["cod_factura"].ToString();
            nImporte = dt.Rows[0]["importe"].ToString();
          }
        }
        dt = null;

        string nSaldo = string.Empty;
        cAntFactura oFactura = new cAntFactura(ref oConn);
        oFactura.CodFactura = nCodFactura;
        dt = oFactura.Get();
        if (dt != null)
        {
          if (dt.Rows.Count > 0)
          {
            nSaldo = dt.Rows[0]["saldo_factura"].ToString();
          }
        }
        dt = null;

        oFactura.SaldoFactura = (int.Parse(nImporte) + int.Parse(nSaldo)).ToString();
        oFactura.Accion = "EDITAR";
        oFactura.Put();

        oDocumentosPago.Accion = "ELIMINAR";
        oDocumentosPago.Put();

        oConn.Close();
      }

      onLoadGrid();

    }

    protected void gdPagos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
      gdPagos.PageIndex = e.NewPageIndex;
      onLoadGrid();
    }

    protected void btnCancelarUpdate_Click(object sender, EventArgs e)
    {
      hdd_cod_documento.Value = string.Empty;
      cmb_centrodistribucion.Enabled = false;
      cmb_documento.Enabled = false;

      txt_codigosap.Text = string.Empty;
      txt_codigosap.Enabled = true;

      //txt_razon_social.Text = string.Empty;
      txt_num_documento.Text = string.Empty;
      cmb_bancos.SelectedValue = string.Empty;

      fch_documento.Text = string.Empty;
      hdd_fchdocument.Value = string.Empty;

      cmb_guiadespacho.Enabled = true;
      cmb_guiadespacho.Items.Clear();
      hddGuiasDespacho.Value = string.Empty;

      cmb_facturas.Items.Clear();
      cmb_facturas.Enabled = true;
      hdd_facturas.Value = string.Empty;

      txt_importe.Text = string.Empty;

      btnCancelarUpdate.Visible = false;

      onLoadGrid();
    }

    protected void btnCerrarValija_Click(object sender, EventArgs e)
    {
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        cAntPagos oPagos = new cAntPagos(ref oConn);
        oPagos.CodPagos = hdd_cod_pago.Value;
        oPagos.CantDocumentos = hdd_cantidad_doc.Value;
        oPagos.ImporteTotal = hdd_importe_total.Value;
        oPagos.Estado = "C";
        oPagos.Accion = "EDITAR";
        oPagos.Put();
      }
      oConn.Close();

      Response.Redirect("pagos_antalis.aspx");
    }

    protected void gdPagos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (e.Row.RowType == DataControlRowType.DataRow)
      {
        if (!bEstadoValija)
        {
          gdPagos.HeaderRow.Cells[0].Visible = false;
          e.Row.Cells[0].Visible = false;
          gdPagos.HeaderRow.Cells[1].Visible = false;
          e.Row.Cells[1].Visible = false;
        }
        if (e.Row.Cells[3].Text.ToString() != "&nbsp;")
        {
          DBConn oConn = new DBConn();
          if (oConn.Open())
          {
            cAntBancos oBancos = new cAntBancos(ref oConn);
            oBancos.NKeyBanco = e.Row.Cells[3].Text.ToString();
            DataTable dt = oBancos.Get();
            if (dt != null)
            {
              if (dt.Rows.Count > 0)
              {
                e.Row.Cells[3].Text = e.Row.Cells[3].Text.ToString() + " - " + dt.Rows[0]["snombre"].ToString();
              }
            }
            dt = null;

          }
          oConn.Close();
        }
      }
    }
  }

  public class cGuiasDespacho
  {
    public string guiasdespacho { get; set; }
  }

  public class cFacturas
  {
    public string nKeyFactura { get; set; }
    public string nNumeroFactura { get; set; }
    public string nMontoFactura { get; set; }
  }
}
