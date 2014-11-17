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

radadApp.factory("migrationService", function ($http, $q) {
    
    var getMigrations = function () {

        var deferred = $q.defer();

        $http.get("/migration/getmigrations")
            .then(
                function (result) {
                    deferred.resolve(result.data);
                },
                function () {
                    deferred.reject();
                });

        return deferred.promise;
    };

    var provideMigration = function(version) {
        var deferred = $q.defer();

        $http.post("/migration/providemigration", { version: version })
            .then(
                function() {
                    deferred.resolve();
                },
                function() {
                    deferred.reject();
                });

        return deferred.promise;
    };
    
    return {
        getMigrations: getMigrations,
        provideMigration: provideMigration
    };
});

radadApp.factory("registrationService", function ($http, $q) {
    var registrateRadio = function (registrateRadioModel) {

        var deferred = $q.defer();

        $http.post("/registration/registrateradio", registrateRadioModel)
            .then(
                function (result) {
                    deferred.resolve(result.data);
                },
                function () {
                    deferred.reject();
                });

        return deferred.promise;
    };

    return {
        registrateRadio: registrateRadio
    };
});

radadApp.directive('passwordVerify', function () {
    return {
        restrict: 'A',
        require: '?ngModel',
        link: function(scope, elem, attrs, ngModel) {
            if (!ngModel) return;

            scope.$watch(attrs.ngModel, function() {
                validate();
            });

            attrs.$observe('passwordVerify', function () {
                validate();
            });

            var validate = function() {

                var val1 = ngModel.$viewValue;
                var val2 = attrs.passwordVerify;

                ngModel.$setValidity('passwordVerify', !val1 || !val2 || val1 === val2);
            };
        }
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