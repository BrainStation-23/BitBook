"use strict";

(function (app) {
    app.factory("friendService", [
        "$rootScope", "identityService", "apiService",
        function ($rootScope, identityService, apiService) {
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

                return apiService.post('/api/friends/', {}, config);
            };

            return {
                addFriend: addFriend,
                unFriend: unFriend
            };
        }
    ]);
})(_$.app);