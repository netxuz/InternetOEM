using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OnlineServices.Conn;

namespace OnlineServices.SystemData
{
  public class SysTblCode
  {
    DBConn.SQLParameters oParam;
    DBConn.DataTypeSQL TypeSQL = new DBConn.DataTypeSQL();

    private string pTabla;
    public string Tabla { get { return pTabla; } set { pTabla = value; } }

    private string pCode;
    public string Code { get { return pCode; } set { pCode = value; } }

    private string pAccion;
    public string Accion { get { return pAccion; } set { pAccion = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }


    private DBConn oConn;

    public SysTblCode(ref DBConn oConn) {
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
              pCode = DateTime.Now.ToString("yyyyMMddHHmmss").ToString();
              cSQL = new StringBuilder();
              cSQL.Append("update sys_tbl_code set ");
              cSQL.Append(" code = ").Append(pCode);
              cSQL.Append(" where tabla = @tabla ");
              oParam.AddParameters("@tabla", pTabla, TypeSQL.Varchar);
              oConn.Update(cSQL.ToString(), oParam);

              break;
            case "ELIMINAR":
              cSQL = new StringBuilder();
              cSQL.Append("delete from sys_tbl_code where tabla = @tabla ");
              oParam.AddParameters("@tabla", pTabla, TypeSQL.Numeric);
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
      oParam = new DBConn.SQLParameters(2);
      DataTable dtData;
      StringBuilder cSQL;
      string Condicion = " where ";

      if (oConn.bIsOpen)
      {
        cSQL = new StringBuilder();
        cSQL.Append("select tabla, code from sys_tbl_code ");

        if (string.IsNullOrEmpty(pTabla))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" tabla = @tabla ");
          oParam.AddParameters("@tabla", pTabla, TypeSQL.Numeric);
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

  }
}
