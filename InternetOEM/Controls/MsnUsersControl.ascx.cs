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
using System.IO;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using Telerik.Web.UI;
using OnlineServices.Conn;
using OnlineServices.CmsData;
using OnlineServices.SystemData;
using OnlineServices.Method;

namespace ICommunity.Controls
{
  public partial class MsnUsersControl : System.Web.UI.UserControl
  {
    private string pCodNodo = string.Empty;
    private string pCodUsuarioRel = string.Empty;
    private OnlineServices.Method.Web oWeb = new OnlineServices.Method.Web();
    private OnlineServices.Method.Log oLog = new OnlineServices.Method.Log();
    private OnlineServices.Method.Culture oCulture = new OnlineServices.Method.Culture();
    private OnlineServices.Method.Usuario oIsUsuario;

    protected void Page_Load(object sender, EventArgs e)
    {
      oIsUsuario = oWeb.GetObjUsuario();
      pCodNodo = Session["CodNodo"].ToString();
      if (((HttpContext.Current.Session["USUARIO"] != null) && (!string.IsNullOrEmpty(HttpContext.Current.Session["USUARIO"].ToString()))) || ((Session["CodUsuarioPerfil"] != null) && (!string.IsNullOrEmpty(Session["CodUsuarioPerfil"].ToString()))))
      {
        if (!string.IsNullOrEmpty(this.Attributes["Method"].ToString()))
        {
          switch (this.Attributes["Method"].ToString())
          {
            case "Comment":
              if ((!string.IsNullOrEmpty(oIsUsuario.Tipo)) && (int.Parse(oIsUsuario.Tipo) > 1))
                putCommentToUser();
              break;
            case "GetComments":
              getComments();
              break;
            case "CommentToUser":
              CommentToUser();
              break;
          }
        }
      }
    }

    protected void putCommentToUser()
    {
      Controls.Add(new LiteralControl("<div class=\"mdlComente\">"));
      //**** Label *****
      Controls.Add(new LiteralControl("<div class=\"labelComente\">"));
      Label oLabel = new Label();
      oLabel.Text = oCulture.GetResource("Comentarios", "Compartir");
      oLabel.CssClass = "lblComente";
      Controls.Add(oLabel);
      Controls.Add(new LiteralControl("</div>"));
      Controls.Add(new LiteralControl("<div class=\"txtComente\">"));
      //**** TextBox *****
      TextBox oTexBox = new TextBox();
      oTexBox.ID = "txtComent_" + oIsUsuario.CodUsuario;
      oTexBox.TextMode = TextBoxMode.MultiLine;
      oTexBox.CssClass = "CssMstComente";
      Controls.Add(oTexBox);
      //Controls.Add(new LiteralControl("</div>"));
      //**** RequiredFieldValidator *****

      RequiredFieldValidator oRequiredFieldValidator = new RequiredFieldValidator();
      oRequiredFieldValidator.ID = "valTxtComenta_" + oIsUsuario.CodUsuario;
      oRequiredFieldValidator.ControlToValidate = "txtComent_" + oIsUsuario.CodUsuario;
      oRequiredFieldValidator.Display = ValidatorDisplay.Static;
      oRequiredFieldValidator.ValidationGroup = "valTxtComenta";
      oRequiredFieldValidator.ErrorMessage = "*";
      Controls.Add(oRequiredFieldValidator);
      Controls.Add(new LiteralControl("</div>"));
      //**** Button *****
      Controls.Add(new LiteralControl("<div class=\"botonComente\">"));
      Button oButton = new Button();
      oButton.ID = "btnComent_" + oIsUsuario.CodUsuario;
      oButton.Text = oCulture.GetResource("Contenido", "btnAceptarComentario");
      oButton.ValidationGroup = "valTxtComenta";
      oButton.Attributes["CodContenidoRel"] = "";
      oButton.Click += new EventHandler(oButton_Click);
      oButton.CssClass = "btnComente";
      Controls.Add(oButton);
      Controls.Add(new LiteralControl("</div>"));

      Controls.Add(new LiteralControl("</div>"));
    }

    protected void getComments()
    {

      Label oLabel;
      Controls.Add(new LiteralControl("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"mdlMisComentarios\" width=\"100%\">"));
      RadBinaryImage oImage;
      DataTable dContenidoUsuario = oWeb.DeserializarTbl(Server.MapPath("."), "Contenidos.bin");
      if (dContenidoUsuario != null)
        if (dContenidoUsuario.Rows.Count > 0)
        {
          StringBuilder js = new StringBuilder();
          js.Append("function goComenta(idControl){");
          js.Append(" var oControl = document.getElementById(idControl); ");
          js.Append(" oControl.style.display = 'block'; ");
          js.Append("}; ");
          Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "goComenta", js.ToString(), true);

          StringBuilder sSQL = new StringBuilder();
          if (!string.IsNullOrEmpty(oIsUsuario.Tipo))
          {
            if (Session["CodUsuarioPerfil"].ToString() != oIsUsuario.CodUsuario)
            {
              if (int.Parse(oIsUsuario.Tipo) > 1)
              {
                sSQL.Append(" cod_usuario = ").Append(oIsUsuario.CodUsuario);
                sSQL.Append(" and cod_contenido_rel is null and est_contenido = 'P' ");
              }
              else
              {
                sSQL.Append(" cod_usuario = ").Append(Session["CodUsuarioPerfil"].ToString());
                sSQL.Append(" and cod_contenido_rel is null and est_contenido = 'P' ");
              }
            }
            else
            {
              if (int.Parse(oIsUsuario.Tipo) > 1)
              {
                sSQL.Append(" cod_usuario = ").Append(Session["CodUsuarioPerfil"].ToString());
                sSQL.Append(" and cod_contenido_rel is null and est_contenido = 'P' ");
              }
              else
              {
                string sCodUsrRel = string.Empty;
                StringBuilder sFile = new StringBuilder();
                sFile.Append("SeguirUsuariosF_").Append(oIsUsuario.CodUsuario).Append(".bin");
                DataTable dUsuarioRel = oWeb.DeserializarTbl(Server.MapPath("."), sFile.ToString());
                if (dUsuarioRel != null)
                  foreach (DataRow oRow in dUsuarioRel.Rows)
                  {
                    sCodUsrRel += (string.IsNullOrEmpty(sCodUsrRel) ? "" : ",");
                    sCodUsrRel += oRow["cod_seg_usuario"].ToString();
                  }
                dUsuarioRel = null;

                if (string.IsNullOrEmpty(sCodUsrRel))
                  sCodUsrRel = "null";

                sSQL.Append(" cod_usuario in(").Append(sCodUsrRel).Append(")");
                sSQL.Append(" and cod_contenido_rel is null and est_contenido = 'P' ");
              }
            }
          }
          else
          {
            sSQL.Append(" cod_usuario = ").Append(Session["CodUsuarioPerfil"].ToString());
            sSQL.Append(" and cod_contenido_rel is null and est_contenido = 'P' ");
          }
          DataRow[] oRows = dContenidoUsuario.Select(sSQL.ToString(), " date_contenido desc ");
          if (oRows != null)
            if (oRows.Count() > 0)
            {
              SysUsuario objUsuario;
              BinaryUsuario dUsuario;
              foreach (DataRow oRow in oRows)
              {
                objUsuario = new SysUsuario();
                objUsuario.Path = Server.MapPath(".");
                objUsuario.CodUsuario = oRow["cod_usuario"].ToString();
                dUsuario = objUsuario.ClassGet();
                if (dUsuario != null)
                  if (dUsuario.EstUsuario == "V")
                  {
                    Controls.Add(new LiteralControl("<tr><td class=\"MsnUsrBlq\" colspan=\"2\" align=\"top\">"));
                    Controls.Add(new LiteralControl("<div class=\"MsnUsrImgUsr\">"));

                    oImage = new RadBinaryImage();
                    oImage.CssClass = "ImageUsrTiny";
                    oImage.DataValue = oWeb.getImageProfileUser(oRow["cod_usuario"].ToString(), 300, 300);
                    oImage.Width = Unit.Pixel(52);
                    oImage.AutoAdjustImageControlSize = false;

                    LinkButton oLinkButton = new LinkButton();
                    oLinkButton.Width = Unit.Pixel(52);
                    oLinkButton.Attributes["CodUsuario"] = oRow["cod_usuario"].ToString();
                    oLinkButton.CssClass = "cTwittImgUserFollowMe";
                    oLinkButton.Click += new EventHandler(oLinkButton_Click);
                    oLinkButton.Controls.Add(oImage);
                    Controls.Add(oLinkButton);

                    Controls.Add(new LiteralControl("</div><div class=\"MsnUsrNomUsr\">"));
                    BinaryUsuario dUser;
                    SysUsuario oUsuario = new SysUsuario();
                    oUsuario.Path = Server.MapPath(".");
                    oUsuario.CodUsuario = oRow["cod_usuario"].ToString();
                    dUser = oUsuario.ClassGet();

                    LinkButton oLnkButton = new LinkButton();
                    oLnkButton.Text = dUser.NomUsuario + " " + dUser.ApeUsuario;
                    oLnkButton.Attributes["CodUsuario"] = oRow["cod_usuario"].ToString();
                    oLnkButton.CssClass = "lblNomUsuarioComment";
                    oLnkButton.Click += new EventHandler(oLinkButton_Click);
                    Controls.Add(oLnkButton);
                    dUser = null;
                    Controls.Add(new LiteralControl("</div>"));
                    Controls.Add(new LiteralControl("<div class=\"MsnUsrComentUsr\">"));
                    oLabel = new Label();
                    oLabel.ID = "lblComment_" + oRow["cod_contenido"].ToString();
                    oLabel.Text = oRow["texto_contenido"].ToString();
                    oLabel.CssClass = "lblComment";
                    Controls.Add(oLabel);
                    Controls.Add(new LiteralControl("</div><div class=\"MsnUsrFchUsr\">"));
                    oLabel = new Label();
                    oLabel.ID = "lblFecha_" + oRow["cod_contenido"].ToString();
                    oLabel.Text = oCulture.GetResource("Global", "Comentado") + " " + String.Format("{0:f}", DateTime.Parse(oRow["date_contenido"].ToString()));
                    oLabel.CssClass = "lblFechaComment";
                    Controls.Add(oLabel);
                    Controls.Add(new LiteralControl("</div>"));
                    if (!string.IsNullOrEmpty(oIsUsuario.CodUsuario))
                    {
                      if ((oRow["cod_usuario"].ToString() != oIsUsuario.CodUsuario) && (int.Parse(oIsUsuario.Tipo) > 1))
                      {
                        Controls.Add(new LiteralControl("<div class=\"MsnUsrDenuncia\">"));
                        if (string.IsNullOrEmpty(oRow["ind_denuncia"].ToString()))
                        {
                          Button oBntDen = new Button();
                          oBntDen.ID = "btnDenuncia" + oRow["cod_contenido"].ToString();
                          oBntDen.CssClass = "btnDenuncia";
                          oBntDen.Text = "Denunciar";
                          oBntDen.Attributes["CodContenido"] = oRow["cod_contenido"].ToString();
                          oBntDen.Click += new EventHandler(oBntDen_Click);
                          Controls.Add(oBntDen);
                        }
                        else
                        {
                          Label oLblDenuncia = new Label();
                          oLblDenuncia.ID = "lblDenuncia" + oRow["cod_contenido"].ToString();
                          oLblDenuncia.Text = "Denunciado";
                          Controls.Add(oLblDenuncia);
                        }
                        Controls.Add(new LiteralControl("</div>"));
                      }
                    }
                    Controls.Add(new LiteralControl("</td></tr>"));
                    getContenidosRel(oRow["cod_contenido"].ToString(), oRow["cod_usuario"].ToString(), oRow["date_contenido"].ToString(), dContenidoUsuario);
                  }
                dUsuario = null;
              }
            }
          oRows = null;
        }
      dContenidoUsuario = null;
      Controls.Add(new LiteralControl("</table>"));
    }

    protected void getContenidosRel(string pCodContenidoRel, string pCodUsuario, string pDateContenido, DataTable dContenidoUsuario)
    {
      var sCodUsuario = string.Empty;
      RadBinaryImage oImage;
      RequiredFieldValidator oRequiredFieldValidator;
      Label oLabel;
      Button oButton;
      StringBuilder sSQL = new StringBuilder();
      sSQL.Append(" cod_contenido_rel = ").Append(pCodContenidoRel);
      DataRow[] oRowsContenidoRel = dContenidoUsuario.Select(sSQL.ToString(), " date_contenido desc ");
      if (oRowsContenidoRel != null)
        if (oRowsContenidoRel.Count() > 0)
        {
          Controls.Add(new LiteralControl("<tr><td class=\"MsnUsrEmptyBlq\"></td><td><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" class=\"RespMsnUsrBlq\">"));
          foreach (DataRow oRow in oRowsContenidoRel)
          {
            Controls.Add(new LiteralControl("<tr><td class=\"MsnUsrCommetBlq\"><div class=\"MsnUsrImgUsrLittle\">"));
            sCodUsuario = oRow["cod_usuario"].ToString();

            oImage = new RadBinaryImage();
            oImage.CssClass = "ImageUsrLittle";
            oImage.DataValue = oWeb.getImageProfileUser(oRow["cod_usuario"].ToString(), 300, 300);
            oImage.Width = Unit.Pixel(32);
            oImage.AutoAdjustImageControlSize = false;
            Controls.Add(oImage);

            LinkButton oLinkButton = new LinkButton();
            oLinkButton.Width = Unit.Pixel(52);
            oLinkButton.Attributes["CodUsuario"] = oRow["cod_usuario"].ToString();
            oLinkButton.CssClass = "ImageUsrLittle";
            oLinkButton.Click += new EventHandler(oLinkButton_Click);
            oLinkButton.Controls.Add(oImage);
            Controls.Add(oLinkButton);

            Controls.Add(new LiteralControl("</div><div class=\"MsnUsrNomUsrLittle\">"));
            BinaryUsuario dUser;
            SysUsuario oUsuario = new SysUsuario();
            oUsuario.Path = Server.MapPath(".");
            oUsuario.CodUsuario = oRow["cod_usuario"].ToString();
            dUser = oUsuario.ClassGet();

            LinkButton oLnkButton = new LinkButton();
            oLnkButton.Text = dUser.NomUsuario + " " + dUser.ApeUsuario;
            oLnkButton.Attributes["CodUsuario"] = oRow["cod_usuario"].ToString();
            oLnkButton.CssClass = "lblNomUsuarioComment";
            oLnkButton.Click += new EventHandler(oLinkButton_Click);
            Controls.Add(oLnkButton);
            dUser = null;
            Controls.Add(new LiteralControl("</div><div class=\"MsnUsrComentUsrLittle\">"));
            oLabel = new Label();
            oLabel.ID = "lblComment_" + oRow["cod_contenido"].ToString();
            oLabel.Text = oRow["texto_contenido"].ToString();
            oLabel.CssClass = "lblComment";
            Controls.Add(oLabel);
            Controls.Add(new LiteralControl("</div><div class=\"MsnUsrFchUsrLittle\">"));
            oLabel = new Label();
            oLabel.ID = "lblFecha_" + oRow["cod_contenido"].ToString();
            oLabel.Text = oCulture.GetResource("Global", "Comentado") + " " + String.Format("{0:f}", DateTime.Parse(oRow["date_contenido"].ToString()));
            oLabel.CssClass = "lblFechaComment";
            Controls.Add(oLabel);
            Controls.Add(new LiteralControl("</div>"));
            if (!string.IsNullOrEmpty(oIsUsuario.Tipo))
            {
              if ((oRow["cod_usuario"].ToString() != oIsUsuario.CodUsuario) && (int.Parse(oIsUsuario.Tipo) > 1))
              {
                Controls.Add(new LiteralControl("<div class=\"MsnUsrDenuncia\">"));
                if (string.IsNullOrEmpty(oRow["ind_denuncia"].ToString()))
                {
                  Button oBntDen = new Button();
                  oBntDen.ID = "btnDenuncia" + oRow["cod_contenido"].ToString();
                  oBntDen.CssClass = "btnDenuncia";
                  oBntDen.Text = "Denunciar";
                  oBntDen.Attributes["CodContenido"] = oRow["cod_contenido"].ToString();
                  oBntDen.Click += new EventHandler(oBntDen_Click);
                  Controls.Add(oBntDen);
                }
                else
                {
                  Label oLblDenuncia = new Label();
                  oLblDenuncia.ID = "lblDenuncia" + oRow["cod_contenido"].ToString();
                  oLblDenuncia.Text = "Denunciado";
                  Controls.Add(oLblDenuncia);
                }
                Controls.Add(new LiteralControl("</div>"));
              }
            }
            Controls.Add(new LiteralControl("</td></tr>"));
          }
          if ((sCodUsuario != oIsUsuario.CodUsuario) && (!string.IsNullOrEmpty(oIsUsuario.CodUsuario)))
          {
            Controls.Add(new LiteralControl("<tr><td>"));
            Controls.Add(new LiteralControl("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">"));
            Controls.Add(new LiteralControl("<tr><td class=\"MsnUsrCommetBlq\" id=\"comentetxt_" + pCodContenidoRel + "\" style=\"display:block;\" valign=\"top\">"));
            Controls.Add(new LiteralControl("<div class=\"MsnUsrBlqTxt\">"));
            TextBox oTexto = new TextBox();
            oTexto.ID = "txt_" + pCodContenidoRel;
            oTexto.TextMode = TextBoxMode.MultiLine;
            oTexto.CssClass = "CssComente";
            Controls.Add(oTexto);
            Controls.Add(new LiteralControl("</div>"));
            Controls.Add(new LiteralControl("<div class=\"MsnUsrBlqVal\">"));
            oRequiredFieldValidator = new RequiredFieldValidator();
            oRequiredFieldValidator.ID = "valTxt_" + pCodContenidoRel;
            oRequiredFieldValidator.ControlToValidate = "txt_" + pCodContenidoRel;
            oRequiredFieldValidator.Display = ValidatorDisplay.Static;
            oRequiredFieldValidator.ValidationGroup = "valTxtComment_" + pCodContenidoRel;
            oRequiredFieldValidator.ErrorMessage = "*";
            oRequiredFieldValidator.CssClass = "csVal";
            Controls.Add(oRequiredFieldValidator);
            Controls.Add(new LiteralControl("</div>"));
            Controls.Add(new LiteralControl("<div class=\"MsnUsrBlqBtn\">"));
            oButton = new Button();
            oButton.ID = "SendComente_" + pCodContenidoRel;
            oButton.Text = "Aceptar";
            oButton.Attributes["CodContenidoRel"] = pCodContenidoRel;
            oButton.Click += new EventHandler(oButton_Click);
            oButton.CssClass = "cBtnMsnUsrComment";
            oButton.ValidationGroup = "valTxtComment_" + pCodContenidoRel;
            Controls.Add(oButton);
            Controls.Add(new LiteralControl("</div>"));
            Controls.Add(new LiteralControl("</td></tr></table>"));
            Controls.Add(new LiteralControl("</td></tr>"));
          }
          Controls.Add(new LiteralControl("</table></dtr></tr>"));
        }
        else
        {
          if ((pCodUsuario != oIsUsuario.CodUsuario) && (!string.IsNullOrEmpty(oIsUsuario.CodUsuario)))
          {
            Controls.Add(new LiteralControl("<tr><td class=\"MsnUsrEmptyBlq\"></td><td>"));
            Controls.Add(new LiteralControl("<table class=\"RespMsnUsrBlq\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">"));
            Controls.Add(new LiteralControl("<tr><td class=\"MsnUsrCommetBlq\" id=\"comentetxt_" + pCodContenidoRel + "\" style=\"display:block;\" valign=\"top\">"));
            Controls.Add(new LiteralControl("<div class=\"MsnUsrBlqTxt\">"));
            TextBox oTexto = new TextBox();
            oTexto.ID = "txt_" + pCodContenidoRel;
            oTexto.TextMode = TextBoxMode.MultiLine;
            oTexto.CssClass = "CssComente";
            Controls.Add(oTexto);
            Controls.Add(new LiteralControl("</div>"));
            Controls.Add(new LiteralControl("<div class=\"MsnUsrBlqVal\">"));
            oRequiredFieldValidator = new RequiredFieldValidator();
            oRequiredFieldValidator.ID = "valTxt_" + pCodContenidoRel;
            oRequiredFieldValidator.ControlToValidate = "txt_" + pCodContenidoRel;
            oRequiredFieldValidator.Display = ValidatorDisplay.Static;
            oRequiredFieldValidator.ValidationGroup = "valTxtComment_" + pCodContenidoRel;
            oRequiredFieldValidator.ErrorMessage = "*";
            oRequiredFieldValidator.CssClass = "csVal";
            Controls.Add(oRequiredFieldValidator);
            Controls.Add(new LiteralControl("</div>"));
            Controls.Add(new LiteralControl("<div class=\"MsnUsrBlqBtn\">"));
            oButton = new Button();
            oButton.ID = "SendComente_" + pCodContenidoRel;
            oButton.Text = "Aceptar";
            oButton.Attributes["CodContenidoRel"] = pCodContenidoRel;
            oButton.CssClass = "cBtnMsnUsrComment";
            oButton.Click += new EventHandler(oButton_Click);
            oButton.ValidationGroup = "valTxtComment_" + pCodContenidoRel;
            Controls.Add(oButton);
            Controls.Add(new LiteralControl("</div>"));
            Controls.Add(new LiteralControl("</td></tr></table>"));
            Controls.Add(new LiteralControl("</td></tr>"));

          }

        }
      oRowsContenidoRel = null;
    }

    protected void CommentToUser()
    {
      oIsUsuario = oWeb.GetObjUsuario();
      if ((Session["CodUsuarioPerfil"] != null) && (!string.IsNullOrEmpty(Session["CodUsuarioPerfil"].ToString())) && (oIsUsuario.CodUsuario != Session["CodUsuarioPerfil"].ToString()) && (oIsUsuario.Tipo == "1"))
      {
        Label oLabel;
        RadPanelItem itemN;
        RadBinaryImage oImage;
        RadPanelBar RadPanelBar1 = new RadPanelBar();
        RadPanelBar1.Width = Unit.Percentage(100);
        RadPanelBar1.EnableEmbeddedSkins = false;
        //RadPanelBar1.Skin = "Sitefinity";
        RadPanelItem item1 = new RadPanelItem();
        item1.Text = oCulture.GetResource("Global", "Comentarios");
        item1.Expanded = true;

        StringBuilder oFolder = new StringBuilder();
        oFolder.Append(Server.MapPath("."));
        StringBuilder sFile = new StringBuilder();
        sFile.Append("ComentarioUsuario_").Append(Session["CodUsuarioPerfil"].ToString()).Append(".bin");
        DataTable ComentarioUsuario = oWeb.DeserializarTbl(oFolder.ToString(), sFile.ToString());
        if (ComentarioUsuario != null)
          if (ComentarioUsuario.Rows.Count > 0)
          {
            foreach (DataRow oRow in ComentarioUsuario.Rows)
            {
              itemN = new RadPanelItem();
              itemN.Controls.Add(new LiteralControl("<div class=\"MsnUsrImgUsr\">"));
              oImage = new RadBinaryImage();
              oImage.DataValue = oWeb.getImageProfileUser(oRow["cod_usuario_rel"].ToString(), 300, 300);
              oImage.CssClass = "ImageUsrTiny";
              oImage.Width = Unit.Pixel(52);
              oImage.AutoAdjustImageControlSize = false;

              LinkButton oLinkButton = new LinkButton();
              oLinkButton.Attributes["CodUsuario"] = oRow["cod_usuario_rel"].ToString();
              oLinkButton.Width = Unit.Pixel(52);
              oLinkButton.CssClass = "ImageUsrTiny";
              oLinkButton.Click += new EventHandler(oLinkButton_Click);
              oLinkButton.Controls.Add(oImage);

              itemN.Controls.Add(oLinkButton);
              itemN.Controls.Add(new LiteralControl("</div><div class=\"MsnUsrNomUsr\">"));
              BinaryUsuario dUser;
              SysUsuario oUsuario = new SysUsuario();
              oUsuario.Path = Server.MapPath(".");
              oUsuario.CodUsuario = oRow["cod_usuario_rel"].ToString();
              dUser = oUsuario.ClassGet();

              LinkButton oLnkButton = new LinkButton();
              oLnkButton.Text = dUser.NomUsuario + " " + dUser.ApeUsuario;
              oLnkButton.Attributes["CodUsuario"] = oRow["cod_usuario_rel"].ToString();
              oLnkButton.CssClass = "lblNomUsuarioComment";
              oLnkButton.Click += new EventHandler(oLinkButton_Click);
              itemN.Controls.Add(oLnkButton);

              //oLabel = new Label();
              //oLabel.ID = "lblNomUsuario_" + oRow["cod_usuario_rel"].ToString();
              //oLabel.Text = dUser.NomUsuario + " " + dUser.ApeUsuario;
              //oLabel.CssClass = "lblNomUsuarioComment";
              dUser = null;
              //itemN.Controls.Add(oLabel);
              itemN.Controls.Add(new LiteralControl("</div><div class=\"MsnUsrComentUsr\">"));

              oLabel = new Label();
              oLabel.ID = "lblComment_" + oRow["cod_usuario_rel"].ToString();
              oLabel.Text = oRow["comentario"].ToString();
              oLabel.CssClass = "lblComment";
              itemN.Controls.Add(oLabel);

              itemN.Controls.Add(new LiteralControl("</div>"));
              item1.Items.Add(itemN);
            }
          }
        ComentarioUsuario = null;

        itemN = new RadPanelItem();
        itemN.Controls.Add(new LiteralControl("<div class=\"comenta\">"));
        itemN.Controls.Add(new LiteralControl("<div class=\"idDejaComment\">"));
        oLabel = new Label();
        oLabel.ID = "lblDejaTuComentario";
        oLabel.CssClass = "lblDejaTuComentarioCss";
        oLabel.Text = oCulture.GetResource("Global", "DejaTuComentario");
        itemN.Controls.Add(oLabel);
        itemN.Controls.Add(new LiteralControl("</div>"));
        itemN.Controls.Add(new LiteralControl("<div class=\"idTextDejaComment\">"));

        TextBox oTextBox = new TextBox();
        oTextBox.ID = "txtDejaTuComentario";
        oTextBox.TextMode = TextBoxMode.MultiLine;
        oTextBox.CssClass = "txtDejaTuComentarioCss";
        itemN.Controls.Add(oTextBox);

        RequiredFieldValidator oRequiredFieldValidator = new RequiredFieldValidator();
        oRequiredFieldValidator.ID = "valtxtDejaTuComentario";
        oRequiredFieldValidator.ControlToValidate = "txtDejaTuComentario";
        oRequiredFieldValidator.Display = ValidatorDisplay.Static;
        oRequiredFieldValidator.ValidationGroup = "DejaTuComentario";
        oRequiredFieldValidator.ErrorMessage = "*";
        itemN.Controls.Add(oRequiredFieldValidator);
        itemN.Controls.Add(new LiteralControl("</div>"));

        itemN.Controls.Add(new LiteralControl("<div class=\"idBtnDejaComment\">"));
        Button oBtnDejaTuComengtario = new Button();
        oBtnDejaTuComengtario.ID = "BtnDejaTuComengtario";
        oBtnDejaTuComengtario.Text = oCulture.GetResource("Global", "btnPublicar");
        oBtnDejaTuComengtario.ValidationGroup = "DejaTuComentario";
        oBtnDejaTuComengtario.CssClass = "cBtnDejaUsrComment";
        oBtnDejaTuComengtario.Click += new EventHandler(oBtnDejaTuComengtario_Click);
        itemN.Controls.Add(oBtnDejaTuComengtario);
        itemN.Controls.Add(new LiteralControl("</div>"));
        itemN.Controls.Add(new LiteralControl("</div>"));
        item1.Items.Add(itemN);

        RadPanelBar1.Items.Add(item1);
        Controls.Add(RadPanelBar1);
        oBtnDejaTuComengtario.Attributes["TxtControlID"] = oTextBox.ClientID;
      }
    }

    void oBtnDejaTuComengtario_Click(object sender, EventArgs e)
    {
      DBConn oConn = new DBConn();
      try
      {
        if (oConn.Open())
        {
          oConn.BeginTransaction();
          StringBuilder oFolder = new StringBuilder();
          oFolder.Append(Server.MapPath(".")).Append(@"\binary\");

          ObjectModel oObjectModel = new ObjectModel(ref oConn);
          SysComentarioUsuario oComentarioUsuario = new SysComentarioUsuario(ref oConn);
          oComentarioUsuario.CodComentario = oObjectModel.getCodeKey("SYS_COMENTARIO_USUARIO");
          oComentarioUsuario.CodUsuario = Session["CodUsuarioPerfil"].ToString();
          oComentarioUsuario.CodUsuarioRel = oIsUsuario.CodUsuario;
          oComentarioUsuario.IpUsuario = oWeb.GetIpUsuario();
          oComentarioUsuario.FecUsuario = DateTime.Now.ToString();
          oComentarioUsuario.Comentario = (Page.FindControl((sender as Button).Attributes["TxtControlID"].Replace("_", "$").ToString()) as TextBox).Text;
          oComentarioUsuario.Accion = "CREAR";
          oComentarioUsuario.Put();
          oObjectModel = null;
          if (string.IsNullOrEmpty(oComentarioUsuario.Error))
          {
            oConn.Commit();

            StringBuilder sFile = new StringBuilder();
            sFile.Append("ComentarioUsuario_").Append(Session["CodUsuarioPerfil"].ToString()).Append(".bin");
            oComentarioUsuario.CodUsuario = Session["CodUsuarioPerfil"].ToString();
            oComentarioUsuario.SerializaTblComentarioUsuario(ref oConn, oFolder.ToString(), sFile.ToString());
            oComentarioUsuario = null;

            oLog.CodEvtLog = "001";
            oLog.IdUsuario = oLog.IdUsuario = (!string.IsNullOrEmpty(oIsUsuario.CodUsuario) ? oIsUsuario.CodUsuario : "-1");
            oLog.ObsLog = "<COMENTARIOA>" + Session["CodUsuarioPerfil"].ToString();
            //oLog.putLog();
          }
          else
            oConn.Rollback();
          oConn.Close();
        }
      }
      catch
      {
        if (oConn.bIsOpen)
        {
          oConn.Rollback();
          oConn.Close();
        }
      }
      Response.Redirect(".");
    }

    void oButton_Click(object sender, EventArgs e)
    {
      string sNomApeUsrOrigen = string.Empty;
      string sEmailDestino = string.Empty;
      string pCodContenido = string.Empty;
      BinaryUsuario dUser;
      StringBuilder sFile = new StringBuilder();
      DBConn oConn = new DBConn();

      try
      {
        if (oConn.Open())
        {
          oConn.BeginTransaction();

          StringBuilder oFolder = new StringBuilder();
          oFolder.Append(Server.MapPath(".")).Append(@"\binary\");

          CmsContenidos oContenidos = new CmsContenidos(ref oConn);
          if (!string.IsNullOrEmpty((sender as Button).Attributes["CodContenidoRel"].ToString()))
            oContenidos.CodContenidoRel = (sender as Button).Attributes["CodContenidoRel"].ToString();
          oContenidos.CodUsuario = oIsUsuario.CodUsuario;
          oContenidos.CodUsuarioRel = HttpContext.Current.Session["CodUsuarioPerfil"].ToString();
          oContenidos.TextoContenido = (string.IsNullOrEmpty((sender as Button).Attributes["CodContenidoRel"].ToString()) ? (this.FindControl("txtComent_" + oIsUsuario.CodUsuario) as TextBox).Text : (this.FindControl("txt_" + (sender as Button).Attributes["CodContenidoRel"].ToString()) as TextBox).Text);
          oContenidos.EstContenido = "P";
          oContenidos.CodNodo = pCodNodo;
          oContenidos.DateContenido = DateTime.Now.ToString();
          oContenidos.PrvContendio = "4";
          oContenidos.IpUsuario = Request.ServerVariables["REMOTE_ADDR"].ToString();
          oContenidos.Accion = "CREAR";
          oContenidos.Put();
          pCodContenido = oContenidos.CodContenido;

          if (string.IsNullOrEmpty(oContenidos.Error))
          {
            oConn.Commit();

            /*sFile.Append("ContenidoUsuario_").Append(oIsUsuario.CodUsuario).Append(".bin");
            oContenidos.CodUsuarioRel = oIsUsuario.CodUsuario;
            oContenidos.SerializaTblContenidoByUser(ref oConn, oFolder.ToString(), sFile.ToString());

            sFile.Length = 0;
            sFile.Append("ContenidoUsuario_").Append(HttpContext.Current.Session["CodUsuarioPerfil"].ToString()).Append(".bin");
            oContenidos.CodUsuario = HttpContext.Current.Session["CodUsuarioPerfil"].ToString();
            oContenidos.CodUsuarioRel = HttpContext.Current.Session["CodUsuarioPerfil"].ToString();
            oContenidos.SerializaTblContenidoByUser(ref oConn, oFolder.ToString(), sFile.ToString());*/

            //sFile.Length = 0;
            //sFile.Append("Contenidos.bin");
            oContenidos.SerializaContenidos(ref oConn, oFolder.ToString(), "Contenidos.bin");

            oLog.CodEvtLog = "002";
            oLog.IdUsuario = oLog.IdUsuario = (!string.IsNullOrEmpty(oIsUsuario.CodUsuario) ? oIsUsuario.CodUsuario : "-1");
            oLog.ObsLog = "<COMENTARIOA>" + Session["CodUsuarioPerfil"].ToString();
            //oLog.putLog();

            SysUsuario oUsuario = new SysUsuario();
            oUsuario.Path = Server.MapPath(".");
            oUsuario.CodUsuario = oIsUsuario.CodUsuario;
            dUser = oUsuario.ClassGet();
            if (dUser != null)
              sNomApeUsrOrigen = dUser.NomUsuario + " " + dUser.ApeUsuario;
            dUser = null;

            oUsuario.CodUsuario = HttpContext.Current.Session["CodUsuarioPerfil"].ToString();
            dUser = oUsuario.ClassGet();
            if (dUser != null)
              sEmailDestino = dUser.EmlUsuario;
            dUser = null;

            StringBuilder sAsunto = new StringBuilder();
            StringBuilder oHtml = new StringBuilder();
            DataTable dParamEmail = oWeb.DeserializarTbl(Server.MapPath("."), "ParamEmail.bin");
            if (dParamEmail != null)
              if (dParamEmail.Rows.Count > 0)
              {
                DataRow[] oRows = dParamEmail.Select(" tipo_email = 'N' ");
                if (oRows != null)
                {
                  if (oRows.Count() > 0)
                  {
                    sAsunto.Append(oRows[0]["asunto_email"].ToString());
                    sAsunto.Replace("[NOMBRESITIO]", Application["SiteName"].ToString());
                    sAsunto.Replace("[USUARIO]", sNomApeUsrOrigen);
                    oHtml.Append(oRows[0]["cuerpo_email"].ToString());
                    oHtml.Replace("[NOMBRE]", sNomApeUsrOrigen);
                    oHtml.Replace("[CUERPO]", (string.IsNullOrEmpty((sender as Button).Attributes["CodContenidoRel"].ToString()) ? (this.FindControl("txtComent_" + oIsUsuario.CodUsuario) as TextBox).Text : (this.FindControl("txt_" + (sender as Button).Attributes["CodContenidoRel"].ToString()) as TextBox).Text));
                    oHtml.Replace("[SITIO]", "http://" + Request.ServerVariables["HTTP_HOST"].ToString());
                    oHtml.Replace("[NOMBRESITIO]", Application["SiteName"].ToString());

                    Emailing oEmailing = new Emailing();
                    oEmailing.FromName = Application["NameSender"].ToString();
                    oEmailing.From = Application["EmailSender"].ToString();
                    oEmailing.Address = sEmailDestino;
                    oEmailing.Subject = (!string.IsNullOrEmpty(sAsunto.ToString()) ? sAsunto.ToString() : sNomApeUsrOrigen + oCulture.GetResource("Mensajes", "sMessage01") + Application["SiteName"].ToString());
                    oEmailing.Body = oHtml;
                    oEmailing.EmailSend();
                  }
                }
                oRows = null;
              }
            dParamEmail = null;
          }
          else
            oConn.Rollback();
          oConn.Close();
        }
      }
      catch (Exception Ex)
      {
        if (oConn.bIsOpen)
        {
          oConn.Rollback();
          oConn.Close();
        }
      }
      Response.Redirect(".");
    }

    void oLinkButton_Click(object sender, EventArgs e)
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
            Session["CodUsuarioPerfil"] = (sender as LinkButton).Attributes["CodUsuario"].ToString();
          }
          oRow = null;
        }
      oNodos = null;
      Page.Response.Redirect(".");
    }

    void oDelButton_Click(object sender, EventArgs e)
    {
      DBConn oConn = new DBConn();
      try
      {
        if (oConn.Open())
        {
          oConn.BeginTransaction();

          CmsContenidos oContenidos = new CmsContenidos(ref oConn);
          oContenidos.CodContenido = (sender as Button).Attributes["CodContenido"].ToString();
          oContenidos.Accion = "ELIMINAR";
          oContenidos.Put();

          if (string.IsNullOrEmpty(oContenidos.Error))
          {
            oConn.Commit();

            StringBuilder oFolder = new StringBuilder();
            oFolder.Append(Server.MapPath(".")).Append(@"\binary\");
            oContenidos.SerializaContenidos(ref oConn, oFolder.ToString(), "Contenidos.bin");
          }
          else
          {
            oConn.Rollback();
          }
          oConn.Close();
        }
      }
      catch (Exception Ex)
      {
        if (oConn.bIsOpen)
        {
          oConn.Rollback();
          oConn.Close();
        }
      }
      Response.Redirect(".");
    }

    void oBntDen_Click(object sender, EventArgs e)
    {
      DBConn oConn = new DBConn();
      try
      {
        if (oConn.Open())
        {
          oConn.BeginTransaction();

          CmsContenidos oContenidos = new CmsContenidos(ref oConn);
          oContenidos.CodContenido = (sender as Button).Attributes["CodContenido"].ToString();
          oContenidos.IndDenuncia = "V";
          oContenidos.Accion = "EDITAR";
          oContenidos.Put();

          if (string.IsNullOrEmpty(oContenidos.Error))
          {
            oConn.Commit();

            StringBuilder oFolder = new StringBuilder();
            oFolder.Append(Server.MapPath(".")).Append(@"\binary\");
            oContenidos.SerializaContenidos(ref oConn, oFolder.ToString(), "Contenidos.bin");
          }
          else
          {
            oConn.Rollback();
          }
          oConn.Close();
        }
      }
      catch (Exception Ex)
      {
        if (oConn.bIsOpen)
        {
          oConn.Rollback();
          oConn.Close();
        }
      }
      Response.Redirect(".");
    }

  }
}