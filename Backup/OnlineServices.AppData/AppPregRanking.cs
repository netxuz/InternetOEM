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
  public class AppPregRanking
  {
    DBConn.SQLParameters oParam;
    DBConn.DataTypeSQL TypeSQL = new DBConn.DataTypeSQL();

    private string pCodPregRanking;
    public string CodPregRanking { get { return pCodPregRanking; } set { pCodPregRanking = value; } }

    private string pPregRanking;
    public string PregRanking { get { return pPregRanking; } set { pPregRanking = value; } }

    private string pEstPregRanking;
    public string EstPregRanking { get { return pEstPregRanking; } set { pEstPregRanking = value; } }

    private string pAccion;
    public string Accion { get { return pAccion; } set { pAccion = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private string cPath = string.Empty;
    public string Path { get { return cPath; } set { cPath = value; } }

    private DBConn oConn;

    public AppPregRanking() { 

    }

    public AppPregRanking(ref DBConn oConn) {
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
              pCodPregRanking = string.Empty;
              cSQL = new StringBuilder();
              cSQL.Append("insert into app_preg_ranking(preg_ranking, est_preg_ranking) values(");
              cSQL.Append("@preg_ranking, @est_preg_ranking)");
              oParam.AddParameters("@preg_ranking", pPregRanking, TypeSQL.Varchar);
              oParam.AddParameters("@est_preg_ranking", pEstPregRanking, TypeSQL.Char);
              oConn.Insert(cSQL.ToString(), oParam);

              cSQL = new StringBuilder();
              cSQL.Append("select @@IDENTITY");
              dtData = oConn.Select(cSQL.ToString());
              if (dtData != null)
                if (dtData.Rows.Count > 0)
                  pCodPregRanking = dtData.Rows[0][0].ToString();
              dtData = null;

              break;
            case "EDITAR":
              cSQL = new StringBuilder();
              cSQL.Append("update app_preg_ranking set ");
              if (!string.IsNullOrEmpty(pPregRanking))
              {
                cSQL.Append(" preg_ranking = @preg_ranking");
                oParam.AddParameters("@preg_ranking", pPregRanking, TypeSQL.Varchar);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pEstPregRanking))
              {
                cSQL.Append(" est_preg_ranking = @est_preg_ranking");
                oParam.AddParameters("@est_preg_ranking", pEstPregRanking, TypeSQL.Char);
                sComa = ", ";
              }

              cSQL.Append(" where cod_preg_ranking = @cod_preg_ranking ");
              oParam.AddParameters("@cod_preg_ranking", pCodPregRanking, TypeSQL.Numeric);
              oConn.Update(cSQL.ToString(), oParam);

              break;
            case "ELIMINAR":
              cSQL = new StringBuilder();
              cSQL.Append("delete from app_preg_ranking where cod_preg_ranking = @cod_preg_ranking");
              oParam.AddParameters("@cod_preg_ranking", pCodPregRanking, TypeSQL.Numeric);
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
        cSQL.Append("select cod_preg_ranking, preg_ranking, est_preg_ranking ");
        cSQL.Append("from app_preg_ranking ");

        if (!string.IsNullOrEmpty(pCodPregRanking))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_preg_ranking = @cod_preg_ranking ");
          oParam.AddParameters("@cod_preg_ranking", pCodPregRanking, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(pEstPregRanking))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" est_preg_ranking = @est_preg_ranking ");
          oParam.AddParameters("@est_preg_ranking", pEstPregRanking, TypeSQL.Char);
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

    public void SerializaTblPregRanking(ref DBConn oConn, string cPath)
    {
      if (string.IsNullOrEmpty(cPath))
        return;

      try
      {
        AppPregRanking oAppPregRanking = new AppPregRanking(ref oConn);
        DataTable oData = oAppPregRanking.Get();

        if (Directory.Exists(cPath) && oData != null)
        {
          string cFile = "PregRanking.bin";
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
