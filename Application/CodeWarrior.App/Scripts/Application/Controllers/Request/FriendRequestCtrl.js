"use strict";

(function (app) {
    app.controller("FriendRequestCtrl", [
        "$scope", "apiService", "identityService", function ($scope, apiService, identityService) {
            $scope.users = [];

            var getConfig = function () {
                return {
                    headers: identityService.getSecurityHeaders(),
                };
            };

            $scope.addFriend = function (user) {
                var config = $.extend(getConfig(), {
                    params: {
                        id: user.id
                    }
                });

                apiService.post('/api/friend/', {}, config)
                        .success(function () {
                            user.friendRequestSent = true;
                        });
            };

            $scope.init = function () {
                apiService.get('/api/friend/requests/', getConfig()).success(function (result) {
                    $scope.users = result;
                }).error(function (error) {
                    console.log(error);
                });
            }();
        }
    ]);
})(_$.app);