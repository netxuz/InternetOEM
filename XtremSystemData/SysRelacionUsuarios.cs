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
  public class SysRelacionUsuarios
  {
    DBConn.SQLParameters oParam;
    DBConn.DataTypeSQL TypeSQL = new DBConn.DataTypeSQL();

    private string pCodUsuario;
    public string CodUsuario { get { return pCodUsuario; } set { pCodUsuario = value; } }

    private string pCodUsuarioRel;
    public string CodUsuarioRel { get { return pCodUsuarioRel; } set { pCodUsuarioRel = value; } }

    private string pEstRelacion;
    public string EstRelacion { get { return pEstRelacion; } set { pEstRelacion = value; } }

    private string pAccion;
    public string Accion { get { return pAccion; } set { pAccion = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private string cPath = string.Empty;
    public string Path { get { return cPath; } set { cPath = value; } }

    private DBConn oConn;

    public SysRelacionUsuarios() { 

    }

    public SysRelacionUsuarios(ref DBConn oConn) {
      this.oConn = oConn;
    }

    public void Put(){
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

              cSQL.Append("insert into sys_relacion_usuarios(cod_usuario, cod_usuario_rel, est_relacion) values(");
              cSQL.Append("@cod_usuario, @cod_usuario_rel, @est_relacion)");
              oParam.AddParameters("@cod_usuario", pCodUsuario, TypeSQL.Numeric);
              oParam.AddParameters("@cod_usuario_rel", pCodUsuarioRel, TypeSQL.Numeric);
              oParam.AddParameters("@est_relacion", pEstRelacion, TypeSQL.Char);
              oConn.Insert(cSQL.ToString(), oParam);

              break;
            case "EDITAR":
              cSQL = new StringBuilder();
              cSQL.Append("update sys_relacion_usuarios set ");
              if (!string.IsNullOrEmpty(pEstRelacion))
              {
                cSQL.Append(" est_relacion = @est_relacion");
                oParam.AddParameters("@est_relacion", pEstRelacion, TypeSQL.Char);
                sComa = ", ";
              }
              cSQL.Append(" where cod_usuario = @cod_usuario and cod_usuario_rel = @cod_usuario_rel ");
              oParam.AddParameters("@cod_usuario", pCodUsuario, TypeSQL.Numeric);
              oParam.AddParameters("@cod_usuario_rel", pCodUsuarioRel, TypeSQL.Numeric);

              oConn.Update(cSQL.ToString(), oParam);

              break;
            case "ELIMINAR":
              string Condicion = " where ";

              cSQL = new StringBuilder();
              cSQL.Append("delete from sys_relacion_usuarios");

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

    public DataTable Get() { 
      oParam = new DBConn.SQLParameters(5);
      DataTable dtData;
      StringBuilder cSQL;
      string Condicion = " where ";

      if (oConn.bIsOpen)
      {
        cSQL = new StringBuilder();
        cSQL.Append("select cod_usuario, cod_usuario_rel, est_relacion from sys_relacion_usuarios ");

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

        if (!string.IsNullOrEmpty(pEstRelacion))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" est_relacion = @est_relacion ");
          oParam.AddParameters("@est_relacion", pEstRelacion, TypeSQL.Char);
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

    public DataTable GetFriendRequest()
    {
      oParam = new DBConn.SQLParameters(5);
      DataTable dtData;
      StringBuilder cSQL;
      string Condicion = " and ";

      if (oConn.bIsOpen)
      {
        cSQL = new StringBuilder();
        cSQL.Append("select a.cod_usuario, a.cod_usuario_rel, a.est_relacion, ");
        cSQL.Append(" (select isnull(nom_usuario,'') + ' ' + isnull(ape_usuario,'') from sys_usuario where cod_usuario = a.cod_usuario_rel ) usuario_relacion, ");
        cSQL.Append(" (select nom_archivo from sys_archivos_usuarios where cod_usuario = a.cod_usuario_rel and tip_archivo = 'P') img_perfil ");
        cSQL.Append("from sys_relacion_usuarios a ");

        cSQL.Append("where a.cod_usuario = @cod_usuario ");
        oParam.AddParameters("@cod_usuario", pCodUsuario, TypeSQL.Numeric);

        if (!string.IsNullOrEmpty(pCodUsuarioRel))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" a.cod_usuario_rel = @cod_usuario_rel ");
          oParam.AddParameters("@cod_usuario_rel", pCodUsuarioRel, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(pEstRelacion))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" a.est_relacion = @est_relacion ");
          oParam.AddParameters("@est_relacion", pEstRelacion, TypeSQL.Char);
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

    public void SerializaTblRelacionUsuarios(ref DBConn oConn, string cPath, string cFile)
    {
      if (string.IsNullOrEmpty(cPath))
        return;

      try
      {
        SysRelacionUsuarios oRelacionUsuarios = new SysRelacionUsuarios(ref oConn);
        oRelacionUsuarios.CodUsuario = pCodUsuario;
        DataTable oData = oRelacionUsuarios.GetFriendRequest();

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
