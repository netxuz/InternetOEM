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

    private string pCantDocumentos;
    public string CantDocumentos { get { return pCantDocumentos; } set { pCantDocumentos = value; } }

    private string pImporteTotal;
    public string ImporteTotal { get { return pImporteTotal; } set { pImporteTotal = value; } }

    private string pEstado;
    public string Estado { get { return pEstado; } set { pEstado = value; } }

    private string sRazonSocial;
    public string RazonSocial { get { return sRazonSocial; } set { sRazonSocial = value; } }

    private string sFechaInicial;
    public string FechaInicial { get { return sFechaInicial; } set { sFechaInicial = value; } }

    private string sFechaFinal;
    public string FechaFinal { get { return sFechaFinal; } set { sFechaFinal = value; } }

    private string pImporteTotalRecibido;
    public string ImporteTotalRecibido { get { return pImporteTotalRecibido; } set { pImporteTotalRecibido = value; } }

    private string pDiscrepancia;
    public string Discrepancia { get { return pDiscrepancia; } set { pDiscrepancia = value; } }

    private string pTipoPago;
    public string TipoPago { get { return pTipoPago; } set { pTipoPago = value; } }

    private bool pEstadoNoValidada;
    public bool EstadoNoValidada { get { return pEstadoNoValidada; } set { pEstadoNoValidada = value; } }

    private string pAccion;
    public string Accion { get { return pAccion; } set { pAccion = value; } }

    private string pError;
    public string Error { get { return pError; } set { pError = value; } }

    private DBConn oConn;

    public cAntPagos()
    {

    }

    public cAntPagos(ref DBConn oConn)
    {
      this.oConn = oConn;
    }

    public DataTable Get()
    {
      oParam = new DBConn.SQLParameters(7);
      DataTable dtData;
      StringBuilder cSQL;
      string Condicion = " where ";

      if (oConn.bIsOpen)
      {
        cSQL = new StringBuilder();
        cSQL.Append("select cod_pago,cod_user,nkey_cliente,cod_centrodist,cod_tipo_pago,convert(varchar, fech_recepcion, 103) fech_recepcion,horario,cant_documentos,importe_total,estado ");
        cSQL.Append(" from ant_pagos");

        if (!string.IsNullOrEmpty(pCodPagos))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_pago = @cod_pago  ");
          oParam.AddParameters("@cod_pago", pCodPagos, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(pCodCentroDist))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_centrodist = @cod_centrodist  ");
          oParam.AddParameters("@cod_centrodist", pCodCentroDist, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(pCodTipoPago))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" cod_tipo_pago = @cod_tipo_pago  ");
          oParam.AddParameters("@cod_tipo_pago", pCodTipoPago, TypeSQL.Numeric);
        }

        if (!string.IsNullOrEmpty(sRazonSocial))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" nkey_cliente in(select nkey_cliente from cliente where snombre like ' + @snombre + ')  ");
          oParam.AddParameters("@snombre", sRazonSocial, TypeSQL.Varchar);
        }

        if ((!string.IsNullOrEmpty(sFechaInicial)) && (!string.IsNullOrEmpty(sFechaFinal)))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" fech_recepcion between @fechainicial and @fechafinal ");
          oParam.AddParameters("@fechainicial", sFechaInicial, TypeSQL.Varchar);
          oParam.AddParameters("@fechafinal", sFechaFinal, TypeSQL.Varchar);
        }

        if (!string.IsNullOrEmpty(pEstado))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" estado = @estado  ");
          oParam.AddParameters("@estado", pEstado, TypeSQL.Char);
        }

        if (pEstadoNoValidada)
        {
          cSQL.Append(Condicion);
          Condicion = " and ";
          cSQL.Append(" estado in ('A','C') ");
        }

        if ((!string.IsNullOrEmpty(pTipoPago)) && (string.IsNullOrEmpty(pCodTipoPago)))
        {
          cSQL.Append(Condicion);
          Condicion = " and ";

          switch (pTipoPago)
          {
            case "E":
              cSQL.Append(" cod_tipo_pago in(3, 5, 6)  ");
              break;
            case "C":
              cSQL.Append(" cod_tipo_pago in(1, 2, 4)  ");
              break;
          }
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

    public DataTable GetCantTipoDoc()
    {
      DataTable dtData;
      StringBuilder cSQL;

      if (oConn.bIsOpen)
      {
        cSQL = new StringBuilder();
        cSQL.Append("select distinct(cod_tipo_pago), count(*) cantidad from ant_pagos where estado = 'C' group by cod_tipo_pago ");
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

              //pCodPagos = oConn.getTableCod("ant_pagos", "cod_pago", oConn);
              pCodPagos = getCod();
              cSQL.Append("insert into ant_pagos(cod_pago,cod_user,nkey_cliente,cod_centrodist,cod_tipo_pago,fech_recepcion,horario,estado) values( ");
              cSQL.Append("@cod_pago,@cod_user,@nkey_cliente,@cod_centrodist,@cod_tipo_pago,@fech_recepcion,@horario,@estado) ");
              oParam.AddParameters("@cod_pago", pCodPagos, TypeSQL.Numeric);
              oParam.AddParameters("@cod_user", pCodUsuario, TypeSQL.Numeric);
              oParam.AddParameters("@nkey_cliente", pNKeyCliente, TypeSQL.Numeric);
              oParam.AddParameters("@cod_centrodist", pCodCentroDist, TypeSQL.Numeric);
              oParam.AddParameters("@cod_tipo_pago", pCodTipoPago, TypeSQL.Numeric);
              oParam.AddParameters("@fech_recepcion", pFechRecepcion, TypeSQL.DateTime);
              oParam.AddParameters("@horario", pHorario, TypeSQL.Char);
              oParam.AddParameters("@estado", pEstado, TypeSQL.Char);
              oConn.Insert(cSQL.ToString(), oParam);

              if (!string.IsNullOrEmpty(oConn.Error))
              {
                pError = oConn.Error;
              }

              break;
            case "EDITAR":
              cSQL = new StringBuilder();
              cSQL.Append("update ant_pagos set ");
              if (!string.IsNullOrEmpty(pCantDocumentos))
              {
                cSQL.Append(" cant_documentos = @cant_documentos ");
                oParam.AddParameters("@cant_documentos", pCantDocumentos, TypeSQL.Numeric);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pImporteTotal))
              {
                cSQL.Append(sComa);
                cSQL.Append(" importe_total = @importe_total ");
                oParam.AddParameters("@importe_total", pImporteTotal, TypeSQL.Numeric);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pEstado))
              {
                cSQL.Append(sComa);
                cSQL.Append(" estado = @estado");
                oParam.AddParameters("@estado", pEstado, TypeSQL.Char);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pImporteTotalRecibido))
              {
                cSQL.Append(sComa);
                cSQL.Append(" importe_total_recibido = @importe_total_recibido");
                oParam.AddParameters("@importe_total_recibido", pImporteTotalRecibido, TypeSQL.Numeric);
                sComa = ", ";
              }
              if (!string.IsNullOrEmpty(pDiscrepancia))
              {
                cSQL.Append(sComa);
                cSQL.Append(" discrepancia = @discrepancia");
                oParam.AddParameters("@discrepancia", pDiscrepancia, TypeSQL.Numeric);
                sComa = ", ";
              }
              cSQL.Append(" where cod_pago = @cod_pago ");
              oParam.AddParameters("@cod_pago", pCodPagos, TypeSQL.Numeric);
              oConn.Update(cSQL.ToString(), oParam);

              break;
            case "ELIMINAR":
              cSQL = new StringBuilder();
              cSQL.Append("delete from ant_pagos where cod_pago = @cod_pago");
              oParam.AddParameters("@cod_pago", pCodPagos, TypeSQL.Numeric);
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

    public string getCod()
    {
      StringBuilder sSQL = new StringBuilder("select count(*) from ant_pagos where convert(varchar, fech_recepcion, 112) = convert(varchar, getdate(), 112) ");
      DataTable dtCodigo = oConn.Select(sSQL.ToString());
      return DateTime.Now.ToString("yyMMdd").ToString() + ((dtCodigo.Rows.Count > 0) ? (long.Parse(dtCodigo.Rows[0][0].ToString()) + 1).ToString() : "1");
    }

  }
}
