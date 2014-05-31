"use strict";

(function(app) {
    app.controller("UserSearchCtrl", [
        "$scope", "apiService", "identityService", "$routeParams", "notifierService", "friendService",
        function ($scope, apiService, identityService, $routeParams, notifierService, friendService) {
            $scope.users = [];

            var getConfig = function() {
                return {
                    headers: identityService.getSecurityHeaders(),
                };
            };

            $scope.addFriend = function(user) {
                if (user.friendRequestSent) {
                    notifierService.notify({ responseType: 'warning', message: 'Friend request alredy sent!' });
                } else {
                    friendService.addFriend(user).success(function() {
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
                        //console.log(error);
                    });
                }
            }();
        }
    ]);
})(_$.app);