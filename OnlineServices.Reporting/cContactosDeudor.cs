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

    private string pNcodHolding;
    public string NcodHolding { get { return pNcodHolding; } set { pNcodHolding = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private DBConn oConn;

    public cContactosDeudor()
    {

    }

    public cContactosDeudor(ref DBConn oConn)
    {
      this.oConn = oConn;
    }

    public DataTable Get()
    {
      DataTable dtData;
      string Condicion = " where ";

      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();
        cSQL.Append("select ");

        if (!string.IsNullOrEmpty(pNcodHolding))
        {
          cSQL.Append(" cliente.ncodholding, ltrim(rtrim(contactosdeudor.ncodigo)) as 'ncodigo',  ");
        }
        else
        {
          if (string.IsNullOrEmpty(lngCodDeudor))
          {
            cSQL.Append(" ltrim(rtrim(contactosdeudor.ncodigo)) as 'ncodigo', ");
          }
        }

        cSQL.Append(" ltrim(rtrim(contactosdeudor.snombre)) as 'snombre', ltrim(rtrim(contactosdeudor.scargo)) as 'scargo', ltrim(rtrim(contactosdeudor.stelefono)) as 'stelefono', ltrim(rtrim(contactosdeudor.semail)) as 'semail', ltrim(rtrim(contactosdeudor.funcion)) as 'funcion'   ");
        cSQL.Append(" from contactosdeudor ");

        if (!string.IsNullOrEmpty(pNcodHolding))
        {
          cSQL.Append(" join cliente on (cliente.nkey_cliente = contactosdeudor.nKey_Cliente ) ");
          cSQL.Append(" where cliente.ncodholding = ").Append(pNcodHolding);
          cSQL.Append(" and cliente.bCuentaCorriente <> 0 ");
        }
        else
        {
          if (!string.IsNullOrEmpty(lngCodDeudor))
          {
            cSQL.Append(" where contactosdeudor.nkey_deudor = ").Append(lngCodDeudor);
          }
          else
          {
            cSQL.Append(" where contactosdeudor.nkey_cliente in (").Append(lngCodNkey).Append(") ");
          }
        }
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
