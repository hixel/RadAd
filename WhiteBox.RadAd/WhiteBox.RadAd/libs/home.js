var radadApp = angular.module("radad", ["ngRoute"]);

radadApp.controller("homeController", function($scope) {
    $scope.freq = 1;
    $scope.test = { name: 'Igor' };

    $scope.save = function() {
        alert($scope.test.name);
    };
});