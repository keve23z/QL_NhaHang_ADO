var options = {
	chart: {
		height: 350,
		type: "area",
		toolbar: {
			show: false,
		},
		dropShadow: {
			enabled: true,
			opacity: 0.1,
			blur: 5,
			left: -10,
			top: 10,
		},
	},
	dataLabels: {
		enabled: false,
	},
	stroke: {
		curve: "straight",
		width: 3,
	},
	series: [
		{
			name: "New",
			data: [300, 400, 600, 500, 700, 500, 600],
		},
		{
			name: "Expired",
			data: [100, 200, 400, 300, 500, 400, 300],
		},
	],
	grid: {
		borderColor: "#dddddd",
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
			right: 30,
			bottom: 0,
			left: 30,
		},
	},
	xaxis: {
		type: "day",
		categories: ["New", "Mid", "Long", "Short", "Low", "High", "Best"],
	},
	colors: ["#0068ff", "#b3d2ff", "#66a4ff"],
	yaxis: {
		show: false,
	},
	markers: {
		size: 0,
		opacity: 0.2,
		colors: ["#0068ff", "#b3d2ff", "#66a4ff"],
		strokeColor: "#fff",
		strokeWidth: 2,
		hover: {
			size: 7,
		},
	},
	tooltip: {
		x: {
			format: "dd/MM/yy",
		},
	},
};

var chart = new ApexCharts(document.querySelector("#type"), options);

chart.render();
