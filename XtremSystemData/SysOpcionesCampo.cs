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
  public class SysOpcionesCampo
  {
    DBConn.SQLParameters oParam;
    DBConn.DataTypeSQL TypeSQL = new DBConn.DataTypeSQL();

    private string pCodOpcion;
    public string CodOpcion { get { return pCodOpcion; } set { pCodOpcion = value; } }

    private string pNomOpcion;
    public string NomOpcion { get { return pNomOpcion; } set { pNomOpcion = value; } }

    private string pAccion;
    public string Accion { get { return pAccion; } set { pAccion = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private DBConn oConn;

    public SysOpcionesCampo(ref DBConn oConn) {
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

              pCodOpcion = oConn.getTableCod("sys_opciones_campo", "cod_opcion", oConn);
              cSQL.Append("insert into sys_opciones_campo(cod_opcion, nom_opcion) values(");
              cSQL.Append("@cod_opcion, @nom_opcion)");
              oParam.AddParameters("@cod_opcion", pCodOpcion, TypeSQL.Numeric);
              oParam.AddParameters("@nom_opcion", pNomOpcion, TypeSQL.Varchar);
              oConn.Insert(cSQL.ToString(), oParam);

              break;
            case "EDITAR":
              cSQL = new StringBuilder();

              cSQL.Append("update sys_opciones_campo set");
              if (!string.IsNullOrEmpty(pNomOpcion))
              {
                cSQL.Append(" nom_opcion = @nom_opcion");
                oParam.AddParameters("@nom_opcion", pNomOpcion, TypeSQL.Varchar);
                sComa = ", ";
              }
              cSQL.Append(" where cod_opcion = @cod_opcion ");
              oParam.AddParameters("@cod_opcion", pCodOpcion, TypeSQL.Numeric);
              oConn.Update(cSQL.ToString(), oParam);
              break;
            case "ELIMINAR":
              cSQL = new StringBuilder();
              cSQL.Append("delete from sys_opciones_campo where cod_opcion = @cod_opcion");
              oParam.AddParameters("@cod_opcion", pCodOpcion, TypeSQL.Numeric);
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
        cSQL.Append("select cod_opcion, nom_opcion ");
        cSQL.Append("from sys_opciones_campo ");

        if (!string.IsNullOrEmpty(pCodOpcion))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_opcion = @cod_opcion ");
          oParam.AddParameters("@cod_opcion", pCodOpcion, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(pNomOpcion))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          //cSQL.Append(" nom_opcion like %@nom_opcion% ");
          cSQL.Append(" nom_opcion = @nom_opcion ");
          oParam.AddParameters("@nom_opcion", pNomOpcion, TypeSQL.Varchar);
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

    public void SerializaTblOpcCampo(ref DBConn oConn, string cPath, string cFile)
    {
      if (string.IsNullOrEmpty(cPath))
        return;

      try
      {
        BinaryOpcionesCampo bOpcionesCampo = new BinaryOpcionesCampo();
        SysOpcionesCampo oOpcionesCampo = new SysOpcionesCampo(ref oConn);
        DataTable oData = oOpcionesCampo.Get();

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
  public class BinaryOpcionesCampo
  { 
    public string CodOpcion = string.Empty;
    public string NomOpcion = string.Empty;
  }

}
