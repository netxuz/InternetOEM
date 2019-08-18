using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.IO;
using System.Configuration;

using OnlineServices.Conn;
using OnlineServices.Method;
using OnlineServices.Reporting;

using OnlineServices.Antalis;
using OnlineServices.SystemData;
using System.Web.Services;
using SelectPdf;

namespace ICommunity.Antalis
{
  public partial class ingreso_pagos : System.Web.UI.Page
  {
    private OnlineServices.Method.Web oWeb = new OnlineServices.Method.Web();
    private OnlineServices.Method.Usuario oIsUsuario;
    bool bEstadoValija = false;

    private void BindDummyRow()
    {
      DataTable dummy = new DataTable();
      dummy.Columns.Add("num_nc");
      dummy.Columns.Add("aplicacion_nota_credito");
      dummy.Rows.Add();
      GridNC.DataSource = dummy;
      GridNC.DataBind();
    }

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
        BindDummyRow();

        Log oLog = new Log();
        oLog.IdUsuario = oIsUsuario.CodUsuario;
        oLog.ObsLog = "INGRESO DE PAGOS ANTALIS";
        oLog.CodEvtLog = "2";
        oLog.AppLog = "ANTALIS";
        oLog.putLog();

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

          cmb_cliente.Items.Add(new ListItem("<< Seleccione Cliente >>", string.Empty));
          SysClienteUsuario oClienteUsuario = new SysClienteUsuario(ref oConn);
          oClienteUsuario.CodUsuario = oIsUsuario.CodUsuario;
          DataTable dt = oClienteUsuario.Get();
          if (dt != null)
          {
            foreach (DataRow dRow in dt.Rows)
            {
              cmb_cliente.Items.Add(new ListItem(dRow["snombre"].ToString(), dRow["nkey_user"].ToString()));
            }
          }
          dt = null;

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
                cmb_cliente.Items.FindByValue(dtPagos.Rows[0]["nkey_cliente"].ToString()).Selected = true;
                cmb_cliente.Enabled = false;
                cmb_centrodistribucion.Items.FindByValue(dtPagos.Rows[0]["cod_centrodist"].ToString()).Selected = true;
                cmb_centrodistribucion.Enabled = false;
                cmb_documento.Items.FindByValue(dtPagos.Rows[0]["cod_tipo_pago"].ToString()).Selected = true;
                cmb_documento.Enabled = false;

                string pCodTipoPago = dtPagos.Rows[0]["cod_tipo_pago"].ToString();

                if ((pCodTipoPago == "1") || (pCodTipoPago == "2"))
                  txt_cta_cte.Enabled = true;
                else
                  txt_cta_cte.Enabled = false;

                if (pCodTipoPago == "3")
                  txt_num_documento.Enabled = false;
                else
                  txt_num_documento.Enabled = true;

                if ((pCodTipoPago == "3") || (pCodTipoPago == "5") || (pCodTipoPago == "6"))
                  cmb_bancos.Enabled = false;


                txt_fecha_recepcion.Value = dtPagos.Rows[0]["fech_recepcion"].ToString();
                lbl_fecha_recepcion.Text = dtPagos.Rows[0]["fech_recepcion"].ToString();
                bEstadoValija = ((dtPagos.Rows[0]["estado"].ToString() != "C") ? true : false);
                if (dtPagos.Rows[0]["estado"].ToString() == "C")
                {
                  btnImprimirValija.Visible = true;
                  btnAbrirValija.Visible = true;
                }

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

          //cCliente oCliente = new cCliente(ref oConn);
          //oCliente.CodNkey = oIsUsuario.CodNkey;
          //DataTable dt = oCliente.GeCliente();
          //if (dt != null)
          //{
          //  if (dt.Rows.Count > 0)
          //  {
          //    lblRazonSocial.Text = dt.Rows[0]["cliente"].ToString();
          //  }
          //}
          //dt = null;



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
            oGuiasDespacho.guiasdespacho = oRow["guiadespacho"].ToString();
            details.Add(oGuiasDespacho);
          }
        }

        Log oLog = new Log();
        oLog.ObsLog = oGuiasFacturas.Error;
        oLog.CodEvtLog = "9";
        oLog.AppLog = "ERRROR - getGuiasDespacho";
        oLog.putLog();

        dtGuias = null;
        oConn.Close();
      }
      return details.ToArray();

    }

    [WebMethod()]
    public static cFacturas[] getFacturas(string sGuiaDespacho, string nkeycliente)
    {
      List<cFacturas> details = new List<cFacturas>();

      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        cGuiasFacturas oGuiasFacturas = new cGuiasFacturas(ref oConn);
        oGuiasFacturas.NKeyCliente = nkeycliente;
        oGuiasFacturas.GuiaDespacho = sGuiaDespacho;
        DataTable dtFacturas = oGuiasFacturas.GetFacturas();
        if (dtFacturas != null)
        {
          foreach (DataRow oRow in dtFacturas.Rows)
          {
            cFacturas oFacturas = new cFacturas();
            //oFacturas.nKeyFactura = oRow["nkey_factura"].ToString();
            oFacturas.nKeyCliente = oRow["nkey_cliente"].ToString();
            oFacturas.nNumeroFactura = oRow["nnumerofactura"].ToString();
            oFacturas.nMontoFactura = string.Format("{0:N0}", int.Parse(oRow["nmontofactura"].ToString()));
            oFacturas.nSaldo = string.Format("{0:N0}", int.Parse(oRow["saldo"].ToString()));
            details.Add(oFacturas);
          }
        }
        dtFacturas = null;
        oConn.Close();
      }
      return details.ToArray();

    }

    [WebMethod]
    public static cExiste[] getValida(string sCodNumDocumento, string sCodBanco, string sCtacte)
    {
      List<cExiste> details = new List<cExiste>();

      cExiste oExiste = new cExiste();
      oExiste.bExiste = "NOEXISTE";

      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        cAntDocumentosPago oDocumentosPago = new cAntDocumentosPago(ref oConn);
        oDocumentosPago.CodBanco = sCodBanco;
        oDocumentosPago.NumDocumento = sCodNumDocumento;
        oDocumentosPago.CuentaCorriente = sCtacte;
        DataTable dt = oDocumentosPago.Get();
        if (dt != null)
          if (dt.Rows.Count > 0)
            oExiste.bExiste = "EXISTE";
        oConn.Close();
      }
      details.Add(oExiste);
      return details.ToArray();
    }

    [WebMethod]
    public static string getDeudor(string nKeyCliente, string nCodigoDeudor)
    {

      string sValue = string.Empty;

      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        cDeudor oDeudor = new cDeudor(ref oConn);
        oDeudor.NCodigoDeudor = nCodigoDeudor;
        oDeudor.NKeyCliente = nKeyCliente;
        DataTable dt = oDeudor.Get();
        if (dt != null)
          if (dt.Rows.Count > 0)
            sValue = dt.Rows[0]["sNombre"].ToString();

        if (string.IsNullOrEmpty(sValue))
        {
          if (!string.IsNullOrEmpty(oDeudor.Error))
            sValue = oDeudor.Error;
          else
            sValue = "SIN INFORMACION";
        }
        oConn.Close();
      }
      return sValue;
    }

    [WebMethod()]
    public static cNotaCredito[] getNotaCredito(string nkeycliente, string ncodigodeudor)
    {
      List<cNotaCredito> details = new List<cNotaCredito>();

      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        cAntNotaCreditoNoAplicada cAntNotaCreditoNoAplicada = new cAntNotaCreditoNoAplicada(ref oConn);
        cAntNotaCreditoNoAplicada.NKeyCliente = nkeycliente;
        cAntNotaCreditoNoAplicada.NCodigoDeudor = ncodigodeudor;
        DataTable dtNotaCreditoNoAplicada = cAntNotaCreditoNoAplicada.Get();
        if (dtNotaCreditoNoAplicada != null)
        {
          foreach (DataRow oRow in dtNotaCreditoNoAplicada.Rows)
          {
            cNotaCredito oNotaCredito = new cNotaCredito();
            oNotaCredito.nKeyCliente = oRow["nkey_cliente"].ToString();
            oNotaCredito.nNumeroNotaCredito = oRow["nNumeroNotaCredito"].ToString();
            oNotaCredito.nMontoNotaCredito = string.Format("{0:N0}", int.Parse(oRow["nMontoNotaCredito"].ToString()));
            oNotaCredito.sExiste = ((int.Parse(oRow["existe"].ToString()) > 0) ? "V" : "F");
            oNotaCredito.nSaldo = string.Format("{0:N0}", int.Parse(oRow["saldo"].ToString()));
            details.Add(oNotaCredito);
          }
        }
        dtNotaCreditoNoAplicada = null;
        oConn.Close();
      }
      return details.ToArray();

    }

    [WebMethod()]
    public static string wsUpdateTable(string CodDocumento) {

      DataTable dt;
      DataSet ds = new DataSet();
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        cAntNotaCredito oNotaCredito = new cAntNotaCredito(ref oConn);
        oNotaCredito.CodDocumento = CodDocumento;
        dt = oNotaCredito.GetDocNotaCredito();
        
        oConn.Close();
        ds.Merge(dt);
      }
      return ds.GetXml();
    }

    protected void btnIngresarImportes_Click(object sender, EventArgs e)
    {

      string lngCodFacturaAnterior = string.Empty;
      string pCodCentroDist = cmb_centrodistribucion.SelectedValue;
      string pCodTipoPago = cmb_documento.SelectedValue;
      string pFchRecepcion = txt_fecha_recepcion.Value;
      string pCodSAP = txt_codigosap.Text;
      string sNomDeudor = lblNomDeudor.Text;
      string pNumOperacion = txt_num_documento.Text;
      string sCuentaCorriente = txt_cta_cte.Text;
      string pCodBanco = cmb_bancos.SelectedValue;
      string pFchDocumento = hdd_fchdocument.Value;
      string pGuiaDespacho = hddGuiasDespacho.Value;
      string pNumGuiaDespacho = txt_num_guia_despacho.Text;
      bool bIndNewGuia = (!string.IsNullOrEmpty(pNumGuiaDespacho) ? true : false);

      string pNumFactura = hdd_num_factura.Text;
      string pAplicacionPagoFactura = (!string.IsNullOrEmpty(txt_aplicacion_pago_factura.Text) ? txt_aplicacion_pago_factura.Text.Replace(".", string.Empty) : "0");
      string pNumNotaCredito = hddNotaCredito.Value;
      string pAplicacionNotaCredito = (!string.IsNullOrEmpty(txt_aplicacion_nota_credito.Text) ? txt_aplicacion_nota_credito.Text.Replace(".", string.Empty) : "0");
      string pApliTtNotaCredito;

      if (!string.IsNullOrEmpty(oWeb.GetData("hdd_val_tt_nc")))
      {
        pApliTtNotaCredito = (!string.IsNullOrEmpty(pAplicacionNotaCredito) ? (int.Parse(pAplicacionNotaCredito) + int.Parse(oWeb.GetData("hdd_val_tt_nc"))) : int.Parse(oWeb.GetData("hdd_val_tt_nc"))).ToString();
      }
      else
      {
        pApliTtNotaCredito = (!string.IsNullOrEmpty(pAplicacionNotaCredito) ? pAplicacionNotaCredito : "0");
      }

      string pSaldoNotaCredito = pAplicacionNotaCredito;
      string pValor = hdd_nuevo_saldo_factura.Value;

      string pImporte = txt_importe.Text.Replace(".", "").Replace(",", "");
      string sValorFactura = string.Empty;

      int iCantNotaCredito = (string.IsNullOrEmpty(hddCantNotaCredito.Value) ? 0 : int.Parse(hddCantNotaCredito.Value));


      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        if (string.IsNullOrEmpty(hdd_cod_pago.Value))
        {
          cAntPagos oPagos = new cAntPagos(ref oConn);
          oPagos.CodUsuario = oIsUsuario.CodUsuario;
          oPagos.NKeyCliente = cmb_cliente.SelectedValue;
          oPagos.CodCentroDist = pCodCentroDist;
          oPagos.CodTipoPago = pCodTipoPago;
          oPagos.FechRecepcion = pFchRecepcion;
          oPagos.Estado = "A";
          oPagos.Accion = "CREAR";
          oPagos.Put();

          if (!string.IsNullOrEmpty(oPagos.Error))
          {
            Response.Write("[Ingresar Valija] Se ha encontrado el siguiente error : " + oPagos.Error);
            Response.End();
          }

          string pCodPago = oPagos.CodPagos;
          hdd_cod_pago.Value = pCodPago;
          lblValija.Text = "# Valija " + hdd_cod_pago.Value;
        }

        cAntFactura oFactura;
        DataTable dtFactura;
        cAntNotaCredito oNotaCredito;
        DataTable dtNotaCredito;
        cAntDocumentosPago oAntDocumentosPago;
        string sImporteFactura = string.Empty;
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

                  if (int.Parse(dtFactura.Rows[0]["valor_factura"].ToString()) < (int.Parse(dt.Rows[0]["aplicacion_pago_factura"].ToString()) + int.Parse(dt.Rows[0]["aplicacion_nota_credito"].ToString())))
                  {
                    oFactura.SaldoFactura = dtFactura.Rows[0]["valor_factura"].ToString();
                  }
                  else
                  {
                    if (int.Parse(dtFactura.Rows[0]["valor_factura"].ToString()) < (int.Parse(dt.Rows[0]["importe_factura"].ToString()) + int.Parse(dt.Rows[0]["aplicacion_pago_factura"].ToString()) + int.Parse(dt.Rows[0]["aplicacion_nota_credito"].ToString())))
                    {
                      oFactura.SaldoFactura = dtFactura.Rows[0]["valor_factura"].ToString();
                    }
                    else
                    {
                      oFactura.SaldoFactura = (int.Parse(dt.Rows[0]["importe_factura"].ToString()) + int.Parse(dt.Rows[0]["aplicacion_pago_factura"].ToString()) + int.Parse(dt.Rows[0]["aplicacion_nota_credito"].ToString())).ToString();
                    }

                  }

                  //oFactura.SaldoFactura = (int.Parse(dtFactura.Rows[0]["saldo_factura"].ToString()) + int.Parse(dt.Rows[0]["aplicacion_pago_factura"].ToString()) + int.Parse(dt.Rows[0]["aplicacion_nota_credito"].ToString())).ToString();
                  oFactura.Accion = "EDITAR";
                  oFactura.Put();

                  if (!string.IsNullOrEmpty(oFactura.Error))
                  {
                    Response.Write("[Ingresar / Factura Editar Saldo 1] Se ha encontrado el siguiente error : " + oFactura.Error);
                    Response.End();
                  }
                }
              }
              dtFactura = null;

              //if (!string.IsNullOrEmpty(dt.Rows[0]["cod_nota_credito"].ToString()))
              //{
              oNotaCredito = new cAntNotaCredito(ref oConn);
              oNotaCredito.CodDocumento = dt.Rows[0]["cod_documento"].ToString();
              dtNotaCredito = oNotaCredito.GetDocNotaCredito();
              if (dtNotaCredito != null)
              {
                foreach (DataRow oRow in dtNotaCredito.Rows)
                {
                  oNotaCredito.CodNotaCredito = oRow["cod_nota_credito"].ToString();
                  DataTable dtNC = oNotaCredito.Get();
                  if (dtNC != null)
                  {
                    if (dtNC.Rows.Count > 0)
                    {
                      oNotaCredito.SaldoNotaCredito = (int.Parse(dtNC.Rows[0]["saldo_nota_credito"].ToString()) + int.Parse(oRow["aplicacion_nota_credito"].ToString())).ToString();
                      oNotaCredito.Accion = "EDITAR";
                      oNotaCredito.Put();

                      if (!string.IsNullOrEmpty(oNotaCredito.Error))
                      {
                        Response.Write("[Ingresar / Nota de Credito Editar Saldo 1] Se ha encontrado el siguiente error : " + oNotaCredito.Error);
                        Response.End();
                      }
                    }
                  }
                  dtNC = null;

                  oNotaCredito.Accion = "ELIMINAR";
                  oNotaCredito.PutDocNotaCredito();
                  if (!string.IsNullOrEmpty(oNotaCredito.Error))
                  {
                    Response.Write("[Ingresar / Nota de Credito Elimina - EDITAR] Se ha encontrado el siguiente error : " + oNotaCredito.Error);
                    Response.End();
                  }

                }
              }
              dtNotaCredito = null;
              //}
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
            oFactura.SaldoFactura = pValor;
            oFactura.CodFactura = dtFactura.Rows[0]["cod_factura"].ToString();
            oFactura.Accion = "EDITAR";
            oFactura.Put();

            if (!string.IsNullOrEmpty(oFactura.Error))
            {
              Response.Write("[Ingresar / Factura Editar Saldo 2] Se ha encontrado el siguiente error : " + oFactura.Error);
              Response.End();
            }
          }
          else
          {
            if (!bIndNewGuia)
            {
              cGuiasFacturas oFacturaDbt = new cGuiasFacturas(ref oConn);
              oFacturaDbt.NumeroFactura = pNumFactura;
              oFacturaDbt.NKeyCliente = cmb_cliente.SelectedValue;
              oFacturaDbt.GuiaDespacho = pGuiaDespacho;
              DataTable dtDacturaDbt = oFacturaDbt.getFactura();
              if (dtDacturaDbt != null)
              {
                if (dtDacturaDbt.Rows.Count > 0)
                {
                  oFactura.ValorFactura = dtDacturaDbt.Rows[0]["saldo"].ToString();
                  oFactura.SaldoFactura = pValor;
                  oFactura.Accion = "CREAR";
                  oFactura.Put();

                  if (!string.IsNullOrEmpty(oFactura.Error))
                  {
                    Response.Write("[Ingresar / Factura Crear] Se ha encontrado el siguiente error : " + oFactura.Error);
                    Response.End();
                  }
                  pCodFactura = oFactura.CodFactura;
                }
              }
              dtDacturaDbt = null;
            }
            else
            {
              oFactura.ValorFactura = txt_valor_factura.Text;
              oFactura.SaldoFactura = pValor;
              oFactura.Accion = "CREAR";
              oFactura.Put();

              if (!string.IsNullOrEmpty(oFactura.Error))
              {
                Response.Write("[Ingresar / Factura Crear - nueva guia] Se ha encontrado el siguiente error : " + oFactura.Error);
                Response.End();
              }
              pCodFactura = oFactura.CodFactura;
            }
          }
        }
        dtFactura = null;


        string pCodNC = string.Empty;
        if (!string.IsNullOrEmpty(pNumNotaCredito))
        {

          oNotaCredito = new cAntNotaCredito(ref oConn);
          oNotaCredito.NumNotaCredito = pNumNotaCredito;
          dtNotaCredito = oNotaCredito.Get();

          if (dtNotaCredito != null)
          {
            if (dtNotaCredito.Rows.Count > 0)
            {
              pCodNC = dtNotaCredito.Rows[0]["cod_nota_credito"].ToString();
              if (int.Parse(dtNotaCredito.Rows[0]["saldo_nota_credito"].ToString()) < int.Parse(pAplicacionNotaCredito))
              {
                oNotaCredito.SaldoNotaCredito = "0";
              }
              else
              {
                oNotaCredito.SaldoNotaCredito = (int.Parse(dtNotaCredito.Rows[0]["saldo_nota_credito"].ToString()) - int.Parse(pAplicacionNotaCredito)).ToString();
              }
              oNotaCredito.CodNotaCredito = dtNotaCredito.Rows[0]["cod_nota_credito"].ToString();
              oNotaCredito.Accion = "EDITAR";
              oNotaCredito.Put();

              if (!string.IsNullOrEmpty(oNotaCredito.Error))
              {
                Response.Write("[Ingresar / Nota Credito Editar Saldo 1] Se ha encontrado el siguiente error : " + oNotaCredito.Error);
                Response.End();
              }
            }
            else
            {
              cAntNotaCreditoNoAplicada oNotaCreditoNoAplicada = new cAntNotaCreditoNoAplicada(ref oConn);
              oNotaCreditoNoAplicada.NNumeroNotaCredito = pNumNotaCredito;
              oNotaCreditoNoAplicada.NKeyCliente = cmb_cliente.SelectedValue;
              DataTable dtNotaCreditoNoAplicada = oNotaCreditoNoAplicada.GetNotaCreditoByData();
              if (dtNotaCreditoNoAplicada != null)
              {
                if (dtNotaCreditoNoAplicada.Rows.Count > 0)
                {
                  oNotaCredito.ValorNotaCredito = dtNotaCreditoNoAplicada.Rows[0]["nMontoNotaCredito"].ToString();

                  if (int.Parse(dtNotaCreditoNoAplicada.Rows[0]["nMontoNotaCredito"].ToString()) < int.Parse(pAplicacionNotaCredito))
                  {
                    oNotaCredito.SaldoNotaCredito = "0";
                  }
                  else
                  {
                    oNotaCredito.SaldoNotaCredito = (int.Parse(dtNotaCreditoNoAplicada.Rows[0]["nMontoNotaCredito"].ToString()) - int.Parse(pAplicacionNotaCredito)).ToString();
                  }

                  oNotaCredito.Accion = "CREAR";
                  oNotaCredito.Put();

                  if (!string.IsNullOrEmpty(oNotaCredito.Error))
                  {
                    Response.Write("[Ingresar / Factura Crear] Se ha encontrado el siguiente error : " + oNotaCredito.Error);
                    Response.End();
                  }
                  pCodNC = oNotaCredito.CodNotaCredito;
                }
              }
              dtNotaCreditoNoAplicada = null;
            }
          }
          dtNotaCredito = null;
        }

        oAntDocumentosPago = new cAntDocumentosPago(ref oConn);
        oAntDocumentosPago.CodPagos = hdd_cod_pago.Value;

        if (!string.IsNullOrEmpty(hdd_cod_documento.Value))
          oAntDocumentosPago.CodDocumento = hdd_cod_documento.Value;
        if (!string.IsNullOrEmpty(hdd_nod_documento.Value))
          oAntDocumentosPago.NodCodDocumento = hdd_nod_documento.Value;

        oAntDocumentosPago.CodFactura = pCodFactura;
        //if (!string.IsNullOrEmpty(pCodNotaCredito))
        //  oAntDocumentosPago.CodNotaCredito = pCodNotaCredito;

        oAntDocumentosPago.CodSAP = pCodSAP;
        oAntDocumentosPago.NombreDeudor = sNomDeudor;
        oAntDocumentosPago.CuentaCorriente = sCuentaCorriente;
        if (!string.IsNullOrEmpty(pNumOperacion))
          oAntDocumentosPago.NumDocumento = pNumOperacion;
        if (!string.IsNullOrEmpty(pCodBanco))
          oAntDocumentosPago.CodBanco = pCodBanco;
        if (!string.IsNullOrEmpty(pFchDocumento))
          oAntDocumentosPago.FchDocumento = pFchDocumento;
        oAntDocumentosPago.NumGuiaDespacho = (!bIndNewGuia ? pGuiaDespacho : pNumGuiaDespacho);

        //importe x total
        if (string.IsNullOrEmpty(hdd_nod_documento.Value))
          oAntDocumentosPago.importe = pImporte.Replace(".", string.Empty);
        //imporre x factura

        oAntDocumentosPago.ImporteFactura = pValor;
        oAntDocumentosPago.AplicacionPagoFactura = pAplicacionPagoFactura;
        //oAntDocumentosPago.SaldoNotaCredito = pSaldoNotaCredito;
        oAntDocumentosPago.AplicacionNotaCredito = pApliTtNotaCredito;

        oAntDocumentosPago.ImporteRecibido = "0";
        oAntDocumentosPago.Discrepancia = "0";
        oAntDocumentosPago.IndNewGuia = (bIndNewGuia ? "V" : "F");

        oAntDocumentosPago.Accion = (string.IsNullOrEmpty(hdd_cod_documento.Value) ? "CREAR" : "EDITAR");
        oAntDocumentosPago.Put();

        if (!string.IsNullOrEmpty(oAntDocumentosPago.Error))
        {
          Response.Write("[Ingresar Pago] Se ha encontrado el siguiente error : " + oAntDocumentosPago.Error);
          Response.End();
        }

        string pCodDocumento = oAntDocumentosPago.CodDocumento;

        if (!string.IsNullOrEmpty(pCodNC))
        {
          oNotaCredito = new cAntNotaCredito(ref oConn);
          oNotaCredito.CodDocumento = pCodDocumento;
          oNotaCredito.CodNotaCredito = pCodNC;
          oNotaCredito.AplicacionNotaCredito = pAplicacionNotaCredito;
          oNotaCredito.Accion = "CREAR";
          oNotaCredito.PutDocNotaCredito();

          if (!string.IsNullOrEmpty(oNotaCredito.Error))
          {
            Response.Write("[Ingresar Nota Credito] Se ha encontrado el siguiente error : " + oNotaCredito.Error);
            Response.End();
          }
        }

        string pCodNotaCredito;
        if (iCantNotaCredito > 0)
        {
          for (int i = 1; i <= iCantNotaCredito; i++)
          {
            if (!string.IsNullOrEmpty(oWeb.GetData("hdd_cod_nc_" + i))) {
              oNotaCredito = new cAntNotaCredito(ref oConn);
              oNotaCredito.NumNotaCredito = oWeb.GetData("hdd_cod_nc_" + i);
              dtNotaCredito = oNotaCredito.Get();

              if (dtNotaCredito != null)
              {
                if (dtNotaCredito.Rows.Count > 0)
                {
                  pCodNotaCredito = dtNotaCredito.Rows[0]["cod_nota_credito"].ToString();
                  if (int.Parse(dtNotaCredito.Rows[0]["saldo_nota_credito"].ToString()) < int.Parse(oWeb.GetData("txt_apl_nc_" + i).Replace(".", "")))
                  {
                    oNotaCredito.SaldoNotaCredito = "0";
                  }
                  else
                  {
                    oNotaCredito.SaldoNotaCredito = (int.Parse(dtNotaCredito.Rows[0]["saldo_nota_credito"].ToString()) - int.Parse(oWeb.GetData("txt_apl_nc_" + i).Replace(".", ""))).ToString();
                  }
                  oNotaCredito.CodNotaCredito = dtNotaCredito.Rows[0]["cod_nota_credito"].ToString();
                  oNotaCredito.Accion = "EDITAR";
                  oNotaCredito.Put();

                  if (!string.IsNullOrEmpty(oNotaCredito.Error))
                  {
                    Response.Write("[Ingresar / Nota Credito Editar Saldo 1] Se ha encontrado el siguiente error : " + oNotaCredito.Error);
                    Response.End();
                  }
                }
                else
                {
                  cAntNotaCreditoNoAplicada oNotaCreditoNoAplicada = new cAntNotaCreditoNoAplicada(ref oConn);
                  oNotaCreditoNoAplicada.NNumeroNotaCredito = oWeb.GetData("hdd_cod_nc_" + i);
                  oNotaCreditoNoAplicada.NKeyCliente = cmb_cliente.SelectedValue;
                  DataTable dtNotaCreditoNoAplicada = oNotaCreditoNoAplicada.GetNotaCreditoByData();
                  if (dtNotaCreditoNoAplicada != null)
                  {
                    if (dtNotaCreditoNoAplicada.Rows.Count > 0)
                    {
                      oNotaCredito.ValorNotaCredito = dtNotaCreditoNoAplicada.Rows[0]["nMontoNotaCredito"].ToString();

                      if (int.Parse(dtNotaCreditoNoAplicada.Rows[0]["nMontoNotaCredito"].ToString()) < int.Parse(oWeb.GetData("txt_apl_nc_" + i).Replace(".", "")))
                      {
                        oNotaCredito.SaldoNotaCredito = "0";
                      }
                      else
                      {
                        oNotaCredito.SaldoNotaCredito = (int.Parse(dtNotaCreditoNoAplicada.Rows[0]["nMontoNotaCredito"].ToString()) - int.Parse(oWeb.GetData("txt_apl_nc_" + i).Replace(".", ""))).ToString();
                      }

                      oNotaCredito.Accion = "CREAR";
                      oNotaCredito.Put();

                      if (!string.IsNullOrEmpty(oNotaCredito.Error))
                      {
                        Response.Write("[Ingresar / Factura Crear] Se ha encontrado el siguiente error : " + oNotaCredito.Error);
                        Response.End();
                      }
                      pCodNotaCredito = oNotaCredito.CodNotaCredito;
                    }
                  }
                  dtNotaCreditoNoAplicada = null;
                }
              }
              dtNotaCredito = null;

              oNotaCredito.CodDocumento = pCodDocumento;
              oNotaCredito.AplicacionNotaCredito = oWeb.GetData("txt_apl_nc_" + i).Replace(".", "");
              oNotaCredito.Accion = "CREAR";
              oNotaCredito.PutDocNotaCredito();

              if (!string.IsNullOrEmpty(oNotaCredito.Error))
              {
                Response.Write("[Ingresar / Nota Credito - Documento] Se ha encontrado el siguiente error : " + oNotaCredito.Error);
                Response.End();
              }
            }
          }
        }

        bEstadoValija = true;

        onLoadGrid();

        oConn.Close();

        hdd_cod_documento.Value = string.Empty;
        hdd_nod_documento.Value = string.Empty;
        cmb_centrodistribucion.Enabled = false;
        cmb_documento.Enabled = false;

        txt_codigosap.Text = string.Empty;
        txt_codigosap.Enabled = true;

        lblNomDeudor.Text = string.Empty;
        lblNomDeudor.Enabled = true;

        txt_cta_cte.Text = string.Empty;
        if ((pCodTipoPago == "1") || (pCodTipoPago == "2"))
          txt_cta_cte.Enabled = true;
        else
          txt_cta_cte.Enabled = false;

        txt_num_documento.Text = string.Empty;
        if (pCodTipoPago == "3")
          txt_num_documento.Enabled = false;
        else
          txt_num_documento.Enabled = true;

        cmb_bancos.SelectedValue = string.Empty;

        if ((pCodTipoPago == "3") || (pCodTipoPago == "5") || (pCodTipoPago == "6"))
          cmb_bancos.Enabled = false;
        else
          cmb_bancos.Enabled = true;

        fch_documento.Text = string.Empty;
        fch_documento.Enabled = true;
        hdd_fchdocument.Value = string.Empty;

        cmb_guiadespacho.Enabled = true;
        cmb_guiadespacho.Items.Clear();
        hddGuiasDespacho.Value = string.Empty;
        txt_num_guia_despacho.Text = string.Empty;

        hdd_num_factura.Text = string.Empty;
        txt_valor_factura.Text = string.Empty;

        txt_importe.Text = string.Empty;
        txt_importe.Enabled = true;

        txt_aplicacion_pago_factura.Text = string.Empty;

        cmb_nota_credito.Enabled = true;
        cmb_nota_credito.Items.Clear();
        hddNotaCredito.Value = string.Empty;
        txt_aplicacion_nota_credito.Text = string.Empty;
        txt_nuevo_saldo_factura.Text = string.Empty;
        hdd_nuevo_saldo_factura.Value = string.Empty;
        hddCantNotaCredito.Value = string.Empty;

      }

      btnIngresarImportes.Text = "INGRESAR PAGO";
      btnCancelarUpdate.Visible = false;

      Log oLog = new Log();
      oLog.IdUsuario = oIsUsuario.CodUsuario;
      oLog.ObsLog = "EJECUCION DE INGRESO DE PAGO ANTALIS VALIJA #" + hdd_cod_pago.Value;
      oLog.CodEvtLog = "2";
      oLog.AppLog = "ANTALIS";
      oLog.putLog();
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

            lblCantidad.Text = string.Format("{0:N0}", dt.Compute("COUNT(cod_documento)", " nod_cod_documento is null "));
            hdd_cantidad_doc.Value = dt.Compute("COUNT(cod_documento)", " nod_cod_documento is null ").ToString();
            lblMonto.Text = string.Format("{0:N0}", dt.Compute("SUM(importe)", string.Empty));
            hdd_importe_total.Value = dt.Compute("SUM(importe)", string.Empty).ToString();

          }
        }
        dt = null;

        oConn.Close();
      }
    }

    protected void gdPagos_SelectedIndexChanged(object sender, EventArgs e)
    {
      string pCodDocumento = gdPagos.SelectedDataKey.Values["cod_documento"].ToString();
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
            hdd_nod_documento.Value = dtDocPago.Rows[0]["nod_cod_documento"].ToString();
            txt_codigosap.Text = dtDocPago.Rows[0]["cod_sap"].ToString();
            txt_codigosap.Enabled = false;
            lblNomDeudor.Text = dtDocPago.Rows[0]["nom_deudor"].ToString();
            lblNomDeudor.Enabled = false;
            txt_cta_cte.Text = dtDocPago.Rows[0]["cuenta_corriente"].ToString();

            if ((cmb_documento.SelectedValue == "1") || (cmb_documento.SelectedValue == "2"))
              txt_cta_cte.Enabled = true;
            else
              txt_cta_cte.Enabled = false;

            if (!string.IsNullOrEmpty(dtDocPago.Rows[0]["nod_cod_documento"].ToString()))
              txt_cta_cte.Enabled = false;

            txt_num_documento.Text = dtDocPago.Rows[0]["num_documento"].ToString();
            if (!string.IsNullOrEmpty(dtDocPago.Rows[0]["nod_cod_documento"].ToString()))
              txt_num_documento.Enabled = false;
            else
              txt_num_documento.Enabled = true;

            if (cmb_documento.SelectedValue == "3")
              txt_num_documento.Enabled = false;
            else
              txt_num_documento.Enabled = true;

            cmb_bancos.SelectedValue = dtDocPago.Rows[0]["cod_banco"].ToString();
            if (!string.IsNullOrEmpty(dtDocPago.Rows[0]["nod_cod_documento"].ToString()))
              cmb_bancos.Enabled = false;
            else
              cmb_bancos.Enabled = true;

            if ((cmb_documento.SelectedValue == "3") || (cmb_documento.SelectedValue == "5") || (cmb_documento.SelectedValue == "6"))
            {
              cmb_bancos.Enabled = false;
            }

            fch_documento.Text = dtDocPago.Rows[0]["fch_documento"].ToString();
            if (!string.IsNullOrEmpty(dtDocPago.Rows[0]["nod_cod_documento"].ToString()))
              fch_documento.Enabled = false;
            else
              fch_documento.Enabled = true;

            if (!string.IsNullOrEmpty(dtDocPago.Rows[0]["nod_cod_documento"].ToString()))
            {
              oAntDocumentosPago = new cAntDocumentosPago(ref oConn);
              oAntDocumentosPago.CodDocumento = dtDocPago.Rows[0]["nod_cod_documento"].ToString();
              DataTable dt = oAntDocumentosPago.GetDocFacturas();
              if (dt != null)
              {
                if (dt.Rows.Count > 0)
                {
                  txt_importe.Text = dt.Rows[0]["importe"].ToString();
                  txt_importe.Enabled = false;
                }
              }
              dt = null;
            }
            else
            {
              txt_importe.Text = dtDocPago.Rows[0]["importe"].ToString();
              txt_importe.Enabled = true;
            }

            cmb_guiadespacho.Items.Clear();
            cGuiasFacturas oGuiasFacturas = new cGuiasFacturas(ref oConn);
            oGuiasFacturas.NKeyCliente = cmb_cliente.SelectedValue;
            oGuiasFacturas.NCodigoDeudor = dtDocPago.Rows[0]["cod_sap"].ToString();
            DataTable dtGuias = oGuiasFacturas.GetGuiaDespacho();
            if (dtGuias != null)
            {
              cmb_guiadespacho.Items.Add(new ListItem("<< Seleccione Guia Despacho >>", string.Empty));
              if (cmb_documento.SelectedValue == "6")
                cmb_guiadespacho.Items.Add(new ListItem("<< Agregar Guia Despacho >>", "x"));

              foreach (DataRow oRow in dtGuias.Rows)
              {
                cmb_guiadespacho.Items.Add(new ListItem(oRow["guiadespacho"].ToString(), oRow["guiadespacho"].ToString()));
              }
            }
            dtGuias = null;

            bool bItemExist = false;
            bool bIndNewGuia = (((!string.IsNullOrEmpty(dtDocPago.Rows[0]["ind_new_guia"].ToString())) && (dtDocPago.Rows[0]["ind_new_guia"].ToString() == "V")) ? true : false);

            foreach (ListItem oItem in cmb_guiadespacho.Items)
            {
              if (oItem.Value == dtDocPago.Rows[0]["num_guia_despacho"].ToString())
                bItemExist = true;
            }

            if (bItemExist)
              cmb_guiadespacho.SelectedValue = dtDocPago.Rows[0]["num_guia_despacho"].ToString();
            else
            {
              if (!bIndNewGuia)
              {
                cmb_guiadespacho.Items.Add(new ListItem(dtDocPago.Rows[0]["num_guia_despacho"].ToString(), dtDocPago.Rows[0]["num_guia_despacho"].ToString()));
                cmb_guiadespacho.SelectedValue = dtDocPago.Rows[0]["num_guia_despacho"].ToString();
              }
            }

            if (bIndNewGuia)
            {
              cmb_guiadespacho.SelectedValue = "x";
              txt_num_guia_despacho.Text = dtDocPago.Rows[0]["num_guia_despacho"].ToString();

              StringBuilder jsLoadNumGuia = new StringBuilder();
              jsLoadNumGuia.Append("var obj = document.getElementById(\"numguidespacho\");");
              jsLoadNumGuia.Append("obj.style.display = \"block\";");
              Page.ClientScript.RegisterStartupScript(this.GetType(), "jsNewGuia", jsLoadNumGuia.ToString(), true);

            }

            hdd_num_factura.Text = dtDocPago.Rows[0]["num_factura"].ToString();

            cAntNotaCreditoNoAplicada cAntNotaCreditoNoAplicada = new cAntNotaCreditoNoAplicada(ref oConn);
            cAntNotaCreditoNoAplicada.NKeyCliente = cmb_cliente.SelectedValue;
            cAntNotaCreditoNoAplicada.NCodigoDeudor = dtDocPago.Rows[0]["cod_sap"].ToString();
            DataTable dtNotaCreditoNoAplicada = cAntNotaCreditoNoAplicada.Get();
            if (dtNotaCreditoNoAplicada != null)
            {
              cmb_nota_credito.Items.Add(new ListItem("<< Seleccione Nota Crédito >>", string.Empty));
              foreach (DataRow oRow in dtNotaCreditoNoAplicada.Rows)
              {
                if (oRow["existe"].ToString() != "0")
                {
                  if (int.Parse(oRow["saldo"].ToString()) > 0)
                  {
                    cmb_nota_credito.Items.Add(new ListItem(oRow["nNumeroNotaCredito"].ToString(), oRow["saldo"].ToString()));
                  }
                }
                else
                {
                  cmb_nota_credito.Items.Add(new ListItem(oRow["nNumeroNotaCredito"].ToString(), oRow["nMontoNotaCredito"].ToString()));
                }
              }
            }
            dtNotaCreditoNoAplicada = null;

            //if (!string.IsNullOrEmpty(dtDocPago.Rows[0]["cod_nota_credito"].ToString()))
            //{
            StringBuilder sHtml = new StringBuilder();
            cAntNotaCredito oNotaCredito = new cAntNotaCredito(ref oConn);
            oNotaCredito.CodDocumento = pCodDocumento;
            DataTable dtNotaCredito = oNotaCredito.GetDocNotaCredito();
            if (dtNotaCredito != null)
            {
              if (dtNotaCredito.Rows.Count > 0)
              {
                int iCant = 1;
                hddCantNotaCredito.Value = dtNotaCredito.Rows.Count.ToString();
                sHtml.Append("<div class='row'><div class='col-md-3 text-center'><span># NOTA DE CREDITO:</span></div><div class='col-md-3 text-center'><span>SALDO NOTA DE CREDITO:</span></div><div class='col-md-3 text-center'><span>APLICACION NOTA DE CREDITO:</span></div><div class='col-md-1'></div></div>");

                foreach (DataRow oRow in dtNotaCredito.Rows)
                {
                  sHtml.Append("<div id='idrow_" + iCant + "' class='row'><div class='col-md-3'><input type='text' id='txt_num_nc_" + iCant + "' name='txt_num_nc_" + iCant + "' value='" + oRow["num_nc"].ToString() + "' class='form-control'></div><div class='col-md-3'><input type='hidden' id='hdd_cod_nc_" + iCant + "' name='hdd_cod_nc_" + iCant + "' value='" + oRow["num_nc"].ToString() + "'><input id='txt_saldo_nc_" + iCant + "' name='txt_saldo_nc_" + iCant + "' type='text' value='" + oRow["aplicacion_nota_credito"].ToString() + "' class='form-control'></div><div class='col-md-3'><input id='txt_apl_nc_" + iCant + "' name='txt_apl_nc_" + iCant + "' type='text' value='" + oRow["aplicacion_nota_credito"].ToString() + "'  class='form-control'></div><div class='col-md-3'><input type='button' value='-' id='idLessBtn" + iCant + "' onclick='dropRow(\"" + iCant + "\",\"" + oRow["num_nc"].ToString() + "\",\"" + oRow["aplicacion_nota_credito"].ToString() + "\")' /></div></div>");
                  iCant++;
                }
                someClass.Controls.Add(new LiteralControl(sHtml.ToString()));
                someClass.Controls.Add(new LiteralControl("<input type='hidden' id='hdd_val_tt_nc' name='hdd_val_tt_nc' value='" + dtDocPago.Rows[0]["aplicacion_nota_credito"].ToString() + "'>"));

                //bool bExisteNC = false;
                //foreach (ListItem oItem in cmb_nota_credito.Items)
                //{
                //  if (oItem.Value == dtNotaCredito.Rows[0]["num_nota_credito"].ToString())
                //    bExisteNC = true;
                //}
                //if (bExisteNC)
                //  cmb_nota_credito.SelectedValue = dtNotaCredito.Rows[0]["saldo_nota_credito"].ToString();
                //else
                //{
                //  string iValNotaCredito = (int.Parse(dtNotaCredito.Rows[0]["saldo_nota_credito"].ToString()) + int.Parse(dtDocPago.Rows[0]["aplicacion_nota_credito"].ToString())).ToString();
                //  cmb_nota_credito.Items.Add(new ListItem(dtNotaCredito.Rows[0]["num_nota_credito"].ToString(), iValNotaCredito));
                //  cmb_nota_credito.SelectedValue = iValNotaCredito;
                //}
                //hddNotaCredito.Value = dtNotaCredito.Rows[0]["num_nota_credito"].ToString();
                //txt_saldo_nota_credito.Text = string.Format("{0:N0}", int.Parse(dtNotaCredito.Rows[0]["saldo_nota_credito"].ToString()) + int.Parse(dtDocPago.Rows[0]["aplicacion_nota_credito"].ToString()));
                //txt_aplicacion_nota_credito.Text = string.Format("{0:N0}", int.Parse(dtDocPago.Rows[0]["aplicacion_nota_credito"].ToString()));
              }
            }
            dtNotaCredito = null;
            //}

            txt_valor_factura.Text = string.Format("{0:N0}", int.Parse(dtDocPago.Rows[0]["importe_factura"].ToString()) + int.Parse(dtDocPago.Rows[0]["aplicacion_pago_factura"].ToString()) + int.Parse(dtDocPago.Rows[0]["aplicacion_nota_credito"].ToString()));
            txt_aplicacion_pago_factura.Text = string.Format("{0:N0}", int.Parse(dtDocPago.Rows[0]["aplicacion_pago_factura"].ToString()));

            txt_nuevo_saldo_factura.Text = string.Format("{0:N0}", (int.Parse(dtDocPago.Rows[0]["importe_factura"].ToString()) + int.Parse(dtDocPago.Rows[0]["aplicacion_pago_factura"].ToString()) + int.Parse(dtDocPago.Rows[0]["aplicacion_nota_credito"].ToString())) - int.Parse(dtDocPago.Rows[0]["aplicacion_pago_factura"].ToString()) - int.Parse(dtDocPago.Rows[0]["aplicacion_nota_credito"].ToString()));
            hdd_nuevo_saldo_factura.Value = ((int.Parse(dtDocPago.Rows[0]["importe_factura"].ToString()) + int.Parse(dtDocPago.Rows[0]["aplicacion_pago_factura"].ToString()) + int.Parse(dtDocPago.Rows[0]["aplicacion_nota_credito"].ToString())) - int.Parse(dtDocPago.Rows[0]["aplicacion_pago_factura"].ToString()) - int.Parse(dtDocPago.Rows[0]["aplicacion_nota_credito"].ToString())).ToString();


            btnCerrarValija.Visible = false;
            btnCancelarUpdate.Visible = true;
          }
        }
        dtDocPago = null;


        oConn.Close();
      }

      btnIngresarImportes.Text = "ACTUALIZAR PAGO";

    }

    protected void gdPagos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
      string pCodDocumento = gdPagos.DataKeys[e.RowIndex].Values["cod_documento"].ToString();

      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        string nCodFactura = string.Empty;
        string nCodNotaCredito = string.Empty;
        string nImporte = string.Empty;
        string nAplicacionPagoFactura = string.Empty;
        string nSaldoNotaCredito = string.Empty;
        string nAplicacionNotaCredito = string.Empty;
        cAntDocumentosPago oDocumentosPago = new cAntDocumentosPago(ref oConn);
        oDocumentosPago.CodDocumento = pCodDocumento;
        DataTable dt = oDocumentosPago.Get();
        if (dt != null)
        {
          if (dt.Rows.Count > 0)
          {
            nCodFactura = dt.Rows[0]["cod_factura"].ToString();
            nCodNotaCredito = dt.Rows[0]["cod_nota_credito"].ToString();
            nImporte = dt.Rows[0]["importe_factura"].ToString();
            nAplicacionPagoFactura = dt.Rows[0]["aplicacion_pago_factura"].ToString();
            nSaldoNotaCredito = dt.Rows[0]["saldo_nota_credito"].ToString();
            nAplicacionNotaCredito = dt.Rows[0]["aplicacion_nota_credito"].ToString();
          }
        }
        dt = null;

        int nValorFactura = 0;
        cAntFactura oFactura = new cAntFactura(ref oConn);
        oFactura.CodFactura = nCodFactura;
        dt = oFactura.Get();
        if (dt != null)
        {
          if (dt.Rows.Count > 0)
          {
            nValorFactura = int.Parse(dt.Rows[0]["valor_factura"].ToString());
          }
        }
        dt = null;

        if (nValorFactura < (int.Parse(nAplicacionPagoFactura) + int.Parse(nAplicacionNotaCredito)))
        {
          oFactura.SaldoFactura = nValorFactura.ToString();
        }
        else {
          if (nValorFactura < (int.Parse(nImporte) + int.Parse(nAplicacionPagoFactura) + int.Parse(nAplicacionNotaCredito)))
          {
            oFactura.SaldoFactura = nValorFactura.ToString();
          }
          else {
            oFactura.SaldoFactura = (int.Parse(nImporte) + int.Parse(nAplicacionPagoFactura) + int.Parse(nAplicacionNotaCredito)).ToString();
          }
            
        }
        oFactura.Accion = "EDITAR";
        oFactura.Put();

        if (!string.IsNullOrEmpty(oFactura.Error))
        {
          Response.Write("[Eliminar / Factura Editar] Se ha encontrado el siguiente error : " + oFactura.Error);
          Response.End();
        }


        cAntNotaCredito oNotaCredito = new cAntNotaCredito(ref oConn);
        oNotaCredito.CodDocumento = pCodDocumento;
        DataTable dtNCDoc = oNotaCredito.GetDocNotaCredito();
        if (dtNCDoc != null)
        {
          if (dtNCDoc.Rows.Count > 0)
          {
            foreach (DataRow oRow in dtNCDoc.Rows)
            {
              oNotaCredito.CodNotaCredito = oRow["cod_nota_credito"].ToString();
              DataTable dtNotaCredito = oNotaCredito.Get();
              if (dtNotaCredito != null)
              {
                if (dtNotaCredito.Rows.Count > 0)
                {
                  oNotaCredito.SaldoNotaCredito = (int.Parse(dtNotaCredito.Rows[0]["saldo_nota_credito"].ToString()) + int.Parse(oRow["aplicacion_nota_credito"].ToString())).ToString();
                  oNotaCredito.Accion = "EDITAR";
                  oNotaCredito.Put();

                  if (!string.IsNullOrEmpty(oNotaCredito.Error))
                  {
                    Response.Write("[Eliminar / Nota Credito Editar] Se ha encontrado el siguiente error : " + oNotaCredito.Error);
                    Response.End();
                  }
                }
              }
              dtNotaCredito = null;

              oNotaCredito.Accion = "ELIMINAR";
              oNotaCredito.PutDocNotaCredito();

              if (!string.IsNullOrEmpty(oNotaCredito.Error))
              {
                Response.Write("[Eliminar / Nota Credito - DOC] Se ha encontrado el siguiente error : " + oNotaCredito.Error);
                Response.End();
              }
            }
          }
        }
        dtNCDoc = null;

        oDocumentosPago.Accion = "ELIMINAR";
        oDocumentosPago.Put();

        if (!string.IsNullOrEmpty(oDocumentosPago.Error))
        {
          Response.Write("[Eliminar / Pago] Se ha encontrado el siguiente error : " + oDocumentosPago.Error);
          Response.End();
        }

        oConn.Close();
      }

      bEstadoValija = true;

      onLoadGrid();

      if (pCodDocumento == hdd_cod_documento.Value)
        hdd_cod_documento.Value = string.Empty;

      txt_codigosap.Text = string.Empty;
      txt_codigosap.Enabled = true;

      lblNomDeudor.Text = string.Empty;
      lblNomDeudor.Enabled = true;

      txt_cta_cte.Text = string.Empty;
      if ((cmb_documento.SelectedValue == "1") || (cmb_documento.SelectedValue == "2"))
        txt_cta_cte.Enabled = true;
      else
        txt_cta_cte.Enabled = false;

      txt_num_documento.Text = string.Empty;
      if (cmb_documento.SelectedValue == "3")
        txt_num_documento.Enabled = false;
      else
        txt_num_documento.Enabled = true;

      cmb_bancos.SelectedValue = string.Empty;
      if ((cmb_documento.SelectedValue == "3") || (cmb_documento.SelectedValue == "5") || (cmb_documento.SelectedValue == "6"))
        cmb_bancos.Enabled = false;
      else
        cmb_bancos.Enabled = true;

      fch_documento.Text = string.Empty;
      fch_documento.Enabled = true;
      hdd_fchdocument.Value = string.Empty;

      cmb_guiadespacho.Enabled = true;
      cmb_guiadespacho.Items.Clear();
      hddGuiasDespacho.Value = string.Empty;
      txt_num_guia_despacho.Text = string.Empty;

      hdd_num_factura.Text = string.Empty;
      txt_valor_factura.Text = string.Empty;
      txt_aplicacion_pago_factura.Text = string.Empty;

      cmb_nota_credito.Enabled = true;
      cmb_nota_credito.Items.Clear();
      hddNotaCredito.Value = string.Empty;
      txt_saldo_nota_credito.Text = string.Empty;
      txt_aplicacion_nota_credito.Text = string.Empty;
      txt_nuevo_saldo_factura.Text = string.Empty;
      hdd_nuevo_saldo_factura.Value = string.Empty;
      hddCantNotaCredito.Value = string.Empty;

      txt_importe.Text = string.Empty;
      txt_importe.Enabled = true;

      btnCancelarUpdate.Visible = false;

      Log oLog = new Log();
      oLog.IdUsuario = oIsUsuario.CodUsuario;
      oLog.ObsLog = "ELIMINACION DE REGISTRO DE PAGO ANTALIS VALIJA # " + hdd_cod_pago.Value;
      oLog.CodEvtLog = "2";
      oLog.AppLog = "ANTALIS";
      oLog.putLog();
    }

    protected void gdPagos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
      bEstadoValija = true;
      gdPagos.PageIndex = e.NewPageIndex;
      onLoadGrid();
    }

    protected void btnCancelarUpdate_Click(object sender, EventArgs e)
    {
      hdd_cod_documento.Value = string.Empty;
      hdd_nod_documento.Value = string.Empty;
      cmb_centrodistribucion.Enabled = false;
      cmb_documento.Enabled = false;

      txt_codigosap.Text = string.Empty;
      txt_codigosap.Enabled = true;

      lblNomDeudor.Text = string.Empty;
      lblNomDeudor.Enabled = true;

      txt_cta_cte.Text = string.Empty;
      if ((cmb_documento.SelectedValue == "1") || (cmb_documento.SelectedValue == "2"))
        txt_cta_cte.Enabled = true;
      else
        txt_cta_cte.Enabled = false;

      txt_num_documento.Text = string.Empty;
      if (cmb_documento.SelectedValue == "3")
        txt_num_documento.Enabled = false;
      else
        txt_num_documento.Enabled = true;

      cmb_bancos.SelectedValue = string.Empty;
      if ((cmb_documento.SelectedValue == "3") || (cmb_documento.SelectedValue == "5") || (cmb_documento.SelectedValue == "6"))
        cmb_bancos.Enabled = false;
      else
        cmb_bancos.Enabled = true;

      fch_documento.Text = string.Empty;
      fch_documento.Enabled = true;
      hdd_fchdocument.Value = string.Empty;

      cmb_guiadespacho.Enabled = true;
      cmb_guiadespacho.Items.Clear();
      hddGuiasDespacho.Value = string.Empty;
      txt_num_guia_despacho.Text = string.Empty;

      hdd_num_factura.Text = string.Empty;
      txt_valor_factura.Text = string.Empty;
      txt_aplicacion_pago_factura.Text = string.Empty;

      cmb_nota_credito.Enabled = true;
      cmb_nota_credito.Items.Clear();
      hddNotaCredito.Value = string.Empty;
      txt_saldo_nota_credito.Text = string.Empty;
      txt_aplicacion_nota_credito.Text = string.Empty;
      txt_nuevo_saldo_factura.Text = string.Empty;
      hdd_nuevo_saldo_factura.Value = string.Empty;

      txt_importe.Text = string.Empty;
      txt_importe.Enabled = true;

      btnCancelarUpdate.Visible = false;

      bEstadoValija = true;

      onLoadGrid();
    }

    protected void btnCerrarValija_Click(object sender, EventArgs e)
    {
      StringBuilder sHtml = new StringBuilder();
      sHtml.Append(File.ReadAllText(Server.MapPath("reportepago.html")));
      string sTipoPago = string.Empty;
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        cAntPagos oPagos = new cAntPagos(ref oConn);
        oPagos.CodPagos = hdd_cod_pago.Value;
        oPagos.CantDocumentos = hdd_cantidad_doc.Value;
        oPagos.ImporteTotal = hdd_importe_total.Value;
        oPagos.ImporteTotalRecibido = "0";
        oPagos.Discrepancia = "0";
        oPagos.Estado = "C";
        oPagos.Accion = "EDITAR";
        oPagos.Put();

        if (!string.IsNullOrEmpty(oPagos.Error))
        {
          Response.Write("[Cerrar / Valija] Se ha encontrado el siguiente error : " + oPagos.Error);
          Response.End();
        }

        switch (cmb_documento.SelectedValue)
        {
          case "1":
            sTipoPago = "Cheque al día";
            break;
          case "2":
            sTipoPago = "Cheque a fecha";
            break;
          case "3":
            sTipoPago = "Efectivo";
            break;
          case "4":
            sTipoPago = "Letra";
            break;
          case "5":
            sTipoPago = "Tarjeta";
            break;
          case "6":
            sTipoPago = "Transferencia";
            break;
        }

        int iTotal = 0;
        int iTotalxPago = 0;
        DataTable dtPagos = oPagos.GetCantTipoDoc();
        if (dtPagos != null)
        {
          if (dtPagos.Rows.Count > 0)
          {
            foreach (DataRow oRow in dtPagos.Rows)
            {
              iTotal = iTotal + int.Parse(oRow["cantidad"].ToString());
              switch (cmb_documento.SelectedValue)
              {
                case "1":
                  iTotalxPago = int.Parse(oRow["cantidad"].ToString());
                  break;
                case "2":
                  iTotalxPago = int.Parse(oRow["cantidad"].ToString());
                  break;
                case "3":
                  iTotalxPago = int.Parse(oRow["cantidad"].ToString());
                  break;
                case "4":
                  iTotalxPago = int.Parse(oRow["cantidad"].ToString());
                  break;
                case "5":
                  iTotalxPago = int.Parse(oRow["cantidad"].ToString());
                  break;
                case "6":
                  iTotalxPago = int.Parse(oRow["cantidad"].ToString());
                  break;
              }
            }
          }
        }
        dtPagos = null;

        StringBuilder sGrafico = new StringBuilder();
        sGrafico.Append("['").Append(sTipoPago).Append("',").Append(iTotalxPago.ToString()).Append("],");
        sGrafico.Append("['Otros Pagos',").Append((iTotal - iTotalxPago).ToString()).Append("]");
        sHtml.Replace("[#DATOSCHART]", sGrafico.ToString());
        sHtml.Replace("[#TIPOPAGO]", sTipoPago);
        sHtml.Replace("[#NUMPAGO]", hdd_cod_pago.Value);
        sHtml.Replace("[#CANTTOTAL]", hdd_cantidad_doc.Value);
        sHtml.Replace("[#MONTOTOTAL]", string.Format("{0:N0}", int.Parse(hdd_importe_total.Value)));
        sHtml.Replace("[#RAZONSOCIAL]", cmb_cliente.SelectedItem.Text);

        cAntCentrosDistribucion oCentrosDistribucion = new cAntCentrosDistribucion(ref oConn);
        oCentrosDistribucion.CodCentroDist = cmb_centrodistribucion.SelectedValue;
        DataTable dt = oCentrosDistribucion.GetByCod();
        if (dt != null)
        {
          if (dt.Rows.Count > 0)
          {
            sHtml.Replace("[#CENTRODIST]", dt.Rows[0]["descripcion"].ToString());
          }
        }
        dt = null;
        sHtml.Replace("[#FCHRECEPCION]", lbl_fecha_recepcion.Text);

        StringBuilder sTable = new StringBuilder();
        cAntDocumentosPago oDocumentosPago = new cAntDocumentosPago(ref oConn);
        oDocumentosPago.CodPagos = hdd_cod_pago.Value;
        dt = oDocumentosPago.GetDocFacturas();
        if (dt != null)
        {
          foreach (DataRow oRow in dt.Rows)
          {
            sTable.Append("<tr>");
            sTable.Append("<td>");

            sTable.Append(getTipoDoc(cmb_documento.SelectedValue));

            //sTable.Append(((oRow["aplicacion_pago_factura"].ToString() != "0") && (oRow["aplicacion_nota_credito"].ToString() != "0") ? "FNC" : ((oRow["aplicacion_pago_factura"].ToString() != "0") ? "F" : "NC")));

            sTable.Append("</td>");
            sTable.Append("<td>").Append(oRow["num_documento"].ToString()).Append("</td>");
            sTable.Append("<td>").Append(oRow["nom_deudor"].ToString()).Append("</td>");
            sTable.Append("<td>").Append(oRow["cuenta_corriente"].ToString()).Append("</td>");

            if (!string.IsNullOrEmpty(oRow["cod_banco"].ToString()))
            {
              cAntBancos oBancos = new cAntBancos(ref oConn);
              oBancos.NKeyBanco = oRow["cod_banco"].ToString();
              DataTable dBanco = oBancos.Get();
              if (dBanco != null)
              {
                if (dBanco.Rows.Count > 0)
                {
                  sTable.Append("<td>").Append(dBanco.Rows[0]["nkey_banco"].ToString() + '-' + dBanco.Rows[0]["snombre"].ToString()).Append("</td>");
                }
              }
              dBanco = null;
            }
            else
            {
              sTable.Append("<td></td>");
            }


            sTable.Append("<td>").Append(oRow["fch_documento"].ToString()).Append("</td>");
            sTable.Append("<td>").Append(((!string.IsNullOrEmpty(oRow["importe"].ToString())) ? string.Format("{0:N0}", int.Parse(oRow["importe"].ToString())) : string.Empty)).Append("</td>");
            sTable.Append("<td>").Append(oRow["num_guia_despacho"].ToString()).Append("</td>");
            sTable.Append("<td>").Append(oRow["num_factura"].ToString()).Append("</td>");
            sTable.Append("<td>").Append(string.Format("{0:N0}", int.Parse(oRow["valor_factura_original"].ToString()))).Append("</td>");
            sTable.Append("<td>").Append(string.Format("{0:N0}", int.Parse(oRow["importe_factura"].ToString()))).Append("</td>");
            sTable.Append("<td>").Append(string.Format("{0:N0}", int.Parse(oRow["aplicacion_pago_factura"].ToString()))).Append("</td>");
            sTable.Append("<td>").Append(string.Format("{0:N0}", int.Parse(oRow["aplicacion_nota_credito"].ToString()))).Append("</td>");
            sTable.Append("</tr>");
          }
        }
        dt = null;
        sHtml.Replace("[#DATOSGRILLA]", sTable.ToString());
      }
      oConn.Close();

      if (!Directory.Exists(Server.MapPath("Valijas/")))
        Directory.CreateDirectory(Server.MapPath("Valijas/"));

      string sFileHtml = Server.MapPath("Valijas/") + hdd_cod_pago.Value + ".html";
      File.WriteAllText(sFileHtml, sHtml.ToString(), Encoding.UTF8);

      idRow1.Visible = false;
      idRow2.Visible = false;
      idRow3.Visible = false;

      bEstadoValija = false;

      onLoadGrid();

      btnImprimirValija.Visible = true;
      btnAbrirValija.Visible = true;
      btnCerrarValija.Visible = false;

      Log oLog = new Log();
      oLog.IdUsuario = oIsUsuario.CodUsuario;
      oLog.ObsLog = "CIERRE DE VALIJA DE PAGO ANTALIS # " + hdd_cod_pago.Value;
      oLog.CodEvtLog = "2";
      oLog.AppLog = "ANTALIS";
      oLog.putLog();
    }

    protected void gdPagos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (e.Row.RowType == DataControlRowType.DataRow)
      {
        bool bHaveKids = false;
        bool bHaveNotaCredito = false;
        string sCodDocumento = gdPagos.DataKeys[e.Row.RowIndex].Values["cod_documento"].ToString();
        string sNodCodDocumento = gdPagos.DataKeys[e.Row.RowIndex].Values["nod_cod_documento"].ToString();
        DBConn oConn = new DBConn();
        if (oConn.Open())
        {
          cAntDocumentosPago oDocumentosPago = new cAntDocumentosPago(ref oConn);
          oDocumentosPago.NodCodDocumento = sCodDocumento;
          DataTable dt = oDocumentosPago.Get();
          if (dt != null)
          {
            if (dt.Rows.Count > 0)
            {
              bHaveKids = true;
            }
          }
          dt = null;

          oDocumentosPago.CodDocumento = sCodDocumento;
          oDocumentosPago.NodCodDocumento = string.Empty;
          dt = oDocumentosPago.Get();
          if (dt != null)
          {
            if (dt.Rows.Count > 0)
            {
              bHaveNotaCredito = (int.Parse(dt.Rows[0]["aplicacion_nota_credito"].ToString()) <= 0 ? true : false);
            }
          }
          dt = null;
        }
        oConn.Close();

        foreach (DataControlFieldCell cell in e.Row.Cells)
        {
          foreach (Control control in cell.Controls)
          {
            LinkButton lnkBtnDelete = control as LinkButton;

            if ((lnkBtnDelete != null && lnkBtnDelete.CommandName == "NotaCredito") && (!bHaveNotaCredito)) { 
              lnkBtnDelete.Visible = true;
              lnkBtnDelete.Attributes.Add("onclick", "showCreateThanksYouForm('" + sCodDocumento + "'); return false;");
            }
            else if ((lnkBtnDelete != null && lnkBtnDelete.CommandName == "Delete") && (!bHaveKids) && (string.IsNullOrEmpty(sNodCodDocumento)))
              lnkBtnDelete.Attributes.Add("onclick", "javascript:return confirm('Esta seguro de eliminar este pago?');");
            else if ((lnkBtnDelete != null && lnkBtnDelete.CommandName == "Delete") && (bHaveKids))
              lnkBtnDelete.Visible = false;
            else if ((lnkBtnDelete != null) && (lnkBtnDelete.CommandName == "SameData") && (!string.IsNullOrEmpty(sNodCodDocumento)))
            {
              lnkBtnDelete.Visible = false;
            }
          }
        }

        if (!bEstadoValija)
        {
          gdPagos.HeaderRow.Cells[0].Visible = false;
          e.Row.Cells[0].Visible = false;
          gdPagos.HeaderRow.Cells[1].Visible = false;
          e.Row.Cells[1].Visible = false;
          gdPagos.HeaderRow.Cells[2].Visible = false;
          e.Row.Cells[2].Visible = false;
        }

        e.Row.Cells[3].Text = getTipoDoc(cmb_documento.SelectedValue);

        if (e.Row.Cells[7].Text.ToString() != "&nbsp;")
        {
          oConn = new DBConn();
          if (oConn.Open())
          {
            cAntBancos oBancos = new cAntBancos(ref oConn);
            oBancos.NKeyBanco = e.Row.Cells[7].Text.ToString();
            DataTable dt = oBancos.Get();
            if (dt != null)
            {
              if (dt.Rows.Count > 0)
              {
                e.Row.Cells[7].Text = e.Row.Cells[7].Text.ToString() + " - " + dt.Rows[0]["snombre"].ToString();
              }
            }
            dt = null;

          }
          oConn.Close();
        }
      }
    }

    protected void btnAbrirValija_Click(object sender, EventArgs e)
    {
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        cAntPagos oPagos = new cAntPagos(ref oConn);
        oPagos.CodPagos = hdd_cod_pago.Value;
        oPagos.Estado = "A";
        oPagos.Accion = "EDITAR";
        oPagos.Put();

        if (!string.IsNullOrEmpty(oPagos.Error))
        {
          Response.Write("[Abrir / Valija] Se ha encontrado el siguiente error : " + oPagos.Error);
          Response.End();
        }
      }
      oConn.Close();

      idRow1.Visible = true;
      idRow2.Visible = true;
      idRow3.Visible = true;

      bEstadoValija = true;
      onLoadGrid();

      btnImprimirValija.Visible = false;
      btnAbrirValija.Visible = false;

      Log oLog = new Log();
      oLog.IdUsuario = oIsUsuario.CodUsuario;
      oLog.ObsLog = "ABRIR VALIJA DE PAGO ANTALIS # " + hdd_cod_pago.Value;
      oLog.CodEvtLog = "2";
      oLog.AppLog = "ANTALIS";
      oLog.putLog();
    }

    protected void btnImprimirValija_Click(object sender, EventArgs e)
    {
      AppSettingsReader appReader = new System.Configuration.AppSettingsReader();
      string sUrl = appReader.GetValue("SiteName", typeof(string)).ToString();
      sUrl = sUrl + hdd_cod_pago.Value + ".html";

      string pdf_page_size = "A4";
      PdfPageSize pageSize = (PdfPageSize)Enum.Parse(typeof(PdfPageSize),
          pdf_page_size, true);

      string pdf_orientation = "Portrait";
      PdfPageOrientation pdfOrientation =
          (PdfPageOrientation)Enum.Parse(typeof(PdfPageOrientation),
          pdf_orientation, true);

      int webPageWidth = 1024;
      int webPageHeight = 0;
      HtmlToPdf converter = new HtmlToPdf();

      // set converter options
      converter.Options.PdfPageSize = pageSize;
      converter.Options.PdfPageOrientation = pdfOrientation;
      converter.Options.WebPageWidth = webPageWidth;
      converter.Options.WebPageHeight = webPageHeight;

      // create a new pdf document converting an url
      PdfDocument doc = converter.ConvertUrl(sUrl);

      string sFile = Server.MapPath("Valijas/") + hdd_cod_pago.Value + ".pdf";
      doc.Save(sFile);
      doc.Close();

      Log oLog = new Log();
      oLog.IdUsuario = oIsUsuario.CodUsuario;
      oLog.ObsLog = "IMPRIME VALIJA DE PAGO ANTALIS # " + hdd_cod_pago.Value;
      oLog.CodEvtLog = "2";
      oLog.AppLog = "ANTALIS";
      oLog.putLog();

      System.Web.HttpResponse oResponse = System.Web.HttpContext.Current.Response;
      oResponse.AppendHeader("Content-Disposition", "attachment; filename=" + hdd_cod_pago.Value + ".pdf");

      // Write the file to the Response
      const int bufferLength = 10000;
      byte[] buffer = new Byte[bufferLength];
      int length = 0;
      Stream download = null;
      try
      {
        download = new FileStream(sFile, FileMode.Open, FileAccess.Read);
        do
        {
          if (oResponse.IsClientConnected)
          {
            length = download.Read(buffer, 0, bufferLength);
            oResponse.OutputStream.Write(buffer, 0, length);
            buffer = new Byte[bufferLength];
          }
          else
          {
            length = -1;
          }
        }
        while (length > 0);
        oResponse.Flush();
        oResponse.End();
      }
      finally
      {
        if (download != null)
          download.Close();
      }

    }

    protected void gdPagos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      if (e.CommandName == "SameData")
      {
        LinkButton BtnSameData = (LinkButton)e.CommandSource;    // the button
        GridViewRow myRow = (GridViewRow)BtnSameData.Parent.Parent;  // the row
        GridView myGrid = (GridView)sender; // the gridview
        string pCodDocumento = gdPagos.DataKeys[myRow.RowIndex].Values["cod_documento"].ToString(); // value of the datakey 

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
              //hdd_cod_documento.Value = dtDocPago.Rows[0]["cod_documento"].ToString();
              hdd_nod_documento.Value = dtDocPago.Rows[0]["cod_documento"].ToString();
              //hdd_nod_documento.Value = "1";
              txt_codigosap.Text = dtDocPago.Rows[0]["cod_sap"].ToString();
              txt_codigosap.Enabled = false;
              lblNomDeudor.Text = dtDocPago.Rows[0]["nom_deudor"].ToString();
              lblNomDeudor.Enabled = false;
              txt_cta_cte.Text = dtDocPago.Rows[0]["cuenta_corriente"].ToString();
              txt_cta_cte.Enabled = false;
              txt_num_documento.Text = dtDocPago.Rows[0]["num_documento"].ToString();
              txt_num_documento.Enabled = false;
              cmb_bancos.SelectedValue = dtDocPago.Rows[0]["cod_banco"].ToString();
              cmb_bancos.Enabled = false;

              cmb_guiadespacho.Items.Clear();
              cGuiasFacturas oGuiasFacturas = new cGuiasFacturas(ref oConn);
              oGuiasFacturas.NKeyCliente = dtDocPago.Rows[0]["nkey_cliente"].ToString(); //cmb_cliente.SelectedValue; //oIsUsuario.CodNkey;
              oGuiasFacturas.NCodigoDeudor = dtDocPago.Rows[0]["cod_sap"].ToString();
              DataTable dtGuias = oGuiasFacturas.GetGuiaDespacho();
              if (dtGuias != null)
              {
                cmb_guiadespacho.Items.Add(new ListItem("<< Seleccione Guia Despacho >>", string.Empty));
                if (cmb_documento.SelectedValue == "6")
                  cmb_guiadespacho.Items.Add(new ListItem("<< Agregar Guia Despacho >>", "x"));
                foreach (DataRow oRow in dtGuias.Rows)
                {
                  cmb_guiadespacho.Items.Add(new ListItem(oRow["guiadespacho"].ToString(), oRow["guiadespacho"].ToString()));
                }
              }
              dtGuias = null;

              hdd_num_factura.Text = string.Empty;
              txt_valor_factura.Text = string.Empty;
              txt_aplicacion_pago_factura.Text = string.Empty;

              cmb_nota_credito.Items.Clear();
              cmb_nota_credito.Items.Add(new ListItem("<< Seleccione Nota Crédito >>", string.Empty));
              cAntNotaCreditoNoAplicada cAntNotaCreditoNoAplicada = new cAntNotaCreditoNoAplicada(ref oConn);
              cAntNotaCreditoNoAplicada.NKeyCliente = dtDocPago.Rows[0]["nkey_cliente"].ToString();  //cmb_cliente.SelectedValue;
              cAntNotaCreditoNoAplicada.NCodigoDeudor = dtDocPago.Rows[0]["cod_sap"].ToString();
              DataTable dtNotaCreditoNoAplicada = cAntNotaCreditoNoAplicada.Get();

              if (!string.IsNullOrEmpty(cAntNotaCreditoNoAplicada.Error)) {
                Response.Write("ERROR : " + cAntNotaCreditoNoAplicada.Error.ToString());
                Response.End();
              }

              if (dtNotaCreditoNoAplicada != null)
              {
                foreach (DataRow oRow in dtNotaCreditoNoAplicada.Rows)
                {
                  if (oRow["existe"].ToString() != "0")
                  {
                    if (int.Parse(oRow["saldo"].ToString()) > 0)
                    {
                      cmb_nota_credito.Items.Add(new ListItem(oRow["nNumeroNotaCredito"].ToString(), oRow["saldo"].ToString()));
                    }
                  }
                  else
                  {
                    cmb_nota_credito.Items.Add(new ListItem(oRow["nNumeroNotaCredito"].ToString(), oRow["nMontoNotaCredito"].ToString()));
                  }
                  //cmb_nota_credito.Items.Add(new ListItem(oRow["nNumeroNotaCredito"].ToString(), oRow["nMontoNotaCredito"].ToString()));
                }
              }
              dtNotaCreditoNoAplicada = null;
              hddNotaCredito.Value = string.Empty;
              txt_saldo_nota_credito.Text = string.Empty;
              txt_aplicacion_nota_credito.Text = string.Empty;

              txt_nuevo_saldo_factura.Text = string.Empty;
              hdd_nuevo_saldo_factura.Value = string.Empty;

              fch_documento.Text = dtDocPago.Rows[0]["fch_documento"].ToString();
              fch_documento.Enabled = false;
              txt_importe.Text = dtDocPago.Rows[0]["importe"].ToString();
              txt_importe.Enabled = false;
              btnCerrarValija.Visible = false;
              btnCancelarUpdate.Visible = false;
            }
          }
          dtDocPago = null;


          oConn.Close();
        }

        btnIngresarImportes.Text = "INGRESAR PAGO";
        btnCancelarUpdate.Visible = true;
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

    protected void LoadGrid(string CodDocumento)
    {
      
    }
  }



  public class cExiste
  {
    public string bExiste { get; set; }
  }

  public class cGuiasDespacho
  {
    public string guiasdespacho { get; set; }
  }

  public class cFacturas
  {
    //public string nKeyFactura { get; set; }
    public string nKeyCliente { get; set; }
    public string nNumeroFactura { get; set; }
    public string nMontoFactura { get; set; }
    public string nSaldo { get; set; }
  }

  public class cNotaCredito
  {
    public string nKeyCliente { get; set; }
    public string nNumeroNotaCredito { get; set; }
    public string nMontoNotaCredito { get; set; }
    public string sExiste { get; set; }
    public string nSaldo { get; set; }
  }
}
