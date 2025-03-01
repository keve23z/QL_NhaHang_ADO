var options = {
	chart: {
		width: 310,
		type: "donut",
	},
	labels: ["Open", "Progress", "Closed"],
	series: [20, 45, 65],
	legend: {
		position: "bottom",
	},
	dataLabels: {
		enabled: false,
	},
	stroke: {
		width: 0,
	},
	colors: ["#0068ff", "#b3d2ff", "#66a4ff"],
};
var chart = new ApexCharts(document.querySelector("#tickets"), options);
chart.render();
