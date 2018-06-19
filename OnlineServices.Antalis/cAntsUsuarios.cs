using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using OnlineServices.Conn;

namespace OnlineServices.Antalis
{
  public class cAntsUsuarios
  {
    DBConn.SQLParameters oParam;
    DBConn.DataTypeSQL TypeSQL = new DBConn.DataTypeSQL();

    private string pCodUsuario;
    public string CodUsuario { get { return pCodUsuario; } set { pCodUsuario = value; } }

    private string pNomUsuario;
    public string NomUsuario { get { return pNomUsuario; } set { pNomUsuario = value; } }

    private string pApeUsuario;
    public string ApeUsuario { get { return pApeUsuario; } set { pApeUsuario = value; } }

    private string pEmlUsuario;
    public string EmlUsuario { get { return pEmlUsuario; } set { pEmlUsuario = value; } }

    private string pEstUsuario;
    public string EstUsuario { get { return pEstUsuario; } set { pEstUsuario = value; } }

    private string pCodRol;
    public string CodRol { get { return pCodRol; } set { pCodRol = value; } }

    private string pCodCentroDist;
    public string CodCentroDist { get { return pCodCentroDist; } set { pCodCentroDist = value; } }

    private string pAccion;
    public string Accion { get { return pAccion; } set { pAccion = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private string cPath = string.Empty;
    public string Path { get { return cPath; } set { cPath = value; } }

    private DBConn oConn;

    public cAntsUsuarios()
    {
    }

    public cAntsUsuarios(ref DBConn oConn)
    {
      this.oConn = oConn;
    }

    public DataTable Get()
    {
      oParam = new DBConn.SQLParameters(20);
      DataTable dtData;
      StringBuilder cSQL;
      string Condicion = " and ";

      if (oConn.bIsOpen)
      {
        cSQL = new StringBuilder();
        cSQL.Append("select cod_user, cod_tipo, nom_user, ape_user, eml_user, login_user, pwd_user, est_user, tipo_user, nkey_user, date_creacion, date_modificacion, fono_usuario, destacado_usuario, ind_validado, nota_ranking, notetarget, tipo_usuario, nkey_usuario, pwd_decrypted ");
        cSQL.Append("from sys_usuario where cod_user in( select cod_user from sys_perfiles_usuarios where cod_perfil = 7) ");

        if (!string.IsNullOrEmpty(pCodUsuario))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_user = @cod_user ");
          oParam.AddParameters("@cod_user", pCodUsuario, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(pNomUsuario))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" nom_user like '%").Append(pNomUsuario).Append("%'");
        }

        if (!string.IsNullOrEmpty(pApeUsuario))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" ape_user like %@ape_user% ");
          oParam.AddParameters("@ape_user", pApeUsuario, TypeSQL.Varchar);
        }
        
        if (!string.IsNullOrEmpty(pEstUsuario))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" est_user = @est_user ");
          oParam.AddParameters("@est_user", pEstUsuario, TypeSQL.Char);
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

    public DataTable GetRoles()
    {
      oParam = new DBConn.SQLParameters(20);
      DataTable dtData;
      StringBuilder cSQL;
      string Condicion = " where ";

      if (oConn.bIsOpen)
      {
        cSQL = new StringBuilder();
        cSQL.Append("select cod_user, cod_rol ");
        cSQL.Append("from ant_user_roles  ");

        if (!string.IsNullOrEmpty(pCodUsuario))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_user = @cod_user ");
          oParam.AddParameters("@cod_user", pCodUsuario, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(pCodRol))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_rol = @cod_rol ");
          oParam.AddParameters("@cod_rol", pCodRol, TypeSQL.Numeric);
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

    public void Put()
    {
      oParam = new DBConn.SQLParameters(20);
      StringBuilder cSQL;
      string sComa = string.Empty;

      if (oConn.bIsOpen)
      {
        try
        {
          switch (pAccion)
          {
            case "CREAR":
              cSQL = new StringBuilder();
              cSQL.Append("insert into ant_user_roles(cod_user, cod_rol) values(@cod_usuario, @cod_rol)");
              oParam.AddParameters("@cod_usuario", pCodUsuario, TypeSQL.Numeric);
              oParam.AddParameters("@cod_rol", pCodRol, TypeSQL.Numeric);
              oConn.Insert(cSQL.ToString(), oParam);

              break;
            case "ELIMINAR":
              cSQL = new StringBuilder();
              cSQL.Append("delete from ant_user_roles where cod_user = @cod_usuario");
              oParam.AddParameters("@cod_usuario", pCodUsuario, TypeSQL.Numeric);
              oConn.Delete(cSQL.ToString(), oParam);
              break;
          }
        }
        catch (Exception ex)
        {
          pError = ex.Message;
        }
      }
    }
  }
}
