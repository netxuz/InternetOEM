using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using OnlineServices.Conn;

namespace OnlineServices.Antalis
{
  public class cAntCentrosDistribucion
  {
    DBConn.SQLParameters oParam;
    DBConn.DataTypeSQL TypeSQL = new DBConn.DataTypeSQL();

    private string pCodUsuario;
    public string CodUsuario { get { return pCodUsuario; } set { pCodUsuario = value; } }

    private string pCodTblDatos;
    public string CodTblDatos { get { return pCodTblDatos; } set { pCodTblDatos = value; } }

    private string pDescripcion;
    public string Descripcion { get { return pDescripcion; } set { pDescripcion = value; } }

    private string pCodCentroDist;
    public string CodCentroDist { get { return pCodCentroDist; } set { pCodCentroDist = value; } }

    private string pAccion;
    public string Accion { get { return pAccion; } set { pAccion = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private DBConn oConn;

    public cAntCentrosDistribucion() {

    }

    public cAntCentrosDistribucion(ref DBConn oConn) {
      this.oConn = oConn;
    }

    public DataTable Get()
    {
      oParam = new DBConn.SQLParameters(3);
      DataTable dtData;
      StringBuilder cSQL;
      string Condicion = " and ";

      if (oConn.bIsOpen)
      {
        cSQL = new StringBuilder();
        cSQL.Append("select codigo, valor, descripcion, muestra, valor_alf from Tabla_datos where codigo = @codtbldatos ");
        cSQL.Append("and not valor in(select cod_centrodist from ant_user_cd where cod_user = @cod_user ) ");
        oParam.AddParameters("@codtbldatos", pCodTblDatos, TypeSQL.Varchar);
        oParam.AddParameters("@cod_user", pCodUsuario, TypeSQL.Numeric);

        if (!string.IsNullOrEmpty(pDescripcion))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" descripcion like '%' + @descripcion + '%'  ");
          oParam.AddParameters("@descripcion", pDescripcion, TypeSQL.Varchar);
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

    public DataTable GetByCod() {
      oParam = new DBConn.SQLParameters(20);
      DataTable dtData;
      StringBuilder cSQL;

      if (oConn.bIsOpen) {
        cSQL = new StringBuilder();
        cSQL.Append(" select descripcion from Tabla_datos where codigo = 'CENDIS' and valor = @cod_centrodist  ");
        oParam.AddParameters("@cod_centrodist", pCodCentroDist, TypeSQL.Numeric);
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
        

    public DataTable GetCentrosDistByUsuario()
    {
      oParam = new DBConn.SQLParameters(20);
      DataTable dtData;
      StringBuilder cSQL;
      string Condicion = " where ";

      if (oConn.bIsOpen)
      {
        cSQL = new StringBuilder();
        cSQL.Append("select a.cod_user, a.cod_centrodist, (select descripcion from Tabla_datos where codigo = 'CENDIS' and valor = a.cod_centrodist ) descripcion ");
        cSQL.Append("from ant_user_cd  a ");

        if (!string.IsNullOrEmpty(pCodUsuario))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" a.cod_user = @cod_user ");
          oParam.AddParameters("@cod_user", pCodUsuario, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(pCodCentroDist))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" a.cod_centrodist = @cod_centrodist ");
          oParam.AddParameters("@cod_centrodist", pCodCentroDist, TypeSQL.Numeric);
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
      oParam = new DBConn.SQLParameters(2);
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
              cSQL.Append("insert into ant_user_cd(cod_user, cod_centrodist) values(@cod_usuario, @cod_centrodist)");
              oParam.AddParameters("@cod_usuario", pCodUsuario, TypeSQL.Numeric);
              oParam.AddParameters("@cod_centrodist", pCodCentroDist, TypeSQL.Numeric);
              oConn.Insert(cSQL.ToString(), oParam);

              break;
            case "ELIMINAR":
              cSQL = new StringBuilder();
              cSQL.Append("delete from ant_user_cd where cod_user = @cod_usuario and cod_centrodist = @cod_centrodist ");
              oParam.AddParameters("@cod_usuario", pCodUsuario, TypeSQL.Numeric);
              oParam.AddParameters("@cod_centrodist", pCodCentroDist, TypeSQL.Numeric);
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
