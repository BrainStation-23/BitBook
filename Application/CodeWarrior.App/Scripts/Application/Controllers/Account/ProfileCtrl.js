"use strict";

(function(app) {
    app.controller("ProfileCtrl", [
        "$scope", "$location", "identityService", "notifierService", "apiService", function($scope, $location, identityService, notifierService, apiService) {
            $scope.init = function() {
                if (!identityService.isLoggedIn()) {
                    $scope.redirectToLogin();
                } else {
                    var config = {
                        headers: identityService.getSecurityHeaders()
                    };
                    apiService.get("/api/profile", config).success(function(data) {
                        $scope.user = data;
                        notifierService.notify({ responseType: "success", message: "Profile data fetched successfully." });
                    });
                }
            }();
        }
    ]);
})(_$.app);