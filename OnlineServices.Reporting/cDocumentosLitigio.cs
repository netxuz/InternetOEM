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
          if (string.IsNullOrEmpty(lngCodDeudor))
          {
            cSQL.Append(" select Código, Razón_Social, Número_Factura, Vencimiento, Saldo, Monto_Litigio, ");
            cSQL.Append(" Fecha_Ingreso, Número_Solicitud, Fecha_Solicitud, (select sum(Saldo) from documentos_en_litigio where nkey_cliente =").Append(lngCodNkey);
            cSQL.Append(" and nkey_deudor = DL.nkey_deudor and Fecha_Ingreso between convert(datetime,'").Append(pDtFchIni).Append("') and convert(datetime,'").Append(pDtFchFin).Append("')) as tot_saldo, ");
            cSQL.Append(" (select sum(Monto_Litigio) from documentos_en_litigio where nkey_cliente = ").Append(lngCodNkey);
            cSQL.Append(" and nkey_deudor = DL.nkey_deudor and Fecha_Ingreso between convert(datetime,'").Append(pDtFchIni).Append("') and convert(datetime,'").Append(pDtFchFin).Append("')) as tot_litigio, ");
            if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
            {
              cSQL.Append(" (select sum(Saldo) from documentos_en_litigio where nkey_cliente = ").Append(lngCodNkey).Append(" and nkey_deudor = ").Append(lngNkeyUsuario).Append(" and Fecha_Ingreso between convert(datetime,'").Append(pDtFchIni).Append("') and convert(datetime,'").Append(pDtFchFin).Append("')) as total_saldo, ");
            }
            else
            {
              if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "V"))
              {
                cSQL.Append(" (select sum(Saldo) from documentos_en_litigio where nkey_cliente = ").Append(lngCodNkey).Append(" and nkey_deudor in (select nkey_deudor from codigodeudor  where nkey_vendedor = ").Append(lngNkeyUsuario).Append(" and nkey_cliente = ").Append(lngCodNkey).Append(" ) and Fecha_Ingreso between convert(datetime,'").Append(pDtFchIni).Append("') and convert(datetime,'").Append(pDtFchFin).Append("')) as total_saldo, ");
              }
              else
              {
                cSQL.Append(" (select sum(Saldo) from documentos_en_litigio where nkey_cliente = ").Append(lngCodNkey).Append(" and Fecha_Ingreso between convert(datetime,'").Append(pDtFchIni).Append("') and convert(datetime,'").Append(pDtFchFin).Append("')) as total_saldo, ");
              }
            }

            cSQL.Append(" (select sum(Monto_Litigio) from documentos_en_litigio where nkey_cliente = ").Append(lngCodNkey).Append(" and Fecha_Ingreso between convert(datetime,'").Append(pDtFchIni).Append("') and convert(datetime,'").Append(pDtFchFin).Append("') ");

            if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
            {
              cSQL.Append(" and nkey_deudor = ").Append(lngNkeyUsuario);
            }
            else
            {
              if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "V"))
                cSQL.Append(" and nkey_deudor in (select nkey_deudor from codigodeudor where nkey_cliente = ").Append(lngCodNkey).Append(" and nkey_vendedor = ").Append(lngNkeyUsuario).Append(" ) ");
            }

            cSQL.Append(") as total_litigio, Sobservacion ");
            cSQL.Append(" from documentos_en_litigio DL where nkey_cliente = ").Append(lngCodNkey).Append(" and Fecha_Ingreso between convert(datetime,'").Append(pDtFchIni).Append("') and convert(datetime,'").Append(pDtFchFin).Append("') ");

            if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
              cSQL.Append("  and DL.nkey_deudor = ").Append(lngNkeyUsuario);

            if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "V"))
              cSQL.Append("  and DL.nkey_deudor in (select codigodeudor.nkey_deudor from codigodeudor where codigodeudor.nkey_vendedor = ").Append(lngNkeyUsuario).Append(" and codigodeudor.nkey_cliente = DL.nkey_cliente) ");

            cSQL.Append(" order by Código, Número_Factura ");
          }
          else
          {
            cSQL.Append("select Código, Razón_Social, Número_Factura, Vencimiento, Saldo, Monto_Litigio, ");
            cSQL.Append(" Fecha_Ingreso, Número_Solicitud, Fecha_Solicitud, ");
            cSQL.Append(" (select sum(Saldo) from documentos_en_litigio where nkey_cliente = ").Append(lngCodNkey).Append(" and nkey_deudor = ").Append(lngCodDeudor).Append(" and Fecha_Ingreso between convert(datetime,'").Append(pDtFchIni).Append("') and convert(datetime,'").Append(pDtFchFin).Append("')) as total_saldo, ");
            cSQL.Append("	(select sum(Monto_Litigio) from documentos_en_litigio where nkey_cliente = ").Append(lngCodNkey).Append(" and nkey_deudor = ").Append(lngCodDeudor).Append(" and Fecha_Ingreso between convert(datetime,'").Append(pDtFchIni).Append("') and convert(datetime,'").Append(pDtFchFin).Append("')) as total_litigio , Sobservacion");
            cSQL.Append("	from documentos_en_litigio where nkey_cliente = ").Append(lngCodNkey).Append(" and nkey_deudor = ").Append(lngCodDeudor).Append(" and Fecha_Ingreso between convert(datetime,'").Append(pDtFchIni).Append("') and convert(datetime,'").Append(pDtFchFin).Append("') ");
            cSQL.Append(" order by Número_Factura ");
          }
        }
        else {
          cSQL.Append("select Código, Razón_Social, Número_Factura, Vencimiento, Saldo, Monto_Litigio, Fecha_Ingreso, Número_Solicitud, Fecha_Solicitud, ");
          cSQL.Append(" (select sum(Saldo) from documentos_en_litigio where nkey_cliente = ").Append(lngCodNkey).Append(" and Número_Factura = ").Append(lngNumFactura).Append(") as total_saldo, ");
          cSQL.Append(" (select sum(Monto_Litigio) from documentos_en_litigio where nkey_cliente = ").Append(lngCodNkey).Append(" and Número_Factura = ").Append(lngNumFactura).Append(") as total_litigio, Sobservacion  ");
          cSQL.Append(" from documentos_en_litigio where nkey_cliente = ").Append(lngCodNkey).Append(" and Número_Factura = ").Append(lngNumFactura);
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
