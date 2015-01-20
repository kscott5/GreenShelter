'use strict';

/* Angular GreenShelter Application Module */

var greenShelterApp = angular.module('greenShelterApp', [
	'ngRoute',
	'greenShelterControllers'
]);

greenShelterApp.config(['$routeProvider', function($routeProvider) {
	$routeProvider.
		when('/', {
			templateUrl: '/home_template.html',
			controller: 'HomeController'
		}).
		when('/home', {
			templateUrl: '/home_template.html',
			controller: 'HomeController'
		}).
		when('/home/index', {
			templateUrl: '/home_template.html',
			controller: 'HomeController'
		}).
		when('/contact', {
			templateUrl: '/contact_template.html',
			controller: 'ContactController'
		}).
		when('/about', {
			templateUrl: '/about_template.html',
			controller: 'AboutController'
		}).
		when('/about/mission', {
			templateUrl: '/about_mission_template.html',
			controller: 'AboutMissionController'
		}).
		when('/about/board', {
			templateUrl: '/about_board_template.html',
			controller: 'AboutBoardController'
		}).
		when('/about/employment', {
			templateUrl: '/about_employment_template.html',
			controller: 'AboutEmploymentController'
		}).
		when('/about/faqs', {
			templateUrl: '/about_faqs_template.html',
			controller: 'AboutFAQController'
		}).
		when('/services', {
			templateUrl: 'our_services_template.html',
			controller: 'OurServicesController'
		}).
		when('/services/family', {
			templateUrl: 'family_services_template.html',
			controller: 'FamilyServiceController'
		}).
		when('/services/housing', {
			templateUrl: 'housing_service_template.html',
			controller: 'HousingServiceController'
		}).
		when('/services/emergency/care', {
			templateUrl: 'emergency_care_service_template.html',
			controller: 'EmergencyCareServiceController'
		}).
      otherwise({
        redirectTo: '/home'
      });
  }]);