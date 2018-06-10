using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OnlineServices.Method;
using System.Data;
using OnlineServices.Conn;
using OnlineServices.SystemData;

namespace ICommunity
{
  public partial class WebForm2 : System.Web.UI.Page
  {

    Web oWeb = new Web();

    protected void Page_Load(object sender, EventArgs e)
    {




    }

    protected void Button1_Click(object sender, EventArgs e)
    {

      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        SysUsuario oUsuario = new SysUsuario(ref oConn);
        oUsuario.IsEncrypt = true;
        DataTable dt = oUsuario.Get();

        if (dt != null)
        {
          foreach (DataRow oRow in dt.Rows)
          {
            oUsuario.CodUsuario = oRow["cod_user"].ToString();
            oUsuario.IsEncrypt = true;
            oUsuario.PwdUsuario = oWeb.Crypt(oRow["pwd_decrypted"].ToString());
            oUsuario.Accion = "EDITAR";
            oUsuario.Put();
          }
        }
        dt = null;

        lblDone.Text = "Terminado";
      }
      oConn.Close();
    }

    protected void btnshowclave_Click(object sender, EventArgs e)
    {
      String pCodUsuario = txtCodUser.Text;
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        SysUsuario oUsuario = new SysUsuario(ref oConn);
        oUsuario.CodUsuario = pCodUsuario;
        DataTable dtUsuario = oUsuario.Get();
        if (dtUsuario != null) {
          if (dtUsuario.Rows.Count > 0) {
            lblPassword.Text = oWeb.UnCrypt(dtUsuario.Rows[0]["pwd_user"].ToString());
          }
        }
        dtUsuario = null;
      }
      oConn.Close();
    }
  }
}