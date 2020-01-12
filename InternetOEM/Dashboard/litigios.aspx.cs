using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Web.Services;
using System.Globalization;

using OnlineServices.Conn;
using OnlineServices.Method;
using OnlineServices.Reporting;
using OnlineServices.Dashboard;
using ClosedXML.Excel;

using OnlineServices.SystemData;

namespace ICommunity.Dashboard
{
  public partial class litigios : System.Web.UI.Page
  {
    private OnlineServices.Method.Web oWeb = new OnlineServices.Method.Web();
    private OnlineServices.Method.Usuario oIsUsuario;

    protected void Page_Load(object sender, EventArgs e)
    {
      toExcel.Visible = false;
      Response.Cache.SetCacheability(HttpCacheability.NoCache);
      Response.Cache.SetExpires(DateTime.Now);
      Response.Cache.SetNoServerCaching();
      Response.Cache.SetNoStore();

      DBConn oConn = new DBConn();
      oIsUsuario = oWeb.ValidaUserAppReport();

      //idUser.Text = "¡Hola " + oIsUsuario.Nombres + "!";

      if (!string.IsNullOrEmpty(hdd_loadnewcondition.Value))
      {
        oIsUsuario.NKeyUsuario = string.Empty;
        oIsUsuario.NCodHolding = string.Empty;
        oIsUsuario.NKeyDeudor = string.Empty;
        oIsUsuario.NCodigodeudor = string.Empty;
        hdd_loadnewcondition.Value = "";
      }

      if (oConn.Open())
      {
        bool indOneHolding = false;
        bool indCmbHoldingAct = false;
        cDashboard oHC = new cDashboard(ref oConn);
        oHC.CodUsuario = oIsUsuario.CodUsuario;
        DataTable dtHolding = oHC.GetHolding();
        if (dtHolding != null)
        {
          if (dtHolding.Rows.Count > 0)
          {
            if (dtHolding.Rows.Count > 1)
            {
              indCmbHoldingAct = true;
              foreach (DataRow oRow in dtHolding.Rows)
              {
                idItemDropdownHolding.Controls.Add(new LiteralControl("<a class=\"dropdown-item item-holding\" href=\"#\"  data-value=\"" + oRow["ncodholding"].ToString() + "\">" + oRow["holding"].ToString().ToUpper() + "</a>"));
              }
            }
            else
            {
              indOneHolding = true;
            }
          }
          else
          {
            idCmbHolding.Visible = false;
          }
        }

        bool indOneCliente = false;
        bool indCmbClienteAct = false;
        oHC.CodUsuario = oIsUsuario.CodUsuario;
        DataTable dtClientes = oHC.GetClientes();
        if (dtClientes != null)
        {
          if (dtClientes.Rows.Count > 0)
          {
            if (dtClientes.Rows.Count > 1)
            {
              indCmbClienteAct = true;
              foreach (DataRow oRow in dtClientes.Rows)
              {
                idItemDropdownCliente.Controls.Add(new LiteralControl("<a class=\"dropdown-item item-cliente\" href=\"#\"  data-value=\"" + oRow["nkey_cliente"].ToString() + "\">" + oRow["sNombre"].ToString().ToUpper() + "</a>"));
              }
            }
            else
            {
              indOneCliente = true;
            }
          }
          else
          {
            idCmbCliente.Visible = false;
          }
        }

        if ((indOneHolding) && (!idCmbCliente.Visible))
        {
          hdd_holding.Value = dtHolding.Rows[0]["ncodholding"].ToString();
          idCmbHolding.Visible = false;
        }
        else
        {
          if (!indCmbHoldingAct)
            foreach (DataRow oRow in dtHolding.Rows)
            {
              idItemDropdownHolding.Controls.Add(new LiteralControl("<a class=\"dropdown-item item-holding\" href=\"#\"  data-value=\"" + oRow["ncodholding"].ToString() + "\">" + oRow["holding"].ToString().ToUpper() + "</a>"));
            }
        }

        if (indOneCliente)
        {
          hdd_cliente.Value = dtClientes.Rows[0]["nkey_cliente"].ToString();
          idCmbCliente.Visible = false;
        }
        else
        {
          if (!indCmbClienteAct)
            foreach (DataRow oRow in dtClientes.Rows)
            {
              idItemDropdownCliente.Controls.Add(new LiteralControl("<a class=\"dropdown-item item-cliente\" href=\"#\"  data-value=\"" + oRow["nkey_cliente"].ToString() + "\">" + oRow["sNombre"].ToString().ToUpper() + "</a>"));
            }
        }
        dtClientes = null;
      }


      if ((!string.IsNullOrEmpty(hdd_cliente.Value)) || (!string.IsNullOrEmpty(hdd_holding.Value)))
      {
        cDashboard oClienteHolding = new cDashboard(ref oConn);
        oClienteHolding.nKeyCliente = hdd_cliente.Value;
        oClienteHolding.CodHolding = hdd_holding.Value;
        DataTable dt = oClienteHolding.GetClienteHolding();
        if (dt != null)
        {
          if (dt.Rows.Count > 0)
          {
            if (!string.IsNullOrEmpty(hdd_cliente.Value))
            {
              dropdownholding.InnerText = "HOLDING";
              dropdownempresas.InnerText = dt.Rows[0]["sNombre"].ToString().ToUpper();
              lblDeudor.Text = "Litigios : " + dt.Rows[0]["sNombre"].ToString().ToUpper();
            }
            else {
              dropdownholding.InnerText = dt.Rows[0]["holding"].ToString().ToUpper();
              dropdownempresas.InnerText = "EMPRESAS";
              lblDeudor.Text = "Litigios : " + dt.Rows[0]["holding"].ToString().ToUpper();
            }

            getResumen();
            getAntiguedad();
            getDetalleCanal();
            getSubMotivo();
            getDetalle();

            Log oLog = new Log();
            oLog.IdUsuario = oIsUsuario.CodUsuario;
            oLog.ObsLog = "LITIGIOS " + dt.Rows[0]["sNombre"].ToString();
            oLog.CodEvtLog = "201";
            oLog.AppLog = "LITIGIOS";
            if (!string.IsNullOrEmpty(hdd_cliente.Value))
              oLog.NkeyCliente = hdd_cliente.Value;
            if (!string.IsNullOrEmpty(hdd_holding.Value))
              oLog.NcodHolding = hdd_holding.Value;
            oLog.putLog();
          }
        }
        dt = null;
      }
      else
      {
        lblDeudor.Text = string.Empty;
        hdd_cliente.Value = string.Empty;
      }
      oConn.Close();
    }

    protected void bnt_logout_Click(object sender, EventArgs e)
    {
      Session["USUARIO"] = string.Empty;
      Session["CodUsuarioPerfil"] = string.Empty;
      Response.Redirect("/");
    }

    public void getResumen()
    {
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        cLitigios oResumen = new cLitigios(ref oConn);
        oResumen.nKeyCliente = hdd_cliente.Value;
        oResumen.CodHolding = hdd_holding.Value;
        //oResumen.nKeyDeudor = oIsUsuario.NKeyDeudor;
        DataTable dt = oResumen.getResumen();
        if (dt != null)
        {
          if (dt.Rows.Count > 0)
          {
            lb_monto_litigios.Text = "$" + double.Parse(dt.Rows[0]["Total_litigios_monto"].ToString()).ToString("N0");
            //lbl_dias_proc_normal.Text = dt.Rows[0]["dias_normalizacion"].ToString() + " días";
          }
        }
        dt = null;


        lbl_dias_proc_normal.Text = oResumen.getDiasProcesoNormalizacion() + " días";
      }
      oConn.Close();
    }

    public void getAntiguedad()
    {
      double iMonto30 = 0;
      double iDocumento30 = 0;

      double iMonto60 = 0;
      double iDocumento60 = 0;

      double iMonto90 = 0;
      double iDocumento90 = 0;

      double iMontoMayor90 = 0;
      double iDocumentoMayor90 = 0;

      DataTable dt;

      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        cLitigios oAntiguedad = new cLitigios(ref oConn);
        oAntiguedad.nKeyCliente = hdd_cliente.Value;
        oAntiguedad.CodHolding = hdd_holding.Value;
        //oAntiguedad.nKeyDeudor = oIsUsuario.NKeyDeudor;
        oAntiguedad.Periodo = "30";
        dt = oAntiguedad.getAntiguedad();
        if (dt != null)
        {
          if (dt.Rows.Count > 0)
          {
            iMonto30 = double.Parse(dt.Rows[0]["saldo"].ToString());
            iDocumento30 = double.Parse(dt.Rows[0]["cantidad"].ToString());
          }
        }
        dt = null;

        oAntiguedad.Periodo = "60";
        dt = oAntiguedad.getAntiguedad();
        if (dt != null)
        {
          if (dt.Rows.Count > 0)
          {
            iMonto60 = double.Parse(dt.Rows[0]["saldo"].ToString());
            iDocumento60 = double.Parse(dt.Rows[0]["cantidad"].ToString());
          }
        }
        dt = null;

        oAntiguedad.Periodo = "90";
        dt = oAntiguedad.getAntiguedad();
        if (dt != null)
        {
          if (dt.Rows.Count > 0)
          {
            iMonto90 = double.Parse(dt.Rows[0]["saldo"].ToString());
            iDocumento90 = double.Parse(dt.Rows[0]["cantidad"].ToString());
          }
        }
        dt = null;

        oAntiguedad.Periodo = "mayor";
        dt = oAntiguedad.getAntiguedad();
        if (dt != null)
        {
          if (dt.Rows.Count > 0)
          {
            iMontoMayor90 = double.Parse(dt.Rows[0]["saldo"].ToString());
            iDocumentoMayor90 = double.Parse(dt.Rows[0]["cantidad"].ToString());
          }
        }
        dt = null;

        double iTotal = iMonto30 + iMonto60 + iMonto90 + iMontoMayor90;
        lb_porcentaje_30.Text = ((iMonto30 / iTotal) * 100).ToString("N0") + "%";
        lb_monto_30.Text = "$" + iMonto30.ToString("N0");
        lb_documento_30.Text = iDocumento30.ToString("N0") + " Facturas";

        lb_porcentaje_60.Text = ((iMonto60 / iTotal) * 100).ToString("N0") + "%";
        lb_monto_60.Text = "$" + iMonto60.ToString("N0");
        lb_documento_60.Text = iDocumento60.ToString("N0") + " Facturas";

        lb_porcentaje_90.Text = ((iMonto90 / iTotal) * 100).ToString("N0") + "%";
        lb_monto_90.Text = "$" + iMonto90.ToString("N0");
        lb_documento_90.Text = iDocumento90.ToString("N0") + " Facturas";

        lbl_porcentaje_mayor90.Text = ((iMontoMayor90 / iTotal) * 100).ToString("N0") + "%";
        lb_monto_mayor90.Text = "$" + iMontoMayor90.ToString("N0");
        lb_documento_mayor90.Text = iDocumentoMayor90.ToString("N0") + " Facturas";

        StringBuilder sHtml_Graph = new StringBuilder();
        sHtml_Graph.Append(" google.charts.setOnLoadCallback(drawPieChart); ");
        sHtml_Graph.Append(" function drawPieChart() { ");
        sHtml_Graph.Append(" var data = google.visualization.arrayToDataTable([ ");
        sHtml_Graph.Append(" ['Días','Monto'], ");
        sHtml_Graph.Append(" ['30 días'," + iMonto30.ToString("0.00", CultureInfo.InvariantCulture) + "], ");
        sHtml_Graph.Append(" ['60 días'," + iMonto60.ToString("0.00", CultureInfo.InvariantCulture) + "], ");
        sHtml_Graph.Append(" ['90 días'," + iMonto90.ToString("0.00", CultureInfo.InvariantCulture) + "], ");
        sHtml_Graph.Append(" ['> 90 días'," + iMontoMayor90.ToString("0.00", CultureInfo.InvariantCulture) + "] ");
        sHtml_Graph.Append(" ]); ");
        sHtml_Graph.Append(" var options = { ");
        sHtml_Graph.Append(" pieHole: 0.9, ");
        sHtml_Graph.Append(" pieSliceText:'none', pieSliceTextStyle: 'none', pieSliceBorderColor: 'none', ");
        sHtml_Graph.Append(" colors: ['#4285F4', '#AA66CC', '#F5A623','#D0021B'], ");
        sHtml_Graph.Append(" legend: 'none', chartArea: { left: 10, right: 10, bottom: 10, top: 10 } ");
        sHtml_Graph.Append(" }; ");
        sHtml_Graph.Append(" var chart = new google.visualization.PieChart(document.getElementById('donut_single')); ");
        sHtml_Graph.Append(" chart.draw(data, options); ");
        sHtml_Graph.Append(" }; ");

        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "donut_single", sHtml_Graph.ToString(), true);

      }
      oConn.Close();
    }

    public void getDetalleCanal()
    {
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        cLitigios oDetalleCanal = new cLitigios(ref oConn);
        oDetalleCanal.nKeyCliente = hdd_cliente.Value;
        oDetalleCanal.CodHolding = hdd_holding.Value;
        //oDetalleCanal.nKeyDeudor = oIsUsuario.NKeyDeudor;
        DataTable dt = oDetalleCanal.getDetalleCanal();
        if (dt != null)
        {
          if (dt.Rows.Count > 0)
          {
            StringBuilder sHtml = new StringBuilder();
            int i = 0;
            foreach (DataRow oRow in dt.Rows)
            {
              if (i == 0)
                sHtml.Append("<div class=\"col-md-6 border-right-litigios scol-line-height-resumen\">");

              if (i == 2)
                sHtml.Append("<div class=\"col-md-6 scol-line-height-resumen\">");

              sHtml.Append("<div class='space-box'><span class='tt-canal'>").Append(oRow["canal"].ToString()).Append("</span></div>");
              sHtml.Append("<div class='space-box'><span class='lb-dt-canal-submotivo-litigios-monto'>").Append("$").Append(double.Parse(oRow["saldo_litigio"].ToString()).ToString("N0")).Append("</span></div>");
              sHtml.Append("<div class='space-box'><span class='lb-dt-canal-submotivo-litigios-cant'>").Append(oRow["cantidad_facturas"].ToString()).Append(" Facturas</span></div>");
              sHtml.Append("<div class='space-divbox'></div>");

              if ((i == 1) || (i == 3))
                sHtml.Append("</div>");

              i++;
            }

            if ((i == 1) || (i == 3))
              sHtml.Append("</div>");

            idDetalle_canal.Controls.Add(new LiteralControl(sHtml.ToString()));
          }
        }
        dt = null;
      }
      oConn.Close();
    }

    public void getSubMotivo()
    {
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        cLitigios oSubMotivo = new cLitigios(ref oConn);
        oSubMotivo.nKeyCliente = hdd_cliente.Value;
        oSubMotivo.CodHolding = hdd_holding.Value;
        //oSubMotivo.nKeyDeudor = oIsUsuario.NKeyDeudor;
        DataTable dt = oSubMotivo.getSubmotivo();
        if (dt != null)
        {
          if (dt.Rows.Count > 0)
          {
            StringBuilder sHtml = new StringBuilder();
            int i = 0;
            foreach (DataRow oRow in dt.Rows)
            {
              if (i == 0)
                sHtml.Append("<div class=\"col-md-6 border-right-litigios scol-line-height-resumen\">");

              if (i == 3)
                sHtml.Append("<div class=\"col-md-6 scol-line-height-resumen\">");

              sHtml.Append("<div class='space-box'><span class='tt-canal'>").Append(oRow["submotivo"].ToString()).Append("</span></div>");
              sHtml.Append("<div class='space-box'><span class='lb-dt-canal-submotivo-litigios-monto'>").Append("$").Append(double.Parse(oRow["saldo"].ToString()).ToString("N0")).Append("</span></div>");
              sHtml.Append("<div class='space-box'><span class='lb-dt-canal-submotivo-litigios-cant'>").Append(oRow["cantidad"].ToString()).Append(" Facturas</span></div>");
              sHtml.Append("<div class='space-divbox'></div>");

              if ((i == 2) || (i == 4))
                sHtml.Append("</div>");

              i++;
            }

            if ((i == 2) || (i == 4))
              sHtml.Append("</div>");

            idSubMotivo.Controls.Add(new LiteralControl(sHtml.ToString()));
          }
        }
        dt = null;
      }
      oConn.Close();
    }

    public void getDetalle()
    {
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        DataTable dt;
        cLitigios oDetalle = new cLitigios(ref oConn);
        oDetalle.nKeyCliente = hdd_cliente.Value;
        oDetalle.CodHolding = hdd_holding.Value;
        if (!string.IsNullOrEmpty(nom_cliente.Text))
          oDetalle.NomCliente = nom_cliente.Text;
        if (!string.IsNullOrEmpty(num_factura.Text))
          oDetalle.NumFactura = num_factura.Text;

        if (!string.IsNullOrEmpty(hdd_periodo.Value))
        {
          oDetalle.Periodo = hdd_periodo.Value;
          oDetalle.OrderBy = true;
        }
        dt = oDetalle.getDetalle();
        gdDetalle.DataSource = dt;
        gdDetalle.DataBind();

        if (dt != null)
        {
          if (dt.Rows.Count > 0)
          {
            toExcel.Visible = true;
          }
        }
        dt = null;

      }
      oConn.Close();
    }

    protected void gdDetalle_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
      gdDetalle.PageIndex = e.NewPageIndex;
      gdDetalle.DataBind();
    }

    protected void lnk_btn_search_Click(object sender, EventArgs e)
    {
      getDetalle();
    }

    protected void lnk_litigios30_Click(object sender, EventArgs e)
    {
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        DataTable dt;
        cLitigios oDetalle = new cLitigios(ref oConn);
        oDetalle.nKeyCliente = hdd_cliente.Value;
        oDetalle.CodHolding = hdd_holding.Value;
        if (!string.IsNullOrEmpty(nom_cliente.Text))
          oDetalle.NomCliente = nom_cliente.Text;
        if (!string.IsNullOrEmpty(num_factura.Text))
          oDetalle.NumFactura = num_factura.Text;
        oDetalle.Periodo = "30";
        oDetalle.OrderBy = true;
        dt = oDetalle.getDetalle();
        gdDetalle.DataSource = dt;
        gdDetalle.DataBind();

        if (dt != null)
        {
          if (dt.Rows.Count > 0)
          {
            toExcel.Visible = true;
          }
        }
        dt = null;

        hdd_periodo.Value = "30";
      }
      oConn.Close();
    }

    protected void lnk_litigios60_Click(object sender, EventArgs e)
    {
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        DataTable dt;
        cLitigios oDetalle = new cLitigios(ref oConn);
        oDetalle.nKeyCliente = hdd_cliente.Value;
        oDetalle.CodHolding = hdd_holding.Value;
        if (!string.IsNullOrEmpty(nom_cliente.Text))
          oDetalle.NomCliente = nom_cliente.Text;
        if (!string.IsNullOrEmpty(num_factura.Text))
          oDetalle.NumFactura = num_factura.Text;
        oDetalle.Periodo = "60";
        oDetalle.OrderBy = true;
        dt = oDetalle.getDetalle();
        gdDetalle.DataSource = dt;
        gdDetalle.DataBind();

        if (dt != null)
        {
          if (dt.Rows.Count > 0)
          {
            toExcel.Visible = true;
          }
        }
        dt = null;

        hdd_periodo.Value = "60";
      }
      oConn.Close();
    }

    protected void lnk_litigios90_Click(object sender, EventArgs e)
    {
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        DataTable dt;
        cLitigios oDetalle = new cLitigios(ref oConn);
        oDetalle.nKeyCliente = hdd_cliente.Value;
        oDetalle.CodHolding = hdd_holding.Value;
        if (!string.IsNullOrEmpty(nom_cliente.Text))
          oDetalle.NomCliente = nom_cliente.Text;
        if (!string.IsNullOrEmpty(num_factura.Text))
          oDetalle.NumFactura = num_factura.Text;
        oDetalle.Periodo = "90";
        oDetalle.OrderBy = true;
        dt = oDetalle.getDetalle();
        gdDetalle.DataSource = dt;
        gdDetalle.DataBind();

        if (dt != null)
        {
          if (dt.Rows.Count > 0)
          {
            toExcel.Visible = true;
          }
        }
        dt = null;

        hdd_periodo.Value = "90";
      }
      oConn.Close();
    }

    protected void lnk_litigiosmayor_Click(object sender, EventArgs e)
    {
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        DataTable dt;
        cLitigios oDetalle = new cLitigios(ref oConn);
        oDetalle.nKeyCliente = hdd_cliente.Value;
        oDetalle.CodHolding = hdd_holding.Value;
        if (!string.IsNullOrEmpty(nom_cliente.Text))
          oDetalle.NomCliente = nom_cliente.Text;
        if (!string.IsNullOrEmpty(num_factura.Text))
          oDetalle.NumFactura = num_factura.Text;
        oDetalle.Periodo = "mayor";
        oDetalle.OrderBy = true;
        dt = oDetalle.getDetalle();
        gdDetalle.DataSource = dt;
        gdDetalle.DataBind();

        if (dt != null)
        {
          if (dt.Rows.Count > 0)
          {
            toExcel.Visible = true;
          }
        }
        dt = null;

        hdd_periodo.Value = "mayor";
      }
      oConn.Close();
    }

    protected void lnk_litigios_Click(object sender, EventArgs e)
    {
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        DataTable dt;
        cLitigios oDetalle = new cLitigios(ref oConn);
        oDetalle.nKeyCliente = hdd_cliente.Value;
        oDetalle.CodHolding = hdd_holding.Value;
        dt = oDetalle.getDetalle();
        gdDetalle.DataSource = dt;
        gdDetalle.DataBind();

        if (dt != null)
        {
          if (dt.Rows.Count > 0)
          {
            toExcel.Visible = true;
          }
        }
        dt = null;

        hdd_periodo.Value = string.Empty;
        num_factura.Text = string.Empty;
        nom_cliente.Text = string.Empty;
      }
      oConn.Close();
    }

    protected void lnk_download_excel_Click(object sender, EventArgs e)
    {

      string sUrl = "dwn_litigios.ashx?hdd_cliente=" + hdd_cliente.Value;
      sUrl = sUrl + "&hdd_holding=" + hdd_holding.Value;
      sUrl = sUrl + "&nom_cliente=" + nom_cliente.Text;
      sUrl = sUrl + "&num_factura=" + num_factura.Text;
      sUrl = sUrl + "&hdd_periodo=" + hdd_periodo.Value;

      Response.Redirect(sUrl);

      ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "hide", "$('#loader').hide();", true);
    }
  }
}