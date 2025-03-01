var options = {
	series: [
		{
			data: [
				{
					x: "Samsung",
					y: [2800, 4500],
				},
				{
					x: "Google",
					y: [3200, 4100],
				},
				{
					x: "Apple",
					y: [2950, 7800],
				},
				{
					x: "Lenovo",
					y: [3000, 4600],
				},
				{
					x: "Chrome",
					y: [3500, 4100],
				},
				{
					x: "Facebook",
					y: [4500, 6500],
				},
				{
					x: "Twitter",
					y: [4100, 5600],
				},
			],
		},
	],
	chart: {
		height: 390,
		type: "rangeBar",
		toolbar: {
			show: false,
		},
	},
	colors: ["#0068ff", "#000000"],
	plotOptions: {
		bar: {
			horizontal: true,
			isDumbbell: true,
			dumbbellColors: [["#0068ff", "#000000"]],
			barHeight: "3",
		},
	},
	legend: {
		show: true,
		showForSingleSeries: true,
		position: "top",
		horizontalAlign: "left",
		customLegendItems: ["In progress", "Completed"],
	},
	fill: {
		type: "gradient",
		gradient: {
			gradientToColors: ["#000000"],
			inverseColors: false,
			stops: [0, 100],
		},
	},
	grid: {
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
};

var chart = new ApexCharts(document.querySelector("#tasks"), options);
chart.render();
