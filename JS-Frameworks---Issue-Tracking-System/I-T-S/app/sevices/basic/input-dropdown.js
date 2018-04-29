'use strict';

angular.module('issueTracker.input-dropdown', [])
    .factory('inputDrop',['$q',
        function($q){
            var filterInput = function(availableOptions, userInput) {
                var a = availableOptions.filter(filterUsernames(userInput));
                return $q.when(a);
            };

            function filterUsernames(userInput) {
                return function(element) {
                    return element.readableName.toLowerCase().indexOf(userInput) === 0;
                };
            };

            var transformUserDataForInputDropdown  = function (usersData){
                var output = [];

                for (var index in usersData) {
                    output.push({
                        readableName: usersData[index].Username,
                        userId: usersData[index].Id,
                        id: index
                    });
                }

                return output;
            };

            var transformUserDataForProjectInputDropdown  = function (usersData){
                var output = [];

                for (var index in usersData) {
                    output.push({
                        readableName: usersData[index].Name,
                        projectId: usersData[index].Id,
                        id: index
                    });
                }

                return output;
            };

            return {
                filterInput: filterInput,
                transformUserDataForInputDropdown: transformUserDataForInputDropdown,
                transformUserDataForProjectInputDropdown: transformUserDataForProjectInputDropdown
            }
        }]);