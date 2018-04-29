'use strict';

angular.module('issueTracker', [
    'ngRoute',
    'ngStorage',
    'issueTracker.home',
    'issueTracker.users.authentication',
    'issueTracker.users.identity',
    'issueTracker.admin.projectManager',
    'issueTracker.projects',
    'issueTracker.dashboard',
    'issueTracker.base',
    'issueTracker.issues.edit',
    'issueTracker.display.projects',
    'issueTracker.admin.projects',
    'issueTracker.issues.add',
    'issueTracker.issues.display',
    'issueTracker.edit.projects',
    'issueTracker.input-dropdown',
    'issueTracker.profile',
    'issueTracker.notifications',
    'angularSpinner'
])
.config(['$routeProvider', '$httpProvider', function($routeProvider, $httpProvider) {
  $routeProvider.otherwise({redirectTo: '/'});

    $httpProvider.interceptors.push(['$q', 'toastr', 'usSpinnerService', function($q, toastr, usSpinnerService){
        return {
            'response': function (response) {
                return response;
            },
            'responseError': function (rejection){
                var errorMsg = undefined;
                if(rejection.data && rejection.data['error_description']){
                    errorMsg = rejection.data['error_description'];
                } else if(rejection.data && rejection.data.ModelState && rejection.data.ModelState['']){
                    var errors = rejection.data.ModelState[''];
                    if (errors.length > 0){
                        errorMsg = errors[0];
                    }
                } else if(rejection.data && rejection.data.Message){
                    errorMsg = rejection.data.Message;
                }

                if(errorMsg){
                    toastr.error(errorMsg,'',{
                        closeButton: true,
                        positionClass: 'toast-top-center'
                    });
                }

                usSpinnerService.stop('spinner');

                return $q.reject(rejection);
            }
        }
    }]);
}])
.run(['$rootScope', '$location', '$route', 'authentication', 'identity', function($rootScope, $location, $route, authentication, identity){

    $rootScope.$on('$routeChangeStart', function(ev, next, current){
        authentication.refreshHeader();
    });

    $rootScope.$on('$routeChangeError', function(event, currentRoute, previousRoute, rejectionMessage){

      if (rejectionMessage === 'Unauthorised Access!') {
        $location.path('/');
      }
    });
}])
.constant('toastr', toastr)
.constant('jQuery', $)
.constant('BASE_URL', 'http://softuni-issue-tracker.azurewebsites.net/');