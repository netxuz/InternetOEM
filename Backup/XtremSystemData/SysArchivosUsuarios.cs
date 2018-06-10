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
  public class SysArchivosUsuarios
  {
    DBConn.SQLParameters oParam;
    DBConn.DataTypeSQL TypeSQL = new DBConn.DataTypeSQL();

    private string pCodArchivo;
    public string CodArchivo { get { return pCodArchivo; } set { pCodArchivo = value; } }

    private string pCodUsuario;
    public string CodUsuario { get { return pCodUsuario; } set { pCodUsuario = value; } }

    private string pNomArchivo;
    public string NomArchivo { get { return pNomArchivo; } set { pNomArchivo = value; } }

    private string pDateArchivo;
    public string DateArchivo { get { return pDateArchivo; } set { pDateArchivo = value; } }

    private string pTipArchivo;
    public string TipArchivo { get { return pTipArchivo; } set { pTipArchivo = value; } }

    private string pImgArchivo;
    public string ImgArchivo { get { return pImgArchivo; } set { pImgArchivo = value; } }

    private string pImgProfileArchivo;
    public string ImgProfileArchivo { get { return pImgProfileArchivo; } set { pImgProfileArchivo = value; } }

    private string pAccion;
    public string Accion { get { return pAccion; } set { pAccion = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private string cPath = string.Empty;
    public string Path { get { return cPath; } set { cPath = value; } }

    private DBConn oConn;

    public SysArchivosUsuarios(ref DBConn oConn){
      this.oConn = oConn;
    }

    public void Put()
    {
      DataTable dtData;
      oParam = new DBConn.SQLParameters(10);
      StringBuilder cSQL;
      string sComa = ", ";
      string Condicion = " where ";

      if (oConn.bIsOpen)
      {
        try
        {
          switch (pAccion)
          {
            case "CREAR":
              pCodArchivo = string.Empty;
              cSQL = new StringBuilder();
              cSQL.Append("insert into sys_archivos_usuarios(cod_usuario, nom_archivo, date_archivo, tip_archivo) values(");
              cSQL.Append("@cod_usuario, @nom_archivo, @date_archivo, @tip_archivo)");
              oParam.AddParameters("@cod_usuario", pCodUsuario, TypeSQL.Numeric);
              oParam.AddParameters("@nom_archivo", pNomArchivo, TypeSQL.Varchar );
              oParam.AddParameters("@date_archivo", pDateArchivo, TypeSQL.DateTime);
              oParam.AddParameters("@tip_archivo", pTipArchivo, TypeSQL.Char);
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
              cSQL.Append("update sys_archivos_usuarios set ");
              
              cSQL.Append(" tip_archivo = @tip_archivo");
              oParam.AddParameters("@tip_archivo", pTipArchivo, TypeSQL.Char);

              cSQL.Append(sComa);
              cSQL.Append(" img_profile_archivo = @img_profile_archivo");
              oParam.AddParameters("@img_profile_archivo", pImgProfileArchivo, TypeSQL.Varchar);             

              if (!string.IsNullOrEmpty(pCodArchivo))
              {
                cSQL.Append(Condicion);
                Condicion = " and ";
                cSQL.Append(" cod_archivo = @cod_archivo ");
                oParam.AddParameters("@cod_archivo", pCodArchivo, TypeSQL.Numeric);
              }

              if (!string.IsNullOrEmpty(pCodUsuario))
              {
                cSQL.Append(Condicion);
                Condicion = " and ";
                cSQL.Append(" cod_usuario = @cod_usuario ");
                oParam.AddParameters("@cod_usuario", pCodUsuario, TypeSQL.Numeric);
              }
              oConn.Update(cSQL.ToString(), oParam);
              break;
            case "ELIMINAR":
              cSQL = new StringBuilder();
              cSQL.Append("delete from sys_archivos_usuarios where cod_archivo = @cod_archivo");
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
        cSQL.Append("select cod_archivo, cod_usuario, nom_archivo, date_archivo, tip_archivo, img_profile_archivo ");
        cSQL.Append("from sys_archivos_usuarios ");

        if (!string.IsNullOrEmpty(pCodArchivo))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_archivo = @cod_archivo ");
          oParam.AddParameters("@cod_archivo", pCodArchivo, TypeSQL.Numeric);
        }
        
        if (!string.IsNullOrEmpty(pCodUsuario))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_usuario = @cod_usuario ");
          oParam.AddParameters("@cod_usuario", pCodUsuario, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(pTipArchivo))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" tip_archivo = @tip_archivo ");
          oParam.AddParameters("@tip_archivo", pTipArchivo, TypeSQL.Char);
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

    public BinaryArchivo ClassGet()
    {
      try
      {
        if (!string.IsNullOrEmpty(pCodUsuario))
        {
          StringBuilder Directorio = new StringBuilder();
          StringBuilder Archivo = new StringBuilder();

          Directorio.Append(cPath);
          Directorio.Append(@"\binary\");

          Archivo.Append("UserArchivo_").Append(pCodUsuario).Append(".bin");

          if (File.Exists(Directorio.ToString() + Archivo.ToString()))
          {
            IFormatter oBinFormat = new BinaryFormatter();
            Stream oFileStream = new FileStream(Directorio.ToString() + Archivo.ToString(), FileMode.Open, FileAccess.Read, FileShare.Read);
            BinaryArchivo oArchivos = (BinaryArchivo)oBinFormat.Deserialize(oFileStream);
            oFileStream.Close();
            return oArchivos;
          }
          else
            return new BinaryArchivo();
        }
        else
          return new BinaryArchivo();
      }
      catch
      {
        return new BinaryArchivo();
      }
    }

    public void SerializaTblUserArchivo(ref DBConn oConn, string cPath, string cFile)
    {
      if (string.IsNullOrEmpty(cPath))
        return;

      try
      {
        SysArchivosUsuarios oArchivosUsuarios = new SysArchivosUsuarios(ref oConn);
        oArchivosUsuarios.CodUsuario = pCodUsuario;
        DataTable oData = oArchivosUsuarios.Get();

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

  [Serializable]
  public class BinaryArchivo
  {
    public string CodArchivo = string.Empty;
    public string CodUsuario = string.Empty;
    public string NomArchivo = string.Empty;
    public string DateArchivo = string.Empty;
    public string TipArchivo = string.Empty;
    public string ImgArchivo = string.Empty;
    public string ImgProfileArchivo = string.Empty;

  }
}
