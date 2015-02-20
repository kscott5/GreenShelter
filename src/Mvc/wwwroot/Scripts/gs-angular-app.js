'use strict';
/*
	Configuration required for angular to work
*/
$gs.angularApp.config(['$routeProvider', 'localStorageServiceProvider', function($routeProvider, localStorageServiceProvider) {
	 localStorageServiceProvider
		.setPrefix('GreenShelter')
		.setStorageType('sessionStorage')
		.setNotify(true, true);

		// Configure the route provider
		$($gs.routes.array).each(function(i){
			var routes = $gs.routes.array[i];
			
			$(routes).each(function(k){
				var route = routes[k];
				console.log('Add routeProvider for ' + route.area + ' - ' + route.name);
				$routeProvider.when( route.whenUrl, {
					templateUrl: route.templateUrl,
					controller: route.controller
				});
			});
		});
		
		// Add default to route provider
		$routeProvider.otherwise({
			redirectTo: '/'
		})		
  }]);