using System;
using System.Text;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using OnlineServices.SystemData;
using OnlineServices.Method;

namespace ICommunity.Controls
{
  public partial class ImgCertificado : System.Web.UI.UserControl
  {
    private string pCodUsuario;
    public string CodUsuario { get { return pCodUsuario; } set { pCodUsuario = value; } }
    private OnlineServices.Method.Culture oCulture = new OnlineServices.Method.Culture();
    private OnlineServices.Method.Web oWeb = new OnlineServices.Method.Web();
    private OnlineServices.Method.Usuario oIsUsuario;
    private BinaryUsuario oBinaryUsuario;
    protected void Page_Load(object sender, EventArgs e)
    {

      if ((Session["CodUsuarioPerfil"] != null) && (!string.IsNullOrEmpty(Session["CodUsuarioPerfil"].ToString())))
        putCertificado(Session["CodUsuarioPerfil"].ToString());
      else if (!string.IsNullOrEmpty(pCodUsuario))
        putCertificado(pCodUsuario);
      
    }

    protected void putCertificado(string CodUsr) {
      StringBuilder sText = new StringBuilder();
      SysUsuario oUsuario = new SysUsuario();
      oUsuario.Path = Server.MapPath(".").ToString();
      oUsuario.CodUsuario = CodUsr;
      oBinaryUsuario = oUsuario.ClassGet();

      if (oBinaryUsuario != null)
      {
        if (oBinaryUsuario.CodTipo != "1")
          if ((!string.IsNullOrEmpty(oBinaryUsuario.IndValidado)) && (oBinaryUsuario.IndValidado == "V"))
            sText.Append("<div class=\"ImgCertificado\"><img id=\"ImgCertificada\" src=\"style/images/Certificada.png\" border=\"0\"></div>");
          else
            sText.Append("<div class=\"ImgCertificado\">").Append(oCulture.GetResource("Usuario", "Certificado")).Append("</div>");
      }
      oBinaryUsuario = null;
      Controls.Add(new LiteralControl(sText.ToString()));
    }
  }
}