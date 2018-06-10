using System;
using System.Collections;
using System.Configuration;
using System.Text;
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
using System.Data;

using OnlineServices.Conn;
using OnlineServices.SystemData;
using OnlineServices.Method;
using Telerik.Web.UI;

namespace ICommunity.Controls
{
  public partial class RelUsuarioControl : System.Web.UI.UserControl
  {
    private string pCodUsuario = string.Empty;
    private string pCodUsuarioRel = string.Empty;
    private OnlineServices.Method.Web oWeb = new OnlineServices.Method.Web();
    private OnlineServices.Method.Culture oCulture = new OnlineServices.Method.Culture();
    private OnlineServices.Method.Usuario oIsUsuario;
    
    protected void Page_Load(object sender, EventArgs e)
    {
      if ((Session["USUARIO"] != null) && (!string.IsNullOrEmpty(Session["USUARIO"].ToString())))
      {
        oIsUsuario = oWeb.GetObjUsuario();
        if (!string.IsNullOrEmpty(this.Attributes["Method"].ToString()))
        {
          switch (this.Attributes["Method"].ToString())
          {
            case "FriendRequest":
              if (oIsUsuario.Tipo == "1")
              {
                pCodUsuarioRel = this.Attributes["CodUsuarioRel"].ToString();
                if ((Session["USUARIO"] != null) && (!string.IsNullOrEmpty(Session["USUARIO"].ToString())) && (pCodUsuarioRel != oIsUsuario.CodUsuario))
                  putFriendRequest();
              }
              break;
            case "ConfirmFriendRequest":
              putConfirmFriendRequest();
              break;
          }
        }
      }
    }

    protected void putFriendRequest()
    {
      StringBuilder cSQL = new StringBuilder();
      cSQL.Append(" cod_usuario_rel = ").Append(pCodUsuarioRel);

      StringBuilder oFolder = new StringBuilder();
      oFolder.Append(Server.MapPath("."));

      StringBuilder sFile = new StringBuilder();
      sFile.Append("RelacionUsuario_").Append(oIsUsuario.CodUsuario).Append(".bin");
      DataTable dRelacionUsuarios = oWeb.DeserializarTbl(oFolder.ToString(), sFile.ToString());

      if (dRelacionUsuarios != null)
      {
        if (dRelacionUsuarios.Rows.Count > 0)
        {
          DataRow[] oRows = dRelacionUsuarios.Select(cSQL.ToString());
          if (oRows != null)
            if (oRows.Count() == 0)
            {
              Controls.Add(new LiteralControl("<div class=\"zonasolicitud\">"));
              Button oButton = new Button();
              oButton.ID = "idBtn_" + oIsUsuario.CodUsuario + "_" + pCodUsuarioRel;
              oButton.Text = oCulture.GetResource("Usuario", "BtnEnviarSolicitudAmistad");
              oButton.Attributes["sType"] = "P";
              oButton.Attributes["CodUsuario"] = oIsUsuario.CodUsuario;
              oButton.Attributes["CodUsuarioRel"] = pCodUsuarioRel;
              oButton.Click += new EventHandler(oButton_Click);
              Controls.Add(oButton);
              Controls.Add(new LiteralControl("</div>"));
            }
            else
            {
              if (oRows[0]["est_relacion"].ToString() == "P")
              {
                Controls.Add(new LiteralControl("<div class=\"zonasolicitud\">"));
                Label oLabel = new Label();
                oLabel.ID = "lbl_" + oIsUsuario.CodUsuario + "_" + pCodUsuarioRel;
                oLabel.Text = oCulture.GetResource("Usuario", "BtnEsperandoConfirmacionAmistad");
                Controls.Add(oLabel);
                Controls.Add(new LiteralControl("</div>"));
              }
            }
          oRows = null;
        }
        else {
          Controls.Add(new LiteralControl("<div class=\"zonasolicitud\">"));
          Button oButton = new Button();
          oButton.ID = "idBtn_" + oIsUsuario.CodUsuario + "_" + pCodUsuarioRel;
          oButton.Text = oCulture.GetResource("Usuario", "BtnEnviarSolicitudAmistad");
          oButton.Attributes["sType"] = "P";
          oButton.Attributes["CodUsuario"] = oIsUsuario.CodUsuario;
          oButton.Attributes["CodUsuarioRel"] = pCodUsuarioRel;
          oButton.Click += new EventHandler(oButton_Click);
          Controls.Add(oButton);
          Controls.Add(new LiteralControl("</div>"));
        }
      }
      else {
        Controls.Add(new LiteralControl("<div class=\"zonasolicitud\">"));
        Button oButton = new Button();
        oButton.ID = "idBtn_" + oIsUsuario.CodUsuario + "_" + pCodUsuarioRel;
        oButton.Text = oCulture.GetResource("Usuario", "BtnEnviarSolicitudAmistad");
        oButton.Attributes["sType"] = "P";
        oButton.Attributes["CodUsuario"] = oIsUsuario.CodUsuario;
        oButton.Attributes["CodUsuarioRel"] = pCodUsuarioRel;
        oButton.Click += new EventHandler(oButton_Click);
        Controls.Add(oButton);
        Controls.Add(new LiteralControl("</div>"));
      }
      dRelacionUsuarios = null;
    }

    protected void putConfirmFriendRequest()
    {
      
      OnlineServices.Method.Usuario oIsUsuario;
      oIsUsuario = oWeb.GetObjUsuario();

      pCodUsuario = oIsUsuario.CodUsuario;
      Button oButton;
      StringBuilder cSQL = new StringBuilder();
      cSQL.Append(" est_relacion = 'C' ");

      StringBuilder oFolder = new StringBuilder();
      oFolder.Append(Server.MapPath("."));

      string sFile = "RelacionUsuario_" + oIsUsuario.CodUsuario + ".bin";
      DataTable dRelacionUsuarios = oWeb.DeserializarTbl(oFolder.ToString(), sFile);

      if (dRelacionUsuarios != null)
        if (dRelacionUsuarios.Rows.Count > 0)
        {
          DataRow[] oRows = dRelacionUsuarios.Select(cSQL.ToString());
          if (oRows != null)
            if (oRows.Count() > 0)
            {
              Label oLabel;
              Controls.Add(new LiteralControl("<div>"));
              oLabel = new Label();
              oLabel.Text = "Confirma Amistad";
              oLabel.CssClass = "lblConfirmaAmistad";
              Controls.Add(oLabel);
              Controls.Add(new LiteralControl("</div>"));

              RadBinaryImage oRadBinaryImage;
              foreach (DataRow oRow in oRows)
              {
                Controls.Add(new LiteralControl("<div>"));
                oRadBinaryImage = new RadBinaryImage();
                oRadBinaryImage.DataValue = oWeb.getImageProfileUser(oRow["cod_usuario_rel"].ToString(), 150, 150);
                Controls.Add(oRadBinaryImage);
                Controls.Add(new LiteralControl("</div>"));

                Controls.Add(new LiteralControl("<div>"));
                oButton = new Button();
                oButton.ID = "idBtc_" + pCodUsuario + "_" + oRow["cod_usuario_rel"].ToString();
                oButton.Text = oCulture.GetResource("Usuario", "btnConfirmarSolicitudAmistad");
                oButton.Attributes["sType"] = "C";
                oButton.Attributes["CodUsuario"] = pCodUsuario;
                oButton.Attributes["CodUsuarioRel"] = oRow["cod_usuario_rel"].ToString();
                oButton.Click += new EventHandler(oButton_Click);
                Controls.Add(oButton);
                Controls.Add(new LiteralControl("</div>"));

                Controls.Add(new LiteralControl("<div>"));
                oButton = new Button();
                oButton.ID = "idBtd_" + pCodUsuario + "_" + oRow["cod_usuario_rel"].ToString();
                oButton.Text = oCulture.GetResource("Usuario", "btnNoSolicitudAmistad");
                oButton.Attributes["sType"] = "N";
                oButton.Attributes["CodUsuario"] = pCodUsuario;
                oButton.Attributes["CodUsuarioRel"] = oRow["cod_usuario_rel"].ToString();
                oButton.Click += new EventHandler(oButton_Click);
                Controls.Add(oButton);
                Controls.Add(new LiteralControl("</div>"));
              }
            }
          oRows = null;
        }
      dRelacionUsuarios = null;
    }

    void oButton_Click(object sender, EventArgs e)
    {
      bool bExecMail = false;
      string sDireccion = sDireccion = ".";
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        string cPath = Server.MapPath(".");
        string sSubject = string.Empty;
        string sNomApeUsrOrigen = string.Empty;
        string sEmailDestino = string.Empty;
        SysUsuario sUsuario;
        StringBuilder oHtml = new StringBuilder();
        BinaryUsuario dUser;
        StringBuilder sFile = new StringBuilder();
        SysRelacionUsuarios oRelacionUsuarios;
        StringBuilder oFolder = new StringBuilder();
        oFolder.Append(Server.MapPath(".")).Append(@"\binary\");

        string sType = (sender as Button).Attributes["sType"].ToString();
        switch (sType)
        {
          case "P":
            oRelacionUsuarios = new SysRelacionUsuarios(ref oConn);
            oRelacionUsuarios.CodUsuario = (sender as Button).Attributes["CodUsuario"].ToString();
            oRelacionUsuarios.CodUsuarioRel = (sender as Button).Attributes["CodUsuarioRel"].ToString();
            oRelacionUsuarios.EstRelacion = "P";
            oRelacionUsuarios.Accion = "CREAR";
            oRelacionUsuarios.Put();
            sFile.Length = 0;
            sFile.Append("RelacionUsuario_").Append((sender as Button).Attributes["CodUsuario"].ToString()).Append(".bin");
            oRelacionUsuarios.SerializaTblRelacionUsuarios(ref oConn, oFolder.ToString(),sFile.ToString());

            oRelacionUsuarios.CodUsuario = (sender as Button).Attributes["CodUsuarioRel"].ToString();
            oRelacionUsuarios.CodUsuarioRel = (sender as Button).Attributes["CodUsuario"].ToString();
            oRelacionUsuarios.EstRelacion = "C";
            oRelacionUsuarios.Accion = "CREAR";
            oRelacionUsuarios.Put();
            sFile.Length = 0;
            sFile.Append("RelacionUsuario_").Append((sender as Button).Attributes["CodUsuarioRel"].ToString()).Append(".bin");
            oRelacionUsuarios.SerializaTblRelacionUsuarios(ref oConn, oFolder.ToString(), sFile.ToString());

            sUsuario = new SysUsuario();
            sUsuario.Path = cPath;
            sUsuario.CodUsuario = (sender as Button).Attributes["CodUsuario"].ToString();
            dUser = sUsuario.ClassGet();
            if (dUser != null)
              sNomApeUsrOrigen = dUser.NomUsuario + " " + dUser.ApeUsuario; 
            dUser = null;

            sUsuario.CodUsuario = (sender as Button).Attributes["CodUsuarioRel"].ToString();
            dUser = sUsuario.ClassGet();
            if (dUser != null)
                sEmailDestino = dUser.EmlUsuario;
            dUser = null;

            oHtml.Append("<HTML><BODY><p><font face=verdana size=2>").Append(sNomApeUsrOrigen);
            oHtml.Append("<br>").Append(oCulture.GetResource("Mensajes", "sMessage04")).Append("</font></p></BODY></HTML>");

            sSubject = sNomApeUsrOrigen + oCulture.GetResource("Mensajes", "sMessage03") + Application["SiteName"].ToString();
            sDireccion = "Escort.aspx?CodUsuario=" + (sender as Button).Attributes["CodUsuarioRel"].ToString();
            bExecMail = true;
            break;
          case "C":
            oRelacionUsuarios = new SysRelacionUsuarios(ref oConn);
            oRelacionUsuarios.CodUsuario = (sender as Button).Attributes["CodUsuario"].ToString();
            oRelacionUsuarios.CodUsuarioRel = (sender as Button).Attributes["CodUsuarioRel"].ToString();
            oRelacionUsuarios.EstRelacion = "V";
            oRelacionUsuarios.Accion = "EDITAR";
            oRelacionUsuarios.Put();
            sFile.Length = 0;
            sFile.Append("RelacionUsuario_").Append((sender as Button).Attributes["CodUsuario"].ToString()).Append(".bin");
            oRelacionUsuarios.SerializaTblRelacionUsuarios(ref oConn, oFolder.ToString(), sFile.ToString());

            oRelacionUsuarios.CodUsuario = (sender as Button).Attributes["CodUsuarioRel"].ToString(); 
            oRelacionUsuarios.CodUsuarioRel = (sender as Button).Attributes["CodUsuario"].ToString();
            oRelacionUsuarios.EstRelacion = "V";
            oRelacionUsuarios.Accion = "EDITAR";
            oRelacionUsuarios.Put();
            sFile.Length = 0;
            sFile.Append("RelacionUsuario_").Append((sender as Button).Attributes["CodUsuarioRel"].ToString()).Append(".bin");
            oRelacionUsuarios.SerializaTblRelacionUsuarios(ref oConn, oFolder.ToString(), sFile.ToString());

            sUsuario = new SysUsuario();
            sUsuario.Path = cPath;
            sUsuario.CodUsuario = (sender as Button).Attributes["CodUsuario"].ToString();
            dUser = sUsuario.ClassGet();
            if (dUser != null)
              sNomApeUsrOrigen = dUser.NomUsuario + " " + dUser.ApeUsuario;
            dUser = null;

            sUsuario.CodUsuario = (sender as Button).Attributes["CodUsuarioRel"].ToString();
            dUser = sUsuario.ClassGet();
            if (dUser != null)
              sEmailDestino = dUser.EmlUsuario;
            dUser = null;

            oHtml.Append("<HTML><BODY><p><font face=verdana size=2>").Append(sNomApeUsrOrigen);
            oHtml.Append("<br>").Append(oCulture.GetResource("Mensajes", "sMessage05")).Append("</font></p></BODY></HTML>");

            sSubject = sNomApeUsrOrigen + oCulture.GetResource("Mensajes", "sMessage05");
            bExecMail = true;
            break;
          case "N":
            oRelacionUsuarios = new SysRelacionUsuarios(ref oConn);
            oRelacionUsuarios.CodUsuario = (sender as Button).Attributes["CodUsuario"].ToString();
            oRelacionUsuarios.CodUsuarioRel = (sender as Button).Attributes["CodUsuarioRel"].ToString();
            oRelacionUsuarios.Accion = "ELIMINAR";
            oRelacionUsuarios.Put();
            sFile.Length = 0;
            sFile.Append("RelacionUsuario_").Append((sender as Button).Attributes["CodUsuario"].ToString()).Append(".bin");
            oRelacionUsuarios.SerializaTblRelacionUsuarios(ref oConn, oFolder.ToString(), sFile.ToString());

            oRelacionUsuarios.CodUsuario = (sender as Button).Attributes["CodUsuarioRel"].ToString();
            oRelacionUsuarios.CodUsuarioRel = (sender as Button).Attributes["CodUsuario"].ToString();
            oRelacionUsuarios.Accion = "ELIMINAR";
            oRelacionUsuarios.Put();
            sFile.Length = 0;
            sFile.Append("RelacionUsuario_").Append((sender as Button).Attributes["CodUsuarioRel"].ToString()).Append(".bin");
            oRelacionUsuarios.SerializaTblRelacionUsuarios(ref oConn, oFolder.ToString(), sFile.ToString());
            break;
        }
        if (bExecMail)
        {
          Emailing oEmailing = new Emailing();
          oEmailing.FromName = Application["NameSender"].ToString();
          oEmailing.From = Application["EmailSender"].ToString();
          oEmailing.Address = sEmailDestino;
          oEmailing.Subject = sSubject;
          oEmailing.Body = oHtml;
          oEmailing.EmailSend();
        }
        oConn.Close();
      }
      Response.Redirect(sDireccion);
    }

  }
}