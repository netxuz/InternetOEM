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

    private string pCodPagos;
    public string CodPagos { get { return pCodPagos; } set { pCodPagos = value; } }

    private string pCodFactura;
    public string CodFactura { get { return pCodFactura; } set { pCodFactura = value; } }

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
        cSQL.Append("select cod_documento, cod_pago, cod_factura, cod_sap, nom_deudor, num_documento, cod_banco, nom_banco, fch_documento, num_guia_despacho, importe  ");
        cSQL.Append(" from ant_documentos_pago ");

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

        if (!string.IsNullOrEmpty(pCodFactura))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_factura = @cod_factura  ");
          oParam.AddParameters("@cod_factura", pCodFactura, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(pNumDocumento))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" num_documento = @num_documento  ");
          oParam.AddParameters("@num_documento", pNumDocumento, TypeSQL.Varchar);
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
        cSQL.Append("select a.cod_documento, a.cod_pago, a.cod_factura,(select num_factura from ant_factura where cod_factura = a.cod_factura) num_factura, a.cod_sap, a.nom_deudor, a.num_documento, a.cod_banco, a.nom_banco, a.fch_documento, a.num_guia_despacho, a.importe ");
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
      oParam = new DBConn.SQLParameters(12);
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
              cSQL.Append("insert into ant_documentos_pago(cod_documento, cod_pago, cod_factura, cod_sap, nom_deudor, num_documento, cod_banco, nom_banco, fch_documento, num_guia_despacho, importe) values( ");
              cSQL.Append("@cod_documento, @cod_pago, @cod_factura, @cod_sap, @nom_deudor, @num_documento, @cod_banco, @nom_banco, @fch_documento, @num_guia_despacho, @importe) ");
              oParam.AddParameters("@cod_documento", pCodDocumento, TypeSQL.Numeric);
              oParam.AddParameters("@cod_pago", pCodPagos, TypeSQL.Numeric);
              oParam.AddParameters("@cod_factura", pCodFactura, TypeSQL.Numeric);
              oParam.AddParameters("@cod_sap", pCodSAP, TypeSQL.Numeric);
              oParam.AddParameters("@nom_deudor", pNombreDeudor, TypeSQL.Varchar);
              oParam.AddParameters("@num_documento", pNumDocumento, TypeSQL.Varchar);
              oParam.AddParameters("@cod_banco", pCodBanco, TypeSQL.Numeric);
              oParam.AddParameters("@nom_banco", pNomBanco, TypeSQL.Varchar);
              oParam.AddParameters("@fch_documento", pFchDocumento, TypeSQL.DateTime);
              oParam.AddParameters("@num_guia_despacho", pNumGuiaDespacho, TypeSQL.Numeric);
              oParam.AddParameters("@importe", pImporte, TypeSQL.Numeric);
              oConn.Insert(cSQL.ToString(), oParam);

              if (!string.IsNullOrEmpty(oConn.Error)) {
                pError = oConn.Error;
              }

              break;
            case "EDITAR":


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
