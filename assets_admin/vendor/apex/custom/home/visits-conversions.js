var options = {
	chart: {
		height: 300,
		type: "bar",
		toolbar: {
			show: false,
		},
	},
	plotOptions: {
		bar: {
			columnWidth: "30%",
			borderRadius: 3,
			distributed: true,
			dataLabels: {
				position: "top",
			},
		},
	},
	series: [
		{
			name: "Visitors",
			data: [52, 73, 34, 66, 82, 49, 38, 59, 44, 86, 30, 60],
		},
	],
	legend: {
		show: false,
	},
	xaxis: {
		categories: [
			"Jan",
			"Feb",
			"Mar",
			"Apr",
			"May",
			"Jun",
			"Jul",
			"Aug",
			"Sep",
			"Oct",
			"Nov",
			"Dec",
		],
		axisBorder: {
			show: false,
		},
		yaxis: {
			show: false,
		},

		tooltip: {
			enabled: true,
		},
		labels: {
			show: true,
		},
	},
	grid: {
		borderColor: "#cccccc",
		strokeDashArray: 5,
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
		padding: {
			top: 0,
			right: 0,
			bottom: 0,
		},
	},
	tooltip: {
		y: {
			formatter: function (val) {
				return val + " million";
			},
		},
	},
	colors: [
		"#0068ff",
		"#164de6",
		"#2545b0",
		"#113aae",
		"#275cf3",
		"#0049b3",
		"#cce1ff",
		"#1a77ff",
		"#99c3ff",
		"#0068ff",
		"#80b4ff",
		"#005ee6",
	],
};
var chart = new ApexCharts(
	document.querySelector("#visits-conversions"),
	options
);
chart.render();
