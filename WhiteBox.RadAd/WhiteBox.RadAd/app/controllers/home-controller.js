(function () {
    var radadApp = angular.module("radad");

    var homeController = function($scope, $http) {
        $scope.freq = 1;
        $scope.test = { name: '' };

        $scope.save = function() {
            $http.post("/api/registration", $scope.test)
                .then(
                    function() {

                    },
                    function() {

                    }
                );
        };
    };
    
    radadApp.controller("homeController", ["$scope", "$http", homeController]);
}());
