angular.module('issueTracker.base', [ 'issueTracker.users.authentication']).
    controller('MainController', [
    '$scope',
    'authentication',
    'identity',
    function($scope, authentication, identity){
        updateNavigationBar();

        $scope.logout = function(){
            $scope.isAuthenticated = authentication.isAuthenticated();
            authentication.logout();
        };

        $scope.$on('login', function () {
            identity.refreshUserDataInLocalStorage()
                .then(function(success){
                    updateNavigationBar()
                });
        });

        $scope.$on('logout', function () {
            $scope.isAuthenticated = authentication.isAuthenticated();
            $scope.isAdmin = authentication.isAdmin();
            $scope.currentUser = undefined;
        });

        function updateNavigationBar(){
            $scope.isAuthenticated = authentication.isAuthenticated();
            $scope.isAdmin = authentication.isAdmin();
            $scope.currentUser = identity.getCurrentUserData();
        }
    }]);
