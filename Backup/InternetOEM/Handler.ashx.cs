using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.IO;
using Telerik.Web.UI;

using OnlineServices.Conn;
using OnlineServices.SystemData;
using OnlineServices.Method;

namespace ICommunity
{
  /// <summary>
  /// Summary description for $codebehindclassname$
  /// </summary>
  [WebService(Namespace = "http://tempuri.org/")]
  [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
  public class Handler : AsyncUploadHandler, System.Web.SessionState.IRequiresSessionState
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
        sPath.Append(@"\escorts\");
        sPath.Append(@"\escort_");
        sPath.Append(userID);
        if (!Directory.Exists(sPath.ToString()))
          Directory.CreateDirectory(sPath.ToString());
        
        if (oConn.Open())
        {
          oConn.BeginTransaction();

          //ObjectModel oObjectModel = new ObjectModel(ref oConn);
          //pCodArchivo = oObjectModel.getCodeKey("SYS_ARCHIVOS_USUARIOS");
          StringBuilder sNameFile = new StringBuilder();
          sNameFile.Append(DateTime.Now.Year.ToString());
          sNameFile.Append(DateTime.Now.Month.ToString());
          sNameFile.Append(DateTime.Now.Day.ToString());
          sNameFile.Append(DateTime.Now.Hour.ToString());
          sNameFile.Append(DateTime.Now.Minute.ToString());
          sNameFile.Append(DateTime.Now.Millisecond.ToString());

          sPath.Append(@"\").Append(sNameFile.ToString()).Append(file.GetExtension());
          sFile.Append(sNameFile.ToString()).Append(file.GetExtension());
          File.WriteAllBytes(sPath.ToString(), imageData);
          imageData = null;
          setWaterMark(sPath);

          SysArchivosUsuarios oArchivosUsuarios = new SysArchivosUsuarios(ref oConn);
          //oArchivosUsuarios.CodArchivo = pCodArchivo;
          oArchivosUsuarios.Accion = "CREAR";
          oArchivosUsuarios.CodUsuario = userID;
          oArchivosUsuarios.DateArchivo = DateTime.Now.ToString();
          oArchivosUsuarios.NomArchivo = sFile.ToString();
          oArchivosUsuarios.Put();
          pCodArchivo = oArchivosUsuarios.CodArchivo;
            
          if (string.IsNullOrEmpty(oArchivosUsuarios.Error))
          {
            oConn.Commit();
            string cPath = HttpContext.Current.Server.MapPath(".") + @"\binary\";
            string sFileUsrArchivo = "UserArchivo_" + userID + ".bin";
            oArchivosUsuarios.SerializaTblUserArchivo(ref oConn, cPath, sFileUsrArchivo);
          }else
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

      using (Bitmap image = ResizeImage(stream, 800, 800))
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

    public Bitmap ResizeImage(Stream stream, int width, int height)
    {
      Image originalImage = Bitmap.FromStream(stream);

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
        originalImage.Dispose();
        return scaledImage;
      }

    }

    public void setWaterMark(StringBuilder sPathImage) {
      string Copyright = "Copyright © 2014 - Derechos exclusivos reservados por Escorts Club";

      StringBuilder sFile = new StringBuilder();
      sFile.Append(HttpContext.Current.Server.MapPath("."));
      sFile.Append(@"\style\images\watermark.png");

      Image imgPhoto = Image.FromFile(sPathImage.ToString());
      int phWidth = imgPhoto.Width;
      int phHeight = imgPhoto.Height;

      //create a Bitmap the Size of the original photograph
      Bitmap bmPhoto = new Bitmap(phWidth, phHeight, PixelFormat.Format16bppRgb565);
      bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

      //load the Bitmap into a Graphics object 
      Graphics grPhoto = Graphics.FromImage(bmPhoto);

      Image imgWatermark;
      if (phWidth < 800)
      {
        FileStream oFileStrem = File.Open(sFile.ToString(), FileMode.Open);
        imgWatermark = ResizeImage(oFileStrem, phWidth, phHeight);
        oFileStrem = null;
      }
      else
      {
        imgWatermark = new Bitmap(sFile.ToString());
      }

      //create a image object containing the watermark
      int wmWidth = imgWatermark.Width;
      int wmHeight = imgWatermark.Height;

      //------------------------------------------------------------
      //Step #1 - Insert Copyright message
      //------------------------------------------------------------

      //Set the rendering quality for this Graphics object
      grPhoto.SmoothingMode = SmoothingMode.AntiAlias;

      //Draws the photo Image object at original size to the graphics object.
      grPhoto.DrawImage(
          imgPhoto,                               // Photo Image object
          new Rectangle(0, 0, phWidth, phHeight), // Rectangle structure
          0,                                      // x-coordinate of the portion of the source image to draw. 
          0,                                      // y-coordinate of the portion of the source image to draw. 
          phWidth,                                // Width of the portion of the source image to draw. 
          phHeight,                               // Height of the portion of the source image to draw. 
          GraphicsUnit.Pixel);                    // Units of measure 

      //-------------------------------------------------------
      //to maximize the size of the Copyright message we will 
      //test multiple Font sizes to determine the largest posible 
      //font we can use for the width of the Photograph
      //define an array of point sizes you would like to consider as possiblities
      //-------------------------------------------------------
      int[] sizes = new int[] { 16, 14, 12, 10, 8, 6, 4 };

      Font crFont = null;
      SizeF crSize = new SizeF();

      //Loop through the defined sizes checking the length of the Copyright string
      //If its length in pixles is less then the image width choose this Font size.
      for (int i = 0; i < 7; i++)
      {
        //set a Font object to Arial (i)pt, Bold
        crFont = new Font("arial", sizes[i], FontStyle.Bold);
        //Measure the Copyright string in this Font
        crSize = grPhoto.MeasureString(Copyright, crFont);

        if ((ushort)crSize.Width < (ushort)phWidth)
          break;
      }

      //Since all photographs will have varying heights, determine a 
      //position 5% from the bottom of the image
      int yPixlesFromBottom = (int)(phHeight * .05);

      //Now that we have a point size use the Copyrights string height 
      //to determine a y-coordinate to draw the string of the photograph
      float yPosFromBottom = ((phHeight - yPixlesFromBottom) - (crSize.Height / 2));

      //Determine its x-coordinate by calculating the center of the width of the image
      float xCenterOfImg = (phWidth / 2);

      //Define the text layout by setting the text alignment to centered
      StringFormat StrFormat = new StringFormat();
      StrFormat.Alignment = StringAlignment.Center;

      //define a Brush which is semi trasparent black (Alpha set to 153)
      SolidBrush semiTransBrush2 = new SolidBrush(Color.FromArgb(153, 0, 0, 0));

      //Draw the Copyright string
      grPhoto.DrawString(Copyright,                 //string of text
          crFont,                                   //font
          semiTransBrush2,                           //Brush
          new PointF(xCenterOfImg + 1, yPosFromBottom + 1),  //Position
          StrFormat);

      //define a Brush which is semi trasparent white (Alpha set to 153)
      SolidBrush semiTransBrush = new SolidBrush(Color.FromArgb(153, 255, 255, 255));

      //Draw the Copyright string a second time to create a shadow effect
      //Make sure to move this text 1 pixel to the right and down 1 pixel
      grPhoto.DrawString(Copyright,                 //string of text
          crFont,                                   //font
          semiTransBrush,                           //Brush
          new PointF(xCenterOfImg, yPosFromBottom),  //Position
          StrFormat);                               //Text alignment

      //------------------------------------------------------------
      //Step #2 - Insert Watermark image
      //------------------------------------------------------------

      //Create a Bitmap based on the previously modified photograph Bitmap
      Bitmap bmWatermark = new Bitmap(bmPhoto);
      bmWatermark.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);
      //Load this Bitmap into a new Graphic Object
      Graphics grWatermark = Graphics.FromImage(bmWatermark);
     
      grWatermark.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
      grWatermark.DrawImage(imgWatermark,
          new Rectangle(0, phHeight / 3, wmWidth, wmHeight),
          0,
          0,
          wmWidth,
          wmHeight,
          GraphicsUnit.Pixel);

      //Replace the original photgraphs bitmap with the new Bitmap
      //imgPhoto = bmWatermark;
      imgPhoto.Dispose();
      bmPhoto.Dispose();
      grPhoto.Dispose();
      imgWatermark.Dispose();
      grWatermark.Dispose();
            
      //save new image to file system.
      bmWatermark.Save(sPathImage.ToString(), ImageFormat.Jpeg);
      bmWatermark.Dispose();

    }

  }
}
