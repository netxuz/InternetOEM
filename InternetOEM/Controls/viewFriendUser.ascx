<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="viewFriendUser.ascx.cs"
  Inherits="ICommunity.Controls.viewFriendUser" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<div class="moduloimgusr">
  <telerik:RadListView ID="rdFriendUser" runat="server" ItemPlaceholderID="idPHldFriend"
    OnNeedDataSource="rdFriendUser_NeedDataSource" OnItemDataBound="rdFriendUser_ItemDataBound" OnItemCommand="rdFriendUser_ItemCommand"
    DataKeyNames="cod_usuario_rel">
    <LayoutTemplate>
      <div class="RadListView RadListViewFloated RLVCarList">
        <div class="rlvFloated rlvNoScroll">
          <asp:PlaceHolder ID="idPHldFriend" runat="server"></asp:PlaceHolder>
        </div>
      </div>
    </LayoutTemplate>
    <ItemTemplate>
      <div class="rlvI">
        <asp:LinkButton ID="idLnkFriendUser" runat="server" CommandName="GOPROFILE">
          <telerik:RadBinaryImage id="idImgFriendUser" runat="server" />
          <asp:Label ID="idLblNomUser" runat="server" Text="Datos del Usuario"></asp:Label>
        </asp:LinkButton>
        <asp:Button ID="idBtnDelFriend" runat="server" Text="Eliminar Usuario" CommandName="GODELFRIEND" />
      </div>
    </ItemTemplate>
  </telerik:RadListView>
</div>
