angular.module('issueTracker.home',
    [
        'issueTracker.users.authentication'
    ])
.config(['$routeProvider', function($routeProvider){
    $routeProvider.when('/',{
        templateUrl: 'app/views/home/home.html',
        controller: 'HomeController'
    })
}])
.controller('HomeController',[
    '$scope',
    '$location',
    'authentication',
    'notification',
    'usSpinnerService',
    'jQuery',
    function($scope, $location, authentication, notification, usSpinnerService, $){

    if(authentication.isAuthenticated()){
        $location.path('/dashboard');
    }

     $scope.active = true;

    $scope.login = function(user){
        usSpinnerService.spin('spinner');

        authentication.loginUser(user).
        then(function(accessToken){
            usSpinnerService.stop('spinner');
            notification.success('You have successfully logged in!');
            $location.path('/dashboard');
        });
    };

    $scope.register = function(user){
        usSpinnerService.spin('spinner');
        authentication.registerUser(user).
            then(function(registeredUser){
                $scope.login(user);
            });
    };
}]);
