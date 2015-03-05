'use strict';

/*
	Initialize Green Shelter $gs variable
*/
var $gs = {};
$(document).ready(
	$gs =(function(){
		function GS() {
			var self = this;
			this.debugging = true;
			
			console.debug('Storing the verification Token');
			this.Token = $('input[name=__RequestVerificationToken]').attr('value');

			var gsRoutes = {
				About: [
					{ Area: 'about', Name: 'index', WhenUrl: '/about', TemplateUrl: '/app/about/index.html', Controller: 'AboutController' },
					{ Area: 'about', Name: 'board', WhenUrl: '/about/board', TemplateUrl: '/app/about/board.html', Controller: 'AboutBoardController' },
					{ Area: 'about', Name: 'employment', WhenUrl: '/about/employment', TemplateUrl: '/app/about/employment.html', Controller: 'AboutEmploymentController' },
					{ Area: 'about', Name: 'faqs', WhenUrl: '/about/faqs', TemplateUrl: '/app/about/faqs.html', Controller: 'AboutFAQController' },
					{ Area: 'about', Name: 'mission', WhenUrl: '/about/mission', TemplateUrl: '/app/about/mission.html', Controller: 'AboutMissionController' }
				],
				Client: [
					{ Area: 'client', Name: 'index', WhenUrl: '/client/login', TemplateUrl: '/app/client/Login.html', Controller: 'LoginController' },
					{ Area: 'client', Name: 'logout', WhenUrl: '/client/logout', TemplateUrl: '/app/client/logout.html', Controller: 'LogoutController' },
					{ Area: 'client', Name: 'Profile', WhenUrl: '/client/Profile', TemplateUrl: '/app/client/Profile.html', Controller: 'ProfileController' },
					{ Area: 'client', Name: 'register', WhenUrl: '/client/register', TemplateUrl: '/app/client/register.html', Controller: 'RegisterController' }
				],
				Contact: [
					{ Area: 'contact', Name: 'index', WhenUrl: '/contact', TemplateUrl: '/app/contact/index.html', Controller: 'ContactController' }
				],
				Home: [
					{ Area: 'home', Name: 'base', WhenUrl: '/', TemplateUrl: '/app/home/index.html', Controller: 'HomeController' },
					{ Area: 'home', Name: 'Controller', WhenUrl: '/spa', TemplateUrl: '/app/home/index.html', Controller: 'HomeController' },
					{ Area: 'home', Name: 'index', WhenUrl: '/spa/startpage', TemplateUrl: '/app/home/index.html', Controller: 'HomeController' }
				],
				Services: [
					{ Area: 'services', Name: 'emergency care', WhenUrl: '/services/emergency/care', TemplateUrl: '/app/services/emergencycare.html', Controller: 'EmergencyCareServiceController' },
					{ Area: 'services', Name: 'family', WhenUrl: '/services/family', TemplateUrl: '/app/services/family.html', Controller: 'FamilyServiceController' },
					{ Area: 'services', Name: 'housing', WhenUrl: '/services/housing', TemplateUrl: '/app/services/housing.html', Controller: 'HousingServiceController' },
					{ Area: 'services', Name: 'index', WhenUrl: '/services', TemplateUrl: '/app/services/index.html', Controller: 'OurServicesController' }
				]
			};
			
			console.debug('Saving Routes to GS() object');
			this.Routes = {
				Array: [
					gsRoutes.About, 
					gsRoutes.Client, 
					gsRoutes.Contact, 
					gsRoutes.Home, 
					gsRoutes.Services
				],
				Collection: gsRoutes
			};
				
			console.debug('Initialize application specific modules and configuration application specific providers'); 
			this.app = angular.module('gsApp', ['ngRoute', 'LocalStorageModule', 'gsControllers', 'gsServices']);
			this.app.config(['$routeProvider', '$logProvider', 'localStorageServiceProvider', 
				function($routeProvider, $logProvider, localStorageServiceProvider) {
					$logProvider.debugEnabled(self.debugging); // Configure log provider
										
					// Configure local storage
					localStorageServiceProvider
						.setPrefix('gsLocalStorage')
						.setStorageType('sessionStorage')
						.setNotify(true, true);

					// Configure the route provider
					$(self.Routes.Array).each(function(i){
						var Routes = self.Routes.Array[i];
						
						$(Routes).each(function(k){
							var route = Routes[k];
							$routeProvider.when( route.WhenUrl, {
								templateUrl: route.TemplateUrl,
								controller: route.Controller
							});
						});
					});
					
					// Add default to route provider
					$routeProvider.otherwise({
						redirectTo: '/'
					})		
				}
			]); // end angular module load and configuration 
			
			console.debug('Initializing the application services');
			this.services = { add: angular.module('gsServices', ['ngResource']).factory };
			this.services.add('Application', ['$resource', 
				function($resource){
					var actions = {
						'getGSJsonData': function(successHandler,errorHandler) {
							$resource('/scripts/gs-Data.json', {}, {
								get: { method: 'GET' }
							}).get().$promise.then(successHandler,errorHandler);
						}
					}; // end actions
					
					return actions;
				}
			]); // end Application Service
			
			this.services.add('Client', ['$resource', '$http',
				function($resource, $http) {
					var actions = {
						'login': function(Data,successHandler,errorHandler) {
							// Use arguments. arguments.length == 1 or arguments.length == 2 
							// Then you can remove the function parameters
							$resource('/api/v1/client/login', {}, {
								post: {	method: 'POST'}
							}).post(Data).$promise.then(successHandler,errorHandler);
						},
						'register': function(Data,successHandler,errorHandler) {
							// Use arguments. arguments.length == 1 or arguments.length == 2.
							// Then you can remove the function parameters
							$resource('/api/v1/client/register', {}, {
								post: {	method: 'POST'}
							}).post(Data).$promise.then(successHandler,errorHandler);
						},
						'getMe': function(Data,successHandler,errorHandler){
							// Use arguments. arguments.length == 1 or arguments.length == 2 
							// Then you can remove the function parameters
							
							$resource('/api/v1/client/me/:GuidId', {}, {
								get: { method: "GET" }
							}).get(Data).$promise.then(successHandler,errorHandler);
						},
						'updateMe': function(Data,successHandler,errorHandler){
							// Use arguments. arguments.length == 1 or arguments.length == 2 
							// Then you can remove the function parameters
							
							$resource('/api/v1/client/me', {}, {
								update: { method: "POST" }
							}).update(Data).$promise.then(successHandler,errorHandler);
						},				
						'getAuthtypes': function(successHandler,errorHandler) {
							// Use arguments. arguments.length == 1 or arguments.length == 2 
							// Then you can remove the function parameters
							
							$resource('/api/v1/client/authtypes', {}, {
								get: { method: 'GET' } 
							}).get().$promise.then(successHandler,errorHandler);
						},
						'external': {
							'login': function(Data,successHandler,errorHandler) {
								// Use arguments. arguments.length == 1 or arguments.length == 2 
								// Then you can remove the function parameters
								$resource('/api/v1/client/externallogin', {}, {
									requested: { method: 'GET', headers: {'Content-Type': 'application/json; charset=utf-8;' } } 
								}).requested(Data).$promise.then(successHandler,errorHandler).catch(errorHander);
							} // end login
						} // end external
					}; // end actions
					
					return actions;
				}
			]); // end Client Service
			
			console.debug('Initialize the controllers');
			this.controllers = { add: angular.module('gsControllers', []).controller };
			this.controllers.add('AppController', ['$scope', '$log', 'Application', 'Client', 
				function($scope, $log, Application, Client) {
					$log.debug('App Controller');
					
					$scope.Message = [];
					
					// NOTE: AngularJS documentation first StateCodes use services to share
					//		 code and State cross controllers. Then StateCodes that controllers 
					//		 can be nested to provide an inheritance effect allowing the
					// 		 controllers to share scope information, etc...
					//
					//		 1001 different ways to do things!!!!
					// 		 https://docs.angularjs.org/guide/controller
					
					$scope.User = {
						GuidId: '',
						UserName: '',
						FirstName: '',
						LastName: '',
						IsAuthenticated: false
					};

					$scope.Routes = self.Routes.Collection;
				}
			]);

			this.controllers.add('HomeController', ['$scope', '$location',
				function($scope, $location) {
					console.log('Home Controller');
					// add binding success to $scope		
					// using $location.get to fetch success
				}
			]);

			this.controllers.add('LoginController', ['$scope', '$location', '$log', 'Client', 
				function($scope, $location, $log, Client) {
					if($scope.User.IsAuthenticated) {
						$location.path('/client/Profile');
					}
					
					$scope.LoginForm = {
						Data: { Token: self.Token, UserName: '', PasswordHash: ''}, 
						Label: {
							Title: 'Account Login',
							UserName: 'UserName',
							Password: 'Password',
							Submit: 'Login'
						}, // end Label
						Clicked: function() {				
							$log.debug('Preparing to log-in to site' + $scope.LoginForm.UserName);
							
							Client.login($scope.LoginForm.Data,
								function(success){
									$log.debug(success);
									
									if(($scope.User.IsAuthenticated=success.Data.IsAuthenticated)) {
										$scope.User.GuidId = success.Data.GuidId;
										$scope.User.UserName = success.Data.UserName;
										$scope.User.FirstName = success.Data.FirstName;
										$scope.User.LastName = success.Data.LastName;
										
										$location.path('/client/Profile');
									}
								},
								function(error) {
									$log.debug(error);
									
									var results = error.data.Data;
									if($.isArray(results)) {
										$.each(results, function(index, value) {
											$log.debug(value.Description);
										});
									} else if($.isPlainObject(results)) {
										
										if(results.IsLockedOut) {
											$log.debug("You are locked out your account");
										} else if(results.IsNotAllowed) {
											$log.debug("Your not allow to login. Contact administrator");
										} else {
											$log.debug(error.data.Description);
										}
									} // end isArray/isPlainObject check
								} 
							); // end Client Login
				
						}, // end Clicked
					}; // end $scope.login
					
					$scope.ExternalForm = {
						Login: {
							Provider: {
								Configured: true,
								Types: []
							},
							Clicked: function(provider) {
								$log.debug('Preparing to log-in using external provider type index: ' + provider);
					
								var myForm = $('#ExternalLoginForm');
								myForm.attr('target', "external_login");
								var myWindow = window.open('', 'external_login', 'width=200px,heigh=200px,resizable=yes');
								ExternalLoginForm.Submit();
							} // end Clicked
						} // end login
					}; // end external
					
					Client.getAuthtypes(
						function(success) {
							$log.debug('Retreived ' + success.Data.length + ' external login providers');
							
							$scope.ExternalForm.Login.Provider.Configured = success.Data.length > 0;
							$scope.ExternalForm.Login.Provider.Types = success.Data;
						},
						function(error) {
							$log.debug('Failed retreiving external login providers. [Error: ' + error.Description + ']');
							$scope.ExternalForm.Login.Provider.Configured = false;
							$scope.ExternalForm.Login.Provider.Types = [];
						}
					);
				} // end function
				
			]); // end LoginController

			this.controllers.add('ProfileController', ['$scope', '$log', '$location', 'Application', 'Client',
				function($scope, $log, $location, Application, Client) {
					
					if(!$scope.User.IsAuthenticated) {
						document.location.hash = '/client/login?returnurl=/#/client/Profile';
					}

					$scope.ProfileForm = {
						Label: {
							Title: 'Profile',
							
							GeneralSection: 'General Information',
							UserName: 'UserName',
							FirstName: 'First Name',
							LastName: 'Last Name',
							
							AddressSection: 'Address Information',
							Street1: 'Street1',
							Street2: 'Street2',
							City: 'City',
							StateCode: 'State',
							ZipCode: 'Zip',
							AddressType: 'Type',
							
							PhoneSection: 'Contact Information',
							PhoneNumber: 'Phone',
							PhoneNumberType: 'Type',
							
							Submit: 'Save'
						},
						Data: {
							GuidId: $scope.User.GuidId,
							FirstName: $scope.User.FirstName,
							LastName: $scope.User.LastName,
							
							Addresses: [
								{ Street1: '', Street2: '', City: '', StateCode: '', ZipCode: '', CountryCode: 'USA' }
							],														
							PhoneNumber: '',
							PhoneNumberType: ''
						},
						
						Clicked: function(){					
							Client.updateMe({ Guid: $scope.User.GuidId, Data: $scope.ProfileForm.Data },
								function(success){
									$log.debug(success);
								},
								function(error){
									$log.debug(error);
								}
							); // end Profile update
						} // end Clicked
					}; // end Profile
					
					Application.getGSJsonData(
						function(success){
							$log.debug(success);
							$scope.AddressTypes = success.AddressTypes;
							$scope.StateCodes = success.StateCodes;
							$scope.PhoneNumberTypes = success.PhoneNumberTypes;
						},
						function(error) {
							$log.debug(error);
						}
					);
					
					$log.debug($scope.User.GuidId);
					Client.getMe({ GuidId: $scope.User.GuidId }, 
						function(success) {
							$log.debug(success);
							
							$scope.ProfileForm.Data = success.Data;							
						},
						function(error){
							$log.debug(error);
						}
					); // end Client.getMe
				} // end function
			]); // end ProfileController

			this.controllers.add('LogoutController', ['$scope', '$location',
				function($scope, $location) {
					
					// TODO: Clear session
				}
			]); // end LogoutController

			this.controllers.add('RegisterController', ['$scope', '$log', 'Client',
				function($scope, $log, Client) {
					$scope.RegistrationForm = { 
						Label: {
							Title: 'New Registration',
							UserName: 'Email',
							Password: 'Password',
							ConfirmPassword: 'Confirm Password',
							SSNo: 'SS #',
							Submit: 'Register'
						},
						Data: {
							UserName: '',
							PasswordHash: '',
							ConfirmPassword: '',
							SSNo: $scope.RegistrationForm.SSNoPart1+$scope.RegistrationForm.SSNoPart2+$scope.RegistrationForm.SSNoPart3
						},
						SSNoPart1: '',
						SSNoPart2: '',
						SSNoPart3: '',
						Clicked: function() {
							var registerForm = $("#registerSection form");
							registerForm.validate({
								rules : {
									UserName : {
										required: true
									},
									PasswordHash: {
										required: true
									},
									ConfirmPassword: {
										equalTo: "#PasswordHash"
									},
									SSNoPart1: {
										required: true,
										digits: true,
										maxlength: 3,
										minlength: 3
									},
									SSNoPart2: {
										required: true,
										digits: true,
										maxlength: 2,
										minlength: 2
									},
									SSNoPart3: {
										required: true,
										digits: true,
										maxlength: 4,
										minlength: 4
									},
									Message: {
										UserName: {
											required: "Required input"
										},
										ConfirmPassword: {
											equalTo: $.validator.format("The value you enter doesn't match {0}")
										},
										digits: "0..9 are the only valid values",
										maxlength: $.validator.format("Maximum length is {0}"),
										minlength: $.validator.format("Minimum length is {0}"),
									}
								}, // end rules
								submitHandler: function() {
									if(registerForm.valid()) {
										$log.debug('Preparing register new User');
								
										Client.register(RegistrationForm.Data,
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
						} // end Clicked
					}; // end $scope.Registration
				} // end function
			]); // end RegisterController
				
			this.controllers.add('AboutController', ['$scope', '$routeParams',
				function($scope, $routeParams) {
					// add binding success to $scope
				}
			]);

			this.controllers.add('AboutMissionController', ['$scope', '$routeParams',
				function($scope, $routeParams) {
					// add binding success to $scope
				}
			]);
				
			this.controllers.add('AboutBoardController', ['$scope', '$routeParams',
				function($scope, $routeParams) {
					// add binding success to $scope
				}
			]);
				
			this.controllers.add('AboutEmploymentController', ['$scope', '$routeParams',
				function($scope, $routeParams) {
					// add binding success to $scope
				}
			]);
				
			this.controllers.add('AboutFAQController', ['$scope', '$routeParams',
				function($scope, $routeParams) {
					// add binding success to $scope
				}
			]);
				
			this.controllers.add('OurServicesController', ['$scope', '$routeParams',
				function($scope, $routeParams) {
					// add binding success to $scope
				}
			]);
				
			this.controllers.add('FamilyServiceController', ['$scope', '$routeParams',
				function($scope, $routeParams) {
					// add binding success to $scope
				}
			]);

			this.controllers.add('HousingServiceController', ['$scope', '$routeParams',
				function($scope, $routeParams) {
					// add binding success to $scope
				}
			]);
				
			this.controllers.add('EmergencyCareServiceController', ['$scope', '$routeParams',
				function($scope, $routeParams) {
					// add binding success to $scope
				}
			]);
				
			this.controllers.add('ContactController', ['$scope', '$routeParams',
				function($scope, $routeParams) {
					// add binding success to $scope
				}
			]);
					
			/**
			* @description
			*
			* This function will ensure the headers nav-bar correctly highlights the
			* active or select nav-bar-item
			*
			*/
			function initNavBar() {
				// NOTE: animate.css is magically creating carousel with help from bootstrap.js
				var allNavObjs = $('.nav nav-bar li');
				
				console.log('TODO: initNavBar fix logic');
				return;
				
				allNavObjs.each(function() {
					var navObj = $(this), parent = null;
					if((parent = navObj.attr('gs-parent'))) {
						navObj.addEventListener('click', function() {
							allNavObj.removeClass('active');
							$('#'+parent).addClass('active');
							return true;
						});
					} else {
						navObj.addEventListener('click', function() {
							allNavObj.removeClass('active');
							navObj.attr().addClass('active');
							return true;
						});
					}
				});
			}
			
			console.debug('Appending the init to the GS() object');
			this.init = function(Data) {
				// This is wrapper function for template based initialization, and
				// the function call below. The function should check for any
				// specific template related DOM object to ensure initialization is 
				// for the intended page.
				
				initNavBar();
			};

			console.debug('Returning initialized GS() object via constructor');
			return this;
		} // end GS()

		console.debug('Returning initialized GS() object via module');
		return new GS();
	})()
);