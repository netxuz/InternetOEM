using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using OnlineServices.Conn;

namespace OnlineServices.Reporting
{
  public class cReporteMotivosNoPago
  {
    private string lngCodNkey;
    public string CodNkey { get { return lngCodNkey; } set { lngCodNkey = value; } }

    private string lngCodDeudor;
    public string CodDeudor { get { return lngCodDeudor; } set { lngCodDeudor = value; } }

    private string sTipoUsuario;
    public string TipoUsuario { get { return sTipoUsuario; } set { sTipoUsuario = value; } }

    private string lngNkeyUsuario;
    public string NkeyUsuario { get { return lngNkeyUsuario; } set { lngNkeyUsuario = value; } }

    private string pNcodHolding;
    public string NcodHolding { get { return pNcodHolding; } set { pNcodHolding = value; } }

    private string pDtFchIni;
    public string DtFchIni { get { return pDtFchIni; } set { pDtFchIni = value; } }

    private string pDtFchFin;
    public string DtFchFin { get { return pDtFchFin; } set { pDtFchFin = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private DBConn oConn;

    public cReporteMotivosNoPago() {

    }

    public cReporteMotivosNoPago(ref DBConn oConn) {
      this.oConn = oConn;
    }

    public DataTable Get()
    {
      DataTable dtData;

      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();
        cSQL.Append("select smotivonopago, count(*) as cantidad, sum(monto) as monto from montosnopago ");
        cSQL.Append(" join cliente on (cliente.nkey_cliente = montosnopago.nKey_Cliente )");

        if (string.IsNullOrEmpty(pNcodHolding))
          cSQL.Append(" where montosnopago.nkey_cliente in(").Append(lngCodNkey).Append(") ");
        else
          cSQL.Append(" where cliente.ncodholding = ").Append(pNcodHolding);

        if (!string.IsNullOrEmpty(lngCodDeudor))
          cSQL.Append(" and nkey_deudor = ").Append(lngCodDeudor);

        if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
          cSQL.Append("  and nkey_deudor = ").Append(lngNkeyUsuario);

        if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "V"))
          cSQL.Append("  and nkey_deudor  in (select nkey_deudor from codigodeudor where nkey_cliente in(").Append(lngCodNkey).Append(") and nkey_vendedor = ").Append(lngNkeyUsuario).Append(") ");

        cSQL.Append(" and dfecha between convert(datetime,'").Append(pDtFchIni).Append("') and convert(datetime,'").Append(pDtFchFin).Append("') ");
        cSQL.Append(" group by smotivonopago ");
        
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
