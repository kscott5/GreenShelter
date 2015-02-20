'use strict';

/ * Angular Green Shelter Controllers */

$gs.angularControllers.add('AppController', ['$scope', '$log', 'localStorageService',
	function($scope, $log, localStorageService) {
		$log.info('App Controller');
		
		$scope.user = {
			username: '',
			password: '',
			confirmpassword: '',
			
			firstname: '',
			lastname: '',
			
			streetname: '',
			city: '',
			state: '',
			zip: '',
			
			phone1: '',
			phone2: '',
			
			token: 'TODO: Get AntiForgeryToken from server',
			rememberme: false,
			authenticated: false
		};
		
		$scope.routes = $gs.routes.collection;
	}
]);

$gs.angularControllers.add('HomeController', ['$scope', '$location',
	function($scope, $location) {
		console.log('Home Controller');
		// add binding success to $scope		
		// using $location.get to fetch success
	}
]);

$gs.angularControllers.add('LoginController', ['$scope', '$location', '$log', 'Client', 
	function($scope, $location, $log, Client) {
		$scope.login = { 
			'label': {
				'username': 'Username',
				'password': 'Password',
				'submit': 'Login',
				'rememberme': 'Remember Me'
			}, // end label
			'clicked': function(login) {
				$("#loginSection form").validate({
					rules : {
						username: {
							required: true
						},
						password: {
							required: true
						}
					}, // end rules
					submitHandler: function(form) {
						$log.info('Preparing to log-in to site');
				
						Client.login.post({username: login.username, password: login.password, rememberme: login.rememberme}).$promise.then(
							function(success){
								$log.info(success.Description);
								$scope.user.authenticated = true;
								$location.path('/client/profile');
							},
							function(error) {
								$log.info(error.Description);
							}
						);	
					} // end submitHandler
				}); // end validate
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
				},
				'clicked': function(provider) {
					$log.info('Preparing to log-in using external provider type index: ' + provider);
		
					var myForm = $('#externalLoginForm');
					myForm.attr('target', "external_login");
					var myWindow = window.open('', 'external_login', 'width=200px,heigh=200px,resizable=yes');
					externalLoginForm.submit();
				} // end clicked
			} // end external
		}; // end $scope.login
		
		Client.authtypes.getProviders().$promise.then(
			function(success) {
				$log.info(success);
				$log.info('Retreived ' + success.Data.length + ' external login providers');
				
				$scope.login.external.provider.configured = success.Data.length > 0;
				$scope.login.external.provider.types = success.Data;
			},
			function(error) {
				$log.info('Failed retreiving external login providers. [Error: ' + error.Description + ']');
				$scope.login.external.provider.configured = false;
				$scope.login.external.provider.types = [];
			}
		);
	}
]); // end LoginController

$gs.angularControllers.add('ProfileController', ['$scope', '$log', '$location',
	function($scope, $log, $location) {
		if(!$scope.user.authenticated) {
			$log.info('TODO: Redirect to login');
		}

		$scope.profile = {
			label: {
				sectiongeneral: 'General Information',
				username: 'Username',
				firstname: 'First Name',
				lastname: 'Last Name',
				
				sectionaddress: 'Address Information',
				streetname: 'Street',
				city: 'City',
				state: 'State',
				zip: 'Zip',
				
				sectionphones: 'Phone Information',
				phone1: 'Phone 1',
				phone2: 'Phone 2',
				
				submit: 'Save'
			},
			clicked: function(){
				
			}
		};
	}
]);

$gs.angularControllers.add('LogoutController', ['$scope', '$location',
	function($scope, $location) {
		
		// TODO: Clear session
	}
]);

$gs.angularControllers.add('RegisterController', ['$scope', '$log', 'Client',
	function($scope, $log, Client) {
		$scope.registration = { 
			'label': {
				'username': 'Email',
				'password': 'Password',
				'confirmpassword': 'Confirm Password',
				'ssno': 'SS #',
				'submit': 'Register'
			},
			'username': '',
			'password': '',
			'confirmpassword': '',
			'token': 'TODO: Get AntiForgeryToken from server',
			'clicked': function(registration) {
				$("#registerSection form").validate({
					rules : {
						username : {
							required: true
						},
						password: {
							required: true
						},
						confirmpassword: {
							equalTo: "#password"
						},
						ssno: {
							required: true,
							digits: true,
							maxlength: 9,
							minlength: 9
						},
						messages: {
							username: {
								required: "Required input"
							},
							confirmpassword: {
								equalTo: $.validator.format("The value you enter doesn't match {0}")
							},
							digits: "0..9 are the only valid values",
							maxlength: $.validator.format("Maximum length is {0}"),
							minlength: $.validator.format("Minimum length is {0}"),
						}
					}, // end rules
					submitHandler: function(form) {
						$log.info('Preparing register new user');
				
						Client.register.post({username: registration.username, password: registration.password, confirmpassword: registration.confirmpassword, ssno: registration.ssno}).$promise.then(
							function(success){
								$log.info(success);
							},
							function(error) {
								$log.info(error);
							}
						);	
					} // end submitHandler
				}); // end validate
			} // end clicked
		}; // end $scope.registration
	} // end function
]); // end RegisterController
	
$gs.angularControllers.add('AboutController', ['$scope', '$routeParams',
	function($scope, $routeParams) {
		// add binding success to $scope
	}
]);

$gs.angularControllers.add('AboutMissionController', ['$scope', '$routeParams',
	function($scope, $routeParams) {
		// add binding success to $scope
	}
]);
	
$gs.angularControllers.add('AboutBoardController', ['$scope', '$routeParams',
	function($scope, $routeParams) {
		// add binding success to $scope
	}
]);
	
$gs.angularControllers.add('AboutEmploymentController', ['$scope', '$routeParams',
	function($scope, $routeParams) {
		// add binding success to $scope
	}
]);
	
$gs.angularControllers.add('AboutFAQController', ['$scope', '$routeParams',
	function($scope, $routeParams) {
		// add binding success to $scope
	}
]);
	
$gs.angularControllers.add('OurServicesController', ['$scope', '$routeParams',
	function($scope, $routeParams) {
		// add binding success to $scope
	}
]);
	
$gs.angularControllers.add('FamilyServiceController', ['$scope', '$routeParams',
	function($scope, $routeParams) {
		// add binding success to $scope
	}
]);

$gs.angularControllers.add('HousingServiceController', ['$scope', '$routeParams',
	function($scope, $routeParams) {
		// add binding success to $scope
	}
]);
	
$gs.angularControllers.add('EmergencyCareServiceController', ['$scope', '$routeParams',
	function($scope, $routeParams) {
		// add binding success to $scope
	}
]);
	
$gs.angularControllers.add('ContactController', ['$scope', '$routeParams',
	function($scope, $routeParams) {
		// add binding success to $scope
	}
]);
	
