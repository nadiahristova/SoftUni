'use strict';

angular.module('issueTracker.dashboard.issuesDisplay', [])
    .factory('issuesDisplay', [
        '$http',
        '$q',
        'BASE_URL',
        function($http, $q, BASE_URL){

        function userIssues(pageSize, pageNumber, orderBy){
            pageNumber = pageNumber || 1;
            pageSize = pageSize || 3;
            orderBy = orderBy || 'DueDate';

            var deferred = $q.defer();

            $http.get(BASE_URL + 'Issues/me?pageSize=' + pageSize + '&pageNumber=' + pageNumber + '&orderBy=' + orderBy)
                .then(function(issues){
                    deferred.resolve(issues.data);
                });

            return deferred.promise;
        }

        function issuesByFilter(pageSize, pageNumber, filter){
            pageNumber = pageNumber || 1;
            pageSize = pageSize || 7;

            var deferred = $q.defer();

            $http.get(BASE_URL + 'Issues/?pageSize=' + pageSize + '&pageNumber=' + pageNumber + '&filter=' + filter)
                .then(function(issues){
                    deferred.resolve(issues.data);
                });

            return deferred.promise;
        }

        function editIssue(issueId, issue){
            var deferred = $q.defer();

            $http.put(BASE_URL + 'Issues/' + issueId, issue)
                .then(function(issue){
                    deferred.resolve(issue.data);
                });

            return deferred.promise;
        }

        function getIssue(issueId){
            var deferred = $q.defer();

            $http.get(BASE_URL + 'Issues/' + issueId)
                .then(function(issue){
                    deferred.resolve(issue.data);
                });

            return deferred.promise;
        }

        function createIssue(issue){
            var deferred = $q.defer();

            $http.post(BASE_URL + 'Issues/', issue)
                .then(function(success){
                    deferred.resolve(success.data);
                });

            return deferred.promise;
        }

        function changeIssueStatus(issueId, statusId){
            var deferred = $q.defer();
            $http.put(BASE_URL + 'issues/' + issueId + '/changestatus?statusId=' + statusId)
                .then(function(success){
                    deferred.resolve(success.data);
                });

            return deferred.promise;
        }

        return {
            userIssues: userIssues,
            issuesByFilter: issuesByFilter,
            getIssue: getIssue,
            editIssue: editIssue,
            createIssue: createIssue,
            changeIssueStatus: changeIssueStatus
        }
    }]);
