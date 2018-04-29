'use strict';

angular.module('issueTracker.notifications', [ ])
    .factory('notification',['toastr',
        function(toastr){
            return {
                success: function(msg){
                    toastr.success(msg,'',{
                        closeButton: true,
                        positionClass: 'toast-top-center',
                        timeOut: 2500,
                        extendedTimeOut: 1000
                    });
                },
                info: function(msg){
                    toastr.info(msg,'',{
                        closeButton: true,
                        positionClass: 'toast-top-center'
                    });
                },
                error: function(msg){
                    toastr.error(msg,'',{
                        closeButton: true,
                        positionClass: 'toast-top-center'
                    });
                }
            }
        }]);
