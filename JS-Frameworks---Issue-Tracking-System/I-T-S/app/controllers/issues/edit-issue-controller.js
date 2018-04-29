angular.module('issueTracker.issues.edit',
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

        $routeProvider.when('/issues/:id/edit', {
            templateUrl:'app/views/issues/edit-issue.html',
            controller:'EditIssueController',
            resolve: routeChecks.authenticated
        })
    }])
    .controller('EditIssueController',[
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
        'inputDrop',
        'notification',
        'usSpinnerService',
        function($scope, $localStorage, $routeParams, $location, $q, $, projects, issuesDisplay, identity, constants, inputDrop, notification, usSpinnerService){
            $scope.isCurrUserAdmin = $localStorage.isAdmin;
            $scope.userID = $localStorage.currUserId;

            var checkIdentity = function(authorId, assigneeId){
                if(!($scope.isCurrUserAdmin || $scope.userID === authorId || $scope.userID === assigneeId)) {
                    $location.path('/dashboard');
                }
            };

            $scope.issueId = $routeParams.id;

            $scope.selectedDropdownItem = null;
            $scope.rawLabels = null;

            var loadIssueData = function (projectId){
                issuesDisplay.getIssue(projectId)
                    .then(function(issue){
                        $scope.issue = issue;

                        checkIdentity(issue.Author.Username, issue.Assignee.Username);
                    }).then(function(success){
                        identity.getUserData()
                            .then(function (users) {
                                $scope.availableOptions = inputDrop.transformUserDataForInputDropdown(users);
                                $scope.selectedDropdownItem = $scope.availableOptions.filter(function(element){
                                    return element.userId === $scope.issue.Assignee.Id;
                                })[0];

                                $scope.priorities = constants.returnPriorities();
                                $scope.statuses = constants.returnStatuses();

                                $scope.currPriority =  $scope.priorities.filter(function(element){
                                    return element.Id === $scope.issue.Priority.Id;
                                })[0];
                                $scope.currStatus =  $scope.statuses.filter(function(element){
                                    return element.Id === $scope.issue.Status.Id;
                                })[0];
                                $scope.rawLabels = constants.returnRawData($scope.issue.Labels);
                     });
                });
            };

            $scope.filterInput = function(userInput){
                return inputDrop.filterInput($scope.availableOptions, userInput)
            };

            $scope.editIssue = function(){
                usSpinnerService.spin('spinner');

                var i = $scope.issue;
                var labels = constants.convertDataToArray($scope.rawLabels);

                var issueEntity = {
                    Title: i.Title,
                    Description: i.Description,
                    DueDate: i.DueDate,
                    StatusId: $scope.currStatus.Id,
                    PriorityId: $scope.currPriority.Id,
                    AssigneeId: $scope.selectedDropdownItem.userId,
                    Labels: labels
                };

                issuesDisplay.editIssue($scope.issueId, issueEntity)
                    .then(function(success){
                        notification.success("Issue was successfully updated.");
                        usSpinnerService.stop('spinner');
                    });
            };

            (function (issueId){
                loadIssueData(issueId);
            })($scope.issueId);
        }
    ]);
