using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using OnlineServices.Conn;

namespace OnlineServices.Dashboard
{
  public class cDashboard
  {
    DBConn.SQLParameters oParam;
    DBConn.DataTypeSQL TypeSQL = new DBConn.DataTypeSQL();

    private string lngCodUsuario;
    public string CodUsuario { get { return lngCodUsuario; } set { lngCodUsuario = value; } }

    private string lngCodHolding;
    public string CodHolding { get { return lngCodHolding; } set { lngCodHolding = value; } }

    private string lngnKeyCliente;
    public string nKeyCliente { get { return lngnKeyCliente; } set { lngnKeyCliente = value; } }

    private string lngnKeyDeudor;
    public string nKeyDeudor { get { return lngnKeyDeudor; } set { lngnKeyDeudor = value; } }

    private string lngCodigoDeudor;
    public string CodigoDeudor { get { return lngCodigoDeudor; } set { lngCodigoDeudor = value; } }

    private string lnKeyCodigoDeudor;
    public string nKeyCodigoDeudor { get { return lnKeyCodigoDeudor; } set { lnKeyCodigoDeudor = value; } }

    private string sNomDeudor;
    public string NomDeudor { get { return sNomDeudor; } set { sNomDeudor = value; } }

    private string iAno;
    public string Ano { get { return iAno; } set { iAno = value; } }

    private string sPeriodo;
    public string Periodo { get { return sPeriodo; } set { sPeriodo = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private DBConn oConn;

    public cDashboard()
    {

    }

    public cDashboard(ref DBConn oConn)
    {
      this.oConn = oConn;
    }

    public DataTable GetClienteHolding()
    {
      oParam = new DBConn.SQLParameters(2);
      DataTable dtData;

      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();
        cSQL.Append("select sNombre, holding from Cliente ");

        if (!string.IsNullOrEmpty(lngnKeyCliente))
        {
          cSQL.Append(" where nkey_cliente = @nkey_cliente ");
          oParam.AddParameters("@nkey_cliente", lngnKeyCliente, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(lngCodHolding))
        {
          cSQL.Append(" where ncodholding = @ncodholding ");
          oParam.AddParameters("@ncodholding", lngCodHolding, TypeSQL.Numeric);
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

    public DataTable GetHolding()
    {
      oParam = new DBConn.SQLParameters(1);
      DataTable dtData;

      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();
        cSQL.Append("select ncodholding, holding from Cliente where ncodholding in(select ncod_holding from sys_holding_cliente where cod_user = @codusuario) and bCuentaCorriente <> 0 group by ncodholding,holding order by holding ");
        oParam.AddParameters("@codusuario", lngCodUsuario, TypeSQL.Numeric);

        dtData = oConn.Select(cSQL.ToString(), oParam);
        pError = oConn.Error;
        return dtData;
      }
      else
      {
        return null;
      }
    }

    public DataTable GetClientes()
    {
      oParam = new DBConn.SQLParameters(2);
      DataTable dtData;
      string Condicion = " where ";

      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();
        cSQL.Append("select nkey_cliente, nRut, sDigitoVerificador, sNombre, sDireccion, sComuna, signomoneda, decimales from Cliente ");

        if (!string.IsNullOrEmpty(lngCodHolding))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" nKey_Cliente in(select a.nKey_Cliente from cliente a where ncodholding = @ncodholding) ");
          oParam.AddParameters("@ncodholding", lngCodHolding, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(lngCodUsuario))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" nKey_Cliente in(select nkey_user from sys_cliente_usuario where cod_user = @cod_user) ");
          oParam.AddParameters("@cod_user", lngCodUsuario, TypeSQL.Numeric);
        }

        cSQL.Append(" and bCuentaCorriente <> 0 and sAnalista <> '' order by sNombre ");

        dtData = oConn.Select(cSQL.ToString(), oParam);
        pError = oConn.Error;
        return dtData;
      }
      else
      {
        return null;
      }
    }

    public DataTable GetDeudores()
    {
      oParam = new DBConn.SQLParameters(5);
      DataTable dtData;

      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();
        cSQL.Append("select distinct(nKey_Deudor), ncodigodeudor, ");
        cSQL.Append(" (select sNombre from Deudor where nKey_Deudor = CodigoDeudor.nKey_Deudor) deudor ");
        cSQL.Append(" from CodigoDeudor ");
        cSQL.Append(" where (select bCuentaCorriente from Cliente where nKey_Cliente = CodigoDeudor.nKey_Cliente) <> 0  ");
        cSQL.Append(" and (select nKey_Analista from Servicio where nKey_Deudor = CodigoDeudor.nKey_Deudor and nKey_Cliente = CodigoDeudor.nKey_Cliente)<> 4  ");

        if (!string.IsNullOrEmpty(nKeyDeudor)) {
          cSQL.Append(" and nkey_deudor = @nkey_deudor ");
          oParam.AddParameters("@nkey_deudor", nKeyDeudor, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(sNomDeudor)) {
          cSQL.Append(" and (ncodigodeudor + (select sNombre from Deudor where nKey_Deudor = CodigoDeudor.nKey_Deudor)) like  '%' + @sNomDeudor + '%'  ");
          oParam.AddParameters("@sNomDeudor", sNomDeudor, TypeSQL.Varchar);
        }        

        if (!string.IsNullOrEmpty(lngCodHolding))
        {
          cSQL.Append(" and (select ncodholding from Cliente where nKey_Cliente = CodigoDeudor.nKey_Cliente )= @ncodholding ");
          oParam.AddParameters("@ncodholding", lngCodHolding, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(lngnKeyCliente))
        {
          cSQL.Append(" and nKey_Cliente = @nkey_cliente  ");
          oParam.AddParameters("@nkey_cliente", lngnKeyCliente, TypeSQL.Numeric);
        }

        cSQL.Append(" Order by deudor ");

        dtData = oConn.Select(cSQL.ToString(), oParam);
        pError = oConn.Error;
        return dtData;
      }
      else
      {
        return null;
      }
    }

    public DataTable GetDeudorByCodigo()
    {
      oParam = new DBConn.SQLParameters(4);
      DataTable dtData;

      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();
        cSQL.Append("select nKey_Cliente, nKey_Deudor, ncodigodeudor, ");
        cSQL.Append(" (select sNombre from Cliente where nKey_Cliente = CodigoDeudor.nKey_Cliente) cliente, ");
        cSQL.Append(" (select nCod from Cliente where nKey_Cliente = CodigoDeudor.nKey_Cliente) codigo_cliente, ");
        cSQL.Append(" (select Holding from Cliente where nKey_Cliente = CodigoDeudor.nKey_Cliente) holding, ");
        cSQL.Append(" (select ncodholding from Cliente where nKey_Cliente = CodigoDeudor.nKey_Cliente) codigo_holding,");
        cSQL.Append(" (select sNombre from Deudor where nKey_Deudor = CodigoDeudor.nKey_Deudor) deudor ");
        cSQL.Append(" from CodigoDeudor ");
        cSQL.Append(" where (select bCuentaCorriente from Cliente where nKey_Cliente = CodigoDeudor.nKey_Cliente ) <> 0  ");
        cSQL.Append(" and (ncodigodeudor + ' ' + (select sNombre from Deudor where nKey_Deudor = CodigoDeudor.nKey_Deudor)) = @sNomDeudor ");
        oParam.AddParameters("@sNomDeudor", sNomDeudor, TypeSQL.Varchar);

        if (!string.IsNullOrEmpty(lngCodHolding))
        {
          cSQL.Append(" and (select ncodholding from Cliente where nKey_Cliente = CodigoDeudor.nKey_Cliente )= @ncodholding ");
          oParam.AddParameters("@ncodholding", lngCodHolding, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(lngnKeyCliente))
        {
          cSQL.Append(" and nKey_Cliente = @nkey_cliente  ");
          oParam.AddParameters("@nkey_cliente", lngnKeyCliente, TypeSQL.Numeric);
        }

        cSQL.Append(" Order by deudor ");

        dtData = oConn.Select(cSQL.ToString(), oParam);
        pError = oConn.Error;
        return dtData;
      }
      else
      {
        return null;
      }
    }

    public DataTable GetDeudorByCodigo_old()
    {
      oParam = new DBConn.SQLParameters(1);
      DataTable dtData;

      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();
        cSQL.Append("select * from deudor where nRUT = @nRUT ");
        oParam.AddParameters("@nRUT", lngCodigoDeudor, TypeSQL.Numeric);

        dtData = oConn.Select(cSQL.ToString(), oParam);
        pError = oConn.Error;
        return dtData;
      }
      else
      {
        return null;
      }
    }

    public string getSimboloMoneda()
    {
      oParam = new DBConn.SQLParameters(2);
      DataTable dtData;
      string sMoneda = string.Empty;

      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();

        if (!string.IsNullOrEmpty(lngnKeyCliente))
        {
          cSQL.Append(" select signomoneda as 'moneda' from cliente where nKey_Cliente = @nKey_Cliente and bCuentaCorriente <> 0 and sAnalista <> '' ");
          oParam.AddParameters("@nKey_Cliente", lngnKeyCliente, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(lngCodHolding))
        {
          cSQL.Append(" select distinct(ncodholding), monedaholding as 'moneda' from cliente where ncodholding = @ncodholding and bCuentaCorriente <> 0 and sAnalista <> '' ");
          oParam.AddParameters("@ncodholding", lngCodHolding, TypeSQL.Numeric);
        }

        dtData = oConn.Select(cSQL.ToString(), oParam);
        if (dtData != null)
        {
          if (dtData.Rows.Count > 0)
          {
            sMoneda = dtData.Rows[0]["moneda"].ToString();
          }
        }
        return sMoneda;
      }
      else
      {
        return sMoneda;
      }
    }

    #region Fichas

    #region GESTIONES
    public DataTable GetGestiones()
    {
      oParam = new DBConn.SQLParameters(2);
      DataTable dtData;

      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();
        cSQL.Append("select  distinct top 5 CONVERT(DATETIME,CAST(dFechaGestion AS VARCHAR(12)),107) as fecha,  ");
        cSQL.Append("case when sTipoGestion = 'Personal' then 'Ingreso de pago' else sTipoGestion end as 'sTipoGestion', left(sObservacion,40) as 'sObservacion' ");
        cSQL.Append("from GestionDeudor ");
        cSQL.Append(" where nKey_Deudor = @nKey_Deudor ");
        oParam.AddParameters("@nKey_Deudor", lngnKeyDeudor, TypeSQL.Numeric);

        if (!string.IsNullOrEmpty(lngnKeyCliente))
        {
          cSQL.Append(" and nKey_Cliente = @nKey_Cliente ");
          oParam.AddParameters("@nKey_Cliente", lngnKeyCliente, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(lngCodHolding))
        {
          cSQL.Append(" and (select ncodholding from cliente where nKey_Cliente = GestionDeudor.nKey_Cliente) = @ncodholding ");
          oParam.AddParameters("@ncodholding", lngCodHolding, TypeSQL.Numeric);
        }

        cSQL.Append(" and sTipoGestion not like '%Depósito%' ");
        //cSQL.Append(" and (select mailenvioctacte from CodigoDeudor where nKey_Deudor= GestionDeudor.nKey_Deudor and nKey_Cliente = GestionDeudor.nKey_Cliente) <> '' ");
        cSQL.Append(" order by fecha desc ");

        dtData = oConn.Select(cSQL.ToString(), oParam);
        pError = oConn.Error;
        return dtData;
      }
      else
      {
        return null;
      }
    }
    #endregion

    #region Overview
    public DataTable GetLCAConsumido()
    {
      oParam = new DBConn.SQLParameters(2);
      DataTable dtData;
      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();

        if (!string.IsNullOrEmpty(lngnKeyCliente))
          cSQL.Append(" select isnull(sum(isnull(saldo,0)),0) as 'consumido'  ");

        if (!string.IsNullOrEmpty(lngCodHolding))
          cSQL.Append(" select isnull(sum(dbo.aplicaTipoCambio(cliente.signomoneda, isnull(saldo,0), cliente.monedaholding)),0) as 'consumido' ");

        cSQL.Append(" from vista_antiguedad_rango join cliente on (Cliente.nKey_Cliente = vista_antiguedad_rango.nKey_Cliente) ");
        cSQL.Append(" where vista_antiguedad_rango.nKey_Deudor = @nKey_Deudor ");
        oParam.AddParameters("@nKey_Deudor", lngnKeyDeudor, TypeSQL.Numeric);

        if (!string.IsNullOrEmpty(lngnKeyCliente))
        {
          cSQL.Append(" and cliente.nKey_Cliente = @nKey_Cliente ");
          oParam.AddParameters("@nKey_Cliente", lngnKeyCliente, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(lngCodHolding))
        {
          cSQL.Append(" and cliente.ncodholding = @ncodholding ");
          oParam.AddParameters("@ncodholding", lngCodHolding, TypeSQL.Numeric);
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

    public DataTable GetLCADisponible()
    {
      oParam = new DBConn.SQLParameters(2);
      DataTable dtData;
      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();

        if (!string.IsNullOrEmpty(lngnKeyCliente))
          cSQL.Append(" select isnull(sum(isnull(nLineaCreditoActual,0)),0) as disponible ");

        if (!string.IsNullOrEmpty(lngCodHolding))
          cSQL.Append(" select isnull(sum(dbo.aplicaTipoCambio(cliente.signomoneda, isnull(nLineaCreditoActual,0), cliente.monedaholding)),0) as disponible ");

        cSQL.Append(" from LineaCredito join Cliente on (Cliente.nKey_Cliente = LineaCredito.nKey_Cliente) ");
        cSQL.Append(" where LineaCredito.nKey_Deudor = @nkey_deudor ");
        oParam.AddParameters("@nkey_deudor", lngnKeyDeudor, TypeSQL.Numeric);

        if (!string.IsNullOrEmpty(lngnKeyCliente))
        {
          cSQL.Append(" and Cliente.nKey_Cliente = @nKey_Cliente ");
          oParam.AddParameters("@nKey_Cliente", lngnKeyCliente, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(lngCodHolding))
        {
          cSQL.Append(" and Cliente.ncodholding = @ncodholding ");
          oParam.AddParameters("@ncodholding", lngCodHolding, TypeSQL.Numeric);
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

    public DataTable GetEstimadoCaja()
    {
      oParam = new DBConn.SQLParameters(3);
      DataTable dtData;
      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();
        if (!string.IsNullOrEmpty(lngnKeyCliente))
        {
          cSQL.Append(" select isnull(sum(metasanalistadeudor.real1 + metasanalistadeudor.real2 + metasanalistadeudor.real3 + metasanalistadeudor.real4 + metasanalistadeudor.real5),0) as real, ");
          cSQL.Append(" isnull(sum(metasanalistadeudor.estimado1 + metasanalistadeudor.estimado2 + metasanalistadeudor.estimado3 + metasanalistadeudor.estimado4 + metasanalistadeudor.estimado5),0) as estimado ");
        }

        if (!string.IsNullOrEmpty(lngCodHolding))
        {
          cSQL.Append(" select isnull(sum(dbo.aplicaTipoCambio(cliente.signomoneda, metasanalistadeudor.real1 + metasanalistadeudor.real2 + metasanalistadeudor.real3 + metasanalistadeudor.real4 + metasanalistadeudor.real5, cliente.monedaholding)),0) as real,  ");
          cSQL.Append(" isnull(sum(dbo.aplicaTipoCambio(cliente.signomoneda, metasanalistadeudor.estimado1 + metasanalistadeudor.estimado2 + metasanalistadeudor.estimado3 + metasanalistadeudor.estimado4 + metasanalistadeudor.estimado5,cliente.monedaholding)),0) as estimado ");
        }

        cSQL.Append(" from metasanalistadeudor join cliente on (Cliente.nkey_cliente = metasanalistadeudor.nKey_Cliente) ");
        cSQL.Append(" where metasanalistadeudor.nkey_deudor = @nkey_deudor ");
        oParam.AddParameters("@nkey_deudor", lngnKeyDeudor, TypeSQL.Numeric);

        if (!string.IsNullOrEmpty(lngnKeyCliente))
        {
          cSQL.Append(" and metasanalistadeudor.nkey_cliente in(select distinct ncodholding from cliente where nkey_cliente = @nkey_cliente and bCuentaCorriente <> 0 and sAnalista <> '') ");
          oParam.AddParameters("@nkey_cliente", lngnKeyCliente, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(lngCodHolding))
        {
          cSQL.Append(" and metasanalistadeudor.nkey_cliente = @ncodholding ");
          oParam.AddParameters("@ncodholding", lngCodHolding, TypeSQL.Numeric);
        }

        cSQL.Append(" and periodo = @periodo ");
        oParam.AddParameters("@periodo", sPeriodo, TypeSQL.Varchar);

        dtData = oConn.Select(cSQL.ToString(), oParam);
        pError = oConn.Error;
        return dtData;

      }
      else
      {
        return null;
      }
    }

    public DataTable GetMonto_TipoPago()
    {
      oParam = new DBConn.SQLParameters(3);
      DataTable dtData;

      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();

        if (!string.IsNullOrEmpty(lngnKeyCliente))
          cSQL.Append(" select top 1 isnull(SUM(isnull(Pago.nMontoPago,0)),0) as monto_ultimo_abono, Pago.sTipoPago ");

        if (!string.IsNullOrEmpty(lngCodHolding))
          cSQL.Append(" select top 1 isnull(SUM(dbo.aplicaTipoCambio(cliente.signomoneda, isnull(Pago.nMontoPago,0), cliente.monedaholding)),0) as monto_ultimo_abono, Pago.sTipoPago ");

        cSQL.Append(" from Pago join cliente on (Cliente.nKey_Cliente = Pago.nKey_Cliente)  ");
        cSQL.Append(" where sTipoPago <> 'Ajuste' ");
        cSQL.Append(" and Pago.nKey_Deudor = @nkey_deudor ");
        oParam.AddParameters("@nkey_deudor", lngnKeyDeudor, TypeSQL.Numeric);

        if (!string.IsNullOrEmpty(lngnKeyCliente))
        {
          cSQL.Append(" and Cliente.nKey_Cliente = @nKey_Cliente ");
          oParam.AddParameters("@nKey_Cliente", lngnKeyCliente, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(lngCodHolding))
        {
          cSQL.Append(" and Cliente.ncodholding = @ncodholding ");
          oParam.AddParameters("@ncodholding", lngCodHolding, TypeSQL.Numeric);
        }

        cSQL.Append(" GROUP BY Pago.dFechaIngreso, Pago.nNumeroDeposito, Pago.sTipoPago order by Pago.dFechaIngreso desc ");

        dtData = oConn.Select(cSQL.ToString(), oParam);
        pError = oConn.Error;
        return dtData;

      }
      else
      {
        return null;
      }
    }

    public DataTable GetUltimoDocPago()
    {
      oParam = new DBConn.SQLParameters(2);
      DataTable dtData;
      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();
        cSQL.Append("select distinct top 1 dFechaIngreso, nKey_Cliente, nnumeroPago, dfechaVencimiento, dFechaAplicado, dFechaDeposito, ");
        cSQL.Append("(select codviapago from CodigoDeudor where nKey_Deudor = Pago.nKey_Deudor and nKey_Cliente = Pago.nKey_Cliente) as viapago, ");
        cSQL.Append("(select sEstatusCredito from LineaCredito where nkey_Cliente = Pago.nKey_Cliente and nKey_Deudor = Pago.nKey_Deudor) as estatus,");
        cSQL.Append("(select descripcion from tabla_datos where codigo='VIAPAGO' and valor= (select codviapago from CodigoDeudor where nKey_Deudor = Pago.nKey_Deudor and nKey_Cliente = Pago.nKey_Cliente)) as descripcion_via_pago, ");
        cSQL.Append("(select sRubro from Rubros where tipo_canal= 'C' and nkey_Rubros = (select nKey_Rubros from CodigoDeudor where nKey_Cliente = Pago.nKey_Cliente and nKey_Deudor = Pago.nKey_Deudor)) as canal, ");
        cSQL.Append("(select seguro from CodigoDeudor where nKey_Deudor = Pago.nKey_Deudor and nKey_Cliente = Pago.nKey_Cliente) as seguro ");
        cSQL.Append(" from Pago where nkey_deudor = @nkey_deudor ");
        oParam.AddParameters("@nkey_deudor", lngnKeyDeudor, TypeSQL.Numeric);

        if (!string.IsNullOrEmpty(lngnKeyCliente))
        {
          cSQL.Append(" and nKey_Cliente = @nKey_Cliente ");
          oParam.AddParameters("@nKey_Cliente", lngnKeyCliente, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(lngCodHolding))
        {
          cSQL.Append(" and (select ncodholding from cliente where nKey_Cliente = Pago.nKey_Cliente) = @ncodholding ");
          oParam.AddParameters("@ncodholding", lngCodHolding, TypeSQL.Numeric);
        }

        cSQL.Append(" order by  dFechaIngreso desc ");
        dtData = oConn.Select(cSQL.ToString(), oParam);
        pError = oConn.Error;
        return dtData;
      }
      else
      {
        return null;
      }
    }
    #endregion

    #region DSO
    public DataTable GetDso()
    {
      oParam = new DBConn.SQLParameters(3);
      DataTable dtData;

      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();
        cSQL.Append("select periodo, dso from ");

        if (!string.IsNullOrEmpty(lngnKeyCliente))
          cSQL.Append(" (select top (6) periodo, isnull(dso,0) as dso ");

        if (!string.IsNullOrEmpty(lngCodHolding))
          cSQL.Append(" (select top (6) periodo, isnull((dbo.aplicaTipoCambio(cliente.signomoneda, isnull(dso,0), cliente.monedaholding)),0) as dso ");

        cSQL.Append(" from tablafinalindicadorweb  join cliente on (Cliente.nkey_cliente = tablafinalindicadorweb.nKey_Cliente) ");
        cSQL.Append(" where tablafinalindicadorweb.nkey_deudor = @nkey_deudor ");
        oParam.AddParameters("@nkey_deudor", lngnKeyDeudor, TypeSQL.Numeric);

        cSQL.Append(" and tablafinalindicadorweb.atraso is not null ");
        cSQL.Append(" and tablafinalindicadorweb.cobrado is not null ");
        cSQL.Append(" and tablafinalindicadorweb.atraso <> 0 ");
        cSQL.Append(" and tablafinalindicadorweb.cobrado <> 0 ");
        cSQL.Append(" and tablafinalindicadorweb.tipoConsulta = 'Deudor' ");

        if (!string.IsNullOrEmpty(lngnKeyCliente))
        {
          cSQL.Append(" and Cliente.nkey_cliente = @nkey_cliente ");
          oParam.AddParameters("@nkey_cliente", lngnKeyCliente, TypeSQL.Numeric);
        }


        if (!string.IsNullOrEmpty(lngCodHolding))
        {
          cSQL.Append(" and Cliente.ncodholding = @ncodholding ");
          oParam.AddParameters("@ncodholding", lngCodHolding, TypeSQL.Numeric);
        }

        cSQL.Append(" order by tablafinalindicadorweb.periodo desc) as T ");
        cSQL.Append(" order by periodo asc ");

        dtData = oConn.Select(cSQL.ToString(), oParam);
        pError = oConn.Error;
        return dtData;
      }
      else
      {
        return null;
      }
    }

    public DataTable GetSlaDso() {
      oParam = new DBConn.SQLParameters(3);
      DataTable dtData;

      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();
        cSQL.Append("select criterio_aceptacion as SLA_DSO, CASE WHEN criterio_aceptacion > 1 THEN 'días' WHEN criterio_aceptacion <= 1 THEN 'día' END as 'unidad'  ");
        cSQL.Append(" from indumbral ");
        cSQL.Append(" where indicador = 18 ");

        if (!string.IsNullOrEmpty(lngnKeyCliente))
        {
          cSQL.Append(" and nkey_cliente = @nkey_cliente and tipo = 'C' ");
          oParam.AddParameters("@nkey_cliente", lngnKeyCliente, TypeSQL.Numeric);
        }


        if (!string.IsNullOrEmpty(lngCodHolding))
        {
          cSQL.Append(" and nkey_cliente = @ncodholding and tipo = 'H' ");
          oParam.AddParameters("@ncodholding", lngCodHolding, TypeSQL.Numeric);
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

    #endregion

    #region PastDueCritico
    public double getTotalPastDue()
    {
      double iTotal = 0;
      oParam = new DBConn.SQLParameters(5);
      DataTable dtData;

      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();

        if (!string.IsNullOrEmpty(lngnKeyCliente))
          cSQL.Append("select isnull(sum(vista_saldo_factura.saldo),0) as past_due ");

        if (!string.IsNullOrEmpty(lngCodHolding))
          cSQL.Append("select isnull(sum(dbo.aplicaTipoCambio(cliente.signomoneda, isnull(vista_saldo_factura.saldo,0), cliente.monedaholding)),0) as past_due ");

        cSQL.Append(" from vista_saldo_factura join cliente on (Cliente.nkey_cliente = vista_saldo_factura.nKey_Cliente) ");
        cSQL.Append(" where vista_saldo_factura.nKey_Deudor =  @nKey_Deudor ");
        oParam.AddParameters("@nkey_deudor", lngnKeyDeudor, TypeSQL.Numeric);

        if (!string.IsNullOrEmpty(lngnKeyCliente))
        {
          cSQL.Append(" and Cliente.nKey_Cliente = @nKey_Cliente ");
          oParam.AddParameters("@nKey_Cliente", lngnKeyCliente, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(lngCodHolding))
        {
          cSQL.Append(" and Cliente.ncodholding = @ncodholding ");
          oParam.AddParameters("@ncodholding", lngCodHolding, TypeSQL.Numeric);
        }

        cSQL.Append(" and vista_saldo_factura.ctatipofact <> 12 and vista_saldo_factura.dias_atraso > 0 and vista_saldo_factura.saldo > 0 ");

        dtData = oConn.Select(cSQL.ToString(), oParam);
        if (dtData != null)
        {
          if (dtData.Rows.Count > 0) {
            iTotal = double.Parse(dtData.Rows[0]["past_due"].ToString());
          }
        }
        dtData = null;
      }
      return iTotal;
    }

    public double getPastDueCritico()
    {
      double PastDueCritico = 0;
      oParam = new DBConn.SQLParameters(5);
      DataTable dtData;

      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();
        if (!string.IsNullOrEmpty(lngnKeyCliente))
          cSQL.Append("select isnull(sum(vista_saldo_factura.saldo),0) as past_due_critico ");

        if (!string.IsNullOrEmpty(lngCodHolding))
          cSQL.Append("select isnull(sum(dbo.aplicaTipoCambio(cliente.signomoneda, isnull(vista_saldo_factura.saldo, 0), cliente.monedaholding)), 0) as past_due_critico ");

        cSQL.Append(" from vista_saldo_factura join cliente on (Cliente.nkey_cliente = vista_saldo_factura.nKey_Cliente) ");
        cSQL.Append(" where vista_saldo_factura.nKey_Deudor = @nKey_Deudor ");
        oParam.AddParameters("@nKey_Deudor", lngnKeyDeudor, TypeSQL.Numeric);

        if (!string.IsNullOrEmpty(lngnKeyCliente))
        {
          cSQL.Append(" and Cliente.nKey_Cliente = @nKey_Cliente ");
          oParam.AddParameters("@nKey_Cliente", lngnKeyCliente, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(lngCodHolding))
        {
          cSQL.Append(" and Cliente.ncodholding = @ncodholding ");
          oParam.AddParameters("@ncodholding", lngCodHolding, TypeSQL.Numeric);
        }

        cSQL.Append(" and vista_saldo_factura.ctatipofact <> 12 and vista_saldo_factura.dias_atraso > 30 and vista_saldo_factura.saldo > 0 ");

        dtData = oConn.Select(cSQL.ToString(), oParam);
        if (dtData != null)
        {
          if (dtData.Rows.Count > 0)
          {
            PastDueCritico = double.Parse(dtData.Rows[0]["past_due_critico"].ToString());
          }
        }
        dtData = null;
      }
      return PastDueCritico;
    }

    public double getTotalPastDueHist()
    {
      double iTotalHist = 0;
      oParam = new DBConn.SQLParameters(5);
      StringBuilder cSQL;
      DataTable dtData;

      if (oConn.bIsOpen)
      {
        cSQL = new StringBuilder();
        if (!string.IsNullOrEmpty(lngnKeyCliente))
          cSQL.Append(" select isnull(tablafinalindicadorweb.deuda,0) as past_due_historico ");

        if (!string.IsNullOrEmpty(lngCodHolding))
          cSQL.Append(" select isnull(sum(dbo.aplicaTipoCambio(cliente.signomoneda, isnull(tablafinalindicadorweb.deuda,0), cliente.monedaholding)),0) as past_due_historico ");

        cSQL.Append(" from tablafinalindicadorweb  ");
        cSQL.Append(" join cliente on (Cliente.nKey_Cliente = tablafinalindicadorweb.nKey_Cliente) ");

        if (!string.IsNullOrEmpty(lngnKeyCliente))
        {
          cSQL.Append(" where Cliente.nKey_Cliente = @nKey_Cliente  ");
          oParam.AddParameters("@nKey_Cliente", lngnKeyCliente, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(lngCodHolding))
        {
          cSQL.Append(" where Cliente.ncodholding = @ncodholding  ");
          oParam.AddParameters("@ncodholding", lngCodHolding, TypeSQL.Numeric);
        }
        cSQL.Append(" and tablafinalindicadorweb.nkey_deudor = @nkey_deudor and tablafinalindicadorweb.periodo = @periodo ");
        oParam.AddParameters("@nkey_deudor", lngnKeyDeudor, TypeSQL.Varchar);
        oParam.AddParameters("@periodo", sPeriodo, TypeSQL.Varchar);

        dtData = oConn.Select(cSQL.ToString(), oParam);
        if (dtData != null)
        {
          if (dtData.Rows.Count > 0)
          {
            iTotalHist = double.Parse(dtData.Rows[0]["past_due_historico"].ToString());
          }
        }
        dtData = null;
      }
      return iTotalHist;
    }

    public double getPastDueCriticoHist()
    {
      double iPastDueCriticoHist = 0;
      oParam = new DBConn.SQLParameters(5);
      StringBuilder cSQL;
      DataTable dtData;

      if (oConn.bIsOpen)
      {
        cSQL = new StringBuilder();
        if (!string.IsNullOrEmpty(lngnKeyCliente))
          cSQL.Append(" select isnull(tablafinalindicadorweb.deuda_mas_30,0) as past_due_critico_historico ");

        if (!string.IsNullOrEmpty(lngCodHolding))
          cSQL.Append(" select isnull(sum(dbo.aplicaTipoCambio(cliente.signomoneda,isnull(tablafinalindicadorweb.deuda_mas_30,0), cliente.monedaholding)),0) as past_due_critico_historico ");

        cSQL.Append(" from tablafinalindicadorweb ");
        cSQL.Append(" join cliente on (Cliente.nKey_Cliente = tablafinalindicadorweb.nKey_Cliente) ");

        if (!string.IsNullOrEmpty(lngnKeyCliente))
        {
          cSQL.Append(" where Cliente.nKey_Cliente = @nKey_Cliente  ");
          oParam.AddParameters("@nKey_Cliente", lngnKeyCliente, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(lngCodHolding))
        {
          cSQL.Append(" where Cliente.ncodholding = @ncodholding  ");
          oParam.AddParameters("@ncodholding", lngCodHolding, TypeSQL.Numeric);
        }
        cSQL.Append(" and tablafinalindicadorweb.nkey_deudor = @nkey_deudor and tablafinalindicadorweb.periodo = @periodo ");
        oParam.AddParameters("@nkey_deudor", lngnKeyDeudor, TypeSQL.Varchar);
        oParam.AddParameters("@periodo", sPeriodo, TypeSQL.Varchar);

        dtData = oConn.Select(cSQL.ToString(), oParam);
        if (dtData != null)
        {
          if (dtData.Rows.Count > 0)
          {
            iPastDueCriticoHist = double.Parse(dtData.Rows[0]["past_due_critico_historico"].ToString());
          }
        }
        dtData = null;
      }
      return iPastDueCriticoHist;
    }

    #endregion

    #region Litigios
    public DataTable GetLitigios()
    {
      oParam = new DBConn.SQLParameters(2);
      DataTable dtData;
      string Condicion = " where ";

      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();

        if (!string.IsNullOrEmpty(lngnKeyCliente))
          cSQL.Append(" select isnull(sum(isnull(saldo,0)),0) as saldo ");

        if (!string.IsNullOrEmpty(lngCodHolding))
          cSQL.Append(" select isnull(sum(dbo.aplicaTipoCambio(cliente.signomoneda, isnull(saldo,0), cliente.monedaholding)),0) as saldo ");

        cSQL.Append(" from vista_saldo_factura join cliente on (Cliente.nkey_cliente = vista_saldo_factura.nKey_Cliente) ");

        if (!string.IsNullOrEmpty(lngnKeyDeudor))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" vista_saldo_factura.nKey_Deudor = @nKey_Deudor ");
          oParam.AddParameters("@nKey_Deudor", lngnKeyDeudor, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(lngnKeyCliente))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cliente.nkey_cliente = @nkey_cliente ");
          oParam.AddParameters("@nkey_cliente", lngnKeyCliente, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(lngCodHolding))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cliente.ncodholding = @ncodholding ");
          oParam.AddParameters("@ncodholding", lngCodHolding, TypeSQL.Numeric);
        }

        cSQL.Append(" and vista_saldo_factura.cantlitigio > 0 and vista_saldo_factura.saldo > 0 ");

        if (!string.IsNullOrEmpty(sPeriodo))
        {
          switch (sPeriodo)
          {
            case "30":
              cSQL.Append(" and (vista_saldo_factura.dias_atraso <= 30) ");
              break;
            case "60":
              cSQL.Append(" and (vista_saldo_factura.dias_atraso >= 31 and vista_saldo_factura.dias_atraso <= 60) ");
              break;
            case "90":
              cSQL.Append(" and (vista_saldo_factura.dias_atraso >= 61 and vista_saldo_factura.dias_atraso <= 90) ");
              break;
            case "mayor":
              cSQL.Append(" and (vista_saldo_factura.dias_atraso > 90) ");
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

    public DataTable GetDiasProcesoNormalizacion()
    {
      oParam = new DBConn.SQLParameters(3);
      DataTable dtData;

      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();
        cSQL.Append(" select nKey_Normalizacion, dFechaNormalizacion, (select dFechaIngreso from Litigio where nKey_Litigio = Normalizacion.nKey_Litigio) as fecha_ingreso, ");
        cSQL.Append(" DATEDIFF(DAY,(select dFechaIngreso from Litigio where nKey_Litigio = Normalizacion.nKey_Litigio), dFechaNormalizacion) as resultado ");
        cSQL.Append(" from Normalizacion  ");
        cSQL.Append(" where (select dFechaIngreso from Litigio where nKey_Litigio = Normalizacion.nKey_Litigio) is not null ");
        cSQL.Append(" and nkey_deudor = @nkey_deudor ");
        oParam.AddParameters("@nkey_deudor", lngnKeyDeudor, TypeSQL.Numeric);

        if (!string.IsNullOrEmpty(lngCodHolding))
        {
          cSQL.Append(" and nkey_cliente in(select nkey_cliente from cliente where ncodholding = @ncodholding ) ");
          oParam.AddParameters("@ncodholding", lngCodHolding, TypeSQL.Numeric);
        }
        else
        {
          if (!string.IsNullOrEmpty(lngnKeyCliente))
          {
            cSQL.Append(" and nkey_cliente = @nkey_cliente ");
            oParam.AddParameters("@nkey_cliente", lngnKeyCliente, TypeSQL.Numeric);
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

    #endregion

    #region DBT
    public DataTable GetDBT()
    {
      oParam = new DBConn.SQLParameters(2);
      DataTable dtData;

      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();

        if (!string.IsNullOrEmpty(lngnKeyCliente))
          cSQL.Append("select top 6 periodo, isnull(CAST(atraso AS DECIMAL) / CAST (cobrado AS DECIMAL),0) as dbt ");

        if (!string.IsNullOrEmpty(lngCodHolding))
          cSQL.Append("select top 6 periodo, isnull((dbo.aplicaTipoCambio(cliente.signomoneda, isnull(CAST(atraso AS DECIMAL) / CAST (cobrado AS DECIMAL),0), cliente.monedaholding)),0) as dbt ");

        cSQL.Append(" from tablafinalindicadorweb join cliente on (Cliente.nkey_cliente = tablafinalindicadorweb.nKey_Cliente) ");
        cSQL.Append(" where tablafinalindicadorweb.nkey_deudor = @nkey_deudor ");
        oParam.AddParameters("@nkey_deudor", lngnKeyDeudor, TypeSQL.Numeric);

        cSQL.Append(" and tablafinalindicadorweb.atraso is not null  ");
        cSQL.Append(" and tablafinalindicadorweb.cobrado is not null ");
        cSQL.Append(" and tablafinalindicadorweb.atraso <> 0  ");
        cSQL.Append(" and tablafinalindicadorweb.cobrado <> 0 ");
        cSQL.Append(" and tablafinalindicadorweb.tipoConsulta= 'Deudor'  ");

        if (!string.IsNullOrEmpty(lngnKeyCliente))
        {
          cSQL.Append(" and cliente.nkey_cliente = @nkey_cliente ");
          oParam.AddParameters("@nkey_cliente", lngnKeyCliente, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(lngCodHolding))
        {
          cSQL.Append(" and Cliente.ncodholding = @ncodholding ");
          oParam.AddParameters("@ncodholding", lngCodHolding, TypeSQL.Numeric);
        }

        cSQL.Append(" order by tablafinalindicadorweb.periodo desc ");

        dtData = oConn.Select(cSQL.ToString(), oParam);
        pError = oConn.Error;
        return dtData;
      }
      else
      {
        return null;
      }
    }

    public DataTable GetCondicionPago() {
      oParam = new DBConn.SQLParameters(2);
      DataTable dtData;

      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();

        cSQL.Append("select top 1 DATEDIFF (DAY, dFechaEmision , dFechaVencimiento )  as condicion_pago ");
        cSQL.Append(" from Factura join cliente on (Cliente.nKey_Cliente = Factura.nKey_Cliente) ");
        cSQL.Append(" where Factura.nKey_Deudor = @nkey_deudor  ");
        oParam.AddParameters("@nkey_deudor", lngnKeyDeudor, TypeSQL.Numeric);

        if (!string.IsNullOrEmpty(lngnKeyCliente))
        {
          cSQL.Append(" and cliente.nkey_cliente = @nkey_cliente ");
          oParam.AddParameters("@nkey_cliente", lngnKeyCliente, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(lngCodHolding))
        {
          cSQL.Append(" and Cliente.ncodholding = @ncodholding ");
          oParam.AddParameters("@ncodholding", lngCodHolding, TypeSQL.Numeric);
        }

        //cSQL.Append(" and DATEDIFF (DAY, dFechaEmision , dFechaVencimiento ) <> 0  and Factura.tipo_factura <> 12 order by nKey_Factura desc ");

        cSQL.Append(" and Factura.tipo_factura  not in (12,9) order by Factura.nKey_Factura desc ");

        dtData = oConn.Select(cSQL.ToString(), oParam);
        pError = oConn.Error;
        return dtData;
      }
      else
      {
        return null;
      }
    }
    #endregion

    #region Estimado vs Real

    public DataTable GetEstimadoVsReal()
    {
      oParam = new DBConn.SQLParameters(3);
      DataTable dtData;
      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();

        if (!string.IsNullOrEmpty(lngnKeyCliente))
        {
          cSQL.Append(" select isnull(sum(metasanalistadeudor.real1 + metasanalistadeudor.real2 + metasanalistadeudor.real3 + metasanalistadeudor.real4 + metasanalistadeudor.real5),0) as real, ");
          cSQL.Append(" isnull(sum(metasanalistadeudor.estimado1 + metasanalistadeudor.estimado2 + metasanalistadeudor.estimado3 + metasanalistadeudor.estimado4 + metasanalistadeudor.estimado5),0) as estimado ");
        }

        if (!string.IsNullOrEmpty(lngCodHolding))
        {
          cSQL.Append(" select isnull(sum(dbo.aplicaTipoCambio(cliente.signomoneda, metasanalistadeudor.real1 + metasanalistadeudor.real2 + metasanalistadeudor.real3 + metasanalistadeudor.real4 + metasanalistadeudor.real5, cliente.monedaholding)),0) as real,  ");
          cSQL.Append(" isnull(sum(dbo.aplicaTipoCambio(cliente.signomoneda, metasanalistadeudor.estimado1 + metasanalistadeudor.estimado2 + metasanalistadeudor.estimado3 + metasanalistadeudor.estimado4 + metasanalistadeudor.estimado5,cliente.monedaholding)),0) as estimado ");
        }

        cSQL.Append(" from metasanalistadeudor join cliente on (Cliente.nkey_cliente = metasanalistadeudor.nKey_Cliente) ");
        cSQL.Append(" where metasanalistadeudor.nkey_deudor = @nkey_deudor ");
        oParam.AddParameters("@nkey_deudor", lngnKeyDeudor, TypeSQL.Numeric);

        if (!string.IsNullOrEmpty(lngnKeyCliente))
        {
          cSQL.Append(" and metasanalistadeudor.nkey_cliente in(select distinct ncodholding from cliente where nkey_cliente = @nkey_cliente and bCuentaCorriente <> 0 and sAnalista <> '')  ");
          oParam.AddParameters("@nkey_cliente", lngnKeyCliente, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(lngCodHolding))
        {
          cSQL.Append(" and metasanalistadeudor.nkey_cliente = @ncodholding ");
          oParam.AddParameters("@ncodholding", lngCodHolding, TypeSQL.Numeric);
        }

        cSQL.Append(" and periodo = @periodo ");
        oParam.AddParameters("@periodo", sPeriodo, TypeSQL.Varchar);

        dtData = oConn.Select(cSQL.ToString(), oParam);
        pError = oConn.Error;
        return dtData;
      }
      else
      {
        return null;
      }
    }

    public double GetTotal()
    {
      double iTotal = 0;
      oParam = new DBConn.SQLParameters(3);
      DataTable dtData;

      if (oConn.bIsOpen)
      {
        StringBuilder cSQLFacturado = new StringBuilder();
        if (!string.IsNullOrEmpty(lngnKeyCliente))
          cSQLFacturado.Append("select isnull(SUM(isnull(saldo,0)),0) total ");

        if (!string.IsNullOrEmpty(lngCodHolding))
          cSQLFacturado.Append("select isnull(SUM(dbo.aplicaTipoCambio(cliente.signomoneda, isnull(saldo,0), cliente.monedaholding)),0) total ");

        cSQLFacturado.Append(" from vista_antiguedad_deuda_new_1 join cliente on (Cliente.nkey_cliente = vista_antiguedad_deuda_new_1.nkey_cliente) ");
        cSQLFacturado.Append(" where vista_antiguedad_deuda_new_1.nKey_Deudor = @nKey_Deudor ");
        oParam.AddParameters("@nKey_Deudor", lngnKeyDeudor, TypeSQL.Numeric);

        cSQLFacturado.Append(" and vista_antiguedad_deuda_new_1.electronica not in (9,12) ");

        if (!string.IsNullOrEmpty(lngnKeyCliente))
        {
          cSQLFacturado.Append(" and cliente.nKey_Cliente = @nkey_cliente ");
          oParam.AddParameters("@nkey_cliente", lngnKeyCliente, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(lngCodHolding))
        {
          cSQLFacturado.Append(" and Cliente.ncodholding = @ncodholding ");
          oParam.AddParameters("@ncodholding", lngCodHolding, TypeSQL.Numeric);
        }

        dtData = oConn.Select(cSQLFacturado.ToString(), oParam);
        if (dtData != null)
        {
          if (dtData.Rows.Count > 0)
          {
            iTotal = double.Parse(dtData.Rows[0]["total"].ToString());
          }
        }
        dtData = null;

        return iTotal;
      }
      else
      {
        return 0;
      }
    }

    public float GetFacturado()
    {
      float ifacturado = 0;
      float inotacredito = 0;
      oParam = new DBConn.SQLParameters(3);
      DataTable dtData;

      if (oConn.bIsOpen)
      {
        StringBuilder cSQLFacturado = new StringBuilder();
        if (!string.IsNullOrEmpty(lngnKeyCliente))
          cSQLFacturado.Append(" select isnull(sum(isnull(Factura.nMontoFactura,0)),0) as facturado ");

        if (!string.IsNullOrEmpty(lngCodHolding))
          cSQLFacturado.Append(" select isnull(sum(dbo.aplicaTipoCambio(cliente.signomoneda, isnull(Factura.nMontoFactura,0), cliente.monedaholding)),0) as facturado ");

        cSQLFacturado.Append(" from Factura join cliente on (Cliente.nkey_cliente = Factura.nKey_Cliente) ");
        cSQLFacturado.Append(" where left(convert(varchar,Factura.dFechaIngreso,112),6) = @dFechaIngreso  ");
        oParam.AddParameters("@dFechaIngreso", sPeriodo, TypeSQL.Varchar);

        cSQLFacturado.Append(" and Factura.nKey_Deudor = @nKey_Deudor and Factura.tipo_factura <> 1 ");
        oParam.AddParameters("@nKey_Deudor", lngnKeyDeudor, TypeSQL.Numeric);

        if (!string.IsNullOrEmpty(lngCodHolding))
        {
          cSQLFacturado.Append(" and Cliente.ncodholding = @ncodholding ");
          oParam.AddParameters("@ncodholding", lngCodHolding, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(lngnKeyCliente))
        {
          cSQLFacturado.Append(" and Cliente.ncodholding = (select distinct ncodholding from cliente where nKey_Cliente  = @nkey_cliente) ");
          oParam.AddParameters("@nkey_cliente", lngnKeyCliente, TypeSQL.Numeric);
        }

        dtData = oConn.Select(cSQLFacturado.ToString(), oParam);
        if (dtData != null)
        {
          if (dtData.Rows.Count > 0)
          {
            ifacturado = float.Parse(dtData.Rows[0]["facturado"].ToString());
          }
        }
        dtData = null;

        oParam = new DBConn.SQLParameters(3);
        StringBuilder cSQLNotaCredito = new StringBuilder();

        if (!string.IsNullOrEmpty(lngnKeyCliente))
          cSQLNotaCredito.Append(" select isnull(sum(isnull(NotaCredito.nMontoNotaCredito,0)),0) as nota_credito  ");

        if (!string.IsNullOrEmpty(lngCodHolding))
          cSQLNotaCredito.Append(" select isnull(sum(dbo.aplicaTipoCambio(cliente.signomoneda, isnull(NotaCredito.nMontoNotaCredito,0), cliente.monedaholding)),0) as nota_credito ");

        cSQLNotaCredito.Append(" from NotaCredito join cliente on (Cliente.nkey_cliente = NotaCredito.nKey_Cliente) ");
        cSQLNotaCredito.Append(" where left(convert(varchar,NotaCredito.dFechaIngreso,112),6) = @dFechaIngreso  ");
        oParam.AddParameters("@dFechaIngreso", sPeriodo, TypeSQL.Varchar);

        cSQLNotaCredito.Append(" and NotaCredito.nKey_Deudor = @nKey_Deudor ");
        oParam.AddParameters("@nKey_Deudor", lngnKeyDeudor, TypeSQL.Numeric);


        if (!string.IsNullOrEmpty(lngnKeyCliente))
        {
          cSQLNotaCredito.Append(" and Cliente.ncodholding = (select distinct ncodholding from cliente where nKey_Cliente  = @nkey_cliente ) ");
          oParam.AddParameters("@nkey_cliente", lngnKeyCliente, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(lngCodHolding))
        {
          cSQLNotaCredito.Append(" and Cliente.ncodholding = @ncodholding ");
          oParam.AddParameters("@ncodholding", lngCodHolding, TypeSQL.Numeric);
        }

        dtData = oConn.Select(cSQLNotaCredito.ToString(), oParam);
        if (dtData != null)
        {
          if (dtData.Rows.Count > 0)
          {
            inotacredito = float.Parse(dtData.Rows[0]["nota_credito"].ToString());
          }
        }
        dtData = null;

        ifacturado = ifacturado - inotacredito;

        return ifacturado;

      }
      else
      {
        return 0;
      }

    }

    public float GetDeudaVencida()
    {

      float iDeudaVencida = 0;
      oParam = new DBConn.SQLParameters(3);
      DataTable dtData;
      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();

        if (!string.IsNullOrEmpty(lngnKeyCliente))
          cSQL.Append("select isnull(sum(isnull(vista_saldo_factura.saldo,0)),0) as deuda_vencida ");

        if (!string.IsNullOrEmpty(lngCodHolding))
          cSQL.Append("select isnull(sum(dbo.aplicaTipoCambio(cliente.signomoneda, isnull(vista_saldo_factura.saldo,0), cliente.monedaholding)),0) as deuda_vencida ");

        cSQL.Append(" from vista_saldo_factura join cliente on (Cliente.nkey_cliente = vista_saldo_factura.nKey_Cliente) ");
        cSQL.Append(" where vista_saldo_factura.nkey_deudor= @nkey_deudor ");
        oParam.AddParameters("@nkey_deudor", lngnKeyDeudor, TypeSQL.Numeric);

        cSQL.Append(" and vista_saldo_factura.saldo > 0 and vista_saldo_factura.dfechavencimiento < @periodo and vista_saldo_factura.ctatipofact not in (12,9) ");
        oParam.AddParameters("@periodo", sPeriodo, TypeSQL.Varchar);

        if (!string.IsNullOrEmpty(lngnKeyCliente))
        {
          cSQL.Append(" and Cliente.nKey_Cliente = @nKey_Cliente ");
          oParam.AddParameters("@nKey_Cliente", lngnKeyCliente, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(lngCodHolding))
        {
          cSQL.Append(" and Cliente.ncodholding = @ncodholding ");
          oParam.AddParameters("@ncodholding", lngCodHolding, TypeSQL.Numeric);
        }

        dtData = oConn.Select(cSQL.ToString(), oParam);

        if (dtData != null)
        {
          if (dtData.Rows.Count > 0)
          {
            iDeudaVencida = float.Parse(dtData.Rows[0]["deuda_vencida"].ToString());
          }
        }
        dtData = null;

        return iDeudaVencida;

      }
      else
      {
        return 0;
      }
    }

    public float GetFacturasMayor30()
    {
      float iFacturasMayor30 = 0;
      oParam = new DBConn.SQLParameters(3);
      DataTable dtData;
      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();
        if (!string.IsNullOrEmpty(lngnKeyCliente))
          cSQL.Append(" select isnull(sum(isnull(saldo,0)),0) as facturas_mayor_30 ");

        if (!string.IsNullOrEmpty(lngCodHolding))
          cSQL.Append(" select isnull(sum(dbo.aplicaTipoCambio(cliente.signomoneda, isnull(saldo,0), cliente.monedaholding)),0) as facturas_mayor_30 ");

        cSQL.Append(" from vista_saldo_factura join cliente on (Cliente.nkey_cliente = vista_saldo_factura.nKey_Cliente) ");
        cSQL.Append(" where vista_saldo_factura.nkey_deudor= @nkey_deudor ");
        oParam.AddParameters("@nkey_deudor", lngnKeyDeudor, TypeSQL.Numeric);

        if (!string.IsNullOrEmpty(lngnKeyCliente))
        {
          cSQL.Append(" and cliente.nKey_Cliente = @nKey_Cliente ");
          oParam.AddParameters("@nKey_Cliente", lngnKeyCliente, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(lngCodHolding))
        {
          cSQL.Append(" and cliente.ncodholding = @ncodholding ");
          oParam.AddParameters("@ncodholding", lngCodHolding, TypeSQL.Numeric);
        }

        cSQL.Append(" and vista_saldo_factura.saldo > 0 and (vista_saldo_factura.dias_atraso > 30) ");

        dtData = oConn.Select(cSQL.ToString(), oParam);
        if (dtData != null)
        {
          if (dtData.Rows.Count > 0)
          {
            iFacturasMayor30 = float.Parse(dtData.Rows[0]["facturas_mayor_30"].ToString());
          }
        }

        return iFacturasMayor30;
      }
      else
      {
        return 0;
      }
    }

    public float GetFacturasMayor30SinLitigio()
    {
      float iFacturasMayor30SinLitigio = 0;
      oParam = new DBConn.SQLParameters(3);
      DataTable dtData;
      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();
        if (!string.IsNullOrEmpty(lngnKeyCliente))
          cSQL.Append(" select isnull(sum(isnull(saldo,0)),0) as factura_mayor_30_sin_litigio ");

        if (!string.IsNullOrEmpty(lngCodHolding))
          cSQL.Append(" select isnull(sum(dbo.aplicaTipoCambio(cliente.signomoneda, isnull(saldo,0), cliente.monedaholding)),0) as factura_mayor_30_sin_litigio ");

        cSQL.Append(" from vista_saldo_factura join cliente on (Cliente.nkey_cliente = vista_saldo_factura.nKey_Cliente)");
        cSQL.Append(" where vista_saldo_factura.nkey_deudor= @nkey_deudor ");
        oParam.AddParameters("@nkey_deudor", lngnKeyDeudor, TypeSQL.Numeric);

        if (!string.IsNullOrEmpty(lngnKeyCliente))
        {
          cSQL.Append(" and cliente.nKey_Cliente = @nKey_Cliente ");
          oParam.AddParameters("@nKey_Cliente", lngnKeyCliente, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(lngCodHolding))
        {
          cSQL.Append(" and cliente.ncodholding = @ncodholding ");
          oParam.AddParameters("@ncodholding", lngCodHolding, TypeSQL.Numeric);
        }

        cSQL.Append(" and vista_saldo_factura.saldo > 0 and (vista_saldo_factura.dias_atraso > 30) and vista_saldo_factura.cantlitigio = 0 ");

        dtData = oConn.Select(cSQL.ToString(), oParam);
        if (dtData != null)
        {
          if (dtData.Rows.Count > 0)
          {
            iFacturasMayor30SinLitigio = float.Parse(dtData.Rows[0]["factura_mayor_30_sin_litigio"].ToString());
          }
        }

        return iFacturasMayor30SinLitigio;
      }
      else
      {
        return 0;
      }
    }

    public double GetDeudaDocumentada()
    {
      double iDeudaDoc = 0;
      oParam = new DBConn.SQLParameters(3);
      DataTable dtData;
      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();

        if (!string.IsNullOrEmpty(lngnKeyCliente))
          cSQL.Append("select isnull(sum(vista_saldo_factura.saldo),0) as monto_deudda_documentada ");

        if (!string.IsNullOrEmpty(lngCodHolding))
          cSQL.Append("select isnull(sum(dbo.aplicaTipoCambio(cliente.signomoneda, isnull(vista_saldo_factura.saldo,0), cliente.monedaholding)),0) as monto_deudda_documentada ");

        cSQL.Append(" from vista_saldo_factura JOIN cliente ON (Cliente.nkey_cliente = vista_saldo_factura.nKey_Cliente) ");
        cSQL.Append(" where vista_saldo_factura.nKey_Deudor = @nKey_Deudor ");
        oParam.AddParameters("@nKey_Deudor", lngnKeyDeudor, TypeSQL.Numeric);

        if (!string.IsNullOrEmpty(lngnKeyCliente))
        {
          cSQL.Append(" and vista_saldo_factura.nKey_Cliente = @nKey_Cliente ");
          oParam.AddParameters("@nKey_Cliente", lngnKeyCliente, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(lngCodHolding))
        {
          cSQL.Append(" and vista_saldo_factura.ncodholding = @ncodholding ");
          oParam.AddParameters("@ncodholding", lngCodHolding, TypeSQL.Numeric);
        }

        cSQL.Append(" and (vista_saldo_factura.ctatipofact = 12 or vista_saldo_factura.ctatipofact = 9) ");

        dtData = oConn.Select(cSQL.ToString(), oParam);
        if (dtData != null)
        {
          if (dtData.Rows.Count > 0)
          {
            iDeudaDoc = double.Parse(dtData.Rows[0]["monto_deudda_documentada"].ToString());
          }
        }
        dtData = null;
      }
      return iDeudaDoc;
    }

    #endregion

    #region Provision
    public string GetImpactoProvision()
    {
      string sImpactoProvision = string.Empty;
      oParam = new DBConn.SQLParameters(3);
      DataTable dtData;
      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();

        if (!string.IsNullOrEmpty(lngnKeyCliente))
          cSQL.Append(" select top 1 periodo, isnull(provision,0)  as impacto_provision  ");

        if (!string.IsNullOrEmpty(lngCodHolding))
          cSQL.Append(" select top 1 periodo, isnull((dbo.aplicaTipoCambio(cliente.signomoneda, isnull(provision,0), cliente.monedaholding)),0)  as impacto_provision ");

        cSQL.Append(" from tablafinalindicadorweb join cliente on (Cliente.nkey_cliente = tablafinalindicadorweb.nKey_Cliente) ");
        cSQL.Append(" where tablafinalindicadorweb.nkey_deudor = @nkey_deudor ");
        oParam.AddParameters("@nkey_deudor", lngnKeyDeudor, TypeSQL.Numeric);

        if (!string.IsNullOrEmpty(lngnKeyCliente))
        {
          cSQL.Append(" and  Cliente.nKey_Cliente = @nKey_Cliente ");
          oParam.AddParameters("@nKey_Cliente", lngnKeyCliente, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(lngCodHolding))
        {
          cSQL.Append(" and  Cliente.ncodholding = @ncodholding ");
          oParam.AddParameters("@ncodholding", lngCodHolding, TypeSQL.Numeric);
        }

        cSQL.Append("order by tablafinalindicadorweb.periodo desc");

        dtData = oConn.Select(cSQL.ToString(), oParam);
        if (dtData != null)
        {
          if (dtData.Rows.Count > 0)
          {
            sImpactoProvision = dtData.Rows[0]["impacto_provision"].ToString();
          }
        }
        dtData = null;

        return sImpactoProvision;

      }
      else
      {
        return sImpactoProvision;
      }
    }

    public string GetProvision()
    {
      string sProvision = string.Empty;
      oParam = new DBConn.SQLParameters(3);
      DataTable dtData;
      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();

        if (!string.IsNullOrEmpty(lngnKeyCliente))
        {
          cSQL.Append(" select isnull(dbo.aplicaTipoCambio(cliente.signomoneda, isnull(criterio_aceptacion,0), cliente.monedaholding),0)  as provision  ");
          cSQL.Append(" from indumbral join cliente on (Cliente.nKey_Cliente = indumbral.nKey_Cliente) ");
          cSQL.Append(" where indicador = 23 and tipo = 'H' ");
          cSQL.Append(" and Cliente.nKey_Cliente in(select distinct ncodholding from cliente where nkey_cliente = @nKey_Cliente and bCuentaCorriente <> 0 and sAnalista <> '') ");
          oParam.AddParameters("@nKey_Cliente", lngnKeyCliente, TypeSQL.Numeric);
        }


        if (!string.IsNullOrEmpty(lngCodHolding))
        {
          cSQL.Append(" Select isnull(dbo.aplicaTipoCambio(cliente.signomoneda, isnull(criterio_aceptacion,0), cliente.monedaholding),0)  as provision ");
          cSQL.Append(" from indumbral join cliente on (Cliente.nKey_Cliente = indumbral.nKey_Cliente) where indicador = 23 ");
          cSQL.Append(" and tipo= 'H' and Cliente.nKey_Cliente = @ncodholding ");
          oParam.AddParameters("@ncodholding", lngCodHolding, TypeSQL.Numeric);
        }

        dtData = oConn.Select(cSQL.ToString(), oParam);
        if (dtData != null)
        {
          if (dtData.Rows.Count > 0)
          {
            sProvision = dtData.Rows[0]["provision"].ToString();
          }
        }

        return sProvision;
      }
      else
      {
        return string.Empty;
      }
    }

    public string GetProvisionAcumulada()
    {
      string sProvision = string.Empty;
      oParam = new DBConn.SQLParameters(3);
      DataTable dtData;
      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();
        cSQL.Append("select top 1 indresultado.nkey_cliente, indresultado.idfecha, indresultado.indicador, ");

        if (!string.IsNullOrEmpty(lngnKeyCliente))
          cSQL.Append(" isnull(valor,0) as provision_acumulada, ");

        if (!string.IsNullOrEmpty(lngCodHolding))
          cSQL.Append(" isnull(dbo.aplicaTipoCambio(cliente.signomoneda, isnull(valor,0), cliente.monedaholding),0) as provision_acumulada, ");

        cSQL.Append(" indresultado.nkey_tipo, indresultado.tipo, indresultado.fechaing  ");
        cSQL.Append(" from indresultado join cliente on (Cliente.nkey_cliente = indresultado.nkey_tipo) ");
        cSQL.Append(" where indresultado.indicador = 23 ");

        if (!string.IsNullOrEmpty(lngnKeyCliente))
        {
          cSQL.Append(" and indresultado.tipo = 'C' and indresultado.nkey_tipo = @nKey_Cliente ");
          oParam.AddParameters("@nKey_Cliente", lngnKeyCliente, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(lngCodHolding))
        {
          cSQL.Append(" and indresultado.tipo = 'H' and indresultado.nkey_tipo = @ncodholding ");
          oParam.AddParameters("@ncodholding", lngCodHolding, TypeSQL.Numeric);
        }

        cSQL.Append(" order by indresultado.idfecha desc ");

        dtData = oConn.Select(cSQL.ToString(), oParam);
        if (dtData != null)
        {
          if (dtData.Rows.Count > 0)
          {
            sProvision = dtData.Rows[0]["provision_acumulada"].ToString();
          }
        }

        return sProvision;
      }
      else
      {
        return string.Empty;
      }
    }

    #endregion

    #region Compromiso de Pago

    public int GetCumplidas()
    {
      int sCumplidas = 0;
      oParam = new DBConn.SQLParameters(3);
      DataTable dtData;
      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();
        cSQL.Append(" SELECT COUNT(cumplidas) as cumplidas FROM PromesasCliente where cumplidas= 1 ");
        cSQL.Append(" and nkey_deudor= @nkey_deudor ");
        oParam.AddParameters("@nkey_deudor", lngnKeyDeudor, TypeSQL.Numeric);

        if (!string.IsNullOrEmpty(lngnKeyCliente))
        {
          cSQL.Append(" and nKey_Cliente = @nKey_Cliente ");
          oParam.AddParameters("@nKey_Cliente", lngnKeyCliente, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(lngCodHolding))
        {
          cSQL.Append(" and nKey_Cliente in (select nKey_Cliente from cliente where ncodholding = @ncodholding) ");
          oParam.AddParameters("@ncodholding", lngCodHolding, TypeSQL.Numeric);
        }

        dtData = oConn.Select(cSQL.ToString(), oParam);
        if (dtData != null)
        {
          if (dtData.Rows.Count > 0)
          {
            sCumplidas = int.Parse(dtData.Rows[0]["cumplidas"].ToString());
          }
        }
        dtData = null;

        return sCumplidas;

      }
      else
      {
        return sCumplidas;
      }
    }

    public int GetNoCumplidas()
    {
      int sNoCumplidas = 0;
      oParam = new DBConn.SQLParameters(3);
      DataTable dtData;
      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();
        cSQL.Append(" SELECT COUNT(nocumplidas) as no_cumplidas FROM PromesasCliente where nocumplidas = 1 ");
        cSQL.Append(" and nkey_deudor= @nkey_deudor ");
        oParam.AddParameters("@nkey_deudor", lngnKeyDeudor, TypeSQL.Numeric);

        if (!string.IsNullOrEmpty(lngnKeyCliente))
        {
          cSQL.Append(" and nKey_Cliente = @nKey_Cliente ");
          oParam.AddParameters("@nKey_Cliente", lngnKeyCliente, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(lngCodHolding))
        {
          cSQL.Append(" and nKey_Cliente in (select nKey_Cliente from cliente where ncodholding = @ncodholding) ");
          oParam.AddParameters("@ncodholding", lngCodHolding, TypeSQL.Numeric);
        }

        dtData = oConn.Select(cSQL.ToString(), oParam);
        if (dtData != null)
        {
          if (dtData.Rows.Count > 0)
          {
            sNoCumplidas = int.Parse(dtData.Rows[0]["no_cumplidas"].ToString());
          }
        }
        dtData = null;

        return sNoCumplidas;

      }
      else
      {
        return sNoCumplidas;
      }
    }

    public string Get_nKeyCodigoDeudor()
    {
      oParam = new DBConn.SQLParameters(3);
      DataTable dtData;
      string nKeyCodigoDeudor = string.Empty;

      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();
        cSQL.Append(" select nKey_CodigoDeudor from codigodeudor ");
        cSQL.Append(" Where nkey_deudor= @nkey_deudor ");
        oParam.AddParameters("@nkey_deudor", lngnKeyDeudor, TypeSQL.Numeric);

        if (!string.IsNullOrEmpty(lngnKeyCliente))
        {
          cSQL.Append(" and nKey_Cliente = @nKey_Cliente ");
          oParam.AddParameters("@nKey_Cliente", lngnKeyCliente, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(lngCodHolding))
        {
          cSQL.Append(" and nKey_Cliente in (select nKey_Cliente from cliente where ncodholding = @ncodholding) ");
          oParam.AddParameters("@ncodholding", lngCodHolding, TypeSQL.Numeric);
        }

        dtData = oConn.Select(cSQL.ToString(), oParam);
        if (dtData != null)
        {
          if (dtData.Rows.Count > 0)
          {
            nKeyCodigoDeudor = dtData.Rows[0]["nKey_CodigoDeudor"].ToString();
          }
        }
        dtData = null;

        return nKeyCodigoDeudor;
      }
      else
      {
        return nKeyCodigoDeudor;
      }
    }

    public DataTable getCalendarioPago()
    {
      oParam = new DBConn.SQLParameters(3);
      DataTable dtData;
      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();
        cSQL.Append(" SELECT DISTINCT TOP 1 CodigoDeudor.nCodigoDeudor AS 'CodigoDeudor', GestionDeudor.dFechaCompromiso AS 'fecha', ");

        if (!string.IsNullOrEmpty(lngnKeyCliente))
          cSQL.Append(" isnull(GestionDeudor.nMontoPrometido,0) as 'Monto' ");

        if (!string.IsNullOrEmpty(lngCodHolding))
          cSQL.Append(" isnull(dbo.aplicaTipoCambio(cliente.signomoneda, isnull(GestionDeudor.nMontoPrometido,0), cliente.monedaholding),0) as 'Monto' ");

        cSQL.Append(" From Factura JOIN cliente ON (Cliente.nkey_cliente = Factura.nKey_Cliente) ");
        cSQL.Append(" LEFT JOIN Aplicacion ON (Factura.nKey_Factura = Aplicacion.nKey_Factura) ");
        cSQL.Append(" LEFT JOIN GestionDeudor ON(GestionDeudor.nKey_Factura = Factura.nKey_Factura) ");
        cSQL.Append(" LEFT JOIN CodigoDeudor ON (CodigoDeudor.nKey_Cliente = Factura.nKey_Cliente AND CodigoDeudor.nKey_Deudor = Factura.nKey_Deudor) ");
        cSQL.Append(" LEFT JOIN Deudor ON (Deudor.nKey_Deudor = Factura.nKey_Deudor) ");
        cSQL.Append(" LEFT JOIN COntactosDeudor ON (GestionDeudor.nKey_Contacto = ContactosDeudor.nKey_ContactoDeudor) ");
        cSQL.Append(" Where Factura.nkey_deudor= @nkey_deudor ");
        oParam.AddParameters("@nkey_deudor", lngnKeyDeudor, TypeSQL.Numeric);

        cSQL.Append(" and GestionDeudor.dFechaCompromiso >= @periodo ");
        oParam.AddParameters("@periodo", sPeriodo, TypeSQL.Varchar);

        if (!string.IsNullOrEmpty(lngnKeyCliente))
        {
          cSQL.Append(" and cliente.nKey_Cliente = @nKey_Cliente ");
          oParam.AddParameters("@nKey_Cliente", lngnKeyCliente, TypeSQL.Numeric);

          cSQL.Append(" GROUP BY  CodigoDeudor.nCodigoDeudor, GestionDeudor.dFechaCompromiso, isnull(GestionDeudor.nMontoPrometido,0), Deudor.sNombre ");
        }

        if (!string.IsNullOrEmpty(lngCodHolding))
        {
          cSQL.Append(" and Cliente.ncodholding = @ncodholding ");
          oParam.AddParameters("@ncodholding", lngCodHolding, TypeSQL.Numeric);

          cSQL.Append(" GROUP BY  CodigoDeudor.nCodigoDeudor, GestionDeudor.dFechaCompromiso, isnull(dbo.aplicaTipoCambio(cliente.signomoneda, isnull(GestionDeudor.nMontoPrometido,0), cliente.monedaholding),0), Deudor.sNombre ");
        }

        cSQL.Append(" ORDER BY GestionDeudor.dFechaCompromiso DESC ");

        dtData = oConn.Select(cSQL.ToString(), oParam);
        pError = oConn.Error;
        return dtData;
      }
      else
      {
        return null;
      }
    }

    public DataTable getDiaPago()
    {
      oParam = new DBConn.SQLParameters(3);
      DataTable dtData;

      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();
        cSQL.Append(" select tipo, dia, valor, nkey_codigodeudor, ");
        cSQL.Append(" CASE WHEN dia = 1 THEN 'Lunes' WHEN dia = 2 THEN 'Martes' WHEN dia = 3 THEN 'Miércoles' WHEN dia = 4 THEN 'Jueves' WHEN dia = 5 THEN 'Viernes' WHEN dia = 6 THEN 'Sábado' WHEN dia = 7 THEN 'Domingo' END AS dia_letra ");
        cSQL.Append(" from calendariopago ");
        cSQL.Append(" where nkey_codigodeudor = @nkey_codigodeudor ");
        cSQL.Append(" order by valor asc ");
        oParam.AddParameters("@nkey_codigodeudor", lnKeyCodigoDeudor, TypeSQL.Numeric);

        dtData = oConn.Select(cSQL.ToString(), oParam);
        pError = oConn.Error;
        return dtData;
      }
      else
      {
        return null;
      }
    }

    #endregion

    #region Facturas Comerciales

    public double getDiscrecionalesMes()
    {
      double iDiscrecional = 0;
      oParam = new DBConn.SQLParameters(3);
      DataTable dtData;
      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();
        if (!string.IsNullOrEmpty(lngnKeyCliente))
          cSQL.Append("select isnull(sum(acu_aplica_po.monto),0) as discrecionales ");

        if (!string.IsNullOrEmpty(lngCodHolding))
          cSQL.Append("select isnull(sum(dbo.aplicaTipoCambio(cliente.signomoneda, isnull(acu_aplica_po.monto,0), cliente.monedaholding)),0) as discrecionales ");

        cSQL.Append(" from acu_aplica_po ");
        cSQL.Append(" join tabla_datos on (tabla_datos.codigo = 'DESCOM' and isnull(valor_ref,0) = 0 and tabla_datos.valor_alf = acu_aplica_po.nkey_cliente and acu_aplica_po.ncodTipoDesc = tabla_datos.valor) ");
        cSQL.Append(" join cliente on (cliente.nKey_Cliente = acu_aplica_po.nkey_cliente ) ");
        cSQL.Append(" where acu_aplica_po.nkey_deudor = @nkey_deudor ");
        oParam.AddParameters("@nkey_deudor", lngnKeyDeudor, TypeSQL.Numeric);

        if (!string.IsNullOrEmpty(lngnKeyCliente))
        {
          cSQL.Append(" and cliente.nkey_cliente = @nkey_cliente ");
          oParam.AddParameters("@nkey_cliente", lngnKeyCliente, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(lngCodHolding))
        {
          cSQL.Append(" and cliente.ncodholding = @ncodholding ");
          oParam.AddParameters("@ncodholding", lngCodHolding, TypeSQL.Numeric);
        }

        cSQL.Append(" and acu_aplica_po.ano = Year(getdate()) and month(acu_aplica_po.fechaingreso) = MONTH(getdate()) ");

        dtData = oConn.Select(cSQL.ToString(), oParam);
        if (dtData != null)
        {
          if (dtData.Rows.Count > 0)
          {
            iDiscrecional = double.Parse(dtData.Rows[0]["discrecionales"].ToString());
          }
        }
        dtData = null;

        return iDiscrecional;
      }
      else
      {
        return iDiscrecional;
      }
    }

    public double getAcuerdosComercialesMes()
    {
      double iAcuerdosComerciales = 0;
      oParam = new DBConn.SQLParameters(3);
      DataTable dtData;

      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();
        if (!string.IsNullOrEmpty(lngnKeyCliente))
        {
          cSQL.Append("select isnull(sum(acu_aplica_po.monto),0) as acuerdos_comerciales ");
        }
        if (!string.IsNullOrEmpty(lngCodHolding))
        {
          cSQL.Append("select isnull(sum(dbo.aplicaTipoCambio(cliente.signomoneda, isnull(acu_aplica_po.monto,0), cliente.monedaholding)),0) as acuerdos_comerciales ");
        }

        cSQL.Append(" from acu_aplica_po ");
        cSQL.Append(" join tabla_datos on (tabla_datos.codigo = 'DESCOM' and isnull(valor_ref,0) <> 0 and tabla_datos.valor_alf = acu_aplica_po.nkey_cliente and acu_aplica_po.ncodTipoDesc = tabla_datos.valor) ");
        cSQL.Append(" join cliente on (cliente.nKey_Cliente = acu_aplica_po.nkey_cliente ) ");
        cSQL.Append(" where acu_aplica_po.nkey_deudor = @nkey_deudor ");
        oParam.AddParameters("@nkey_deudor", lngnKeyDeudor, TypeSQL.Numeric);

        if (!string.IsNullOrEmpty(lngnKeyCliente))
        {
          cSQL.Append(" and cliente.nkey_cliente = @nkey_cliente ");
          oParam.AddParameters("@nkey_cliente", lngnKeyCliente, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(lngCodHolding))
        {
          cSQL.Append(" and cliente.ncodholding = @ncodholding ");
          oParam.AddParameters("@ncodholding", lngCodHolding, TypeSQL.Numeric);
        }

        cSQL.Append(" and acu_aplica_po.ano = YEAR(getdate()) and month(acu_aplica_po.fechaingreso) = MONTH(getdate()) ");

        dtData = oConn.Select(cSQL.ToString(), oParam);
        if (dtData != null)
        {
          if (dtData.Rows.Count > 0)
          {
            iAcuerdosComerciales = double.Parse(dtData.Rows[0]["acuerdos_comerciales"].ToString());
          }
        }
        dtData = null;

        return iAcuerdosComerciales;

      }
      else
      {
        return iAcuerdosComerciales;
      }
    }

    public DataTable getNoDiscrecionalAno()
    {
      oParam = new DBConn.SQLParameters(3);
      DataTable dtData;
      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();
        if (!string.IsNullOrEmpty(lngnKeyCliente))
          cSQL.Append("select count(*) as documentos, isnull(sum(acu_aplica_po.monto),0) as acuerdos_comerciales ");

        if (!string.IsNullOrEmpty(lngCodHolding))
          cSQL.Append("select count(*) as documentos, isnull(sum(dbo.aplicaTipoCambio(cliente.signomoneda, isnull(acu_aplica_po.monto,0), cliente.monedaholding)),0) as acuerdos_comerciales ");

        cSQL.Append(" from acu_aplica_po ");
        cSQL.Append(" join tabla_datos on (tabla_datos.codigo = 'DESCOM' and isnull(valor_ref,0) <> 0 and tabla_datos.valor_alf = acu_aplica_po.nkey_cliente and acu_aplica_po.ncodTipoDesc = tabla_datos.valor) ");
        cSQL.Append(" join cliente on (cliente.nKey_Cliente = acu_aplica_po.nkey_cliente ) ");
        cSQL.Append(" where acu_aplica_po.nkey_deudor = @nkey_deudor ");
        oParam.AddParameters("@nkey_deudor", lngnKeyDeudor, TypeSQL.Numeric);

        if (!string.IsNullOrEmpty(lngnKeyCliente))
        {
          cSQL.Append(" and cliente.nkey_cliente = @nkey_cliente ");
          oParam.AddParameters("@nkey_cliente", lngnKeyCliente, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(lngCodHolding))
        {
          cSQL.Append(" and cliente.ncodholding = @ncodholding ");
          oParam.AddParameters("@ncodholding", lngCodHolding, TypeSQL.Numeric);
        }

        cSQL.Append(" and acu_aplica_po.ano = year(getdate()) ");

        dtData = oConn.Select(cSQL.ToString(), oParam);
        return dtData;
      }
      else
      {
        return null;
      }
    }

    public double getTotalAcuerdosComercialesAno()
    {
      double iAcuerdosComerciales = 0;
      oParam = new DBConn.SQLParameters(3);
      DataTable dtData;
      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();
        if (!string.IsNullOrEmpty(lngnKeyCliente))
        {
          cSQL.Append("select isnull(sum(acu_po.monto),0) as 'total' ");
        }
        if (!string.IsNullOrEmpty(lngCodHolding))
        {
          cSQL.Append("select isnull(sum(dbo.aplicaTipoCambio(cliente.signomoneda, isnull(acu_po.monto,0), cliente.monedaholding)),0) as 'total' ");
        }

        cSQL.Append(" from AcuerdosComerciales ");
        cSQL.Append(" left join acu_provision on (acu_provision.nkey_cliente = AcuerdosComerciales.nkey_cliente and acu_provision.nkey_deudor = AcuerdosComerciales.nkey_deudor and acu_provision.numcontrato = AcuerdosComerciales.numcontrato and acu_provision.ncodtipodesc = AcuerdosComerciales.ncodTipoDesc and acu_provision.ncodformdesc = AcuerdosComerciales.ncodFormDesc ) ");
        cSQL.Append(" left join acu_po on (acu_po.nkey_acuprovision = acu_provision.nkey_acuprovision) ");
        cSQL.Append(" join acu_ventas on (acu_ventas.nkey_ventas = acu_provision.nkey_ventas) ");
        cSQL.Append(" join cliente on (cliente.nKey_Cliente = AcuerdosComerciales.nkey_cliente ) ");
        cSQL.Append(" where AcuerdosComerciales.nkey_deudor = @nkey_deudor ");
        oParam.AddParameters("@nkey_deudor", lngnKeyDeudor, TypeSQL.Numeric);

        if (!string.IsNullOrEmpty(lngnKeyCliente))
        {
          cSQL.Append(" and cliente.nkey_cliente = @nkey_cliente ");
          oParam.AddParameters("@nkey_cliente", lngnKeyCliente, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(lngCodHolding))
        {
          cSQL.Append(" and cliente.ncodholding = @ncodholding ");
          oParam.AddParameters("@ncodholding", lngCodHolding, TypeSQL.Numeric);
        }

        cSQL.Append(" and acu_ventas.tipo = 'R' and AcuerdosComerciales.estado = 'A' and acu_ventas.ano = year(getdate()) ");

        dtData = oConn.Select(cSQL.ToString(), oParam);
        if (dtData != null)
        {
          if (dtData.Rows.Count > 0)
          {
            iAcuerdosComerciales = double.Parse(dtData.Rows[0]["total"].ToString());
          }
        }
        dtData = null;

        return iAcuerdosComerciales;
      }
      else
      {
        return iAcuerdosComerciales;
      }
    }

    #endregion

    #endregion
  }
}
