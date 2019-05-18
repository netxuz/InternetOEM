using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

using OnlineServices.Conn;
using OnlineServices.SystemData;
using OnlineServices.Method;

namespace ICommunity.Reporting
{
  public partial class Login : System.Web.UI.Page
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
        btnAceptar.Text = oCulture.GetResource("Global", "btnAceptar");
      }
    }

    protected void btnAceptar_Click(object sender, EventArgs e)
    {
      DateTime dNow = DateTime.Now;
      StringBuilder oFolder = new StringBuilder();
      oFolder.Append(Server.MapPath("..\\"));
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
        DataRow[] oRow = dUsuario.Select(" login_user = '" + sLogin + "' and pwd_user = '" + oWeb.Crypt(sPwd) + "' and est_user = 'V' ");
        if (oRow != null)
          if (oRow.Count() > 0)
          {
            oIsUsuario = oWeb.GetObjUsuario();
            oIsUsuario.CodUsuario = oRow[0]["cod_user"].ToString();
            oIsUsuario.Tipo = oRow[0]["cod_tipo"].ToString();
            oIsUsuario.Nombres = (oRow[0]["nom_user"].ToString() + " " + oRow[0]["ape_user"].ToString()).Trim();
            oIsUsuario.Email = oRow[0]["eml_user"].ToString();
            oIsUsuario.Fono = oRow[0]["fono_usuario"].ToString();
            oIsUsuario.CodNkey = oRow[0]["nkey_user"].ToString();
            oIsUsuario.TipoUsuario = oRow[0]["tipo_usuario"].ToString();
            oIsUsuario.NKeyUsuario = oRow[0]["nkey_usuario"].ToString();           

            Session["USUARIO"] = oIsUsuario;


            dExito = true;
            Log oLog = new Log();
            oLog.IdUsuario = oRow[0]["cod_user"].ToString(); ;
            oLog.ObsLog = "USUARIO LOGEADO CORRECTAMENTE";
            oLog.CodEvtLog = "0";
            oLog.AppLog = "LOGIN";
            oLog.putLog();

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
        Response.Redirect("default.aspx");
      }
    }
  }
}