using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using OnlineServices.Conn;

namespace OnlineServices.Antalis
{
  public class cAntFactura
  {
    DBConn.SQLParameters oParam;
    DBConn.DataTypeSQL TypeSQL = new DBConn.DataTypeSQL();

    private string pCodFactura;
    public string CodFactura { get { return pCodFactura; } set { pCodFactura = value; } }

    private string pNumFactura;
    public string NumFactura { get { return pNumFactura; } set { pNumFactura = value; } }

    private string pValorFactura;
    public string ValorFactura { get { return pValorFactura; } set { pValorFactura = value; } }

    private string pSaldoFactura;
    public string SaldoFactura { get { return pSaldoFactura; } set { pSaldoFactura = value; } }

    private string pAccion;
    public string Accion { get { return pAccion; } set { pAccion = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private DBConn oConn;

    public cAntFactura() {

    }

    public cAntFactura(ref DBConn oConn) {
      this.oConn = oConn;
    }

    public DataTable Get() {
      oParam = new DBConn.SQLParameters(3);
      DataTable dtData;
      StringBuilder cSQL;
      string Condicion = " where ";

      if (oConn.bIsOpen)
      {
        cSQL = new StringBuilder();
        cSQL.Append("select cod_factura, num_factura, valor_factura, saldo_factura ");
        cSQL.Append(" from ant_factura ");

        if (!string.IsNullOrEmpty(pCodFactura))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_factura = @cod_factura  ");
          oParam.AddParameters("@cod_factura", pCodFactura, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(pNumFactura))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" num_factura = @num_factura  ");
          oParam.AddParameters("@num_factura", pNumFactura, TypeSQL.Numeric);
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

              pCodFactura = oConn.getTableCod("ant_factura", "cod_factura", oConn);
              cSQL.Append("insert into ant_factura(cod_factura, num_factura, valor_factura, saldo_factura) values( ");
              cSQL.Append("@cod_factura, @num_factura, @valor_factura, @saldo_factura) ");
              oParam.AddParameters("@cod_factura", pCodFactura, TypeSQL.Numeric);
              oParam.AddParameters("@num_factura", pNumFactura, TypeSQL.Numeric);
              oParam.AddParameters("@valor_factura", pValorFactura, TypeSQL.Numeric);
              oParam.AddParameters("@saldo_factura", pSaldoFactura, TypeSQL.Numeric);
              oConn.Insert(cSQL.ToString(), oParam);

              break;
            case "EDITAR":
              cSQL = new StringBuilder();
              cSQL.Append("update ant_factura set ");
              if (!string.IsNullOrEmpty(pSaldoFactura))
              {
                cSQL.Append(" saldo_factura = @saldo_factura");
                oParam.AddParameters("@saldo_factura", pSaldoFactura, TypeSQL.Numeric);
                sComa = ", ";
              }
              cSQL.Append(" where cod_factura = @cod_factura ");
              oParam.AddParameters("@cod_factura", pCodFactura, TypeSQL.Numeric);
              oConn.Update(cSQL.ToString(), oParam);

              break;
            case "ELIMINAR":
              string Condicion = " where ";

              cSQL = new StringBuilder();
              cSQL.Append("delete from ant_factura ");

              if (!string.IsNullOrEmpty(pCodFactura))
              {
                cSQL.Append(Condicion);
                Condicion = " and ";
                cSQL.Append(" cod_factura = @cod_factura  ");
                oParam.AddParameters("@cod_factura", pCodFactura, TypeSQL.Numeric);
              }

              if (!string.IsNullOrEmpty(pNumFactura))
              {
                cSQL.Append(Condicion);
                Condicion = " and ";
                cSQL.Append(" num_factura = @num_factura  ");
                oParam.AddParameters("@num_factura", pNumFactura, TypeSQL.Numeric);
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
