using System;
using System.IO;
using System.Text;
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
using Ionic.Zip;
using OnlineServices.Method;

namespace ICommunity
{
  public partial class HojasEstilos : System.Web.UI.Page
  {
    Web oWeb = new Web();
    Culture oCulture = new Culture();
    UploadedFile oUpload;
    protected void Page_Load(object sender, EventArgs e)
    {
      oWeb.ValidaSessionAdm();
      if (!IsPostBack) {
        btnGrabar.Text = oCulture.GetResource("Global", "btnGrabar");
        btnGrabar.ToolTip = oCulture.GetResource("Global", "btnGrabar");

        lblCss.Text = oCulture.GetResource("HojasEstilos", "lblCss");
        lblZip.Text = oCulture.GetResource("HojasEstilos", "lblZip");
        getAsignados();
        getDisponibles();
      }
      
    }

    protected void btnGrabar_Click(object sender, EventArgs e)
    {
      StringBuilder sPath = new StringBuilder();
      sPath.Append(Server.MapPath("."));
      sPath.Append(@"\style\");

      //--- Sube Css al repositorio del sitio ---//
      string sExt;
      string sFile;
      sExt = ".css";
      rdUploadFileCss.AllowedFileExtensions = sExt.Split(new string[] { "," }, StringSplitOptions.None);
      if (rdUploadFileCss.UploadedFiles.Count > 0)
      {
        oUpload = rdUploadFileCss.UploadedFiles[0];
        oUpload.SaveAs(sPath.ToString() + oUpload.GetName());

      }
      //--- Sube Zip de imagenes al repositorio del sitio ---//
      sPath.Append(@"\images\");
      sExt = ".zip";
      rdUploadZip.AllowedFileExtensions = sExt.Split(new string[] { "," }, StringSplitOptions.None);
      if (rdUploadZip.UploadedFiles.Count > 0)
      {
        oUpload = rdUploadZip.UploadedFiles[0];
        sFile = oUpload.GetName();
        oUpload.SaveAs(sPath.ToString() + sFile.ToString());

        if (sFile.IndexOf(".zip", StringComparison.OrdinalIgnoreCase) != -1)
        {
          //Descomprime Archivo Zip.
          using (ZipFile oZip = ZipFile.Read(sPath.ToString() + sFile.ToString()))
          {
            foreach (ZipEntry oZipEntry in oZip)
            {
              oZipEntry.Extract(sPath.ToString(), ExtractExistingFileAction.OverwriteSilently);
            }
          }
        }
      }

      sPath = new StringBuilder();
      sPath.Append(Server.MapPath("."));
      sPath.Append(@"\style\");

      StreamWriter sw = File.CreateText(sPath.ToString() + "masterstyle.css");
      if (rdListAsignados.Items.Count > 0)
      {
        for (int i = 0; i < rdListAsignados.Items.Count; i++)
        {
          sw.WriteLine("@import url(\"" + rdListAsignados.Items[i].Value + "\");");
        }
      }
      sw.Close();
    }

    protected void getAsignados()
    {
      StringBuilder sPath = new StringBuilder();
      sPath.Append(Server.MapPath("."));
      sPath.Append(@"\style\");

      DirectoryInfo oDirectory = new DirectoryInfo(sPath.ToString());
      if (!oDirectory.Exists)
        Directory.CreateDirectory(sPath.ToString());

      if (!File.Exists(sPath.ToString() + "masterstyle.css")) {
        StreamWriter sw = File.CreateText(sPath.ToString() + "masterstyle.css");
        sw.Close();
      }

      StreamReader sr = new StreamReader(sPath.ToString() + "masterstyle.css");
      string sMasterStyle = sr.ReadToEnd();
      sr.Close();

      string[] files = Directory.GetFiles(sPath.ToString(), "*.css");
      foreach (string file in files)
      {
        if (file.Replace(sPath.ToString(), "") != "masterstyle.css")
        {
          if (sMasterStyle.IndexOf(file.Replace(sPath.ToString(), ""), StringComparison.OrdinalIgnoreCase) > 0)
          {
            rdListAsignados.Items.Add(new RadListBoxItem(file.Replace(sPath.ToString(), ""), file.Replace(sPath.ToString(), "")));
          }
        }
      }

    }

    protected void getDisponibles() {
      StringBuilder sPath = new StringBuilder();
      sPath.Append(Server.MapPath("."));
      sPath.Append(@"\style\");

      DirectoryInfo oDirectory = new DirectoryInfo(sPath.ToString());
      if (!oDirectory.Exists)
        Directory.CreateDirectory(sPath.ToString());
      //--- Pregunta si existe el archivo global.css ---
      if (!File.Exists(sPath.ToString() + "masterstyle.css"))
      {
        StreamWriter sw = File.CreateText(sPath.ToString() + "masterstyle.css");
        sw.Close();
      }

      StreamReader sr = new StreamReader(sPath.ToString() + "masterstyle.css");
      string sMasterStyle = sr.ReadToEnd();
      sr.Close();

      string[] files = Directory.GetFiles(sPath.ToString(), "*.css");
      foreach (string file in files)
      {
        if (file.Replace(sPath.ToString(), "") != "masterstyle.css")
        {
          if (sMasterStyle.IndexOf(file.Replace(sPath.ToString(), ""), StringComparison.OrdinalIgnoreCase) == -1)
          {
            rdListDisponibles.Items.Add(new RadListBoxItem(file.Replace(sPath.ToString(), ""), file.Replace(sPath.ToString(), "")));
          }
        }
      }

    }

    protected void rdListDisponibles_Deleted(object sender, RadListBoxEventArgs e)
    {
      StringBuilder sPath = new StringBuilder();
      sPath.Append(Server.MapPath("."));
      sPath.Append(@"\style\");
      sPath.Append(e.Items[0].Value);
      if (File.Exists(sPath.ToString()))
        File.Delete(sPath.ToString());
    }
    
  }
}
