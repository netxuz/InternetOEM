using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

using OnlineServices.Conn;
using OnlineServices.SystemData;
using OnlineServices.Antalis;
using OnlineServices.Method;

namespace ICommunity.Antalis
{
  public partial class usuario_rol_antalis : System.Web.UI.Page
  {

    Web oWeb = new Web();
    Culture oCulture = new Culture();
    protected void Page_Load(object sender, EventArgs e)
    {
      oWeb.ValidaSessionAdm();
      if (!IsPostBack)
      {
        CodUsuario.Value = oWeb.GetData("CodUsuario");
        DBConn oConn = new DBConn();
        if (oConn.Open())
        {
          SysUsuario oUsuario = new SysUsuario(ref oConn);
          oUsuario.CodUsuario = CodUsuario.Value;
          DataTable dt = oUsuario.Get();
          if (dt != null)
          {
            if (dt.Rows.Count > 0)
            {
              lblUsuario.Text = dt.Rows[0]["nom_user"].ToString() + ' ' + dt.Rows[0]["ape_user"].ToString();

              cAntsUsuarios oAntsUsuarios = new cAntsUsuarios(ref oConn);
              oAntsUsuarios.CodUsuario = CodUsuario.Value;
              DataTable dtRol = oAntsUsuarios.GetRoles();
              if (dtRol != null)
              {
                if (dtRol.Rows.Count > 0)
                {
                  foreach (DataRow oRow in dtRol.Rows)
                  {
                    if (oRow["cod_rol"].ToString() == "1")
                      chk_ingreso_pagos.Checked = true;

                    if (oRow["cod_rol"].ToString() == "2") { 
                      chk_controller.Checked = true;
                      cmb_tipo_pago.SelectedValue = oRow["tipo"].ToString();

                      Page.ClientScript.RegisterStartupScript(this.GetType(), "show", "$(function () { document.getElementById(\"idRowTipoPago\").style.display = 'block'; });", true);
                    }

                  }
                }
              }
              dtRol = null;
            }
          }
          dt = null;
        }
        oConn.Close();
      }
    }

    protected void btnGrabar_Click(object sender, EventArgs e)
    {
      DBConn oConn = new DBConn();
      if (oConn.Open()) {

        cAntsUsuarios oAntsUsuarios = new cAntsUsuarios(ref oConn);
        oAntsUsuarios.CodUsuario = CodUsuario.Value;
        oAntsUsuarios.Accion = "ELIMINAR";
        oAntsUsuarios.Put();

        if (chk_ingreso_pagos.Checked) {
          oAntsUsuarios.Accion = "CREAR";
          oAntsUsuarios.CodRol = "1";
          oAntsUsuarios.Put();
        }

        if (chk_controller.Checked) {
          oAntsUsuarios.Accion = "CREAR";
          oAntsUsuarios.CodRol = "2";
          oAntsUsuarios.Tipo = cmb_tipo_pago.SelectedValue;
          oAntsUsuarios.Put();

          Page.ClientScript.RegisterStartupScript(this.GetType(), "show", "$(function () { document.getElementById(\"idRowTipoPago\").style.display = 'block'; });", true);
        }

      }
      oConn.Close();
    }

    protected void btnVolver_Click(object sender, EventArgs e)
    {
      Response.Redirect("asignacion_roles.aspx");
    }
  }
}