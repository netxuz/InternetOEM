using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using OnlineServices.Conn;

namespace OnlineServices.Reporting
{
  public class cDeudores
  {
    DBConn.SQLParameters oParam;
    DBConn.DataTypeSQL TypeSQL = new DBConn.DataTypeSQL();

    private string lngCodNkey;
    public string CodNkey { get { return lngCodNkey; } set { lngCodNkey = value; } }

    private string lngCodDeudor;
    public string CodDeudor { get { return lngCodDeudor; } set { lngCodDeudor = value; } }

    private string sTipoUsuario;
    public string TipoUsuario { get { return sTipoUsuario; } set { sTipoUsuario = value; } }

    private string lngNkeyUsuario;
    public string NkeyUsuario { get { return lngNkeyUsuario; } set { lngNkeyUsuario = value; } }

    private string pNombre;
    public string Nombre { get { return pNombre; } set { pNombre = value; } }

    private string pRUT;
    public string RUT { get { return pRUT; } set { pRUT = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private DBConn oConn;

    public cDeudores()
    {

    }

    public cDeudores(ref DBConn oConn)
    {
      this.oConn = oConn;
    }

    public DataTable Get() {
      oParam = new DBConn.SQLParameters(10);
      DataTable dtData;

      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();
        cSQL.Append("select deudor.snombre RazonSocial, codigodeudor.ncodigodeudor as RUT , deudor.nkey_deudor lngCodDeudor");
        cSQL.Append(" from deudor join codigodeudor ");
        cSQL.Append(" on (codigodeudor.nkey_cliente = @cod_nkey ");
        cSQL.Append(" and deudor.nkey_deudor = codigodeudor.nkey_deudor) ");
        oParam.AddParameters("@cod_nkey", lngCodNkey, TypeSQL.Numeric);

        if (!string.IsNullOrEmpty(pNombre))
        {
          cSQL.Append(" and deudor.sNombre Like '%").Append(pNombre).Append("%'");
        }
        if (!string.IsNullOrEmpty(pRUT))
        {
          cSQL.Append(" and codigodeudor.ncodigodeudor like '%").Append(pRUT).Append("%'");
        }
        if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
        {
          cSQL.Append("  and deudor.nkey_deudor = @nkey_usuario ");
          oParam.AddParameters("@nkey_usuario", lngNkeyUsuario, TypeSQL.Numeric);

        }
        if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "V"))
        {
          cSQL.Append("  and codigodeudor.nkey_vendedor = @nkey_usuario ");
          oParam.AddParameters("@nkey_usuario", lngNkeyUsuario, TypeSQL.Numeric);

        }
        cSQL.Append(" Order by deudor.sNombre ");

        dtData = oConn.Select(cSQL.ToString(), oParam);
        pError = oConn.Error;
        return dtData;
      }
      else {
        return null;
      }

      
    }

    public DataTable GetDeudor() {
      oParam = new DBConn.SQLParameters(10);
      DataTable dtData;

      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();

        cSQL.Append("select a.snombre, b.ncodigodeudor as ncod from deudor a, codigodeudor b  where a.nKey_Deudor = @lngCodDeudor ");
        cSQL.Append(" and a.nKey_Deudor = b.nKey_Deudor ");
        oParam.AddParameters("@lngCodDeudor", lngCodDeudor, TypeSQL.Numeric);

        dtData = oConn.Select(cSQL.ToString(), oParam);
        pError = oConn.Error;
        return dtData;
      }
      else
      {
        return null;
      }
    }

    public DataTable GetDeudorRubrosIntermobi() {
      oParam = new DBConn.SQLParameters(10);
      DataTable dtData;

      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();
        cSQL.Append("select deudor.snombre, codigodeudor.ncodigodeudor as ncod , deudor.nkey_deudor  ");
        cSQL.Append(" from deudor join codigodeudor ");
        cSQL.Append(" on (codigodeudor.nkey_cliente = @cod_nkey ");
        oParam.AddParameters("@cod_nkey", CodNkey, TypeSQL.Numeric);

        cSQL.Append(" and deudor.nkey_deudor = codigodeudor.nkey_deudor) ");
        

        if (!string.IsNullOrEmpty(pNombre))
        {
          cSQL.Append(" and deudor.sNombre like '%' + @nombre + '%' ");
          oParam.AddParameters("@nombre", pNombre, TypeSQL.Varchar);
        }
        if (!string.IsNullOrEmpty(pRUT))
        {
          cSQL.Append(" and codigodeudor.ncodigodeudor like '%' + @rut + '%'");
          oParam.AddParameters("@rut", pRUT, TypeSQL.Varchar);
        }
        if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
        {
          cSQL.Append("  and deudor.nkey_deudor = @nkey_usuario ");
          oParam.AddParameters("@nkey_usuario", lngNkeyUsuario, TypeSQL.Numeric);

        }
        if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "V"))
        {
          cSQL.Append("  and codigodeudor.nkey_vendedor = @nkey_usuario ");
          oParam.AddParameters("@nkey_usuario", lngNkeyUsuario, TypeSQL.Numeric);

        }
        cSQL.Append(" Order by deudor.sNombre ");

        dtData = oConn.Select(cSQL.ToString(), oParam);
        pError = oConn.Error;
        return dtData;
      }
      else
      {
        return null;
      }
    }

  }
}
