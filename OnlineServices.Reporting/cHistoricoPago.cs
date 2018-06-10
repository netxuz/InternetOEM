using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using OnlineServices.Conn;

namespace OnlineServices.Reporting
{
  public class cHistoricoPago
  {
    DBConn.SQLParameters oParam;
    DBConn.DataTypeSQL TypeSQL = new DBConn.DataTypeSQL();

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

    private string sQuery;
    public string Query { get { return sQuery; } set { sQuery = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private DBConn oConn;

    public cHistoricoPago()
    {

    }

    public cHistoricoPago(ref DBConn oConn)
    {
      this.oConn = oConn;
    }

    public DataTable Get()
    {
      oParam = new DBConn.SQLParameters(10);
      DataTable dtData;

      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();
        cSQL.Append("select CodigoDeudor.nCodigoDeudor AS 'Código', Deudor.sNombre AS 'RazónSocial', ");
        cSQL.Append(" Número_Factura, Plazo, Vencimiento, Monto_Factura, Tipo_Pago, Número_Pago, Abono, Atraso, ");
        cSQL.Append(" (select sum(Monto_Factura) from historico_pagos ");
        cSQL.Append(" where nkey_cliente = ").Append(lngCodNkey);
        if (!string.IsNullOrEmpty(lngCodDeudor))
          cSQL.Append(" and nKey_Deudor = ").Append(lngCodDeudor);

        if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
          cSQL.Append("  and nkey_deudor = ").Append(lngNkeyUsuario);

        if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "V"))
          cSQL.Append("  and nkey_deudor  in (select nkey_deudor from codigodeudor where nkey_cliente = ").Append(lngCodNkey).Append(" and nkey_vendedor = ").Append(lngNkeyUsuario).Append(") ");

        cSQL.Append(" and fechapago between convert(datetime,'").Append(pDtFchIni).Append("') and convert(datetime,'").Append(pDtFchFin).Append("')) as tot_factura, ");
        cSQL.Append(" (select sum(Abono) from historico_pagos ");
        cSQL.Append(" where nkey_cliente = ").Append(lngCodNkey);

        if (!string.IsNullOrEmpty(lngCodDeudor))
          cSQL.Append(" and nKey_Deudor = ").Append(lngCodDeudor);

        if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
          cSQL.Append("  and nkey_deudor = ").Append(lngNkeyUsuario);

        if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "V"))
          cSQL.Append("  and nkey_deudor  in (select nkey_deudor from codigodeudor where nkey_cliente = ").Append(lngCodNkey).Append(" and nkey_vendedor = ").Append(lngNkeyUsuario).Append(") ");

        cSQL.Append(" and fechapago between convert(datetime,'").Append(pDtFchIni).Append("') and convert(datetime,'").Append(pDtFchFin).Append("')) as tot_abono ");
        cSQL.Append(" from historico_pagos ");
        cSQL.Append(" LEFT JOIN Deudor ");
        cSQL.Append("    ON (historico_pagos.nKey_Deudor = Deudor.nKey_Deudor) ");
        cSQL.Append(" LEFT JOIN CodigoDeudor ");
        cSQL.Append("  ON(historico_pagos.nKey_Cliente = CodigoDeudor.nKey_Cliente ");
        cSQL.Append("   AND historico_pagos.nKey_Deudor = CodigoDeudor.nKey_Deudor) ");
        cSQL.Append(" where historico_pagos.nkey_cliente = ").Append(lngCodNkey);

        if (!string.IsNullOrEmpty(lngCodDeudor))
          cSQL.Append(" and historico_pagos.nkey_deudor = ").Append(lngCodDeudor);

        if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
          cSQL.Append("  and historico_pagos.nkey_deudor = ").Append(lngNkeyUsuario);

        if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "V"))
          cSQL.Append("  and codigodeudor.nkey_vendedor = ").Append(lngNkeyUsuario);

        cSQL.Append(" and historico_pagos.fechapago Between convert(datetime,'").Append(pDtFchIni).Append("') and convert(datetime,'").Append(pDtFchFin).Append("') ");
        cSQL.Append(" order by historico_pagos.fechapago ");

        dtData = oConn.Select(cSQL.ToString());
        sQuery = cSQL.ToString();
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
