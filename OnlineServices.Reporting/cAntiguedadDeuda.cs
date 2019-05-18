using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using OnlineServices.Conn;

namespace OnlineServices.Reporting
{
  public class cAntiguedadDeuda
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

    private string pEstado;
    public string Estado { get { return pEstado; } set { pEstado = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private DBConn oConn;

    public cAntiguedadDeuda() {

    }

    public cAntiguedadDeuda(ref DBConn oConn) {
      this.oConn = oConn;
    }

    public DataTable Get()
    {
      DataTable dtData;

      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();

        if ((!string.IsNullOrEmpty(pEstado)) && (pEstado == "0"))
        {

          cSQL.Append("select  AntiguedadDeudaWeb.stipodocumento, AntiguedadDeudaWeb.nnumerofactura, ");
          cSQL.Append(" sum(AntiguedadDeudaWeb.nSaldo) as 'Total', ");
          cSQL.Append(" isnull((select sum(web.nSaldo) from AntiguedadDeudaWeb web where Web.nkey_cliente in(").Append(lngCodNkey).Append(") ");

          if (!string.IsNullOrEmpty(lngCodDeudor))
            cSQL.Append(" and web.nkey_deudor =").Append(lngCodDeudor);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
            cSQL.Append("  and web.nkey_deudor = ").Append(lngNkeyUsuario);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "V"))
            cSQL.Append("  and web.nkey_deudor  in (select nkey_deudor from codigodeudor where nkey_cliente in(").Append(lngCodNkey).Append(") and nkey_vendedor = ").Append(lngNkeyUsuario).Append(") ");

          cSQL.Append(" and web.natraso = '0' and web.nnumerofactura = AntiguedadDeudaWeb.nnumerofactura and Web.stipodocumento=AntiguedadDeudaWeb.stipodocumento ),0) as 'total_0', ");
          cSQL.Append(" isnull((select sum(web.nSaldo) from AntiguedadDeudaWeb web where Web.nkey_cliente in(").Append(lngCodNkey).Append(") ");

          if (!string.IsNullOrEmpty(lngCodDeudor))
            cSQL.Append(" and web.nkey_deudor =").Append(lngCodDeudor);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
            cSQL.Append("  and web.nkey_deudor = ").Append(lngNkeyUsuario);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "V"))
            cSQL.Append("  and web.nkey_deudor  in (select nkey_deudor from codigodeudor where nkey_cliente in(").Append(lngCodNkey).Append(") and nkey_vendedor = ").Append(lngNkeyUsuario).Append(") ");

          cSQL.Append(" and web.natraso = '15' and web.nnumerofactura = AntiguedadDeudaWeb.nnumerofactura and Web.stipodocumento=AntiguedadDeudaWeb.stipodocumento ),0) as 'total_15', ");
          cSQL.Append(" isnull((select sum(web.nSaldo) from AntiguedadDeudaWeb web where Web.nkey_cliente in(").Append(lngCodNkey).Append(") ");

          if (!string.IsNullOrEmpty(lngCodDeudor))
            cSQL.Append(" and web.nkey_deudor =").Append(lngCodDeudor);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
            cSQL.Append("  and web.nkey_deudor = ").Append(lngNkeyUsuario);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "V"))
            cSQL.Append("  and web.nkey_deudor  in (select nkey_deudor from codigodeudor where nkey_cliente in(").Append(lngCodNkey).Append(") and nkey_vendedor = ").Append(lngNkeyUsuario).Append(") ");

          cSQL.Append(" and web.natraso = '30' and web.nnumerofactura = AntiguedadDeudaWeb.nnumerofactura and Web.stipodocumento=AntiguedadDeudaWeb.stipodocumento ),0) as 'total_30', ");
          cSQL.Append(" isnull((select sum(web.nSaldo) from AntiguedadDeudaWeb web where Web.nkey_cliente in(").Append(lngCodNkey).Append(") ");

          if (!string.IsNullOrEmpty(lngCodDeudor))
            cSQL.Append(" and web.nkey_deudor =").Append(lngCodDeudor);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
            cSQL.Append("  and web.nkey_deudor = ").Append(lngNkeyUsuario);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "V"))
            cSQL.Append("  and web.nkey_deudor  in (select nkey_deudor from codigodeudor where nkey_cliente in(").Append(lngCodNkey).Append(") and nkey_vendedor = ").Append(lngNkeyUsuario).Append(") ");

          cSQL.Append(" and web.natraso = '60' and web.nnumerofactura = AntiguedadDeudaWeb.nnumerofactura and Web.stipodocumento=AntiguedadDeudaWeb.stipodocumento ),0) as 'total_60', ");
          cSQL.Append(" isnull((select sum(web.nSaldo) from AntiguedadDeudaWeb web where Web.nkey_cliente in(").Append(lngCodNkey).Append(") ");

          if (!string.IsNullOrEmpty(lngCodDeudor))
            cSQL.Append(" and web.nkey_deudor =").Append(lngCodDeudor);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
            cSQL.Append("  and web.nkey_deudor = ").Append(lngNkeyUsuario);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "V"))
            cSQL.Append("  and web.nkey_deudor  in (select nkey_deudor from codigodeudor where nkey_cliente in(").Append(lngCodNkey).Append(") and nkey_vendedor = ").Append(lngNkeyUsuario).Append(") ");

          cSQL.Append(" and web.natraso = '90' and web.nnumerofactura = AntiguedadDeudaWeb.nnumerofactura and Web.stipodocumento=AntiguedadDeudaWeb.stipodocumento ),0) as 'total_90', ");

          cSQL.Append(" isnull((select sum(web.nSaldo) from AntiguedadDeudaWeb web where Web.nkey_cliente in(").Append(lngCodNkey).Append(") ");

          if (!string.IsNullOrEmpty(lngCodDeudor))
            cSQL.Append(" and web.nkey_deudor =").Append(lngCodDeudor);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
            cSQL.Append("  and web.nkey_deudor = ").Append(lngNkeyUsuario);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "V"))
            cSQL.Append("  and web.nkey_deudor  in (select nkey_deudor from codigodeudor where nkey_cliente in(").Append(lngCodNkey).Append(") and nkey_vendedor = ").Append(lngNkeyUsuario).Append(") ");

          cSQL.Append(" and web.natraso = '180' and web.nnumerofactura = AntiguedadDeudaWeb.nnumerofactura and Web.stipodocumento=AntiguedadDeudaWeb.stipodocumento ),0) as 'total_180', ");
          cSQL.Append(" isnull((select sum(web.nSaldo) from AntiguedadDeudaWeb web where Web.nkey_cliente in(").Append(lngCodNkey).Append(") ");

          if (!string.IsNullOrEmpty(lngCodDeudor))
            cSQL.Append(" and web.nkey_deudor = ").Append(lngCodDeudor);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
            cSQL.Append("  and web.nkey_deudor = ").Append(lngNkeyUsuario);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "V"))
            cSQL.Append("  and web.nkey_deudor  in (select nkey_deudor from codigodeudor where nkey_cliente in(").Append(lngCodNkey).Append(") and nkey_vendedor = ").Append(lngNkeyUsuario).Append(") ");

          cSQL.Append(" and web.natraso = '360' and web.nnumerofactura = AntiguedadDeudaWeb.nnumerofactura and Web.stipodocumento=AntiguedadDeudaWeb.stipodocumento ),0) as 'total_360' ");
          cSQL.Append(" from AntiguedadDeudaWeb join codigodeudor on (codigodeudor.nkey_cliente = AntiguedadDeudaWeb.nkey_cliente and codigodeudor.nkey_deudor = AntiguedadDeudaWeb.nkey_deudor) ");
          cSQL.Append(" join deudor on (deudor.nkey_deudor = codigodeudor.nkey_deudor) ");
          cSQL.Append(" where AntiguedadDeudaWeb.nkey_cliente in(").Append(lngCodNkey).Append(") ");

          if (!string.IsNullOrEmpty(lngCodDeudor))
            cSQL.Append(" and AntiguedadDeudaWeb.nkey_deudor = ").Append(lngCodDeudor);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
            cSQL.Append("  and web.nkey_deudor = ").Append(lngNkeyUsuario);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "V"))
            cSQL.Append("  and web.nkey_deudor  in (select nkey_deudor from codigodeudor where nkey_cliente in(").Append(lngCodNkey).Append(") and nkey_vendedor = ").Append(lngNkeyUsuario).Append(") ");

          cSQL.Append(" group by AntiguedadDeudaWeb.stipodocumento,AntiguedadDeudaWeb.nnumerofactura ");
          cSQL.Append(" Order by sum(AntiguedadDeudaWeb.nSaldo) desc ");

        }
        else if ((!string.IsNullOrEmpty(pEstado)) && (pEstado == "1"))
        {
          cSQL.Append(" select  CodigoDeudor.ncodigodeudor, deudor.snombre,sum(AntiguedadDeudaWeb.nSaldo) as 'Total', ");
          cSQL.Append(" isnull((select sum(web.nSaldo) from AntiguedadDeudaWeb web where Web.nkey_cliente in(").Append(lngCodNkey).Append(")  and web.nkey_deudor = AntiguedadDeudaWeb.nKEy_Deudor and web.natraso = '0'),0) as 'total_0', ");
          cSQL.Append(" isnull((select sum(web.nSaldo) from AntiguedadDeudaWeb web where Web.nkey_cliente in(").Append(lngCodNkey).Append(")  and web.nkey_deudor = AntiguedadDeudaWeb.nKEy_Deudor and web.natraso = '15'),0) as 'total_15', ");
          cSQL.Append(" isnull((select sum(web.nSaldo) from AntiguedadDeudaWeb web where Web.nkey_cliente in(").Append(lngCodNkey).Append(")  and web.nkey_deudor = AntiguedadDeudaWeb.nKEy_Deudor and web.natraso = '30'),0) as 'total_30', ");
          cSQL.Append(" isnull((select sum(web.nSaldo) from AntiguedadDeudaWeb web where Web.nkey_cliente in(").Append(lngCodNkey).Append(")  and web.nkey_deudor = AntiguedadDeudaWeb.nKEy_Deudor and web.natraso = '60'),0) as 'total_60', ");
          cSQL.Append(" isnull((select sum(web.nSaldo) from AntiguedadDeudaWeb web where Web.nkey_cliente in(").Append(lngCodNkey).Append(")  and web.nkey_deudor = AntiguedadDeudaWeb.nKEy_Deudor and web.natraso = '90'),0) as 'total_90', ");
          cSQL.Append(" isnull((select sum(web.nSaldo) from AntiguedadDeudaWeb web where Web.nkey_cliente in(").Append(lngCodNkey).Append(")  and web.nkey_deudor = AntiguedadDeudaWeb.nKEy_Deudor and web.natraso = '180'),0) as 'total_180', ");
          cSQL.Append(" isnull((select sum(web.nSaldo) from AntiguedadDeudaWeb web where Web.nkey_cliente in(").Append(lngCodNkey).Append(")  and web.nkey_deudor = AntiguedadDeudaWeb.nKEy_Deudor and web.natraso = '360'),0) as 'total_360' ");
          cSQL.Append(" from AntiguedadDeudaWeb join codigodeudor on (codigodeudor.nkey_cliente = AntiguedadDeudaWeb.nkey_cliente and codigodeudor.nkey_deudor = AntiguedadDeudaWeb.nkey_deudor) ");
          cSQL.Append(" Join Servicio On(Servicio.nKey_Cliente = AntiguedadDeudaWeb.nKey_Cliente And Servicio.nKEy_Deudor = AntiguedadDeudaWeb.nKEy_Deudor ");
          cSQL.Append(" and servicio.nkey_analista <> 4 ) ");
          cSQL.Append(" join deudor on (deudor.nkey_deudor = codigodeudor.nkey_deudor) where AntiguedadDeudaWeb.nkey_cliente in(").Append(lngCodNkey).Append(") ");

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
            cSQL.Append("  and AntiguedadDeudaWeb.nkey_deudor = ").Append(lngNkeyUsuario);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "V"))
            cSQL.Append("  and codigodeudor.nkey_vendedor = ").Append(lngNkeyUsuario).Append("  ");

          cSQL.Append(" group by CodigoDeudor.ncodigodeudor, deudor.snombre,AntiguedadDeudaWeb.nkey_deudor ");
          cSQL.Append(" Order by sum(AntiguedadDeudaWeb.nSaldo) desc ");

        }
        else if ((!string.IsNullOrEmpty(pEstado)) && (pEstado == "2"))
        {
          cSQL.Append(" select  CodigoDeudor.ncodigodeudor, deudor.snombre,sum(AntiguedadDeudaWeb.nSaldo) as 'Total', ");
          cSQL.Append(" isnull((select sum(web.nSaldo) from AntiguedadDeudaWeb web where Web.nkey_cliente in(").Append(lngCodNkey).Append(")  and web.nkey_deudor = AntiguedadDeudaWeb.nKEy_Deudor and web.natraso = '0'),0) as 'total_0', ");
          cSQL.Append(" isnull((select sum(web.nSaldo) from AntiguedadDeudaWeb web where Web.nkey_cliente in(").Append(lngCodNkey).Append(")  and web.nkey_deudor = AntiguedadDeudaWeb.nKEy_Deudor and web.natraso = '15'),0) as 'total_15', ");
          cSQL.Append(" isnull((select sum(web.nSaldo) from AntiguedadDeudaWeb web where Web.nkey_cliente in(").Append(lngCodNkey).Append(")  and web.nkey_deudor = AntiguedadDeudaWeb.nKEy_Deudor and web.natraso = '30'),0) as 'total_30', ");
          cSQL.Append(" isnull((select sum(web.nSaldo) from AntiguedadDeudaWeb web where Web.nkey_cliente in(").Append(lngCodNkey).Append(")  and web.nkey_deudor = AntiguedadDeudaWeb.nKEy_Deudor and web.natraso = '60'),0) as 'total_60', ");
          cSQL.Append(" isnull((select sum(web.nSaldo) from AntiguedadDeudaWeb web where Web.nkey_cliente in(").Append(lngCodNkey).Append(")  and web.nkey_deudor = AntiguedadDeudaWeb.nKEy_Deudor and web.natraso = '90'),0) as 'total_90', ");
          cSQL.Append(" isnull((select sum(web.nSaldo) from AntiguedadDeudaWeb web where Web.nkey_cliente in(").Append(lngCodNkey).Append(")  and web.nkey_deudor = AntiguedadDeudaWeb.nKEy_Deudor and web.natraso = '180'),0) as 'total_180', ");
          cSQL.Append(" isnull((select sum(web.nSaldo) from AntiguedadDeudaWeb web where Web.nkey_cliente in(").Append(lngCodNkey).Append(")  and web.nkey_deudor = AntiguedadDeudaWeb.nKEy_Deudor and web.natraso = '360'),0) as 'total_360' ");
          cSQL.Append(" from AntiguedadDeudaWeb join codigodeudor on (codigodeudor.nkey_cliente = AntiguedadDeudaWeb.nkey_cliente and codigodeudor.nkey_deudor = AntiguedadDeudaWeb.nkey_deudor) ");
          cSQL.Append(" Join Analista On(Analista.nKey_Analista = ").Append(lngCodDeudor).Append(") Join Servicio On(Servicio.nKey_Cliente = AntiguedadDeudaWeb.nKey_Cliente And Servicio.nKEy_Deudor = AntiguedadDeudaWeb.nKEy_Deudor ");
          cSQL.Append(" and servicio.nkey_analista = ").Append(lngCodDeudor).Append(" ) join deudor on (deudor.nkey_deudor = codigodeudor.nkey_deudor) where AntiguedadDeudaWeb.nkey_cliente in(").Append(lngCodNkey).Append(") ");
          cSQL.Append(" group by CodigoDeudor.ncodigodeudor, deudor.snombre,AntiguedadDeudaWeb.nkey_deudor ");
          cSQL.Append(" Order by sum(AntiguedadDeudaWeb.nSaldo) desc ");

        }
        else if ((!string.IsNullOrEmpty(pEstado)) && (pEstado == "3"))
        {
          cSQL.Append("select  CodigoDeudor.ncodigodeudor, deudor.snombre,sum(AntiguedadDeudaWeb.nSaldo) as 'Total', ");
          cSQL.Append(" isnull((select sum(web.nSaldo) from AntiguedadDeudaWeb web where Web.nkey_cliente in(").Append(lngCodNkey).Append(")  and web.nkey_deudor = AntiguedadDeudaWeb.nKEy_Deudor and web.natraso = '0'),0) as 'total_0', ");
          cSQL.Append(" isnull((select sum(web.nSaldo) from AntiguedadDeudaWeb web where Web.nkey_cliente in(").Append(lngCodNkey).Append(")  and web.nkey_deudor = AntiguedadDeudaWeb.nKEy_Deudor and web.natraso = '15'),0) as 'total_15', ");
          cSQL.Append(" isnull((select sum(web.nSaldo) from AntiguedadDeudaWeb web where Web.nkey_cliente in(").Append(lngCodNkey).Append(")  and web.nkey_deudor = AntiguedadDeudaWeb.nKEy_Deudor and web.natraso = '30'),0) as 'total_30', ");
          cSQL.Append(" isnull((select sum(web.nSaldo) from AntiguedadDeudaWeb web where Web.nkey_cliente in(").Append(lngCodNkey).Append(")  and web.nkey_deudor = AntiguedadDeudaWeb.nKEy_Deudor and web.natraso = '60'),0) as 'total_60', ");
          cSQL.Append(" isnull((select sum(web.nSaldo) from AntiguedadDeudaWeb web where Web.nkey_cliente in(").Append(lngCodNkey).Append(")  and web.nkey_deudor = AntiguedadDeudaWeb.nKEy_Deudor and web.natraso = '90'),0) as 'total_90', ");
          cSQL.Append(" isnull((select sum(web.nSaldo) from AntiguedadDeudaWeb web where Web.nkey_cliente in(").Append(lngCodNkey).Append(")  and web.nkey_deudor = AntiguedadDeudaWeb.nKEy_Deudor and web.natraso = '180'),0) as 'total_180', ");
          cSQL.Append(" isnull((select sum(web.nSaldo) from AntiguedadDeudaWeb web where Web.nkey_cliente in(").Append(lngCodNkey).Append(")  and web.nkey_deudor = AntiguedadDeudaWeb.nKEy_Deudor and web.natraso = '360'),0) as 'total_360' ");
          cSQL.Append(" from AntiguedadDeudaWeb join codigodeudor on (codigodeudor.nkey_cliente = AntiguedadDeudaWeb.nkey_cliente and codigodeudor.nkey_deudor = AntiguedadDeudaWeb.nkey_deudor and codigodeudor.nkey_vendedor = ").Append(lngCodDeudor).Append(") ");
          cSQL.Append(" join deudor on (deudor.nkey_deudor = codigodeudor.nkey_deudor) where AntiguedadDeudaWeb.nkey_cliente in(").Append(lngCodNkey).Append(") ");

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
            cSQL.Append("  and AntiguedadDeudaWeb.nkey_deudor = ").Append(lngNkeyUsuario);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "V"))
            cSQL.Append("  and codigodeudor.nkey_vendedor = ").Append(lngNkeyUsuario).Append("  ");

          cSQL.Append("  group by CodigoDeudor.ncodigodeudor, deudor.snombre,AntiguedadDeudaWeb.nkey_deudor ");
          cSQL.Append(" Order by sum(AntiguedadDeudaWeb.nSaldo) desc");

        }
        else if ((!string.IsNullOrEmpty(pEstado)) && (pEstado == "4")) {
          cSQL.Append(" select  CodigoDeudor.ncodigodeudor, deudor.snombre,sum(AntiguedadDeudaWeb.nSaldo) as 'Total', ");
          cSQL.Append(" isnull((select sum(web.nSaldo) from AntiguedadDeudaWeb web where Web.nkey_cliente in(").Append(lngCodNkey).Append(")  and web.nkey_deudor = AntiguedadDeudaWeb.nKEy_Deudor and web.natraso = '0'),0) as 'total_0', ");
          cSQL.Append(" isnull((select sum(web.nSaldo) from AntiguedadDeudaWeb web where Web.nkey_cliente in(").Append(lngCodNkey).Append(")  and web.nkey_deudor = AntiguedadDeudaWeb.nKEy_Deudor and web.natraso = '15'),0) as 'total_15', ");
          cSQL.Append(" isnull((select sum(web.nSaldo) from AntiguedadDeudaWeb web where Web.nkey_cliente in(").Append(lngCodNkey).Append(")  and web.nkey_deudor = AntiguedadDeudaWeb.nKEy_Deudor and web.natraso = '30'),0) as 'total_30', ");
          cSQL.Append(" isnull((select sum(web.nSaldo) from AntiguedadDeudaWeb web where Web.nkey_cliente in(").Append(lngCodNkey).Append(")  and web.nkey_deudor = AntiguedadDeudaWeb.nKEy_Deudor and web.natraso = '60'),0) as 'total_60', ");
          cSQL.Append(" isnull((select sum(web.nSaldo) from AntiguedadDeudaWeb web where Web.nkey_cliente in(").Append(lngCodNkey).Append(")  and web.nkey_deudor = AntiguedadDeudaWeb.nKEy_Deudor and web.natraso = '90'),0) as 'total_90', ");
          cSQL.Append(" isnull((select sum(web.nSaldo) from AntiguedadDeudaWeb web where Web.nkey_cliente in(").Append(lngCodNkey).Append(")  and web.nkey_deudor = AntiguedadDeudaWeb.nKEy_Deudor and web.natraso = '180'),0) as 'total_180', ");
          cSQL.Append(" isnull((select sum(web.nSaldo) from AntiguedadDeudaWeb web where Web.nkey_cliente in(").Append(lngCodNkey).Append(")  and web.nkey_deudor = AntiguedadDeudaWeb.nKEy_Deudor and web.natraso = '360'),0) as 'total_360' ");
          cSQL.Append(" from AntiguedadDeudaWeb ");
          cSQL.Append(" join codigodeudor on (codigodeudor.nkey_cliente = AntiguedadDeudaWeb.nkey_cliente and codigodeudor.nkey_deudor = AntiguedadDeudaWeb.nkey_deudor) ");
          cSQL.Append(" Join Servicio On(Servicio.nKey_Cliente = AntiguedadDeudaWeb.nKey_Cliente And Servicio.nKEy_Deudor = AntiguedadDeudaWeb.nKEy_Deudor and servicio.nkey_analista <> 4 ) ");
          cSQL.Append(" join deudor on (deudor.nkey_deudor = codigodeudor.nkey_deudor) ");
          cSQL.Append(" join cliente on (cliente.nkey_cliente = AntiguedadDeudaWeb.nKey_Cliente ) ");
          cSQL.Append(" where cliente.ncodholding  = ").Append(pNcodHolding);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
            cSQL.Append("  and AntiguedadDeudaWeb.nkey_deudor = ").Append(lngNkeyUsuario);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "V"))
            cSQL.Append("  and codigodeudor.nkey_vendedor = ").Append(lngNkeyUsuario).Append("  ");

          cSQL.Append(" group by CodigoDeudor.ncodigodeudor, deudor.snombre,AntiguedadDeudaWeb.nkey_deudor ");
          cSQL.Append(" Order by sum(AntiguedadDeudaWeb.nSaldo) desc ");
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
