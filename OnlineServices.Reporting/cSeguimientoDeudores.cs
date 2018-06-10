using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using OnlineServices.Conn;

namespace OnlineServices.Reporting
{
  public class cSeguimientoDeudores
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

    private string pDtFchFin;
    public string DtFchFin { get { return pDtFchFin; } set { pDtFchFin = value; } }

    private string lngNumFactura;
    public string NumFactura { get { return lngNumFactura; } set { lngNumFactura = value; } }

    private bool blnIndAccion;
    public bool IndAccion { get { return blnIndAccion; } set { blnIndAccion = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private DBConn oConn;

    public cSeguimientoDeudores()
    {

    }

    public cSeguimientoDeudores(ref DBConn oConn)
    {
      this.oConn = oConn;
    }

    public DataTable Get()
    {
      DataTable dtData;

      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();

        if (blnIndAccion)
        {
          cSQL.Append("select distinct  Fecha_Gestión, Contacto, Compromiso_Pago, ");
          cSQL.Append(" Monto_prometido, Deudor, Analista, Observación, Razón_Social from seguimiento_deudores_t  ");
          cSQL.Append(" where nkey_cliente = ").Append(lngCodNkey);
          if (!string.IsNullOrEmpty(lngCodDeudor))
            cSQL.Append(" and nkey_deudor = ").Append(lngCodDeudor);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
            cSQL.Append("  and nkey_deudor = ").Append(lngNkeyUsuario);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "V"))
            cSQL.Append("  and nkey_deudor in ( select nkey_deudor from codigodeudor where nkey_cliente = ").Append(lngCodNkey).Append(" and nkey_vendedor  = ").Append(lngNkeyUsuario).Append(" ) ");

          cSQL.Append(" and Fecha_Gestión between convert(datetime,'").Append(pDtFchIni).Append("') and convert(datetime,'").Append(pDtFchFin).Append("') ");
          cSQL.Append(" order by Fecha_Gestión ");

        }
        else {
          cSQL.Append("select distinct  Fecha_Gestión, Contacto, Compromiso_Pago, ");
          cSQL.Append(" Monto_prometido, Deudor, Analista, Observación, Razón_Social from seguimiento_deudores_t  ");
          cSQL.Append(" where nkey_cliente =").Append(lngCodNkey);
          cSQL.Append(" and Número_Factura = ").Append(lngNumFactura);
          cSQL.Append(" order by Fecha_Gestión ");

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
