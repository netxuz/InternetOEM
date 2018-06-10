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

namespace ICommunity.Controls
{
  public partial class RestarPassword : System.Web.UI.UserControl
  {
    Culture oCulture = new Culture();
    Web oWeb = new Web();
    OnlineServices.Method.Usuario oIsUsuario;
    
    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        lblmsnrstpwd.Text = oCulture.GetResource("Usuario", "TitRestPwd");
        lblmsgreingnewpwd.Text = oCulture.GetResource("Usuario", "MsgIngNewPwd");
        lblRstPwd01.Text = oCulture.GetResource("Usuario", "NuevaClaveUsuario");
        lblRstPwd02.Text = oCulture.GetResource("Usuario", "RepNuevaClaveUsuario");
        btnRstPpwd.Text = oCulture.GetResource("Global", "btnAceptar");
        btnAceptar.Text = oCulture.GetResource("Global", "btnAceptar");
      }
    }

    protected void btnRstPpwd_Click(object sender, EventArgs e)
    {
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        oConn.BeginTransaction();
        string pCodUsuario = Session["USRCHANGEPWD"].ToString();
        string sClave = oWeb.Crypt(txtRstPwd01.Text);
        SysUsuario oUsuario = new SysUsuario(ref oConn);
        oUsuario.CodUsuario = pCodUsuario;
        oUsuario.PwdUsuario = sClave;
        oUsuario.Accion = "EDITAR";
        oUsuario.Put();
        if (string.IsNullOrEmpty(oUsuario.Error)) {
          string cPath = Server.MapPath(".") + @"\binary\";
          oUsuario.SerializaTblUsuario(ref oConn, cPath, "Usuarios.bin");
          string sFileUsr = "Usuario_" + pCodUsuario + ".bin";
          oUsuario.SerializaUsuario(ref oConn, cPath, sFileUsr);

          oConn.Commit();

          context_rstpwd.Visible = false;
          context_olvpwd_resp.Visible = true;

          DataTable dUsuario = oUsuario.Get();
          if (dUsuario.Rows.Count > 0)
          {
            oIsUsuario = oWeb.GetObjUsuario();
            oIsUsuario.CodUsuario = pCodUsuario;
            oIsUsuario.Tipo = dUsuario.Rows[0]["cod_tipo"].ToString();
            oIsUsuario.Nombres = dUsuario.Rows[0]["nom_usuario"].ToString() + " " + dUsuario.Rows[0]["ape_usuario"].ToString();
            oIsUsuario.Email = dUsuario.Rows[0]["eml_usuario"].ToString();
            oIsUsuario.Fono = dUsuario.Rows[0]["fono_usuario"].ToString();
            Session["USUARIO"] = oIsUsuario;
          }
          dUsuario = null;

          Session["USRCHANGEPWD"] = string.Empty;
          lblmsnrstpwd_resp.Text = oCulture.GetResource("Usuario", "MsnRstPwdResp");
        }
        oConn.Close();
      }

    }

    protected void btnAceptar_Click(object sender, EventArgs e)
    {
      DataRow[] oRow;
      oIsUsuario = oWeb.GetObjUsuario();
      Session["CodUsuarioPerfil"] = oIsUsuario.CodUsuario;

      DataTable oNodos = oWeb.DeserializarTbl(Server.MapPath("."), "Nodos.bin");
      if (oNodos != null)
        if (oNodos.Rows.Count > 0)
        {
          oRow = oNodos.Select(" pf_nodo = 'V' ");
          if (oRow != null)
            if (oRow.Count() > 0)
              Session["CodNodo"] = oRow[0]["cod_nodo"].ToString();
          oRow = null;
        }
      oNodos = null;

      Page.Response.Redirect(".");
    }
  }
}