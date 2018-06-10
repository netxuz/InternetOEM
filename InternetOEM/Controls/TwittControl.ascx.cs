using System;
using System.Collections;
using System.Configuration;
using System.Text;
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
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using OnlineServices.Conn;
using OnlineServices.SystemData;
using OnlineServices.Method;
using Telerik.Web.UI;

namespace ICommunity.Controls
{
  public partial class TwittControl : System.Web.UI.UserControl
  {
    private string pCodUsuario = string.Empty;
    private string pCodSegUsuario = string.Empty;
    private OnlineServices.Method.Web oWeb = new OnlineServices.Method.Web();
    private OnlineServices.Method.Culture oCulture = new OnlineServices.Method.Culture();
    private OnlineServices.Method.Usuario oIsUsuario;

    protected void Page_Load(object sender, EventArgs e)
    {
      Label olabel;
      if ((Session["USUARIO"] != null) && (!string.IsNullOrEmpty(Session["USUARIO"].ToString())))
      {
        oIsUsuario = oWeb.GetObjUsuario();
        switch (this.Attributes["Method"].ToString())
        {
          case "putFollow":
            pCodSegUsuario = this.Attributes["CodSegUsuario"].ToString();
            putFollow();
            break;
          case "getFollow":
            //Clientes == 1
            Controls.Add(new LiteralControl("<div id=\"idSiguiendo\">"));
            if (oIsUsuario.Tipo == "1")
            {
              olabel = new Label();
              olabel.Text = oCulture.GetResource("Usuario", "BtnFollowing");
              Controls.Add(olabel);
              Controls.Add(new LiteralControl("</div>"));
              getFollow(); 
            }
            else
            {
              olabel = new Label();
              olabel.Text = oCulture.GetResource("Usuario", "lblFollowers");
              Controls.Add(olabel);
              Controls.Add(new LiteralControl("</div>"));
              getFollowMe();
            }
            break;
        }
      }
    }

    protected void putFollow()
    {
      StringBuilder sFile = new StringBuilder();
      sFile.Append("SeguirUsuariosF_").Append(oIsUsuario.CodUsuario).Append(".bin");
      DataTable dSeguirUsuarios = oWeb.DeserializarTbl(Server.MapPath(".").ToString(), sFile.ToString());

      if (dSeguirUsuarios != null)
      {
        if (dSeguirUsuarios.Rows.Count > 0)
        {
          DataRow[] oRows = dSeguirUsuarios.Select(" cod_seg_usuario =" + pCodSegUsuario);
          if (oRows != null)
            if (oRows.Count() == 0)
            {
              Controls.Add(new LiteralControl("<div class=\"zonafollow\">"));
              Button oButton = new Button();
              oButton.ID = "idPutBtnFollow_" + oIsUsuario.CodUsuario + "_" + pCodSegUsuario;
              oButton.Text = oCulture.GetResource("Usuario", "BtnFollow");
              oButton.Attributes["CodUsuario"] = oIsUsuario.CodUsuario;
              oButton.Attributes["CodSegUsuario"] = pCodSegUsuario;
              oButton.CssClass = "cBtnFollow";
              oButton.Click += new EventHandler(oButton_Fullow);
              Controls.Add(oButton);
              Controls.Add(new LiteralControl("</div>"));
            }
            else
            {
              Controls.Add(new LiteralControl("<div class=\"zonaUnfollow\">"));
              Button oButton = new Button();
              oButton.ID = "idPutBtnUnFollow_" + oIsUsuario.CodUsuario + "_" + pCodSegUsuario;
              oButton.Text = oCulture.GetResource("Usuario", "BtnFollowing");
              oButton.Attributes["CodUsuario"] = oIsUsuario.CodUsuario;
              oButton.Attributes["CodSegUsuario"] = pCodSegUsuario;
              oButton.CssClass = "cBtnFollowing";
              oButton.Click += new EventHandler(oButton_UnFullow);
              Controls.Add(oButton);
              oButton.Attributes["onmouseover"] = "document.getElementById('" + oButton.ClientID + "').value='" + oCulture.GetResource("Usuario", "BtnUnFollow") + "'";
              oButton.Attributes["onmouseout"] = "document.getElementById('" + oButton.ClientID + "').value='" + oCulture.GetResource("Usuario", "BtnFollowing") + "'";
              Controls.Add(new LiteralControl("</div>"));
            }
          oRows = null;
        }
        else
        {
          Controls.Add(new LiteralControl("<div class=\"zonafollow\">"));
          Button oButton = new Button();
          oButton.ID = "idPutBtnFollow_" + oIsUsuario.CodUsuario + "_" + pCodSegUsuario;
          oButton.Text = oCulture.GetResource("Usuario", "BtnFollow");
          oButton.Attributes["CodUsuario"] = oIsUsuario.CodUsuario;
          oButton.Attributes["CodSegUsuario"] = pCodSegUsuario;
          oButton.CssClass = "cBtnFollow";
          oButton.Click += new EventHandler(oButton_Fullow);
          Controls.Add(oButton);
          Controls.Add(new LiteralControl("</div>"));
        }
      }
      dSeguirUsuarios = null;
    }

    protected void getFollow()
    {
      Controls.Add(new LiteralControl("<div id=\"zonafollow\">"));
      StringBuilder sFile = new StringBuilder();
      sFile.Append("SeguirUsuariosF_").Append(oIsUsuario.CodUsuario).Append(".bin");
      DataTable dSeguirFUsuarios = oWeb.DeserializarTbl(Server.MapPath(".").ToString(), sFile.ToString());

      SysUsuario oUsuario;
      BinaryUsuario dUsuario;
      if (dSeguirFUsuarios != null)
        foreach (DataRow oRow in dSeguirFUsuarios.Rows)
        {
          oUsuario = new SysUsuario();
          oUsuario.Path = Server.MapPath(".");
          oUsuario.CodUsuario = oRow["cod_seg_usuario"].ToString();
          dUsuario = oUsuario.ClassGet();
          if (dUsuario.EstUsuario == "V")
          {
            Controls.Add(new LiteralControl("<div class=\"bfollow\">"));
            Controls.Add(new LiteralControl("<div class=\"bImgUsrFollow\">"));

            RadBinaryImage oImage = new RadBinaryImage();
            oImage.CssClass = "cTwittImg";
            oImage.DataValue = oWeb.getImageProfileUser(oRow["cod_seg_usuario"].ToString(), 300, 300);
            oImage.Width = Unit.Pixel(52);
            oImage.AutoAdjustImageControlSize = false;
            LinkButton oLinkButton = new LinkButton();
            //oLinkButton.Height = Unit.Pixel(52);
            oLinkButton.Width = Unit.Pixel(52);
            oLinkButton.Attributes["CodUsuario"] = oRow["cod_seg_usuario"].ToString();
            oLinkButton.CssClass = "cTwittImgUserFollowMe";
            oLinkButton.Click += new EventHandler(oLinkButton_Click);
            oLinkButton.Controls.Add(oImage);
            Controls.Add(oLinkButton);

            Controls.Add(new LiteralControl("</div>"));
            Controls.Add(new LiteralControl("<div>"));

            LinkButton oLnkButton = new LinkButton();
            oLnkButton.Text = oRow["usuario_follow"].ToString();
            oLnkButton.Attributes["CodUsuario"] = oRow["cod_seg_usuario"].ToString();
            oLnkButton.CssClass = "bNamefollow";
            oLnkButton.Click += new EventHandler(oLinkButton_Click);
            Controls.Add(oLnkButton);

            //Controls.Add(new LiteralControl(oRow["usuario_follow"].ToString()));
            Controls.Add(new LiteralControl("</div>"));
            Controls.Add(new LiteralControl("<div class=\"bBtnfollow\">"));

            Controls.Add(new LiteralControl("<div class=\"zonaUnfollow\">"));
            Button oButton = new Button();
            oButton.ID = "idGetBtnUnFollow_" + oIsUsuario.CodUsuario + "_" + oRow["cod_seg_usuario"].ToString();
            oButton.Text = oCulture.GetResource("Usuario", "BtnFollowing");
            oButton.Attributes["CodUsuario"] = oIsUsuario.CodUsuario;
            oButton.Attributes["CodSegUsuario"] = oRow["cod_seg_usuario"].ToString();
            oButton.CssClass = "cGetBtnFollow";
            oButton.Click += new EventHandler(oButton_UnFullow);
            Controls.Add(oButton);
            oButton.Attributes["onmouseover"] = "document.getElementById('" + oButton.ClientID + "').value='" + oCulture.GetResource("Usuario", "BtnUnFollow") + "'";
            oButton.Attributes["onmouseout"] = "document.getElementById('" + oButton.ClientID + "').value='" + oCulture.GetResource("Usuario", "BtnFollowing") + "'";
            Controls.Add(new LiteralControl("</div>"));

            Controls.Add(new LiteralControl("</div>"));

            Controls.Add(new LiteralControl("<div class=\"bDescfollow\">"));

            DataTable dCampoUsuarios = oWeb.DeserializarTbl(Server.MapPath(".").ToString(), "CampoUsuarios.bin");
            if (dCampoUsuarios != null)
            {
              if (dCampoUsuarios.Rows.Count > 0)
              {
                DataRow[] cRow = dCampoUsuarios.Select(" tipo_campo = 2 and desp_campo = 'O' and ind_despliegue = 'V' ");
                if (cRow != null)
                {
                  if (cRow.Count() > 0)
                  {
                    DataTable dInfoUsuarios = oWeb.DeserializarTbl(Server.MapPath(".").ToString(), "InfoUsuario_" + oRow["cod_seg_usuario"].ToString() + ".bin");
                    if (dInfoUsuarios != null)
                    {
                      if (dInfoUsuarios.Rows.Count > 0)
                      {
                        DataRow[] iRow = dInfoUsuarios.Select(" cod_campo = " + cRow[0]["cod_campo"].ToString());
                        if (iRow != null)
                        {
                          if (iRow.Count() > 0)
                          {
                            Controls.Add(new LiteralControl(iRow[0]["val_campo"].ToString()));
                          }
                        }
                        iRow = null;
                      }
                    }
                    dInfoUsuarios = null;
                  }
                }
                cRow = null;
              }
            }
            dCampoUsuarios = null;

            Controls.Add(new LiteralControl("</div>"));

            Controls.Add(new LiteralControl("</div>"));
          }
          dUsuario = null;
        }
      dSeguirFUsuarios = null;
      Controls.Add(new LiteralControl("</div>"));
    }

    protected void getFollowMe()
    {
      Controls.Add(new LiteralControl("<div id=\"zonafollowme\">"));
      StringBuilder sFile = new StringBuilder();
      sFile.Append("SeguirUsuariosM_").Append(oIsUsuario.CodUsuario).Append(".bin");
      DataTable dSeguirMUsuarios = oWeb.DeserializarTbl(Server.MapPath(".").ToString(), sFile.ToString());

      if (dSeguirMUsuarios != null)
        foreach (DataRow oRow in dSeguirMUsuarios.Rows)
        {
          Controls.Add(new LiteralControl("<div class=\"bfollowme\">"));
          Controls.Add(new LiteralControl("<div class=\"bImgUsrFollowme\">"));

          RadBinaryImage oImage = new RadBinaryImage();
          oImage.CssClass = "cTwittImg";
          oImage.DataValue = oWeb.getImageProfileUser(oRow["cod_usuario"].ToString(), 62, 62);
          LinkButton oLinkButton = new LinkButton();
          oLinkButton.Height = Unit.Pixel(62);
          oLinkButton.Attributes["CodUsuario"] = oRow["cod_usuario"].ToString();
          oLinkButton.Width = Unit.Pixel(62);
          oLinkButton.CssClass = "cTwittImgUserFollowMe";
          oLinkButton.Click += new EventHandler(oLinkButton_Click);
          oLinkButton.Controls.Add(oImage);
          Controls.Add(oLinkButton);

          Controls.Add(new LiteralControl("</div>"));
          Controls.Add(new LiteralControl("<div>"));
          LinkButton oLnkButton = new LinkButton();
          oLnkButton.Text = oRow["usuario_followme"].ToString();
          oLnkButton.Attributes["CodUsuario"] = oRow["cod_usuario"].ToString();
          oLnkButton.CssClass = "bNamefollowme";
          oLnkButton.Click += new EventHandler(oLinkButton_Click);
          Controls.Add(oLnkButton);

          //Controls.Add(new LiteralControl(oRow["usuario_followme"].ToString()));
          Controls.Add(new LiteralControl("</div>"));
          Controls.Add(new LiteralControl("<div class=\"bBtnfollowme\">"));

          Controls.Add(new LiteralControl("<div class=\"zonaUnfollowme\">"));
          //Button oButton = new Button();
          //oButton.ID = "idGetBtnUnFollow_" + oIsUsuario.CodUsuario + "_" + oRow["cod_usuario"].ToString();
          //oButton.Text = oCulture.GetResource("Usuario", "BtnFollowing");
          //oButton.Attributes["CodUsuario"] = oRow["cod_usuario"].ToString();
          //oButton.Attributes["CodSegUsuario"] = oIsUsuario.CodUsuario;
          //oButton.CssClass = "cPutBtnFollow";
          //oButton.Click += new EventHandler(oButton_UnFullow);
          //Controls.Add(oButton);
          Controls.Add(new LiteralControl("</div>"));

          Controls.Add(new LiteralControl("</div>"));
          Controls.Add(new LiteralControl("<div class=\"bDescfollow\">"));

          DataTable dCampoUsuarios = oWeb.DeserializarTbl(Server.MapPath(".").ToString(), "CampoUsuarios.bin");
          if (dCampoUsuarios != null)
          {
            if (dCampoUsuarios.Rows.Count > 0)
            {
              DataRow[] cRow = dCampoUsuarios.Select(" tipo_campo = 2 and desp_campo = 'O' and ind_despliegue = 'V' ");
              if (cRow != null)
              {
                if (cRow.Count() > 0)
                {
                  DataTable dInfoUsuarios = oWeb.DeserializarTbl(Server.MapPath(".").ToString(), "InfoUsuario_" + oRow["cod_usuario"].ToString() + ".bin");
                  if (dInfoUsuarios != null)
                  {
                    if (dInfoUsuarios.Rows.Count > 0)
                    {
                      DataRow[] iRow = dInfoUsuarios.Select(" cod_campo = " + cRow[0]["cod_campo"].ToString());
                      if (iRow != null)
                      {
                        if (iRow.Count() > 0)
                        {
                          Controls.Add(new LiteralControl(iRow[0]["val_campo"].ToString()));
                        }
                      }
                      iRow = null;
                    }
                  }
                  dInfoUsuarios = null;
                }
              }
              cRow = null;
            }
          }
          dCampoUsuarios = null;

          Controls.Add(new LiteralControl("</div>"));
          Controls.Add(new LiteralControl("</div>"));
        }
      dSeguirMUsuarios = null;
      Controls.Add(new LiteralControl("</div>"));
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

    void oButton_Fullow(object sender, EventArgs e)
    {
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        SysSeguirUsuarios oSeguirUsuarios = new SysSeguirUsuarios(ref oConn);
        oSeguirUsuarios.CodUsuario = (sender as Button).Attributes["CodUsuario"].ToString();
        oSeguirUsuarios.CodSegUsuario = (sender as Button).Attributes["CodSegUsuario"].ToString();
        oSeguirUsuarios.Accion = "CREAR";
        oSeguirUsuarios.Put();

        StringBuilder oFolder = new StringBuilder();
        oFolder.Append(Server.MapPath(".")).Append(@"\binary\");
        StringBuilder sFile = new StringBuilder();
        sFile.Append("SeguirUsuariosF_").Append((sender as Button).Attributes["CodUsuario"].ToString()).Append(".bin");
        oSeguirUsuarios.SerializaUserFollow(ref oConn, oFolder.ToString(), sFile.ToString());
        sFile.Length = 0;
        sFile.Append("SeguirUsuariosM_").Append((sender as Button).Attributes["CodSegUsuario"].ToString()).Append(".bin");
        oSeguirUsuarios.SerializaUserFollowMe(ref oConn, oFolder.ToString(), sFile.ToString());
        oConn.Close();
      }
      Response.Redirect("Escort.aspx?CodUsuario=" + (sender as Button).Attributes["CodSegUsuario"].ToString());
    }

    void oButton_UnFullow(object sender, EventArgs e)
    {
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        SysSeguirUsuarios oSeguirUsuarios = new SysSeguirUsuarios(ref oConn);
        oSeguirUsuarios.CodUsuario = (sender as Button).Attributes["CodUsuario"].ToString();
        oSeguirUsuarios.CodSegUsuario = (sender as Button).Attributes["CodSegUsuario"].ToString();
        oSeguirUsuarios.Accion = "ELIMINAR";
        oSeguirUsuarios.Put();

        StringBuilder oFolder = new StringBuilder();
        oFolder.Append(Server.MapPath(".")).Append(@"\binary\");
        StringBuilder sFile = new StringBuilder();
        sFile.Append("SeguirUsuariosF_").Append((sender as Button).Attributes["CodUsuario"].ToString()).Append(".bin");
        oSeguirUsuarios.SerializaUserFollow(ref oConn, oFolder.ToString(), sFile.ToString());
        sFile.Length = 0;
        sFile.Append("SeguirUsuariosM_").Append((sender as Button).Attributes["CodSegUsuario"].ToString()).Append(".bin");
        oSeguirUsuarios.SerializaUserFollowMe(ref oConn, oFolder.ToString(), sFile.ToString());
        oConn.Close();
      }
      if (this.Attributes["Method"].ToString() == "putFollow")
        Response.Redirect("Escort.aspx?CodUsuario=" + (sender as Button).Attributes["CodSegUsuario"].ToString());
      else
        Response.Redirect(".");
    }

  }
}