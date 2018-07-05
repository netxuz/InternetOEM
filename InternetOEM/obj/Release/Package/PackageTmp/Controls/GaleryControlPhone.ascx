<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GaleryControlPhone.ascx.cs" Inherits="ICommunity.Controls.GaleryControlPhone" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<div class="galery-phone">
  <telerik:RadListView ID="rdGaleryCntrl" runat="server" ItemPlaceholderID="idPHldGalery"
    OnNeedDataSource="rdGaleryCntrl_NeedDataSource" OnItemDataBound="rdGaleryCntrl_ItemDataBound">
    <LayoutTemplate>
      <div class="RadListView RadListViewFloated RLVCarList">
        <div class="rlvFloated rlvNoScroll">
          <asp:PlaceHolder ID="idPHldGalery" runat="server"></asp:PlaceHolder>
        </div>
      </div>
    </LayoutTemplate>
    <ItemTemplate>
      <div class="rlvIBlock">
        <div class="rdlvI">
          <asp:LinkButton ID="idLnkButton" runat="server" OnClick="oLinkButton_Click">
            <telerik:RadBinaryImage ID="idImgUser" runat="server" />
            <div class="rdlblNombre">
              <asp:Label ID="idLblNomUser" runat="server" Text=''></asp:Label>
            </div>
          </asp:LinkButton>
        </div>
      </div>
    </ItemTemplate>
  </telerik:RadListView>
</div>
