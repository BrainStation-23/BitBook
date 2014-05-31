"use strict";

(function(app) {
    app.controller("FriendRequestCtrl", [
        "$scope", "apiService", "identityService", function($scope, apiService, identityService) {
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

            $scope.rejectFriend = function(user) {
                var config = $.extend(getConfig(), {
                    params: {
                        id: user.id
                    }
                });

                apiService.remove('/api/friend/', config)
                    .success(function() {
                        removeUser(user);
                    });
            };

            $scope.addFriend = function(user) {
                var config = $.extend(getConfig(), {
                    params: {
                        id: user.id
                    }
                });

                apiService.post('/api/friend/', {}, config)
                    .success(function() {
                        removeUser(user);
                    });
            };

            $scope.init = function() {
                apiService.get('/api/friend/requests/', getConfig()).success(function(result) {
                    $scope.users = result;
                }).error(function(error) {
                    console.log(error);
                });
            }();
        }
    ]);
})(_$.app);