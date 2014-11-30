(function () {
    var radadApp = angular.module("radad");

    var migrationService = function($http, $q) {

        var getMigrations = function() {

            var deferred = $q.defer();

            $http.get("/migration/getmigrations")
                .then(
                    function(result) {
                        deferred.resolve(result.data);
                    },
                    function() {
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
    };
    
    radadApp.factory("migrationService", migrationService);
}());