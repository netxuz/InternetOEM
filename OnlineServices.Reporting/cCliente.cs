using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using OnlineServices.Conn;

namespace OnlineServices.Reporting
{
  public class cCliente
  {
    DBConn.SQLParameters oParam;
    DBConn.DataTypeSQL TypeSQL = new DBConn.DataTypeSQL();

    private string lngCodNkey;
    public string CodNkey { get { return lngCodNkey; } set { lngCodNkey = value; } }

    private string sArrNkeyCliente;
    public string ArrNkeyCliente { get { return sArrNkeyCliente; } set { sArrNkeyCliente = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private DBConn oConn;

    public cCliente() {

    }

    public cCliente(ref DBConn oConn) {
      this.oConn = oConn;
    }

    public DataTable GeCliente()
    {
      oParam = new DBConn.SQLParameters(10);
      DataTable dtData;

      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();

        cSQL.Append("select Cliente.nRut AS 'Rut', Cliente.sDigitoVerificador AS 'DV', Cliente.sNombre AS 'Cliente', Cliente.sDireccion AS 'Direccion', Cliente.sComuna AS 'Comuna', signomoneda, decimales ");
        cSQL.Append(" from Cliente where nKey_Cliente = @nKeyCliente ");
        oParam.AddParameters("@nKeyCliente", lngCodNkey, TypeSQL.Numeric);

        dtData = oConn.Select(cSQL.ToString(), oParam);
        pError = oConn.Error;
        return dtData;
      }
      else
      {
        return null;
      }
    }

    public DataTable GetClientes()
    {
      DataTable dtData;

      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();
        cSQL.Append("select nkey_cliente, nRut, sDigitoVerificador, sNombre, sDireccion, sComuna, signomoneda, decimales ");
        cSQL.Append(" from Cliente where nKey_Cliente in(");
        cSQL.Append(sArrNkeyCliente);
        cSQL.Append(") ");

        dtData = oConn.Select(cSQL.ToString());
        pError = oConn.Error;
        return dtData;
      }
      else
      {
        return null;
      }
    }

    public DataTable GetHolding() {
      DataTable dtData;

      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();
        cSQL.Append("select distinct(ncodholding), holding ");
        cSQL.Append(" from Cliente where nKey_Cliente in(");
        cSQL.Append(sArrNkeyCliente);
        cSQL.Append(") ");

        dtData = oConn.Select(cSQL.ToString());
        pError = oConn.Error;
        return dtData;
      }
      else
      {
        return null;
      }
    }

  }
}
