var options = {
	chart: {
		height: 260,
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
			name: "Claimed",
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
			right: 10,
			bottom: 0,
			left: 10,
		},
	},
	xaxis: {
		type: "day",
		categories: ["Sun", "Mon", "Tue", "Wedn", "Thu", "Fri", "Sat"],
	},
	colors: ["#0068ff", "#eeeeee", "#66a4ff"],
	yaxis: {
		show: false,
	},
	markers: {
		size: 0,
		opacity: 0.2,
		colors: ["#0068ff", "#eeeeee", "#66a4ff"],
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

var chart = new ApexCharts(document.querySelector("#deals"), options);

chart.render();
