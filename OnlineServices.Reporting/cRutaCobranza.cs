using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using OnlineServices.Conn;

namespace OnlineServices.Reporting
{
  public class cRutaCobranza
  {
    private string lngCodNkey;
    public string CodNkey { get { return lngCodNkey; } set { lngCodNkey = value; } }

    private string sTipoUsuario;
    public string TipoUsuario { get { return sTipoUsuario; } set { sTipoUsuario = value; } }

    private string lngNkeyUsuario;
    public string NkeyUsuario { get { return lngNkeyUsuario; } set { lngNkeyUsuario = value; } }

    private string pDtFchIni;
    public string DtFchIni { get { return pDtFchIni; } set { pDtFchIni = value; } }

    private string pDtFchFin;
    public string DtFchFin { get { return pDtFchFin; } set { pDtFchFin = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private DBConn oConn;

    public cRutaCobranza()
    {
    }

    public cRutaCobranza(ref DBConn oConn)
    {
      this.oConn = oConn;
    }

    public DataTable Get()
    {
      DataTable dtData;
      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();

        cSQL.Append("Select ruta.dfecharuta, Codigodeudor.ncodigodeudor, Deudor.snombre ");
        cSQL.Append(" From ruta Join codigodeudor On (codigodeudor.nkey_cliente = ruta.nkey_cliente and codigodeudor.nkey_deudor = ruta.nkey_deudor) ");
        cSQL.Append(" Join deudor On (deudor.nkey_deudor = ruta.nkey_deudor) ");
        cSQL.Append(" Where ruta.nkey_cliente = ").Append(lngCodNkey).Append(" and  ruta.dfecharuta between convert(datetime,'").Append(pDtFchIni).Append("') and convert(datetime,'").Append(pDtFchFin).Append("') ");
        if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
          cSQL.Append("  and deudor.nkey_deudor = ").Append(lngNkeyUsuario);

        if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "V"))
          cSQL.Append("  and codigodeudor.nkey_vendedor = ").Append(lngNkeyUsuario);

        cSQL.Append(" Order by ruta.dfecharuta, Codigodeudor.ncodigodeudor ");

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
