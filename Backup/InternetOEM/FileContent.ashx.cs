using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.IO;
using Telerik.Web.UI;

using OnlineServices.Conn;
using OnlineServices.SystemData;
using OnlineServices.CmsData;
using OnlineServices.Method;

namespace ICommunity
{
  /// <summary>
  /// Summary description for $codebehindclassname$
  /// </summary>
  [WebService(Namespace = "http://tempuri.org/")]
  [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
  public class FileContent : AsyncUploadHandler, System.Web.SessionState.IRequiresSessionState
  {

    protected override IAsyncUploadResult Process(UploadedFile file, HttpContext context, IAsyncUploadConfiguration configuration, string tempFileName)
    {
      // Call the base Process method to save the file to the temporary folder
      // base.Process(file, context, configuration, tempFileName);

      // Populate the default (base) result into an object of type SampleAsyncUploadResult
      SampleAsyncUploadResult result = CreateDefaultUploadResult<SampleAsyncUploadResult>(file);

      string userID = string.Empty;
      // You can obtain any custom information passed from the page via casting the configuration parameter to your custom class
      SampleAsyncUploadConfiguration sampleConfiguration = configuration as SampleAsyncUploadConfiguration;
      if (sampleConfiguration != null)
      {
        userID = sampleConfiguration.UserID;
      }

      // Populate any additional fields into the upload result.
      // The upload result is available both on the client and on the server
      result.ImageID = InsertImage(file, userID);

      return result;
    }

    public string InsertImage(UploadedFile file, string userID)
    {
      string pCodArchivo = string.Empty;
      DBConn oConn = new DBConn();
      try
      {
        byte[] imageData = GetImageBytes(file.InputStream);
        StringBuilder sPath = new StringBuilder();
        StringBuilder sFile = new StringBuilder();
        sPath.Append(HttpContext.Current.Server.MapPath("."));
        sPath.Append(@"\rps_onlineservice\");
        sPath.Append(@"\contenido\");
        sPath.Append(@"\contenido_");
        sPath.Append(userID);
        if (!Directory.Exists(sPath.ToString()))
          Directory.CreateDirectory(sPath.ToString());

        if (oConn.Open())
        {
          oConn.BeginTransaction();

          ObjectModel oObjectModel = new ObjectModel(ref oConn);
          pCodArchivo = oObjectModel.getCodeKey("CMS_ARCHIVOS");

          sPath.Append(@"\").Append(pCodArchivo).Append(file.GetExtension());
          sFile.Append(pCodArchivo).Append(file.GetExtension());
          File.WriteAllBytes(sPath.ToString(), imageData);

          CmsArchivos oArchivos = new CmsArchivos(ref oConn);
          oArchivos.CodArchivo = pCodArchivo;
          oArchivos.Accion = "CREAR";
          oArchivos.CodContenido = userID;
          oArchivos.DateArchivo = DateTime.Now.ToString();
          oArchivos.NomArchivo = sFile.ToString();
          oArchivos.ExtArchivo = file.GetExtension();
          oArchivos.Put();

          if (string.IsNullOrEmpty(oArchivos.Error))
          {
            oConn.Commit();
            string cPath = HttpContext.Current.Server.MapPath(".") + @"\binary\";
            string sFileUsrArchivo = "ContenidoArchivo_" + userID + ".bin";
            oArchivos.SerializaTblArchivo(ref oConn, cPath, sFileUsrArchivo);
          }
          else
            oConn.Rollback();

          oConn.Close();
        }
      }
      catch (Exception Ex)
      {
        if (oConn.bIsOpen)
        {
          oConn.Rollback();
          oConn.Close();
        }
      }
      return pCodArchivo;
    }

    public byte[] GetImageBytes(Stream stream)
    {
      byte[] buffer;

      using (Bitmap image = ResizeImage(stream))
      {
        using (MemoryStream ms = new MemoryStream())
        {
          image.Save(ms, ImageFormat.Png);

          //return the current position in the stream at the beginning
          ms.Position = 0;

          buffer = new byte[ms.Length];
          ms.Read(buffer, 0, (int)ms.Length);
          return buffer;
        }
      }
    }

    public Bitmap ResizeImage(Stream stream)
    {
      Image originalImage = Bitmap.FromStream(stream);

      int height = 800;
      int width = 800;

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
