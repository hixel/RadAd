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
    
        .when("/registration", {
            controller: "registrationController",
            templateUrl: "/templates/registration/choice-registration.html"
        })
    
        .when("/registration-radio", {
            controller: "registrationController",
            templateUrl: "/templates/registration/registration-radio.html"
        })
    
        .when("/registration-client", {
            controller: "registrationController",
            templateUrl: "/templates/registration/registration-client.html"
        })
    
        .otherwise({ redirectTo: "/home" });
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
});

radadApp.controller("registrationController", function ($scope, registrationService, $route, $http) {
    $scope.result = {};
    $scope.registration = {};
    
    $scope.registrateRadio = function() {
        registrationService.registrateRadio($scope.registration)
            .then(function(result) {
                $scope.result = result;
            });
    };
    
    $scope.updateCaptchaCall = function () {
        $http.post('/Account/JsonCaptchaInit')
            .success(function (data) {
                $scope.registration.captchaImageUrl = data.imageUrl;
                $scope.registration.captchaToken = data.tokenValue;
                $scope.registration.captcha = '';
            });
    };
    
    $scope.updateCaptchaCall();
});