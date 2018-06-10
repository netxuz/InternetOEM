using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;
using System.Text;
using System.Data;
using OnlineServices.Conn;

namespace OnlineServices.CmsData
{
  public class CmsTemplate
  {
    DBConn.SQLParameters oParam;
    DBConn.DataTypeSQL TypeSQL = new DBConn.DataTypeSQL();

    private string pCodTemplate;
    public string CodTemplate { get { return pCodTemplate; } set { pCodTemplate = value; } }

    private string pNomTemplate;
    public string NomTemplate { get { return pNomTemplate; } set { pNomTemplate = value; } }

    private string pTextoTemplate;
    public string TextoTemplate { get { return pTextoTemplate; } set { pTextoTemplate = value; } }

    private string pArchTemplate;
    public string ArchTemplate { get { return pArchTemplate; } set { pArchTemplate = value; } }

    private string pEstTemplate;
    public string EstTemplate { get { return pEstTemplate; } set { pEstTemplate = value; } }

    private string pAccion;
    public string Accion { get { return pAccion; } set { pAccion = value; } }

    private string pError = string.Empty;
    public string Error { get { return pError; } set { pError = value; } }

    private string cPath = string.Empty;
    public string Path { get { return cPath; } set { cPath = value; } }

    private DBConn oConn;

    public CmsTemplate() { 

    }

    public CmsTemplate(ref DBConn oConn) {
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
              pCodTemplate = string.Empty;
              cSQL = new StringBuilder();
              cSQL.Append("insert into cms_template(nom_template, texto_template, arch_template, est_template) values(");
              cSQL.Append("@nom_template, @texto_template, @arch_template, @est_template)");
              oParam.AddParameters("@nom_template", pNomTemplate, TypeSQL.Varchar);
              oParam.AddParameters("@texto_template", pTextoTemplate, TypeSQL.Text);
              oParam.AddParameters("@arch_template", pArchTemplate, TypeSQL.Varchar);
              oParam.AddParameters("@est_template", pEstTemplate, TypeSQL.Char);
              oConn.Insert(cSQL.ToString(), oParam);

              cSQL = new StringBuilder();
              cSQL.Append("select @@IDENTITY");
              dtData = oConn.Select(cSQL.ToString());
              if (dtData != null)
                if (dtData.Rows.Count > 0)
                  pCodTemplate = dtData.Rows[0][0].ToString();
              dtData = null;

              break;
            case "EDITAR":
              cSQL = new StringBuilder();
              cSQL.Append("update cms_template set ");
              if (!string.IsNullOrEmpty(pNomTemplate))
              {
                cSQL.Append(" nom_template = @nom_template");
                oParam.AddParameters("@nom_template", pNomTemplate, TypeSQL.Varchar);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pTextoTemplate))
              {
                cSQL.Append(sComa);
                cSQL.Append(" texto_template = @texto_template");
                oParam.AddParameters("@texto_template", pTextoTemplate, TypeSQL.Text);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pArchTemplate))
              {
                cSQL.Append(sComa);
                cSQL.Append(" arch_template = @arch_template");
                oParam.AddParameters("@arch_template", pArchTemplate, TypeSQL.Varchar);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pEstTemplate))
              {
                cSQL.Append(sComa);
                cSQL.Append(" est_template = @est_template");
                oParam.AddParameters("@est_template", pEstTemplate, TypeSQL.Char);
                sComa = ", ";
              }
              cSQL.Append(" where cod_template = @cod_template");
              oParam.AddParameters("@cod_template", pCodTemplate, TypeSQL.Numeric);

              oConn.Update(cSQL.ToString(), oParam);
              break;
            case "ELIMINAR":
              cSQL = new StringBuilder();
              cSQL.Append("update cms_nodos set cod_template = null where cod_template = @cod_template");
              oParam.AddParameters("@cod_template", pCodTemplate, TypeSQL.Numeric);
              oConn.Delete(cSQL.ToString(), oParam);

              cSQL = new StringBuilder();
              cSQL.Append("delete from cms_template where cod_template = @cod_template");
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
        cSQL.Append("select cod_template, nom_template, texto_template, arch_template, est_template, convert(bigint, timestamp) timestamp ");
        cSQL.Append("from cms_template ");

        if (!string.IsNullOrEmpty(pCodTemplate))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_template = @cod_template ");
          oParam.AddParameters("@cod_template", pCodTemplate, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(pNomTemplate))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" nom_template like %@nom_template% ");
          oParam.AddParameters("@nom_template", pNomTemplate, TypeSQL.Varchar);
        }

        if (!string.IsNullOrEmpty(pEstTemplate))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" est_template = @est_template ");
          oParam.AddParameters("@est_template", pEstTemplate, TypeSQL.Char);
        }
        cSQL.Append(" order by nom_template ");

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

    public string SerializaTemplate(ref DBConn oConn, string cPath, string cFile)
    {
      if (string.IsNullOrEmpty(cPath))
        return string.Empty;

      try
      {
        BinaryTemplate oTemplate = new BinaryTemplate();

        CmsTemplate oCmsTemplate = new CmsTemplate(ref oConn);
        oCmsTemplate.CodTemplate = pCodTemplate;
        DataTable oData = oCmsTemplate.Get();
        if (oData != null)
          if (oData.Rows.Count > 0)
          {
            oTemplate.CodTemplate = oData.Rows[0]["cod_template"].ToString();
            oTemplate.NomTemplate = oData.Rows[0]["nom_template"].ToString();
            oTemplate.TextoTemplate = oData.Rows[0]["texto_template"].ToString();
            oTemplate.ArchTemplate = oData.Rows[0]["arch_template"].ToString();
            oTemplate.EstTemplate = oData.Rows[0]["est_template"].ToString();
          }
        oData.Dispose();

        if (Directory.Exists(cPath) && !string.IsNullOrEmpty(cFile))
        {
          IFormatter oBinFormat = new BinaryFormatter();
          Stream oFileStream = new FileStream(cPath + cFile, FileMode.Create, FileAccess.Write);
          oBinFormat.Serialize(oFileStream, oTemplate);
          oFileStream.Close();

          oFileStream = null;
          oTemplate = null;
        }
        return string.Empty;
      }
      catch (Exception Ex)
      {
        return Ex.Source + " - " + Ex.Message + " - " + Ex.StackTrace;
      }
    }

    public BinaryTemplate ClassGet()
    {
      try
      {
        if (!string.IsNullOrEmpty(pCodTemplate))
        {
          StringBuilder Directorio = new StringBuilder();
          StringBuilder Archivo = new StringBuilder();

          Directorio.Append(cPath);
          Directorio.Append(@"\binary\");

          Archivo.Append("template_");
          Archivo.Append(pCodTemplate);
          Archivo.Append(".bin");

          if (File.Exists(Directorio.ToString() + Archivo.ToString()))
          {
            IFormatter oBinFormat = new BinaryFormatter();
            Stream oFileStream = new FileStream(Directorio.ToString() + Archivo.ToString(), FileMode.Open, FileAccess.Read, FileShare.Read);
            BinaryTemplate oTemplate = (BinaryTemplate)oBinFormat.Deserialize(oFileStream);
            oFileStream.Close();
            return oTemplate;
          }
          else
            return new BinaryTemplate();
        }
        else
          return new BinaryTemplate();
      }
      catch
      {
        return new BinaryTemplate();
      }
    }
  
  }

  [Serializable]
  public class BinaryTemplate
  {
    public string CodTemplate = string.Empty;
    public string NomTemplate = string.Empty;
    public string TextoTemplate = string.Empty;
    public string ArchTemplate = string.Empty;
    public string EstTemplate = string.Empty;
  }
}
