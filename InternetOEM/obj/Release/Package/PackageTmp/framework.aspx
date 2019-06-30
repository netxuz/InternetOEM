<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="framework.aspx.cs" Inherits="ICommunity.framework" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta charset="utf-8">
  <meta http-equiv="X-UA-Compatible" content="IE=edge">
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <title></title>
  <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
  <link rel="stylesheet" href="css/styleadmin.css">
  <link rel="stylesheet" type="text/css" href="/css/stylesdebtcontrol.css" media="screen" />
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
  <style type="text/css">
    html, body, form {
      height: 100%;
      margin: 0px;
      padding: 0px;
    }

    #idUpdatePanel {
      height: 100%;
    }
  </style>
</head>
<body id="open" scroll="no">
  <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="idUpdatePanel" runat="server">
      <ContentTemplate>
        <asp:HiddenField ID="CodNodo" runat="server" />
        <div class="FrameWork" style="height: 100%;">
          <telerik:RadSplitter ID="Radsplitter5" runat="server" BorderSize="0" Width="100%" Height="100%"
            Orientation="Horizontal">
            <telerik:RadPane ID="rdpTop" runat="server" CssClass="blq-superior" Height="80px">
              <div class="blq-menusuperior">
                <telerik:RadMenu ID="rdMenu" runat="server" BorderWidth="0" OnItemClick="rdMenu_ItemClick">
                  <Items>
                    <telerik:RadMenuItem BorderWidth="0" CssClass="mn-superior" Text="TEMPLATES" />
                    <telerik:RadMenuItem BorderWidth="0" CssClass="mn-superior" Text="CONFIGURACION" />
                    <telerik:RadMenuItem BorderWidth="0" CssClass="mn-superior" Text="APLICACIONES" />
                  </Items>
                </telerik:RadMenu>
              </div>
              <div class="blq-menuinferior">
                <telerik:RadMenu ID="rdSubMenu" runat="server" Flow="Horizontal" Visible="false"
                  OnItemClick="rdSubMenu_ItemClick">
                </telerik:RadMenu>
                <div class="btn_logoff">
                  <asp:LinkButton ID="lnkBtnLogout" runat="server" CssClass="btn btn-default btn-sm" OnClick="lnkBtnLogout_Click"><span class="glyphicon glyphicon-log-out">&nbsp;Logout</span></asp:LinkButton>
                </div>
              </div>
            </telerik:RadPane>
            <telerik:RadPane ID="rpnBarraInferior" runat="server" Scrolling="None">
              <telerik:RadSplitter ID="Radsplitter6" runat="server" CollapseMode="None">
                <telerik:RadPane ID="RadPane1" runat="server" CssClass="blq-inferior-izquierdo" Width="300px" Scrolling="1">
                  <telerik:RadTreeView runat="Server" ID="rdTree" EnableDragAndDrop="true" EnableDragAndDropBetweenNodes="true"
                    Skin="Vista" OnContextMenuItemClick="rdTree_ContextMenuItemClick" OnNodeClick="rdTree_NodeClick">
                    <ContextMenus>
                      <telerik:RadTreeViewContextMenu runat="server" ID="Menu" ClickToOpen="True" Skin="Vista">
                        <Items>
                          <telerik:RadMenuItem Text="Crear" Value="CREAR">
                          </telerik:RadMenuItem>
                          <telerik:RadMenuItem Text="Editar" Value="EDITAR">
                          </telerik:RadMenuItem>
                          <telerik:RadMenuItem Text="Eliminar" Value="ELIMINAR">
                          </telerik:RadMenuItem>
                          <telerik:RadMenuItem Text="Ordena Nodos" Value="ORDENAR">
                          </telerik:RadMenuItem>
                          <telerik:RadMenuItem Text="Contenidos" Value="CONTENIDOS">
                          </telerik:RadMenuItem>
                          <telerik:RadMenuItem Text="Crear Contenido" Value="CREARCONTENIDO">
                          </telerik:RadMenuItem>
                        </Items>
                      </telerik:RadTreeViewContextMenu>
                    </ContextMenus>
                    <Nodes>
                    </Nodes>
                  </telerik:RadTreeView>
                </telerik:RadPane>
                <telerik:RadSplitBar ID="Radsplitbar6" runat="server" CollapseMode="None" />
                <telerik:RadPane ID="rpnNavigate" CssClass="blq-inferior-derecho" runat="server">
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
