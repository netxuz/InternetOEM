using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OnlineServices.Conn;

namespace OnlineServices.SystemData
{
  public class SysLog
  {
    DBConn.SQLParameters oParam;
    DBConn.DataTypeSQL TypeSQL = new DBConn.DataTypeSQL();

    private string pCodLog;
    public string CodLog { get { return pCodLog; } set { pCodLog = value; } }

    private string pIpUsuario;
    public string IpUsuario { get { return pIpUsuario; } set { pIpUsuario = value; } }

    private string pIdUsuario;
    public string IdUsuario { get { return pIdUsuario; } set { pIdUsuario = value; } }

    private string pFchLog;
    public string FchLog { get { return pFchLog; } set { pFchLog = value; } }

    private string pPagLog;
    public string PagLog { get { return pPagLog; } set { pPagLog = value; } }

    private string pCodEvtLog;
    public string CodEvtLog { get { return pCodEvtLog; } set { pCodEvtLog = value; } }

    private string pObsLog;
    public string ObsLog { get { return pObsLog; } set { pObsLog = value; } }

    private string pAppLog;
    public string AppLog { get { return pAppLog; } set { pAppLog = value; } }

    private string pNombreApellido;
    public string NombreApellido { get { return pNombreApellido; } set { pNombreApellido = value; } }

    private string sFechaInicial;
    public string FechaInicial { get { return sFechaInicial; } set { sFechaInicial = value; } }

    private string sFechaFinal;
    public string FechaFinal { get { return sFechaFinal; } set { sFechaFinal = value; } }

    private string iNkeyCliente;
    public string NkeyCliente { get { return iNkeyCliente; } set { iNkeyCliente = value; } }

    private string iNcodHolding;
    public string NcodHolding { get { return iNcodHolding; } set { iNcodHolding = value; } }

    private string iNkeyDeudor;
    public string NkeyDeudor { get { return iNkeyDeudor; } set { iNkeyDeudor = value; } }

    private string pAccion;
    public string Accion { get { return pAccion; } set { pAccion = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private DBConn oConn;

    public SysLog() { 

    }

    public SysLog(ref DBConn oConn)
    {
      this.oConn = oConn;
    }

    public void Put()
    {
      DataTable dtData;
      oParam = new DBConn.SQLParameters(10);
      StringBuilder cSQL;
      string sComa = string.Empty;

      if (oConn.bIsOpen)
      {
        try
        {
          switch (pAccion)
          {
            case "CREAR":
              pCodLog = string.Empty;
              cSQL = new StringBuilder();
              cSQL.Append("insert into sys_log(ip_usuario, id_usuario, fch_log, pag_log, cod_evt_log, obs_log, app_log, nkey_cliente, ncodholding, nkey_deudor ) values(");
              cSQL.Append("@ip_usuario, @id_usuario, @fch_log, @pag_log, @cod_evt_log, @obs_log, @app_log, @nkey_cliente, @ncodholding, @nkey_deudor)");
              oParam.AddParameters("@ip_usuario", pIpUsuario, TypeSQL.Varchar);
              oParam.AddParameters("@id_usuario", pIdUsuario, TypeSQL.Numeric);
              oParam.AddParameters("@fch_log", pFchLog, TypeSQL.DateTime);
              oParam.AddParameters("@pag_log", pPagLog, TypeSQL.Varchar);
              oParam.AddParameters("@cod_evt_log", pCodEvtLog, TypeSQL.Numeric);
              oParam.AddParameters("@obs_log", pObsLog, TypeSQL.Text);
              oParam.AddParameters("@app_log", pAppLog, TypeSQL.Varchar);
              oParam.AddParameters("@nkey_cliente", iNkeyCliente, TypeSQL.Numeric);
              oParam.AddParameters("@ncodholding", iNcodHolding, TypeSQL.Numeric);
              oParam.AddParameters("@nkey_deudor", iNkeyDeudor, TypeSQL.Numeric);
              oConn.Insert(cSQL.ToString(), oParam);

              cSQL = new StringBuilder();
              cSQL.Append("select @@IDENTITY");
              dtData = oConn.Select(cSQL.ToString());
              if (dtData != null)
                if (dtData.Rows.Count > 0)
                  pCodLog = dtData.Rows[0][0].ToString();
              dtData = null;

              break;
            case "ELIMINAR":
              cSQL = new StringBuilder();
              cSQL.Append("delete from sys_log where cod_log = @cod_log ");
              oParam.AddParameters("@cod_log", pCodEvtLog, TypeSQL.Numeric);
              oConn.Delete(cSQL.ToString(), oParam);

              break;
          }
        }
        catch (Exception Ex)
        {
          pError = Ex.Message;
        }
      }
    }

    public DataTable Get()
    {
      oParam = new DBConn.SQLParameters(10);
      DataTable dtData;
      StringBuilder cSQL;
      string Condicion = " where ";

      if (oConn.bIsOpen)
      {
        cSQL = new StringBuilder();
        cSQL.Append("select a.cod_log, a.ip_usuario, a.id_usuario, (select nom_user + ' ' + ape_user from sys_usuario where cod_user = a.id_usuario) nombreapellido, convert(varchar,a.fch_log, 103) + ' ' +  convert(varchar,a.fch_log, 108) fechahora, a.pag_log, a.cod_evt_log, a.obs_log, a.app_log ");
        cSQL.Append("from sys_log a ");

        if (!string.IsNullOrEmpty(pCodLog))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" a.cod_log = @cod_log ");
          oParam.AddParameters("@cod_log", pCodLog, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(pIpUsuario))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" a.ip_usuario = @ip_usuario ");
          oParam.AddParameters("@ip_usuario", pIpUsuario, TypeSQL.Varchar);
        }

        if (!string.IsNullOrEmpty(pIdUsuario))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" a.id_usuario = @id_usuario ");
          oParam.AddParameters("@id_usuario", pIdUsuario, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(pCodEvtLog))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" a.cod_evt_log = @cod_evt_log ");
          oParam.AddParameters("@cod_evt_log", pCodEvtLog, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(pObsLog))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" a.obs_log like '%' + @obs_log + '%' ");
          oParam.AddParameters("@obs_log", pObsLog, TypeSQL.Text);
        }

        if (!string.IsNullOrEmpty(pAppLog))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" a.app_log like '%' + @app_log + '%' ");
          oParam.AddParameters("@app_log", pAppLog, TypeSQL.Varchar);
        }

        if (!string.IsNullOrEmpty(pNombreApellido))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" a.id_usuario in(select cod_user from sys_usuario where concat(nom_user, ' ', ape_user) like '%' + @nombreapellido + '%' ) ");
          oParam.AddParameters("@nombreapellido", pNombreApellido, TypeSQL.Varchar);
        }

        if ((!string.IsNullOrEmpty(sFechaInicial)) && (!string.IsNullOrEmpty(sFechaFinal)))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" a.fch_log between @fechainicial and @fechafinal ");
          oParam.AddParameters("@fechainicial", sFechaInicial, TypeSQL.Varchar);
          oParam.AddParameters("@fechafinal", sFechaFinal, TypeSQL.Varchar);
        }

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

  }
}
