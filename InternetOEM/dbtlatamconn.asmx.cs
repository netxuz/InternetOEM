using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Text;
using OnlineServices.Conn;


namespace ICommunity
{
  /// <summary>
  /// Summary description for dbtlatamconn
  /// </summary>
  [WebService(Namespace = "http://tempuri.org/")]
  [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
  [System.ComponentModel.ToolboxItem(false)]
  // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
  [System.Web.Script.Services.ScriptService]
  public class dbtlatamconn : System.Web.Services.WebService
  {

    [WebMethod]
    public DataTable ExecuteSQL(StringBuilder cSQL, DBConn.SQLParameters oParam)
    {
      DataTable dtData = null;
      string pError = string.Empty;

      DBConn oConn = new DBConn();
      if (oConn.Open()) {
        dtData = oConn.Select(cSQL.ToString(), oParam);
        pError = oConn.Error;
      }
      oConn.Close();

      return dtData;
    }
  }
}
