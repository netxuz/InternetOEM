using System;
using System.IO;
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

using Telerik.Web.UI;
using OnlineServices.Conn;
using OnlineServices.SystemData;
using OnlineServices.Method;

namespace ICommunity
{
  public partial class Usuarios : System.Web.UI.Page
  {
    Web oWeb = new Web();
    Culture oCulture = new Culture();
    protected void Page_Load(object sender, EventArgs e)
    {
      oWeb.ValidaSessionAdm();
      btnCrear.Text = oCulture.GetResource("Global", "btnCrear");
      btnBuscar.Text = oCulture.GetResource("Global", "btnBuscar");
      btnCrear.ToolTip = oCulture.GetResource("Global", "btnCrear");
    }

    protected void rdUsuarios_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        GridColumn oGridColumn;

        oGridColumn = rdUsuarios.MasterTableView.Columns.FindByUniqueName("NomUsuario");
        oGridColumn.HeaderText = oCulture.GetResource("Usuario", "NomUsuario");

        oGridColumn = rdUsuarios.MasterTableView.Columns.FindByUniqueName("ApeUsuario");
        oGridColumn.HeaderText = oCulture.GetResource("Usuario", "ApeUsuario");

        oGridColumn = rdUsuarios.MasterTableView.Columns.FindByUniqueName("EstUsuario");
        oGridColumn.HeaderText = oCulture.GetResource("Global", "Estado");

        SysUsuario oUsuario = new SysUsuario(ref oConn);
        if (!string.IsNullOrEmpty(txt_buscar.Text.ToString())) {
          oUsuario.NomUsuario = txt_buscar.Text;
        }
        rdUsuarios.DataSource = oUsuario.Get();

        oConn.Close();
      }

    }

    protected void rdUsuarios_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
      if (e.Item is GridDataItem)
      {
        GridDataItem oItem = e.Item as GridDataItem;

        if ((!string.IsNullOrEmpty(oItem["EstUsuario"].Text)) && (oItem["EstUsuario"].Text != "&nbsp;"))
        {
          if (oItem["EstUsuario"].Text == "V")
            oItem["EstUsuario"].Text = oCulture.GetResource("Global", "Vigente");
          else if (oItem["EstUsuario"].Text == "B")
            oItem["EstUsuario"].Text = oCulture.GetResource("Global", "Bloqueado");
          else if (oItem["EstUsuario"].Text == "E")
            oItem["EstUsuario"].Text = oCulture.GetResource("Global", "Eliminado");
          else
            oItem["EstUsuario"].Text = oCulture.GetResource("Global", "NoVigente");
        }
      }
    }

    protected void rdUsuarios_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
      switch (e.CommandName)
      {
        case "cmdEdit":
          string[] cParam = new string[2];
          cParam[0] = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["cod_usuario"].ToString();
          Response.Redirect(String.Format("Usuario.aspx?CodUsuario={0}", cParam));
          break;
        case "cmdDelete":
          string pCodUsuario = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["cod_usuario"].ToString();
          DBConn oConn = new DBConn();
          if (oConn.Open())
          {
            string sPath = string.Empty;
            sPath = Server.MapPath(".") + @"\binary\ComentarioUsuario_" + pCodUsuario + ".bin";
            File.Delete(sPath);
            SysComentarioUsuario oComentarioUsuario = new SysComentarioUsuario(ref oConn);
            oComentarioUsuario.CodUsuario = pCodUsuario;
            oComentarioUsuario.Accion = "ELIMINAR";
            oComentarioUsuario.Put();

            sPath = string.Empty;
            sPath = Server.MapPath(".") + @"\binary\InfoUsuario_" + pCodUsuario + ".bin";
            File.Delete(sPath);
            SyrInfoUsuarios oInfoUsuarios = new SyrInfoUsuarios(ref oConn);
            oInfoUsuarios.CodUsuario = pCodUsuario;
            oInfoUsuarios.Accion = "ELIMINAR";
            oInfoUsuarios.Put();

            SyrPerfilesUsuarios oPerfilesUsuarios = new SyrPerfilesUsuarios(ref oConn);
            oPerfilesUsuarios.CodUsuario = pCodUsuario;
            oPerfilesUsuarios.Accion = "ELIMINAR";
            oPerfilesUsuarios.Put();

            sPath = string.Empty;
            sPath = Server.MapPath(".") + @"\binary\RelacionUsuario_" + pCodUsuario + ".bin";
            File.Delete(sPath);
            SysRelacionUsuarios oRelacionUsuarios = new SysRelacionUsuarios(ref oConn);
            oRelacionUsuarios.CodUsuario = pCodUsuario;
            oRelacionUsuarios.Accion = "ELIMINAR";
            oRelacionUsuarios.Put();

            /* Eliminar referencia de los usuarios con el usuario eliminado */

            sPath = string.Empty;
            sPath = Server.MapPath(".") + @"\binary\UserArchivo_" + pCodUsuario + ".bin";
            File.Delete(sPath);
            SysArchivosUsuarios oArchivosUsuarios = new SysArchivosUsuarios(ref oConn);
            oArchivosUsuarios.CodUsuario = pCodUsuario;
            DataTable dUserFile = oArchivosUsuarios.Get();
            if (dUserFile != null)
              if (dUserFile.Rows.Count > 0) {
                sPath = string.Empty;
                sPath = Server.MapPath(".") + @"\rps_onlineservice\usuarios\usuario_" + pCodUsuario + @"\";
                foreach(DataRow oRow in dUserFile.Rows){
                  sPath = sPath + oRow["nom_archivo"].ToString();
                  File.Delete(sPath);
                }
              }
            dUserFile = null;
            oArchivosUsuarios.Accion = "ELIMINAR";
            oArchivosUsuarios.Put();

            sPath = string.Empty;
            sPath = Server.MapPath(".") + @"\binary\Usuario_" + pCodUsuario + ".bin";
            File.Delete(sPath);
            SysUsuario oUsuario = new SysUsuario(ref oConn);
            oUsuario.CodUsuario = pCodUsuario;
            oUsuario.EstUsuario = "E";
            oUsuario.Accion = "EDITAR";
            oUsuario.Put();

            sPath = string.Empty;
            sPath = Server.MapPath(".") + @"\binary\";
            string sFile = "Usuarios.bin";
            oUsuario.SerializaTblUsuario(ref oConn, sPath, sFile);

            oConn.Close();
          }
          rdUsuarios.Rebind();
          break;
      }
    }

    protected void btnCrear_Click(object sender, EventArgs e)
    {
      Response.Redirect("Usuario.aspx");
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
      rdUsuarios.Rebind();
    }
  }
}
