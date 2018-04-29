angular.module('issueTracker.edit.projects',
    [
        'ui.bootstrap',
        'issueTracker.dashboard.issuesDisplay',
        'issueTracker.constants',
        'inputDropdown',
        'issueTracker.input-dropdown',
        'issueTracker.notifications'
    ])
    .config(['$routeProvider', function($routeProvider){
        var routeChecks = {
            authenticated: ['$q', 'authentication', function($q, authentication){
                if (authentication.isAuthenticated()){
                    return $q.when(true);
                }

                return $q.reject('Unauthorised Access!');
            }]
        };

        $routeProvider.when('/projects/:id/edit', {
            templateUrl:'app/views/projects/edit-project.html',
            controller:'EditProjectController',
            resolve: routeChecks.authenticated
        })
    }])
    .controller('EditProjectController',[
        '$scope',
        '$localStorage',
        '$routeParams',
        'jQuery',
        'projects',
        'issuesDisplay',
        'identity',
        'constants',
        'inputDrop',
        'notification',
        'usSpinnerService',
        function($scope, $localStorage, $routeParams, $, projects, issuesDisplay, identity, constants, inputDrop, notification, usSpinnerService){
            $scope.isCurrUserAdmin = $localStorage.isAdmin;
            $scope.userID = $localStorage.currUserId;

            $scope.projectID = $routeParams.id;

            $scope.loadProjectData = function (projectId){
                usSpinnerService.spin('spinner');
                projects.getProjectById(projectId)
                    .then(function(project){
                        $scope.project = project;
                        $scope.projectLeaderId = project.Lead.Id;

                        $scope.rawLabels = constants.returnRawData($scope.project.Labels);
                        $scope.rawPriorities = constants.returnRawData($scope.project.Priorities);
                    }).then(function(success){
                        identity.getUserData()
                            .then(function (users) {
                                $scope.availableOptions = inputDrop.transformUserDataForInputDropdown(users);
                                $scope.selectedDropdownItem = $scope.availableOptions.filter(function(element){
                                    return element.userId === $scope.projectLeaderId;
                                })[0];

                                usSpinnerService.stop('spinner');
                            })
                });
            };

            $scope.editProject = function(projectInfo) {
                usSpinnerService.spin('spinner');

                var editedEntitie = {
                    Name: projectInfo.Name,
                    Description: projectInfo.Description,
                    LeadId: $scope.selectedDropdownItem.userId,
                    TransitionShemeId: projectInfo.TransitionSchemeId,
                    Labels: constants.convertDataToArray($scope.rawLabels),
                    Prioritioes: constants.convertDataToArray($scope.rawPriorities),
                };

                projects.editProject($scope.projectID, editedEntitie)
                    .then(function (project) {
                        notification.success("The project was successfully updated.");
                        usSpinnerService.stop('spinner');
                    });
            };

            $scope.filterInput = function(userInput){
                return inputDrop.filterInput($scope.availableOptions, userInput)
            };


            (function (projectID){
                $scope.loadProjectData(projectID);
            })($scope.projectID);
        }
    ]);

