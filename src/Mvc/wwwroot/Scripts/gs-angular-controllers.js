'use strict';

/ * Angular Green Shelter Controllers */

$gs.controllers.add('AppController', ['$scope', '$log', 'Client', 
	function($scope, $log, Client) {
		$log.debug('App Controller');
		
		$scope.user = {
			username: '',
			password: '',
			confirmpassword: '',
			
			firstname: '',
			lastname: '',
			
			rememberme: false,
			authenticated: false
		};
		
		$scope.external = {
			'login': {
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
					$log.debug('Preparing to log-in using external provider type index: ' + provider);
		
					var myForm = $('#externalLoginForm');
					myForm.attr('target', "external_login");
					var myWindow = window.open('', 'external_login', 'width=200px,heigh=200px,resizable=yes');
					externalLoginForm.submit();
				} // end clicked
			} // end login
		}; // end external
		
		$scope.routes = $gs.routes.collection;
			
		Client.authtypes.getProviders().$promise.then(
			function(success) {
				$log.debug(success);
				$log.debug('Retreived ' + success.Data.length + ' external login providers');
				
				$scope.external.login.provider.configured = success.Data.length > 0;
				$scope.external.login.provider.types = success.Data;
			},
			function(error) {
				$log.debug('Failed retreiving external login providers. [Error: ' + error.Description + ']');
				$scope.external.login.provider.configured = false;
				$scope.external.login.provider.types = [];
			}
		);
	}
]);

$gs.controllers.add('HomeController', ['$scope', '$location',
	function($scope, $location) {
		console.log('Home Controller');
		// add binding success to $scope		
		// using $location.get to fetch success
	}
]);

$gs.controllers.add('LoginController', ['$scope', '$location', '$log', 'Client', 
	function($scope, $location, $log, Client) {
		$scope.login = { 
			'label': {
				'username': 'Username',
				'password': 'Password',
				'submit': 'Login',
				'rememberme': 'Remember Me'
			}, // end label
			'clicked': function(user) {
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
						$log.debug('Preparing to log-in to site' + user.username);
				
						Client.login.post({username: user.username, password: user.password, rememberme: user.rememberme, token: $gs.token}).$promise.then(
							function(success){
								$log.debug(success.Description);
								$log.debug(success.Data)
								
								$scope.user.username = success.Data.username;
								$scope.user.authenticated = success.Data.authenticated;
								$scope.user.firstname = success.Data.firstname;
								$scope.user.lastname = success.Data.lastname
								
								$location.path('/client/profile');
							},
							function(error) {
								$log.debug(error.Description);
							}
						);	
					} // end submitHandler
				}); // end validate
			}, // end clicked
		}; // end $scope.login
	} // end function
]); // end LoginController

$gs.controllers.add('ProfileController', ['$scope', '$log', '$location', 'JSONP',
	function($scope, $log, $location, JSONP) {
		if(!$scope.user.authenticated) {
			$log.debug('TODO: Redirect to login');
		}

		$scope.profile = {
			label: {
				generalsection: 'General Information',
				username: 'Username',
				firstname: 'First Name',
				lastname: 'Last Name',
				
				addresssection: 'Address Information',
				street1: 'Street1',
				street2: 'Street2',
				city: 'City',
				statecode: 'State',
				zipcode: 'Zip',
				
				phonesection: 'Contact Information',
				phonenumber: 'Phone',
				
				submit: 'Save'
			},
			
			firstname: $scope.user.firstname,
			lastname: $scope.user.lastname,
			
			street1: '',
			street2: '',
			city: '',
			statecode: '',
			zipcode: '',
			countrycode: 'USA',
			addresstype: '',
			
			phonenumber: '',
			phonenumbertype: '',
			
			clicked: function(profile){
				
			}
		}; // end profile
		
		JSONP.data.get().$promise.then(
			function(success) {
				$scope.addresstypes = success.addresstypes;
				$scope.phonetypes = success.phonetypes;
				$scope.states = success.states;
			},
			function(error){
				
			}
		); // end JSONP
	} // end function
]); // end ProfileController

$gs.controllers.add('LogoutController', ['$scope', '$location',
	function($scope, $location) {
		
		// TODO: Clear session
	}
]); // end LogoutController

$gs.controllers.add('RegisterController', ['$scope', '$log', 'Client',
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
						$log.debug('Preparing register new user');
				
						Client.register.post({username: registration.username, password: registration.password, confirmpassword: registration.confirmpassword, ssno: registration.ssno}).$promise.then(
							function(success){
								$log.debug(success);
							},
							function(error) {
								$log.debug(error);
							}
						);	
					} // end submitHandler
				}); // end validate
			} // end clicked
		}; // end $scope.registration
	} // end function
]); // end RegisterController
	
$gs.controllers.add('AboutController', ['$scope', '$routeParams',
	function($scope, $routeParams) {
		// add binding success to $scope
	}
]);

$gs.controllers.add('AboutMissionController', ['$scope', '$routeParams',
	function($scope, $routeParams) {
		// add binding success to $scope
	}
]);
	
$gs.controllers.add('AboutBoardController', ['$scope', '$routeParams',
	function($scope, $routeParams) {
		// add binding success to $scope
	}
]);
	
$gs.controllers.add('AboutEmploymentController', ['$scope', '$routeParams',
	function($scope, $routeParams) {
		// add binding success to $scope
	}
]);
	
$gs.controllers.add('AboutFAQController', ['$scope', '$routeParams',
	function($scope, $routeParams) {
		// add binding success to $scope
	}
]);
	
$gs.controllers.add('OurServicesController', ['$scope', '$routeParams',
	function($scope, $routeParams) {
		// add binding success to $scope
	}
]);
	
$gs.controllers.add('FamilyServiceController', ['$scope', '$routeParams',
	function($scope, $routeParams) {
		// add binding success to $scope
	}
]);

$gs.controllers.add('HousingServiceController', ['$scope', '$routeParams',
	function($scope, $routeParams) {
		// add binding success to $scope
	}
]);
	
$gs.controllers.add('EmergencyCareServiceController', ['$scope', '$routeParams',
	function($scope, $routeParams) {
		// add binding success to $scope
	}
]);
	
$gs.controllers.add('ContactController', ['$scope', '$routeParams',
	function($scope, $routeParams) {
		// add binding success to $scope
	}
]);
	
