// Sparkline 1
var options1 = {
	series: [
		{
			name: "Clicks",
			data: [1, 2, 3, 2, 1],
		},
		{
			name: "Clicks",
			data: [1, 2, 3, 2, 1],
		},
	],
		chart: {
		type: "bar",
		height: 100,
		stacked: true,
		width: "100%",
		sparkline: {
			enabled: true,
		},
	},
	colors: ["#cccccc", "#ffffff"],
	stroke: {
		width: 0,
	},
	tooltip: {
		fixed: {
			enabled: false,
		},
		x: {
			show: false,
		},
		marker: {
			show: false,
		},
	},
	xaxis: {
		type: "day",
		categories: ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday"],
	},
	tooltip: {
		y: {
			formatter: function (val) {
				return val + "K";
			},
		},
	},
};
var chart1 = new ApexCharts(document.querySelector("#option1"), options1);
chart1.render();



// Sparkline 2
var options2 = {
	series: [
		{
			name: "Orders",
			data: [1, 2, 3, 2, 1],
		},
		{
			name: "Orders",
			data: [1, 2, 3, 2, 1],
		},
	],
		chart: {
		type: "bar",
		height: 100,
		stacked: true,
		width: "100%",
		sparkline: {
			enabled: true,
		},
	},
	colors: ["#cccccc", "#ffffff"],
	stroke: {
		width: 0,
	},
	tooltip: {
		fixed: {
			enabled: false,
		},
		x: {
			show: false,
		},
		marker: {
			show: false,
		},
	},
	xaxis: {
		type: "day",
		categories: ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday"],
	},
	tooltip: {
		y: {
			formatter: function (val) {
				return val + "K";
			},
		},
	},
};
var chart2 = new ApexCharts(document.querySelector("#option2"), options2);
chart2.render();




// Sparkline 3
var options3 = {
	series: [
		{
			name: "Income",
			data: [1, 2, 3, 2, 1],
		},
		{
			name: "Income",
			data: [1, 2, 3, 2, 1],
		},
	],
		chart: {
		type: "bar",
		height: 100,
		stacked: true,
		width: "100%",
		sparkline: {
			enabled: true,
		},
	},
	colors: ["#cccccc", "#ffffff"],
	stroke: {
		width: 0,
	},
	tooltip: {
		fixed: {
			enabled: false,
		},
		x: {
			show: false,
		},
		marker: {
			show: false,
		},
	},
	xaxis: {
		type: "day",
		categories: ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday"],
	},
	tooltip: {
		y: {
			formatter: function (val) {
				return val + "K";
			},
		},
	},
};
var chart3 = new ApexCharts(document.querySelector("#option3"), options3);
chart3.render();