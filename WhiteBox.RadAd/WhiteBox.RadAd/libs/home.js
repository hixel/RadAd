var radadApp = angular.module("radad", ["ngRoute"]);

radadApp.controller("homeController", function($scope, $http, $window) {
    $scope.freq = 1;
    $scope.test = { name: '' };

    $scope.save = function () {
        $http.post("/api/registration", $scope.test)
            .then(
                function () {
                    
                },
                function() {

                }
            );
    };
});