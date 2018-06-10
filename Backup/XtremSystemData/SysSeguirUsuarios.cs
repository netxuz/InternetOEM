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
  public class SysSeguirUsuarios
  {
    DBConn.SQLParameters oParam;
    DBConn.DataTypeSQL TypeSQL = new DBConn.DataTypeSQL();

    private string pCodUsuario;
    public string CodUsuario { get { return pCodUsuario; } set { pCodUsuario = value; } }

    private string pCodSegUsuario;
    public string CodSegUsuario { get { return pCodSegUsuario; } set { pCodSegUsuario = value; } }

    private string pAccion;
    public string Accion { get { return pAccion; } set { pAccion = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private string cPath = string.Empty;
    public string Path { get { return cPath; } set { cPath = value; } }

    private DBConn oConn;

    public SysSeguirUsuarios() { 

    }

    public SysSeguirUsuarios(ref DBConn oConn)
    {
      this.oConn = oConn;
    }

    public void Put()
    {
      oParam = new DBConn.SQLParameters(5);
      StringBuilder cSQL;
      string sComa = string.Empty;

      if (oConn.bIsOpen)
      {
        try
        {
          switch (pAccion)
          {
            case "CREAR":
              cSQL = new StringBuilder();

              cSQL.Append("insert into sys_seguir_usuarios(cod_usuario, cod_seg_usuario) values(");
              cSQL.Append("@cod_usuario, @cod_seg_usuario)");
              oParam.AddParameters("@cod_usuario", pCodUsuario, TypeSQL.Numeric);
              oParam.AddParameters("@cod_seg_usuario", pCodSegUsuario, TypeSQL.Numeric);
              oConn.Insert(cSQL.ToString(), oParam);

              break;
            case "ELIMINAR":
              string Condicion = " where ";

              cSQL = new StringBuilder();
              cSQL.Append("delete from sys_seguir_usuarios");

              if (!string.IsNullOrEmpty(pCodUsuario))
              {
                cSQL.Append(Condicion);
                Condicion = " and ";
                cSQL.Append(" cod_usuario = @cod_usuario ");
                oParam.AddParameters("@cod_usuario", pCodUsuario, TypeSQL.Numeric);
              }

              if (!string.IsNullOrEmpty(pCodSegUsuario))
              {
                cSQL.Append(Condicion);
                Condicion = " and ";
                cSQL.Append(" cod_seg_usuario = @cod_seg_usuario ");
                oParam.AddParameters("@cod_seg_usuario", pCodSegUsuario, TypeSQL.Numeric);
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
      oParam = new DBConn.SQLParameters(5);
      DataTable dtData;
      StringBuilder cSQL;
      string Condicion = " where ";

      if (oConn.bIsOpen)
      {
        cSQL = new StringBuilder();
        cSQL.Append("select cod_usuario, cod_seg_usuario from sys_seguir_usuarios ");

        if (!string.IsNullOrEmpty(pCodUsuario))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_usuario = @cod_usuario ");
          oParam.AddParameters("@cod_usuario", pCodUsuario, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(pCodSegUsuario))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_seg_usuario = @cod_seg_usuario ");
          oParam.AddParameters("@cod_seg_usuario", pCodSegUsuario, TypeSQL.Numeric);
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

    public DataTable GetUserFollow()
    {
      oParam = new DBConn.SQLParameters(5);
      DataTable dtData;
      StringBuilder cSQL;
      string Condicion = " and ";

      if (oConn.bIsOpen)
      {
        cSQL = new StringBuilder();
        cSQL.Append("select a.cod_usuario, a.cod_seg_usuario, ");
        cSQL.Append(" (select isnull(nom_usuario,'') + ' ' + isnull(ape_usuario,'') from sys_usuario where cod_usuario = a.cod_seg_usuario ) usuario_follow ");
        cSQL.Append("from sys_seguir_usuarios a ");
        cSQL.Append("where a.cod_usuario = @cod_usuario ");
        oParam.AddParameters("@cod_usuario", pCodUsuario, TypeSQL.Numeric);

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

    public DataTable GetUserFollowMe()
    {
      oParam = new DBConn.SQLParameters(5);
      DataTable dtData;
      StringBuilder cSQL;
      string Condicion = " and ";

      if (oConn.bIsOpen)
      {
        cSQL = new StringBuilder();
        cSQL.Append("select a.cod_usuario, a.cod_seg_usuario, ");
        cSQL.Append(" (select isnull(nom_usuario,'') + ' ' + isnull(ape_usuario,'') from sys_usuario where cod_usuario = a.cod_usuario ) usuario_followme ");
        cSQL.Append("from sys_seguir_usuarios a ");
        cSQL.Append("where a.cod_seg_usuario = @cod_seg_usuario ");
        oParam.AddParameters("@cod_seg_usuario", pCodSegUsuario, TypeSQL.Numeric);

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

    public void SerializaUserFollow(ref DBConn oConn, string cPath, string cFile)
    {
      if (string.IsNullOrEmpty(cPath))
        return;

      try
      {
        SysSeguirUsuarios oSeguirUsuarios = new SysSeguirUsuarios(ref oConn);
        oSeguirUsuarios.CodUsuario = pCodUsuario;
        DataTable oData = oSeguirUsuarios.GetUserFollow();

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

    public void SerializaUserFollowMe(ref DBConn oConn, string cPath, string cFile)
    {
      if (string.IsNullOrEmpty(cPath))
        return;

      try
      {
        SysSeguirUsuarios oSeguirUsuarios = new SysSeguirUsuarios(ref oConn);
        oSeguirUsuarios.CodSegUsuario = pCodSegUsuario;
        DataTable oData = oSeguirUsuarios.GetUserFollowMe();

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
