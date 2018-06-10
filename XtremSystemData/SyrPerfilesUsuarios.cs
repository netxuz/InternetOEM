using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OnlineServices.Conn;

namespace OnlineServices.SystemData
{
  public class SyrPerfilesUsuarios
  {
    DBConn.SQLParameters oParam;
    DBConn.DataTypeSQL TypeSQL = new DBConn.DataTypeSQL();

    private string pCodPerfil;
    public string CodPerfil { get { return pCodPerfil; } set { pCodPerfil = value; } }

    private string pCodUsuario;
    public string CodUsuario { get { return pCodUsuario; } set { pCodUsuario = value; } }

    private string pNomApeUsuario;
    public string NomApeUsuario { get { return pNomApeUsuario; } set { pNomApeUsuario = value; } }

    private bool pNOTIn;
    public bool NOTIn { get { return pNOTIn; } set { pNOTIn = value; } }

    private string pAccion;
    public string Accion { get { return pAccion; } set { pAccion = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private DBConn oConn;

    public SyrPerfilesUsuarios(ref DBConn oConn)
    {
      this.oConn = oConn;
    }

    public void Put()
    {
      oParam = new DBConn.SQLParameters(10);
      StringBuilder cSQL;
      string Condicion = " where ";

      if (oConn.bIsOpen)
      {
        try
        {
          switch (pAccion)
          {
            case "CREAR":
              cSQL = new StringBuilder();

              cSQL.Append("insert into sys_perfiles_usuarios(cod_user, cod_perfil) values(");
              cSQL.Append("@cod_user, @cod_perfil)");
              oParam.AddParameters("@cod_user", pCodUsuario, TypeSQL.Numeric);
              oParam.AddParameters("@cod_perfil", pCodPerfil, TypeSQL.Numeric);
              oConn.Insert(cSQL.ToString(), oParam);

              break;
            case "ELIMINAR":
              cSQL = new StringBuilder();
              cSQL.Append("delete from sys_perfiles_usuarios  ");
              if (!string.IsNullOrEmpty(pCodUsuario))
              {
                cSQL.Append(Condicion);
                Condicion = " and ";
                cSQL.Append(" cod_user = @cod_user");
                oParam.AddParameters("@cod_user", pCodUsuario, TypeSQL.Numeric);
              }
              if (!string.IsNullOrEmpty(pCodPerfil))
              {
                cSQL.Append(Condicion);
                Condicion = " and ";
                cSQL.Append(" cod_perfil = @cod_perfil");
                oParam.AddParameters("@cod_perfil", pCodPerfil, TypeSQL.Numeric);
              }

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
        cSQL.Append("select cod_user, cod_perfil from sys_perfiles_usuarios  ");

        if (!string.IsNullOrEmpty(pCodUsuario))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_user = @cod_user ");
          oParam.AddParameters("@cod_user", pCodUsuario, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(pCodPerfil))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_perfil = @cod_perfil ");
          oParam.AddParameters("@cod_perfil", pCodPerfil, TypeSQL.Numeric);
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

    public DataTable GetUsuariosByPerfil()
    {
      oParam = new DBConn.SQLParameters(10);
      DataTable dtData;
      StringBuilder cSQL;
      string Condicion = " where ";
      string CondicionInorNOtIn = " where ";

      if (!pNOTIn)
        CondicionInorNOtIn = " where not ";

      if (oConn.bIsOpen)
      {
        cSQL = new StringBuilder();
        cSQL.Append("select cod_user, (isnull(nom_user,'') + ' ' + isnull(ape_user,'')) nombre_usuario from sys_usuario ");
        cSQL.Append(CondicionInorNOtIn);
        cSQL.Append(" cod_user in(select cod_user from sys_perfiles_usuarios ");

        if (!string.IsNullOrEmpty(pCodPerfil))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_perfil = @cod_perfil ");
          oParam.AddParameters("@cod_perfil", pCodPerfil, TypeSQL.Numeric);
        }

        cSQL.Append(") ");

        if (!string.IsNullOrEmpty(NomApeUsuario))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" nom_user + ' ' + ape_user like '%' + @nombre + '%'  ");
          oParam.AddParameters("@nombre", NomApeUsuario, TypeSQL.Varchar);
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
