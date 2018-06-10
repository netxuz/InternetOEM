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
  public class SysEmailToUser
  {
    DBConn.SQLParameters oParam;
    DBConn.DataTypeSQL TypeSQL = new DBConn.DataTypeSQL();

    private string pCodEmail;
    public string CodEmail { get { return pCodEmail; } set { pCodEmail = value; } }

    private string pCodEmailRel;
    public string CodEmailRel { get { return pCodEmailRel; } set { pCodEmailRel = value; } }

    private string pCodUsuario;
    public string CodUsuario { get { return pCodUsuario; } set { pCodUsuario = value; } }

    private string pCodUsrSendEmail;
    public string CodUsrSendEmail { get { return pCodUsrSendEmail; } set { pCodUsrSendEmail = value; } }

    private string pCuerpoEmail;
    public string CuerpoEmail { get { return pCuerpoEmail; } set { pCuerpoEmail = value; } }

    private string pFechaEmail;
    public string FechaEmail { get { return pFechaEmail; } set { pFechaEmail = value; } }

    private string pEstEmail;
    public string EstEmail { get { return pEstEmail; } set { pEstEmail = value; } }

    private string pAccion;
    public string Accion { get { return pAccion; } set { pAccion = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private string cPath = string.Empty;
    public string Path { get { return cPath; } set { cPath = value; } }

    private DBConn oConn;

    public SysEmailToUser(ref DBConn oConn)
    {
      this.oConn = oConn;
    }

    public void Put()
    {
      DataTable dtData;
      oParam = new DBConn.SQLParameters(7);
      StringBuilder cSQL;
      string sComa = string.Empty;
      string Condicion = " where ";

      if (oConn.bIsOpen)
      {
        try
        {
          switch (pAccion)
          {
            case "CREAR":
              cSQL = new StringBuilder();
              cSQL.Append("insert into sys_emailtouser(cod_email_rel, cod_usuario, cod_usr_send_email, cuerpo_email, fecha_email, est_email) values(");
              cSQL.Append("@cod_email_rel, @cod_usuario, @cod_usr_send_email, @cuerpo_email, @fecha_email, @est_email)");
              oParam.AddParameters("@cod_email_rel", pCodEmailRel, TypeSQL.Numeric);
              oParam.AddParameters("@cod_usuario", pCodUsuario, TypeSQL.Numeric);
              oParam.AddParameters("@cod_usr_send_email", pCodUsrSendEmail, TypeSQL.Numeric);
              oParam.AddParameters("@cuerpo_email", pCuerpoEmail, TypeSQL.Text);
              oParam.AddParameters("@fecha_email", pFechaEmail, TypeSQL.DateTime);
              oParam.AddParameters("@est_email", pEstEmail, TypeSQL.Char);
              oConn.Insert(cSQL.ToString(), oParam);

              cSQL = new StringBuilder();
              cSQL.Append("select @@IDENTITY");
              dtData = oConn.Select(cSQL.ToString());
              if (dtData != null)
                if (dtData.Rows.Count > 0)
                  pCodEmail = dtData.Rows[0][0].ToString();
              dtData = null;

              break;
            case "ELIMINAR":
              cSQL = new StringBuilder();
              cSQL.Append("delete from sys_emailtouser");

              if (!string.IsNullOrEmpty(pCodEmail))
              {
                cSQL.Append(Condicion);
                Condicion = " and ";
                cSQL.Append(" cod_email = @cod_email ");
                oParam.AddParameters("@cod_email", pCodEmail, TypeSQL.Numeric);
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
        cSQL.Append("select cod_email, cod_email_rel, cod_usuario, cod_usr_send_email, cuerpo_email, fecha_email, est_email ");
        cSQL.Append("from sys_emailtouser ");

        if (!string.IsNullOrEmpty(pCodEmail))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_email = @cod_email ");
          oParam.AddParameters("@cod_email", pCodEmail, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(pCodUsuario))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_usuario = @cod_usuario ");
          oParam.AddParameters("@cod_usuario", pCodUsuario, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(pCodUsrSendEmail))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_usr_send_email = @cod_usr_send_email ");
          oParam.AddParameters("@cod_usr_send_email", pCodUsrSendEmail, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(pEstEmail))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" est_email = @est_email ");
          oParam.AddParameters("@est_email", pEstEmail, TypeSQL.Char);
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

    public void Serializa(ref DBConn oConn, string cPath, string cFile)
    {
      if (string.IsNullOrEmpty(cPath))
        return;

      try
      {
        DataTable oData = Get();

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
