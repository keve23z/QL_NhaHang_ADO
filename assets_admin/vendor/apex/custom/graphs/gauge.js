var options = {
	series: [75],
	chart: {
		height: 240,
		type: "radialBar",
		offsetY: -10,
	},
	plotOptions: {
		radialBar: {
			startAngle: -135,
			endAngle: 135,
			dataLabels: {
				name: {
					fontSize: "16px",
					color: undefined,
					offsetY: 120,
				},
				value: {
					offsetY: 76,
					fontSize: "21px",
					color: undefined,
					formatter: function (val) {
						return val + "%";
					},
				},
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
	stroke: {
		dashArray: 4,
	},
	labels: ["Sales Ratio"],
};

var chart = new ApexCharts(document.querySelector("#gauge"), options);
chart.render();
