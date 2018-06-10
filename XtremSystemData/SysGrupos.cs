using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OnlineServices.Conn;

namespace OnlineServices.SystemData
{
  public class SysGrupos
  {
    DBConn.SQLParameters oParam;
    DBConn.DataTypeSQL TypeSQL = new DBConn.DataTypeSQL();

    private string pCodGrupo;
    public string CodGrupo { get { return pCodGrupo; } set { pCodGrupo = value; } }

    private string pNomGrupo;
    public string NomGrupo { get { return pNomGrupo; } set { pNomGrupo = value; } }

    private string pEstGrupo;
    public string EstGrupo { get { return pEstGrupo; } set { pEstGrupo = value; } }

    private string pAccion;
    public string Accion { get { return pAccion; } set { pAccion = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private DBConn oConn;

    public SysGrupos(ref DBConn oConn) {
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
              pCodGrupo = string.Empty;
              cSQL = new StringBuilder();
              cSQL.Append("insert into sys_grupos(nom_grupo, est_grupo ) values(");
              cSQL.Append("@nom_grupo, @est_grupo) ");
              oParam.AddParameters("@nom_grupo", pNomGrupo, TypeSQL.Varchar);
              oParam.AddParameters("@est_grupo", pEstGrupo, TypeSQL.Char);
              oConn.Insert(cSQL.ToString(), oParam);

              cSQL = new StringBuilder();
              cSQL.Append("select @@IDENTITY");
              dtData = oConn.Select(cSQL.ToString());
              if (dtData != null)
                if (dtData.Rows.Count > 0)
                  pCodGrupo = dtData.Rows[0][0].ToString();
              dtData = null;

              break;
            case "EDITAR":
              cSQL = new StringBuilder();
              cSQL.Append("update sys_grupos set ");
              if (!string.IsNullOrEmpty(pNomGrupo)) {
                cSQL.Append(" nom_grupo = @nom_grupo ");
                oParam.AddParameters("@nom_grupo", pNomGrupo, TypeSQL.Varchar);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pEstGrupo))
              {
                cSQL.Append(sComa);
                cSQL.Append(" est_grupo = @est_grupo ");
                oParam.AddParameters("@est_grupo", pEstGrupo, TypeSQL.Char);
              }
              cSQL.Append(" where cod_grupo = @cod_grupo ");
              oParam.AddParameters("@cod_grupo", pCodGrupo, TypeSQL.Numeric);
              oConn.Update(cSQL.ToString(), oParam);

              break;
            case "ELIMINAR":
              cSQL = new StringBuilder();
              cSQL.Append("delete from sys_grupos where cod_grupo = @cod_grupo ");
              oParam.AddParameters("@cod_grupo", pCodGrupo, TypeSQL.Numeric);
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
        cSQL.Append("select cod_grupo, nom_grupo, est_grupo ");
        cSQL.Append("from sys_grupos ");

        if (string.IsNullOrEmpty(pCodGrupo))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_grupo = @cod_grupo ");
          oParam.AddParameters("@cod_grupo", pCodGrupo, TypeSQL.Numeric);
        }

        if (string.IsNullOrEmpty(pNomGrupo))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" nom_grupo like %@nom_grupo% ");
          oParam.AddParameters("@nom_grupo", pNomGrupo, TypeSQL.Varchar);
        }

        if (string.IsNullOrEmpty(pEstGrupo))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" est_grupo = @est_grupo ");
          oParam.AddParameters("@est_grupo", pEstGrupo, TypeSQL.Char);
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
