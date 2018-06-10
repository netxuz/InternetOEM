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
  public class AppBanner
  {
    DBConn.SQLParameters oParam;
    DBConn.DataTypeSQL TypeSQL = new DBConn.DataTypeSQL();

    private string pCodBanner;
    public string CodBanner { get { return pCodBanner; } set { pCodBanner = value; } }

    private string pNomBanner;
    public string NomBanner { get { return pNomBanner; } set { pNomBanner = value; } }

    private string pTipoBanner;
    public string TipoBanner { get { return pTipoBanner; } set { pTipoBanner = value; } }

    private string pEstBanner;
    public string EstBanner { get { return pEstBanner; } set { pEstBanner = value; } }

    private string pAccion;
    public string Accion { get { return pAccion; } set { pAccion = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private string cPath = string.Empty;
    public string Path { get { return cPath; } set { cPath = value; } }

    private DBConn oConn;

    public AppBanner() { 

    }

    public AppBanner(ref DBConn oConn)
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
              pCodBanner = string.Empty;
              cSQL = new StringBuilder();
              cSQL.Append("insert into app_banner(nom_banner, tipo_banner, est_banner) values(");
              cSQL.Append("@nom_banner, @tipo_banner, @est_banner)");
              oParam.AddParameters("@nom_banner", pNomBanner, TypeSQL.Varchar);
              oParam.AddParameters("@tipo_banner", pTipoBanner, TypeSQL.Char);
              oParam.AddParameters("@est_banner", pEstBanner, TypeSQL.Char);
              oConn.Insert(cSQL.ToString(), oParam);

              cSQL = new StringBuilder();
              cSQL.Append("select @@IDENTITY");
              dtData = oConn.Select(cSQL.ToString());
              if (dtData != null)
                if (dtData.Rows.Count > 0)
                  pCodBanner = dtData.Rows[0][0].ToString();
              dtData = null;

              break;
            case "EDITAR":
              cSQL = new StringBuilder();
              cSQL.Append("update app_banner set ");
              if (!string.IsNullOrEmpty(pNomBanner))
              {
                cSQL.Append(" nom_banner = @nom_banner");
                oParam.AddParameters("@nom_banner", pNomBanner, TypeSQL.Varchar);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pTipoBanner))
              {
                cSQL.Append(sComa);
                cSQL.Append(" tipo_banner = @tipo_banner");
                oParam.AddParameters("@tipo_banner", pTipoBanner, TypeSQL.Char);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pEstBanner))
              {
                cSQL.Append(sComa);
                cSQL.Append(" est_banner = @est_banner");
                oParam.AddParameters("@est_banner", pEstBanner, TypeSQL.Char);
                sComa = ", ";
              }
              cSQL.Append(" where cod_banner = @cod_banner ");
              oParam.AddParameters("@cod_banner", pCodBanner, TypeSQL.Numeric);
              oConn.Update(cSQL.ToString(), oParam);

              break;
            case "ELIMINAR":
              cSQL = new StringBuilder();
              cSQL.Append("delete from app_banner where cod_banner = @cod_banner");
              oParam.AddParameters("@cod_banner", pCodBanner, TypeSQL.Numeric);
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
        cSQL.Append("select cod_banner, nom_banner, tipo_banner, est_banner ");
        cSQL.Append("from app_banner ");

        if (!string.IsNullOrEmpty(pCodBanner))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_banner = @cod_banner ");
          oParam.AddParameters("@cod_banner", pCodBanner, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(pEstBanner))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" est_banner = @est_banner ");
          oParam.AddParameters("@est_banner", pEstBanner, TypeSQL.Char);
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

    public void SerializaBanner(ref DBConn oConn, string cPath)
    {
      if (string.IsNullOrEmpty(cPath))
        return;

      try
      {
        DataTable oData = Get();

        if (Directory.Exists(cPath) && oData != null)
        {
          string cFile = "Banner.bin";
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
