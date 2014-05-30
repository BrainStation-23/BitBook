"use strict";

(function(app) {
    app.controller("ProfileCtrl", [
        "$scope", "$location", "identityService", "notifierService", function ($scope, $location, identityService, notifierService) {
            $scope.init = function() {
                if (!identityService.isLoggedIn()) {
                    $location.path("/");
                } else {
                    identityService.getUserInfo().success(function (data) {
                        $scope.user = data;
                        notifierService.notify({ responseType: "success", message: "Profile data fetched successfully." });
                    });
                }
            }();
        }
    ]);
})(_$.app);