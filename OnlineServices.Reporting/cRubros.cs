using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using OnlineServices.Conn;

namespace OnlineServices.Reporting
{
  public class cRubros
  {
    DBConn.SQLParameters oParam;
    DBConn.DataTypeSQL TypeSQL = new DBConn.DataTypeSQL();

    private string pIndCanal;
    public string IndCanal { get { return pIndCanal; } set { pIndCanal = value; } }

    private string pNombre;
    public string Nombre { get { return pNombre; } set { pNombre = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private DBConn oConn;

    public cRubros() {

    }

    public cRubros(ref DBConn oConn) {
      this.oConn = oConn;
    }

    public DataTable Get()
    {
      oParam = new DBConn.SQLParameters(10);
      DataTable dtData;

      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();
        cSQL.Append("select * from rubros  where tipo_canal = @canal ");
        oParam.AddParameters("@canal", pIndCanal, TypeSQL.Varchar);

        if (!string.IsNullOrEmpty(pNombre))
        {
          cSQL.Append(" and sRubro like '%' + @nombre + '%' ");
          oParam.AddParameters("@nombre", pNombre, TypeSQL.Varchar);
        }

        cSQL.Append(" order by sRubro ");        

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
