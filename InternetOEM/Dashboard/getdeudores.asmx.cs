using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;

using OnlineServices.Conn;
using OnlineServices.Method;
using OnlineServices.Reporting;
using OnlineServices.Dashboard;

namespace ICommunity.Dashboard
{
  /// <summary>
  /// Summary description for getdeudores
  /// </summary>
  [WebService(Namespace = "http://tempuri.org/")]
  [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
  [System.ComponentModel.ToolboxItem(false)]
  // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
  [System.Web.Script.Services.ScriptService]
  public class getdeudores : System.Web.Services.WebService
  {

    [WebMethod]
    public List<string> getDeudores(string sNomDeudor, string sHolding, string sCliente)
    {
      List<string> listDeudores = new List<string>();
      DBConn oConn = new DBConn();
      if (oConn.Open()) {
        cDashboard oDeudores = new cDashboard(ref oConn);
        oDeudores.NomDeudor = sNomDeudor;
        if (!string.IsNullOrEmpty(sHolding))
          oDeudores.CodHolding = sHolding;
        if (!string.IsNullOrEmpty(sCliente))
          oDeudores.nKeyCliente = sCliente;
        DataTable dtDeudores = oDeudores.GetDeudores();

        if (dtDeudores != null)
        {
          if (dtDeudores.Rows.Count > 0)
          {
            foreach (DataRow oRow in dtDeudores.Rows)
            {
              listDeudores.Add(oRow["ncodigodeudor"].ToString() + " " + oRow["deudor"].ToString().Replace("'", ""));
            }
          }
        }
        dtDeudores = null;
      }
      oConn.Close();

      return listDeudores;
    }
  }
}
