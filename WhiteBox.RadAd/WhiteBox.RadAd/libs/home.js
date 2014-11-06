var radadApp = angular.module("radad", ["ngRoute"]);

radadApp.config(function ($routeProvider) {
    $routeProvider
        .when("/home", {
            controller: "homeController",
            templateUrl: "/templates/home/index.html"
        })
    
        .when("/migration", {
            controller: "migrationController",
            templateUrl: "/templates/migration/list.html"
        })
    
        .otherwise({ redirectTo: "/home" });
});

radadApp.factory("migrationService", function($http, $q) {
    var migrations = {};
    
    var getMigrations = function () {

        var deferred = $q.defer();

        $http.get("/api/migration")
            .then(
                function(result) {
                    migrations = result.data;
                    deferred.resolve();
                },
                function() {
                    deferred.reject();
                });

        return deferred.promise;
    };

    var internalGetMigration = function() {
        return migrations;
    };
    
    return {
        migrations: internalGetMigration,
        getMigrations: getMigrations
    };
});

radadApp.controller("homeController", function($scope, $http) {
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

radadApp.controller("migrationController", function ($scope, migrationService, $http, $route) {
    $scope.loaded = false;
    $scope.provided = null;
    $scope.migrationInfo = {};
    
    migrationService.getMigrations()
        .then(function () {
            $scope.migrationInfo = migrationService.migrations();
        })
        .then(function() {
            $scope.loaded = true;
        });
    
    $scope.provide = function () {
        $scope.provided = false;
        
        $http.post("/api/migration", { version: $scope.version })
        .then(
            function (result) {
                $scope.provided = true;
                $route.reload();
            },
            function () {
                $scope.provided = true;
            });
    }
});