'use strict';

/ * Angular Green Shelter Services */

var greenShelterServices = angular.module('greenShelterServices', ['ngResource']);

greenShelterServices.factory('Client', ['$resource',
	function($resource) {
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
					me: {	method: 'POST'}
				}),				
			'authtypes': $resource('/api/v1/client/authtypes', {}, {
					getProviders: {	method: 'GET'}
				})
		};
		
		return action;
	}
]);