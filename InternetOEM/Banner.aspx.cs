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

using OnlineServices.Conn;
using OnlineServices.Method;
using OnlineServices.AppData;
using Telerik.Web.UI;

namespace ICommunity
{
  public partial class Banner : System.Web.UI.Page
  {
    Web oWeb = new Web();
    OnlineServices.Method.Usuario oIsUsuario;
    protected void Page_Load(object sender, EventArgs e)
    {
      oWeb.ValidaSessionAdm();
      if (!IsPostBack)
      {
        CodBanner.Value = oWeb.GetData("CodBanner");
        if (!string.IsNullOrEmpty(CodBanner.Value))
        {
          DBConn oConn = new DBConn();
          if (oConn.Open())
          {
            AppBanner oBanner = new AppBanner(ref oConn);
            oBanner.CodBanner = CodBanner.Value;
            DataTable dBanner = oBanner.Get();
            if (dBanner != null)
            {
              if (dBanner.Rows.Count > 0)
              {
                txtTituloBanner.Text = dBanner.Rows[0]["nom_banner"].ToString();
                rdCmbTipoBanner.Items.FindItemByValue(dBanner.Rows[0]["tipo_banner"].ToString()).Selected = true;
                rdCmbEstado.Items.FindItemByValue(dBanner.Rows[0]["est_banner"].ToString()).Selected = true;
                Image.Visible = true;
              }
            }
            dBanner = null;
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
        AppBanner oBanner = new AppBanner(ref oConn);
        oBanner.CodBanner = CodBanner.Value;
        oBanner.NomBanner = txtTituloBanner.Text;
        oBanner.TipoBanner = rdCmbTipoBanner.SelectedValue;
        oBanner.EstBanner = rdCmbEstado.SelectedValue;
        oBanner.Accion = (string.IsNullOrEmpty(CodBanner.Value) ? "CREAR" : "EDITAR");
        oBanner.Put();
        CodBanner.Value = oBanner.CodBanner;

        if (string.IsNullOrEmpty(oBanner.Error))
        {
          oConn.Commit();
          StringBuilder cPath = new StringBuilder();
          cPath.Append(Server.MapPath(".")).Append(@"\binary\");
          oBanner.CodBanner = string.Empty;
          oBanner.SerializaBanner(ref oConn, cPath.ToString());
          Image.Visible = true;

          AppImgBanner oImgBanner = new AppImgBanner(ref oConn);
          cPath = new StringBuilder();
          cPath.Append(Server.MapPath(".")).Append(@"\binary\");
          oImgBanner.SerializaImgBanner(ref oConn, cPath.ToString());
        }
        else
        {
          oConn.Rollback();
        }
        oConn.Close();
      }
      rdImage.Rebind();
    }

    protected void rdImage_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
      GridEditableItem editedItem = e.Item as GridEditableItem;
      string pCodImgBanner = (e.Item.ItemIndex > -1 ? e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["cod_img_banner"].ToString() : string.Empty);
      UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
      if (((userControl.FindControl("FileUploadImg") as FileUpload).HasFile) || (!string.IsNullOrEmpty((userControl.FindControl("ComentarioImage") as TextBox).Text)))
      {
        DBConn oConn = new DBConn();
        if (oConn.Open())
        {
          oConn.BeginTransaction();
          AppImgBanner oImgBanner = new AppImgBanner(ref oConn);
          oImgBanner.CodBanner = CodBanner.Value;
          if (string.IsNullOrEmpty(pCodImgBanner)) {
            DataTable dImgBanner = oImgBanner.Get();
            oImgBanner.OrdImgBanner = (dImgBanner.Rows.Count + 1).ToString();
            dImgBanner = null;
          }
          oImgBanner.CodImgBanner = pCodImgBanner;
          if ((userControl.FindControl("FileUploadImg") as FileUpload).HasFile)
            oImgBanner.NomImgBanner = (userControl.FindControl("FileUploadImg") as FileUpload).FileName;
          if (!string.IsNullOrEmpty((userControl.FindControl("TxtComentarioImage") as RadEditor).Content))
            oImgBanner.TextImgBanner = (userControl.FindControl("TxtComentarioImage") as RadEditor).Content;
          if (!string.IsNullOrEmpty((userControl.FindControl("txtUrlLink") as TextBox).Text))
            oImgBanner.UrlImgBanner = (userControl.FindControl("txtUrlLink") as TextBox).Text;
          oImgBanner.Accion = (string.IsNullOrEmpty(pCodImgBanner) ? "CREAR" : "EDITAR");
          oImgBanner.Put();
          pCodImgBanner = oImgBanner.CodImgBanner;

          if (string.IsNullOrEmpty(oImgBanner.Error))
          {
            if ((userControl.FindControl("FileUploadImg") as FileUpload).HasFile)
            {
              StringBuilder sPath = new StringBuilder();
              StringBuilder sFile = new StringBuilder();
              sPath.Append(Server.MapPath("."));
              sPath.Append(@"\rps_onlineservice\");
              sPath.Append(@"\banners\");
              sPath.Append(@"\banner_");
              sPath.Append(CodBanner.Value);
              if (!Directory.Exists(sPath.ToString()))
                Directory.CreateDirectory(sPath.ToString());
              sPath.Append(@"\").Append((userControl.FindControl("FileUploadImg") as FileUpload).FileName);
              byte[] imageData = oWeb.GetImageBytes((userControl.FindControl("FileUploadImg") as FileUpload).FileContent);
              File.WriteAllBytes(sPath.ToString(), imageData);
            }

            oConn.Commit();
            StringBuilder cPath = new StringBuilder();
            cPath.Append(Server.MapPath(".")).Append(@"\binary\");
            oImgBanner.CodImgBanner = string.Empty;
            oImgBanner.CodBanner = string.Empty;
            oImgBanner.SerializaImgBanner(ref oConn, cPath.ToString());
          }
          else
          {
            oConn.Rollback();
          }
          oConn.Close();
          rdImage.Rebind();
        }
      }
    }

    protected void rdImage_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
      string sNomImage = string.Empty;
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        string pCodImgBanner = (e.Item.ItemIndex > -1 ? e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["cod_img_banner"].ToString() : string.Empty);
        AppImgBanner oImgBanner = new AppImgBanner(ref oConn);
        oImgBanner.CodBanner = CodBanner.Value;
        oImgBanner.CodImgBanner = pCodImgBanner;
        DataTable dImgBanner = oImgBanner.Get();
        if (dImgBanner != null)
        {
          if (dImgBanner.Rows.Count > 0)
          {
            sNomImage = dImgBanner.Rows[0]["nom_img_banner"].ToString();
          }
        }
        dImgBanner = null;
        oImgBanner.Accion = "ELIMINAR";
        oImgBanner.Put();

        if (string.IsNullOrEmpty(oImgBanner.Error))
        {
          StringBuilder cPath = new StringBuilder();
          cPath.Append(Server.MapPath(".")).Append(@"\rps_onlineservice\banners\banner_").Append(CodBanner.Value).Append(@"\").Append(sNomImage);
          if (File.Exists(cPath.ToString()))
            File.Delete(cPath.ToString());
          cPath = new StringBuilder();
          cPath.Append(Server.MapPath(".")).Append(@"\binary\");
          oImgBanner.CodImgBanner = string.Empty;
          oImgBanner.CodBanner = string.Empty;
          oImgBanner.SerializaImgBanner(ref oConn, cPath.ToString());
        }
      }
      rdImage.Rebind();
    }

    protected void rdImage_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
      if (!string.IsNullOrEmpty(CodBanner.Value))
      {
        DBConn oConn = new DBConn();
        if (oConn.Open())
        {
          AppImgBanner oBannerImage = new AppImgBanner(ref oConn);
          oBannerImage.CodBanner = CodBanner.Value;
          rdImage.DataSource = oBannerImage.Get();
        }
        oConn.Close();
      }
    }

    protected void rdImage_RowDrop(object sender, Telerik.Web.UI.GridDragDropEventArgs e)
    {
      AppImgBanner oImgBanner;
      int iIndex = 0;
      int iPos = e.DestDataItem.ItemIndex + 1;
      bool indEscribe;

      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        oImgBanner = new AppImgBanner(ref oConn);
        oImgBanner.CodBanner = CodBanner.Value;
        DataTable dImgBanner = oImgBanner.Get();
        if (dImgBanner != null)
        {
          if (dImgBanner.Rows.Count > 0)
          {
            string[] sArray = new string[dImgBanner.Rows.Count];
            foreach (DataRow oRow in dImgBanner.Rows)
            {
              if (oRow["ord_img_banner"].ToString() != iPos.ToString())
              {
                indEscribe = false;
                foreach (GridDataItem draggedItem in e.DraggedItems)
                {
                  if (oRow["cod_img_banner"].ToString() == draggedItem.GetDataKeyValue("cod_img_banner").ToString())
                    indEscribe = true;
                }
                if (!indEscribe)
                {
                  sArray[iIndex] = oRow["cod_img_banner"].ToString();
                  iIndex++;
                }
              }
              else
              {
                foreach (GridDataItem draggedItem in e.DraggedItems)
                {
                  sArray[iIndex] = draggedItem.GetDataKeyValue("cod_img_banner").ToString();
                  iIndex++;
                }
                sArray[iIndex] = oRow["cod_img_banner"].ToString();
                iIndex++;
              }
            }
            for (int i = 0; i < sArray.Length; i++)
            {
              oImgBanner.CodImgBanner = sArray[i].ToString();
              oImgBanner.OrdImgBanner = (i + 1).ToString();
              oImgBanner.Accion = "EDITAR";
              oImgBanner.Put();
            }
          }
        }
        dImgBanner = null;
        oImgBanner = new AppImgBanner(ref oConn);
        oImgBanner.CodImgBanner = string.Empty;
        oImgBanner.CodBanner = string.Empty;
        oImgBanner.SerializaImgBanner(ref oConn, Server.MapPath(".") + @"\binary\");
        rdImage.Rebind();
        oConn.Close();
      }
    }
  }
}
