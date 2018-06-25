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
          txt_fecha_recepcion.Text = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString();
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
              cmb_bancos.Items.Add(new ListItem(oRow["snombre"].ToString(), oRow["nkey_banco"].ToString()));
            }
          }
          dtBancos = null;

          if (!string.IsNullOrEmpty(hdd_cod_pago.Value))
          {
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
                txt_fecha_recepcion.Text = dtPagos.Rows[0]["fech_recepcion"].ToString();
                //txt_codigosap.Text = dtPagos.Rows[0]["cod_sap"].ToString();
                //txt_codigosap.Enabled = false;
                //txt_razon_social.Text = dtPagos.Rows[0]["nom_deudor"].ToString();
                //txt_razon_social.Enabled = false;
                //cmb_guiadespacho.Items.Add(new ListItem("<< Seleccione Guia Despacho >>", string.Empty));

                //cGuiasFacturas oGuiasFacturas = new cGuiasFacturas(ref oConn);
                //oGuiasFacturas.NKeyCliente = oIsUsuario.CodNkey;
                //oGuiasFacturas.NCodigoDeudor = txt_codigosap.Text;
                //DataTable dtGuias = oGuiasFacturas.GetGuiaDespacho();
                //if (dtGuias != null)
                //{

                //  foreach (DataRow oRow in dtGuias.Rows)
                //  {
                //    cmb_guiadespacho.Items.Add(new ListItem(oRow["guidespacho"].ToString(), oRow["guidespacho"].ToString()));
                //  }
                //}

              }
            }
            dtPagos = null;

            onLoadGrid();
          }

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
                  oHtmControl.Controls.Add(new LiteralControl("<li><a href='../antalis/pagos_antalis.aspx'>Pagos</a></li>"));
                if (oRow["cod_rol"].ToString() == "2")
                  oHtmControl.Controls.Add(new LiteralControl("<li><a href='../antalis/validacion_pagos.aspx'>Validación de Pago</a></li>"));
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
              oHtmControl.Controls.Add(new LiteralControl("<li><a href=\"" + oRow["url_consulta_new"].ToString() + "\">" + oRow["nom_consulta"].ToString() + "</a></li>"));
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
      string pFchRecepcion = txt_fecha_recepcion.Text;
      string pCodSAP = txt_codigosap.Text;
      string sRazonSocial = txt_razon_social.Text;
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

        cAntDocumentosPago oAntDocumentosPago = new cAntDocumentosPago(ref oConn);
        oAntDocumentosPago.CodPagos = pCodPago;
        oAntDocumentosPago.CodSAP = pCodSAP;
        oAntDocumentosPago.NombreDeudor = sRazonSocial;
        oAntDocumentosPago.NumDocumento = pNumOperacion;
        oAntDocumentosPago.CodBanco = pCodBanco;
        oAntDocumentosPago.FchDocumento = pFchDocumento;
        oAntDocumentosPago.NumGuiaDespacho = pGuiaDespacho;
        oAntDocumentosPago.importe = pImporte;
        oAntDocumentosPago.Accion = "CREAR";
        oAntDocumentosPago.Put();

        string pCodDocumento = oAntDocumentosPago.CodDocumento;

        cAntFactura oFactura = new cAntFactura(ref oConn);
        oFactura.NumFactura = pNumFactura;
        oFactura.CodDocumento = pCodDocumento;
        oFactura.ValorFactura = pValor;
        oFactura.SaldoFactura = (int.Parse(pValor) - int.Parse(pImporte)).ToString();
        oFactura.Accion = "CREAR";
        oFactura.Put();

        string pCodFactura = oFactura.CodFactura;

        onLoadGrid();

        oConn.Close();

        cmb_centrodistribucion.Items.FindByValue(string.Empty).Selected = true;
        cmb_documento.Items.FindByValue(string.Empty).Selected = true;
        txt_codigosap.Text = string.Empty;
        txt_razon_social.Text = string.Empty;
        txt_num_documento.Text = string.Empty;
        //cmb_bancos.Items.FindByValue(string.Empty).Selected = true;
        hdd_fchdocument.Value = string.Empty;
        hddGuiasDespacho.Value = string.Empty;
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
        gdPagos.DataSource = oDocumentosPago.GetDocFacturas();
        gdPagos.DataBind();

        oConn.Close();
      }
    }

    protected void gdPagos_SelectedIndexChanged(object sender, EventArgs e)
    {
      string pCodDocumento = gdPagos.SelectedDataKey.Value.ToString();
      DBConn oConn = new DBConn();
      if (oConn.Open()) {
        cAntDocumentosPago oAntDocumentosPago = new cAntDocumentosPago(ref oConn);
        oAntDocumentosPago.CodDocumento = pCodDocumento;
        DataTable dtDocPago = oAntDocumentosPago.GetDocFacturas();

        if (dtDocPago != null) {
          if (dtDocPago.Rows.Count > 0) {
            hdd_cod_documento.Value = dtDocPago.Rows[0]["cod_documento"].ToString();
            txt_codigosap.Text = dtDocPago.Rows[0]["cod_sap"].ToString();
            txt_codigosap.Enabled = false;
            txt_razon_social.Text = dtDocPago.Rows[0]["nom_deudor"].ToString();
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
            cmb_facturas.Items.FindByValue(dtDocPago.Rows[0]["num_factura"].ToString() + "|" + dtDocPago.Rows[0]["valor_factura"].ToString()).Selected = true;
            cmb_facturas.Enabled = false;

            fch_documento.Text = dtDocPago.Rows[0]["fch_documento"].ToString();
            txt_importe.Text = dtDocPago.Rows[0]["importe"].ToString();

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
      if (oConn.Open()) {
        cAntFactura oFactura = new cAntFactura(ref oConn);
        oFactura.CodDocumento = pCodDocumento;
        oFactura.Accion = "ELIMINAR";
        oFactura.Put();

        cAntDocumentosPago oDocumentosPago = new cAntDocumentosPago(ref oConn);
        oDocumentosPago.CodDocumento = pCodDocumento;
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
