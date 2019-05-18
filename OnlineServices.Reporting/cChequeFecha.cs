using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using OnlineServices.Conn;

namespace OnlineServices.Reporting
{
  public class cChequeFecha
  {
    private string lngCodNkey;
    public string CodNkey { get { return lngCodNkey; } set { lngCodNkey = value; } }

    private string lngCodDeudor;
    public string CodDeudor { get { return lngCodDeudor; } set { lngCodDeudor = value; } }

    private string sTipoUsuario;
    public string TipoUsuario { get { return sTipoUsuario; } set { sTipoUsuario = value; } }

    private string lngNkeyUsuario;
    public string NkeyUsuario { get { return lngNkeyUsuario; } set { lngNkeyUsuario = value; } }

    private string pNcodHolding;
    public string NcodHolding { get { return pNcodHolding; } set { pNcodHolding = value; } }

    private string pDtFchIni;
    public string DtFchIni { get { return pDtFchIni; } set { pDtFchIni = value; } }

    private string pDtFchFin;
    public string DtFchFin { get { return pDtFchFin; } set { pDtFchFin = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private DBConn oConn;

    public cChequeFecha()
    {

    }

    public cChequeFecha(ref DBConn oConn)
    {
      this.oConn = oConn;
    }

    public DataTable Get()
    {
      DataTable dtData;
      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();

        cSQL.Append("select cliente.snombre as 'NomCliente' ,codigodeudor.ncodigodeudor as 'CodDeudor' ,deudor.snombre as 'Nomdeudor' ");
        cSQL.Append(" ,aplicacion.nAbonoFactura as 'MontoPago' ,pago.dfecharecepcion as 'FechaPago' ,factura.nNumeroFactura as 'NumFactura' ");
        cSQL.Append(" ,factura.nMontoFactura as 'MontoFactura' ,factura.dfechavencimiento as 'FechaVencFac' ,datediff(DAY,factura.dfechavencimiento,pago.dfechavencimiento) as 'DiasExceso' ");
        cSQL.Append(" ,pago.dfechavencimiento as 'VencCheque' ");
        cSQL.Append(" ,vendedor.snombre as 'NomVendedor', pago.nnumeropago as 'Numpago', pago.nmontopago as 'Monpago', ");
        cSQL.Append(" isnull(pago.snumerocuentabancariacliente,' ' ) as 'ctabancaria',");
        cSQL.Append(" isnull(CuentaBancariaCliente.snumerocuentacontable,' ') as 'ctacontable', ");
        cSQL.Append(" cliente.ncod, cliente.ncodholding , cliente.holding ");
        //cSQL.Append(" from pago join cliente on (cliente.nkey_cliente = ").Append(lngCodNkey).Append(") ");
        cSQL.Append(" from pago ");
        cSQL.Append(" join cliente on (cliente.nkey_cliente = pago.nkey_cliente ) ");

        cSQL.Append(" join aplicacion on (aplicacion.nkey_pago = pago.nkey_pago) join factura on (factura.nkey_factura = aplicacion.nkey_factura) ");
        cSQL.Append(" join deudor on (deudor.nkey_deudor = pago.nkey_deudor) join codigodeudor on (codigodeudor.nkey_cliente = pago.nkey_cliente and codigodeudor.nkey_deudor = pago.nkey_deudor) ");
        cSQL.Append(" join servicio on (servicio.nkey_cliente = pago.nkey_cliente and servicio.nkey_deudor = pago.nkey_deudor and servicio.nkey_analista <> 4) ");
        cSQL.Append(" left join vendedor on (vendedor.nkey_cliente = pago.nkey_cliente  and vendedor.nkey_vendedor = codigodeudor.nkey_vendedor) ");

        cSQL.Append(" left join CuentaBancariaCliente on (CuentaBancariaCliente.nKey_Cliente = pago.nKey_Cliente and CuentaBancariaCliente.nKey_Banco = pago.nKey_Banco and CuentaBancariaCliente.sNumeroCuentaBancaria = pago.sNumeroCuentaBancariaCliente)  ");

        //cSQL.Append(" where pago.nkey_cliente in(").Append(lngCodNkey).Append(") ");

        if (string.IsNullOrEmpty(pNcodHolding))
        {
          cSQL.Append(" where cliente.nkey_cliente in(").Append(lngCodNkey).Append(")");
        }
        else
        {
          cSQL.Append("	where cliente.ncodholding = ").Append(pNcodHolding);
        }

        if (!string.IsNullOrEmpty(lngCodDeudor))
          cSQL.Append(" and pago.nkey_deudor = ").Append(lngCodDeudor);

        cSQL.Append(" and stipopago = 'Cheque'  ");
        cSQL.Append(" and pago.dfechavencimiento > factura.dfechavencimiento and pago.dfecharecepcion between convert(datetime,'").Append(pDtFchIni).Append("') and convert(datetime,'").Append(pDtFchFin).Append("') ");
        cSQL.Append(" and pago.dfechavencimiento > pago.dfecharecepcion ");

        if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
          cSQL.Append("  and pago.nkey_deudor = ").Append(lngNkeyUsuario);

        if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "V"))
          cSQL.Append("  and vendedor.nkey_vendedor = ").Append(lngNkeyUsuario);

        cSQL.Append("   order by datediff(DAY,factura.dfechavencimiento,pago.dfechavencimiento) desc   ");

        dtData = oConn.Select(cSQL.ToString());
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
