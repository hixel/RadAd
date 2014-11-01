var radadApp = angular.module("radad", ["ngRoute"]);

radadApp.controller("homeController", function($scope) {
    $scope.greet = "hello!";
});