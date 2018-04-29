angular.module('issueTracker.issues.display',
    [
        'issueTracker.dashboard.issuesDisplay',
        'issueTracker.constants',
        'issueTracker.projects',
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

        $routeProvider.when('/issues/:id', {
            templateUrl:'app/views/issues/display-issue.html',
            controller:'DisplayIssueController',
            resolve: routeChecks.authenticated
        })
    }])
    .controller('DisplayIssueController',[
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
        'usSpinnerService',
        'inputDrop',
        function($scope, $localStorage, $routeParams, $location, $q, $, projects, issuesDisplay, identity, constants, notification, usSpinnerService, inputDrop){
            $scope.isCurrUserAdmin = $localStorage.isAdmin;
            $scope.userID = $localStorage.currUserId;

            $scope.issueId = $routeParams.id;

            var loadIssueData = function (issueId){

                issuesDisplay.getIssue(issueId)
                    .then(function(issue){
                        $scope.assigneeId = issue.Assignee.Id;
                        $scope.projectLeader = issue.Author.Id;
                        $scope.labels = constants.returnRawData(issue.Labels);

                        $scope.issue = issue;

                        $scope.statuses = constants.returnStatuses();
                        $scope.currStatus =  $scope.statuses.filter(function(element){
                            return element.Id === issue.Status.Id;
                        })[0];

                        $scope.str = inputDrop.transformUserDataForProjectInputDropdown(constants.returnStatuses());
                        $scope.st = $scope.str.filter(function(element){
                            return element.id === issue.Status.Id;
                        })[0];

                        $scope.str = inputDrop.transformUserDataForProjectInputDropdown(issue.AvailableStatuses);

                        $scope.statuses = issue.AvailableStatuses;
                        $scope.statuses.push($scope.currStatus);
                    });
            };

            $scope.changeIssueStatus = function(){
                usSpinnerService.spin('spinner');
                var a = $scope.str;
                var b = $scope.st;

                issuesDisplay.changeIssueStatus($scope.issueId, $scope.currStatus.Id)
                    .then(function(success){
                        notification.success("Issue status was successfully updated.");
                        usSpinnerService.stop('spinner');
                    });
            };

            (function (issueId){
                loadIssueData(issueId);
            })($scope.issueId);
        }
    ]);
