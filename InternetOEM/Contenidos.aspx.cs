using System;
using System.IO;
using System.Text;
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

using Telerik.Web.UI;
using OnlineServices.Conn;
using OnlineServices.CmsData;
using OnlineServices.Method;

namespace ICommunity
{
  public partial class Contenidos : System.Web.UI.Page
  {
    Web oWeb = new Web();
    Culture oCulture = new Culture();
    protected void Page_Load(object sender, EventArgs e)
    {
      oWeb.ValidaSessionAdm();
      if (!IsPostBack)
      {
        btnCrear.Text = oCulture.GetResource("Global", "btnCrear");
        btnCrear.ToolTip = oCulture.GetResource("Global", "btnCrear");
        CodNodo.Value = oWeb.GetData("CodNodo");
      }
    }

    protected void btnCrear_Click(object sender, EventArgs e)
    {
      Response.Redirect(String.Format("Contenido.aspx?CodNodo={0}", CodNodo.Value));
    }

    protected void rdContenido_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
      switch (e.CommandName)
      {
        case "cmdEdit":
          string[] cParam = new string[2];
          cParam[0] = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["cod_contenido"].ToString();
          cParam[1] = CodNodo.Value;
          Response.Redirect(String.Format("Contenido.aspx?CodContenido={0}&CodNodo={1}", cParam));
          break;
        case "cmdDelete":
          string pCodContenido = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["cod_contenido"].ToString();
          DBConn oConn = new DBConn();
          if (oConn.Open())
          {
            StringBuilder sPath;
            CmsArchivos oArchivos = new CmsArchivos(ref oConn);
            oArchivos.CodContenido = pCodContenido;
            DataTable dArchivos = oArchivos.Get();
            if (dArchivos != null)
              if (dArchivos.Rows.Count > 0) {
                foreach (DataRow oRow in dArchivos.Rows) {
                  sPath = new StringBuilder();
                  sPath.Append(Server.MapPath("."));
                  sPath.Append(@"\rps_onlineservice\");
                  sPath.Append(@"\contenido\");
                  sPath.Append(@"\contenido_");
                  sPath.Append(pCodContenido);
                  sPath.Append(@"\");
                  sPath.Append(oRow["nom_archivo"].ToString());
                  if (File.Exists(sPath.ToString())) {
                    File.Delete(sPath.ToString());
                    oArchivos = new CmsArchivos(ref oConn);
                    oArchivos.CodArchivo = oRow["cod_archivo"].ToString();
                    oArchivos.Accion = "ELIMINAR";
                    oArchivos.Put();
                  }
                }
              }
            dArchivos = null;

            sPath = new StringBuilder();
            sPath.Append(Server.MapPath("."));
            sPath.Append(@"\binary\Contenido_");
            sPath.Append(pCodContenido);
            sPath.Append(".bin");
            File.Delete(sPath.ToString());

            CmsContenidos oContenidos = new CmsContenidos(ref oConn);
            oContenidos.CodContenido = pCodContenido;
            oContenidos.Accion = "ELIMINAR";
            oContenidos.Put();
            oConn.Close();
          }
          rdContenido.Rebind();
          break;
      }
    }

    protected void rdContenido_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
      if (e.Item is GridDataItem)
      {
        GridDataItem oItem = e.Item as GridDataItem;

        if ((!string.IsNullOrEmpty(oItem["EstContenido"].Text)) && (oItem["EstContenido"].Text != "&nbsp;"))
        {
          if (oItem["EstContenido"].Text == "V")
            oItem["EstContenido"].Text = oCulture.GetResource("Global", "Vigente");
          else
            oItem["EstContenido"].Text = oCulture.GetResource("Global", "NoVigente");
        }
      }
    }

    protected void rdContenido_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        CmsContenidos oContenidos = new CmsContenidos(ref oConn);
        oContenidos.CodNodo = CodNodo.Value;

        GridColumn oGridColumn;

        oGridColumn = rdContenido.MasterTableView.Columns.FindByUniqueName("NomContenido");
        oGridColumn.HeaderText = oCulture.GetResource("Contenido", "NomContenido");

        oGridColumn = rdContenido.MasterTableView.Columns.FindByUniqueName("EstContenido");
        oGridColumn.HeaderText = oCulture.GetResource("Contenido", "EstContenido");

        rdContenido.DataSource = oContenidos.Get();

        oConn.Close();
      }
    }
  }
}
