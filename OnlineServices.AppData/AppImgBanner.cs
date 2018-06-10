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
  public class AppImgBanner
  {
    DBConn.SQLParameters oParam;
    DBConn.DataTypeSQL TypeSQL = new DBConn.DataTypeSQL();

    private string pCodImgBanner;
    public string CodImgBanner { get { return pCodImgBanner; } set { pCodImgBanner = value; } }

    private string pCodBanner;
    public string CodBanner { get { return pCodBanner; } set { pCodBanner = value; } }

    private string pNomImgBanner;
    public string NomImgBanner { get { return pNomImgBanner; } set { pNomImgBanner = value; } }

    private string pTextImgBanner;
    public string TextImgBanner { get { return pTextImgBanner; } set { pTextImgBanner = value; } }

    private string pOrdImgBanner;
    public string OrdImgBanner { get { return pOrdImgBanner; } set { pOrdImgBanner = value; } }

    private string pUrlImgBanner;
    public string UrlImgBanner { get { return pUrlImgBanner; } set { pUrlImgBanner = value; } }

    private string pAccion;
    public string Accion { get { return pAccion; } set { pAccion = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private string cPath = string.Empty;
    public string Path { get { return cPath; } set { cPath = value; } }

    private DBConn oConn;

    public AppImgBanner() { 

    }

    public AppImgBanner(ref DBConn oConn)
    {
      this.oConn = oConn;
    }

    public void Put()
    {
      DataTable dtData;
      oParam = new DBConn.SQLParameters(10);
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
              pCodImgBanner = string.Empty;
              cSQL = new StringBuilder();
              cSQL.Append("insert into app_img_banner(cod_banner, nom_img_banner, text_img_banner, ord_img_banner, url_img_banner) values(");
              cSQL.Append("@cod_banner, @nom_img_banner, @text_img_banner, @ord_img_banner, @url_img_banner)");
              oParam.AddParameters("@cod_banner", pCodBanner, TypeSQL.Numeric);
              oParam.AddParameters("@nom_img_banner", pNomImgBanner, TypeSQL.Varchar);
              oParam.AddParameters("@text_img_banner", pTextImgBanner, TypeSQL.Text);
              oParam.AddParameters("@ord_img_banner", pOrdImgBanner, TypeSQL.Int);
              oParam.AddParameters("@url_img_banner", pUrlImgBanner, TypeSQL.Varchar);
              oConn.Insert(cSQL.ToString(), oParam);

              cSQL = new StringBuilder();
              cSQL.Append("select @@IDENTITY");
              dtData = oConn.Select(cSQL.ToString());
              if (dtData != null)
                if (dtData.Rows.Count > 0)
                  pCodImgBanner = dtData.Rows[0][0].ToString();
              dtData = null;

              break;
            case "EDITAR":
              cSQL = new StringBuilder();
              cSQL.Append("update app_img_banner set ");
              if (!string.IsNullOrEmpty(pTextImgBanner))
              {
                cSQL.Append(sComa);
                cSQL.Append(" text_img_banner = @text_img_banner");
                oParam.AddParameters("@text_img_banner", pTextImgBanner, TypeSQL.Text);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pOrdImgBanner))
              {
                cSQL.Append(sComa);
                cSQL.Append(" ord_img_banner = @ord_img_banner");
                oParam.AddParameters("@ord_img_banner", pOrdImgBanner, TypeSQL.Int);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pUrlImgBanner))
              {
                cSQL.Append(sComa);
                cSQL.Append(" url_img_banner = @url_img_banner");
                oParam.AddParameters("@url_img_banner", pUrlImgBanner, TypeSQL.Varchar);
                sComa = ", ";
              }

              cSQL.Append(" where cod_img_banner = @cod_img_banner ");
              oParam.AddParameters("@cod_img_banner", pCodImgBanner, TypeSQL.Numeric);
              oConn.Update(cSQL.ToString(), oParam);

              break;
            case "ELIMINAR":
              cSQL = new StringBuilder();
              cSQL.Append("delete from app_img_banner ");

              if (!string.IsNullOrEmpty(pCodBanner))
              {
                cSQL.Append(Condicion);
                Condicion = " and ";
                cSQL.Append(" cod_banner = @cod_banner ");
                oParam.AddParameters("@cod_banner", pCodBanner, TypeSQL.Numeric);
              }

              if (!string.IsNullOrEmpty(pCodImgBanner))
              {
                cSQL.Append(Condicion);
                Condicion = " and ";
                cSQL.Append(" cod_img_banner = @cod_img_banner ");
                oParam.AddParameters("@cod_img_banner", pCodImgBanner, TypeSQL.Numeric);
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
        cSQL.Append("select cod_img_banner, cod_banner, nom_img_banner, text_img_banner, ord_img_banner, url_img_banner ");
        cSQL.Append("from app_img_banner ");

        if (!string.IsNullOrEmpty(pCodBanner))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_banner = @cod_banner ");
          oParam.AddParameters("@cod_banner", pCodBanner, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(pCodImgBanner))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_img_banner = @cod_img_banner ");
          oParam.AddParameters("@cod_img_banner", pCodImgBanner, TypeSQL.Numeric);
        }

        cSQL.Append(" order by ord_img_banner");

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

    public void SerializaImgBanner(ref DBConn oConn, string cPath)
    {
      if (string.IsNullOrEmpty(cPath))
        return;

      try
      {
        DataTable oData = Get();

        if (Directory.Exists(cPath) && oData != null)
        {
          string cFile = "ImgBanner.bin";
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

