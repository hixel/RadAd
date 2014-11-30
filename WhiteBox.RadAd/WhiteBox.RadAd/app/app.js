(function() {

    var radadApp = angular.module("radad", ["ngRoute"]);

    radadApp.config(function($routeProvider) {
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

}());