using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using OnlineServices.Conn;

namespace OnlineServices.Reporting
{
  public class cReporteClientesRiesgosos
  {
    private string lngCodNkey;
    public string CodNkey { get { return lngCodNkey; } set { lngCodNkey = value; } }

    private string pNcodHolding;
    public string NcodHolding { get { return pNcodHolding; } set { pNcodHolding = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private DBConn oConn;

    public cReporteClientesRiesgosos() {

    }

    public cReporteClientesRiesgosos(ref DBConn oConn) {
      this.oConn = oConn;
    }

    public DataTable Get()
    {
      DataTable dtData;

      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();
        cSQL.Append("select clientes_riesgosos.snombre, clientes_riesgosos.saldo as 'DeudaTotal', clientes_riesgosos.vencido as 'DeudaVencida', ");
        cSQL.Append(" clientes_riesgosos.ncodigodeudor ");
        cSQL.Append(" from clientes_riesgosos ");
        if (string.IsNullOrEmpty(pNcodHolding))
          cSQL.Append(" where  clientes_riesgosos.nkey_cliente in (").Append(lngCodNkey).Append(") ");
        else
          cSQL.Append(" where cliente.ncodholding = ").Append(pNcodHolding);

        cSQL.Append(" order by clientes_riesgosos.ncodigodeudor ");

        dtData = oConn.Select(cSQL.ToString());
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
