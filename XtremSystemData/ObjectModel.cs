using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OnlineServices.Conn;
using OnlineServices.SystemData;

namespace OnlineServices.SystemData
{
  public class ObjectModel
  {
    private string pError = string.Empty;
    public string Error { get { return pError; } set { pError = value; } }

    private DBConn oConn;

    public ObjectModel(ref DBConn oConn) {
      this.oConn = oConn;
    }

    public string getCodeKey(string pNomTable)
    {
      string CodKey = string.Empty;
      if (oConn.bIsOpen)
      {
        try { 
          SysTblCode oTblCode = new SysTblCode(ref oConn);
          oTblCode.Tabla = pNomTable;
          oTblCode.Accion = "CREAR";
          oTblCode.Put();
          CodKey = oTblCode.Code;

        }catch(Exception Ex){
          pError = Ex.Message;
        }
      }
      return CodKey;
    }

  }
}
