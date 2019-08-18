using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Text;
using OnlineServices.Conn;
using OnlineServices.Method;
using OnlineServices.Reporting;

using OnlineServices.Antalis;
using OnlineServices.SystemData;

namespace ICommunity.Antalis
{
  public partial class pagos_antalis : System.Web.UI.Page
  {
    private OnlineServices.Method.Web oWeb = new OnlineServices.Method.Web();
    private OnlineServices.Method.Usuario oIsUsuario;

    protected void Page_Load(object sender, EventArgs e)
    {
      oIsUsuario = oWeb.ValidaUserAppReport();

      getMenu(idReportePago, oIsUsuario.CodUsuario, "1");
      getMenu(idProcesoSeguimiento, oIsUsuario.CodUsuario, "2");
      getMenu(idCartolas, oIsUsuario.CodUsuario, "3");
      getMenu(idProcesoNormalizacion, oIsUsuario.CodUsuario, "4");
      getMenu(idIndicadoresClaves, oIsUsuario.CodUsuario, "5");
      getMenu(IndClasificacionRiesgo, oIsUsuario.CodUsuario, "6");

      getMenuAntalis(indAntalis, oIsUsuario.CodUsuario);

      if (!IsPostBack)
      {

        DBConn oConn = new DBConn();
        if (oConn.Open())
        {
          cAntCentrosDistribucion oCentrosDistribucion = new cAntCentrosDistribucion(ref oConn);
          oCentrosDistribucion.CodUsuario = oIsUsuario.CodUsuario;
          DataTable dtCntDst = oCentrosDistribucion.GetCentrosDistByUsuario();
          if (dtCntDst != null)
          {
            if (dtCntDst.Rows.Count > 0)
            {
              if (dtCntDst.Rows.Count > 1)
                cmb_centrodistribucion.Items.Add(new ListItem("<< Seleccione una opcion >>", ""));

              foreach (DataRow oRow in dtCntDst.Rows)
              {
                cmb_centrodistribucion.Items.Add(new ListItem(oRow["descripcion"].ToString(), oRow["cod_centrodist"].ToString()));
              }
            }
            else
            {
              cmb_centrodistribucion.Items.Add(new ListItem("No existen centros de distribución asociados", ""));
              cmb_centrodistribucion.Enabled = false;
            }
          }
          dtCntDst = null;

          Log oLog = new Log();
          oLog.IdUsuario = oIsUsuario.CodUsuario;
          oLog.ObsLog = "REPORTE DE PAGOS ANTALIS ESTADO ABIERTO / CERRADO";
          oLog.CodEvtLog = "2";
          oLog.AppLog = "ANTALIS";
          oLog.putLog();

        }
        oConn.Close();
        onLoadGrid();
      }
    }

    protected void getMenuAntalis(System.Web.UI.HtmlControls.HtmlGenericControl oHtmControl, string pCoduser)
    {

      DBConn oConn = new DBConn();
      if (oConn.Open())
      {

        SyrPerfilesUsuarios oSysPerfilesUsuarios = new SyrPerfilesUsuarios(ref oConn);
        oSysPerfilesUsuarios.CodUsuario = pCoduser;
        oSysPerfilesUsuarios.CodPerfil = "7";
        DataTable dtPerfil = oSysPerfilesUsuarios.Get();
        if (dtPerfil != null)
        {
          if (dtPerfil.Rows.Count > 0)
          {
            cAntsUsuarios oAntsUsuarios = new cAntsUsuarios(ref oConn);
            oAntsUsuarios.CodUsuario = pCoduser;
            DataTable dtAntRoles = oAntsUsuarios.GetRoles();
            if (dtAntRoles != null)
            {
              foreach (DataRow oRow in dtAntRoles.Rows)
              {

                if (oRow["cod_rol"].ToString() == "1")
                  oHtmControl.Controls.Add(new LiteralControl("<li><a href='../antalis/pagos_antalis.aspx'>Ingreso de Pago</a></li>"));
                if (oRow["cod_rol"].ToString() == "2")
                  oHtmControl.Controls.Add(new LiteralControl("<li><a href='../antalis/controllerpagos.aspx'>Validación de Pago</a></li>"));
              }
            }
            dtAntRoles = null;
            oHtmControl.Controls.Add(new LiteralControl("<li><a href='../antalis/reportevalijas.aspx'>Valijas Validadas</a></li>"));
          }
        }
        dtPerfil = null;
      }
      oConn.Close();

    }

    protected void getMenu(System.Web.UI.HtmlControls.HtmlGenericControl oHtmControl, string pCodUser, string oOrdConsulta)
    {
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        cReportes oReportes = new cReportes(ref oConn);
        oReportes.CodUser = pCodUser;
        oReportes.OrdConsulta = oOrdConsulta;
        DataTable dtQuery = oReportes.GetMenu();
        if (dtQuery != null)
        {
          if (dtQuery.Rows.Count > 0)
          {
            foreach (DataRow oRow in dtQuery.Rows)
            {
              oHtmControl.Controls.Add(new LiteralControl("<li><a href=\"../reporting/" + oRow["url_consulta_new"].ToString() + "\">" + oRow["nom_consulta"].ToString() + "</a></li>"));
            }
          }
        }
        dtQuery = null;
      }
      oConn.Close();
    }

    protected void btnIngresarPago_Click(object sender, EventArgs e)
    {
      Response.Redirect("ingreso_pagos.aspx");
    }

    protected void onLoadGrid()
    {
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        cAntPagos oPagos = new cAntPagos(ref oConn);
        oPagos.EstadoNoValidada = true;

        if (!string.IsNullOrEmpty(txt_num_valija.Text))
        {
          oPagos.CodPagos = txt_num_valija.Text;
        }

        if (!string.IsNullOrEmpty(cmb_centrodistribucion.SelectedValue))
        {
          oPagos.CodCentroDist = cmb_centrodistribucion.SelectedValue;
        }

        if (!string.IsNullOrEmpty(cmb_documento.SelectedValue))
        {
          oPagos.CodTipoPago = cmb_documento.SelectedValue;
        }

        if (!string.IsNullOrEmpty(txt_cliente.Text))
        {
          oPagos.RazonSocial = txt_cliente.Text;
        }

        if ((!string.IsNullOrEmpty(hdd_fch_inicio.Value)) && (!string.IsNullOrEmpty(hdd_fch_hasta.Value)))
        {
          oPagos.FechaInicial = DateTime.Parse(hdd_fch_inicio.Value).ToString("yyyyMMdd");
          oPagos.FechaFinal = DateTime.Parse(hdd_fch_hasta.Value).ToString("yyyyMMdd");
        }

        gdPagos.DataSource = oPagos.Get();
        gdPagos.DataBind();
        oConn.Close();
      }

    }

    protected void gdPagos_SelectedIndexChanged(object sender, EventArgs e)
    {
      string pCodPago = gdPagos.SelectedDataKey.Value.ToString();
      Response.Redirect(String.Format("ingreso_pagos.aspx?CodPago={0}", pCodPago));
    }

    protected void gdPagos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
      string pCodPago = gdPagos.DataKeys[e.RowIndex].Value.ToString();

      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        int nImporteFactura;
        int nSaldo;
        int nAplicacionPagoFactura;
        //int nSaldoNotaCredito;
        int nAplicacionNotaCredito;
        DataTable dt;
        cAntFactura oFactura;
        cAntNotaCredito oNotaCredito;
        cAntDocumentosPago oDocumentosPago = new cAntDocumentosPago(ref oConn);
        oDocumentosPago.CodPagos = pCodPago;
        DataTable dtDocPago = oDocumentosPago.Get();
        if (dtDocPago != null)
        {
          foreach (DataRow oRow in dtDocPago.Rows)
          {
            nSaldo = 0;
            nImporteFactura = int.Parse(oRow["importe_factura"].ToString());
            nAplicacionPagoFactura = int.Parse(oRow["aplicacion_pago_factura"].ToString());
            //nSaldoNotaCredito = int.Parse(oRow["saldo_nota_credito"].ToString());
            nAplicacionNotaCredito = int.Parse(oRow["aplicacion_nota_credito"].ToString());

            int nValorFactura = 0;
            oFactura = new cAntFactura(ref oConn);
            oFactura.CodFactura = oRow["cod_factura"].ToString();
            dt = oFactura.Get();
            if (dt != null)
            {
              if (dt.Rows.Count > 0)
              {
                nValorFactura = int.Parse(dt.Rows[0]["valor_factura"].ToString());
              }
            }
            dt = null;

            if (nValorFactura < (nAplicacionPagoFactura + nAplicacionNotaCredito))
            {
              oFactura.SaldoFactura = nValorFactura.ToString();
            }
            else
            {
              if (nValorFactura < (nImporteFactura + nAplicacionPagoFactura + nAplicacionNotaCredito))
              {
                oFactura.SaldoFactura = nValorFactura.ToString();
              }
              else
              {
                oFactura.SaldoFactura = (nImporteFactura + nAplicacionPagoFactura + nAplicacionNotaCredito).ToString();
              }

            }

            oFactura.Accion = "EDITAR";
            oFactura.Put();

            if (!string.IsNullOrEmpty(oFactura.Error))
            {
              Response.Write("[Eliminar / Factura Editar] Se ha encontrado el siguiente error : " + oFactura.Error);
              Response.End();
            }



            //if (!string.IsNullOrEmpty(oRow["cod_nota_credito"].ToString())) {
            oNotaCredito = new cAntNotaCredito(ref oConn);
            oNotaCredito.CodDocumento = oRow["cod_documento"].ToString();
            DataTable dtNC = oNotaCredito.GetDocNotaCredito();
            if (dtNC != null)
            {
              if (dtNC.Rows.Count > 0)
              {
                foreach (DataRow oRowNC in dtNC.Rows)
                {
                  oNotaCredito.CodNotaCredito = oRowNC["cod_nota_credito"].ToString();
                  dt = oNotaCredito.Get();
                  if (dt != null)
                  {
                    if (dt.Rows.Count > 0)
                    {
                      oNotaCredito.SaldoNotaCredito = (int.Parse(dt.Rows[0]["saldo_nota_credito"].ToString()) + int.Parse(oRowNC["aplicacion_nota_credito"].ToString())).ToString();
                      oNotaCredito.Accion = "EDITAR";
                      oNotaCredito.Put();

                      if (!string.IsNullOrEmpty(oNotaCredito.Error))
                      {
                        Response.Write("[Eliminar / Nota Credito Editar] Se ha encontrado el siguiente error : " + oNotaCredito.Error);
                        Response.End();
                      }
                    }
                  }
                  dt = null;
                  oNotaCredito.Accion = "ELIMINAR";
                  oNotaCredito.PutDocNotaCredito();
                  if (!string.IsNullOrEmpty(oNotaCredito.Error))
                  {
                    Response.Write("[Eliminar / REL Nota Credito - DOC] Se ha encontrado el siguiente error : " + oNotaCredito.Error);
                    Response.End();
                  }
                }
              }
            }
            dtNC = null;

            //}

            oDocumentosPago.CodDocumento = oRow["cod_documento"].ToString();
            oDocumentosPago.Accion = "ELIMINAR";
            oDocumentosPago.Put();

            if (!string.IsNullOrEmpty(oDocumentosPago.Error))
            {
              Response.Write("[Eliminar / Documento Pago] Se ha encontrado el siguiente error : " + oDocumentosPago.Error);
              Response.End();
            }

          }
        }
        dtDocPago = null;


        cAntPagos oPagos = new cAntPagos(ref oConn);
        oPagos.CodPagos = pCodPago;
        oPagos.Accion = "ELIMINAR";
        oPagos.Put();

        if (!string.IsNullOrEmpty(oPagos.Error))
        {
          Response.Write("[Eliminar / Pagos] Se ha encontrado el siguiente error : " + oPagos.Error);
          Response.End();
        }

      }
      oConn.Close();

      onLoadGrid();
    }

    protected void gdPagos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
      gdPagos.PageIndex = e.NewPageIndex;
      onLoadGrid();
    }

    protected void gdPagos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (e.Row.RowType == DataControlRowType.DataRow)
      {

        foreach (DataControlFieldCell cell in e.Row.Cells)
        {
          foreach (Control control in cell.Controls)
          {
            LinkButton lnkBtnDelete = control as LinkButton;
            if (lnkBtnDelete != null && lnkBtnDelete.CommandName == "Delete")
              lnkBtnDelete.Attributes.Add("onclick", "javascript:return confirm('Esta seguro de eliminar este pago?');");
          }
        }

        string sCodCentroDist = e.Row.Cells[3].Text.ToString();
        string sTipoDocumento = e.Row.Cells[4].Text.ToString();
        string sEstado = e.Row.Cells[6].Text.ToString();

        DBConn oConn = new DBConn();
        if (oConn.Open())
        {
          cAntCentrosDistribucion oCentrosDistribucion = new cAntCentrosDistribucion(ref oConn);
          oCentrosDistribucion.CodCentroDist = sCodCentroDist;
          DataTable dt = oCentrosDistribucion.GetByCod();
          if (dt != null)
          {
            if (dt.Rows.Count > 0)
            {
              e.Row.Cells[3].Text = dt.Rows[0]["descripcion"].ToString();
            }
          }
          dt = null;
        }
        oConn.Close();

        switch (sTipoDocumento)
        {
          case "1":
            e.Row.Cells[4].Text = "CHEQUE AL DIA";
            break;
          case "2":
            e.Row.Cells[4].Text = "CHEQUE A FECHA";
            break;
          case "3":
            e.Row.Cells[4].Text = "EFECTIVO";
            break;
          case "4":
            e.Row.Cells[4].Text = "LETRA";
            break;
          case "5":
            e.Row.Cells[4].Text = "TARJETA";
            break;
          case "6":
            e.Row.Cells[4].Text = "TRANSFERENCIA";
            break;
        }

        if (sEstado == "A")
          e.Row.Cells[6].Text = "ABIERTO";
        else
          e.Row.Cells[6].Text = "CERRADO";

      }
    }

    protected void idBuscar_Click(object sender, EventArgs e)
    {
      onLoadGrid();
    }
  }
}