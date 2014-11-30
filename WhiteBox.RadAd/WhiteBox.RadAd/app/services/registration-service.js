(function () {
    var radadApp = angular.module("radad");

    var registrationService = function($http, $q) {
        var _registrateRadio = function(registrateRadioModel) {

            var deferred = $q.defer();

            $http.post("/registration/registrateradio", registrateRadioModel)
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
            registrateRadio: _registrateRadio
        };
    };
    
    radadApp.factory("registrationService", registrationService);
}());