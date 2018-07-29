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
using dcControlPanel.Base;

using OnlineServices.Antalis;
using OnlineServices.SystemData;

namespace ICommunity.Reporting
{
  public partial class _default : System.Web.UI.Page
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

      getMonitores();
    }

    protected void getMenuAntalis(System.Web.UI.HtmlControls.HtmlGenericControl oHtmControl, string pCoduser) {

      DBConn oConn = new DBConn();
      if (oConn.Open()) {

        SyrPerfilesUsuarios oSysPerfilesUsuarios = new SyrPerfilesUsuarios(ref oConn);
        oSysPerfilesUsuarios.CodUsuario = pCoduser;
        oSysPerfilesUsuarios.CodPerfil = "7";
        DataTable dtPerfil = oSysPerfilesUsuarios.Get();
        if (dtPerfil != null) {
          if (dtPerfil.Rows.Count > 0) {
            cAntsUsuarios oAntsUsuarios = new cAntsUsuarios(ref oConn);
            oAntsUsuarios.CodUsuario = pCoduser;
            DataTable dtAntRoles = oAntsUsuarios.GetRoles();
            if (dtAntRoles != null) {
              foreach (DataRow oRow in dtAntRoles.Rows) {

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
              oHtmControl.Controls.Add(new LiteralControl("<li><a href=\"" + oRow["url_consulta_new"].ToString() + "\">" + oRow["nom_consulta"].ToString() + "</a></li>"));
            }
          }
        }
        dtQuery = null;
      }
      oConn.Close();
    }

    protected void getMonitores() {
      StringBuilder sUrlPage;
      StringBuilder sHtml = new StringBuilder();
      dcControlPanel.Conn.DBConn oConn = new dcControlPanel.Conn.DBConn();

      if (oConn.Open()) {
        cAppVistasCliente oAppVistasCliente = new cAppVistasCliente(ref oConn);
        oAppVistasCliente.CodCliente = oIsUsuario.CodUsuario;
        oAppVistasCliente.NotIn = false;
        DataTable DataVista  = oAppVistasCliente.Get();

        foreach (DataRow oRow in DataVista.Rows)
        {
          cAptMonitorPages oAptMonitorPages = new cAptMonitorPages(ref oConn);
          oAptMonitorPages.CodMonitor = oRow["cod_monitor"].ToString();
          DataTable dtmPage = oAptMonitorPages.Get();
          if (dtmPage != null)
          {
            if (dtmPage.Rows.Count > 0)
            {
              string indexToken = string.Empty;
              if (string.IsNullOrEmpty(dtmPage.Rows[0]["cod_cliente"].ToString()))
              {
                indexToken = oRow["cod_monitor"].ToString() + DateTime.Now.ToString("yyyyMMdd");
                Session[indexToken] = 0;
              }
              sUrlPage = new StringBuilder();
              sUrlPage.Append(dtmPage.Rows[0]["url_page"].ToString());
              sUrlPage.Append("?codmonitor=").Append(oRow["cod_monitor"].ToString());
              sUrlPage.Append("&codusuario=").Append(oIsUsuario.CodUsuario);
              sUrlPage.Append("&authuser=").Append(oIsUsuario.CodUsuario);
              sUrlPage.Append("&indexToken=").Append(indexToken);
              sUrlPage.Append("&order=").Append(dtmPage.Rows[0]["order_page"].ToString());

              sHtml.Append("<div class=\"col-md-6 wow fadeInUp\" data-wow-duration=\"4s\" data-wow-delay=\"1s\">");
              sHtml.Append("<iframe src =\"/PaneldeControl/" + sUrlPage.ToString() + "\" width=\"600px\" height=\"300px\" scrolling=\"no\"></iframe>");
              sHtml.Append("</div>");

            }
          }
        }
        idpanel.Controls.Add(new LiteralControl(sHtml.ToString()));
      }
      oConn.Close();
    }
  }
}