(function () {
    var radadApp = angular.module("radad");
    
    var passwordVerify = function() {
        return {
            restrict: 'A',
            require: '?ngModel',
            link: function(scope, elem, attrs, ngModel) {
                if (!ngModel) return;

                scope.$watch(attrs.ngModel, function() {
                    validate();
                });

                attrs.$observe('passwordVerify', function() {
                    validate();
                });

                var validate = function() {

                    var val1 = ngModel.$viewValue;
                    var val2 = attrs.passwordVerify;

                    ngModel.$setValidity('passwordVerify', !val1 || !val2 || val1 === val2);
                };
            }
        };
    };
    
    radadApp.directive('passwordVerify', passwordVerify);
}());