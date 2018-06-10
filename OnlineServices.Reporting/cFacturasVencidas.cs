using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using OnlineServices.Conn;

namespace OnlineServices.Reporting
{
  public class cFacturasVencidas
  {
    private string lngCodNkey;
    public string CodNkey { get { return lngCodNkey; } set { lngCodNkey = value; } }

    private string lngCodDeudor;
    public string CodDeudor { get { return lngCodDeudor; } set { lngCodDeudor = value; } }

    private string sTipoUsuario;
    public string TipoUsuario { get { return sTipoUsuario; } set { sTipoUsuario = value; } }

    private string lngNkeyUsuario;
    public string NkeyUsuario { get { return lngNkeyUsuario; } set { lngNkeyUsuario = value; } }

    private string pDtFchIni;
    public string DtFchIni { get { return pDtFchIni; } set { pDtFchIni = value; } }

    private string plngMntMyr;
    public string lngMntMyr { get { return plngMntMyr; } set { plngMntMyr = value; } }

    private string plngAtrzMyr;
    public string lngAtrzMyr { get { return plngAtrzMyr; } set { plngAtrzMyr = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private DBConn oConn;

    public cFacturasVencidas() {

    }

    public cFacturasVencidas(ref DBConn oConn) {
      this.oConn = oConn;
    }

    public DataTable Get()
    {
      DataTable dtData;

      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();
        cSQL.Append("select nKey_Cliente, sTipoDocumento, nNumeroDocumento, nOrigen, dFechaEmision, ");
        cSQL.Append("dFechaVencimiento, nMonto, nSaldo, nAtraso, nCodigoDeudor , sNombreDeudor ");
        cSQL.Append(" from Facturas_Vencidas where nKey_Cliente = ").Append(lngCodNkey);
        if (!string.IsNullOrEmpty(lngCodDeudor))
          cSQL.Append(" and facturas_vencidas.keydeudor = ").Append(lngCodDeudor);

        if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
          cSQL.Append("  and facturas_vencidas.keydeudor = ").Append(lngNkeyUsuario);

        if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "V"))
          cSQL.Append("  and facturas_vencidas.keydeudor in (select codigodeudor.nkey_deudor from codigodeudor where codigodeudor.nkey_vendedor = ").Append(lngNkeyUsuario).Append(" and codigodeudor.nkey_cliente = ").Append(lngCodNkey).Append(" )  ");

        cSQL.Append(" and dFechaVencimiento <= convert(datetime,'").Append(pDtFchIni).Append("') ");
        cSQL.Append(" and nMonto > ").Append(plngMntMyr).Append(" and nAtraso >  ").Append(plngAtrzMyr);
        cSQL.Append(" order by dFechaVencimiento asc, nNumeroDocumento asc, nAtraso desc ");

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
