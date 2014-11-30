(function () {
    var radadApp = angular.module("radad");
    
    var registrationController = function($scope, registrationService, $route, $http) {
        $scope.result = {};
        $scope.registration = {};

        $scope.registrateRadio = function() {
            registrationService.registrateRadio($scope.registration)
                .then(function(result) {
                    $scope.result = result;
                });
        };

        $scope.updateCaptchaCall = function() {
            $http.post('/Account/JsonCaptchaInit')
                .success(function(data) {
                    $scope.registration.captchaImageUrl = data.imageUrl;
                    $scope.registration.captchaToken = data.tokenValue;
                    $scope.registration.captcha = '';
                });
        };

        $scope.updateCaptchaCall();
    };

    radadApp.controller("registrationController", ["$scope", "registrationService", "$route", "$http", registrationController]);
}());