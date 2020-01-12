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

using OnlineServices.Conn;
using OnlineServices.CmsData;
using OnlineServices.Method;
using Telerik.Web.UI;

namespace ICommunity
{
  public partial class framework : System.Web.UI.Page
  {
    Web oWeb = new Web();
    protected void Page_Load(object sender, EventArgs e)
    {
      oWeb.ValidaSessionAdm();
      if (!IsPostBack)
      {
        rdTree.Nodes.Clear();
        RadTreeNode Node = new RadTreeNode("DEBTCONTROL");
        Node.Expanded = true;
        Node.ContextMenuID = "Menu";
        Node.CssClass = "mn_inicial";
        Node.Value = "0";
        rdTree.Nodes.Add(Node);

        DBConn oConn = new DBConn();
        oConn.Open();
        CmsNodos oNodos = new CmsNodos(ref oConn);
        DataTable dNodos = oNodos.Get();
        if (dNodos != null)
          if (dNodos.Rows.Count > 0)
            getTree(dNodos, Node, "0");
        dNodos = null;
        oConn.Close();
      }
    }

    protected void FilterMenu(RadMenuItem oItem)
    {
      RadMenuItem rdMenu;
      rdSubMenu.Items.Clear();
      switch (oItem.Text)
      {
        case "TEMPLATES":
          rdMenu = new RadMenuItem();
          rdMenu.Text = "PAGINA";
          rdMenu.CssClass = "mn-superiordos";
          rdSubMenu.Items.Add(rdMenu);

          rdMenu = new RadMenuItem();
          rdMenu.Text = "ZONA";
          rdMenu.CssClass = "mn-superiordos";
          rdSubMenu.Items.Add(rdMenu);

          //rdMenu = new RadMenuItem();
          //rdMenu.Text = "HOJAS DE ESTILO";
          //rdMenu.CssClass = "mn-superior";
          //rdSubMenu.Items.Add(rdMenu);
          break;
        case "CONFIGURACION":
          rdMenu = new RadMenuItem();
          rdMenu.Text = "USUARIOS";
          rdMenu.CssClass = "mn-superiordos";
          rdSubMenu.Items.Add(rdMenu);

          //rdMenu = new RadMenuItem();
          //rdMenu.Text = "GRUPOS";
          //rdMenu.CssClass = "mn-superior";
          //rdSubMenu.Items.Add(rdMenu);

          //rdMenu = new RadMenuItem();
          //rdMenu.Text = "CAMPOS USUARIOS";
          //rdMenu.CssClass = "mn-superior";
          //rdSubMenu.Items.Add(rdMenu);

          //rdMenu = new RadMenuItem();
          //rdMenu.Text = "CAMPOS DINAMICOS";
          //rdMenu.CssClass = "mn-superior";
          //rdSubMenu.Items.Add(rdMenu);

          rdMenu = new RadMenuItem();
          rdMenu.Text = "PERFILES";
          rdMenu.CssClass = "mn-superiordos";
          rdSubMenu.Items.Add(rdMenu);

          rdMenu = new RadMenuItem();
          rdMenu.Text = "PARAMETROS";
          rdMenu.CssClass = "mn-superiordos";
          rdSubMenu.Items.Add(rdMenu);

          rdMenu = new RadMenuItem();
          rdMenu.Text = "EMAIL";
          rdMenu.CssClass = "mn-superiordos";
          rdSubMenu.Items.Add(rdMenu);

          rdMenu = new RadMenuItem();
          rdMenu.Text = "TIPO CAMBIO";
          rdMenu.CssClass = "mn-superiordos";
          rdSubMenu.Items.Add(rdMenu);
          break;
        case "APLICACIONES":
          rdMenu = new RadMenuItem();
          rdMenu.Text = "REPORTES DEBTNET";
          rdMenu.CssClass = "mn-superiordos";
          rdSubMenu.Items.Add(rdMenu);

          rdMenu = new RadMenuItem();
          rdMenu.Text = "REPORTES RUTA";
          rdMenu.CssClass = "mn-superiordos";
          rdSubMenu.Items.Add(rdMenu);

          rdMenu = new RadMenuItem();
          rdMenu.Text = "ANTALIS";
          rdMenu.CssClass = "mn-superiordos";
          rdSubMenu.Items.Add(rdMenu);

          rdMenu = new RadMenuItem();
          rdMenu.Text = "CONSULTA DE LOG'S";
          rdMenu.CssClass = "mn-superiordos";
          rdSubMenu.Items.Add(rdMenu);

          //rdMenu = new RadMenuItem();
          //rdMenu.Text = "RANKING";
          //rdMenu.CssClass = "mn-superior";
          //rdSubMenu.Items.Add(rdMenu);

          //rdMenu = new RadMenuItem();
          //rdMenu.Text = "BANNERS";
          //rdMenu.CssClass = "mn-superior";
          //rdSubMenu.Items.Add(rdMenu);

          //rdMenu = new RadMenuItem();
          //rdMenu.Text = "CORREOS MASIVOS";
          //rdMenu.CssClass = "mn-superior";
          //rdSubMenu.Items.Add(rdMenu);
          break;
      }

      //rdSubMenu.Items[0].Width = Unit.Pixel(168);
      rdSubMenu.Visible = true;
      oItem.Selected = true;
    }

    protected void rdMenu_ItemClick(object sender, Telerik.Web.UI.RadMenuEventArgs e)
    {
      FilterMenu(e.Item);
    }

    protected void rdSubMenu_ItemClick(object sender, Telerik.Web.UI.RadMenuEventArgs e)
    {
      switch (e.Item.Text)
      {
        case "PAGINA":
          rpnNavigate.ContentUrl = "Templates.aspx";
          break;
        case "ZONA":
          rpnNavigate.ContentUrl = "Zonas.aspx";
          break;
        case "HOJAS DE ESTILO":
          rpnNavigate.ContentUrl = "HojasEstilos.aspx";
          break;
        case "USUARIOS":
          rpnNavigate.ContentUrl = "Usuarios.aspx";
          break;
        case "GRUPOS":
          rpnNavigate.ContentUrl = "Grupos.aspx";
          break;
        case "CAMPOS USUARIOS":
          rpnNavigate.ContentUrl = "CampoUsuarios.aspx";
          break;
        case "CAMPOS DINAMICOS":
          rpnNavigate.ContentUrl = "CamposDinamicos.aspx";
          break;
        case "PERFILES":
          rpnNavigate.ContentUrl = "perfiles.aspx";
          break;
        case "PARAMETROS":
          rpnNavigate.ContentUrl = "parametros.aspx";
          break;
        case "EMAIL":
          rpnNavigate.ContentUrl = "ParamEmail.aspx";
          break;
        case "RANKING":
          rpnNavigate.ContentUrl = "PreguntaRanking.aspx";
          break;
        case "BANNERS":
          rpnNavigate.ContentUrl = "Banners.aspx";
          break;
        case "CORREOS MASIVOS":
          rpnNavigate.ContentUrl = "ETarget.aspx";
          break;
        case "REPORTES DEBTNET":
          rpnNavigate.ContentUrl = "menu_app_debtcontrol.aspx";
          break;
        case "REPORTES RUTA":
          rpnNavigate.ContentUrl = "menu_app_reportesruta.aspx";
          break;
        case "ANTALIS":
          rpnNavigate.ContentUrl = "antalis/default.aspx";
          break;
        case "CONSULTA DE LOG'S":
          rpnNavigate.ContentUrl = "consulta_logs.aspx";
          break;
        case "TIPO CAMBIO":
          rpnNavigate.ContentUrl = "tipo_cambio.aspx";
          break;
      }
    }

    protected void getTree(DataTable oData, RadTreeNode oTreeNodo, string CodNodoRel)
    {
      DataRow[] oDataRow;
      if (string.IsNullOrEmpty(CodNodoRel))
        oDataRow = oData.Select("cod_nodo_rel is null ", "");
      else
        oDataRow = oData.Select("cod_nodo_rel = " + CodNodoRel, "");

      RadTreeNode oNode;
      foreach (DataRow oRow in oDataRow)
      {
        oNode = new RadTreeNode(oRow["titulo_nodo"].ToString(), oRow["cod_nodo"].ToString());
        if (string.IsNullOrEmpty(CodNodoRel))
          oNode.Expanded = true;
        oNode.ContextMenuID = "Menu";
        oNode.ImageUrl = "~/images/icons/icon_file.png";
        oTreeNodo.Nodes.Add(oNode);
        getTree(oData, oNode, oRow["cod_nodo"].ToString());
      }
    }

    protected void rdTree_ContextMenuItemClick(object sender, Telerik.Web.UI.RadTreeViewContextMenuEventArgs e)
    {
      StringBuilder cPath;
      StringBuilder cUrl = new StringBuilder();
      switch (e.MenuItem.Value)
      {
        case "CREAR":
          cUrl.Append("Nodo.aspx?CodNodoRel=");
          cUrl.Append(e.Node.Value);
          rpnNavigate.ContentUrl = cUrl.ToString();
          break;
        case "EDITAR":
          cUrl.Append("Nodo.aspx?CodNodo=");
          cUrl.Append(e.Node.Value);
          rpnNavigate.ContentUrl = cUrl.ToString();
          break;
        case "ORDENAR":
          cUrl.Append("NodoOrden.aspx?CodNodo=");
          cUrl.Append(e.Node.Value);
          rpnNavigate.ContentUrl = cUrl.ToString();
          break;
        case "ELIMINAR":

          DBConn oConn = new DBConn();
          if (oConn.Open())
          {
            cPath = new StringBuilder();
            cPath.Append(Server.MapPath(".")).Append(@"\binary\Nodo_").Append(e.Node.Value).Append(".bin");
            if (File.Exists(cPath.ToString()))
              File.Delete(cPath.ToString());

            CmsNodos oNodos = new CmsNodos(ref oConn);
            oNodos.Accion = "ELIMINAR";
            oNodos.CodNodo = e.Node.Value;
            oNodos.Put();

            cPath.Length = 0;
            cPath.Append(Server.MapPath(".")).Append(@"\binary\");
            oNodos.SerializaTblNodo(ref oConn, cPath.ToString(), "Nodos.bin");

            e.Node.Remove();
          }

          break;
        case "CONTENIDOS":
          cUrl.Append("Contenidos.aspx?CodNodo=");
          cUrl.Append(e.Node.Value);
          rpnNavigate.ContentUrl = cUrl.ToString();
          break;
        case "CREARCONTENIDO":
          cUrl.Append("Contenido.aspx?CodNodo=");
          cUrl.Append(e.Node.Value);
          rpnNavigate.ContentUrl = cUrl.ToString();
          break;
      }
    }

    protected void rdTree_NodeClick(object sender, Telerik.Web.UI.RadTreeNodeEventArgs e)
    {
      if (!string.IsNullOrEmpty(e.Node.Value) & e.Node.Level > 0)
      {
        CodNodo.Value = e.Node.Value;
        if (!string.IsNullOrEmpty(rpnNavigate.ClientID))
        {
          StringBuilder cUrl = new StringBuilder();
          cUrl.Append("NodoPreview.aspx");
          Session["CodNodo"] = e.Node.Value;
          rpnNavigate.ContentUrl = cUrl.ToString();
        }
      }
    }

    protected void lnkBtnLogout_Click(object sender, EventArgs e)
    {
      Session["USUARIO"] = string.Empty;
      Session["CodUsuarioPerfil"] = string.Empty;
      Response.Redirect("/adm");
    }
  }
}
