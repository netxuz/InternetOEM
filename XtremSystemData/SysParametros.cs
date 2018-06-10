using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;
using System.Text;
using System.Data;
using OnlineServices.Conn;

namespace OnlineServices.SystemData
{
  public class SysParametros
  {
    DBConn.SQLParameters oParam;
    DBConn.DataTypeSQL TypeSQL = new DBConn.DataTypeSQL();

    private string pCodCodigo;
    public string CodCodigo { get { return pCodCodigo; } set { pCodCodigo = value; } }

    private string pNomParametro;
    public string NomParametro { get { return pNomParametro; } set { pNomParametro = value; } }

    private string pValorParametro;
    public string ValorParametro { get { return pValorParametro; } set { pValorParametro = value; } }

    private string pShowParametro;
    public string ShowParametro { get { return pShowParametro; } set { pShowParametro = value; } }

    private string pAccion;
    public string Accion { get { return pAccion; } set { pAccion = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private string cPath = string.Empty;
    public string Path { get { return cPath; } set { cPath = value; } }

    private DBConn oConn;

    public SysParametros()
    {
    }

    public SysParametros(ref DBConn oConn)
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

              pCodCodigo = oConn.getTableCod("sys_parametros", "cod_codigo", oConn);
              cSQL.Append("insert into sys_parametros(cod_codigo, nom_parametro, valor_parametro, show_parametro) values(");
              cSQL.Append("@cod_codigo, @nom_parametro, @valor_parametro, @show_parametro)");
              oParam.AddParameters("@cod_codigo", pCodCodigo, TypeSQL.Numeric);
              oParam.AddParameters("@nom_parametro", pNomParametro, TypeSQL.Varchar);
              oParam.AddParameters("@valor_parametro", pValorParametro, TypeSQL.Varchar);
              oParam.AddParameters("@show_parametro", pShowParametro, TypeSQL.Char);
              oConn.Insert(cSQL.ToString(), oParam);

              break;
            case "EDITAR":
              cSQL = new StringBuilder();
              cSQL.Append("update sys_parametros set ");
              if (!string.IsNullOrEmpty(pNomParametro))
              {
                cSQL.Append(sComa);
                cSQL.Append(" nom_parametro = @nom_parametro");
                oParam.AddParameters("@nom_parametro", pNomParametro, TypeSQL.Varchar);
                sComa = ", ";
              }
              /*if (!string.IsNullOrEmpty(pValorParametro))
              {*/
                cSQL.Append(sComa);
                cSQL.Append(" valor_parametro = @valor_parametro");
                oParam.AddParameters("@valor_parametro", pValorParametro, TypeSQL.Varchar);
                sComa = ", ";
              /*}*/
              if (!string.IsNullOrEmpty(pShowParametro))
              {
                cSQL.Append(sComa);
                cSQL.Append(" show_parametro = @show_parametro");
                oParam.AddParameters("@show_parametro", pShowParametro, TypeSQL.Char);
                sComa = ", ";
              }

              cSQL.Append(" where cod_codigo = @cod_codigo ");
              oParam.AddParameters("@cod_codigo", pCodCodigo, TypeSQL.Numeric);
              oConn.Update(cSQL.ToString(), oParam);

              break;
            case "ELIMINAR":
              cSQL = new StringBuilder();
              cSQL.Append("delete from sys_parametros where cod_codigo = @cod_codigo");
              oParam.AddParameters("@cod_codigo", pCodCodigo, TypeSQL.Numeric);
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
        cSQL.Append("select cod_codigo, nom_parametro, valor_parametro, show_parametro ");
        cSQL.Append("from sys_parametros ");

        if (!string.IsNullOrEmpty(pCodCodigo))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_codigo = @cod_codigo ");
          oParam.AddParameters("@cod_codigo", pCodCodigo, TypeSQL.Numeric);
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

    public void SerializaTblParametros(ref DBConn oConn, string cPath)
    {
      if (string.IsNullOrEmpty(cPath))
        return;

      try
      {
        SysParametros oParametros = new SysParametros(ref oConn);
        DataTable oData = oParametros.Get();

        if (Directory.Exists(cPath) && oData != null)
        {
          string cFile = "parametros.bin";
          IFormatter oBinFormat = new BinaryFormatter();
          Stream oFileStream = new FileStream(cPath + cFile, FileMode.Create, FileAccess.Write);
          oBinFormat.Serialize(oFileStream, oData);
          oFileStream.Close();
          oFileStream = null;
          oData.Dispose();
        }

      }
      catch (Exception Ex)
      {
        //return Ex.Source + " - " + Ex.Message + " - " + Ex.StackTrace;
      }
    }   
  }
}
