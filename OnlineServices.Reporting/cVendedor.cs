using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using OnlineServices.Conn;

namespace OnlineServices.Reporting
{
  public class cVendedor
  {
    DBConn.SQLParameters oParam;
    DBConn.DataTypeSQL TypeSQL = new DBConn.DataTypeSQL();

    private string lngCodNkey;
    public string CodNkey { get { return lngCodNkey; } set { lngCodNkey = value; } }

    private string pNombre;
    public string Nombre { get { return pNombre; } set { pNombre = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private DBConn oConn;

    public cVendedor()
    {

    }

    public cVendedor(ref DBConn oConn)
    {
      this.oConn = oConn;
    }

    public DataTable Get()
    {
      oParam = new DBConn.SQLParameters(10);
      DataTable dtData;

      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();
        cSQL.Append("Select nkey_vendedor, snombre from vendedor ");
        cSQL.Append(" where nkey_cliente = @nkey_cliente ");
        oParam.AddParameters("@nkey_cliente", lngCodNkey, TypeSQL.Numeric);

        if (!string.IsNullOrEmpty(pNombre))
        {
          cSQL.Append(" and sNombre Like '%' + @snombre + '%'");
          oParam.AddParameters("@snombre", pNombre, TypeSQL.Varchar);
        }
        
        cSQL.Append(" Order by sNombre ");

        dtData = oConn.Select(cSQL.ToString(), oParam);
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
