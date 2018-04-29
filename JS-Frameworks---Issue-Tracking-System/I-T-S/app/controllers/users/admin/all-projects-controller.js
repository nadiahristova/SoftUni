'use strict';

angular.module('issueTracker.admin.projects',
    [
        'issueTracker.projects',
        'inputDropdown',
        'ui.bootstrap',
        'issueTracker.input-dropdown'
    ])
    .config(['$routeProvider', function($routeProvider){
        var routeChecks = {
            authenticated: ['$q', 'authentication', function($q, authentication){
                if (authentication.isAuthenticated() && authentication.isAdmin()){
                    return $q.when(true);
                }

                return $q.reject('Unauthorised Access!');
            }]
        };

        $routeProvider.when('/Projects', {
            templateUrl:'app/views/users/admin/all-projects.html',
            controller:'AllProjectsController',
            resolve: routeChecks.authenticated
        })
    }])
    .controller('AllProjectsController',[
        '$scope',
        '$location',
        '$q',
        '$localStorage',
        'projects',
        'inputDrop',
        'usSpinnerService',
        function($scope, $location, $q, $localStorage, projects, inputDrop, usSpinnerService){
            $scope.isCurrUserAdmin = $localStorage.isAdmin;

            $scope.numPerPage = 15;
            $scope.currentPage = 1;
            $scope.maxSize = 5;

            $scope.loadFields = function(){
                usSpinnerService.spin('spinner');
                projects.retrieveProjects(2000, 1)
                    .then(function (projects) {
                        $scope.availableOptions = inputDrop.transformUserDataForProjectInputDropdown(projects.Projects);
                        usSpinnerService.stop('spinner');
                    });
            };

            $scope.selectedDropdownItem = null;

            $scope.pageChangedProjects = function (numPerPage, currentPage){
                projects.retrieveProjects(numPerPage, currentPage)
                    .then(function(projects){
                        $scope.affilProjects = projects.Projects;
                        $scope.totalCount = projects.TotalCount;
                        $scope.totalPages = projects.TotalPages;
                    });
            };

            $scope.filterInput = function(userInput){
                return inputDrop.filterInput($scope.availableOptions, userInput)
            };

            (function(numberPerPage, currentPage){
                $scope.pageChangedProjects(numberPerPage, currentPage);
                $scope.loadFields();
            })($scope.numPerPage, $scope.currentPage);
        }
    ]);
