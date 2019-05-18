using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using OnlineServices.Conn;

namespace OnlineServices.SystemData
{
  public class SysClienteUsuario
  {
    DBConn.SQLParameters oParam;
    DBConn.DataTypeSQL TypeSQL = new DBConn.DataTypeSQL();

    private string pCodUsuario;
    public string CodUsuario { get { return pCodUsuario; } set { pCodUsuario = value; } }

    private string pNkeyUser;
    public string NkeyUser { get { return pNkeyUser; } set { pNkeyUser = value; } }

    private string psNombre;
    public string sNombre { get { return psNombre; } set { psNombre = value; } }

    private string pAccion;
    public string Accion { get { return pAccion; } set { pAccion = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private DBConn oConn;

    public SysClienteUsuario(ref DBConn oConn)
    {
      this.oConn = oConn;
    }

    public DataTable Get()
    {
      oParam = new DBConn.SQLParameters(20);
      DataTable dtData;
      StringBuilder cSQL;
      string Condicion = " where ";

      if (oConn.bIsOpen)
      {
        cSQL = new StringBuilder();
        cSQL.Append("select a.cod_user, a.nkey_user, (select snombre from cliente where nkey_cliente = a.nkey_user) snombre from sys_cliente_usuario a ");

        if (!string.IsNullOrEmpty(pCodUsuario))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" a.cod_user = @cod_user ");
          oParam.AddParameters("@cod_user", pCodUsuario, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(pNkeyUser))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" a.nkey_user = @nkey_user ");
          oParam.AddParameters("@nkey_user", pNkeyUser, TypeSQL.Numeric);
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

    public DataTable GetClienteNotInUsuario()
    {
      oParam = new DBConn.SQLParameters(20);
      DataTable dtData;
      StringBuilder cSQL;
      string Condicion = " and ";

      if (oConn.bIsOpen)
      {
        cSQL = new StringBuilder();
        cSQL.Append("select * from cliente where not nkey_cliente in(select nkey_user from sys_cliente_usuario where cod_user = @cod_user ) ");
        oParam.AddParameters("@cod_user", pCodUsuario, TypeSQL.Numeric);

        if (!string.IsNullOrEmpty(psNombre))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" snombre like '%' + @snombre + '%'  ");
          oParam.AddParameters("@snombre", psNombre, TypeSQL.Varchar);
        }      

        cSQL.Append(" order by snombre ");

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
      string Condicion = " where ";

      if (oConn.bIsOpen)
      {
        try
        {
          switch (pAccion)
          {
            case "CREAR":
              cSQL = new StringBuilder();
              cSQL.Append("insert into sys_cliente_usuario(cod_user, nkey_user) values(");
              cSQL.Append("@cod_user, @nkey_user)");
              oParam.AddParameters("@cod_user", pCodUsuario, TypeSQL.Numeric);
              oParam.AddParameters("@nkey_user", pNkeyUser, TypeSQL.Numeric);
              oConn.Insert(cSQL.ToString(), oParam);

              break;

            case "ELIMINAR":
              cSQL = new StringBuilder();
              cSQL.Append("delete from sys_cliente_usuario ");
              
              if (!string.IsNullOrEmpty(pCodUsuario))
              {
                cSQL.Append(Condicion);
                Condicion = " and ";
                cSQL.Append(" cod_user = @cod_user  ");
                oParam.AddParameters("@cod_user", pCodUsuario, TypeSQL.Numeric);
              }

              if (!string.IsNullOrEmpty(pNkeyUser))
              {
                cSQL.Append(Condicion);
                Condicion = " and ";
                cSQL.Append(" nkey_user = @nkey_user  ");
                oParam.AddParameters("@nkey_user", pNkeyUser, TypeSQL.Numeric);
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

  }
}
