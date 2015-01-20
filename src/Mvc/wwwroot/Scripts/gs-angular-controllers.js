'use strict';

/ * Angular Green Shelter Controllers */

var greenShelterControllers = angular.module('greenShelterControllers', []);

greenShelterControllers.controller('HomeCtrl', ['$scope', '$http',
  function($scope, $http) {
    // add binding data to $scope
    $scope.example = 'data1';
    $scope.example2 = 'data2';
  }]);

greenShelterControllers.controller('AboutCtrl', ['$scope', '$routeParams',
  function($scope, $routeParams) {
    $scope.phoneId = $routeParams.phoneId;
  }]);
