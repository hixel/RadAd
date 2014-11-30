(function () {
    var radadApp = angular.module("radad");

    var migrationController = function($scope, migrationService, $http, $route) {
        $scope.loaded = false;
        $scope.provided = null;

        migrationService.getMigrations()
            .then(function(result) {
                $scope.migrationInfo = result;
            })
            .then(function() {
                $scope.loaded = true;
            });

        $scope.provide = function() {
            $scope.provided = false;

            migrationService.provideMigration($scope.version)
                .then(
                    function() {
                        $scope.provided = true;
                        $route.reload();
                    },
                    function() {
                        $scope.provided = true;
                    });
        };
    };
    
    radadApp.controller("migrationController", ["$scope", "migrationService", "$http", "$route", migrationController]);
}());
