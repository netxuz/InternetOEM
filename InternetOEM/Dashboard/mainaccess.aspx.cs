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
using OnlineServices.SystemData;
using OnlineServices.Antalis;
using OnlineServices.Dashboard;

namespace ICommunity.Dashboard
{
  public partial class mainaccess : System.Web.UI.Page
  {
    private OnlineServices.Method.Web oWeb = new OnlineServices.Method.Web();
    private OnlineServices.Method.Usuario oIsUsuario;

    protected void Page_Load(object sender, EventArgs e)
    {
      oIsUsuario = oWeb.ValidaUserAppReport();

      lbl_lomasvistopor.Text = "Lo más visto por \"" + oIsUsuario.Nombres + "\"";

      getMenu(zone_dashboard, oIsUsuario.CodUsuario, "0", false, true);
      getMenu(idReportePago, oIsUsuario.CodUsuario, "1", true, false);
      getMenu(idProcesoSeguimiento, oIsUsuario.CodUsuario, "2", true, false);
      getMenu(idCartolas, oIsUsuario.CodUsuario, "3", true, false);
      getMenu(idProcesoNormalizacion, oIsUsuario.CodUsuario, "4", true, false);
      getMenu(idIndicadoresClaves, oIsUsuario.CodUsuario, "5", true, false);
      getMenu(IndClasificacionRiesgo, oIsUsuario.CodUsuario, "6", true, false);

      getMenuAntalis(indAntalis, oIsUsuario.CodUsuario);

      getLast20Request();

      get20MostRequest();

      getMasValoradoPorLaGerencia();

      getVisionDeCompania();

      getAnalizandoClientesDiaADia();
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

    protected void getLast20Request()
    {
      string sUrl = string.Empty;
      string cs_div = string.Empty;
      string[] sCss = { "blq-cyan", "blq-teal" };
      Random rand = new Random();
      int index;

      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        cMainAcces oLast20Request = new cMainAcces(ref oConn);
        oLast20Request.CodUsuario = oIsUsuario.CodUsuario;
        DataTable dt = oLast20Request.getLast20Request();

        if (dt != null)
        {
          if (dt.Rows.Count > 0)
          {
            StringBuilder sHtml = new StringBuilder();
            int iCant = 1;
            foreach (DataRow oRow in dt.Rows)
            {
              sUrl = string.Empty;
              //index = rand.Next(sCss.Length);

              //if (oRow["cod_evt_log"].ToString() == "1")
              //{
              //  sUrl = "..";
              //}
              //else if ((oRow["cod_evt_log"].ToString() == "2")|| (oRow["cod_evt_log"].ToString() == "9"))
              //{
              //  sUrl = "..";
              //}

              sUrl = sUrl + oRow["pag_log"].ToString();

              if (oRow["cod_evt_log"].ToString() == "100")
              {
                sUrl = sUrl + "?frommenu=1&nkeycliente=" + oRow["nkey_cliente"].ToString();
                sUrl = sUrl + "&ncodholding=" + oRow["ncodholding"].ToString();
                sUrl = sUrl + "&nkey_deudor=" + oRow["nkey_deudor"].ToString();
              }

              sHtml.Append("<div class='lastrequest ");

              if (iCant < 11)
                index = 0;
              else
                index = 1;

              sHtml.Append(sCss[index].ToString());

              sHtml.Append("'>");

              sHtml.Append("<a href='");
              sHtml.Append(sUrl);
              sHtml.Append("'>");

              sHtml.Append("<div class='tt-blq-fx'>");
              sHtml.Append(oRow["funcionalidad"].ToString().ToUpper());
              sHtml.Append("</div>");

              sHtml.Append("<div class='btm-blq-fch'>");
              sHtml.Append("<div class='tt-fecha'>Última visita:</div>");
              sHtml.Append("<div class='dt-fecha'>" + oRow["fecha"].ToString() + "</div>");
              sHtml.Append("</div>");
              sHtml.Append("<div class='btm-blq-getin'>");
              sHtml.Append("<i class='fas fa-sign-in-alt'></i>");
              sHtml.Append("</div>");

              sHtml.Append("</a>");

              sHtml.Append("</div>");
              iCant++;
            }
            Last20Request.Controls.Add(new LiteralControl(sHtml.ToString()));
          }
        }
        dt = null;
      }
      oConn.Close();
    }

    protected void get20MostRequest()
    {
      string sUrl = string.Empty;
      string[] sCss = { "blq-lime", "blq-unique-color"};
      int index;
      //Random rand = new Random();

      DBConn oConn = new DBConn();

      if (oConn.Open())
      {
        cMainAcces o20MostRequest = new cMainAcces(ref oConn);
        o20MostRequest.CodUsuario = oIsUsuario.CodUsuario;
        DataTable dt = o20MostRequest.get20MostRequest();

        if (dt != null)
        {
          if (dt.Rows.Count > 0)
          {
            StringBuilder sHtml = new StringBuilder();
            int iCant = 1;
            foreach (DataRow oRow in dt.Rows)
            {
              sUrl = string.Empty;
              //if (oRow["cod_evt_log"].ToString() == "1")
              //{
              //  sUrl = "..";
              //}
              //else if ((oRow["cod_evt_log"].ToString() == "2") || (oRow["cod_evt_log"].ToString() == "9"))
              //{
              //  sUrl = "..";
              //}

              sUrl = sUrl + oRow["pag_log"].ToString();

              if (oRow["cod_evt_log"].ToString() == "100")
              {
                sUrl = sUrl + "?frommenu=1&nkeycliente=" + oRow["nkey_cliente"].ToString();
                sUrl = sUrl + "&ncodholding=" + oRow["ncodholding"].ToString();
                sUrl = sUrl + "&nkey_deudor=" + oRow["nkey_deudor"].ToString();
              }

              //index = rand.Next(sCss.Length);
              if (iCant < 11)
                index = 0;
              else
                index = 1;

              sHtml.Append("<div class='mostrequest ");
              sHtml.Append(sCss[index].ToString());
              sHtml.Append("'>");

              sHtml.Append("<a href='");
              sHtml.Append(sUrl);
              sHtml.Append("'>");

              sHtml.Append("<div class='tt-blq-fx'>");
              sHtml.Append(oRow["funcionalidad"].ToString().ToUpper());
              sHtml.Append("</div>");

              sHtml.Append("<div class='btm-blq'>");
              sHtml.Append("<div class='dt-fecha'><i class=\"far fa-eye\"></i> " + oRow["cantidad"].ToString() + "</div>");
              sHtml.Append("</div>");
              sHtml.Append("<div class='btm-blq-getin'>");
              sHtml.Append("<i class='fas fa-sign-in-alt'></i>");
              sHtml.Append("</div>");

              sHtml.Append("</a>");

              sHtml.Append("</div>");
              iCant++;
            }
            id20MostRequest.Controls.Add(new LiteralControl(sHtml.ToString()));
          }
        }
        dt = null;
      }
      oConn.Close();
    }

    protected void getMasValoradoPorLaGerencia()
    {
      string sUrl = string.Empty;

      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        cReportes oReportes = new cReportes(ref oConn);
        oReportes.CodUser = oIsUsuario.CodUsuario;
        oReportes.CodConsulta = "13, 10";
        DataTable dtQuery = oReportes.GetMenu();
        if (dtQuery != null)
        {
          if (dtQuery.Rows.Count > 0)
          {
            foreach (DataRow oRow in dtQuery.Rows)
            {
              StringBuilder sHtml = new StringBuilder();
              sUrl = "../reporting/" + oRow["url_consulta_new"].ToString();

              sHtml.Append("<div class='mostrequest blq-deep-orange'>");

              sHtml.Append("<a href='");
              sHtml.Append(sUrl);
              sHtml.Append("'>");

              sHtml.Append("<div class='tt-blq-fx'>");
              sHtml.Append(oRow["nom_consulta"].ToString().ToUpper());
              sHtml.Append("</div>");

              sHtml.Append("<div class='btm-blq-getin'>");
              sHtml.Append("<i class='fas fa-sign-in-alt'></i>");
              sHtml.Append("</div>");

              sHtml.Append("</a>");

              sHtml.Append("</div>");

              MasValoradoPorlaGerencia.Controls.Add(new LiteralControl(sHtml.ToString()));
            }


          }
        }
        dtQuery = null;
      }
      oConn.Close();
    }

    protected void getVisionDeCompania()
    {
      string sUrl = string.Empty;
      string[] sCss = { "blq-orange", "blq-teal-accent-2" };
      int index;

      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        cReportes oReportes = new cReportes(ref oConn);
        oReportes.CodUser = oIsUsuario.CodUsuario;
        oReportes.CodConsulta = "13, 10, 1";
        DataTable dtQuery = oReportes.GetMenu();
        if (dtQuery != null)
        {
          if (dtQuery.Rows.Count > 0)
          {
            int iCant = 1;
            foreach (DataRow oRow in dtQuery.Rows)
            {
              StringBuilder sHtml = new StringBuilder();
              sUrl = "../reporting/" + oRow["url_consulta_new"].ToString();

              //sHtml.Append("<div class='mostrequest blq-orange'>");
              if (iCant < 11)
                index = 0;
              else
                index = 1;

              sHtml.Append("<div class='mostrequest ");
              sHtml.Append(sCss[index].ToString());
              sHtml.Append("'>");

              sHtml.Append("<a href='");
              sHtml.Append(sUrl);
              sHtml.Append("'>");

              sHtml.Append("<div class='tt-blq-fx'>");
              sHtml.Append(oRow["nom_consulta"].ToString().ToUpper());
              sHtml.Append("</div>");

              sHtml.Append("<div class='btm-blq-getin'>");
              sHtml.Append("<i class='fas fa-sign-in-alt'></i>");
              sHtml.Append("</div>");

              sHtml.Append("</a>");

              sHtml.Append("</div>");

              VisionDeCompañía.Controls.Add(new LiteralControl(sHtml.ToString()));
              iCant++;
            }


          }
        }
        dtQuery = null;
      }
      oConn.Close();
    }  

    protected void getAnalizandoClientesDiaADia()
    {
      string sUrl = string.Empty;

      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        cReportes oReportes = new cReportes(ref oConn);
        oReportes.CodUser = oIsUsuario.CodUsuario;
        oReportes.CodConsulta = "10, 2, 4";
        DataTable dtQuery = oReportes.GetMenu();
        if (dtQuery != null)
        {
          if (dtQuery.Rows.Count > 0)
          {
            foreach (DataRow oRow in dtQuery.Rows)
            {
              StringBuilder sHtml = new StringBuilder();
              sUrl = "../reporting/" + oRow["url_consulta_new"].ToString();

              sHtml.Append("<div class='mostrequest blq-teal-accent-2'>");

              sHtml.Append("<a href='");
              sHtml.Append(sUrl);
              sHtml.Append("'>");

              sHtml.Append("<div class='tt-blq-fx'>");
              sHtml.Append(oRow["nom_consulta"].ToString().ToUpper());
              sHtml.Append("</div>");

              sHtml.Append("<div class='btm-blq-getin'>");
              sHtml.Append("<i class='fas fa-sign-in-alt'></i>");
              sHtml.Append("</div>");

              sHtml.Append("</a>");

              sHtml.Append("</div>");

              AnalizandoClientesDiaADia.Controls.Add(new LiteralControl(sHtml.ToString()));
            }


          }
        }
        dtQuery = null;
      }
      oConn.Close();
    }

    protected void bnt_logout_Click(object sender, EventArgs e)
    {
      Session["USUARIO"] = string.Empty;
      Session["CodUsuarioPerfil"] = string.Empty;
      Response.Redirect("/");
    }
  }
}