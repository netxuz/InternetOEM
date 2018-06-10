using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using OnlineServices.Method;
using OnlineServices.SystemData;

namespace ICommunity.Controls
{
  public partial class MenuRecursivoDeck : System.Web.UI.UserControl
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
      string cPath = Server.MapPath(".");
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

        if ((Session["USUARIO"] == null) || (string.IsNullOrEmpty(Session["USUARIO"].ToString())))
          sQueryFilter.Append(" and prv_nodo = 0 ");

        DataRow[] oRows = dNodos.Select(sQueryFilter.ToString(), " ord_nodo asc ");
        if (oRows != null)
          if (oRows.Count() > 0)
          {
            bool bAllow;
            //Button oButton;
            //LinkButton oButton;

            foreach (DataRow oRow in oRows)
            {
              bAllow = true;
              if ((oRow["ind_despl_usr_client"].ToString() == "V") || (oRow["ind_despl_usr_site"].ToString() == "V"))
              {
                StringBuilder oFolder = new StringBuilder();
                oFolder.Append(Server.MapPath("."));

                SysUsuario oUsuario = new SysUsuario();
                oUsuario.Path = oFolder.ToString();
                oUsuario.CodUsuario = Session["CodUsuarioPerfil"].ToString();
                oBinaryUsuario = oUsuario.ClassGet();
                if (oBinaryUsuario != null)
                {
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
                getNodosHijo(dNodos, oRow["cod_nodo"].ToString(), oRow);
              }
            }

          }
        oRows = null;
      }
      dNodos = null;
    }

    protected void getNodosHijo(DataTable dNodos, string CodNodo, DataRow oRow)
    {
      LinkButton oButton;
      StringBuilder sQueryFilter = new StringBuilder();
      sQueryFilter.Append(" cod_nodo_rel = ").Append(CodNodo);
      DataRow[] oRows = dNodos.Select(sQueryFilter.ToString(), " ord_nodo asc ");
      if (oRows != null)
        if (oRows.Count() > 0)
        {
          cdprimarynav.Controls.Add(new LiteralControl("<li class=\"cd-nav__top-li__doubleCol\">"));

          oButton = new LinkButton();
          oButton.ID = "btnNav_" + oRow["cod_nodo"].ToString();
          oButton.Text = oRow["titulo_nodo"].ToString();
          oButton.ResolveUrl("javascript:void(0)");
          cdprimarynav.Controls.Add(oButton);
          cdprimarynav.Controls.Add(new LiteralControl("<ul class=\"cd-nav__secondary-ul\">"));


          //oButton = new LinkButton();
          //oButton.ID = "btnNav_" + oRow["cod_nodo"].ToString();
          //oButton.Text = oRow["titulo_nodo"].ToString();
          //oButton.Attributes["CodNodo"] = oRow["cod_nodo"].ToString();
          //oButton.Attributes["IndPerfil"] = oRow["pf_nodo"].ToString();
          //oButton.Attributes["IndDesplUsrClient"] = oRow["ind_despl_usr_client"].ToString();
          //oButton.CausesValidation = false;
          //oButton.Click += new EventHandler(OButton_Click);
          //Controls.Add(oButton);

          cdprimarynav.Controls.Add(new LiteralControl("<li class=\"cd-nav__tab-items\">"));
          int i = 1;
          foreach (DataRow oFila in oRows)
          {
            

            oButton = new LinkButton();
            oButton.ID = "btnNav_items_" + oFila["cod_nodo"].ToString();
            oButton.Text = oFila["titulo_nodo"].ToString();
            oButton.CssClass = "cd-nav__tab-item--" + i;
            cdprimarynav.Controls.Add(oButton);
            i++;
          }
          cdprimarynav.Controls.Add(new LiteralControl("</li>"));
          cdprimarynav.Controls.Add(new LiteralControl("<li class=\"divider-vertical\">&nbsp;</li>"));

          i = 1;
          foreach (DataRow oFila in oRows) {

            cdprimarynav.Controls.Add(new LiteralControl("<li class=\"cd-nav__top-li__li cd-nav__tab-item--" + i + "\">"));

            oButton = new LinkButton();
            oButton.ID = "btnNav_item" + oFila["cod_nodo"].ToString();
            oButton.Text = oFila["titulo_nodo"].ToString();
            oButton.CssClass = "cd-nav__top-li__li__header";
            oButton.ResolveUrl("javascript:void(0)");
            cdprimarynav.Controls.Add(oButton);
            cdprimarynav.Controls.Add(new LiteralControl("<ul class=\"cd-nav__ul-custom\">"));
            cdprimarynav.Controls.Add(new LiteralControl("<li class=\"go-back\"><a href=\"javascript: void(0)\">Volver</a></li>"));

            if (!string.IsNullOrEmpty(oFila["texto_nodo"].ToString())) {
              cdprimarynav.Controls.Add(new LiteralControl("<li>"));
              cdprimarynav.Controls.Add(new LiteralControl("<div class=\"cd-nav__intro\">"));
              cdprimarynav.Controls.Add(new LiteralControl("<h4>" + oFila["titulo_nodo"].ToString() + "</h4>"));
              cdprimarynav.Controls.Add(new LiteralControl("<p>" + oFila["texto_nodo"].ToString() + "</p>"));
              cdprimarynav.Controls.Add(new LiteralControl("</div>"));
              cdprimarynav.Controls.Add(new LiteralControl("</li>"));
            }

            getNodosSubHijo(dNodos, oFila["cod_nodo"].ToString(), oFila);

            cdprimarynav.Controls.Add(new LiteralControl("</ul>"));
            cdprimarynav.Controls.Add(new LiteralControl("</li>"));
            
            i++;
          }

          cdprimarynav.Controls.Add(new LiteralControl("</ul>"));
          cdprimarynav.Controls.Add(new LiteralControl("</li>"));
        }
        else
        {
          cdprimarynav.Controls.Add(new LiteralControl("<li class=\"cd-nav__top-li__solo\">"));
          oButton = new LinkButton();
          oButton.ID = "btnNav_" + oRow["cod_nodo"].ToString();
          oButton.Text = oRow["titulo_nodo"].ToString();
          oButton.Attributes["CodNodo"] = oRow["cod_nodo"].ToString();
          oButton.Attributes["IndPerfil"] = oRow["pf_nodo"].ToString();
          oButton.Attributes["IndDesplUsrClient"] = oRow["ind_despl_usr_client"].ToString();
          oButton.CausesValidation = false;
          oButton.Click += new EventHandler(OButton_Click);
          cdprimarynav.Controls.Add(oButton);

          cdprimarynav.Controls.Add(new LiteralControl("</li>"));
        }
      oRows = null;
    }

    protected void getNodosSubHijo(DataTable dNodos, string CodNodo, DataRow oRow)
    {
      LinkButton oButton;
      StringBuilder sQueryFilter = new StringBuilder();
      sQueryFilter.Append(" cod_nodo_rel = ").Append(CodNodo);
      DataRow[] oRows = dNodos.Select(sQueryFilter.ToString(), " ord_nodo asc ");
      if (oRows != null)
        if (oRows.Count() > 0)
        {
          foreach (DataRow oFila in oRows)
          {
            cdprimarynav.Controls.Add(new LiteralControl("<li>"));
            oButton = new LinkButton();
            oButton.ID = "btnNav_sub" + oFila["cod_nodo"].ToString();
            oButton.Text = oFila["titulo_nodo"].ToString();
            oButton.ResolveUrl("javascript:void(0)");
            cdprimarynav.Controls.Add(oButton);
            cdprimarynav.Controls.Add(new LiteralControl("</li>"));
          }
        }
        else
        {
          cdprimarynav.Controls.Add(new LiteralControl("<li>"));
          oButton = new LinkButton();
          oButton.ID = "btnNav_sub" + oRow["cod_nodo"].ToString();
          oButton.Text = oRow["titulo_nodo"].ToString();
          oButton.Attributes["CodNodo"] = oRow["cod_nodo"].ToString();
          oButton.Attributes["IndPerfil"] = oRow["pf_nodo"].ToString();
          oButton.Attributes["IndDesplUsrClient"] = oRow["ind_despl_usr_client"].ToString();
          oButton.CausesValidation = false;
          oButton.Click += new EventHandler(OButton_Click);
          cdprimarynav.Controls.Add(oButton);

          cdprimarynav.Controls.Add(new LiteralControl("</li>"));
        }
      oRows = null;
    }

    private void OButton_Click(object sender, EventArgs e)
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