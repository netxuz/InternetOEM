using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using OnlineServices.Conn;

namespace OnlineServices.Dashboard
{
  public class cWidgets
  {
    DBConn.SQLParameters oParam;
    DBConn.DataTypeSQL TypeSQL = new DBConn.DataTypeSQL();

    private string lngCodUser;
    public string CodUser { get { return lngCodUser; } set { lngCodUser = value; } }

    private string lngCodWidget;
    public string CodWidget { get { return lngCodWidget; } set { lngCodWidget = value; } }

    private string iOrder;
    public string Order { get { return iOrder; } set { iOrder = value; } }

    private string sAccion;
    public string Accion { get { return sAccion; } set { sAccion = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private DBConn oConn;

    public cWidgets() {

    }

    public cWidgets(ref DBConn oConn) {
      this.oConn = oConn;
    }

    public DataTable getWidgetsAvailable() {
      oParam = new DBConn.SQLParameters(1);
      DataTable dtData;

      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();

        cSQL.Append("select * from dashboard_widget");
        cSQL.Append(" where not cod_widget in(select cod_widget from sys_user_widget where cod_user = @cod_user) and est_widget = 'V' ");
        oParam.AddParameters("@cod_user", lngCodUser, TypeSQL.Numeric);

        dtData = oConn.Select(cSQL.ToString(), oParam);
        pError = oConn.Error;
        return dtData;
      }
      else
      {
        return null;
      }
    }

    public DataTable getWidgetsByUser()
    {
      oParam = new DBConn.SQLParameters(1);
      DataTable dtData;

      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();

        cSQL.Append("select a.*, b.orden_widget from dashboard_widget a, sys_user_widget b");
        cSQL.Append(" where a.cod_widget = b.cod_widget ");
        cSQL.Append(" and b.cod_user = @cod_user ");
        cSQL.Append(" order by b.orden_widget ");
        oParam.AddParameters("@cod_user", lngCodUser, TypeSQL.Numeric);

        dtData = oConn.Select(cSQL.ToString(), oParam);
        pError = oConn.Error;
        return dtData;
      }
      else
      {
        return null;
      }
    }

    public void Put() {
      oParam = new DBConn.SQLParameters(10);
      StringBuilder cSQL;
      string sComa = string.Empty;

      if (oConn.bIsOpen)
      {
        try
        {
          switch (sAccion)
          {
            case "CREAR":
              cSQL = new StringBuilder();
              //pCodPagos = getCod();
              cSQL.Append("insert into sys_user_widget(cod_user,cod_widget,orden_widget) values( ");
              cSQL.Append("@cod_user,@cod_widget,@orden_widget) ");
              oParam.AddParameters("@cod_user", lngCodUser, TypeSQL.Numeric);
              oParam.AddParameters("@cod_widget", lngCodWidget, TypeSQL.Numeric);
              oParam.AddParameters("@orden_widget", iOrder, TypeSQL.Int);
              oConn.Insert(cSQL.ToString(), oParam);

              if (!string.IsNullOrEmpty(oConn.Error))
              {
                pError = oConn.Error;
              }

              break;
            
            case "ELIMINAR":
              cSQL = new StringBuilder();
              cSQL.Append("delete from sys_user_widget where cod_user = @cod_user and cod_widget = @cod_widget ");
              oParam.AddParameters("@cod_user", lngCodUser, TypeSQL.Numeric);
              oParam.AddParameters("@cod_widget", lngCodWidget, TypeSQL.Numeric);
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
