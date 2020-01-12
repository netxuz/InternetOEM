using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Web.Services;

using OnlineServices.Conn;
using OnlineServices.Method;
using OnlineServices.Reporting;
using OnlineServices.Dashboard;


namespace ICommunity.Dashboard
{
  public partial class _default : System.Web.UI.Page
  {
    private OnlineServices.Method.Web oWeb = new OnlineServices.Method.Web();
    private OnlineServices.Method.Usuario oIsUsuario;

    protected void Page_Load(object sender, EventArgs e)
    {
      Response.Cache.SetCacheability(HttpCacheability.NoCache);
      Response.Cache.SetExpires(DateTime.Now);
      Response.Cache.SetNoServerCaching();
      Response.Cache.SetNoStore();

      DBConn oConn = new DBConn();
      oIsUsuario = oWeb.ValidaUserAppReport();

      if (!IsPostBack)
      {
        bool bFromMenu = (!string.IsNullOrEmpty(oWeb.GetData("frommenu")) ? true : false);
        //idUser.Text = "¡Hola " + oIsUsuario.Nombres + "!";
        idUser.Text = "CUSTOMER PROFILE ";

        if (oConn.Open())
        {
          if (bFromMenu)
          {
            hdd_cliente.Value = oWeb.GetData("nkey_cliente");
            hdd_holding.Value = oWeb.GetData("ncodholding");

            cDashboard oDeudor = new cDashboard(ref oConn);
            if (!string.IsNullOrEmpty(hdd_cliente.Value))
              oDeudor.nKeyCliente = hdd_cliente.Value;
            if (!string.IsNullOrEmpty(hdd_holding.Value))
              oDeudor.CodHolding = hdd_holding.Value;

            if (!string.IsNullOrEmpty(oWeb.GetData("nkey_deudor")))
              oDeudor.nKeyDeudor = oWeb.GetData("nkey_deudor");

            DataTable dt = oDeudor.GetDeudores();
            if (dt != null)
            {
              if (dt.Rows.Count > 0)
              {
                hdd_ncodigodeudor.Value = dt.Rows[0]["ncodigodeudor"].ToString() + " " + dt.Rows[0]["deudor"].ToString().Replace("'", "");
              }
            }
            dt = null;

            hdd_load_msj.Value = "1";
          }
          else
          {
            cDashboardKeys oDashboardKeys = new cDashboardKeys(ref oConn);
            oDashboardKeys.CodUser = oIsUsuario.CodUsuario;
            DataTable dt = oDashboardKeys.Get();
            if (dt != null)
            {
              if (dt.Rows.Count > 0)
              {
                hdd_cliente.Value = dt.Rows[0]["nkey_cliente"].ToString();
                hdd_holding.Value = dt.Rows[0]["ncodholding"].ToString();
                hdd_ncodigodeudor.Value = dt.Rows[0]["ncodigodeudor"].ToString();

                oIsUsuario.NKeyUsuario = dt.Rows[0]["nkey_cliente"].ToString();
                oIsUsuario.NCodHolding = dt.Rows[0]["ncodholding"].ToString();
                oIsUsuario.NCodigodeudor = dt.Rows[0]["ncodigodeudor"].ToString();
                oIsUsuario.NKeyDeudor = dt.Rows[0]["nkey_deudor"].ToString();

                hdd_load_msj.Value = "1";
              }
            }
            dt = null;
          }
        }
        oConn.Close();
      }

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
        if (!string.IsNullOrEmpty(hdd_delete_widget.Value))
        {
          cWidgets oWidgets = new cWidgets(ref oConn);
          oWidgets.CodUser = oIsUsuario.CodUsuario;
          oWidgets.CodWidget = hdd_delete_widget.Value;
          oWidgets.Accion = "ELIMINAR";
          oWidgets.Put();

          if (!string.IsNullOrEmpty(oWidgets.Error))
          {
            Response.Write("Error de eliminación : " + oWidgets.Error);
            Response.End();
          }

          hdd_delete_widget.Value = string.Empty;
        }

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
          hdd_load_msj.Value = "1";
        }
        else
        {
          if (!indCmbHoldingAct)
            foreach (DataRow oRow in dtHolding.Rows)
            {
              idItemDropdownHolding.Controls.Add(new LiteralControl("<a class=\"dropdown-item item-holding\" href=\"#\"  data-value=\"" + oRow["ncodholding"].ToString() + "\">" + oRow["holding"].ToString().ToUpper() + "</a>"));
            }
        }

        if ((indOneCliente) && (!idCmbHolding.Visible))
        {
          hdd_cliente.Value = dtClientes.Rows[0]["nkey_cliente"].ToString();
          idCmbCliente.Visible = false;
          hdd_load_msj.Value = "1";
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
        dtHolding = null;
      }
      oConn.Close();

      if (string.IsNullOrEmpty(hdd_load_msj.Value))
      {
        hdd_load_msj.Value = "1";
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "openmodal", "$('#modalRelatedContent').modal('show');", true);
      }
      else
      {
        if ((!string.IsNullOrEmpty(hdd_cliente.Value)) || (!string.IsNullOrEmpty(hdd_holding.Value)))
        {
          if (oConn.Open())
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
                  lblClienteHolding.Text = "EMPRESA: " + dt.Rows[0]["sNombre"].ToString().ToUpper();
                }
                if (!string.IsNullOrEmpty(hdd_holding.Value))
                {
                  dropdownholding.InnerText = dt.Rows[0]["holding"].ToString().ToUpper();
                  dropdownempresas.InnerText = "EMPRESAS";
                  lblClienteHolding.Text = "HOLDING: " + dt.Rows[0]["holding"].ToString().ToUpper();
                }
              }
            }
            dt = null;
          }
          oConn.Close();
        }

        if ((!string.IsNullOrEmpty(hdd_ncodigodeudor.Value)) && (!string.IsNullOrEmpty(hdd_cliente.Value)) || (!string.IsNullOrEmpty(hdd_holding.Value)))
        {
          if (oConn.Open())
          {
            cDashboard oDeudor = new cDashboard(ref oConn);
            oDeudor.nKeyCliente = hdd_cliente.Value;
            oDeudor.CodHolding = hdd_holding.Value;
            oDeudor.NomDeudor = hdd_ncodigodeudor.Value;
            //oDeudor.CodigoDeudor = hdd_ncodigodeudor.Value;
            DataTable dt = oDeudor.GetDeudorByCodigo();
            if (dt != null)
            {
              if (dt.Rows.Count > 0)
              {
                cDashboardKeys oDashboardKeys = new cDashboardKeys(ref oConn);
                oDashboardKeys.CodUser = oIsUsuario.CodUsuario;
                oDashboardKeys.Accion = "ELIMINAR";
                oDashboardKeys.Put();

                if (!string.IsNullOrEmpty(hdd_cliente.Value))
                  oDashboardKeys.KeyCliente = hdd_cliente.Value;

                if (!string.IsNullOrEmpty(hdd_holding.Value))
                  oDashboardKeys.CodHolding = hdd_holding.Value;

                oDashboardKeys.CodigoDeudor = hdd_ncodigodeudor.Value;
                oDashboardKeys.KeyDeudor = dt.Rows[0]["nkey_deudor"].ToString();
                oDashboardKeys.Accion = "CREAR";
                oDashboardKeys.Put();

                oIsUsuario.NKeyUsuario = hdd_cliente.Value;
                oIsUsuario.NCodHolding = hdd_holding.Value;
                oIsUsuario.NCodigodeudor = hdd_ncodigodeudor.Value;
                oIsUsuario.NKeyDeudor = dt.Rows[0]["nkey_deudor"].ToString();

                oIsUsuario.Moneda = oDeudor.getSimboloMoneda();

                //lblDeudor.Text = "Estás en el CUSTOMER PROFILE " + dt.Rows[0]["deudor"].ToString() + " Cod. " + dt.Rows[0]["ncodigodeudor"].ToString();
                lblDeudor.Text = (dt.Rows[0]["deudor"].ToString() + " Cod. " + dt.Rows[0]["ncodigodeudor"].ToString()).ToUpper();

                Log oLog = new Log();
                oLog.IdUsuario = oIsUsuario.CodUsuario;
                oLog.ObsLog = "CUSTOMER PROFILE " + dt.Rows[0]["deudor"].ToString() + " Cod. " + dt.Rows[0]["ncodigodeudor"].ToString();
                oLog.CodEvtLog = "100";
                oLog.AppLog = "CUSTOMER PROFILE";
                if (!string.IsNullOrEmpty(hdd_cliente.Value))
                  oLog.NkeyCliente = hdd_cliente.Value;
                if (!string.IsNullOrEmpty(hdd_holding.Value))
                  oLog.NcodHolding = hdd_holding.Value;
                oLog.NkeyDeudor = dt.Rows[0]["nkey_deudor"].ToString();
                oLog.putLog();

              }
            }
            dt = null;
          }
          oConn.Close();
        }
        else
        {
          hdd_ncodigodeudor.Value = string.Empty;
          lblDeudor.Text = string.Empty; ;
        }
      }

      getWidgetAvailable();

      DataTable dtWidget = null;
      if (oConn.Open())
      {
        cWidgets oWidgets = new cWidgets(ref oConn);
        oWidgets.CodUser = oIsUsuario.CodUsuario;
        dtWidget = oWidgets.getWidgetsByUser();
      }
      oConn.Close();

      DrawDashboard(dtWidget);
    }

    //Draw dashboard
    public void DrawDashboard(DataTable dtWidget)
    {
      int x = 1;
      for (int i = 0; i < 3; i++)
      {
        idContainer.Controls.Add(new LiteralControl("<div class=\"row\">"));
        for (int j = 0; j < 3; j++)
        {
          idContainer.Controls.Add(new LiteralControl("<div class=\"col-md col-marg-bottom\">"));
          if (dtWidget.Rows.Count > 0)
          {
            DataRow[] oRow = dtWidget.Select("orden_widget = " + x.ToString());
            if (oRow.Length > 0)
            {
              string _pathUsrControl = @"Controls\" + oRow[0][3].ToString();
              Control _usrControl = (Control)Page.LoadControl(_pathUsrControl);
              idContainer.Controls.Add(_usrControl);
            }
            else
            {
              StringBuilder sFicha = new StringBuilder();
              sFicha.Append(geFicha().ToString());
              sFicha.Replace("[X]", x.ToString());
              idContainer.Controls.Add(new LiteralControl(sFicha.ToString()));
            }
          }
          else
          {
            StringBuilder sFicha = new StringBuilder();
            sFicha.Append(geFicha().ToString());
            sFicha.Replace("[X]", x.ToString());
            idContainer.Controls.Add(new LiteralControl(sFicha.ToString()));
          }
          idContainer.Controls.Add(new LiteralControl("</div>"));
          x++;
        }
        idContainer.Controls.Add(new LiteralControl("</div>"));
        idContainer.Controls.Add(new LiteralControl("<div class=\"row row-line-height\"><div class=\"col-md-12\"><br /></div></div>"));
      }
    }

    public StringBuilder geFicha()
    {
      StringBuilder sHtmlFicha = new StringBuilder();
      sHtmlFicha.Append("<div class=\"card\">");
      sHtmlFicha.Append("<div class=\"cb-ficha\">");
      sHtmlFicha.Append("<a class=\"waves-effect waves-light mr-4 widget\" data-value=\"btn_[X]\"><i class=\"fas fa-plus logohome\"></i></a>");
      sHtmlFicha.Append("<h7 class=\"card-title tt-ficha text-left\">Agrega Widget</h7>");
      sHtmlFicha.Append("</div>");
      sHtmlFicha.Append("</div>");

      return sHtmlFicha;
    }

    //Modal
    public void getWidgetAvailable()
    {
      DBConn oConn = new DBConn();

      if (oConn.Open())
      {
        cWidgets oWidgets = new cWidgets(ref oConn);
        oWidgets.CodUser = oIsUsuario.CodUsuario;
        gdWidgets.DataSource = oWidgets.getWidgetsAvailable();
        gdWidgets.DataBind();
      }
      oConn.Close();
    }

    protected void gdWidgets_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
      gdWidgets.PageIndex = e.NewPageIndex;
      getWidgetAvailable();
    }

    protected void gdWidgets_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      if (e.CommandName == "Seleted")
      {
        LinkButton btnSelected = (LinkButton)e.CommandSource;    // the button
        GridViewRow myRow = (GridViewRow)btnSelected.Parent.Parent;  // the row
        //GridView myGrid = (GridView)sender; // the gridview
        string lngCodWidget = gdWidgets.DataKeys[myRow.RowIndex].Values["cod_widget"].ToString(); // value of the datakey

        DBConn oConn = new DBConn();
        if (oConn.Open())
        {
          cWidgets oWidgets = new cWidgets(ref oConn);
          oWidgets.CodUser = oIsUsuario.CodUsuario;
          oWidgets.CodWidget = lngCodWidget;
          oWidgets.Order = hdd_pos_widget.Value;
          oWidgets.Accion = "CREAR";
          oWidgets.Put();

          if (!string.IsNullOrEmpty(oWidgets.Error))
          {
            Response.Write(oWidgets.Error);
          }
        }
        oConn.Close();

        hdd_delete_widget.Value = string.Empty;

        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "hide", "document.forms[0].submit();", true);
        getWidgetAvailable();
        //PanelWidgetAvailable.Update();
      }
    }

    protected void PanelWidget_Load(object sender, EventArgs e)
    {
      DataTable dtWidget = null;
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        cWidgets oWidgets = new cWidgets(ref oConn);
        oWidgets.CodUser = oIsUsuario.CodUsuario;
        dtWidget = oWidgets.getWidgetsByUser();
      }
      oConn.Close();

      DrawDashboard(dtWidget);
    }

    protected void bnt_logout_Click(object sender, EventArgs e)
    {
      Session["USUARIO"] = string.Empty;
      Session["CodUsuarioPerfil"] = string.Empty;
      Response.Redirect("/");
    }
  }

  public class cDeudor
  {
    public string sNombre;
  }

  public class cHolding
  {
    public string nCodHolding;
    public string sNombreHolding;
  }

}