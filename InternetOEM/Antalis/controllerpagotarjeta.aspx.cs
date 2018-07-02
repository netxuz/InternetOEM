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
using System.Web.Services;

namespace ICommunity.Antalis
{
  public partial class controllerpagotarjeta : System.Web.UI.Page
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
                  oHtmControl.Controls.Add(new LiteralControl("<li><a href='../antalis/pagos_antalis.aspx'>Pagos</a></li>"));
                if (oRow["cod_rol"].ToString() == "2")
                  oHtmControl.Controls.Add(new LiteralControl("<li><a href='../antalis/controllerpagos.aspx'>Validación de Pago</a></li>"));
              }
            }
            dtAntRoles = null;
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
  }
}