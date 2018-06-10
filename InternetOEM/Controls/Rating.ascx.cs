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

using Telerik.Web.UI;
using System.Drawing;

namespace ICommunity.Controls
{
  public partial class Rating : System.Web.UI.UserControl
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        UpdateBinaryRatingLabel();
      }
    }

    protected void ratingBinary_Rate(object sender, EventArgs e)
    {
      UpdateBinaryRatingLabel();
    }

    private void UpdateBinaryRatingLabel()
    {
      lblBinaryRating.Text = "Rating: " + ratingBinary.Value.ToString();

      if (ratingBinary.Value < 0)
      {
        lblBinaryRating.ForeColor = System.Drawing.Color.Red;
      }
      else
      {
        lblBinaryRating.ForeColor = System.Drawing.Color.Green;
      }
    }
  }
}