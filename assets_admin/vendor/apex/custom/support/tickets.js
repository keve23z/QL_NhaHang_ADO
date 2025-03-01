var options = {
	series: [
		{
			name: "Tickets",
			data: [200, 330, 548, 740, 880, 1100],
		},
	],
	chart: {
		type: "bar",
		height: 300,
		toolbar: {
			show: false,
		},
	},
	dataLabels: {
		enabled: false,
	},
	plotOptions: {
		bar: {
			borderRadius: 0,
			horizontal: true,
			distributed: true,
			barHeight: "80%",
			isFunnel: true,
		},
	},
	colors: [
		"#0049b3",
		"#0053cc",
		"#005ee6",
		"#0068ff",
		"#1a77ff",
		"#3386ff",
		"#4d95ff",
		"#66a4ff",
	],
	dataLabels: {
		enabled: true,
		formatter: function (val, opt) {
			return opt.w.globals.labels[opt.dataPointIndex];
		},
		dropShadow: {
			enabled: true,
		},
	},
	xaxis: {
		categories: ["Closed", "Hold", "Resolved", "In Progress", "Open", "Total"],
	},
	legend: {
		show: true,
	},
};

var chart = new ApexCharts(document.querySelector("#tickets"), options);
chart.render();
