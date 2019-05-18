using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using OnlineServices.Conn;

namespace OnlineServices.Antalis
{
  public class cAntNotaCredito
  {
    DBConn.SQLParameters oParam;
    DBConn.DataTypeSQL TypeSQL = new DBConn.DataTypeSQL();

    private string pCodNotaCredito;
    public string CodNotaCredito { get { return pCodNotaCredito; } set { pCodNotaCredito = value; } }

    private string pNumNotaCredito;
    public string NumNotaCredito { get { return pNumNotaCredito; } set { pNumNotaCredito = value; } }

    private string pValorNotaCredito;
    public string ValorNotaCredito { get { return pValorNotaCredito; } set { pValorNotaCredito = value; } }

    private string pSaldoNotaCredito;
    public string SaldoNotaCredito { get { return pSaldoNotaCredito; } set { pSaldoNotaCredito = value; } }

    private string pAccion;
    public string Accion { get { return pAccion; } set { pAccion = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private DBConn oConn;

    public cAntNotaCredito() {

    }

    public cAntNotaCredito(ref DBConn oConn) {
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
        cSQL.Append("select cod_nota_credito, num_nota_credito, valor_nota_credito, saldo_nota_credito ");
        cSQL.Append(" from ant_nota_credito ");

        if (!string.IsNullOrEmpty(pCodNotaCredito))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_nota_credito = @cod_nota_credito  ");
          oParam.AddParameters("@cod_nota_credito", pCodNotaCredito, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(pNumNotaCredito))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" num_nota_credito = @num_nota_credito  ");
          oParam.AddParameters("@num_nota_credito", pNumNotaCredito, TypeSQL.Numeric);
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

              pCodNotaCredito = oConn.getTableCod("ant_nota_credito", "cod_nota_credito", oConn);
              cSQL.Append("insert into ant_nota_credito(cod_nota_credito, num_nota_credito, valor_nota_credito, saldo_nota_credito) values( ");
              cSQL.Append("@cod_nota_credito, @num_nota_credito, @valor_nota_credito, @saldo_nota_credito) ");
              oParam.AddParameters("@cod_nota_credito", pCodNotaCredito, TypeSQL.Numeric);
              oParam.AddParameters("@num_nota_credito", pNumNotaCredito, TypeSQL.Numeric);
              oParam.AddParameters("@valor_nota_credito", pValorNotaCredito, TypeSQL.Numeric);
              oParam.AddParameters("@saldo_nota_credito", pSaldoNotaCredito, TypeSQL.Numeric);
              oConn.Insert(cSQL.ToString(), oParam);

              break;
            case "EDITAR":
              cSQL = new StringBuilder();
              cSQL.Append("update ant_nota_credito set ");
              if (!string.IsNullOrEmpty(pSaldoNotaCredito))
              {
                cSQL.Append(" saldo_nota_credito = @saldo_nota_credito");
                oParam.AddParameters("@saldo_nota_credito", pSaldoNotaCredito, TypeSQL.Numeric);
                sComa = ", ";
              }
              cSQL.Append(" where cod_nota_credito = @cod_nota_credito ");
              oParam.AddParameters("@cod_nota_credito", pCodNotaCredito, TypeSQL.Numeric);
              oConn.Update(cSQL.ToString(), oParam);

              break;
            case "ELIMINAR":
              string Condicion = " where ";

              cSQL = new StringBuilder();
              cSQL.Append("delete from ant_nota_credito ");

              if (!string.IsNullOrEmpty(pCodNotaCredito))
              {
                cSQL.Append(Condicion);
                Condicion = " and ";
                cSQL.Append(" cod_nota_credito = @cod_nota_credito  ");
                oParam.AddParameters("@cod_nota_credito", pCodNotaCredito, TypeSQL.Numeric);
              }

              if (!string.IsNullOrEmpty(pNumNotaCredito))
              {
                cSQL.Append(Condicion);
                Condicion = " and ";
                cSQL.Append(" num_nota_credito = @num_nota_credito  ");
                oParam.AddParameters("@num_nota_credito", pNumNotaCredito, TypeSQL.Numeric);
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
