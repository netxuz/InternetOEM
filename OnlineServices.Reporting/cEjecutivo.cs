using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using OnlineServices.Conn;

namespace OnlineServices.Reporting
{
  public class cEjecutivo
  {
    DBConn.SQLParameters oParam;
    DBConn.DataTypeSQL TypeSQL = new DBConn.DataTypeSQL();

    private string lngCodNkey;
    public string CodNkey { get { return lngCodNkey; } set { lngCodNkey = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private DBConn oConn;

    public cEjecutivo()
    {

    }

    public cEjecutivo(ref DBConn oConn)
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

        cSQL.Append("select Ejecutivo.sNombre as 'Ejecutivo', ");
        cSQL.Append(" Ejecutivo.sFax as 'Fax',");
        cSQL.Append(" Ejecutivo.sTelefono as 'Telefono', ");
        cSQL.Append(" Ejecutivo.sEMail as 'EMail' ");
        cSQL.Append(" From Ejecutivo where nkey_cliente = @nKeyCliente ");
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

  }
}
