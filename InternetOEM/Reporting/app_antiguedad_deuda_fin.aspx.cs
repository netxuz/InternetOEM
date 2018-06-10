using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using OnlineServices.Conn;
using OnlineServices.Method;
using OnlineServices.Reporting;
using Telerik.Web.UI;
using ClosedXML.Excel;

namespace ICommunity.Reporting
{
  public partial class app_antiguedad_deuda_fin : System.Web.UI.Page
  {
    private OnlineServices.Method.Web oWeb = new OnlineServices.Method.Web();
    private OnlineServices.Method.Usuario oIsUsuario;
    protected void Page_Load(object sender, EventArgs e)
    {
      oIsUsuario = oWeb.ValidaUserAppReport();
      oIsUsuario = oWeb.GetObjUsuario();

      DateTime dTimeNow = DateTime.Now;
      getMenu(idReportePago, oIsUsuario.CodUsuario, "1");
      getMenu(idProcesoSeguimiento, oIsUsuario.CodUsuario, "2");
      getMenu(idCartolas, oIsUsuario.CodUsuario, "3");
      getMenu(idProcesoNormalizacion, oIsUsuario.CodUsuario, "4");
      getMenu(idIndicadoresClaves, oIsUsuario.CodUsuario, "5");
      getMenu(IndClasificacionRiesgo, oIsUsuario.CodUsuario, "6");

    }

    protected void getMenu(System.Web.UI.HtmlControls.HtmlGenericControl oHtmControl, string pCodUser, string oOrdConsulta)
    {
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        cReportes oReportes = new cReportes(ref oConn);
        oReportes.CodUser = pCodUser;
        oReportes.OrdConsulta = oOrdConsulta;
        DataTable dtQuery = oReportes.GetMenu();
        if (dtQuery != null)
        {
          if (dtQuery.Rows.Count > 0)
          {
            foreach (DataRow oRow in dtQuery.Rows)
            {
              oHtmControl.Controls.Add(new LiteralControl("<li><a href=\"" + oRow["url_consulta_new"].ToString() + "\">" + oRow["nom_consulta"].ToString() + "</a></li>"));
            }
          }
        }
        dtQuery = null;
      }
      oConn.Close();
    }

    protected void idBuscar_Click(object sender, EventArgs e)
    {
      idGrilla.Visible = true;
      rdGridAntiguedadDeuda.Rebind();
    }

    protected void rdGridAntiguedadDeuda_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
      rdGridAntiguedadDeuda.DataSource = getDatatble();
    }

    protected void rdGridAntiguedadDeuda_ItemCommand(object source, GridCommandEventArgs e)
    {
      if (e.CommandName == RadGrid.ExportToExcelCommandName)
      {
        DataTable dt = getDatatble();

        XLWorkbook wb = new XLWorkbook();
        wb.Worksheets.Add(dt, "antiguedaddeuda");

        Response.Clear();
        Response.Buffer = true;
        Response.Charset = "";
        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        Response.AddHeader("content-disposition", "attachment;filename=antiguedaddeuda_" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx");
        using (MemoryStream MyMemoryStream = new MemoryStream())
        {
          wb.SaveAs(MyMemoryStream);
          MyMemoryStream.WriteTo(Response.OutputStream);
          Response.Flush();
          Response.End();
        }
      }
    }

    public DataTable getDatatble()
    {
      DataTable dt = null;

      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        cAntiguedadDeuda oAntiguedadDeuda = new cAntiguedadDeuda(ref oConn);
        oAntiguedadDeuda.CodDeudor = hddCodDeudor.Value;
        oAntiguedadDeuda.CodNkey = oIsUsuario.CodNkey;
        oAntiguedadDeuda.Estado = RdBtnTypeQuery.SelectedValue;
        oAntiguedadDeuda.NkeyUsuario = oIsUsuario.NKeyUsuario;
        oAntiguedadDeuda.TipoUsuario = oIsUsuario.TipoUsuario;
        dt = oAntiguedadDeuda.Get();

      }
      oConn.Close();

      return dt;
    }

    protected void rdGridAntiguedadDeuda_PreRender(object source, EventArgs e)
    {
      if (RdBtnTypeQuery.SelectedValue == "1")
      {
        rdGridAntiguedadDeuda.MasterTableView.GetColumn("sTipoDocumento").Visible = false;
        rdGridAntiguedadDeuda.MasterTableView.GetColumn("nNumeroFactura").Visible = false;

        rdGridAntiguedadDeuda.MasterTableView.GetColumn("ncodigodeudor").Visible = true;
        rdGridAntiguedadDeuda.MasterTableView.GetColumn("snombre").Visible = true;
      }
      else {
        rdGridAntiguedadDeuda.MasterTableView.GetColumn("ncodigodeudor").Visible = false;
        rdGridAntiguedadDeuda.MasterTableView.GetColumn("snombre").Visible = false;

        rdGridAntiguedadDeuda.MasterTableView.GetColumn("sTipoDocumento").Visible = true;
        rdGridAntiguedadDeuda.MasterTableView.GetColumn("nNumeroFactura").Visible = true;
      }
    }
  }
}