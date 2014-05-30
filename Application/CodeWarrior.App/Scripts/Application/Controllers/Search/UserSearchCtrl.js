"use strict";

(function(app) {
    app.controller("UserSearchCtrl", [
        "$scope", "apiService", "identityService", "$routeParams", "notifierService",
        function($scope, apiService, identityService, $routeParams, notifierService) {
            $scope.users = [];

            var getConfig = function() {
                return {
                    headers: identityService.getSecurityHeaders(),
                };
            };

            $scope.addFriend = function(user) {
                if (user.friendRequestSent) {
                    notifierService.notify({responseType: 'warning', message: 'Friend request alredy sent!'});
                } else {
                    var config = $.extend(getConfig(), {
                        params: {
                            id: user.id
                        }
                    });
                    apiService.post('/api/friend/', {}, config)
                        .success(function() {
                            user.friendRequestSent = true;
                        });
                }
            };

            $scope.init = function() {
                if ($routeParams.key) {
                    var config = $.extend(getConfig(), {
                        params: {
                            type: 'user',
                            key: $routeParams.key
                        }
                    });

                    apiService.get('/api/Search/', config).success(function(result) {
                        $scope.users = result;
                    }).error(function(error) {
                        console.log(error);
                    });
                }
            }();
        }
    ]);
})(_$.app);