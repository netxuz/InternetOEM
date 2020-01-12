using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using OnlineServices.Conn;


namespace OnlineServices.Dashboard
{
  public class cLitigios
  {
    DBConn.SQLParameters oParam;
    DBConn.DataTypeSQL TypeSQL = new DBConn.DataTypeSQL();

    private string lngCodHolding;
    public string CodHolding { get { return lngCodHolding; } set { lngCodHolding = value; } }

    private string lngnKeyCliente;
    public string nKeyCliente { get { return lngnKeyCliente; } set { lngnKeyCliente = value; } }

    private string lngnKeyDeudor;
    public string nKeyDeudor { get { return lngnKeyDeudor; } set { lngnKeyDeudor = value; } }

    private string lNumFactura;
    public string NumFactura { get { return lNumFactura; } set { lNumFactura = value; } }

    private string sNomCliente;
    public string NomCliente { get { return sNomCliente; } set { sNomCliente = value; } }

    private string sPeriodo;
    public string Periodo { get { return sPeriodo; } set { sPeriodo = value; } }

    private bool bOrderBy;
    public bool OrderBy { get { return bOrderBy; } set { bOrderBy = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private DBConn oConn;

    public cLitigios()
    {
    }

    public cLitigios(ref DBConn oConn)
    {
      this.oConn = oConn;
    }

    public DataTable getResumen()
    {
      oParam = new DBConn.SQLParameters(5);
      DataTable dtData;

      if (oConn.bIsOpen)
      {
        //StringBuilder cSQL = new StringBuilder();
        //cSQL.Append(" SELECT count(*) as total_litigios_cantidad, ");

        //if (!string.IsNullOrEmpty(lngnKeyCliente))
        //  cSQL.Append(" isnull(sum(isnull(Litigio.nMontoLitigio,0)),0) as Total_litigios_monto ");

        //if (!string.IsNullOrEmpty(lngCodHolding))
        //  cSQL.Append(" isnull(sum(dbo.aplicaTipoCambio(cliente.signomoneda, isnull(Litigio.nMontoLitigio,0), cliente.monedaholding)),0) as Total_litigios_monto ");

        //cSQL.Append(" FROM Litigio ");
        //cSQL.Append(" JOIN Factura ON (Factura.nKey_Factura = Litigio.nKey_Factura ) ");
        //cSQL.Append(" LEFT JOIN SolicitudNotaCredito  ON ((SolicitudNotaCredito.nKey_cliente = Litigio.nKey_cliente and SolicitudNotaCredito.nKey_deudor = Litigio.nKey_deudor and SolicitudNotaCredito.nKey_factura = Litigio.nKey_factura and SolicitudNotaCredito.nmontolitigio = Litigio.nmontolitigio and Litigio.nKey_Solicitud is null) or (SolicitudNotaCredito.nKey_cliente = Litigio.nKey_cliente and SolicitudNotaCredito.nKey_deudor = Litigio.nKey_deudor and Litigio.nKey_Solicitud is not null and SolicitudNotaCredito.nKey_SolicitudNotaCredito = Litigio.nKey_Solicitud )) ");
        //cSQL.Append(" JOIN CodigoDeudor ON(CodigoDeudor.nKey_Cliente = Litigio.nKey_Cliente AND CodigoDeudor.nKey_Deudor = Litigio.nKey_Deudor) ");
        //cSQL.Append(" JOIN Cliente ON(Cliente.nKey_Cliente = Litigio.nKey_Cliente) ");
        //cSQL.Append(" JOIN SERVICIO ON (SERVICIO.nkey_cliente = Litigio.nkey_cliente and SERVICIO.nkey_deudor = Litigio.nkey_deudor and SERVICIO.nkey_analista != 4 ) ");
        //cSQL.Append(" join vista_saldo_factura on (Factura.nNumeroFactura = vista_saldo_factura.nNumeroFactura and Factura.nKey_Cliente = vista_saldo_factura.nKey_Cliente and Factura.nKey_Deudor = vista_saldo_factura.nKey_Deudor and Factura.dFechaVencimiento = vista_saldo_factura.dfechavencimiento) ");

        //if (!string.IsNullOrEmpty(lngnKeyCliente))
        //{
        //  cSQL.Append(" where Cliente.nKey_Cliente = @nKey_Cliente  ");
        //  oParam.AddParameters("@nKey_Cliente", lngnKeyCliente, TypeSQL.Numeric);
        //}

        //if (!string.IsNullOrEmpty(lngCodHolding))
        //{
        //  cSQL.Append(" where Cliente.ncodholding = @ncodholding ");
        //  oParam.AddParameters("@ncodholding", lngCodHolding, TypeSQL.Numeric);
        //}

        //cSQL.Append(" AND NOT EXISTS (SELECT * FROM Normalizacion WHERE Normalizacion.nKey_Cliente = Litigio.nKey_Cliente AND Normalizacion.nKey_Deudor = Litigio.nKey_Deudor AND Normalizacion.nKey_Litigio = Litigio.nKey_Litigio) ");
        //cSQL.Append(" and factura.salfac <> 0 ");

        StringBuilder cSQL = new StringBuilder();
        cSQL.Append(" SELECT count(*) as cantidad_facturas, ");
        cSQL.Append(" isnull(sum(vista_saldo_factura.dias_atraso),0) AS dias_atraso, ");

        if (!string.IsNullOrEmpty(lngnKeyCliente))
          cSQL.Append(" isnull(sum(vista_saldo_factura.saldo),0) AS Total_litigios_monto ");

        if (!string.IsNullOrEmpty(lngCodHolding))
          cSQL.Append(" isnull(sum(dbo.aplicaTipoCambio(cliente.signomoneda, vista_saldo_factura.saldo, cliente.monedaholding)),0) AS Total_litigios_monto ");

        //cSQL.Append(" (sum (vista_saldo_factura.dias_atraso)) / (count(*)) AS dias_normalizacion ");

        cSQL.Append(" FROM Litigio ");
        cSQL.Append(" join vista_saldo_factura on (litigio.nKey_Cliente = vista_saldo_factura.nKey_Cliente and litigio.nKey_Deudor = vista_saldo_factura.nKey_Deudor and Litigio.nKey_Factura = vista_saldo_factura.ctakey) ");
        cSQL.Append(" JOIN Cliente ON(Cliente.nKey_Cliente = Litigio.nKey_Cliente) ");
        cSQL.Append(" JOIN CodigoDeudor ON(CodigoDeudor.nKey_Cliente = Litigio.nKey_Cliente AND CodigoDeudor.nKey_Deudor = Litigio.nKey_Deudor) ");
        cSQL.Append(" JOIN SERVICIO ON (SERVICIO.nkey_cliente = Litigio.nkey_cliente and SERVICIO.nkey_deudor = Litigio.nkey_deudor and SERVICIO.nkey_analista != 4 ) ");

        if (!string.IsNullOrEmpty(lngnKeyCliente)) {
          cSQL.Append(" WHERE cliente.nKey_Cliente = @nKey_Cliente ");
          oParam.AddParameters("@nKey_Cliente", lngnKeyCliente, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(lngCodHolding)) {
          cSQL.Append(" WHERE cliente.ncodholding = @ncodholding ");
          oParam.AddParameters("@ncodholding", lngCodHolding, TypeSQL.Numeric);
        }

        cSQL.Append(" and vista_saldo_factura.saldo <>  0 and Litigio.bAclarado = 0 ");

        dtData = oConn.Select(cSQL.ToString(), oParam);
        pError = oConn.Error;
        return dtData;
      }
      else
      {
        return null;
      }
    }

    public string getDiasProcesoNormalizacion()
    {
      string sDias = "0";
      oParam = new DBConn.SQLParameters(5);
      DataTable dtData;

      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();
        cSQL.Append(" select (isnull(sum(DATEDIFF(DAY,(Litigio.dFechaIngreso), Normalizacion.dFechaNormalizacion)),0) / count(*)) as dias_normalizacion ");
        cSQL.Append(" from Normalizacion ");
        cSQL.Append(" join cliente on (Cliente.nkey_cliente = Normalizacion.nKey_Cliente) ");
        cSQL.Append(" join Litigio on (Normalizacion.nKey_Litigio = Litigio.nKey_Litigio) ");

        if (!string.IsNullOrEmpty(lngnKeyCliente))
        {
          cSQL.Append(" where Normalizacion.nKey_Cliente = @nKey_Cliente  ");
          oParam.AddParameters("@nKey_Cliente", lngnKeyCliente, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(lngCodHolding))
        {
          cSQL.Append(" where Cliente.ncodholding = @ncodholding ");
          oParam.AddParameters("@ncodholding", lngCodHolding, TypeSQL.Numeric);
        }

        cSQL.Append(" and Litigio.dFechaIngreso is not null ");

        dtData = oConn.Select(cSQL.ToString(), oParam);
        if (dtData != null) {
          if (dtData.Rows.Count > 0) {
            sDias = dtData.Rows[0]["dias_normalizacion"].ToString();
          }
        }
        dtData = null;

        return sDias;
      }
      else
      {
        return sDias;
      }
    }

    public DataTable getAntiguedad()
    {
      oParam = new DBConn.SQLParameters(5);
      string Condicion = " where ";
      StringBuilder cSQL;
      DataTable dtData;

      if (oConn.bIsOpen)
      {
        cSQL = new StringBuilder();
        cSQL.Append("select count(*) as 'cantidad', isnull(sum(vista_saldo_factura.dias_atraso),0) as 'dias_atraso', ");

        if (!string.IsNullOrEmpty(lngnKeyCliente))
          cSQL.Append(" isnull(sum(vista_saldo_factura.saldo),0) as 'saldo' ");

        if (!string.IsNullOrEmpty(lngCodHolding))
          cSQL.Append(" isnull(sum(dbo.aplicaTipoCambio(cliente.signomoneda, vista_saldo_factura.saldo, cliente.monedaholding)),0) as 'saldo' ");

        cSQL.Append(" FROM Litigio ");
        cSQL.Append(" join vista_saldo_factura on (litigio.nKey_Cliente = vista_saldo_factura.nKey_Cliente and litigio.nKey_Deudor = vista_saldo_factura.nKey_Deudor and Litigio.nKey_Factura = vista_saldo_factura.ctakey) ");
        cSQL.Append(" JOIN Cliente ON(Cliente.nKey_Cliente = Litigio.nKey_Cliente) ");
        cSQL.Append(" JOIN CodigoDeudor ON(CodigoDeudor.nKey_Cliente = Litigio.nKey_Cliente AND CodigoDeudor.nKey_Deudor = Litigio.nKey_Deudor) ");
        cSQL.Append(" JOIN SERVICIO ON (SERVICIO.nkey_cliente = Litigio.nkey_cliente and SERVICIO.nkey_deudor = Litigio.nkey_deudor and SERVICIO.nkey_analista != 4 ) ");

        if (!string.IsNullOrEmpty(lngnKeyCliente))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" Cliente.nKey_Cliente = @nKey_Cliente  ");
          oParam.AddParameters("@nKey_Cliente", lngnKeyCliente, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(lngCodHolding))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" Cliente.ncodholding = @ncodholding  ");
          oParam.AddParameters("@ncodholding", lngCodHolding, TypeSQL.Numeric);
        }

        cSQL.Append(" and vista_saldo_factura.saldo <>  0 ");
        cSQL.Append(" and Litigio.bAclarado = 0  ");

        if (!string.IsNullOrEmpty(sPeriodo))
        {
          switch (sPeriodo)
          {
            case "30":
              cSQL.Append(" and vista_saldo_factura.dias_atraso <= 30 ");
              break;
            case "60":
              cSQL.Append(" and (vista_saldo_factura.dias_atraso > 30 and vista_saldo_factura.dias_atraso <= 60) ");
              break;
            case "90":
              cSQL.Append(" and (vista_saldo_factura.dias_atraso >= 61 and vista_saldo_factura.dias_atraso <= 90) ");
              break;
            case "mayor":
              cSQL.Append(" and vista_saldo_factura.dias_atraso > 90 ");
              break;
          }
        }

        dtData = oConn.Select(cSQL.ToString(), oParam);
        pError = oConn.Error;
        return dtData;
      }
      else
      {
        return null;
      }
    }

    public DataTable getDetalleCanal()
    {
      oParam = new DBConn.SQLParameters(5);
      string Condicion = " where ";
      DataTable dtData;

      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();
        cSQL.Append(" SELECT top 4 Rubros.sRubro as 'canal', count(*) as cantidad_facturas, isnull(sum(vista_saldo_factura.dias_atraso),0) AS dias_atraso, ");

        if (!string.IsNullOrEmpty(lngnKeyCliente))
          cSQL.Append(" isnull(sum(vista_saldo_factura.saldo),0) AS saldo_litigio ");

        if (!string.IsNullOrEmpty(lngCodHolding))
          cSQL.Append(" isnull(sum(dbo.aplicaTipoCambio(cliente.signomoneda, vista_saldo_factura.saldo, cliente.monedaholding)),0) AS saldo_litigio ");

        cSQL.Append(" from Litigio ");
        cSQL.Append(" join vista_saldo_factura on (litigio.nKey_Cliente = vista_saldo_factura.nKey_Cliente and litigio.nKey_Deudor = vista_saldo_factura.nKey_Deudor and Litigio.nKey_Factura = vista_saldo_factura.ctakey) ");
        cSQL.Append(" JOIN Cliente ON(Cliente.nKey_Cliente = Litigio.nKey_Cliente) ");
        cSQL.Append(" JOIN CodigoDeudor ON(CodigoDeudor.nKey_Cliente = Litigio.nKey_Cliente AND CodigoDeudor.nKey_Deudor = Litigio.nKey_Deudor) ");
        cSQL.Append(" join Rubros on (CodigoDeudor.nKey_Rubros = Rubros.nKey_Rubros) ");
        cSQL.Append(" JOIN SERVICIO ON (SERVICIO.nkey_cliente = Litigio.nkey_cliente and SERVICIO.nkey_deudor = Litigio.nkey_deudor and SERVICIO.nkey_analista != 4 ) ");

        if (!string.IsNullOrEmpty(lngnKeyCliente))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" Cliente.nKey_Cliente = @nKey_Cliente  ");
          oParam.AddParameters("@nKey_Cliente", lngnKeyCliente, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(lngCodHolding))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" Cliente.ncodholding = @ncodholding  ");
          oParam.AddParameters("@ncodholding", lngCodHolding, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(lngnKeyDeudor))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" vista_saldo_factura.nKey_Deudor = @nKey_Deudor   ");
          oParam.AddParameters("@nKey_Deudor", lngnKeyDeudor, TypeSQL.Numeric);
        }

        cSQL.Append(" and vista_saldo_factura.saldo <>  0 and Litigio.bAclarado = 0  ");
        cSQL.Append(" group by Rubros.sRubro ");

        if (!string.IsNullOrEmpty(lngnKeyCliente))
          cSQL.Append(" order by isnull(sum(vista_saldo_factura.saldo),0) desc ");

        if (!string.IsNullOrEmpty(lngCodHolding))
          cSQL.Append(" order by isnull(sum(dbo.aplicaTipoCambio(cliente.signomoneda, vista_saldo_factura.saldo, cliente.monedaholding)),0) desc ");

        dtData = oConn.Select(cSQL.ToString(), oParam);
        pError = oConn.Error;
        return dtData;
      }
      else
      {
        return null;
      }
    }

    public DataTable getSubmotivo()
    {
      oParam = new DBConn.SQLParameters(5);
      DataTable dtData;
      StringBuilder cSQL;

      if (oConn.bIsOpen)
      {
        cSQL = new StringBuilder();

        cSQL.Append(" SELECT distinct top 6 CASE rtrim(ltrim(Litigio.submotivo)) WHEN '0' THEN 'Factura Comercial' else rtrim(ltrim(Litigio.submotivo)) END as 'submotivo', ");
        cSQL.Append(" count(*) as cantidad, ");
        cSQL.Append(" isnull(sum(vista_saldo_factura.dias_atraso),0) AS dias_atraso, ");

        if (!string.IsNullOrEmpty(lngnKeyCliente))
          cSQL.Append(" isnull(sum(vista_saldo_factura.saldo),0) AS saldo ");

        if (!string.IsNullOrEmpty(lngCodHolding))
          cSQL.Append(" isnull(sum(dbo.aplicaTipoCambio(cliente.signomoneda, vista_saldo_factura.saldo, cliente.monedaholding)),0) AS saldo ");

        cSQL.Append(" FROM Litigio ");
        cSQL.Append(" join vista_saldo_factura on (litigio.nKey_Cliente = vista_saldo_factura.nKey_Cliente and litigio.nKey_Deudor = vista_saldo_factura.nKey_Deudor and Litigio.nKey_Factura = vista_saldo_factura.ctakey) ");
        cSQL.Append(" JOIN Cliente ON(Cliente.nKey_Cliente = Litigio.nKey_Cliente) ");
        cSQL.Append(" JOIN CodigoDeudor ON(CodigoDeudor.nKey_Cliente = Litigio.nKey_Cliente AND CodigoDeudor.nKey_Deudor = Litigio.nKey_Deudor) ");
        cSQL.Append(" JOIN SERVICIO ON (SERVICIO.nkey_cliente = Litigio.nkey_cliente and SERVICIO.nkey_deudor = Litigio.nkey_deudor and SERVICIO.nkey_analista != 4 ) ");

        if (!string.IsNullOrEmpty(lngnKeyCliente))
        {
          cSQL.Append(" where Cliente.nKey_Cliente = @nKey_Cliente ");
          oParam.AddParameters("@nKey_Cliente", lngnKeyCliente, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(lngCodHolding))
        {
          cSQL.Append(" where Cliente.ncodholding = @ncodholding ");
          oParam.AddParameters("@ncodholding", lngCodHolding, TypeSQL.Numeric);
        }

        cSQL.Append(" and vista_saldo_factura.saldo <>  0 and Litigio.bAclarado = 0  ");
        cSQL.Append(" group by  Litigio.submotivo ");


        if (!string.IsNullOrEmpty(lngnKeyCliente))
          cSQL.Append(" order by isnull(sum(vista_saldo_factura.saldo),0) desc ");

        if (!string.IsNullOrEmpty(lngCodHolding))
          cSQL.Append(" order by isnull(sum(dbo.aplicaTipoCambio(cliente.signomoneda, vista_saldo_factura.saldo, cliente.monedaholding)),0) desc ");

        dtData = oConn.Select(cSQL.ToString(), oParam);
        pError = oConn.Error;
        return dtData;
      }
      else
      {
        pError = "Conexion Cerrada";
        return null;
      }
    }

    public DataTable getDetalle()
    {
      oParam = new DBConn.SQLParameters(5);
      StringBuilder cSQL;
      string Condicion = " where ";
      DataTable dtData;

      if (oConn.bIsOpen)
      {
        cSQL = new StringBuilder();
        cSQL.Append("SELECT  CodigoDeudor.nCodigoDeudor AS 'codigo', Deudor.sNombre AS 'cliente', vista_saldo_factura.nNumeroFactura AS 'factura', vista_saldo_factura.dFechaVencimiento AS 'vencimiento', vista_saldo_factura.dias_atraso AS 'dias_atraso', ");
        cSQL.Append(" SolicitudNotaCredito.nNumeroSolicitud AS 'numero_solicitud', ");

        if (!string.IsNullOrEmpty(lngnKeyCliente))
        {
          cSQL.Append(" (vista_saldo_factura.saldo) as 'saldo_factura',  ");
          cSQL.Append(" isnull(Litigio.nMontoLitigio,0) AS 'monto_litigio',  ");
        }

        if (!string.IsNullOrEmpty(lngCodHolding))
        {
          cSQL.Append(" isnull(dbo.aplicaTipoCambio(cliente.signomoneda, isnull(vista_saldo_factura.saldo,0), cliente.monedaholding),0) as 'saldo_factura', ");
          cSQL.Append(" isnull(dbo.aplicaTipoCambio(cliente.signomoneda, isnull(Litigio.nMontoLitigio,0), cliente.monedaholding),0)   AS 'monto_litigio', ");
        }

        cSQL.Append(" isnull(SolicitudNotaCredito.dFecha,litigio.dfechaingreso) AS 'fecha_solicitud', ");
        cSQL.Append(" isnull(litigio.sobservacion,' ')+' '+case ISNULL( Litigio.NumFactuemi ,0) when 0 then ' ' else str(Litigio.NumFactuemi)  end +' '+convert(varchar,ISNULL(convert(varchar,litigio.fecemiemi,101),' '))+' '+case isnull(litigio.montoemi ,0) when 0 then  ' ' else  str(litigio.montoemi ) end as 'motivo', ");
        cSQL.Append(" litigio.submotivo as 'submotivo', ");
        cSQL.Append(" Litigio.sDescripcion as 'comentario', ");
        cSQL.Append(" isnull(litigio.autorizoemi, ' ')  as 'contacto', ISNULL(vendedor.sNombre, ' ' ) as 'vendedor', Rubros.sRubro as 'canal' ");
        cSQL.Append(" FROM Litigio ");

        cSQL.Append(" join vista_saldo_factura on (litigio.nKey_Cliente = vista_saldo_factura.nKey_Cliente and litigio.nKey_Deudor = vista_saldo_factura.nKey_Deudor and Litigio.nKey_Factura = vista_saldo_factura.ctakey) ");
        cSQL.Append(" join Deudor on (deudor.nKey_Deudor = litigio.nKey_Deudor)  ");
        cSQL.Append(" JOIN SERVICIO ON (SERVICIO.nkey_cliente = Litigio.nkey_cliente and SERVICIO.nkey_deudor = Litigio.nkey_deudor and SERVICIO.nkey_analista != 4 ) ");
        cSQL.Append(" LEFT JOIN SolicitudNotaCredito ON ( (SolicitudNotaCredito.nKey_cliente = Litigio.nKey_cliente and SolicitudNotaCredito.nKey_deudor = Litigio.nKey_deudor and SolicitudNotaCredito.nKey_factura = Litigio.nKey_factura and SolicitudNotaCredito.nmontolitigio = Litigio.nmontolitigio and Litigio.nKey_Solicitud is null) or (SolicitudNotaCredito.nKey_cliente = Litigio.nKey_cliente and SolicitudNotaCredito.nKey_deudor = Litigio.nKey_deudor  and Litigio.nKey_Solicitud is not null and SolicitudNotaCredito.nKey_SolicitudNotaCredito  = Litigio.nKey_Solicitud )) ");
        cSQL.Append(" JOIN Cliente ON(Cliente.nKey_Cliente = Litigio.nKey_Cliente) ");
        cSQL.Append(" JOIN CodigoDeudor ON(CodigoDeudor.nKey_Cliente = Litigio.nKey_Cliente AND CodigoDeudor.nKey_Deudor = Litigio.nKey_Deudor)  ");
        cSQL.Append(" join analista ON (analista.nkey_analista = servicio.nkey_analista) ");
        cSQL.Append(" left join Vendedor on (vendedor.nKey_Vendedor = CodigoDeudor.nkey_vendedor) ");
        cSQL.Append(" join Rubros on (CodigoDeudor.nKey_Rubros = Rubros.nKey_Rubros) ");

        if (!string.IsNullOrEmpty(lngnKeyCliente))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" Cliente.nKey_Cliente = @nKey_Cliente  ");
          oParam.AddParameters("@nKey_Cliente", lngnKeyCliente, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(lngCodHolding))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" Cliente.ncodholding = @ncodholding  ");
          oParam.AddParameters("@ncodholding", lngCodHolding, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(lngnKeyDeudor))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" Deudor.nKey_Deudor = @nKey_Deudor   ");
          oParam.AddParameters("@nKey_Deudor", lngnKeyDeudor, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(lNumFactura))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" vista_saldo_factura.nNumeroFactura = @nNumeroFactura   ");
          oParam.AddParameters("@nNumeroFactura", lNumFactura, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(sNomCliente))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" Deudor.sNombre like '%" + sNomCliente + "%'   ");
          oParam.AddParameters("@sNomCliente", sNomCliente, TypeSQL.Varchar);
        }

        cSQL.Append(" and vista_saldo_factura.saldo <>  0 and Litigio.bAclarado = 0 ");
        

        if (!string.IsNullOrEmpty(sPeriodo))
        {
          switch (sPeriodo)
          {
            case "30":
              cSQL.Append(" and (vista_saldo_factura.dias_atraso >= 0 and vista_saldo_factura.dias_atraso <= 30) ");
              break;
            case "60":
              cSQL.Append(" and (vista_saldo_factura.dias_atraso > 30 and vista_saldo_factura.dias_atraso <= 60) ");
              break;
            case "90":
              cSQL.Append(" and (vista_saldo_factura.dias_atraso >= 61 and vista_saldo_factura.dias_atraso <= 90) ");
              break;
            case "mayor":
              cSQL.Append(" and vista_saldo_factura.dias_atraso > 90 ");
              break;
          }
        }

        //if (!string.IsNullOrEmpty(lngnKeyCliente))
        //{
        //  cSQL.Append(" GROUP BY cliente.nkey_cliente, deudor.nkey_deudor, Cliente.sNombre , CodigoDeudor.nCodigoDeudor, Deudor.sNombre, vista_saldo_factura.nNumeroFactura, vista_saldo_factura.nMontoFactura, vista_saldo_factura.dFechaVencimiento, vista_saldo_factura.dias_atraso, Litigio.nMontoLitigio, Litigio.dFechaIngreso, SolicitudNotaCredito.nNumeroSolicitud, SolicitudNotaCredito.dFecha,   isnull(litigio.sobservacion,' ')+' '+case ISNULL( Litigio.NumFactuemi ,0) when 0 then ' ' else str(Litigio.NumFactuemi)  end +' '+convert(varchar,ISNULL(convert(varchar,litigio.fecemiemi,101),' '))+' '+case isnull(litigio.montoemi ,0) when 0 then  ' ' else  str(litigio.montoemi ) end , Litigio.sDescripcion , analista.snombre, analista.ncod, litigio.submotivo ,  isnull(litigio.autorizoemi, ' '), factura.nkey_factura, ISNULL(vendedor.nCod,' ') , ISNULL(vendedor.sNombre, ' ' ), factura.tipo_factura, cliente.decimales, Rubros.sRubro  ");
        //}

        //if (!string.IsNullOrEmpty(lngCodHolding))
        //{
        //  cSQL.Append(" GROUP BY cliente.nkey_cliente, deudor.nkey_deudor, Cliente.sNombre, CodigoDeudor.nCodigoDeudor, Deudor.sNombre, vista_saldo_factura.nNumeroFactura, vista_saldo_factura.nMontoFactura, vista_saldo_factura.dFechaVencimiento, vista_saldo_factura.dias_atraso, vista_saldo_factura.saldo, Litigio.nMontoLitigio, Litigio.dFechaIngreso, Cliente.monedaholding, Cliente.signomoneda, SolicitudNotaCredito.nNumeroSolicitud, SolicitudNotaCredito.dFecha,  isnull(litigio.sobservacion,' ')+' '+case ISNULL( Litigio.NumFactuemi ,0) when 0 then ' ' else str(Litigio.NumFactuemi)  end +' '+convert(varchar,ISNULL(convert(varchar,litigio.fecemiemi,101),' '))+' '+case isnull(litigio.montoemi ,0) when 0 then  ' ' else  str(litigio.montoemi ) end , Litigio.sDescripcion , analista.snombre, analista.ncod, litigio.submotivo ,  isnull(litigio.autorizoemi, ' '), factura.nkey_factura, ISNULL(vendedor.nCod,' ') , ISNULL(vendedor.sNombre, ' ' ), factura.tipo_factura, cliente.decimales, Rubros.sRubro  ");
        //}

        if (bOrderBy)
        {
          cSQL.Append(" Order by Rubros.sRubro, Litigio.nMontoLitigio ");
        }
        else
        {
          cSQL.Append(" Order by cliente.snombre, CodigoDeudor.nCodigoDeudor, Litigio.dFechaIngreso, Deudor.sNombre, vista_saldo_factura.nNumeroFactura ");
        }

        dtData = oConn.Select(cSQL.ToString(), oParam);
        pError = oConn.Error;
        return dtData;
      }
      else
      {
        return null;
      }
    }
  }
}
