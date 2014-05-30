"use strict";

(function(app) {
    app.controller("AccountFriendListCtrl", [
        "$scope", "$location", "identityService", "notifierService", "apiService", function($scope, $location, identityService, notifierService, apiService) {
            $scope.init = function() {
                if (!identityService.isLoggedIn()) {
                    $scope.redirectToLogin();
                } else {
                    var config = {
                        headers: identityService.getSecurityHeaders()
                    };
                    apiService.get("/api/friend", config).success(function(data) {
                        $scope.friends = data;
                        notifierService.notify({ responseType: "success", message: "Record fetched successfully." });
                    });
                }
            }();
        }
    ]);
})(_$.app);