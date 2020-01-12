<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="prueba3.aspx.cs" Inherits="ICommunity.Dashboard.prueba3" %>

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

  <style>
    body {
      padding-top: 20px;
    }

    .carousel {
      width: 100%;
    }

    .slide-box {
      display: flex;
      justify-content: space-between;
    }

    @media (min-width: 576px) and (max-width: 767.98px) {
      .slide-box img {
        -ms-flex: 0 0 50%;
        flex: 0 0 50%;
        max-width: 50%;
      }
    }

    @media (min-width: 768px) and (max-width: 991.98px) {
      .slide-box img {
        -ms-flex: 0 0 33.3333%;
        flex: 0 0 33.3333%;
        max-width: 33.3333%;
      }
    }

    @media (min-width: 992px) {
      .slide-box img {
        -ms-flex: 0 0 25%;
        flex: 0 0 25%;
        max-width: 25%;
      }
    }

    .carousel-caption {
      background-color: rgba(0, 0, 0, 0.5);
      padding: 20px;
      border-radius: .5rem;
    }


    .lastrequest {
      font-size: .7rem;
      font-weight: 400;
      border-radius: 8px;
      box-shadow: 0 2px 5px 0 rgba(0,0,0,.16), 0 2px 10px 0 rgba(0,0,0,.12);
      margin: 10px;
      height: 150px;
      width: 150px;
      float: left;
      position: relative;
      min-height: 150px;
      color: #fff;
    }

    .mostrequest {
      font-size: .7rem;
      font-weight: 400;
      border-radius: 8px;
      box-shadow: 0 2px 5px 0 rgba(0,0,0,.16), 0 2px 10px 0 rgba(0,0,0,.12);
      margin: 10px;
      height: 150px;
      width: 150px;
      float: left;
      position: relative;
      min-height: 150px;
      color: #fff;
    }

    .blq-cel {
      background-image: linear-gradient(180deg, #B9D7F6 5%, #66A5EB 100%);
      /*background-color: #66A5EB;*/
    }

    .blq-mar {
      background-image: linear-gradient(180deg, #D6A5AD 5%, #9B1E31 100%);
    }

    .blq-ver {
      background-image: linear-gradient(180deg, #B8D5C7 5%, #247E53 100%);
    }

    .blq-ama {
      background-image: linear-gradient(180deg, #FCE4C0 5%, #F7AB3C 100%);
    }

    .blq-azu {
      background-image: linear-gradient(180deg, #C8D2F0 5%, #375FCC 100%);
    }

    .blq-nar {
      background-image: linear-gradient(180deg, #FCD3BC 5%, #F87C37 100%);
    }

    .blq-mor {
      background-image: linear-gradient(180deg, #D6BEEB 5%, #8F51C8 100%);
    }

    .blq-plo {
      background-image: linear-gradient(180deg, #DBD7D2 5%,#DBD7D2 100%);
    }

    .tt-blq-fx {
      padding: 20px 5px 0px 5px;
      font-weight: 900;
    }

    .btm-blq-fch {
      padding: 10px;
      position: absolute;
      bottom: 0;
      left: 0px;
    }

    .btm-blq {
      padding: 10px;
      position: absolute;
      bottom: 0;
      left: 0px;
    }

    .btm-blq-getin {
      font-size: 1.2rem;
      padding: 10px;
      position: absolute;
      bottom: 0;
      right: 5px;
    }

    .tt-fecha {
      font-weight: 400;
    }

    .dt-fecha {
      font-weight: 700;
    }

    .go-in {
      font-size: 0.8rem;
    }

    .tt-menu {
      font-size: 1.2rem;
      font-weight: 900;
      color: #73A7D2;
    }
  </style>
</head>
<body>
  <form id="form1" runat="server">
    <div class="container">
      <div class="row">
        <div id="carousel" class="carousel slide" data-ride="carousel">
          <ol class="carousel-indicators">
            <li data-target="#carousel" data-slide-to="0" class="active"></li>
            <li data-target="#carousel" data-slide-to="1"></li>
          </ol>
          <div class="carousel-inner">
            <div class="carousel-item active">
              <div class="d-none d-lg-block">
                <div class="slide-box">
                  <div class="lastrequest blq-plo">
                    <div class="tt-blq-fx">PROVISIÓN</div>
                    <div class="btm-blq-fch">
                      <div class="dt-fecha">Próximamente</div>
                    </div>
                  </div>
                  <div class="lastrequest blq-azu">
                    <a href="litigios.aspx">
                      <div class="tt-blq-fx">LITIGIOS</div>
                      <div class="btm-blq-getin">
                        <svg class="svg-inline--fa fa-sign-in-alt fa-w-16" aria-hidden="true" data-prefix="fas" data-icon="sign-in-alt" role="img" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512" data-fa-i2svg="">
                          <path fill="currentColor" d="M416 448h-84c-6.6 0-12-5.4-12-12v-40c0-6.6 5.4-12 12-12h84c17.7 0 32-14.3 32-32V160c0-17.7-14.3-32-32-32h-84c-6.6 0-12-5.4-12-12V76c0-6.6 5.4-12 12-12h84c53 0 96 43 96 96v192c0 53-43 96-96 96zm-47-201L201 79c-15-15-41-4.5-41 17v96H24c-13.3 0-24 10.7-24 24v96c0 13.3 10.7 24 24 24h136v96c0 21.5 26 32 41 17l168-168c9.3-9.4 9.3-24.6 0-34z"></path></svg><!-- <i class="fas fa-sign-in-alt"></i> -->
                      </div>
                    </a>
                  </div>
                  <div class="lastrequest blq-plo">
                    <div class="tt-blq-fx">PROVISIÓN</div>
                    <div class="btm-blq-fch">
                      <div class="dt-fecha">Próximamente</div>
                    </div>
                  </div>
                  <div class="lastrequest blq-plo">
                    <div class="tt-blq-fx">PAST DUE CRÍTICO (Valor). No incluye notas de crédito</div>
                    <div class="btm-blq-fch">
                      <div class="dt-fecha">Próximamente</div>
                    </div>
                  </div>
                  <div class="lastrequest blq-plo">
                    <div class="tt-blq-fx">PAST DUE CRÍTICO (Días). No incluye Notas de crédito</div>
                    <div class="btm-blq-fch">
                      <div class="dt-fecha">Próximamente</div>
                    </div>
                  </div>
                  <div class="lastrequest blq-plo">
                    <div class="tt-blq-fx">DEUDA VENCIDA &gt; 30 DÍAS SIN LITIGIOS</div>
                    <div class="btm-blq-fch">
                      <div class="dt-fecha">Próximamente</div>
                    </div>
                  </div>
                  <div class="lastrequest blq-plo">
                    <div class="tt-blq-fx">SOBREGIRO LÍNEA DE CRÉDITO</div>
                    <div class="btm-blq-fch">
                      <div class="dt-fecha">Próximamente</div>
                    </div>
                  </div>
                  <div class="lastrequest blq-plo">
                    <div class="tt-blq-fx">ACUERDOS COMERCIALES Y DISCRECIONALES</div>
                    <div class="btm-blq-fch">
                      <div class="dt-fecha">Próximamente</div>
                    </div>
                  </div>
                  <div class="lastrequest blq-plo">
                    <div class="tt-blq-fx">MOVIMIENTOS NO IDENTIFICADOS</div>
                    <div class="btm-blq-fch">
                      <div class="dt-fecha">Próximamente</div>
                    </div>
                  </div>
                  <div class="mostrequest blq-azu"><a href="../reporting/app_resumen_cobranza_find.aspx">
                    <div class="tt-blq-fx">Resumen de Cobranza</div>
                    <div class="btm-blq-getin">
                      <svg class="svg-inline--fa fa-sign-in-alt fa-w-16" aria-hidden="true" data-prefix="fas" data-icon="sign-in-alt" role="img" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512" data-fa-i2svg="">
                        <path fill="currentColor" d="M416 448h-84c-6.6 0-12-5.4-12-12v-40c0-6.6 5.4-12 12-12h84c17.7 0 32-14.3 32-32V160c0-17.7-14.3-32-32-32h-84c-6.6 0-12-5.4-12-12V76c0-6.6 5.4-12 12-12h84c53 0 96 43 96 96v192c0 53-43 96-96 96zm-47-201L201 79c-15-15-41-4.5-41 17v96H24c-13.3 0-24 10.7-24 24v96c0 13.3 10.7 24 24 24h136v96c0 21.5 26 32 41 17l168-168c9.3-9.4 9.3-24.6 0-34z"></path></svg><!-- <i class="fas fa-sign-in-alt"></i> --></div>
                  </a></div>
                  <div class="mostrequest blq-mor"><a href="../reporting/app_antiguedad_deuda_fin.aspx">
                    <div class="tt-blq-fx">Antiguedad de Deuda</div>
                    <div class="btm-blq-getin">
                      <svg class="svg-inline--fa fa-sign-in-alt fa-w-16" aria-hidden="true" data-prefix="fas" data-icon="sign-in-alt" role="img" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512" data-fa-i2svg="">
                        <path fill="currentColor" d="M416 448h-84c-6.6 0-12-5.4-12-12v-40c0-6.6 5.4-12 12-12h84c17.7 0 32-14.3 32-32V160c0-17.7-14.3-32-32-32h-84c-6.6 0-12-5.4-12-12V76c0-6.6 5.4-12 12-12h84c53 0 96 43 96 96v192c0 53-43 96-96 96zm-47-201L201 79c-15-15-41-4.5-41 17v96H24c-13.3 0-24 10.7-24 24v96c0 13.3 10.7 24 24 24h136v96c0 21.5 26 32 41 17l168-168c9.3-9.4 9.3-24.6 0-34z"></path></svg><!-- <i class="fas fa-sign-in-alt"></i> --></div>
                  </a></div>
                  <div class="mostrequest blq-mar"><a href="../reporting/app_control_metas_real_find.aspx">
                    <div class="tt-blq-fx">Control Metas vs Real</div>
                    <div class="btm-blq-getin">
                      <svg class="svg-inline--fa fa-sign-in-alt fa-w-16" aria-hidden="true" data-prefix="fas" data-icon="sign-in-alt" role="img" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512" data-fa-i2svg="">
                        <path fill="currentColor" d="M416 448h-84c-6.6 0-12-5.4-12-12v-40c0-6.6 5.4-12 12-12h84c17.7 0 32-14.3 32-32V160c0-17.7-14.3-32-32-32h-84c-6.6 0-12-5.4-12-12V76c0-6.6 5.4-12 12-12h84c53 0 96 43 96 96v192c0 53-43 96-96 96zm-47-201L201 79c-15-15-41-4.5-41 17v96H24c-13.3 0-24 10.7-24 24v96c0 13.3 10.7 24 24 24h136v96c0 21.5 26 32 41 17l168-168c9.3-9.4 9.3-24.6 0-34z"></path></svg><!-- <i class="fas fa-sign-in-alt"></i> --></div>
                  </a></div>
                </div>
              </div>
            </div>
            <%--<div class="d-none d-md-block d-lg-none">
                <div class="slide-box">
                  <img src="https://picsum.photos/240/200/?image=0&random" alt="First slide">
                  <img src="https://picsum.photos/240/200/?image=1&random" alt="First slide">
                  <img src="https://picsum.photos/240/200/?image=2&random" alt="First slide">
                </div>
              </div>
              <div class="d-none d-sm-block d-md-none">
                <div class="slide-box">
                  <img src="https://picsum.photos/270/200/?image=0&random" alt="First slide">
                  <img src="https://picsum.photos/270/200/?image=1&random" alt="First slide">
                </div>
              </div>
              <div class="d-block d-sm-none">
                <img class="d-block w-100" src="https://picsum.photos/600/400/?image=0&random" alt="First slide">
              </div>--%>
          </div>
          <%--<div class="carousel-item">
              <div class="d-none d-lg-block">
                <div class="slide-box">
                  <img src="https://picsum.photos/285/200/?image=4&random" alt="Second slide">
                  <img src="https://picsum.photos/285/200/?image=5&random" alt="Second slide">
                  <img src="https://picsum.photos/285/200/?image=6&random" alt="Second slide">
                  <img src="https://picsum.photos/285/200/?image=7&random" alt="Second slide">
                </div>
              </div>
              <div class="d-none d-md-block d-lg-none">
                <div class="slide-box">
                  <img src="https://picsum.photos/240/200/?image=3&random" alt="Second slide">
                  <img src="https://picsum.photos/240/200/?image=4&random" alt="Second slide">
                  <img src="https://picsum.photos/240/200/?image=5&random" alt="Second slide">
                </div>
              </div>
              <div class="d-none d-sm-block d-md-none">
                <div class="slide-box">
                  <img src="https://picsum.photos/270/200/?image=2&random" alt="Second slide">
                  <img src="https://picsum.photos/270/200/?image=3&random" alt="Second slide">
                </div>
              </div>
              <div class="d-block d-sm-none">
                <img class="d-block w-100" src="https://picsum.photos/600/400/?image=1&random" alt="Second slide">
              </div>
            </div>--%>
        </div>
        <a class="carousel-control-prev" href="#carousel" role="button" data-slide="prev">
          <span class="carousel-control-prev-icon" aria-hidden="true"></span>
          <span class="sr-only">Previous</span>
        </a>
        <a class="carousel-control-next" href="#carousel" role="button" data-slide="next">
          <span class="carousel-control-next-icon" aria-hidden="true"></span>
          <span class="sr-only">Next</span>
        </a>
      </div>
    </div>
    </div>
  </form>
  <!-- JQuery -->
  <script type="text/javascript" src="js/jquery-3.4.1.min.js"></script>
  <!-- Bootstrap tooltips -->
  <script type="text/javascript" src="js/popper.min.js"></script>
  <!-- Bootstrap core JavaScript -->
  <script type="text/javascript" src="js/bootstrap.min.js"></script>
  <!-- MDB core JavaScript -->
  <script type="text/javascript" src="js/mdb.min.js"></script>
</body>
</html>
