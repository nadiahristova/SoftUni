'use strict';

angular.module('issueTracker.admin.projectManager',
    [
        'issueTracker.projects',
        'inputDropdown',
        'issueTracker.constants',
        'issueTracker.input-dropdown',
        'issueTracker.notifications'
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

        $routeProvider.when('/addProject', {
            templateUrl:'app/views/users/admin/add-project.html',
            controller:'ProjectManagerController',
            resolve: routeChecks.authenticated
        })
    }])
    .controller('ProjectManagerController',[
        '$scope',
        '$location',
        '$q',
        'projects',
        'identity',
        'constants',
        'inputDrop',
        'notification',
        'usSpinnerService',
        function($scope, $location, $q, projects, identity, constants, inputDrop, notification, usSpinnerService){

            $scope.loadFields = function(){
                identity.getUserData()
                    .then(function (users) {
                        $scope.availableOptions = inputDrop.transformUserDataForInputDropdown(users);
                    });

                $scope.rawPriorities = constants.returnRawData(constants.returnPriorities());
            };

            $scope.loadFields();

            $scope.selectedDropdownItem = null;

            $scope.addProject = function(projectInfo) {
                usSpinnerService.spin('spinner');

                projectInfo.Labels = constants.convertDataToArray(projectInfo.Labels);//convertToArray(projectInfo.Labels);
                projectInfo.Priorities = constants.convertDataToArray($scope.rawPriorities);//convertToArray($scope.rawPriorities);
                projectInfo.ProjectKey = $scope.genProjectKey(projectInfo.Name);
                projectInfo.LeadId = $scope.selectedDropdownItem.userId;

                projects.addProject(projectInfo)
                    .then(function (project) {
                        notification.success('New project was successfully created.');
                        usSpinnerService.stop('spinner');
                });
            };

            $scope.filterInput = function(userInput){
                return inputDrop.filterInput($scope.availableOptions, userInput)
            };

            $scope.genProjectKey = function(projectName){
                return constants.genProjectKey(projectName);
            };
        }
    ]);
