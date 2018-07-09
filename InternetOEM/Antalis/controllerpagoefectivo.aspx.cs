using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

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
  public partial class controllerpagoefectivo : System.Web.UI.Page
  {
    private OnlineServices.Method.Web oWeb = new OnlineServices.Method.Web();
    private OnlineServices.Method.Usuario oIsUsuario;
    Emailing oEmailing = new Emailing();

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
        hdd_cod_pago.Value = oWeb.GetData("CodPago");

        DBConn oConn = new DBConn();
        if (oConn.Open())
        {
          cAntPagos oPagos = new cAntPagos(ref oConn);
          oPagos.CodPagos = hdd_cod_pago.Value;
          DataTable dt = oPagos.Get();
          if (dt != null)
          {
            if (dt.Rows.Count > 0)
            {
              lblValija.Text = "# Valija " + dt.Rows[0]["cod_pago"].ToString();

              cCliente oCliente = new cCliente(ref oConn);
              oCliente.CodNkey = dt.Rows[0]["nkey_cliente"].ToString();
              DataTable dtCliente = oCliente.GeCliente();
              if (dtCliente != null)
              {
                if (dtCliente.Rows.Count > 0)
                {
                  lblRazonSocial.Text = dtCliente.Rows[0]["cliente"].ToString();
                }
              }
              dtCliente = null;

              cAntCentrosDistribucion oCentrosDistribucion = new cAntCentrosDistribucion(ref oConn);
              oCentrosDistribucion.CodCentroDist = dt.Rows[0]["cod_centrodist"].ToString();
              DataTable dtCentro = oCentrosDistribucion.GetByCod();
              if (dtCentro != null)
              {
                if (dtCentro.Rows.Count > 0)
                {
                  lblCentroDistribucion.Text = dtCentro.Rows[0]["descripcion"].ToString();
                }
              }
              dtCentro = null;

              lblFecharecepcion.Text = dt.Rows[0]["fech_recepcion"].ToString();
              lblimporte.Text = dt.Rows[0]["importe_total"].ToString();
              hdd_importe.Value = dt.Rows[0]["importe_total"].ToString();

            }
          }
          dt = null;

        }
        oConn.Close();
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
          }
        }
        dtPerfil = null;
      }
      oConn.Close();

      oHtmControl.Controls.Add(new LiteralControl("<li><a href='../antalis/reportevalijas.aspx'>Valijas Validadas</a></li>"));
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

    protected void btnRechazar_Click(object sender, EventArgs e)
    {
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        cAntPagos oPagos = new cAntPagos(ref oConn);
        oPagos.CodPagos = hdd_cod_pago.Value;
        oPagos.ImporteTotalRecibido = txt_importe_recibido.Text;
        oPagos.Discrepancia = txt_discrepancia.Text;
        oPagos.Estado = "A";
        oPagos.Accion = "EDITAR";
        oPagos.Put();
        oConn.Close();
      }

      StringBuilder sHmtl = new StringBuilder();
      sHmtl.Append("Se informa que la valija # ").Append(lblValija.Text).Append(" a sido rechazada.");

      AppSettingsReader appReader = new System.Configuration.AppSettingsReader();
      string sEmlAddr = appReader.GetValue("AntalisMail", typeof(string)).ToString();

      oEmailing.FromName = Application["NameSender"].ToString();
      oEmailing.From = Application["EmailSender"].ToString();
      oEmailing.Address = sEmlAddr;
      oEmailing.Subject = "Valija # " + lblValija.Text + ", RECHAZADA";
      oEmailing.Body = sHmtl;
      oEmailing.EmailSend();

      Response.Redirect("controllerpagos.aspx");

    }

    protected void btnAprobar_Click(object sender, EventArgs e)
    {
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        cAntPagos oPagos = new cAntPagos(ref oConn);
        oPagos.CodPagos = hdd_cod_pago.Value;
        oPagos.ImporteTotalRecibido = txt_importe_recibido.Text;
        oPagos.Discrepancia = txt_discrepancia.Text;
        oPagos.Estado = "V";
        oPagos.Accion = "EDITAR";
        oPagos.Put();
        oConn.Close();
      }

      StringBuilder sHmtl = new StringBuilder();
      sHmtl.Append("Se informa que la valija # ").Append(lblValija.Text).Append(" a sido validada.");

      AppSettingsReader appReader = new System.Configuration.AppSettingsReader();
      string sEmlAddr = appReader.GetValue("AntalisMail", typeof(string)).ToString();

      oEmailing.FromName = Application["NameSender"].ToString();
      oEmailing.From = Application["EmailSender"].ToString();
      oEmailing.Address = sEmlAddr;
      oEmailing.Subject = "Valija # " + lblValija.Text + ", VALIDADA";
      oEmailing.Body = sHmtl;
      oEmailing.EmailSend();

      Response.Redirect("controllerpagos.aspx");
    }
  }
}