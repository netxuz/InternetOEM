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
using Telerik.Web.UI;

namespace ICommunity.Controls
{
  public partial class EmailToUser : System.Web.UI.UserControl
  {
    Web oWeb = new Web();
    private OnlineServices.Method.Usuario oIsUsuario;
    private OnlineServices.Method.Culture oCulture = new OnlineServices.Method.Culture();

    protected void Page_Load(object sender, EventArgs e)
    {
      oIsUsuario = oWeb.GetObjUsuario();
      if (int.Parse(oIsUsuario.Tipo) == 1)
      {
        bloquemensaje.Visible = true;
        bloquehistoria.Visible = true;
        bloqueusuarios.Visible = false;
        getHistoryEmails(Session["CodUsuarioPerfil"].ToString());
      }
      else
      {
        bloquemensaje.Visible = false;
        bloquehistoria.Visible = false;
        bloqueusuarios.Visible = true;
        getMessageUser();
      }
    }

    protected void getMessageUser()
    {
      Controls.Add(new LiteralControl("<div class=\"mdlMisComentarios\">"));
      Label oLabel;
      StringBuilder sSQL = new StringBuilder();
      sSQL.Append(" cod_usuario = ").Append(oIsUsuario.CodUsuario).Append(" and est_email = 'V' ");
      RadBinaryImage oImage;
      DataTable dEmlUsr = oWeb.DeserializarTbl(Server.MapPath("."), "EmailToUser.bin");
      if (dEmlUsr != null)
      {
        if (dEmlUsr.Rows.Count > 0)
        {
          DataRow[] oRows = dEmlUsr.Select(sSQL.ToString());
          if (oRows != null)
            if (oRows.Count() > 0)
            {
              foreach (DataRow oRow in oRows)
              {
                Controls.Add(new LiteralControl("<div class=\"bloquePorUsuario\">"));
                /*******************************************************************/
                Controls.Add(new LiteralControl("<div class=\"imgComent\">"));
                oImage = new RadBinaryImage();
                oImage.DataValue = oWeb.getImageProfileUser(oRow["cod_usr_send_email"].ToString(), 62, 62);
                Controls.Add(oImage);
                Controls.Add(new LiteralControl("</div>"));
                /*******************************************************************/
                Controls.Add(new LiteralControl("<div class=\"bloqueUsuarioComent\">"));
                /*******************************************************************/
                Controls.Add(new LiteralControl("<div class=\"bNombreComment\">"));
                BinaryUsuario dUser;
                SysUsuario oUsuario = new SysUsuario();
                oUsuario.Path = Server.MapPath(".");
                oUsuario.CodUsuario = oRow["cod_usr_send_email"].ToString();
                dUser = oUsuario.ClassGet();
                oLabel = new Label();
                oLabel.ID = "lblNomUsuario_" + oRow["cod_email"].ToString();
                oLabel.Text = dUser.NomUsuario + " " + dUser.ApeUsuario;
                oLabel.CssClass = "lblNomUsuarioComment";
                dUser = null;
                Controls.Add(oLabel);
                Controls.Add(new LiteralControl("</div>"));
                /*******************************************************************/
                /*******************************************************************/
                Controls.Add(new LiteralControl("<div class=\"fechaComent\">"));
                oLabel = new Label();
                oLabel.ID = "lblFecha_" + oRow["cod_email"].ToString();
                oLabel.Text = oCulture.GetResource("Global", "Comentado") + " " + String.Format("{0:f}", DateTime.Parse(oRow["fecha_email"].ToString()));
                oLabel.CssClass = "lblFechaComment";
                Controls.Add(oLabel);
                Controls.Add(new LiteralControl("</div>"));
                /*******************************************************************/
                /*******************************************************************/
                Controls.Add(new LiteralControl("<div class=\"bloqueComent\">"));
                oLabel = new Label();
                oLabel.ID = "lblComment_" + oRow["cod_email"].ToString();
                oLabel.Text = oRow["cuerpo_email"].ToString();
                oLabel.CssClass = "lblComment";
                Controls.Add(oLabel);
                Controls.Add(new LiteralControl("</div>"));
                /*******************************************************************/
                /*******************************************************************/
                Controls.Add(new LiteralControl("</div>"));
                /*******************************************************************/
                //getEmailRel(oRow["cod_email"].ToString(), oRow["cod_usuario"].ToString(), oRow["fecha_email"].ToString(), dEmlUsr);
                /*******************************************************************/
                Controls.Add(new LiteralControl("</div>"));
              }
            }

          Controls.Add(new LiteralControl("<div>"));
          Button oButton = new Button();
          oButton.ID = "IDComente";
          oButton.Text = "Comente";
          oButton.OnClientClick = String.Format("goComenta('{0}'); return false;", "IDComente");
          Controls.Add(oButton);
          Controls.Add(new LiteralControl("</div>"));
          oRows = null;
        }
      }
      dEmlUsr = null;
      Controls.Add(new LiteralControl("</div>"));
    }

    protected void getHistoryEmails(string lngCodUsuario)
    {
      Controls.Add(new LiteralControl("<div class=\"mdlMisComentarios\">"));
      Label oLabel;
      StringBuilder sSQL = new StringBuilder();
      sSQL.Append(" cod_usuario = ").Append(lngCodUsuario).Append(" and cod_usr_send_email = ").Append(oIsUsuario.CodUsuario);
      RadBinaryImage oImage;
      DataTable dEmlUsr = oWeb.DeserializarTbl(Server.MapPath("."), "EmailToUser.bin");
      if (dEmlUsr != null)
      {
        if (dEmlUsr.Rows.Count > 0)
        {
          DataRow[] oRows = dEmlUsr.Select(sSQL.ToString(), "fecha_email desc");
          if (oRows != null)
            if (oRows.Count() > 0)
            {
              foreach (DataRow oRow in oRows)
              {
                Controls.Add(new LiteralControl("<div class=\"bloquePorUsuario\">"));
                /*******************************************************************/
                Controls.Add(new LiteralControl("<div class=\"imgComent\">"));
                oImage = new RadBinaryImage();
                oImage.DataValue = oWeb.getImageProfileUser(oRow["cod_usr_send_email"].ToString(), 62, 62);
                Controls.Add(oImage);
                Controls.Add(new LiteralControl("</div>"));
                /*******************************************************************/
                Controls.Add(new LiteralControl("<div class=\"bloqueUsuarioComent\">"));
                /*******************************************************************/
                Controls.Add(new LiteralControl("<div class=\"bNombreComment\">"));
                BinaryUsuario dUser;
                SysUsuario oUsuario = new SysUsuario();
                oUsuario.Path = Server.MapPath(".");
                oUsuario.CodUsuario = oRow["cod_usr_send_email"].ToString();
                dUser = oUsuario.ClassGet();
                oLabel = new Label();
                oLabel.ID = "lblNomUsuario_" + oRow["cod_email"].ToString();
                oLabel.Text = dUser.NomUsuario + " " + dUser.ApeUsuario;
                oLabel.CssClass = "lblNomUsuarioComment";
                dUser = null;
                Controls.Add(oLabel);
                Controls.Add(new LiteralControl("</div>"));
                /*******************************************************************/
                /*******************************************************************/
                Controls.Add(new LiteralControl("<div class=\"fechaComent\">"));
                oLabel = new Label();
                oLabel.ID = "lblFecha_" + oRow["cod_email"].ToString();
                oLabel.Text = oCulture.GetResource("Global", "Comentado") + " " + String.Format("{0:f}", DateTime.Parse(oRow["fecha_email"].ToString()));
                oLabel.CssClass = "lblFechaComment";
                Controls.Add(oLabel);
                Controls.Add(new LiteralControl("</div>"));
                /*******************************************************************/
                /*******************************************************************/
                Controls.Add(new LiteralControl("<div class=\"bloqueComent\">"));
                oLabel = new Label();
                oLabel.ID = "lblComment_" + oRow["cod_email"].ToString();
                oLabel.Text = oRow["cuerpo_email"].ToString();
                oLabel.CssClass = "lblComment";
                Controls.Add(oLabel);
                Controls.Add(new LiteralControl("</div>"));
                /*******************************************************************/
                /*******************************************************************/
                Controls.Add(new LiteralControl("</div>"));
                /*******************************************************************/
                getEmailRel(oRow["cod_email"].ToString(), oRow["cod_usuario"].ToString(), oRow["fecha_email"].ToString(), dEmlUsr);
                /*******************************************************************/
                Controls.Add(new LiteralControl("</div>"));
              }
            }
          oRows = null;
        }
      }
      dEmlUsr = null;
      Controls.Add(new LiteralControl("<div>"));
    }

    protected void btnAceptar_Click(object sender, EventArgs e)
    {
      try
      {
        DBConn oConn = new DBConn();
        if (oConn.Open())
        {
          oConn.BeginTransaction();
          string pCodEmailRel = "0";
          if (((sender as Button).Attributes["odEmailRel"] != null) && (!string.IsNullOrEmpty((sender as Button).Attributes["odEmailRel"].ToString())))
            pCodEmailRel = (sender as Button).Attributes["odEmailRel"].ToString();

          SysEmailToUser oEmailToUser = new SysEmailToUser(ref oConn);
          oEmailToUser.Accion = "CREAR";
          oEmailToUser.CodUsuario = Session["CodUsuarioPerfil"].ToString();
          oEmailToUser.CodUsrSendEmail = oIsUsuario.CodUsuario;
          oEmailToUser.CuerpoEmail = txtCuerpo.Text;
          oEmailToUser.CodEmailRel = pCodEmailRel;
          oEmailToUser.FechaEmail = DateTime.Now.ToString();
          oEmailToUser.EstEmail = "V";
          oEmailToUser.Put();
          if (string.IsNullOrEmpty(oEmailToUser.Error))
          {
            oConn.Commit();
            oEmailToUser.Serializa(ref oConn, Server.MapPath(".") + @"\binary\", "EmailToUser.bin");
          }
          else
            oConn.Rollback();

          oConn.Close();
        }
      }
      catch
      {

      }
      Response.Redirect(".");
    }

    protected void getEmailRel(string pCodEmailRel, string pCodUsuario, string pDateEmail, DataTable dEmlUsr)
    {
      RadBinaryImage oImage;
      Label oLabel;
      StringBuilder sSQL = new StringBuilder();
      sSQL.Append(" cod_email_rel = ").Append(pCodEmailRel);
      DataRow[] oRowsEmailRel = dEmlUsr.Select(sSQL.ToString());
      if (oRowsEmailRel != null)
        if (oRowsEmailRel.Count() > 0)
        {
          foreach (DataRow oRow in oRowsEmailRel)
          {
            Controls.Add(new LiteralControl("<div>"));
            oImage = new RadBinaryImage();
            oImage.DataValue = oWeb.getImageProfileUser(oRow["cod_usuario"].ToString(), 62, 62);
            Controls.Add(oImage);
            Controls.Add(new LiteralControl("</div>"));

            Controls.Add(new LiteralControl("<div>"));
            oLabel = new Label();
            oLabel.ID = "lblComment_" + oRow["cod_email"].ToString();
            oLabel.Text = oRow["cuerpo_email"].ToString();
            Controls.Add(oLabel);
            Controls.Add(new LiteralControl("</div>"));

            Controls.Add(new LiteralControl("<div>"));
            oLabel = new Label();
            oLabel.ID = "lblFecha_" + oRow["cod_email"].ToString();
            oLabel.Text = oCulture.GetResource("Global", "Comentado") + " " + String.Format("{0:f}", DateTime.Parse(oRow["fecha_email"].ToString()));
            Controls.Add(oLabel);
            Controls.Add(new LiteralControl("</div>"));

            getEmailRel(oRow["cod_email"].ToString(), oRow["cod_usuario"].ToString(), oRow["fecha_email"].ToString(), dEmlUsr);
          }
        }
      oRowsEmailRel = null;
    }
  }
}