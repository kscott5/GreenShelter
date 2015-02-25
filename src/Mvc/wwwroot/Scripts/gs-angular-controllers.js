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
			
		Client.getAuthtypes(
			function(success) {
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
				'title': 'Account Login',
				'username': 'Username',
				'password': 'Password',
				'submit': 'Login',
				'rememberme': 'Remember Me'
			}, // end label
			'clicked': function(user) {
				var form = $("#loginSection form");
				form.validate({
					rules : {
						username: {
							required: true
						},
						password: {
							required: true
						}
					}, // end rules
					submitHandler: function() {
						if(form.valid()) {
							$log.debug('Preparing to log-in to site' + user.username);
							
							Client.login({username: user.username, password: user.password, rememberme: user.rememberme, token: $gs.token},
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
						} // end form.valid
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
				title: 'Profile',
				
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
				addresstype: 'Type',
				
				phonesection: 'Contact Information',
				phonenumber: 'Phone',
				phonetype: 'Type',
				
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
			
			clicked: function(profile){
				var addressInfo = { 
					street1: profile.street1, street2: profile.street2, 
					city: profile.city, state: profile.state, 
					countrycode: profile.countrycode, zipcode: profile.zipcode, 
					addresstype: profile.addresstype
				};
				
				var data = { 
					id: $scope.user.id, username: profile.username, 
					lastname: profile.lastname, address: addressinfo, 
					token: $user.token,  phonenumber: profile.phonenumber, 
					phonetype: profile.phonetype 
				};
				
				Client.updateMe(data,
					function(success){
						$log.debug(success);
					},
					function(error){
						$log.debug(error);
					}
				); // end profile update
			} // end clicked
		}; // end profile
		
		JSONP.getAppData(
			function(success) {
				$scope.addresstypes = success.addresstypes;
				$scope.phonetypes = success.phonetypes;
				$scope.states = success.states;
			},
			function(error){
				$log.debug(error);
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
				'title': 'New Registration',
				'username': 'Email',
				'password': 'Password',
				'confirmpassword': 'Confirm Password',
				'ssno': 'SS #',
				'submit': 'Register'
			},
			'username': '',
			'password': '',
			'confirmpassword': '',
			'ssnoPart1': '',
			'ssnoPart2': '',
			'ssnoPart3': '',
			'clicked': function(registration) {
				var registerForm = $("#registerSection form");
				registerForm.validate({
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
						ssnoPart1: {
							required: true,
							digits: true,
							maxlength: 3,
							minlength: 3
						},
						ssnoPart2: {
							required: true,
							digits: true,
							maxlength: 2,
							minlength: 2
						},
						ssnoPart3: {
							required: true,
							digits: true,
							maxlength: 4,
							minlength: 4
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
						if(registerForm.valid()) {
							$log.debug('Preparing register new user');
					
							var data = {
								username: registration.username, password: registration.password, 
								confirmpassword: registration.confirmpassword, 
								ssno: registration.ssnoPart1+registration.ssnoPart2+registration.ssnoPart3
							};
							
							Client.register(data,
								function(success){
									$log.debug(success);
								},
								function(error) {
									$log.debug(error);
								}
							);	
						} // end if form valid
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
	
