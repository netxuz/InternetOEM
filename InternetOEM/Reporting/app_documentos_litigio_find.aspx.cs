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
  public partial class app_documentos_litigio_find : System.Web.UI.Page
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
      if (!IsPostBack)
      {
        RadDatePicker1.DateInput.DateFormat = "dd-MM-yyyy";
        RadDatePicker2.DateInput.DateFormat = "dd-MM-yyyy";
        RadDatePicker1.SelectedDate = dTimeNow.AddMonths(-1);
        RadDatePicker2.SelectedDate = dTimeNow;
      }

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
      rdGridDocLitigio.Rebind();
    }

    protected void rdGridDocLitigio_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
      rdGridDocLitigio.DataSource = getDatatble();
    }

    protected void rdGridDocLitigio_ItemCommand(object source, GridCommandEventArgs e)
    {
      if (e.CommandName == RadGrid.ExportToExcelCommandName)
      {
        DataTable dt = getDatatble();

        XLWorkbook wb = new XLWorkbook();
        wb.Worksheets.Add(dt, "documentoslitigio");

        Response.Clear();
        Response.Buffer = true;
        Response.Charset = "";
        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        Response.AddHeader("content-disposition", "attachment;filename=documentoslitigio_" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx");
        using (MemoryStream MyMemoryStream = new MemoryStream())
        {
          wb.SaveAs(MyMemoryStream);
          MyMemoryStream.WriteTo(Response.OutputStream);
          Response.Flush();
          Response.End();
        }
      }
    }

    protected void rdGridDocLitigio_ItemDataBound(object sender, GridItemEventArgs e)
    {

    }

    public DataTable getDatatble()
    {
      DataTable dt = null;

      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        cDocumentosLitigio oDocumentosLitigio = new cDocumentosLitigio(ref oConn);
        oDocumentosLitigio.CodDeudor = hddCodDeudor.Value;
        oDocumentosLitigio.NumFactura = rdTxtNumFactura.Text;
        oDocumentosLitigio.IndAccion = bool.Parse(rdRdBtnTypeQuery.SelectedValue);
        oDocumentosLitigio.CodNkey = oIsUsuario.CodNkey;
        oDocumentosLitigio.NkeyUsuario = oIsUsuario.NKeyUsuario;
        oDocumentosLitigio.TipoUsuario = oIsUsuario.TipoUsuario;
        oDocumentosLitigio.DtFchIni = DateTime.Parse(RadDatePicker1.SelectedDate.ToString()).ToString("yyyyMMdd");
        oDocumentosLitigio.DtFchFin = DateTime.Parse(RadDatePicker2.SelectedDate.ToString()).ToString("yyyyMMdd");
        dt = oDocumentosLitigio.Get();

      }
      oConn.Close();

      return dt;
    }

  }
}