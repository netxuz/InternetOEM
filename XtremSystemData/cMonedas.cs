using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using OnlineServices.Conn;

namespace OnlineServices.SystemData
{
  public class cMonedas
  {
    DBConn.SQLParameters oParam;
    DBConn.DataTypeSQL TypeSQL = new DBConn.DataTypeSQL();

    private string pCodLicMoneda;
    public string CodLicMoneda { get { return pCodLicMoneda; } set { pCodLicMoneda = value; } }

    private string pCodMoneda;
    public string CodMoneda { get { return pCodMoneda; } set { pCodMoneda = value; } }

    private string pFechaMoneda;
    public string FechaMoneda { get { return pFechaMoneda; } set { pFechaMoneda = value; } }

    private string pValorMoneda;
    public string ValorMoneda { get { return pValorMoneda; } set { pValorMoneda = value; } }

    private string pMes;
    public string Mes { get { return pMes; } set { pMes = value; } }

    private string pAno;
    public string Ano { get { return pAno; } set { pAno = value; } }

    private string pAccion;
    public string Accion { get { return pAccion; } set { pAccion = value; } }

    private string pError = string.Empty;
    public string Error { get { return pError; } set { pError = value; } }

    private DBConn oConn;

    public cMonedas()
    {

    }

    public cMonedas(ref DBConn oConn)
    {
      this.oConn = oConn;
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
        cSQL.Append("select cod_lic_moneda, cod_moneda, fecha_moneda, valor_moneda ");
        cSQL.Append("from lic_monedas ");

        if (!string.IsNullOrEmpty(pCodMoneda))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_moneda = @cod_moneda");
          oParam.AddParameters("@cod_moneda", pCodMoneda, TypeSQL.Numeric);

        }

        if (!string.IsNullOrEmpty(pFechaMoneda))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" fecha_moneda = @fecha_moneda");
          oParam.AddParameters("@fecha_moneda", pFechaMoneda, TypeSQL.DateTime);

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

    public DataTable GetByCambio()
    {
      oParam = new DBConn.SQLParameters(10);
      DataTable dtData;
      StringBuilder cSQL;
      string Condicion = " where ";

      if (oConn.bIsOpen)
      {
        cSQL = new StringBuilder();
        cSQL.Append("select top 24 cod_lic_moneda, cod_moneda, month(fecha_moneda) 'mes', year(fecha_moneda) 'ano', valor_moneda ");
        cSQL.Append("from lic_monedas ");

        if (!string.IsNullOrEmpty(pCodMoneda))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_moneda = @cod_moneda ");
          oParam.AddParameters("@cod_moneda", pCodMoneda, TypeSQL.Numeric);

        }

        cSQL.Append("order by fecha_moneda desc");

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

    public float GetValueUSD()
    {
      oParam = new DBConn.SQLParameters(10);
      float oValorUsd = 0;
      DataTable dtData;
      StringBuilder cSQL;
      string Condicion = " where ";

      if (oConn.bIsOpen)
      {
        cSQL = new StringBuilder();
        cSQL.Append("select valor_moneda from lic_monedas ");

        if (!string.IsNullOrEmpty(pMes))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" month(fecha_moneda) = @mes");
          oParam.AddParameters("@mes", pMes, TypeSQL.Numeric);

        }

        if (!string.IsNullOrEmpty(pAno))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" year(fecha_moneda) = @ano");
          oParam.AddParameters("@ano", pAno, TypeSQL.Numeric);

        }

        if (!string.IsNullOrEmpty(pCodMoneda))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_moneda = @cod_moneda");
          oParam.AddParameters("@cod_moneda", pCodMoneda, TypeSQL.Numeric);

        }

        dtData = oConn.Select(cSQL.ToString(), oParam);
        if (dtData != null)
        {
          if (dtData.Rows.Count > 0)
          {
            oValorUsd = float.Parse(dtData.Rows[0]["valor_moneda"].ToString());
          }
        }
        dtData = null;

        pError = oConn.Error;
        return oValorUsd;
      }
      else
      {
        pError = "Conexion Cerrada";
        return oValorUsd;
      }

    }

    public void Put() {
      oParam = new DBConn.SQLParameters(10);
      StringBuilder cSQL;
      string sComa = string.Empty;

      if (oConn.bIsOpen) {
        try
        {
          switch (pAccion) {
            case "CREAR":
              cSQL = new StringBuilder();
              pCodLicMoneda = oConn.getTableCod("lic_monedas", "cod_lic_moneda", oConn);
              cSQL.Append("insert into lic_monedas(cod_lic_moneda, cod_moneda, fecha_moneda, valor_moneda) values(");
              cSQL.Append("@cod_lic_moneda, @cod_moneda, @fecha_moneda, @valor_moneda)");
              oParam.AddParameters("@cod_lic_moneda", pCodLicMoneda, TypeSQL.Numeric);
              oParam.AddParameters("@cod_moneda", pCodMoneda, TypeSQL.Int);
              oParam.AddParameters("@fecha_moneda", pFechaMoneda, TypeSQL.Varchar);
              oParam.AddParameters("@valor_moneda", pValorMoneda, TypeSQL.Float);
              oConn.Insert(cSQL.ToString(), oParam);
              break;
            case "ELIMINAR":
              cSQL = new StringBuilder();
              cSQL.Append("delete from lic_monedas where cod_lic_moneda = @cod_lic_moneda");
              oParam.AddParameters("@cod_lic_moneda", pCodLicMoneda, TypeSQL.Numeric);
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
