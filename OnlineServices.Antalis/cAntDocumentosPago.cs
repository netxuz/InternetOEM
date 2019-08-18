using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using OnlineServices.Conn;

namespace OnlineServices.Antalis
{
  public class cAntDocumentosPago
  {
    DBConn.SQLParameters oParam;
    DBConn.DataTypeSQL TypeSQL = new DBConn.DataTypeSQL();

    private string pCodDocumento;
    public string CodDocumento { get { return pCodDocumento; } set { pCodDocumento = value; } }

    private string pNodCodDocumento;
    public string NodCodDocumento { get { return pNodCodDocumento; } set { pNodCodDocumento = value; } }
    

    private string pCodPagos;
    public string CodPagos { get { return pCodPagos; } set { pCodPagos = value; } }

    private string pCodFactura;
    public string CodFactura { get { return pCodFactura; } set { pCodFactura = value; } }

    private string pCodNotaCredito;
    public string CodNotaCredito { get { return pCodNotaCredito; } set { pCodNotaCredito = value; } }

    private string pCodSAP;
    public string CodSAP { get { return pCodSAP; } set { pCodSAP = value; } }

    private string pNombreDeudor;
    public string NombreDeudor { get { return pNombreDeudor; } set { pNombreDeudor = value; } }

    private string pNumDocumento;
    public string NumDocumento { get { return pNumDocumento; } set { pNumDocumento = value; } }

    private string pCodBanco;
    public string CodBanco { get { return pCodBanco; } set { pCodBanco = value; } }

    private string pNomBanco;
    public string NomBanco { get { return pNomBanco; } set { pNomBanco = value; } }

    private string pFchDocumento;
    public string FchDocumento { get { return pFchDocumento; } set { pFchDocumento = value; } }

    private string pNumGuiaDespacho;
    public string NumGuiaDespacho { get { return pNumGuiaDespacho; } set { pNumGuiaDespacho = value; } }
    
    private string pImporte;
    public string importe { get { return pImporte; } set { pImporte = value; } }

    private string pImporteRecibido;
    public string ImporteRecibido { get { return pImporteRecibido; } set { pImporteRecibido = value; } }

    private string pDiscrepancia;
    public string Discrepancia { get { return pDiscrepancia; } set { pDiscrepancia = value; } }

    private string pCuentaCorriente;
    public string CuentaCorriente { get { return pCuentaCorriente; } set { pCuentaCorriente = value; } }

    private string pImporteFactura;
    public string ImporteFactura { get { return pImporteFactura; } set { pImporteFactura = value; } }

    private string pAplicacionPagoFactura;
    public string AplicacionPagoFactura { get { return pAplicacionPagoFactura; } set { pAplicacionPagoFactura = value; } }

    private string pSaldoNotaCredito;
    public string SaldoNotaCredito { get { return pSaldoNotaCredito; } set { pSaldoNotaCredito = value; } }    

    private string pAplicacionNotaCredito;
    public string AplicacionNotaCredito { get { return pAplicacionNotaCredito; } set { pAplicacionNotaCredito = value; } }

    private string pIndNewGuia;
    public string IndNewGuia { get { return pIndNewGuia; } set { pIndNewGuia = value; } }

    private string pAccion;
    public string Accion { get { return pAccion; } set { pAccion = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private DBConn oConn;

    public cAntDocumentosPago()
    {

    }

    public cAntDocumentosPago(ref DBConn oConn) {
      this.oConn = oConn;
    }

    public DataTable Get()
    {
      oParam = new DBConn.SQLParameters(3);
      DataTable dtData;
      StringBuilder cSQL;
      string Condicion = " where ";

      if (oConn.bIsOpen)
      {
        cSQL = new StringBuilder();
        cSQL.Append("select cod_documento, nod_cod_documento, cod_pago, cod_factura, cod_nota_credito, cod_sap, nom_deudor, num_documento, cod_banco, nom_banco, convert(varchar, fch_documento, 103) fch_documento, num_guia_despacho, importe, importe_recibido, discrepancia, cuenta_corriente, importe_factura, aplicacion_pago_factura, saldo_nota_credito, aplicacion_nota_credito, ind_new_guia ");
        cSQL.Append(" from ant_documentos_pago ");

        if (!string.IsNullOrEmpty(pCodDocumento))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_documento = @cod_documento  ");
          oParam.AddParameters("@cod_documento", pCodDocumento, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(pNodCodDocumento))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" nod_cod_documento = @nod_cod_documento  ");
          oParam.AddParameters("@nod_cod_documento", pNodCodDocumento, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(pCodPagos))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_pago = @cod_pago  ");
          oParam.AddParameters("@cod_pago", pCodPagos, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(pCodFactura))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_factura = @cod_factura  ");
          oParam.AddParameters("@cod_factura", pCodFactura, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(pCodNotaCredito))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_nota_credito = @cod_nota_credito  ");
          oParam.AddParameters("@cod_nota_credito", pCodNotaCredito, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(pNumDocumento))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" num_documento = @num_documento  ");
          oParam.AddParameters("@num_documento", pNumDocumento, TypeSQL.Varchar);
        }

        if (!string.IsNullOrEmpty(pCodBanco))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_banco = @cod_banco  ");
          oParam.AddParameters("@cod_banco", pCodBanco, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(pCuentaCorriente))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cuenta_corriente = @cuenta_corriente  ");
          oParam.AddParameters("@cuenta_corriente", pCuentaCorriente, TypeSQL.Varchar);
        }

        dtData = oConn.Select(cSQL.ToString(), oParam);
        pError = oConn.Error;
        return dtData;
      }
      else
      {
        pError = "Conexion Cerrada";
        return null;
      }
    }

    public DataTable GetDocFacturas()
    {
      oParam = new DBConn.SQLParameters(3);
      DataTable dtData;
      StringBuilder cSQL;
      string Condicion = " where ";

      if (oConn.bIsOpen)
      {
        cSQL = new StringBuilder();
        cSQL.Append("select (select nkey_cliente from ant_pagos where cod_pago = a.cod_pago) nkey_cliente, a.cod_documento, a.nod_cod_documento, a.cod_pago, a.cod_factura, a.cod_nota_credito, (select num_factura from ant_factura where cod_factura = a.cod_factura) num_factura, a.cod_sap, a.nom_deudor, a.num_documento, a.cod_banco, a.nom_banco, convert(varchar, a.fch_documento, 103) fch_documento, a.num_guia_despacho, a.importe, (select valor_factura from ant_factura where cod_factura = a.cod_factura) valor_factura,  (select saldo_factura from ant_factura where cod_factura = a.cod_factura) saldo_factura, a.importe_recibido, a.discrepancia, a.cuenta_corriente, a.importe_factura, a.aplicacion_pago_factura, a.saldo_nota_credito, a.aplicacion_nota_credito, a.ind_new_guia, (a.aplicacion_pago_factura + a.aplicacion_nota_credito) as valor_aplicado_factura, (a.importe_factura + a.aplicacion_pago_factura + a.aplicacion_nota_credito) valor_factura_original ");
        cSQL.Append(" from ant_documentos_pago a ");

        if (!string.IsNullOrEmpty(pCodDocumento))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" a.cod_documento = @cod_documento  ");
          oParam.AddParameters("@cod_documento", pCodDocumento, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(pCodPagos))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" a.cod_pago = @cod_pago  ");
          oParam.AddParameters("@cod_pago", pCodPagos, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(pCuentaCorriente))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" a.cuenta_corriente = @cuenta_corriente  ");
          oParam.AddParameters("@cuenta_corriente", pCuentaCorriente, TypeSQL.Varchar);
        }

        dtData = oConn.Select(cSQL.ToString(), oParam);
        pError = oConn.Error;
        return dtData;
      }
      else
      {
        pError = "Conexion Cerrada";
        return null;
      }
    }

    public void Put()
    {
      oParam = new DBConn.SQLParameters(21);
      StringBuilder cSQL;
      string sComa = string.Empty;

      if (oConn.bIsOpen)
      {
        try
        {
          switch (pAccion)
          {
            case "CREAR":
              cSQL = new StringBuilder();

              pCodDocumento = oConn.getTableCod("ant_documentos_pago", "cod_documento", oConn);
              cSQL.Append("insert into ant_documentos_pago(cod_documento, nod_cod_documento, cod_pago, cod_factura, cod_nota_credito, cod_sap, nom_deudor, num_documento, cod_banco, nom_banco, fch_documento, num_guia_despacho, importe, importe_recibido, discrepancia, cuenta_corriente, importe_factura, aplicacion_pago_factura, saldo_nota_credito, aplicacion_nota_credito, ind_new_guia) values( ");
              cSQL.Append("@cod_documento, @nod_cod_documento, @cod_pago, @cod_factura, @cod_nota_credito, @cod_sap, @nom_deudor, @num_documento, @cod_banco, @nom_banco, @fch_documento, @num_guia_despacho, @importe, @importe_recibido, @discrepancia, @cuenta_corriente, @importe_factura, @aplicacion_pago_factura, @saldo_nota_credito, @aplicacion_nota_credito, @ind_new_guia) ");
              oParam.AddParameters("@cod_documento", pCodDocumento, TypeSQL.Numeric);
              oParam.AddParameters("@nod_cod_documento", pNodCodDocumento, TypeSQL.Numeric);
              oParam.AddParameters("@cod_pago", pCodPagos, TypeSQL.Numeric);
              oParam.AddParameters("@cod_factura", pCodFactura, TypeSQL.Numeric);
              oParam.AddParameters("@cod_nota_credito", pCodNotaCredito, TypeSQL.Numeric);
              oParam.AddParameters("@cod_sap", pCodSAP, TypeSQL.Numeric);
              oParam.AddParameters("@nom_deudor", pNombreDeudor, TypeSQL.Varchar);
              oParam.AddParameters("@num_documento", pNumDocumento, TypeSQL.Varchar);
              oParam.AddParameters("@cod_banco", pCodBanco, TypeSQL.Numeric);
              oParam.AddParameters("@nom_banco", pNomBanco, TypeSQL.Varchar);
              oParam.AddParameters("@fch_documento", pFchDocumento, TypeSQL.DateTime);
              oParam.AddParameters("@num_guia_despacho", pNumGuiaDespacho, TypeSQL.Numeric);
              oParam.AddParameters("@importe", pImporte, TypeSQL.Numeric);
              oParam.AddParameters("@importe_recibido", pImporteRecibido, TypeSQL.Numeric);
              oParam.AddParameters("@discrepancia", pDiscrepancia, TypeSQL.Numeric);
              oParam.AddParameters("@cuenta_corriente", pCuentaCorriente, TypeSQL.Varchar);
              oParam.AddParameters("@importe_factura", pImporteFactura, TypeSQL.Numeric);
              oParam.AddParameters("@aplicacion_pago_factura", pAplicacionPagoFactura, TypeSQL.Numeric);
              oParam.AddParameters("@saldo_nota_credito", pSaldoNotaCredito, TypeSQL.Numeric);
              oParam.AddParameters("@aplicacion_nota_credito", pAplicacionNotaCredito, TypeSQL.Numeric);
              oParam.AddParameters("@ind_new_guia", pIndNewGuia, TypeSQL.Char);
              oConn.Insert(cSQL.ToString(), oParam);

              if (!string.IsNullOrEmpty(oConn.Error)) {
                pError = oConn.Error;
              }

              break;
            case "EDITAR":
              cSQL = new StringBuilder();
              cSQL.Append("update ant_documentos_pago set ");
              if (!string.IsNullOrEmpty(pCodFactura))
              {
                cSQL.Append(" cod_factura = @cod_factura");
                oParam.AddParameters("@cod_factura", pCodFactura, TypeSQL.Numeric);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pCodNotaCredito))
              {
                cSQL.Append(sComa);
                cSQL.Append(" cod_nota_credito = @cod_nota_credito");
                oParam.AddParameters("@cod_nota_credito", pCodNotaCredito, TypeSQL.Numeric);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pNombreDeudor))
              {
                cSQL.Append(sComa);
                cSQL.Append(" nom_deudor = @nom_deudor");
                oParam.AddParameters("@nom_deudor", pNombreDeudor, TypeSQL.Varchar);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pNumDocumento))
              {
                cSQL.Append(sComa);
                cSQL.Append(" num_documento = @num_documento");
                oParam.AddParameters("@num_documento", pNumDocumento, TypeSQL.Varchar);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pCodBanco))
              {
                cSQL.Append(sComa);
                cSQL.Append(" cod_banco = @cod_banco");
                oParam.AddParameters("@cod_banco", pCodBanco, TypeSQL.Numeric);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pFchDocumento))
              {
                cSQL.Append(sComa);
                cSQL.Append(" fch_documento = @fch_documento");
                oParam.AddParameters("@fch_documento", pFchDocumento, TypeSQL.DateTime);
                sComa = ", ";
              }

              if (!string.IsNullOrEmpty(pNumGuiaDespacho))
              {
                cSQL.Append(sComa);
                cSQL.Append(" num_guia_despacho = @num_guia_despacho");
                oParam.AddParameters("@num_guia_despacho", pNumGuiaDespacho, TypeSQL.Varchar);
                sComa = ", ";
              }

              if (!string.IsNullOrEmpty(pImporte))
              {
                cSQL.Append(sComa);
                cSQL.Append(" importe = @importe");
                oParam.AddParameters("@importe", pImporte, TypeSQL.Numeric);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pImporteRecibido))
              {
                cSQL.Append(sComa);
                cSQL.Append(" importe_recibido = @importe_recibido");
                oParam.AddParameters("@importe_recibido", pImporteRecibido, TypeSQL.Numeric);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pDiscrepancia))
              {
                cSQL.Append(sComa);
                cSQL.Append(" discrepancia = @discrepancia");
                oParam.AddParameters("@discrepancia", pDiscrepancia, TypeSQL.Numeric);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pCuentaCorriente))
              {
                cSQL.Append(sComa);
                cSQL.Append(" cuenta_corriente = @cuenta_corriente");
                oParam.AddParameters("@cuenta_corriente", pCuentaCorriente, TypeSQL.Varchar);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pImporteFactura))
              {
                cSQL.Append(sComa);
                cSQL.Append(" importe_factura = @importe_factura");
                oParam.AddParameters("@importe_factura", pImporteFactura, TypeSQL.Numeric);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pAplicacionPagoFactura))
              {
                cSQL.Append(sComa);
                cSQL.Append(" aplicacion_pago_factura = @aplicacion_pago_factura");
                oParam.AddParameters("@aplicacion_pago_factura", pAplicacionPagoFactura, TypeSQL.Numeric);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pAplicacionNotaCredito))
              {
                cSQL.Append(sComa);
                cSQL.Append(" aplicacion_nota_credito = @aplicacion_nota_credito");
                oParam.AddParameters("@aplicacion_nota_credito", pAplicacionNotaCredito, TypeSQL.Numeric);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pIndNewGuia))
              {
                cSQL.Append(sComa);
                cSQL.Append(" ind_new_guia = @ind_new_guia");
                oParam.AddParameters("@ind_new_guia", pIndNewGuia, TypeSQL.Char);
                sComa = ", ";
              }
              cSQL.Append(" where cod_documento = @cod_documento ");
              oParam.AddParameters("@cod_documento", pCodDocumento, TypeSQL.Numeric);
              oConn.Update(cSQL.ToString(), oParam);

              break;
            case "ELIMINAR":
              string Condicion = " where ";

              cSQL = new StringBuilder();
              cSQL.Append("delete from ant_documentos_pago ");

              if (!string.IsNullOrEmpty(pCodDocumento))
              {
                cSQL.Append(Condicion);
                Condicion = " and ";
                cSQL.Append(" cod_documento = @cod_documento  ");
                oParam.AddParameters("@cod_documento", pCodDocumento, TypeSQL.Numeric);
              }
              
              if (!string.IsNullOrEmpty(pCodPagos))
              {
                cSQL.Append(Condicion);
                Condicion = " and ";
                cSQL.Append(" cod_pago = @cod_pago  ");
                oParam.AddParameters("@cod_pago", pCodPagos, TypeSQL.Numeric);
              }

              oConn.Delete(cSQL.ToString(), oParam);

              break;
          }
        }
        catch (Exception Ex)
        {
          pError = Ex.Message;
        }
      }
    }

  }
}
