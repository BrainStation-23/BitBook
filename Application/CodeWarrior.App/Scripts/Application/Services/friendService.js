"use strict";

(function (app) {
    app.factory("friendService", [
        "$rootScope", "identityService", "apiService","signalRConnectionService",
        function ($rootScope, identityService, apiService, signalRConnectionService) {
            var signalRConnection = signalRConnectionService.getSignalRConnection();
            var getConfig = function () {
                return {
                    headers: identityService.getSecurityHeaders(),
                };
            };

            var unFriend = function (user) {
                var config = $.extend(getConfig(), {
                    params: {
                        id: user.id
                    }
                });

                return apiService.remove('/api/friends/', config);
            };

            var addFriend = function (user) {
                var config = $.extend(getConfig(), {
                    params: {
                        id: user.id
                    }
                });
                
                var promise = apiService.post('/api/friends/', {}, config);
                promise.success(function () {
                    var notificationMesage =  user.firstName + " " + user.lastName + "</a> send you a friend request.";
                    signalRConnection.server.addFriendNotification(notificationMesage, user.id);
                });
                return promise;
            };

            return {
                addFriend: addFriend,
                unFriend: unFriend
            };
        }
    ]);
})(_$.app);