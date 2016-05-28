//----------------------------------------------------------------------Users Registered to classes table code-----------------------------------------------
$(document).ready(function () {

    var request = {
    };

    var dataString = JSON.stringify(request);
    $.ajax({
        url: 'FormaFitWebService.asmx/getRegisteredUsersFromDB',
        type: 'POST',
        contentType: 'application/json; charset = utf-8',
        dataType: 'json',
        data: dataString,
        success:
            function successCBRegisteredUsersFromDB(doc) {
                doc = $.parseJSON(doc.d);
                $('#RegisteredUsersToClassesTable').bootstrapTable({
                    data: doc,
                    pagination: true,
                    pageSize: 10,
                    pageList: [10, 25, 50, 100, 'All'],
                    pageNumber: 1
                });
            },
    });

});


//-----------------------------------------------------------------------Charts code-----------------------------------------------

// note, each data item has "bullet" field.
var columnChartData = [
    {
        "name": "יוגה",
        "points": 7,
        "color": "#7F8DA9",
        "bullet": "images/yoga.jpg"
    },
    {
        "name": "ספינינג",
        "points": 10,
        "color": "#FEC514",
        "bullet": "images/spining.png"
    },
    {
        "name": "פילאטיס",
        "points": 14,
        "color": "#DB4C3C",
        "bullet": "images/pilatis.jpg"
    },
    {
        "name": "זומבה",
        "points": 8,
        "color": "#31D578",
        "bullet": "images/zumba.jpg"
    }
];

AmCharts.ready(function () {

    AmCharts.rtl = true
    var chart = new AmCharts.AmSerialChart();
    chart.dataProvider = columnChartData;
    chart.categoryField = "name";
    chart.startDuration = 1;
    chart.autoMargins = false;
    chart.marginRight = 0;
    chart.marginLeft = 0;
    chart.marginBottom = 0;
    chart.marginTop = 0;
    chart.fontSize = 15;
    chart.addTitle("חוגים נבחרים לחודש מאי");
    var categoryAxis = chart.categoryAxis;
    categoryAxis.inside = true;
    categoryAxis.axisAlpha = 0;
    categoryAxis.gridAlpha = 0;
    categoryAxis.tickLength = 0;


    // value
    var valueAxis = new AmCharts.ValueAxis();
    valueAxis.minimum = 0;
    valueAxis.axisAlpha = 0;
    valueAxis.maximum = 20;
    valueAxis.dashLength = 4;
    chart.addValueAxis(valueAxis);

    // GRAPH
    var graph = new AmCharts.AmGraph();
    graph.valueField = "points";
    graph.customBulletField = "bullet"; // field of the bullet in data provider
    graph.bulletOffset = 16; // distance from the top of the column to the bullet
    graph.colorField = "color";
    graph.bulletSize = 34; // bullet image should be rectangle (width = height)
    graph.type = "column";
    graph.fillAlphas = 0.8;
    graph.cornerRadiusTop = 8;
    graph.lineAlpha = 0;
    graph.balloonText = "<span style='font-size:13px;'>[[category]]: <b>[[value]]</b></span>";
    chart.addGraph(graph);

    // WRITE
    chart.write("chartdiv");
});