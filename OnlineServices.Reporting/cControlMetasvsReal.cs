using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using OnlineServices.Conn;

namespace OnlineServices.Reporting
{
  public class cControlMetasvsReal
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

    private string pEstado;
    public string Estado { get { return pEstado; } set { pEstado = value; } }

    private string pDtFchIni;
    public string DtFchIni { get { return pDtFchIni; } set { pDtFchIni = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private DBConn oConn;

    public cControlMetasvsReal()
    {

    }

    public cControlMetasvsReal(ref DBConn oConn)
    {
      this.oConn = oConn;
    }

    public DataTable Get()
    {
      DataTable dtData;

      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();
        if (pEstado == "0")
        {
          cSQL.Append("Select fecha_ini_sem1,fecha_fin_sem1,fecha_ini_sem2,fecha_fin_sem2, fecha_ini_sem3,fecha_fin_sem3,fecha_ini_sem4,fecha_fin_sem4,fecha_ini_sem5,fecha_fin_sem5, ");
          cSQL.Append(" real1,estimado1, ");
          cSQL.Append(" real2,estimado2, ");
          cSQL.Append(" real3,estimado3, ");
          cSQL.Append(" real4,estimado4, ");
          cSQL.Append(" real5,estimado5, ");
          cSQL.Append(" real1 + real2 + real3 + real4 + real5 as'treal', ");
          cSQL.Append(" estimado1 + estimado2 + estimado3 + estimado4 + estimado5 as 'testimado', ");
          cSQL.Append(" (select sum(real1) ");
          cSQL.Append(" from Vista_MetasAnalistaCli ");
          cSQL.Append(" where Vista_MetasAnalistaCli.nkey_cliente in(").Append(lngCodNkey).Append(") ");

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
            cSQL.Append("  and Vista_MetasAnalistaCli.nkey_deudor = ").Append(lngNkeyUsuario);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "V"))
            cSQL.Append("  and Vista_MetasAnalistaCli.nkey_deudor in (select codigodeudor.nkey_deudor from codigodeudor where codigodeudor.nkey_vendedor = ").Append(lngNkeyUsuario).Append(" and codigodeudor.nkey_cliente in(").Append(lngCodNkey).Append(") )  ");

          cSQL.Append(" and Vista_MetasAnalistaCli.periodo = convert(datetime,'").Append(pDtFchIni).Append("') ) as 'TotReal1',");
          cSQL.Append(" (select sum(real2) ");
          cSQL.Append(" from Vista_MetasAnalistaCli ");
          cSQL.Append(" where Vista_MetasAnalistaCli.nkey_cliente in(").Append(lngCodNkey).Append(") ");

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
            cSQL.Append("  and Vista_MetasAnalistaCli.nkey_deudor = ").Append(lngNkeyUsuario);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "V"))
            cSQL.Append("  and Vista_MetasAnalistaCli.nkey_deudor in (select codigodeudor.nkey_deudor from codigodeudor where codigodeudor.nkey_vendedor = ").Append(lngNkeyUsuario).Append(" and codigodeudor.nkey_cliente in(").Append(lngCodNkey).Append(") )  ");

          cSQL.Append(" and Vista_MetasAnalistaCli.periodo = convert(datetime,'").Append(pDtFchIni).Append("') ) as 'TotReal2',");
          cSQL.Append(" (select sum(real3) ");
          cSQL.Append(" from Vista_MetasAnalistaCli ");
          cSQL.Append(" where Vista_MetasAnalistaCli.nkey_cliente in(").Append(lngCodNkey).Append(") ");

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
            cSQL.Append("  and Vista_MetasAnalistaCli.nkey_deudor = ").Append(lngNkeyUsuario);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "V"))
            cSQL.Append("  and Vista_MetasAnalistaCli.nkey_deudor in (select codigodeudor.nkey_deudor from codigodeudor where codigodeudor.nkey_vendedor = ").Append(lngNkeyUsuario).Append(" and codigodeudor.nkey_cliente in(").Append(lngCodNkey).Append(") )  ");

          cSQL.Append(" and Vista_MetasAnalistaCli.periodo = convert(datetime,'").Append(pDtFchIni).Append("') ) as 'TotReal3', ");
          cSQL.Append(" (select sum(real4) ");
          cSQL.Append(" from Vista_MetasAnalistaCli ");
          cSQL.Append(" where Vista_MetasAnalistaCli.nkey_cliente in(").Append(lngCodNkey).Append(") ");

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
            cSQL.Append("  and Vista_MetasAnalistaCli.nkey_deudor = ").Append(lngNkeyUsuario);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "V"))
            cSQL.Append("  and Vista_MetasAnalistaCli.nkey_deudor in (select codigodeudor.nkey_deudor from codigodeudor where codigodeudor.nkey_vendedor = ").Append(lngNkeyUsuario).Append(" and codigodeudor.nkey_cliente in(").Append(lngCodNkey).Append(") )  ");

          cSQL.Append(" and Vista_MetasAnalistaCli.periodo = convert(datetime,'").Append(pDtFchIni).Append("') ) as 'TotReal4', ");
          cSQL.Append(" (select sum(real5) ");
          cSQL.Append(" from Vista_MetasAnalistaCli ");
          cSQL.Append(" where Vista_MetasAnalistaCli.nkey_cliente in(").Append(lngCodNkey).Append(") ");

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
            cSQL.Append("  and Vista_MetasAnalistaCli.nkey_deudor = ").Append(lngNkeyUsuario);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "V"))
            cSQL.Append("  and Vista_MetasAnalistaCli.nkey_deudor in (select codigodeudor.nkey_deudor from codigodeudor where codigodeudor.nkey_vendedor = ").Append(lngNkeyUsuario).Append(" and codigodeudor.nkey_cliente in(").Append(lngCodNkey).Append(") )  ");

          cSQL.Append(" and Vista_MetasAnalistaCli.periodo = convert(datetime,'").Append(pDtFchIni).Append("') ) as 'TotReal5', ");
          cSQL.Append(" (select sum(estimado1) ");
          cSQL.Append(" from Vista_MetasAnalistaCli ");
          cSQL.Append(" where Vista_MetasAnalistaCli.nkey_cliente in(").Append(lngCodNkey).Append(") ");

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
            cSQL.Append("  and Vista_MetasAnalistaCli.nkey_deudor = ").Append(lngNkeyUsuario);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "V"))
            cSQL.Append("  and Vista_MetasAnalistaCli.nkey_deudor in (select codigodeudor.nkey_deudor from codigodeudor where codigodeudor.nkey_vendedor = ").Append(lngNkeyUsuario).Append(" and codigodeudor.nkey_cliente in(").Append(lngCodNkey).Append(") )  ");

          cSQL.Append(" and Vista_MetasAnalistaCli.periodo = convert(datetime,'").Append(pDtFchIni).Append("') ) as 'TotEst1',");
          cSQL.Append(" (select sum(estimado2) ");
          cSQL.Append(" from Vista_MetasAnalistaCli ");
          cSQL.Append(" where Vista_MetasAnalistaCli.nkey_cliente in(").Append(lngCodNkey).Append(") ");

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
            cSQL.Append("  and Vista_MetasAnalistaCli.nkey_deudor = ").Append(lngNkeyUsuario);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "V"))
            cSQL.Append("  and Vista_MetasAnalistaCli.nkey_deudor in (select codigodeudor.nkey_deudor from codigodeudor where codigodeudor.nkey_vendedor = ").Append(lngNkeyUsuario).Append(" and codigodeudor.nkey_cliente in(").Append(lngCodNkey).Append(") )  ");

          cSQL.Append(" and Vista_MetasAnalistaCli.periodo = convert(datetime,'").Append(pDtFchIni).Append("') ) as 'TotEst2', ");
          cSQL.Append(" (select sum(estimado3) ");
          cSQL.Append(" from Vista_MetasAnalistaCli ");
          cSQL.Append(" where Vista_MetasAnalistaCli.nkey_cliente in(").Append(lngCodNkey).Append(") ");

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
            cSQL.Append("  and Vista_MetasAnalistaCli.nkey_deudor = ").Append(lngNkeyUsuario);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "V"))
            cSQL.Append("  and Vista_MetasAnalistaCli.nkey_deudor in (select codigodeudor.nkey_deudor from codigodeudor where codigodeudor.nkey_vendedor = ").Append(lngNkeyUsuario).Append(" and codigodeudor.nkey_cliente in(").Append(lngCodNkey).Append(") )  ");

          cSQL.Append(" and Vista_MetasAnalistaCli.periodo = convert(datetime,'").Append(pDtFchIni).Append("') ) as 'TotEst3', ");
          cSQL.Append(" (select sum(estimado4) ");
          cSQL.Append(" from Vista_MetasAnalistaCli ");
          cSQL.Append(" where Vista_MetasAnalistaCli.nkey_cliente in(").Append(lngCodNkey).Append(") ");

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
            cSQL.Append("  and Vista_MetasAnalistaCli.nkey_deudor = ").Append(lngNkeyUsuario);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "V"))
            cSQL.Append("  and Vista_MetasAnalistaCli.nkey_deudor in (select codigodeudor.nkey_deudor from codigodeudor where codigodeudor.nkey_vendedor = ").Append(lngNkeyUsuario).Append(" and codigodeudor.nkey_cliente in(").Append(lngCodNkey).Append(") )  ");

          cSQL.Append(" and Vista_MetasAnalistaCli.periodo = convert(datetime,'").Append(pDtFchIni).Append("') ) as 'TotEst4', ");
          cSQL.Append(" (select sum(estimado5) ");
          cSQL.Append(" from Vista_MetasAnalistaCli ");
          cSQL.Append(" where Vista_MetasAnalistaCli.nkey_cliente in(").Append(lngCodNkey).Append(") ");

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
            cSQL.Append("  and Vista_MetasAnalistaCli.nkey_deudor = ").Append(lngNkeyUsuario);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "V"))
            cSQL.Append("  and Vista_MetasAnalistaCli.nkey_deudor in (select codigodeudor.nkey_deudor from codigodeudor where codigodeudor.nkey_vendedor = ").Append(lngNkeyUsuario).Append(" and codigodeudor.nkey_cliente in(").Append(lngCodNkey).Append(") )  ");

          cSQL.Append(" and Vista_MetasAnalistaCli.periodo = convert(datetime,'").Append(pDtFchIni).Append("') ) as 'TotEst5', ");
          cSQL.Append(" NomCLiente,ncodigodeudor , NomDeudor, periodo ");
          cSQL.Append(" from Vista_MetasAnalistaCli ");
          cSQL.Append(" where Vista_MetasAnalistaCli.nkey_cliente in(").Append(lngCodNkey).Append(") ");
          cSQL.Append(" and Vista_MetasAnalistaCli.periodo = convert(datetime,'").Append(pDtFchIni).Append("') ");
        }
        else if (pEstado == "1")
        {
          cSQL.Append("Select fecha_ini_sem1,fecha_fin_sem1, fecha_ini_sem2,fecha_fin_sem2, fecha_ini_sem3,fecha_fin_sem3,fecha_ini_sem4,fecha_fin_sem4,fecha_ini_sem5,fecha_fin_sem5,");
          cSQL.Append(" real1,estimado1, ");
          cSQL.Append(" real2,estimado2, ");
          cSQL.Append(" real3,estimado3, ");
          cSQL.Append(" real4,estimado4, ");
          cSQL.Append(" real5,estimado5, ");
          cSQL.Append(" real1 + real2 + real3 + real4 + real5 as'treal', ");
          cSQL.Append(" estimado1 + estimado2 + estimado3 + estimado4 + estimado5 as 'testimado', ");
          cSQL.Append(" (select sum(real1) ");
          cSQL.Append(" from Vista_MetasAnalistaCli ");
          cSQL.Append(" JOIN Servicio ");
          cSQL.Append(" ON(Servicio.nKey_Deudor = Vista_MetasAnalistaCli.nKey_Deudor ");
          cSQL.Append(" AND Servicio.nKey_Cliente in(").Append(lngCodNkey).Append(") and servicio.nkey_analista = ").Append(lngCodDeudor).Append(") ");
          cSQL.Append(" join analista  ");
          cSQL.Append(" on (analista.nkey_analista = ").Append(lngCodDeudor).Append(") ");
          cSQL.Append(" where  Vista_MetasAnalistaCli.nkey_cliente in(").Append(lngCodNkey).Append(") ");

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
            cSQL.Append("  and Vista_MetasAnalistaCli.nkey_deudor = ").Append(lngNkeyUsuario);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "v"))
            cSQL.Append("  and Vista_MetasAnalistaCli.nkey_deudor in (select codigodeudor.nkey_deudor from codigodeudor where codigodeudor.nkey_vendedor = ").Append(lngNkeyUsuario).Append(" and codigodeudor.nkey_cliente in(").Append(lngCodNkey).Append(") )  ");

          cSQL.Append(" and Vista_MetasAnalistaCli.periodo = convert(datetime,'").Append(pDtFchIni).Append("') ) as 'TotReal1',");
          cSQL.Append(" (select sum(real2) ");
          cSQL.Append(" from Vista_MetasAnalistaCli ");
          cSQL.Append(" JOIN Servicio ");
          cSQL.Append(" ON(Servicio.nKey_Deudor = Vista_MetasAnalistaCli.nKey_Deudor ");
          cSQL.Append(" AND Servicio.nKey_Cliente in(").Append(lngCodNkey).Append(") and servicio.nkey_analista = ").Append(lngCodDeudor).Append(") ");
          cSQL.Append(" join analista  ");
          cSQL.Append(" on (analista.nkey_analista = ").Append(lngCodDeudor).Append(") ");
          cSQL.Append(" where  Vista_MetasAnalistaCli.nkey_cliente in(").Append(lngCodNkey).Append(") ");

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
            cSQL.Append("  and Vista_MetasAnalistaCli.nkey_deudor = ").Append(lngNkeyUsuario);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "v"))
            cSQL.Append("  and Vista_MetasAnalistaCli.nkey_deudor in (select codigodeudor.nkey_deudor from codigodeudor where codigodeudor.nkey_vendedor = ").Append(lngNkeyUsuario).Append(" and codigodeudor.nkey_cliente in(").Append(lngCodNkey).Append(") )  ");

          cSQL.Append(" and Vista_MetasAnalistaCli.periodo = convert(datetime,'").Append(pDtFchIni).Append("') ) as 'TotReal2', ");
          cSQL.Append(" (select sum(real3) ");
          cSQL.Append(" from Vista_MetasAnalistaCli ");
          cSQL.Append(" JOIN Servicio ");
          cSQL.Append(" ON(Servicio.nKey_Deudor = Vista_MetasAnalistaCli.nKey_Deudor ");
          cSQL.Append(" AND Servicio.nKey_Cliente in(").Append(lngCodNkey).Append(") and servicio.nkey_analista = ").Append(lngCodDeudor).Append(") ");
          cSQL.Append(" join analista  ");
          cSQL.Append(" on (analista.nkey_analista = ").Append(lngCodDeudor).Append(") ");
          cSQL.Append(" where  Vista_MetasAnalistaCli.nkey_cliente in(").Append(lngCodNkey).Append(") ");

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
            cSQL.Append("  and Vista_MetasAnalistaCli.nkey_deudor = ").Append(lngNkeyUsuario);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "v"))
            cSQL.Append("  and Vista_MetasAnalistaCli.nkey_deudor in (select codigodeudor.nkey_deudor from codigodeudor where codigodeudor.nkey_vendedor = ").Append(lngNkeyUsuario).Append(" and codigodeudor.nkey_cliente in(").Append(lngCodNkey).Append(") )  ");

          cSQL.Append(" and Vista_MetasAnalistaCli.periodo = convert(datetime,'").Append(pDtFchIni).Append("') ) as 'TotReal3', ");
          cSQL.Append(" (select sum(real4) ");
          cSQL.Append(" from Vista_MetasAnalistaCli ");
          cSQL.Append(" JOIN Servicio ");
          cSQL.Append(" ON(Servicio.nKey_Deudor = Vista_MetasAnalistaCli.nKey_Deudor ");
          cSQL.Append(" AND Servicio.nKey_Cliente in(").Append(lngCodNkey).Append(") and servicio.nkey_analista = ").Append(lngCodDeudor).Append(") ");
          cSQL.Append(" join analista  ");
          cSQL.Append(" on (analista.nkey_analista = ").Append(lngCodDeudor).Append(") ");
          cSQL.Append(" where  Vista_MetasAnalistaCli.nkey_cliente in(").Append(lngCodNkey).Append(") ");

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
            cSQL.Append("  and Vista_MetasAnalistaCli.nkey_deudor = ").Append(lngNkeyUsuario);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "v"))
            cSQL.Append("  and Vista_MetasAnalistaCli.nkey_deudor in (select codigodeudor.nkey_deudor from codigodeudor where codigodeudor.nkey_vendedor = ").Append(lngNkeyUsuario).Append(" and codigodeudor.nkey_cliente in(").Append(lngCodNkey).Append(") )  ");

          cSQL.Append(" and Vista_MetasAnalistaCli.periodo = convert(datetime,'").Append(pDtFchIni).Append("') ) as 'TotReal4',");
          cSQL.Append(" (select sum(real5) ");
          cSQL.Append(" from Vista_MetasAnalistaCli ");
          cSQL.Append(" JOIN Servicio ");
          cSQL.Append(" ON(Servicio.nKey_Deudor = Vista_MetasAnalistaCli.nKey_Deudor ");
          cSQL.Append(" AND Servicio.nKey_Cliente in(").Append(lngCodNkey).Append(") and servicio.nkey_analista = ").Append(lngCodDeudor).Append(") ");
          cSQL.Append(" join analista  ");
          cSQL.Append(" on (analista.nkey_analista = ").Append(lngCodDeudor).Append(") ");
          cSQL.Append(" where  Vista_MetasAnalistaCli.nkey_cliente in(").Append(lngCodNkey).Append(") ");

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
            cSQL.Append("  and Vista_MetasAnalistaCli.nkey_deudor = ").Append(lngNkeyUsuario);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "v"))
            cSQL.Append("  and Vista_MetasAnalistaCli.nkey_deudor in (select codigodeudor.nkey_deudor from codigodeudor where codigodeudor.nkey_vendedor = ").Append(lngNkeyUsuario).Append(" and codigodeudor.nkey_cliente in(").Append(lngCodNkey).Append(") )  ");

          cSQL.Append(" and Vista_MetasAnalistaCli.periodo = convert(datetime,'").Append(pDtFchIni).Append("') ) as 'TotReal5',");
          cSQL.Append(" (select sum(estimado1) ");
          cSQL.Append(" from Vista_MetasAnalistaCli ");
          cSQL.Append(" JOIN Servicio ");
          cSQL.Append(" ON(Servicio.nKey_Deudor = Vista_MetasAnalistaCli.nKey_Deudor ");
          cSQL.Append(" AND Servicio.nKey_Cliente in(").Append(lngCodNkey).Append(") and servicio.nkey_analista = ").Append(lngCodDeudor).Append(") ");
          cSQL.Append(" join analista  ");
          cSQL.Append(" on (analista.nkey_analista = ").Append(lngCodDeudor).Append(") ");
          cSQL.Append(" where  Vista_MetasAnalistaCli.nkey_cliente in(").Append(lngCodNkey).Append(") ");

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
            cSQL.Append("  and Vista_MetasAnalistaCli.nkey_deudor = ").Append(lngNkeyUsuario);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "v"))
            cSQL.Append("  and Vista_MetasAnalistaCli.nkey_deudor in (select codigodeudor.nkey_deudor from codigodeudor where codigodeudor.nkey_vendedor = ").Append(lngNkeyUsuario).Append(" and codigodeudor.nkey_cliente in(").Append(lngCodNkey).Append(") )  ");

          cSQL.Append(" and Vista_MetasAnalistaCli.periodo = convert(datetime,'").Append(pDtFchIni).Append("') ) as 'TotEst1', ");
          cSQL.Append(" (select sum(estimado2) ");
          cSQL.Append(" from Vista_MetasAnalistaCli ");
          cSQL.Append(" JOIN Servicio ");
          cSQL.Append(" ON(Servicio.nKey_Deudor = Vista_MetasAnalistaCli.nKey_Deudor ");
          cSQL.Append(" AND Servicio.nKey_Cliente in(").Append(lngCodNkey).Append(") and servicio.nkey_analista = ").Append(lngCodDeudor).Append(") ");
          cSQL.Append(" join analista  ");
          cSQL.Append(" on (analista.nkey_analista = ").Append(lngCodDeudor).Append(") ");
          cSQL.Append(" where  Vista_MetasAnalistaCli.nkey_cliente in(").Append(lngCodNkey).Append(") ");

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
            cSQL.Append("  and Vista_MetasAnalistaCli.nkey_deudor = ").Append(lngNkeyUsuario);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "v"))
            cSQL.Append("  and Vista_MetasAnalistaCli.nkey_deudor in (select codigodeudor.nkey_deudor from codigodeudor where codigodeudor.nkey_vendedor = ").Append(lngNkeyUsuario).Append(" and codigodeudor.nkey_cliente = ").Append(lngNkeyUsuario).Append(" )  ");

          cSQL.Append(" and Vista_MetasAnalistaCli.periodo = convert(datetime,'").Append(pDtFchIni).Append("') ) as 'TotEst2', ");
          cSQL.Append(" (select sum(estimado3) ");
          cSQL.Append(" from Vista_MetasAnalistaCli ");
          cSQL.Append(" JOIN Servicio ");
          cSQL.Append(" ON(Servicio.nKey_Deudor = Vista_MetasAnalistaCli.nKey_Deudor ");
          cSQL.Append(" AND Servicio.nKey_Cliente in(").Append(lngCodNkey).Append(") and servicio.nkey_analista = ").Append(lngCodDeudor).Append(") ");
          cSQL.Append(" join analista  ");
          cSQL.Append(" on (analista.nkey_analista = ").Append(lngCodDeudor).Append(") ");
          cSQL.Append(" where  Vista_MetasAnalistaCli.nkey_cliente in(").Append(lngCodNkey).Append(") ");

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
            cSQL.Append("  and Vista_MetasAnalistaCli.nkey_deudor = ").Append(lngNkeyUsuario);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "v"))
            cSQL.Append("  and Vista_MetasAnalistaCli.nkey_deudor in (select codigodeudor.nkey_deudor from codigodeudor where codigodeudor.nkey_vendedor = ").Append(lngNkeyUsuario).Append(" and codigodeudor.nkey_cliente in(").Append(lngCodNkey).Append(") )  ");

          cSQL.Append(" and Vista_MetasAnalistaCli.periodo = convert(datetime,'").Append(pDtFchIni).Append("') ) as 'TotEst3', ");
          cSQL.Append(" (select sum(estimado4) ");
          cSQL.Append(" from Vista_MetasAnalistaCli ");
          cSQL.Append(" JOIN Servicio ");
          cSQL.Append(" ON(Servicio.nKey_Deudor = Vista_MetasAnalistaCli.nKey_Deudor ");
          cSQL.Append(" AND Servicio.nKey_Cliente in(").Append(lngCodNkey).Append(") and servicio.nkey_analista = ").Append(lngCodDeudor).Append(") ");
          cSQL.Append(" join analista  ");
          cSQL.Append(" on (analista.nkey_analista = ").Append(lngCodDeudor).Append(") ");
          cSQL.Append(" where  Vista_MetasAnalistaCli.nkey_cliente in(").Append(lngCodNkey).Append(") ");
          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
            cSQL.Append("  and Vista_MetasAnalistaCli.nkey_deudor = ").Append(lngNkeyUsuario);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "v"))
            cSQL.Append("  and Vista_MetasAnalistaCli.nkey_deudor in (select codigodeudor.nkey_deudor from codigodeudor where codigodeudor.nkey_vendedor = ").Append(lngNkeyUsuario).Append(" and codigodeudor.nkey_cliente in(").Append(lngCodNkey).Append(") )  ");

          cSQL.Append(" and Vista_MetasAnalistaCli.periodo = convert(datetime,'").Append(pDtFchIni).Append("') ) as 'TotEst4',");
          cSQL.Append(" (select sum(estimado5) ");
          cSQL.Append(" from Vista_MetasAnalistaCli ");
          cSQL.Append(" JOIN Servicio ");
          cSQL.Append(" ON(Servicio.nKey_Deudor = Vista_MetasAnalistaCli.nKey_Deudor ");
          cSQL.Append(" AND Servicio.nKey_Cliente in(").Append(lngCodNkey).Append(") and servicio.nkey_analista = ").Append(lngCodDeudor).Append(") ");
          cSQL.Append(" join analista  ");
          cSQL.Append(" on (analista.nkey_analista = ").Append(lngCodDeudor).Append(") ");
          cSQL.Append(" where  Vista_MetasAnalistaCli.nkey_cliente in(").Append(lngCodNkey).Append(") ");

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
            cSQL.Append("  and Vista_MetasAnalistaCli.nkey_deudor = ").Append(lngNkeyUsuario);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "v"))
            cSQL.Append("  and Vista_MetasAnalistaCli.nkey_deudor in (select codigodeudor.nkey_deudor from codigodeudor where codigodeudor.nkey_vendedor = ").Append(lngNkeyUsuario).Append(" and codigodeudor.nkey_cliente in(").Append(lngCodNkey).Append(") )  ");

          cSQL.Append(" and Vista_MetasAnalistaCli.periodo = convert(datetime,'").Append(pDtFchIni).Append("') ) as 'TotEst5',");
          cSQL.Append(" NomCLiente, ncodigodeudor , NomDeudor, periodo, ");
          cSQL.Append(" analista.snombre as 'NomAnalis' ");
          cSQL.Append(" from Vista_MetasAnalistaCli ");
          cSQL.Append(" JOIN Servicio ");
          cSQL.Append(" ON(Servicio.nKey_Deudor = Vista_MetasAnalistaCli.nKey_Deudor ");
          cSQL.Append(" AND Servicio.nKey_Cliente in(").Append(lngCodNkey).Append(") and servicio.nkey_analista = ").Append(lngCodDeudor).Append(") ");
          cSQL.Append(" join analista  ");
          cSQL.Append(" on (analista.nkey_analista = ").Append(lngCodDeudor).Append(") ");
          cSQL.Append(" where Vista_MetasAnalistaCli.nkey_cliente in(").Append(lngCodNkey).Append(") ");

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
            cSQL.Append("  and Vista_MetasAnalistaCli.nkey_deudor = ").Append(lngNkeyUsuario);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "v"))
            cSQL.Append("  and Vista_MetasAnalistaCli.nkey_deudor in (select codigodeudor.nkey_deudor from codigodeudor where codigodeudor.nkey_vendedor = ").Append(lngNkeyUsuario).Append(" and codigodeudor.nkey_cliente in(").Append(lngCodNkey).Append(") )  ");

          cSQL.Append(" and Vista_MetasAnalistaCli.periodo = convert(datetime,'").Append(pDtFchIni).Append("') ");
        }
        else if (pEstado == "2")
        {
          cSQL.Append("select cliente.snombre as 'NomCLiente', codigodeudor.ncodigodeudor , deudor.snombre as 'NomDeudor', real1+real2+real3+real4+real5 as 'Real', ");
          cSQL.Append(" estimado1+estimado2+estimado3+estimado4+estimado5 as 'Estimado', ");
          cSQL.Append(" ((real1+real2+real3+real4+real5) - (estimado1+estimado2+estimado3+estimado4+estimado5)) as 'diferencia',  ");
          cSQL.Append("(select sum(real1+real2+real3+real4+real5) ");
          cSQL.Append(" from codigodeudor  ");
          cSQL.Append(" join  metasanalistadeudor ");
          cSQL.Append(" on (metasanalistadeudor.nkey_cliente in(").Append(lngCodNkey).Append(") ");
          cSQL.Append(" and convert(datetime,metasanalistadeudor.periodo) = convert(datetime,'").Append(pDtFchIni).Append("') ");
          cSQL.Append(" and metasanalistadeudor.nkey_deudor = codigodeudor.nkey_deudor ");
          cSQL.Append(" and MetasAnalistaDeudor.nkey_metasanalista = metas.nkey_metasanalista ) ");
          cSQL.Append(" join cliente  ");
          cSQL.Append(" on (cliente.nkey_cliente = codigodeudor.nkey_cliente) ");
          cSQL.Append(" join deudor ");
          cSQL.Append(" on (deudor.nkey_deudor = codigodeudor.nkey_deudor) ");
          cSQL.Append(" JOIN Servicio ");
          cSQL.Append(" ON(Servicio.nKey_Deudor = codigodeudor.nKey_Deudor ");
          cSQL.Append(" AND Servicio.nKey_Cliente in(").Append(lngCodNkey).Append(") and servicio.nkey_analista != 4) ");
          cSQL.Append(" where codigodeudor.nkey_cliente in(").Append(lngCodNkey).Append(") ");

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
            cSQL.Append("  and Vista_MetasAnalistaCli.nkey_deudor = ").Append(lngNkeyUsuario);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "V"))
            cSQL.Append("  and Vista_MetasAnalistaCli.nkey_deudor in (select codigodeudor.nkey_deudor from codigodeudor where codigodeudor.nkey_vendedor = ").Append(lngNkeyUsuario).Append(" and codigodeudor.nkey_cliente in(").Append(lngCodNkey).Append(") )  ");

          cSQL.Append(" ) as 'TotReal', ");
          cSQL.Append("(select sum(estimado1+estimado2+estimado3+estimado4+estimado5) ");
          cSQL.Append(" from codigodeudor  ");
          cSQL.Append(" join  metasanalistadeudor ");
          cSQL.Append(" on (metasanalistadeudor.nkey_cliente in(").Append(lngCodNkey).Append(") ");
          cSQL.Append(" and convert(datetime,metasanalistadeudor.periodo) = convert(datetime,'").Append(pDtFchIni).Append("') ");
          cSQL.Append(" and metasanalistadeudor.nkey_deudor = codigodeudor.nkey_deudor ");
          cSQL.Append(" and MetasAnalistaDeudor.nkey_metasanalista = metas.nkey_metasanalista ) ");
          cSQL.Append(" join cliente  ");
          cSQL.Append(" on (cliente.nkey_cliente = codigodeudor.nkey_cliente) ");
          cSQL.Append(" join deudor ");
          cSQL.Append(" on (deudor.nkey_deudor = codigodeudor.nkey_deudor) ");
          cSQL.Append(" JOIN Servicio ");
          cSQL.Append(" ON(Servicio.nKey_Deudor = codigodeudor.nKey_Deudor ");
          cSQL.Append(" AND Servicio.nKey_Cliente in(").Append(lngCodNkey).Append(") and servicio.nkey_analista != 4) ");
          cSQL.Append(" where codigodeudor.nkey_cliente in(").Append(lngCodNkey).Append(") ");

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
            cSQL.Append("  and Vista_MetasAnalistaCli.nkey_deudor = ").Append(lngNkeyUsuario);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "V"))
            cSQL.Append("  and Vista_MetasAnalistaCli.nkey_deudor in (select codigodeudor.nkey_deudor from codigodeudor where codigodeudor.nkey_vendedor = ").Append(lngNkeyUsuario).Append(" and codigodeudor.nkey_cliente in(").Append(lngCodNkey).Append(") )  ");

          cSQL.Append(" ) as 'TotEstimado', ");
          cSQL.Append(" gestiondeudor.sobservacion as 'Comentario', ");
          cSQL.Append(" gestiondeudor.sDeudor as 'Sdeudor',  ");
          cSQL.Append(" gestiondeudor.sAnalista as 'SAnalista', ");
          cSQL.Append(" Menu_EtapaCobranza.sNombre as 'EtapaCob' ");
          cSQL.Append(" from codigodeudor  ");
          cSQL.Append(" join  metasanalistadeudor metas ");
          cSQL.Append(" on (metas.nkey_cliente in(").Append(lngCodNkey).Append(") ");
          cSQL.Append(" and convert(datetime,metas.periodo) = convert(datetime,'").Append(pDtFchIni).Append("') ");
          cSQL.Append(" and metas.nkey_deudor = codigodeudor.nkey_deudor ");
          cSQL.Append(" and Metas.nkey_metasanalista = (select ");
          cSQL.Append(" max(MD.nkey_metasanalista)  ");
          cSQL.Append(" from MetasAnalistaDeudor MD where  ");
          cSQL.Append(" MD.nKey_Analista = Metas.nKey_Analista  ");
          cSQL.Append(" AND MD.periodo = Metas.periodo   ");
          cSQL.Append(" AND MD.nkey_cliente = Metas.nkey_cliente  ");
          cSQL.Append(" AND MD.nkey_deudor = Metas.nkey_deudor )) ");
          cSQL.Append(" join cliente  ");
          cSQL.Append(" on (cliente.nkey_cliente = codigodeudor.nkey_cliente) ");
          cSQL.Append(" join deudor ");
          cSQL.Append(" on (deudor.nkey_deudor = codigodeudor.nkey_deudor) ");
          cSQL.Append(" JOIN Servicio ");
          cSQL.Append(" ON(Servicio.nKey_Deudor = codigodeudor.nKey_Deudor ");
          cSQL.Append(" AND Servicio.nKey_Cliente in(").Append(lngCodNkey).Append(") and servicio.nkey_analista != 4) ");
          cSQL.Append(" LEFT JOIN gestiondeudor ");
          cSQL.Append(" ON (gestiondeudor.nkey_cliente = Metas.nkey_cliente  ");
          cSQL.Append(" AND gestiondeudor.nkey_deudor = Metas.nkey_deudor  ");
          cSQL.Append(" AND gestiondeudor.nkey_gestiondeudor = (select  max(GD.nkey_gestiondeudor ) ");
          cSQL.Append(" from gestiondeudor GD where gd.nkey_cliente = gestiondeudor.nkey_cliente and ");
          cSQL.Append(" GD.nkey_deudor = gestiondeudor.nkey_deudor)) ");
          cSQL.Append(" left join Menu_EtapaCobranza ");
          cSQL.Append(" on (Menu_EtapaCobranza.nkey_etapacobranza = gestiondeudor.netapacobranza) ");
          cSQL.Append(" where codigodeudor.nkey_cliente in(").Append(lngCodNkey).Append(") ");

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
            cSQL.Append("  and codigodeudor.nkey_deudor = ").Append(lngNkeyUsuario);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "V"))
            cSQL.Append("  and codigodeudor.nkey_vendedor = ").Append(lngNkeyUsuario);

          cSQL.Append(" Order by codigodeudor.ncodigodeudor ");
        }
        else if (pEstado == "3") {
          cSQL.Append("Select fecha_ini_sem1,fecha_fin_sem1,fecha_ini_sem2,fecha_fin_sem2, fecha_ini_sem3,fecha_fin_sem3,fecha_ini_sem4,fecha_fin_sem4,fecha_ini_sem5,fecha_fin_sem5, ");
          cSQL.Append(" real1,estimado1, ");
          cSQL.Append(" real2,estimado2, ");
          cSQL.Append(" real3,estimado3, ");
          cSQL.Append(" real4,estimado4, ");
          cSQL.Append(" real5,estimado5, ");
          cSQL.Append(" real1 + real2 + real3 + real4 + real5 as'treal', ");
          cSQL.Append(" estimado1 + estimado2 + estimado3 + estimado4 + estimado5 as 'testimado', ");
          cSQL.Append(" (select sum(real1) ");
          cSQL.Append(" from Vista_MetasAnalistaCli join cliente on (cliente.nkey_cliente = Vista_MetasAnalistaCli.nKey_Cliente ) where cliente.ncodholding  = ").Append(pNcodHolding);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
            cSQL.Append("  and Vista_MetasAnalistaCli.nkey_deudor = ").Append(lngNkeyUsuario);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "V"))
            cSQL.Append("  and Vista_MetasAnalistaCli.nkey_deudor in (select codigodeudor.nkey_deudor from codigodeudor where codigodeudor.nkey_vendedor = ").Append(lngNkeyUsuario).Append(" and codigodeudor.nkey_cliente in(").Append(lngCodNkey).Append(") )  ");

          cSQL.Append(" and Vista_MetasAnalistaCli.periodo = convert(datetime,'").Append(pDtFchIni).Append("') ) as 'TotReal1',");
          cSQL.Append(" (select sum(real2) ");
          cSQL.Append(" from Vista_MetasAnalistaCli join cliente on (cliente.nkey_cliente = Vista_MetasAnalistaCli.nKey_Cliente ) where cliente.ncodholding  = ").Append(pNcodHolding);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
            cSQL.Append("  and Vista_MetasAnalistaCli.nkey_deudor = ").Append(lngNkeyUsuario);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "V"))
            cSQL.Append("  and Vista_MetasAnalistaCli.nkey_deudor in (select codigodeudor.nkey_deudor from codigodeudor where codigodeudor.nkey_vendedor = ").Append(lngNkeyUsuario).Append(" and codigodeudor.nkey_cliente in(").Append(lngCodNkey).Append(") )  ");

          cSQL.Append(" and Vista_MetasAnalistaCli.periodo = convert(datetime,'").Append(pDtFchIni).Append("') ) as 'TotReal2',");
          cSQL.Append(" (select sum(real3) ");
          cSQL.Append(" from Vista_MetasAnalistaCli join cliente on (cliente.nkey_cliente = Vista_MetasAnalistaCli.nKey_Cliente ) where cliente.ncodholding  = ").Append(pNcodHolding);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
            cSQL.Append("  and Vista_MetasAnalistaCli.nkey_deudor = ").Append(lngNkeyUsuario);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "V"))
            cSQL.Append("  and Vista_MetasAnalistaCli.nkey_deudor in (select codigodeudor.nkey_deudor from codigodeudor where codigodeudor.nkey_vendedor = ").Append(lngNkeyUsuario).Append(" and codigodeudor.nkey_cliente in(").Append(lngCodNkey).Append(") )  ");

          cSQL.Append(" and Vista_MetasAnalistaCli.periodo = convert(datetime,'").Append(pDtFchIni).Append("') ) as 'TotReal3', ");
          cSQL.Append(" (select sum(real4) ");
          cSQL.Append(" from Vista_MetasAnalistaCli join cliente on (cliente.nkey_cliente = Vista_MetasAnalistaCli.nKey_Cliente ) where cliente.ncodholding  = ").Append(pNcodHolding);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
            cSQL.Append("  and Vista_MetasAnalistaCli.nkey_deudor = ").Append(lngNkeyUsuario);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "V"))
            cSQL.Append("  and Vista_MetasAnalistaCli.nkey_deudor in (select codigodeudor.nkey_deudor from codigodeudor where codigodeudor.nkey_vendedor = ").Append(lngNkeyUsuario).Append(" and codigodeudor.nkey_cliente in(").Append(lngCodNkey).Append(") )  ");

          cSQL.Append(" and Vista_MetasAnalistaCli.periodo = convert(datetime,'").Append(pDtFchIni).Append("') ) as 'TotReal4', ");
          cSQL.Append(" (select sum(real5) ");
          cSQL.Append(" from Vista_MetasAnalistaCli join cliente on (cliente.nkey_cliente = Vista_MetasAnalistaCli.nKey_Cliente ) where cliente.ncodholding  = ").Append(pNcodHolding);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
            cSQL.Append("  and Vista_MetasAnalistaCli.nkey_deudor = ").Append(lngNkeyUsuario);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "V"))
            cSQL.Append("  and Vista_MetasAnalistaCli.nkey_deudor in (select codigodeudor.nkey_deudor from codigodeudor where codigodeudor.nkey_vendedor = ").Append(lngNkeyUsuario).Append(" and codigodeudor.nkey_cliente in(").Append(lngCodNkey).Append(") )  ");

          cSQL.Append(" and Vista_MetasAnalistaCli.periodo = convert(datetime,'").Append(pDtFchIni).Append("') ) as 'TotReal5', ");
          cSQL.Append(" (select sum(estimado1) ");
          cSQL.Append(" from Vista_MetasAnalistaCli join cliente on (cliente.nkey_cliente = Vista_MetasAnalistaCli.nKey_Cliente ) where cliente.ncodholding  = ").Append(pNcodHolding);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
            cSQL.Append("  and Vista_MetasAnalistaCli.nkey_deudor = ").Append(lngNkeyUsuario);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "V"))
            cSQL.Append("  and Vista_MetasAnalistaCli.nkey_deudor in (select codigodeudor.nkey_deudor from codigodeudor where codigodeudor.nkey_vendedor = ").Append(lngNkeyUsuario).Append(" and codigodeudor.nkey_cliente in(").Append(lngCodNkey).Append(") )  ");

          cSQL.Append(" and Vista_MetasAnalistaCli.periodo = convert(datetime,'").Append(pDtFchIni).Append("') ) as 'TotEst1',");
          cSQL.Append(" (select sum(estimado2) ");
          cSQL.Append(" from Vista_MetasAnalistaCli join cliente on (cliente.nkey_cliente = Vista_MetasAnalistaCli.nKey_Cliente ) where cliente.ncodholding  = ").Append(pNcodHolding);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
            cSQL.Append("  and Vista_MetasAnalistaCli.nkey_deudor = ").Append(lngNkeyUsuario);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "V"))
            cSQL.Append("  and Vista_MetasAnalistaCli.nkey_deudor in (select codigodeudor.nkey_deudor from codigodeudor where codigodeudor.nkey_vendedor = ").Append(lngNkeyUsuario).Append(" and codigodeudor.nkey_cliente in(").Append(lngCodNkey).Append(") )  ");

          cSQL.Append(" and Vista_MetasAnalistaCli.periodo = convert(datetime,'").Append(pDtFchIni).Append("') ) as 'TotEst2', ");
          cSQL.Append(" (select sum(estimado3) ");
          cSQL.Append(" from Vista_MetasAnalistaCli join cliente on (cliente.nkey_cliente = Vista_MetasAnalistaCli.nKey_Cliente ) where cliente.ncodholding  = ").Append(pNcodHolding);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
            cSQL.Append("  and Vista_MetasAnalistaCli.nkey_deudor = ").Append(lngNkeyUsuario);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "V"))
            cSQL.Append("  and Vista_MetasAnalistaCli.nkey_deudor in (select codigodeudor.nkey_deudor from codigodeudor where codigodeudor.nkey_vendedor = ").Append(lngNkeyUsuario).Append(" and codigodeudor.nkey_cliente in(").Append(lngCodNkey).Append(") )  ");

          cSQL.Append(" and Vista_MetasAnalistaCli.periodo = convert(datetime,'").Append(pDtFchIni).Append("') ) as 'TotEst3', ");
          cSQL.Append(" (select sum(estimado4) ");
          cSQL.Append(" from Vista_MetasAnalistaCli join cliente on (cliente.nkey_cliente = Vista_MetasAnalistaCli.nKey_Cliente ) where cliente.ncodholding  = ").Append(pNcodHolding);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
            cSQL.Append("  and Vista_MetasAnalistaCli.nkey_deudor = ").Append(lngNkeyUsuario);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "V"))
            cSQL.Append("  and Vista_MetasAnalistaCli.nkey_deudor in (select codigodeudor.nkey_deudor from codigodeudor where codigodeudor.nkey_vendedor = ").Append(lngNkeyUsuario).Append(" and codigodeudor.nkey_cliente in(").Append(lngCodNkey).Append(") )  ");

          cSQL.Append(" and Vista_MetasAnalistaCli.periodo = convert(datetime,'").Append(pDtFchIni).Append("') ) as 'TotEst4', ");
          cSQL.Append(" (select sum(estimado5) ");
          cSQL.Append(" from Vista_MetasAnalistaCli join cliente on (cliente.nkey_cliente = Vista_MetasAnalistaCli.nKey_Cliente ) where cliente.ncodholding  = ").Append(pNcodHolding);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
            cSQL.Append("  and Vista_MetasAnalistaCli.nkey_deudor = ").Append(lngNkeyUsuario);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "V"))
            cSQL.Append("  and Vista_MetasAnalistaCli.nkey_deudor in (select codigodeudor.nkey_deudor from codigodeudor where codigodeudor.nkey_vendedor = ").Append(lngNkeyUsuario).Append(" and codigodeudor.nkey_cliente in(").Append(lngCodNkey).Append(") )  ");

          cSQL.Append(" and Vista_MetasAnalistaCli.periodo = convert(datetime,'").Append(pDtFchIni).Append("') ) as 'TotEst5', ");
          cSQL.Append(" NomCLiente,ncodigodeudor , NomDeudor, periodo ");
          cSQL.Append(" from Vista_MetasAnalistaCli join cliente on (cliente.nkey_cliente = Vista_MetasAnalistaCli.nKey_Cliente ) where cliente.ncodholding  = ").Append(pNcodHolding);
          cSQL.Append(" and Vista_MetasAnalistaCli.periodo = convert(datetime,'").Append(pDtFchIni).Append("') ");
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
