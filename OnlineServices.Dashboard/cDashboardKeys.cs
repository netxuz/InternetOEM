using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using OnlineServices.Conn;

namespace OnlineServices.Dashboard
{
  public class cDashboardKeys
  {
    DBConn.SQLParameters oParam;
    DBConn.DataTypeSQL TypeSQL = new DBConn.DataTypeSQL();

    private string nCodUser;
    public string CodUser { get { return nCodUser; } set { nCodUser = value; } }

    private string nKeyCliente;
    public string KeyCliente { get { return nKeyCliente; } set { nKeyCliente = value; } }

    private string nCodHolding;
    public string CodHolding { get { return nCodHolding; } set { nCodHolding = value; } }

    private string nCodigoDeudor;
    public string CodigoDeudor { get { return nCodigoDeudor; } set { nCodigoDeudor = value; } }

    private string nKeyDeudor;
    public string KeyDeudor { get { return nKeyDeudor; } set { nKeyDeudor = value; } }

    private string sAccion;
    public string Accion { get { return sAccion; } set { sAccion = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private DBConn oConn;

    public cDashboardKeys()
    {

    }

    public cDashboardKeys(ref DBConn oConn)
    {
      this.oConn = oConn;
    }

    public DataTable Get()
    {
      oParam = new DBConn.SQLParameters(1);
      DataTable dtData;

      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();

        cSQL.Append("select * from dashboard_keys");
        cSQL.Append(" where cod_user = @cod_user ");
        oParam.AddParameters("@cod_user", nCodUser, TypeSQL.Numeric);

        dtData = oConn.Select(cSQL.ToString(), oParam);
        pError = oConn.Error;
        return dtData;
      }
      else
      {
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
          switch (sAccion)
          {
            case "CREAR":
              cSQL = new StringBuilder();
              //pCodPagos = getCod();
              cSQL.Append("insert into dashboard_keys(cod_user,nkey_cliente,ncodholding,ncodigodeudor,nkey_deudor) values( ");
              cSQL.Append("@cod_user,@nkey_cliente,@ncodholding,@ncodigodeudor,@nkey_deudor) ");
              oParam.AddParameters("@cod_user", nCodUser, TypeSQL.Numeric);
              oParam.AddParameters("@nkey_cliente", nKeyCliente, TypeSQL.Int);
              oParam.AddParameters("@ncodholding", nCodHolding, TypeSQL.Int);
              oParam.AddParameters("@ncodigodeudor", nCodigoDeudor, TypeSQL.Varchar);
              oParam.AddParameters("@nkey_deudor", nKeyDeudor, TypeSQL.Int);
              oConn.Insert(cSQL.ToString(), oParam);

              if (!string.IsNullOrEmpty(oConn.Error))
              {
                pError = oConn.Error;
              }

              break;

            case "ELIMINAR":
              cSQL = new StringBuilder();
              cSQL.Append("delete from dashboard_keys where cod_user = @cod_user ");
              oParam.AddParameters("@cod_user", nCodUser, TypeSQL.Numeric);
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
