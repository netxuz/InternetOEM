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
  public class SysCampoUsuarios
  {
    DBConn.SQLParameters oParam;
    DBConn.DataTypeSQL TypeSQL = new DBConn.DataTypeSQL();

    private string pCodCampo;
    public string CodCampo { get { return pCodCampo; } set { pCodCampo = value; } }

    private string pNomCampo;
    public string NomCampo { get { return pNomCampo; } set { pNomCampo = value; } }

    private string pTipoCampo;
    public string TipoCampo { get { return pTipoCampo; } set { pTipoCampo = value; } }

    private string pEstCampo;
    public string EstCampo { get { return pEstCampo; } set { pEstCampo = value; } }

    private string pDespCampo;
    public string DespCampo { get { return pDespCampo; } set { pDespCampo = value; } }

    private string pOrdCampo;
    public string OrdCampo { get { return pOrdCampo; } set { pOrdCampo = value; } }

    private string pIndDespliegue;
    public string IndDespliegue { get { return pIndDespliegue; } set { pIndDespliegue = value; } }

    private string pIndDesplieguePortal;
    public string IndDesplieguePortal { get { return pIndDesplieguePortal; } set { pIndDesplieguePortal = value; } }

    private string pIndValidacion;
    public string IndValidacion { get { return pIndValidacion; } set { pIndValidacion = value; } }

    private string pAccion;
    public string Accion { get { return pAccion; } set { pAccion = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private DBConn oConn;

    public SysCampoUsuarios() { 
    }

    public SysCampoUsuarios(ref DBConn oConn)
    {
      this.oConn = oConn;
    }

    public void Put()
    {
      oParam = new DBConn.SQLParameters(15);
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

              pCodCampo = oConn.getTableCod("sys_campo_usuarios", "cod_campo", oConn);
              cSQL.Append("insert into sys_campo_usuarios(cod_campo, nom_campo, tipo_campo, est_campo, desp_campo, ord_campo, ind_despliegue, ind_despliegue_portal, ind_validacion) values(");
              cSQL.Append("@cod_campo, @nom_campo, @tipo_campo, @est_campo, @desp_campo, @ord_campo, @ind_despliegue, @ind_despliegue_portal, @ind_validacion)");
              oParam.AddParameters("@cod_campo", pCodCampo, TypeSQL.Numeric);
              oParam.AddParameters("@nom_campo", pNomCampo, TypeSQL.Varchar);
              oParam.AddParameters("@tipo_campo", pTipoCampo, TypeSQL.Char);
              oParam.AddParameters("@est_campo", pEstCampo, TypeSQL.Char);
              oParam.AddParameters("@desp_campo", pDespCampo, TypeSQL.Char);
              oParam.AddParameters("@ord_campo", pOrdCampo, TypeSQL.Int);
              oParam.AddParameters("@ind_despliegue", pIndDespliegue, TypeSQL.Char);
              oParam.AddParameters("@ind_despliegue_portal", pIndDesplieguePortal, TypeSQL.Char);
              oParam.AddParameters("@ind_validacion", pIndValidacion, TypeSQL.Char);
              oConn.Insert(cSQL.ToString(), oParam);

              break;
            case "EDITAR":
              cSQL = new StringBuilder();
              cSQL.Append("update sys_campo_usuarios set ");
              if (!string.IsNullOrEmpty(pNomCampo))
              {
                cSQL.Append(" nom_campo = @nom_campo");
                oParam.AddParameters("@nom_campo", pNomCampo, TypeSQL.Varchar);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pTipoCampo))
              {
                cSQL.Append(sComa);
                cSQL.Append(" tipo_campo = @tipo_campo");
                oParam.AddParameters("@tipo_campo", pTipoCampo, TypeSQL.Char);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pEstCampo))
              {
                cSQL.Append(sComa);
                cSQL.Append(" est_campo = @est_campo");
                oParam.AddParameters("@est_campo", pEstCampo, TypeSQL.Char);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pDespCampo))
              {
                cSQL.Append(sComa);
                cSQL.Append(" desp_campo = @desp_campo");
                oParam.AddParameters("@desp_campo", pDespCampo, TypeSQL.Char);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pOrdCampo))
              {
                cSQL.Append(sComa);
                cSQL.Append(" ord_campo = @ord_campo");
                oParam.AddParameters("@ord_campo", pOrdCampo, TypeSQL.Int);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pIndDespliegue))
              {
                cSQL.Append(sComa);
                cSQL.Append(" ind_despliegue = @ind_despliegue");
                oParam.AddParameters("@ind_despliegue", pIndDespliegue, TypeSQL.Char);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pIndDesplieguePortal))
              {
                cSQL.Append(sComa);
                cSQL.Append(" ind_despliegue_portal = @ind_despliegue_portal");
                oParam.AddParameters("@ind_despliegue_portal", pIndDesplieguePortal, TypeSQL.Char);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pIndValidacion))
              {
                cSQL.Append(sComa);
                cSQL.Append(" ind_validacion = @ind_validacion");
                oParam.AddParameters("@ind_validacion", pIndValidacion, TypeSQL.Char);
                sComa = ", ";
              }
              cSQL.Append(" where cod_campo = @cod_campo ");
              oParam.AddParameters("@cod_campo", pCodCampo, TypeSQL.Numeric);
              oConn.Update(cSQL.ToString(), oParam);

              break;
            case "ELIMINAR":
              cSQL = new StringBuilder();
              cSQL.Append("delete from sys_campo_usuarios where cod_campo = @cod_campo");
              oParam.AddParameters("@cod_campo", pCodCampo, TypeSQL.Numeric);
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
      oParam = new DBConn.SQLParameters(15);
      DataTable dtData;
      StringBuilder cSQL;
      string Condicion = " where ";

      if (oConn.bIsOpen)
      {
        cSQL = new StringBuilder();
        cSQL.Append("select cod_campo, nom_campo, tipo_campo, est_campo, desp_campo, ord_campo, ind_despliegue, ind_despliegue_portal, ind_validacion ");
        cSQL.Append("from sys_campo_usuarios ");

        if (!string.IsNullOrEmpty(pCodCampo))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_campo = @cod_campo ");
          oParam.AddParameters("@cod_campo", pCodCampo, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(pNomCampo))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" nom_campo like %@nom_campo% ");
          oParam.AddParameters("@nom_campo", pNomCampo, TypeSQL.Varchar);
        }

        if (!string.IsNullOrEmpty(pTipoCampo))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" tipo_campo = @tipo_campo ");
          oParam.AddParameters("@tipo_campo", pTipoCampo, TypeSQL.Char);
        }

        if (!string.IsNullOrEmpty(pEstCampo))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" est_campo = @est_campo ");
          oParam.AddParameters("@est_campo", pEstCampo, TypeSQL.Char);
        }
        if (!string.IsNullOrEmpty(pDespCampo))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" desp_campo = @desp_campo ");
          oParam.AddParameters("@desp_campo", pDespCampo, TypeSQL.Char);
        }
        if (!string.IsNullOrEmpty(pIndDespliegue))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" ind_despliegue = @ind_despliegue ");
          oParam.AddParameters("@ind_despliegue", pIndDespliegue, TypeSQL.Char);
        }
        if (!string.IsNullOrEmpty(pIndDesplieguePortal))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" ind_despliegue_portal = @ind_despliegue_portal ");
          oParam.AddParameters("@ind_despliegue_portal", pIndDesplieguePortal, TypeSQL.Char);
        }

        if (!string.IsNullOrEmpty(pIndValidacion))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" ind_validacion = @ind_validacion ");
          oParam.AddParameters("@ind_validacion", pIndValidacion, TypeSQL.Char);
        }

        cSQL.Append(" order by ord_campo ");

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

    public void SerializaTblCmpUsuario(ref DBConn oConn, string cPath, string cFile)
    {
      if (string.IsNullOrEmpty(cPath))
        return;

      try
      {
        BinaryCampoUsuario bCampoUsuarios = new BinaryCampoUsuario();
        SysCampoUsuarios oCampoUsuarios = new SysCampoUsuarios(ref oConn);
        DataTable oData = oCampoUsuarios.Get();

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
  public class BinaryCampoUsuario
  {
    public string CodCampo = string.Empty;
    public string NomCampo = string.Empty;
    public string TipoCampo = string.Empty;
    public string EstCampo = string.Empty;
    public string DespCampo = string.Empty;
    public string OrdCampo = string.Empty;
    public string IndDespliegue = string.Empty;
    public string IndDesplieguePortal = string.Empty;
    public string pIndValidacion = string.Empty;
  }

}
