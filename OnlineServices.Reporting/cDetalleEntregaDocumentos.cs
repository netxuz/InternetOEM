using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using OnlineServices.Conn;

namespace OnlineServices.Reporting
{
  public class cDetalleEntregaDocumentos
  {
    DBConn.SQLParameters oParam;
    DBConn.DataTypeSQL TypeSQL = new DBConn.DataTypeSQL();

    private string lngCodNkey;
    public string CodNkey { get { return lngCodNkey; } set { lngCodNkey = value; } }

    private string pNcodHolding;
    public string NcodHolding { get { return pNcodHolding; } set { pNcodHolding = value; } }

    private string pDtFchIni;
    public string DtFchIni { get { return pDtFchIni; } set { pDtFchIni = value; } }

    private string pDtFchFin;
    public string DtFchFin { get { return pDtFchFin; } set { pDtFchFin = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private DBConn oConn;

    public cDetalleEntregaDocumentos() {
    }

    public cDetalleEntregaDocumentos(ref DBConn oConn) {
      this.oConn = oConn;
    }

    public DataTable Get()
    {
      oParam = new DBConn.SQLParameters(10);
      DataTable dtData;

      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();
        //cSQL.Append("exec DetalleEntregaDoctos  '").Append(pDtFchIni).Append("', '").Append(pDtFchIni).Append("', '").Append(lngCodNkey).Append("', 'rgonzalez'");
        cSQL.Append("select * from VistaDetalleEntregaDoctos where rol = 'rgonzalez' ");

        if (!string.IsNullOrEmpty(lngCodNkey))
        {
          cSQL.Append(" and nkey_cliente in (").Append(lngCodNkey).Append(")");
        }

        if (!string.IsNullOrEmpty(pNcodHolding))
        {
          cSQL.Append(" and ncodholding = ").Append(pNcodHolding);
        }

        //cSQL.Append(" and Recepcion between convert(datetime, '").Append(pDtFchIni).Append("') and convert(datetime, '").Append(pDtFchFin).Append("') ");

        cSQL.Append(" order by ncodholding, snombre, NomDeu, FIngreso, tipo_docto ");

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
