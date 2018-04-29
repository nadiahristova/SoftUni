angular.module('issueTracker.display.projects', ['ui.bootstrap', 'issueTracker.dashboard.issuesDisplay'])
    .config(['$routeProvider', function($routeProvider){
        var routeChecks = {
            authenticated: ['$q', 'authentication', function($q, authentication){
                if (authentication.isAuthenticated()){
                    return $q.when(true);
                }

                return $q.reject('Unauthorised Access!');
            }]
        };

        $routeProvider.when('/projects/:id', {
            templateUrl:'app/views/projects/display-project.html',
            controller:'ViewProjectController',
            resolve: routeChecks.authenticated
        })
    }])
    .controller('ViewProjectController',[
        '$scope',
        '$localStorage',
        '$routeParams',
        'jQuery',
        'projects',
        'issuesDisplay',
        function($scope, $localStorage, $routeParams, $, projects, issuesDisplay){
            $scope.isCurrUserAdmin = $localStorage.isAdmin;
            $scope.userID = $localStorage.currUserId;

            $scope.projectID = $routeParams.id;
            $scope.toggled= true;
            $scope.numPerPage = 5;
            $scope.currentPageIssues = 1;
            $scope.maxSize = 3;

            $scope.pageChangedIssues = function (numPerPage, currentPage){

                var filter = 'Project.Id==' + $scope.projectID;

                issuesDisplay.issuesByFilter(numPerPage, currentPage, filter)
                    .then(function(issues){
                        $scope.ownIssues = issues.Issues;
                        $scope.totalCountIssues = issues.TotalCount;
                        $scope.totalPagesIssues = issues.TotalPages;
                    });
            };

            $scope.loadProjectData = function (projectId){
                projects.getProjectById(projectId)
                    .then(function(project){
                        $scope.project = project;
                    });
            };

            (function (projectID, numPerPage, currentPage){
                $scope.loadProjectData(projectID);
                $scope.pageChangedIssues(numPerPage, currentPage);
            })($scope.projectID, $scope.numPerPage, $scope.currentPage);

            $(".rotate").click(function(){
               $(this).children('.fa').toggleClass("down");
            });
        }
    ]);

