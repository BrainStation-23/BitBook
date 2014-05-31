"use strict";

(function(app) {
    app.controller("AccountFriendListCtrl", [
        "$scope", "$location", "identityService", "notifierService", "apiService", "friendService", function ($scope, $location, identityService, notifierService, apiService, friendService) {
            $scope.init = function() {
                if (!identityService.isLoggedIn()) {
                    $scope.redirectToLogin();
                } else {
                    var config = {
                        headers: identityService.getSecurityHeaders()
                    };
                    apiService.get("/api/friends", config).success(function(data) {
                        $scope.friends = data;
                        notifierService.notify({ responseType: "success", message: "Record fetched successfully." });
                    });
                }
            }();

            $scope.toggleFriend = function (user) {
                if (user.isMyFriend) {
                    friendService.unFriend(user).success(function () {
                        user.isMyFriend = false;
                        user.isFriendRequestedRejected = true;
                        user.isFriendActionDisabled = true;
                        notifierService.notify({ responseType: "success", "message": "Friend removed successfully!" });
                    });
                } else {
                    friendService.addFriend(user).success(function () {
                        user.isFriendRequestSent = true;
                        user.isFriendActionDisabled = true;
                        notifierService.notify({ responseType: "success", "message": "Friend added successfully!" });
                    });
                }
            };
        }
    ]);
})(_$.app);