var options = {
	chart: {
		width: 300,
		type: "donut",
	},
	labels: ["Team A", "Team B", "Team C", "Team D", "Team E"],
	series: [20, 35, 45, 65, 40],
	legend: {
		position: "bottom",
	},
	dataLabels: {
		enabled: false,
	},
	stroke: {
		width: 0,
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
var chart = new ApexCharts(document.querySelector("#donut"), options);
chart.render();
