using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using OnlineServices.Conn;

namespace OnlineServices.SystemData
{
  public class SysHoldingCliente
  {
    DBConn.SQLParameters oParam;
    DBConn.DataTypeSQL TypeSQL = new DBConn.DataTypeSQL();

    private string pCodUsuario;
    public string CodUsuario { get { return pCodUsuario; } set { pCodUsuario = value; } }

    private string pCodHolding;
    public string CodHolding { get { return pCodHolding; } set { pCodHolding = value; } }

    private string psNombre;
    public string sNombre { get { return psNombre; } set { psNombre = value; } }

    private string pAccion;
    public string Accion { get { return pAccion; } set { pAccion = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private DBConn oConn;

    public SysHoldingCliente(ref DBConn oConn)
    {
      this.oConn = oConn;
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
        cSQL.Append("select a.cod_user, a.ncod_holding, (select distinct holding from cliente where ncodholding = a.ncod_holding) snombre from sys_holding_cliente a ");

        if (!string.IsNullOrEmpty(pCodUsuario))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" a.cod_user = @cod_user ");
          oParam.AddParameters("@cod_user", pCodUsuario, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(pCodHolding))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" a.ncod_holding = @ncod_holding ");
          oParam.AddParameters("@ncod_holding", pCodHolding, TypeSQL.Numeric);
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

    public DataTable GetHoldingNotInUsuario()
    {
      oParam = new DBConn.SQLParameters(20);
      DataTable dtData;
      StringBuilder cSQL;
      string Condicion = " and ";

      if (oConn.bIsOpen)
      {
        cSQL = new StringBuilder();
        cSQL.Append("select distinct(ncodholding) as 'ncodholding', holding from cliente where not ncodholding in(select ncod_holding from sys_holding_cliente where cod_user = @cod_user ) and bCuentaCorriente <> 0 and sAnalista <> '' ");
        oParam.AddParameters("@cod_user", pCodUsuario, TypeSQL.Numeric);

        if (!string.IsNullOrEmpty(psNombre))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" holding like '%' + @snombre + '%'  ");
          oParam.AddParameters("@snombre", psNombre, TypeSQL.Varchar);
        }

        cSQL.Append(" order by holding asc ");

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

    public void Put()
    {
      oParam = new DBConn.SQLParameters(20);
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
              cSQL.Append("insert into sys_holding_cliente(cod_user, ncod_holding) values(");
              cSQL.Append("@cod_user, @ncod_holding)");
              oParam.AddParameters("@cod_user", pCodUsuario, TypeSQL.Numeric);
              oParam.AddParameters("@ncod_holding", pCodHolding, TypeSQL.Numeric);
              oConn.Insert(cSQL.ToString(), oParam);

              break;

            case "ELIMINAR":
              cSQL = new StringBuilder();
              cSQL.Append("delete from sys_holding_cliente ");

              if (!string.IsNullOrEmpty(pCodUsuario))
              {
                cSQL.Append(Condicion);
                Condicion = " and ";
                cSQL.Append(" cod_user = @cod_user  ");
                oParam.AddParameters("@cod_user", pCodUsuario, TypeSQL.Numeric);
              }

              if (!string.IsNullOrEmpty(pCodHolding))
              {
                cSQL.Append(Condicion);
                Condicion = " and ";
                cSQL.Append(" ncod_holding = @ncod_holding  ");
                oParam.AddParameters("@ncod_holding", pCodHolding, TypeSQL.Numeric);
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
  }
}
