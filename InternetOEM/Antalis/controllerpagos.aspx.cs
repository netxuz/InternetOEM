﻿using System;
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
  public partial class controllerpagos : System.Web.UI.Page
  {
    private OnlineServices.Method.Web oWeb = new OnlineServices.Method.Web();
    private OnlineServices.Method.Usuario oIsUsuario;

    protected void Page_Load(object sender, EventArgs e)
    {
      oIsUsuario = oWeb.ValidaUserAppReport();

      //getMenu(idReportePago, oIsUsuario.CodUsuario, "1");
      //getMenu(idProcesoSeguimiento, oIsUsuario.CodUsuario, "2");
      //getMenu(idCartolas, oIsUsuario.CodUsuario, "3");
      //getMenu(idProcesoNormalizacion, oIsUsuario.CodUsuario, "4");
      //getMenu(idIndicadoresClaves, oIsUsuario.CodUsuario, "5");
      //getMenu(IndClasificacionRiesgo, oIsUsuario.CodUsuario, "6");

      //getMenuAntalis(indAntalis, oIsUsuario.CodUsuario);

      if (!IsPostBack)
      {
        Log oLog = new Log();
        oLog.IdUsuario = oIsUsuario.CodUsuario;
        oLog.ObsLog = "REPORTE DE VALIJAS A VALIDAR";
        oLog.CodEvtLog = "2";
        oLog.AppLog = "ANTALIS";
        oLog.putLog();

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

          cAntUsrTiposPago oAntUsrTiposPago = new cAntUsrTiposPago(ref oConn);
          oAntUsrTiposPago.CodUsuario = oIsUsuario.CodUsuario;
          DataTable dtUsrTiposPago = oAntUsrTiposPago.Get();
          if (dtUsrTiposPago != null) {
            if (dtUsrTiposPago.Rows.Count > 0) {
              foreach (DataRow oRow in dtUsrTiposPago.Rows) {
                if (oRow["tipo_pago"].ToString() == "1")
                  cmb_documento.Items.Add(new ListItem("Cheque al día", "1"));
                if (oRow["tipo_pago"].ToString() == "2")
                  cmb_documento.Items.Add(new ListItem("Cheque al fecha", "2"));
                if (oRow["tipo_pago"].ToString() == "3")
                  cmb_documento.Items.Add(new ListItem("Efectivo", "3"));
                if (oRow["tipo_pago"].ToString() == "4")
                  cmb_documento.Items.Add(new ListItem("Letra", "4"));
                if (oRow["tipo_pago"].ToString() == "5")
                  cmb_documento.Items.Add(new ListItem("Tarjeta", "5"));
                if (oRow["tipo_pago"].ToString() == "6")
                  cmb_documento.Items.Add(new ListItem("Transferencia", "6"));
              }
            }
          }
          dtUsrTiposPago = null;

          //cAntsUsuarios oUsuarios = new cAntsUsuarios(ref oConn);
          //oUsuarios.CodUsuario = oIsUsuario.CodUsuario;
          //oUsuarios.CodRol = "2";
          //DataTable dt = oUsuarios.GetRoles();
          //if (dt != null)
          //{
          //  if (dt.Rows.Count > 0)
          //  {
          //    hdd_tipo_controller.Value = dt.Rows[0]["tipo"].ToString();
          //    switch (hdd_tipo_controller.Value)
          //    {
          //      case "E":
          //        cmb_documento.Items.Add(new ListItem("Efectivo", "3"));
          //        cmb_documento.Items.Add(new ListItem("Tarjeta", "5"));
          //        cmb_documento.Items.Add(new ListItem("Transferencia", "6"));
          //        break;

          //      case "C":
          //        cmb_documento.Items.Add(new ListItem("Cheque al día", "1"));
          //        cmb_documento.Items.Add(new ListItem("Cheque al fecha", "2"));
          //        cmb_documento.Items.Add(new ListItem("Letra", "4"));
          //        break;
          //    }
          //  }
          //}
          //dt = null;

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

    protected void onLoadGrid()
    {
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        cAntPagos oPagos = new cAntPagos(ref oConn);
        oPagos.CodUsuario = oIsUsuario.CodUsuario;
        oPagos.Estado = "C";

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

        if ((!string.IsNullOrEmpty(txt_num_documento.Text))&&(!string.IsNullOrEmpty(cmd_tipo_documento.SelectedValue)))
        {
          oPagos.TipoDocumento = cmd_tipo_documento.SelectedValue;
          oPagos.NumDocumento = txt_num_documento.Text;
        }

        gdPagos.DataSource = oPagos.GetPagosValidar();
        gdPagos.DataBind();
        oConn.Close();
      }

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
        string sCodCentroDist = e.Row.Cells[2].Text.ToString();
        string sTipoDocumento = e.Row.Cells[3].Text.ToString();

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
              e.Row.Cells[2].Text = dt.Rows[0]["descripcion"].ToString();
            }
          }
          dt = null;
        }
        oConn.Close();

        switch (sTipoDocumento)
        {
          case "1":
            e.Row.Cells[3].Text = "CHEQUE AL DIA";
            break;
          case "2":
            e.Row.Cells[3].Text = "CHEQUE A FECHA";
            break;
          case "3":
            e.Row.Cells[3].Text = "EFECTIVO";
            break;
          case "4":
            e.Row.Cells[3].Text = "LETRA";
            break;
          case "5":
            e.Row.Cells[3].Text = "TARJETA";
            break;
          case "6":
            e.Row.Cells[3].Text = "TRANSFERENCIA";
            break;
        }
      }
    }

    protected void idBuscar_Click(object sender, EventArgs e)
    {
      onLoadGrid();
    }

    protected void gdPagos_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
      string sCodPago = gdPagos.DataKeys[e.NewSelectedIndex].Values[0].ToString();
      string sCodTipoPago = gdPagos.DataKeys[e.NewSelectedIndex].Values[1].ToString();

      switch (sCodTipoPago)
      {
        case "1":
        case "2":
        case "4":
        case "5":
        case "6":
          Response.Redirect(String.Format("controllerpagochequedia.aspx?CodPago={0}", sCodPago));
          break;
        case "3":
          Response.Redirect(String.Format("controllerpagoefectivo.aspx?CodPago={0}", sCodPago));
          break;
      }
    }

    protected void bnt_logout_Click(object sender, EventArgs e)
    {
      Session["USUARIO"] = string.Empty;
      Session["CodUsuarioPerfil"] = string.Empty;
      Response.Redirect("/");
    }
  }
}