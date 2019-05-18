using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using OnlineServices.Conn;

namespace OnlineServices.Antalis
{
  public class cAntUsrTiposPago
  {
    DBConn.SQLParameters oParam;
    DBConn.DataTypeSQL TypeSQL = new DBConn.DataTypeSQL();

    private string pCodUsuario;
    public string CodUsuario { get { return pCodUsuario; } set { pCodUsuario = value; } }

    private string pTipoPago;
    public string TipoPago { get { return pTipoPago; } set { pTipoPago = value; } }

    private string pAccion;
    public string Accion { get { return pAccion; } set { pAccion = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private DBConn oConn;

    public cAntUsrTiposPago() {

    }

    public cAntUsrTiposPago(ref DBConn oConn) {
      this.oConn = oConn;
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
        cSQL.Append("select cod_user, tipo_pago from ant_usr_tipos_de_pago ");

        if (!string.IsNullOrEmpty(pCodUsuario))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_user = @cod_user ");
          oParam.AddParameters("@cod_user", pCodUsuario, TypeSQL.Numeric);
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

    public void Put()
    {
      oParam = new DBConn.SQLParameters(20);
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
              cSQL.Append("insert into ant_usr_tipos_de_pago(cod_user, tipo_pago) values(@cod_usuario, @tipo_pago)");
              oParam.AddParameters("@cod_usuario", pCodUsuario, TypeSQL.Numeric);
              oParam.AddParameters("@tipo_pago", pTipoPago, TypeSQL.Numeric);
              oConn.Insert(cSQL.ToString(), oParam);
              break;

            case "ELIMINAR":
              cSQL = new StringBuilder();
              cSQL.Append("delete from ant_usr_tipos_de_pago where cod_user = @cod_usuario");
              oParam.AddParameters("@cod_usuario", pCodUsuario, TypeSQL.Numeric);
              oConn.Delete(cSQL.ToString(), oParam);
              break;
          }
        }
        catch (Exception ex)
        {
          pError = ex.Message;
        }
      }
    }

  }

}
