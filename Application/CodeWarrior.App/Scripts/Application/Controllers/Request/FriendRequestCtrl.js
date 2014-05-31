"use strict";

(function(app) {
    app.controller("FriendRequestCtrl", [
        "$scope", "apiService", "friendService", "identityService", function ($scope, apiService, friendService, identityService) {
            $scope.users = [];

            var removeUser = function(user) {
                $scope.users = _.select($scope.users, function(u) {
                    return user.id != u.id;
                });
            };

            var getConfig = function() {
                return {
                    headers: identityService.getSecurityHeaders(),
                };
            };

            $scope.rejectFriend = function (user) {
                friendService.unFriend(user).success(function() {
                    removeUser(user);
                });
            };

            $scope.addFriend = function (user) {
                friendService.addFriend(user).success(function() {
                    removeUser(user);
                });
            };

            $scope.init = function() {
                apiService.get('/api/friends/requests/', getConfig()).success(function(result) {
                    $scope.users = result;
                });
            }();
        }
    ]);
})(_$.app);