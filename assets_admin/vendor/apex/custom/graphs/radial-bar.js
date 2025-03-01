var options = {
	series: [40, 50, 60, 70, 80],
	chart: {
		height: 240,
		type: "radialBar",
	},
	plotOptions: {
		radialBar: {
			dataLabels: {
				name: {
					fontSize: "22px",
				},
				value: {
					fontSize: "16px",
				},
				total: {
					show: true,
					label: "Total",
					formatter: function (w) {
						// By default this function returns the average of all series. The below is just an example to show the use of custom formatter function
						return 249;
					},
				},
			},
		},
	},
	labels: ["Samsung", "Apple", "Nokia", "Motorola", "Huawei"],
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

var chart = new ApexCharts(document.querySelector("#radial"), options);
chart.render();
