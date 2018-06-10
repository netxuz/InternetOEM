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
using System.IO;

using Telerik.Web.UI;
using OnlineServices.Conn;
using OnlineServices.Method;
using OnlineServices.SystemData;

namespace ICommunity.Controls
{
  public partial class viewFriendUser : System.Web.UI.UserControl
  {
    Web oWeb = new Web();
    OnlineServices.Method.Usuario oIsUsuario;

    protected void Page_Load(object sender, EventArgs e)
    {
      oIsUsuario = oWeb.GetObjUsuario();
    }

    protected void rdFriendUser_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
      rdFriendUser.Visible = false;
      StringBuilder oFolder = new StringBuilder();
      oFolder.Append(Server.MapPath("."));

      StringBuilder sFile = new StringBuilder();
      sFile.Append("RelacionUsuario_").Append(oIsUsuario.CodUsuario).Append(".bin");
      DataTable dRelacionUsuarios = oWeb.DeserializarTbl(oFolder.ToString(), sFile.ToString());

      if (dRelacionUsuarios != null)
        if (dRelacionUsuarios.Rows.Count > 0)
        {
          DataRow[] oRows = dRelacionUsuarios.Select(" est_relacion = 'V' ");
          if (oRows != null)
            if (oRows.Count() > 0)
            {
              rdFriendUser.DataSource = oRows;
              rdFriendUser.Visible = true;
            }
          oRows = null;
        }
        else
          rdFriendUser.Visible = false;
      else
        rdFriendUser.Visible = false;
      dRelacionUsuarios = null;
    }

    protected void rdFriendUser_ItemDataBound(object sender, RadListViewItemEventArgs e)
    {
      if (e.Item is RadListViewDataItem)
      {
        RadBinaryImage oBinaryImage = e.Item.FindControl("idImgFriendUser") as RadBinaryImage;
        oBinaryImage.DataValue = oWeb.getImageProfileUser(((DataRow)(((e.Item as RadListViewDataItem).DataItem))).ItemArray[1].ToString(), 150, 150);

        Label oLblNomUser = e.Item.FindControl("idLblNomUser") as Label;
        oLblNomUser.Text = ((DataRow)(((e.Item as RadListViewDataItem).DataItem))).ItemArray[3].ToString();

      }
    }

    protected void rdFriendUser_ItemCommand(object sender, RadListViewCommandEventArgs e)
    {
      if (e.ListViewItem is RadListViewDataItem)
      {
        RadListViewDataItem dataItem = (RadListViewDataItem)e.ListViewItem;
        if (e.CommandName == "GOPROFILE")
        {
          string cPath = Server.MapPath(".");
          DataTable oNodos = oWeb.DeserializarTbl(cPath, "Nodos.bin");
          if (oNodos != null)
            if (oNodos.Rows.Count > 0)
            {
              DataRow[] oRow = oNodos.Select(" pf_nodo = 'V' ");
              if (oRow != null)
              {
                if (oRow.Count() > 0)
                {
                  Session["CodNodo"] = oRow[0]["cod_nodo"].ToString();
                  Session["CodUsuarioPerfil"] = dataItem.GetDataKeyValue("cod_usuario_rel").ToString();
                }
              }
              oRow = null;
            }
          oNodos = null;
        }
        if (e.CommandName == "GODELFRIEND") {
          
          SysRelacionUsuarios oRelacionUsuarios;
          StringBuilder oFolder = new StringBuilder();
          oFolder.Append(Server.MapPath(".")).Append(@"\binary\");

          StringBuilder sFile = new StringBuilder();
          StringBuilder sError = new StringBuilder();
          DBConn oConn = new DBConn();
          try
          {
            if (oConn.Open())
            {
              oConn.BeginTransaction();
              oRelacionUsuarios = new SysRelacionUsuarios(ref oConn);
              oRelacionUsuarios.CodUsuario = oIsUsuario.CodUsuario;
              oRelacionUsuarios.CodUsuarioRel = dataItem.GetDataKeyValue("cod_usuario_rel").ToString();
              oRelacionUsuarios.Accion = "ELIMINAR";
              oRelacionUsuarios.Put();
              sError.Append(oRelacionUsuarios.Error);
              if (sError.Length == 0)
              {
                sFile.Append("RelacionUsuario_").Append(oIsUsuario.CodUsuario).Append(".bin");
                oRelacionUsuarios.SerializaTblRelacionUsuarios(ref oConn, oFolder.ToString(), sFile.ToString());

                oRelacionUsuarios.CodUsuarioRel = dataItem.GetDataKeyValue("cod_usuario_rel").ToString();
                oRelacionUsuarios.CodUsuario = oIsUsuario.CodUsuario;
                oRelacionUsuarios.Accion = "ELIMINAR";
                oRelacionUsuarios.Put();
                sError.Append(oRelacionUsuarios.Error);
                if (sError.Length == 0)
                {
                  sFile.Append("RelacionUsuario_").Append(dataItem.GetDataKeyValue("cod_usuario_rel").ToString()).Append(".bin");
                  oRelacionUsuarios.SerializaTblRelacionUsuarios(ref oConn, oFolder.ToString(), sFile.ToString());
                }
              }
              if (sError.Length == 0)
                oConn.Commit();
              else
                oConn.Rollback();
              oConn.Close();
            }
          }
          catch {
            if (oConn.bIsOpen) {
              oConn.Rollback();
              oConn.Close();
            }
          }

        }
      }
      Page.Response.Redirect(".");
    }

  }
}