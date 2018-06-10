using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
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
using OnlineServices.SystemData;

namespace ICommunity.Controls
{
  public partial class NavegacionPrincipal : System.Web.UI.UserControl
  {
    Web oWeb = new Web();
    private OnlineServices.Method.Usuario oIsUsuario;
    private BinaryUsuario oBinaryUsuario;

    private string sRuta = string.Empty;
    public string Ruta { get { return sRuta; } set { sRuta = value; } }

    protected void Page_Load(object sender, EventArgs e)
    {
      oIsUsuario = oWeb.GetObjUsuario();
      StringBuilder sQueryFilter = new StringBuilder();
      string cPath = Server.MapPath(".").ToUpper();
      if (!string.IsNullOrEmpty(sRuta))
        cPath = cPath.Replace(sRuta.ToUpper(), "");

      DataTable dNodos = oWeb.DeserializarTbl(cPath, "Nodos.bin");
      if (dNodos != null)
      { 
        sQueryFilter.Append(" est_nodo = 'V' ");
        if (!string.IsNullOrEmpty(this.Attributes["CodNodo"]))
          sQueryFilter.Append(" and cod_nodo_rel = ").Append(this.Attributes["CodNodo"].ToString());
        else
          sQueryFilter.Append(" and cod_nodo_rel = 0 ");

        if ((Session["USUARIO"] == null)||(string.IsNullOrEmpty(Session["USUARIO"].ToString())))
          sQueryFilter.Append(" and prv_nodo = 0 ");

        DataRow[] oRows = dNodos.Select(sQueryFilter.ToString(), " ord_nodo asc ");
        if (oRows != null)
          if (oRows.Count() > 0)
          {
            bool bAllow;
            //Button oButton;
            LinkButton oButton;
            Controls.Add(new LiteralControl("<ul>"));
            foreach(DataRow oRow in oRows){
              bAllow = true;
              if ((oRow["ind_despl_usr_client"].ToString() == "V") || (oRow["ind_despl_usr_site"].ToString() == "V"))
              {
                StringBuilder oFolder = new StringBuilder();
                oFolder.Append(Server.MapPath("."));

                SysUsuario oUsuario = new SysUsuario();
                oUsuario.Path = oFolder.ToString();
                oUsuario.CodUsuario = Session["CodUsuarioPerfil"].ToString();
                oBinaryUsuario = oUsuario.ClassGet();
                if (oBinaryUsuario != null) {
                  if ((oBinaryUsuario.CodTipo == "1") && (oRow["ind_despl_usr_client"].ToString() == "V"))
                    bAllow = false;
                  if ((oBinaryUsuario.CodTipo != "1") && (oRow["ind_despl_usr_site"].ToString() == "V"))
                    bAllow = false;
                }
                oBinaryUsuario = null;
              }

              if (oRow["ini_asoc_usr_nodo"].ToString() == "V")
                if (oIsUsuario.CodUsuario != Session["CodUsuarioPerfil"].ToString())
                  bAllow = false;

              if (oRow["ind_registrate_nodo"].ToString() == "V")
                if (!string.IsNullOrEmpty(oIsUsuario.CodUsuario))
                  bAllow = false;

              if (bAllow)
              {
                Controls.Add(new LiteralControl("<li class=\"" + (((Session["CodNodo"] != null)&&(Session["CodNodo"].ToString() == oRow["cod_nodo"].ToString())) ? "BtnNavHover" : "BtnNav") + "\">"));
                //oButton = new Button();
                oButton = new LinkButton();
                oButton.ID = "btnNav_" + oRow["cod_nodo"].ToString();
                oButton.Text = oRow["titulo_nodo"].ToString();
                oButton.Attributes["CodNodo"] = oRow["cod_nodo"].ToString();
                oButton.Attributes["IndPerfil"] = oRow["pf_nodo"].ToString();
                oButton.Attributes["IndDesplUsrClient"] = oRow["ind_despl_usr_client"].ToString();
                oButton.CssClass = (((Session["CodNodo"] != null) && (Session["CodNodo"].ToString() == oRow["cod_nodo"].ToString())) ? "BtnNavHover" : "BtnNav");
                oButton.CausesValidation = false;
                oButton.Click += new EventHandler(oButton_Click);
                Controls.Add(oButton);
                
                Controls.Add(new LiteralControl("</li>"));
              }
            }
            Controls.Add(new LiteralControl("</ul>"));
          }
        oRows = null;
      }
      dNodos = null;
    }

    void oButton_Click(object sender, EventArgs e)
    {
      oIsUsuario = oWeb.GetObjUsuario();
      Session["CodContenido"] = string.Empty;
      Session["CodNodo"] = (sender as LinkButton).Attributes["CodNodo"].ToString();
      if ((sender as LinkButton).Attributes["IndDesplUsrClient"].ToString() != "V")
      {
        if ((sender as LinkButton).Attributes["IndPerfil"].ToString() == "V")
          Session["CodUsuarioPerfil"] = oIsUsuario.CodUsuario;
        else
          Session["CodUsuarioPerfil"] = string.Empty;
      }
      if (string.IsNullOrEmpty(sRuta))
        Page.Response.Redirect(".");
      else
        Page.Response.Redirect("../default.aspx");
    }
  }
}