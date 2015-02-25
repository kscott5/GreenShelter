'use strict';

/ * Angular Green Shelter Services */

$gs.services.add('Client', ['$resource', '$http',
	function($resource, $http) {
		var actions = {
			'login': function(data,successHandler,errorHandler) {
				// Use arguments. arguments.length == 1 or arguments.length == 2 
				// Then you can remove the function parameters
				$resource('/api/v1/client/login', {}, {
					post: {	method: 'POST'}
				}).post(data).$promise.then(successHandler,errorHandler);
			},
			'register': function(data,successHandler,errorHandler) {
				// Use arguments. arguments.length == 1 or arguments.length == 2.
				// Then you can remove the function parameters
				$resource('/api/v1/client/register', {}, {
					post: {	method: 'POST'}
				}).post(data).$promise.then(successHandler,errorHandler);
			},
			'getMe': function(data,successHandler,errorHandler){
				// Use arguments. arguments.length == 1 or arguments.length == 2 
				// Then you can remove the function parameters
				
				$resource('/api/v1/client/me', {}, {
					get: { method: "GET" }
				}).get(data).$promise.then(successHandler,errorHandler);
			},
			'updateMe': function(data,successHandler,errorHandler){
				// Use arguments. arguments.length == 1 or arguments.length == 2 
				// Then you can remove the function parameters
				
				$resource('/api/v1/client/me', {}, {
					update: { method: "POST" }
				}).update(data).$promise.then(successHandler,errorHandler);
			},				
			'getAuthtypes': function(successHandler,errorHandler) {
				// Use arguments. arguments.length == 1 or arguments.length == 2 
				// Then you can remove the function parameters
				
				$resource('/api/v1/client/authtypes', {}, {
					get: { method: 'GET' } 
				}).get().$promise.then(successHandler,errorHandler);
			},
			'external': {
				'login': function(data,successHandler,errorHandler) {
					// Use arguments. arguments.length == 1 or arguments.length == 2 
					// Then you can remove the function parameters
					$resource('/api/v1/client/externallogin', {}, {
						requested: { method: 'GET', headers: {'Content-Type': 'application/json; charset=utf-8;' } } 
					}).requested(data).$promise.then(successHandler,errorHandler);
				} // end login
			} // end external
		}; // end actions
		
		return actions;
	}
]); // end Client

$gs.services.add('JSONP', ['$resource', 
	function($resource){
		var actions = {
			'getAppData': function(successHandler,errorHandler) {
				$resource('/scripts/gs-data.json', {}, {
					get: { method: 'GET' }
				}).get().$promise.then(successHandler,errorHandler);
			}
		}; // end actions
		
		return actions;
	}
]); // end JSONP