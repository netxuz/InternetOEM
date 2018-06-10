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
  public class SysUsuario
  {
    DBConn.SQLParameters oParam;
    DBConn.DataTypeSQL TypeSQL = new DBConn.DataTypeSQL();

    private string pCodUsuario;
    public string CodUsuario { get { return pCodUsuario; } set { pCodUsuario = value; } }

    private string pCodTipo;
    public string CodTipo { get { return pCodTipo; } set { pCodTipo = value; } }

    private string pNomUsuario;
    public string NomUsuario { get { return pNomUsuario; } set { pNomUsuario = value; } }

    private string pApeUsuario;
    public string ApeUsuario { get { return pApeUsuario; } set { pApeUsuario = value; } }

    private string pEmlUsuario;
    public string EmlUsuario { get { return pEmlUsuario; } set { pEmlUsuario = value; } }

    private string pLoginUsuario;
    public string LoginUsuario { get { return pLoginUsuario; } set { pLoginUsuario = value; } }

    private string pPwdUsuario;
    public string PwdUsuario { get { return pPwdUsuario; } set { pPwdUsuario = value; } }

    private string pEstUsuario;
    public string EstUsuario { get { return pEstUsuario; } set { pEstUsuario = value; } }

    private string pFonoUsuario;
    public string FonoUsuario { get { return pFonoUsuario; } set { pFonoUsuario = value; } }

    private string pDestacadoUsuario;
    public string DestacadoUsuario { get { return pDestacadoUsuario; } set { pDestacadoUsuario = value; } }

    private string pIndValidado;
    public string IndValidado { get { return pIndValidado; } set { pIndValidado = value; } }

    private string pNotaRanking;
    public string NotaRanking { get { return pNotaRanking; } set { pNotaRanking = value; } }

    private bool bNotInUsr;
    public bool NotInUsr { get { return bNotInUsr; } set { bNotInUsr = value; } }

    private string pNotInCodUsr;
    public string NotInCodUsr { get { return pNotInCodUsr; } set { pNotInCodUsr = value; } }

    private string pNotEtarget;
    public string NotEtarget { get { return pNotEtarget; } set { pNotEtarget = value; } }

    private string pAccion;
    public string Accion { get { return pAccion; } set { pAccion = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private string cPath = string.Empty;
    public string Path { get { return cPath; } set { cPath = value; } }

    private DBConn oConn;

    public SysUsuario() { 
    }

    public SysUsuario(ref DBConn oConn) {
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
              pCodUsuario = string.Empty;
              cSQL = new StringBuilder();
              cSQL.Append("insert into sys_usuario(cod_tipo, nom_usuario, ape_usuario, eml_usuario, login_usuario, pwd_usuario, est_usuario, fono_usuario, destacado_usuario, ind_validado, nota_ranking, notetarget) values(");
              cSQL.Append("@cod_tipo, @nom_usuario, @ape_usuario, @eml_usuario, @login_usuario, @pwd_usuario, @est_usuario, @fono_usuario, @destacado_usuario, @ind_validado, @nota_ranking, @notetarget)");
              oParam.AddParameters("@cod_tipo", pCodTipo, TypeSQL.Numeric);
              oParam.AddParameters("@nom_usuario", pNomUsuario, TypeSQL.Varchar);
              oParam.AddParameters("@ape_usuario", pApeUsuario, TypeSQL.Varchar);
              oParam.AddParameters("@eml_usuario", pEmlUsuario, TypeSQL.Varchar);
              oParam.AddParameters("@login_usuario", pLoginUsuario, TypeSQL.Varchar);
              oParam.AddParameters("@pwd_usuario", pPwdUsuario, TypeSQL.Varchar);
              oParam.AddParameters("@est_usuario", pEstUsuario, TypeSQL.Char);
              oParam.AddParameters("@fono_usuario", pFonoUsuario, TypeSQL.Varchar);
              oParam.AddParameters("@destacado_usuario", pDestacadoUsuario, TypeSQL.Char);
              oParam.AddParameters("@ind_validado", pIndValidado, TypeSQL.Char);
              oParam.AddParameters("@nota_ranking", pNotaRanking, TypeSQL.Float);
              oParam.AddParameters("@notetarget", pNotEtarget, TypeSQL.Char);
              oConn.Insert(cSQL.ToString(), oParam);

              cSQL = new StringBuilder();
              cSQL.Append("select @@IDENTITY");
              dtData = oConn.Select(cSQL.ToString());
              if (dtData != null)
                if (dtData.Rows.Count > 0)
                  pCodUsuario = dtData.Rows[0][0].ToString();
              dtData = null;

              break;
            case "EDITAR":
              cSQL = new StringBuilder();
              cSQL.Append("update sys_usuario set ");
              if (!string.IsNullOrEmpty(pCodTipo))
              {
                cSQL.Append(" cod_tipo = @cod_tipo");
                oParam.AddParameters("@cod_tipo", pCodTipo, TypeSQL.Numeric);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pNomUsuario)) {
                cSQL.Append(sComa);
                cSQL.Append(" nom_usuario = @nom_usuario");
                oParam.AddParameters("@nom_usuario", pNomUsuario, TypeSQL.Varchar);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pApeUsuario))
              {
                cSQL.Append(sComa);
                cSQL.Append(" ape_usuario = @ape_usuario");
                oParam.AddParameters("@ape_usuario", pApeUsuario, TypeSQL.Varchar);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pEmlUsuario))
              {
                cSQL.Append(sComa);
                cSQL.Append(" eml_usuario = @eml_usuario");
                oParam.AddParameters("@eml_usuario", pEmlUsuario, TypeSQL.Varchar);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pLoginUsuario))
              {
                cSQL.Append(sComa);
                cSQL.Append(" login_usuario = @login_usuario");
                oParam.AddParameters("@login_usuario", pLoginUsuario, TypeSQL.Varchar);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pPwdUsuario))
              {
                cSQL.Append(sComa);
                cSQL.Append(" pwd_usuario = @pwd_usuario");
                oParam.AddParameters("@pwd_usuario", pPwdUsuario, TypeSQL.Varchar);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pEstUsuario))
              {
                cSQL.Append(sComa);
                cSQL.Append(" est_usuario = @est_usuario");
                oParam.AddParameters("@est_usuario", pEstUsuario, TypeSQL.Char);
              }
              if (!string.IsNullOrEmpty(pFonoUsuario))
              {
                cSQL.Append(sComa);
                cSQL.Append(" fono_usuario = @fono_usuario");
                oParam.AddParameters("@fono_usuario", pFonoUsuario, TypeSQL.Varchar);
              }
              if (!string.IsNullOrEmpty(pDestacadoUsuario))
              {
                cSQL.Append(sComa);
                cSQL.Append(" destacado_usuario = @destacado_usuario");
                oParam.AddParameters("@destacado_usuario", pDestacadoUsuario, TypeSQL.Char);
              }
              if (!string.IsNullOrEmpty(pIndValidado))
              {
                cSQL.Append(sComa);
                cSQL.Append(" ind_validado = @ind_validado");
                oParam.AddParameters("@ind_validado", pIndValidado, TypeSQL.Char);
              }
              if (!string.IsNullOrEmpty(pNotaRanking))
              {
                cSQL.Append(sComa);
                cSQL.Append(" nota_ranking = @nota_ranking");
                oParam.AddParameters("@nota_ranking", pNotaRanking, TypeSQL.Float);
              }
              if (!string.IsNullOrEmpty(pNotEtarget))
              {
                cSQL.Append(sComa);
                cSQL.Append(" notetarget = @notetarget");
                oParam.AddParameters("@notetarget", pNotEtarget, TypeSQL.Char);
              }

              cSQL.Append(" where cod_usuario = @cod_usuario ");
              oParam.AddParameters("@cod_usuario", pCodUsuario, TypeSQL.Numeric);
              oConn.Update(cSQL.ToString(), oParam);

              break;
            case "ELIMINAR":
              cSQL = new StringBuilder();
              cSQL.Append("delete from sys_usuario where cod_usuario = @cod_usuario");
              oParam.AddParameters("@cod_usuario", pCodUsuario, TypeSQL.Numeric);
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
      oParam = new DBConn.SQLParameters(20);
      DataTable dtData;
      StringBuilder cSQL;
      string Condicion = " where ";

      if (oConn.bIsOpen)
      {
        cSQL = new StringBuilder();
        cSQL.Append("select cod_usuario, cod_tipo, nom_usuario, ape_usuario, eml_usuario, login_usuario, pwd_usuario, est_usuario, date_creacion, date_modificacion, fono_usuario, destacado_usuario, ind_validado, nota_ranking, notetarget ");
        cSQL.Append("from sys_usuario ");

        if (!string.IsNullOrEmpty(pCodUsuario)) {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_usuario = @cod_usuario ");
          oParam.AddParameters("@cod_usuario", pCodUsuario, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(pNomUsuario))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" nom_usuario like '%").Append(pNomUsuario).Append("%'");
        }

        if (!string.IsNullOrEmpty(pApeUsuario))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" ape_usuario like %@ape_usuario% ");
          oParam.AddParameters("@ape_usuario", pApeUsuario, TypeSQL.Varchar);
        }

        if (!string.IsNullOrEmpty(pLoginUsuario))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" login_usuario = @login_usuario ");
          oParam.AddParameters("@login_usuario", pLoginUsuario, TypeSQL.Varchar);
        }

        if (!string.IsNullOrEmpty(pPwdUsuario))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" pwd_usuario = @pwd_usuario ");
          oParam.AddParameters("@pwd_usuario", pPwdUsuario, TypeSQL.Varchar);
        }

        if (!string.IsNullOrEmpty(pEstUsuario))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" est_usuario = @est_usuario ");
          oParam.AddParameters("@est_usuario", pEstUsuario, TypeSQL.Char);
        }

        if (!string.IsNullOrEmpty(pDestacadoUsuario))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" destacado_usuario = @destacado_usuario ");
          oParam.AddParameters("@destacado_usuario", pDestacadoUsuario, TypeSQL.Char);
        }

        if (!string.IsNullOrEmpty(pIndValidado))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" ind_validado = @ind_validado ");
          oParam.AddParameters("@ind_validado", pIndValidado, TypeSQL.Char);
        }

        if (!string.IsNullOrEmpty(pEmlUsuario)) {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" eml_usuario = @eml_usuario ");
          oParam.AddParameters("@eml_usuario", pEmlUsuario, TypeSQL.Char);
        }

        if (!string.IsNullOrEmpty(pNotaRanking))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" nota_ranking = @nota_ranking ");
          oParam.AddParameters("@nota_ranking", pNotaRanking, TypeSQL.Float);
        }

        if (!string.IsNullOrEmpty(pNotEtarget))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" notetarget = @notetarget ");
          oParam.AddParameters("@notetarget", pNotEtarget, TypeSQL.Char);
        }

        if ((bNotInUsr) && (!string.IsNullOrEmpty(pNotInCodUsr))) {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_usuario != @cod_usuario ");
          oParam.AddParameters("@cod_usuario", pNotInCodUsr, TypeSQL.Numeric);
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

    public string SerializaUsuario(ref DBConn oConn, string cPath, string cFile)
    {
      if (string.IsNullOrEmpty(cPath))
        return string.Empty;

      try
      {
        BinaryUsuario bUsuario = new BinaryUsuario();
        SysUsuario oUsuario = new SysUsuario(ref oConn);
        oUsuario.CodUsuario = pCodUsuario;
        DataTable oData = oUsuario.Get();

        if (oData != null)
          if (oData.Rows.Count > 0)
          {
            bUsuario.CodUsuario = oData.Rows[0]["cod_usuario"].ToString();
            bUsuario.CodTipo = oData.Rows[0]["cod_tipo"].ToString();
            bUsuario.NomUsuario = oData.Rows[0]["nom_usuario"].ToString();
            bUsuario.ApeUsuario = oData.Rows[0]["ape_usuario"].ToString();
            bUsuario.EmlUsuario = oData.Rows[0]["eml_usuario"].ToString();
            bUsuario.LoginUsuario = oData.Rows[0]["login_usuario"].ToString();
            bUsuario.PwdUsuario = oData.Rows[0]["pwd_usuario"].ToString();
            bUsuario.EstUsuario = oData.Rows[0]["est_usuario"].ToString();
            bUsuario.DateCreacion = oData.Rows[0]["date_creacion"].ToString();
            bUsuario.DateModificacion = oData.Rows[0]["date_modificacion"].ToString();
            bUsuario.FonoUsuario = oData.Rows[0]["fono_usuario"].ToString();
            bUsuario.DestacadoUsuario = oData.Rows[0]["destacado_usuario"].ToString();
            bUsuario.IndValidado = oData.Rows[0]["ind_validado"].ToString();
            bUsuario.NotaRanking = oData.Rows[0]["nota_ranking"].ToString();
            bUsuario.NotEtarget = oData.Rows[0]["notetarget"].ToString();
          }
        oData.Dispose();
        oUsuario = null;

        if (Directory.Exists(cPath) && !string.IsNullOrEmpty(cFile))
        {
          IFormatter oBinFormat = new BinaryFormatter();
          Stream oFileStream = new FileStream(cPath + cFile, FileMode.Create, FileAccess.Write);
          oBinFormat.Serialize(oFileStream, bUsuario);
          oFileStream.Close();

          oFileStream = null;
          bUsuario = null;
        }
        return string.Empty;
      }
      catch (Exception Ex)
      {
        return Ex.Source + " - " + Ex.Message + " - " + Ex.StackTrace;
      }
    }    

    public BinaryUsuario ClassGet()
    {
      try
      {
        if (!string.IsNullOrEmpty(pCodUsuario))
        {
          StringBuilder Directorio = new StringBuilder();
          StringBuilder Archivo = new StringBuilder();

          Directorio.Append(cPath);
          Directorio.Append(@"\binary\");

          Archivo.Append("Usuario_").Append(pCodUsuario).Append(".bin");

          if (File.Exists(Directorio.ToString() + Archivo.ToString()))
          {
            IFormatter oBinFormat = new BinaryFormatter();
            Stream oFileStream = new FileStream(Directorio.ToString() + Archivo.ToString(), FileMode.Open, FileAccess.Read, FileShare.Read);
            BinaryUsuario oUsuarios = (BinaryUsuario)oBinFormat.Deserialize(oFileStream);
            oFileStream.Close();
            return oUsuarios;
          }
          else
            return new BinaryUsuario();
        }
        else
          return new BinaryUsuario();
      }
      catch
      {
        return new BinaryUsuario();
      }
    }

    public void SerializaTblUsuario(ref DBConn oConn, string cPath, string cFile)
    {
      if (string.IsNullOrEmpty(cPath))
        return;

      try
      {
        SysUsuario oUsuario = new SysUsuario(ref oConn);
        DataTable oData = oUsuario.Get();

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
  public class BinaryUsuario
  {
    public string CodUsuario = string.Empty;
    public string CodTipo  = string.Empty;
    public string NomUsuario = string.Empty;
    public string ApeUsuario = string.Empty;
    public string EmlUsuario = string.Empty;
    public string LoginUsuario = string.Empty;
    public string PwdUsuario = string.Empty;
    public string EstUsuario = string.Empty;
    public string DateCreacion = string.Empty;
    public string DateModificacion = string.Empty;
    public string FonoUsuario = string.Empty;
    public string DestacadoUsuario = string.Empty;
    public string IndValidado = string.Empty;
    public string NotaRanking = string.Empty;
    public string NotEtarget = string.Empty;

  }
}
