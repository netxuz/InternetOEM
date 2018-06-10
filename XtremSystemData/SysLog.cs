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
              cSQL.Append("insert into sys_log(ip_usuario, id_usuario, fch_log, pag_log, cod_evt_log, obs_log ) values(");
              cSQL.Append("@ip_usuario, @id_usuario, @fch_log, @pag_log, @cod_evt_log, @obs_log)");
              oParam.AddParameters("@ip_usuario", pIpUsuario, TypeSQL.Varchar);
              oParam.AddParameters("@id_usuario", pIdUsuario, TypeSQL.Numeric);
              oParam.AddParameters("@fch_log", pFchLog, TypeSQL.DateTime);
              oParam.AddParameters("@pag_log", pPagLog, TypeSQL.Varchar);
              oParam.AddParameters("@cod_evt_log", pCodEvtLog, TypeSQL.Numeric);
              oParam.AddParameters("@obs_log", pObsLog, TypeSQL.Text);
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
        cSQL.Append("select cod_log, ip_usuario, id_usuario, fch_log, pag_log, cod_evt_log, obs_log ");
        cSQL.Append("from sys_log ");

        if (string.IsNullOrEmpty(pCodLog))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_log = @cod_log ");
          oParam.AddParameters("@cod_log", pCodLog, TypeSQL.Numeric);
        }

        if (string.IsNullOrEmpty(pIpUsuario))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" ip_usuario = @ip_usuario ");
          oParam.AddParameters("@ip_usuario", pIpUsuario, TypeSQL.Varchar);
        }

        if (string.IsNullOrEmpty(pIdUsuario))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" id_usuario = @id_usuario ");
          oParam.AddParameters("@id_usuario", pIdUsuario, TypeSQL.Numeric);
        }

        if (string.IsNullOrEmpty(pCodEvtLog))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_evt_log = @cod_evt_log ");
          oParam.AddParameters("@cod_evt_log", pCodEvtLog, TypeSQL.Numeric);
        }

        if (string.IsNullOrEmpty(pObsLog))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" obs_log like %@obs_log% ");
          oParam.AddParameters("@obs_log", pObsLog, TypeSQL.Text);
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
