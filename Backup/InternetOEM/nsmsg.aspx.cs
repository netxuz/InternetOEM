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
  public partial class nsmsg : System.Web.UI.Page
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
      //HttpCookie oHttpCookie;
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
        DBConn oConn = new DBConn();
        if (oConn.Open())
        {
          oConn.BeginTransaction();

          SysUsuario oUsuario = new SysUsuario(ref oConn);
          oUsuario.CodUsuario = oIsUsuario.CodUsuario;
          oUsuario.NotEtarget = "1";
          oUsuario.Put();
          if (string.IsNullOrEmpty(oUsuario.Error))
          {
            oConn.Commit();
            string cPath = Server.MapPath(".") + @"\binary\";
            string sFile = "Usuarios.bin";
            oUsuario.SerializaTblUsuario(ref oConn, cPath, sFile);
            string sFileUsr = "Usuario_" + oIsUsuario.CodUsuario + ".bin";
            oUsuario.SerializaUsuario(ref oConn, cPath, sFileUsr);

            StringBuilder js = new StringBuilder();
            js.Append("function LgRespuesta() {");
            js.Append(" window.radalert('Su solicitud de no recibir más correos ha sido actualizada.', 200, 100,'Atención'); ");
            js.Append(" Sys.Application.remove_load(LgRespuesta); ");
            js.Append("};");
            js.Append("Sys.Application.add_load(LgRespuesta);");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "LgRespuesta", js.ToString(), true);
            masterboard.Visible = false;
          }

          oConn.Close();
        }

      }
    }

  }
}
