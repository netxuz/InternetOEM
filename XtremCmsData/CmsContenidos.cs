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
  public class CmsContenidos
  {
    DBConn.SQLParameters oParam;
    DBConn.DataTypeSQL TypeSQL = new DBConn.DataTypeSQL();

    private string pCodContenido;
    public string CodContenido { get { return pCodContenido; } set { pCodContenido = value; } }

    private string pCodContenidoRel;
    public string CodContenidoRel { get { return pCodContenidoRel; } set { pCodContenidoRel = value; } }

    private string pCodNodo;
    public string CodNodo { get { return pCodNodo; } set { pCodNodo = value; } }

    private string pCodUsuario;
    public string CodUsuario { get { return pCodUsuario; } set { pCodUsuario = value; } }

    private string pCodUsuarioRel;
    public string CodUsuarioRel { get { return pCodUsuarioRel; } set { pCodUsuarioRel = value; } }

    private string pTituloContenido;
    public string TituloContenido { get { return pTituloContenido; } set { pTituloContenido = value; } }

    private string pTextoContenido;
    public string TextoContenido { get { return pTextoContenido; } set { pTextoContenido = value; } }

    private string pDateContenido;
    public string DateContenido { get { return pDateContenido; } set { pDateContenido = value; } }

    private string pEstContenido;
    public string EstContenido { get { return pEstContenido; } set { pEstContenido = value; } }

    private string pPrvContendio;
    public string PrvContendio { get { return pPrvContendio; } set { pPrvContendio = value; } }

    private string pDestContenido;
    public string DestContenido { get { return pDestContenido; } set { pDestContenido = value; } }

    private string pIndRss;
    public string IndRss { get { return pIndRss; } set { pIndRss = value; } }

    private string pResumenContenido;
    public string ResumenContenido { get { return pResumenContenido; } set { pResumenContenido = value; } }

    private string pIpUsuario;
    public string IpUsuario { get { return pIpUsuario; } set { pIpUsuario = value; } }

    private string pIndDenuncia;
    public string IndDenuncia { get { return pIndDenuncia; } set { pIndDenuncia = value; } }

    private string pAccion;
    public string Accion { get { return pAccion; } set { pAccion = value; } }

    private string pError = string.Empty;
    public string Error { get { return pError; } set { pError = value; } }

    private string cPath = string.Empty;
    public string Path { get { return cPath; } set { cPath = value; } }

    private DBConn oConn;

    public CmsContenidos()
    {

    }

    public CmsContenidos(ref DBConn oConn) {
      this.oConn = oConn;
    }

    public void Put()
    {
      DataTable dtData;
      oParam = new DBConn.SQLParameters(20);
      StringBuilder cSQL;
      string sComa = string.Empty;

      if (oConn.bIsOpen)
      {
        try
        {
          switch (pAccion)
          {
            case "CREAR":
              pCodContenido = string.Empty;
              cSQL = new StringBuilder();
              cSQL.Append("insert into cms_contenidos(cod_contenido_rel, cod_nodo, cod_usuario, cod_usuario_rel, titulo_contenido, texto_contenido, date_contenido, est_contenido, prv_contendio, dest_contenido, ind_rss, resumen_contenido, ip_usuario, ind_denuncia) values(");
              cSQL.Append("@cod_contenido_rel, @cod_nodo, @cod_usuario, @cod_usuario_rel, @titulo_contenido, @texto_contenido, @date_contenido, @est_contenido, @prv_contendio, @dest_contenido, @ind_rss, @resumen_contenido, @ip_usuario, @ind_denuncia) ");
              oParam.AddParameters("@cod_contenido_rel", pCodContenidoRel, TypeSQL.Numeric);
              oParam.AddParameters("@cod_nodo", pCodNodo, TypeSQL.Numeric);
              oParam.AddParameters("@cod_usuario", pCodUsuario, TypeSQL.Numeric);
              oParam.AddParameters("@cod_usuario_rel", pCodUsuarioRel, TypeSQL.Numeric);
              oParam.AddParameters("@titulo_contenido", pTituloContenido, TypeSQL.Varchar);
              oParam.AddParameters("@texto_contenido", pTextoContenido, TypeSQL.Text);
              oParam.AddParameters("@date_contenido", pDateContenido, TypeSQL.DateTime);
              oParam.AddParameters("@est_contenido", pEstContenido, TypeSQL.Char);
              oParam.AddParameters("@prv_contendio", pPrvContendio, TypeSQL.Int);
              oParam.AddParameters("@dest_contenido", pDestContenido, TypeSQL.Int);
              oParam.AddParameters("@ind_rss", pIndRss, TypeSQL.Char);
              oParam.AddParameters("@resumen_contenido", pResumenContenido, TypeSQL.Text);
              oParam.AddParameters("@ip_usuario", pIpUsuario, TypeSQL.Varchar);
              oParam.AddParameters("@ind_denuncia", pIndDenuncia, TypeSQL.Char);
              oConn.Insert(cSQL.ToString(), oParam);

              cSQL = new StringBuilder();
              cSQL.Append("select @@IDENTITY");
              dtData = oConn.Select(cSQL.ToString());
              if (dtData != null)
                if (dtData.Rows.Count > 0)
                  pCodContenido = dtData.Rows[0][0].ToString();
              dtData = null;

              break;
            case "EDITAR":
              cSQL = new StringBuilder();
              cSQL.Append("update cms_contenidos set ");
              if (!string.IsNullOrEmpty(pCodContenidoRel))
              {
                cSQL.Append(" cod_contenido_rel = @cod_contenido_rel");
                oParam.AddParameters("@cod_contenido_rel", pCodContenidoRel, TypeSQL.Numeric);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pCodNodo))
              {
                cSQL.Append(" cod_nodo = @cod_nodo");
                oParam.AddParameters("@cod_nodo", pCodNodo, TypeSQL.Numeric);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pTituloContenido))
              {
                cSQL.Append(sComa);
                cSQL.Append(" titulo_contenido = @titulo_contenido");
                oParam.AddParameters("@titulo_contenido", pTituloContenido, TypeSQL.Varchar);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pTextoContenido))
              {
                cSQL.Append(sComa);
                cSQL.Append(" texto_contenido = @texto_contenido");
                oParam.AddParameters("@texto_contenido", pTextoContenido, TypeSQL.Text);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pDateContenido))
              {
                cSQL.Append(sComa);
                cSQL.Append(" date_contenido = @date_contenido");
                oParam.AddParameters("@date_contenido", pDateContenido, TypeSQL.DateTime);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pEstContenido))
              {
                cSQL.Append(sComa);
                cSQL.Append(" est_contenido = @est_contenido");
                oParam.AddParameters("@est_contenido", pEstContenido, TypeSQL.Char);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pPrvContendio))
              {
                cSQL.Append(sComa);
                cSQL.Append(" prv_contendio = @prv_contendio");
                oParam.AddParameters("@prv_contendio", pPrvContendio, TypeSQL.Int);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pDestContenido))
              {
                cSQL.Append(sComa);
                cSQL.Append(" dest_contenido = @dest_contenido");
                oParam.AddParameters("@dest_contenido", pDestContenido, TypeSQL.Int);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pIndRss))
              {
                cSQL.Append(sComa);
                cSQL.Append(" ind_rss = @ind_rss");
                oParam.AddParameters("@ind_rss", pIndRss, TypeSQL.Char);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pResumenContenido))
              {
                cSQL.Append(sComa);
                cSQL.Append(" resumen_contenido = @resumen_contenido");
                oParam.AddParameters("@resumen_contenido", pResumenContenido, TypeSQL.Text);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pIpUsuario))
              {
                cSQL.Append(sComa);
                cSQL.Append(" ip_usuario = @ip_usuario");
                oParam.AddParameters("@ip_usuario", pIpUsuario, TypeSQL.Varchar);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pIndDenuncia))
              {
                cSQL.Append(sComa);
                cSQL.Append(" ind_denuncia = @ind_denuncia");
                oParam.AddParameters("@ind_denuncia", pIndDenuncia, TypeSQL.Char);
                sComa = ", ";
              }
              cSQL.Append(" where cod_contenido = @cod_contenido ");
              oParam.AddParameters("@cod_contenido", pCodContenido, TypeSQL.Numeric);
              oConn.Update(cSQL.ToString(), oParam);

              break;
            case "ELIMINAR":
              cSQL = new StringBuilder();
              cSQL.Append("delete from cms_contenidos where cod_contenido = @cod_contenido ");
              oParam.AddParameters("@cod_contenido", pCodContenido, TypeSQL.Numeric);
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
        cSQL.Append("select cod_contenido, cod_contenido_rel, cod_nodo, cod_usuario, cod_usuario_rel, titulo_contenido, texto_contenido, date_contenido, est_contenido, prv_contendio, dest_contenido, ind_rss, resumen_contenido, ip_usuario, ind_denuncia ");
        cSQL.Append("from cms_contenidos ");

        if (!string.IsNullOrEmpty(pCodContenido)) {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_contenido = @cod_contenido ");
          oParam.AddParameters("@cod_contenido", pCodContenido, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(pCodContenidoRel))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_contenido_rel = @cod_contenido_rel ");
          oParam.AddParameters("@cod_contenido_rel", pCodContenidoRel, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(pCodNodo))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_nodo = @cod_nodo");
          oParam.AddParameters("@cod_nodo", pCodNodo, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(pCodUsuario))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_usuario = @cod_usuario");
          oParam.AddParameters("@cod_usuario", pCodUsuario, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(pCodUsuarioRel))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_usuario_rel = @cod_usuario_rel");
          oParam.AddParameters("@cod_usuario_rel", pCodUsuarioRel, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(pTituloContenido))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" titulo_contenido = %@titulo_contenido%");
          oParam.AddParameters("@titulo_contenido", pTituloContenido, TypeSQL.Varchar);
        }
        if (!string.IsNullOrEmpty(pEstContenido))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" est_contenido = @est_contenido");
          oParam.AddParameters("@est_contenido", pEstContenido, TypeSQL.Char);
        }
        if (!string.IsNullOrEmpty(pDestContenido))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" dest_contenido = @dest_contenido");
          oParam.AddParameters("@dest_contenido", pDestContenido, TypeSQL.Int);
        }
        if (!string.IsNullOrEmpty(pIndRss))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" ind_rss = @ind_rss");
          oParam.AddParameters("@ind_rss", pIndRss, TypeSQL.Char);
        }
        if (!string.IsNullOrEmpty(pIndDenuncia))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" ind_denuncia = @ind_denuncia");
          oParam.AddParameters("@ind_denuncia", pIndDenuncia, TypeSQL.Char);
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

    public DataTable GetByUser()
    {
      oParam = new DBConn.SQLParameters(10);
      DataTable dtData;
      StringBuilder cSQL;
      string Condicion = " where ";

      if (oConn.bIsOpen)
      {
        cSQL = new StringBuilder();
        cSQL.Append("select a.cod_contenido, a.cod_contenido_rel, a.cod_nodo, a.cod_usuario, a.cod_usuario_rel, a.titulo_contenido, a.texto_contenido, a.date_contenido, a.est_contenido, a.prv_contendio, a.dest_contenido, a.ind_rss, a.resumen_contenido, a.ip_usuario, ind_denuncia");
        cSQL.Append(" (select nom_archivo from sys_archivos_usuarios where cod_usuario = a.cod_usuario and tip_archivo = 'P' ) img_perfil ");
        cSQL.Append("from cms_contenidos a ");

        if (!string.IsNullOrEmpty(pCodUsuario))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" ( a.cod_usuario = @cod_usuario or a.cod_usuario_rel = @cod_usuario_rel ) ");
          oParam.AddParameters("@cod_usuario", pCodUsuario, TypeSQL.Numeric);
          oParam.AddParameters("@cod_usuario_rel", pCodUsuarioRel, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(pEstContenido))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" a.est_contenido = @est_contenido");
          oParam.AddParameters("@est_contenido", pEstContenido, TypeSQL.Char);
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

    public string SerializaContenido(ref DBConn oConn, string cPath, string cFile)
    {
      if (string.IsNullOrEmpty(cPath))
        return string.Empty;

      try
      {
        BinaryContenido oContenido = new BinaryContenido();
        CmsContenidos oContenidos = new CmsContenidos(ref oConn);
        oContenidos.CodContenido = pCodContenido;
        DataTable oData = oContenidos.Get();

        if (oData != null)
          if (oData.Rows.Count > 0)
          {
            oContenido.CodContenido = oData.Rows[0]["cod_contenido"].ToString();
            oContenido.CodNodo = oData.Rows[0]["cod_nodo"].ToString();
            oContenido.CodUsuario = oData.Rows[0]["cod_usuario"].ToString();
            oContenido.CodUsuarioRel = oData.Rows[0]["cod_usuario_rel"].ToString();
            oContenido.TituloContenido = oData.Rows[0]["titulo_contenido"].ToString();
            oContenido.TextoContenido = oData.Rows[0]["texto_contenido"].ToString();
            oContenido.DateContenido = oData.Rows[0]["date_contenido"].ToString();
            oContenido.EstContenido = oData.Rows[0]["est_contenido"].ToString();
            oContenido.PrvContendio = oData.Rows[0]["prv_contendio"].ToString();
            oContenido.DestContenido = oData.Rows[0]["dest_contenido"].ToString();
            oContenido.IndRss = oData.Rows[0]["ind_rss"].ToString();
            oContenido.ResumenContenido = oData.Rows[0]["resumen_contenido"].ToString();
            oContenido.IpUsuario = oData.Rows[0]["ip_usuario"].ToString();
            oContenido.IndDenuncia = oData.Rows[0]["ind_denuncia"].ToString();
          }
        oData.Dispose();
        oContenidos = null;

        if (Directory.Exists(cPath) && !string.IsNullOrEmpty(cFile))
        {
          IFormatter oBinFormat = new BinaryFormatter();
          Stream oFileStream = new FileStream(cPath + cFile, FileMode.Create, FileAccess.Write);
          oBinFormat.Serialize(oFileStream, oContenido);
          oFileStream.Close();

          oFileStream = null;
          oContenido = null;
        }
        return string.Empty;
      }
      catch (Exception Ex)
      {
        return Ex.Source + " - " + Ex.Message + " - " + Ex.StackTrace;
      }
    }

    public void SerializaContenidos(ref DBConn oConn, string cPath, string cFile)
    {
      if (string.IsNullOrEmpty(cPath))
        return;

      try
      {
        CmsContenidos oContenidos = new CmsContenidos(ref oConn);
        DataTable oData = oContenidos.Get();

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

    public void SerializaTblContenidoByUser(ref DBConn oConn, string cPath, string cFile)
    {
      if (string.IsNullOrEmpty(cPath))
        return;

      try
      {
        CmsContenidos oContenidos = new CmsContenidos(ref oConn);
        oContenidos.CodUsuario = pCodUsuario;
        oContenidos.CodUsuarioRel = pCodUsuarioRel;
        DataTable oData = oContenidos.GetByUser();

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

    public void SerializaTblContenidoByNodo(ref DBConn oConn, string cPath, string cFile)
    {
      if (string.IsNullOrEmpty(cPath))
        return;

      try
      {
        CmsContenidos oContenidos = new CmsContenidos(ref oConn);
        oContenidos.CodNodo = pCodNodo;
        DataTable oData = oContenidos.Get();

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

    public BinaryContenido ClassGet()
    {
      try
      {
        if (!string.IsNullOrEmpty(pCodContenido))
        {
          StringBuilder Directorio = new StringBuilder();
          StringBuilder Archivo = new StringBuilder();

          Directorio.Append(cPath);
          Directorio.Append(@"\binary\");

          Archivo.Append("Contenido_");
          Archivo.Append(pCodContenido);
          Archivo.Append(".bin");

          if (File.Exists(Directorio.ToString() + Archivo.ToString()))
          {
            IFormatter oBinFormat = new BinaryFormatter();
            Stream oFileStream = new FileStream(Directorio.ToString() + Archivo.ToString(), FileMode.Open, FileAccess.Read, FileShare.Read);
            BinaryContenido oContenido = (BinaryContenido)oBinFormat.Deserialize(oFileStream);
            oFileStream.Close();
            return oContenido;
          }
          else
            return new BinaryContenido();
        }
        else
          return new BinaryContenido();
      }
      catch
      {
        return new BinaryContenido();
      }
    }
    
  }

  [Serializable]
  public class BinaryContenido { 
    public string CodContenido = string.Empty;
    public string CodNodo = string.Empty;
    public string CodUsuario = string.Empty;
    public string CodUsuarioRel = string.Empty;
    public string TituloContenido = string.Empty;
    public string TextoContenido = string.Empty;
    public string DateContenido = string.Empty;
    public string EstContenido = string.Empty;
    public string PrvContendio = string.Empty;
    public string DestContenido = string.Empty;
    public string IndRss = string.Empty;
    public string ResumenContenido = string.Empty;
    public string IpUsuario = string.Empty;
    public string IndDenuncia = string.Empty;
  }
}
