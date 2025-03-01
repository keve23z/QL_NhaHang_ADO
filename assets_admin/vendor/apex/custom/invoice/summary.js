var options = {
	chart: {
		height: 300,
		type: "bar",
		stacked: true,
		toolbar: {
			show: false,
		},
	},
	plotOptions: {
		bar: {
			columnWidth: "30%",
			borderRadius: 3,
			// distributed: true,
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
		{
			name: "Test",
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
		"#3386ff",
		"#4d95ff",
		"#66a4ff",
		"#80b4ff",
		"#99c3ff",
		"#b3d2ff",
		"#cce1ff",
	],
};
var chart = new ApexCharts(document.querySelector("#invoices"), options);
chart.render();
