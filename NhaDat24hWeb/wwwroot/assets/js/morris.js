$(function () {
    "use strict";


	
	// Extra chart
	 Morris.Area({
		element: 'extra-area-chart',
		data: [{
					period: '7/2023',
					iphone: 0,
					ipad: 0,
					itouch: 0
				}, {
					period: '8/2023',
					iphone: 50,
					ipad: 15,
					itouch: 5
				}, {
					period: '9/2023',
					iphone: 20,
					ipad: 50,
					itouch: 65
				}, {
					period: '10/2023',
					iphone: 60,
					ipad: 12,
					itouch: 7
				}, {
					period: '11/2023',
					iphone: 30,
					ipad: 20,
					itouch: 120
				}, {
					period: '12/2023',
					iphone: 25,
					ipad: 80,
					itouch: 40
				}

				],
				lineColors: ['#1dc130', '#2f3d4a', '#009efb'],
				xkey: 'period',
				ykeys: ['iphone', 'ipad', 'itouch'],
				labels: ['Website A', 'Website B', 'Website C'],
				pointSize: 0,
				lineWidth: 0,
				resize:true,
				fillOpacity: 0.8,
				behaveLikeLine: true,
				gridLineColor: '#e0e0e0',
				hideHover: 'auto'
			
		});
}); 