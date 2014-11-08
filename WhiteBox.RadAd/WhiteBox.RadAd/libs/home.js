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

radadApp.factory("migrationService", function ($http, $q, $timeout) {
    
    var getMessages = function () {
        var deferred = $q.defer();

        $timeout(function () {
            deferred.resolve(['Hello', 'world']);
        }, 2000);

        return deferred.promise;
    };

    var getMigrations = function () {

        var deferred = $q.defer();

        $http.get("/api/migration")
            .then(
                function(result) {
                    deferred.resolve(result.data);
                },
                function() {
                    deferred.reject();
                });

        return deferred.promise;
    };

    return {
        getMigrations: getMigrations,
        getMessages: getMessages
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

    migrationService.getMigrations()
        .then(function (result) {
            $scope.migrationInfo = result;
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