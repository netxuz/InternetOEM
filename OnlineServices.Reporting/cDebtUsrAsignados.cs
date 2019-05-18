using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using OnlineServices.Conn;

namespace OnlineServices.Reporting
{
  public class cDebtUsrAsignados
  {
    DBConn.SQLParameters oParam;
    DBConn.DataTypeSQL TypeSQL = new DBConn.DataTypeSQL();

    private string pCodUsuario;
    public string CodUsuario { get { return pCodUsuario; } set { pCodUsuario = value; } }

    private string pCodConsulta;
    public string CodConsulta { get { return pCodConsulta; } set { pCodConsulta = value; } }

    private string pNomConsulta;
    public string NomConsulta { get { return pNomConsulta; } set { pNomConsulta = value; } }

    private string pFiltroDeudor;
    public string FiltroDeudor { get { return pFiltroDeudor; } set { pFiltroDeudor = value; } }

    private string pEstConsulta;
    public string EstConsulta { get { return pEstConsulta; } set { pEstConsulta = value; } }

    private string pFiltroHolding;
    public string FiltroHolding { get { return pFiltroHolding; } set { pFiltroHolding = value; } }

    private bool pNOTIn;
    public bool NOTIn { get { return pNOTIn; } set { pNOTIn = value; } }

    private string pAccion;
    public string Accion { get { return pAccion; } set { pAccion = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private DBConn oConn;

    public cDebtUsrAsignados(ref DBConn oConn)
    {
      this.oConn = oConn;
    }

    public void Put()
    {
      oParam = new DBConn.SQLParameters(10);
      StringBuilder cSQL;
      string Condicion = " where ";
      string sComa = string.Empty;

      if (oConn.bIsOpen)
      {
        try
        {
          switch (pAccion)
          {
            case "CREAR":
              pFiltroDeudor = "V";
              pFiltroHolding = "V";

              cSQL = new StringBuilder();
              cSQL.Append("insert into debt_usr_asignados(cod_user, cod_consulta, filtro_deudor, filtro_holding) values(");
              cSQL.Append("@cod_user, @cod_consulta, @filtro_deudor, @filtro_holding)");
              oParam.AddParameters("@cod_user", pCodUsuario, TypeSQL.Numeric);
              oParam.AddParameters("@cod_consulta", pCodConsulta, TypeSQL.Numeric);
              oParam.AddParameters("@filtro_deudor", pFiltroDeudor, TypeSQL.Varchar);
              oParam.AddParameters("@filtro_holding", pFiltroHolding, TypeSQL.Varchar);
              oConn.Insert(cSQL.ToString(), oParam);

              break;
            case "EDITAR":
              cSQL = new StringBuilder();
              cSQL.Append("update debt_usr_asignados set ");

              if (!string.IsNullOrEmpty(pFiltroDeudor))
              {
                cSQL.Append(sComa);
                cSQL.Append(" filtro_deudor = @filtro_deudor");
                oParam.AddParameters("@filtro_deudor", pFiltroDeudor, TypeSQL.Varchar);
                sComa = ", ";
              }

              if (!string.IsNullOrEmpty(pFiltroHolding))
              {
                cSQL.Append(sComa);
                cSQL.Append(" filtro_holding = @filtro_holding");
                oParam.AddParameters("@filtro_holding", pFiltroHolding, TypeSQL.Varchar);
                sComa = ", ";
              }

              cSQL.Append(" where cod_user = @cod_usuario and cod_consulta = @cod_consulta ");
              oParam.AddParameters("@cod_usuario", pCodUsuario, TypeSQL.Numeric);
              oParam.AddParameters("@cod_consulta", pCodConsulta, TypeSQL.Numeric);

              oConn.Update(cSQL.ToString(), oParam);

              break;
            case "ELIMINAR":
              cSQL = new StringBuilder();
              cSQL.Append("delete from debt_usr_asignados  ");
              if (!string.IsNullOrEmpty(pCodUsuario))
              {
                cSQL.Append(Condicion);
                Condicion = " and ";
                cSQL.Append(" cod_user = @cod_user");
                oParam.AddParameters("@cod_user", pCodUsuario, TypeSQL.Numeric);
              }
              if (!string.IsNullOrEmpty(pCodConsulta))
              {
                cSQL.Append(Condicion);
                Condicion = " and ";
                cSQL.Append(" cod_consulta = @cod_consulta");
                oParam.AddParameters("@cod_consulta", pCodConsulta, TypeSQL.Numeric);
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

    public DataTable GetConsultaByUsuario()
    {
      oParam = new DBConn.SQLParameters(10);
      DataTable dtData;
      StringBuilder cSQL;
      string Condicion = " where ";
      string CondicionInorNOtIn = " where ";

      if (!pNOTIn)
        CondicionInorNOtIn = " where not ";

      if (oConn.bIsOpen)
      {
        cSQL = new StringBuilder();
        cSQL.Append("select cod_consulta, nom_consulta from debt_consultas ");

        cSQL.Append(CondicionInorNOtIn);
        cSQL.Append(" cod_consulta in(select cod_consulta from debt_usr_asignados ");

        if (!string.IsNullOrEmpty(pCodUsuario))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_user = @cod_user ");
          oParam.AddParameters("@cod_user", pCodUsuario, TypeSQL.Numeric);
        }

        cSQL.Append(") ");

        if (!string.IsNullOrEmpty(pNomConsulta))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" nom_consulta like '%' + @nombre + '%'  ");
          oParam.AddParameters("@nombre", pNomConsulta, TypeSQL.Varchar);
        }

        if (!string.IsNullOrEmpty(pEstConsulta)) {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" est_consulta = @est_consulta  ");
          oParam.AddParameters("@est_consulta", pEstConsulta, TypeSQL.Char);
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

    public DataTable Get()
    {
      oParam = new DBConn.SQLParameters(10);
      DataTable dtData;
      StringBuilder cSQL;
      string Condicion = " where ";

      if (oConn.bIsOpen)
      {
        cSQL = new StringBuilder();
        cSQL.Append("select cod_user, cod_consulta, filtro_deudor, filtro_holding from debt_usr_asignados ");

        if (!string.IsNullOrEmpty(pCodUsuario))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_user = @cod_user ");
          oParam.AddParameters("@cod_user", pCodUsuario, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(pCodConsulta))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_consulta = @cod_consulta ");
          oParam.AddParameters("@cod_consulta", pCodConsulta, TypeSQL.Numeric);
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
