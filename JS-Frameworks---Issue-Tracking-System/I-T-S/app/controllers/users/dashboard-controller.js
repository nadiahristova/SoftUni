angular.module('issueTracker.dashboard',
    ['issueTracker.dashboard.issuesDisplay', 'issueTracker.projects', 'ui.bootstrap'])
    .config(['$routeProvider', function($routeProvider){
        var routeChecks = {
            authenticated: ['$q', 'authentication', function($q, authentication){
                if (authentication.isAuthenticated()){
                    return $q.when(true);
                }

                return $q.reject('Unauthorised Access!');
            }]
        };

        $routeProvider.when('/dashboard', {
            templateUrl:'app/views/users/dashboard.html',
            controller:'DashboardController',
            resolve: routeChecks.authenticated
        })
    }])
    .controller('DashboardController',[
        '$scope',
        '$q',
        '$localStorage',
        'jQuery',
        'issuesDisplay',
        'projects',
        'usSpinnerService',
        function($scope, $q, $localStorage, $, issuesDisplay, projects, usSpinnerService){
            var affiliatedProjectIds = {};
            var projectFilter = 'Lead.Id=="' + $localStorage.currUserId + '"';

            $scope.isCurrUserAdmin = $localStorage.isAdmin;
            $scope.userID = $localStorage.currUserId;

            $scope.maxSize = 5;
            $scope.numPerPage = 5;
            $scope.currentPageIssues = 1;
            $scope.currentPageProjects = 1;

            $scope.orderedByTitleDesc = false;
            $scope.orderedByDescriptionDesc = false;
            $scope.orderedByProjectDesc = false;
            $scope.orderedByDueDateDesc = true;

            $scope.pageChangedIssues = function (numPerPage, currentPage, orderBy, orderDesc){

                if(orderDesc){
                    orderBy += ' desc';
                }

                issuesDisplay.userIssues(numPerPage, currentPage, orderBy)
                    .then(function(issues){
                        console.log(issues);
                        $scope.ownIssues = issues.Issues;
                        $scope.totalCountIssues = issues.TotalCount;
                        $scope.totalPagesIssues = issues.TotalPages;
                    });
            };

            $scope.pageChangedProjects = function (numPerPage, currentPage){
                projects.retrieveProjects(numPerPage, currentPage, projectFilter)
                    .then(function(projects){
                        $scope.affilProjects = projects.Projects;
                        $scope.totalCountProjects = projects.TotalCount;
                        $scope.totalPagesProjects = projects.TotalPages;
                    });
            };

            var setContainsValue = function(value){
                return affiliatedProjectIds[value] === true;
            };

            var loadAdditionalProjectIds = function(){
                var deffered = $q.defer();
                issuesDisplay.userIssues(2000, 1)
                    .then(function(issues){
                        $.each(issues.Issues, function(index, issue){
                            var projectId = issue.Project.Id;
                            if(!setContainsValue(projectId)){
                                affiliatedProjectIds[projectId] = true;
                            }
                        });

                        deffered.resolve(true);
                    });

                return deffered.promise;
            };

            var extractAdditionalProjectIds = function(){
                var deffered = $q.defer();
                projects.retrieveProjects(1000, 1, projectFilter)
                    .then(function(projects){
                        $.each(projects.Projects, function(index, project){
                            if(setContainsValue(project.Id)){
                                affiliatedProjectIds[project.Id] = false;
                            }
                        });

                        deffered.resolve(true);
                    });

                return deffered.promise;
            };

            var loadInitialData = function(numPerPage, currentPage){

                usSpinnerService.spin('spinner');

                loadAdditionalProjectIds()
                    .then(function(success){
                        extractAdditionalProjectIds().then(function(success){
                            $.each(affiliatedProjectIds, function(id, value){
                                if(value === true){
                                    projectFilter += ' or Id==' + id;
                                }
                            });

                            $scope.pageChangedIssues(numPerPage, currentPage);
                            $scope.pageChangedProjects(numPerPage, currentPage);

                            usSpinnerService.stop('spinner');
                        });
                    });
            };

            (function (numPerPage, currentPage){
                loadInitialData(numPerPage, currentPage);
                console.log($scope);
            })($scope.numPerPage, $scope.currentPage);
        }
    ]);
