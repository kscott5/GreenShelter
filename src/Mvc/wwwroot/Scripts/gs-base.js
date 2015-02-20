'use strict';
var $gs = {};

/*
	Adds angular modules for Green Shelter application
*/
$gs.angularApp = (function($, module){
/* Angular GreenShelter Application Module */

	return module('greenShelterApp', [
		'ngRoute',
		'LocalStorageModule',
		'greenShelterControllers',
		'greenShelterServices'
	]);	
})($, angular.module);

/*
	Adds angular modules for Green Shelter controllers
*/
$gs.angularControllers = (function($, module){
/* Angular GreenShelter Controllers Module */

	return { 
		add: module('greenShelterControllers', []).controller 
	};	
})($, angular.module);

/*
	Creates constants for 
*/
$gs.routes = (function($) {
	var gsRoutes = {
		about: [
			{ area: 'about', name: 'index', whenUrl: '/about', templateUrl: '/app/about/index.html', controller: 'AboutController' },
			{ area: 'about', name: 'board', whenUrl: '/about/board', templateUrl: '/app/about/board.html', controller: 'AboutBoardController' },
			{ area: 'about', name: 'employment', whenUrl: '/about/employment', templateUrl: '/app/about/employment.html', controller: 'AboutEmploymentController' },
			{ area: 'about', name: 'faqs', whenUrl: '/about/faqs', templateUrl: '/app/about/faqs.html', controller: 'AboutFAQController' },
			{ area: 'about', name: 'mission', whenUrl: '/about/mission', templateUrl: '/app/about/mission.html', controller: 'AboutMissionController' }
		],
		client: [
			{ area: 'client', name: 'index', whenUrl: '/client/login', templateUrl: '/app/client/login.html', controller: 'LoginController' },
			{ area: 'client', name: 'logout', whenUrl: '/client/logout', templateUrl: '/app/client/logout.html', controller: 'LogoutController' },
			{ area: 'client', name: 'profile', whenUrl: '/client/profile', templateUrl: '/app/client/profile.html', controller: 'ProfileController' },
			{ area: 'client', name: 'register', whenUrl: '/client/register', templateUrl: '/app/client/register.html', controller: 'RegisterController' }
		],
		contact: [
			{ area: 'contact', name: 'index', whenUrl: '/contact', templateUrl: '/app/contact/index.html', controller: 'ContactController' }
		],
		home: [
			{ area: 'home', name: 'base', whenUrl: '/', templateUrl: '/app/home/index.html', controller: 'HomeController' },
			{ area: 'home', name: 'controller', whenUrl: '/spa', templateUrl: '/app/home/index.html', controller: 'HomeController' },
			{ area: 'home', name: 'index', whenUrl: '/spa/startpage', templateUrl: '/app/home/index.html', controller: 'HomeController' }
		],
		services: [
			{ area: 'services', name: 'emergency care', whenUrl: '/services/emergency/care', templateUrl: '/app/services/emergencycare.html', controller: 'EmergencyCareServiceController' },
			{ area: 'services', name: 'family', whenUrl: '/services/family', templateUrl: '/app/services/family.html', controller: 'FamilyServiceController' },
			{ area: 'services', name: 'housing', whenUrl: '/services/housing', templateUrl: '/app/services/housing.html', controller: 'HousingServiceController' },
			{ area: 'services', name: 'index', whenUrl: '/services', templateUrl: '/app/services/index.html', controller: 'OurServicesController' }
		]
	};
	
	return {
		array: [
			gsRoutes.about, 
			gsRoutes.client, 
			gsRoutes.contact, 
			gsRoutes.home, 
			gsRoutes.services
		],
		collection: gsRoutes
	};
})($);