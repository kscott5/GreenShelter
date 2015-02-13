'use strict';

/ * Angular Green Shelter Controllers */

var greenShelterControllers = angular.module('greenShelterControllers', []);

greenShelterControllers.controller('AppController', ['$scope', '$http',
	function($scope, $http) {
		console.log('App Controller');
		$scope.user = {
			'email': '',
			'name': '',
			'authenticated': false
		};
		

	}
]);

greenShelterControllers.controller('HomeController', ['$scope', '$http',
	function($scope, $http) {
		console.log('Home Controller');
		// add binding data to $scope		
		// using $http.get to fetch data
	}
]);

greenShelterControllers.controller('LoginController', ['$scope', '$http', '$log', 'Client', 
	function($scope, $http, $log, Client) {
		$scope.login = { 
			'label': {
				'username': 'Username',
				'password': 'Password',
				'submit': 'Login',
				'remember_me': 'Remember Me'
			}, // end label
			'email': '',
			'password': '',
			'token': 'TODO: Get AntiForgeryToken from server',
			'clicked': function(login) {
				if($("#loginForm form").valid()) {				
					$log.info('Preparing to log-in to site');
				
					Client.login.post(login).$promise.then(
						function(data){
							$log.info(data.Description);
						},
						function(error) {
							$log.info(error.Description);
						}
					);
				}
			}, // end clicked
			'external': {
				'provider': {
					'configured': true,
					'types': [
						// TODO: Replace this list with list from server
						{ 'AuthenticationType': 'Google', 'Caption': 'Google'},
						{ 'AuthenticationType': 'Facebook', 'Caption': 'Facebook'},
						{ 'AuthenticationType': 'Twitter', 'Caption': 'Twitter'},
						{ 'AuthenticationType': 'MSN', 'Caption': 'MSN'}
					]
				}
			} // end external
		}; // end $scope.login
		
		Client.authtypes.getProviders().$promise.then(
			function(data) {
				$log.info(data);
				$log.info('Retreived ' + data.Data.length + ' external login providers');
				
				$scope.login.external.provider.configured = data.Data.length > 0;
				$scope.login.external.provider.types = data.Data;
			},
			function(error) {
				$log.info('Failed retreiving external login providers. [Error: ' + error.Description + ']');
				$scope.login.external.provider.configured = false;
			}
		);
	}
]);

greenShelterControllers.controller('LogoutController', ['$scope', '$http',
	function($scope, $http) {
		
		// TODO: Clear session
	}
]);

greenShelterControllers.controller('RegistrationController', ['$scope', '$http',
	function($scope, $http) {
		$scope.registration = { 
			'label': {
				'email': 'Email',
				'password': 'Password',
				'confirm_password': 'Confirm Password',
				'submit': 'Register'
			},
			'email': '',
			'password': '',
			'confirm_password': '',
			'token': 'TODO: Get AntiForgeryToken from server',
			'clicked': function(registration) {console.log('registration.email: ' + registration.email); }
		};
				
		// TODO: Call login $http.get
	}
]);
	
greenShelterControllers.controller('AboutController', ['$scope', '$routeParams',
	function($scope, $routeParams) {
		// add binding data to $scope
	}
]);

greenShelterControllers.controller('AboutMissionController', ['$scope', '$routeParams',
	function($scope, $routeParams) {
		// add binding data to $scope
	}
]);
	
greenShelterControllers.controller('AboutBoardController', ['$scope', '$routeParams',
	function($scope, $routeParams) {
		// add binding data to $scope
	}
]);
	
greenShelterControllers.controller('AboutEmploymentController', ['$scope', '$routeParams',
	function($scope, $routeParams) {
		// add binding data to $scope
	}
]);
	
greenShelterControllers.controller('AboutFAQController', ['$scope', '$routeParams',
	function($scope, $routeParams) {
		// add binding data to $scope
	}
]);
	
greenShelterControllers.controller('OurServicesController', ['$scope', '$routeParams',
	function($scope, $routeParams) {
		// add binding data to $scope
	}
]);
	
greenShelterControllers.controller('FamilyServiceController', ['$scope', '$routeParams',
	function($scope, $routeParams) {
		// add binding data to $scope
	}
]);

greenShelterControllers.controller('HousingServiceController', ['$scope', '$routeParams',
	function($scope, $routeParams) {
		// add binding data to $scope
	}
]);
	
greenShelterControllers.controller('EmergencyCareServiceController', ['$scope', '$routeParams',
	function($scope, $routeParams) {
		// add binding data to $scope
	}
]);
	
greenShelterControllers.controller('ContactController', ['$scope', '$routeParams',
	function($scope, $routeParams) {
		// add binding data to $scope
	}
]);
	
