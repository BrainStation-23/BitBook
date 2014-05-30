"use strict";

(function(app) {
    app.controller("HomeCtrl", [
        "$scope", "identityService", function($scope, identityService) {
            $scope.init = function() {
                if (!identityService.isLoggedIn()) {
                    $scope.redirectToLogin();
                }
            }();
        }
    ]);
})(_$.app);