'use strict';

/* Angular GreenShelter Application Module */

var greenShelterApp = angular.module('greenShelterApp', [
	'ngRoute',
	'greenShelterControllers'
]);

greenShelterApp.config(['$routeProvider',
  function($routeProvider) {
    $routeProvider.
      when('/', {
        templateUrl: 'app/home.cshtml',
        controller: 'HomeCtrl'
      }).
      otherwise({
        redirectTo: '/'
      });
  }]);