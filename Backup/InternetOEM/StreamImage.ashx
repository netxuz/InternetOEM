<%@ WebHandler Language="C#" Class="StreamImage" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

public class StreamImage : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        int id = -1;
        bool isNumber = int.TryParse(context.Request.QueryString["imageID"], out id);

        if (!isNumber)
        {
            context.Response.End();
        }

        byte[] imageData = GetImage(id);

        context.Response.ContentType = "image/jpeg";
        context.Response.BinaryWrite(imageData);
        context.Response.Flush();
    }

    private byte[] GetImage(int id)
    {
        //string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["TelerikConnectionString35"].ConnectionString;

        //using (SqlConnection conn = new SqlConnection(connectionString))
        //{
        //    string cmdText = "SELECT ImageData FROM AsyncUploadImages WHERE ImageID = @ImageID;";
            
        //    SqlCommand cmd = new SqlCommand(cmdText, conn);
        //    SqlParameter idParam = new SqlParameter("@ImageID", SqlDbType.Int);
        //    idParam.Value = id;
            
        //    cmd.Parameters.Add(idParam);
        //    conn.Open();
            
        //    using (SqlDataReader reader = cmd.ExecuteReader())
        //    {
        //        if (reader.Read())
        //        {
        //            byte[] imageData = (byte[])reader["ImageData"];
        //            return imageData;
        //        }
        //        else
        //        {
        //            throw new ArgumentException("Invalid ID");
        //        } 
        //    }
        //}
    }
 
    public bool IsReusable {
        get
        {
            return false;
        }
    }

}
