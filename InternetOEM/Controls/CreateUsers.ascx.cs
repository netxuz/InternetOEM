using System;
using System.Collections;
using System.Configuration;
using System.Globalization;
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
using System.IO;

using Telerik.Web.UI;
using OnlineServices.Conn;
using OnlineServices.SystemData;
using OnlineServices.Method;


namespace ICommunity.Controls
{
  public partial class CreateUsers : System.Web.UI.UserControl
  {
    bool bNoApellido = false;
    bool bNoFono = false;
    bool bUsrSystem = false;
    bool bUsrNormal = false;
    bool bUsrLevel = false;
    bool bEmailOk;
    bool bLoginOk;
    string sErrorMsg = string.Empty;
    Web oWeb = new Web();
    Culture oCulture = new Culture();
    CultureInfo oCultureInfo = new CultureInfo("es-CL");

    protected void Page_Load(object sender, EventArgs e)
    {
      bNoApellido = (Application["NoApellido"].ToString() == "1" ? true : false);
      if (bNoApellido)
        idApellido.Visible = false;

      bNoFono = (Application["NoFono"].ToString() == "1" ? true : false);
      if (bNoFono)
        idFono.Visible = false;

      if (!string.IsNullOrEmpty(this.Attributes["TypeUsr"]))
      {
        bUsrNormal = (this.Attributes["TypeUsr"].ToString() == "0" ? true : false);
        bUsrLevel = (this.Attributes["TypeUsr"].ToString() == "1" ? true : false);

        if ((Session["SaveUser"] != null) && (Session["SaveUser"].ToString() == "1"))
        {
          StringBuilder js = new StringBuilder();
          js.Append("function LgRespuesta() {");
          js.Append(" window.radalert('").Append(oCulture.GetResource("Mensajes", "sMessage15")).Append("', 300, 100,'" + oCulture.GetResource("Global", "MnsAtencion") + "'); ");
          js.Append(" Sys.Application.remove_load(LgRespuesta); ");
          js.Append("};");
          js.Append("Sys.Application.add_load(LgRespuesta);");
          Page.ClientScript.RegisterStartupScript(this.GetType(), "LgRespuesta", js.ToString(), true);

          Session["SaveUser"] = string.Empty;
        }
      }
      else
        bUsrSystem = true;

      //if (!IsPostBack)
      //{
      if (string.IsNullOrEmpty(CodUsuario.Value))
        if (!string.IsNullOrEmpty(this.Attributes["CodUsuario"]))
          CodUsuario.Value = this.Attributes["CodUsuario"];
        else
          CodUsuario.Value = oWeb.GetData("CodUsuario");

      btnGrabar.Text = oCulture.GetResource("Global", "btnGrabar");
      btnGrabar.ToolTip = oCulture.GetResource("Global", "btnGrabar");
      lblNomUsuario.Text = oCulture.GetResource("Usuario", "NomUsuario");
      if (!bNoApellido)
        lblApeUsuario.Text = oCulture.GetResource("Usuario", "ApeUsuario");
      lblLogin.Text = oCulture.GetResource("Usuario", "LoginUsuario");
      lblClave.Text = oCulture.GetResource("Usuario", "ClaveUsuario");
      lblRepClave.Text = oCulture.GetResource("Usuario", "RepClaveUsuario");
      lblEmlUsuario.Text = oCulture.GetResource("Usuario", "EmlUsuario");
      lblTipoUsuario.Text = oCulture.GetResource("Usuario", "TipoUsuario");
      lblEstUsuario.Text = oCulture.GetResource("Global", "Estado");
      if (bNoFono)
        lblFonoUsuario.Text = oCulture.GetResource("Usuario", "FonoUsuario");
      lblNumeroCliente.Text = oCulture.GetResource("Usuario", "NumeroCliente");
      lblCertificado.Text = oCulture.GetResource("Usuario", "IndCertificado");

      if (bUsrSystem)
      {
        lblDestUsuario.Text = oCulture.GetResource("Global", "Destacado");
        rdCmbTipoUsuario.Items.Add(new ListItem(oCulture.GetResource("Usuario", "NomTipo1"), "1"));
      }
      if ((bUsrSystem) || (bUsrLevel))
      {
        rdCmbTipoUsuario.Items.Add(new ListItem(oCulture.GetResource("Usuario", "NomTipo2"), "2"));
        rdCmbTipoUsuario.Items.Add(new ListItem(oCulture.GetResource("Usuario", "NomTipo3"), "3"));
        rdCmbTipoUsuario.Items.Add(new ListItem(oCulture.GetResource("Usuario", "NomTipo4"), "4"));
      }
      if (bUsrNormal)
      {
        idTipoUsuario.Visible = false;
      }

      rdCmbEstUsuario.Items.Add(new ListItem(oCulture.GetResource("Global", "Vigente"), "V"));
      rdCmbEstUsuario.Items.Add(new ListItem(oCulture.GetResource("Global", "NoVigente"), "N"));
      rdCmbEstUsuario.Items.Add(new ListItem(oCulture.GetResource("Global", "Bloqueado"), "B"));
      rdCmbEstUsuario.Items.Add(new ListItem(oCulture.GetResource("Global", "Eliminado"), "E"));

      if (!bUsrSystem)
      {
        idDestacado.Visible = false;
        idEstUsuario.Visible = false;
        idCertificado.Visible = false;
      }

      //}
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        if ((!string.IsNullOrEmpty(CodUsuario.Value)) && (!IsPostBack))
        {
          SysUsuario oUsuario = new SysUsuario(ref oConn);
          oUsuario.CodUsuario = CodUsuario.Value;
          DataTable dUsuario = oUsuario.Get();
          if (dUsuario != null)
          {
            if (dUsuario.Rows.Count > 0)
            {
              txtLoginAlmacenado.Visible = true;
              txtNomUsuario.Text = dUsuario.Rows[0]["nom_user"].ToString();
              if (!bNoApellido)
                txtApeUsuario.Text = dUsuario.Rows[0]["ape_user"].ToString();
              txtLoginAlmacenado.Text = dUsuario.Rows[0]["login_user"].ToString();
              txtLoginAlmacenado.Enabled = false;
              //txtLogin.Enabled = false;
              txtLogin.Visible = false;
              valLogin.Enabled = false;
              bLoginOk = true;
              txtClave.Attributes.Add("value", oWeb.UnCrypt(dUsuario.Rows[0]["pwd_user"].ToString()));
              txtRepClave.Attributes.Add("value", oWeb.UnCrypt(dUsuario.Rows[0]["pwd_user"].ToString()));
              txtEmlUsuario.Text = dUsuario.Rows[0]["eml_user"].ToString();
              if (bUsrSystem)
              {
                check_destacado.Checked = (dUsuario.Rows[0]["destacado_usuario"].ToString() == "V" ? true : false);
                rdCmbEstUsuario.Items.FindByValue(dUsuario.Rows[0]["est_user"].ToString()).Selected = true;
                check_certificado.Checked = (dUsuario.Rows[0]["ind_validado"].ToString() == "V" ? true : false);
              }
              if ((bUsrSystem) || (bUsrLevel))
                rdCmbTipoUsuario.Items.FindByValue(dUsuario.Rows[0]["cod_tipo"].ToString()).Selected = true;
              txtFonoUsuario.Text = dUsuario.Rows[0]["fono_usuario"].ToString();
              txtNumeroCliente.Text = dUsuario.Rows[0]["nkey_user"].ToString();
            }
          }
        }
        else if (!string.IsNullOrEmpty(CodUsuario.Value))
        {
          bLoginOk = true;
        }
        #region Campos Dinamicos

        DataTable dCampoUsuarios;
        Label lblTitulo;
        TextBox txtCampo;
        CheckBox oCheckBox;
        RadComboBox rdComboBox;
        RadDatePicker rdDatePicker;
        RadNumericTextBox rdNumTxtCampo;
        RequiredFieldValidator oRequiredFieldValidator;
        DataTable dInfUsr;
        DataTable dDataOpc;
        DataTable dCampoOpciones;
        SyrInfoUsuarios oInfoUsuarios;
        SysOpcionesCampo oOpcionesCampo;
        SyrCampoOpciones oCampoOpciones;
        SysCampoUsuarios oCampoUsuarios = new SysCampoUsuarios(ref oConn);
        if ((bUsrSystem) || (bUsrLevel))
          dCampoUsuarios = oCampoUsuarios.Get();
        else
        {
          oCampoUsuarios.IndDespliegue = "V";
          dCampoUsuarios = oCampoUsuarios.Get();
        }
        if (dCampoUsuarios != null)
          if (dCampoUsuarios.Rows.Count > 0)
          {
            foreach (DataRow oRow in dCampoUsuarios.Rows)
            {
              idUsrInf.Controls.Add(new LiteralControl("<div class=\"dCampo\">"));
              idUsrInf.Controls.Add(new LiteralControl("<div class=\"dLabel\">"));
              lblTitulo = new Label();
              lblTitulo.ID = "lbl_" + oRow["cod_campo"].ToString();
              lblTitulo.Text = oRow["nom_campo"].ToString();
              lblTitulo.CssClass = "cLabel";
              idUsrInf.Controls.Add(lblTitulo);
              idUsrInf.Controls.Add(new LiteralControl("</div>"));
              idUsrInf.Controls.Add(new LiteralControl("<div class=\"dControl\">"));
              switch (oRow["tipo_campo"].ToString().Trim())
              {
                case "0":
                  rdComboBox = new RadComboBox();
                  rdComboBox.ID = "oCampo_" + oRow["cod_campo"].ToString();
                  rdComboBox.CssClass = "cControl";
                  idUsrInf.Controls.Add(rdComboBox);
                  oCampoOpciones = new SyrCampoOpciones(ref oConn);
                  oCampoOpciones.CodCampo = oRow["cod_campo"].ToString();
                  dCampoOpciones = oCampoOpciones.GetOpcionByCodCampos();
                  if (dCampoOpciones != null)
                    if (dCampoOpciones.Rows.Count > 0)
                    {
                      rdComboBox.Items.Add(new RadComboBoxItem("Seleccione Dato", ""));
                      foreach (DataRow dRow in dCampoOpciones.Rows)
                      {
                        rdComboBox.Items.Add(new RadComboBoxItem(dRow["nom_opcion"].ToString(), dRow["cod_opcion"].ToString()));
                      }
                    }
                  dCampoOpciones = null;

                  if (!string.IsNullOrEmpty(CodUsuario.Value) && (!IsPostBack))
                  {
                    oInfoUsuarios = new SyrInfoUsuarios(ref oConn);
                    oInfoUsuarios.CodCampo = oRow["cod_campo"].ToString();
                    oInfoUsuarios.CodUsuario = CodUsuario.Value;
                    dInfUsr = oInfoUsuarios.Get();
                    if (dInfUsr != null)
                      if (dInfUsr.Rows.Count > 0)
                      {
                        oOpcionesCampo = new SysOpcionesCampo(ref oConn);
                        oOpcionesCampo.CodOpcion = dInfUsr.Rows[0]["val_campo"].ToString();
                        dDataOpc = oOpcionesCampo.Get();
                        if (dDataOpc != null)
                          if (dDataOpc.Rows.Count > 0)
                          {
                            rdComboBox.Items.FindItemByValue(dDataOpc.Rows[0]["cod_opcion"].ToString()).Selected = true;
                          }
                        dDataOpc = null;
                      }
                    dInfUsr = null;

                  }
                  break;
                case "1":
                  switch (oRow["desp_campo"].ToString())
                  {
                    case "M":

                      break;
                    case "C":
                      oCampoOpciones = new SyrCampoOpciones(ref oConn);
                      oCampoOpciones.CodCampo = oRow["cod_campo"].ToString();
                      dCampoOpciones = oCampoOpciones.GetOpcionByCodCampos();
                      if (dCampoOpciones != null)
                        if (dCampoOpciones.Rows.Count > 0)
                        {
                          foreach (DataRow dRow in dCampoOpciones.Rows)
                          {
                            idUsrInf.Controls.Add(new LiteralControl("<div class=\"dControlCheck\">"));
                            oCheckBox = new CheckBox();
                            oCheckBox.ID = "oCheck_" + oRow["cod_campo"].ToString() + "_" + dRow["cod_opcion"].ToString();
                            oCheckBox.Text = dRow["nom_opcion"].ToString();

                            if (!string.IsNullOrEmpty(CodUsuario.Value) && (!IsPostBack))
                            {
                              oInfoUsuarios = new SyrInfoUsuarios(ref oConn);
                              oInfoUsuarios.CodUsuario = CodUsuario.Value;
                              oInfoUsuarios.CodCampo = oRow["cod_campo"].ToString();
                              oInfoUsuarios.ValCampo = dRow["cod_opcion"].ToString();
                              dInfUsr = oInfoUsuarios.Get();
                              if (dInfUsr != null)
                                if (dInfUsr.Rows.Count > 0)
                                  oCheckBox.Checked = true;
                              dInfUsr = null;
                            }
                            idUsrInf.Controls.Add(oCheckBox);
                            idUsrInf.Controls.Add(new LiteralControl("</div>"));
                          }
                        }
                      dCampoOpciones = null;
                      break;
                  }
                  break;
                case "2":
                  txtCampo = new TextBox();
                  txtCampo.ID = "oCampo_" + oRow["cod_campo"].ToString();
                  txtCampo.CssClass = "cControl";
                  if (oRow["desp_campo"].ToString() == "O")
                    txtCampo.TextMode = TextBoxMode.MultiLine;
                  idUsrInf.Controls.Add(txtCampo);

                  if ((oRow["ind_validacion"].ToString() == "V") && (!bUsrSystem))
                  {
                    oRequiredFieldValidator = new RequiredFieldValidator();
                    oRequiredFieldValidator.ID = "oRequiredFieldValidator_" + oRow["cod_campo"].ToString();
                    oRequiredFieldValidator.ControlToValidate = txtCampo.ID;
                    oRequiredFieldValidator.Display = ValidatorDisplay.Static;
                    oRequiredFieldValidator.ErrorMessage = "*";
                    idUsrInf.Controls.Add(oRequiredFieldValidator);
                  }

                  if (!string.IsNullOrEmpty(CodUsuario.Value) && (!IsPostBack))
                  {
                    oInfoUsuarios = new SyrInfoUsuarios(ref oConn);
                    oInfoUsuarios.CodCampo = oRow["cod_campo"].ToString();
                    oInfoUsuarios.CodUsuario = CodUsuario.Value;
                    dInfUsr = oInfoUsuarios.Get();
                    if (dInfUsr != null)
                      if (dInfUsr.Rows.Count > 0)
                      {
                        if (oRow["desp_campo"].ToString() == "O")
                          txtCampo.Text = dInfUsr.Rows[0]["text_campo"].ToString();
                        else
                          txtCampo.Text = dInfUsr.Rows[0]["val_campo"].ToString();
                      }
                    dInfUsr = null;
                  }
                  break;
                case "4":
                  rdDatePicker = new RadDatePicker();
                  rdDatePicker.ID = "oCampo_" + oRow["cod_campo"].ToString();
                  rdDatePicker.MinDate = DateTime.Parse("1900-01-01");
                  rdDatePicker.Culture = oCultureInfo;
                  rdDatePicker.CssClass = "cControl";
                  rdDatePicker.Calendar.CultureInfo = oCultureInfo;
                  rdDatePicker.Calendar.FirstDayOfWeek = FirstDayOfWeek.Monday;
                  idUsrInf.Controls.Add(rdDatePicker);

                  if (!string.IsNullOrEmpty(CodUsuario.Value) && (!IsPostBack))
                  {
                    oInfoUsuarios = new SyrInfoUsuarios(ref oConn);
                    oInfoUsuarios.CodCampo = oRow["cod_campo"].ToString();
                    oInfoUsuarios.CodUsuario = CodUsuario.Value;
                    dInfUsr = oInfoUsuarios.Get();
                    if (dInfUsr != null)
                      if (dInfUsr.Rows.Count > 0)
                      {
                        if (!string.IsNullOrEmpty(dInfUsr.Rows[0]["val_campo"].ToString()))
                          rdDatePicker.SelectedDate = DateTime.Parse(dInfUsr.Rows[0]["val_campo"].ToString());
                      }
                    dInfUsr = null;
                  }
                  break;
                case "5":
                  rdNumTxtCampo = new RadNumericTextBox();
                  rdNumTxtCampo.ID = "oCampo_" + oRow["cod_campo"].ToString();
                  rdNumTxtCampo.CssClass = "cControl";
                  rdNumTxtCampo.Culture = oCultureInfo;
                  rdNumTxtCampo.NumberFormat.DecimalDigits = 0;
                  idUsrInf.Controls.Add(rdNumTxtCampo);

                  if ((oRow["ind_validacion"].ToString() == "V") && (!bUsrSystem))
                  {
                    oRequiredFieldValidator = new RequiredFieldValidator();
                    oRequiredFieldValidator.ID = "oRequiredFieldValidator_" + oRow["cod_campo"].ToString();
                    oRequiredFieldValidator.ControlToValidate = rdNumTxtCampo.ID;
                    oRequiredFieldValidator.Display = ValidatorDisplay.Static;
                    oRequiredFieldValidator.ErrorMessage = "*";
                    idUsrInf.Controls.Add(oRequiredFieldValidator);
                  }

                  if (!string.IsNullOrEmpty(CodUsuario.Value) && (!IsPostBack))
                  {
                    oInfoUsuarios = new SyrInfoUsuarios(ref oConn);
                    oInfoUsuarios.CodCampo = oRow["cod_campo"].ToString();
                    oInfoUsuarios.CodUsuario = CodUsuario.Value;
                    dInfUsr = oInfoUsuarios.Get();
                    if (dInfUsr != null)
                      if (dInfUsr.Rows.Count > 0)
                        rdNumTxtCampo.Text = dInfUsr.Rows[0]["val_campo"].ToString();
                    dInfUsr = null;
                  }

                  break;
                case "6":
                  txtCampo = new TextBox();
                  txtCampo.ID = "oCampo_" + oRow["cod_campo"].ToString();
                  txtCampo.CssClass = "cControl";
                  idUsrInf.Controls.Add(txtCampo);

                  if (!string.IsNullOrEmpty(CodUsuario.Value) && (!IsPostBack))
                  {
                    oInfoUsuarios = new SyrInfoUsuarios(ref oConn);
                    oInfoUsuarios.CodCampo = oRow["cod_campo"].ToString();
                    oInfoUsuarios.CodUsuario = CodUsuario.Value;
                    dInfUsr = oInfoUsuarios.Get();
                    if (dInfUsr != null)
                      if (dInfUsr.Rows.Count > 0)
                      {
                        txtCampo.Text = dInfUsr.Rows[0]["val_campo"].ToString();
                      }
                    dInfUsr = null;
                  }
                  break;
              }
              idUsrInf.Controls.Add(new LiteralControl("</div>"));
              idUsrInf.Controls.Add(new LiteralControl("</div>"));
            }
            dCampoUsuarios = null;
          }
          else
          {
            RadPanelBar oRadPanelBar = (this.FindControl("RadPanelBar") as RadPanelBar);
            if (oRadPanelBar != null)
              oRadPanelBar.Items[1].Visible = false;
          }
        oConn.Close();

        #endregion
      }
    }

    protected void ServerValidationEml(object source, ServerValidateEventArgs args)
    {
      try
      {
        bool bValida = false;
        if (oWeb.ValidaMail(args.Value))
        {
          DBConn oConn = new DBConn();
          if (oConn.Open())
          {
            SysUsuario oUsuario = new SysUsuario(ref oConn);
            oUsuario.NotInCodUsr = CodUsuario.Value;
            oUsuario.NotInUsr = true;
            oUsuario.EmlUsuario = args.Value;
            DataTable dUsuario = oUsuario.Get();
            if (dUsuario != null)
              if (dUsuario.Rows.Count == 0)
                bValida = true;
              else
                sErrorMsg = "sError04";
            dUsuario = null;
            oConn.Close();
          }
        }
        else
          sErrorMsg = "sError01";
        bEmailOk = args.IsValid = bValida;
      }
      catch
      {
        sErrorMsg = "sError05";
        bEmailOk = args.IsValid = false;
      }

    }

    protected void ServerValidationLogin(object source, ServerValidateEventArgs args)
    {
      try
      {
        bool bValida = false;
        if (string.IsNullOrEmpty(CodUsuario.Value))
        {
          DBConn oConn = new DBConn();
          if (oConn.Open())
          {
            SysUsuario oUsuario = new SysUsuario(ref oConn);
            oUsuario.LoginUsuario = txtLogin.Text;
            DataTable dUsuario = oUsuario.Get();
            if (dUsuario != null)
              if (dUsuario.Rows.Count == 0)
                bValida = true;
              else
                sErrorMsg = "sError02";
            dUsuario = null;
            oConn.Close();
          }
        }
        else
          bValida = true;

        bLoginOk = args.IsValid = bValida;
      }
      catch
      {
        sErrorMsg = "sError05";
        bEmailOk = args.IsValid = false;
      }
    }

    protected void btnGrabar_Click(object sender, EventArgs e)
    {
      if (Page.IsValid)
      {
        //if ((bEmailOk) && (bLoginOk))
        if (bLoginOk)
        {
          bool bAccion = true;
          string sAccion = string.Empty;
          string pCodUsuario = string.Empty;
          DBConn oConn = new DBConn();
          StringBuilder sError = new StringBuilder();

          if (oConn.Open())
          {
            oConn.BeginTransaction();

            if (!string.IsNullOrEmpty(CodUsuario.Value))
            {
              pCodUsuario = CodUsuario.Value;
              bAccion = false;
            }

            string sClave = oWeb.Crypt(txtClave.Text);
            SysUsuario oUsuario = new SysUsuario(ref oConn);
            oUsuario.CodUsuario = pCodUsuario;
            oUsuario.NomUsuario = txtNomUsuario.Text;
            if (!bNoApellido)
              oUsuario.ApeUsuario = txtApeUsuario.Text;
            oUsuario.LoginUsuario = txtLogin.Text;
            oUsuario.PwdUsuario = sClave;
            oUsuario.EmlUsuario = txtEmlUsuario.Text;
            if (!bUsrSystem)
            {
              if (bAccion)
              {
                oUsuario.DestacadoUsuario = string.Empty;
                oUsuario.IndValidado = string.Empty;
              }
              oUsuario.EstUsuario = "V";
            }
            else
            {
              oUsuario.DestacadoUsuario = (check_destacado.Checked ? "V" : "N");
              oUsuario.EstUsuario = rdCmbEstUsuario.SelectedValue;
              oUsuario.IndValidado = (check_certificado.Checked ? "V" : "N");
            }
            if (bNoFono)
              oUsuario.FonoUsuario = txtFonoUsuario.Text;
            oUsuario.NumeroCliente = txtNumeroCliente.Text;
            if (bUsrNormal)
            {
              oUsuario.CodTipo = "1";
            }
            else
              oUsuario.CodTipo = rdCmbTipoUsuario.SelectedValue;
            oUsuario.Accion = (bAccion ? "CREAR" : "EDITAR");
            oUsuario.Put();
            sError.Append(oUsuario.Error);

            pCodUsuario = oUsuario.CodUsuario;
            CodUsuario.Value = pCodUsuario;
            if (sError.Length == 0)
            {
              if ((Session["USUARIO"] == null) || (string.IsNullOrEmpty(Session["USUARIO"].ToString())))
              {
                OnlineServices.Method.Usuario oIsUsuario;
                oIsUsuario = oWeb.GetObjUsuario();
                oIsUsuario.CodUsuario = pCodUsuario;
                oIsUsuario.Tipo = oUsuario.CodTipo;
                oIsUsuario.Nombres = (txtNomUsuario.Text + " " + txtApeUsuario.Text).Trim();
                oIsUsuario.Email = txtEmlUsuario.Text;
                oIsUsuario.Fono = txtFonoUsuario.Text;
                Session["USUARIO"] = oIsUsuario;
              }

              string cPath = Server.MapPath(".") + @"\binary\";
              string sFile = "Usuarios.bin";
              oUsuario.SerializaTblUsuario(ref oConn, cPath, sFile);
              string sFileUsr = "Usuario_" + pCodUsuario + ".bin";
              oUsuario.SerializaUsuario(ref oConn, cPath, sFileUsr);

              #region Campos Dinamicos
              SyrInfoUsuarios oInfoUsuarios;
              oInfoUsuarios = new SyrInfoUsuarios(ref oConn);
              oInfoUsuarios.CodUsuario = pCodUsuario;
              oInfoUsuarios.Accion = "ELIMINAR";
              oInfoUsuarios.Put();
              sError.Append(oInfoUsuarios.Error);

              if (sError.Length == 0)
              {
                DataTable dCampoUsuarios;
                SysCampoUsuarios oCampoUsuarios = new SysCampoUsuarios(ref oConn);
                if ((bUsrSystem) || (bUsrLevel))
                  dCampoUsuarios = oCampoUsuarios.Get();
                else
                {
                  oCampoUsuarios.IndDespliegue = "V";
                  dCampoUsuarios = oCampoUsuarios.Get();
                }
                if (dCampoUsuarios != null)
                  if (dCampoUsuarios.Rows.Count > 0)
                  {
                    foreach (DataRow oRow in dCampoUsuarios.Rows)
                    {

                      oInfoUsuarios = new SyrInfoUsuarios(ref oConn);
                      oInfoUsuarios.CodUsuario = pCodUsuario;
                      oInfoUsuarios.CodCampo = oRow["cod_campo"].ToString();
                      oInfoUsuarios.Accion = "CREAR";
                      switch (oRow["tipo_campo"].ToString().Trim())
                      {
                        case "0":
                          if (Page.FindControl(((this.FindControl("RadPanelBar") as RadPanelBar)).Items[1].UniqueID + "$oCampo_" + oRow["cod_campo"]) != null)
                          {
                            oInfoUsuarios.ValCampo = (Page.FindControl(((this.FindControl("RadPanelBar") as RadPanelBar)).Items[1].UniqueID + "$oCampo_" + oRow["cod_campo"]) as RadComboBox).SelectedValue;
                            oInfoUsuarios.Put();
                            sError.Append(oInfoUsuarios.Error);
                            if (!string.IsNullOrEmpty(oInfoUsuarios.Error))
                              sError.Append("\n");
                          }
                          break;
                        case "1":
                          SyrCampoOpciones oCampoOpciones = new SyrCampoOpciones(ref oConn);
                          oCampoOpciones.CodCampo = oRow["cod_campo"].ToString();
                          DataTable dCampoOpciones = oCampoOpciones.GetOpcionByCodCampos();
                          if (dCampoOpciones != null)
                            if (dCampoOpciones.Rows.Count > 0)
                            {
                              foreach (DataRow dRow in dCampoOpciones.Rows)
                              {
                                if (Page.FindControl(((this.FindControl("RadPanelBar") as RadPanelBar)).Items[1].UniqueID + "$oCheck_" + oRow["cod_campo"] + "_" + dRow["cod_opcion"].ToString()) != null)
                                {
                                  if ((Page.FindControl(((this.FindControl("RadPanelBar") as RadPanelBar)).Items[1].UniqueID + "$oCheck_" + oRow["cod_campo"] + "_" + dRow["cod_opcion"].ToString()) as CheckBox).Checked)
                                  {
                                    oInfoUsuarios.ValCampo = dRow["cod_opcion"].ToString();
                                    oInfoUsuarios.TipCampo = "C";
                                    oInfoUsuarios.Put();
                                    sError.Append(oInfoUsuarios.Error);
                                    if (!string.IsNullOrEmpty(oInfoUsuarios.Error))
                                      sError.Append("\n");
                                  }
                                }
                              }
                            }
                          dCampoOpciones = null;
                          break;
                        case "2":
                          if (Page.FindControl(((this.FindControl("RadPanelBar") as RadPanelBar)).Items[1].UniqueID + "$oCampo_" + oRow["cod_campo"]) != null)
                          {
                            if (oRow["desp_campo"].ToString() == "O")
                              oInfoUsuarios.TextCampo = (Page.FindControl(((this.FindControl("RadPanelBar") as RadPanelBar)).Items[1].UniqueID + "$oCampo_" + oRow["cod_campo"]) as TextBox).Text;
                            else
                              oInfoUsuarios.ValCampo = (Page.FindControl(((this.FindControl("RadPanelBar") as RadPanelBar)).Items[1].UniqueID + "$oCampo_" + oRow["cod_campo"]) as TextBox).Text;
                            oInfoUsuarios.Put();
                            sError.Append(oInfoUsuarios.Error);
                            if (!string.IsNullOrEmpty(oInfoUsuarios.Error))
                              sError.Append("\n");
                          }
                          break;
                        case "4":
                          if (Page.FindControl(((this.FindControl("RadPanelBar") as RadPanelBar)).Items[1].UniqueID + "$oCampo_" + oRow["cod_campo"]) != null)
                          {
                            oInfoUsuarios.ValCampo = (Page.FindControl(((this.FindControl("RadPanelBar") as RadPanelBar)).Items[1].UniqueID + "$oCampo_" + oRow["cod_campo"]) as RadDatePicker).SelectedDate.ToString();
                            oInfoUsuarios.Put();
                            sError.Append(oInfoUsuarios.Error);
                            if (!string.IsNullOrEmpty(oInfoUsuarios.Error))
                              sError.Append("\n");
                          }
                          break;
                        case "5":
                          if (Page.FindControl(((this.FindControl("RadPanelBar") as RadPanelBar)).Items[1].UniqueID + "$oCampo_" + oRow["cod_campo"]) != null)
                          {
                            oInfoUsuarios.ValCampo = (Page.FindControl(((this.FindControl("RadPanelBar") as RadPanelBar)).Items[1].UniqueID + "$oCampo_" + oRow["cod_campo"]) as RadNumericTextBox).Value.ToString();
                            oInfoUsuarios.Put();
                            sError.Append(oInfoUsuarios.Error);
                            if (!string.IsNullOrEmpty(oInfoUsuarios.Error))
                              sError.Append("\n");
                          }
                          break;
                        case "6":
                          if (Page.FindControl(((this.FindControl("RadPanelBar") as RadPanelBar)).Items[1].UniqueID + "$oCampo_" + oRow["cod_campo"]) != null)
                          {
                            oInfoUsuarios.ValCampo = (Page.FindControl(((this.FindControl("RadPanelBar") as RadPanelBar)).Items[1].UniqueID + "$oCampo_" + oRow["cod_campo"]) as TextBox).Text;
                            oInfoUsuarios.Put();
                            sError.Append(oInfoUsuarios.Error);
                            if (!string.IsNullOrEmpty(oInfoUsuarios.Error))
                              sError.Append("\n");
                          }
                          break;
                      }
                    }
                  }
                dCampoUsuarios = null;
              }

              oInfoUsuarios.SerializaTblInfoUsuario(ref oConn, cPath, "InfoUsuario_" + pCodUsuario + ".bin");

              #endregion



              if (sError.Length == 0)
              {
                oConn.Commit();

                if (bAccion)
                {
                  StringBuilder sPath = new StringBuilder();
                  StringBuilder sAsunto = new StringBuilder();
                  StringBuilder oHtml = new StringBuilder();
                  sPath.Append(Server.MapPath("."));
                  DataTable dParamEmail = oWeb.DeserializarTbl(sPath.ToString(), "ParamEmail.bin");
                  if (dParamEmail != null)
                    if (dParamEmail.Rows.Count > 0)
                    {
                      DataRow[] oRows;
                      if (bUsrNormal)
                        oRows = dParamEmail.Select(" tipo_email = 'B' ");
                      else
                        oRows = dParamEmail.Select(" tipo_email = 'C' ");
                      if (oRows != null)
                      {
                        if (oRows.Count() > 0)
                        {
                          sAsunto.Append(oRows[0]["asunto_email"].ToString());
                          sAsunto.Replace("[NOMBRESITIO]", Application["SiteName"].ToString());
                          oHtml.Append(oRows[0]["cuerpo_email"].ToString());
                          oHtml.Replace("[NOMBRE]", txtNomUsuario.Text);
                          oHtml.Replace("[SITIO]", "http://" + Request.ServerVariables["HTTP_HOST"].ToString());
                          oHtml.Replace("[NOMBRESITIO]", Application["SiteName"].ToString());
                          oHtml.Replace("[LOGINUSUARIO]", txtLogin.Text);
                          oHtml.Replace("[PASSWORD]", txtClave.Text);

                          //Email al usuario que se registro.
                          Emailing oEmailing;
                          oEmailing = new Emailing();
                          oEmailing.FromName = Application["NameSender"].ToString();
                          oEmailing.From = Application["EmailSender"].ToString();
                          oEmailing.Address = txtEmlUsuario.Text;
                          oEmailing.Subject = (!string.IsNullOrEmpty(sAsunto.ToString()) ? sAsunto.ToString() : oCulture.GetResource("Mensajes", "sMessage08") + Application["SiteName"].ToString());
                          oEmailing.Body = oHtml;
                          oEmailing.EmailSend();
                          if (!string.IsNullOrEmpty(oEmailing.Error))
                            sError.Append(oEmailing.Error).Append(" - ");

                          //Email al usuario administrador del sistema.
                          oEmailing = new Emailing();
                          oEmailing.FromName = Application["NameSender"].ToString();
                          oEmailing.From = Application["EmailSender"].ToString();
                          oEmailing.Address = Application["EmailSender"].ToString();
                          oEmailing.Subject = (!string.IsNullOrEmpty(sAsunto.ToString()) ? sAsunto.ToString() : oCulture.GetResource("Mensajes", "sMessage08") + Application["SiteName"].ToString());
                          oEmailing.Body = oHtml;
                          oEmailing.EmailSend();
                          if (!string.IsNullOrEmpty(oEmailing.Error))
                            sError.Append(oEmailing.Error).Append(" - ");

                        }
                      }
                      oRows = null;
                    }
                  dParamEmail = null;
                }

                if ((bUsrNormal) || (bUsrLevel))
                {
                  if (bUsrNormal)
                  {
                    Session["SaveUser"] = "1";
                  }
                  else
                  {
                    if (bAccion)
                    {
                      DataTable oParam = oWeb.DeserializarTbl(Server.MapPath("."), "parametros.bin");
                      if (oParam != null)
                        if (oParam.Rows.Count > 0)
                        {
                          DataRow[] oRows = oParam.Select(" cod_codigo = '11' ");
                          if (oRows != null)
                            if (oRows.Count() > 0)
                              if (oRows[0]["valor_parametro"].ToString() == "1")
                              {
                                DataTable oNodos = oWeb.DeserializarTbl(Server.MapPath("."), "Nodos.bin");
                                if (oNodos != null)
                                  if (oNodos.Rows.Count > 0)
                                  {
                                    DataRow[] oNodRows = oNodos.Select(" ind_pagexito_nodo = 'V' ");
                                    if (oNodRows != null)
                                      if (oNodRows.Count() > 0)
                                        Session["CodNodo"] = oNodRows[0]["cod_nodo"].ToString();
                                    oNodRows = null;
                                  }
                                oNodos = null;
                              }
                          oRows = null;
                        }
                      oParam = null;
                    }
                  }
                  Response.Redirect(".");
                }
                else
                {
                  StringBuilder js = new StringBuilder();
                  js.Append("function LgRespuestaOK() {");
                  js.Append(" window.radalert('").Append(oCulture.GetResource("Mensajes", "sMessage15")).Append("', 300, 100,'" + oCulture.GetResource("Global", "MnsAtencion") + "'); ");
                  js.Append(" Sys.Application.remove_load(LgRespuestaOK); ");
                  js.Append("};");
                  js.Append("Sys.Application.add_load(LgRespuestaOK);");
                  Page.ClientScript.RegisterStartupScript(Page.GetType(), "LgRespuestaOK", js.ToString(), true);
                }
              }
              else
              {
                oConn.Rollback();
                StringBuilder js = new StringBuilder();
                js.Append("function LgRespuestaNoOk() {");
                js.Append(" window.radalert('").Append(oCulture.GetResource("Mensajes", "sMessage16")).Append("', 300, 100,'" + oCulture.GetResource("Global", "MnsAtencion") + "'); ");
                js.Append(" Sys.Application.remove_load(LgRespuestaNoOk); ");
                js.Append("};");
                js.Append("Sys.Application.add_load(LgRespuestaNoOk);");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "LgRespuestaNoOk", js.ToString(), true);
              }
            }
            oConn.Close();
          }
        }
        else
        {
          string sErrorMsn = string.Empty;
          if (!bEmailOk)
            sErrorMsn = oCulture.GetResource("Error", sErrorMsg);
          else if (!bLoginOk)
            sErrorMsn = oCulture.GetResource("Error", sErrorMsg);

          StringBuilder js = new StringBuilder();
          js.Append("function LgRespuesta() {");
          js.Append(" window.radalert('").Append(sErrorMsn).Append("', 300, 100,'" + oCulture.GetResource("Global", "MnsAtencion") + "'); ");
          js.Append(" Sys.Application.remove_load(LgRespuesta); ");
          js.Append("};");
          js.Append("Sys.Application.add_load(LgRespuesta);");
          Page.ClientScript.RegisterStartupScript(this.GetType(), "LgRespuesta", js.ToString(), true);

        }
      }
      else
      {
        if (!string.IsNullOrEmpty(sErrorMsg))
          valSum.HeaderText = oCulture.GetResource("Error", sErrorMsg);
        //lblErrorMsg.Text = "Existen campos que debes llenar (*)";
      }
    }

    protected void CreateHtml(string sCodUsuario)
    {

      string cPath = Server.MapPath(".") + @"\Resources\template.txt";
      string cPathHtml = Server.MapPath(".") + @"Usuarios\" + sCodUsuario;
      StringBuilder strFileHtml = new StringBuilder();


    }
  }

}