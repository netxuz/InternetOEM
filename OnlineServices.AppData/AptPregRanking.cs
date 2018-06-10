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
  public class AptPregRanking
  {
    DBConn.SQLParameters oParam;
    DBConn.DataTypeSQL TypeSQL = new DBConn.DataTypeSQL();

    private string pCodRanking;
    public string CodRanking { get { return pCodRanking; } set { pCodRanking = value; } }

    private string pCodPregRanking;
    public string CodPregRanking { get { return pCodPregRanking; } set { pCodPregRanking = value; } }

    private string pNotaPregRanking;
    public string NotaPregRanking { get { return pNotaPregRanking; } set { pNotaPregRanking = value; } }

    private string pAccion;
    public string Accion { get { return pAccion; } set { pAccion = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private string cPath = string.Empty;
    public string Path { get { return cPath; } set { cPath = value; } }

    private DBConn oConn;

    public AptPregRanking() { 

    }

    public AptPregRanking(ref DBConn oConn)
    {
      this.oConn = oConn;
    }

    public void Put()
    {
      DataTable dtData;
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
              cSQL.Append("insert into apt_preg_ranking(cod_ranking, cod_preg_ranking, nota_preg_ranking) values(");
              cSQL.Append("@cod_ranking, @cod_preg_ranking, @nota_preg_ranking)");
              oParam.AddParameters("@cod_ranking", pCodRanking, TypeSQL.Numeric);
              oParam.AddParameters("@cod_preg_ranking", pCodPregRanking, TypeSQL.Numeric);
              oParam.AddParameters("@nota_preg_ranking", pNotaPregRanking, TypeSQL.Numeric);
              oConn.Insert(cSQL.ToString(), oParam);

              break;
            case "ELIMINAR":
              cSQL = new StringBuilder();
              cSQL.Append("delete from apt_preg_ranking ");

              if (!string.IsNullOrEmpty(pCodRanking))
              {
                cSQL.Append(Condicion);
                Condicion = " and ";
                cSQL.Append(" cod_ranking = @cod_ranking ");
                oParam.AddParameters("@cod_ranking", pCodRanking, TypeSQL.Numeric);
              }

              if (!string.IsNullOrEmpty(pCodPregRanking))
              {
                cSQL.Append(Condicion);
                Condicion = " and ";
                cSQL.Append(" cod_preg_ranking = @cod_preg_ranking ");
                oParam.AddParameters("@cod_preg_ranking", pCodPregRanking, TypeSQL.Numeric);
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
        cSQL.Append("select cod_ranking, cod_preg_ranking, nota_preg_ranking from apt_preg_ranking ");

        if (!string.IsNullOrEmpty(pCodRanking))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_ranking = @cod_ranking ");
          oParam.AddParameters("@cod_ranking", pCodRanking, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(pCodPregRanking))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_preg_ranking = @cod_preg_ranking ");
          oParam.AddParameters("@cod_preg_ranking", pCodPregRanking, TypeSQL.Numeric);
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
        DataTable oData = Get();

        if (Directory.Exists(cPath) && oData != null)
        {
          StringBuilder cFile = new StringBuilder();
          cFile.Append(cPath).Append("AptPregRanking_").Append(pCodRanking).Append(".bin");
          IFormatter oBinFormat = new BinaryFormatter();
          Stream oFileStream = new FileStream(cFile.ToString(), FileMode.Create, FileAccess.Write);
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
