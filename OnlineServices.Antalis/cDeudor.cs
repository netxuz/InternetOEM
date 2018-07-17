using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using OnlineServices.Conn;

namespace OnlineServices.Antalis
{
  public class cDeudor
  {
    DBConn.SQLParameters oParam;
    DBConn.DataTypeSQL TypeSQL = new DBConn.DataTypeSQL();

    private string pNKeyCliente;
    public string NKeyCliente { get { return pNKeyCliente; } set { pNKeyCliente = value; } }

    private string pNKeyDeudor;
    public string NKeyDeudor { get { return pNKeyDeudor; } set { pNKeyDeudor = value; } }

    private string pNCodigoDeudor; //CODIGO SAP
    public string NCodigoDeudor { get { return pNCodigoDeudor; } set { pNCodigoDeudor = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private DBConn oConn;

    public cDeudor() {

    }

    public cDeudor(ref DBConn oConn) {
      this.oConn = oConn;
    }

    public DataTable Get()
    {
      oParam = new DBConn.SQLParameters(2);
      DataTable dtData;
      StringBuilder cSQL;

      if (oConn.bIsOpen)
      {
        cSQL = new StringBuilder();
        cSQL.Append("select * from deudor where nkey_deudor in(select nkey_deudor  from codigodeudor where ncodigodeudor = @ncodigodeudor and nkey_cliente = @nkeycliente) ");
        oParam.AddParameters("@nkeycliente", pNKeyCliente, TypeSQL.Numeric);
        oParam.AddParameters("@ncodigodeudor", pNCodigoDeudor, TypeSQL.Varchar);

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
