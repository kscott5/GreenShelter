'use strict';

/ * Angular Green Shelter Services */

var greenShelterServices = angular.module('greenShelterServices', ['ngResource']);

greenShelterServices.factory('Login', ['$resource',
	function($resource) {
		return $resource('/api/v1/account/login', {}, {
			query: { method: 'GET'},
			save: {	method: 'POST'}			
		});
	}
]);