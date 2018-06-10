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

using Telerik.Web.UI;
using OnlineServices.Conn;
using OnlineServices.Method;
using OnlineServices.CmsData;

namespace ICommunity
{
  public partial class NodoOrden : System.Web.UI.Page
  {
    private string pCodNodo = string.Empty;
    Culture oCulture = new Culture();
    Web oWeb = new Web();

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        btnGrabar.Text = oCulture.GetResource("Global", "btnGrabar");
        CodNodo.Value = oWeb.GetData("CodNodo");
        getListNodos();
      }
    }

    void getListNodos()
    {
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        CmsNodos oNodos = new CmsNodos(ref oConn);
        oNodos.CodNodoRel = CodNodo.Value;
        oNodos.IndOrden = " Order by ord_nodo asc ";
        rdListNodos.DataSource = oNodos.Get();
        rdListNodos.DataTextField = "titulo_nodo";
        rdListNodos.DataValueField = "cod_nodo";
        rdListNodos.DataBind();

        oConn.Close();
      }
    }

    protected void btnGrabar_Click(object sender, EventArgs e)
    {
      int iCount = 0;
      string cPath = Server.MapPath(".") + @"\binary\";
      
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        CmsNodos oNodos = new CmsNodos(ref oConn);
        foreach (RadListBoxItem oItem in rdListNodos.Items)
        {
          iCount = oItem.Index;
          iCount++;
          oNodos.CodNodo = oItem.Value.ToString();
          oNodos.OrdNodo = iCount.ToString();
          oNodos.Accion = "EDITAR";
          oNodos.Put();

          if (string.IsNullOrEmpty(oNodos.Error)) {
            oNodos.SerializaNodo(ref oConn, cPath, "Nodo_" + oNodos.CodNodo + ".bin");
            oNodos.SerializaTblNodo(ref oConn, cPath, "Nodos.bin");
          }

        }
        oConn.Close();
      }
    }

  }
}
