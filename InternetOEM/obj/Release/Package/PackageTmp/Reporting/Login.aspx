<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ICommunity.Reporting.Login" %>

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
  <link href="../Dashboard/css/bootstrap.min.css" rel="stylesheet" />
  <!-- Material Design Bootstrap -->
  <link href="../Dashboard/css/mdb.min.css" rel="stylesheet" />
  <!-- Your custom styles (optional) -->
  <link href="../Dashboard/css/style.css" rel="stylesheet" />
  <title>Home</title>
  <!-- SCRIPTS -->
  <link href='https://fonts.googleapis.com/css?family=Lato:100,400,700,900' rel='stylesheet' type='text/css' />
  <!-- JQuery -->
  <script type="text/javascript" src="../Dashboard/js/jquery-3.4.1.min.js"></script>
  <!-- Bootstrap tooltips -->
  <script type="text/javascript" src="../Dashboard/js/popper.min.js"></script>
  <!-- Bootstrap core JavaScript -->
  <script type="text/javascript" src="../Dashboard/js/bootstrap.min.js"></script>
  <!-- MDB core JavaScript -->
  <script type="text/javascript" src="../Dashboard/js/mdb.min.js"></script>
  <style>
    body, html {
      background-image: url(../images/fondo14.jpg);
      background-position: center;
      background-repeat: no-repeat;
      background-size: auto;
      font-family: 'Lato', sans-serif !important;
      font-weight: 400;
      height: 100vh;
      margin: 0px;
      line-height: unset;
    }

    .alto {
      height: 100vh;
    }

    .Valiniamiento {
      display: flex;
      justify-content: center;
      align-content: center;
      flex-direction: column;
    }

    .border-right-login {
      border: 1px solid #000;
      height: 100%;
    }

    .logo-title {
      margin-bottom: 30px;
    }

    .logodebtcontrol {
      width: 100px;
    }

    .img-logo {
      background: url(../images/logodebtcontrol.png);
      width: 70px;
      height: 60px;
      background-position: center;
      background-size: contain;
      float: left;
    }

    .tt-cb-blue-login {
      font-family: Roboto,sans-serif !important;
      color: #0B82B8;
      text-align: left;
      display: inline-block;
      font-size: 40px;
      font-weight: 300 !important;
      line-height: 1;
    }

    .tt-cb-deep-blue-login {
      font-family: Roboto,sans-serif !important;
      color: #3D5374;
      text-align: left;
      display: inline-block;
      font-size: 40px;
      font-weight: 300 !important;
      line-height: 1;
    }

    .brand_slogan {
      font-family: Roboto,sans-serif !important;
      color: #3D5374;
      font-weight: 300 !important;
      font-size: 10px;
    }

    .form-w {
      width:85%!important;
    }

    .form-row {
      margin-left: 40px !important;
      margin-right: 40px !important;
      border: 1px solid #ffca28;
      border-radius: 10px;
      padding: 20px 20px 20px 20px;
    }

    .tt-login {
      font-size: 1.1rem;
      font-weight: 700;
      color: #1B3568;
    }

    .lb-login {
      font-size: .9rem;
      font-weight: 700;
      color: #3F97C3;
      margin-bottom: 2px;
    }

    .form-control {
      background-color: #E8F0FE !important;
      margin-bottom: 20px !important;
    }

    .waves-effect {
      overflow: unset;
    }

    .btn-info {
      background-color: #2A8BBC !important;
      font-family: 'Lato', sans-serif !important;
      font-weight: 900 !important;
      border-radius: 10px;
    }

    .cb-ficha {
      padding: 20px 30px 20px 30px;
    }

    .card {
      border-radius: .7rem !important;
    }

    .trash-ficha {
      position: absolute !important;
      font-size: 1.3rem !important;
      font-weight: lighter !important;
      right: 0 !important;
      color: #E4E4E4 !important;
    }

    a.block-ref {
      color: #fff !important;
    }

    .blq-sec {
      background-color: #0d47a1;
      margin: 20px 40px 20px 40px;
      padding: 10px 40px 10px 40px;
      border-radius: 10px;
      /*border: 1px solid #C0D9F6;*/
    }

    hr {
      margin-top: 10px !important;
      margin-bottom: 10px !important;
    }

    .card-body {
      padding: unset;
    }
    .card-title {
      font-size:.8rem;
    }

    .padding-logo {
      padding: 40px 0px 40px 0px;
    }

    .tt-info {
      font-size: 30px;
      color: #51626F;
    }

    .sub-tt-info {
      color: #D9D9D9;
    }

    .padding-bottom-tt-info {
      padding-bottom: 40px;
    }

    .tt-parrafo {
      color: #51626F;
      font-weight: 900;
    }

    p {
      /*margin-top: 20px;*/
      margin-bottom: 20px;
      color: #51626F;
    }

    li {
      color: #51626F;
    }

    .margin-info {
      margin-right: 10px;
    }

    .img-logo2 {
      background: url(../images/logodebtcontrol.png);
      width: 80px;
      height: 60px;
      background-position: center;
      background-size: contain;
      float: left;
    }

    .tt-cb-blue-login2 {
      font-family: Roboto,sans-serif !important;
      color: #0B82B8;
      text-align: left;
      display: inline-block;
      font-size: 40px;
      font-weight: 300 !important;
      line-height: 1;
    }

    .tt-cb-deep-blue-login2 {
      font-family: Roboto,sans-serif !important;
      color: #3D5374;
      text-align: left;
      display: inline-block;
      font-size: 40px;
      font-weight: 300 !important;
      line-height: 1;
    }

    .brand_slogan2 {
      font-family: Roboto,sans-serif !important;
      color: #3D5374;
      font-size: 12px;
      font-weight: 300 !important;
    }

    @media (min-width: 576px) {
      .modal-dialog {
        max-width: 900px !important;
      }
    }
  </style>
</head>
<body>
  <div class="container-fluid">
    <div class="row">
      <div class="col-md-4"></div>
      <div class="col-md-4 alto Valiniamiento">
        <asp:Panel ID="panel" CssClass="form-w" runat="server" DefaultButton="btnAceptar">
          <form id="form1" runat="server">
            <div class="card">
              <div class="cb-ficha">
                <img src="../images/logofull.jpg" width="100%" />
              </div>
              <div class="card-body">
                <div class="form-row">
                  <p class="text-center w-100">
                    <asp:Label ID="lblTitle" runat="server" Text="Bienvenido a DBT Always On" CssClass="tt-login" Visible="true"></asp:Label>
                  </p>
                  <asp:Label ID="lblLogin" runat="server" Text="Usuario" Visible="true" CssClass="lb-login"></asp:Label>
                  <asp:TextBox ID="txtLogin" runat="server" CssClass="form-control"></asp:TextBox>
                  <asp:Label ID="lblPassword" runat="server" Text="" Visible="true" CssClass="lb-login"></asp:Label>
                  <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                  <div class="d-flex justify-content-center w-100">
                    <asp:Button ID="btnAceptar" runat="server" Text="Ingresar" OnClick="btnAceptar_Click" CssClass="btn btn-amber btn-sm" />
                  </div>
                </div>
                <a href="#" class="block-ref" data-toggle="modal" data-target="#modalPush">
                  <div class="blq-sec">
                    <div class="float-right waves-effect"><i class="fas fa-angle-right fa-1x"></i></div>
                    <i class="fas fa-info-circle fa-1x"></i> <span class="card-title tt-ficha text-left">Información de seguridad</span>
                  </div>
                </a>
              </div>
            </div>
          </form>
        </asp:Panel>
      </div>
      <div class="col-md-4"></div>
    </div>
  </div>

  <!--Modal: modalPush-->
  <div class="modal fade" id="modalPush" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel"
    aria-hidden="true">
    <div class="modal-dialog modal-notify modal-info" role="document">
      <!--Content-->
      <div class="modal-content">
        <!--Header-->
        <%--<div class="modal-header d-flex justify-content-center">
        <p class="heading">Be always up to date</p>
      </div>--%>

        <!--Body-->
        <div class="modal-body">

          <div class="container">
            <div class="row">
              <div class="col-md-6 padding-logo">
                <div class="img-logo2"></div>
                <div><span class="tt-cb-blue-login2">debt</span><span class="tt-cb-deep-blue-login2">control</span></div>
                <div><span class="brand_slogan2">CENTRO DE SERVICIOS FINANCIEROS</span></div>
              </div>
            </div>
            <div class="row">
              <div class="col-md-12 padding-bottom-tt-info">
                <i class="fas fa-exclamation-circle fa-5x red-text float-left margin-info"></i>
                <div><span class="tt-info">INFORMACIÓN DE SEGURIDAD</span></div>
                <div><span class="sub-tt-info">EVITEMOS SER VÍCTIMAS DE CORREOS FRAUDULENTOS</span></div>
              </div>
            </div>
            <div class="row">
              <div class="=col-md-12">
                <div><span class="tt-parrafo">Tu seguridad es prioridad para nosotros.</span></div>
                <div><span class="tt-parrafo">En Debtcontrol nos tomamos muy en serio la seguridad de nuestros portales web.</span></div>
                <p>
                  Para ayudarnos a prevenir posibles filtraciones en los portales cada usuario tiene un papel relevante y activo en la seguridad de tu información. ¿Ha tenido problemas para iniciar sesión en DBT Always On? ¿Piensas que puedes estar siendo víctima de un fraude?
                </p>
                <div><span class="tt-parrafo">E-mails fraudulentos (o phishing)</span></div>
                <p>
                  Los estafadores, en ocasiones, envían correos electrónicos haciéndose pasar por usuarios de DBT Always On. Estos correos electrónicos contienen información falsa y pueden estar relacionados con una llamada de un presunto colaborador de Debtcontrol. También pueden pedir que hagas clic en un enlace para actualizar tu base de datos. Este tipo de correo falso se usa para captar la información de tu sesión en DBT Always On.
                </p>
                <div><span class="tt-parrafo">Si te encuentras en alguno de estos casos:</span></div>
                <div>
                  <ul>
                    <li>Has recibido un email que te lleva a una página falsa de DBT Always On.</li>
                    <li>Has recibido un email donde te pide que indiques tu información de inicio de sesión en la plataforma DBT Always On</li>
                    <li>Has recibido una llamada de un presunto colaborador de Debtcontrol y te que le proporciones tu información de casilla corporativa</li>
                  </ul>
                </div>
                <div><span class="tt-parrafo">Nosotros te recomendamos lo siguiente:</span></div>
                <div>
                  <ul>
                    <li>Verifica que la dirección de la casilla corporativa efectivamente corresponda a la utilizada por Debtcontrol, si existe una variación por favor:
              <ul>
                <li>No hagas clic en ningún enlace.</li>
                <li>No abras ficheros adjuntos.</li>
                <li>No respondas los emails.</li>
                <li>No llames a ninguno de los números de teléfono que te envíen.</li>
              </ul>
                    </li>
                    <li>Infórmanos de lo sucedido vía la persona responsable de su cuenta en Debtcontrol o llámenos al +56 2 2599 6100.</li>
                    <li>Reenvíanos el email.</li>
                    <li>Solicita el cambio de tu password DBT Always On</li>
                  </ul>
                </div>
                <div><span class="tt-parrafo">Recomendaciones de seguridad</span></div>
                <div><span class="tt-parrafo">¿Estoy en el sitio seguro de DBT Always On?</span></div>
                <p>
                  Asegúrate que en la barra de direcciones de tu navegador aparezca:
          <div>
            <ul>
              <li>Empiece por https://</li>
              <li>Muestre un candado.</li>
            </ul>
          </div>
                </p>
                <div>
                  <img src="../images/security.jpg" border="0" />
                </div>
              </div>
            </div>
          </div>

        </div>

        <!--Footer-->
        <%--<div class="modal-footer flex-center">
          <a href="https://mdbootstrap.com/products/jquery-ui-kit/" class="btn btn-info">Yes</a>
          <a type="button" class="btn btn-outline-info waves-effect" data-dismiss="modal">No</a>
        </div>--%>
      </div>
      <!--/.Content-->
    </div>
  </div>
  <!--Modal: modalPush-->
</body>
</html>
