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
using System.Drawing;
using System.Drawing.Imaging;

using OnlineServices.Conn;
using OnlineServices.Method;
using OnlineServices.SystemData;

namespace ICommunity
{
  public partial class serializaimages : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      StringBuilder sFile = new StringBuilder();
      string sNomArchivo = string.Empty;
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        StringBuilder sNameFile = new StringBuilder();
        sNameFile.Append(DateTime.Now.Year.ToString());
        sNameFile.Append(DateTime.Now.Month.ToString());
        sNameFile.Append(DateTime.Now.Day.ToString());
        sNameFile.Append(DateTime.Now.Hour.ToString());
        sNameFile.Append(DateTime.Now.Minute.ToString());
        sNameFile.Append(DateTime.Now.Millisecond.ToString());
        sNameFile.Append(".jpg");

        SysArchivosUsuarios oArchivosUsuarios = new SysArchivosUsuarios(ref oConn);
        oArchivosUsuarios.TipArchivo = "P";
        DataTable dArchivosUsuarios = oArchivosUsuarios.Get();
        if (dArchivosUsuarios != null){
          foreach(DataRow oRow in dArchivosUsuarios.Rows){
            if (string.IsNullOrEmpty(oRow["img_profile_archivo"].ToString()))
            {
              sNomArchivo = oRow["nom_archivo"].ToString();
              sFile = new StringBuilder();
              sFile.Append(HttpContext.Current.Server.MapPath("."));
              sFile.Append(@"\rps_onlineservice\escorts\escort_").Append(oRow["cod_usuario"].ToString()).Append(@"\");
              sFile.Append(sNomArchivo);

              if (File.Exists(sFile.ToString()))
              {
                FileStream fileStream = new FileStream(sFile.ToString(), FileMode.Open);
                byte[] imageData = GetImageBytes(fileStream);
                fileStream.Close();

                sFile = new StringBuilder();
                sFile.Append(HttpContext.Current.Server.MapPath("."));
                sFile.Append(@"\rps_onlineservice\escorts\escort_").Append(oRow["cod_usuario"].ToString()).Append(@"\");
                sFile.Append(sNameFile.ToString());
                File.WriteAllBytes(sFile.ToString(), imageData);

                oArchivosUsuarios.CodUsuario = oRow["cod_usuario"].ToString();
                oArchivosUsuarios.CodArchivo = oRow["cod_archivo"].ToString();
                oArchivosUsuarios.ImgProfileArchivo = sNameFile.ToString();
                oArchivosUsuarios.Accion = "EDITAR";
                oArchivosUsuarios.Put();
              }
            }
          }
        }
        dArchivosUsuarios = null;

      }
      oConn.Close();
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
