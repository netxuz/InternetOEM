using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OnlineServices.Conn;

namespace OnlineServices.CmsData
{
  public class CmsArchivos
  {
    DBConn.SQLParameters oParam;
    DBConn.DataTypeSQL TypeSQL = new DBConn.DataTypeSQL();

    private string pCodArchivo;
    public string CodArchivo { get { return pCodArchivo; } set { pCodArchivo = value; } }

    private string pCodArchivoRel;
    public string CodArchivoRel { get { return pCodArchivoRel; } set { pCodArchivoRel = value; } }

    private string pCodContenido;
    public string CodContenido { get { return pCodContenido; } set { pCodContenido = value; } }

    private string pNomArchivo;
    public string NomArchivo { get { return pNomArchivo; } set { pNomArchivo = value; } }

    private string pDesArchivo;
    public string DesArchivo { get { return pDesArchivo; } set { pDesArchivo = value; } }

    private string pDateArchivo;
    public string DateArchivo { get { return pDateArchivo; } set { pDateArchivo = value; } }

    private string pExtArchivo;
    public string ExtArchivo { get { return pExtArchivo; } set { pExtArchivo = value; } }

    private string pImgArchivo;
    public string ImgArchivo { get { return pImgArchivo; } set { pImgArchivo = value; } }

    private string pAccion;
    public string Accion { get { return pAccion; } set { pAccion = value; } }

    private string pError = string.Empty;
    public string Error { get { return pError; } set { pError = value; } }

    private string cPath = string.Empty;
    public string Path { get { return cPath; } set { cPath = value; } }

    private DBConn oConn;

    public CmsArchivos() { 

    }

    public CmsArchivos(ref DBConn oConn)
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
              pCodArchivo = string.Empty;
              cSQL = new StringBuilder();
              cSQL.Append("insert into cms_archivos(cod_archivo, cod_archivo_rel, cod_contenido, nom_archivo, des_archivo, date_archivo, ext_archivo, img_archivo) values(");
              cSQL.Append("@cod_archivo, @cod_archivo_rel, @cod_contenido, @nom_archivo, @des_archivo, @date_archivo, @ext_archivo, @img_archivo) ");
              oParam.AddParameters("@cod_archivo_rel", pCodArchivoRel, TypeSQL.Numeric);
              oParam.AddParameters("@cod_contenido", pCodContenido, TypeSQL.Numeric);
              oParam.AddParameters("@nom_archivo", pNomArchivo, TypeSQL.Varchar);
              oParam.AddParameters("@des_archivo", pDesArchivo, TypeSQL.Text);
              oParam.AddParameters("@date_archivo", pDateArchivo, TypeSQL.DateTime);
              oParam.AddParameters("@ext_archivo", pExtArchivo, TypeSQL.Varchar);
              oParam.AddParameters("@img_archivo", pImgArchivo, TypeSQL.Varchar);
              oConn.Insert(cSQL.ToString(), oParam);

              cSQL = new StringBuilder();
              cSQL.Append("select @@IDENTITY");
              dtData = oConn.Select(cSQL.ToString());
              if (dtData != null)
                if (dtData.Rows.Count > 0)
                  pCodArchivo = dtData.Rows[0][0].ToString();
              dtData = null;

              break;
            case "EDITAR":
              cSQL = new StringBuilder();
              cSQL.Append("update cms_archivos set ");
              if (!string.IsNullOrEmpty(pCodArchivoRel))
              {
                cSQL.Append(" cod_archivo_rel = @cod_archivo_rel");
                oParam.AddParameters("@cod_archivo_rel", pCodArchivoRel, TypeSQL.Numeric);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pNomArchivo))
              {
                cSQL.Append(sComa);
                cSQL.Append(" nom_archivo = @nom_archivo");
                oParam.AddParameters("@nom_archivo", pNomArchivo, TypeSQL.Varchar);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pDesArchivo))
              {
                cSQL.Append(sComa);
                cSQL.Append(" des_archivo = @des_archivo");
                oParam.AddParameters("@des_archivo", pDesArchivo, TypeSQL.Text);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pDateArchivo))
              {
                cSQL.Append(sComa);
                cSQL.Append(" date_archivo = @date_archivo");
                oParam.AddParameters("@date_archivo", pDateArchivo, TypeSQL.DateTime);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pExtArchivo))
              {
                cSQL.Append(sComa);
                cSQL.Append(" ext_archivo = @ext_archivo");
                oParam.AddParameters("@ext_archivo", pExtArchivo, TypeSQL.Varchar);
                sComa = ", ";
              }
              cSQL.Append(" where cod_archivo = @cod_archivo ");
              oParam.AddParameters("@cod_archivo", pCodArchivo, TypeSQL.Numeric);
              oConn.Update(cSQL.ToString(), oParam);

              break;
            case "ELIMINAR":
              cSQL = new StringBuilder();
              cSQL.Append("delete from cms_archivos where cod_archivo = @cod_archivo ");
              oParam.AddParameters("@cod_archivo", pCodArchivo, TypeSQL.Numeric);
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
        cSQL.Append("select cod_archivo, cod_archivo_rel, cod_contenido, nom_archivo, des_archivo, date_archivo, ext_archivo, img_archivo ");
        cSQL.Append("from cms_archivos ");

        if (!string.IsNullOrEmpty(pCodArchivo))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_archivo = @cod_archivo ");
          oParam.AddParameters("@cod_archivo", pCodArchivo, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(pCodArchivoRel))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_archivo_rel = @cod_archivo_rel");
          oParam.AddParameters("@cod_archivo_rel", pCodArchivoRel, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(pCodContenido))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_contenido = @cod_contenido");
          oParam.AddParameters("@cod_contenido", pCodContenido, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(pNomArchivo))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" nom_archivo = %@nom_archivo%");
          oParam.AddParameters("@nom_archivo", pNomArchivo, TypeSQL.Varchar);
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

    public void SerializaTblArchivo(ref DBConn oConn, string cPath, string cFile)
    {
      if (string.IsNullOrEmpty(cPath))
        return;

      try
      {
        CmsArchivos oArchivos = new CmsArchivos(ref oConn);
        oArchivos.CodContenido = pCodContenido;
        DataTable oData = oArchivos.Get();

        if (Directory.Exists(cPath) && !string.IsNullOrEmpty(cFile) && oData != null)
        {
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
