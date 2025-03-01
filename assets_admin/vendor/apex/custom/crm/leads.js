var options = {
	chart: {
		height: 260,
		width: "100%",
		type: "bar",
		toolbar: {
			show: false,
		},
	},
	plotOptions: {
		bar: {
			horizontal: false,
			columnWidth: "60%",
			borderRadius: 4,
		},
	},
	dataLabels: {
		enabled: false,
	},
	stroke: {
		show: true,
		width: 0,
		colors: ["#0068ff", "#b3d2ff", "#66a4ff"],
	},
	series: [
		{
			name: "Leads",
			data: [2000, 4000, 8000, 12000, 9000, 5000, 3000],
		},
	],
	legend: {
		show: false,
	},
	xaxis: {
		categories: ["A1", "A2", "A3", "A4", "A5", "A6", "A7"],
	},
	yaxis: {
		show: false,
	},
	fill: {
		colors: ["#0068ff", "#b3d2ff", "#66a4ff"],
	},
	tooltip: {
		y: {
			formatter: function (val) {
				return +val;
			},
		},
	},
	grid: {
		show: false,
		xaxis: {
			lines: {
				show: true,
			},
		},
		yaxis: {
			lines: {
				show: false,
			},
		},
	},
	colors: ["#ffffff"],
};
var chart = new ApexCharts(document.querySelector("#leads"), options);
chart.render();
