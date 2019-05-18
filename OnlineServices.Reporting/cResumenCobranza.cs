using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using OnlineServices.Conn;

namespace OnlineServices.Reporting
{
  public class cResumenCobranza
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

    private string pNcodHolding;
    public string NcodHolding { get { return pNcodHolding; } set { pNcodHolding = value; } }

    private string pDtFchIni;
    public string DtFchIni { get { return pDtFchIni; } set { pDtFchIni = value; } }

    private string pDtFchFin;
    public string DtFchFin { get { return pDtFchFin; } set { pDtFchFin = value; } }

    private string pNumPago;
    public string NumPago { get { return pNumPago; } set { pNumPago = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private DBConn oConn;

    public cResumenCobranza()
    {

    }

    public cResumenCobranza(ref DBConn oConn)
    {
      this.oConn = oConn;
    }

    public DataTable Get()
    {
      oParam = new DBConn.SQLParameters(10);
      DataTable dtData;

      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();
        cSQL.Append("select Código, resumen_cobranza.nKey_Deudor, RazónSocial, Zona_Cobranza, Tipo_Pago, Número_Pago, isnull(Monto_Pago,0) as 'Monto_Pago', ");
        cSQL.Append(" resumen_cobranza.ctabancaria,resumen_cobranza.ctacontable, cliente.ncod, cliente.ncodholding , cliente.holding, ");
        cSQL.Append("Emisión_Pago, Banco, Plaza, Sala, Tipo_Documento_Aplicado, Nº_Documento_Aplicado, isnull(Monto, 0) as 'Monto', isnull(Abono,0) as 'Abono', isnull(Saldo,0) as 'Saldo', Observación, ");
        cSQL.Append("(select sum(isnull(Monto_pago,0)) from resumen_cobranza where nkey_cliente in (");
        cSQL.Append(lngCodNkey);
        cSQL.Append(") ");

        if (!string.IsNullOrEmpty(pNumPago)) {
          cSQL.Append("and Número_Pago = @numpago ");
          oParam.AddParameters("@numpago", pNumPago, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(lngCodDeudor))
        {
          cSQL.Append("and nKey_Deudor = @lngCodDeudor ");
        }

        if ((!string.IsNullOrEmpty(sTipoUsuario))&&(sTipoUsuario == "D")) {
          cSQL.Append("  and resumen_cobranza.nkey_deudor = @nkey_usuario ");
        }

        if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "V"))
        {
          cSQL.Append("  and resumen_cobranza.nkey_deudor in (select codigodeudor.nkey_deudor from codigodeudor where codigodeudor.nkey_vendedor = @nkey_usuario and codigodeudor.nkey_cliente in(");
          cSQL.Append(lngCodNkey);
          cSQL.Append(") )  ");
        }

        cSQL.Append(" and Recepcion between convert(datetime, '").Append(pDtFchIni).Append("') and convert(datetime, '").Append(pDtFchFin).Append("')) as total_monto, ");
        cSQL.Append(" (select sum(isnull(Abono,0)) from resumen_cobranza where nkey_cliente in(");
        cSQL.Append(lngCodNkey);
        cSQL.Append(") ");

        if (!string.IsNullOrEmpty(lngCodDeudor)){
          cSQL.Append(" and nKey_Deudor = @lngCodDeudor ");
        }

        if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
        {
          cSQL.Append(" and resumen_cobranza.nkey_deudor = @nkey_usuario ");
        }

        if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "V")) {
          cSQL.Append(" and resumen_cobranza.nkey_deudor in (select codigodeudor.nkey_deudor from codigodeudor where codigodeudor.nkey_vendedor = @nkey_usuario and codigodeudor.nkey_cliente in(");
          cSQL.Append(lngCodNkey);
          cSQL.Append(") )  ");
        }

        cSQL.Append(" and Recepcion between convert(datetime, '").Append(pDtFchIni).Append("') and convert(datetime,'").Append(pDtFchFin).Append("')) as total_abono, ");
        cSQL.Append(" (select sum(isnull(Saldo,0)) from resumen_cobranza where nkey_cliente in(");
        cSQL.Append(lngCodNkey);
        cSQL.Append(") ");
        
        if (!string.IsNullOrEmpty(lngCodDeudor)){
          cSQL.Append(" and nKey_Deudor = @lngCodDeudor ");
        }
        if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D")) {
          cSQL.Append(" and resumen_cobranza.nkey_deudor = @nkey_usuario ");
        }
        if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "V"))
        {
          cSQL.Append(" and resumen_cobranza.nkey_deudor in (select codigodeudor.nkey_deudor from codigodeudor where codigodeudor.nkey_vendedor = @nkey_usuario and codigodeudor.nkey_cliente in(");
          cSQL.Append(lngCodNkey);
          cSQL.Append(") ) ");
        }
        cSQL.Append(" and Recepcion between convert(datetime, '").Append(pDtFchIni).Append("') and convert(datetime, '").Append(pDtFchFin).Append("')) as total_saldo, ");
        cSQL.Append(" (select sum(isnull(Monto_Pago,0)) from resumen_cobranza where nkey_cliente in(");
        cSQL.Append(lngCodNkey);
        cSQL.Append(") ");
        
        if (!string.IsNullOrEmpty(lngCodDeudor)){
          cSQL.Append(" and nKey_Deudor = @lngCodDeudor ");
        }
        if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
        {
          cSQL.Append(" and resumen_cobranza.nkey_deudor = @nkey_usuario ");
        }
        if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "V"))
        {
          cSQL.Append(" and resumen_cobranza.nkey_deudor in (select codigodeudor.nkey_deudor from codigodeudor where codigodeudor.nkey_vendedor = @nkey_usuario and codigodeudor.nkey_cliente in(");
          cSQL.Append(lngCodNkey);
          cSQL.Append(") ) ");
        }
        cSQL.Append(" and Recepcion between convert(datetime, '").Append(pDtFchIni).Append("') and convert(datetime, '").Append(pDtFchFin).Append("')) as total_pago ");
        cSQL.Append(" from resumen_cobranza ");

        cSQL.Append(" join cliente on (cliente.nkey_cliente = resumen_cobranza.nKey_Cliente ) ");

        if (string.IsNullOrEmpty(pNcodHolding))
        {
          cSQL.Append(" where resumen_cobranza.nKey_Cliente in (");
          cSQL.Append(lngCodNkey);
          cSQL.Append(")");
        }
        else {
          cSQL.Append(" where cliente.ncodholding  = @ncodholding ");
          oParam.AddParameters("@ncodholding", pNcodHolding, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(lngCodDeudor)){
          cSQL.Append(" and resumen_cobranza.nKey_Deudor = @lngCodDeudor ");
        }

        if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
        {
          cSQL.Append(" and resumen_cobranza.nkey_deudor = @nkey_usuario ");
        }
        if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "V"))
        {
          cSQL.Append(" and resumen_cobranza.nkey_deudor in (select codigodeudor.nkey_deudor from codigodeudor where codigodeudor.nkey_vendedor = @nkey_usuario and codigodeudor.nkey_cliente in(");
          cSQL.Append(lngCodNkey);
          cSQL.Append(") ) ");
        }

        cSQL.Append(" and Recepcion between convert(datetime, '").Append(pDtFchIni).Append("') and convert(datetime, '").Append(pDtFchFin).Append("') ");
        cSQL.Append(" order by Código, Número_Pago, [ORDER], Numero_Factura, ORDER2 ");

        //oParam.AddParameters("@cod_nkey", lngCodNkey, TypeSQL.Numeric);

        if (!string.IsNullOrEmpty(sTipoUsuario))
          oParam.AddParameters("@nkey_usuario", lngNkeyUsuario, TypeSQL.Numeric);

        if (!string.IsNullOrEmpty(lngCodDeudor))
          oParam.AddParameters("@lngCodDeudor", lngCodDeudor, TypeSQL.Numeric);        

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
