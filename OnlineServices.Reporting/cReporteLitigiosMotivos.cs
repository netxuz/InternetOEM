using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using OnlineServices.Conn;

namespace OnlineServices.Reporting
{
  public class cReporteLitigiosMotivos
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

    public cReporteLitigiosMotivos() {

    }

    public cReporteLitigiosMotivos(ref DBConn oConn) {
      this.oConn = oConn;
    }

    public DataTable Get()
    {
      DataTable dtData;

      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();
        cSQL.Append("select count(litigio.nmontolitigio) as cantidad, sum(litigio.nmontolitigio) as monto, sdescripcion  from litigio, cliente ");
        cSQL.Append(" where cliente.nkey_cliente = litigio.nkey_cliente");

        if (string.IsNullOrEmpty(pNcodHolding))
          cSQL.Append(" and cliente.nkey_cliente in(").Append(lngCodNkey).Append(") ");
        else
          cSQL.Append(" and cliente.ncodholding = ").Append(pNcodHolding);

        if (!string.IsNullOrEmpty(lngCodDeudor))
          cSQL.Append(" and litigio.nkey_deudor =").Append(lngCodDeudor);

        if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
          cSQL.Append("  and litigio.nkey_deudor = ").Append(lngNkeyUsuario);

        if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "V"))
          cSQL.Append("  and litigio.nkey_deudor  in (select nkey_deudor from codigodeudor where nkey_cliente in(").Append(lngCodNkey).Append(") and nkey_vendedor = ").Append(lngNkeyUsuario).Append(") ");

        cSQL.Append(" and litigio.dFechaIngreso between convert(datetime,'").Append(pDtFchIni).Append("') and convert(datetime,'").Append(pDtFchFin).Append("') ");
        cSQL.Append(" group by litigio.sdescripcion ");

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
