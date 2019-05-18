using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using OnlineServices.Conn;

namespace OnlineServices.Reporting
{
  public class cPromesasPagos
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

    private bool blnTypeQuery;
    public bool TypeQuery { get { return blnTypeQuery; } set { blnTypeQuery = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private DBConn oConn;

    public cPromesasPagos() {

    }

    public cPromesasPagos(ref DBConn oConn) {
      this.oConn = oConn;

    }

    public DataTable Get()
    {
      DataTable dtData;

      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();
        if (blnTypeQuery)
        {
          cSQL.Append("select CodigoDeudor.nCodigoDeudor AS 'CódigoDeudor', ");
          cSQL.Append(" Última_Gestión, Monto_Prometido, ");
          cSQL.Append(" sum(total_pagado) as 'Total_Pagado', sum(Saldo) as 'Saldo', avg(Atraso) as 'Atraso' , Observaciones, Razón_Social, FechaCompromiso, ComerntarioDeudor, ComerntarioAnalista   ");
          cSQL.Append(" from promesa_pago ");
          cSQL.Append(" LEFT JOIN CodigoDeudor ");
          cSQL.Append("  ON(promesa_pago.nKey_Cliente = CodigoDeudor.nKey_Cliente ");
          cSQL.Append("  AND promesa_pago.nKey_Deudor = CodigoDeudor.nKey_Deudor) ");
          cSQL.Append(" JOIN cliente ON(promesa_pago.nKey_Cliente = cliente.nKey_Cliente)");

          if (string.IsNullOrEmpty(pNcodHolding))
            cSQL.Append(" where promesa_pago.nkey_cliente in (").Append(lngCodNkey).Append(") ");
          else
            cSQL.Append(" cliente.ncodholding = ").Append(pNcodHolding);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
            cSQL.Append("  and promesa_pago.nkey_deudor = ").Append(lngNkeyUsuario);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "V"))
            cSQL.Append("  and codigodeudor.nkey_vendedor = ").Append(lngNkeyUsuario);

          cSQL.Append(" and promesa_pago.Fecha_Pago between convert(datetime,'").Append(pDtFchIni).Append("') and convert(datetime,'").Append(pDtFchFin).Append("') ");
          cSQL.Append("  group by CodigoDeudor.nCodigoDeudor , 	 Última_Gestión, Monto_Prometido,  Observaciones, Razón_Social, FechaCompromiso, ComerntarioDeudor, ComerntarioAnalista   ");

        }
        else {

          cSQL.Append("select CodigoDeudor.nCodigoDeudor AS 'CódigoDeudor', ");
          cSQL.Append(" Última_Gestión, Monto_Prometido, ");
          cSQL.Append(" sum(total_pagado) as 'Total_Pagado', sum(Saldo) as 'Saldo', avg(Atraso) as 'Atraso' , Observaciones, Razón_Social, FechaCompromiso, ComerntarioDeudor, ComerntarioAnalista   ");
          cSQL.Append(" from promesa_pago ");
          cSQL.Append(" LEFT JOIN CodigoDeudor ");
          cSQL.Append(" ON(promesa_pago.nKey_Cliente = CodigoDeudor.nKey_Cliente ");
          cSQL.Append(" AND promesa_pago.nKey_Deudor = CodigoDeudor.nKey_Deudor) ");
          cSQL.Append(" JOIN servicio ON (servicio.nkey_cliente = promesa_pago.nkey_cliente and ");
          cSQL.Append(" servicio.nkey_deudor = promesa_pago.nkey_deudor and servicio.nkey_analista = ").Append(lngCodDeudor).Append(" ) ");
          cSQL.Append(" JOIN Analista ON (analista.nkey_analista = servicio.nkey_analista) ");
          cSQL.Append(" where promesa_pago.nkey_cliente in (").Append(lngCodNkey).Append(") and promesa_pago.Fecha_Pago between convert(datetime,'").Append(pDtFchIni).Append("') and convert(datetime,'").Append(pDtFchFin).Append("') ");

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
            cSQL.Append("  and promesa_pago.nkey_deudor = ").Append(lngNkeyUsuario);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "V"))
            cSQL.Append("  and codigodeudor.nkey_vendedor = ").Append(lngNkeyUsuario);

          cSQL.Append("  group by CodigoDeudor.nCodigoDeudor , 	 Última_Gestión, Monto_Prometido,  Observaciones, Razón_Social, FechaCompromiso, ComerntarioDeudor, ComerntarioAnalista   ");

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
