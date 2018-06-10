<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PageObject.ascx.cs"
  Inherits="ICommunity.Controls.PageObject" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<telerik:RadTabStrip id="rdTabStrip" runat="server" Skin="Vista" MultiPageID="rdMultiPage" SelectedIndex="0">
<tabs>
  <telerik:RadTab Text="Objetos" Visible="true">    
  </telerik:RadTab>
  <telerik:RadTab Text="Zonas" Visible="false">
  </telerik:RadTab>
  <telerik:RadTab Text="Apps" Visible="false">
  </telerik:RadTab>
  <telerik:RadTab Text="Nodos" Visible="false">
  </telerik:RadTab>
</tabs>
</telerik:RadTabStrip>
<telerik:RadMultiPage ID="rdMultiPage" runat="server" SelectedIndex="0">
  <telerik:RadPageView id="rdPageView1" runat="server">
    <telerik:RadTreeView ID="rdTreeObject" runat="server" Height="500px"
      Width="300px" EnableDragAndDrop="true"
      OnClientNodeDragStart="OnClientNodeDragStart" OnClientNodeDragging="OnClientNodeDragging"
      OnClientNodeDropping="OnClientNodeDropping" Skin="Sitefinity">
    </telerik:RadTreeView>
  </telerik:RadPageView>
  <telerik:RadPageView id="RadPageView1" runat="server">
    <telerik:RadTreeView ID="rdTreeZona" runat="server" Height="500px"
      Width="300px" EnableDragAndDrop="true" Skin="Sitefinity"
      OnClientNodeDragStart="OnClientNodeDragStart" OnClientNodeDragging="OnClientNodeDragging"
      OnClientNodeDropping="OnClientNodeDropping">
    </telerik:RadTreeView>
  </telerik:RadPageView>
  <telerik:RadPageView id="RadPageView2" runat="server">
    <telerik:RadTreeView ID="rdTreeApps" runat="server" Height="500px"
      Width="300px" EnableDragAndDrop="true" Skin="Sitefinity"
      OnClientNodeDragStart="OnClientNodeDragStart" OnClientNodeDragging="OnClientNodeDragging"
      OnClientNodeDropping="OnClientNodeDropping">
    </telerik:RadTreeView>
  </telerik:RadPageView>
  <telerik:RadPageView id="RadPageView3" runat="server">
    <telerik:RadTreeView ID="RadTreeNodos" runat="server" Height="500px"
      Width="300px" EnableDragAndDrop="true" Skin="Sitefinity"
      OnClientNodeDragStart="OnClientNodeDragStart" OnClientNodeDragging="OnClientNodeDragging"
      OnClientNodeDropping="OnClientNodeDropping">
    </telerik:RadTreeView>
  </telerik:RadPageView>
</telerik:RadMultiPage>

<script language="javascript" type="text/javascript">
function OnClientLoad(editor)
{ 
var tree;

  tree = $find("<%= rdTreeObject.ClientID %>");
  makeUnselectable(tree.get_element());
  tree = $find("<%= rdTreeZona.ClientID %>");
  if (tree != null) {
    makeUnselectable(tree.get_element());
  }
}
function OnClientNodeDragStart(sender, args)
{
  setOverlayVisible(true);
}

function OnClientNodeDropping(sender, args)
{
  var editor = GetIdEditor();
  var event = args.get_domEvent();
                        
  document.body.style.cursor = "default";
    
  var cValue = args.get_sourceNode().get_value();
  var cText = args.get_sourceNode().get_text();
    
  var cHtml = '<FIELDSET class="TypeElement" accept="';
  cHtml += 'Type:' + cValue; 
  cHtml += '">' + cText + '</FIELDSET>';
  
  editor.setFocus();
  editor.pasteHtml(cHtml);
    
  setOverlayVisible(false);            
}

function OnClientNodeDragging(sender, args)
{                        

  var editor = GetIdEditor();
  var event = args.get_domEvent();
    
  document.body.style.cursor = "hand";
}

/* ================== Utility methods needed for the Drag/Drop ===============================*/

//Make all treeview nodes unselectable to prevent selection in editor being lost
function makeUnselectable(element)
{
    var nodes = element.getElementsByTagName("*");
    for (var index = 0; index < nodes.length; index++)
    {
        var elem = nodes[index]; 
        elem.setAttribute("unselectable","on"); 
    } 
}

//Create and display an overlay to prevent the editor content area from capturing mouse events
var shimId = null;            
function setOverlayVisible(toShow)
{
    if (!shimId)
    {
        var div = document.createElement("DIV");
        document.body.appendChild(div);
        shimId = new Telerik.Web.UI.ModalExtender(div);
    }
    
    if (toShow) shimId.show();
    else shimId.hide(); 
}

//Check if the image is over the editor or not
function isMouseOverEditor(editor, events)
{         
    var editorFrame = editor.get_contentAreaElement();
    var editorRect = $telerik.getBounds(editorFrame);    
                                
    var mouseX = events.clientX;
    var mouseY = events.clientY; 

    if (mouseX < (editorRect.x + editorRect.width) &&
     mouseX > editorRect.x &&
        mouseY < (editorRect.y + editorRect.height) &&
     mouseY > editorRect.y)
    {
        return true;
    }            
    return false;
}
               
/* ================== These two methods are not related to the drag/drop functionality, but to the preview functionality =======*/        

function Scale(img, width, height)
{
    var hRatio = img.height/height;
    var wRatio = img.width/width;

    if (img.width > width && img.height > height)
    {
        var ratio = (hRatio>=wRatio ? hRatio:wRatio);
        img.width = (img.width /ratio);
        img.height = (img.height /ratio);
    }
    else
    {
        if (img.width > width)
        {
            img.width = (img.width /wRatio);
            img.height = (img.height /wRatio);
        }
        else
        {
            if (img.height > height)
            {
                img.width = (img.width /hRatio);
                img.height = (img.height /hRatio);
            }
        }
    }
}

function BeforeClick(sender, args)
{         
    
    var node = args.get_node();
    var object = document.createElement("IMG");
    object.src = node.get_value();
    if (node.get_attributes().getAttribute("Category") == "Folder")
    {
        return;
    }
    
    var previewPane = document.getElementById("previewPane");
    
    if (object.complete)
    {
        Scale(object, 100, 100);
        previewPane.innerHTML = "";
        previewPane.appendChild(object);
    }
    else
    {
        previewPane.innerHTML = "Loading image...";
        object.onload = function()
        {
            Scale(object, 100, 100);
            previewPane.innerHTML = "";
            previewPane.appendChild(object);
            object.onload = null;
        }
    }    
}
</script>

