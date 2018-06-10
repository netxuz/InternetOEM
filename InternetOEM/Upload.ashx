<%@ WebHandler Language="C#" Class="Upload" %>

using System;
using System.Web;
using System.IO;
using System.Text;
using OnlineServices.Conn;
using OnlineServices.SystemData;
using OnlineServices.Method;

public class Upload : IHttpHandler
{

  public void ProcessRequest(HttpContext context)
  {
    context.Response.ContentType = "text/plain";
    context.Response.Expires = -1;
    try
    {
      HttpPostedFile postedFile = context.Request.Files["Filedata"];
      StringBuilder sPath = new StringBuilder();
      sPath.Append(Server.MapPath("."));
      sPath.Append(@"\rps_onlineservice\");
      sPath.Append(@"\usuarios\");
      sPath.Append(@"\usuario_");
      sPath.Append(context.Session["CodUsuario"].ToString());
      if (!Directory.Exists(sPath.ToString()))
        Directory.CreateDirectory(sPath.ToString());

      sPath.Append(@"\" + postedFile.FileName);
      postedFile.SaveAs(sPath.ToString());
      context.Response.Write("rps_onlineservice/" + postedFile.FileName);
      context.Response.StatusCode = 200;

      DBConn oConn = new DBConn();
      if (oConn.Open()){
        SysArchivosUsuarios oArchivosUsuarios = new SysArchivosUsuarios(ref oConn);
        oArchivosUsuarios.Accion = "CREAR";
        oArchivosUsuarios.CodUsuario = context.Session["CodUsuario"].ToString();
        oArchivosUsuarios.DateArchivo = DateTime.Now.ToString();
        oArchivosUsuarios.NomArchivo = postedFile.FileName;
        oArchivosUsuarios.Put();
        
        oConn.Close();
      }
    }
    catch (Exception ex)
    {
      context.Response.Write("Error: " + ex.Message);
    }
  }

  public bool IsReusable
  {
    get
    {
      return false;
    }
  }
}