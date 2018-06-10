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
using OnlineServices.Method;
using OnlineServices.SystemData;
using Telerik.Web.UI;

namespace ICommunity
{
  public partial class CampoUsuarios : System.Web.UI.Page
  {
    Web oWeb = new Web();
    protected void Page_Load(object sender, EventArgs e)
    {
      oWeb.ValidaSessionAdm();
    }

    protected void rdCampoUsuarios_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
      DBConn oConn = new DBConn();
      if (oConn.Open()){
        SysCampoUsuarios oCampoUsuarios = new SysCampoUsuarios(ref oConn);
        rdCampoUsuarios.DataSource = oCampoUsuarios.Get();
      }
      oConn.Close();
    }

    protected void rdCampoUsuarios_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
      string sDespCampo = string.Empty;
      DBConn oConn = new DBConn();
      if (oConn.Open()) {
        string pCodCampo = (e.Item.ItemIndex > -1 ? e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["cod_campo"].ToString() : string.Empty);
        GridEditableItem editedItem = e.Item as GridEditableItem;
        UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
        string sTipo = (userControl.FindControl("rdCmbTipo") as RadComboBox).SelectedValue.ToString();

        SysCampoUsuarios oCampoUsuarios = new SysCampoUsuarios(ref oConn);
        if (string.IsNullOrEmpty(pCodCampo)){
          DataTable dCampoUsuario = oCampoUsuarios.Get();
          oCampoUsuarios.OrdCampo = (dCampoUsuario.Rows.Count + 1).ToString();
          dCampoUsuario = null;
        }
        switch (sTipo){
          case "1":
              sDespCampo = ((userControl.FindControl("chk_multSelect") as CheckBox).Checked ? "C" : string.Empty);
            break;
          case "2":
              sDespCampo = ((userControl.FindControl("chk_observacion") as CheckBox).Checked ? "O" : string.Empty);
            break;
          default:
              sDespCampo = string.Empty;
            break;
        }
        

        oCampoUsuarios.CodCampo = pCodCampo;
        oCampoUsuarios.NomCampo = (userControl.FindControl("TxtColumna") as RadTextBox).Text;
        oCampoUsuarios.TipoCampo = sTipo;
        oCampoUsuarios.DespCampo = sDespCampo;
        oCampoUsuarios.EstCampo = "V";
        oCampoUsuarios.IndDespliegue = ((userControl.FindControl("chk_despl_usr") as CheckBox).Checked ? "V" : "N");
        oCampoUsuarios.IndDesplieguePortal = ((userControl.FindControl("chk_despl_portal") as CheckBox).Checked ? "V" : "N");
        oCampoUsuarios.IndValidacion = ((userControl.FindControl("chk_ind_validacion") as CheckBox).Checked ? "V" : "N");
        oCampoUsuarios.Accion = (string.IsNullOrEmpty(pCodCampo) ? "CREAR" : "EDITAR");
        oCampoUsuarios.Put();
        pCodCampo = oCampoUsuarios.CodCampo;

        string cPath = Server.MapPath(".") + @"\binary\";
        string sFile = "CampoUsuarios.bin";
        oCampoUsuarios.SerializaTblCmpUsuario(ref oConn, cPath, sFile);

        if ((sTipo == "0")||(sTipo == "1")){
          SyrCampoOpciones oCampoOpciones;
          oCampoOpciones = new SyrCampoOpciones(ref oConn);
          oCampoOpciones.CodCampo = pCodCampo;
          oCampoOpciones.Accion = "ELIMINAR";
          oCampoOpciones.Put();

          string pCodOpcion = string.Empty;
          DataTable dOpcionesCampo;
          SysOpcionesCampo oOpcionesCampo = new SysOpcionesCampo(ref oConn);
          foreach (string sData in (userControl.FindControl("txtAtributos") as RadTextBox).Text.Split('\n'))
          {
            string sDato = sData.Replace("\r", "");
            oOpcionesCampo.NomOpcion = sDato;
            dOpcionesCampo = oOpcionesCampo.Get();
            if (dOpcionesCampo != null)
              if (dOpcionesCampo.Rows.Count > 0)
              {
                pCodOpcion = dOpcionesCampo.Rows[0]["cod_opcion"].ToString();
              }
              else
              {
                oOpcionesCampo.Accion = "CREAR";
                oOpcionesCampo.Put();
                pCodOpcion = oOpcionesCampo.CodOpcion;
              }
            dOpcionesCampo = null;

            oCampoOpciones = new SyrCampoOpciones(ref oConn);
            oCampoOpciones.CodCampo = pCodCampo;
            oCampoOpciones.CodOpcion = pCodOpcion;
            oCampoOpciones.Accion = "CREAR";
            oCampoOpciones.Put();
          }

          SyrInfoUsuarios oInfoUsuarios = new SyrInfoUsuarios(ref oConn);
          oInfoUsuarios.CodCampo = pCodCampo;
          oInfoUsuarios.Accion = "ELIMINAR";
          oInfoUsuarios.Put();

          SysUsuario oUsuario = new SysUsuario(ref oConn);
          DataTable dUsuario = oUsuario.Get();
          if (dUsuario.Rows.Count > 0)
          {
            foreach (DataRow oRow in dUsuario.Rows)
            {
              oInfoUsuarios.CodUsuario = oRow["cod_usuario"].ToString();
              oInfoUsuarios.SerializaTblInfoUsuario(ref oConn, Server.MapPath(".") + @"\binary\", "InfoUsuario_" + oRow["cod_usuario"].ToString() + ".bin");
            }
          }
          dUsuario = null;

          sFile = "OpcionesCampo.bin";
          oOpcionesCampo.SerializaTblOpcCampo(ref oConn, cPath, sFile);
        }


        oConn.Close();
      }
    }

    protected void rdCampoUsuarios_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
      DBConn oConn = new DBConn();
      if (oConn.Open()) {
        string pCodCampo = (e.Item.ItemIndex > -1 ? e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["cod_campo"].ToString() : string.Empty);
        SyrCampoOpciones oCampoOpciones = new SyrCampoOpciones(ref oConn);
        oCampoOpciones.CodCampo = pCodCampo;
        oCampoOpciones.Accion = "ELIMINAR";
        oCampoOpciones.Put();

        SyrInfoUsuarios oInfoUsuarios = new SyrInfoUsuarios(ref oConn);
        oInfoUsuarios.CodCampo = pCodCampo;
        oInfoUsuarios.Accion = "ELIMINAR";
        oInfoUsuarios.Put();

        SysUsuario oUsuario = new SysUsuario(ref oConn);
        DataTable dUsuario = oUsuario.Get();
        if (dUsuario.Rows.Count > 0){
          foreach (DataRow oRow in dUsuario.Rows) { 
            oInfoUsuarios.CodUsuario = oRow["cod_usuario"].ToString();
            oInfoUsuarios.SerializaTblInfoUsuario(ref oConn, Server.MapPath(".") + @"\binary\", "InfoUsuario_" + oRow["cod_usuario"].ToString() + ".bin");
          }
        }
        dUsuario = null;

        SysCampoUsuarios oCampoUsuarios = new SysCampoUsuarios(ref oConn);
        oCampoUsuarios.CodCampo = pCodCampo;
        oCampoUsuarios.Accion = "ELIMINAR";
        oCampoUsuarios.Put();
        oCampoUsuarios.SerializaTblCmpUsuario(ref oConn, Server.MapPath(".") + @"\binary\", "CampoUsuarios.bin");

        oConn.Close();
      }
    }

    protected void rdCampoUsuarios_RowDrop(object sender, Telerik.Web.UI.GridDragDropEventArgs e)
    {
      SysCampoUsuarios oCampoUsuarios;
      int iIndex = 0;
      int iPos = e.DestDataItem.ItemIndex + 1;
      bool indEscribe;

      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        oCampoUsuarios = new SysCampoUsuarios(ref oConn);
        DataTable dCampoUsuarios = oCampoUsuarios.Get();
        if (dCampoUsuarios != null) {
          if (dCampoUsuarios.Rows.Count > 0) {
            string[] sArray = new string[dCampoUsuarios.Rows.Count];
            foreach(DataRow oRow in dCampoUsuarios.Rows){
              if (oRow["ord_campo"].ToString() != iPos.ToString())
              {
                indEscribe = false;
                foreach (GridDataItem draggedItem in e.DraggedItems)
                {
                  if (oRow["cod_campo"].ToString() == draggedItem.GetDataKeyValue("cod_campo").ToString())
                    indEscribe = true;
                }
                if (!indEscribe)
                {
                  sArray[iIndex] = oRow["cod_campo"].ToString();
                  iIndex++;
                }
              }
              else {
                foreach (GridDataItem draggedItem in e.DraggedItems)
                {
                  sArray[iIndex] = draggedItem.GetDataKeyValue("cod_campo").ToString();
                  iIndex++;
                }
                sArray[iIndex] = oRow["cod_campo"].ToString();
                iIndex++;
              }
            }
            for (int i = 0; i < sArray.Length; i++)
            {
              oCampoUsuarios.CodCampo = sArray[i].ToString();
              oCampoUsuarios.OrdCampo = (i + 1).ToString();
              oCampoUsuarios.EstCampo = "V";
              oCampoUsuarios.Accion = "EDITAR";
              oCampoUsuarios.Put();

              SyrInfoUsuarios oInfoUsuarios = new SyrInfoUsuarios(ref oConn);
              oInfoUsuarios.CodCampo = sArray[i].ToString();
              SysUsuario oUsuario = new SysUsuario(ref oConn);
              DataTable dUsuario = oUsuario.Get();
              if (dUsuario.Rows.Count > 0)
              {
                foreach (DataRow oRow in dUsuario.Rows)
                {
                  oInfoUsuarios.CodUsuario = oRow["cod_usuario"].ToString();
                  oInfoUsuarios.SerializaTblInfoUsuario(ref oConn, Server.MapPath(".") + @"\binary\", "InfoUsuario_" + oRow["cod_usuario"].ToString() + ".bin");
                }
              }
              dUsuario = null;
            }
          }
        }
        dCampoUsuarios = null;
        oCampoUsuarios = new SysCampoUsuarios();
        oCampoUsuarios.SerializaTblCmpUsuario(ref oConn, Server.MapPath(".") + @"\binary\", "CampoUsuarios.bin");
        rdCampoUsuarios.Rebind();
        oConn.Close();
      }
    }
  }
}
