"use strict";

(function(app) {
    app.controller("PendingFriendRequestCtrl", [
        "$scope", "$location", "identityService", "notifierService", "apiService", "friendService", function ($scope, $location, identityService, notifierService, apiService, friendService) {
            $scope.init = function() {
                if (!identityService.isLoggedIn()) {
                    $scope.redirectToLogin();
                } else {
                    $scope.pendingRequestsFetchInProgress = true;
                    var config = {
                        headers: identityService.getSecurityHeaders()
                    };
                    apiService.get("/api/friends/requests", config).success(function (data) {
                        $scope.pendingRequests = data;
                        $scope.pendingRequestsFetchInProgress = false;
                    }).error(function() {
                        notifierService.notify({responseType: "error", message: "Oops! Something happend."});
                    });
                }
            }();

            $scope.addFriend = function (user) {
                if (user.isMyFriend) {
                    friendService.unFriend(user).success(function () {
                        user.isMyFriend = false;
                        user.isFriendRequestedRejected = true;
                        user.isFriendActionDisabled = true;
                        notifierService.notify({ responseType: "success", "message": "Friend rejected successfully!" });
                    });
                } else {
                    friendService.addFriend(user).success(function () {
                        user.isFriendRequestSent = true;
                        user.isFriendActionDisabled = true;
                        notifierService.notify({responseType: "success", "message": "Friend added successfully!"});
                    });
                }
            };
        }
    ]);
})(_$.app);