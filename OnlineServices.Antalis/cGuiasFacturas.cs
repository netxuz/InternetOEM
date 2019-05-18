using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using OnlineServices.Conn;

namespace OnlineServices.Antalis
{
  public class cGuiasFacturas
  {
    DBConn.SQLParameters oParam;
    DBConn.DataTypeSQL TypeSQL = new DBConn.DataTypeSQL();

    private string pNKeyCliente;
    public string NKeyCliente { get { return pNKeyCliente; } set { pNKeyCliente = value; } }

    private string pNKeyDeudor;
    public string NKeyDeudor { get { return pNKeyDeudor; } set { pNKeyDeudor = value; } }

    private string pNCodigoDeudor; //CODIGO SAP
    public string NCodigoDeudor { get { return pNCodigoDeudor; } set { pNCodigoDeudor = value; } }

    private string pGuiaDespacho;
    public string GuiaDespacho { get { return pGuiaDespacho; } set { pGuiaDespacho = value; } }

    private string pNkeyFactura;
    public string NkeyFactura { get { return pNkeyFactura; } set { pNkeyFactura = value; } }

    private string pNumeroFactura;
    public string NumeroFactura { get { return pNumeroFactura; } set { pNumeroFactura = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private DBConn oConn;

    public cGuiasFacturas()
    {

    }

    public cGuiasFacturas(ref DBConn oConn)
    {
      this.oConn = oConn;
    }

    public DataTable GetGuiaDespacho()
    {
      oParam = new DBConn.SQLParameters(2);
      DataTable dtData;
      StringBuilder cSQL;

      if (oConn.bIsOpen)
      {
        cSQL = new StringBuilder();
        cSQL.Append("Select distinct(guiadespacho) from vista_saldo_factura where nkey_cliente = @nkeycliente and nkey_deudor in(select nkey_deudor from codigodeudor where ncodigodeudor = @ncodigodeudor and nKey_Cliente = @nkeycliente) ");
        cSQL.Append(" and not nNumeroFactura in(select num_factura from ant_factura where saldo_factura <= 0 ) ");
        cSQL.Append(" and saldo > 0 ");
        cSQL.Append(" and ctatipofact = 2 ");
        cSQL.Append(" and GuiaDespacho <> 0 ");
        oParam.AddParameters("@nkeycliente", pNKeyCliente, TypeSQL.Numeric);
        oParam.AddParameters("@ncodigodeudor", pNCodigoDeudor, TypeSQL.Varchar);

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

    public DataTable GetFacturas()
    {
      oParam = new DBConn.SQLParameters(2);
      DataTable dtData;
      StringBuilder cSQL;

      if (oConn.bIsOpen)
      {
        cSQL = new StringBuilder();
        cSQL.Append("Select a.nkey_cliente, a.nnumerofactura, a.saldo as nmontofactura, isnull((select saldo_factura from ant_factura where num_factura = a.nnumerofactura),0) saldo ");
        cSQL.Append(" from vista_saldo_factura a where a.guiadespacho = @guidespacho ");
        cSQL.Append(" and not a.nnumerofactura in(select num_factura from ant_factura where saldo_factura <= 0 ) ");
        cSQL.Append(" and a.nkey_cliente = @nkeycliente ");
        cSQL.Append(" and a.saldo > 0 ");
        cSQL.Append(" and a.ctatipofact = 2 ");
        oParam.AddParameters("@guidespacho", pGuiaDespacho, TypeSQL.Numeric);
        oParam.AddParameters("@nkeycliente", pNKeyCliente, TypeSQL.Numeric);

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

    public DataTable getFactura()
    {
      oParam = new DBConn.SQLParameters(3);
      DataTable dtData;
      StringBuilder cSQL;
      string Condicion = " and ";

      if (oConn.bIsOpen)
      {
        cSQL = new StringBuilder();
        cSQL.Append("select * from vista_saldo_factura where saldo > 0 and ctatipofact = 2 ");

        if (!string.IsNullOrEmpty(pNumeroFactura))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" nNumeroFactura = @nNumeroFactura ");
          oParam.AddParameters("@nNumeroFactura", pNumeroFactura, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(pNKeyCliente))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" nkey_cliente = @nkey_cliente ");
          oParam.AddParameters("@nkey_cliente", pNKeyCliente, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(pGuiaDespacho))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" GuiaDespacho = @GuiaDespacho ");
          oParam.AddParameters("@GuiaDespacho", pGuiaDespacho, TypeSQL.Numeric);
        }

        dtData = oConn.Select(cSQL.ToString(), oParam);
        pError = oConn.Error;
        return dtData;

      }
      else {
        pError = "Conexion Cerrada";
        return null;
      }
    }
  }
}
