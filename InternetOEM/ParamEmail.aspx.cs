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

using OnlineServices.Conn;
using OnlineServices.SystemData;
using OnlineServices.Method;

namespace ICommunity
{
  public partial class ParamEmail : System.Web.UI.Page
  {
    Web oWeb = new Web();
    protected void Page_Load(object sender, EventArgs e)
    {
      oWeb.ValidaSessionAdm();
      if (!IsPostBack)
        getDataEmail(rdCmbEmails.SelectedValue);
    }

    private void getDataEmail(string pTipoEmail) {
      DBConn oConn = new DBConn();
      if (oConn.Open()) {
        SysParamEmail oParamEmail = new SysParamEmail(ref oConn);
        oParamEmail.TipoEmail = pTipoEmail;
        DataTable dParamEmail = oParamEmail.Get();
        if (dParamEmail != null)
          if (dParamEmail.Rows.Count > 0)
          {
            hdd_accion.Value = "EDITAR";
            txtNomEmail.Text = dParamEmail.Rows[0]["nom_email"].ToString();
            txtAsunto.Text = dParamEmail.Rows[0]["asunto_email"].ToString();
            rdCuerpoEmail.Content = dParamEmail.Rows[0]["cuerpo_email"].ToString();
          }
          else
          {
            hdd_accion.Value = "CREAR";
            txtNomEmail.Text = string.Empty;
            txtAsunto.Text = string.Empty;
            rdCuerpoEmail.Content = string.Empty;
          }
        dParamEmail = null;

        oConn.Close();
      }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
      DBConn oConn = new DBConn();
      if (oConn.Open()) {
        SysParamEmail oParamEmail = new SysParamEmail(ref oConn);
        oParamEmail.TipoEmail = rdCmbEmails.SelectedValue;
        oParamEmail.NomEmail = txtNomEmail.Text;
        oParamEmail.AsuntoEmail = txtAsunto.Text;
        oParamEmail.CuerpoEmail = rdCuerpoEmail.Content;
        oParamEmail.Accion = hdd_accion.Value;
        oParamEmail.Put();
        if (string.IsNullOrEmpty(oParamEmail.Error)) {
          oParamEmail.Path = Server.MapPath(".") + @"\binary\";
          oParamEmail.TipoEmail = string.Empty;
          oParamEmail.SerializaParamEmail();
        }
        oConn.Close();
      }
    }

    protected void rdCmbEmails_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
      getDataEmail(rdCmbEmails.SelectedValue);
    }

  }
}
