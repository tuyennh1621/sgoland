var ctx = document.getElementById("Chart2")


var _yValues = [];
var _xValues = [];
_xValues = ctx.dataset.key.split(',');
_yValues1 = ctx.dataset.value.split(',');
_yValues2 = ctx.dataset.value1.split(',');
_yValues3 = ctx.dataset.value2.split(',');
new Chart("Chart2", {
    type: "line",
    data: {
        labels: _xValues,
        datasets: [{
            fill: false,
            label: 'Tạo đơn',
            lineTension: 0,
            backgroundColor: "#9ad0f5",
            borderColor: "#67b8f0",
            data: _yValues1
        }, {
            label: 'Chờ thanh toán',
            fill: false,
            lineTension: 0,
            backgroundColor: "#ffb0c1",
            borderColor: "#ff88a2",
            data: _yValues2
        }, {
            label: 'Hoàn thành',
            fill: false,
            lineTension: 0,
            backgroundColor: "#ffcf9f",
            borderColor: "#ffb76f",
            data: _yValues3
        }]
    },
    options: {
        legend: { display: false },
        scales: {
            yAxes: [{ ticks: { min: 4, max: 16 } }],
        }
    }
});