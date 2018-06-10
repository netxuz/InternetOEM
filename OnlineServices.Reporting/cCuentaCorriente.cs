using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using OnlineServices.Conn;

namespace OnlineServices.Reporting
{
  public class cCuentaCorriente
  {
    private string lngCodNkey;
    public string CodNkey { get { return lngCodNkey; } set { lngCodNkey = value; } }

    private string lngCodDeudor;
    public string CodDeudor { get { return lngCodDeudor; } set { lngCodDeudor = value; } }

    private string sTipoUsuario;
    public string TipoUsuario { get { return sTipoUsuario; } set { sTipoUsuario = value; } }

    private string lngNkeyUsuario;
    public string NkeyUsuario { get { return lngNkeyUsuario; } set { lngNkeyUsuario = value; } }

    private string pEstado;
    public string Estado { get { return pEstado; } set { pEstado = value; } }


    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private DBConn oConn;

    public cCuentaCorriente() {

    }

    public cCuentaCorriente(ref DBConn oConn) {
      this.oConn = oConn;
    }

    public DataTable Get()
    {
      DataTable dtData;

      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();

        cSQL.Append("select Estado, TipoDocumento, NumeroDocumento, Emision, sOrigen, NumeroOrigen, ");
        cSQL.Append(" nMontoOriginal, dVencimiento, nDebe, nHaber, nSaldo, ");
        cSQL.Append(" (select sum(convert(float, nDebe)) from Cuenta_Corriente where nKey_Cliente = ").Append(lngCodNkey).Append(" and nKey_Deudor = ").Append(lngCodDeudor);

        if (!string.IsNullOrEmpty(pEstado))
          cSQL.Append(" and Estado = '").Append(pEstado).Append("'");

        if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
          cSQL.Append("  and Cuenta_Corriente.nkey_deudor = ").Append(lngNkeyUsuario);

        if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "V"))
          cSQL.Append("  and Cuenta_Corriente.nkey_deudor in (select codigodeudor.nkey_deudor from codigodeudor where codigodeudor.nkey_vendedor = ").Append(lngNkeyUsuario).Append(" and codigodeudor.nkey_cliente = ").Append(lngCodNkey).Append(" )  ");

        cSQL.Append(" ) as tot_debe, ");
        cSQL.Append(" (select sum(convert(float, nHaber)) from Cuenta_Corriente where nKey_Cliente = ").Append(lngCodNkey).Append(" and nKey_Deudor = ").Append(lngCodDeudor);

        if (!string.IsNullOrEmpty(pEstado))
          cSQL.Append(" and Estado ='").Append(pEstado).Append("'");

        if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
          cSQL.Append("  and Cuenta_Corriente.nkey_deudor = ").Append(lngNkeyUsuario);

        if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "V"))
          cSQL.Append("  and Cuenta_Corriente.nkey_deudor in (select codigodeudor.nkey_deudor from codigodeudor where codigodeudor.nkey_vendedor = ").Append(lngNkeyUsuario).Append(" and codigodeudor.nkey_cliente = ").Append(lngCodNkey).Append(" )  ");

        cSQL.Append(") as tot_haber, ");
        cSQL.Append("(select sum(convert(float, nSaldo)) from Cuenta_Corriente where nKey_Cliente = ").Append(lngCodNkey).Append(" and nKey_Deudor = ").Append(lngCodDeudor);

        if (!string.IsNullOrEmpty(pEstado))
          cSQL.Append(" and Estado ='").Append(pEstado).Append("'");

        if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
          cSQL.Append("  and Cuenta_Corriente.nkey_deudor = ").Append(lngNkeyUsuario);

        if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "V"))
          cSQL.Append("  and Cuenta_Corriente.nkey_deudor in (select codigodeudor.nkey_deudor from codigodeudor where codigodeudor.nkey_vendedor = ").Append(lngNkeyUsuario).Append(" and codigodeudor.nkey_cliente = ").Append(lngCodNkey).Append(" )  ");

        cSQL.Append(" ) as tot_saldo ");
        cSQL.Append(" from Cuenta_Corriente where nKey_Cliente = ").Append(lngCodNkey).Append(" and nKey_Deudor = ").Append(lngCodDeudor);

        if (!string.IsNullOrEmpty(pEstado))
          cSQL.Append(" and Estado ='").Append(pEstado).Append("'");

        if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
          cSQL.Append("  and Cuenta_Corriente.nkey_deudor = ").Append(lngNkeyUsuario);

        if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "V"))
          cSQL.Append("  and Cuenta_Corriente.nkey_deudor in (select codigodeudor.nkey_deudor from codigodeudor where codigodeudor.nkey_vendedor = ").Append(lngNkeyUsuario).Append(" and codigodeudor.nkey_cliente = ").Append(lngCodNkey).Append(" )  ");

        cSQL.Append(" order by nkey_cuentaCorriente ");

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
