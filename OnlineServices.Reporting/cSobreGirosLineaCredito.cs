using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using OnlineServices.Conn;

namespace OnlineServices.Reporting
{
  public class cSobreGirosLineaCredito
  {
    private string lngCodNkey;
    public string CodNkey { get { return lngCodNkey; } set { lngCodNkey = value; } }

    private string sTipoUsuario;
    public string TipoUsuario { get { return sTipoUsuario; } set { sTipoUsuario = value; } }

    private string lngNkeyUsuario;
    public string NkeyUsuario { get { return lngNkeyUsuario; } set { lngNkeyUsuario = value; } }

    private string pNcodHolding;
    public string NcodHolding { get { return pNcodHolding; } set { pNcodHolding = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private DBConn oConn;

    public cSobreGirosLineaCredito() {
    }

    public cSobreGirosLineaCredito(ref DBConn oConn) {
      this.oConn = oConn;
    }

    public DataTable Get()
    {
      DataTable dtData;

      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();

        if (string.IsNullOrEmpty(pNcodHolding))
        {

          cSQL.Append("Select ncodigodeudor, snombre,  fecha, isnull(lineactual,0) as lineactual, isnull(saldo,0) as saldo From saldo_lineacreditoweb Where nkey_cliente in (").Append(lngCodNkey).Append(") ");

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
            cSQL.Append("  and saldo_lineacreditoweb.nkey_deudor = ").Append(lngNkeyUsuario);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "V"))
            cSQL.Append("  and saldo_lineacreditoweb.nkey_deudor in (select codigodeudor.nkey_deudor from codigodeudor where codigodeudor.nkey_vendedor = ").Append(lngNkeyUsuario).Append(" and codigodeudor.nkey_cliente in (").Append(lngCodNkey).Append(") )  ");

          cSQL.Append(" order by ncodigodeudor ");
        }
        else {
          cSQL.Append("Select ncodigodeudor, snombre,  dfechacreditoactual as 'fecha', isnull(nlineacreditoactual,0) as lineactual, isnull(saldo,0) as saldo From saldo_lineacredito Where ncodholding = ").Append(pNcodHolding);
          cSQL.Append(" order by ncodigodeudor ");

        }
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
