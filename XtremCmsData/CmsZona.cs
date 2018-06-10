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
  public class CmsZona
  {
    DBConn.SQLParameters oParam;
    DBConn.DataTypeSQL TypeSQL = new DBConn.DataTypeSQL();

    private string pCodZona;
    public string CodZona { get { return pCodZona; } set { pCodZona = value; } }

    private string pNomZona;
    public string NomZona { get { return pNomZona; } set { pNomZona = value; } }

    private string pTextoZona;
    public string TextoZona { get { return pTextoZona; } set { pTextoZona = value; } }

    private string pEstZona;
    public string EstZona { get { return pEstZona; } set { pEstZona = value; } }

    private string pIndDespCont;
    public string IndDespCont { get { return pIndDespCont; } set { pIndDespCont = value; } }

    private string pAccion;
    public string Accion { get { return pAccion; } set { pAccion = value; } }

    private string pError = string.Empty;
    public string Error { get { return pError; } set { pError = value; } }

    private string cPath = string.Empty;
    public string Path { get { return cPath; } set { cPath = value; } }

    private DBConn oConn;

    public CmsZona()
    {

    }

    public CmsZona(ref DBConn oConn)
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
              pCodZona = string.Empty;
              cSQL = new StringBuilder();
              cSQL.Append("insert into cms_zona(nom_zona, texto_zona, est_zona, ind_desp_cont) values(");
              cSQL.Append("@nom_zona, @texto_zona, @est_zona, @ind_desp_cont)");
              oParam.AddParameters("@nom_zona", pNomZona, TypeSQL.Varchar);
              oParam.AddParameters("@texto_zona", pTextoZona, TypeSQL.Text);
              oParam.AddParameters("@est_zona", pEstZona, TypeSQL.Char);
              oParam.AddParameters("@ind_desp_cont", pIndDespCont, TypeSQL.Char);
              oConn.Insert(cSQL.ToString(), oParam);

              cSQL = new StringBuilder();
              cSQL.Append("select @@IDENTITY");
              dtData = oConn.Select(cSQL.ToString());
              if (dtData != null)
                if (dtData.Rows.Count > 0)
                  pCodZona = dtData.Rows[0][0].ToString();
              dtData = null;

              break;
            case "EDITAR":
              cSQL = new StringBuilder();
              cSQL.Append("update cms_zona set ");
              if (!string.IsNullOrEmpty(pNomZona))
              {
                cSQL.Append(" nom_zona = @nom_zona");
                oParam.AddParameters("@nom_zona", pNomZona, TypeSQL.Varchar);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pTextoZona))
              {
                cSQL.Append(sComa);
                cSQL.Append(" texto_zona = @texto_zona");
                oParam.AddParameters("@texto_zona", pTextoZona, TypeSQL.Text);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pEstZona))
              {
                cSQL.Append(sComa);
                cSQL.Append(" est_zona = @est_zona");
                oParam.AddParameters("@est_zona", pEstZona, TypeSQL.Char);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pIndDespCont)) {
                cSQL.Append(sComa);
                cSQL.Append(" ind_desp_cont = @ind_desp_cont ");
                oParam.AddParameters("@ind_desp_cont", pIndDespCont, TypeSQL.Char);
                sComa = ", ";
              }
              cSQL.Append(" where cod_zona = @cod_zona");
              oParam.AddParameters("@cod_zona", pCodZona, TypeSQL.Numeric);

              oConn.Update(cSQL.ToString(), oParam);
              break;
            case "ELIMINAR":
              cSQL = new StringBuilder();
              cSQL.Append("delete from cms_zona where cod_zona = @cod_zona");
              oParam.AddParameters("@cod_zona", pCodZona, TypeSQL.Numeric);

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
      DataTable dtData;
      StringBuilder cSQL;
      oParam = new DBConn.SQLParameters(10);
      string Condicion = " where ";

      if (oConn.bIsOpen)
      {
        cSQL = new StringBuilder();
        cSQL.Append("select cod_zona, nom_zona, texto_zona, est_zona, ind_desp_cont ");
        cSQL.Append("from cms_zona ");

        if (!string.IsNullOrEmpty(pCodZona))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_zona = @cod_zona ");
          oParam.AddParameters("@cod_zona", pCodZona, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(pNomZona))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" nom_zona like %@nom_zona% ");
          oParam.AddParameters("@nom_zona", pNomZona, TypeSQL.Varchar);
        }

        if (!string.IsNullOrEmpty(pEstZona))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" est_zona = @est_zona ");
          oParam.AddParameters("@est_zona", pEstZona, TypeSQL.Char);
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

    public string SerializaZona(ref DBConn oConn, string cPath, string cFile)
    {
      if (string.IsNullOrEmpty(cPath))
        return string.Empty;

      try
      {
        BinaryZona oBinZona = new BinaryZona();
        CmsZona oZona = new CmsZona(ref oConn);
        oZona.CodZona = pCodZona;
        DataTable oData = oZona.Get();
        if (oData != null)
          if (oData.Rows.Count > 0)
          {
            oBinZona.CodZona = oData.Rows[0]["cod_zona"].ToString();
            oBinZona.NomZona = oData.Rows[0]["nom_zona"].ToString();
            oBinZona.TextoZona = oData.Rows[0]["texto_zona"].ToString();
            oBinZona.EstZona = oData.Rows[0]["est_zona"].ToString();
            oBinZona.IndDespCont = oData.Rows[0]["ind_desp_cont"].ToString();
          }
        oData.Dispose();
        oZona = null;

        if (Directory.Exists(cPath) && !string.IsNullOrEmpty(cFile))
        {
          IFormatter oBinFormat = new BinaryFormatter();
          Stream oFileStream = new FileStream(cPath + cFile, FileMode.Create, FileAccess.Write);
          oBinFormat.Serialize(oFileStream, oBinZona);
          oFileStream.Close();

          oFileStream = null;
          oBinFormat = null;
        }
        return string.Empty;
      }
      catch (Exception Ex)
      {
        return Ex.Source + " - " + Ex.Message + " - " + Ex.StackTrace;
      }
    }

    public BinaryZona ClassGet()
    {
      try
      {
        if (!string.IsNullOrEmpty(pCodZona))
        {
          StringBuilder Directorio = new StringBuilder();
          StringBuilder Archivo = new StringBuilder();

          Directorio.Append(cPath);
          Directorio.Append(@"\binary\");

          Archivo.Append("zona_");
          Archivo.Append(pCodZona);
          Archivo.Append(".bin");

          if (File.Exists(Directorio.ToString() + Archivo.ToString()))
          {
            IFormatter oBinFormat = new BinaryFormatter();
            Stream oFileStream = new FileStream(Directorio.ToString() + Archivo.ToString(), FileMode.Open, FileAccess.Read, FileShare.Read);
            BinaryZona oZona = (BinaryZona)oBinFormat.Deserialize(oFileStream);
            oFileStream.Close();
            return oZona;
          }
          else
            return new BinaryZona();
        }
        else
          return new BinaryZona();
      }
      catch
      {
        return new BinaryZona();
      }
    }

  }

  [Serializable]
  public class BinaryZona
  {
    public string CodZona = string.Empty;
    public string NomZona = string.Empty;
    public string TextoZona = string.Empty;
    public string EstZona = string.Empty;
    public string IndDespCont = string.Empty;
  }
}
