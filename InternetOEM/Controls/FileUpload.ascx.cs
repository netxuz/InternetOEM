using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Drawing;
using System.Xml.Linq;
using System.IO;
using System.Text;

using Telerik.Web.UI;
using Telerik.Web.UI.AsyncUpload;
using OnlineServices.Method;


namespace ICommunity.Controls
{
  public partial class FileUpload : System.Web.UI.UserControl
  {
    private string sUID = string.Empty;
    public string pUID { get { return sUID; } set { sUID = value; } }

    private string sUrlPage = string.Empty;
    public string pUrlPage { get { return sUrlPage; } set { sUrlPage = value; } }

    Culture oCulture = new Culture();
    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
        lblFotos.Text = oCulture.GetResource("MIsFotos", "lblCargarFotos");
      SampleAsyncUploadConfiguration config = RadAsyncUpload1.CreateDefaultUploadConfiguration<SampleAsyncUploadConfiguration>();
      config.UserID = sUID;
      RadAsyncUpload1.UploadConfiguration = config;
      RadAsyncUpload1.HttpHandlerUrl = sUrlPage;
      RadAsyncUpload1.FileUploaded += new FileUploadedEventHandler(RadAsyncUpload1_FileUploaded);

    }

    public void RadAsyncUpload1_FileUploaded(object sender, FileUploadedEventArgs e)
    {
      SampleAsyncUploadResult result = e.UploadResult as SampleAsyncUploadResult;
    }

  }
}