using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using OnlineServices.Conn;

namespace OnlineServices.Reporting
{
  public class cCopiaCedibleDebtControl
  {
    private string lngCodNkey;
    public string CodNkey { get { return lngCodNkey; } set { lngCodNkey = value; } }

    private string pDtFchIni;
    public string DtFchIni { get { return pDtFchIni; } set { pDtFchIni = value; } }

    private string pDtFchFin;
    public string DtFchFin { get { return pDtFchFin; } set { pDtFchFin = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private DBConn oConn;

    public cCopiaCedibleDebtControl() {

    }

    public cCopiaCedibleDebtControl(ref DBConn oConn) {
      this.oConn = oConn;
    }

    public DataTable Get()
    {
      DataTable dtData;

      if (oConn.bIsOpen)
      {
        StringBuilder cSQL = new StringBuilder();
        cSQL.Append("exec NoEntregaDocumentoUlt '").Append(lngCodNkey).Append("','").Append(pDtFchIni).Append("', '").Append(pDtFchFin).Append("','S','D','COPIA CEDIBLE EN DEBTCONTROL','1', null");

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
