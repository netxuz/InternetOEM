using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using OnlineServices.Conn;

namespace OnlineServices.AppRuta
{
  public class cAppRegActividad
  {
    DBConn.SQLParameters oParam;
    DBConn.DataTypeSQL TypeSQL = new DBConn.DataTypeSQL();

    private string pCodUsuario;
    public string CodUsuario { get { return pCodUsuario; } set { pCodUsuario = value; } }

    private string pIndFecha;
    public string IndFecha { get { return pIndFecha; } set { pIndFecha = value; } }

    private string pAccion;
    public string Accion { get { return pAccion; } set { pAccion = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private DBConn oConn;

    public cAppRegActividad(ref DBConn oConn) {
      this.oConn = oConn;
    }


    public cAppRegActividad()
    {

    }

    public DataTable Get()
    {
      oParam = new DBConn.SQLParameters(10);
      DataTable dtData;
      StringBuilder cSQL;
      string Condicion = " and ";

      if (oConn.bIsOpen)
      {
        cSQL = new StringBuilder();
        cSQL.Append("select distinct(a.cod_user), (select nom_user + ' ' + ape_user from sys_usuario where cod_user = a.cod_user) nombre_usuario, ");
        cSQL.Append(" convert(varchar,fecha_actividad,103) fecha_actividad, convert(varchar,a.fecha_actividad,112) key_fecha_actividad from app_reg_actividad a where a.est_actividad = 'C' ");

        if (!string.IsNullOrEmpty(pCodUsuario))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" a.cod_user = @cod_user ");
          oParam.AddParameters("@cod_user", pCodUsuario, TypeSQL.Numeric);
        }

        cSQL.Append("group by a.cod_user, convert(varchar,a.fecha_actividad,103), convert(varchar,a.fecha_actividad,112) order by convert(varchar,a.fecha_actividad,112) ");

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

    public DataTable GetRuta()
    {
      oParam = new DBConn.SQLParameters(10);
      DataTable dtData;
      StringBuilder cSQL;

      if (oConn.bIsOpen)
      {
        cSQL = new StringBuilder();
        cSQL.Append("select ");
        cSQL.Append(" (select snombre from cliente where nkey_cliente = a.nkey_cliente) cliente, ");
        cSQL.Append(" (select snombre from deudor where nkey_deudor = a.nkey_deudor) deudor, a.* ");
        cSQL.Append(" from app_reg_actividad a where convert(varchar,a.fecha_actividad,112) = @fecha ");
        cSQL.Append(" and a.cod_user = @cod_user ");
        cSQL.Append(" order by a.fecha_actividad ");
        oParam.AddParameters("@fecha", pIndFecha, TypeSQL.Varchar);
        oParam.AddParameters("@cod_user", pCodUsuario, TypeSQL.Numeric);

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
