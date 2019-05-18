using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using OnlineServices.Conn;

namespace OnlineServices.Reporting
{
  public class cDocumentosLitigio
  {
    private string lngCodNkey;
    public string CodNkey { get { return lngCodNkey; } set { lngCodNkey = value; } }

    private string lngCodDeudor;
    public string CodDeudor { get { return lngCodDeudor; } set { lngCodDeudor = value; } }

    private string lngNumFactura;
    public string NumFactura { get { return lngNumFactura; } set { lngNumFactura = value; } }

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

    private bool bIndAccion;
    public bool IndAccion { get { return bIndAccion; } set { bIndAccion = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private DBConn oConn;

    public cDocumentosLitigio() {

    }

    public cDocumentosLitigio(ref DBConn oConn) {
      this.oConn = oConn;
    }

    public DataTable Get()
    {
      DataTable dtData;

      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();

        if (bIndAccion)
        {
          cSQL.Append("select ");

          if (!string.IsNullOrEmpty(pNcodHolding))
          {
            cSQL.Append(" ncodholding, Código,  ");
          }
          else
          {
            if (string.IsNullOrEmpty(lngCodDeudor))
            {
              cSQL.Append(" Código, ");
            }
          }

          cSQL.Append(" [Razón Social], [Número Factura], Vencimiento, Saldo, MontoLitigio, [Número Solicitud], [Fecha Solicitud], numNC, montoNC, Observacion, SubMotivo, SDescripcion, NomContacto ");
          cSQL.Append(" from vista_documentos_litigio  ");
          cSQL.Append(" where FechaIngreso between convert(datetime,'").Append(pDtFchIni).Append("') and convert(datetime,'").Append(pDtFchFin).Append("') ");

          if (!string.IsNullOrEmpty(pNcodHolding))
          {
            cSQL.Append(" and ncodholding = ").Append(pNcodHolding);
          }
          else
          {
            if (!string.IsNullOrEmpty(lngCodDeudor))
            {
              cSQL.Append(" and nkey_deudor = ").Append(lngCodDeudor);
            }
            else
            {
              cSQL.Append(" and nkey_cliente in (").Append(lngCodNkey).Append(") ");
            }
          }

          cSQL.Append(" Order by [Razón Social], nkey_deudor, [Fecha Solicitud], Cliente, [Número Factura] ");
        }
        else {

          cSQL.Append("select ");

          if (!string.IsNullOrEmpty(pNcodHolding))
          {
            cSQL.Append(" ncodholding, Código,  ");
          }
          else
          {
            cSQL.Append(" Código, ");
          }

          cSQL.Append(" [Razón Social], [Número Factura], Vencimiento, Saldo, MontoLitigio, [Número Solicitud], [Fecha Solicitud], numNC, montoNC, Observacion, SubMotivo, SDescripcion, NomContacto ");
          cSQL.Append(" from vista_documentos_litigio  ");
          cSQL.Append(" where FechaIngreso between convert(datetime,'").Append(pDtFchIni).Append("') and convert(datetime,'").Append(pDtFchFin).Append("') ");

          if (!string.IsNullOrEmpty(lngNumFactura)) {
            cSQL.Append(" and [Número Factura]  = ").Append(lngNumFactura);
          }

          if (!string.IsNullOrEmpty(pNcodHolding))
          {
            cSQL.Append(" and ncodholding = ").Append(pNcodHolding);
          }
          else {
            cSQL.Append(" and nkey_cliente in (").Append(lngCodNkey).Append(") ");
          }        

          cSQL.Append(" Order by [Razón Social], CodCliente, [Fecha Solicitud], Cliente, [Número Factura] ");
        }


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
