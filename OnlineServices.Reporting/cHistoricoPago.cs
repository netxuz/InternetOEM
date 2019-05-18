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

    private string pNcodHolding;
    public string NcodHolding { get { return pNcodHolding; } set { pNcodHolding = value; } }

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
        cSQL.Append(" select ");

        if (!string.IsNullOrEmpty(pNcodHolding)) {
          cSQL.Append(" cliente.ncod as 'ncodholding', CodigoDeudor.nCodigoDeudor AS 'Código', ");
        }
        else
        {
          if (string.IsNullOrEmpty(lngCodDeudor))
          {
            cSQL.Append(" CodigoDeudor.nCodigoDeudor AS 'Código', ");
          }
        }

        cSQL.Append(" Deudor.sNombre AS 'Razón Social', Tipo_Pago as 'Tipo Pago', Número_Pago as 'Número Pago', FechaCheque as 'Fecha Documento', TotalPago as 'Total Pago', Número_Factura as 'Número Factura', FecEmiFac as 'Emisión', Vencimiento, Monto_Factura as 'Aplic_Factura', datediff(dd,Vencimiento, FechaCheque) as 'DBT', NomVendedor as 'Vendedor' ");
        
        cSQL.Append(" from historico_pagos ");
        cSQL.Append(" LEFT JOIN Deudor ");
        cSQL.Append("    ON (historico_pagos.nKey_Deudor = Deudor.nKey_Deudor) ");
        cSQL.Append(" LEFT JOIN CodigoDeudor ");
        cSQL.Append("  ON(historico_pagos.nKey_Cliente = CodigoDeudor.nKey_Cliente ");
        cSQL.Append("   AND historico_pagos.nKey_Deudor = CodigoDeudor.nKey_Deudor) ");

        cSQL.Append(" join cliente on (cliente.nkey_cliente = historico_pagos.nkey_cliente ) ");

        if (!string.IsNullOrEmpty(pNcodHolding)) {
          cSQL.Append("where cliente.ncodholding = ").Append(pNcodHolding);
        }
        else {
          if (!string.IsNullOrEmpty(lngCodDeudor))
          {
            cSQL.Append(" and historico_pagos.nkey_deudor = ").Append(lngCodDeudor);
          }
          else {
            cSQL.Append(" where historico_pagos.nkey_cliente in(").Append(lngCodNkey).Append(") ");
          }
        }          

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
