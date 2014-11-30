(function () {
    var radadApp = angular.module("radad");

    var registrationService = function($http, $q) {
        var registrateRadio = function(registrateRadioModel) {

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
            registrateRadio: registrateRadio
        };
    };
    
    radadApp.factory("registrationService", registrationService);
}());