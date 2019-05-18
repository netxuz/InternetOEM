using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using OnlineServices.Conn;

namespace OnlineServices.Reporting
{
  public class cClientesRetenidos
  {
    private string lngCodNkey;
    public string CodNkey { get { return lngCodNkey; } set { lngCodNkey = value; } }

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

    public cClientesRetenidos() {

    }

    public cClientesRetenidos(ref DBConn oConn) {
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
          cSQL.Append("SELECT  top 100 percent cliente.snombre as 'Nomcliente' , CodigoDeudor.nCodigoDeudor, Deudor.sNombre AS 'NOMBRE', Menu_CategoriaRiesgo.sNombre, ");
          cSQL.Append(" DATEDIFF(DAY,LineaCredito.dFechaEstatusCredito,GETDATE()) AS 'RETENIDO', ");
          cSQL.Append(" (select sum(saldo) from vista_antiguedad_deuda_new where nkey_cliente in(").Append(lngCodNkey).Append(") ");
          cSQL.Append(" and nkey_deudor =  LineaCredito.nKey_Deudor) AS 'DeudaTotal', (select sum(saldo) from vista_antiguedad_deuda_new where nkey_cliente in(").Append(lngCodNkey).Append(") ");
          cSQL.Append(" and nkey_deudor =  LineaCredito.nKey_Deudor and dfechavencimiento < getdate()) AS 'DeudaVencida' , ");
          cSQL.Append(" gestiondeudor.sobservacion as 'Observacion' , vendedor.snombre as 'NomVendedor', ");
          cSQL.Append(" (select sum(saldo*dias_atraso) / sum(saldo) from vista_saldo_factura v1 where nkey_cliente in(").Append(lngCodNkey).Append(") ");
          cSQL.Append(" and v1.nkey_deudor = LineaCredito.nKey_Deudor and v1.saldo > 0 and v1.dias_atraso > 0 ) as 'DiasPonderado' ");
          cSQL.Append(" FROM LineaCredito JOIN CodigoDeudor ON(CodigoDeudor.nKey_Cliente in(").Append(lngCodNkey).Append(") AND CodigoDeudor.nKey_Deudor = LineaCredito.nKey_Deudor) ");
          cSQL.Append(" JOIN Deudor ON (Deudor.nKey_Deudor = LineaCredito.nKey_Deudor) LEFT JOIN CategoriaRiesgo ON(LineaCredito.nKey_Cliente in(").Append(lngCodNkey).Append(") AND LineaCredito.nKey_Deudor = CategoriaRiesgo.nKey_Deudor) ");
          cSQL.Append(" LEFT JOIN Menu_CategoriaRiesgo ON(Menu_CategoriaRiesgo.nKey_MenuCategoriaRiesgo = CategoriaRiesgo.nKey_MenuCategoriaRiesgo) ");
          cSQL.Append(" JOIN CLIENTE ON (cliente.nkey_cliente in(").Append(lngCodNkey).Append(") ) ");
          cSQL.Append(" LEFT JOIN gestiondeudor ON (gestiondeudor.nkey_cliente in(").Append(lngCodNkey).Append(") ");
          cSQL.Append(" AND gestiondeudor.nkey_deudor =LineaCredito.nkey_deudor AND gestiondeudor.nKey_GestionDeudor = (select max(nKey_GestionDeudor) from ");
          cSQL.Append(" gestiondeudor GD where GD.nkey_cliente in(").Append(lngCodNkey).Append(") and GD.nkey_deudor = gestiondeudor.nkey_deudor )) ");
          cSQL.Append(" JOIN servicio ON (servicio.nkey_cliente  = cliente.nkey_cliente and servicio.nkey_deudor = CodigoDeudor.nKey_Deudor and servicio.nkey_analista != 4) ");
          cSQL.Append(" LEFT JOIN vendedor ON(vendedor.nkey_cliente = servicio.nkey_cliente and vendedor.nkey_vendedor = servicio.nKey_vendedor) ");
          cSQL.Append(" WHERE LineaCredito.sEstatusCredito LIKE '%Retenido%' AND LineaCredito.nKey_Cliente in(").Append(lngCodNkey).Append(") ");

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
            cSQL.Append("  and codigodeudor.nkey_deudor = ").Append(lngNkeyUsuario);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "V"))
            cSQL.Append("  and codigodeudor.nkey_vendedor = ").Append(lngNkeyUsuario);

          cSQL.Append(" ORDER BY nCodigoDeudor ");
        }
        else {
          cSQL.Append("select * from clientes_retenidos where clientes_retenidos.ncodholding  =").Append(pNcodHolding);
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
