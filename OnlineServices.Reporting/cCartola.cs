using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using OnlineServices.Conn;

namespace OnlineServices.Reporting
{
  public class cCartola
  {
    private string lngCodNkey;
    public string CodNkey { get { return lngCodNkey; } set { lngCodNkey = value; } }

    private string lngCodDeudor;
    public string CodDeudor { get { return lngCodDeudor; } set { lngCodDeudor = value; } }

    private string sTipoUsuario;
    public string TipoUsuario { get { return sTipoUsuario; } set { sTipoUsuario = value; } }

    private string lngNkeyUsuario;
    public string NkeyUsuario { get { return lngNkeyUsuario; } set { lngNkeyUsuario = value; } }

    private string pDtMes;
    public string DtMes { get { return pDtMes; } set { pDtMes = value; } }

    private string pDtAno;
    public string DtAno { get { return pDtAno; } set { pDtAno = value; } }

    private string lngKeyAct;
    public string KeyAct { get { return lngKeyAct; } set { lngKeyAct = value; } }

    private string lngKeyAnt;
    public string KeyAnt { get { return lngKeyAnt; } set { lngKeyAnt = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private DBConn oConn;

    public cCartola()
    {

    }

    public cCartola(ref DBConn oConn)
    {
      this.oConn = oConn;
    }

    public DataTable Get()
    {
      DataTable dtData;

      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();

        cSQL.Append(" select SUM(historicocartola.cantidaddocumentos) AS 'Cantidad_Documentos', ");
        cSQL.Append(" sum(historicocartola.montodocumento) AS 'Monto_Documento', ");
        cSQL.Append(" sum(historicocartola.cantidadpagos) AS 'Cantidad_Pagos', ");
        cSQL.Append(" sum(historicocartola.montopago) AS 'Monto', ");
        cSQL.Append(" SUM(historicocartola.MontoFactura) AS 'Monto_Factura', ");
        cSQL.Append(" SUM(historicocartola.montoNC) AS 'MontoNC', ");
        cSQL.Append(" SUM(historicocartola.montoND) AS 'MontoND', ");
        cSQL.Append(" SUM(historicocartola.montofp) AS 'MontoFP', ");
        cSQL.Append(" SUM(historicocartola.montopagos2) AS 'Monto_Pago', ");
        cSQL.Append(" historicocartola.fecha AS 'Fecha_Transacción', ");
        cSQL.Append(" historicocartola.tipopago AS 'Tipo_Pago', ");
        cSQL.Append(" historicocartola.tipodocumento AS 'Tipo_Documento' ");
        cSQL.Append(" From historicocartola ");

        if (!string.IsNullOrEmpty(lngCodDeudor))
        {
          cSQL.Append(" Join Deudor ON ( historicocartola.nkey_deudor = Deudor.nkey_deudor And Deudor.nkey_deudor = ").Append(lngCodDeudor);
          cSQL.Append(" and  historicocartola.nkey_deudor = ").Append(lngCodDeudor).Append(")");
        }
        cSQL.Append(" WHERE historicocartola.nkey_cliente = ").Append(lngCodNkey);
        cSQL.Append(" and historicocartola.nkey_tablafechacierre = ").Append(lngKeyAct);

        if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
          cSQL.Append("  and historicocartola.nkey_deudor = ").Append(lngNkeyUsuario);

        if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "V"))
          cSQL.Append("  and historicocartola.nkey_deudor in (select codigodeudor.nkey_deudor from codigodeudor where codigodeudor.nkey_vendedor = ").Append(lngNkeyUsuario).Append(" and codigodeudor.nkey_cliente = ").Append(lngCodNkey).Append(" )  ");

        cSQL.Append(" group by fecha,tipopago,tipodocumento ");
        cSQL.Append(" Order by fecha, tipodocumento, tipopago  ");

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

    public DataTable GetDataMesAnterior()
    {
      DataTable dtData;

      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();
        cSQL.Append(" SELECT nKey_TablaFechaCierre as 'keyAnt', (dFechaFinalPeriodo + 1) as 'fechaInicial' ");
        cSQL.Append(" FROM TablaFechaCierre ");
        cSQL.Append(" WHERE  nPeriodoMes = ").Append(pDtMes);
        cSQL.Append(" AND nPeriodoAño = ").Append(pDtAno);
        cSQL.Append(" And nKey_Cliente =").Append(lngCodNkey);

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

    public DataTable GetDataMesSolicitado()
    {
      DataTable dtData;

      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();
        cSQL.Append(" SELECT nKey_TablaFechaCierre as 'keyAct', (dFechaFinalPeriodo) as 'fechaFinal' ");
        cSQL.Append(" FROM TablaFechaCierre ");
        cSQL.Append(" WHERE  nPeriodoMes = ").Append(pDtMes);
        cSQL.Append(" AND nPeriodoAño = ").Append(pDtAno);
        cSQL.Append(" And nKey_Cliente =").Append(lngCodNkey);

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

    public DataTable GetReturnSaldoAnterior()
    {
      DataTable dtData;

      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();
        cSQL.Append("SELECT Sum(Monto) as 'SaldoAnterior' FROM documentos_cierre ");
        cSQL.Append(" Join tablahistoricoservicio  ");
        cSQL.Append(" ON (tablahistoricoservicio.nkey_cliente = documentos_cierre.nkey_cliente ");
        cSQL.Append(" and tablahistoricoservicio.nkey_deudor = documentos_cierre.nkey_deudor ");
        cSQL.Append(" and tablahistoricoservicio.nkey_analista != 4 ");
        cSQL.Append(" AND tablahistoricoservicio.nKey_TablaFechaCierre = ").Append(lngKeyAnt).Append(")");
        cSQL.Append(" WHERE documentos_cierre.nKey_Cliente =").Append(lngCodNkey);

        if (!string.IsNullOrEmpty(lngKeyAct))
          cSQL.Append(" AND documentos_cierre.nKey_TablaFechaCierre =").Append(lngKeyAnt);

        if (!string.IsNullOrEmpty(lngCodDeudor))
          cSQL.Append(" AND documentos_cierre.nkey_deudor =").Append(lngCodDeudor);

        if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
          cSQL.Append("  and documentos_cierre.nkey_deudor = ").Append(lngNkeyUsuario);

        if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "V"))
          cSQL.Append("  and documentos_cierre.nkey_deudor in (select codigodeudor.nkey_deudor from codigodeudor where codigodeudor.nkey_vendedor = ").Append(lngNkeyUsuario).Append(" and codigodeudor.nkey_cliente = ").Append(lngCodNkey).Append(" )  ");

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
