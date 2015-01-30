'use strict';

/* Angular GreenShelter Application Module */

var greenShelterApp = angular.module('greenShelterApp', [
	'ngRoute',
	'greenShelterControllers',
	'greenShelterServices'
]);

greenShelterApp.config(['$routeProvider', function($routeProvider) {
	$routeProvider.
		/* Static Templates */
		when('/', {
			templateUrl: '/app/home/index.html',
			controller: 'HomeController'
		}).
		when('/spa', {
			templateUrl: '/app/home/index.html',
			controller: 'HomeController'
		}).
		when('/spa/startpage', {
			templateUrl: '/app/home/index.html',
			controller: 'HomeController'
		}).
		when('/contact', {
			templateUrl: '/app/contact/index.html',
			controller: 'ContactController'
		}).
		when('/about', {
			templateUrl: '/app/about/index.html',
			controller: 'AboutController'
		}).
		when('/about/mission', {
			templateUrl: '/app/about/mission.html',
			controller: 'AboutMissionController'
		}).
		when('/about/board', {
			templateUrl: '/app/about/board.html',
			controller: 'AboutBoardController'
		}).
		when('/about/employment', {
			templateUrl: '/app/about/employment.html',
			controller: 'AboutEmploymentController'
		}).
		when('/about/faqs', {
			templateUrl: '/app/about/faqs.html',
			controller: 'AboutFAQController'
		}).
		when('/services', {
			templateUrl: '/app/services/index.html',
			controller: 'OurServicesController'
		}).
		when('/services/family', {
			templateUrl: '/app/services/family.html',
			controller: 'FamilyServiceController'
		}).
		when('/services/housing', {
			templateUrl: '/app/services/housing.html',
			controller: 'HousingServiceController'
		}).
		when('/services/emergency/care', {
			templateUrl: '/app/services/emergencycare.html',
			controller: 'EmergencyCareServiceController'
		}).
		
		/* Dynamic Templates */
				when('/client/login', {
			templateUrl: '/app/client/login.html',
			controller: 'LoginController'
		}).
		when('/client/logout', {
			templateUrl: '/app/client/logout.html',
			controller: 'LogoutController'
		}).
		when('/client/register', {
			templateUrl: '/app/client/registration.html',
			controller: 'RegistrationController'
		}).

      otherwise({
        redirectTo: '/'
      });
  }]);