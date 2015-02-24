'use strict';

/ * Angular Green Shelter Services */

$gs.services.add('Client', ['$resource', '$http',
	function($resource, $http) {
		var action = {
			'login': $resource('/api/v1/client/login', {}, {
				post: {	method: 'POST'}
			}),
			'register': $resource('/api/v1/client/register', {}, {
				post: {	method: 'POST'}
			}),
			'get': $resource('/api/v1/client/me', {}, {
				me: { method: 'GET'},
				post: {	method: 'POST'}
			}),
			'update': $resource('/api/v1/client/me', {}, {
				me: { method: 'POST'}
			}),				
			'authtypes': $resource('/api/v1/client/authtypes', {}, {
				getProviders: {	method: 'GET'}
			}),
			'external': {
				'login': $resource('/api/v1/client/externallogin', {}, { 
					requested: { 
						method: 'GET',
						headers: {
							'Content-Type': 'application/json; charset=utf-8;'
						}
					}
				})
			}
		};
		
		return action;
	}
]); // end Client

$gs.services.add('JSONP', ['$resource', 
	function($resource){
		var actions = {
			'data':	$resource('/scripts/gs-data.json', {}, {
				get: { method: 'GET' }
			})
		};
		
		return actions;
	}
]); // end JSONP