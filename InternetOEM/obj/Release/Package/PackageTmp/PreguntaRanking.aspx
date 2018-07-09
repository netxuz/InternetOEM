<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PreguntaRanking.aspx.cs"
  Inherits="ICommunity.PreguntaRanking" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Pregunta Ranking</title>
</head>
<body>
  <form id="form1" runat="server">
  <asp:ScriptManager ID="ScriptManager" runat="server">
  </asp:ScriptManager>
  <div class="entorno">
    <div class="panel">
      <telerik:RadGrid ID="rdPreguntaRanking" Skin="Windows7" runat="server" PageSize="100"
        GridLines="None" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
        ShowStatusBar="true" OnNeedDataSource="rdPreguntaRanking_NeedDataSource" OnInsertCommand="rdPreguntaRanking_InsertCommand" OnUpdateCommand="rdPreguntaRanking_UpdateCommand"
        OnDeleteCommand="rdPreguntaRanking_DeleteCommand">
        <MasterTableView Width="100%" CommandItemDisplay="Top" DataKeyNames="cod_preg_ranking">
          <Columns>
            <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn">
            </telerik:GridEditCommandColumn>
            <telerik:GridBoundColumn DataField="preg_ranking" HeaderText="Pregunta" UniqueName="PregRanking">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="est_preg_ranking" HeaderText="Estado" UniqueName="EstPregRanking">
            </telerik:GridBoundColumn>
            <telerik:GridButtonColumn ConfirmText="Desea eliminar la pregunta?" ConfirmDialogType="RadWindow"
              ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete" Text="Delete"
              UniqueName="DeleteColumn">
            </telerik:GridButtonColumn>
          </Columns>
        </MasterTableView>
      </telerik:RadGrid>
    </div>
  </div>
  </form>
</body>
</html>
