using System;
using System.Text;
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
using System.Drawing;
using System.Drawing.Imaging;

using Telerik.Web.UI;
using OnlineServices.Conn;
using OnlineServices.Method;
using OnlineServices.SystemData;

namespace ICommunity.Controls
{
  public partial class viewImagesUsers : System.Web.UI.UserControl
  {
    Web oWeb = new Web();
    Culture oCulture = new Culture();
    OnlineServices.Method.Controls oControls = new OnlineServices.Method.Controls();
    OnlineServices.Method.Usuario oIsUsuario;

    protected void Page_Load(object sender, EventArgs e)
    {
      oIsUsuario = oWeb.GetObjUsuario();
      objwin.Controls.Add(oControls.LoadRadWindow());

      if (!IsPostBack)
      {
        lblMisImagenes.Text = oCulture.GetResource("MisFotos", "lblMisImagenes");
        btnUpload.Text = oCulture.GetResource("Usuario", "CargaFotos");
        if ((Session["CodUsuarioPerfil"] == null) || (Session["CodUsuarioPerfil"].ToString() != oIsUsuario.CodUsuario))
          btnUpload.Visible = false;
      }
      else
      {
        if ((Session["ReloadImageUser"] != null) && (!string.IsNullOrEmpty(Session["ReloadImageUser"].ToString())))
        {
          Session["ReloadImageUser"] = string.Empty;
          rdUserImage.Rebind();
        }
      }
    }

    protected void rdUserImage_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
      string cPath = Server.MapPath(".");
      DataTable dUserArchivo = oWeb.DeserializarTbl(cPath, "UserArchivo_" + Session["CodUsuarioPerfil"].ToString() + ".bin");
      if (dUserArchivo != null)
        if (dUserArchivo.Rows.Count > 0)
        {
          rdUserImage.DataSource = dUserArchivo;
          rdUserImage.Visible = true;
        }
        else
          rdUserImage.Visible = false;
      else
        rdUserImage.Visible = false;
      dUserArchivo = null;
    }

    protected void rdUserImage_ItemCommand(object sender, RadListViewCommandEventArgs e)
    {
      try
      {
        if (e.ListViewItem is RadListViewDataItem)
        {
          StringBuilder sNameFile = new StringBuilder();
          sNameFile.Append(DateTime.Now.Year.ToString());
          sNameFile.Append(DateTime.Now.Month.ToString());
          sNameFile.Append(DateTime.Now.Day.ToString());
          sNameFile.Append(DateTime.Now.Hour.ToString());
          sNameFile.Append(DateTime.Now.Minute.ToString());
          sNameFile.Append(DateTime.Now.Millisecond.ToString());
          sNameFile.Append(".jpg");

          RadListViewDataItem dataItem = (RadListViewDataItem)e.ListViewItem;
          SysArchivosUsuarios oArchivosUsuarios;

          if (e.CommandName == "IMGPERFIL")
          {
            DBConn oConn = new DBConn();
            if (oConn.Open())
            {
              StringBuilder sFile;
              string sNomArchivo = string.Empty;

              oArchivosUsuarios = new SysArchivosUsuarios(ref oConn);
              oArchivosUsuarios.CodUsuario = oIsUsuario.CodUsuario;
              oArchivosUsuarios.TipArchivo = "P";
              DataTable dFotoPerfil = oArchivosUsuarios.Get();
              if (dFotoPerfil != null) {
                if (dFotoPerfil.Rows.Count > 0) {
                  if (!string.IsNullOrEmpty(dFotoPerfil.Rows[0]["img_profile_archivo"].ToString()))
                  {
                    sNomArchivo = dFotoPerfil.Rows[0]["img_profile_archivo"].ToString();
                    sFile = new StringBuilder();
                    sFile.Append(HttpContext.Current.Server.MapPath("."));
                    sFile.Append(@"\rps_onlineservice\escorts\escort_").Append(oIsUsuario.CodUsuario).Append(@"\");
                    sFile.Append(sNomArchivo);
                    if (File.Exists(sFile.ToString()))
                    {
                      File.Delete(sFile.ToString());
                    }
                  }
                }
              }
              dFotoPerfil = null;

              oArchivosUsuarios.ImgProfileArchivo = string.Empty;
              oArchivosUsuarios.Accion = "EDITAR";
              oArchivosUsuarios.TipArchivo = string.Empty;
              oArchivosUsuarios.Put();

              oArchivosUsuarios.CodArchivo = dataItem.GetDataKeyValue("cod_archivo").ToString();
              dFotoPerfil = oArchivosUsuarios.Get();
              if (dFotoPerfil != null)
              {
                if (dFotoPerfil.Rows.Count > 0) {
                  sNomArchivo = dFotoPerfil.Rows[0]["nom_archivo"].ToString();
                  sFile = new StringBuilder();
                  sFile.Append(HttpContext.Current.Server.MapPath("."));
                  sFile.Append(@"\rps_onlineservice\escorts\escort_").Append(oIsUsuario.CodUsuario).Append(@"\");
                  sFile.Append(sNomArchivo);
                  FileStream fileStream = new FileStream(sFile.ToString(), FileMode.Open);
                  byte[] imageData = GetImageBytes(fileStream);
                  fileStream.Close();
                  sFile = new StringBuilder();
                  sFile.Append(HttpContext.Current.Server.MapPath("."));
                  sFile.Append(@"\rps_onlineservice\escorts\escort_").Append(oIsUsuario.CodUsuario).Append(@"\");
                  sFile.Append(sNameFile.ToString());
                  File.WriteAllBytes(sFile.ToString(), imageData);
                  oArchivosUsuarios.ImgProfileArchivo = sNameFile.ToString();
                  oArchivosUsuarios.TipArchivo = "P";
                  oArchivosUsuarios.Put();
                }
              }
              dFotoPerfil = null;

              if (string.IsNullOrEmpty(oArchivosUsuarios.Error))
              {
                string cPath = HttpContext.Current.Server.MapPath(".") + @"\binary\";
                string sFileUsrArchivo = "UserArchivo_" + oIsUsuario.CodUsuario + ".bin";
                oArchivosUsuarios.SerializaTblUserArchivo(ref oConn, cPath, sFileUsrArchivo);
              }

              oConn.Close();
            }
          }

          if (e.CommandName == "IMGDELETE")
          {
            DBConn oConn = new DBConn();
            if (oConn.Open())
            {
              string sArchivo = string.Empty;
              StringBuilder sPath = new StringBuilder();
              sPath.Append(HttpContext.Current.Server.MapPath("."));
              sPath.Append(@"\rps_onlineservice\");
              sPath.Append(@"\escorts\");
              sPath.Append(@"\escort_");
              sPath.Append(oIsUsuario.CodUsuario);
              sPath.Append(@"\");

              oArchivosUsuarios = new SysArchivosUsuarios(ref oConn);
              oArchivosUsuarios.CodUsuario = oIsUsuario.CodUsuario;
              oArchivosUsuarios.CodArchivo = dataItem.GetDataKeyValue("cod_archivo").ToString();
              DataTable dArchivosUsuarios = oArchivosUsuarios.Get();
              if (dArchivosUsuarios != null)
                if (dArchivosUsuarios.Rows.Count > 0)
                {
                  sArchivo = dArchivosUsuarios.Rows[0]["nom_archivo"].ToString();
                }
              dArchivosUsuarios = null;
              oArchivosUsuarios.Accion = "ELIMINAR";
              oArchivosUsuarios.Put();

              if (string.IsNullOrEmpty(oArchivosUsuarios.Error))
              {
                sPath.Append(sArchivo);
                File.Delete(sPath.ToString());

                string cPath = HttpContext.Current.Server.MapPath(".") + @"\binary\";
                string sFileUsrArchivo = "UserArchivo_" + oIsUsuario.CodUsuario + ".bin";
                oArchivosUsuarios.SerializaTblUserArchivo(ref oConn, cPath, sFileUsrArchivo);
              }

              oConn.Close();
            }
          }

        }
        Response.Redirect(".");
      }
      catch
      {
        //Error:
      }
      rdUserImage.Rebind();
    }

    protected void rdUserImage_ItemDataBound(object sender, RadListViewItemEventArgs e)
    {
      if (e.Item is RadListViewDataItem)
      {
        StringBuilder sPath;
        sPath = new StringBuilder();
        sPath.Append(HttpContext.Current.Server.MapPath("."));
        sPath.Append(@"\rps_onlineservice\");
        sPath.Append(@"\escorts\");
        sPath.Append(@"\escort_");
        sPath.Append(((e.Item as RadListViewDataItem).DataItem as DataRowView)["cod_usuario"].ToString());
        sPath.Append(@"\");
        sPath.Append(((e.Item as RadListViewDataItem).DataItem as DataRowView)["nom_archivo"].ToString());
        FileStream fileStream = new FileStream(sPath.ToString(), FileMode.Open);

        RadBinaryImage oBinaryImage = e.Item.FindControl("oBinaryImage") as RadBinaryImage;
        oBinaryImage.DataValue = oWeb.GetImageBytes(fileStream, 300, 300);
        oBinaryImage.Width = Unit.Pixel(200);
        oBinaryImage.AutoAdjustImageControlSize = false;
        //oBinaryImage.ImageUrl = string.Format("{0}", sPath.ToString());
        fileStream.Close();

        sPath = new StringBuilder();
        sPath.Append("../rps_onlineservice").Append("/escorts").Append("/escort_");
        sPath.Append(((e.Item as RadListViewDataItem).DataItem as DataRowView)["cod_usuario"].ToString());
        sPath.Append("/");
        sPath.Append(((e.Item as RadListViewDataItem).DataItem as DataRowView)["nom_archivo"].ToString());

        HyperLink oHyperLink = e.Item.FindControl("idLnkButton") as HyperLink;
        oHyperLink.NavigateUrl = string.Format("{0}", sPath.ToString());
        //oHyperLink.NavigateUrl = string.Format("{0}", oBinaryImage.ImageUrl.ToString());
        oHyperLink.Attributes["rel"] = "ImgFotoUser";

        
      }

      if ((Session["CodUsuarioPerfil"] != null) && (Session["CodUsuarioPerfil"].ToString() == oIsUsuario.CodUsuario))
      {
        if (e.Item is RadListViewDataItem)
        {
          if (((e.Item as RadListViewDataItem).DataItem as DataRowView)["tip_archivo"].ToString() == "P")
          {
            Button btnImgPrincipal = e.Item.FindControl("btnImgPrincipal") as Button;
            btnImgPrincipal.Visible = false;
          }
        }
      }
      else {
        Button btnImgPrincipal = e.Item.FindControl("btnImgPrincipal") as Button;
        btnImgPrincipal.Visible = false;

        Button btnEliminar = e.Item.FindControl("btnEliminar") as Button;
        btnEliminar.Visible = false;
      }
    }

    public byte[] GetImageBytes(Stream stream)
    {
      System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
      EncoderParameters myEncoderParameters = new EncoderParameters(1);
      EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 100L);
      myEncoderParameters.Param[0] = myEncoderParameter;
      
      byte[] buffer;

      using (Bitmap image = ResizeImage(stream))
      {
        using (MemoryStream ms = new MemoryStream())
        {
          image.Save(ms, GetEncoderInfo("image/jpeg"), myEncoderParameters);

          //return the current position in the stream at the beginning
          ms.Position = 0;

          buffer = new byte[ms.Length];
          ms.Read(buffer, 0, (int)ms.Length);
          return buffer;
        }
      }
    }

    private static ImageCodecInfo GetEncoderInfo(String mimeType)
    {
      int j;
      ImageCodecInfo[] encoders;
      encoders = ImageCodecInfo.GetImageEncoders();
      for (j = 0; j < encoders.Length; ++j)
      {
        if (encoders[j].MimeType == mimeType)
          return encoders[j];
      }
      return null;
    }

    public Bitmap ResizeImage(Stream stream)
    {
      System.Drawing.Image originalImage = Bitmap.FromStream(stream);

      int height = 150;
      int width = 150;

      double ratio = Math.Min(originalImage.Width, originalImage.Height) / (double)Math.Max(originalImage.Width, originalImage.Height);

      if (originalImage.Width > originalImage.Height)
      {
        height = Convert.ToInt32(height * ratio);
      }
      else
      {
        width = Convert.ToInt32(width * ratio);
      }

      Bitmap scaledImage = new Bitmap(width, height);

      using (Graphics g = Graphics.FromImage(scaledImage))
      {
        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
        g.DrawImage(originalImage, 0, 0, width, height);

        return scaledImage;
      }

    }

  }
}