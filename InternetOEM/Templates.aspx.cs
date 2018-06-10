using System;
using System.IO;
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
using OnlineServices.Conn;
using OnlineServices.CmsData;
using OnlineServices.Method;

namespace ICommunity
{
  public partial class Templates : System.Web.UI.Page
  {
    public object TimeStamp{
      get {
        byte[] bt = new byte[8];
        for (int i = 0; i < 8; i++)
        {
          bt[i] =
              Convert.ToByte(
              ViewState["TimeStamp"].ToString().Substring(i * 3, 2), 16);
        }
        return bt;
      }
      set {
        ViewState["TimeStamp"] = BitConverter.ToString((byte[])value);
      }
    }

    Web oWeb = new Web();
    Culture oCulture = new Culture();

    protected void Page_Load(object sender, EventArgs e)
    {
      oWeb.ValidaSessionAdm();
      btnCrear.Text = oCulture.GetResource("Global", "btnCrear");
      btnCrear.ToolTip = oCulture.GetResource("Global", "btnCrear");
    }

    protected void rdTemplate_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
      DBConn oConn = new DBConn();
      if (oConn.Open()) {

        CmsTemplate oTemplate = new CmsTemplate(ref oConn);
        oTemplate.Get();

        GridColumn oGridColumn;

        oGridColumn = rdTemplate.MasterTableView.Columns.FindByUniqueName("NomTemplate");
        oGridColumn.HeaderText = oCulture.GetResource("Template", "NomTemplate");

        oGridColumn = rdTemplate.MasterTableView.Columns.FindByUniqueName("EstTemplate");
        oGridColumn.HeaderText = oCulture.GetResource("Template", "EstTemplate");

        rdTemplate.DataSource = oTemplate.Get();

        oConn.Close();
      }

    }

    protected void rdTemplate_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
      if (e.Item is GridDataItem)
      {
        GridDataItem oItem = e.Item as GridDataItem;

        (oItem["Editar"].Controls[0] as Button).CssClass = "BtnColEditar";
        (oItem["Eliminar"].Controls[0] as Button).CssClass = "BtnColEliminar";

        if ((!string.IsNullOrEmpty(oItem["EstTemplate"].Text)) && (oItem["EstTemplate"].Text != "&nbsp;"))
        { 
          if (oItem["EstTemplate"].Text == "V")
            oItem["EstTemplate"].Text = oCulture.GetResource("Template", "Vigente");
          else
            oItem["EstTemplate"].Text = oCulture.GetResource("Template", "NoVigente");
        }
      }
    }

    protected void rdTemplate_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
      switch (e.CommandName)
      {
        case "cmdEdit":
          string[] cParam = new string[2];
          cParam[0] = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["cod_template"].ToString();
          Response.Redirect(String.Format("Template.aspx?CodTemplate={0}", cParam));
          break;
        case "cmdDelete":
          string pCodTemplate = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["cod_template"].ToString();
          DBConn oConn = new DBConn();
          if (oConn.Open()) {
            string sPath = Server.MapPath(".") + @"\binary\Template_" + pCodTemplate + ".bin";
            File.Delete(sPath);

            CmsTemplate oTemplate = new CmsTemplate(ref oConn);
            oTemplate.CodTemplate = pCodTemplate;
            oTemplate.Accion = "ELIMINAR";
            oTemplate.Put();

            oConn.Close();
          }
          rdTemplate.Rebind();
          break;
      }
    }

    protected void btnCrear_Click(object sender, EventArgs e)
    {
      Response.Redirect("Template.aspx");
    }
  }
}
