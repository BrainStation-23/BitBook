"use strict";

(function(app) {
    app.controller("PendingFriendRequestCtrl", [
        "$scope", "$location", "identityService", "notifierService", "apiService", function($scope, $location, identityService, notifierService, apiService) {
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
        }
    ]);
})(_$.app);