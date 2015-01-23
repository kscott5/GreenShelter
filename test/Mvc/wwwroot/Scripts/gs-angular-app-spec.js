'use strict';

/* Jasmine specs for angular routes */

describe('gs-angular-route-spec', function() {
	it('should redirect /home to /#/', function() {
		browser.get('http://localhost:8080/home');
		browser.getLocationAbsUrl().then(function(url){
			// not correct according http://angular.github.io/protractor/#/api?view=Protractor.prototype.getLocationAbsUrl
			expect(url).toBe('/');
		});
	});
	
	it('should redirect /home/index to /#/', function() {
		browser.get('http://localhost:8080/home/index');
		browser.getLocationAbsUrl().then(function(url){
			// not correct according http://angular.github.io/protractor/#/api?view=Protractor.prototype.getLocationAbsUrl
			expect(url).toBe('/');
		});
	});
	
	it('should redirect /#/about to /about', function() {
		browser.get('http://localhost:8080/#/about');
		browser.getLocationAbsUrl().then(function(url){
			// not correct according http://angular.github.io/protractor/#/api?view=Protractor.prototype.getLocationAbsUrl
			expect(url).toBe('/about');
		});
	});
	
	it('should redirect /#/about/mission to /about/mission', function() {
		browser.get('http://localhost:8080/#/about/mission');
		browser.getLocationAbsUrl().then(function(url){
			// not correct according http://angular.github.io/protractor/#/api?view=Protractor.prototype.getLocationAbsUrl
			expect(url).toBe('/about/mission');
		});
	});
	
});