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
  public class SysParamEmail
  {
    DBConn.SQLParameters oParam;
    DBConn.DataTypeSQL TypeSQL = new DBConn.DataTypeSQL();

    private string pCodEmail;
    public string CodEmail { get { return pCodEmail; } set { pCodEmail = value; } }

    private string pNomEmail;
    public string NomEmail { get { return pNomEmail; } set { pNomEmail = value; } }

    private string pAsuntoEmail;
    public string AsuntoEmail { get { return pAsuntoEmail; } set { pAsuntoEmail = value; } }

    private string pCuerpoEmail;
    public string CuerpoEmail { get { return pCuerpoEmail; } set { pCuerpoEmail = value; } }

    private string pTipoEmail;
    public string TipoEmail { get { return pTipoEmail; } set { pTipoEmail = value; } }

    private string pAccion;
    public string Accion { get { return pAccion; } set { pAccion = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private string cPath = string.Empty;
    public string Path { get { return cPath; } set { cPath = value; } }

    private DBConn oConn;

    public SysParamEmail() { 
    }

    public SysParamEmail(ref DBConn oConn)
    {
      this.oConn = oConn;
    }

    public void Put()
    {
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
              cSQL = new StringBuilder();

              pCodEmail = oConn.getTableCod("sys_param_email", "cod_email", oConn);
              cSQL.Append("insert into sys_param_email(cod_email, nom_email, asunto_email, cuerpo_email, tipo_email) values(");
              cSQL.Append("@cod_email, @nom_email, @asunto_email, @cuerpo_email, @tipo_email)");
              oParam.AddParameters("@cod_email", pCodEmail, TypeSQL.Numeric);
              oParam.AddParameters("@nom_email", pNomEmail, TypeSQL.Varchar);
              oParam.AddParameters("@asunto_email", pAsuntoEmail, TypeSQL.Varchar);
              oParam.AddParameters("@cuerpo_email", pCuerpoEmail, TypeSQL.Text);
              oParam.AddParameters("@tipo_email", pTipoEmail, TypeSQL.Char);
              oConn.Insert(cSQL.ToString(), oParam);

              break;
            case "EDITAR":
              cSQL = new StringBuilder();
              cSQL.Append("update sys_param_email set ");
              if (!string.IsNullOrEmpty(pNomEmail))
              {
                cSQL.Append(sComa);
                cSQL.Append(" nom_email = @nom_email");
                oParam.AddParameters("@nom_email", pNomEmail, TypeSQL.Varchar);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pAsuntoEmail))
              {
                cSQL.Append(sComa);
                cSQL.Append(" asunto_email = @asunto_email");
                oParam.AddParameters("@asunto_email", pAsuntoEmail, TypeSQL.Varchar);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pCuerpoEmail))
              {
                cSQL.Append(sComa);
                cSQL.Append(" cuerpo_email = @cuerpo_email");
                oParam.AddParameters("@cuerpo_email", pCuerpoEmail, TypeSQL.Text);
                sComa = ", ";
              }

              if (!string.IsNullOrEmpty(pCodEmail))
              {
                cSQL.Append(" where cod_email = @cod_email ");
                oParam.AddParameters("@cod_email", pCodEmail, TypeSQL.Numeric);
              }
              if (!string.IsNullOrEmpty(pTipoEmail))
              {
                cSQL.Append(" where tipo_email = @tipo_email");
                oParam.AddParameters("@tipo_email", pTipoEmail, TypeSQL.Char);
              }
              oConn.Update(cSQL.ToString(), oParam);

              break;
            case "ELIMINAR":
              cSQL = new StringBuilder();
              cSQL.Append("delete from sys_param_email where cod_email = @cod_email");
              oParam.AddParameters("@cod_email", pCodEmail, TypeSQL.Numeric);
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
        cSQL.Append("select cod_email, nom_email, asunto_email, cuerpo_email, tipo_email ");
        cSQL.Append("from sys_param_email ");

        if (!string.IsNullOrEmpty(pCodEmail))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_email = @cod_email ");
          oParam.AddParameters("@cod_email", pCodEmail, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(pTipoEmail))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" tipo_email = @tipo_email ");
          oParam.AddParameters("@tipo_email", pTipoEmail, TypeSQL.Char);
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

    public void SerializaParamEmail()
    {
      if (string.IsNullOrEmpty(cPath))
        return;

      try
      {
        DataTable oData = Get();

        if (Directory.Exists(cPath) && oData != null)
        {
          string cFile = "ParamEmail.bin";
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
