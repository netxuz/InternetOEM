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
  public class SysComentarioUsuario
  {
    DBConn.SQLParameters oParam;
    DBConn.DataTypeSQL TypeSQL = new DBConn.DataTypeSQL();

    private string pCodComentario;
    public string CodComentario { get { return pCodComentario; } set { pCodComentario = value; } }

    private string pCodUsuario;
    public string CodUsuario { get { return pCodUsuario; } set { pCodUsuario = value; } }

    private string pCodUsuarioRel;
    public string CodUsuarioRel { get { return pCodUsuarioRel; } set { pCodUsuarioRel = value; } }

    private string pComentario;
    public string Comentario { get { return pComentario; } set { pComentario = value; } }

    private string pIpUsuario;
    public string IpUsuario { get { return pIpUsuario; } set { pIpUsuario = value; } }

    private string pFecUsuario;
    public string FecUsuario { get { return pFecUsuario; } set { pFecUsuario = value; } }

    private string pAccion;
    public string Accion { get { return pAccion; } set { pAccion = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private string cPath = string.Empty;
    public string Path { get { return cPath; } set { cPath = value; } }

    private DBConn oConn;

    public SysComentarioUsuario() { 

    }

    public SysComentarioUsuario(ref DBConn oConn)
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
              pCodComentario = string.Empty;
              cSQL = new StringBuilder();
              cSQL.Append("insert into sys_comentario_usuario(cod_usuario, cod_usuario_rel, comentario, ip_usuario, fec_usuario) values(");
              cSQL.Append("@cod_usuario, @cod_usuario_rel, @comentario, @ip_usuario, @fec_usuario)");
              oParam.AddParameters("@cod_usuario", pCodUsuario, TypeSQL.Numeric);
              oParam.AddParameters("@cod_usuario_rel", pCodUsuarioRel, TypeSQL.Numeric);
              oParam.AddParameters("@comentario", pComentario, TypeSQL.Text);
              oParam.AddParameters("@ip_usuario", pIpUsuario, TypeSQL.Varchar);
              oParam.AddParameters("@fec_usuario", pFecUsuario, TypeSQL.DateTime);
              oConn.Insert(cSQL.ToString(), oParam);

              cSQL = new StringBuilder();
              cSQL.Append("select @@IDENTITY");
              dtData = oConn.Select(cSQL.ToString());
              if (dtData != null)
                if (dtData.Rows.Count > 0)
                  pCodComentario = dtData.Rows[0][0].ToString();
              dtData = null;

              break;
            case "ELIMINAR":
              string Condicion = " where ";
              oParam = new DBConn.SQLParameters(5);
              cSQL = new StringBuilder();
              cSQL.Append("delete from sys_comentario_usuario ");

              if (!string.IsNullOrEmpty(pCodComentario))
              {
                cSQL.Append(Condicion);
                Condicion = " and ";
                cSQL.Append(" cod_comentario = @cod_comentario ");
                oParam.AddParameters("@cod_comentario", pCodComentario, TypeSQL.Numeric);
              }

              if (!string.IsNullOrEmpty(pCodUsuario))
              {
                cSQL.Append(Condicion);
                Condicion = " and ";
                cSQL.Append(" cod_usuario = @cod_usuario ");
                oParam.AddParameters("@cod_usuario", pCodUsuario, TypeSQL.Numeric);
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
        cSQL.Append("select cod_comentario, cod_usuario, cod_usuario_rel, comentario, ip_usuario, fec_usuario ");
        cSQL.Append("from sys_comentario_usuario ");

        if (!string.IsNullOrEmpty(pCodComentario))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_comentario = @cod_comentario ");
          oParam.AddParameters("@cod_comentario", pCodComentario, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(pCodUsuario))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_usuario = @cod_usuario ");
          oParam.AddParameters("@cod_usuario", pCodUsuario, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(pCodUsuarioRel))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_usuario_rel = @cod_usuario_rel ");
          oParam.AddParameters("@cod_usuario_rel", pCodUsuarioRel, TypeSQL.Numeric);
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

    public void SerializaTblComentarioUsuario(ref DBConn oConn, string cPath, string cFile)
    {
      if (string.IsNullOrEmpty(cPath))
        return;

      try
      {
        SysComentarioUsuario oComentarioUsuario = new SysComentarioUsuario(ref oConn);
        oComentarioUsuario.CodUsuario = pCodUsuario;
        DataTable oData = oComentarioUsuario.Get();

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
