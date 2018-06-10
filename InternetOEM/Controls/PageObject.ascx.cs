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
using System.Xml;

using Telerik.Web.UI;
using OnlineServices.Conn;
using OnlineServices.CmsData;
using OnlineServices.AppData;
using OnlineServices.Method;

namespace ICommunity.Controls
{
  public partial class PageObject : System.Web.UI.UserControl
  {
    private bool bIndZonas = false;
    public bool pbIndZonas { get { return bIndZonas; } set { bIndZonas = value; } }

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack) {
        if (bIndZonas)
        {
          rdTabStrip.Tabs[1].Visible = true;
          LoadTreeZonas();
          rdTabStrip.Tabs[2].Visible = true;
          LoadTreeApps();
          rdTabStrip.Tabs[3].Visible = true;
          LoadTreeNodos();
        }
        LoadTreeObjects();
      }
    }

    protected void LoadTreeApps() {
      RadTreeNode oNode;
      DBConn oConn = new DBConn();
      if (oConn.Open()) {
        AppBanner oBanner = new AppBanner(ref oConn);
        oBanner.EstBanner = "V";
        DataTable dBanner = oBanner.Get();
        if (dBanner != null) {
          if (dBanner.Rows.Count > 0) {
            foreach (DataRow oRow in dBanner.Rows) {
              oNode = new RadTreeNode(oRow["nom_banner"].ToString(), "banners_" + oRow["cod_banner"].ToString());
              rdTreeApps.Nodes.Add(oNode);
            }
          }
        }
        dBanner = null;
      }
      oConn.Close();

    }

    protected void LoadTreeZonas()
    { 
      RadTreeNode oNode;
      DBConn oConn = new DBConn();
      if (oConn.Open()){
        CmsZona oZona = new CmsZona(ref oConn);
        oZona.EstZona = "V";
        DataTable dZona = oZona.Get();
        if (dZona != null)
          if (dZona.Rows.Count > 0){
            foreach(DataRow oRow in dZona.Rows){
              oNode = new RadTreeNode(oRow["nom_zona"].ToString(),"zona_" + oRow["cod_zona"].ToString());
              rdTreeZona.Nodes.Add(oNode);
            }
          }
        dZona = null;
        oConn.Close();
      }
      
    }

    protected void LoadTreeNodos() {
      RadTreeNode Node = new RadTreeNode("DEBTCONTROL");
      Node.Expanded = true;
      //Node.ContextMenuID = "Menu";
      //Node.CssClass = "mn_inicial";
      Node.Value = "nodo_0";
      RadTreeNodos.Nodes.Add(Node);

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
        oNode = new RadTreeNode(oRow["titulo_nodo"].ToString(),"nodo_" + oRow["cod_nodo"].ToString());
        //if (string.IsNullOrEmpty(CodNodoRel))
        //oNode.Expanded = true;
        //oNode.ContextMenuID = "Menu";
        //oNode.ImageUrl = "~/images/icons/icon_file.png";
        oTreeNodo.Nodes.Add(oNode);
        getTree(oData, oNode, oRow["cod_nodo"].ToString());
      }
    }

    protected void LoadTreeObjects() {
      string cName = string.Empty, cParent = string.Empty, cEtiqueta = string.Empty;
      RadTreeNode oRoot = new RadTreeNode();
      XmlDocument rssDocument = new XmlDocument();
      rssDocument.Load(Server.MapPath("~/") + @"\Resources\PageObject.xml");
      XmlNodeList xmlObjeto = rssDocument.SelectNodes("OnLine/objeto");
      for (int i = 0; i < xmlObjeto.Count; i++)
      {
        cName = xmlObjeto[i].Attributes[0].Value;
        cParent = xmlObjeto[i].Attributes[1].Value;
        if (string.IsNullOrEmpty(cParent))
        {
          cEtiqueta = GetValor(xmlObjeto[i], "etiqueta");
          if (xmlObjeto[i].SelectSingleNode("parametro") != null)
            cName = "{" + cName + "}";
          oRoot = new RadTreeNode(cEtiqueta, cName);
          Children(ref oRoot, xmlObjeto, cName);
          rdTreeObject.Nodes.Add(oRoot);
        }
      }
    }

    protected void Children(ref RadTreeNode oRoot, XmlNodeList xmlObjeto, string cChildren)
    {
      string cName = string.Empty, cParent = string.Empty, cEtiqueta = string.Empty, cText = string.Empty, cValue = string.Empty;
      for (int i = 0; i < xmlObjeto.Count; i++)
      {
        cName = xmlObjeto[i].Attributes[0].Value;
        cParent = xmlObjeto[i].Attributes[1].Value;
        if (cParent == cChildren)
        {
          switch (cName)
          {
            case "metadata":
              //OsdDescriptor oOsdDescriptor = new OsdDescriptor(oConn);
              //oOsdDescriptor.CodSistema.Valor = pCodSistema;
              //oData = oOsdDescriptor.Get();
              //cText = GetValor(xmlObjeto[i], "text");
              //cValue = GetValor(xmlObjeto[i], "value");
              //foreach (DataRow oRow in oData.Rows)
              //{
              //  oRoot.Nodes.Add(new RadTreeNode(oRow[cText].ToString(), oRow[cValue].ToString()));
              //}
              break;
            default:
              cEtiqueta = GetValor(xmlObjeto[i], "etiqueta");
              if (xmlObjeto[i].SelectSingleNode("parametro") != null)
                cName = "{" + cName + "}";
              oRoot.Nodes.Add(new RadTreeNode(cEtiqueta, cName));
              break;
          }
        }
      }
    }

    protected string GetValor(XmlNode oNode, string cKey)
    {
      string cValue = string.Empty;
      XmlNode oInfo = oNode.SelectSingleNode(cKey);
      if (oInfo != null)
      {
        cValue = oInfo.InnerText;
      }
      else
      {
        cValue = string.Empty;
      }
      return cValue;
    }

  }
}