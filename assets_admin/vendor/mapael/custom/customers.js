$(function () {
	$(".customers").mapael({
		map: {
			name: "world_countries",
			defaultArea: {
				attrs: {
					stroke: "#fff",
					"stroke-width": 1,
				},
			},
		},
		legend: {
			area: {
				mode: "horizontal",
				title: "Customers",
				labelAttrs: {
					"font-size": 12,
				},
				marginLeft: 5,
				marginLeftLabel: 5,
				slices: [
					{
						max: 5000000,
						attrs: {
							fill: "#0068ff",
						},
						label: "< 5 millions",
					},
					{
						min: 5000000,
						max: 10000000,
						attrs: {
							fill: "#4d95ff",
						},
						label: "> 5 millions and < 10 millions",
					},
					{
						min: 10000000,
						max: 50000000,
						attrs: {
							fill: "#66a4ff",
						},
						label: "> 10 millions and < 50 millions",
					},
					{
						min: 50000000,
						attrs: {
							fill: "#0068ff",
						},
						label: "> 50 millions",
					},
				],
			},
			plot: {
				mode: "horizontal",
				title: "Cities Customers",
				labelAttrs: {
					"font-size": 12,
				},
				marginLeft: 5,
				marginLeftLabel: 5,
				slices: [
					{
						max: 500000,
						attrs: {
							fill: "#e1204d",
						},
						attrsHover: {
							transform: "s1.5",
							"stroke-width": 1,
						},
						label: "< 500 000",
						size: 10,
					},
					{
						min: 500000,
						max: 1000000,
						attrs: {
							fill: "#e1204d",
						},
						attrsHover: {
							transform: "s1.5",
							"stroke-width": 1,
						},
						label: "> 500 000 and 1 million",
						size: 20,
					},
					{
						min: 1000000,
						attrs: {
							fill: "#e1204d",
						},
						attrsHover: {
							transform: "s1.5",
							"stroke-width": 1,
						},
						label: "> 1 million",
						size: 30,
					},
				],
			},
		},
		plots: {
			paris: {
				latitude: 48.86,
				longitude: 2.3444,
				value: 500000000,
				tooltip: { content: "Paris<br />Customers: 500000000" },
			},
			newyork: {
				latitude: 40.667,
				longitude: -73.833,
				value: 200001,
				tooltip: { content: "New york<br />Customers: 200001" },
			},
			sydney: {
				latitude: -33.917,
				longitude: 151.167,
				value: 600000,
				tooltip: { content: "Sydney<br />Customers: 600000" },
			},
			brasilia: {
				latitude: -15.781682,
				longitude: -47.924195,
				value: 200000001,
				tooltip: { content: "Brasilia<br />Customers: 200000001" },
			},
			tokyo: {
				latitude: 35.687418,
				longitude: 139.692306,
				value: 200001,
				tooltip: { content: "Tokyo<br />Customers: 200001" },
			},
		},
		areas: {
			AF: {
				value: "35320445",
				attrs: {
					href: "#",
				},
				tooltip: {
					content:
						'<span style="font-weight:bold;">Afghanistan</span><br />Customers : 35320445',
				},
			},
			ZA: {
				value: "50586757",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">South Africa</span><br />Customers : 50586757',
				},
			},
			AL: {
				value: "3215988",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Albania</span><br />Customers : 3215988',
				},
			},
			DZ: {
				value: "35980193",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Algeria</span><br />Customers : 35980193',
				},
			},
			DE: {
				value: "81726000",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Germany</span><br />Customers : 81726000',
				},
			},
			AD: {
				value: "86165",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Andorra</span><br />Customers : 86165',
				},
			},
			AO: {
				value: "19618432",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Angola</span><br />Customers : 19618432',
				},
			},
			AG: {
				value: "89612",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Antigua And Barbuda</span><br />Customers : 89612',
				},
			},
			SA: {
				value: "28082541",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Saudi Arabia</span><br />Customers : 28082541',
				},
			},
			AR: {
				value: "40764561",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Argentina</span><br />Customers : 40764561',
				},
			},
			AM: {
				value: "3100236",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Armenia</span><br />Customers : 3100236',
				},
			},
			AU: {
				value: "22620600",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Australia</span><br />Customers : 22620600',
				},
			},
			AT: {
				value: "8419000",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Austria</span><br />Customers : 8419000',
				},
			},
			AZ: {
				value: "9168000",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Azerbaijan</span><br />Customers : 9168000',
				},
			},
			BS: {
				value: "347176",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Bahamas</span><br />Customers : 347176',
				},
			},
			BH: {
				value: "1323535",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Bahrain</span><br />Customers : 1323535',
				},
			},
			BD: {
				value: "150493658",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Bangladesh</span><br />Customers : 150493658',
				},
			},
			BB: {
				value: "273925",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Barbados</span><br />Customers : 273925',
				},
			},
			BE: {
				value: "11008000",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Belgium</span><br />Customers : 11008000',
				},
			},
			BZ: {
				value: "356600",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Belize</span><br />Customers : 356600',
				},
			},
			BJ: {
				value: "9099922",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Benin</span><br />Customers : 9099922',
				},
			},
			BT: {
				value: "738267",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Bhutan</span><br />Customers : 738267',
				},
			},
			BY: {
				value: "9473000",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Belarus</span><br />Customers : 9473000',
				},
			},
			MM: {
				value: "48336763",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Myanmar</span><br />Customers : 48336763',
				},
			},
			BO: {
				value: "10088108",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Bolivia, Plurinational State Of</span><br />Customers : 10088108',
				},
			},
			BA: {
				value: "3752228",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Bosnia And Herzegovina</span><br />Customers : 3752228',
				},
			},
			BW: {
				value: "2030738",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Botswana</span><br />Customers : 2030738',
				},
			},
			BR: {
				value: "196655014",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Brazil</span><br />Customers : 196655014',
				},
			},
			BN: {
				value: "405938",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Brunei Darussalam</span><br />Customers : 405938',
				},
			},
			BG: {
				value: "7476000",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Bulgaria</span><br />Customers : 7476000',
				},
			},
			BF: {
				value: "16967845",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Burkina Faso</span><br />Customers : 16967845',
				},
			},
			BI: {
				value: "8575172",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Burundi</span><br />Customers : 8575172',
				},
			},
			KH: {
				value: "14305183",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Cambodia</span><br />Customers : 14305183',
				},
			},
			CM: {
				value: "20030362",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Cameroon</span><br />Customers : 20030362',
				},
			},
			CA: {
				value: "34482779",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Canada</span><br />Customers : 34482779',
				},
			},
			CV: {
				value: "500585",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Cape Verde</span><br />Customers : 500585',
				},
			},
			CF: {
				value: "4486837",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Central African Republic</span><br />Customers : 4486837',
				},
			},
			CL: {
				value: "17269525",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Chile</span><br />Customers : 17269525',
				},
			},
			CN: {
				value: "1344130000",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">China</span><br />Customers : 1344130000',
				},
			},
			CY: {
				value: "1116564",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Cyprus</span><br />Customers : 1116564',
				},
			},
			CO: {
				value: "46927125",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Colombia</span><br />Customers : 46927125',
				},
			},
			KM: {
				value: "753943",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Comoros</span><br />Customers : 753943',
				},
			},
			CG: {
				value: "4139748",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Congo</span><br />Customers : 4139748',
				},
			},
			CD: {
				value: "67757577",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Congo, The Democratic Republic Of The</span><br />Customers : 67757577',
				},
			},
			KP: {
				value: "24451285",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Korea, Democratic People\'s Republic Of</span><br />Customers : 24451285',
				},
			},
			KR: {
				value: "49779000",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Korea, Republic Of</span><br />Customers : 49779000',
				},
			},
			CR: {
				value: "4726575",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Costa Rica</span><br />Customers : 4726575',
				},
			},
			CI: {
				value: "20152894",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">C\u00d4te D\'ivoire</span><br />Customers : 20152894',
				},
			},
			HR: {
				value: "4407000",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Croatia</span><br />Customers : 4407000',
				},
			},
			CU: {
				value: "11253665",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Cuba</span><br />Customers : 11253665',
				},
			},
			DK: {
				value: "5574000",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Denmark</span><br />Customers : 5574000',
				},
			},
			DJ: {
				value: "905564",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Djibouti</span><br />Customers : 905564',
				},
			},
			DM: {
				value: "67675",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Dominica</span><br />Customers : 67675',
				},
			},
			EG: {
				value: "82536770",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Egypt</span><br />Customers : 82536770',
				},
			},
			AE: {
				value: "7890924",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">United Arab Emirates</span><br />Customers : 7890924',
				},
			},
			EC: {
				value: "14666055",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Ecuador</span><br />Customers : 14666055',
				},
			},
			ER: {
				value: "5415280",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Eritrea</span><br />Customers : 5415280',
				},
			},
			ES: {
				value: "46235000",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Spain</span><br />Customers : 46235000',
				},
			},
			EE: {
				value: "1340000",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Estonia</span><br />Customers : 1340000',
				},
			},
			US: {
				value: "311591917",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">United States</span><br />Customers : 311591917',
				},
			},
			ET: {
				value: "84734262",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Ethiopia</span><br />Customers : 84734262',
				},
			},
			FJ: {
				value: "868406",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Fiji</span><br />Customers : 868406',
				},
			},
			FI: {
				value: "5387000",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Finland</span><br />Customers : 5387000',
				},
			},
			FR: {
				value: "65436552",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">France</span><br />Customers : 65436552',
				},
			},
			GA: {
				value: "1534262",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Gabon</span><br />Customers : 1534262',
				},
			},
			GM: {
				value: "1776103",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Gambia</span><br />Customers : 1776103',
				},
			},
			GE: {
				value: "4486000",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Georgia</span><br />Customers : 4486000',
				},
			},
			GH: {
				value: "24965816",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Ghana</span><br />Customers : 24965816',
				},
			},
			GR: {
				value: "11304000",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Greece</span><br />Customers : 11304000',
				},
			},
			GD: {
				value: "104890",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Grenada</span><br />Customers : 104890',
				},
			},
			GT: {
				value: "14757316",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Guatemala</span><br />Customers : 14757316',
				},
			},
			GN: {
				value: "10221808",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Guinea</span><br />Customers : 10221808',
				},
			},
			GQ: {
				value: "720213",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Equatorial Guinea</span><br />Customers : 720213',
				},
			},
			GW: {
				value: "1547061",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Guinea-bissau</span><br />Customers : 1547061',
				},
			},
			GY: {
				value: "756040",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Guyana</span><br />Customers : 756040',
				},
			},
			HT: {
				value: "10123787",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Haiti</span><br />Customers : 10123787',
				},
			},
			HN: {
				value: "7754687",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Honduras</span><br />Customers : 7754687',
				},
			},
			HU: {
				value: "9971000",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Hungary</span><br />Customers : 9971000',
				},
			},
			JM: {
				value: "2709300",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Jamaica</span><br />Customers : 2709300',
				},
			},
			JP: {
				value: "127817277",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Japan</span><br />Customers : 127817277',
				},
			},
			MH: {
				value: "54816",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Marshall Islands</span><br />Customers : 54816',
				},
			},
			PW: {
				value: "20609",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Palau</span><br />Customers : 20609',
				},
			},
			SB: {
				value: "552267",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Solomon Islands</span><br />Customers : 552267',
				},
			},
			IN: {
				value: "1241491960",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">India</span><br />Customers : 1241491960',
				},
			},
			ID: {
				value: "242325638",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Indonesia</span><br />Customers : 242325638',
				},
			},
			JO: {
				value: "6181000",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Jordan</span><br />Customers : 6181000',
				},
			},
			IR: {
				value: "74798599",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Iran, Islamic Republic Of</span><br />Customers : 74798599',
				},
			},
			IQ: {
				value: "32961959",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Iraq</span><br />Customers : 32961959',
				},
			},
			IE: {
				value: "4487000",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Ireland</span><br />Customers : 4487000',
				},
			},
			IS: {
				value: "319000",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Iceland</span><br />Customers : 319000',
				},
			},
			IL: {
				value: "7765700",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Israel</span><br />Customers : 7765700',
				},
			},
			IT: {
				value: "60770000",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Italy</span><br />Customers : 60770000',
				},
			},
			KZ: {
				value: "16558459",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Kazakhstan</span><br />Customers : 16558459',
				},
			},
			KE: {
				value: "41609728",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Kenya</span><br />Customers : 41609728',
				},
			},
			KG: {
				value: "5507000",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Kyrgyzstan</span><br />Customers : 5507000',
				},
			},
			KI: {
				value: "101093",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Kiribati</span><br />Customers : 101093',
				},
			},
			KW: {
				value: "2818042",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Kuwait</span><br />Customers : 2818042',
				},
			},
			LA: {
				value: "6288037",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Lao People\'s Democratic Republic</span><br />Customers : 6288037',
				},
			},
			LS: {
				value: "2193843",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Lesotho</span><br />Customers : 2193843',
				},
			},
			LV: {
				value: "2220000",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Latvia</span><br />Customers : 2220000',
				},
			},
			LB: {
				value: "4259405",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Lebanon</span><br />Customers : 4259405',
				},
			},
			LR: {
				value: "4128572",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Liberia</span><br />Customers : 4128572',
				},
			},
			LY: {
				value: "6422772",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Libya</span><br />Customers : 6422772',
				},
			},
			LI: {
				value: "36304",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Liechtenstein</span><br />Customers : 36304',
				},
			},
			LT: {
				value: "3203000",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Lithuania</span><br />Customers : 3203000',
				},
			},
			LU: {
				value: "517000",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Luxembourg</span><br />Customers : 517000',
				},
			},
			MK: {
				value: "2063893",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Macedonia, The Former Yugoslav Republic Of</span><br />Customers : 2063893',
				},
			},
			MG: {
				value: "21315135",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Madagascar</span><br />Customers : 21315135',
				},
			},
			MY: {
				value: "28859154",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Malaysia</span><br />Customers : 28859154',
				},
			},
			MW: {
				value: "15380888",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Malawi</span><br />Customers : 15380888',
				},
			},
			MV: {
				value: "320081",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Maldives</span><br />Customers : 320081',
				},
			},
			ML: {
				value: "15839538",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Mali</span><br />Customers : 15839538',
				},
			},
			MT: {
				value: "419000",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Malta</span><br />Customers : 419000',
				},
			},
			MA: {
				value: "32272974",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Morocco</span><br />Customers : 32272974',
				},
			},
			MU: {
				value: "1286051",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Mauritius</span><br />Customers : 1286051',
				},
			},
			MR: {
				value: "3541540",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Mauritania</span><br />Customers : 3541540',
				},
			},
			MX: {
				value: "114793341",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Mexico</span><br />Customers : 114793341',
				},
			},
			FM: {
				value: "111542",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Micronesia, Federated States Of</span><br />Customers : 111542',
				},
			},
			MD: {
				value: "3559000",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Moldova, Republic Of</span><br />Customers : 3559000',
				},
			},
			MC: {
				value: "35427",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Monaco</span><br />Customers : 35427',
				},
			},
			MN: {
				value: "2800114",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Mongolia</span><br />Customers : 2800114',
				},
			},
			ME: {
				value: "632261",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Montenegro</span><br />Customers : 632261',
				},
			},
			MZ: {
				value: "23929708",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Mozambique</span><br />Customers : 23929708',
				},
			},
			NA: {
				value: "2324004",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Namibia</span><br />Customers : 2324004',
				},
			},
			NP: {
				value: "30485798",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Nepal</span><br />Customers : 30485798',
				},
			},
			NI: {
				value: "5869859",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Nicaragua</span><br />Customers : 5869859',
				},
			},
			NE: {
				value: "16068994",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Niger</span><br />Customers : 16068994',
				},
			},
			NG: {
				value: "162470737",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Nigeria</span><br />Customers : 162470737',
				},
			},
			NO: {
				value: "4952000",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Norway</span><br />Customers : 4952000',
				},
			},
			NZ: {
				value: "4405200",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">New Zealand</span><br />Customers : 4405200',
				},
			},
			OM: {
				value: "2846145",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Oman</span><br />Customers : 2846145',
				},
			},
			UG: {
				value: "34509205",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Uganda</span><br />Customers : 34509205',
				},
			},
			UZ: {
				value: "29341200",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Uzbekistan</span><br />Customers : 29341200',
				},
			},
			PK: {
				value: "176745364",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Pakistan</span><br />Customers : 176745364',
				},
			},
			PS: {
				value: "4019433",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Palestine, State Of</span><br />Customers : 4019433',
				},
			},
			PA: {
				value: "3571185",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Panama</span><br />Customers : 3571185',
				},
			},
			PG: {
				value: "7013829",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Papua New Guinea</span><br />Customers : 7013829',
				},
			},
			PY: {
				value: "6568290",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Paraguay</span><br />Customers : 6568290',
				},
			},
			NL: {
				value: "16696000",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Netherlands</span><br />Customers : 16696000',
				},
			},
			PE: {
				value: "29399817",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Peru</span><br />Customers : 29399817',
				},
			},
			PH: {
				value: "94852030",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Philippines</span><br />Customers : 94852030',
				},
			},
			PL: {
				value: "38216000",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Poland</span><br />Customers : 38216000',
				},
			},
			PT: {
				value: "10637000",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Portugal</span><br />Customers : 10637000',
				},
			},
			QA: {
				value: "1870041",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Qatar</span><br />Customers : 1870041',
				},
			},
			DO: {
				value: "10056181",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Dominican Republic</span><br />Customers : 10056181',
				},
			},
			RO: {
				value: "21390000",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Romania</span><br />Customers : 21390000',
				},
			},
			GB: {
				value: "62641000",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">United Kingdom</span><br />Customers : 62641000',
				},
			},
			RU: {
				value: "141930000",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Russian Federation</span><br />Customers : 141930000',
				},
			},
			RW: {
				value: "10942950",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Rwanda</span><br />Customers : 10942950',
				},
			},
			KN: {
				value: "53051",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Saint Kitts And Nevis</span><br />Customers : 53051',
				},
			},
			SM: {
				value: "31735",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">San Marino</span><br />Customers : 31735',
				},
			},
			VC: {
				value: "109365",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Saint Vincent And The Grenadines</span><br />Customers : 109365',
				},
			},
			LC: {
				value: "176000",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Saint Lucia</span><br />Customers : 176000',
				},
			},
			SV: {
				value: "6227491",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">El Salvador</span><br />Customers : 6227491',
				},
			},
			WS: {
				value: "183874",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Samoa</span><br />Customers : 183874',
				},
			},
			ST: {
				value: "168526",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Sao Tome And Principe</span><br />Customers : 168526',
				},
			},
			SN: {
				value: "12767556",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Senegal</span><br />Customers : 12767556',
				},
			},
			RS: {
				value: "7261000",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Serbia</span><br />Customers : 7261000',
				},
			},
			SC: {
				value: "86000",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Seychelles</span><br />Customers : 86000',
				},
			},
			SL: {
				value: "5997486",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Sierra Leone</span><br />Customers : 5997486',
				},
			},
			SG: {
				value: "5183700",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Singapore</span><br />Customers : 5183700',
				},
			},
			SK: {
				value: "5440000",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Slovakia</span><br />Customers : 5440000',
				},
			},
			SI: {
				value: "2052000",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Slovenia</span><br />Customers : 2052000',
				},
			},
			SO: {
				value: "9556873",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Somalia</span><br />Customers : 9556873',
				},
			},
			SD: {
				value: "34318385",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Sudan</span><br />Customers : 34318385',
				},
			},
			SS: {
				value: "10314021",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">South Sudan</span><br />Customers : 10314021',
				},
			},
			LK: {
				value: "20869000",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Sri Lanka</span><br />Customers : 20869000',
				},
			},
			SE: {
				value: "9453000",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Sweden</span><br />Customers : 9453000',
				},
			},
			CH: {
				value: "7907000",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Switzerland</span><br />Customers : 7907000',
				},
			},
			SR: {
				value: "529419",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Suriname</span><br />Customers : 529419',
				},
			},
			SZ: {
				value: "1067773",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Swaziland</span><br />Customers : 1067773',
				},
			},
			SY: {
				value: "20820311",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Syrian Arab Republic</span><br />Customers : 20820311',
				},
			},
			TJ: {
				value: "6976958",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Tajikistan</span><br />Customers : 6976958',
				},
			},
			TZ: {
				value: "46218486",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Tanzania, United Republic Of</span><br />Customers : 46218486',
				},
			},
			TD: {
				value: "11525496",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Chad</span><br />Customers : 11525496',
				},
			},
			CZ: {
				value: "10546000",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Czech Republic</span><br />Customers : 10546000',
				},
			},
			TH: {
				value: "69518555",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Thailand</span><br />Customers : 69518555',
				},
			},
			TL: {
				value: "1175880",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Timor-leste</span><br />Customers : 1175880',
				},
			},
			TG: {
				value: "6154813",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Togo</span><br />Customers : 6154813',
				},
			},
			TO: {
				value: "104509",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Tonga</span><br />Customers : 104509',
				},
			},
			TT: {
				value: "1346350",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Trinidad And Tobago</span><br />Customers : 1346350',
				},
			},
			TN: {
				value: "10673800",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Tunisia</span><br />Customers : 10673800',
				},
			},
			TM: {
				value: "5105301",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Turkmenistan</span><br />Customers : 5105301',
				},
			},
			TR: {
				value: "73639596",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Turkey</span><br />Customers : 73639596',
				},
			},
			TV: {
				value: "9847",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Tuvalu</span><br />Customers : 9847',
				},
			},
			VU: {
				value: "245619",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Vanuatu</span><br />Customers : 245619',
				},
			},
			VE: {
				value: "29278000",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Venezuela, Bolivarian Republic Of</span><br />Customers : 29278000',
				},
			},
			VN: {
				value: "87840000",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Viet Nam</span><br />Customers : 87840000',
				},
			},
			UA: {
				value: "45706100",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Ukraine</span><br />Customers : 45706100',
				},
			},
			UY: {
				value: "3368595",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Uruguay</span><br />Customers : 3368595',
				},
			},
			YE: {
				value: "24799880",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Yemen</span><br />Customers : 24799880',
				},
			},
			ZM: {
				value: "13474959",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Zambia</span><br />Customers : 13474959',
				},
			},
			ZW: {
				value: "12754378",
				href: "#",
				tooltip: {
					content:
						'<span style="font-weight:bold;">Zimbabwe</span><br />Customers : 12754378',
				},
			},
		},
	});
});
