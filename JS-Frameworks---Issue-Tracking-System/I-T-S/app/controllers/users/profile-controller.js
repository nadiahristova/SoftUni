angular.module('issueTracker.profile',
    [
        'issueTracker.users.identity'
    ])
    .config(['$routeProvider', function($routeProvider){
        var routeChecks = {
            authenticated: ['$q', 'authentication',
                function($q, authentication){
                if (authentication.isAuthenticated()){
                    return $q.when(true);
                }

                return $q.reject('Unauthorised Access!');
            }]
        };

        $routeProvider.when('/profile/password', {
            templateUrl:'app/views/users/profile.html',
            controller:'ProfileController',
            resolve: routeChecks.authenticated
        })
    }])
    .controller('ProfileController',[
        '$scope',
        'identity',
        'authentication',
        'notification',
        'usSpinnerService',
        function($scope, identity, authentication, notification, usSpinnerService){

            var loadInitialData = function(){
                usSpinnerService.spin('spinner');
                identity.requestLoggedInUserData()
                    .then(function(user){
                        $scope.user = user;
                        usSpinnerService.stop('spinner');
                    });
            };

            $scope.changeUserPassword = function(profileData){
                usSpinnerService.spin('spinner');
                authentication.changePassword(profileData)
                    .then(function(success){
                        notification.info('Your password has been changed.');
                        usSpinnerService.stop('spinner');
                    });
            };

            (function (){
                loadInitialData();
            })();
        }
    ]);
