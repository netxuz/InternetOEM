using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OnlineServices.Conn;

namespace OnlineServices.SystemData
{
  public class SyrCampoOpciones
  {
    DBConn.SQLParameters oParam;
    DBConn.DataTypeSQL TypeSQL = new DBConn.DataTypeSQL();

    private string pCodCampo;
    public string CodCampo { get { return pCodCampo; } set { pCodCampo = value; } }

    private string pCodOpcion;
    public string CodOpcion { get { return pCodOpcion; } set { pCodOpcion = value; } }

    private string pAccion;
    public string Accion { get { return pAccion; } set { pAccion = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private DBConn oConn;

    public SyrCampoOpciones(ref DBConn oConn)
    {
      this.oConn = oConn;
    }

    public void Put()
    {
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
              cSQL = new StringBuilder();

              cSQL.Append("insert into syr_campo_opciones(cod_campo, cod_opcion) values(");
              cSQL.Append("@cod_campo, @cod_opcion)");
              oParam.AddParameters("@cod_campo", pCodCampo, TypeSQL.Numeric);
              oParam.AddParameters("@cod_opcion", pCodOpcion, TypeSQL.Numeric);
              oConn.Insert(cSQL.ToString(), oParam);

              break;

            case "ELIMINAR":
              string Condicion = " where ";
              cSQL = new StringBuilder();
              cSQL.Append("delete from syr_campo_opciones");

              if (!string.IsNullOrEmpty(pCodCampo))
              {
                cSQL.Append(Condicion);
                Condicion = " and ";
                cSQL.Append(" cod_campo = @cod_campo ");
                oParam.AddParameters("@cod_campo", pCodCampo, TypeSQL.Numeric);
              }

              if (!string.IsNullOrEmpty(pCodOpcion))
              {
                cSQL.Append(Condicion);
                Condicion = " and ";
                cSQL.Append(" cod_opcion = @cod_opcion ");
                oParam.AddParameters("@cod_opcion", pCodOpcion, TypeSQL.Numeric);
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
        cSQL.Append("select cod_campo, cod_opcion ");
        cSQL.Append("from syr_campo_opciones ");

        if (!string.IsNullOrEmpty(pCodCampo))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_campo = @cod_campo ");
          oParam.AddParameters("@cod_campo", pCodCampo, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(pCodOpcion))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_opcion = @cod_opcion ");
          oParam.AddParameters("@cod_opcion", pCodOpcion, TypeSQL.Numeric);
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

    public DataTable GetOpcionByCodCampos()
    {
      oParam = new DBConn.SQLParameters(10);
      DataTable dtData;
      StringBuilder cSQL;
      string Condicion = " where ";

      if (oConn.bIsOpen)
      {
        cSQL = new StringBuilder();
        cSQL.Append("select cod_opcion, nom_opcion ");
        cSQL.Append("from sys_opciones_campo where cod_opcion in(select cod_opcion from syr_campo_opciones ");

        if (!string.IsNullOrEmpty(pCodCampo))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_campo = @cod_campo ");
          oParam.AddParameters("@cod_campo", pCodCampo, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(pCodOpcion))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_opcion = @cod_opcion ");
          oParam.AddParameters("@cod_opcion", pCodOpcion, TypeSQL.Numeric);
        }

        cSQL.Append(")");

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
