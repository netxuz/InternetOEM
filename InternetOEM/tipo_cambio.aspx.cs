using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using OnlineServices.Conn;
using OnlineServices.Method;
using OnlineServices.SystemData;
using Telerik.Web.UI;

namespace ICommunity
{
  public partial class tipo_cambio : System.Web.UI.Page
  {
    Web oWeb = new Web();
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void rdGridTipoCambio_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        cMonedas oMonedas = new cMonedas(ref oConn);
        oMonedas.CodMoneda = rdTipoCambio.SelectedValue;
        DataTable dtMoneda = oMonedas.GetByCambio();
        if (dtMoneda != null)
          if (dtMoneda.Rows.Count > 0)
          {
            rdGridTipoCambio.DataSource = dtMoneda;
          }
          else
          {
            DataTable dummy = new DataTable();
            dummy.Columns.Add("cod_lic_moneda");
            dummy.Columns.Add("cod_moneda");
            dummy.Columns.Add("mes");
            dummy.Columns.Add("ano");
            dummy.Columns.Add("valor_moneda");
            dummy.Rows.Add();
            rdGridTipoCambio.DataSource = dummy;
          }
        dtMoneda = null;
        oConn.Close();
      }
    }

    protected void rdGridTipoCambio_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
      if (e.Item is GridDataItem)
      {
        GridDataItem item = (GridDataItem)e.Item;
        DataRowView row = (DataRowView)e.Item.DataItem;
        if (!string.IsNullOrEmpty(row["mes"].ToString()))
          item["mes"].Text = oWeb.getMes(int.Parse(row["mes"].ToString())).ToUpper();

        (item["Eliminar"].Controls[0] as Button).CssClass = "BtnColEliminar";
      }
    }

    protected void rdTipoCambio_SelectedIndexChanged(object sender, EventArgs e)
    {
      rdGridTipoCambio.Rebind();
    }

    protected void btn_grabar_Click(object sender, EventArgs e)
    {
      DBConn oConn = new DBConn();
      if (oConn.Open()) {
        cMonedas oMonedas = new cMonedas(ref oConn);
        oMonedas.CodMoneda = rsTpCmb.SelectedValue;
        oMonedas.FechaMoneda = DateTime.Now.Year.ToString() + rdCmbMeses.SelectedValue + "01";
        oMonedas.ValorMoneda = txt_valor.Text;
        oMonedas.Accion = "CREAR";
        oMonedas.Put();

        rdGridTipoCambio.Rebind();

        rdCmbMeses.SelectedValue = "0";
        rsTpCmb.SelectedValue = "1";
        txt_valor.Text = string.Empty;

        StringBuilder sHtml = new StringBuilder();
        sHtml.Append("  $(\"#centralModalWarning\").modal(); ");
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "centralModalWarning", sHtml.ToString(), true);
      }
      oConn.Close();
    }

    protected void rdGridTipoCambio_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
      switch (e.CommandName)
      {
        case "cmdDelete":
          string pCodLicMoneda = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["cod_lic_moneda"].ToString();
          DBConn oConn = new DBConn();
          if (oConn.Open())
          {
            cMonedas oMonedas = new cMonedas(ref oConn);
            oMonedas.CodLicMoneda = pCodLicMoneda;
            oMonedas.Accion = "ELIMINAR";
            oMonedas.Put();

            oConn.Close();
          }

          rdGridTipoCambio.Rebind();

          rdCmbMeses.SelectedValue = "0";
          rsTpCmb.SelectedValue = "1";
          txt_valor.Text = string.Empty;

          StringBuilder sHtml = new StringBuilder();
          sHtml.Append("  $(\"#centralModalWarning\").modal(); ");
          ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "centralModalWarning", sHtml.ToString(), true);

          break;
      }
    }
  }
}