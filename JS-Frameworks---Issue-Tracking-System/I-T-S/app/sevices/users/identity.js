angular.module('issueTracker.users.identity', []).
    factory('identity', [
    '$http',
    '$localStorage',
    '$q',
    'BASE_URL',
    function($http, $localStorage, $q, BASE_URL){
        function requestUserProfile(){
            if(!$localStorage.currUserId){
                var userProfileDeferred = $q.defer();

                $http.get(BASE_URL + 'Users/me')
                    .then(function(response){
                        $localStorage.username = response.data.Username;
                        $localStorage.currUserId = response.data.Id;
                        $localStorage.isAdmin = response.data.isAdmin;
                        userProfileDeferred.resolve(response.data);
                    });

                return userProfileDeferred.promise;
            }

            return $q.when(true);
        };

        function refreshUserDataInLocalStorage(){
            var userProfileDeferred = $q.defer();

            $http.get(BASE_URL + 'Users/me')
                .then(function(response){
                    $localStorage.username = response.data.Username;
                    $localStorage.currUserId = response.data.Id;
                    $localStorage.isAdmin = response.data.isAdmin;
                    userProfileDeferred.resolve(response.data);
                });

            return userProfileDeferred.promise;
        };

        function requestLoggedInUserData(){
            var defer = $q.defer();

            $http.get(BASE_URL + 'Users/me')
                .then(function(response){
                    defer.resolve(response.data);
                });

            return defer.promise;
        };

        function removeUserProfile(){
            $localStorage.username = undefined;
            $localStorage.currUserId = undefined;
            $localStorage.isAdmin = undefined;
        };

        function getCurrentUserData(){

            return {
                username: $localStorage.username,
                id: $localStorage.currUserId,
                isAdmin: $localStorage.isAdmin
            }
        };

        function getUserData(){
            var deferred = $q.defer();

            $http.get(BASE_URL + 'Users')
                .then(function(response){
                    deferred.resolve(response.data);
                });

            return deferred.promise;
        };

    return {
        requestUserProfile: requestUserProfile,
        removeUserProfile: removeUserProfile,
        getCurrentUserData: getCurrentUserData,
        getUserData: getUserData,
        refreshUserDataInLocalStorage: refreshUserDataInLocalStorage,
        requestLoggedInUserData: requestLoggedInUserData
    }
}]);
