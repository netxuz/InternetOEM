using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using OnlineServices.Conn;

namespace OnlineServices.Antalis
{
  public class cAntPagos
  {
    DBConn.SQLParameters oParam;
    DBConn.DataTypeSQL TypeSQL = new DBConn.DataTypeSQL();

    private string pCodPagos;
    public string CodPagos { get { return pCodPagos; } set { pCodPagos = value; } }

    private string pCodUsuario;
    public string CodUsuario { get { return pCodUsuario; } set { pCodUsuario = value; } }

    private string pNKeyCliente;
    public string NKeyCliente { get { return pNKeyCliente; } set { pNKeyCliente = value; } }

    private string pCodCentroDist;
    public string CodCentroDist { get { return pCodCentroDist; } set { pCodCentroDist = value; } }

    private string pCodTipoPago; //Cod Tipo Documento
    public string CodTipoPago { get { return pCodTipoPago; } set { pCodTipoPago = value; } }

    private string pFechRecepcion;
    public string FechRecepcion { get { return pFechRecepcion; } set { pFechRecepcion = value; } }

    private string pHorario;
    public string Horario { get { return pHorario; } set { pHorario = value; } }

    private string pCodSAP;
    public string CodSAP { get { return pCodSAP; } set { pCodSAP = value; } }

    private string pNombreDeudor;
    public string NombreDeudor { get { return pNombreDeudor; } set { pNombreDeudor = value; } }

    private string pCantDocumentos;
    public string CantDocumentos { get { return pCantDocumentos; } set { pCantDocumentos = value; } }

    private string pImporteTotal;
    public string ImporteTotal { get { return pImporteTotal; } set { pImporteTotal = value; } }

    private string pEstado;
    public string Estado { get { return pEstado; } set { pEstado = value; } }

    private string pAccion;
    public string Accion { get { return pAccion; } set { pAccion = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private DBConn oConn;

    public cAntPagos() {

    }

    public cAntPagos(ref DBConn oConn) {
      this.oConn = oConn;
    }

    public DataTable Get()
    {
      oParam = new DBConn.SQLParameters(3);
      DataTable dtData;
      StringBuilder cSQL;
      string Condicion = " where ";

      if (oConn.bIsOpen)
      {
        cSQL = new StringBuilder();
        cSQL.Append("select cod_pago,cod_user,nkey_cliente,cod_centrodist,cod_tipo_pago,fech_recepcion,horario,cod_sap,nom_deudor,cant_documentos,importe_total,estado ");
        cSQL.Append(" from ant_pagos");

        if (!string.IsNullOrEmpty(pCodPagos))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_pago = @cod_pago  ");
          oParam.AddParameters("@cod_pago", pCodPagos, TypeSQL.Numeric);
        }

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

    public void Put()
    {
      oParam = new DBConn.SQLParameters(10);
      StringBuilder cSQL;
      string sComa = string.Empty;

      if (oConn.bIsOpen)
      {
        try
        {
          switch (pAccion)
          {
            case "CREAR":
              cSQL = new StringBuilder();

              pCodPagos = oConn.getTableCod("ant_pagos", "cod_pago", oConn);
              cSQL.Append("insert into ant_pagos(cod_pago,cod_user,nkey_cliente,cod_centrodist,cod_tipo_pago,fech_recepcion,horario,cod_sap,nom_deudor,estado) values( ");
              cSQL.Append("@cod_pago,@cod_user,@nkey_cliente,@cod_centrodist,@cod_tipo_pago,@fech_recepcion,@horario,@cod_sap,@nom_deudor,@estado) ");
              oParam.AddParameters("@cod_pago", pCodPagos, TypeSQL.Numeric);
              oParam.AddParameters("@cod_user", pCodUsuario, TypeSQL.Numeric);
              oParam.AddParameters("@nkey_cliente", pNKeyCliente, TypeSQL.Numeric);
              oParam.AddParameters("@cod_centrodist", pCodCentroDist, TypeSQL.Numeric);
              oParam.AddParameters("@cod_tipo_pago", pCodTipoPago, TypeSQL.Numeric);
              oParam.AddParameters("@fech_recepcion", pFechRecepcion, TypeSQL.DateTime);
              oParam.AddParameters("@horario", pHorario, TypeSQL.Char);
              oParam.AddParameters("@cod_sap", pCodSAP, TypeSQL.Varchar);
              oParam.AddParameters("@nom_deudor", pNombreDeudor, TypeSQL.Varchar);
              oParam.AddParameters("@estado", pEstado, TypeSQL.Char);
              oConn.Insert(cSQL.ToString(), oParam);

              if (!string.IsNullOrEmpty(oConn.Error)) {
                pError = oConn.Error;
              }

              break;
            case "EDITAR":
              

              break;
            case "ELIMINAR":
              cSQL = new StringBuilder();
              cSQL.Append("delete from ant_pagos where cod_pago = @cod_pago");
              oParam.AddParameters("@cod_codigo", pCodPagos, TypeSQL.Numeric);
              oConn.Delete(cSQL.ToString(), oParam);

              break;
          }
        }
        catch (Exception Ex)
        {
          pError = Ex.Message;
        }
      }
    }

  }
}
