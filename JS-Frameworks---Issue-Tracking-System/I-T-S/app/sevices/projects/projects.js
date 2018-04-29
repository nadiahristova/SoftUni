'use strict';

angular.module('issueTracker.projects', [])
    .factory('projects', [
            '$http',
            '$localStorage',
            '$q',
            '$location',
            'identity',
            'BASE_URL',
            function($http, $localStorage, $q, $location, identity, BASE_URL){
                function addProject(projectInfo){
                    var deffered = $q.defer();

                    $http.post(BASE_URL + 'Projects', projectInfo).
                    then(function (response){
                        deffered.resolve(response.data);
                    });

                    return deffered.promise;
                };

                function retrieveProjects(pageSize, pageNumber, filter){
                    pageNumber = pageNumber || 1;
                    pageSize = pageSize || 3;
                    filter = filter || '';

                    var deferred = $q.defer();

                    $http.get(BASE_URL + 'Projects/?pageSize=' + pageSize + '&pageNumber=' + pageNumber + '&filter=' + filter)
                        .then(function(projects){
                            deferred.resolve(projects.data);
                        });

                    return deferred.promise;
                }

                function getProjectById(id){
                    var deffered = $q.defer();

                    $http.get(BASE_URL + 'Projects/'+ id)
                        .then(function (response){
                            deffered.resolve(response.data);
                        });

                    return deffered.promise;
                };



                function editProject(projectId, ptoject){
                    var deffered = $q.defer();

                    $http.put(BASE_URL + 'Projects/'+ projectId, ptoject)
                        .then(function (response){
                            deffered.resolve(response.data);
                        });

                    return deffered.promise;
                };

                return {
                    addProject: addProject,
                    retrieveProjects: retrieveProjects,
                    getProjectById: getProjectById,
                    editProject: editProject
                }

            }]);
