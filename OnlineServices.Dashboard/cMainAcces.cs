using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using OnlineServices.Conn;

namespace OnlineServices.Dashboard
{
  public class cMainAcces
  {
    DBConn.SQLParameters oParam;
    DBConn.DataTypeSQL TypeSQL = new DBConn.DataTypeSQL();

    private string nCodUsuario;
    public string CodUsuario { get { return nCodUsuario; } set { nCodUsuario = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private DBConn oConn;

    public cMainAcces()
    {

    }

    public cMainAcces(ref DBConn oConn)
    {
      this.oConn = oConn;
    }

    public DataTable getLast20Request() {

      oParam = new DBConn.SQLParameters(2);
      DataTable dtData;

      if (oConn.bIsOpen) {
        StringBuilder cSQL = new StringBuilder();
        cSQL.Append("select top 20 pag_log, convert(varchar(200), obs_log) funcionalidad, convert(varchar,max(fch_log),103) fecha, cod_evt_log, nkey_cliente, ncodholding, nkey_deudor from sys_log   ");
        cSQL.Append(" where not cod_evt_log in (0, 201) and obs_log not like '%#%' ");

        if (!string.IsNullOrEmpty(nCodUsuario))
        {
          cSQL.Append(" and id_usuario = @id_usuario ");
          oParam.AddParameters("@id_usuario", nCodUsuario, TypeSQL.Numeric);
        }

        cSQL.Append(" group by pag_log, convert(varchar(200), obs_log), cod_evt_log, nkey_cliente, ncodholding, nkey_deudor  order by max(fch_log) desc ");
        dtData = oConn.Select(cSQL.ToString(), oParam);
        pError = oConn.Error;
        return dtData;

      }
      else
      {
        return null;
      }

    }

    public DataTable get20MostRequest() {

      oParam = new DBConn.SQLParameters(2);
      DataTable dtData;

      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();
        cSQL.Append("select top 20 pag_log, convert(varchar(200), obs_log) funcionalidad, count(*) cantidad, cod_evt_log, nkey_cliente, ncodholding, nkey_deudor from sys_log  ");
        cSQL.Append(" where not cod_evt_log in (0, 201) and obs_log not like '%#%' ");

        if (!string.IsNullOrEmpty(nCodUsuario))
        {
          cSQL.Append(" and id_usuario = @id_usuario ");
          oParam.AddParameters("@id_usuario", nCodUsuario, TypeSQL.Numeric);
        }
        cSQL.Append(" and convert(varchar,fch_log,112) > convert(varchar, DATEADD(MONTH, -3, getdate()),112) ");
        cSQL.Append(" group by pag_log, convert(varchar(200), obs_log), cod_evt_log, nkey_cliente, ncodholding, nkey_deudor ");
        cSQL.Append(" order by cantidad desc ");
        dtData = oConn.Select(cSQL.ToString(), oParam);
        pError = oConn.Error;
        return dtData;

      }
      else
      {
        return null;
      }

    }

    public DataTable getTop20cli_may_pastdue() {
      oParam = new DBConn.SQLParameters(2);
      DataTable dtData;

      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();
        cSQL.Append("select top 20  Deudor.sNombre, deudor.nkey_deudor, cliente.nKey_Cliente, isnull(sum(dbo.aplicaTipoCambio(cliente.signomoneda, vista_saldo_factura.saldo, cliente.monedaholding)),0) as past_due   ");
        cSQL.Append(" from vista_saldo_factura  ");
        cSQL.Append(" join cliente on (Cliente.nkey_cliente = vista_saldo_factura.nKey_Cliente) ");
        cSQL.Append(" join Deudor on (Deudor.nKey_Deudor = vista_saldo_factura.nKey_Deudor)  ");
        cSQL.Append(" join Servicio on (Servicio.nKey_Deudor = vista_saldo_factura.nKey_Deudor and Servicio.nKey_Cliente = vista_saldo_factura.nKey_Cliente) ");
        cSQL.Append(" join Factura on (Factura.nKey_Cliente = vista_saldo_factura.nKey_Cliente and Factura.nKey_Deudor = vista_saldo_factura.nKey_Deudor and Factura.nNumeroFactura = vista_saldo_factura.nNumeroFactura)   ");
        cSQL.Append(" where Cliente.nKey_Cliente in (select nkey_user FROM sys_cliente_usuario where cod_user = @cod_user )  ");
        oParam.AddParameters("@cod_user", nCodUsuario, TypeSQL.Numeric);

        cSQL.Append(" and Servicio.nKey_Analista <> 4 ");
        cSQL.Append(" and vista_saldo_factura.ctatipofact not in (9,12)  ");
        cSQL.Append(" and vista_saldo_factura.dias_atraso > 0 ");
        cSQL.Append(" and Factura.ficticia='N' and vista_saldo_factura.saldo > 0 ");
        cSQL.Append(" group by Deudor.sNombre, deudor.nkey_deudor, cliente.nKey_Cliente ");
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

    public DataTable getTop20cli_may_pastdue_critico()
    {
      oParam = new DBConn.SQLParameters(2);
      DataTable dtData;

      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();
        cSQL.Append("select top 20  Deudor.sNombre, deudor.nkey_deudor, cliente.nKey_Cliente, isnull(sum(dbo.aplicaTipoCambio(cliente.signomoneda, vista_saldo_factura.saldo, cliente.monedaholding)),0) as past_due_critico ");
        cSQL.Append(" from vista_saldo_factura ");
        cSQL.Append(" join cliente on (Cliente.nkey_cliente = vista_saldo_factura.nKey_Cliente) ");
        cSQL.Append(" join Deudor on (Deudor.nKey_Deudor = vista_saldo_factura.nKey_Deudor)  ");
        cSQL.Append(" join Servicio on (Servicio.nKey_Deudor = vista_saldo_factura.nKey_Deudor and Servicio.nKey_Cliente = vista_saldo_factura.nKey_Cliente)  ");
        cSQL.Append(" join Factura on (Factura.nKey_Cliente = vista_saldo_factura.nKey_Cliente and Factura.nKey_Deudor = vista_saldo_factura.nKey_Deudor and Factura.nNumeroFactura = vista_saldo_factura.nNumeroFactura) ");
        cSQL.Append(" where Cliente.nKey_Cliente in (select nkey_user FROM sys_cliente_usuario where cod_user = @cod_user)  ");
        oParam.AddParameters("@cod_user", nCodUsuario, TypeSQL.Numeric);

        cSQL.Append(" and Servicio.nKey_Analista <> 4  ");
        cSQL.Append(" and vista_saldo_factura.dias_atraso > 30  ");
        cSQL.Append(" and Factura.ficticia='N' ");
        cSQL.Append(" and vista_saldo_factura.ctatipofact not in (9,12) ");
        cSQL.Append(" and vista_saldo_factura.dias_atraso > 0  ");
        cSQL.Append(" and vista_saldo_factura.saldo > 0 ");
        cSQL.Append(" group by Deudor.sNombre, deudor.nkey_deudor, cliente.nKey_Cliente ");
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

    public DataTable getTopCliMayProvision()
    {
      oParam = new DBConn.SQLParameters(2);
      DataTable dtData;

      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();
        cSQL.Append("select top 20  Deudor.sNombre, deudor.nKey_Deudor, cliente.nKey_Cliente, tablafinalindicadorweb.periodo, (dbo.aplicaTipoCambio(cliente.signomoneda, isnull(tablafinalindicadorweb.provision,0), cliente.monedaholding)) as impacto_provision ");
        cSQL.Append(" from tablafinalindicadorweb  ");
        cSQL.Append(" join Deudor on (Deudor.nKey_Deudor = tablafinalindicadorweb.nKey_Deudor) ");
        cSQL.Append(" join Cliente on (Cliente.nKey_Cliente = tablafinalindicadorweb.nkey_cliente) ");
        cSQL.Append(" where  Cliente.nKey_Cliente in (select nkey_user FROM sys_cliente_usuario where cod_user = @cod_user)  ");
        oParam.AddParameters("@cod_user", nCodUsuario, TypeSQL.Numeric);

        cSQL.Append(" and (tablafinalindicadorweb.provision is not null and tablafinalindicadorweb.provision <> 0) ");
        cSQL.Append(" order by tablafinalindicadorweb.periodo desc, tablafinalindicadorweb.provision desc ");
        dtData = oConn.Select(cSQL.ToString(), oParam);
        pError = oConn.Error;
        return dtData;

      }
      else
      {
        return null;
      }
    }

    public DataTable getTopCliMasLitigios()
    {
      oParam = new DBConn.SQLParameters(2);
      DataTable dtData;

      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();
        cSQL.Append("SELECT Top 20 Deudor.sNombre, deudor.nKey_Deudor, cliente.nKey_Cliente, count(*) as cantidad_litigios, isnull(sum(dbo.aplicaTipoCambio(cliente.signomoneda, isnull(Litigio.nMontoLitigio,0), cliente.monedaholding)),0) AS monto_litigio ");
        cSQL.Append(" FROM Litigio  ");
        cSQL.Append(" join vista_saldo_factura on (litigio.nKey_Cliente = vista_saldo_factura.nKey_Cliente and litigio.nKey_Deudor = vista_saldo_factura.nKey_Deudor and Litigio.nKey_Factura = vista_saldo_factura.ctakey)");
        cSQL.Append(" JOIN Cliente ON(Cliente.nKey_Cliente = Litigio.nKey_Cliente) ");
        cSQL.Append(" JOIN CodigoDeudor ON(CodigoDeudor.nKey_Cliente = Litigio.nKey_Cliente AND CodigoDeudor.nKey_Deudor = Litigio.nKey_Deudor) ");
        cSQL.Append(" JOIN Deudor ON(Deudor.nKey_Deudor = Litigio.nKey_Deudor) ");
        cSQL.Append(" WHERE cliente.nKey_Cliente in (select nkey_user FROM sys_cliente_usuario where cod_user = @cod_user) ");
        oParam.AddParameters("@cod_user", nCodUsuario, TypeSQL.Numeric);

        cSQL.Append(" and vista_saldo_factura.saldo <>  0 ");
        cSQL.Append(" and Litigio.bAclarado = 0 ");
        cSQL.Append(" group by Deudor.sNombre, deudor.nKey_Deudor, cliente.nKey_Cliente ");
        cSQL.Append(" order by sum(dbo.aplicaTipoCambio(cliente.signomoneda, Litigio.nMontoLitigio, cliente.monedaholding)) desc ");
        dtData = oConn.Select(cSQL.ToString(), oParam);
        pError = oConn.Error;
        return dtData;

      }
      else
      {
        return null;
      }
    }

    public DataTable getTopMenorCredibilidad()
    {
      oParam = new DBConn.SQLParameters(2);
      DataTable dtData;

      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();
        cSQL.Append("select top 20 Deudor.sNombre, deudor.nKey_Deudor, nKey_Cliente, count(Deudor.sNombre) as no_cumplidas ");
        cSQL.Append(" from PromesasCliente  ");
        cSQL.Append(" join Deudor on (Deudor.nKey_Deudor = PromesasCliente.nKey_Deudor)  ");
        cSQL.Append(" where PromesasCliente.nocumplidas= 1  ");
        cSQL.Append(" and nKey_Cliente in (select nkey_user FROM sys_cliente_usuario where cod_user = @cod_user ) ");
        oParam.AddParameters("@cod_user", nCodUsuario, TypeSQL.Numeric);

        cSQL.Append(" group by Deudor.sNombre, deudor.nKey_Deudor, nKey_Cliente ");
        cSQL.Append(" order by count(Deudor.sNombre) desc ");
        dtData = oConn.Select(cSQL.ToString(), oParam);
        pError = oConn.Error;
        return dtData;

      }
      else
      {
        return null;
      }
    }

    public DataTable getTopMejorCredibilidad()
    {
      oParam = new DBConn.SQLParameters(2);
      DataTable dtData;

      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();
        cSQL.Append("select top 50 Deudor.sNombre, deudor.nKey_Deudor, nKey_Cliente, count(Deudor.sNombre) as cumplidas ");
        cSQL.Append(" from PromesasCliente ");
        cSQL.Append(" join Deudor on (Deudor.nKey_Deudor = PromesasCliente.nKey_Deudor)  ");
        cSQL.Append(" where PromesasCliente.cumplidas= 1  ");
        cSQL.Append(" and nKey_Cliente in (select nkey_user FROM sys_cliente_usuario where cod_user = @cod_user) ");
        oParam.AddParameters("@cod_user", nCodUsuario, TypeSQL.Numeric);

        cSQL.Append(" group by Deudor.sNombre, deudor.nKey_Deudor, nKey_Cliente ");
        cSQL.Append(" order by count(Deudor.sNombre) desc ");
        dtData = oConn.Select(cSQL.ToString(), oParam);
        pError = oConn.Error;
        return dtData;

      }
      else
      {
        return null;
      }
    }

    public DataTable getTopVentaPotencial()
    {
      oParam = new DBConn.SQLParameters(2);
      DataTable dtData;

      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();
        cSQL.Append("select top 50 Deudor.sNombre, deudor.nKey_Deudor, nKey_Cliente, sum(LineaCredito.nLineaCreditoActual)as linea_credito_actual  ");
        cSQL.Append(" from LineaCredito ");
        cSQL.Append(" join Deudor on (Deudor.nKey_Deudor = LineaCredito.nKey_Deudor) ");
        cSQL.Append(" where LineaCredito.nKey_Cliente in (select nkey_user FROM sys_cliente_usuario where cod_user = @cod_user) ");
        oParam.AddParameters("@cod_user", nCodUsuario, TypeSQL.Numeric);

        cSQL.Append(" Group by Deudor.sNombre, deudor.nKey_Deudor, nKey_Cliente ");
        cSQL.Append(" order by sum(LineaCredito.nLineaCreditoActual) desc ");
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
