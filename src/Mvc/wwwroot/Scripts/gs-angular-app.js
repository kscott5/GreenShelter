'use strict';
/*
	Configuration required for angular to work
*/
$gs.app.config(['$routeProvider', '$logProvider', 'localStorageServiceProvider', function($routeProvider, $logProvider, localStorageServiceProvider) {
	$logProvider.debugEnabled(true); // Configure log provider
	
	// Configure local storage
	localStorageServiceProvider
		.setPrefix('gsLocalStorage')
		.setStorageType('sessionStorage')
		.setNotify(true, true);

	// Configure the route provider
	$($gs.routes.array).each(function(i){
		var routes = $gs.routes.array[i];
		
		$(routes).each(function(k){
			var route = routes[k];
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