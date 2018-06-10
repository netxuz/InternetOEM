<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="framework.aspx.cs" Inherits="ICommunity.framework" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta http-equiv="X-UA-Compatible" content="IE=8" />
  <title></title>
  <style type="text/css">
    html, body, form
    {
      height: 100%;
      margin: 0px;
      padding: 0px;
      
    }
    #idUpdatePanel
    {
    	height:100%;
    }
  </style>
</head>
<body id="open" scroll="no">
  <form id="form1" runat="server">
  <asp:ScriptManager ID="ScriptManager1" runat="server">
  </asp:ScriptManager>
  <asp:UpdatePanel Id="idUpdatePanel" runat="server">
  <ContentTemplate>
  <asp:HiddenField ID="CodNodo" runat="server" />
  <div class="FrameWork" style="height:100%;">
    <telerik:RadSplitter ID="Radsplitter5" runat="server" Width="100%" Height="100%"
      Orientation="Horizontal">
      <telerik:RadPane ID="rdpTop" runat="server" Height="80">
        <telerik:RadMenu ID="rdMenu" runat="server" OnItemClick="rdMenu_ItemClick">
          <Items>
            <telerik:RadMenuItem Text="TEMPLATES" />
            <telerik:RadMenuItem Text="CONFIGURACION" />
            <telerik:RadMenuItem Text="APLICACIONES" />
          </Items>
        </telerik:RadMenu>
        <telerik:RadMenu ID="rdSubMenu" runat="server" Flow="Horizontal" Visible="false"
          OnItemClick="rdSubMenu_ItemClick">
        </telerik:RadMenu>
      </telerik:RadPane>
      <telerik:RadPane ID="rpnBarraInferior" runat="server" Scrolling="None">
        <telerik:RadSplitter ID="Radsplitter6" runat="server" CollapseMode="None">
          <telerik:RadPane ID="RadPane1" runat="server" Width="220" Scrolling="None">
            <telerik:RadTreeView runat="Server" ID="rdTree" EnableDragAndDrop="true" EnableDragAndDropBetweenNodes="true"
              Skin="Vista" OnContextMenuItemClick="rdTree_ContextMenuItemClick" OnNodeClick="rdTree_NodeClick">
              <ContextMenus>
                <telerik:RadTreeViewContextMenu runat="server" ID="Menu" ClickToOpen="True" Skin="Vista">
                  <Items>
                    <telerik:RadMenuItem Text="Add" Value="Add">
                    </telerik:RadMenuItem>
                    <telerik:RadMenuItem Text="Edit" Value="Edit">
                    </telerik:RadMenuItem>
                    <telerik:RadMenuItem Text="Delete" Value="Delete">
                    </telerik:RadMenuItem>
                    <telerik:RadMenuItem Text="Ordena Nodos" Value="Ordena">
                    </telerik:RadMenuItem>
                    <telerik:RadMenuItem Text="Contenidos" Value="Contenidos">
                    </telerik:RadMenuItem>
                    <telerik:RadMenuItem Text="Crear Contenido" Value="CrearContenido">
                    </telerik:RadMenuItem>
                  </Items>
                </telerik:RadTreeViewContextMenu>
              </ContextMenus>
              <Nodes>
               
              </Nodes>
            </telerik:RadTreeView>
          </telerik:RadPane>
          <telerik:RadSplitBar ID="Radsplitbar6" runat="server" CollapseMode="Forward" />
          <telerik:RadPane ID="rpnNavigate" CssClass="aa" runat="server">
          </telerik:RadPane>
        </telerik:RadSplitter>
      </telerik:RadPane>
    </telerik:RadSplitter>
  </div>
  </ContentTemplate>
  </asp:UpdatePanel>
  <script language="javascript" type="text/javascript">
      function ChangeName() {
        var tree = $find("<%= rdTree.ClientID %>");
        tree.trackChanges();
        var node = tree.findNodeByValue(ChangeName.arguments[0]);

        node.set_text(ChangeName.arguments[1]);
        tree.commitChanges();
      }
      function AddNode() {
        var tree = $find("<%= rdTree.ClientID %>");
        tree.trackChanges();
        var node = new Telerik.Web.UI.RadTreeNode();
        var nodeparent = tree.findNodeByValue(AddNode.arguments[0]);

        node.set_value(AddNode.arguments[1]);
        node.set_text(AddNode.arguments[2]);  
        node.set_contextMenuID("Menu");
        nodeparent.get_nodes().add(node);
        nodeparent.expand();
        tree.commitChanges();
      }
  </script>

  </form>
</body>
</html>
