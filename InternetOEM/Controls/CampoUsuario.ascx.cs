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
using OnlineServices.Web.UI;
using OnlineServices.Method;
using OnlineServices.SystemData;

namespace ICommunity.Controls
{
  public partial class CampoUsuario : System.Web.UI.UserControl
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      idOpciones.Visible = false;
      idOpcionesTxt.Visible = false;
      if (DataBinder.Eval(this.Parent.NamingContainer, "DataItem.cod_campo") != null) {
        hddCodCampo.Value = DataBinder.Eval(this.Parent.NamingContainer, "DataItem.cod_campo").ToString();
        rdCmbTipo.Items.FindItemByValue(DataBinder.Eval(this.Parent.NamingContainer, "DataItem.tipo_campo").ToString().Trim()).Selected = true;
        string sTipo = DataBinder.Eval(this.Parent.NamingContainer, "DataItem.tipo_campo").ToString().Trim();
        switch (sTipo){
          case "0":
          case "1":
            DBConn oConn = new DBConn();
            if (oConn.Open()) {
              idOpciones.Visible = true;
              StringBuilder sData = new StringBuilder();
              SyrCampoOpciones oCampoOpciones = new SyrCampoOpciones(ref oConn);
              oCampoOpciones.CodCampo = DataBinder.Eval(this.Parent.NamingContainer, "DataItem.cod_campo").ToString();
              DataTable dCampoOpciones = oCampoOpciones.GetOpcionByCodCampos();
              if (dCampoOpciones != null)
                if (dCampoOpciones.Rows.Count > 0) {
                  foreach (DataRow oRow in dCampoOpciones.Rows) {
                    sData.Append(oRow["nom_opcion"].ToString()).Append(Environment.NewLine);
                  }
                  txtAtributos.Text = sData.ToString();
                }
              dCampoOpciones = null;
              if (sTipo == "1")
                chk_multSelect.Checked = (DataBinder.Eval(this.Parent.NamingContainer, "DataItem.desp_campo").ToString().Trim() == "C" ? true : false);
              else
                chk_multSelect.Visible = false;
            }
            break;
          case "2":
            idOpcionesTxt.Visible = true;
            chk_observacion.Checked = (DataBinder.Eval(this.Parent.NamingContainer, "DataItem.desp_campo").ToString().Trim() == "O" ? true : false);
            chk_despl_usr.Checked = (DataBinder.Eval(this.Parent.NamingContainer, "DataItem.ind_despliegue").ToString().Trim() == "V" ? true : false);
            chk_despl_portal.Checked = (DataBinder.Eval(this.Parent.NamingContainer, "DataItem.ind_despliegue_portal").ToString().Trim() == "V" ? true : false);
            chk_ind_validacion.Checked = (DataBinder.Eval(this.Parent.NamingContainer, "DataItem.ind_validacion").ToString().Trim() == "V" ? true : false);
            break;
          case "5":
            idOpcionesTxt.Visible = true;
            chk_observacion.Enabled = false;
            chk_despl_usr.Checked = (DataBinder.Eval(this.Parent.NamingContainer, "DataItem.ind_despliegue").ToString().Trim() == "V" ? true : false);
            chk_despl_portal.Checked = (DataBinder.Eval(this.Parent.NamingContainer, "DataItem.ind_despliegue_portal").ToString().Trim() == "V" ? true : false);
            chk_ind_validacion.Checked = (DataBinder.Eval(this.Parent.NamingContainer, "DataItem.ind_validacion").ToString().Trim() == "V" ? true : false);
            break;
          default:
            break;
        }
      }
    }

    protected void rdCmbTipo_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    { 
      if ((e.Value == "0")||(e.Value == "1"))
        idOpciones.Visible = true;
      else
        idOpciones.Visible = false;
    }
  }
}