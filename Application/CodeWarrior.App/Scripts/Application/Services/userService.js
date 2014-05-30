"use strict";

(function(app) {
    app.factory("userService", [
        "$rootScope", "$http", "identityService", "apiService",
        function($rootScope, $http, identityService, apiService) {
            function searchUser(key) {
                var config = {
                    headers: identityService.getSecurityHeaders(),
                    params: {
                        type: 'user',
                        key: key
                    }
                };

                return apiService.get('api/Search/', config);
            };

            return {
                searchUser: searchUser
            };
        }
    ]);
})(_$.app);