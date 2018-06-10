using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using OnlineServices.Conn;


namespace OnlineServices.Reporting
{
  public class cComportamientoPago
  {
    private string lngCodNkey;
    public string CodNkey { get { return lngCodNkey; } set { lngCodNkey = value; } }

    private string lngCodDeudor;
    public string CodDeudor { get { return lngCodDeudor; } set { lngCodDeudor = value; } }

    private string lngCodAnalista;
    public string CodAnalista { get { return lngCodAnalista; } set { lngCodAnalista = value; } }

    private string sTipoUsuario;
    public string TipoUsuario { get { return sTipoUsuario; } set { sTipoUsuario = value; } }

    private string lngNkeyUsuario;
    public string NkeyUsuario { get { return lngNkeyUsuario; } set { lngNkeyUsuario = value; } }

    private string pEstado;
    public string Estado { get { return pEstado; } set { pEstado = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private DBConn oConn;

    public cComportamientoPago() {

    }

    public cComportamientoPago(ref DBConn oConn) {
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
          cSQL.Append("Select cliente.snombre as 'NomCliente', tablacomportamientopagoweb.periodo as 'periodo', tablacomportamientopagoweb.orden as 'orden', sum(tablacomportamientopagoweb.Actual) as 'Actual', ");
          cSQL.Append(" sum(isnull(tablacomportamientopagoweb.mes1,0)) as 'Mes-1', sum(isnull(tablacomportamientopagoweb.mes2,0)) as 'Mes-2', sum(isnull(tablacomportamientopagoweb.mes3,0)) as 'Mes-3', sum(isnull(tablacomportamientopagoweb.mes4,0)) as 'Mes-4', ");
          cSQL.Append(" sum(isnull(tablacomportamientopagoweb.mes5,0)) as 'Mes-5', sum(isnull(tablacomportamientopagoweb.mes6,0)) as 'Mes-6', sum(isnull(tablacomportamientopagoweb.mes7,0)) as 'Mes-7', sum(isnull(tablacomportamientopagoweb.mes8,0)) as 'Mes-8', ");
          cSQL.Append(" sum(isnull(tablacomportamientopagoweb.mes9,0)) as 'Mes-9', sum(isnull(tablacomportamientopagoweb.mes10,0)) as 'Mes-10', tablacomportamientopagoweb.fechafactact as 'FechaFact', tablacomportamientopagoweb.mesano as 'MesAno', ");
          cSQL.Append(" tablacomportamientopagoweb.mesano1 as 'MesAno1', tablacomportamientopagoweb.mesano2 as 'MesAno2', tablacomportamientopagoweb.mesano3 as 'MesAno3', tablacomportamientopagoweb.mesano4 as 'MesAno4', tablacomportamientopagoweb.mesano5 as 'MesAno5', ");
          cSQL.Append(" tablacomportamientopagoweb.mesano6 as 'MesAno6', tablacomportamientopagoweb.mesano7 as 'MesAno7', tablacomportamientopagoweb.mesano8 as 'MesAno8', tablacomportamientopagoweb.mesano9 as 'MesAno9', tablacomportamientopagoweb.mesfila  as 'Mesfila' ");
          cSQL.Append(" From tablacomportamientopagoweb JOIN cliente ON (tablacomportamientopagoweb.nkey_cliente = cliente.nkey_cliente) where tablacomportamientopagoweb.nkey_cliente = ").Append(lngCodNkey);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "D"))
            cSQL.Append("  and tablacomportamientopagoweb.nkey_deudor = ").Append(lngNkeyUsuario);

          if ((!string.IsNullOrEmpty(sTipoUsuario)) && (sTipoUsuario == "V"))
            cSQL.Append("  and tablacomportamientopagoweb.nkey_deudor  in (select nkey_deudor from codigodeudor where nkey_cliente = ").Append(lngCodNkey).Append(" and nkey_vendedor = ").Append(lngNkeyUsuario).Append(") ");

          cSQL.Append(" group by cliente.snombre , ");
          cSQL.Append(" tablacomportamientopagoweb.periodo, tablacomportamientopagoweb.orden, tablacomportamientopagoweb.fechafactact, tablacomportamientopagoweb.mesano, tablacomportamientopagoweb.mesano1, tablacomportamientopagoweb.mesano2, ");
          cSQL.Append(" tablacomportamientopagoweb.mesano3, tablacomportamientopagoweb.mesano4, tablacomportamientopagoweb.mesano5, tablacomportamientopagoweb.mesano6, tablacomportamientopagoweb.mesano7, tablacomportamientopagoweb.mesano8, tablacomportamientopagoweb.mesano9, ");
          cSQL.Append(" tablacomportamientopagoweb.mesfila order by tablacomportamientopagoweb.orden ");
        }
        else if (pEstado == "2")
        {
          cSQL.Append("Select Analista.snombre as 'NomDeudor', tablacomportamientopagoweb.periodo as 'periodo', tablacomportamientopagoweb.orden as 'orden', sum(tablacomportamientopagoweb.Actual) as 'Actual', sum(tablacomportamientopagoweb.mes1) as 'Mes-1', sum(tablacomportamientopagoweb.mes2) as 'Mes-2', ");
          cSQL.Append(" sum(tablacomportamientopagoweb.mes3) as 'Mes-3', sum(tablacomportamientopagoweb.mes4) as 'Mes-4', sum(tablacomportamientopagoweb.mes5) as 'Mes-5', sum(tablacomportamientopagoweb.mes6) as 'Mes-6', sum(tablacomportamientopagoweb.mes7) as 'Mes-7', sum(tablacomportamientopagoweb.mes8) as 'Mes-8', ");
          cSQL.Append(" sum(tablacomportamientopagoweb.mes9) as 'Mes-9', sum(tablacomportamientopagoweb.mes10) as 'Mes-10', tablacomportamientopagoweb.fechafactact as 'FechaFact', tablacomportamientopagoweb.mesano as 'MesAno', tablacomportamientopagoweb.mesano1 as 'MesAno1', tablacomportamientopagoweb.mesano2 as 'MesAno2', ");
          cSQL.Append(" tablacomportamientopagoweb.mesano3 as 'MesAno3', tablacomportamientopagoweb.mesano4 as 'MesAno4', tablacomportamientopagoweb.mesano5 as 'MesAno5', tablacomportamientopagoweb.mesano6 as 'MesAno6', tablacomportamientopagoweb.mesano7 as 'MesAno7', tablacomportamientopagoweb.mesano8 as 'MesAno8', tablacomportamientopagoweb.mesano9 as 'MesAno9', ");
          cSQL.Append(" tablacomportamientopagoweb.mesfila  as 'Mesfila' From tablacomportamientopagoweb join codigodeudor on (codigodeudor.nkey_cliente = tablacomportamientopagoweb.nkey_cliente  and codigodeudor.nkey_deudor = tablacomportamientopagoweb.nkey_deudor) ");
          cSQL.Append(" join deudor on (deudor.nkey_deudor = codigodeudor.nkey_deudor) Join Analista On(Analista.nKey_Analista = ").Append(lngCodAnalista).Append(") ");
          cSQL.Append(" Join Servicio On(Servicio.nKey_Cliente = tablacomportamientopagoweb.nKey_Cliente And Servicio.nKEy_Deudor = tablacomportamientopagoweb.nKEy_Deudor ");
          cSQL.Append(" and servicio.nkey_analista = ").Append(lngCodAnalista).Append(" ) where tablacomportamientopagoweb.nkey_deudor = ").Append(lngCodDeudor).Append(" and tablacomportamientopagoweb.nkey_cliente= ").Append(lngCodNkey);
          cSQL.Append(" group by Analista.snombre , tablacomportamientopagoweb.periodo, tablacomportamientopagoweb.orden, tablacomportamientopagoweb.fechafactact, tablacomportamientopagoweb.mesano, tablacomportamientopagoweb.mesano1, tablacomportamientopagoweb.mesano2, tablacomportamientopagoweb.mesano3, ");
          cSQL.Append(" tablacomportamientopagoweb.mesano4, tablacomportamientopagoweb.mesano5, tablacomportamientopagoweb.mesano6, tablacomportamientopagoweb.mesano7, tablacomportamientopagoweb.mesano8, tablacomportamientopagoweb.mesano9, tablacomportamientopagoweb.mesfila order by tablacomportamientopagoweb.orden ");
        }
        else if (pEstado == "3") {
          cSQL.Append(" Select Deudor.snombre as 'NomDeudor', tablacomportamientopagoweb.periodo as 'periodo', tablacomportamientopagoweb.orden as 'orden', sum(tablacomportamientopagoweb.Actual) as 'Actual', sum(tablacomportamientopagoweb.mes1) as 'Mes-1', sum(tablacomportamientopagoweb.mes2) as 'Mes-2', sum(tablacomportamientopagoweb.mes3) as 'Mes-3', sum(tablacomportamientopagoweb.mes4) as 'Mes-4', ");
          cSQL.Append(" sum(tablacomportamientopagoweb.mes5) as 'Mes-5', sum(tablacomportamientopagoweb.mes6) as 'Mes-6', sum(tablacomportamientopagoweb.mes7) as 'Mes-7', sum(tablacomportamientopagoweb.mes8) as 'Mes-8', sum(tablacomportamientopagoweb.mes9) as 'Mes-9', sum(tablacomportamientopagoweb.mes10) as 'Mes-10', tablacomportamientopagoweb.fechafactact as 'FechaFact', tablacomportamientopagoweb.mesano as 'MesAno', tablacomportamientopagoweb.mesano1 as 'MesAno1', ");
          cSQL.Append(" tablacomportamientopagoweb.mesano2 as 'MesAno2', tablacomportamientopagoweb.mesano3 as 'MesAno3', tablacomportamientopagoweb.mesano4 as 'MesAno4', tablacomportamientopagoweb.mesano5 as 'MesAno5', tablacomportamientopagoweb.mesano6 as 'MesAno6', tablacomportamientopagoweb.mesano7 as 'MesAno7', tablacomportamientopagoweb.mesano8 as 'MesAno8', tablacomportamientopagoweb.mesano9 as 'MesAno9', tablacomportamientopagoweb.mesfila  as 'Mesfila' ");
          cSQL.Append(" From tablacomportamientopagoweb JOIN cliente  ON (tablacomportamientopagoweb.nkey_cliente = cliente.nkey_cliente) JOIN deudor  On (deudor.nkey_deudor = tablacomportamientopagoweb.nkey_deudor) ");
          cSQL.Append(" where tablacomportamientopagoweb.nkey_deudor = ").Append(lngCodDeudor).Append(" and tablacomportamientopagoweb.nkey_cliente= ").Append(lngCodNkey).Append(" group by Deudor.snombre , tablacomportamientopagoweb.periodo, tablacomportamientopagoweb.orden, ");
          cSQL.Append(" tablacomportamientopagoweb.fechafactact, tablacomportamientopagoweb.mesano, tablacomportamientopagoweb.mesano1, tablacomportamientopagoweb.mesano2, tablacomportamientopagoweb.mesano3, tablacomportamientopagoweb.mesano4, tablacomportamientopagoweb.mesano5, tablacomportamientopagoweb.mesano6, ");
          cSQL.Append(" tablacomportamientopagoweb.mesano7, tablacomportamientopagoweb.mesano8, tablacomportamientopagoweb.mesano9, tablacomportamientopagoweb.mesfila order by tablacomportamientopagoweb.orden ");
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
