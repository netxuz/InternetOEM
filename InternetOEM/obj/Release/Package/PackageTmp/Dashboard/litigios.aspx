<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="litigios.aspx.cs" Inherits="ICommunity.Dashboard.litigios" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta charset="utf-8" />
  <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
  <meta http-equiv="x-ua-compatible" content="ie=edge" />
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
  <link href='http://fonts.googleapis.com/css?family=Lato:100,400,700,900' rel='stylesheet' type='text/css' />
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
    header {
      background-image: url(../images/barrasuperior_menu.png);
      /*background-image: url(../images/barrasuperior_dash.png);*/
      /*background-position: center;*/
      background-repeat: no-repeat;
      background-size: cover;
    }

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

    /*.col, .col-1, .col-10, .col-11, .col-12, .col-2, .col-3, .col-4, .col-5, .col-6, .col-7, .col-8, .col-9, .col-auto, .col-lg, .col-lg-1, .col-lg-10, .col-lg-11, .col-lg-12, .col-lg-2, .col-lg-3, .col-lg-4, .col-lg-5, .col-lg-6, .col-lg-7, .col-lg-8, .col-lg-9, .col-lg-auto, .col, .col-1, .col-10, .col-11, .col-12, .col-2, .col-3, .col-4, .col-5, .col-6, .col-7, .col-8, .col-9, .col-auto, .col-sm, .col-sm-1, .col-sm-10, .col-sm-11, .col-sm-12, .col-sm-2, .col-sm-3, .col-sm-4, .col-sm-5, .col-sm-6, .col-sm-7, .col-sm-8, .col-sm-9, .col-sm-auto, .col-xl, .col-xl-1, .col-xl-10, .col-xl-11, .col-xl-12, .col-xl-2, .col-xl-3, .col-xl-4, .col-xl-5, .col-xl-6, .col-xl-7, .col-xl-8, .col-xl-9, .col-xl-auto {
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
      margin-bottom: 6px !important;
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
      padding: .35rem 1rem;
      width: 350px;
    }

    .txt-search {
      color: #6784AA !important;
      font-weight: 600 !important;
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
      border-color:#ffca28!important;
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

    #labelOverlay-litigios {
      position: absolute;
      top: 50%;
      left: 50%;
      width:170px;
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
      color: #B2B2B2;
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
      font-size: .9rem !important;
      font-weight: 900 !important;
      color: #79ACD3 !important;
      padding-top: 8px !important;
      padding-bottom: 6px !important;
      text-align: center;
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
      font-size: .7rem !important;
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

    /*-------------------------*/

    .row-resumen-litigios30 {
      background-color: #4285F4;
      border-radius: 15px;
      line-height: 1;
      margin: 1px;
      box-shadow: 0 2px 5px 0 rgba(0,0,0,.16), 0 2px 10px 0 rgba(0,0,0,.12);
    }

    .col-resumen-litigios30 {
      background-color: #ffffff;
      border: 1px solid #4285F4;
      border-radius: 15px;
      line-height: 1;
    }

    .tt-resumen-litigios30 {
      font-size: .7rem !important;
      font-weight: 700;
      color: #4285F4;
    }

    /*-------------------------*/

    .row-resumen-litigios60 {
      background-color: #AA66CC;
      border-radius: 15px;
      line-height: 1;
      margin: 1px;
      box-shadow: 0 2px 5px 0 rgba(0,0,0,.16), 0 2px 10px 0 rgba(0,0,0,.12);
    }

    .col-resumen-litigios60 {
      background-color: #ffffff;
      border: 1px solid #AA66CC;
      border-radius: 15px;
      line-height: 1;
    }

    .tt-resumen-litigios60 {
      font-size: .7rem !important;
      font-weight: 700;
      color: #AA66CC;
    }

    /*-------------------------*/

    .row-resumen-litigios90 {
      background-color: #F5A623;
      border-radius: 15px;
      line-height: 1;
      margin: 1px;
      box-shadow: 0 2px 5px 0 rgba(0,0,0,.16), 0 2px 10px 0 rgba(0,0,0,.12);
    }

    .col-resumen-litigios90 {
      background-color: #ffffff;
      border: 1px solid #F5A623;
      border-radius: 15px;
      line-height: 1;
    }

    .tt-resumen-litigios90 {
      font-size: .7rem !important;
      font-weight: 700;
      color: #F5A623;
    }

    /*-------------------------*/

    .row-resumen-litigios-mayor90 {
      background-color: #D0021B;
      border-radius: 15px;
      line-height: 1;
      margin: 1px;
      box-shadow: 0 2px 5px 0 rgba(0,0,0,.16), 0 2px 10px 0 rgba(0,0,0,.12);
    }

    .col-resumen-litigios-mayor90 {
      background-color: #ffffff;
      border: 1px solid #D0021B;
      border-radius: 15px;
      line-height: 1;
    }

    .tt-resumen-litigios-mayor90 {
      font-size: .7rem !important;
      font-weight: 700;
      color: #D0021B;
    }

    /*-------------------------*/

    .lb-dt-resumen-litigios-monto {
      font-size: .8rem;
      font-weight: 700;
      color: #808080;
    }

    .lb-dt-resumen-litigios-cant {
      font-size: .7rem;
      font-weight: 700;
      color: #808080;
    }

    .scircle-calendar-litigos {
      background-color: #699FF7;
      padding: 10px;
      border-radius: 100%;
    }

    .row-cabecera-litigios-resumen {
      background-color: #EAF1FF;
      padding: 10px 10px 10px 10px;
      border-radius: 10px;
      margin-bottom: 10px;
      margin-left: 3px;
      margin-right: 3px;
    }

    .scol-line-height-resumen {
      line-height: 1;
      padding-bottom: 10px;
    }

    .tt-monto-litigio {
      color: #B7B7B7;
      font-weight: 700;
    }

    #labelOverlay-litigios {
      position: absolute;
      cursor: default;
    }

      #labelOverlay-litigios span {
        line-height: 0.3;
        padding: 0;
        margin: 8px;
      }

        #labelOverlay-litigios span.monto_litigios {
          font-size: 1rem !important;
          font-weight: bold !important;
          color: #737373 !important;
        }

    .sPorcentaje-antig-litigio {
      font-size: .8rem;
      font-weight: 700;
      color: #ffffff;
    }

    .space-box {
      padding: 1px;
    }

    .row-margin-bottom {
      margin-bottom: 10px;
    }

    .tt-canal {
      font-size: .8rem !important;
      color: #A7CBF5 !important;
      font-weight: 700 !important;
    }

    .lb-dt-canal-submotivo-litigios-monto {
      font-size: .9rem;
      font-weight: 700;
      color: #B7B7B7;
    }

    .lb-dt-canal-submotivo-litigios-cant {
      font-size: .7rem;
      font-weight: 700;
      color: #B7B7B7;
    }

    .GridPager {
      background-color: #fff;
      padding: 2px;
      margin: 2% auto;
    }

      .GridPager a {
        margin: auto 1%;
        border-radius: 50%;
        background-color: #EEEEEE;
        padding: 5px 10px 5px 10px;
        color: #A3A3A3 !important;
        text-decoration: none;
      }

      .GridPager span {
        background-color: #4A90E2;
        color: #fff !important;
        border-radius: 50%;
        padding: 5px 10px 5px 10px;
      }

      .GridPager a:hover {
        background-color: #4A90E2;
        color: #fff !important;
      }

    .search-width-litigios {
      width: 200px !important;
    }

    .border-right-litigios {
      border-right: 1px solid #E5E5E5;
    }

    .padding-right-detalle {
      padding-right: 30px;
    }

    .download_excel {
      font-size: .9rem !important;
      font-weight: 700;
      color: #4CAF50;
    }

    .right-icons {
      float: right;
    }

    .col-marg-bottom {
      margin-bottom: 6px;
    }
    .space-divbox {
      height:20px;
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
    <header>
      <nav class="navbar navbar-expand-lg debtcontrol navbar-light">

        <!-- Navbar brand -->
        <a class="navbar-brand tam-logo text-center" href="#">
          <img src="../images/logodebtcontrol.png" border="0" width="55px" /></a>

        <!-- Collapse button -->
        <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent-6"
          aria-controls="navbarSupportedContent-6" aria-expanded="false" aria-label="Toggle navigation">
          <span class="navbar-toggler-icon"></span>
        </button>

        <!-- Collapsible content -->
        <div class="collapse navbar-collapse" id="navbarSupportedContent-6">
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
        <div class="row">
          <div class="col-12 barra-cliente-holding">
            <asp:Label ID="idUser" runat="server" CssClass="lbl-usuario"></asp:Label>
            <asp:Label ID="lblDeudor" runat="server"></asp:Label>
            <asp:Label ID="lblClienteHolding" runat="server" CssClass="title align-middle blue-text" Visible="false"></asp:Label>
          </div>
        </div>
        <div class="row">
          <div class="col-md-6 col-marg-bottom">
            <div class="card">
              <div class="cb-ficha">
                <span class="card-title tt-ficha text-left">Resumen</span>
              </div>
              <hr />
              <div class="card-body">

                <div class="row">
                  <div class="col-md-4 text-center Valiniamiento">
                    <asp:LinkButton ID="lnk_litigios" runat="server" OnClick="lnk_litigios_Click">
                      <div id="donut_single"></div>
                      <div id="labelOverlay-litigios">
                        <span class="tt-monto-litigio">Litigios:</span>
                        <asp:Label ID="lb_monto_litigios" runat="server" CssClass="monto_litigios"></asp:Label>
                      </div>
                    </asp:LinkButton>
                  </div>
                  <div class="col-md-8">
                    <div class="row">
                      <div class="col-12">
                        <div class="row row-cabecera-litigios-resumen">
                          <div class="col-3 text-center Valiniamiento">
                            <div class="scircle-calendar-litigos">
                              <i class="fas fa-calendar-alt fa-2x white-text"></i>
                            </div>
                          </div>
                          <div class="col-6 text-left Valiniamiento">
                            <span class="lbl-dia-proceso-normal-litigio">Días proceso normalización:</span>
                          </div>
                          <div class="col text-left Valiniamiento">
                            <asp:Label runat="server" ID="lbl_dias_proc_normal" CssClass="lbl-dias-litigios"></asp:Label>
                          </div>
                        </div>
                      </div>
                    </div>
                    <div class="row row-margin-bottom">
                      <div class="col-6">
                        <div class="row row-resumen-litigios30">
                          <div class="col-4 text-center Valiniamiento">
                            <asp:Label ID="lb_porcentaje_30" runat="server" CssClass="sPorcentaje-antig-litigio"></asp:Label>
                          </div>
                          <div class="col-8 col-resumen-litigios30">
                            <div><span class="tt-resumen-litigios30">0-30 días:</span></div>
                            <div>
                              <asp:LinkButton ID="lnk_litigios30" runat="server" OnClick="lnk_litigios30_Click">
                                <asp:Label ID="lb_monto_30" runat="server" CssClass="lb-dt-resumen-litigios-monto"></asp:Label>
                              </asp:LinkButton>
                            </div>
                            <div>
                              <asp:Label ID="lb_documento_30" runat="server" CssClass="lb-dt-resumen-litigios-cant"></asp:Label>
                            </div>
                          </div>
                        </div>
                      </div>
                      <div class="col-6">

                        <div class="row row-resumen-litigios60">
                          <div class="col-4 text-center Valiniamiento">
                            <asp:Label ID="lb_porcentaje_60" runat="server" CssClass="sPorcentaje-antig-litigio"></asp:Label>
                          </div>
                          <div class="col-8 col-resumen-litigios60">
                            <div><span class="tt-resumen-litigios60">31-60 días:</span></div>
                            <div>
                              <asp:LinkButton ID="lnk_litigios60" runat="server" OnClick="lnk_litigios60_Click">
                                <asp:Label ID="lb_monto_60" runat="server" CssClass="lb-dt-resumen-litigios-monto"></asp:Label>
                              </asp:LinkButton>
                            </div>
                            <div>
                              <asp:Label ID="lb_documento_60" runat="server" CssClass="lb-dt-resumen-litigios-cant"></asp:Label>
                            </div>
                          </div>
                        </div>

                      </div>
                    </div>
                    <div class="row">
                      <div class="col-6">

                        <div class="row row-resumen-litigios90">
                          <div class="col-4 text-center Valiniamiento">
                            <asp:Label ID="lb_porcentaje_90" runat="server" CssClass="sPorcentaje-antig-litigio"></asp:Label>
                          </div>
                          <div class="col-8 col-resumen-litigios90">
                            <div><span class="tt-resumen-litigios90">61-90 días:</span></div>
                            <div>
                              <asp:LinkButton ID="lnk_litigios90" runat="server" OnClick="lnk_litigios90_Click">
                                <asp:Label ID="lb_monto_90" runat="server" CssClass="lb-dt-resumen-litigios-monto"></asp:Label>
                              </asp:LinkButton>
                            </div>
                            <div>
                              <asp:Label ID="lb_documento_90" runat="server" CssClass="lb-dt-resumen-litigios-cant"></asp:Label>
                            </div>
                          </div>
                        </div>

                      </div>
                      <div class="col-6">

                        <div class="row row-resumen-litigios-mayor90">
                          <div class="col-4 text-center Valiniamiento">
                            <asp:Label ID="lbl_porcentaje_mayor90" runat="server" CssClass="sPorcentaje-antig-litigio"></asp:Label>
                          </div>
                          <div class="col-8 col-resumen-litigios-mayor90">
                            <div><span class="tt-resumen-litigios-mayor90">> 90 días:</span></div>
                            <div>
                              <asp:LinkButton ID="lnk_litigiosmayor" runat="server" OnClick="lnk_litigiosmayor_Click">
                                <asp:Label ID="lb_monto_mayor90" runat="server" CssClass="lb-dt-resumen-litigios-monto"></asp:Label>
                              </asp:LinkButton>
                            </div>
                            <div>
                              <asp:Label ID="lb_documento_mayor90" runat="server" CssClass="lb-dt-resumen-litigios-cant"></asp:Label>
                            </div>
                          </div>
                        </div>

                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="col-md col-marg-bottom">
            <div class="card">
              <div class="cb-ficha">
                <span class="card-title tt-ficha text-left">Detalle Canal (top 4)</span>
              </div>
              <hr />
              <div class="card-body">
                <div id="idDetalle_canal" runat="server" class="row">
                </div>
              </div>
            </div>
          </div>
          <div class="col-md col-marg-bottom">
            <div class="card">
              <div class="cb-ficha">
                <span class="card-title tt-ficha text-left">Submotivo</span>
              </div>
              <hr />
              <div class="card-body">
                <div id="idSubMotivo" runat="server" class="row">
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="row row-line-height">
          <div class="col-12">
            <br>
          </div>
        </div>
        <div class="row">
          <div class="col-12">
            <div class="card">
              <div class="cb-ficha">
                <div class="row">
                  <div class="col-md-7 text-left">
                    <span class="card-title tt-ficha text-left">Detalle</span>
                  </div>
                  <div class="col-md form-inline md-form form-sm active-cyan-2 mt-2">
                    <asp:TextBox ID="nom_cliente" runat="server" placeholder="nombre cliente" CssClass="form-control form-control-sm mr-3 w-75 search-width-litigios" aria-label="Search"></asp:TextBox>
                    <asp:TextBox ID="num_factura" runat="server" placeholder="# de documento" CssClass="form-control form-control-sm mr-3 w-75 search-width-litigios" aria-label="Search"></asp:TextBox>
                    <asp:LinkButton ID="lnk_btn_search" runat="server" CssClass="fas fa-search" aria-hidden="true" OnClick="lnk_btn_search_Click"></asp:LinkButton>
                  </div>
                </div>
              </div>
              <hr />
              <div class="card-body">
                <div id="toExcel" runat="server" class="row" visible="false">
                  <div class="col-12 text-right padding-right-detalle">
                    <%--<asp:LinkButton ID="lnk_download_excel" runat="server" CssClass="download_excel" OnClick="lnk_download_excel_Click"><i class="fas fa-file-excel green-text"></i> Export</asp:LinkButton>--%>
                    <a href="#" id="lnk_download_excel2" class="download_excel"><i class="fas fa-file-excel green-text"></i>Export</a>
                  </div>
                </div>
                <div class="row">
                  <div class="col-12">
                    <div class="table-responsive-md">
                      <asp:GridView ID="gdDetalle" runat="server" ShowFooter="false" CssClass="table table-curved"
                        BorderStyle="None" Width="100%" AllowPaging="true" BorderWidth="0" GridLines="None"
                        AutoGenerateColumns="false" DataKeyNames="codigo" OnPageIndexChanging="gdDetalle_PageIndexChanging" PageSize="10">
                        <AlternatingRowStyle CssClass="tr-border-round" />
                        <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                        <Columns>
                          <asp:BoundField HeaderText="Código" DataField="codigo">
                            <HeaderStyle CssClass="cabeceragestion" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" CssClass="" />
                          </asp:BoundField>
                          <asp:BoundField HeaderText="Cliente" DataField="cliente">
                            <HeaderStyle HorizontalAlign="Center" CssClass="cabeceragestion" />
                            <ItemStyle HorizontalAlign="Left" CssClass="" />
                          </asp:BoundField>
                          <asp:BoundField HeaderText="Factura" DataField="factura">
                            <HeaderStyle HorizontalAlign="Center" CssClass="cabeceragestion" />
                            <ItemStyle HorizontalAlign="Center" CssClass="" />
                          </asp:BoundField>
                          <asp:BoundField HeaderText="Días Atraso" DataField="dias_atraso">
                            <HeaderStyle HorizontalAlign="Center" CssClass="cabeceragestion" />
                            <ItemStyle HorizontalAlign="Center" CssClass="" />
                          </asp:BoundField>
                          <asp:BoundField HeaderText="Saldo Factura" DataField="saldo_factura" DataFormatString="{0:N0}">
                            <HeaderStyle HorizontalAlign="Center" CssClass="cabeceragestion" />
                            <ItemStyle HorizontalAlign="Right" CssClass="" />
                          </asp:BoundField>
                          <asp:BoundField HeaderText="Monto Litigio" DataField="monto_litigio" DataFormatString="{0:N0}">
                            <HeaderStyle HorizontalAlign="Center" CssClass="cabeceragestion" />
                            <ItemStyle HorizontalAlign="Right" CssClass="" />
                          </asp:BoundField>
                          <asp:BoundField HeaderText="Fecha Solicitud" DataField="fecha_solicitud" DataFormatString="{0:dd/MM/yyyy}">
                            <HeaderStyle HorizontalAlign="Center" CssClass="cabeceragestion" />
                            <ItemStyle HorizontalAlign="Right" CssClass="" />
                          </asp:BoundField>
                          <asp:BoundField HeaderText="Motivo" DataField="comentario">
                            <HeaderStyle HorizontalAlign="Center" CssClass="cabeceragestion" />
                            <ItemStyle HorizontalAlign="Left" CssClass="" />
                          </asp:BoundField>
                          <asp:BoundField HeaderText="Sub Motivo" DataField="submotivo">
                            <HeaderStyle HorizontalAlign="Center" CssClass="cabeceragestion" />
                            <ItemStyle HorizontalAlign="Left" CssClass="" />
                          </asp:BoundField>
                          <asp:BoundField HeaderText="Comentario" DataField="motivo">
                            <HeaderStyle HorizontalAlign="Center" CssClass="cabeceragestion" />
                            <ItemStyle HorizontalAlign="Left" CssClass="" />
                          </asp:BoundField>
                          <asp:BoundField HeaderText="Vendedor" DataField="vendedor">
                            <HeaderStyle HorizontalAlign="Center" CssClass="cabeceragestion" />
                            <ItemStyle HorizontalAlign="Left" CssClass="" />
                          </asp:BoundField>
                          <asp:BoundField HeaderText="Canal" DataField="Canal">
                            <HeaderStyle HorizontalAlign="Center" CssClass="cabeceragestion" />
                            <ItemStyle HorizontalAlign="Left" CssClass="" />
                          </asp:BoundField>
                        </Columns>
                      </asp:GridView>
                    </div>
                  </div>
                </div>
                <div class="row">
                  <div class="col-12">
                    <br />
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="row">
          <div class="col-12">
            <br />
          </div>
        </div>
      </div>
    </main>
    <iframe id="id_iframe_oculto" width="0" height="0" hidden="hidden"></iframe>
    <asp:HiddenField ID="hdd_holding" runat="server" />
    <asp:HiddenField ID="hdd_cliente" runat="server" />
    <asp:HiddenField ID="hdd_ncodigodeudor" runat="server" />
    <asp:HiddenField ID="hdd_loadnewcondition" runat="server" />
    <asp:HiddenField ID="hdd_periodo" runat="server" />
    <script type="text/javascript">
      google.charts.load('current', { 'packages': ['corechart', 'bar'] });
    </script>
  </form>
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

      $('#lnk_download_excel2').click(function () {
        $('#loader').show();

        var iframe = $('#id_iframe_oculto');
        var sUrl = 'dwn_litigios.ashx?hdd_cliente=' + $('#hdd_cliente').val();
        var sUrl = sUrl + '&hdd_holding=' + $('#hdd_holding').val();
        var sUrl = sUrl + '&hdd_periodo=' + $('#hdd_periodo').val();
        var sUrl = sUrl + '&nom_cliente=' + $('#nom_cliente').val();
        var sUrl = sUrl + '&num_factura=' + $('#num_factura').val();
        iframe.attr('src', sUrl);
        $('#loader').hide();
        return false;

      });

      //$("#txtSearch").autocomplete({
      //  source: function (request, response) {
      //    $.ajax({
      //      url: 'getdeudores.asmx/getDeudores',
      //      method: 'POST',
      //      contentType: 'application/json;chartset=utf-8',
      //      data: '{"sNomDeudor":"' + request.term + '","sHolding":"","sCliente":""}',
      //      dataType: 'json',
      //      success: function (result) {
      //        response(result.d);
      //      },
      //      error: function (result) {
      //        alert('error: ' + result.d);
      //      }
      //    });
      //  },
      //  select: function (event, ui) {
      //    //var iPos = ui.item.value.indexOf(" ");
      //    //var sValor = ui.item.value.substr(0, iPos);
      //    //$('#hdd_ncodigodeudor').val(sValor);
      //    $('#hdd_ncodigodeudor').val(ui.item.value);
      //    $('#loader').show();
      //    document.forms[0].submit();
      //  }
      //});

      $('#loader').hide();

      //var x = $("#donut_single").position();
      //$("#labelOverlay-litigios").css({ top: x.top + 60, left: x.left + 25, position: 'absolute' });

    });
  </script>
</body>
</html>
