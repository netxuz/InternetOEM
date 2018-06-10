using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OnlineServices.Conn;

namespace OnlineServices.SystemData
{
  public class SysPerfiles
  {
    DBConn.SQLParameters oParam;
    DBConn.DataTypeSQL TypeSQL = new DBConn.DataTypeSQL();

    private string pCodPerfil;
    public string CodPerfil { get { return pCodPerfil; } set { pCodPerfil = value; } }

    private string pNomPerfil;
    public string NomPerfil { get { return pNomPerfil; } set { pNomPerfil = value; } }

    private string pEstPerfil;
    public string EstPerfil { get { return pEstPerfil; } set { pEstPerfil = value; } }

    private string pAccion;
    public string Accion { get { return pAccion; } set { pAccion = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private DBConn oConn;

    public SysPerfiles(ref DBConn oConn)
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
              pCodPerfil = string.Empty;
              cSQL = new StringBuilder();
              cSQL.Append("insert into sys_perfiles( nom_perfil, est_perfil ) values(");
              cSQL.Append("@nom_perfil, @est_perfil)");
              oParam.AddParameters("@nom_perfil", pNomPerfil, TypeSQL.Varchar);
              oParam.AddParameters("@est_perfil", pEstPerfil, TypeSQL.Char);
              oConn.Insert(cSQL.ToString(), oParam);

              cSQL = new StringBuilder();
              cSQL.Append("select @@IDENTITY");
              dtData = oConn.Select(cSQL.ToString());
              if (dtData != null)
                if (dtData.Rows.Count > 0)
                  pCodPerfil = dtData.Rows[0][0].ToString();
              dtData = null;

              break;
            case "EDITAR":
              cSQL = new StringBuilder();
              cSQL.Append("update sys_perfiles set ");
              if (!string.IsNullOrEmpty(pNomPerfil))
              {
                cSQL.Append(" nom_perfil = @nom_perfil");
                oParam.AddParameters("@nom_perfil", pNomPerfil, TypeSQL.Varchar);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pEstPerfil))
              {
                cSQL.Append(sComa);
                cSQL.Append(" est_perfil = @est_perfil ");
                oParam.AddParameters("@est_perfil", pEstPerfil, TypeSQL.Char);
              }
              cSQL.Append(" where cod_perfil = @cod_perfil");
              oParam.AddParameters("@cod_perfil", pCodPerfil, TypeSQL.Numeric);
              oConn.Update(cSQL.ToString(), oParam);

              break;
            case "ELIMINAR":
              cSQL = new StringBuilder();
              cSQL.Append("delete from sys_perfiles where cod_perfil = @cod_perfil ");
              oParam.AddParameters("@cod_perfil", pCodPerfil, TypeSQL.Numeric);
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
        cSQL.Append("select cod_perfil, nom_perfil, est_perfil ");
        cSQL.Append("from sys_perfiles ");

        if (string.IsNullOrEmpty(pCodPerfil))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_perfil = @cod_perfil ");
          oParam.AddParameters("@cod_perfil", pCodPerfil, TypeSQL.Numeric);
        }

        if (string.IsNullOrEmpty(pNomPerfil))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" nom_perfil like %nom_perfil% ");
          oParam.AddParameters("@nom_perfil", pNomPerfil, TypeSQL.Varchar);
        }

        if (string.IsNullOrEmpty(pEstPerfil))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" est_perfil = @est_perfil ");
          oParam.AddParameters("@est_perfil", pEstPerfil, TypeSQL.Char);
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
