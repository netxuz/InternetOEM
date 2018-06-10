using System;
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
using System.Text;

using OnlineServices.Conn;
using OnlineServices.SystemData;
using OnlineServices.Method;

namespace ICommunity.Controls
{
  public partial class LoginUsers : System.Web.UI.UserControl
  {
    Culture oCulture = new Culture();
    Web oWeb = new Web();
    OnlineServices.Method.Usuario oIsUsuario;

    protected void Page_Load(object sender, EventArgs e)
    {
      oIsUsuario = oWeb.GetObjUsuario();
      if (!IsPostBack)
      {
        lblLogin.Text = oCulture.GetResource("LoginUsers", "lblLogin");
        lblPassword.Text = oCulture.GetResource("LoginUsers", "txtPassword");
        chkRememberLogin.Text = oCulture.GetResource("LoginUsers", "lblRememberLogin");
        lnkOlvidoPass.Text = oCulture.GetResource("LoginUsers", "lblOlvPassword");
        btnAceptar.Text = oCulture.GetResource("Global", "btnAceptar");
      }

      if (oIsUsuario.CodUsuario != null)
        if (oIsUsuario.CodUsuario != string.Empty)
          idCntrUsrLogin.Visible = false;

    }

    protected void btnAceptar_Click(object sender, EventArgs e)
    {
      HttpCookie oHttpCookie;
      DateTime dNow = DateTime.Now;
      StringBuilder oFolder = new StringBuilder();
      oFolder.Append(Server.MapPath("."));
      string sLogin = txtLogin.Text;
      string sPwd = txtPassword.Text;
      string sComilla = Convert.ToChar(39).ToString();
      string sComillaDoble = Convert.ToChar(39).ToString() + Convert.ToChar(39).ToString();

      sLogin = sLogin.Replace(sComilla, sComillaDoble);
      sPwd = sPwd.Replace(sComilla, sComillaDoble);

      bool dExito = false;
      DataTable dUsuario = oWeb.DeserializarTbl(oFolder.ToString(), "Usuarios.bin");
      if (dUsuario != null)
      {
        DataRow[] oRow = dUsuario.Select(" login_usuario = '" + sLogin + "' and pwd_usuario = '" + oWeb.Crypt(sPwd) + "' and est_usuario = 'V' ");
        if (oRow != null)
          if (oRow.Count() > 0)
          {
            oIsUsuario = oWeb.GetObjUsuario();
            oIsUsuario.CodUsuario = oRow[0]["cod_usuario"].ToString();
            oIsUsuario.Tipo = oRow[0]["cod_tipo"].ToString();
            oIsUsuario.Nombres = (oRow[0]["nom_usuario"].ToString() + " " + oRow[0]["ape_usuario"].ToString()).Trim();
            oIsUsuario.Email = oRow[0]["eml_usuario"].ToString();
            oIsUsuario.Fono = oRow[0]["fono_usuario"].ToString();

            Session["USUARIO"] = oIsUsuario;
            dExito = true;
            idCntrUsrLogin.Visible = false;
            if (chkRememberLogin.Checked)
            {
              oHttpCookie = new HttpCookie("AccesCookie");
              oHttpCookie.Value = oWeb.Crypt(oIsUsuario.CodUsuario);
              oHttpCookie.Expires = dNow.AddYears(100);
              Response.Cookies.Add(oHttpCookie);
            }
            else
            {
              oHttpCookie = new HttpCookie("AccesCookie");
              oHttpCookie.Value = string.Empty;
              oHttpCookie.Expires = dNow.AddYears(100);
              Response.Cookies.Add(oHttpCookie);
            }
          }
        oRow = null;
      }
      dUsuario = null;

      if (!dExito)
      {
        StringBuilder js = new StringBuilder();
        js.Append("function LgRespuesta() {");
        js.Append(" window.radalert('Login o Clave incorrecta, por favor vuelva a intentarlo.', 200, 100,'Atención'); ");
        js.Append(" Sys.Application.remove_load(LgRespuesta); ");
        js.Append("};");
        js.Append("Sys.Application.add_load(LgRespuesta);");
        Page.ClientScript.RegisterStartupScript(this.GetType(), "LgRespuesta", js.ToString(), true);
      }
      else
      {
        DataTable oParam = oWeb.DeserializarTbl(Server.MapPath("."), "parametros.bin");
        if (oParam != null)
          if (oParam.Rows.Count > 0)
          {
            DataRow[] oRows;
            oRows = oParam.Select(" cod_codigo = '8' and valor_parametro = '1' ");
            if (oRows != null)
              if (oRows.Count() > 0)
                putSessionNodo("inicio");
            oRows = null;
            oRows = oParam.Select(" cod_codigo = '12' and valor_parametro = '1' ");
            if (oRows != null)
              if (oRows.Count() > 0)
                putSessionNodo("perfil");
            oRows = null;
          }
        oParam = null;
        Response.Redirect(".");
      }
    }

    protected void lnkOlvidoPass_Click(object sender, EventArgs e)
    {
      DataRow[] oRow;
      DataTable oNodos = oWeb.DeserializarTbl(Server.MapPath("."), "Nodos.bin");
      if (oNodos != null)
        if (oNodos.Rows.Count > 0)
        {
          oRow = oNodos.Select(" cod_nodo_rel = 0 and est_nodo = 'O' and ind_olvclave_nodo = 'V' ");
          if (oRow != null)
            if (oRow.Count() > 0)
              Session["CodNodo"] = oRow[0]["cod_nodo"].ToString();
          oRow = null;
        }
      oNodos = null;
      Page.Response.Redirect(".");
    }

    protected void putSessionNodo(string indNodo) {

      string sTypeNodo = string.Empty;
      switch (indNodo){
        case "inicio":
          sTypeNodo = " ini_nodo = 'V' ";
          break;
        case "perfil":
          Session["CodUsuarioPerfil"] = oIsUsuario.CodUsuario;
          sTypeNodo = " pf_nodo = 'V' ";
          break;
      }

      DataTable oNodos = oWeb.DeserializarTbl(Server.MapPath("."), "Nodos.bin");
      if (oNodos != null)
        if (oNodos.Rows.Count > 0)
        {
          DataRow[] oNodRows = oNodos.Select(sTypeNodo);
          if (oNodRows != null)
            if (oNodRows.Count() > 0)
              Session["CodNodo"] = oNodRows[0]["cod_nodo"].ToString();
          oNodRows = null;
        }
      oNodos = null;
    }

  }
}