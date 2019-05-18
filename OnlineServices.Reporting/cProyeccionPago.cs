using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using OnlineServices.Conn;

namespace OnlineServices.Reporting
{
  public class cProyeccionPago
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

    public cProyeccionPago() {

    }

    public cProyeccionPago(ref DBConn oConn) {
      this.oConn = oConn;
    }

    public DataTable Get()
    {
      DataTable dtData;

      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();
        cSQL.Append("select ProyeccionPagoWeb.nKey_Cliente, ProyeccionPagoWeb.nKey_Deudor, nNumeroFactura, dFechaEmision, dFechaVencimiento, nMontoFactura, nAtraso, cliente.ncod, cliente.ncodholding , cliente.holding, ");
        cSQL.Append(" (select sum(nMontoFactura) from ProyeccionPagoWeb where nKey_Cliente in(").Append(lngCodNkey).Append(")");

        if (!string.IsNullOrEmpty(lngCodDeudor))
          cSQL.Append(" and nKey_Deudor = ").Append(lngCodDeudor);

        if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
          cSQL.Append("  and ProyeccionPagoWeb.nkey_deudor = ").Append(lngNkeyUsuario);
        if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "V"))
          cSQL.Append("  and ProyeccionPagoWeb.nkey_deudor in (select codigodeudor.nkey_deudor from codigodeudor where codigodeudor.nkey_vendedor = ").Append(lngNkeyUsuario).Append(" and codigodeudor.nkey_cliente = ").Append(lngCodNkey).Append(" )  ");

        cSQL.Append(" and dFechaVencimiento between convert(datetime,'").Append(pDtFchIni).Append("') and convert(datetime,'").Append(pDtFchFin).Append("')) as tot_factura ");
        cSQL.Append(" from proyeccionpagoweb ");
        cSQL.Append(" join cliente on (cliente.nkey_cliente = proyeccionpagoweb.nKey_Cliente ) ");
        cSQL.Append("  where proyeccionpagoweb.nkey_cliente = cliente.nKey_Cliente ");

        if (string.IsNullOrEmpty(pNcodHolding))
          cSQL.Append(" and proyeccionpagoweb.nKey_Cliente in(").Append(lngCodNkey).Append(")");
        else
          cSQL.Append(" and cliente.ncodholding  = ").Append(pNcodHolding);

        if (!string.IsNullOrEmpty(lngCodDeudor))
          cSQL.Append(" and nKey_Deudor = ").Append(lngCodDeudor);

        if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
          cSQL.Append("  and ProyeccionPagoWeb.nkey_deudor = ").Append(lngNkeyUsuario);

        if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "V"))
          cSQL.Append("  and ProyeccionPagoWeb.nkey_deudor in (select codigodeudor.nkey_deudor from codigodeudor where codigodeudor.nkey_vendedor = ").Append(lngNkeyUsuario).Append(" and codigodeudor.nkey_cliente = ").Append(lngCodNkey).Append(" )  ");

        cSQL.Append(" and dFechaVencimiento between convert(datetime,'").Append(pDtFchIni).Append("') and convert(datetime,'").Append(pDtFchFin).Append("') ");
        cSQL.Append(" order by dFechaVencimiento, nNumeroFactura ");

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
