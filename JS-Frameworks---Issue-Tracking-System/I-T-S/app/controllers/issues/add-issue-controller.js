angular.module('issueTracker.issues.add',
    [
        'ui.bootstrap',
        'issueTracker.dashboard.issuesDisplay',
        'issueTracker.constants',
        'inputDropdown',
        'issueTracker.projects',
        'issueTracker.input-dropdown'
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

        $routeProvider.when('/projects/:id/add-issue', {
            templateUrl:'app/views/issues/add-issue.html',
            controller:'AddIssueController',
            resolve: routeChecks.authenticated
        })
    }])
    .controller('AddIssueController',[
        '$scope',
        '$localStorage',
        '$routeParams',
        '$location',
        '$q',
        'jQuery',
        'projects',
        'issuesDisplay',
        'identity',
        'constants',
        'notification',
        'inputDrop',
        'usSpinnerService',
        function($scope, $localStorage, $routeParams, $location, $q, $, projects, issuesDisplay, identity, constants, notification, inputDrop, usSpinnerService){
            $scope.isCurrUserAdmin = $localStorage.isAdmin;
            $scope.userID = $localStorage.currUserId;

            var checkIdentity = function(leadId){
                if(!($scope.isCurrUserAdmin || $scope.userID === leadId)) {
                    $location.path('/dashboard');
                }
            };

            $scope.projectId = $routeParams.id;
            $scope.issueAuthor = $localStorage.username;

            $scope.selectedDropdownItem = null;
            $scope.currPriority = null;
            $scope.currStatus = null;
            $scope.rawLabels = null;

            var loadIssueData = function (projectId){
                usSpinnerService.spin('spinner');

                projects.getProjectById(projectId)
                    .then(function(project){
                        $scope.projectName = project.Name;
                        checkIdentity(project.Lead.Id);
                    }).then(function(success){
                        identity.getUserData()
                            .then(function (users) {
                                $scope.availableOptions = inputDrop.transformUserDataForInputDropdown(users);

                                $scope.priorities = constants.returnPriorities();
                                $scope.statuses = constants.returnStatuses();

                                $scope.currPriority = $scope.priorities[0];
                                $scope.currStatus = $scope.statuses[0];

                                usSpinnerService.stop('spinner');
                            })
                    })
                };

            $scope.filterInput = function(userInput){
                return inputDrop.filterInput($scope.availableOptions, userInput)
            };

            $scope.createIssue = function(issue){
                usSpinnerService.spin('spinner');

                var labels = returnLabels();

                console.log(labels);

                var issueEntity = {
                    Title: issue.Title,
                    Description: issue.Description,
                    ProjectId: $scope.projectId,
                    DueDate: issue.DueDate,
                    PriorityId: $scope.currPriority.Id,
                    AssigneeId: $scope.selectedDropdownItem.userId,
                    Labels: labels
                };

                issuesDisplay.createIssue(issueEntity)
                    .then(function(success){
                        notification.success("Issue was successfully created.");
                        usSpinnerService.stop('spinner');
                    });
            };

            function returnLabels(){
                var labels = [];

                if($scope.rawLabels && typeof $scope.rawLabels === 'string' ){
                    $.each($scope.rawLabels.trim().split(/[\s*,]+/), function(index, element){
                        labels.push({
                            Name: element
                        })
                    });
                }

                return labels;
            }

            (function (projectId){
                loadIssueData(projectId);
            })($scope.projectId);
        }
    ]);
