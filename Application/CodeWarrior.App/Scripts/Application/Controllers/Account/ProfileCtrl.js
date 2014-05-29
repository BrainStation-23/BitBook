"use strict";

(function(app) {
    app.controller("ProfileCtrl", [
        "$scope", "identityService", "notifierService", function($scope, identityService, notifierService) {
            $scope.init = function() {
                identityService.getUserInfo().success(function(data) {
                    $scope.user = data;
                    notifierService.notify({ responseType: "success", message: "Profile data fetched successfully." });
                });
            }();
        }
    ]);
})(_$.app);