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

using OnlineServices.Conn;
using OnlineServices.SystemData;
using OnlineServices.Method;

namespace ICommunity
{
  public partial class Login : System.Web.UI.Page
  {
    Culture oCulture = new Culture();
    Web oWeb = new Web();
    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        lLogin .Text = oCulture.GetResource("LoginUsers", "lblLogin");
        lPassword.Text = oCulture.GetResource("LoginUsers", "txtPassword");
        btnAceptar.Text = oCulture.GetResource("Global", "btnAceptar");
      }
    }

    protected void btnAceptar_Click(object sender, EventArgs e)
    {
      OnlineServices.Method.Usuario oIsUsuario;
      string sMsnLogin = string.Empty;
      bool dExito = false;
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        SysUsuario oUsuario = new SysUsuario(ref oConn);
        oUsuario.LoginUsuario = txtLogin.Text;
        oUsuario.PwdUsuario = oWeb.Crypt(txtPassword.Text);
        oUsuario.EstUsuario = "V";
        DataTable dUsuario = oUsuario.Get();

        if (dUsuario != null)
          if (dUsuario != null)
            if (dUsuario.Rows.Count > 0)
            {

              oIsUsuario = oWeb.GetObjUsuario();
              oIsUsuario.CodUsuario = dUsuario.Rows[0]["cod_usuario"].ToString();
              oIsUsuario.Tipo = dUsuario.Rows[0]["cod_tipo"].ToString();
              oIsUsuario.Nombres = (dUsuario.Rows[0]["nom_usuario"].ToString() + " " + dUsuario.Rows[0]["ape_usuario"].ToString()).Trim();
              oIsUsuario.Email = dUsuario.Rows[0]["eml_usuario"].ToString();
              oIsUsuario.Fono = dUsuario.Rows[0]["fono_usuario"].ToString();
              Session["USUARIO"] = oIsUsuario;

              SyrPerfilesUsuarios oPerfilesUsuarios = new SyrPerfilesUsuarios(ref oConn);
              oPerfilesUsuarios.CodUsuario = dUsuario.Rows[0]["cod_usuario"].ToString();
              DataTable dPerfilesUsuarios = oPerfilesUsuarios.Get();
              if (dPerfilesUsuarios != null)
                if (dPerfilesUsuarios.Rows.Count > 0)
                {
                  dExito = true;
                  Session["Administrador"] = "1";
                }else
                  sMsnLogin = oCulture.GetResource("LoginUsers", "MsnLoggin02");
              dPerfilesUsuarios = null;
            }
            else {
              sMsnLogin = oCulture.GetResource("LoginUsers", "MsnLoggin01");
            }
        dUsuario = null;
        oConn.Close();
      }else
        sMsnLogin = oCulture.GetResource("LoginUsers", "MsnLoggin03");

      if (!dExito)
      {
        StringBuilder js = new StringBuilder();
        js.Append("function LgRespuesta() {");
        js.Append(" window.radalert('").Append(sMsnLogin).Append("', 200, 100,'Atención'); ");
        js.Append(" Sys.Application.remove_load(LgRespuesta); ");
        js.Append("};");
        js.Append("Sys.Application.add_load(LgRespuesta);");
        Page.ClientScript.RegisterStartupScript(this.GetType(), "LgRespuesta", js.ToString(), true);
      }
      else
        Response.Redirect("framework.aspx");

    }

  }
}
