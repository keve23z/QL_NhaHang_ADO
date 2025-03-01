var options = {
	chart: {
		height: 205,
		type: "bar",
		toolbar: {
			show: false,
		},
	},
	plotOptions: {
		bar: {
			columnWidth: "60%",
			borderRadius: 8,
			distributed: true,
			dataLabels: {
				position: "top",
			},
		},
	},
	series: [
		{
			name: "Income",
			data: [20, 30, 40, 50, 60, 70],
		},
	],
	legend: {
		show: false,
	},
	xaxis: {
		categories: ["Jan", "Feb", "Mar", "Apr", "May", "June"],
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
			rotate: -45,
			rotateAlways: true,
		},
	},
	grid: {
		borderColor: "#b7c6d8",
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
var chart = new ApexCharts(document.querySelector("#income"), options);
chart.render();
