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
using OnlineServices.Dashboard;
using OnlineServices.Antalis;
using OnlineServices.SystemData;

namespace ICommunity.Dashboard
{
  public partial class clientes_mas_litigios : System.Web.UI.Page
  {
    private OnlineServices.Method.Web oWeb = new OnlineServices.Method.Web();
    private OnlineServices.Method.Usuario oIsUsuario;

    protected void Page_Load(object sender, EventArgs e)
    {
      oIsUsuario = oWeb.ValidaUserAppReport();

      getMenu(zone_dashboard, oIsUsuario.CodUsuario, "0", false, true);
      getMenu(idReportePago, oIsUsuario.CodUsuario, "1", true, false);
      getMenu(idProcesoSeguimiento, oIsUsuario.CodUsuario, "2", true, false);
      getMenu(idCartolas, oIsUsuario.CodUsuario, "3", true, false);
      getMenu(idProcesoNormalizacion, oIsUsuario.CodUsuario, "4", true, false);
      getMenu(idIndicadoresClaves, oIsUsuario.CodUsuario, "5", true, false);
      getMenu(IndClasificacionRiesgo, oIsUsuario.CodUsuario, "6", true, false);

      getMenuAntalis(indAntalis, oIsUsuario.CodUsuario);

      get20CliMasLitigios();

      Log oLog = new Log();
      oLog.IdUsuario = oIsUsuario.CodUsuario;
      oLog.ObsLog = "TOP 20 CLIENTES CON MÁS LITIGIOS";
      oLog.CodEvtLog = "105";
      oLog.AppLog = "TOP 20 CLIENTES CON MÁS LITIGIOS";
      oLog.putLog();
    }

    protected void getMenu(System.Web.UI.HtmlControls.HtmlGenericControl oHtmControl, string pCodUser, string oOrdConsulta, bool iReport, bool wIcons)
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
              string sControls = "<a href=\"" + ((iReport) ? "../reporting/" : string.Empty) + oRow["url_consulta_new"].ToString() + "\">";
              if (wIcons)
                sControls = sControls + "<i class='fas fa-th-list'></i> ";
              sControls = sControls + oRow["nom_consulta"].ToString() + "</a>";

              oHtmControl.Controls.Add(new LiteralControl(sControls));
            }
          }
        }
        dtQuery = null;
      }
      oConn.Close();
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

    protected void get20CliMasLitigios()
    {
      string[] sCss = { "blq-rojo", "blq-naranjo", "blq-amarillo", "blq-verde-claro", "blq-verde" };
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        cMainAcces oTopCliMayMntPastDue = new cMainAcces(ref oConn);
        oTopCliMayMntPastDue.CodUsuario = oIsUsuario.CodUsuario;
        DataTable dt = oTopCliMayMntPastDue.getTopCliMasLitigios();
        if (dt != null)
        {
          if (dt.Rows.Count > 0)
          {
            int iCant = 1;
            int index = 0;
            foreach (DataRow oRow in dt.Rows)
            {
              string sHtml = string.Empty;
              sHtml = "<div class='blq-cliente " + sCss[index].ToString() + "'>";
              sHtml = sHtml + "<a href='default.aspx?frommenu=1&nkeycliente=" + oRow["nkey_cliente"].ToString() + "&nkey_deudor=" + oRow["nkey_deudor"].ToString() + "'>";
              sHtml = sHtml + "<div class='blq-categoria'><span class='txt-categoria'>" + ((iCant.ToString().Length == 1) ? "0" : string.Empty) + iCant.ToString() + "</span></div>";
              sHtml = sHtml + "<div class='blq-data-client'>";
              sHtml = sHtml + "<div class='blq-tt-cliente'><span class='tt-cliente'>" + oRow["sNombre"].ToString() + "</span></div>";
              sHtml = sHtml + "<div class='blq-dt-pastdue'><span class='dt-pastdue'>$ " + Math.Truncate(double.Parse(oRow["monto_litigio"].ToString())).ToString("N0") + "</span></div>";
              sHtml = sHtml + "</div>";
              sHtml = sHtml + "</a>";
              sHtml = sHtml + "</div>";
              ClientesDeCuidado.Controls.Add(new LiteralControl(sHtml.ToString()));
              iCant++;
              if (index == 4)
                index = 0;
              else
                index++;
            }
          }
        }
        oConn.Close();
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