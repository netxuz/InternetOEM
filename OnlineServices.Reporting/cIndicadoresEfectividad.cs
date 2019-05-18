using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using OnlineServices.Conn;

namespace OnlineServices.Reporting
{
  public class cIndicadoresEfectividad
  {
    private string lngCodNkey;
    public string CodNkey { get { return lngCodNkey; } set { lngCodNkey = value; } }

    private string lngCodDeudor;
    public string CodDeudor { get { return lngCodDeudor; } set { lngCodDeudor = value; } }

    private string lngCodRubro;
    public string CodRubro { get { return lngCodRubro; } set { lngCodRubro = value; } }

    private string pEstado;
    public string Estado { get { return pEstado; } set { pEstado = value; } }

    private string pCriterio;
    public string Criterio { get { return pCriterio; } set { pCriterio = value; } }

    private string pAnoIni;
    public string AnoIni { get { return pAnoIni; } set { pAnoIni = value; } }

    private string pMesIni;
    public string MesIni { get { return pMesIni; } set { pMesIni = value; } }

    private string pNcodHolding;
    public string NcodHolding { get { return pNcodHolding; } set { pNcodHolding = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private DBConn oConn;

    public cIndicadoresEfectividad()
    {

    }

    public cIndicadoresEfectividad(ref DBConn oConn)
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
          cSQL.Append("Select  tablafinalindicadorweb.periodo as 'periodo', ");
          cSQL.Append(" sum(tablafinalindicadorweb.deuda) as 'Deuda', ");
          cSQL.Append(" sum(tablafinalindicadorweb.Vencido) as 'Vencido', ");
          cSQL.Append(" sum(tablafinalindicadorweb.facturado) as 'Facturado', ");
          cSQL.Append(" sum(tablafinalindicadorweb.cobrado) as 'Cobrado', ");
          cSQL.Append(" sum(tablafinalindicadorweb.atraso) as 'Atraso', ");
          cSQL.Append(" sum(isnull(tablafinalindicadorweb.NC,0)) as 'NC', ");
          cSQL.Append(" sum(tablafinalindicadorweb.FP) as 'FP', ");
          cSQL.Append(" sum(tablafinalindicadorweb.Ficticio) as 'Ficticio', ");
          cSQL.Append(" sum(tablafinalindicadorweb.dso) as 'DSO', ");
          cSQL.Append(" sum(tablafinalindicadorweb.deuda_mas_30) as 'deuda_mas_30'  ");
          cSQL.Append(" From tablafinalindicadorweb ");
          cSQL.Append(" where tablafinalindicadorweb.nkey_cliente in(").Append(lngCodNkey).Append(") ");
          cSQL.Append(" and  tablafinalindicadorweb.tipoConsulta = 'Cliente' ");
          cSQL.Append(" group by tablafinalindicadorweb.periodo ");
          cSQL.Append(" order by tablafinalindicadorweb.periodo desc ");
        }
        else if (pEstado == "11")
        {
          cSQL.Append("Select  tablafinalindicadorweb.periodo as 'periodo', ");
          cSQL.Append(" sum(tablafinalindicadorweb.deuda) as 'Deuda', ");
          cSQL.Append(" sum(tablafinalindicadorweb.Vencido) as 'Vencido', ");
          cSQL.Append(" sum(tablafinalindicadorweb.facturado) as 'Facturado', ");
          cSQL.Append(" sum(tablafinalindicadorweb.cobrado) as 'Cobrado', ");
          cSQL.Append(" sum(tablafinalindicadorweb.atraso) as 'Atraso', ");
          cSQL.Append(" sum(isnull(tablafinalindicadorweb.NC,0)) as 'NC', ");
          cSQL.Append(" sum(tablafinalindicadorweb.FP) as 'FP', ");
          cSQL.Append(" sum(tablafinalindicadorweb.Ficticio) as 'Ficticio', ");
          cSQL.Append(" sum(tablafinalindicadorweb.dso) as 'DSO', ");
          cSQL.Append(" sum(tablafinalindicadorweb.deuda_mas_30) as 'deuda_mas_30'  ");
          cSQL.Append(" From tablafinalindicadorweb ");
          cSQL.Append(" join cliente on (cliente.nkey_cliente = tablafinalindicadorweb.nKey_Cliente ) ");
          cSQL.Append(" where cliente.ncodholding  = ").Append(pNcodHolding);
          cSQL.Append(" and  tablafinalindicadorweb.tipoConsulta = 'Cliente' ");
          cSQL.Append(" group by tablafinalindicadorweb.periodo ");
          cSQL.Append(" order by tablafinalindicadorweb.periodo desc ");
        }
        else if (pEstado == "1")
        {
          cSQL.Append("Select  tablafinalindicadorweb.periodo as 'periodo', ");
          cSQL.Append(" sum(tablafinalindicadorweb.deuda) as 'Deuda', ");
          cSQL.Append(" sum(tablafinalindicadorweb.Vencido) as 'Vencido', ");
          cSQL.Append(" sum(tablafinalindicadorweb.facturado) as 'Facturado', ");
          cSQL.Append(" sum(tablafinalindicadorweb.cobrado) as 'Cobrado', ");
          cSQL.Append(" sum(tablafinalindicadorweb.atraso) as 'Atraso', ");
          cSQL.Append(" sum(isnull(tablafinalindicadorweb.NC,0)) as 'NC', ");
          cSQL.Append(" sum(tablafinalindicadorweb.FP) as 'FP', ");
          cSQL.Append(" sum(tablafinalindicadorweb.Ficticio) as 'Ficticio', ");
          cSQL.Append(" sum(tablafinalindicadorweb.dso) as 'DSO', ");
          cSQL.Append(" sum(tablafinalindicadorweb.deuda_mas_30) as 'deuda_mas_30' , ");
          cSQL.Append(" Vendedor.snombre as 'NomVendedor' ");
          cSQL.Append(" From tablafinalindicadorweb ");
          cSQL.Append(" JOIN Vendedor ");
          cSQL.Append(" On (Vendedor.nkey_vendedor=").Append(lngCodDeudor).Append(") ");
          cSQL.Append(" where tablafinalindicadorweb.nkey_cliente in(").Append(lngCodNkey).Append(") ");
          cSQL.Append(" and tablafinalindicadorweb.nkey_vendedor =").Append(lngCodDeudor);
          cSQL.Append(" and tablafinalindicadorweb.tipoConsulta = 'Vendedor' ");
          cSQL.Append(" group by tablafinalindicadorweb.periodo, Vendedor.snombre ");
          cSQL.Append(" order by tablafinalindicadorweb.periodo desc ");
        }
        else if (pEstado == "2")
        {
          cSQL.Append("Select  tablafinalindicadorweb.periodo as 'periodo', ");
          cSQL.Append(" sum(tablafinalindicadorweb.deuda) as 'Deuda', ");
          cSQL.Append(" sum(tablafinalindicadorweb.Vencido) as 'Vencido', ");
          cSQL.Append(" sum(tablafinalindicadorweb.facturado) as 'Facturado', ");
          cSQL.Append(" sum(tablafinalindicadorweb.cobrado) as 'Cobrado', ");
          cSQL.Append(" sum(tablafinalindicadorweb.atraso) as 'Atraso', ");
          cSQL.Append(" sum(isnull(tablafinalindicadorweb.NC,0)) as 'NC', ");
          cSQL.Append(" sum(tablafinalindicadorweb.FP) as 'FP', ");
          cSQL.Append(" sum(tablafinalindicadorweb.Ficticio) as 'Ficticio', ");
          cSQL.Append(" sum(tablafinalindicadorweb.dso) as 'DSO', ");
          cSQL.Append(" sum(tablafinalindicadorweb.deuda_mas_30) as 'deuda_mas_30' , ");
          cSQL.Append(" analista.snombre as 'NomAnalista' ");
          cSQL.Append(" From tablafinalindicadorweb ");
          cSQL.Append(" JOIN Analista On (Analista.nkey_analista = ").Append(lngCodDeudor).Append(") ");
          cSQL.Append(" where tablafinalindicadorweb.nkey_cliente in(").Append(lngCodNkey).Append(") ");
          cSQL.Append(" and tablafinalindicadorweb.nkey_analista = ").Append(lngCodDeudor);
          cSQL.Append(" and  tablafinalindicadorweb.tipoConsulta = 'Analista' ");
          cSQL.Append(" group by tablafinalindicadorweb.periodo, analista.snombre ");
          cSQL.Append(" order by tablafinalindicadorweb.periodo desc ");
        }
        else if (pEstado == "3")
        {
          cSQL.Append("Select  tablafinalindicadorweb.periodo as 'periodo', ");
          cSQL.Append(" sum(tablafinalindicadorweb.deuda) as 'Deuda', ");
          cSQL.Append(" sum(tablafinalindicadorweb.Vencido) as 'Vencido', ");
          cSQL.Append(" sum(tablafinalindicadorweb.facturado) as 'Facturado', ");
          cSQL.Append(" sum(tablafinalindicadorweb.cobrado) as 'Cobrado', ");
          cSQL.Append(" sum(tablafinalindicadorweb.atraso) as 'Atraso', ");
          cSQL.Append(" sum(isnull(tablafinalindicadorweb.NC,0)) as 'NC', ");
          cSQL.Append(" sum(tablafinalindicadorweb.FP) as 'FP', ");
          cSQL.Append(" sum(tablafinalindicadorweb.Ficticio) as 'Ficticio', ");
          cSQL.Append(" sum(tablafinalindicadorweb.dso) as 'DSO', ");
          cSQL.Append(" sum(tablafinalindicadorweb.deuda_mas_30) as 'deuda_mas_30' , ");
          cSQL.Append(" Deudor.snombre as 'NomDeudor' ");
          cSQL.Append(" From tablafinalindicadorweb ");
          cSQL.Append(" Join deudor ");
          cSQL.Append(" On (deudor.nkey_deudor =").Append(lngCodDeudor).Append(") ");
          cSQL.Append(" where tablafinalindicadorweb.nkey_cliente in(").Append(lngCodNkey).Append(") ");
          cSQL.Append(" and tablafinalindicadorweb.nkey_deudor = ").Append(lngCodDeudor);
          cSQL.Append(" and tablafinalindicadorweb.tipoConsulta = 'Deudor' ");
          cSQL.Append(" group by tablafinalindicadorweb.periodo, Deudor.snombre ");
          cSQL.Append(" order by tablafinalindicadorweb.periodo desc ");
        }
        else if (pEstado == "4")
        {
          cSQL.Append("Select  tablafinalindicadorweb.periodo as 'periodo', ");
          cSQL.Append(" sum(tablafinalindicadorweb.deuda) as 'Deuda', ");
          cSQL.Append(" sum(tablafinalindicadorweb.Vencido) as 'Vencido', ");
          cSQL.Append(" sum(tablafinalindicadorweb.facturado) as 'Facturado', ");
          cSQL.Append(" sum(tablafinalindicadorweb.cobrado) as 'Cobrado', ");
          cSQL.Append(" sum(tablafinalindicadorweb.atraso) as 'Atraso', ");
          cSQL.Append(" sum(isnull(tablafinalindicadorweb.NC,0)) as 'NC', ");
          cSQL.Append(" sum(tablafinalindicadorweb.FP) as 'FP', ");
          cSQL.Append(" sum(tablafinalindicadorweb.Ficticio) as 'Ficticio', ");
          cSQL.Append(" sum(tablafinalindicadorweb.dso) as 'DSO', ");
          cSQL.Append(" sum(tablafinalindicadorweb.deuda_mas_30) as 'deuda_mas_30' , ");
          cSQL.Append(" isnull(rubros.srubro,'Sin Rubro') as 'Rubro' ");
          cSQL.Append(" From tablafinalindicadorweb ");
          cSQL.Append(" left JOIN rubros On (rubros.nKey_Rubros = ").Append(lngCodDeudor).Append(") ");
          cSQL.Append(" where tablafinalindicadorweb.nkey_cliente in(").Append(lngCodNkey).Append(") ");
          cSQL.Append(" and rubros.nkey_rubros = tablafinalindicadorweb.nkey_vendedor ");
          cSQL.Append(" and  tablafinalindicadorweb.tipoConsulta = 'Rubros' ");
          cSQL.Append(" group by tablafinalindicadorweb.periodo, rubros.srubro ");
          cSQL.Append(" order by tablafinalindicadorweb.periodo desc ");
        }
        else if (pEstado == "5")
        {
          cSQL.Append("Select  tablafinalindicadorweb.periodo as 'periodo', ");
          cSQL.Append(" sum(tablafinalindicadorweb.deuda) as 'Deuda', ");
          cSQL.Append(" sum(tablafinalindicadorweb.Vencido) as 'Vencido', ");
          cSQL.Append(" sum(tablafinalindicadorweb.facturado) as 'Facturado', ");
          cSQL.Append(" sum(tablafinalindicadorweb.cobrado) as 'Cobrado', ");
          cSQL.Append(" sum(tablafinalindicadorweb.atraso) as 'Atraso', ");
          cSQL.Append(" sum(isnull(tablafinalindicadorweb.NC,0)) as 'NC', ");
          cSQL.Append(" sum(tablafinalindicadorweb.FP) as 'FP', ");
          cSQL.Append(" sum(tablafinalindicadorweb.Ficticio) as 'Ficticio', ");
          cSQL.Append(" sum(tablafinalindicadorweb.dso) as 'DSO', ");
          cSQL.Append(" sum(tablafinalindicadorweb.deuda_mas_30) as 'deuda_mas_30' , ");
          cSQL.Append(" isnull(rubros.srubro,'Sin Canal')  as 'Rubro' ");
          cSQL.Append(" From tablafinalindicadorweb ");
          cSQL.Append(" left JOIN rubros On (rubros.nkey_rubros = ").Append(lngCodDeudor).Append(") ");
          cSQL.Append(" where tablafinalindicadorweb.nkey_cliente in(").Append(lngCodNkey).Append(") ");
          cSQL.Append(" and rubros.nkey_rubros = tablafinalindicadorweb.nkey_vendedor ");
          cSQL.Append(" and  tablafinalindicadorweb.tipoConsulta = 'Canal' ");
          cSQL.Append(" group by tablafinalindicadorweb.periodo, rubros.srubro ");
          cSQL.Append(" order by tablafinalindicadorweb.periodo desc ");
        }
        else if (pEstado == "6")
        {
          cSQL.Append("Select  tablafinalindicadorweb.periodo as 'periodo', ");
          cSQL.Append(" sum(tablafinalindicadorweb.deuda) as 'Deuda', ");
          cSQL.Append(" sum(tablafinalindicadorweb.Vencido) as 'Vencido', ");
          cSQL.Append(" sum(tablafinalindicadorweb.facturado) as 'Facturado', ");
          cSQL.Append(" sum(tablafinalindicadorweb.cobrado) as 'Cobrado', ");
          cSQL.Append(" sum(tablafinalindicadorweb.atraso) as 'Atraso', ");
          cSQL.Append(" sum(isnull(tablafinalindicadorweb.NC,0)) as 'NC', ");
          cSQL.Append(" sum(tablafinalindicadorweb.FP) as 'FP', ");
          cSQL.Append(" sum(tablafinalindicadorweb.Ficticio) as 'Ficticio', ");

          if (pCriterio == "2")
          {
            cSQL.Append(" isnull(isnull(sum(tablafinalindicadorweb.atraso),0)/isnull(sum(tablafinalindicadorweb.cobrado),null),0) as 'DBT', ");
          }
          else if (pCriterio == "3")
          {
            cSQL.Append(" ((isnull(isnull(sum(tablafinalindicadorweb.Vencido),0)/isnull(sum(tablafinalindicadorweb.deuda),null),0))*100) as 'Overdue', ");
          }

          cSQL.Append(" sum(tablafinalindicadorweb.dso) as 'DSO', ");
          cSQL.Append(" sum(tablafinalindicadorweb.deuda_mas_30) as 'deuda_mas_30' , ");
          cSQL.Append(" Deudor.snombre as 'NomDeudor', ");
          cSQL.Append(" CodigoDeudor.ncodigodeudor as 'CodDeudor' ");
          cSQL.Append(" From tablafinalindicadorweb ");
          cSQL.Append(" Join deudor ");
          cSQL.Append(" On (deudor.nkey_deudor = tablafinalindicadorweb.nkey_deudor) ");
          cSQL.Append(" Join Codigodeudor ");
          cSQL.Append(" On (Codigodeudor.nkey_deudor = deudor.nkey_deudor and codigodeudor.nkey_cliente = tablafinalindicadorweb.nkey_cliente) ");
          cSQL.Append(" where tablafinalindicadorweb.nkey_cliente in(").Append(lngCodNkey).Append(") ");
          cSQL.Append(" and  tablafinalindicadorweb.tipoConsulta = 'Deudor' ");
          cSQL.Append(" and tablafinalindicadorweb.ano =").Append(pAnoIni);
          cSQL.Append(" and tablafinalindicadorweb.mes =").Append(pMesIni);
          cSQL.Append(" group by tablafinalindicadorweb.periodo, Deudor.snombre, CodigoDeudor.ncodigodeudor ");

          if (pCriterio == "1")
          {
            cSQL.Append(" Order by sum(tablafinalindicadorweb.dso) desc ");
          }
          else if (pCriterio == "2")
          {
            cSQL.Append(" having sum(tablafinalindicadorweb.atraso) > 0 And sum(tablafinalindicadorweb.cobrado) > 0 ");
            cSQL.Append(" Order by isnull(isnull(sum(tablafinalindicadorweb.atraso),0)/isnull(sum(tablafinalindicadorweb.cobrado),null),0) desc ");

          }
          else if (pCriterio == "3")
          {
            cSQL.Append(" having sum(tablafinalindicadorweb.Vencido) > 0 And sum(tablafinalindicadorweb.deuda) > 0 ");
            cSQL.Append(" Order by ((isnull(isnull(sum(tablafinalindicadorweb.Vencido),0)/isnull(sum(tablafinalindicadorweb.deuda),null),0))*100) desc ");
          }
          else
          {
            cSQL.Append(" order by tablafinalindicadorweb.periodo desc ");
          }

        }
        else if (pEstado == "7")
        {
          cSQL.Append("Select  tablafinalindicadorweb.periodo as 'periodo', ");
          cSQL.Append(" sum(tablafinalindicadorweb.deuda) as 'Deuda', ");
          cSQL.Append(" sum(tablafinalindicadorweb.Vencido) as 'Vencido', ");
          cSQL.Append(" sum(tablafinalindicadorweb.facturado) as 'Facturado', ");
          cSQL.Append(" sum(tablafinalindicadorweb.cobrado) as 'Cobrado', ");
          cSQL.Append(" sum(tablafinalindicadorweb.atraso) as 'Atraso', ");
          cSQL.Append(" sum(isnull(tablafinalindicadorweb.NC,0)) as 'NC', ");
          cSQL.Append(" sum(tablafinalindicadorweb.FP) as 'FP', ");
          cSQL.Append(" sum(tablafinalindicadorweb.Ficticio) as 'Ficticio', ");

          if (pCriterio == "2")
          {
            cSQL.Append(" isnull(isnull(sum(tablafinalindicadorweb.atraso),0)/isnull(sum(tablafinalindicadorweb.cobrado),null),0) as 'DBT', ");
          }
          else if (pCriterio == "3")
          {
            cSQL.Append(" ((isnull(isnull(sum(tablafinalindicadorweb.Vencido),0)/isnull(sum(tablafinalindicadorweb.deuda),null),0))*100) as 'Overdue', ");
          }

          cSQL.Append(" sum(tablafinalindicadorweb.dso) as 'DSO', ");
          cSQL.Append(" sum(tablafinalindicadorweb.deuda_mas_30) as 'deuda_mas_30' , ");
          cSQL.Append(" isnull(rubros.srubro, 'Sin Rubro') as 'Rubro' ");
          cSQL.Append(" From tablafinalindicadorweb ");
          cSQL.Append(" left JOIN rubros On (rubros.nkey_rubros = tablafinalindicadorweb.nkey_vendedor ) ");
          cSQL.Append(" where tablafinalindicadorweb.nkey_cliente in(").Append(lngCodNkey).Append(") ");
          cSQL.Append(" and  tablafinalindicadorweb.tipoConsulta = 'Rubros' ");
          cSQL.Append(" and tablafinalindicadorweb.ano = ").Append(pAnoIni);
          cSQL.Append(" and tablafinalindicadorweb.mes = ").Append(pMesIni);
          cSQL.Append(" group by tablafinalindicadorweb.periodo, rubros.srubro ");

          if (pCriterio == "1")
          {
            cSQL.Append(" Order by sum(tablafinalindicadorweb.dso) desc ");
          }
          else if (pCriterio == "2")
          {
            cSQL.Append(" having sum(tablafinalindicadorweb.atraso) > 0 And sum(tablafinalindicadorweb.cobrado) > 0 ");
            cSQL.Append(" Order by isnull(isnull(sum(tablafinalindicadorweb.atraso),0)/isnull(sum(tablafinalindicadorweb.cobrado),null),0) desc ");
          }
          else if (pCriterio == "3")
          {
            cSQL.Append(" having sum(tablafinalindicadorweb.Vencido) > 0 And sum(tablafinalindicadorweb.deuda) > 0 ");
            cSQL.Append(" Order by ((isnull(isnull(sum(tablafinalindicadorweb.Vencido),0)/isnull(sum(tablafinalindicadorweb.deuda),null),0))*100) desc ");
          }
          else
          {
            cSQL.Append(" order by rubros.srubro asc ");
          }

        }
        else if (pEstado == "8")
        {
          cSQL.Append("Select  tablafinalindicadorweb.periodo as 'periodo', ");
          cSQL.Append(" sum(tablafinalindicadorweb.deuda) as 'Deuda', ");
          cSQL.Append(" sum(tablafinalindicadorweb.Vencido) as 'Vencido', ");
          cSQL.Append(" sum(tablafinalindicadorweb.facturado) as 'Facturado', ");
          cSQL.Append(" sum(tablafinalindicadorweb.cobrado) as 'Cobrado', ");
          cSQL.Append(" sum(tablafinalindicadorweb.atraso) as 'Atraso', ");
          cSQL.Append(" sum(isnull(tablafinalindicadorweb.NC,0)) as 'NC', ");
          cSQL.Append(" sum(tablafinalindicadorweb.FP) as 'FP', ");
          cSQL.Append(" sum(tablafinalindicadorweb.Ficticio) as 'Ficticio', ");

          if (pCriterio == "2")
          {
            cSQL.Append(" isnull(isnull(sum(tablafinalindicadorweb.atraso),0)/isnull(sum(tablafinalindicadorweb.cobrado),null),0) as 'DBT', ");
          }
          else if (pCriterio == "3")
          {
            cSQL.Append(" ((isnull(isnull(sum(tablafinalindicadorweb.Vencido),0)/isnull(sum(tablafinalindicadorweb.deuda),null),0))*100) as 'Overdue', ");
          }
          cSQL.Append(" sum(tablafinalindicadorweb.dso) as 'DSO', ");
          cSQL.Append(" sum(tablafinalindicadorweb.deuda_mas_30) as 'deuda_mas_30' , ");
          cSQL.Append(" Vendedor.snombre as 'NomVendedor' ");
          cSQL.Append(" From tablafinalindicadorweb ");
          cSQL.Append(" JOIN Vendedor ");
          cSQL.Append(" On (Vendedor.nkey_vendedor= tablafinalindicadorweb.nkey_vendedor ) ");
          cSQL.Append(" where tablafinalindicadorweb.nkey_cliente in(").Append(lngCodNkey).Append(") ");
          cSQL.Append(" and tablafinalindicadorweb.tipoConsulta = 'Vendedor' ");
          cSQL.Append(" and tablafinalindicadorweb.ano = ").Append(pAnoIni);
          cSQL.Append(" and tablafinalindicadorweb.mes = ").Append(pMesIni);
          cSQL.Append(" group by tablafinalindicadorweb.periodo, Vendedor.snombre ");

          if (pCriterio == "1")
          {
            cSQL.Append(" Order by sum(tablafinalindicadorweb.dso) desc ");
          }
          else if (pCriterio == "2")
          {
            cSQL.Append(" having sum(tablafinalindicadorweb.atraso) > 0 And sum(tablafinalindicadorweb.cobrado) > 0 ");
            cSQL.Append(" Order by isnull(isnull(sum(tablafinalindicadorweb.atraso),0)/isnull(sum(tablafinalindicadorweb.cobrado),null),0) desc ");
          }
          else if (pCriterio == "3")
          {
            cSQL.Append(" having sum(tablafinalindicadorweb.Vencido) > 0 And sum(tablafinalindicadorweb.deuda) > 0 ");
            cSQL.Append(" Order by ((isnull(isnull(sum(tablafinalindicadorweb.Vencido),0)/isnull(sum(tablafinalindicadorweb.deuda),null),0))*100) desc ");
          }
          else
          {
            cSQL.Append(" order by Vendedor.snombre  asc ");
          }
        }
        else if (pEstado == "9")
        {
          cSQL.Append("Select  tablafinalindicadorweb.periodo as 'periodo', ");
          cSQL.Append(" sum(tablafinalindicadorweb.deuda) as 'Deuda', ");
          cSQL.Append(" sum(tablafinalindicadorweb.Vencido) as 'Vencido', ");
          cSQL.Append(" sum(tablafinalindicadorweb.facturado) as 'Facturado', ");
          cSQL.Append(" sum(tablafinalindicadorweb.cobrado) as 'Cobrado', ");
          cSQL.Append(" sum(tablafinalindicadorweb.atraso) as 'Atraso', ");
          cSQL.Append(" sum(isnull(tablafinalindicadorweb.NC,0)) as 'NC', ");
          cSQL.Append(" sum(tablafinalindicadorweb.FP) as 'FP', ");
          cSQL.Append(" sum(tablafinalindicadorweb.Ficticio) as 'Ficticio', ");

          if (pCriterio == "2")
          {
            cSQL.Append(" isnull(isnull(sum(tablafinalindicadorweb.atraso),0)/isnull(sum(tablafinalindicadorweb.cobrado),null),0) as 'DBT', ");
          }
          else if (pCriterio == "3")
          {
            cSQL.Append(" ((isnull(isnull(sum(tablafinalindicadorweb.Vencido),0)/isnull(sum(tablafinalindicadorweb.deuda),null),0))*100) as 'Overdue', ");
          }

          cSQL.Append(" sum(tablafinalindicadorweb.dso) as 'DSO', ");
          cSQL.Append(" sum(tablafinalindicadorweb.deuda_mas_30) as 'deuda_mas_30' , ");
          cSQL.Append(" analista.snombre as 'NomAnalista' ");
          cSQL.Append(" From tablafinalindicadorweb ");
          cSQL.Append(" JOIN Analista On (Analista.nkey_analista = tablafinalindicadorweb.nkey_analista ) ");
          cSQL.Append(" where tablafinalindicadorweb.nkey_cliente in (").Append(lngCodNkey).Append(") ");
          cSQL.Append(" and  tablafinalindicadorweb.tipoConsulta = 'Analista' ");
          cSQL.Append(" and tablafinalindicadorweb.ano = ").Append(pAnoIni);
          cSQL.Append(" and tablafinalindicadorweb.mes = ").Append(pMesIni);
          cSQL.Append(" group by tablafinalindicadorweb.periodo, analista.snombre ");

          if (pCriterio == "1")
          {
            cSQL.Append(" Order by sum(tablafinalindicadorweb.dso) desc ");
          }
          else if (pCriterio == "2")
          {
            cSQL.Append(" having sum(tablafinalindicadorweb.atraso) > 0 And sum(tablafinalindicadorweb.cobrado) > 0 ");
            cSQL.Append(" Order by isnull(isnull(sum(tablafinalindicadorweb.atraso),0)/isnull(sum(tablafinalindicadorweb.cobrado),null),0) desc ");
          }
          else if (pEstado == "3")
          {
            cSQL.Append(" having sum(tablafinalindicadorweb.Vencido) > 0 And sum(tablafinalindicadorweb.deuda) > 0 ");
            cSQL.Append(" Order by ((isnull(isnull(sum(tablafinalindicadorweb.Vencido),0)/isnull(sum(tablafinalindicadorweb.deuda),null),0))*100) desc ");
          }
          else
          {
            cSQL.Append(" order by tablafinalindicadorweb.periodo desc ");
          }
        }
        else if (pEstado == "10")
        {
          cSQL.Append("Select  tablafinalindicadorweb.periodo as 'periodo', ");
          cSQL.Append(" sum(tablafinalindicadorweb.deuda) as 'Deuda', ");
          cSQL.Append(" sum(tablafinalindicadorweb.Vencido) as 'Vencido', ");
          cSQL.Append(" sum(tablafinalindicadorweb.facturado) as 'Facturado', ");
          cSQL.Append(" sum(tablafinalindicadorweb.cobrado) as 'Cobrado', ");
          cSQL.Append(" sum(tablafinalindicadorweb.atraso) as 'Atraso', ");
          cSQL.Append(" sum(isnull(tablafinalindicadorweb.NC,0)) as 'NC', ");
          cSQL.Append(" sum(tablafinalindicadorweb.FP) as 'FP', ");
          cSQL.Append(" sum(tablafinalindicadorweb.Ficticio) as 'Ficticio', ");

          if (pEstado == "2")
          {
            cSQL.Append(" isnull(isnull(sum(tablafinalindicadorweb.atraso),0)/isnull(sum(tablafinalindicadorweb.cobrado),null),0) as 'DBT', ");
          }
          else if (pEstado == "3")
          {
            cSQL.Append(" ((isnull(isnull(sum(tablafinalindicadorweb.Vencido),0)/isnull(sum(tablafinalindicadorweb.deuda),null),0))*100) as 'Overdue', ");

          }

          cSQL.Append(" sum(tablafinalindicadorweb.dso) as 'DSO', ");
          cSQL.Append(" sum(tablafinalindicadorweb.deuda_mas_30) as 'deuda_mas_30' , ");
          cSQL.Append(" isnull(rubros.srubro,'Sin Canal') as 'Rubro' ");
          cSQL.Append(" From tablafinalindicadorweb ");
          cSQL.Append(" left JOIN rubros On (rubros.nkey_rubros = tablafinalindicadorweb.nkey_vendedor ) ");
          cSQL.Append(" where tablafinalindicadorweb.nkey_cliente in(").Append(lngCodNkey).Append(") ");
          cSQL.Append(" and  tablafinalindicadorweb.tipoConsulta = 'Canal' ");
          cSQL.Append(" and tablafinalindicadorweb.ano = ").Append(pAnoIni);
          cSQL.Append(" and tablafinalindicadorweb.mes = ").Append(pMesIni);
          cSQL.Append(" group by tablafinalindicadorweb.periodo, rubros.srubro ");

          if (pEstado == "1")
          {
            cSQL.Append(" Order by sum(tablafinalindicadorweb.dso) desc ");
          }
          else if (pEstado == "2")
          {
            cSQL.Append(" having sum(tablafinalindicadorweb.atraso) > 0 And sum(tablafinalindicadorweb.cobrado) > 0 ");
            cSQL.Append(" Order by isnull(isnull(sum(tablafinalindicadorweb.atraso),0)/isnull(sum(tablafinalindicadorweb.cobrado),null),0) desc ");
          }
          else if (pEstado == "3")
          {
            cSQL.Append(" having sum(tablafinalindicadorweb.Vencido) > 0 And sum(tablafinalindicadorweb.deuda) > 0 ");
            cSQL.Append(" Order by ((isnull(isnull(sum(tablafinalindicadorweb.Vencido),0)/isnull(sum(tablafinalindicadorweb.deuda),null),0))*100) desc ");
          }
          else
          {
            cSQL.Append(" order by rubros.srubro asc ");
          }

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

