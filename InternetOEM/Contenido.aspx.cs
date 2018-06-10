using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;
using System.IO;

using Telerik.Web.UI;
using OnlineServices.Conn;
using OnlineServices.SystemData;
using OnlineServices.CmsData;
using OnlineServices.Method;

namespace ICommunity
{
  public partial class Contenido : System.Web.UI.Page
  {
    Web oWeb = new Web();
    Log oLog = new Log();
    OnlineServices.Method.Usuario oIsUsuario;

    protected void Page_Load(object sender, EventArgs e)
    {
      oWeb.ValidaSessionAdm();
      if (!IsPostBack)
      {
        CodContenido.Value = oWeb.GetData("CodContenido");
        CodNodo.Value = oWeb.GetData("CodNodo");
        if (!string.IsNullOrEmpty(CodContenido.Value))
        {
          DBConn oConn = new DBConn();
          if (oConn.Open())
          {
            CmsContenidos oContenidos = new CmsContenidos(ref oConn);
            oContenidos.CodContenido = CodContenido.Value;
            DataTable dContenidos = oContenidos.Get();
            if (dContenidos != null)
              if (dContenidos.Rows.Count > 0)
              {
                txtTitulo.Text = dContenidos.Rows[0]["titulo_contenido"].ToString();
                rdCmbEstado.FindItemByValue(dContenidos.Rows[0]["est_contenido"].ToString()).Selected = true;
                rdDescripcion.Content = dContenidos.Rows[0]["texto_contenido"].ToString();
                rdResumen.Content = dContenidos.Rows[0]["resumen_contenido"].ToString();
                chk_destacado.Checked = (dContenidos.Rows[0]["dest_contenido"].ToString() == "1" ? true : false);
                chk_rss.Checked = (dContenidos.Rows[0]["ind_rss"].ToString() == "1" ? true : false);
                btnArchivos.Visible = true;
                idGridFile.Visible = true;
              }
            dContenidos = null;

            oConn.Close();
          }
        }
      }
      else
      {
        if ((Session["ReloadImageCont"] != null) && (!string.IsNullOrEmpty(Session["ReloadImageCont"].ToString())))
        {
          rdgArchivos.Rebind();
        }
      }

    }

    protected void btnGrabar_Click(object sender, EventArgs e)
    {
      oIsUsuario = oWeb.GetObjUsuario();
      string sFile;
      string cAccion = "CREAR";
      StringBuilder sPath;
      DBConn oConn = new DBConn();
      try
      {
        if (oConn.Open())
        {
          oConn.BeginTransaction();

          if (string.IsNullOrEmpty(CodContenido.Value))
          {
            ObjectModel oObjectModel = new ObjectModel(ref oConn);
            CodContenido.Value = oObjectModel.getCodeKey("CMS_CONTENIDOS");
          }else
            cAccion = "EDITAR";

          CmsContenidos oContenidos = new CmsContenidos(ref oConn);
          oContenidos.CodContenido = CodContenido.Value;
          oContenidos.CodNodo = CodNodo.Value;
          oContenidos.CodUsuario = oIsUsuario.CodUsuario;
          oContenidos.TituloContenido = txtTitulo.Text;
          oContenidos.TextoContenido = rdDescripcion.Content;
          oContenidos.DateContenido = DateTime.Now.ToString();
          oContenidos.EstContenido = rdCmbEstado.SelectedValue;
          oContenidos.DestContenido = (chk_destacado.Checked ? "1" : "0");
          oContenidos.IndRss = (chk_rss.Checked ? "1" : "0");
          oContenidos.ResumenContenido = rdResumen.Content;
          oContenidos.IpUsuario = oWeb.GetIpUsuario();
          oContenidos.Accion = cAccion;
          oContenidos.Put();
          CodContenido.Value = oContenidos.CodContenido;
          if (string.IsNullOrEmpty(oContenidos.Error))
          {

            oConn.Commit();

            sPath = new StringBuilder();
            sPath.Append(Server.MapPath("."));
            sPath.Append(@"\binary\");
            sFile = "Contenido_" + oContenidos.CodContenido + ".bin";
            oContenidos.SerializaContenido(ref oConn, sPath.ToString(), sFile);

            sFile = "Contenidos.bin";
            oContenidos.SerializaContenidos(ref oConn, sPath.ToString(), sFile);

            sFile = "Contenido_n" + CodNodo.Value + ".bin";
            oContenidos.SerializaTblContenidoByNodo(ref oConn, sPath.ToString(), sFile);

            sPath = new StringBuilder();
            sPath.Append(Server.MapPath("."));
            sPath.Append(@"\rps_onlineservice\");
            sPath.Append(@"\contenido\");
            sPath.Append(@"\contenido_");
            sPath.Append(CodContenido.Value);
            sPath.Append(@"\");
            if (!Directory.Exists(sPath.ToString()))
            {
              Directory.CreateDirectory(sPath.ToString());
            }

            oLog.CodEvtLog = "003";
            oLog.IdUsuario = (!string.IsNullOrEmpty(oIsUsuario.CodUsuario)? oIsUsuario.CodUsuario : "-1");
            oLog.ObsLog = "<CONTENIDO>" + Session["CodUsuarioPerfil"].ToString();
            //oLog.putLog();
          }
          else
          {
            oConn.Rollback();
          }
          oConn.Close();
          btnArchivos.Visible = true;
          idGridFile.Visible = true;
          rdgArchivos.Rebind();
        }
      }
      catch (Exception Ex)
      {
        if (oConn.bIsOpen)
        {
          oConn.Rollback();
          oConn.Close();
        }
      }
    }

    protected void rdgArchivos_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        CmsArchivos oArchivos = new CmsArchivos(ref oConn);
        oArchivos.CodContenido = CodContenido.Value;
        rdgArchivos.DataSource = oArchivos.Get();

        oConn.Close();
      }
    }

    protected void rdgArchivos_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
      switch (e.CommandName)
      {
        case "cmdDelete":
          StringBuilder sPath = new StringBuilder();
          sPath.Append(Server.MapPath("."));
          sPath.Append(@"\rps_onlineservice\");
          sPath.Append(@"\contenido\");
          sPath.Append(@"\contenido_");
          sPath.Append(CodContenido.Value);
          sPath.Append(@"\");
          string pCodArchivo = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["cod_archivo"].ToString();
          DBConn oConn = new DBConn();
          if (oConn.Open())
          {
            CmsArchivos oArchivos = new CmsArchivos(ref oConn);
            oArchivos.CodArchivo = pCodArchivo;
            DataTable dArchivos = oArchivos.Get();
            if (dArchivos != null)
              if (dArchivos.Rows.Count > 0)
              {
                sPath.Append(dArchivos.Rows[0]["nom_archivo"].ToString());
                File.Delete(sPath.ToString());
              }
            dArchivos = null;

            oArchivos.Accion = "ELIMINAR";
            oArchivos.Put();
            oConn.Close();
          }
          rdgArchivos.Rebind();
          break;
      }
    }
  }
}
