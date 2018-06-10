using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using OnlineServices.Conn;

namespace OnlineServices.Reporting
{
  public class cReportes
  {
    DBConn.SQLParameters oParam;
    DBConn.DataTypeSQL TypeSQL = new DBConn.DataTypeSQL();

    private string pCodUser;
    public string CodUser { get { return pCodUser; } set { pCodUser = value; } }

    private string pOrdConsulta;
    public string OrdConsulta { get { return pOrdConsulta; } set { pOrdConsulta = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private string cPath = string.Empty;
    public string Path { get { return cPath; } set { cPath = value; } }

    private DBConn oConn;

    public cReportes()
    {

    }

    public cReportes(ref DBConn oConn)
    {
      this.oConn = oConn;
    }

    public DataTable GetMenu()
    {
      oParam = new DBConn.SQLParameters(10);
      DataTable dtData;
      StringBuilder cSQL;
      string Condicion = " and ";

      if (oConn.bIsOpen)
      {
        cSQL = new StringBuilder();
        cSQL.Append("select a.cod_user, a.cod_consulta, b.nom_consulta, b.url_consulta, b.est_consulta, b.ord_consulta, b.url_consulta_new ");
        cSQL.Append("from debt_usr_asignados a, debt_consultas b ");
        cSQL.Append(" where a.cod_consulta = b.cod_consulta ");

        if (!string.IsNullOrEmpty(pCodUser))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" a.cod_user = @coduser ");
          oParam.AddParameters("@coduser", pCodUser, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(pOrdConsulta))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" b.ord_consulta = @ordconsulta ");
          oParam.AddParameters("@ordconsulta", pOrdConsulta, TypeSQL.Numeric);
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
