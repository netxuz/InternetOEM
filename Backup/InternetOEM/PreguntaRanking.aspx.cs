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

using OnlineServices.Conn;
using OnlineServices.Method;
using OnlineServices.AppData;
using Telerik.Web.UI;

namespace ICommunity
{
  public partial class PreguntaRanking : System.Web.UI.Page
  {
    Web oWeb = new Web();
    protected void Page_Load(object sender, EventArgs e)
    {
      oWeb.ValidaSessionAdm();
    }

    protected void rdPreguntaRanking_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        AppPregRanking oPregRanking = new AppPregRanking(ref oConn);
        oPregRanking.EstPregRanking = "V";
        rdPreguntaRanking.DataSource = oPregRanking.Get();
      }
      oConn.Close();
    }

    protected void rdPreguntaRanking_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
      GridEditFormInsertItem insertedItem = (GridEditFormInsertItem)e.Item;
      string sPregRanking = (insertedItem["PregRanking"].Controls[0] as TextBox).Text;
      try
      {
        DBConn oConn = new DBConn();
        if (oConn.Open())
        {
          AppPregRanking oPregRanking = new AppPregRanking(ref oConn);
          oPregRanking.EstPregRanking = "V";
          oPregRanking.PregRanking = sPregRanking;
          oPregRanking.Accion = "CREAR";
          oPregRanking.Put();
          if (string.IsNullOrEmpty(oPregRanking.Error))
          {
            oPregRanking.SerializaTblPregRanking(ref oConn, Server.MapPath(".") + @"\binary\");
          }
          else
          {
            rdPreguntaRanking.Controls.Add(new LiteralControl("Unable to insert Employee. Reason: " + oPregRanking.Error));
            e.Canceled = true;
          }
          oConn.Close();
        }
        else
        {
          rdPreguntaRanking.Controls.Add(new LiteralControl("Unable to insert Employee. Reason: " + oConn.Error));
          e.Canceled = true;
        }
      }
      catch (Exception ex)
      {
        rdPreguntaRanking.Controls.Add(new LiteralControl("Unable to insert Employee. Reason: " + ex.Message));
        e.Canceled = true;
      }
    }

    protected void rdPreguntaRanking_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
      GridEditableItem editedItem = e.Item as GridEditableItem;
      string sCodPregRanking = editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["cod_preg_ranking"].ToString();
      string sPregRanking = (editedItem["PregRanking"].Controls[0] as TextBox).Text;  
      try
      {
        DBConn oConn = new DBConn();
        if (oConn.Open())
        {
          AppPregRanking oPregRanking = new AppPregRanking(ref oConn);
          oPregRanking.CodPregRanking = sCodPregRanking;
          oPregRanking.PregRanking = sPregRanking;
          oPregRanking.Accion = "EDITAR";
          oPregRanking.Put();
          if (string.IsNullOrEmpty(oPregRanking.Error))
          {
            oPregRanking.SerializaTblPregRanking(ref oConn, Server.MapPath(".") + @"\binary\");
          }
          else
          {
            rdPreguntaRanking.Controls.Add(new LiteralControl("Unable to insert Employee. Reason: " + oPregRanking.Error));
            e.Canceled = true;
          }
          oConn.Close();
        }
        else
        {
          rdPreguntaRanking.Controls.Add(new LiteralControl("Unable to insert Employee. Reason: " + oConn.Error));
          e.Canceled = true;
        }
      }
      catch (Exception ex)
      {
        rdPreguntaRanking.Controls.Add(new LiteralControl("Unable to insert Employee. Reason: " + ex.Message));
        e.Canceled = true;
      }
    }

    protected void rdPreguntaRanking_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
      DBConn oConn = new DBConn();
      if (oConn.Open())
      {
        string pCodPregRanking = (e.Item.ItemIndex > -1 ? e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["cod_preg_ranking"].ToString() : string.Empty);
        AppPregRanking oPregRanking = new AppPregRanking(ref oConn);
        oPregRanking.CodPregRanking = pCodPregRanking;
        oPregRanking.Accion = "EDITAR";
        oPregRanking.EstPregRanking = "E";
        oPregRanking.Put();
        oPregRanking.SerializaTblPregRanking(ref oConn, Server.MapPath(".") + @"\binary\");

        oConn.Close();
      }
    }

  }
}
