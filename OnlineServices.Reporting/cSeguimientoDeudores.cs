using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using OnlineServices.Conn;

namespace OnlineServices.Reporting
{
  public class cSeguimientoDeudores
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

    private string lngNumFactura;
    public string NumFactura { get { return lngNumFactura; } set { lngNumFactura = value; } }

    private bool blnIndAccion;
    public bool IndAccion { get { return blnIndAccion; } set { blnIndAccion = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private DBConn oConn;

    public cSeguimientoDeudores()
    {

    }

    public cSeguimientoDeudores(ref DBConn oConn)
    {
      this.oConn = oConn;
    }

    public DataTable Get()
    {
      DataTable dtData;

      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();

        if (blnIndAccion)
        {
          cSQL.Append("select ");

          if (!string.IsNullOrEmpty(pNcodHolding))
          {
            cSQL.Append(" ncod as 'ncodholding', codigo,  ");
          }
          else
          {
            if (string.IsNullOrEmpty(lngCodDeudor))
            {
              cSQL.Append(" codigo, ");
            }
          }

          cSQL.Append(" RazonSocial as 'Razón Social', Fecha as 'Fecha Gestión', TipoGestion as 'Tipo Gestión', Contacto, Para, Deudor, Analista, Observacion, CompromisoPago as 'Compromiso Pago', sum(montoprometido) as 'Monto Prometido' ");
          cSQL.Append(" from Vista_reporte_ultimas_gestiones_2  ");

          if (!string.IsNullOrEmpty(pNcodHolding))
          {
            cSQL.Append(" where ncodholding = ").Append(pNcodHolding);
          }
          else {
            if (!string.IsNullOrEmpty(lngCodDeudor))
            {
              cSQL.Append(" where nKey_Deudor = ").Append(lngCodDeudor);
            }
            else {
              cSQL.Append(" where nkey_cliente in (").Append(lngCodNkey).Append(") ");
            }
          }

          cSQL.Append(" and Fecha between convert(datetime,'").Append(pDtFchIni).Append("') and convert(datetime,'").Append(pDtFchFin).Append("') ");

          cSQL.Append(" group by ");

          if (!string.IsNullOrEmpty(pNcodHolding))
          {
            cSQL.Append(" ncod, codigo, ");
          }
          else {
            if (string.IsNullOrEmpty(lngCodDeudor)) {
              cSQL.Append(" codigo, ");
            }
          }
          cSQL.Append(" RazonSocial, Fecha, TipoGestion, Contacto, Para, Deudor, Analista, Observacion, CompromisoPago, montoprometido");
          cSQL.Append(" order by Fecha ");

        }
        else
        {
          cSQL.Append("select ");

          if (!string.IsNullOrEmpty(pNcodHolding))
          {
            cSQL.Append(" ncod as 'ncodholding', codigo,  ");
          }
          else
          {
            if (string.IsNullOrEmpty(lngCodDeudor))
            {
              cSQL.Append(" codigo, ");
            }
          }
          cSQL.Append(" RazonSocial as 'Razón Social', Fecha as 'Fecha Gestión', TipoGestion as 'Tipo Gestión', Contacto, Para, Deudor, Analista, Observacion, CompromisoPago as 'Compromiso Pago', sum(montoprometido) as 'Monto Prometido' ");
          cSQL.Append(" from Vista_reporte_ultimas_gestiones_4  ");

          if (!string.IsNullOrEmpty(pNcodHolding))
          {
            cSQL.Append(" where ncodholding = ").Append(pNcodHolding);
          }
          else {
            cSQL.Append(" where nkey_cliente in(").Append(lngCodNkey).Append(") ");
          }                    
          
          cSQL.Append(" and nnumerofactura = ").Append(lngNumFactura);


          cSQL.Append(" group by ");

          if (!string.IsNullOrEmpty(pNcodHolding))
          {
            cSQL.Append(" ncod, codigo, ");
          }
          else
          {
            if (string.IsNullOrEmpty(lngCodDeudor))
            {
              cSQL.Append(" codigo, ");
            }
          }

          cSQL.Append(" RazonSocial, Fecha, TipoGestion, Contacto, Para, Deudor, Analista, Observacion, CompromisoPago, montoprometido ");
          cSQL.Append(" order by Fecha ");

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
