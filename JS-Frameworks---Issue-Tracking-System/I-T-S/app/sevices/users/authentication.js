'use strict';

angular.module('issueTracker.users.authentication', [])
    .factory(
        'authentication', [
            '$http',
            '$localStorage',
            '$q',
            '$location',
            'identity',
            'BASE_URL',
            '$rootScope',
            function($http, $localStorage, $q, $location, identity, BASE_URL, $rootScope){
                function registerUser(user){
                    var deffered = $q.defer();

                    $http.post(BASE_URL + 'api/Account/Register', user).
                        then(function (response){
                            deffered.resolve(response.data);
                        });

                    return deffered.promise;
                };

                function loginUser(user){
                    var deffered = $q.defer();
                    var loginData = 'Username=' + user.email + '&Password=' + user.password + '&grant_type=password';
                    //identity.refreshUserDataInLocalStorage();
                    var request = {
                        method: 'POST',
                        url: BASE_URL + 'api/Token',
                        data: loginData,
                        headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
                    };

                    $http(request).
                        then(function (response){
                            var accessToken = response.data.access_token;
                            $localStorage.accessToken = accessToken;
                            refreshHeader();
                            identity.refreshUserDataInLocalStorage()
                                .then(function(){
                                    $rootScope.$broadcast('login');
                                    deffered.resolve(response.data);
                                });
                        });
                    return deffered.promise;
                };

                function logout(){
                    $localStorage.$reset();
                    $localStorage.accessToken = undefined;
                    identity.removeUserProfile();
                    $rootScope.$broadcast('logout');
                    $location.path('/');
                };

                function isAuthenticated(){
                    return !!$localStorage.accessToken;
                };

                function isAdmin(){
                    if($localStorage.isAdmin === undefined){
                        return false;
                    }

                    return $localStorage.isAdmin;
                };

                function refreshHeader(){
                    if (isAuthenticated()){
                        $http.defaults.headers.common.Authorization = 'Bearer ' + $localStorage.accessToken;
                    }
                };

                function makeAdmin(){
                    var deffered = $q.defer();

                    $http.put(BASE_URL + 'users/makeAdmin',{UserId: '7ed7d763-0392-4ca5-a17f-54144bc98c6b'}).
                    then(function (response){
                        deffered.resolve(response.data);
                    });

                    return deffered.promise;
                };

                function changePassword(newPasssword){
                    var deffered = $q.defer();

                    $http.post(BASE_URL + 'api/Account/ChangePassword', newPasssword).
                    then(function (response){
                        deffered.resolve(response.data);
                    });

                    return deffered.promise;
                };

            return {
                registerUser: registerUser,
                loginUser: loginUser,
                logout: logout,
                refreshHeader: refreshHeader,
                isAdmin: isAdmin,
                isAuthenticated: isAuthenticated,
                makeAdmin: makeAdmin,
                changePassword: changePassword
            }
    }]);
