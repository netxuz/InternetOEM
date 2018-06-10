using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using OnlineServices.Conn;

namespace OnlineServices.Reporting
{
  public class cAnalistas
  {
    DBConn.SQLParameters oParam;
    DBConn.DataTypeSQL TypeSQL = new DBConn.DataTypeSQL();

    private string pNombre;
    public string Nombre { get { return pNombre; } set { pNombre = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private DBConn oConn;

    public cAnalistas() {

    }

    public cAnalistas(ref DBConn oConn) {
      this.oConn = oConn;
    }

    public DataTable Get()
    {
      oParam = new DBConn.SQLParameters(2);
      DataTable dtData;

      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();
        cSQL.Append("select nkey_analista, snombre from analista ");

        if (!string.IsNullOrEmpty(pNombre)) { 
          cSQL.Append(" where sNombre like '%' + @analista + '%'");
          oParam.AddParameters("@analista", pNombre, TypeSQL.Varchar);
        }

        cSQL.Append(" order by sNombre ");

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
