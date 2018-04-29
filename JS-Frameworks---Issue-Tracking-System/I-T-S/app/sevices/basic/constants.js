'use strict';

angular.module('issueTracker.constants', [])
    .factory('constants',
        function(){
            var returnPriorities = function() {
                return [{
                    Id: 1,
                    Name: "Low"
                     }, {
                        Id: 6,
                        Name: "High"
                    }, {
                        Id: 2,
                        Name: "Urgent"
                    },  {
                        Id: 3,
                        Name: "Danger"
                    }]
            };

            var returnStatuses = function() {
                return [{
                    Id: 1,
                    Name: "Closed"
                },
                {
                    Id: 2,
                    Name: "Open"
                },
                {
                    Id: 3,
                    Name: "In Progress"
                },
                {
                    Id: 4,
                    Name: "Stopped Progress"
                }]
            };

            var returnRawData = function(arr){
                var data = null;

                if(arr && arr.length > 0){
                    data = jQuery.map( arr, function(a) {
                        return a.Name;
                    }).join(', ');
                }

                return data;
            };

            var convertDataToArray = function(value){
                var arr = [];
                if(value && value.length > 0){
                    arr = value.split(/[,\s*]+/);
                    arr.forEach(function(part, index, arr) {
                        arr[index] = { Name : part };
                    });
                }

                return arr;
            };

            var genProjectKey = function(projectName){
                var projectKeys = undefined;
                if(projectName && projectName.length > 0){
                    var matches = projectName.match(/\b(\w)/g);
                    projectKeys = matches.join('').toUpperCase();
                }

                return projectKeys;
            };

            return {
                returnPriorities: returnPriorities,
                returnStatuses: returnStatuses,
                returnRawData: returnRawData,
                convertDataToArray: convertDataToArray,
                genProjectKey: genProjectKey
            }
});