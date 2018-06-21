using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using OnlineServices.Conn;

namespace OnlineServices.Antalis
{
  public class cAntDocPago
  {
    DBConn.SQLParameters oParam;
    DBConn.DataTypeSQL TypeSQL = new DBConn.DataTypeSQL();

    private string pCodDocumento;
    public string CodDocumento { get { return pCodDocumento; } set { pCodDocumento = value; } }

    private string pCodFactura;
    public string CodFactura { get { return pCodFactura; } set { pCodFactura = value; } }

    private string pAccion;
    public string Accion { get { return pAccion; } set { pAccion = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private DBConn oConn;

    public cAntDocPago() {

    }

    public cAntDocPago(ref DBConn oConn) {
      this.oConn = oConn;
    }

    public DataTable Get()
    {
      oParam = new DBConn.SQLParameters(3);
      DataTable dtData;
      StringBuilder cSQL;

      if (oConn.bIsOpen)
      {
        cSQL = new StringBuilder();
        cSQL.Append("select cod_factura, num_factura, valor_factura, saldo_factura from ant_factura ");
        cSQL.Append(" where cod_factura in(select cod_factura from ant_doc_pago where cod_documento = @cod_documento ) ");
        oParam.AddParameters("@cod_documento", pCodDocumento, TypeSQL.Numeric);

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
      oParam = new DBConn.SQLParameters(10);
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
              cSQL.Append("insert into ant_doc_pago(@cod_documento, @cod_factura) values( ");
              cSQL.Append("@cod_documento, @cod_factura) ");
              oParam.AddParameters("@cod_documento", pCodDocumento, TypeSQL.Numeric);
              oParam.AddParameters("@cod_factura", pCodFactura, TypeSQL.Numeric);
              oConn.Insert(cSQL.ToString(), oParam);

              break;
            case "EDITAR":


              break;
            case "ELIMINAR":
              string Condicion = " where ";

              cSQL = new StringBuilder();
              cSQL.Append("delete from ant_doc_pago ");

              if (!string.IsNullOrEmpty(pCodDocumento))
              {
                cSQL.Append(Condicion);
                Condicion = " and ";
                cSQL.Append(" cod_documento = @cod_documento  ");
                oParam.AddParameters("@cod_documento", pCodDocumento, TypeSQL.Numeric);
              }

              if (!string.IsNullOrEmpty(pCodFactura))
              {
                cSQL.Append(Condicion);
                Condicion = " and ";
                cSQL.Append(" cod_factura = @cod_factura  ");
                oParam.AddParameters("@cod_factura", pCodFactura, TypeSQL.Numeric);
              }

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
