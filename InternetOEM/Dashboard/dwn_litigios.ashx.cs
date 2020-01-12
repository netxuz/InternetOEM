using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;

using OnlineServices.Conn;
using OnlineServices.Method;
using OnlineServices.Reporting;
using OnlineServices.Dashboard;
using ClosedXML.Excel;

namespace ICommunity.Dashboard
{
  /// <summary>
  /// Summary description for dwn_litigios
  /// </summary>
  public class dwn_litigios : IHttpHandler
  {
    private OnlineServices.Method.Web oWeb = new OnlineServices.Method.Web();
    public void ProcessRequest(HttpContext context)
    {
      DataTable dt = null;
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        cLitigios oDetalle = new cLitigios(ref oConn);
        oDetalle.nKeyCliente = oWeb.GetData("hdd_cliente");
        oDetalle.CodHolding = oWeb.GetData("hdd_holding");
        
        if (!string.IsNullOrEmpty(oWeb.GetData("nom_cliente")))
          oDetalle.NomCliente = oWeb.GetData("nom_cliente");

        if (!string.IsNullOrEmpty(oWeb.GetData("num_factura")))
          oDetalle.NumFactura = oWeb.GetData("num_factura");

        if (!string.IsNullOrEmpty(oWeb.GetData("hdd_periodo")))
        {
          oDetalle.Periodo = oWeb.GetData("hdd_periodo");
          oDetalle.OrderBy = true;
        }

        dt = oDetalle.getDetalle();

      }
      oConn.Close();

      System.Web.HttpResponse oResponse = System.Web.HttpContext.Current.Response;
      XLWorkbook wb = new XLWorkbook();
      wb.Worksheets.Add(dt, "ReporteVentas");

      oResponse.Clear();
      oResponse.Buffer = true;
      oResponse.Charset = "";
      oResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
      oResponse.AddHeader("content-disposition", "attachment;filename=Reporte_Litigios_" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx");
      using (MemoryStream MyMemoryStream = new MemoryStream())
      {
        wb.SaveAs(MyMemoryStream);
        MyMemoryStream.WriteTo(oResponse.OutputStream);
        oResponse.Flush();
        oResponse.End();
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
}