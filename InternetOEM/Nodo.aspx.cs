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

using Telerik.Web.UI;
using OnlineServices.Conn;
using OnlineServices.CmsData;
using OnlineServices.Method;

namespace ICommunity
{
  public partial class Nodo : System.Web.UI.Page
  {
    Web oWeb = new Web();
    OnlineServices.Method.Usuario oIsUsuario;

    protected void Page_Load(object sender, EventArgs e)
    {
      oWeb.ValidaSessionAdm();
      if (!IsPostBack)
      {
        CodNodo.Value = oWeb.GetData("CodNodo");
        CodNodoRel.Value = oWeb.GetData("CodNodoRel");
        DBConn oConn = new DBConn();
        if (oConn.Open())
        {
          CmsTemplate oTemplate = new CmsTemplate(ref oConn);
          DataTable dTemplate = oTemplate.Get();
          if (dTemplate != null)
            if (dTemplate.Rows.Count > 0)
            {
              rdCmbTemplate.Items.Add(new ListItem("Seleccione Template", ""));
              foreach (DataRow oRow in dTemplate.Rows)
              {
                rdCmbTemplate.Items.Add(new ListItem(oRow["nom_template"].ToString(), oRow["cod_template"].ToString()));
              }
            }
          dTemplate = null;

          if (!string.IsNullOrEmpty(CodNodo.Value))
          {
            CmsNodos oNodos = new CmsNodos(ref oConn);
            oNodos.CodNodo = CodNodo.Value;
            DataTable dNodos = oNodos.Get();
            if (dNodos != null)
              if (dNodos.Rows.Count > 0)
              {
                CodNodoRel.Value = dNodos.Rows[0]["cod_nodo_rel"].ToString();
                txtTitulo.Text = dNodos.Rows[0]["titulo_nodo"].ToString();
                textDescripcion.Text = dNodos.Rows[0]["texto_nodo"].ToString();
                rdCmbTemplate.Items.FindByValue(dNodos.Rows[0]["cod_template"].ToString()).Selected = true;
                rdCmbEstado.Items.FindByValue(dNodos.Rows[0]["est_nodo"].ToString()).Selected = true;
                chk_inicio.Checked = (dNodos.Rows[0]["ini_nodo"].ToString() == "V" ? true : false);
                chk_perfil.Checked = (dNodos.Rows[0]["pf_nodo"].ToString() == "V" ? true : false);
                txtTitHeader.Text = dNodos.Rows[0]["titleheader_nodo"].ToString();
                txtKeyWords.Text = dNodos.Rows[0]["keywords_nodo"].ToString();
                chk_contenido.Checked = (dNodos.Rows[0]["cont_nodo"].ToString() == "V" ? true : false);
                chk_privado.Checked = (dNodos.Rows[0]["prv_nodo"].ToString() == "1" ? true : false);
                OrdNodo.Value = dNodos.Rows[0]["ord_nodo"].ToString();
                chk_asocusrperfil.Checked = (dNodos.Rows[0]["ini_asoc_usr_nodo"].ToString() == "V" ? true : false);
                chk_PrivUsrClient.Checked = (dNodos.Rows[0]["ind_despl_usr_client"].ToString() == "V" ? true : false);
                chk_olvclave.Checked = (dNodos.Rows[0]["ind_olvclave_nodo"].ToString() == "V" ? true : false);
                chk_rstclave.Checked = (dNodos.Rows[0]["ind_rstclave_nodo"].ToString() == "V" ? true : false);
                chk_login.Checked = (dNodos.Rows[0]["ind_login_nodo"].ToString() == "V" ? true : false);
                chk_PrivUsrSite.Checked = (dNodos.Rows[0]["ind_despl_usr_site"].ToString() == "V" ? true : false);
                chk_poltsecure.Checked = (dNodos.Rows[0]["ind_poltsecure_nodo"].ToString() == "V" ? true : false);
                chk_termuse.Checked = (dNodos.Rows[0]["ind_termuse_nodo"].ToString() == "V" ? true : false);
                chk_registrate.Checked = (dNodos.Rows[0]["ind_registrate_nodo"].ToString() == "V" ? true : false);
                chk_pagexito.Checked = (dNodos.Rows[0]["ind_pagexito_nodo"].ToString() == "V" ? true : false);
                chk_pagefotos.Checked = (dNodos.Rows[0]["ind_photo_nodo"].ToString() == "V" ? true : false);
                chk_ini_nod_phone.Checked = (dNodos.Rows[0]["ini_nodo_phone"].ToString() == "V" ? true : false);
                chk_prf_nod_phone.Checked = (dNodos.Rows[0]["pf_nodo_phone"].ToString() == "V" ? true : false);
                chk_cont_nod_phone.Checked = (dNodos.Rows[0]["cont_nodo_phone"].ToString() == "V" ? true : false);
                sAccion.Value = "EDITAR";
              }
            dTemplate = null;
          }
          oConn.Close();
        }
      }
    }

    protected void btnGrabar_Click(object sender, EventArgs e)
    {
      oIsUsuario = oWeb.GetObjUsuario();
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        int iCount = int.Parse((string.IsNullOrEmpty(OrdNodo.Value) ? "0" : OrdNodo.Value));
        CmsNodos oNodos = new CmsNodos(ref oConn);
        if (string.IsNullOrEmpty(OrdNodo.Value))
        {
          DataTable dNodo = oNodos.Get();
          iCount = (dNodo == null ? 0 : dNodo.Rows.Count);
          iCount++;
        }
        oConn.BeginTransaction();
        oNodos.CodNodo = CodNodo.Value;
        oNodos.CodNodoRel = (string.IsNullOrEmpty(CodNodoRel.Value)? null : CodNodoRel.Value);
        oNodos.CodUsuario = oIsUsuario.CodUsuario;
        oNodos.CodTemplate = rdCmbTemplate.SelectedValue;
        oNodos.TituloNodo = txtTitulo.Text;
        oNodos.TextoNodo = textDescripcion.Text;
        oNodos.DateNodo = DateTime.Now.ToString();
        oNodos.EstNodo = rdCmbEstado.SelectedValue;
        oNodos.PrvNodo = (chk_privado.Checked ? "1" : "0");
        oNodos.IniNodo = (chk_inicio.Checked ? "V" : "N");
        oNodos.PfNodo = (chk_perfil.Checked ? "V" : "N");
        oNodos.ContNodo = (chk_contenido.Checked ? "V" : "N");
        oNodos.TitleHeaderNodo = txtTitHeader.Text;
        oNodos.KeywordsNodo = txtKeyWords.Text;
        oNodos.OrdNodo = iCount.ToString();
        oNodos.IniAsocUsrNodo = (chk_asocusrperfil.Checked ? "V" : "N");
        oNodos.IndDesplUsrClient = (chk_PrivUsrClient.Checked ? "V" : "N");
        oNodos.IndOlvClaveNodo = (chk_olvclave.Checked ? "V" : "N");
        oNodos.IndRstClaveNodo = (chk_rstclave.Checked ? "V" : "N");
        oNodos.IndLoginNodo = (chk_login.Checked ? "V" : "N");
        oNodos.IndDesplUsrSite = (chk_PrivUsrSite.Checked ? "V" : "N");
        oNodos.IndPoltSecureNodo = (chk_poltsecure.Checked ? "V" : "N");
        oNodos.IndTermUseNodo = (chk_termuse.Checked ? "V" : "N");
        oNodos.IndRegistrateNodo = ( chk_registrate.Checked ? "V" : "N");
        oNodos.IndPagExitoNodo = ( chk_pagexito.Checked ? "V" : "N");
        oNodos.IndPhotoNodo = (chk_pagefotos.Checked ? "V" : "N");
        oNodos.IndIniNodoPhone = (chk_ini_nod_phone.Checked ? "V" : "N");
        oNodos.IndPfNodoPhone = (chk_prf_nod_phone.Checked ? "V" : "N");
        oNodos.IndContNodoPhone = (chk_cont_nod_phone.Checked ? "V" : "N");
        oNodos.Accion = (string.IsNullOrEmpty(CodNodo.Value) ? "CREAR" : "EDITAR");
        oNodos.Put();
        CodNodo.Value = oNodos.CodNodo;

        if (string.IsNullOrEmpty(oNodos.Error))
        {
          oConn.Commit();
          string cPath = Server.MapPath(".") + @"\binary\";
          oNodos.SerializaNodo(ref oConn, cPath, "Nodo_" + oNodos.CodNodo + ".bin");
          oNodos.SerializaTblNodo(ref oConn, cPath, "Nodos.bin");

          StringBuilder Script = new StringBuilder();
          if (string.IsNullOrEmpty(sAccion.Value))
          {
            if (!Page.ClientScript.IsClientScriptBlockRegistered("jsNewNodo"))
            {
              Script.Append("window.top.AddNode('");
              Script.Append((CodNodoRel.Value == string.Empty ? "0" : CodNodoRel.Value));
              Script.Append("', '");
              Script.Append(CodNodo.Value);
              Script.Append("', '");
              Script.Append(txtTitulo.Text);
              Script.Append("');");
              Page.ClientScript.RegisterStartupScript(this.GetType(), "jsNewNodo", Script.ToString(), true);
            }
          }
          else
          {
            if (!Page.ClientScript.IsClientScriptBlockRegistered("jsChangeName"))
            {
              Script.Append("window.top.ChangeName('");
              Script.Append(CodNodo.Value);
              Script.Append("', '");
              Script.Append(txtTitulo.Text);
              Script.Append("');");
              Page.ClientScript.RegisterStartupScript(this.GetType(), "jsChangeName", Script.ToString(), true);
            }
          }
        }
        else {
          oConn.Rollback();
        }
        oConn.Close();
      }
    }
  }
}
