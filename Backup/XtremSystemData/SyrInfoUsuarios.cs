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
  public class SyrInfoUsuarios
  {
    DBConn.SQLParameters oParam;
    DBConn.DataTypeSQL TypeSQL = new DBConn.DataTypeSQL();

    private string pCodCampo;
    public string CodCampo { get { return pCodCampo; } set { pCodCampo = value; } }

    private string pCodUsuario;
    public string CodUsuario { get { return pCodUsuario; } set { pCodUsuario = value; } }

    private string pValCampo;
    public string ValCampo { get { return pValCampo; } set { pValCampo = value; } }

    private string pTipCampo;
    public string TipCampo { get { return pTipCampo; } set { pTipCampo = value; } }

    private string pTextCampo;
    public string TextCampo { get { return pTextCampo; } set { pTextCampo = value; } }

    private string pAccion;
    public string Accion { get { return pAccion; } set { pAccion = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private DBConn oConn;

    public SyrInfoUsuarios(ref DBConn oConn)
    {
      this.oConn = oConn;
    }

    public void Put()
    {
      oParam = new DBConn.SQLParameters(10);
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

              cSQL.Append("insert into syr_info_usuarios(cod_usuario, cod_campo, val_campo, tip_campo, text_campo) values(");
              cSQL.Append("@cod_usuario, @cod_campo, @val_campo, @tip_campo, @text_campo) ");
              oParam.AddParameters("@cod_usuario", pCodUsuario, TypeSQL.Numeric);
              oParam.AddParameters("@cod_campo", pCodCampo, TypeSQL.Numeric);
              oParam.AddParameters("@val_campo", pValCampo, TypeSQL.Varchar);
              oParam.AddParameters("@tip_campo", pTipCampo, TypeSQL.Char);
              oParam.AddParameters("@text_campo", pTextCampo, TypeSQL.Text);
              oConn.Insert(cSQL.ToString(), oParam);

              break;
            case "ELIMINAR":
              cSQL = new StringBuilder();
              cSQL.Append("delete from syr_info_usuarios ");

              if (!string.IsNullOrEmpty(pCodCampo))
              {
                cSQL.Append(Condicion);
                Condicion = " and ";
                cSQL.Append(" cod_campo = @cod_campo ");
                oParam.AddParameters("@cod_campo", pCodCampo, TypeSQL.Numeric);
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
        cSQL.Append("select cod_usuario, cod_campo, val_campo, tip_campo, text_campo ");
        cSQL.Append("from syr_info_usuarios ");

        if (!string.IsNullOrEmpty(pCodUsuario))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_usuario = @cod_usuario ");
          oParam.AddParameters("@cod_usuario", pCodUsuario, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(pCodCampo))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_campo = @cod_campo ");
          oParam.AddParameters("@cod_campo", pCodCampo, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(pValCampo)) {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" val_campo = @val_campo ");
          oParam.AddParameters("@val_campo", pValCampo, TypeSQL.Varchar);
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

    public DataTable GetInfoUsuario()
    {
      oParam = new DBConn.SQLParameters(10);
      DataTable dtData;
      StringBuilder cSQL;
      string Condicion = " and ";

      if (oConn.bIsOpen)
      {
        cSQL = new StringBuilder();
        cSQL.Append("select a.cod_usuario, a.cod_campo, a.val_campo, a.tip_campo, a.text_campo, b.nom_campo, b.tipo_campo, b.desp_campo, b.ord_campo ");
        cSQL.Append(" from syr_info_usuarios a, sys_campo_usuarios b ");
        cSQL.Append(" where a.cod_campo = b.cod_campo ");

        if (!string.IsNullOrEmpty(pCodUsuario))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" a.cod_usuario = @cod_usuario ");
          oParam.AddParameters("@cod_usuario", pCodUsuario, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(pCodCampo))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" a.cod_campo = @cod_campo ");
          oParam.AddParameters("@cod_campo", pCodCampo, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(pValCampo))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" a.val_campo = @val_campo ");
          oParam.AddParameters("@val_campo", pValCampo, TypeSQL.Varchar);
        }

        cSQL.Append(" order by b.ord_campo ");

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

    public void SerializaTblInfoUsuario(ref DBConn oConn, string cPath, string cFile)
    {
      if (string.IsNullOrEmpty(cPath))
        return;

      try
      {
        SyrInfoUsuarios oInfoUsuarios = new SyrInfoUsuarios(ref oConn);
        oInfoUsuarios.CodUsuario = pCodUsuario;
        DataTable oData = oInfoUsuarios.GetInfoUsuario();

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
  public class BinaryInfoUsuarios
  {
    public string CodUsuario = string.Empty;
    public string CodCampo = string.Empty;
    public string ValCampo = string.Empty;
    public string TipCampo = string.Empty;
    public string TextCampo = string.Empty;
  }
}
