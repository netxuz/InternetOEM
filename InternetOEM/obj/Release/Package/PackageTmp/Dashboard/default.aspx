<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="ICommunity.Dashboard._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta charset="utf-8" />
  <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
  <meta http-equiv="x-ua-compatible" content="ie=edge" />
  <meta http-equiv="Pragma" content="no-cache" />
  <meta http-equiv="Expires" content="-1" />
  <meta http-equiv="CACHE-CONTROL" content="NO-CACHE" />
  <!-- Font Awesome -->
  <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.8.2/css/all.css" />
  <!-- Bootstrap core CSS -->
  <link href="css/bootstrap.min.css" rel="stylesheet" />
  <!-- Material Design Bootstrap -->
  <link href="css/mdb.min.css" rel="stylesheet" />
  <!-- Your custom styles (optional) -->
  <link href="css/style.css" rel="stylesheet" />

  <title>Dashboard</title>
  <!-- SCRIPTS -->
  <link href='https://fonts.googleapis.com/css?family=Lato:100,400,700,900' rel='stylesheet' type='text/css' />
  <!-- JQuery -->
  <script type="text/javascript" src="js/jquery-3.4.1.min.js"></script>
  <!-- Bootstrap tooltips -->
  <script type="text/javascript" src="js/popper.min.js"></script>
  <!-- Bootstrap core JavaScript -->
  <script type="text/javascript" src="js/bootstrap.min.js"></script>
  <!-- MDB core JavaScript -->
  <script type="text/javascript" src="js/mdb.min.js"></script>

  <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
  <style>
    /*--------------------------------------------------------------------------*/
    /*--------------------------------------------------------------------------*/
    /*--------------------------------------------------------------------------*/
    .noinfo {
      font-size: 1rem !important;
      font-weight: 700 !important;
      color: #79ACD3 !important;
    }

    #loader {
      position: absolute;
      left: 50%;
      top: 50%;
      z-index: 1;
      width: 200px;
      height: 200px;
      margin: -75px 0 0 -75px;
      border: 16px solid #f3f3f3;
      border-radius: 50%;
      border-top: 16px solid #6498FF;
      border-bottom: 16px solid #6498FF;
      -webkit-animation: spin 2s linear infinite;
      animation: spin 2s linear infinite;
    }

    @-webkit-keyframes spin {
      0% {
        -webkit-transform: rotate(0deg);
      }

      100% {
        -webkit-transform: rotate(360deg);
      }
    }

    @keyframes spin {
      0% {
        transform: rotate(0deg);
      }

      100% {
        transform: rotate(360deg);
      }
    }

    /*--------------------------------------------------------------------------*/
    /*--------------------------------------------------------------------------*/
    /*--------------------------------------------------------------------------*/
    .Valiniamiento {
      display: flex;
      justify-content: center;
      align-content: center;
      flex-direction: column;
    }

    .tt-dashborad {
      font-size: 1.4rem !important;
      font-weight: 300 !important;
    }

    .blue-grey-text {
      color: #fff !important;
      font-weight: bold !important;
    }

    .btn-trasparent {
      font-size: 14px !important;
      padding: 20px 10px 20px 10px !important;
      background-color: #ffffff;
      color: #808080;
      border: 0px;
    }

      .btn-trasparent:hover {
        font-size: 14px !important;
        padding: 20px 10px 20px 10px !important;
        background-color: #ffffff;
        color: #696969;
        border: 0px;
      }

    .modal-dialog.modal-warning.modal-info .badge, .modal-dialog.modal-warning.modal-info .modal-header {
      background-color: #ffc107 !important;
    }

    .row-alert-align {
      display: flex;
      align-items: center;
    }

    .percentaje-overview {
      font-size: 3rem !important;
      font-weight: 300 !important;
      color: #699ED5 !important;
    }

    .lb-col-comp_finan {
      font-size: 1rem !important;
      font-weight: 400 !important;
      color: #45526E !important;
    }

    .lb-col-overview {
      font-size: .7em !important;
      font-weight: 400 !important;
      color: #324A69 !important;
    }

    .row-line-height {
      line-height: 1 !important;
    }

    .hr-overview {
      margin-top: .5rem !important;
      margin-bottom: .5rem !important;
    }

    .form-control-lg {
      font-size: 1.1em !important;
    }

    .text-alert {
      font-size: .9rem !important;
      font-weight: 400;
      color: #A6A6A6;
    }

    .dropdown-weight {
      font-weight: 300 !important;
    }

    .widget {
      position: absolute !important;
      font-size: .85rem !important;
      right: 0 !important;
      color: #00C851 !important;
    }

    .lblltigiostitle {
      font-size: .8em !important;
      color: #12CBCA;
      font-weight: bold !important;
    }

    .lbllitigiosdata {
      font-size: 1em !important;
      font-weight: 300 !important;
      padding-bottom: .3rem !important;
    }

    .lbldata {
      font-size: 1.8em !important;
      font-weight: 300 !important;
      color: #45526E !important;
    }

    .lbl_est_real_data {
      font-size: 1.3em !important;
      font-weight: 300;
      color: #45526E !important;
    }

    .tt_est_real_dat {
      font-size: .7rem !important;
      font-weight: 500;
      color: #699ED5;
    }

    .colestreal {
      margin-bottom: 15px;
    }

    .rowOverDue {
      background-color: #98E454;
      padding-left: 20px !important;
      padding-right: 20px !important;
      border-radius: .25rem;
      margin-bottom: 2px;
    }

    .col_Over_Due_top {
      margin-top: 10px;
    }

    .col_Over_Due {
      margin-bottom: 10px;
    }

    .rowCriticoOverDue {
      background-color: #FF3547;
      padding-left: 20px !important;
      padding-right: 20px !important;
      border-radius: .25rem;
    }

    .percentaje-ind_credi {
      font-size: 1.5em !important;
      color: #699ED5 !important;
    }

    .estadovsreal {
      line-height: 1;
    }

    .vcenter {
      display: inline-block;
      vertical-align: middle;
      float: none;
    }

    .modal-dialog.modal-notify.modal-info .badge, .modal-dialog.modal-notify.modal-info .modal-header {
      background-color: #45526E !important;
    }

    .md-form {
      margin-top: 0px !important;
      margin-bottom: 0px !important;
      margin-left: 1.5rem !important;
    }

    /*.navbar {
      height: 80px !important;
    }*/

    /*.col, .col-1, .col-10, .col-11, .col-12, .col-2, .col-3, .col-4, .col-5, .col-6, .col-7, .col-8, .col-9, .col-auto, .col-lg, .col-lg-1, .col-lg-10, .col-lg-11, .col-lg-12, .col-lg-2, .col-lg-3, .col-lg-4, .col-lg-5, .col-lg-6, .col-lg-7, .col-lg-8, .col-lg-9, .col-lg-auto, .col-md, .col-md-1, .col-md-10, .col-md-11, .col-md-12, .col-md-2, .col-md-3, .col-md-4, .col-md-5, .col-md-6, .col-md-7, .col-md-8, .col-md-9, .col-md-auto, .col-sm, .col-sm-1, .col-sm-10, .col-sm-11, .col-sm-12, .col-sm-2, .col-sm-3, .col-sm-4, .col-sm-5, .col-sm-6, .col-sm-7, .col-sm-8, .col-sm-9, .col-sm-auto, .col-xl, .col-xl-1, .col-xl-10, .col-xl-11, .col-xl-12, .col-xl-2, .col-xl-3, .col-xl-4, .col-xl-5, .col-xl-6, .col-xl-7, .col-xl-8, .col-xl-9, .col-xl-auto {
      padding-right: 5px !important;
      padding-left: 5px !important;
    }*/

    .tt-alerta {
      font-weight: 300;
    }

    .col_status_overview {
      padding-top: 15px;
    }

    .col_tt_status_overview {
      margin-top: 5px;
    }

    /*--------------------------------------------------------------------------------------*/
    .body-dashboard {
      font-family: 'Lato', sans-serif !important;
      background: #F6F7FF !important;
      /*line-height: 1rem !important;*/
    }

    .debtcontrol {
      /*background: #fff !important;*/
      /*background-image: linear-gradient(135deg, #fff 70%, #3D5375);*/
    }

    .nav-background {
      background: #3D5375 !important;
    }

    .nav-seleccion {
      /*box-shadow: 0 1px 1px 0 rgba(13,5,1,.16), 0 2px 10px 0 rgba(13,5,1,.12) !important;*/
      box-shadow: 0 1px 1px 0 #DEE9F6, 0 2px 10px 0 #DEE9F6 !important;
      border-radius: 1rem !important;
      padding-left: 1rem !important;
      margin-left: 10px !important;
      margin-right: 10px !important;
      margin-bottom: 6px!important;
      width: 350px;
      font-size:.7rem;
      background-color:#fff;
    }

    .text-combo-seleccion {
      color: #6784AA !important;
      font-weight: 600 !important;
    }

    .nav-search {
      /*box-shadow: 0 1px 1px 0 rgba(13,5,1,.16), 0 2px 10px 0 rgba(13,5,1,.12) !important;*/
      box-shadow: 0 1px 1px 0 #DEE9F6, 0 2px 10px 0 #DEE9F6 !important;
      border-radius: 1rem !important;
      margin-left: 10px !important;
      margin-right: 10px !important;
      margin-bottom:6px!important;
      padding: .25rem 1rem;
      width: 350px;
      background-color:#fff;
    }

    .txt-search {
      color: #6784AA !important;
      font-weight: 700 !important;
      font-size: .8rem !important;
      border: none;
      width: 300px;
    }

    #txtSearch::placeholder { /* Chrome, Firefox, Opera, Safari 10.1+ */
      color: #6784AA !important;
      font-weight: 600 !important;
      border: none;
      width: 300px;
    }

    .navbar .dropdown-menu a {
      color: #6784AA !important;
      padding: 10px !important;
      line-height: .8 !important;
      font-size: .75rem !important;
      font-weight: 700 !important;
    }

    .ui-widget.ui-widget-content {
      border: none !important;
    }

    .ui-menu-item-wrapper {
      color: #6784AA !important;
      padding: 10px !important;
      line-height: .8 !important;
      font-size: .8rem !important;
      font-weight: 700 !important;
      background-color: #fff !important;
      border: none !important;
      box-shadow: 0 1px 1px 0 #DEE9F6, 0 2px 10px 0 #DEE9F6 !important;
      border-radius: 20px;
    }

      .ui-menu-item-wrapper:hover {
        background-color: #E7F0FB !important;
      }

    .dropdown-item:focus, .dropdown-item:hover {
      background-color: #E7F0FB !important;
    }

    .dropdown-menu {
      background-color: #fff !important;
      border: none !important;
      box-shadow: 0 1px 1px 0 #DEE9F6, 0 2px 10px 0 #DEE9F6 !important;
      border-radius: 20px;
      color: #6784AA !important;
    }

    .barra-cliente-holding {
      padding: 20px !important;
      font-size: 1.2rem !important;
      font-weight: 400 !important;
      color: #0d47a1;
    }

    .lbl-usuario {
      font-weight: 900 !important;
    }

    hr {
      margin-top: 0rem !important;
      margin-bottom: .7rem !important;
    }

    .tt-ficha {
      color: #1a237e !important;
      font-size: 1.1rem !important;
      font-weight: 700;
    }

    .trash-ficha {
      position: absolute !important;
      font-size: 1.3rem !important;
      font-weight: lighter !important;
      right: 0 !important;
      color: #E4E4E4 !important;
    }

    .light-trash {
      font-weight: 400 !important;
    }

    .card {
      height: 100% !important;
      border-radius: 2rem !important;
    }

    .card-body {
      padding: 0rem 1em 1rem 1rem !important;
    }

    .cb-ficha {
      padding: 20px 0px 10px 30px;
    }

    #donut_single {
      width: 150px;
      height: 150px;
      margin-left: auto;
      margin-right: auto;
    }

    #labelOverlay {
      position: absolute;
      top: 50%;
      left: 50%;
      transform: translate(-50%, -50%);
    }

      #labelOverlay span {
        line-height: 0.3;
        padding: 0;
        margin: 8px;
      }

        #labelOverlay span.percentaje-overview {
          font-size: 2rem !important;
          font-weight: bold !important;
          color: #1578FF !important;
        }

        #labelOverlay span.percentaje-overview-low {
          font-size: 1.5rem !important;
          font-weight: bold !important;
          color: #1578FF !important;
        }

        #labelOverlay span.percentaje-overview-crema {
          font-size: 2rem !important;
          font-weight: bold !important;
          color: #FFDFAB !important;
        }

        #labelOverlay span.percentaje-overview-crema-low {
          font-size: 1.5rem !important;
          font-weight: bold !important;
          color: #FFDFAB !important;
        }

        #labelOverlay span.percentaje-overview-red {
          font-size: 2rem !important;
          font-weight: bold !important;
          color: #D0021B !important;
        }

        #labelOverlay span.percentaje-overview-red-low {
          font-size: 1.5rem !important;
          font-weight: bold !important;
          color: #D0021B !important;
        }

    .bkg_overview_blue {
      background-color: #E7F0FB;
      border-radius: .5rem;
      padding-top: 5px;
      padding-bottom: 5px;
    }

    .bkg_overview_pink {
      background-color: #FFF4E1;
      border-radius: .5rem;
      padding-top: 5px;
      padding-bottom: 5px;
    }

    .tt-overview {
      font-size: 1.5rem !important;
      color: #1578FF !important;
      font-weight: 600;
      line-height: 1;
    }

    .blue-dollar {
      font-weight: 600 !important;
      color: #1578FF !important;
    }
    /*.space_data {
      padding-top:7px;
    }*/
    .tt-col-blue-overview {
      font-size: .8rem !important;
      font-weight: 700 !important;
      color: #1578FF !important;
    }

    .tt-col-pink-overview {
      font-size: .8rem !important;
      font-weight: 700 !important;
      color: #EDB559 !important;
    }

    .lb-col-blue-overview {
      font-size: 1rem;
      font-weight: 400 !important;
      color: #1578FF !important;
    }

    .lb-col-pink-overview {
      font-size: 1rem;
      font-weight: 400 !important;
      color: #EDB559 !important;
    }

    .lbl_data_blu_overview {
      color: #1578FF;
      font-size: 1rem !important;
    }

    .lbl_data_pink_overview {
      color: #EDB559;
      font-size: 1rem !important;
    }

    .space-tt-detalle-pago {
      padding: 0px 0px 10px 0px;
    }

    .tt-detalle-pago {
      color: #757575;
      font-size: 1rem !important;
      font-weight: 700;
    }

    .hr-dash {
      border: 1px dashed #E4E4E4 !important;
      margin-top: 1rem !important;
      margin-bottom: .8rem !important;
    }

    .tt-dat-overview {
      font-size: .8rem !important;
      color: #79ACD3 !important;
      font-weight: 700 !important;
    }

    .lb-dat-overview {
      font-size: .8rem !important;
      color: #c5c5c5 !important;
      font-weight: 700 !important;
    }

    .lb-detalle-cuentas-x-cobrar {
      color: #B2B2B2;
      font-size: 1rem !important;
      font-weight: 700;
    }

    .separador-right {
      border-right: 1px solid rgba(0,0,0,.1);
    }

    .cabeceragestion {
      font-size: 1.1rem !important;
      font-weight: 900 !important;
      color: #79ACD3 !important;
      padding-top: 8px !important;
      padding-bottom: 6px !important;
    }

    .table-curved {
      border-collapse: collapse;
      margin-left: 10px;
    }

      .table-curved th {
        padding: 3px 10px;
      }

      .table-curved td {
        color: #B2B2B2 !important;
        position: relative;
        padding: 6px 10px;
        border-bottom: 2px solid white;
        border-top: 2px solid white;
      }

      /*.table-curved td:first-child:before {
          content: '';
          position: absolute;
          border-radius: 8px 0 0 8px;
          background-color: #c5c5c5;
          width: 12px;
          height: 100%;
          left: -12px;
          top: 0px;
        }*/
      .table-curved .tr-border-round {
        background-color: #F3F8FF;
      }

        .table-curved .tr-border-round td:first-child:before {
          content: '';
          position: absolute;
          border-radius: 8px 0 0 8px;
          background-color: #F3F8FF;
          width: 12px;
          height: 100%;
          left: -12px;
          top: 0px;
        }

      .table-curved td:last-child {
        border-radius: 0 10px 10px 0;
      }

    /*.table-curved tr:hover td {
        background-color: #c5c5c5;
      }*/

    /*.table-curved tr.blue td:first-child:before {
        background-color: cornflowerblue;
      }

      .table-curved tr.green td:first-child:before {
        background-color: forestgreen;
      }*/
    table.table td, table.table th {
      padding-top: 8px !important;
      padding-bottom: 6px !important;
    }

    .table td, .table th {
      border-top: 0 !important;
    }

    table td {
      font-size: .8rem !important;
      font-weight: 700 !important;
    }

    .tt_provison_data {
      font-size: 1rem !important;
      color: #95DB48;
      font-weight: 400 !important;
    }

    .lb_provison_data {
      font-size: 1.1rem !important;
      color: #7ED321;
      font-weight: 700 !important;
    }

    .tit_impacto_provison {
      font-size: 1rem !important;
      color: #DC5757;
      font-weight: 400 !important;
    }

    .lb_impacto_provison {
      font-size: 1.1rem !important;
      color: #D0021B;
      font-weight: 700 !important;
    }

    .row-provison-sla {
      background-color: #F7F9FD;
      border-radius: 10px;
      padding: 10px 0px 10px 0px;
    }

    .lbl-txt-provision-sla {
      font-size: 1rem;
      color: #5E9ACA;
      font-weight: 400 !important;
    }

    .lbl-monto-provision-sla {
      font-size: 1.1rem;
      color: #5E9ACA;
      font-weight: 700 !important;
    }

    .calendario-pago-back-left {
      background-color: #FFFDEB;
      border-radius: 10px 0px 0px 10px;
      line-height: 1;
      padding: 15px;
    }

    .calendario-pago-back-right {
      background-color: #FFFDEB;
      border-radius: 0px 10px 10px 0px;
      line-height: 1;
      padding: 15px;
    }

    .pink-calendario {
      color: #FBD99A !important;
    }

    .lbl-tt-calendario {
      color: #F7AF38;
    }

    .tt-lb-compromiso-pago {
      color: #B2B2B2;
      font-size: 1rem !important;
      font-weight: 700;
    }

    .box-riesgo-rojo {
      font-size: 1.1rem !important;
      font-weight: 700;
      color: #D0021B;
    }

    .box-riesgo-naranjo {
      font-size: 1.1rem !important;
      font-weight: 700;
      color: #F57F23;
    }

    .box-riesgo-amarillo {
      font-size: 1.1rem !important;
      font-weight: 700;
      color: #F5D423;
    }

    .box-riesgo-verdeclaro {
      font-size: 1.1rem !important;
      font-weight: 700;
      color: #7ED321;
    }

    .box-riesgo-verdeoscuro {
      font-size: 1.1rem !important;
      font-weight: 700;
      color: #4EA528;
    }

    .txt-riesgo {
      font-size: 1rem !important;
      font-weight: 700;
      color: #c5c5c5;
    }

    .sbackgroudcolorbar_rojo {
      background-color: #D0021B;
      width: 100px;
      padding: 10px 5px 10px 5px;
      border-radius: 20px;
    }

    .sbackgroudcolorbar_naranjo {
      background-color: #F57F23;
      width: 100px;
      padding: 10px 5px 10px 5px;
      border-radius: 20px;
    }

    .sbackgroudcolorbar_amarillo {
      background-color: #F5D423;
      width: 100px;
      padding: 10px 5px 10px 5px;
      border-radius: 20px;
    }

    .sbackgroudcolorbar_verdeclaro {
      background-color: #7ED321;
      width: 100px;
      padding: 10px 5px 10px 5px;
      border-radius: 20px;
    }

    .sbackgroudcolorbar_verdeoscuro {
      background-color: #4EA528;
      width: 100px;
      padding: 10px 5px 10px 5px;
      border-radius: 20px;
    }

    .index-riesgo {
      font-size: 3rem;
      color: #ffffff;
      font-weight: 900;
    }

    .index-creb-blanco {
      line-height: .9 !important;
    }

    .index-creb-rojo {
      background-color: #D0021B;
      line-height: .9 !important;
      border-radius: 10px 0px 0px 10px;
    }

    .index-creb-naranjo {
      background-color: #F57F23;
      line-height: .9 !important;
    }

    .index-creb-amarillo {
      background-color: #F5D423;
      line-height: .9 !important;
    }

    .index-creb-verdeclaro {
      background-color: #7ED321;
      line-height: .9 !important;
    }

    .index-creb-verdeoscuro {
      background-color: #4EA528;
      line-height: .9 !important;
      border-radius: 0px 10px 10px 0px;
    }

    #myCanvas {
      position: absolute;
      top: 0;
      bottom: 0;
      right: 0;
      left: 0;
      margin: auto;
    }

    .tt_total_pas_due {
      font-size: 1.3rem !important;
      font-weight: 900;
      color: #7ED321;
    }

    .cabecera-total-pasdue {
      background-color: #F3FBEA !important;
      border-radius: 10px 10px 0px 0px !important;
      padding: 5px 0px 5px 0px !important;
    }

    .firtline-total-pasdue {
      padding-top: 5px !important;
      border-left: 1px solid #F4F4F4;
      border-right: 1px solid #F4F4F4;
    }

    .middleline-total-pasdue {
      border-left: 1px solid #F4F4F4;
      border-right: 1px solid #F4F4F4;
      border-bottom: 1px solid #F4F4F4;
      padding-bottom: 5px !important;
    }

    .lblttOverDue {
      font-size: 1.1rem;
      font-weight: 400;
      color: #81D426 !important;
    }

    .lbldtOverDue {
      font-size: 1.3rem;
      font-weight: 900;
      color: #81D426 !important;
    }

    .cabecera-critico-pasdue {
      background-color: #FCF2F2 !important;
      border-radius: 10px 10px 0px 0px !important;
      padding: 5px 0px 5px 0px !important;
    }

    .tt_critico_pas_due {
      font-size: 1.3rem !important;
      font-weight: 900;
      color: #D22828;
    }

    .lblttCriticoOverDue {
      font-size: 1.1rem;
      font-weight: 400;
      color: #E27070 !important;
    }

    .lbldtCriticoOverDue {
      font-size: 1.3rem;
      font-weight: 900;
      color: #D22828 !important;
    }

    .padding-litigios {
      padding: 20px 10px 0px 10px;
    }

    .sback-litigios {
      padding-top: 10px !important;
      padding-bottom: 10px !important;
      background-color: #EAF1FF !important;
      border-radius: 10px;
    }

    .scircle-calendar {
      background-color: #699FF7;
      padding: 10px;
      border-radius: 100px;
    }

    .lbl-dia-proceso-normal-litigio {
      font-size: .8rem !important;
      font-weight: 900;
      color: #9FC1FB;
    }

    .lbl-dias-litigios {
      font-size: 1rem;
      font-weight: 900;
      color: #92B9FA;
    }

    .litigios30 {
      font-size: .7rem;
      background-image: url('img/arrow-blue.png');
      background-position: center;
      background-size: cover;
      color: #fff;
      font-weight: 400 !important;
      height: 33px;
    }

    .litigios60 {
      font-size: .7rem;
      background-image: url('img/arrow-morado.png');
      background-position: center;
      background-size: cover;
      color: #fff;
      font-weight: 400 !important;
      height: 33px;
    }

    .litigios90 {
      font-size: .7rem;
      background-image: url('img/arrow-yellow.png');
      background-position: center;
      background-size: cover;
      color: #fff;
      font-weight: 400 !important;
      height: 33px;
    }

    .litigiosMay {
      font-size: .7rem;
      background-image: url('img/arrow-red.png');
      background-position: center;
      background-size: cover;
      color: #fff;
      font-weight: 400 !important;
      height: 33px;
    }

    .montos-litigios30 {
      padding: 10px 0px 10px 0px;
      font-size: .7rem;
      color: #4285F4;
      font-weight: 700 !important;
      border: 1px solid #4285F4;
      border-radius: 100px;
      box-shadow: 0 2px 5px 0 rgba(0,0,0,.16), 0 2px 10px 0 rgba(0,0,0,.12);
    }

    .montos-litigios60 {
      padding: 10px 0px 10px 0px;
      font-size: .7rem;
      color: #AA66CC;
      font-weight: 700 !important;
      border: 1px solid #AA66CC;
      border-radius: 100px;
      box-shadow: 0 2px 5px 0 rgba(0,0,0,.16), 0 2px 10px 0 rgba(0,0,0,.12);
    }

    .montos-litigios90 {
      padding: 10px 0px 10px 0px;
      font-size: .7rem;
      color: #FFBB33;
      font-weight: 700 !important;
      border: 1px solid #FFBB33;
      border-radius: 100px;
      box-shadow: 0 2px 5px 0 rgba(0,0,0,.16), 0 2px 10px 0 rgba(0,0,0,.12);
    }

    .montos-litigiosMay {
      padding: 10px 0px 10px 0px;
      font-size: .7rem;
      color: #D0021B;
      font-weight: 700 !important;
      border: 1px solid #D0021B;
      border-radius: 100px;
      box-shadow: 0 2px 5px 0 rgba(0,0,0,.16), 0 2px 10px 0 rgba(0,0,0,.12);
    }

    .space-top-litigios {
      padding-top: 15px;
    }

    .tam-logo {
      width: 80px !important;
    }

    .tt-status {
      color: #79ACD3;
      font-weight: 900;
    }

    .lbl-dt-status {
      color: #79ACD3;
      font-weight: 700;
    }

    .tt-acumalado-cy {
      font-size: 1rem;
      color: #93C3E9;
      font-weight: 700;
    }

    .lb-acumalado-cy {
      font-size: .8rem;
      color: #93C3E9;
      font-weight: 700;
    }

    .dt-acumalado-cy {
      font-size: .8rem;
      color: #979797;
      font-weight: 700;
    }

    .mc-fact-cm {
      padding: 0px 20px 0px 20px;
    }

    .border-right-acum-cy {
      border-right: 1px solid #DCDDDF;
    }

    .space-col-top-fac-com {
      padding-top: 5px;
    }

    .space-col-btm-fac-com {
      padding-bottom: 5px;
    }

    .td-acum_cy {
      width: 100px;
      border-right: 1px solid #DCDDDF;
    }

    .td-dt-acum-cy {
      padding-left: 10px;
    }

    .bk-acumulado-cy {
      background-color: #F7F9FD;
      border-radius: 10px;
    }

    .tt-legend-discresionales {
      font-size: .6rem;
      color: #F68A36;
      font-weight: 900;
    }

    .tt-legend-acuer-comer {
      font-size: .6rem;
      color: #90C752;
      font-weight: 900;
    }

    .square-discresionales {
      color: #F68A36 !important;
      font-size: .5rem !important;
    }

    .square-acuer-comer {
      color: #90C752 !important;
      font-size: .5rem !important;
    }

    .row-center-pastdue {
      margin-left: auto;
      margin-right: auto;
    }

    .row-cb-pastdue {
      background-color: #D7E6FD;
      border-radius: 10px 10px 0px 0px;
      padding: 3px;
    }

    .row-md-pastdue {
      background-color: #F0F7FF;
    }

    .row-bottom-pastdue {
      background-color: #F0F7FF;
      border-radius: 0px 0px 10px 10px;
    }

    .tt-tb-pastdue {
      font-size: .7rem;
      font-weight: 700;
      color: #79AAF8;
    }

    .dt-tb-pastdue {
      font-size: .7rem;
      font-weight: 700;
      color: #979797;
    }

    .col-tt-cab-barra {
      border-right: 1px solid #E7F0FD;
    }

    .col_dt-barra-rgt {
      border-right: 1px solid #D9DCE1;
    }

    .sback-dso {
      padding-top: 10px !important;
      padding-bottom: 10px !important;
      background-color: #FFFDEB !important;
      border-radius: 10px;
    }

    .scircle-calendar-dso {
      background-color: #FBD99A;
      padding: 10px;
      border-radius: 100px;
    }

    .lbl-dia-proceso-normal-dso {
      font-size: .8rem !important;
      font-weight: 900;
      color: #FBD99A;
    }

    .lbl-dias-dso {
      font-size: 1rem;
      font-weight: 900;
      color: #EDB559 !important;
    }

    .sback-dbt {
      padding-top: 10px !important;
      padding-bottom: 10px !important;
      background-color: #F7F7F7 !important;
      border-radius: 10px;
    }

    .scircle-calendar-dbt {
      background-color: #C4C4C4;
      padding: 10px;
      border-radius: 100px;
    }

    .lbl-dia-proceso-normal-dbt {
      font-size: .8rem !important;
      font-weight: 900;
      color: #5E9ACA;
    }

    .lbl-dias-dbt {
      font-size: 1rem;
      font-weight: 900;
      color: #5E9ACA !important;
    }

    .right-icons {
      float:right;
    }

    .col-marg-bottom {
      margin-bottom:6px;
    }

    header {
      background-image: url(../images/barrasuperior_menu.png);
      /*background-image: url(../images/barrasuperior_dash.png);*/
      /*background-position: center;*/
      background-repeat: no-repeat;
      background-size: cover;
    }

    hr {
      border-color: #ffca28 !important;
    }
    .navbar-nav{
      padding-left:20px!important;
    }
  </style>
</head>
<body class="body-dashboard">
  <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>
    <div id="loader"></div>
    <!--Navbar-->
    <header>
      <nav class="navbar navbar-expand-lg debtcontrol navbar-light">

        <!-- Navbar brand -->
        <a class="text-center" href="#">
          <img src="../images/logodebtcontrol.png" border="0" width="55px" /></a>

        <!-- Collapse button -->
        <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent-6"
          aria-controls="navbarSupportedContent-6" aria-expanded="false" aria-label="Toggle navigation">
          <span class="navbar-toggler-icon"></span>
        </button>

        <!-- Collapsible content -->
        <div class="collapse navbar-collapse" id="navbarSupportedContent-6">

          <!-- Links -->
          <%--<ul class="navbar-nav mr-auto">--%>
          <ul class="navbar-nav">

            <li id="idCmbHolding" runat="server" class="nav-item dropdown nav-seleccion">
              <a class="nav-link dropdown-toggle text-combo-seleccion" id="dropdownholding" runat="server" data-toggle="dropdown" aria-haspopup="true"
                aria-expanded="false">HOLDING</a>
              <div id="idItemDropdownHolding" runat="server" class="dropdown-menu dropdown-secondary" aria-labelledby="dropdownholding">
              </div>
            </li>
            <li id="idCmbCliente" runat="server" class="nav-item dropdown nav-seleccion">
              <a class="nav-link dropdown-toggle text-combo-seleccion" id="dropdownempresas" runat="server" data-toggle="dropdown" aria-haspopup="true"
                aria-expanded="false">EMPRESAS</a>
              <div id="idItemDropdownCliente" runat="server" class="dropdown-menu dropdown-secondary" aria-labelledby="dropdownempresas">
              </div>
            </li>
          </ul>
          <div class="form-inline">
            <div class="nav-search">
              <input id="txtSearch" class="txt-search" type="text" value="" placeholder="BUSCAR CLIENTE" />
            </div>
          </div>
          <ul class="navbar-nav ml-auto nav-flex-icons right-icons">
            <li class="nav-item">
              <a class="nav-link waves-effect waves-light" href="mainaccess.aspx"><i class="fas fa-home  fa-2x white-text"></i></a>
            </li>
            <li class="nav-item">
              <asp:LinkButton ID="bnt_logout" runat="server" CssClass="nav-link waves-effect waves-light" OnClick="bnt_logout_Click"><i class="fas fa-power-off fa-2x red-text"></i></asp:LinkButton>
            </li>
          </ul>
        </div>
        <!-- Collapsible content -->

      </nav>
    </header>
    <main>
      <div id="idContainer" runat="server" class="container-fluid">
        <!-- Card -->
        <div class="row">
          <div class="col-md-12 barra-cliente-holding">
            <asp:Label ID="idUser" runat="server" CssClass="lbl-usuario"></asp:Label>
            <asp:Label ID="lblDeudor" runat="server"></asp:Label>
            <asp:Label ID="lblClienteHolding" runat="server" CssClass="title align-middle blue-text" Visible="false"></asp:Label>
          </div>
        </div>
      </div>
    </main>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>


    <!-- Button trigger modal-->

    <!-- Modal: modalCart -->
    <div class="modal fade" id="modalCart" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel"
      aria-hidden="true">
      <div class="modal-dialog modal-full-height modal-right modal-notify modal-info" role="document">
        <div class="modal-content">
          <!--Header-->
          <div class="modal-header">
            <p class="heading lead">Selecciona Widget</p>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true" class="white-text">×</span>
            </button>
          </div>
          <!--Body-->
          <div class="modal-body">

            <%--<asp:UpdatePanel ID="PanelWidgetAvailable" runat="server" UpdateMode="Conditional">
              <ContentTemplate>--%>
            <asp:GridView ID="gdWidgets" runat="server" ShowHeader="false" ShowFooter="false" CssClass="table table-hover" BorderStyle="None" AllowPaging="true" BorderWidth="0" GridLines="None"
              AutoGenerateColumns="false" DataKeyNames="cod_widget"
              OnPageIndexChanging="gdWidgets_PageIndexChanging" OnRowCommand="gdWidgets_RowCommand">
              <Columns>
                <asp:TemplateField>
                  <ItemTemplate>
                    <asp:LinkButton runat="server" ID="btnSelected" CssClass="" CommandName="Seleted"><i class="far fa-circle"></i> </asp:LinkButton>
                  </ItemTemplate>
                  <ItemStyle Width="50px" />
                </asp:TemplateField>
                <asp:BoundField DataField="nom_widget">
                  <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
              </Columns>
            </asp:GridView>
            <%-- </ContentTemplate>
            </asp:UpdatePanel>--%>
          </div>
          <!--Footer-->
          <div class="modal-footer">
            <button class="btn btn-primary">Close</button>
          </div>
        </div>
      </div>
    </div>
    <!-- Button trigger modal-->

    <!--Modal: modalRelatedContent-->
    <div class="modal fade right" id="modalRelatedContent" tabindex="-1" role="dialog"
      aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="false">
      <div class="modal-dialog modal-side modal-bottom-right modal-warning modal-info" role="document">
        <!--Content-->
        <div class="modal-content">
          <!--Header-->
          <div class="modal-header">
            <p class="h4 white-text tt-alerta" style="font-weight: 400"><strong>Atención</strong> <i class="fas fa-exclamation"></i></p>

            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true" class="white-text">&times;</span>
            </button>
          </div>

          <!--Body-->
          <div class="modal-body">

            <div class="row row-alert-align">
              <div class="col-5 text-center">
                <i class="fas fa-exclamation-triangle fa-5x amber-text"></i>
              </div>

              <div class="col-7">
                <p class="justify-content-center text-alert">Para comenzar seleccione Holding o Cliente, para luego seguir con el Deudor, para esto utilice la búsqueda.</p>
              </div>
            </div>
            <div class="row">
              <div class="col-12 text-center">
                <button type="button" class="btn btn-warning btn-md" data-dismiss="modal">Cerrar</button>
              </div>
            </div>
          </div>
        </div>
        <!--/.Content-->
      </div>
    </div>
    <!--Modal: modalRelatedContent-->

    <asp:HiddenField ID="hdd_pos_widget" runat="server" />
    <asp:HiddenField ID="hdd_holding" runat="server" />
    <asp:HiddenField ID="hdd_cliente" runat="server" />
    <asp:HiddenField ID="hdd_ncodigodeudor" runat="server" />
    <asp:HiddenField ID="hdd_load_msj" runat="server" />
    <asp:HiddenField ID="hdd_delete_widget" runat="server" />
    <asp:HiddenField ID="hdd_loadnewcondition" runat="server" />
    <asp:HiddenField ID="hdd_new_seleted" runat="server" />
    <script type="text/javascript">
      google.charts.load('current', { 'packages': ['corechart', 'bar'] });
    </script>
  </form>
  <!-- Modal: modalCart -->
  <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
  <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
  <script type="text/javascript">

    $(document).ready(function () {
      $('.widget').click(function () {
        var posWidget = $(this).attr('data-value');
        $('#hdd_pos_widget').val(posWidget.substr(4, posWidget.length));
        $("#modalCart").modal();
      });

      $('.item-holding').click(function () {
        $('#hdd_holding').val($(this).attr('data-value'));
        $('#hdd_cliente').val('');
        $('#hdd_ncodigodeudor').val('');
        $('#hdd_loadnewcondition').val('1');
        $('#loader').show();
        document.forms[0].submit();
      });

      $('.item-cliente').click(function () {
        $('#hdd_holding').val('');
        $('#hdd_cliente').val($(this).attr('data-value'));
        $('#hdd_ncodigodeudor').val('');
        $('#hdd_loadnewcondition').val('1');
        $('#loader').show();
        document.forms[0].submit();
      });

      //$("#txtSearch").autocomplete({
      //  source: availableTags,
      //  select: function (event, ui) {
      //    var iPos = ui.item.value.indexOf(" ");
      //    var sValor = ui.item.value.substr(0, iPos);
      //    $('#hdd_ncodigodeudor').val(sValor);
      //    $('#loader').show();
      //    document.forms[0].submit();
      //  }
      //});

      $("#txtSearch").autocomplete({
        source: function (request, response) {
          if (($.trim(request.term) != "") && (request.term.length >= 3))
            $.ajax({
              url: 'getdeudores.asmx/getDeudores',
              method: 'POST',
              contentType: 'application/json;chartset=utf-8',
              data: '{"sNomDeudor":"' + request.term + '","sHolding":"' + $("#hdd_holding").val() + '","sCliente":"' + $("#hdd_cliente").val() + '"}',
              dataType: 'json',
              success: function (result) {
                response(result.d);
              },
              error: function (result) {
                alert('error: ' + result.d);
              }
            });
        },
        select: function (event, ui) {
          //var iPos = ui.item.value.indexOf(" ");
          //var sValor = ui.item.value.substr(0, iPos);
          //$('#hdd_ncodigodeudor').val(sValor);
          $('#hdd_ncodigodeudor').val(ui.item.value);
          $('#loader').show();
          document.forms[0].submit();
        }
      });

      $('.trash-ficha').click(function () {
        $('#hdd_delete_widget').val($(this).attr('data-value'));
        $('#loader').show();
        document.forms[0].submit();
      });

      $('#loader').hide();

      //var x = $("#donut_single").position();
      //$("#labelOverlay").css({ top: x.top + 70, left: x.left + 30, position: 'absolute' });

    });
  </script>
</body>
</html>
