using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using OnlineServices.Conn;

namespace OnlineServices.Reporting
{
  public class cContactosDeudor
  {
    private string lngCodNkey;
    public string CodNkey { get { return lngCodNkey; } set { lngCodNkey = value; } }

    private string lngCodDeudor;
    public string CodDeudor { get { return lngCodDeudor; } set { lngCodDeudor = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private DBConn oConn;

    public cContactosDeudor() {

    }

    public cContactosDeudor(ref DBConn oConn) {
      this.oConn = oConn;
    }

    public DataTable Get()
    {
      DataTable dtData;
      string Condicion = " where ";

      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();
        cSQL.Append("Select ltrim(rtrim(contactosdeudor.ncodigo)) as 'ncodigo', ltrim(rtrim(contactosdeudor.snombre)) as 'snombre', ltrim(rtrim(contactosdeudor.scargo)) as 'scargo', ltrim(rtrim(contactosdeudor.stelefono)) as 'stelefono', ltrim(rtrim(contactosdeudor.semail)) as 'semail', ltrim(rtrim(contactosdeudor.funcion)) as 'funcion'   ");
        cSQL.Append(" from contactosdeudor ");

        if (!string.IsNullOrEmpty(lngCodDeudor))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" contactosdeudor.nKey_Deudor=").Append(lngCodDeudor);
        }

        cSQL.Append(Condicion);
        cSQL.Append("  contactosdeudor.nKey_cliente = ").Append(lngCodNkey);
        cSQL.Append("  and activo = 'S' order by  contactosdeudor.ncodigo ");

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
