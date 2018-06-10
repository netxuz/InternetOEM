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
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

using OnlineServices.Conn;
using OnlineServices.SystemData;
using OnlineServices.Method;
using Telerik.Web.UI;

namespace ICommunity
{
  public partial class LoadImgUser : System.Web.UI.UserControl
  {
    private OnlineServices.Method.Web oWeb = new OnlineServices.Method.Web();
    private OnlineServices.Method.Usuario oIsUsuario;
    protected void Page_Load(object sender, EventArgs e)
    {
      FileUploadImg.Attributes["onChange"] = "getStats('" + dispName.ClientID + "',this.value);";
      oIsUsuario = oWeb.GetObjUsuario();
      if (!IsPostBack) {
        StringBuilder sPath = new StringBuilder();
        sPath.Append(Server.MapPath(".")).Append(@"\binary\").Append("UserArchivo_").Append(oIsUsuario.CodUsuario).Append(".bin");
        if (File.Exists(sPath.ToString())) {
          DataTable dFile = oWeb.DeserializarTbl(Server.MapPath("."), "UserArchivo_" + oIsUsuario.CodUsuario + ".bin");
          if (dFile != null)
            if (dFile.Rows.Count > 0)
            {
              DataRow[] oRow = dFile.Select(" tip_archivo = 'P' ");
              if (oRow.Count() > 0)
                hddCodUsrImgFileProfile.Value = oRow[0]["cod_archivo"].ToString();
              oRow = null;
            }
          dFile = null;
        }
      }
    }

    protected void btnAceptar_Click(object sender, EventArgs e)
    {
      string pCodArchivo = string.Empty;
      DBConn oConn = new DBConn();
      try
      {
        if (FileUploadImg.HasFile)
        //foreach (UploadedFile file in FileUploadImg..UploadedFiles)
        {
          //byte[] imageData = GetImageBytes(file.InputStream);
          byte[] imageData = GetImageBytes(FileUploadImg.FileContent);
          StringBuilder sPath = new StringBuilder();
          StringBuilder sFile = new StringBuilder();
          sPath.Append(Server.MapPath("."));
          sPath.Append(@"\rps_onlineservice\");
          sPath.Append(@"\escorts\");
          sPath.Append(@"\escort_");
          sPath.Append(oIsUsuario.CodUsuario);
          if (!Directory.Exists(sPath.ToString()))
            Directory.CreateDirectory(sPath.ToString());

          if (oConn.Open())
          {
            oConn.BeginTransaction();
            SysArchivosUsuarios oArchivosUsuarios = new SysArchivosUsuarios(ref oConn);

            if (!string.IsNullOrEmpty(hddCodUsrImgFileProfile.Value)) {
              DataTable dFile = oWeb.DeserializarTbl(Server.MapPath("."), "UserArchivo_" + oIsUsuario.CodUsuario + ".bin");
              if (dFile != null)
                if (dFile.Rows.Count > 0)
                {
                  DataRow[] oRow = dFile.Select(" tip_archivo = 'P' ");
                  if (oRow.Count() > 0)
                  {
                    File.Delete(sPath.ToString() + @"\" + oRow[0]["nom_archivo"].ToString());
                    oArchivosUsuarios.CodArchivo = oRow[0]["cod_archivo"].ToString();
                    oArchivosUsuarios.Accion = "ELIMINAR";
                    oArchivosUsuarios.Put();
                  }
                  oRow = null;
                }
              dFile = null;
            }

            StringBuilder sNameFile = new StringBuilder();
            sNameFile.Append(DateTime.Now.Year.ToString());
            sNameFile.Append(DateTime.Now.Month.ToString());
            sNameFile.Append(DateTime.Now.Day.ToString());
            sNameFile.Append(DateTime.Now.Hour.ToString());
            sNameFile.Append(DateTime.Now.Minute.ToString());
            sNameFile.Append(DateTime.Now.Millisecond.ToString());

            //ObjectModel oObjectModel = new ObjectModel(ref oConn);
            //pCodArchivo = oObjectModel.getCodeKey("SYS_ARCHIVOS_USUARIOS");

            //sPath.Append(@"\").Append(pCodArchivo).Append(file.GetExtension());
            sPath.Append(@"\").Append(sNameFile.ToString()).Append(Path.GetExtension(FileUploadImg.FileName));
            //sFile.Append(pCodArchivo).Append(file.GetExtension());
            sFile.Append(sNameFile.ToString()).Append(Path.GetExtension(FileUploadImg.FileName));
            File.WriteAllBytes(sPath.ToString(), imageData);

            
            //oArchivosUsuarios.CodArchivo = pCodArchivo;
            oArchivosUsuarios.Accion = "CREAR";
            oArchivosUsuarios.CodUsuario = oIsUsuario.CodUsuario;
            oArchivosUsuarios.DateArchivo = DateTime.Now.ToString();
            oArchivosUsuarios.NomArchivo = sFile.ToString();
            oArchivosUsuarios.TipArchivo = "P";
            oArchivosUsuarios.Put();
            pCodArchivo = oArchivosUsuarios.CodArchivo;
            hddCodUsrImgFileProfile.Value = pCodArchivo;

            if (string.IsNullOrEmpty(oArchivosUsuarios.Error))
            {
              oConn.Commit();
              string cPath = Server.MapPath(".") + @"\binary\";
              string sFileUsrArchivo = "UserArchivo_" + oIsUsuario.CodUsuario + ".bin";
              oArchivosUsuarios.SerializaTblUserArchivo(ref oConn, cPath, sFileUsrArchivo);
            }
            else
              oConn.Rollback();

            oConn.Close();
          }
        }
        Response.Redirect(".");
      }
      catch (Exception Ex)
      {
        if (oConn.bIsOpen)
        {
          oConn.Rollback();
          oConn.Close();
        }
      }
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
      System.Drawing.Image originalImage = Bitmap.FromStream(stream);

      int height = 300;
      int width = 300;

      double ratio = System.Math.Min(originalImage.Width, originalImage.Height) / (double)System.Math.Max(originalImage.Width, originalImage.Height);

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