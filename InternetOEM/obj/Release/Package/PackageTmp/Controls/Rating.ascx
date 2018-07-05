<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Rating.ascx.cs" Inherits="ICommunity.Controls.Rating" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<style type="text/css">
  .divBigRating, .divSmallRating
  {
    text-align: center;
    margin: 1.6em 0;
  }
  .divBigRating
  {
    width: 120px;
    height: 92px;
    background: url( 'Style/Images/Rating2/rating_back_big.png' ) no-repeat;
  }
  .divSmallRating
  {
    width: 120px;
    height: 37px;
    background: url( 'Style/Images/Rating2/rating_back_small.png' ) no-repeat;
  }
  .ratingClass, .ratingClass1, .ratingClass2
  {
    position: relative;
    top: 17px;
    text-align: left;
    margin: 0 auto;
    padding: 0;
  }
  .ratingClass1
  {
    top: 10px;
  }
  .ratingClass2
  {
    top: 7px;
  }
  #tableWrapper TR TD
  {
    width: 300px;
    height: 130px;
    padding-left: 50px;
  }
</style>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
  <AjaxSettings>
    <telerik:AjaxSetting AjaxControlID="ratingBinary" EventName="Rate">
      <UpdatedControls>
        <telerik:AjaxUpdatedControl ControlID="lblBinaryRating" UpdatePanelRenderMode="Inline" />
      </UpdatedControls>
    </telerik:AjaxSetting>
  </AjaxSettings>
</telerik:RadAjaxManager>
<asp:Label ID="lblBinaryRating" runat="server" Font-Size="Medium"></asp:Label>
<div class="divSmallRating">
  <telerik:RadRating ID="ratingBinary" runat="server" Orientation="Horizontal" SelectionMode="Single"
    ItemHeight="20px" ItemWidth="20px" CssClass="ratingClass1" OnRate="ratingBinary_Rate"
    AutoPostBack="true">
    <Items>
      <telerik:RadRatingItem Value="-1" HoveredImageUrl="Style/Images/Rating2/downh.png"
        HoveredSelectedImageUrl="Style/Images/Rating2/downh.png" SelectedImageUrl="Style/Images/Rating2/downh.png"
        ImageUrl="Style/Images/Rating2/down.png" ToolTip="No" />
      <telerik:RadRatingItem Value="0" HoveredImageUrl="Style/Images/Rating2/0h.png" HoveredSelectedImageUrl="Style/Images/Rating2/0h.png"
        SelectedImageUrl="Style/Images/Rating2/0.png" ImageUrl="Style/Images/Rating2/0.png"
        ToolTip="Reset Current Rating" />
      <telerik:RadRatingItem Value="1" HoveredImageUrl="Style/Images/Rating2/uph.png" HoveredSelectedImageUrl="Style/Images/Rating2/uph.png"
        SelectedImageUrl="Style/Images/Rating2/uph.png" ImageUrl="Style/Images/Rating2/up.png"
        ToolTip="Yes" />
    </Items>
  </telerik:RadRating>
</div>
