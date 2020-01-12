<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="usuario_rep_debtcontrol.aspx.cs" Inherits="ICommunity.usuario_rep_debtcontrol" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta charset="utf-8">
  <meta http-equiv="X-UA-Compatible" content="IE=edge">
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <title></title>
  <link rel="stylesheet" href="css/styleadmin.css">
  <link rel="stylesheet" type="text/css" href="css/stylesdebtcontrol.css" media="screen" />
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.0.0/jquery.min.js"></script>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
  <link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
  <style>
    .RadGrid_Sitefinity .rgRow a, .RadGrid_Sitefinity .rgAltRow a, .RadGrid_Sitefinity .rgEditRow a, .RadGrid_Sitefinity .rgFooter a, .RadGrid_Sitefinity .rgEditForm a {
      background: none !important;
      color: #777 !important;
      padding: 0px 0px 0px 0px !important;
    }

    .filtro {
      background: none !important;
      padding: 0px 0px 0px 0px !important;
      color: #777 !important;
      font: inherit !important;
    }

    .popover-title {
      text-align: center;
    }

    .custom-popover li {
      border: none !important;
      text-align: center;
    }

      .custom-popover li:nth-child(2) {
        border-top: 1px solid #ccc !important;
      }

      .custom-popover li:last-child {
        border-top: 1px solid #ccc !important;
      }
  </style>

</head>
<body class="bodyadm">
  <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:HiddenField ID="CodUsuario" runat="server" />
    <div class="container">
      <div class="row">&nbsp;</div>
      <div class="row">
        <asp:Button ID="btnVolver" runat="server" class="btn btn-default" Text="Volver" OnClick="btnVolver_Click" />
        <button type="button" id="btnSeleccionar" class="btn btn-primary" data-toggle="modal" data-target="#myModal">Agregar Reporte a Usuario</button>
      </div>
      <div class="row">&nbsp;</div>
      <div class="row">
        <h4>
          <asp:Label ID="lblUsuario" runat="server"></asp:Label></h4>
      </div>
      <div class="row">&nbsp;</div>
      <div class="row">
        <div class="form-group">
          <div class="col-xs-4">
            <asp:TextBox ID="txtBuscar" placeholder="Buscar" CssClass="form-control" runat="server"></asp:TextBox>
          </div>
          <div class="col-xs-4">
            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="btnBuscar_Click" />
          </div>
        </div>
      </div>
      <div class="row">&nbsp;</div>
      <div class="row">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" OnLoad="UpdatePanel1_Load">
          <ContentTemplate>
            <asp:Label ID="lblStatus" runat="server" Visible="false"></asp:Label>
            <telerik:RadGrid ID="rdReportesUsuario" runat="server" ShowStatusBar="True" AutoGenerateColumns="false"
              OnNeedDataSource="rdReportesUsuario_NeedDataSource" OnItemCommand="rdReportesUsuario_ItemCommand" OnItemDataBound="rdReportesUsuario_ItemDataBound"
              AllowSorting="True" PageSize="10" AllowPaging="True" GridLines="None" Skin="Sitefinity">
              <MasterTableView DataKeyNames="cod_consulta" AutoGenerateColumns="false" ShowHeader="true"
                TableLayout="Fixed" ShowHeadersWhenNoRecords="true" CommandItemDisplay="Top">
                <CommandItemSettings ShowExportToExcelButton="false" ShowRefreshButton="false" ShowAddNewRecordButton="false" />
                <Columns>

                  <telerik:GridButtonColumn ButtonType="PushButton" UniqueName="Eliminar" CommandName="cmdDelete">
                    <HeaderStyle Font-Size="Smaller" Width="50px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                  </telerik:GridButtonColumn>

                  <telerik:GridBoundColumn UniqueName="NomConsulta" DataField="nom_consulta" AllowSorting="true">
                    <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" />
                  </telerik:GridBoundColumn>

                  <telerik:GridTemplateColumn>
                    <HeaderTemplate>
                      <table>
                        <tr>
                          <td>filtros</td>
                        </tr>
                      </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                      <div class="form-group">
                        <a id="showPopover_<%# DataBinder.Eval(Container.DataItem, "cod_consulta") %>" data="<%# DataBinder.Eval(Container.DataItem, "cod_consulta") %>" class="filtro" data-toggle="popover" data-placement="left" title="Filstros asociados"><span class="glyphicon glyphicon-list-alt"></span> Filstros asociados</a>
                      </div>
                    </ItemTemplate>
                  </telerik:GridTemplateColumn>
                </Columns>
              </MasterTableView>

            </telerik:RadGrid>
          </ContentTemplate>
        </asp:UpdatePanel>
      </div>
      <div class="row">
        <div class="modal fade" id="myModal" role="dialog">
          <div class="modal-dialog modal-lg">

            <!-- Modal content-->
            <div class="modal-content">
              <div class="modal-header">
                <%--<button type="button" class="close" data-dismiss="modal">&times;</button>--%>
                <asp:Button ID="btnClose2" Text="&times;" runat="server" CssClass="close" OnClick="btnClose2_Click" />
                <h4 class="modal-title">Selecciona Consultas</h4>
              </div>
              <div class="modal-body">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                  <ContentTemplate>
                    <div class="row">
                      <div class="col-xs-4">
                        <asp:TextBox ID="txtBuscarReportes" CssClass="form-control" runat="server"></asp:TextBox>
                      </div>
                      <div class="col-xs-4">
                        <asp:Button ID="BtnBuscarReportesNotIn" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="BtnBuscarReportesNotIn_Click" />
                      </div>
                    </div>
                    <div class="row">&nbsp;</div>
                    <div class="row">
                      <telerik:RadGrid ID="rdReportesNotIn" runat="server" ShowStatusBar="True" AutoGenerateColumns="false"
                        OnNeedDataSource="rdReportesNotIn_NeedDataSource" OnItemCommand="rdReportesNotIn_ItemCommand" OnItemDataBound="rdReportesNotIn_ItemDataBound"
                        AllowSorting="True" PageSize="7" AllowPaging="True" GridLines="None" Skin="Sitefinity">
                        <MasterTableView DataKeyNames="cod_consulta" AutoGenerateColumns="false" ShowHeader="true"
                          TableLayout="Fixed" ShowHeadersWhenNoRecords="true" CommandItemDisplay="Top">
                          <CommandItemSettings ShowExportToExcelButton="false" ShowRefreshButton="false" ShowAddNewRecordButton="false" />
                          <Columns>

                            <telerik:GridButtonColumn ButtonType="PushButton" UniqueName="Agregar" CommandName="cmdAgregar">
                              <HeaderStyle Font-Size="Smaller" Width="50px" HorizontalAlign="Center" />
                              <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridButtonColumn>

                            <telerik:GridBoundColumn UniqueName="NomConsulta" DataField="nom_consulta" AllowSorting="true">
                              <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                              <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                          </Columns>
                        </MasterTableView>
                      </telerik:RadGrid>
                    </div>
                  </ContentTemplate>
                </asp:UpdatePanel>
              </div>
              <div class="modal-footer">
                <asp:Button ID="btnClose" Text="Close" runat="server" CssClass="btn btn-default" OnClick="btnClose_Click" />
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div id="popover-content" class="hide">
      <div class="container">
        <div class="row">
          <div class="custom-control custom-checkbox">
            <input type="checkbox" class="custom-control-input" id="chk_deudor">
            <label class="custom-control-label" for="chk_deudor">Deudor</label>
          </div>
        </div>
        <div class="row">
          <div class="custom-control custom-checkbox">
            <input type="checkbox" class="custom-control-input" id="chk_holding">
            <label class="custom-control-label" for="chk_holding">Holding</label>
          </div>
        </div>
        <div class="row">
          <button type="button" onclick="javascript:onUpdate()" class="btn btn-success">Aceptar</button>
          <button type="button" onclick="javascript:onClose()" class="btn btn-primary">Cerrar</button>
        </div>
      </div>
      
      <%--<input class="headerSearch search-query" id="str1" name="str1" type="text" value="" /></li>--%>
    </div>

  </form>
  <script>
    var isVisible = false;
    var objPopover;

    $('.filtro').popover({
      html: true,
      trigger: 'manual',
      content: function () {
        return $('#popover-content').html();
      }
    }).click(function (e) {
      if (!isVisible) {
        objPopover = $(this);
        $(this).popover('show');
        isVisible = true;
        e.preventDefault();

        var cUrl = "usuario_rep_debtcontrol.aspx/getFiltros";
        var datos = "{CodUsuario:" + $("#CodUsuario").val() + ",CodConsulta:" + $(this).attr("data") + "}";
        $.ajax({
          type: "POST",
          url: cUrl,
          data: datos,
          contentType: "application/json; charset=utf-8",
          dataType: "json",

          success: function (data) {
            $.each(data.d, function (key, value) {
              if (value.Deudor == "V")
                $("#chk_deudor").prop("checked", true);

              if (value.Holding == "V")
                $("#chk_holding").prop("checked", true);

            });
          },

          error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(textStatus + ": " + XMLHttpRequest.responseText);
          }
        });
      }
    });

    function onUpdate() {

      if (isVisible) {
        sDeudor = "F";
        if ($("#chk_deudor").is(':checked'))
          sDeudor = "V";

        sHolding = "F";
        if ($("#chk_holding").is(':checked')) {
          sHolding = "V";
        }

        var cUrl = "usuario_rep_debtcontrol.aspx/setFiltros";
        var datos = "{CodUsuario:" + $("#CodUsuario").val() + ",CodConsulta:" + objPopover.attr("data") + ",sDeudor:'" + sDeudor + "',sHolding:'" + sHolding + "'}";
        $.ajax({
          type: "POST",
          url: cUrl,
          data: datos,
          contentType: "application/json; charset=utf-8",
          dataType: "json",

          success: function (data) {
            objPopover.popover('hide')
            isVisible = false
          },

          error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(textStatus + ": " + XMLHttpRequest.responseText);
          }
        });

      }       
    }

    function onClose() {
      objPopover.popover('hide');
      isVisible = false;
    }

  </script>

</body>
</html>
