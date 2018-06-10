using System;
using System.Text;
using System.IO;
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
using OnlineServices.SystemData;

namespace ICommunity.Controls
{
  public partial class NombreUsuario : System.Web.UI.UserControl
  {
    Web oWeb = new Web();
    private OnlineServices.Method.Usuario oIsUsuario;
    private BinaryUsuario oBinaryUsuario;
    protected void Page_Load(object sender, EventArgs e)
    {
      bool blnLabel = (!string.IsNullOrEmpty(this.Attributes["blnLabel"]) ? true : false);
      if ((Session["CodUsuarioPerfil"] != null) && (!string.IsNullOrEmpty(Session["CodUsuarioPerfil"].ToString())))
      {
        SysUsuario oUsuario = new SysUsuario();
        oUsuario.Path = Server.MapPath(".").ToString();
        oUsuario.CodUsuario = Session["CodUsuarioPerfil"].ToString();
        oBinaryUsuario = oUsuario.ClassGet();

        if (oBinaryUsuario != null)
        {
          if (!blnLabel)
          {
            Button oButton = new Button();
            oButton.Text = oBinaryUsuario.NomUsuario + " " + oBinaryUsuario.ApeUsuario;
            oButton.CssClass = (!string.IsNullOrEmpty(this.Attributes["csStyle"]) ? this.Attributes["csStyle"] : "lblNombreUsuario");
            oButton.Click += new EventHandler(oButton_Click);
            Controls.Add(oButton);
          }
          else
          {
            Label oLabel = new Label();
            oLabel.Text = oBinaryUsuario.NomUsuario + " " + oBinaryUsuario.ApeUsuario;
            oLabel.CssClass = (!string.IsNullOrEmpty(this.Attributes["csStyle"]) ? this.Attributes["csStyle"] : "lblNombreUsuario");
            Controls.Add(oLabel);
          }
        }
        oBinaryUsuario = null;
      }
    }

    void oButton_Click(object sender, EventArgs e)
    {
      DataRow[] oRow;
      DataTable oNodos = oWeb.DeserializarTbl(Server.MapPath("."), "Nodos.bin");
      if (oNodos != null)
        if (oNodos.Rows.Count > 0)
        {
          oRow = oNodos.Select(" cod_nodo_rel = 0 and est_nodo = 'V' and pf_nodo = 'V' ");
          if (oRow != null)
          {
            Session["CodNodo"] = oRow[0]["cod_nodo"].ToString();
          }
          oRow = null;
        }
      oNodos = null;
      Page.Response.Redirect(".");
    }
  }
}