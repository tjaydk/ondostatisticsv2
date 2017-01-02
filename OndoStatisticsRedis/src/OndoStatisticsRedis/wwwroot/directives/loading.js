angular.module('loadingDirective', [])
.controller('loadingCtrl', [function () {
    var self = this;

    //cancel function that simply reloads the current page
    self.cancel = function () {
        window.location.reload();
    }
}])
.directive('loading', function () {
    return {
        templateUrl: 'directives/loading.html',
        restrict: 'EA'
    }
});