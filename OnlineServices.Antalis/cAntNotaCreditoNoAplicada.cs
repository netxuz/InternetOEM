using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using OnlineServices.Conn;

namespace OnlineServices.Antalis
{
  public class cAntNotaCreditoNoAplicada
  {
    DBConn.SQLParameters oParam;
    DBConn.DataTypeSQL TypeSQL = new DBConn.DataTypeSQL();

    private string pNKeyCliente;
    public string NKeyCliente { get { return pNKeyCliente; } set { pNKeyCliente = value; } }

    private string pNCodigoDeudor;
    public string NCodigoDeudor { get { return pNCodigoDeudor; } set { pNCodigoDeudor = value; } }

    private string pNNumeroNotaCredito;
    public string NNumeroNotaCredito { get { return pNNumeroNotaCredito; } set { pNNumeroNotaCredito = value; } }

    private string pAccion;
    public string Accion { get { return pAccion; } set { pAccion = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private DBConn oConn;

    public cAntNotaCreditoNoAplicada()
    {

    }

    public cAntNotaCreditoNoAplicada(ref DBConn oConn)
    {
      this.oConn = oConn;
    }

    public DataTable GetNotaCreditoByData()
    {
      oParam = new DBConn.SQLParameters(7);
      DataTable dtData;
      StringBuilder cSQL;

      if (oConn.bIsOpen)
      {
        cSQL = new StringBuilder();
        cSQL.Append("select nKey_NotaCredito, nKey_Cliente, nKey_Deudor, nNumeroNotaCredito, nNumeroFactura, nMontoNotaCredito, dFechaEmision ");
        cSQL.Append(" from notacredito_noaplicada where nNumeroNotaCredito = @nNumeroNotaCredito and nKey_Cliente = @nKey_Cliente ");
        oParam.AddParameters("@nNumeroNotaCredito", pNNumeroNotaCredito, TypeSQL.Numeric);
        oParam.AddParameters("@nKey_Cliente", pNKeyCliente, TypeSQL.Numeric);
        
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

    public DataTable Get() {
      oParam = new DBConn.SQLParameters(7);
      DataTable dtData;
      StringBuilder cSQL;
      string Condicion = " where ";

      if (oConn.bIsOpen)
      {
        cSQL = new StringBuilder();
        cSQL.Append("select a.nKey_NotaCredito, a.nKey_Cliente, a.nKey_Deudor, a.nNumeroNotaCredito, a.nNumeroFactura, a.nMontoNotaCredito, a.dFechaEmision, isnull((select num_nota_credito from ant_nota_credito where num_nota_credito = a.nNumeroNotaCredito),0) existe, isnull((select saldo_nota_credito from ant_nota_credito where num_nota_credito = a.nNumeroNotaCredito),0) saldo ");
        cSQL.Append(" from notacredito_noaplicada a where a.nKey_Cliente = @nKey_Cliente and a.nkey_deudor in(select nkey_deudor from codigodeudor where ncodigodeudor = @ncodigodeudor and nKey_Cliente = @nKey_Cliente) ");
        //cSQL.Append(" and not a.nNumeroNotaCredito in(select num_nota_credito from ant_nota_credito where saldo_nota_credito <= 0 ) ");
        oParam.AddParameters("@nKey_Cliente", pNKeyCliente, TypeSQL.Numeric);
        oParam.AddParameters("@ncodigodeudor", pNCodigoDeudor, TypeSQL.Numeric);

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

  }
}
