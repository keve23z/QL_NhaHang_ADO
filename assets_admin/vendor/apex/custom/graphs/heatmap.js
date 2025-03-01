options = {
	series: [
		{
			name: "USA",
			data: [
				{
					x: "Q1",
					y: 33,
				},
				{
					x: "Q2",
					y: 43,
				},
				{
					x: "Q3",
					y: 56,
				},
				{
					x: "Q4",
					y: 69,
				},
			],
		},
		{
			name: "India",
			data: [
				{
					x: "Q1",
					y: 55,
				},
				{
					x: "Q2",
					y: 65,
				},
				{
					x: "Q3",
					y: 43,
				},
				{
					x: "Q4",
					y: 67,
				},
			],
		},
		{
			name: "Brazil",
			data: [
				{
					x: "Q1",
					y: 31,
				},
				{
					x: "Q2",
					y: 39,
				},
				{
					x: "Q3",
					y: 21,
				},
				{
					x: "Q4",
					y: 30,
				},
			],
		},
		{
			name: "Indonesia",
			data: [
				{
					x: "Q1",
					y: 43,
				},
				{
					x: "Q2",
					y: 55,
				},
				{
					x: "Q3",
					y: 38,
				},
				{
					x: "Q4",
					y: 49,
				},
			],
		},
	],
	legend: {
		show: false,
	},
	chart: {
		height: 300,
		type: "heatmap",
		toolbar: {
			show: false,
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
	tooltip: {
		y: {
			formatter: function (val) {
				return "$" + val + " Million";
			},
		},
	},
};

var chart = new ApexCharts(document.querySelector("#heatmap"), options);
chart.render();
