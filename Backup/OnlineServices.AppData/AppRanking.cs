using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;
using System.Text;
using System.Data;
using OnlineServices.Conn;

namespace OnlineServices.AppData
{
  public class AppRanking
  {
    DBConn.SQLParameters oParam;
    DBConn.DataTypeSQL TypeSQL = new DBConn.DataTypeSQL();

    private string pCodRanking;
    public string CodRanking { get { return pCodRanking; } set { pCodRanking = value; } }

    private string pCodUsuario;
    public string CodUsuario { get { return pCodUsuario; } set { pCodUsuario = value; } }

    private string pCodCliente;
    public string CodCliente { get { return pCodCliente; } set { pCodCliente = value; } }

    private string pFchRanking;
    public string FchRanking { get { return pFchRanking; } set { pFchRanking = value; } }

    private string pNotaRanking;
    public string NotaRanking { get { return pNotaRanking; } set { pNotaRanking = value; } }

    private string pObsRanking;
    public string ObsRanking { get { return pObsRanking; } set { pObsRanking = value; } }

    private string pAccion;
    public string Accion { get { return pAccion; } set { pAccion = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private string cPath = string.Empty;
    public string Path { get { return cPath; } set { cPath = value; } }

    private DBConn oConn;

    public AppRanking() { 

    }

    public AppRanking(ref DBConn oConn)
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
              pCodRanking = string.Empty;
              cSQL = new StringBuilder();
              cSQL.Append("insert into app_ranking(cod_usuario, cod_cliente, fch_ranking, nota_ranking, obs_ranking) values(");
              cSQL.Append("@cod_usuario, @cod_cliente, @fch_ranking, @nota_ranking, @obs_ranking)");
              oParam.AddParameters("@cod_usuario", pCodUsuario, TypeSQL.Numeric);
              oParam.AddParameters("@cod_cliente", pCodCliente, TypeSQL.Numeric);
              oParam.AddParameters("@fch_ranking", pFchRanking, TypeSQL.DateTime);
              oParam.AddParameters("@nota_ranking", pNotaRanking, TypeSQL.Float);
              oParam.AddParameters("@obs_ranking", pObsRanking, TypeSQL.Text);
              oConn.Insert(cSQL.ToString(), oParam);

              cSQL = new StringBuilder();
              cSQL.Append("select @@IDENTITY");
              dtData = oConn.Select(cSQL.ToString());
              if (dtData != null)
                if (dtData.Rows.Count > 0)
                  pCodRanking = dtData.Rows[0][0].ToString();
              dtData = null;

              break;
            case "EDITAR":
              cSQL = new StringBuilder();
              cSQL.Append("update app_ranking set ");
              if (!string.IsNullOrEmpty(pFchRanking))
              {
                cSQL.Append(" fch_ranking = @fch_ranking");
                oParam.AddParameters("@fch_ranking", pFchRanking, TypeSQL.DateTime);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pNotaRanking))
              {
                cSQL.Append(sComa);
                cSQL.Append(" nota_ranking = @nota_ranking");
                oParam.AddParameters("@nota_ranking", pNotaRanking, TypeSQL.Float);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pObsRanking))
              {
                cSQL.Append(sComa);
                cSQL.Append(" obs_ranking = @obs_ranking");
                oParam.AddParameters("@obs_ranking", pObsRanking, TypeSQL.Text);
                sComa = ", ";
              }
              cSQL.Append(" where cod_ranking = @cod_ranking ");
              oParam.AddParameters("@cod_ranking", pCodRanking, TypeSQL.Numeric);
              oConn.Update(cSQL.ToString(), oParam);

              break;
            case "ELIMINAR":
              cSQL = new StringBuilder();
              cSQL.Append("delete from app_ranking where cod_ranking = @cod_ranking");
              oParam.AddParameters("@cod_ranking", pCodRanking, TypeSQL.Numeric);
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
        cSQL.Append("select cod_ranking, cod_usuario, cod_cliente, fch_ranking, nota_ranking, obs_ranking ");
        cSQL.Append("from app_ranking ");

        if (!string.IsNullOrEmpty(pCodRanking))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_ranking = @cod_ranking ");
          oParam.AddParameters("@cod_ranking", pCodRanking, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(pCodUsuario))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_usuario = @cod_usuario ");
          oParam.AddParameters("@cod_usuario", pCodUsuario, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(pCodCliente))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_cliente = @cod_cliente ");
          oParam.AddParameters("@cod_cliente", pCodCliente, TypeSQL.Numeric);
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

    public void SerializaTblRanking(ref DBConn oConn, string cPath)
    {
      if (string.IsNullOrEmpty(cPath))
        return;

      try
      {
        AppRanking oAppRanking = new AppRanking(ref oConn);
        DataTable oData = oAppRanking.Get();

        if (Directory.Exists(cPath) && oData != null)
        {
          string cFile = "Ranking.bin";
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
