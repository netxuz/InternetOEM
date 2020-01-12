<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="prueba.aspx.cs" Inherits="ICommunity.Dashboard.prueba" %>

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
  <!-- JQuery -->
  <script type="text/javascript" src="js/jquery-3.4.1.min.js"></script>
  <!-- Bootstrap tooltips -->
  <script type="text/javascript" src="js/popper.min.js"></script>
  <!-- Bootstrap core JavaScript -->
  <script type="text/javascript" src="js/bootstrap.min.js"></script>
  <!-- MDB core JavaScript -->
  <script type="text/javascript" src="js/mdb.min.js"></script>
  <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
  <link href='http://fonts.googleapis.com/css?family=Lato:100,400,700,900' rel='stylesheet' type='text/css' />
  <script src="https://www.gstatic.com/charts/loader.js"></script>
  <style>
    .prueba {
      font-family: 'Lato', sans-serif;
      font-size: 1rem;
      font-weight: 100;
    }

    .img-charts {
      /*position: relative;*/
      border: 1px solid #000;
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

    #labelOverlay p {
        line-height: 0.3;
        padding: 0;
        margin: 8px;
      }

        #labelOverlay p.used-size {
          line-height: 0.5;
          font-size: 20pt;
          color: #8e8e8e;
        }

        #labelOverlay p.total-size {
          line-height: 0.5;
          font-size: 12pt;
          color: #cdcdcd;
        }

    #lineChart1 {
      width: 500px;
      height: 400px;
    }

    #pieChart {
      width: 500px;
      height: 400px;
    }

    #horizontalBar {
      width: 500px;
      height: 400px;
    }

    #lineChart2 {
      width: 500px;
      height: 400px;
    }


    #legend_div {
      color: #999;
      font-family: Roboto;
      position: absolute;
      right: 0px;
      text-align: right;
      top: 0px;
      width: 60%;
      z-index: 1000;
    }

    .legend-marker {
      display: inline-block;
      padding: 6px 6px 6px 6px;
    }

    .legend-marker-color {
      border-radius: 25%;
      display: inline-block;
      height: 12px;
      width: 12px;
    }

    .index-creb-blanco {
      line-height: .9 !important;
      width: 100%;
    }

    .index-creb-rojo {
      background-color: #D0021B;
      line-height: .9 !important;
      border-radius: 10px 0px 0px 10px;
      width: 100%;
    }

    .index-creb-naranjo {
      background-color: #F57F23;
      line-height: .9 !important;
      width: 100%;
    }

    .index-creb-amarillo {
      background-color: #F5D423;
      line-height: .9 !important;
      width: 100%;
    }

    .index-creb-verdeclaro {
      background-color: #7ED321;
      line-height: .9 !important;
      width: 100%;
    }

    .index-creb-verdeoscuro {
      background-color: #4EA528;
      line-height: .9 !important;
      border-radius: 0px 10px 10px 0px;
      width: 100%;
    }

    #myCanvas {
      position: absolute;
      top: 0;
      bottom: 0;
      right: 0;
      left: 0;
      margin: auto;
    }
  </style>
</head>
<body>
  <form id="form1" runat="server">
    <div class="container">
      <div class="row">
        <div class="col-md-12">
          <div class="input-prepend">
            <span class="add-on"><i class="icon-envelope"></i></span>
            <input class="span2" type="text" placeholder="Email address">
          </div>
        </div>
      </div>
      <div class="row">
        <div class="col-md-12">
          <span class="prueba">esta es una prueba</span>
        </div>
      </div>
      <div class="row">
        <div class="col-md-12">
          <div class="row">
            <div class="col-md-6">
              <div class="row">
                <div class="col-md-6">
                </div>
                <div class="col-md-6 img-charts">
                  <div id="donut_single"></div>
                  <div id="labelOverlay">
                    <p class="used-size">41<span>GB</span></p>
                    <p class="total-size">of 50GB</p>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="row">
        <div class="col-md-12">
          <div id="lineChart1"></div>
        </div>
      </div>
      <div class="row">
        <div class="col-md-12">
          <div id="pieChart"></div>
        </div>
      </div>
      <div class="row">
        <div class="col-md-12">
          <div id="horizontalBar"></div>
        </div>
      </div>
      <div class="row">
        <div class="col-md-12">
          <div id="legend_div"></div>
          <div id="lineChart2"></div>
        </div>
      </div>

      <div class="row">
        <div class="col-md-4">
          <div class="row" style="position: relative;">
            <div class="col-md-1 index-creb-blanco"></div>
            <div class="col-md-2 index-creb-rojo">&nbsp;</div>
            <div class="col-md-2 index-creb-naranjo">&nbsp;</div>
            <div class="col-md-2 index-creb-amarillo">&nbsp;</div>
            <div class="col-md-2 index-creb-verdeclaro">
              <canvas id="myCanvas" width="50" height="50"></canvas>
            </div>
            <div class="col-md-2 index-creb-verdeoscuro">&nbsp;</div>
            <div class="col-md-1 index-creb-blanco"></div>
          </div>
          <div class="row">
            <div class="col-md-2 text-center"><span class="lb-dat-overview">0</span></div>
            <div class="col-md-2 text-center"><span class="lb-dat-overview">20</span></div>
            <div class="col-md-2 text-center"><span class="lb-dat-overview">40</span></div>
            <div class="col-md-2 text-center"><span class="lb-dat-overview">60</span></div>
            <div class="col-md-2 text-center"><span class="lb-dat-overview">80</span></div>
            <div class="col-md-2 text-center"><span class="lb-dat-overview">100</span></div>
          </div>
        </div>

      </div>
      <div class="row">
        <div class="col-md-12">
          <div id="horizontalBar2"></div>
        </div>
      </div>
      <div class="row">
        <div class="col-md-12">
          <div id="chartPasDue"></div>
        </div>
      </div>
    </div>
    <script type="text/javascript">
      google.charts.load('current', { 'packages': ['corechart', 'bar', 'line'], });
    </script>
    <script type="text/javascript">

      //***********************************************************************************************************************************************************************
      //google.charts.setOnLoadCallback(drawChart);
      //function drawChart() {
      //  var data = google.visualization.arrayToDataTable([['Porcentaje', 'Dato'], ['Estimado', 0.00], ['Real', 0.00], ]);
      //  var options = { pieHole: 0.9, pieSliceBorderColor: 'none', colors: ['#1578FF', '#FFDFAB'], legend: 'none', chartArea: { left: 10, right: 10, bottom: 10, top: 10 } };
      //  var chart = new google.visualization.PieChart(document.getElementById('donut_single'));
      //  chart.draw(data, options);
      //};
      //google.charts.setOnLoadCallback(drawlineChart2);
      //function drawlineChart2() {
      //  var data = google.visualization.arrayToDataTable([
      //  ['PERIODO', 'Facturado', { type: 'string', role: 'annotation' }, 'Estimado', { type: 'string', role: 'annotation' }, 'Real', { type: 'string', role: 'annotation' }],
      // ['Anterior', 243706100, '243M', 31827740, '31M', 7765071.00, '7M'],
      // ['Actual', 0, '0M', 0, '0M', 0, '0M']
      //  ]);

      //  var options = { annotations: { alwaysOutside: true, textStyle: { fontSize: 12, auraColor: 'none', color: '#fff' }, boxStyle: { stroke: '#79B6CD', strokeWidth: 1, rx: 10, ry: 10, gradient: { color1: '#79B6CD', color2: '#79B6CD', x1: '0%', y1: '0%', x2: '100%', y2: '100%' } } }, legend: { position: 'top', textStyle: { color: '#c5c5c5', fontName: 'Lato', fontSize: 13, bold: true, } }, vAxis: { format: 'short', textStyle: { color: '#c5c5c5', fontName: 'Lato', fontSize: 12, bold: true } }, hAxis: { textStyle: { color: '#c5c5c5', fontName: 'Lato', fontSize: 13, bold: true } }, colors: ['#BAE1EF', '#A5E6FE', '#6498FF'] };

      //  var chart = new google.visualization.ColumnChart(document.getElementById('lineChart2')); chart.draw(data, options);
      //}
      //google.charts.setOnLoadCallback(drawChartDso);
      //function drawChartDso() {
      //  var data = google.visualization.arrayToDataTable([
      //    ['Perido', 'Días'],
      //    ['2019- 2', 7.39],
      //    ['2019- 5', 93.12],
      //    ['2019- 6', 95.17],
      //    ['2019- 7', 125.14],
      //    ['2019- 8', 150.69],
      //    ['2019- 9', 99.73]
      //  ]);
      //  var options = { legend: { position: 'top', textStyle: { color: '#c5c5c5', fontName: 'Lato', fontSize: 13, bold: true, } }, vAxis: { format: 'short', textStyle: { color: '#c5c5c5', fontName: 'Lato', fontSize: 12, bold: true } }, hAxis: { textStyle: { color: '#629DCC', fontName: 'Lato', fontSize: 13, bold: true } }, colors: ['#F5A624'], pointSize: 10, backgroundColor: '#fff' };
      //  var chart = new google.visualization.AreaChart(document.getElementById('lineChart1'));
      //  chart.draw(data, options);
      //}
      //google.charts.setOnLoadCallback(drawChartProvison);
      //function drawChartProvison() {
      //  var data = google.visualization.arrayToDataTable([['TIPO', 'MONTO'], ['Impacto Provisión', 7719942.50], ['Provisión Acumulada', 480800000.00]]);
      //  var options = { pieHole: 0.5, pieSliceText: 'none', pieSliceTextStyle: 'none', pieSliceBorderColor: 'none', legend: 'none', colors: ['#D0021B', '#7ED321'], chartArea: { left: 10, right: 10, bottom: 10, top: 10 } };
      //  var chart = new google.visualization.PieChart(document.getElementById('pieChart'));
      //  chart.draw(data, options);

      //}
      //google.charts.setOnLoadCallback(drawRightY);
      //function drawRightY() {
      //  var data = google.visualization.arrayToDataTable([
      //    ['Meses', 'Días', { role: 'annotation' }],
      //    ['2019- 9', -10.59, '-10.59'],
      //    ['2019- 8', 9.70, '9.70'],
      //    ['2019- 7', 1.00, '1.00'],
      //    ['2019- 6', 1.14, '1.14'],
      //    ['2019- 5', -16.00, '-16.00'],
      //    ['2019- 2', 15.38, '15.38']
      //  ]);
      //  var materialOptions = { annotations: { alwaysOutside: true, textStyle: { fontSize: 12, auraColor: 'none', color: '#fff' }, boxStyle: { stroke: '#79B6CD', strokeWidth: 1, rx: 10, ry: 10, gradient: { color1: '#79B6CD', color2: '#79B6CD', x1: '0%', y1: '0%', x2: '100%', y2: '100%' } } }, legend: { position: 'top', textStyle: { color: '#c5c5c5', fontName: 'Lato', fontSize: 13, bold: true, } }, vAxis: { format: 'short', textStyle: { color: '#c5c5c5', fontName: 'Lato', fontSize: 12, bold: true } }, hAxis: { textStyle: { color: '#91BBDB', fontName: 'Lato', fontSize: 13, bold: true } }, colors: ['#A5E6FE', '#A5E6FE', '#A5E6FE', '#A5E6FE', '#A5E6FE'], bars: 'horizontal', axes: { y: { 0: { side: 'right' } } } };

      //  var materialChart = new google.visualization.BarChart(document.getElementById('horizontalBar'));
      //  materialChart.draw(data, materialOptions);
      //}

      //google.charts.setOnLoadCallback(drawChartProvison);
      //function drawChartProvison() {
      //  var data = google.visualization.arrayToDataTable([['TIPO', 'MONTO'],
      //    ['Impacto Provisión', 7719942.50],
      //    ['Provisión Acumulada', 480800000.00]
      //  ]);
      //  var options = { pieHole: 1, pieSliceTextStyle: { color: '#ffffff', fontName: 'Lato', fontSize: 40 }, pieSliceBorderColor: 'none', legend: 'none', colors: ['#7ED321', '#7ED321'], chartArea: { left: 0, right: 0, bottom: 10, top: 10 } };
      //  var chart = new google.visualization.PieChart(document.getElementById('pieChart'));
      //  chart.draw(data, options);

      //}

      //(function () {
      //  var requestAnimationFrame = window.requestAnimationFrame || window.mozRequestAnimationFrame || window.webkitRequestAnimationFrame || window.msRequestAnimationFrame;
      //  window.requestAnimationFrame = requestAnimationFrame;
      //}());

      //var canvas = document.getElementById('myCanvas');
      //var context = canvas.getContext('2d');
      //var circles = [];

      //createCircle(0, 0, '53%', function () {
      //  //createCircle(270, 100, '460', function () {
      //  //  createCircle(440, 100, '20', function () {
      //  //    createCircle(610, 100, '15', null);
      //  //  });
      //  //});
      //});



      //function createCircle(x, y, text, callback) {
      //  var radius = 75;
      //  var endPercent = 101;
      //  var curPerc = 0;
      //  var counterClockwise = false;
      //  var circ = Math.PI * 2;
      //  var quart = Math.PI / 2;

      //  context.lineWidth = 10;
      //  context.strokeStyle = '#ad2323';
      //  context.shadowOffsetX = 0;
      //  context.shadowOffsetY = 0;

      //  function doText(context, x, y, text) {
      //    context.lineWidth = 1;
      //    context.fillStyle = "#ad2323";
      //    context.lineStyle = "#ad2323";
      //    context.font = "28px Lato";
      //    context.fillText(text, x - 15, y + 5);
      //  }

      //  function animate(current) {
      //    context.lineWidth = 10;
      //    context.clearRect(0, 0, canvas.width, canvas.height);
      //    context.beginPath();
      //    context.arc(x, y, radius, -(quart), ((circ) * current) - quart, false);
      //    context.stroke();
      //    curPerc++;
      //    if (circles.length) {
      //      for (var i = 0; i < circles.length; i++) {
      //        context.lineWidth = 10;
      //        context.beginPath();
      //        context.arc(circles[i].x, circles[i].y, radius, -(quart), ((circ) * circles[i].curr) - quart, false);
      //        context.stroke();
      //        doText(context, circles[i].x, circles[i].y, circles[i].text);
      //      }
      //    }
      //    if (curPerc < endPercent) {
      //      requestAnimationFrame(function () {
      //        animate(curPerc / 100)
      //      });
      //    } else {
      //      var circle = {
      //        x: x,
      //        y: y,
      //        curr: current,
      //        text: text
      //      };
      //      circles.push(circle);
      //      doText(context, x, y, text);
      //      if (callback) callback.call();
      //    }
      //  }
      //  animate();
      //}


      google.charts.setOnLoadCallback(drawChart);
      function drawChart() {
        var data = google.visualization.arrayToDataTable([
          ['Porcentaje', 'Dato'],
          ['Estimado', 0.00],
          ['Real', 0],
        ]); var options = { pieHole: 0.9, pieSliceText: 'none', pieSliceTextStyle: 'none', pieSliceBorderColor: 'none', colors: ['#1578FF', '#FFDFAB'], legend: 'none', chartArea: { left: 10, right: 10, bottom: 10, top: 10 } };
        var chart = new google.visualization.PieChart(document.getElementById('donut_single'));
        chart.draw(data, options);
      };
      google.charts.setOnLoadCallback(drawlineChart2);
      function drawlineChart2() {
        var data = google.visualization.arrayToDataTable([
          ['PERIODO', 'Facturado', { type: 'string', role: 'annotation' }, 'Estimado', { type: 'string', role: 'annotation' }, 'Real', { type: 'string', role: 'annotation' }],
          ['Anterior', -2546, '0M', 0, '0M', 0, '0M'],
          ['Actual', 5536844, '6M', 0, '0M', 0, '0M']
        ]);
        var options = { annotations: { alwaysOutside: true, textStyle: { fontSize: 12, auraColor: 'none', color: '#fff' }, boxStyle: { stroke: '#79B6CD', strokeWidth: 1, rx: 10, ry: 10, gradient: { color1: '#79B6CD', color2: '#79B6CD', x1: '0%', y1: '0%', x2: '100%', y2: '100%' } } }, legend: { position: 'bottom', textStyle: { color: '#c5c5c5', fontName: 'Lato', fontSize: 13, bold: true, } }, vAxis: { format: 'short', textStyle: { color: '#c5c5c5', fontName: 'Lato', fontSize: 12, bold: true } }, hAxis: { textStyle: { color: '#c5c5c5', fontName: 'Lato', fontSize: 13, bold: true } }, colors: ['#BAE1EF', '#A5E6FE', '#6498FF'] };
        var chart = new google.visualization.ColumnChart(document.getElementById('lineChart2'));
        chart.draw(data, options);
      }
      var canvas = document.getElementById('myCanvas');
      var context = canvas.getContext('2d');
      var centerX = canvas.width / 2;
      var centerY = canvas.height / 2;
      var radius = 20;
      context.beginPath();
      context.arc(centerX, centerY, radius, 0, 2 * Math.PI, false);
      context.fillStyle = '#F57F23';
      context.fill();
      context.lineWidth = 5;
      context.strokeStyle = '#fff';
      context.stroke();
      context.lineWidth = 1;
      context.fillStyle = "#fff";
      context.lineStyle = "#fff";
      context.font = "12px Lato";
      context.fillText('30%', 15, 30);

      google.charts.setOnLoadCallback(drawChartPasDue);
      function drawChartPasDue() {
        var data = google.visualization.arrayToDataTable([
          ['PERIODO', 'Total', 'Crítico'],
          ['nov 2019', 1766350.15, 326683.31],
          ['nov 2018', 0, 0]
        ]);
        var options = { legend: { position: 'bottom', textStyle: { color: '#c5c5c5', fontName: 'Lato', fontSize: 13, bold: true, } }, vAxis: { format: 'short', textStyle: { color: '#639ECC', fontName: 'Lato', fontSize: 14, bold: true } }, hAxis: { textStyle: { color: '#ACACAC', fontName: 'Lato', fontSize: 10, bold: true } }, colors: ['#7ED321', '#6498FF'], bars: 'horizontal' };
        var chart = new google.charts.Bar(document.getElementById('chartPasDue'));
        chart.draw(data, google.charts.Bar.convertOptions(options));
      }

    </script>


  </form>

</body>
</html>
